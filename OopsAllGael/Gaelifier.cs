using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;
using OopsAllGael.EventEnum;

namespace OopsAllGael
{
    class Gaelifier
    {
        
        static Dictionary<int, PARAM.Row> npc_params = new Dictionary<int, PARAM.Row>();  // npc param id => npc params
        static HashSet<int> entityIds = new HashSet<int>();
        static HashSet<uint> visitedEvents = new HashSet<uint>();
        static HashSet<uint> visitedCommonEvents = new HashSet<uint>();
        static Dictionary<string, List<int>> enemyEntityIdMap = new Dictionary<string, List<int>>();

        public static void Gaelify()
        {
            Directory.CreateDirectory(@"mod\event");
            Directory.CreateDirectory(@"mod\script");
            Directory.CreateDirectory(@"mod\map\mapstudio");
            Directory.CreateDirectory(@"mod\sfx");

            InsertParams();
            InsertEnemies();
            ModifyEvents();
            InsertScripts();
            InsertSfx();
        }

        static void InsertScripts()
        {
            // Extract Gael scripts
            BND4 gael_bnd = BND4.Read(@"script\m51_01_00_00.luabnd.dcx");
            List<BinderFile> gael_scripts = new List<BinderFile>();
            BinderFile tmp_bndf = gael_bnd.Files.Find(bndf => bndf.Name.EndsWith("620100_battle.lua")) ?? throw new KeyNotFoundException();
            gael_scripts.Add(tmp_bndf);

            // Modify Gael's phase 3 health threshold
            tmp_bndf = gael_bnd.Files.Find(bndf => bndf.Name.EndsWith("620000_battle.lua")) ?? throw new KeyNotFoundException();
            BitConverter.GetBytes(0.42 / 0.65).CopyTo(tmp_bndf.Bytes, 0x667);
            gael_scripts.Add(tmp_bndf);

            // Copy scripts to other binderfiles
            foreach (string mapId in Constants.MAP_IDS)
            {
                string filename = @"script\" + mapId + ".luabnd.dcx";
                BND4 bnd = BND4.Read(filename);
                bnd.Files.InsertRange(0, gael_scripts);
                bnd.Write(Constants.MOD_DIR + filename);
            }
        }

        static void InsertEnemies()
        {
            TeamType[] enemy_types_to_replace =
            {
                TeamType.Hollow,
                TeamType.Enemy,
                TeamType.StrongEnemy,
                TeamType.DecoyEnemy,
                TeamType.Hostile,
                TeamType.Enemy1,
                TeamType.Enemy2,
                TeamType.StrongEnemy1,
                TeamType.HostileNpc,
                TeamType.Indiscriminate
            };

            // Extract Gael info
            MSB3 gael_msb = MSB3.Read(@"map\mapstudio\m51_01_00_00.msb.dcx");
            List<MSB3.Model.Enemy> gael_models = gael_msb.Models.Enemies.FindAll(model => model.Name == "c6201" || model.Name == "c6200");

            // Make every enemy Gael
            foreach (string mapId in Constants.MAP_IDS)
            {
                string msbFilename = @"map\mapstudio\" + mapId + ".msb.dcx";
                MSB3 msb = MSB3.Read(msbFilename);
                msb.Models.Enemies.AddRange(gael_models);
                List<MSB3.Part.Enemy> new_gaels = new List<MSB3.Part.Enemy>();

                foreach (MSB3.Part.Enemy enemy in msb.Parts.Enemies)
                {
                    TeamType team = (TeamType)Util.GetCellValue<byte>(npc_params[enemy.NPCParamID], "subTeamType");
                    if (enemy.EntityID == -1)// || Array.FindIndex(enemy_types_to_replace, t => t == team) == -1)
                    {
                        continue;
                    }

                    enemy.ModelName = "c6201";
                    enemy.NPCParamID = Constants.MY_GAEL_PHASE1_PARAM_ID;
                    enemy.ThinkParamID = 620100;

                    // add a phase 2 Gael
                    MSB3.Part.Enemy new_gael = Util.CopyEnemy(enemy);
                    new_gael.EntityID = enemy.EntityID * 10;
                    new_gael.ModelName = "c6200";
                    new_gael.NPCParamID = Constants.MY_GAEL_PHASE2_PARAM_ID;
                    new_gael.Name = enemy.Name + " (2)";
                    new_gael.ThinkParamID = 620000;
                    new_gaels.Add(new_gael);

                    if (entityIds.Contains(enemy.EntityID))
                    {
                        Console.WriteLine("Warning: duplicate entity ID: " + enemy.EntityID + ", " + team);
                    }
                    else if (entityIds.Contains(enemy.EntityID * 10))
                    {
                        Console.WriteLine("Warning: gael2 entity ID collision: " + enemy.EntityID);
                    }
                    entityIds.Add(enemy.EntityID);
                    if (!enemyEntityIdMap.ContainsKey(mapId))
                    {
                        enemyEntityIdMap[mapId] = new List<int>();
                    }
                    enemyEntityIdMap[mapId].Add(enemy.EntityID);
                }

                msb.Parts.Enemies.AddRange(new_gaels);
                msb.Write(Constants.MOD_DIR + msbFilename);
            }
        }

