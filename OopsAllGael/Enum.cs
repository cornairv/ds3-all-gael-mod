using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsAllGael
{
    namespace EventEnum
    {
        ////////// Event script enums (generated from DarkScript3's json) //////////

        enum OnOff
        {
            Off = 0,
            On = 1
        }
        enum OnOffChange
        {
            Off = 0,
            On = 1,
            Change = 2
        }
        enum TargetEventFlagType
        {
            EventFlag = 0,
            EventId = 1,
            EventIdSlotNumber = 2
        }
        enum ComparisonType
        {
            Equal = 0,
            Notequal = 1,
            Greater = 2,
            Less = 3,
            Greaterorequal = 4,
            Lessorequal = 5
        }
        enum LogicalOperationType
        {
            AllOn = 0,
            AllOff = 1,
            NotAllOff = 2,
            NotAllOn = 3
        }
        enum CalculationType
        {
            Add = 0,
            Sub = 1,
            Mult = 2,
            Div = 3,
            Mod = 4,
            Assign = 5
        }
        enum ConditionGroup
        {
            Or_15 = -15,
            Or_14 = -14,
            Or_13 = -13,
            Or_12 = -12,
            Or_11 = -11,
            Or_10 = -10,
            Or_09 = -9,
            Or_08 = -8,
            Or_07 = -7,
            Or_06 = -6,
            Or_05 = -5,
            Or_04 = -4,
            Or_03 = -3,
            Or_02 = -2,
            Or_01 = -1,
            Main = 0,
            And_01 = 1,
            And_02 = 2,
            And_03 = 3,
            And_04 = 4,
            And_05 = 5,
            And_06 = 6,
            And_07 = 7,
            And_08 = 8,
            And_09 = 9,
            And_10 = 10,
            And_11 = 11,
            And_12 = 12,
            And_13 = 13,
            And_14 = 14,
            And_15 = 15
        }
        enum CountDirection
        {
            Up = 0,
            Down = 1
        }
        enum CutscenePlayMode
        {
            Skippable = 0,
            Unskippable = 2,
            SkippableWithFadeOut = 8,
            UnskippableWithFadeOut = 10
        }
        enum DestructionState
        {
            Undestroyed = 0,
            Destroyed = 1
        }
        enum InsideOutsideState
        {
            Outside = 0,
            Inside = 1
        }
        enum ConditionState
        {
            Fail = 0,
            Pass = 1
        }
        enum DeathState
        {
            Alive = 0,
            Dead = 1
        }
        enum OwnershipState
        {
            DoesnTOwn = 0,
            Owns = 1
        }
        enum EventEndType
        {
            End = 0,
            Restart = 1
        }
        enum TeamType
        {
            Default = -1,
            Disabled = 0,
            Human = 1,
            WhitePhantom = 2,
            BlackPhantom = 3,
            Hollow = 4,
            WanderingPhantom = 5,
            Enemy = 6,
            StrongEnemy = 7,
            Ally = 8,
            HostileAlly = 9,
            DecoyEnemy = 10,
            ChildOfRed = 11,
            FriendlyEnemy = 12,
            Invader = 13,
            Host = 19,
            CoOp = 20,
            Hostile = 21,
            WanderingPhantom1 = 22,
            Enemy1 = 23,
            Enemy2 = 24,
            StrongEnemy1 = 25,
            FriendlyNpc = 26,
            HostileNpc = 27,
            CoOpNpc = 28,
            Indiscriminate = 29,
            Object = 30,
            WhiteBerserker = 31,
            RedBerserker = 32,
            ArchEnemyTeam = 33
        }
        enum TargetEntityType
        {
            Object = 0,
            Area = 1,
            Character = 2
        }
        enum DisabledEnabled
        {
            Disabled = 0,
            Enabled = 1
        }
        enum BitopType
        {
            Add = 0,
            Delete = 1,
            Invert = 2
        }
        enum NavimeshType
        {
            Solid = 1,
            Exit = 2,
            Obstacle = 4,
            Wall = 8,
            LandingPoint = 64,
            Event = 128,
            Cliff = 256,
            OpenSpace = 512,
            Ladder = 1024,
            Hole = 2048,
            Door = 4096,
            ClosedDoor = 8192,
            WallTouchingFloor = 32
        }
        enum PromptType
        {
            YesNo = 0,
            OkCancel = 1
        }
        enum NumberOfOptions
        {
            OneButton = 1,
            TwoButtons = 2,
            NoButtons = 6
        }
        enum EventState
        {
            Completed = 0,
            Restart = 1,
            Waiting = 2,
            Running = 3
        }
        enum SoundType
        {
            A_environmentalSound = 0,
            C_characterMotion = 1,
            F_menuSe = 2,
            O_object = 3,
            P_cutsceneSe = 4,
            S_sfx = 5,
            M_bgm = 6,
            V_voice = 7,
            X_dependsOnFloorMaterial = 8,
            B_dependsOnArmorMaterial = 9,
            G_ghost = 10
        }
        enum DamageTargetType
        {
            Character = 1,
            Map = 2,
            CharacterMap = 3
        }
        enum ItemType
        {
            Weapon = 0,
            Armor = 1,
            Ring = 2,
            Goods = 3
        }
        enum ReactionType
        {
            HollowHuman = 48,
            All = 255
        }
        enum CallType
        {
            ObjectAction = 0,
            WanderingSystem = 1,
            Other = 255
        }
        enum TargetType
        {
            Alive = 0,
            WhitePhantom = 1,
            BlackPhantom = 2,
            Hollow = 8,
            Invader = 12
        }
        enum StatueType
        {
            StoneStatue = 0,
            CrystalStatue = 1
        }
        enum TextBannerType
        {
            HeirOfFireDestroyed = 1,
            Dead = 2,
            Revival = 3,
            SoulRecovery = 4,
            TargetDefeated = 5,
            PhantomDeath = 6,
            BlackPhantomDeath = 7,
            MapName = 8,
            BeginMatch = 12,
            HollowArenaDraw = 14,
            HollowArenaWin = 15,
            HollowArenaLoss = 16,
            Unknown = 17,
            DutyFulfilled = 19,
            LordOfCinderFallen = 22
        }
        enum MultiplayerState
        {
            Host = 0,
            Client = 1,
            TryingToCreateSession = 2,
            TryingToJoinSession = 3,
            LeavingSession = 4,
            FailedToCreateSession = 5
        }
        enum NpcPartType
        {
            Part1 = 1,
            Part2 = 2,
            Part3 = 3,
            Part4 = 4,
            Part5 = 5,
            Part6 = 6,
            Part7 = 7,
            Part8 = 8,
            Part9 = 9,
            Part10 = 10,
            Part11 = 11,
            Part12 = 12,
            Part13 = 13,
            Part14 = 14,
            Part15 = 15,
            Part16 = 16,
            Part17 = 17,
            Part18 = 18,
            Part19 = 19,
            Part20 = 20,
            Part21 = 21,
            Part22 = 22,
            Part23 = 23,
            Part24 = 24,
            Part25 = 25,
            Part26 = 26,
            Part27 = 27,
            Part28 = 28,
            Part29 = 29,
            Part30 = 30,
            WeakPoint = 31
        }
        enum AuthorityLevel
        {
            Normal = 0,
            Forced = 4095
        }
        enum HandleRespawnType
        {
            Ignore = 0,
            RestartEvent = 1,
            EndEvent = 2
        }
        enum AiStateType
        {
            Normal = 0,
            Recognition = 1,
            Alert = 2,
            Combat = 3
        }
        enum SummonSignType
        {
            WhiteSign = 0,
            BlackSign = 1,
            RedSign = 2,
            DetectionSign = 3,
            WhiteReliefSign = 4,
            BlackReliefSign = 5
        }
        enum ClassType
        {
            Warrior = 0,
            Knight = 1,
            Wanderer = 2,
            Thief = 3,
            Bandit = 4,
            Hunter = 5,
            Sorcerer = 6,
            Pyromancer = 7,
            Cleric = 8,
            Deprived = 9,
            TempWarrior = 20,
            TempKnight = 21,
            TempSorcerer = 22,
            TempPyromancer = 23,
            ChiWarrior = 24,
            ChiKnight = 25,
            ChiSorcerer = 26,
            ChiPyromancer = 27
        }
        enum CharacterUpdateFrequency
        {
            NoUpdate = -1,
            AlwaysUpdate = 0,
            Every2Frames = 2,
            Every5Frames = 5
        }
        enum TendencyType
        {
            WhiteTendency = 0,
            BlackTendency = 1
        }
        enum Interpolation
        {
            Interpolated = 0,
            Uninterpolated = 1
        }
        enum DisplayState
        {
            Hide = 0,
            Display = 1
        }
        enum AddDelete
        {
            Delete = 0,
            Add = 1
        }
        enum DamageType
        {
            Unspecified = 0,
            Fire = 1,
            Magic = 2,
            Lightning = 3,
            Blunt = 4,
            Slash = 5,
            Thrust = 6,
            None = 7,
            AllPhysical = 8
        }
        enum Label
        {
            Label0 = 0,
            Label1 = 1,
            Label2 = 2,
            Label3 = 3,
            Label4 = 4,
            Label5 = 5,
            Label6 = 6,
            Label7 = 7,
            Label8 = 8,
            Label9 = 9,
            Label10 = 10,
            Label11 = 11,
            Label12 = 12,
            Label13 = 13,
            Label14 = 14,
            Label15 = 15,
            Label16 = 16,
            Label17 = 17,
            Label18 = 18,
            Label19 = 19,
            Label20 = 20
        }
        enum ArmorType
        {
            Head = 0,
            Body = 1,
            Arms = 2,
            Legs = 3
        }
        enum PlayerPlaylogParameter
        {
            PrimaryParameters = 0,
            TemporaryParameters = 1,
            Weapon = 2,
            Armor = 3
        }
        enum PlaylogMultiplayerType
        {
            HostOnly = 0,
            GuestOnly = 1,
            Both = 2
        }
        enum SingleplayerSummonSignType
        {
            NormalCoOp = 0,
            ScriptedInvasion = 1,
            ScriptedInvasionWideArea = 2
        }
        enum Gender
        {
            Male = 0,
            Female = 1
        }
        enum DoorState
        {
            DoorClosed = 0,
            DoorOpen = 1,
            DoorClosing = 2,
            DoorOpening = 3
        }
        enum ClientType
        {
            CoOp = 0,
            Invader = 1,
            BetrayalInvader = 2
        }
        enum GenericDialogResult
        {
            Cancel = 0,
            Left = 1,
            Right = 2,
            Leave = 3
        }
        enum PlaygoState
        {
            Downloaded1stChunk = 0,
            BdInstalling = 1,
            Installed = 2
        }
        enum HollowArenaMatchType
        {
            Duel = 0,
            TwoPlayerBrawl = 1,
            FourPlayerBrawl = 2,
            SixPlayerBrawl = 3,
            TwoVersusThree = 4,
            ThreeVersusThree = 5
        }
        enum HollowArenaResult
        {
            Win = 0,
            Draw = 1
        }
    }
}
