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
        static bool aggressive = true;
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

                InsertEnemies(); // need npc_params before inserting enemies

                PARAM.Row gaelPhase1Row = param.Rows.Find(row => row.ID == Constants.GAEL_PHASE1_PARAM_ID) ?? throw new KeyNotFoundException();
                PARAM.Row gaelPhase2Row = param.Rows.Find(row => row.ID == Constants.GAEL_PHASE2_PARAM_ID) ?? throw new KeyNotFoundException();

                // create a new entry for Gael phase 1 with modified parameters
                PARAM.Row myGaelPhase1Row = Util.CopyRow(gaelPhase1Row, paramdef);
                myGaelPhase1Row.ID = Constants.MY_GAEL_PHASE1_PARAM_ID;
                myGaelPhase1Row.Name = "gael1";
                Util.SetCellValue(myGaelPhase1Row, "disableIntiliazeDead", true);
                Util.SetCellValue(myGaelPhase1Row, "disableRespawn", false);
                Util.SetCellValue(myGaelPhase1Row, "Hp", (int)Math.Ceiling(0.35 * Util.GetCellValue<int>(gaelPhase2Row, "Hp")));

                // create a new entry for Gael phase 2 with modified parameters
                PARAM.Row myGaelPhase2Row = Util.CopyRow(gaelPhase2Row, paramdef);
                myGaelPhase2Row.ID = Constants.MY_GAEL_PHASE2_PARAM_ID;
                myGaelPhase2Row.Name = "gael2";
                Util.ModifyCellValue(myGaelPhase2Row, "Hp", (int value) => (int)Math.Ceiling(value * 0.65));
                //Util.SetCellValue(myGaelPhase2Row, "getSoul", 120000);
                //Util.SetCellValue(myGaelPhase2Row, "isSoulGetByBoss", true);
                Util.SetCellValue(myGaelPhase2Row, "subTeamType", (byte)TeamType.StrongEnemy);
                Util.SetCellValue(myGaelPhase2Row, "disableIntiliazeDead", false);
                Util.SetCellValue(myGaelPhase2Row, "dropType", 1);


                List<PARAM.Row> newRows = new List<PARAM.Row>();
                newRows.Add(myGaelPhase1Row);
                newRows.Add(myGaelPhase2Row);

                foreach (PARAM.Row row in param.Rows)
                {
                    if (row.ID == 148000 || row.ID == 149000) // irithyll ghosts
                    {
                        Util.SetCellValue(row, "normalChangeModelId", 6201);
                        //Util.SetCellValue(row, "normalChangeTexChrId", 1480);
                        //Util.SetCellValue(row, "normalChangeAnimChrId", 6201);
                        Util.SetCellValue(row, "ModelDispMask0", true);
                        Util.SetCellValue(row, "ModelDispMask1", true);
                        Util.SetCellValue(row, "ModelDispMask2", true);
                        Util.SetCellValue(row, "ModelDispMask4", true);
                        Util.SetCellValue(row, "ModelDispMask5", true);
                        Util.SetCellValue(row, "ModelDispMask6", true);
                    }
                    if (!npcParamIds.ContainsKey(row.ID)) continue;
                    row.Name = "edit";
                    PARAM.Row phase2Row = Util.CopyRow(row, paramdef);
                    phase2Row.ID = npcParamIds[row.ID];

                    int npcSpEffectIndex = 0;
                    List<int> npcSpEffectIds = new List<PARAM.Cell>(row.Cells)
                        .FindAll(cell => cell.Def.DisplayName.StartsWith("spEffectId"))
                        .Select(cell => (int)cell.Value)
                        .Where(spEffectId => spEffectId != 0)
                        .ToList();

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        PARAM.Cell cell = row.Cells[i];
                        switch (cell.Def.DisplayName)
                        {
                            case "PhantomParamId":
                            case "TextId":
                            case "subPhantomParamId":
                            case "npcType":
                            case "subNpcType":
                            case "NameId":
                            case "disableRespawn": // buggy wandering NPCs if this isn't included
                                // keep original enemy's values
                                break;
                            case "teamType":
                            case "subTeamType":
                                TeamType team = (TeamType)(byte)cell.Value;
                                if (!Constants.FRIENDLY_TEAMS.Contains(team) && team != TeamType.Indiscriminate)
                                {

                                    cell.Value = TeamType.StrongEnemy;
                                }
                                break;
                            /*
                            case string name when name.StartsWith("spEffectId"):
                                if ((int)cell.Value == 0 && npcSpEffectIndex < npcSpEffectIds.Count)
                                {
                                    cell.Value = npcSpEffectIds[npcSpEffectIndex++];
                                }
                                break;
                            */
                            default:
                                cell.Value = myGaelPhase1Row.Cells[i].Value;
                                break;
                        }
                    }

                    npcSpEffectIndex = 0;
                    for (int i = 0; i < phase2Row.Cells.Count; i++)
                    {
                        PARAM.Cell cell = phase2Row.Cells[i];
                        switch (cell.Def.DisplayName)
                        {
                            case string name when name.StartsWith("ItemLotId"):
                            case "humanityLotId":
                            case "PhantomParamId":
                            case "TextId":
                            case "subPhantomParamId":
                            case "npcType":
                            case "subNpcType":
                            case "disableRespawn":
                            case "NameId":
                            case "disableIntiliazeDead":
                            case "isSoulGetByBoss":
                                // keep original enemy's values
                                break;
                            case "teamType":
                            case "subTeamType":
                                TeamType team = (TeamType)(byte)cell.Value;
                                if (!Constants.FRIENDLY_TEAMS.Contains(team) && team != TeamType.Indiscriminate)
                                {
                                    cell.Value = TeamType.StrongEnemy;
                                }
                                break;
                            case "getSoul":
                                cell.Value = 120000;
                                break;
                            /*
                            case string name when name.StartsWith("spEffectId"):
                                if ((int)cell.Value == 0 && npcSpEffectIndex < npcSpEffectIds.Count)
                                {
                                    cell.Value = npcSpEffectIds[npcSpEffectIndex++];
                                }
                                break;
                            */
                            default:
                                cell.Value = myGaelPhase2Row.Cells[i].Value;
                                break;
                        }
                    }
                    newRows.Add(phase2Row);
                }

                foreach (PARAM.Row row in newRows)
                {
                    Util.InsertRow(param, row);
                }

                tmp_bndf.Bytes = param.Write();
            }

            {
                BinderFile tmpBndf = bnd.Files.Find(bndf => bndf.Name.EndsWith("NpcThinkParam.param")) ?? throw new KeyNotFoundException();
                PARAM param = PARAM.Read(tmpBndf.Bytes);
                string paramdefFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "NPC_THINK_PARAM_ST.xml");
                PARAMDEF paramdef = PARAMDEF.XmlDeserialize(paramdefFile);
                param.ApplyParamdef(paramdef);

                PARAM.Row gaelPhase1Row = param.Rows.Find(row => row.ID == 620100) ?? throw new KeyNotFoundException();
                foreach (PARAM.Row row in param.Rows)
                {
                    if (!thinkParamIds.Contains(row.ID)) continue;
                    row.Name = "edit";
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        PARAM.Cell cell = row.Cells[i];
                        switch (cell.Def.DisplayName)
                        {
                            case string _ when aggressive:
                                goto default;
                            /*
                            case "logicId":
                            case string name when name.ToLower().Contains("home"):
                            case string name2 when name2.ToLower().Contains("forget"):
                            case string name3 when name3.StartsWith("goal"):
                            case string name4 when name4.StartsWith("enableNavi"):
                            case string name5 when name5.ToLower().Contains("state"):
                            case "battleGoalId" when !friendlyThinkParamIds.Contains(row.ID):
                                cell.Value = gaelPhase1Row.Cells[i].Value;
                                break;
                            */

                            //case string name1 when name1.EndsWith("_angX"):
                            //case string name2 when name2.EndsWith("_angY"):
                            //case "logicId":
                            case string name0 when name0.EndsWith("_dist"):
                            case string name1 when name1.EndsWith("Dark"):
                            //case string name4 when name4.StartsWith("callHelp_") && !name4.Contains("Action"):
                            case "teamAttackEffectivity":
                            case "nonBattleActLife":
                            case "BattleStartDist":
                            case "disablePathMove":
                            case "interestCategory":
                            case "thinkAttrDoAdmirer":
                                break; // keep original value
                            default:
                                cell.Value = gaelPhase1Row.Cells[i].Value;
                                break;
                        }
                    }
                }

                tmpBndf.Bytes = param.Write();
            }

            /*
            {
                BinderFile tmpBndf = bnd.Files.Find(bndf => bndf.Name.EndsWith("CharaInitParam.param")) ?? throw new KeyNotFoundException();
                PARAM param = PARAM.Read(tmpBndf.Bytes);
                string paramdefFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "CHARACTER_INIT_PARAM.xml");
                PARAMDEF paramdef = PARAMDEF.XmlDeserialize(paramdefFile);
                param.ApplyParamdef(paramdef);

                PARAM.Row deprivedRow = param.Rows.Find(row => row.ID == 0) ?? throw new KeyNotFoundException();
                foreach (PARAM.Row row in param.Rows)
                {
                    if (!charaInitIds.Contains(row.ID)) continue;
                    row.Name = "edit";
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        PARAM.Cell cell = row.Cells[i];
                        if (cell.Def.DisplayName.StartsWith("Item_"))
                        {
                            // pass
                        }
                        else
                        {
                            cell.Value = deprivedRow.Cells[i].Value;
                        }
                    }
                }

                tmpBndf.Bytes = param.Write();
            }
            */

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

            {
                // boss souls params
                BinderFile tmpBndf = bnd.Files.Find(bndf => bndf.Name.EndsWith("GameAreaParam.param")) ?? throw new KeyNotFoundException();
                PARAM param = PARAM.Read(tmpBndf.Bytes);
                string paramdefFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "GAME_AREA_PARAM_ST.xml");
                PARAMDEF paramdef = PARAMDEF.XmlDeserialize(paramdefFile);
                param.ApplyParamdef(paramdef);

                foreach (PARAM.Row row in param.Rows)
                {
                    row.Name = "edit";
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        PARAM.Cell cell = row.Cells[i];
                        switch (cell.Def.DisplayName)
                        {
                            case "BonusSoul_single":
                            case "BonusSoul_multi":
                                cell.Value = 0;// 120000;
                                break;
                        }
                    }
                }

                tmpBndf.Bytes = param.Write();
            }

            SFUtil.EncryptDS3Regulation(Constants.MOD_DIR + filename, bnd);
        }
    }
}