        static void InsertCommonEvents(EMEVD emevd)
        {
            EventArg<int> gael_phase1 = new EventArg<int>();
            EventArg<int> gael_phase2 = new EventArg<int>();
            EMEVD.Event newEvent = new EventBuilder()
                .Event(Constants.GAEL_EVENT_ID, EMEVD.Event.RestBehaviorType.Restart)
                .Param(gael_phase1).Param(gael_phase2)

                // disable first Gael if second is dead
                .IfCharacterDeadAlive(ConditionGroup.Or01, gael_phase2, DeathState.Dead, ComparisonType.Equal, 1)
                .GotoIfConditionGroupStateUncompiled(Label.Label0, ConditionState.Fail, ConditionGroup.Or01)
                .ChangeCharacterEnableState(gael_phase1, DisabledEnabled.Disabled)
                .EndUnconditionally(EventEndType.End)

                // disable second gael until first is dead
                .Label0()
                .ChangeCharacterEnableState(gael_phase2, DisabledEnabled.Disabled)
                .IfCharacterHpRatio(ConditionGroup.Main, gael_phase1, ComparisonType.Equal, 0, ComparisonType.Equal, 1)
                .WarpCharacterAndCopyFloor(gael_phase2, TargetEntityType.Character, gael_phase1, 260, gael_phase1)
                .WaitFixedTimeSeconds(5)
                .ChangeCharacterEnableState(gael_phase2, DisabledEnabled.Enabled)
                .ForceAnimationPlayback(gael_phase2, 3036, false, true, false, 0, 1)
                .Build();

            emevd.Events.Add(newEvent);
        }

        static void ModifyEvents()
        {
            string commonFilename = @"event\common_func.emevd.dcx";
            EMEVD commonEmevd = EMEVD.Read(commonFilename);
            List<EMEVD.Event> commonEvents = new List<EMEVD.Event>(commonEmevd.Events);
            foreach (string mapId in Constants.MAP_IDS)
            {
                string filename = @"event\" + mapId + ".emevd.dcx";
                EMEVD emevd = EMEVD.Read(filename);
                Console.WriteLine(filename);

                List<EMEVD.Event> events = new List<EMEVD.Event>(emevd.Events);

                int i = events.FindIndex(ev => ev.ID == 0);
                EMEVD.Event startEvent = events[i];
                events.RemoveAt(i);

                i = events.FindIndex(ev => ev.ID == 50);
                EMEVD.Event resetEvent = events[i];
                events.RemoveAt(i);

                visitedEvents.Clear();

                EventBuilder startEventBuilder = new EventBuilder()
                    .Event(startEvent)
                    .ForEach(EventModifier(events, commonEvents));

                foreach (int entityId in enemyEntityIdMap[mapId])
                {
                    startEventBuilder.InitializeCommonEvent(Constants.GAEL_EVENT_ID, null, (uint)entityId, (uint)entityId * 10);
                }

                new EventBuilder()
                    .Event(resetEvent)
                    .ForEach(EventModifier(events, commonEvents));

                /* Only unreachable events should be left over so don't do anything with them
                foreach (EMEVD.Event event_ in events)
                {
                    EventBuilder eventBuilder = new EventBuilder()
                        .Event(event_);
                    eventBuilder.ForEach(InstrModifier(eventBuilder, new Dictionary<int, object>()));
                }
                */

                emevd.Write(Constants.MOD_DIR + filename);
            }
            /*
            foreach (EMEVD.Event event_ in commonEvents)
            {
                EventBuilder eventBuilder = new EventBuilder()
                    .Event(event_);
                eventBuilder.ForEach(InstrModifier(eventBuilder, new Dictionary<int, object>()));
            }
            */
            InsertCommonEvents(commonEmevd);
            commonEmevd.Write(Constants.MOD_DIR + commonFilename);
        }

        static Predicate<MyInstr> EventModifier(List<EMEVD.Event> events, List<EMEVD.Event> commonEvents)
        {
            return delegate (MyInstr myInstr)
            {
                EventArg<uint> eventId = null;
                EventArg<uint>[] parameters = null;
                EMEVD.Event match = null;
                if (myInstr is InstrWrapper.InitializeEvent instr)
                {
                    eventId = instr.eventId;
                    parameters = instr.parameters;
                    int i = events.FindIndex(ev => ev.ID == eventId.Value);
                    if (i != -1)
                    {
                        match = events[i];
                        events.RemoveAt(i);
                        visitedEvents.Add(instr.eventId.Value);
                    }
                    else if (!visitedEvents.Contains(eventId.Value))
                    {
                        Console.WriteLine("Warning: event ID " + eventId.Value + " not found");
                    }
                }
                else if (myInstr is InstrWrapper.InitializeCommonEvent instr2)
                {
                    eventId = instr2.eventId;
                    parameters = instr2.parameters;
                    int i = commonEvents.FindIndex(ev => ev.ID == eventId.Value);
                    if (i != -1)
                    {
                        match = commonEvents[i];
                        commonEvents.RemoveAt(i);
                        visitedCommonEvents.Add(instr2.eventId.Value);
                    }
                    else if (!visitedCommonEvents.Contains(eventId.Value))
                    {
                        Console.WriteLine("Warning: common event ID " + eventId.Value + " not found");
                    }
                }

                if (match != null)
                {
                    EventBuilder eventBuilder = new EventBuilder()
                        .Event(match);

                    Dictionary<int, object> paramMap = new Dictionary<int, object>();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        paramMap[i] = parameters[i].Value;
                    }
                    eventBuilder.ForEach(InstrModifier(eventBuilder, paramMap));
                }
                return false;
            };
        }

