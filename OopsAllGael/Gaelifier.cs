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
            Directory.CreateDirectory(Constants.MOD_DIR + @"event");
            Directory.CreateDirectory(Constants.MOD_DIR + @"script");
            Directory.CreateDirectory(Constants.MOD_DIR + @"map\mapstudio");
            Directory.CreateDirectory(Constants.MOD_DIR + @"sfx");
            //Directory.CreateDirectory(Constants.MOD_DIR + @"chr");
            //Directory.CreateDirectory(Constants.MOD_DIR + @"action\script");
            Directory.CreateDirectory(Constants.MOD_DIR + @"remo");

            InsertParams();
            ModifyEvents();
            InsertScripts();
            InsertSfx();
            //InsertModels();
            ModifyCutscenes();
        }

        static void InsertScripts()
        {
            // Extract Gael scripts
            BND4 gael_bnd = BND4.Read(@"script\m51_01_00_00.luabnd.dcx");
            List<BinderFile> gael_scripts = new List<BinderFile>();

            // Modify Gael's phase 1 health triggers
            double ratio = (0.35 * 2776) / 1031;
            double shift = 1 - ratio;
            BinderFile tmp_bndf = gael_bnd.Files.Find(bndf => bndf.Name.EndsWith("620100_battle.lua")) ?? throw new KeyNotFoundException();
            BitConverter.GetBytes(0.7 - shift).CopyTo(tmp_bndf.Bytes, 0x897);
            BitConverter.GetBytes(0.5 - shift).CopyTo(tmp_bndf.Bytes, 0x8a0);
            BitConverter.GetBytes(0.3 - shift).CopyTo(tmp_bndf.Bytes, 0x8a9);
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
            EventArg<int> gael_phase1;
            EventArg<int> gael_phase2;
            EventArg<int> startEnabled;
            EMEVD.Event newEvent = new EventBuilder()
                .Event(Constants.GAEL_EVENT_ID, EMEVD.Event.RestBehaviorType.Restart)
                .Param(out gael_phase1).Param(out gael_phase2).Param(out startEnabled)

                // disable first Gael if second is dead
                .WaitFixedTimeFrames(1)

                .SkipIfComparison(2, ComparisonType.Equal, 0, startEnabled) // force to start enabled
                .SetCharacterAiState(gael_phase1, DisabledEnabled.Enabled)
                .SetCharacterAiState(gael_phase2, DisabledEnabled.Enabled)

                .IfCharacterDeadAlive(ConditionGroup.Or01, gael_phase1, DeathState.Dead, ComparisonType.Equal, 1)
                .IfCharacterDeadAlive(ConditionGroup.Or01, gael_phase2, DeathState.Dead, ComparisonType.Equal, 1)
                .GotoIfConditionGroupStateUncompiled(Label.Label0, ConditionState.Fail, ConditionGroup.Or01)
                .ChangeCharacterEnableState(gael_phase1, DisabledEnabled.Disabled)
                .ChangeCharacterEnableState(gael_phase2, DisabledEnabled.Disabled)
                .ActivateHit(gael_phase1, DisabledEnabled.Disabled)
                .ActivateHit(gael_phase2, DisabledEnabled.Disabled)
                .EndUnconditionally(EventEndType.End)

                // disable second gael until first is dead
                .Label0()
                //.SetCharacterTalkRange(gael_phase2, 0)
                .ChangeCharacterEnableState(gael_phase2, DisabledEnabled.Disabled)
                .ActivateHit(gael_phase2, DisabledEnabled.Disabled)

                // enable second gael when first is dead
                .IfCharacterHpRatio(ConditionGroup.Main, gael_phase1, ComparisonType.Equal, 0, ComparisonType.Equal, 1)
                .WarpCharacterAndCopyFloor(gael_phase2, TargetEntityType.Character, gael_phase1, 260, gael_phase1)
                .WaitFixedTimeSeconds(4)
                .ChangeCharacterEnableState(gael_phase2, DisabledEnabled.Enabled)
                //.SetCharacterTalkRange(gael_phase2, 30)
                .ActivateHit(gael_phase2, DisabledEnabled.Enabled)
                .ForceAnimationPlayback(gael_phase2, 3036, false, false, false, 0, 1)

                /* too buggy
                // handle respawning enemies
                .IfCharacterDeadAlive(ConditionGroup.Main, gael_phase1, DeathState.Alive, ComparisonType.Equal, 1)
                //.EndIfConditionGroupStateUncompiled(EventEndType.End, ConditionState.Fail, ConditionGroup.Or02)
                .ChangeCharacterEnableState(gael_phase1, DisabledEnabled.Disabled)
                .ActivateHit(gael_phase1, DisabledEnabled.Disabled)

                .IfCharacterHpRatio(ConditionGroup.Main, gael_phase2, ComparisonType.Equal, 0, ComparisonType.Equal, 1)
                .ChangeCharacterEnableState(gael_phase1, DisabledEnabled.Enabled)
                .ActivateHit(gael_phase1, DisabledEnabled.Enabled)
                .ForceAnimationPlayback(gael_phase1, 3031, false, false, false, 0, 1)
                .IfCharacterDeadAlive(ConditionGroup.Main, gael_phase2, DeathState.Alive, ComparisonType.Equal, 1)
                .EndUnconditionally(EventEndType.Restart)
                */
                .Build();

            emevd.Events.Add(newEvent);


            newEvent = new EventBuilder()
                .Event(Constants.GAEL_GRAVITY_EVENT, EMEVD.Event.RestBehaviorType.Restart)
                .Param(out gael_phase1).Param(out gael_phase2)

                .IfCharacterAiState(ConditionGroup.Or02, gael_phase1, AiStateType.Combat, ComparisonType.Equal, 1)
                .IfCharacterHpRatio(ConditionGroup.Or02, gael_phase1, ComparisonType.Less, 0.99f, ComparisonType.Equal, 1)
                .IfConditionGroup(ConditionGroup.Main, ConditionState.Pass, ConditionGroup.Or02)
                .SetCharacterGravity(gael_phase1, DisabledEnabled.Enabled)
                .SetCharacterGravity(gael_phase2, DisabledEnabled.Enabled)
                .SetCharacterMaphit(gael_phase1, false)
                .SetCharacterMaphit(gael_phase2, false)
                .Build();

            emevd.Events.Add(newEvent);

            EventArg<short> slotNumber;
            EventArg<int> nameId;
            newEvent = new EventBuilder()
                .Event(Constants.GAEL_BOSS_EVENT, EMEVD.Event.RestBehaviorType.Restart)
                .Param(out gael_phase1).Param(out gael_phase2).Param(out slotNumber).Param(out nameId)
                .WaitFixedTimeFrames(1)
                .IfCharacterDeadAlive(ConditionGroup.And01, gael_phase2, DeathState.Alive, ComparisonType.Equal, 1)
                .IfCharacterDeadAlive(ConditionGroup.And01, gael_phase1, DeathState.Alive, ComparisonType.Equal, 1)
                .IfCharacterHpRatio(ConditionGroup.And01, gael_phase1, ComparisonType.Equal, 0.0f, ComparisonType.Equal, 1)
                .IfConditionGroup(ConditionGroup.Main, ConditionState.Pass, ConditionGroup.And01)
                .WaitFixedTimeSeconds(4)
                .DisplayBossHealthBar(DisabledEnabled.Enabled, gael_phase2, slotNumber, nameId)
                .Build();

            emevd.Events.Add(newEvent);

            newEvent = new EventBuilder()
                .Event(Constants.GAEL_CAGE_EVENT, EMEVD.Event.RestBehaviorType.Restart)
                .Param(out gael_phase1)
                .ActivateHit(gael_phase1, DisabledEnabled.Disabled)
                .SetCharacterGravity(gael_phase1, DisabledEnabled.Disabled)
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

                EventBuilder startEventBuilder = new EventBuilder()
                    .Event(startEvent, 0)
                    .ForEach(EventModifier(events, commonEvents, visitedEvents));

                startEventBuilder.InitializeCommonEvent(Constants.GAEL_CAGE_EVENT, null, 3100710); // undead settlement Cornyx


                new EventBuilder()
                    .Event(resetEvent, 0)
                    .ForEach(EventModifier(events, commonEvents, visitedEvents));

                for (int j = 0; j < bossGaels.Count; j++)
                {
                    startEventBuilder.InitializeCommonEvent(Constants.GAEL_BOSS_EVENT, null, bossGaels[j], gaelEntityIds[bossGaels[j]], bossGaelHpBarSlots[j], bossGaelNameIds[j]);
                }
                foreach (int entityId in enemyEntityIdMap[mapId])
                {
                    uint? layerMask = null;
                    if (entityId == 3100741)
                    {
                        layerMask = 0b1111111100; // hodrick event
                    }
                    int startEnabled = aggressive && startEnabledEnemies.Contains(entityId) ? 1 : 0;
                    startEventBuilder.InitializeCommonEvent(Constants.GAEL_EVENT_ID, layerMask, entityId, gaelEntityIds[entityId], startEnabled);
                    if (!flyingEntityIds.Contains(entityId))
                    {
                        startEventBuilder.InitializeCommonEvent(Constants.GAEL_GRAVITY_EVENT, layerMask, entityId, gaelEntityIds[entityId]);
                    }
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
        static Predicate<MyInstr> EventModifier(List<EMEVD.Event> events, List<EMEVD.Event> commonEvents, HashSet<int> visitedEvents)
        {
            return delegate (MyInstr myInstr)
            {
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

                    Dictionary<int, EventArg<int>> gaelMap = new Dictionary<int, EventArg<int>>();
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (IsGael(null, parameters[i].Value))
                        {
                            // Add another parameter with gael phase 2
                            EventArg<int> newGaelParam;
                            eventBuilder.Param(out newGaelParam);
                            gaelMap[i] = newGaelParam;
                            instr.AddParam(gaelEntityIds[parameters[i].Value]);
                            instr.modified = true;
                        }
                    }

                    eventBuilder.ForEach(InstrModifier(eventBuilder, gaelMap, instr, visitedEvents.Contains(eventId.Value)));
                    if (!visitedEvents.Contains(eventId.Value))
                    {
                        visitedEvents.Add(eventId.Value);
                        //Console.WriteLine("Warning: skipped event ID " + eventId.Value);
                        eventBuilder.Seek(0);
                        PostprocessEvent(eventBuilder, eventId.Value);
                    }

                    eventBuilder.Seek(0).ForEach(EventModifier(events, commonEvents, visitedEvents));

                    return instr.modified;
                }

                return false;
            };
        }

        static void InsertSfx()
        {
            string filename = @"sfx\frpg_sfxbnd_commoneffects_effect.ffxbnd.dcx";
            BND4 common_sfx_bnd = BND4.Read(filename);
            BND4 gael_sfx_bnd = BND4.Read(@"sfx\frpg_sfxbnd_m51_effect.ffxbnd.dcx");

            // remove annoying clouds
            BinderFile sfx = gael_sfx_bnd.Files.Find(f => f.Name.EndsWith("f000000120.fxr"));
            FXR3 fxr = FXR3.Read(sfx.Bytes);
            //fxr.Section1Tree.Section2s.Clear();
            //fxr.Section4Tree.Section4s.Clear();
            fxr.Section4Tree.Section4s.Clear();
            sfx.Bytes = fxr.Write();

            common_sfx_bnd.Files.InsertRange(0, gael_sfx_bnd.Files);
            common_sfx_bnd.Write(Constants.MOD_DIR + filename);
        }

        static void ModifyCutscenes()
        {
            byte[] gael1Unicode = Encoding.Unicode.GetBytes("c6201");
            byte[] gael1UTF8 = Encoding.UTF8.GetBytes("c6201");
            byte[] gael2Unicode = Encoding.Unicode.GetBytes("c6200");
            byte[] gael2UTF8 = Encoding.UTF8.GetBytes("c6200");
            byte[] yhormUnicode = Encoding.Unicode.GetBytes("c5260");
            byte[] yhormUTF8 = Encoding.UTF8.GetBytes("c5260");
            byte[] fillianoreUnicode = Encoding.Unicode.GetBytes("c6310");
            byte[] fillianoreUTF8 = Encoding.UTF8.GetBytes("c6310");
            byte[] demonUnicode = Encoding.Unicode.GetBytes("o007010");
            byte[] demonUTF8 = Encoding.UTF8.GetBytes("o007010");
            byte[] notDemonUnicode = Encoding.Unicode.GetBytes("o000708");
            byte[] notDemonUTF8 = Encoding.UTF8.GetBytes("o000708");
            byte[] lorianUnicode = Encoding.Unicode.GetBytes("o302596");
            byte[] lorianUTF8 = Encoding.UTF8.GetBytes("o302596");
            byte[] notLorianUnicode = Encoding.Unicode.GetBytes("o304854");
            byte[] notLorianUTF8 = Encoding.UTF8.GetBytes("o304854");
            
            byte[] placeholderUnicode = Encoding.Unicode.GetBytes("dbeef");
            byte[] placeholderUTF8 = Encoding.UTF8.GetBytes("dbeef");
            foreach (string filename in Directory.GetFiles("remo"))
            {
                if (!filename.EndsWith(".remobnd.dcx")) continue;

                DCX.Type compressionType;
                byte[] dcx = DCX.Decompress(filename, out compressionType);
                if (Path.GetFileName(filename).StartsWith("s51_01_")) // replace phase 1 gael with phase 2 gael
                {
                    Util.ReplaceInByteArray(dcx, gael1Unicode, placeholderUnicode);
                    Util.ReplaceInByteArray(dcx, gael1UTF8, placeholderUTF8);
                }
                else if (Path.GetFileName(filename).StartsWith("s39_00_")) // replace yhorm with phase 2 gael
                {
                    Util.ReplaceInByteArray(dcx, yhormUnicode, placeholderUnicode);
                    Util.ReplaceInByteArray(dcx, yhormUTF8, placeholderUTF8);
                }
                Util.ReplaceCXXXXInByteArray(dcx, gael1Unicode);
                Util.ReplaceCXXXXInByteArray(dcx, gael1UTF8);
                //Util.ReplaceCXXXXInByteArray(dcx, replacementUnicode, 'o');
                //Util.ReplaceCXXXXInByteArray(dcx, replacementUTF8, 'o');
                if (Path.GetFileName(filename).StartsWith("s51_01_") ||
                    Path.GetFileName(filename).StartsWith("s39_00_"))
                {
                    Util.ReplaceInByteArray(dcx, placeholderUnicode, gael2Unicode);
                    Util.ReplaceInByteArray(dcx, placeholderUTF8, gael2UTF8);
                }
                //else if (Path.GetFileName(filename).StartsWith("s30_00_")) // replace flying demon things with ladder
                {
                    Util.ReplaceInByteArray(dcx, demonUnicode, notDemonUnicode);
                    Util.ReplaceInByteArray(dcx, demonUTF8, notDemonUTF8);
                }
                //else if (Path.GetFileName(filename).StartsWith("s34_01_")) // replace lorian with chair
                {
                    //Util.ReplaceInByteArray(dcx, lorianUnicode, notLorianUnicode);
                    //Util.ReplaceInByteArray(dcx, lorianUTF8, notLorianUTF8);
                }
                DCX.Compress(dcx, compressionType, Constants.MOD_DIR + filename);
            }
        }
    }
}
