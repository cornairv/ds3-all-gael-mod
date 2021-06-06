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
                List<int> gaelSpEffectIds = new List<PARAM.Cell>(gaelPhase1Row.Cells)
                    .FindAll(cell => cell.Def.DisplayName.StartsWith("spEffectId"))
                    .Select(cell => (int)cell.Value)
                    .ToList();
                //Util.SetCellValue(gael1Row, "subTeamType", (byte)teamType);

                // create a new entry for Gael phase 2 with modified parameters
                PARAM.Row myGaelPhase2Row = Util.CopyRow(gaelPhase2Row, paramdef);
                myGaelPhase2Row.ID = Constants.MY_GAEL_PHASE2_PARAM_ID;
                myGaelPhase2Row.Name = "gael2";
                Util.ModifyCellValue(myGaelPhase2Row, "Hp", (int value) => (int)Math.Ceiling(value * 0.65));
                Util.SetCellValue(myGaelPhase2Row, "getSoul", 120000);
                Util.SetCellValue(myGaelPhase2Row, "isSoulGetByBoss", true);
                Util.SetCellValue(myGaelPhase2Row, "disableIntiliazeDead", false);
                Util.SetCellValue(myGaelPhase2Row, "subTeamType", (byte)TeamType.StrongEnemy);

                foreach (PARAM.Row row in param.Rows)
                {
                    if (!npcParamIds.Contains(row.ID)) continue;
                    row.Name = "edit";
                    int gaelSpEffectIndex = 0;
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        PARAM.Cell cell = row.Cells[i];
                        //cell.Value = myGaelPhase1Row.Cells[i].Value;
                        //continue;
                        if (cell.Def.DisplayName.StartsWith("ItemLotId") || cell.Def.DisplayName == "PhantomParamId"
                            || cell.Def.DisplayName == "TextId" || cell.Def.DisplayName == "subPhantomParamId")
                        {
                            // pass
                        }
                        else if (cell.Def.DisplayName.StartsWith("spEffectId"))
                        {
                            if ((int)cell.Value == 0 && gaelSpEffectIndex < gaelSpEffectIds.Count)
                            {
                                cell.Value = gaelSpEffectIds[gaelSpEffectIndex++];
                            }
                        }
                        /*
                        else if (cell.Def.DisplayName == "normalChangeAnimChrId")
                        {
                            // make sure param has an override anim or we found an anim in InsertEnemies(), but not both
                            if ((short)cell.Value == -1)
                            {
                                MyDebug.Assert(npcParamIdToAnimId.ContainsKey(row.ID));
                                cell.Value = npcParamIdToAnimId[row.ID];
                            }
                            else
                            {
                                MyDebug.Assert(!npcParamIdToAnimId.ContainsKey(row.ID));
                            }
                        }
                        */
                        else if (cell.Def.DisplayName.StartsWith("normalChange"))
                        {
                            //cell.Value = -1;
                        }
                        else
                        {
                            cell.Value = myGaelPhase1Row.Cells[i].Value;
                        }
                    }
                }
                Util.InsertRow(param, myGaelPhase1Row);
                Util.InsertRow(param, myGaelPhase2Row);

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
                        //cell.Value = gaelPhase1Row.Cells[i].Value;
                        //continue;
                        if (cell.Def.DisplayName == "logicId" || cell.Def.DisplayName == "battleGoalId")
                        {
                            cell.Value = gaelPhase1Row.Cells[i].Value;
                        }
                        else
                        {
                            // pass
                        }
                    }
                }

                tmpBndf.Bytes = param.Write();
            }

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
