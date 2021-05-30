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
            Or15 = -15,
            Or14 = -14,
            Or13 = -13,
            Or12 = -12,
            Or11 = -11,
            Or10 = -10,
            Or09 = -9,
            Or08 = -8,
            Or07 = -7,
            Or06 = -6,
            Or05 = -5,
            Or04 = -4,
            Or03 = -3,
            Or02 = -2,
            Or01 = -1,
            Main = 0,
            And01 = 1,
            And02 = 2,
            And03 = 3,
            And04 = 4,
            And05 = 5,
            And06 = 6,
            And07 = 7,
            And08 = 8,
            And09 = 9,
            And10 = 10,
            And11 = 11,
            And12 = 12,
            And13 = 13,
            And14 = 14,
            And15 = 15
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
        enum Bool
        {
            False = 0,
            True = 1
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
            AEnvironmentalSound = 0,
            CCharacterMotion = 1,
            FMenuSe = 2,
            OObject = 3,
            PCutsceneSe = 4,
            SSfx = 5,
            MBgm = 6,
            VVoice = 7,
            XDependsOnFloorMaterial = 8,
            BDependsOnArmorMaterial = 9,
            GGhost = 10
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

    // class and id combined for each instruction type
    enum InstrType
    {
        IfConditionGroup = (0 << 8) | 0,
        IfParameterComparison = (0 << 8) | 1,
        IfElapsedSeconds = (1 << 8) | 0,
        IfElapsedFrames = (1 << 8) | 1,
        IfRandomElapsedSeconds = (1 << 8) | 2,
        IfRandomElapsedFrames = (1 << 8) | 3,
        IfEventFlag = (3 << 8) | 0,
        IfBatchEventFlags = (3 << 8) | 1,
        IfInOutsideArea = (3 << 8) | 2,
        IfEntityInOutsideRadiusOfEntity = (3 << 8) | 3,
        IfPlayerHasDoesnTHaveItem = (3 << 8) | 4,
        IfActionButton = (3 << 8) | 5,
        IfMultiplayerState = (3 << 8) | 6,
        IfAllPlayersInOutsideArea = (3 << 8) | 7,
        IfPlayerInOutMap = (3 << 8) | 8,
        IfMultiplayerEvent = (3 << 8) | 9,
        IfCountEventFlags = (3 << 8) | 10,
        IfWorldTendency = (3 << 8) | 11,
        IfEventValue = (3 << 8) | 12,
        IfActionButtonBossFog = (3 << 8) | 13,
        IfDroppedItemsInArea = (3 << 8) | 14,
        IfDroppedItem = (3 << 8) | 15,
        IfPlayerHasDoesnTHaveItemIncludingBbox = (3 << 8) | 16,
        IfGameCycle = (3 << 8) | 17,
        IfActionButtonLineSegment = (3 << 8) | 18,
        IfActionButtonLineSegmentBossRoom = (3 << 8) | 19,
        IfCompareEventValues = (3 << 8) | 20,
        IfOwnsDlc = (3 << 8) | 21,
        IfOnlineMode = (3 << 8) | 22,
        IfDamageType = (3 << 8) | 23,
        IfActionButtonInArea = (3 << 8) | 24,
        IfPlayerIsNotInOwnWorldExcludesArena = (3 << 8) | 26,
        IfMapCeremonyState = (3 << 8) | 28,
        IfMultiplayerNetworkPenalized = (3 << 8) | 29,
        IfPlayerGender = (3 << 8) | 30,
        IfOngoingCutsceneFinished = (3 << 8) | 31,
        IfHollowArenaMatchReadyState = (3 << 8) | 32,
        IfHollowArenaSoloResults = (3 << 8) | 33,
        IfHollowArenaSoloScoreComparison = (3 << 8) | 34,
        IfHollowArenaTeamResults = (3 << 8) | 35,
        IfSteamDisconnected = (3 << 8) | 38,
        IfFriendlyPhantomCountComparison = (3 << 8) | 39,
        IfCharacterDeadAlive = (4 << 8) | 0,
        IfCharacterDamagedBy = (4 << 8) | 1,
        IfCharacterHpRatio = (4 << 8) | 2,
        IfCharacterType = (4 << 8) | 3,
        IfCharacterTargetedBy = (4 << 8) | 4,
        IfCharacterHasSpeffect = (4 << 8) | 5,
        IfNpcPartHp = (4 << 8) | 6,
        IfCharacterBackreadStatus = (4 << 8) | 7,
        IfCharacterHasEventMessage = (4 << 8) | 8,
        IfCharacterAiState = (4 << 8) | 9,
        IfPlayerUsingSkullLantern = (4 << 8) | 10,
        IfPlayerSClass = (4 << 8) | 11,
        IfPlayerSCovenant = (4 << 8) | 12,
        IfPlayerSSoulLevel = (4 << 8) | 13,
        IfCharacterHpValue = (4 << 8) | 14,
        IfCharacterDrawgroupState = (4 << 8) | 15,
        IfPlayerRemainingYoelLevelCount = (4 << 8) | 26,
        IfCharacterInvadeType = (4 << 8) | 27,
        IfCharacterChameleon = (4 << 8) | 28,
        IfObjectDestroyed = (5 << 8) | 0,
        IfObjectHitBy = (5 << 8) | 1,
        IfObjactEventFlag = (5 << 8) | 2,
        IfObjectHp = (5 << 8) | 3,
        IfObjectBurnState = (5 << 8) | 9,
        IfObjectBackread = (5 << 8) | 10,
        IfObjectBackreadUnknown511 = (5 << 8) | 11,
        IfPlayerMovingOnHit = (11 << 8) | 0,
        IfPlayerAttackingOnHit = (11 << 8) | 1,
        IfPlayerStandingOnHit = (11 << 8) | 2,
        WaitForConditionGroupState = (1000 << 8) | 0,
        SkipIfConditionGroupStateUncompiled = (1000 << 8) | 1,
        EndIfConditionGroupStateUncompiled = (1000 << 8) | 2,
        SkipUnconditionally = (1000 << 8) | 3,
        EndUnconditionally = (1000 << 8) | 4,
        SkipIfComparison = (1000 << 8) | 5,
        EndIfComparison = (1000 << 8) | 6,
        SkipIfConditionGroupStateCompiled = (1000 << 8) | 7,
        EndIfConditionGroupStateCompiled = (1000 << 8) | 8,
        WaitForNetworkApproval = (1000 << 8) | 9,
        GotoIfConditionGroupStateUncompiled = (1000 << 8) | 101,
        GotoUnconditionally = (1000 << 8) | 103,
        GotoIfComparison = (1000 << 8) | 105,
        GotoIfConditionGroupStateCompiled = (1000 << 8) | 107,
        WaitFixedTimeSeconds = (1001 << 8) | 0,
        WaitFixedTimeFrames = (1001 << 8) | 1,
        WaitRandomTimeSeconds = (1001 << 8) | 2,
        WaitRandomTimeFrames = (1001 << 8) | 3,
        WaitHollowArenaHalfTimeStayparamLookup = (1001 << 8) | 4,
        WaitForEventFlag = (1003 << 8) | 0,
        SkipIfEventFlag = (1003 << 8) | 1,
        EndIfEventFlag = (1003 << 8) | 2,
        SkipIfBatchEventFlags = (1003 << 8) | 3,
        EndIfBatchEventFlags = (1003 << 8) | 4,
        SkipIfMultiplayerState = (1003 << 8) | 5,
        EndIfMultiplayerState = (1003 << 8) | 6,
        SkipIfPlayerInOutMap = (1003 << 8) | 7,
        EndIfPlayerInOutMap = (1003 << 8) | 8,
        SkipIfNumberOfCoOpClients = (1003 << 8) | 9,
        EndIfNumberOfCoOpClients = (1003 << 8) | 10,
        GotoIfCharacterHasSpeffect = (1003 << 8) | 11,
        GotoIfPlayerIsNotInOwnWorldExcludesArena = (1003 << 8) | 12,
        EndIfPlayerIsNotInOwnWorldExcludesArena = (1003 << 8) | 13,
        SkipIfNumberOfClientsOfType = (1003 << 8) | 14,
        GotoIfNumberOfClientsOfType = (1003 << 8) | 15,
        EndIfNumberOfClientsOfType = (1003 << 8) | 16,
        GotoIfEventFlag = (1003 << 8) | 101,
        GotoIfBatchEventFlags = (1003 << 8) | 103,
        GotoIfMultiplayerState = (1003 << 8) | 105,
        GotoIfPlayerInOutMap = (1003 << 8) | 107,
        GotoIfNumberOfCoOpClients = (1003 << 8) | 109,
        EndIfCharacterHasSpeffect = (1003 << 8) | 111,
        SkipIfCharacterHasSpeffect = (1003 << 8) | 112,
        GotoIfPlayerInOutsideArea = (1003 << 8) | 200,
        EndIfPlayerInOutsideArea = (1003 << 8) | 201,
        SkipIfPlayerInOutsideArea = (1003 << 8) | 202,
        GotoIfHollowArenaMatchType = (1003 << 8) | 211,
        WaitForObjectDestroyed = (1005 << 8) | 0,
        SkipIfObjectDestroyed = (1005 << 8) | 1,
        EndIfObjectDestroyed = (1005 << 8) | 2,
        GotoIfObjectDestroyed = (1005 << 8) | 101,
        Label0 = (1014 << 8) | 0,
        Label1 = (1014 << 8) | 1,
        Label2 = (1014 << 8) | 2,
        Label3 = (1014 << 8) | 3,
        Label4 = (1014 << 8) | 4,
        Label5 = (1014 << 8) | 5,
        Label6 = (1014 << 8) | 6,
        Label7 = (1014 << 8) | 7,
        Label8 = (1014 << 8) | 8,
        Label9 = (1014 << 8) | 9,
        Label10 = (1014 << 8) | 10,
        Label11 = (1014 << 8) | 11,
        Label12 = (1014 << 8) | 12,
        Label13 = (1014 << 8) | 13,
        Label14 = (1014 << 8) | 14,
        Label15 = (1014 << 8) | 15,
        Label16 = (1014 << 8) | 16,
        Label17 = (1014 << 8) | 17,
        Label18 = (1014 << 8) | 18,
        Label19 = (1014 << 8) | 19,
        Label20 = (1014 << 8) | 20,
        InitializeEvent = (2000 << 8) | 0,
        TerminateEvent = (2000 << 8) | 1,
        SetNetworkSyncState = (2000 << 8) | 2,
        ClearCompiledConditionGroupState = (2000 << 8) | 3,
        PrefetchRequest = (2000 << 8) | 4,
        SaveRequest = (2000 << 8) | 5,
        InitializeCommonEvent = (2000 << 8) | 6,
        PlayCutsceneToAll = (2002 << 8) | 1,
        PlayCutsceneAndWarpAllPlayers = (2002 << 8) | 2,
        PlayCutsceneToPlayer = (2002 << 8) | 3,
        PlayCutsceneAndWarpPlayer = (2002 << 8) | 4,
        PlayCutsceneAndRotatePlayerAroundVerticalAxis = (2002 << 8) | 5,
        PlayCutsceneChangeTimePeriodAndWarpPlayer = (2002 << 8) | 6,
        PlayCutsceneAndChangeTimePeriod = (2002 << 8) | 7,
        DummyPlayCutsceneAndWarpPlayer = (2002 << 8) | 8,
        PlayCutsceneChangeMapCeremonyAndWarpPlayer = (2002 << 8) | 9,
        PlayCutsceneToPlayerAndChangeCurrentMapCeremony = (2002 << 8) | 10,
        PlayOngoingCutsceneAndWarpPlayer = (2002 << 8) | 11,
        PlayCutsceneAndWarpPlayerUnknown200212 = (2002 << 8) | 12,
        RequestAnimationPlayback = (2003 << 8) | 1,
        SetEventFlag = (2003 << 8) | 2,
        DeActivateGenerator = (2003 << 8) | 3,
        AwardItemLot = (2003 << 8) | 4,
        ShootBullet = (2003 << 8) | 5,
        DeActivateMapHit = (2003 << 8) | 6,
        SetMapVisibility = (2003 << 8) | 7,
        SetEventState = (2003 << 8) | 8,
        InvertEventFlag = (2003 << 8) | 9,
        SetEventNavimesh = (2003 << 8) | 10,
        DisplayBossHealthBar = (2003 << 8) | 11,
        HandleBossDefeat = (2003 << 8) | 12,
        ModifyNavimeshConnectionBitflag = (2003 << 8) | 13,
        WarpPlayer = (2003 << 8) | 14,
        HandleMinibossDefeat = (2003 << 8) | 15,
        TriggerMultiplayerEvent = (2003 << 8) | 16,
        RandomlySetEventFlagInRange = (2003 << 8) | 17,
        ForceAnimationPlayback = (2003 << 8) | 18,
        SetAreaDrawparamBankSlotIndex = (2003 << 8) | 19,
        SetTemporaryPlayerRespawnPoint = (2003 << 8) | 20,
        IncrementGameCycle = (2003 << 8) | 21,
        BatchSetEventFlags = (2003 << 8) | 22,
        SetPlayerRespawnPoint = (2003 << 8) | 23,
        RemoveItemFromPlayer = (2003 << 8) | 24,
        PlaceNpcSummonSign = (2003 << 8) | 25,
        SetVisibilityOfMessage = (2003 << 8) | 26,
        Missing = (2003 << 8) | 27,
        AwardAchievement = (2003 << 8) | 28,
        ChangeWorldTendency = (2003 << 8) | 29,
        DisableVagrantSpawning = (2003 << 8) | 30,
        IncrementEventValue = (2003 << 8) | 31,
        ClearEventValue = (2003 << 8) | 32,
        SetSnugglyNextTrade = (2003 << 8) | 33,
        SpawnSnugglyItem = (2003 << 8) | 34,
        MoveBloodstainAndDroppedItems = (2003 << 8) | 35,
        AwardItemsIncludingClients = (2003 << 8) | 36,
        BattleOfStoicism1v1RankingRequest = (2003 << 8) | 37,
        BattleOfStoicism2v2RankingRequest = (2003 << 8) | 38,
        BattleOfStoicismFfaRankingRequest = (2003 << 8) | 39,
        BattleOfStoicismExitRequest = (2003 << 8) | 40,
        EventValueOperation = (2003 << 8) | 41,
        StoreItemAmountHeldInEventValue = (2003 << 8) | 42,
        DirectlyGivePlayerItem = (2003 << 8) | 43,
        ToggleDirectionDisplay = (2003 << 8) | 44,
        SetMapHitgridCorrespondence = (2003 << 8) | 45,
        SetMapContentImageDisplay = (2003 << 8) | 46,
        SetMapBoundariesDisplay = (2003 << 8) | 47,
        SetAreaWind = (2003 << 8) | 48,
        WarpPlayerToRespawnPoint = (2003 << 8) | 49,
        StartEnemyGenerator = (2003 << 8) | 50,
        SummonNpc = (2003 << 8) | 51,
        InitializeWarpObject = (2003 << 8) | 52,
        MakeEnemyAppearEvent = (2003 << 8) | 54,
        SetCurrentMapCeremony = (2003 << 8) | 57,
        SetMapCeremony = (2003 << 8) | 59,
        DisplayEpitaphMessage = (2003 << 8) | 61,
        SetNetworkConnectedEventFlag = (2003 << 8) | 62,
        BatchSetNetworkConnectedEventFlags = (2003 << 8) | 63,
        SetOmissionModeCounts = (2003 << 8) | 64,
        ResetOmissionModeCountsToDefault = (2003 << 8) | 65,
        InitializeCrowTrade = (2003 << 8) | 66,
        InitializeCrowTradeArea = (2003 << 8) | 67,
        SetNetworkInteractionState = (2003 << 8) | 70,
        HideHud = (2003 << 8) | 71,
        SetBonfireWarpingState = (2003 << 8) | 72,
        SetAutogeneratedEventSpecificEventFlag1Unknown200373 = (2003 << 8) | 73,
        HandleBossDefeatAndDisplayBanner = (2003 << 8) | 74,
        SetAutogeneratedEventSpecificEventFlag2Unknown200375 = (2003 << 8) | 75,
        DisableLoadingScreenTips = (2003 << 8) | 76,
        AwardGestureItem = (2003 << 8) | 77,
        SendNpcSummonHome = (2003 << 8) | 78,
        Unknown200379 = (2003 << 8) | 79,
        DisplayDecoratedBossHealthBar = (2003 << 8) | 80,
        PlaceNpcSummonSignWorksForNonEmberedPlayers = (2003 << 8) | 81,
        SetCharacterAiState = (2004 << 8) | 1,
        SetCharacterTeamType = (2004 << 8) | 2,
        CharacterWarpRequest = (2004 << 8) | 3,
        ForceCharacterDeath = (2004 << 8) | 4,
        ChangeCharacterEnableState = (2004 << 8) | 5,
        EzstateInstructionRequest = (2004 << 8) | 6,
        CreateBulletOwner = (2004 << 8) | 7,
        SetSpeffect = (2004 << 8) | 8,
        SetSpecialAnimationS = (2004 << 8) | 9,
        SetCharacterGravity = (2004 << 8) | 10,
        SetCharacterEventTarget = (2004 << 8) | 11,
        SetCharacterImmortality = (2004 << 8) | 12,
        SetCharacterHome = (2004 << 8) | 13,
        RotateCharacter = (2004 << 8) | 14,
        SetCharacterInvincibility = (2004 << 8) | 15,
        ClearCharacterSAiTarget = (2004 << 8) | 16,
        RequestCharacterAiCommand = (2004 << 8) | 17,
        SetEventPoint = (2004 << 8) | 18,
        SetCharacterAiId = (2004 << 8) | 19,
        RequestCharacterAiRePlan = (2004 << 8) | 20,
        ClearSpeffect = (2004 << 8) | 21,
        CreateNpcPart = (2004 << 8) | 22,
        SetNpcPartHp = (2004 << 8) | 23,
        SetNpcPartSeAndSfx = (2004 << 8) | 24,
        SetNpcPartBulletDamageMultiplier = (2004 << 8) | 25,
        ChangeCharacterDispmask = (2004 << 8) | 26,
        ChangeCharacterHitmask = (2004 << 8) | 27,
        SetNetworkUpdateAuthority = (2004 << 8) | 28,
        SetCharacterBackreadState = (2004 << 8) | 29,
        SetCharacterHpBarDisplay = (2004 << 8) | 30,
        SetCharacterMaphit = (2004 << 8) | 31,
        RequestCharacterAiCommandEventValue = (2004 << 8) | 32,
        CreateReferredDamagePair = (2004 << 8) | 33,
        SetNetworkUpdateRate = (2004 << 8) | 34,
        SetCharacterDefaultBackreadState = (2004 << 8) | 35,
        HellkiteBreathControl = (2004 << 8) | 36,
        ForceCharacterTreasure = (2004 << 8) | 37,
        BetrayCharacterSCurrentCovenant = (2004 << 8) | 38,
        SetCharacterAnimationState = (2004 << 8) | 39,
        WarpCharacterAndSetFloor = (2004 << 8) | 40,
        IssueShortWarpRequest = (2004 << 8) | 41,
        WarpCharacterAndCopyFloor = (2004 << 8) | 42,
        RequestCharacterAnimationReset = (2004 << 8) | 43,
        ChangeCharacterTeamTypeAndForceExitSpecialAnimations = (2004 << 8) | 44,
        RegisterNpcHumanityEventValue = (2004 << 8) | 45,
        IncrementPlayerPvpSin = (2004 << 8) | 46,
        BonfireLikeRecovery = (2004 << 8) | 47,
        ChangeCharacterSCloth = (2004 << 8) | 48,
        ChangeCharacterPatrolBehavior = (2004 << 8) | 49,
        SetLockOnPoint = (2004 << 8) | 50,
        TestRequestRagdollRestraint = (2004 << 8) | 51,
        ChangeCharacter = (2004 << 8) | 52,
        AdaptHpChangingSpeffectToNpcPartOfTarget = (2004 << 8) | 53,
        ImmediateActivateUnknown200454 = (2004 << 8) | 54,
        SetCharacterTalkRange = (2004 << 8) | 55,
        IncrementCharacterGameCycle = (2004 << 8) | 57,
        SetMultiplayerDependentBuffsNonBoss = (2004 << 8) | 58,
        Unknown200459 = (2004 << 8) | 59,
        SetCharacterRemainingYoelLevels = (2004 << 8) | 60,
        RequestObjectDestruction = (2005 << 8) | 1,
        RequestObjectRestoration = (2005 << 8) | 2,
        DeActivateObject = (2005 << 8) | 3,
        SetObjectTreasureState = (2005 << 8) | 4,
        InitializeObjact = (2005 << 8) | 5,
        SetObjactState = (2005 << 8) | 6,
        ReproduceObjectAnimation = (2005 << 8) | 7,
        ReproduceObjectDestruction = (2005 << 8) | 8,
        CreateDamagingObject = (2005 << 8) | 9,
        RegisterStatue = (2005 << 8) | 10,
        WarpObjectToCharacter = (2005 << 8) | 11,
        DeleteObjectEvent = (2005 << 8) | 12,
        SetObjectInvulnerability = (2005 << 8) | 13,
        DeActivateObjactAssignIdx = (2005 << 8) | 14,
        RerollObjectTreasure = (2005 << 8) | 15,
        ExtinguishBurningObjects = (2005 << 8) | 16,
        ShowObjectByMapCeremony = (2005 << 8) | 17,
        DestroyObject = (2005 << 8) | 19,
        DeleteMapSfx = (2006 << 8) | 1,
        SpawnMapSfx = (2006 << 8) | 2,
        SpawnOneshotSfx = (2006 << 8) | 3,
        CreateObjectFollowingSfx = (2006 << 8) | 4,
        DeleteObjectFollowingSfx = (2006 << 8) | 5,
        DisplayGenericDialog = (2007 << 8) | 1,
        DisplayBanner = (2007 << 8) | 2,
        DisplayStatusMessage = (2007 << 8) | 3,
        DisplayMessage = (2007 << 8) | 4,
        SetBattleOfStocismNametagA = (2007 << 8) | 5,
        SetBattleOfStocismNametagB = (2007 << 8) | 6,
        SetBattleOfStocismNametagC = (2007 << 8) | 7,
        SetBattleOfStocismNametagD = (2007 << 8) | 8,
        DisplayBattleOfStocismDissolutionMessage = (2007 << 8) | 9,
        DisplayGenericDialogAndSetEventFlags = (2007 << 8) | 10,
        DisplayAreaWelcomeMessage = (2007 << 8) | 11,
        DisplayHollowArenaPvpMessage = (2007 << 8) | 12,
        ChangeCamera = (2008 << 8) | 1,
        SetCameraVibration = (2008 << 8) | 2,
        SetLockcamSlotNumber = (2008 << 8) | 3,
        RegisterLadder = (2009 << 8) | 0,
        InitializeWanderingDemon = (2009 << 8) | 1,
        RegisterWanderingDemon = (2009 << 8) | 2,
        RegisterBonfire = (2009 << 8) | 3,
        ActivateMultiplayerDependantBuffs = (2009 << 8) | 4,
        RegisterHealingFountain = (2009 << 8) | 5,
        IssueBossRoomEntryNotification = (2009 << 8) | 6,
        SendInvadingPhantomsHome = (2009 << 8) | 8,
        SendAllPhantomsHomeAndUpdateServerPvpStats = (2009 << 8) | 10,
        SendAllPhantomsHome = (2009 << 8) | 11,
        PlayBgm = (2010 << 8) | 1,
        PlaySe = (2010 << 8) | 2,
        SetMapSoundState = (2010 << 8) | 3,
        EnableBossMapSound = (2010 << 8) | 4,
        NotifySoundDampeningOfDoorEvent = (2010 << 8) | 5,
        EnableMapSoundWithFade = (2010 << 8) | 6,
        Unknown201007 = (2010 << 8) | 7,
        ActivateHit = (2011 << 8) | 1,
        ActivateHitBackreadMask = (2011 << 8) | 2,
        ActivateHitres = (2011 << 8) | 3,
        ActivateHitAndCreateNavimesh = (2011 << 8) | 4,
        ActivateMapPart = (2012 << 8) | 1,
        SetAreaWelcomeMessageState = (2012 << 8) | 8,
        CreatePlaylog = (2013 << 8) | 1,
        StartTimeMeasurement = (2013 << 8) | 2,
        EndTimeMeasurement = (2013 << 8) | 3,
        ParameterOutput = (2013 << 8) | 4
    }
}
