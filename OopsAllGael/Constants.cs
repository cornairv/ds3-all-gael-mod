using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsAllGael
{
    static class Constants
    {
        public static readonly string[] MAP_IDS =
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
        public static readonly string[] _MAP_IDS =
        {
            "m40_00_00_00"
        };

        public static readonly int[] BOSS_IDS =
        {
            3000800,
            3000830,
            3000899,
            3010800,
            3100800,
            3200851,
            3300801,
            3300850,
            3410200,
            3410830,
            3410832,
            3500800,
            3700800,
            3700850,
            3700851,
            3800830,
            3900800,
            4000800,
            4000830,
            4100800,
            4500801,
            4500802,
            4500861,
            5000801,
            5000802,
            5110800,
            5110801
        };

        public const string MOD_DIR = @"mod\";

        public const int GAEL_PHASE1_PARAM_ID = 620100;
        public const int GAEL_PHASE2_PARAM_ID = 620000;

        public const int MY_GAEL_PHASE1_PARAM_ID = 650100;
        public const int MY_GAEL_PHASE2_PARAM_ID = 650000;

        public const int GAEL_EVENT_ID = 30000000;
        public const int GAEL_BOSS_EVENT = 30000001;
        public const int GAEL_NPC_EVENT = 30000002;

        public const float GAEL_AGGRO_RANGE = 20.0f;
    }
}
