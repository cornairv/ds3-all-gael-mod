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
        static Dictionary<string, List<int>> enemyEntityIdMap = new Dictionary<string, List<int>>(); // map id => gael ids

        static Dictionary<int, int> npcParamIds = new Dictionary<int, int>();
        static HashSet<int> charaInitIds = new HashSet<int>();
        static HashSet<int> thinkParamIds = new HashSet<int>();
        static HashSet<int> friendlyThinkParamIds = new HashSet<int>();
        static HashSet<int> modelIds = new HashSet<int>(); 
        static HashSet<int> animIds = new HashSet<int>();
        static Dictionary<int, int> npcParamIdToAnimId = new Dictionary<int, int>();

        static void InsertEnemies()
        {
            // Extract Gael info
            MSB3 gael_msb = MSB3.Read(@"map\mapstudio\m51_01_00_00.msb.dcx");
            List<MSB3.Model.Enemy> gael_models = gael_msb.Models.Enemies.FindAll(model => model.Name == "c6201" || model.Name == "c6200");
            int newEnemyIdCounter = 100000000;
            // Make every enemy Gael
            foreach (string mapId in Constants.MAP_IDS)
            {
                string msbFilename = @"map\mapstudio\" + mapId + ".msb.dcx";
                MSB3 msb = MSB3.Read(msbFilename);
                msb.Models.Enemies.AddRange(gael_models);
                List<MSB3.Part.Enemy> new_gaels = new List<MSB3.Part.Enemy>();
                enemyEntityIdMap[mapId] = new List<int>();

                for (int i = 0; i < msb.Parts.Enemies.Count; i++)
                {
                    MSB3.Part.Enemy enemy = msb.Parts.Enemies[i];
                    allEntityIds.Add(enemy.EntityID);

                    // dragon
                    if (enemy.EntityID == 3000850)
                    {
                        continue;
                    }


                    TeamType team = (TeamType)Util.GetCellValue<byte>(npc_params[enemy.NPCParamID], "subTeamType");
                    if (enemy.ThinkParamID == 0 || team == TeamType.Object)// || Array.FindIndex(enemy_types_to_replace, t => t == team) == -1)
                    {
                        if (enemy.EntityID != 522304) // deacon above elevator
                        {
                            continue;
                        }
                    }
                    if (enemy.EntityID == -1)
                    {
                        enemy.EntityID = newEnemyIdCounter++;
                    }

                    bool isFriendly = team == TeamType.Ally || team == TeamType.CoOp || team == TeamType.CoOpNpc || team == TeamType.WhitePhantom || team == TeamType.Human || team == TeamType.FriendlyNpc;
                    TeamType newTeam = isFriendly ? team : TeamType.StrongEnemy;

                    if (isFriendly)
                    {
                        friendlyEntityIds.Add(enemy.EntityID);
                        friendlyThinkParamIds.Add(enemy.ThinkParamID);
                    }
                    //if (enemy.ModelName != "c2021") continue;

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
                    if (enemy.ThinkParamID != 620000 && enemy.ThinkParamID != 620100 && !isFriendly)
                        thinkParamIds.Add(enemy.ThinkParamID);
                    modelIds.Add(modelId);
                    animIds.Add(animId);

                    // CRGW
                    if (enemy.EntityID == 3100800)
                    {
                        enemy.Position = new Vector3(enemy.Position.X - 5, enemy.Position.Y, enemy.Position.Z + 5);
                    }

                    enemy.ModelName = "c6201";
                    enemy.CharaInitID = -1;

                    // add a phase 2 Gael
                    MSB3.Part.Enemy phase2Gael = Util.CopyEnemy(enemy);
                    phase2Gael.EntityID = newEnemyIdCounter++;
                    phase2Gael.ModelName = "c6200";
                    phase2Gael.NPCParamID = npcParamIds[enemy.NPCParamID];
                    phase2Gael.Name = enemy.Name + " (2)";
                    phase2Gael.ThinkParamID = 620000;
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

                    if (eventRegion.EntityID >= 3102570 && eventRegion.EntityID <= 3102571) // rat spawners near Morne
                    {
                        eventRegion.Position = new Vector3(eventRegion.Position.X, eventRegion.Position.Y, eventRegion.Position.Z + 2.5f);
                    }
                }

                msb.Parts.Enemies.AddRange(new_gaels);
                msb.Write(Constants.MOD_DIR + msbFilename);
            }
        }
    }
}
