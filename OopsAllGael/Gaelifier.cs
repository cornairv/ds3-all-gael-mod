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
        static readonly string[] map_ids =
        {
            "m30_00_00_00",
            "m30_01_00_00",
            "m31_00_00_00",
            "m32_00_00_00",
            "m33_00_00_00",
            "m34_01_00_00",
            "m35_00_00_00",
            "m37_00_00_00",
            "m38_00_00_00",
            "m39_00_00_00",
            "m40_00_00_00",
            "m41_00_00_00",
            "m45_00_00_00",
            "m50_00_00_00",
            "m51_00_00_00",
            "m51_01_00_00"
        };
        const string MOD_DIR = @"mod\";

        static Dictionary<int, PARAM.Row> npc_params;  // npc param id => npc params

        public static void Gaelify()
        {
            Directory.CreateDirectory(@"mod\event");
            Directory.CreateDirectory(@"mod\script");
            Directory.CreateDirectory(@"mod\map\mapstudio");
            Directory.CreateDirectory(@"mod\sfx");

            InsertParams();
            InsertEnemies();
            InsertCommonEvents();
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
            foreach (string map_id in map_ids)
            {
                string filename = map_id + ".luabnd.dcx";
                BND4 bnd = BND4.Read(@"script\" + filename);
                bnd.Files.InsertRange(0, gael_scripts);
                bnd.Write(@"mod\script\" + filename);
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
            foreach (string map_id in map_ids)
            {
                string filename = @"map\mapstudio\" + map_id + ".msb.dcx";
                MSB3 msb = MSB3.Read(filename);
                msb.Models.Enemies.AddRange(gael_models);
                List<MSB3.Part.Enemy> new_gaels = new List<MSB3.Part.Enemy>();

                foreach (MSB3.Part.Enemy enemy in msb.Parts.Enemies)
                {
                    TeamType team = (TeamType)Util.GetCellValue<byte>(npc_params[enemy.NPCParamID], "subTeamType");
                    if (Array.FindIndex(enemy_types_to_replace, t => t == team) == -1)
                    {
                        continue;
                    }

                    enemy.ModelName = "c6201";
                    enemy.NPCParamID = 620100 + 30000;
                    enemy.ThinkParamID = 620100;

                    // add a phase 2 Gael
                    MSB3.Part.Enemy new_gael = Util.CopyEnemy(enemy);
                    new_gael.EntityID = enemy.EntityID * 10;
                    new_gael.ModelName = "c6200";
                    new_gael.NPCParamID = 620000 + 30000;
                    new_gael.Name = enemy.Name + " (2)";
                    new_gael.ThinkParamID = 620000;
                    new_gaels.Add(new_gael);

                    InsertEnemyEvents(map_id, enemy, new_gael);
                }
                msb.Parts.Enemies.AddRange(new_gaels);
                msb.Write(MOD_DIR + filename);
            }
        }

        static void InsertEnemyEvents(string map_id, MSB3.Part.Enemy gael1, MSB3.Part.Enemy gael2)
        {
            string filename = @"event\" + map_id + ".emevd.dcx";
            EMEVD emevd = EMEVD.Read(filename);

            EventArg<int> gael1_id = "param1";
            EMEVD.Event newEvent = new EventBuilder()
                .Event(123456789, EMEVD.Event.RestBehaviorType.Restart)
                .Param(gael1_id)
                .IfCharacterHpRatio(ConditionGroup.Main, gael1_id, ComparisonType.Less, 0.5f, ComparisonType.Equal, 1)
                .Build();

            emevd.Events.Add(newEvent);

            emevd.Write(MOD_DIR + filename);
        }

        static void InsertCommonEvents()
        {

        }

        static void InsertSfx()
        {
            string filename = @"sfx\frpg_sfxbnd_commoneffects_effect.ffxbnd.dcx";
            BND4 common_sfx_bnd = BND4.Read(filename);
            BND4 gael_sfx_bnd = BND4.Read(@"sfx\frpg_sfxbnd_m51_effect.ffxbnd.dcx");
            common_sfx_bnd.Files.InsertRange(0, gael_sfx_bnd.Files);
            common_sfx_bnd.Write(MOD_DIR + filename);
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
            npc_params = new Dictionary<int,PARAM.Row>();
            foreach (PARAM.Row row in param.Rows)
            {
                npc_params[row.ID] = row;
            }

            // create a new entry for Gael phase 1 with modified parameters
            PARAM.Row gael_row = param.Rows.Find(row => row.ID == 620100) ?? throw new KeyNotFoundException();
            PARAM.Row new_row = Util.CopyRow(gael_row, paramdef);
            new_row.ID = gael_row.ID + 30000;
            new_row.Name = "gael1";
            Util.InsertRow(param, new_row);

            // create a new entry for Gael phase 2 with modified parameters
            gael_row = param.Rows.Find(row => row.ID == 620000) ?? throw new KeyNotFoundException();
            new_row = Util.CopyRow(gael_row, paramdef);
            new_row.ID = gael_row.ID + 30000;
            new_row.Name = "gael2";
            Util.ModifyCellValue(new_row, "Hp", (int value) => (int) Math.Ceiling(value * 0.65));
            Util.SetCellValue(new_row, "getSoul", 120000);
            Util.SetCellValue(new_row, "isSoulGetByBoss", true);
            Util.SetCellValue(new_row, "disableIntiliazeDead", false);
            Util.InsertRow(param, new_row);

            tmp_bndf.Bytes = param.Write();

            SFUtil.EncryptDS3Regulation(MOD_DIR + filename, bnd);
        }
    }
}
