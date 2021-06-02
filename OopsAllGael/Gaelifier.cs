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
    partial class Gaelifier
    {

        static Dictionary<int, PARAM.Row> npc_params = new Dictionary<int, PARAM.Row>();  // npc param id => npc params


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

        static void InsertCommonEvents(EMEVD emevd)
        {
            EventArg<int> gael_phase1 = new EventArg<int>();
            EventArg<int> gael_phase2 = new EventArg<int>();
            EMEVD.Event newEvent = new EventBuilder()
                .Event(Constants.GAEL_EVENT_ID, EMEVD.Event.RestBehaviorType.Restart)
                .Param(gael_phase1).Param(gael_phase2)

                // disable first Gael if second is dead
                .WaitFixedTimeFrames(1)
                .IfCharacterDeadAlive(ConditionGroup.Or01, gael_phase2, DeathState.Dead, ComparisonType.Equal, 1)
                .GotoIfConditionGroupStateUncompiled(Label.Label0, ConditionState.Fail, ConditionGroup.Or01)
                .ChangeCharacterEnableState(gael_phase1, DisabledEnabled.Disabled)
                .ActivateHit(gael_phase1, DisabledEnabled.Disabled)
                .EndUnconditionally(EventEndType.End)

                // disable second gael until first is dead
                .Label0()
                .ChangeCharacterEnableState(gael_phase2, DisabledEnabled.Disabled)
                .ActivateHit(gael_phase2, DisabledEnabled.Disabled)
                .IfCharacterHpRatio(ConditionGroup.Main, gael_phase1, ComparisonType.Equal, 0, ComparisonType.Equal, 1)
                .WarpCharacterAndCopyFloor(gael_phase2, TargetEntityType.Character, gael_phase1, 260, gael_phase1)
                .WaitFixedTimeSeconds(5)
                .ChangeCharacterEnableState(gael_phase2, DisabledEnabled.Enabled)
                .ForceAnimationPlayback(gael_phase2, 3036, false, true, false, 0, 1)
                .ActivateHit(gael_phase2, DisabledEnabled.Enabled)
                .Build();

            emevd.Events.Add(newEvent);

            gael_phase1 = new EventArg<int>();
            gael_phase2 = new EventArg<int>();
            EventArg<short> slotNumber = new EventArg<short>();
            EventArg<int> nameId = new EventArg<int>();
            newEvent = new EventBuilder()
                .Event(Constants.GAEL_BOSS_EVENT, EMEVD.Event.RestBehaviorType.Restart)
                .Param(gael_phase1).Param(gael_phase2).Param(slotNumber).Param(nameId)
                .IfCharacterDeadAlive(ConditionGroup.And01, gael_phase2, DeathState.Alive, ComparisonType.Equal, 1)
                .IfCharacterHpRatio(ConditionGroup.And01, gael_phase1, ComparisonType.Equal, 0.0f, ComparisonType.Equal, 1)
                .IfConditionGroup(ConditionGroup.Main, ConditionState.Pass, ConditionGroup.And01)
                .WaitFixedTimeSeconds(5)
                .DisplayBossHealthBar(DisabledEnabled.Disabled, gael_phase1, slotNumber, nameId)
                .DisplayBossHealthBar(DisabledEnabled.Enabled, gael_phase2, slotNumber, nameId)
                .Build();

            emevd.Events.Add(newEvent);

            gael_phase1 = new EventArg<int>();
            newEvent = new EventBuilder()
                .Event(Constants.GAEL_AGGRO_EVENT, EMEVD.Event.RestBehaviorType.Restart)
                .Param(gael_phase1)
                .SetCharacterAiState(gael_phase1, DisabledEnabled.Disabled)
                .IfCharacterHpRatio(ConditionGroup.Or01, gael_phase1, ComparisonType.Lessorequal, 0.99f, ComparisonType.Equal, 1)
                .IfEntityInOutsideRadiusOfEntity(ConditionGroup.Or01, InsideOutsideState.Inside, 10000, gael_phase1, Constants.GAEL_AGGRO_RANGE, 1)
                .IfConditionGroup(ConditionGroup.Main, ConditionState.Pass, ConditionGroup.Or01)
                .SetCharacterAiState(gael_phase1, DisabledEnabled.Enabled)
                .Build();

            emevd.Events.Add(newEvent);
        }

        
        static void ModifyEvents()
        {
            string commonFilename = @"event\common_func.emevd.dcx";
            EMEVD commonEmevd = EMEVD.Read(commonFilename);
            List<EMEVD.Event> commonEvents = new List<EMEVD.Event>(commonEmevd.Events);
            HashSet<int> visitedEvents = new HashSet<int>();

            foreach (string mapId in Constants.MAP_IDS)
            {
                string filename = @"event\" + mapId + ".emevd.dcx";
                EMEVD emevd = EMEVD.Read(filename);
                Console.WriteLine(mapId);

                List<EMEVD.Event> events = new List<EMEVD.Event>(emevd.Events);

                int i = events.FindIndex(ev => ev.ID == 0);
                EMEVD.Event startEvent = events[i];
                events.RemoveAt(i);

                i = events.FindIndex(ev => ev.ID == 50);
                EMEVD.Event resetEvent = events[i];
                events.RemoveAt(i);

                HashSet<int> unvisitedEnemies = new HashSet<int>(enemyEntityIdMap[mapId]);

                EventBuilder startEventBuilder = new EventBuilder()
                    .Event(startEvent, 0)
                    .ForEach(EventModifier(events, commonEvents, visitedEvents, unvisitedEnemies));

                foreach (int entityId in enemyEntityIdMap[mapId])
                {
                    startEventBuilder.InitializeCommonEvent(Constants.GAEL_EVENT_ID, null, entityId, gaelEntityIds[entityId]);
                }

                foreach (int entityId in enemiesThatNeedAggroRange[mapId].Concat(unvisitedEnemies))
                {
                    startEventBuilder.InitializeCommonEvent(Constants.GAEL_AGGRO_EVENT, null, entityId);
                }

                new EventBuilder()
                    .Event(resetEvent, 0)
                    .ForEach(EventModifier(events, commonEvents, visitedEvents, new HashSet<int>()));

                for (int j = 0; j < bossGaels.Count; j++)
                {
                    startEventBuilder.InitializeCommonEvent(Constants.GAEL_BOSS_EVENT, null, bossGaels[i], gaelEntityIds[bossGaels[i]], bossGaelHpBarSlots[i], bossGaelNameIds[i]);
                }
                bossGaels.Clear();
                bossGaelHpBarSlots.Clear();
                bossGaelNameIds.Clear();
                emevd.Write(Constants.MOD_DIR + filename);
            }
            InsertCommonEvents(commonEmevd);
            commonEmevd.Write(Constants.MOD_DIR + commonFilename);
        }

        // Modify events referenced by InitializeEvent/InitializeCommonEvent instructions
        static Predicate<MyInstr> EventModifier(List<EMEVD.Event> events, List<EMEVD.Event> commonEvents, HashSet<int> visitedEvents, HashSet<int> unvisitedEnemies)
        {
            return delegate (MyInstr myInstr)
            {
                bool ret = false;
                if (myInstr is InstrWrapper.AbstractInitializeEvent instr)
                {
                    EventArg<int> eventId = instr.eventId;
                    List<EventArg<int>> parameters = new List<EventArg<int>>(instr.parameters);
                    EMEVD.Event match = (instr is InstrWrapper.InitializeEvent ? events : commonEvents).Find(ev => ev.ID == eventId.Value);
                    if (match == null)
                    {
                        Console.WriteLine("Warning: event ID " + eventId.Value + " not found");
                        return false;
                    }
                    

                    EventBuilder eventBuilder = new EventBuilder()
                        .Event(match, instr.parameters.Length);

                    if (!visitedEvents.Contains(eventId.Value))
                    {
                        if (PreprocessEvent(eventBuilder, eventId.Value))
                        {
                            visitedEvents.Add(eventId.Value);
                            return false;
                        }
                        eventBuilder.Seek(0);
                    }

                    Dictionary<int, object> paramMap = new Dictionary<int, object>();
                    Dictionary<int, EventArg<int>> gaelMap = new Dictionary<int, EventArg<int>>();
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        paramMap[i] = parameters[i].Value;
                        if (IsGael(null, parameters[i].Value))
                        {
                            // Add another parameter with gael phase 2
                            EventArg<int> newGaelParam = new EventArg<int>();
                            eventBuilder.Param(newGaelParam);
                            gaelMap[i] = newGaelParam;
                            unvisitedEnemies.Remove(parameters[i].Value);
                            instr.AddParam(gaelEntityIds[parameters[i].Value]);
                            ret = true;
                        }
                    }
                    if (!visitedEvents.Contains(eventId.Value))
                    {
                        //Console.WriteLine("Warning: skipped event ID " + eventId.Value);
                        eventBuilder.ForEach(InstrModifier(eventBuilder, paramMap, gaelMap, unvisitedEnemies));
                        visitedEvents.Add(eventId.Value);
                        PostprocessEvent(eventBuilder, eventId.Value);
                    }

                }

                return ret;
            };
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
            string filename = "Data0.bdt";
            BND4 bnd = SFUtil.DecryptDS3Regulation(filename);

            // NPC params
            {
                BinderFile tmp_bndf = bnd.Files.Find(bndf => bndf.Name.EndsWith("NpcParam.param")) ?? throw new KeyNotFoundException();
                PARAM param = PARAM.Read(tmp_bndf.Bytes);
                string paramdefFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "NPC_PARAM_ST.xml");
                PARAMDEF paramdef = PARAMDEF.XmlDeserialize(paramdefFile);
                param.ApplyParamdef(paramdef);

                // put params in a dictionary for later use
                foreach (PARAM.Row row in param.Rows)
                {
                    npc_params[row.ID] = row;
                }

                PARAM.Row gaelPhase1Row = param.Rows.Find(row => row.ID == Constants.GAEL_PHASE1_PARAM_ID) ?? throw new KeyNotFoundException();
                PARAM.Row gaelPhase2Row = param.Rows.Find(row => row.ID == Constants.GAEL_PHASE2_PARAM_ID) ?? throw new KeyNotFoundException();
                foreach (TeamType teamType in Enum.GetValues(typeof(TeamType)))
                {
                    if (teamType == TeamType.Default) continue;
                    // create a new entry for Gael phase 1 with modified parameters
                    PARAM.Row new_row = Util.CopyRow(gaelPhase1Row, paramdef);
                    new_row.ID = Constants.MY_GAEL_PHASE1_PARAM_ID + (int)teamType;
                    new_row.Name = "gael1";
                    Util.SetCellValue(new_row, "subTeamType", (byte)teamType);
                    Util.InsertRow(param, new_row);

                    // create a new entry for Gael phase 2 with modified parameters
                    new_row = Util.CopyRow(gaelPhase2Row, paramdef);
                    new_row.ID = Constants.MY_GAEL_PHASE2_PARAM_ID + (int)teamType;
                    new_row.Name = "gael2";
                    Util.ModifyCellValue(new_row, "Hp", (int value) => (int) Math.Ceiling(value * 0.65));
                    Util.SetCellValue(new_row, "getSoul", 120000);
                    Util.SetCellValue(new_row, "isSoulGetByBoss", true);
                    Util.SetCellValue(new_row, "disableIntiliazeDead", false);
                    Util.SetCellValue(new_row, "subTeamType", (byte)teamType);
                    Util.InsertRow(param, new_row);
                }

                tmp_bndf.Bytes = param.Write();
            }

            {
                // load action button params
                BinderFile tmpBndf = bnd.Files.Find(bndf => bndf.Name.EndsWith("ActionButtonParam.param")) ?? throw new KeyNotFoundException();
                PARAM param = PARAM.Read(tmpBndf.Bytes);
                string paramdefFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "ACTIONBUTTON_PARAM_ST.xml");
                PARAMDEF paramdef = PARAMDEF.XmlDeserialize(paramdefFile);
                param.ApplyParamdef(paramdef);

                PARAM.Row gundyrRow = param.Rows.Find(row => row.ID == 8900) ?? throw new KeyNotFoundException();
                Util.SetCellValue(gundyrRow, "Radius", 8);

                tmpBndf.Bytes = param.Write();
            }



            SFUtil.EncryptDS3Regulation(Constants.MOD_DIR + filename, bnd);
        }
    }
}
