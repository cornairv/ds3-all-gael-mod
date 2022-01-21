using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using SoulsFormats;
using OopsAllGael.EventEnum;

namespace OopsAllGael
{
    partial class Gaelifier
    {
        static Dictionary<int, int> gaelEntityIds = new Dictionary<int, int>(); // gael phase 1 id => gael phase 2 id
        static HashSet<int> friendlyEntityIds = new HashSet<int>();
        static HashSet<int> allEntityIds = new HashSet<int>();
        static HashSet<int> flyingEntityIds = new HashSet<int>();
        static Dictionary<string, List<int>> enemyEntityIdMap = new Dictionary<string, List<int>>(); // map id => gael ids

        static Dictionary<int, int> npcParamIds = new Dictionary<int, int>();
        static HashSet<int> charaInitIds = new HashSet<int>();
        static HashSet<int> thinkParamIds = new HashSet<int>();
        static HashSet<int> friendlyThinkParamIds = new HashSet<int>();
        static HashSet<int> modelIds = new HashSet<int>(); 
        static HashSet<int> animIds = new HashSet<int>();
        static Dictionary<int, int> npcParamIdToAnimId = new Dictionary<int, int>();
        static int newEntityIdCounter = 100000000;

        static HashSet<int> startEnabledEnemies = new HashSet<int>();


