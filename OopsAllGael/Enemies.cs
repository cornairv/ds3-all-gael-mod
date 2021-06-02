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
        static Dictionary<string, List<int>> enemiesThatNeedAggroRange = new Dictionary<string, List<int>>(); // map id => gael ids

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
                enemiesThatNeedAggroRange[mapId] = new List<int>();

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

                    if (enemy.CharaInitID != -1)
                    {
                        if (!isFriendly)
                        {
                            enemiesThatNeedAggroRange[mapId].Add(enemy.EntityID);
                        }
                    }

                    if (isFriendly)
                    {
                        friendlyEntityIds.Add(enemy.EntityID);
                    }

                    enemy.ModelName = "c6201";
                    enemy.NPCParamID = Constants.MY_GAEL_PHASE1_PARAM_ID + (int)newTeam;
                    enemy.CharaInitID = -1;
                    if (!isFriendly)
                    {
                        enemy.ThinkParamID = 620100;
                    }

                    // add a phase 2 Gael
                    MSB3.Part.Enemy new_gael = Util.CopyEnemy(enemy);
                    new_gael.EntityID = newEnemyIdCounter++;
                    new_gael.ModelName = "c6200";
                    new_gael.NPCParamID = Constants.MY_GAEL_PHASE2_PARAM_ID + (int)newTeam;
                    new_gael.Name = enemy.Name + " (2)";
                    new_gael.ThinkParamID = 620000;
                    new_gael.TalkID = 0;
                    new_gaels.Add(new_gael);

                    if (gaelEntityIds.ContainsKey(enemy.EntityID))
                    {
                        Console.WriteLine("Warning: duplicate entity ID: " + enemy.EntityID + ", " + team);
                    }
                    else if (gaelEntityIds.ContainsKey(new_gael.EntityID))
                    {
                        Console.WriteLine("Warning: gael2 entity ID collision: " + new_gael.EntityID);
                    }
                    gaelEntityIds[enemy.EntityID] = new_gael.EntityID;
                    enemyEntityIdMap[mapId].Add(enemy.EntityID);
                }

                //foreach (MSB3.Event)

                msb.Parts.Enemies.AddRange(new_gaels);
                msb.Write(Constants.MOD_DIR + msbFilename);
            }
        }
    }
}
