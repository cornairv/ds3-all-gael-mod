using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        static HashSet<int> npcParamIds = new HashSet<int>();
        static HashSet<int> charaInitIds = new HashSet<int>();
        static HashSet<int> thinkParamIds = new HashSet<int>();
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
                    TeamType team = (TeamType)Util.GetCellValue<byte>(npc_params[enemy.NPCParamID], "subTeamType");
                    if (enemy.ThinkParamID == 0 || team == TeamType.Object)// || Array.FindIndex(enemy_types_to_replace, t => t == team) == -1)
                    {
                        continue;
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

                    npcParamIds.Add(enemy.NPCParamID);
                    charaInitIds.Add(enemy.CharaInitID);
                    thinkParamIds.Add(enemy.ThinkParamID);
                    modelIds.Add(modelId);
                    animIds.Add(animId);

                    enemy.ModelName = "c6201";

                    // add a phase 2 Gael
                    MSB3.Part.Enemy phase2Gael = Util.CopyEnemy(enemy);
                    phase2Gael.EntityID = newEnemyIdCounter++;
                    phase2Gael.ModelName = "c6200";
                    phase2Gael.NPCParamID = Constants.MY_GAEL_PHASE2_PARAM_ID;
                    phase2Gael.Name = enemy.Name + " (2)";
                    phase2Gael.ThinkParamID = 620000;
                    phase2Gael.TalkID = 0;
                    phase2Gael.CharaInitID = enemy.CharaInitID;
                    enemy.CharaInitID = -1;
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

                msb.Parts.Enemies.AddRange(new_gaels);
                msb.Write(Constants.MOD_DIR + msbFilename);
            }
        }
    }
}