        static void InsertEnemies()
        {
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
                enemyEntityIdMap[mapId] = new List<int>();
                Dictionary<string, string> gaelNames = new Dictionary<string, string>();

                for (int i = 0; i < msb.Parts.Enemies.Count; i++)
                {
                    MSB3.Part.Enemy enemy = msb.Parts.Enemies[i];
                    if (allEntityIds.Contains(enemy.EntityID) && enemy.EntityID != -1)
                    {
                        Console.WriteLine("Warning: duplicate entity ID " + enemy.EntityID);
                    }
                    allEntityIds.Add(enemy.EntityID);

                    TeamType team = (TeamType)Util.GetCellValue<byte>(npc_params[enemy.NPCParamID], "subTeamType");
                    bool isFriendly = team == TeamType.Ally || team == TeamType.CoOp || team == TeamType.CoOpNpc || team == TeamType.WhitePhantom || team == TeamType.Human || team == TeamType.FriendlyNpc;

                    if (enemy.ThinkParamID == 0 || team == TeamType.Object)// || Array.FindIndex(enemy_types_to_replace, t => t == team) == -1)
                    {
                        switch (enemy.EntityID)
                        {
                            case 3500586: // deacon above elevator
                            case 3500850: // rosaria
                            case 3500668: // grub near Rosaria
                            case 3500669: // grub next to Rosaria
                            case 5100890: // fillianore
                            case 5100810: // judicator
                            case 5000705: // dreg heap hag
                            case 4500705: // painter 1
                            case 4500706: // painter 2
                            //case 3900470: // profane capital flame bowl
                            //case 3100196: // arrow firing entity
                            case 4100200: // kiln soldier
                            case 5110200: // crusty fillianore
                            case 3300561: // stray demon (?)
                                Console.WriteLine("Override unthinking enemy: " + mapId + ": " + enemy.Name + ", " + enemy.EntityID);
                                break;
                            default:
                                if (enemy.ModelName != "c1000" && enemy.ModelName != "c0100" && enemy.ModelName != "c0000")
                                    Console.WriteLine("Unthinking enemy: " + mapId + ": " + enemy.Name + ", " + enemy.EntityID);
                                continue;
                        }
                    }
                    if (enemy.EntityID == -1)
                    {
                        enemy.EntityID = newEntityIdCounter++;
                    }

                    if (enemy.EntityID == 3700711 || enemy.EntityID == 3700706) // replace dead Anri with dead small Gael
                    {
                        enemy.CharaInitID = 21005;
                        continue;
                    }
                    if (enemy.EntityID == 4500170 // keep Friede Gael as small Gael
                        || friendlyNpcSummonIds.Contains(enemy.EntityID)) // replace friendly NPC summons with small Gael
                    {
                        enemy.NPCParamID = 21005;
                        //enemy.ThinkParamID = 21005; // needs original AI to find fog wall
                        enemy.CharaInitID = 21005;
                        continue;
                    }

                    if (!isFriendly)
                    {
                        startEnabledEnemies.Add(enemy.EntityID);
                    }

                    // dragons
                    if (enemy.EntityID == 3000850 || enemy.EntityID == 3010830 || enemy.EntityID == 3010831)
                    {

                    }
                    // ballista
                    else if (enemy.EntityID == 3800100)
                    {
                        startEnabledEnemies.Remove(enemy.EntityID);
                        continue;
                    }
                    // arrow firing entity
                    else if (enemy.EntityID == 3100196)
                    {
                        startEnabledEnemies.Remove(enemy.EntityID);
                        continue;
                    }
                    // profane capital flame bowl
                    else if (enemy.EntityID == 3900470)
                    {
                        startEnabledEnemies.Remove(enemy.EntityID);
                        continue;
                    }

                    if (isFriendly)
                    {
                        friendlyEntityIds.Add(enemy.EntityID);
                        friendlyThinkParamIds.Add(enemy.ThinkParamID);
                    }

                    int modelId = Util.GetCellValue<short>(npc_params[enemy.NPCParamID], "normalChangeModelId");
                    if (modelId == -1)
                    {
                        modelId = int.Parse(enemy.ModelName.Substring(1));
                    }

                    int animId = Util.GetCellValue<short>(npc_params[enemy.NPCParamID], "normalChangeAnimChrId");
                    if (animId == -1)
                    {
                        animId = int.Parse(enemy.ModelName.Substring(1));
                        MyDebug.Assert(!(npcParamIdToAnimId.ContainsKey(enemy.NPCParamID) && npcParamIdToAnimId[enemy.NPCParamID] != modelId));
                        npcParamIdToAnimId[enemy.NPCParamID] = modelId;
                    }

                    if (modelId == 1) throw new Exception();

                    npcParamIds[enemy.NPCParamID] = enemy.NPCParamID + 1000000;
                    charaInitIds.Add(enemy.CharaInitID);
                    if (!isFriendly && enemy.ThinkParamID != Constants.GAEL_PHASE2_PARAM_ID)
                        thinkParamIds.Add(enemy.ThinkParamID);
                    modelIds.Add(modelId);
                    animIds.Add(animId);

                    // CRGW
                    if (enemy.EntityID == 3100800)
                    {
                        enemy.Position = new Vector3(enemy.Position.X - 5, enemy.Position.Y, enemy.Position.Z + 5);
                    }

                    // butterflies
                    if (enemy.NPCParamID == 308000)
                    {
                        //flyingEntityIds.Add(enemy.EntityID);
                    }

                    // gravetender greatwolf
                    if (enemy.EntityID == 4500861)
                    {
                        enemy.Position = new Vector3(enemy.Position.X + 7, enemy.Position.Y, enemy.Position.Z);
                    }

                    // midir
                    if (enemy.EntityID == 5100200)
                    {
                        enemy.Position = new Vector3(enemy.Position.X - 5, enemy.Position.Y + 1, enemy.Position.Z + 5);
                    }

                    // siegward rats
                    if (enemy.EntityID == 3900307)
                    {
                        enemy.Position = new Vector3(167.913f, -361.557f, -834.374f);
                    }
                    else if (enemy.EntityID == 3900306)
                    {
                        enemy.Position = new Vector3(168.803f, -361.527f, -828.895f);
                    }
                    else if (enemy.EntityID == 3900304)
                    {
                        enemy.Position = new Vector3(159.599f, -361.754f, -820.507f);
                    }
                    else if (enemy.EntityID == 3900305)
                    {
                        enemy.Position = new Vector3(163.688f, -361.754f, -821.579f);
                    }

                    // horace (can fall through floor)
                    if (enemy.EntityID == 3300715)
                    {
                        enemy.Position = new Vector3(-15.522f, -236.421f, -551.041f);
                    }

                    // gravetender wolves
                    if (Array.FindIndex(enemy.EntityGroups, groupId =>
                        groupId == 4505860 || // gravetender
                        groupId == 3305800    // abyss watchers
                        ) != -1)
                    {
                        startEnabledEnemies.Remove(enemy.EntityID);
                    }

                    // halflight guys
                    if (enemy.EntityID == 5100800 || enemy.EntityID == 5100801 || enemy.EntityID == 5100804 || enemy.EntityID == 5100810 || enemy.EntityID == 5100811)
                    {
                        startEnabledEnemies.Remove(enemy.EntityID);
                    }

                    // sirris, anri combat AI
                    if (enemy.EntityID == 3100726 || enemy.EntityID == 3700160 || enemy.EntityID == 3700707)
                    {
                        enemy.ThinkParamID = Constants.GAEL_PHASE1_PARAM_ID;
                    }

                    enemy.ModelName = "c6201";
                    enemy.CharaInitID = -1;
                    if (enemy.ThinkParamID == Constants.GAEL_PHASE2_PARAM_ID)
                        enemy.ThinkParamID = Constants.GAEL_PHASE1_PARAM_ID;

                    // add a phase 2 Gael
                    MSB3.Part.Enemy phase2Gael = Util.CopyEnemy(enemy);
                    phase2Gael.EntityID = newEntityIdCounter++;
                    phase2Gael.ModelName = "c6200";
                    phase2Gael.NPCParamID = npcParamIds[enemy.NPCParamID];
                    phase2Gael.Name = enemy.Name + " (2)";
                    phase2Gael.ThinkParamID = Constants.GAEL_PHASE2_PARAM_ID;
                    phase2Gael.TalkID = 0;
                    phase2Gael.CharaInitID = enemy.CharaInitID;
                    new_gaels.Add(phase2Gael);

                    if (gaelEntityIds.ContainsKey(enemy.EntityID))
                    {
                        Console.WriteLine("Warning: duplicate entity ID: " + enemy.EntityID + ", " + team);
                    }
                    else if (gaelEntityIds.ContainsKey(phase2Gael.EntityID))
                    {
                        Console.WriteLine("Warning: gael2 entity ID collision: " + phase2Gael.EntityID);
                    }
                    gaelEntityIds[enemy.EntityID] = phase2Gael.EntityID;
                    gaelNames[enemy.Name] = phase2Gael.Name;
                    enemyEntityIdMap[mapId].Add(enemy.EntityID);
                }

                for (int i = 0; i < msb.Regions.Events.Count; i++)
                {
                    MSB3.Region.Event eventRegion = msb.Regions.Events[i];
                    if (eventRegion.EntityID >= 3102830 && eventRegion.EntityID <= 3102835) // CRGW enemy spawner
                    {
                        Vector3 pr = new Vector3(130.755f, 0, -930.947f); // point on right side of arena
                        Vector3 pl = new Vector3(169.213f, 0, -918.242f); // point on left
                        Vector3 p = eventRegion.EntityID > 3102832 ? pl : pr;
                        Vector3 normal = Vector3.Normalize(pl - pr);
                        float d = Vector3.Dot(p - eventRegion.Position, normal);
                        eventRegion.Position = new Vector3(eventRegion.Position.X + normal.X * d, eventRegion.Position.Y, eventRegion.Position.Z + normal.Z * d);
                    }

                    else if (eventRegion.EntityID >= 3102570 && eventRegion.EntityID <= 3102571) // rat spawners near Morne
                    {
                        eventRegion.Position = new Vector3(eventRegion.Position.X, eventRegion.Position.Y, eventRegion.Position.Z + 2.5f);
                    }

                    else if (eventRegion.Name == "【生成ポイント】犬ネズミ_巣穴A") // rat spawners near Siegward
                    {
                        eventRegion.Position = new Vector3(159.599f, -361.754f, -820.507f);
                        Console.WriteLine(eventRegion.Name);
                    }
                    else if (eventRegion.Name == "【生成ポイント】犬ネズミ_巣穴C")
                    {
                        eventRegion.Position = new Vector3(167.913f, -361.557f, -834.374f);
                        Console.WriteLine(eventRegion.Name);
                    }
                    else if (eventRegion.Name == "【生成ポイント】犬ネズミ_巣穴B")
                    {
                        eventRegion.Position = new Vector3(168.803f, -361.527f, -828.895f);
                        Console.WriteLine(eventRegion.Name);
                    }


                    else if (eventRegion.EntityID == 3002600) // high wall dragon
                    {
                        if (!aggressive)
                        {
                            eventRegion.Position = new Vector3(-124.349f, 36.141f, 9.454f);
                        }
                    }
                }

                // add gael 2 to spawn generators
                List<MSB3.Event.Generator> gael2Generators = new List<MSB3.Event.Generator>();
                for (int i = 0; i < msb.Events.Generators.Count; i++)
                {
                    MSB3.Event.Generator generator = msb.Events.Generators[i];
                    MSB3.Event.Generator newGenerator = null;
                    int currentGael = 0;
                    for (int j = 0; j < generator.SpawnPartNames.Length; j++)
                    {
                        string name = generator.SpawnPartNames[j];
                        if (name != null && gaelNames.ContainsKey(name))
                        {
                            //generator.SpawnPartNames[j] = null; // just disable respawning Gaels
                            if (generator.MinInterval < 6)
                                generator.MinInterval = 6;
                            if (generator.MaxInterval < 6)
                                generator.MaxInterval = 6;
                            /* too buggy
                            if (newGenerator == null)
                                newGenerator = CreateGael2Generator(generator);
                            newGenerator.SpawnPartNames[currentGael++] = gaelNames[name];
                            */
                        }
                    }

                    if (newGenerator != null)
                        gael2Generators.Add(newGenerator);
                }
                msb.Events.Generators.AddRange(gael2Generators);

                for (int i = 0; i < msb.Parts.Objects.Count; i++)
                {
                    MSB3.Part.Object obj = msb.Parts.Objects[i];

                    // replace Lorian with lamp
                    if (obj.ModelName == "o302596")
                    {
                        obj.ModelName = "o304849";
                    }
                }

                msb.Parts.Enemies.AddRange(new_gaels);
                msb.Write(Constants.MOD_DIR + msbFilename);
            }
        }

        static MSB3.Event.Generator CreateGael2Generator(MSB3.Event.Generator generator)
        {
            MSB3.Event.Generator newGenerator = (MSB3.Event.Generator)generator.DeepCopy();
            newGenerator.EntityID = newEntityIdCounter++;
            newGenerator.MaxInterval = 6;
            newGenerator.MinInterval = 6;
            /*
            newGenerator.InitialSpawnCount = 255;
            newGenerator.LimitNum = 9999;
            newGenerator.MinGenNum = 9999;
            newGenerator.MaxGenNum = 9999;
            */
            newGenerator.Name += " (2)";
            for (int i = 0; i < newGenerator.SpawnPartNames.Length; i++)
            {
                newGenerator.SpawnPartNames[i] = null;
            }
            return newGenerator;
        }
    }
}