        static Predicate<MyInstr> InstrModifier(EventBuilder eventBuilder, Dictionary<int, object> paramMap)
        {
            return delegate (MyInstr myInstr)
            {
                bool ret = false;
                if (myInstr is InstrWrapper.ForceAnimationPlayback instr)
                {
                    if (IsGael(paramMap, 0, instr.entityId))
                    {
                        // looping animations are often used to "disable" enemies so replace with disabling AI
                        if (instr.shouldLoop.isParam ? Convert.ToBoolean(paramMap[2]) : instr.shouldLoop.Value)
                        {
                            eventBuilder.Remove();
                            eventBuilder.SetCharacterAiState(instr.entityId, DisabledEnabled.Disabled);
                        }
                        else
                        {
                            eventBuilder.Remove();
                            eventBuilder.SetCharacterAiState(instr.entityId, DisabledEnabled.Enabled);
                        }
                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterHpRatio instr2)
                {
                    if (IsGael(paramMap, 1, instr2.targetEntityId))
                    {
                        eventBuilder.Noop();
                    }
                    if (paramMap.ContainsKey(3))
                    {
                        Console.WriteLine("HP param");
                    }
                }
                return ret;
            };
        }


        static bool IsGael(Dictionary<int, object> paramMap, int argPos, EventArg<int> entityId)
        {
            return entityId.isParam ? entityIds.Contains(Convert.ToInt32(paramMap[argPos])) : entityIds.Contains(entityId.Value);
        }

        static void InsertSfx()
        {
            string filename = @"sfx\frpg_sfxbnd_commoneffects_effect.ffxbnd.dcx";
            BND4 common_sfx_bnd = BND4.Read(filename);
            BND4 gael_sfx_bnd = BND4.Read(@"sfx\frpg_sfxbnd_m51_effect.ffxbnd.dcx");
            common_sfx_bnd.Files.InsertRange(0, gael_sfx_bnd.Files);
            common_sfx_bnd.Write(Constants.MOD_DIR + filename);
        }

        static void InsertParams()
        {
            // load NPC params
            string filename = "Data0.bdt";
            BND4 bnd = SFUtil.DecryptDS3Regulation(filename);
            BinderFile tmp_bndf = bnd.Files.Find(bndf => bndf.Name.EndsWith("NpcParam.param")) ?? throw new KeyNotFoundException();
            PARAM param = PARAM.Read(tmp_bndf.Bytes);
            string paramdef_file = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "NPC_PARAM_ST.xml");
            PARAMDEF paramdef = PARAMDEF.XmlDeserialize(paramdef_file);
            param.ApplyParamdef(paramdef);

            // put params in a dictionary for later use
            foreach (PARAM.Row row in param.Rows)
            {
                npc_params[row.ID] = row;
            }

            // create a new entry for Gael phase 1 with modified parameters
            PARAM.Row gael_row = param.Rows.Find(row => row.ID == Constants.GAEL_PHASE1_PARAM_ID) ?? throw new KeyNotFoundException();
            PARAM.Row new_row = Util.CopyRow(gael_row, paramdef);
            new_row.ID = Constants.MY_GAEL_PHASE1_PARAM_ID;
            new_row.Name = "gael1";
            Util.InsertRow(param, new_row);

            // create a new entry for Gael phase 2 with modified parameters
            gael_row = param.Rows.Find(row => row.ID == Constants.GAEL_PHASE2_PARAM_ID) ?? throw new KeyNotFoundException();
            new_row = Util.CopyRow(gael_row, paramdef);
            new_row.ID = Constants.MY_GAEL_PHASE2_PARAM_ID;
            new_row.Name = "gael2";
            Util.ModifyCellValue(new_row, "Hp", (int value) => (int) Math.Ceiling(value * 0.65));
            Util.SetCellValue(new_row, "getSoul", 120000);
            Util.SetCellValue(new_row, "isSoulGetByBoss", true);
            Util.SetCellValue(new_row, "disableIntiliazeDead", false);
            Util.InsertRow(param, new_row);

            tmp_bndf.Bytes = param.Write();

            SFUtil.EncryptDS3Regulation(Constants.MOD_DIR + filename, bnd);
        }
    }
}
