using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;

namespace OopsAllGael
{
    public struct EventArg<T>
    {
        readonly T value;
        readonly string name;

        EventArg(T value)
        {
            this.value = value;
            name = null;
        }

        public EventArg(string name)
        {
            value = default;
            this.name = name;
        }

        public bool IsParam => name != null;
        public string ParamName => name ?? throw new InvalidOperationException();
        public T Value => name == null ? value : throw new InvalidOperationException();

        public static implicit operator EventArg<T>(T arg) => new EventArg<T>(arg);
        public static implicit operator EventArg<T>(string arg) => new EventArg<T>(arg);
        //public static implicit operator string(EventArg<T> arg) => arg.ParamName;
    }

    class EventBuilder
    {
        EMEVD.Event event_;

        List<string> paramNames;
        List<Type> paramTypes;
        List<int> paramOffsets;
        List<int> paramSizes;

        public EMEVD.Event Build()
        {
            return event_;
        }

        public EventBuilder Event(long id, EMEVD.Event.RestBehaviorType restBehavior)
        {
            event_ = new EMEVD.Event(id, restBehavior);
            paramNames = new List<string>();
            paramTypes = new List<Type>();
            paramOffsets = new List<int>();
            paramSizes = new List<int>();
            return this;
        }

        public EventBuilder Param<T>(string paramName, int size = 4)
        {
            paramNames.Add(paramName);
            paramTypes.Add(typeof(T));
            paramOffsets.Add(paramOffsets.LastOrDefault() + paramSizes.LastOrDefault());
            paramSizes.Add(size);
            return this;
        }

        public EventBuilder UncheckedParam(string paramName, int size = 4)
        {
            paramNames.Add(paramName);
            paramTypes.Add(null);
            paramOffsets.Add(paramOffsets.LastOrDefault() + paramSizes.LastOrDefault());
            paramSizes.Add(size);
            return this;
        }

        object CreateArgObject<T>(EventArg<T> arg, EMEVD.Instruction.ArgType argType, ref int offset)
        {
            object value = 0;
            if (arg.IsParam)
            {
                int i = paramNames.IndexOf(arg.ParamName);
                if (i == -1)
                {
                    throw new ArgumentException("Parameter does not exist", arg.ParamName);
                }

                Type expectedType = typeof(T);
                Type actualType = paramTypes[i];
                if (expectedType != actualType)
                {
                    //throw new ArgumentException(string.Format("Expected {0}, got {1}", expectedType, actualType), arg.ParamName);
                    Console.WriteLine(string.Format("Type warning: Expected {0}, got {1} {2", expectedType, actualType, arg.ParamName));

                }

                EMEVD.Parameter param = new EMEVD.Parameter(event_.Instructions.Count, offset, paramOffsets[i], paramSizes[i]);
                event_.Parameters.Add(param);
            }
            else
            {
                value = arg.Value;
            }

            switch (argType)
            {
                case EMEVD.Instruction.ArgType.Byte:
                    offset += 1;
                    return Convert.ToByte(value);
                case EMEVD.Instruction.ArgType.SByte:
                    offset += 1;
                    return Convert.ToSByte(value);
                case EMEVD.Instruction.ArgType.Int16:
                    offset += 2;
                    return Convert.ToInt16(value);
                case EMEVD.Instruction.ArgType.UInt16:
                    offset += 2;
                    return Convert.ToUInt16(value);
                case EMEVD.Instruction.ArgType.Int32:
                    offset += 4;
                    return Convert.ToInt32(value);
                case EMEVD.Instruction.ArgType.UInt32:
                    offset += 4;
                    return Convert.ToUInt32(value);
                case EMEVD.Instruction.ArgType.Single:
                    offset += 4;
                    return Convert.ToSingle(value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        ////////// Instruction functions (generated from DarkScript3's json) //////////

        public EventBuilder IfConditionGroup(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(0, 0, args));
            return this;
        }
        public EventBuilder IfParameterComparison(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(leftHandSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(rightHandSide, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(0, 1, args));
            return this;
        }
        public EventBuilder IfElapsedSeconds(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<float> targetNumberOfSeconds)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetNumberOfSeconds, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1, 0, args));
            return this;
        }
        public EventBuilder IfElapsedFrames(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetNumberOfFrames)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetNumberOfFrames, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1, 1, args));
            return this;
        }
        public EventBuilder IfRandomElapsedSeconds(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<float> targetSecondsMin,
            EventArg<float> targetSecondsMax)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetSecondsMin, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(targetSecondsMax, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1, 2, args));
            return this;
        }
        public EventBuilder IfRandomElapsedFrames(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetFramesMin,
            EventArg<int> targetFramesMax)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetFramesMin, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetFramesMax, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1, 3, args));
            return this;
        }
        public EventBuilder IfEventFlag(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.OnOffChange> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 0, args));
            return this;
        }
        public EventBuilder IfBatchEventFlags(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(startingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(endingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 1, args));
            return this;
        }
        public EventBuilder IfInOutsideArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 2, args));
            return this;
        }
        public EventBuilder IfEntityInOutsideRadiusOfEntity(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityAId,
            EventArg<int> targetEntityBId,
            EventArg<float> targetDistance,
            EventArg<int> minimumNumberOfTargets)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityAId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEntityBId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(minimumNumberOfTargets, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 3, args));
            return this;
        }
        public EventBuilder IfPlayerHasDoesnTHaveItem(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<EventEnum.OwnershipState> desiredPossessionState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(desiredPossessionState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 4, args));
            return this;
        }
        public EventBuilder IfActionButton(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TargetEntityType> targetEntityType,
            EventArg<int> targetEntityId,
            EventArg<float> reactionAngleDeg,
            EventArg<short> dummypolyId,
            EventArg<float> reactionDistance,
            EventArg<int> helpMessageId,
            EventArg<EventEnum.ReactionType> reactionType,
            EventArg<int> padId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionAngleDeg, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(reactionDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(helpMessageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(padId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 5, args));
            return this;
        }
        public EventBuilder IfMultiplayerState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredMultiplayerState, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 6, args));
            return this;
        }
        public EventBuilder IfAllPlayersInOutsideArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> areaEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 7, args));
            return this;
        }
        public EventBuilder IfPlayerInOutMap(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(shouldPlayerBeInside, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 8, args));
            return this;
        }
        public EventBuilder IfMultiplayerEvent(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<uint> multiplayerEventId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(multiplayerEventId, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 9, args));
            return this;
        }
        public EventBuilder IfCountEventFlags(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> countThreshold)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(startingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(endingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(countThreshold, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 10, args));
            return this;
        }
        public EventBuilder IfWorldTendency(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TendencyType> tendencyType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> tendencyThreshold)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(tendencyType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(tendencyThreshold, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 11, args));
            return this;
        }
        public EventBuilder IfEventValue(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> baseEventFlagId,
            EventArg<byte> numberOfUsedFlagBits,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<uint> thresholdValue)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(baseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfUsedFlagBits, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(thresholdValue, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 12, args));
            return this;
        }
        public EventBuilder IfActionButtonBossFog(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TargetEntityType> targetEntityType,
            EventArg<int> targetEntityId,
            EventArg<float> reactionAngleDeg,
            EventArg<short> dummypolyId,
            EventArg<float> reactionDistance,
            EventArg<int> helpMessageId,
            EventArg<EventEnum.ReactionType> reactionType,
            EventArg<int> padId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionAngleDeg, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(reactionDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(helpMessageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(padId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 13, args));
            return this;
        }
        public EventBuilder IfDroppedItemsInArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> areaEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 14, args));
            return this;
        }
        public EventBuilder IfDroppedItem(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 15, args));
            return this;
        }
        public EventBuilder IfPlayerHasDoesnTHaveItemIncludingBbox(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<EventEnum.OwnershipState> desiredPossessionState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(desiredPossessionState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 16, args));
            return this;
        }
        public EventBuilder IfGameCycle(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> completedGameCyclesThreshold)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(completedGameCyclesThreshold, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 17, args));
            return this;
        }
        public EventBuilder IfActionButtonLineSegment(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TargetEntityType> targetEntityType,
            EventArg<int> targetEntityId,
            EventArg<float> reactionAngleDeg,
            EventArg<short> dummypolyId,
            EventArg<float> reactionDistance,
            EventArg<int> helpMessageId,
            EventArg<EventEnum.ReactionType> reactionType,
            EventArg<int> padId,
            EventArg<int> lineSegmentDefinedByEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionAngleDeg, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(reactionDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(helpMessageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(padId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(lineSegmentDefinedByEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 18, args));
            return this;
        }
        public EventBuilder IfActionButtonLineSegmentBossRoom(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TargetEntityType> targetEntityType,
            EventArg<int> targetEntityId,
            EventArg<float> reactionAngleDeg,
            EventArg<short> dummypolyId,
            EventArg<float> reactionDistance,
            EventArg<int> helpMessageId,
            EventArg<EventEnum.ReactionType> reactionType,
            EventArg<int> padId,
            EventArg<int> lineSegmentDefinedByEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionAngleDeg, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(reactionDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(helpMessageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(padId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(lineSegmentDefinedByEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 19, args));
            return this;
        }
        public EventBuilder IfCompareEventValues(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> leftSideBaseEventFlagId,
            EventArg<byte> leftSideNumberOfUsedFlagBits,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> rightSideBaseEventFlagId,
            EventArg<byte> rightSideNumberOfUsedFlagBits)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(leftSideBaseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(leftSideNumberOfUsedFlagBits, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(rightSideBaseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(rightSideNumberOfUsedFlagBits, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 20, args));
            return this;
        }
        public EventBuilder IfOwnsDlc(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> ownsDlc)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(ownsDlc, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 21, args));
            return this;
        }
        public EventBuilder IfOnlineMode(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> onlineMode)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(onlineMode, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 22, args));
            return this;
        }
        public EventBuilder IfDamageType(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> attackerEntityId,
            EventArg<EventEnum.DamageType> damageType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(attackerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(damageType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 23, args));
            return this;
        }
        public EventBuilder IfActionButtonInArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> actionButtonParameterId,
            EventArg<int> startingPointAreaEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(actionButtonParameterId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(startingPointAreaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 24, args));
            return this;
        }
        public EventBuilder IfPlayerIsNotInOwnWorldExcludesArena(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> isNotIn)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(isNotIn, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 26, args));
            return this;
        }
        public EventBuilder IfMapCeremonyState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.DisabledEnabled> desiredState,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<ushort> ceremonyId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(ceremonyId, EMEVD.Instruction.ArgType.UInt16, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 28, args));
            return this;
        }
        public EventBuilder IfMultiplayerNetworkPenalized(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 29, args));
            return this;
        }
        public EventBuilder IfPlayerGender(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.Gender> gender)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(gender, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 30, args));
            return this;
        }
        public EventBuilder IfOngoingCutsceneFinished(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> cutsceneId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 31, args));
            return this;
        }
        public EventBuilder IfHollowArenaMatchReadyState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> ready)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(ready, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 32, args));
            return this;
        }
        public EventBuilder IfHollowArenaSoloResults(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.HollowArenaResult> result)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(result, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 33, args));
            return this;
        }
        public EventBuilder IfHollowArenaSoloScoreComparison(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetScore,
            EventArg<int> unknown)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetScore, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 34, args));
            return this;
        }
        public EventBuilder IfHollowArenaTeamResults(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.HollowArenaResult> result)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(result, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 35, args));
            return this;
        }
        public EventBuilder IfSteamDisconnected(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> disconnected)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(disconnected, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 38, args));
            return this;
        }
        public EventBuilder IfFriendlyPhantomCountComparison(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> desiredComparisonState,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> targetCount)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(desiredComparisonState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetCount, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(3, 39, args));
            return this;
        }
        public EventBuilder IfCharacterDeadAlive(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.DeathState> desiredLifeState,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(desiredLifeState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 0, args));
            return this;
        }
        public EventBuilder IfCharacterDamagedBy(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> attackerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(attackerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 1, args));
            return this;
        }
        public EventBuilder IfCharacterHpRatio(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> targetHpRatio,
            EventArg<EventEnum.ComparisonType> comparisonType2,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetHpRatio, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(comparisonType2, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 2, args));
            return this;
        }
        public EventBuilder IfCharacterType(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.TargetType> targetType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 3, args));
            return this;
        }
        public EventBuilder IfCharacterTargetedBy(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> aggressorEntityId,
            EventArg<int> targetEntityId,
            EventArg<bool> shouldHave)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(aggressorEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldHave, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 4, args));
            return this;
        }
        public EventBuilder IfCharacterHasSpeffect(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(speffectId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldHave, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 5, args));
            return this;
        }
        public EventBuilder IfNpcPartHp(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> npcPartId,
            EventArg<int> hpThreshold,
            EventArg<EventEnum.ComparisonType> comparisonType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(npcPartId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(hpThreshold, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 6, args));
            return this;
        }
        public EventBuilder IfCharacterBackreadStatus(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<bool> isBackread,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(isBackread, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 7, args));
            return this;
        }
        public EventBuilder IfCharacterHasEventMessage(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> targetEventMessageId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEventMessageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldHave, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 8, args));
            return this;
        }
        public EventBuilder IfCharacterAiState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.AiStateType> aiState,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(aiState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 9, args));
            return this;
        }
        public EventBuilder IfPlayerUsingSkullLantern(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> usingSkullLantern)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(usingSkullLantern, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 10, args));
            return this;
        }
        public EventBuilder IfPlayerSClass(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ClassType> classType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(classType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 11, args));
            return this;
        }
        public EventBuilder IfPlayerSCovenant(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<byte> covenantIndex)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(covenantIndex, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 12, args));
            return this;
        }
        public EventBuilder IfPlayerSSoulLevel(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<uint> targetSoulLevel)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetSoulLevel, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 13, args));
            return this;
        }
        public EventBuilder IfCharacterHpValue(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> targetHp,
            EventArg<EventEnum.ComparisonType> comparisonType2,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetHp, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType2, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 14, args));
            return this;
        }
        public EventBuilder IfCharacterDrawgroupState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetCharacterEntityId,
            EventArg<bool> insideDrawgroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetCharacterEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(insideDrawgroup, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 15, args));
            return this;
        }
        public EventBuilder IfPlayerRemainingYoelLevelCount(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> targetLevelCount)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetLevelCount, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 26, args));
            return this;
        }
        public EventBuilder IfCharacterInvadeType(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<byte> invadeTypeUnknown,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(invadeTypeUnknown, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 27, args));
            return this;
        }
        public EventBuilder IfCharacterChameleon(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetCharacterEntityId,
            EventArg<int> chameleonSfxId,
            EventArg<bool> isTransformed)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetCharacterEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(chameleonSfxId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(isTransformed, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(4, 28, args));
            return this;
        }
        public EventBuilder IfObjectDestroyed(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(damageState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetObjects, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(5, 0, args));
            return this;
        }
        public EventBuilder IfObjectHitBy(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> attackerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(attackerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(5, 1, args));
            return this;
        }
        public EventBuilder IfObjactEventFlag(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> objactEventFlag)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(objactEventFlag, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(5, 2, args));
            return this;
        }
        public EventBuilder IfObjectHp(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> hpThreshold)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(hpThreshold, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(5, 3, args));
            return this;
        }
        public EventBuilder IfObjectBurnState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> burnStateUnknown,
            EventArg<EventEnum.ComparisonType> comparisonType2,
            EventArg<float> numberOfTargetObjects)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(burnStateUnknown, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType2, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetObjects, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(5, 9, args));
            return this;
        }
        public EventBuilder IfObjectBackread(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<bool> isBackread,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(isBackread, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetObjects, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(5, 10, args));
            return this;
        }
        public EventBuilder IfObjectBackreadUnknown511(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<bool> isBackread,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(isBackread, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetObjects, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(5, 11, args));
            return this;
        }
        public EventBuilder IfPlayerMovingOnHit(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> hitEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(11, 0, args));
            return this;
        }
        public EventBuilder IfPlayerAttackingOnHit(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> hitEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(11, 1, args));
            return this;
        }
        public EventBuilder IfPlayerStandingOnHit(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> hitEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(resultConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(11, 2, args));
            return this;
        }
        public EventBuilder WaitForConditionGroupState(
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 0, args));
            return this;
        }
        public EventBuilder SkipIfConditionGroupStateUncompiled(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 1, args));
            return this;
        }
        public EventBuilder EndIfConditionGroupStateUncompiled(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 2, args));
            return this;
        }
        public EventBuilder SkipUnconditionally(
            EventArg<byte> numberOfSkippedLines)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 3, args));
            return this;
        }
        public EventBuilder EndUnconditionally(
            EventArg<EventEnum.EventEndType> executionEndType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 4, args));
            return this;
        }
        public EventBuilder SkipIfComparison(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(leftHandSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(rightHandSide, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 5, args));
            return this;
        }
        public EventBuilder EndIfComparison(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(leftHandSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(rightHandSide, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 6, args));
            return this;
        }
        public EventBuilder SkipIfConditionGroupStateCompiled(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 7, args));
            return this;
        }
        public EventBuilder EndIfConditionGroupStateCompiled(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 8, args));
            return this;
        }
        public EventBuilder WaitForNetworkApproval(
            EventArg<float> timeoutS)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(timeoutS, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 9, args));
            return this;
        }
        public EventBuilder GotoIfConditionGroupStateUncompiled(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 101, args));
            return this;
        }
        public EventBuilder GotoUnconditionally(
            EventArg<EventEnum.Label> label)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 103, args));
            return this;
        }
        public EventBuilder GotoIfComparison(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(leftHandSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(rightHandSide, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 105, args));
            return this;
        }
        public EventBuilder GotoIfConditionGroupStateCompiled(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredConditionGroupState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetConditionGroup, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1000, 107, args));
            return this;
        }
        public EventBuilder WaitFixedTimeSeconds(
            EventArg<float> numberOfSeconds)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSeconds, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1001, 0, args));
            return this;
        }
        public EventBuilder WaitFixedTimeFrames(
            EventArg<int> numberOfFrames)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfFrames, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1001, 1, args));
            return this;
        }
        public EventBuilder WaitRandomTimeSeconds(
            EventArg<float> minNumberOfSeconds,
            EventArg<float> maxNumberOfSeconds)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(minNumberOfSeconds, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(maxNumberOfSeconds, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1001, 2, args));
            return this;
        }
        public EventBuilder WaitRandomTimeFrames(
            EventArg<int> minNumberOfFrames,
            EventArg<int> maxNumberOfFrames)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(minNumberOfFrames, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(maxNumberOfFrames, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1001, 3, args));
            return this;
        }
        public EventBuilder WaitHollowArenaHalfTimeStayparamLookup(
            EventArg<EventEnum.HollowArenaMatchType> matchType,
            EventArg<bool> isSecondHalf)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(matchType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(isSecondHalf, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1001, 4, args));
            return this;
        }
        public EventBuilder WaitForEventFlag(
            EventArg<EventEnum.OnOffChange> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 0, args));
            return this;
        }
        public EventBuilder SkipIfEventFlag(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.OnOff> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 1, args));
            return this;
        }
        public EventBuilder EndIfEventFlag(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.OnOff> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 2, args));
            return this;
        }
        public EventBuilder SkipIfBatchEventFlags(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(startingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(endingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 3, args));
            return this;
        }
        public EventBuilder EndIfBatchEventFlags(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(startingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(endingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 4, args));
            return this;
        }
        public EventBuilder SkipIfMultiplayerState(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredMultiplayerState, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 5, args));
            return this;
        }
        public EventBuilder EndIfMultiplayerState(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredMultiplayerState, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 6, args));
            return this;
        }
        public EventBuilder SkipIfPlayerInOutMap(
            EventArg<byte> numberOfSkippedLines,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(shouldPlayerBeInside, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 7, args));
            return this;
        }
        public EventBuilder EndIfPlayerInOutMap(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(shouldPlayerBeInside, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 8, args));
            return this;
        }
        public EventBuilder SkipIfNumberOfCoOpClients(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetNumberOfClients, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 9, args));
            return this;
        }
        public EventBuilder EndIfNumberOfCoOpClients(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetNumberOfClients, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 10, args));
            return this;
        }
        public EventBuilder GotoIfCharacterHasSpeffect(
            EventArg<EventEnum.Label> label,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(speffectId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldHave, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 11, args));
            return this;
        }
        public EventBuilder GotoIfPlayerIsNotInOwnWorldExcludesArena(
            EventArg<EventEnum.Label> label,
            EventArg<bool> isNotIn)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(isNotIn, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 12, args));
            return this;
        }
        public EventBuilder EndIfPlayerIsNotInOwnWorldExcludesArena(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<bool> isNotIn)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(isNotIn, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 13, args));
            return this;
        }
        public EventBuilder SkipIfNumberOfClientsOfType(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ClientType> clientType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(clientType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetNumberOfClients, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 14, args));
            return this;
        }
        public EventBuilder GotoIfNumberOfClientsOfType(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ClientType> clientType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(clientType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetNumberOfClients, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 15, args));
            return this;
        }
        public EventBuilder EndIfNumberOfClientsOfType(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ClientType> clientType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(clientType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetNumberOfClients, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 16, args));
            return this;
        }
        public EventBuilder GotoIfEventFlag(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.OnOff> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 101, args));
            return this;
        }
        public EventBuilder GotoIfBatchEventFlags(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredFlagState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEventFlagType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(startingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(endingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 103, args));
            return this;
        }
        public EventBuilder GotoIfMultiplayerState(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredMultiplayerState, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 105, args));
            return this;
        }
        public EventBuilder GotoIfPlayerInOutMap(
            EventArg<EventEnum.Label> label,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(shouldPlayerBeInside, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 107, args));
            return this;
        }
        public EventBuilder GotoIfNumberOfCoOpClients(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetNumberOfClients, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 109, args));
            return this;
        }
        public EventBuilder EndIfCharacterHasSpeffect(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(speffectId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldHave, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 111, args));
            return this;
        }
        public EventBuilder SkipIfCharacterHasSpeffect(
            EventArg<byte> numberOfSkippedLines,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(speffectId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldHave, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 112, args));
            return this;
        }
        public EventBuilder GotoIfPlayerInOutsideArea(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 200, args));
            return this;
        }
        public EventBuilder EndIfPlayerInOutsideArea(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 201, args));
            return this;
        }
        public EventBuilder SkipIfPlayerInOutsideArea(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfTargetCharacters, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 202, args));
            return this;
        }
        public EventBuilder GotoIfHollowArenaMatchType(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.HollowArenaMatchType> matchType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(matchType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1003, 211, args));
            return this;
        }
        public EventBuilder WaitForObjectDestroyed(
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(damageState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1005, 0, args));
            return this;
        }
        public EventBuilder SkipIfObjectDestroyed(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(numberOfSkippedLines, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(damageState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetObjects, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1005, 1, args));
            return this;
        }
        public EventBuilder EndIfObjectDestroyed(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(executionEndType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(damageState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetObjects, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1005, 2, args));
            return this;
        }
        public EventBuilder GotoIfObjectDestroyed(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(label, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(damageState, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(comparisonType, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(numberOfTargetObjects, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(1005, 101, args));
            return this;
        }
        public EventBuilder Label0(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 0));
            return this;
        }
        public EventBuilder Label1(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 1));
            return this;
        }
        public EventBuilder Label2(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 2));
            return this;
        }
        public EventBuilder Label3(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 3));
            return this;
        }
        public EventBuilder Label4(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 4));
            return this;
        }
        public EventBuilder Label5(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 5));
            return this;
        }
        public EventBuilder Label6(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 6));
            return this;
        }
        public EventBuilder Label7(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 7));
            return this;
        }
        public EventBuilder Label8(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 8));
            return this;
        }
        public EventBuilder Label9(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 9));
            return this;
        }
        public EventBuilder Label10(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 10));
            return this;
        }
        public EventBuilder Label11(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 11));
            return this;
        }
        public EventBuilder Label12(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 12));
            return this;
        }
        public EventBuilder Label13(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 13));
            return this;
        }
        public EventBuilder Label14(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 14));
            return this;
        }
        public EventBuilder Label15(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 15));
            return this;
        }
        public EventBuilder Label16(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 16));
            return this;
        }
        public EventBuilder Label17(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 17));
            return this;
        }
        public EventBuilder Label18(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 18));
            return this;
        }
        public EventBuilder Label19(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 19));
            return this;
        }
        public EventBuilder Label20(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(1014, 20));
            return this;
        }
        public EventBuilder InitializeEvent(
            EventArg<int> eventSlotId,
            EventArg<uint> eventId,
            EventArg<uint> parameters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventSlotId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(eventId, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(parameters, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2000, 0, args));
            return this;
        }
        public EventBuilder TerminateEvent(
            EventArg<int> eventSlotId,
            EventArg<uint> eventId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventSlotId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(eventId, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2000, 1, args));
            return this;
        }
        public EventBuilder SetNetworkSyncState(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2000, 2, args));
            return this;
        }
        public EventBuilder ClearCompiledConditionGroupState(
            EventArg<byte> dummy)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(dummy, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2000, 3, args));
            return this;
        }
        public EventBuilder PrefetchRequest(
            EventArg<uint> requestId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(requestId, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2000, 4, args));
            return this;
        }
        public EventBuilder SaveRequest(
            EventArg<byte> dummy)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(dummy, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2000, 5, args));
            return this;
        }
        public EventBuilder InitializeCommonEvent(
            EventArg<uint> eventId,
            EventArg<uint> parameters)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventId, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(parameters, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2000, 6, args));
            return this;
        }
        public EventBuilder PlayCutsceneToAll(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 1, args));
            return this;
        }
        public EventBuilder PlayCutsceneAndWarpAllPlayers(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(pointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 2, args));
            return this;
        }
        public EventBuilder PlayCutsceneToPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 3, args));
            return this;
        }
        public EventBuilder PlayCutsceneAndWarpPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(pointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 4, args));
            return this;
        }
        public EventBuilder PlayCutsceneAndRotatePlayerAroundVerticalAxis(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<float> yAxisXCoordM,
            EventArg<float> yAxisZCoordM,
            EventArg<int> rotationDeg,
            EventArg<float> verticalTranslationM,
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(yAxisXCoordM, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(yAxisZCoordM, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(rotationDeg, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(verticalTranslationM, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 5, args));
            return this;
        }
        public EventBuilder PlayCutsceneChangeTimePeriodAndWarpPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId,
            EventArg<byte> timePeriodId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(pointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(timePeriodId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 6, args));
            return this;
        }
        public EventBuilder PlayCutsceneAndChangeTimePeriod(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> playerEntityId,
            EventArg<byte> timePeriodId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(timePeriodId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 7, args));
            return this;
        }
        public EventBuilder DummyPlayCutsceneAndWarpPlayer(
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(pointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 8, args));
            return this;
        }
        public EventBuilder PlayCutsceneChangeMapCeremonyAndWarpPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<uint> ceremonyId,
            EventArg<int> unknown,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(ceremonyId, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(pointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 9, args));
            return this;
        }
        public EventBuilder PlayCutsceneToPlayerAndChangeCurrentMapCeremony(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<uint> ceremonyId,
            EventArg<int> unknown,
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(ceremonyId, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 10, args));
            return this;
        }
        public EventBuilder PlayOngoingCutsceneAndWarpPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId,
            EventArg<byte> unknown,
            EventArg<byte> unknown2)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(pointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(unknown2, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 11, args));
            return this;
        }
        public EventBuilder PlayCutsceneAndWarpPlayerUnknown200212(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId,
            EventArg<int> pointEntityId2)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(cutsceneId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playbackMethod, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(pointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(pointEntityId2, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2002, 12, args));
            return this;
        }
        public EventBuilder RequestAnimationPlayback(
            EventArg<int> entityId,
            EventArg<int> animationId,
            EventArg<bool> shouldLoop,
            EventArg<bool> shouldWaitForCompletion)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(animationId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldLoop, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(shouldWaitForCompletion, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 1, args));
            return this;
        }
        public EventBuilder SetEventFlag(
            EventArg<int> eventFlagId,
            EventArg<EventEnum.OnOff> flagState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(flagState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 2, args));
            return this;
        }
        public EventBuilder DeActivateGenerator(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 3, args));
            return this;
        }
        public EventBuilder AwardItemLot(
            EventArg<int> itemLotId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(itemLotId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 4, args));
            return this;
        }
        public EventBuilder ShootBullet(
            EventArg<int> bulletTeamEntityId,
            EventArg<int> bulletProducerEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> behaviorId,
            EventArg<int> firingAngleX,
            EventArg<int> firingAngleY,
            EventArg<int> firingAngleZ)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(bulletTeamEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(bulletProducerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(behaviorId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(firingAngleX, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(firingAngleY, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(firingAngleZ, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 5, args));
            return this;
        }
        public EventBuilder DeActivateMapHit(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 6, args));
            return this;
        }
        public EventBuilder SetMapVisibility(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 7, args));
            return this;
        }
        public EventBuilder SetEventState(
            EventArg<int> eventId,
            EventArg<int> eventSlotId,
            EventArg<EventEnum.EventEndType> eventState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(eventSlotId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(eventState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 8, args));
            return this;
        }
        public EventBuilder InvertEventFlag(
            EventArg<int> eventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 9, args));
            return this;
        }
        public EventBuilder SetEventNavimesh(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Int16, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 10, args));
            return this;
        }
        public EventBuilder DisplayBossHealthBar(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            EventArg<int> entityId,
            EventArg<short> slotNumber,
            EventArg<int> nameId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(slotNumber, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(nameId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 11, args));
            return this;
        }
        public EventBuilder HandleBossDefeat(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 12, args));
            return this;
        }
        public EventBuilder ModifyNavimeshConnectionBitflag(
            EventArg<int> entityId,
            EventArg<EventEnum.NavimeshType> navimeshType,
            EventArg<EventEnum.BitopType> typeBitOperation)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(navimeshType, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(typeBitOperation, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 13, args));
            return this;
        }
        public EventBuilder WarpPlayer(
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> initialAreaId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(initialAreaId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 14, args));
            return this;
        }
        public EventBuilder HandleMinibossDefeat(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 15, args));
            return this;
        }
        public EventBuilder TriggerMultiplayerEvent(
            EventArg<uint> multiplayerEventId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(multiplayerEventId, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 16, args));
            return this;
        }
        public EventBuilder RandomlySetEventFlagInRange(
            EventArg<uint> eventFlagIdMin,
            EventArg<uint> eventFlagIdMax,
            EventArg<EventEnum.OnOff> flagState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagIdMin, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(eventFlagIdMax, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(flagState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 17, args));
            return this;
        }
        public EventBuilder ForceAnimationPlayback(
            EventArg<int> entityId,
            EventArg<int> animationId,
            EventArg<bool> shouldLoop,
            EventArg<bool> shouldWaitForCompletion,
            EventArg<bool> ignoreWaitForTransition,
            EventArg<byte> unknown,
            EventArg<float> unknown2)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(animationId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldLoop, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(shouldWaitForCompletion, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(ignoreWaitForTransition, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(unknown2, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 18, args));
            return this;
        }
        public EventBuilder SetAreaDrawparamBankSlotIndex(
            EventArg<short> areaId,
            EventArg<short> areaDrawparamBankSlotIndex)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(areaDrawparamBankSlotIndex, EMEVD.Instruction.ArgType.Int16, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 19, args));
            return this;
        }
        public EventBuilder SetTemporaryPlayerRespawnPoint(
            EventArg<int> respawnPointEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(respawnPointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 20, args));
            return this;
        }
        public EventBuilder IncrementGameCycle(
            EventArg<byte> dummy)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(dummy, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 21, args));
            return this;
        }
        public EventBuilder BatchSetEventFlags(
            EventArg<int> eventFlagIdStart,
            EventArg<int> eventFlagIdEnd,
            EventArg<EventEnum.OnOff> flagState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagIdStart, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(eventFlagIdEnd, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(flagState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 22, args));
            return this;
        }
        public EventBuilder SetPlayerRespawnPoint(
            EventArg<int> respawnPointEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(respawnPointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 23, args));
            return this;
        }
        public EventBuilder RemoveItemFromPlayer(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> number)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(number, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 24, args));
            return this;
        }
        public EventBuilder PlaceNpcSummonSign(
            EventArg<EventEnum.SummonSignType> signType,
            EventArg<int> summonedNpcEntityId,
            EventArg<int> spawnPointEntityId,
            EventArg<int> summonEventFlagId,
            EventArg<int> dismissalEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(signType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(summonedNpcEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(spawnPointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(summonEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dismissalEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 25, args));
            return this;
        }
        public EventBuilder SetVisibilityOfMessage(
            EventArg<int> messageEntityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 26, args));
            return this;
        }
        public EventBuilder Missing(
            EventArg<byte> dummy)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(dummy, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 27, args));
            return this;
        }
        public EventBuilder AwardAchievement(
            EventArg<int> achievementId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(achievementId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 28, args));
            return this;
        }
        public EventBuilder ChangeWorldTendency(
            EventArg<EventEnum.TendencyType> tendencyType,
            EventArg<sbyte> changeValue)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(tendencyType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(changeValue, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 29, args));
            return this;
        }
        public EventBuilder DisableVagrantSpawning(
            EventArg<bool> disabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 30, args));
            return this;
        }
        public EventBuilder IncrementEventValue(
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits,
            EventArg<uint> maximumAllowedValue)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(baseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfUsedFlagBits, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(maximumAllowedValue, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 31, args));
            return this;
        }
        public EventBuilder ClearEventValue(
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(baseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfUsedFlagBits, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 32, args));
            return this;
        }
        public EventBuilder SetSnugglyNextTrade(
            EventArg<int> eventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 33, args));
            return this;
        }
        public EventBuilder SpawnSnugglyItem(
            EventArg<int> itemLotId,
            EventArg<int> placementAreaEntityId,
            EventArg<int> eventFlagId,
            EventArg<int> hitEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(itemLotId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(placementAreaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 34, args));
            return this;
        }
        public EventBuilder MoveBloodstainAndDroppedItems(
            EventArg<int> sourceAreaEntityId,
            EventArg<int> destinationAreaEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(sourceAreaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(destinationAreaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 35, args));
            return this;
        }
        public EventBuilder AwardItemsIncludingClients(
            EventArg<int> itemLotId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(itemLotId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 36, args));
            return this;
        }
        public EventBuilder BattleOfStoicism1v1RankingRequest(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(2003, 37));
            return this;
        }
        public EventBuilder BattleOfStoicism2v2RankingRequest(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(2003, 38));
            return this;
        }
        public EventBuilder BattleOfStoicismFfaRankingRequest(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(2003, 39));
            return this;
        }
        public EventBuilder BattleOfStoicismExitRequest(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(2003, 40));
            return this;
        }
        public EventBuilder EventValueOperation(
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits,
            EventArg<int> operand,
            EventArg<int> baseEventFlagIdOperand,
            EventArg<uint> numberOfUsedEventFlagBitsOperand,
            EventArg<EventEnum.CalculationType> calculationType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(baseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfUsedFlagBits, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(operand, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(baseEventFlagIdOperand, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfUsedEventFlagBitsOperand, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(calculationType, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 41, args));
            return this;
        }
        public EventBuilder StoreItemAmountHeldInEventValue(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(baseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfUsedFlagBits, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 42, args));
            return this;
        }
        public EventBuilder DirectlyGivePlayerItem(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(baseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(numberOfUsedFlagBits, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 43, args));
            return this;
        }
        public EventBuilder ToggleDirectionDisplay(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 44, args));
            return this;
        }
        public EventBuilder SetMapHitgridCorrespondence(
            EventArg<int> hitEntityId,
            EventArg<short> level,
            EventArg<short> gridCoordX,
            EventArg<short> gridCoordY,
            EventArg<EventEnum.AddDelete> process)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(level, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(gridCoordX, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(gridCoordY, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(process, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 45, args));
            return this;
        }
        public EventBuilder SetMapContentImageDisplay(
            EventArg<short> contentImagePartId,
            EventArg<EventEnum.DisplayState> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(contentImagePartId, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 46, args));
            return this;
        }
        public EventBuilder SetMapBoundariesDisplay(
            EventArg<short> hierarchy,
            EventArg<short> gridCoordX,
            EventArg<short> gridCoordY,
            EventArg<EventEnum.DisplayState> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(hierarchy, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(gridCoordX, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(gridCoordY, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 47, args));
            return this;
        }
        public EventBuilder SetAreaWind(
            EventArg<int> areaEntityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            EventArg<float> changeTimeS,
            EventArg<int> windParameterId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(changeTimeS, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(windParameterId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 48, args));
            return this;
        }
        public EventBuilder WarpPlayerToRespawnPoint(
            EventArg<int> respawnPointParameterId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(respawnPointParameterId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 49, args));
            return this;
        }
        public EventBuilder StartEnemyGenerator(
            EventArg<int> enemySpawnerId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(enemySpawnerId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 50, args));
            return this;
        }
        public EventBuilder SummonNpc(
            EventArg<EventEnum.SingleplayerSummonSignType> signType,
            EventArg<int> summonedNpcEntityId,
            EventArg<int> spawnPointEntityId,
            EventArg<int> summonEventFlagId,
            EventArg<int> dismissalEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(signType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(summonedNpcEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(spawnPointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(summonEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dismissalEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 51, args));
            return this;
        }
        public EventBuilder InitializeWarpObject(
            EventArg<int> warpObjectEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(warpObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 52, args));
            return this;
        }
        public EventBuilder MakeEnemyAppearEvent(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 54, args));
            return this;
        }
        public EventBuilder SetCurrentMapCeremony(
            EventArg<uint> ceremonyId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(ceremonyId, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 57, args));
            return this;
        }
        public EventBuilder SetMapCeremony(
            EventArg<uint> areaId,
            EventArg<byte> blockId,
            EventArg<uint> ceremonyId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(ceremonyId, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 59, args));
            return this;
        }
        public EventBuilder DisplayEpitaphMessage(
            EventArg<int> messageId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 61, args));
            return this;
        }
        public EventBuilder SetNetworkConnectedEventFlag(
            EventArg<int> eventFlagId,
            EventArg<EventEnum.OnOff> flagState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(flagState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 62, args));
            return this;
        }
        public EventBuilder BatchSetNetworkConnectedEventFlags(
            EventArg<int> eventFlagIdStart,
            EventArg<int> eventFlagIdEnd,
            EventArg<EventEnum.OnOff> flagState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagIdStart, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(eventFlagIdEnd, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(flagState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 63, args));
            return this;
        }
        public EventBuilder SetOmissionModeCounts(
            EventArg<int> level1Count,
            EventArg<int> level2Count)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(level1Count, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(level2Count, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 64, args));
            return this;
        }
        public EventBuilder ResetOmissionModeCountsToDefault(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(2003, 65));
            return this;
        }
        public EventBuilder InitializeCrowTrade(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> itemLotId,
            EventArg<int> tradeCompletedEventFlagId,
            EventArg<int> crowDialogueResponseEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(itemLotId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(tradeCompletedEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(crowDialogueResponseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 66, args));
            return this;
        }
        public EventBuilder InitializeCrowTradeArea(
            EventArg<int> areaEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 67, args));
            return this;
        }
        public EventBuilder SetNetworkInteractionState(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 70, args));
            return this;
        }
        public EventBuilder HideHud(
            EventArg<bool> shouldBeHidden)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(shouldBeHidden, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 71, args));
            return this;
        }
        public EventBuilder SetBonfireWarpingState(
            EventArg<int> bonfireEntityId,
            EventArg<int> animationId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(bonfireEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(animationId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 72, args));
            return this;
        }
        public EventBuilder SetAutogeneratedEventSpecificEventFlag1Unknown200373(
            EventArg<byte> unknown,
            EventArg<byte> unknown2)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(unknown2, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 73, args));
            return this;
        }
        public EventBuilder HandleBossDefeatAndDisplayBanner(
            EventArg<int> entityId,
            EventArg<EventEnum.TextBannerType> bannerType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(bannerType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 74, args));
            return this;
        }
        public EventBuilder SetAutogeneratedEventSpecificEventFlag2Unknown200375(
            EventArg<byte> unknown,
            EventArg<byte> unknown2)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(unknown2, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 75, args));
            return this;
        }
        public EventBuilder DisableLoadingScreenTips(
            EventArg<bool> shouldBeDisabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(shouldBeDisabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 76, args));
            return this;
        }
        public EventBuilder AwardGestureItem(
            EventArg<ushort> gestureIndex,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(gestureIndex, EMEVD.Instruction.ArgType.UInt16, ref offset),
                CreateArgObject(itemType, EMEVD.Instruction.ArgType.UInt16, ref offset),
                CreateArgObject(itemId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 77, args));
            return this;
        }
        public EventBuilder SendNpcSummonHome(
            EventArg<int> summonEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(summonEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 78, args));
            return this;
        }
        public EventBuilder Unknown200379(
            EventArg<byte> unknown)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 79, args));
            return this;
        }
        public EventBuilder DisplayDecoratedBossHealthBar(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            EventArg<int> entityId,
            EventArg<short> slotNumber,
            EventArg<int> nameId,
            EventArg<byte> decorationId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.SByte, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(slotNumber, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(nameId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(decorationId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 80, args));
            return this;
        }
        public EventBuilder PlaceNpcSummonSignWorksForNonEmberedPlayers(
            EventArg<EventEnum.SummonSignType> signType,
            EventArg<int> summonedNpcEntityId,
            EventArg<int> spawnPointEntityId,
            EventArg<int> summonEventFlagId,
            EventArg<int> dismissalEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(signType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(summonedNpcEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(spawnPointEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(summonEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dismissalEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2003, 81, args));
            return this;
        }
        public EventBuilder SetCharacterAiState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 1, args));
            return this;
        }
        public EventBuilder SetCharacterTeamType(
            EventArg<int> entityId,
            EventArg<EventEnum.TeamType> teamType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(teamType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 2, args));
            return this;
        }
        public EventBuilder CharacterWarpRequest(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(warpType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(warpDestinationEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 3, args));
            return this;
        }
        public EventBuilder ForceCharacterDeath(
            EventArg<int> entityId,
            EventArg<bool> shouldReceiveSouls)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldReceiveSouls, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 4, args));
            return this;
        }
        public EventBuilder ChangeCharacterEnableState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 5, args));
            return this;
        }
        public EventBuilder EzstateInstructionRequest(
            EventArg<int> entityId,
            EventArg<int> command,
            EventArg<byte> slot)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(command, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(slot, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 6, args));
            return this;
        }
        public EventBuilder CreateBulletOwner(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 7, args));
            return this;
        }
        public EventBuilder SetSpeffect(
            EventArg<int> entityId,
            EventArg<int> speffectId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(speffectId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 8, args));
            return this;
        }
        public EventBuilder SetSpecialAnimationS(
            EventArg<int> entityId,
            EventArg<int> waitingAnimation,
            EventArg<int> damageAnimation,
            EventArg<int> cancelAnimation,
            EventArg<int> deathAnimation,
            EventArg<int> returnAnimation)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(waitingAnimation, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(damageAnimation, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(cancelAnimation, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(deathAnimation, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(returnAnimation, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 9, args));
            return this;
        }
        public EventBuilder SetCharacterGravity(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 10, args));
            return this;
        }
        public EventBuilder SetCharacterEventTarget(
            EventArg<int> entityId,
            EventArg<int> entityId2)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId2, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 11, args));
            return this;
        }
        public EventBuilder SetCharacterImmortality(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 12, args));
            return this;
        }
        public EventBuilder SetCharacterHome(
            EventArg<int> entityId,
            EventArg<int> areaEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 13, args));
            return this;
        }
        public EventBuilder RotateCharacter(
            EventArg<int> entityId,
            EventArg<int> entityId2,
            EventArg<int> playAnimationId,
            EventArg<bool> shouldWaitForCompletion)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId2, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(playAnimationId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldWaitForCompletion, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 14, args));
            return this;
        }
        public EventBuilder SetCharacterInvincibility(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 15, args));
            return this;
        }
        public EventBuilder ClearCharacterSAiTarget(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 16, args));
            return this;
        }
        public EventBuilder RequestCharacterAiCommand(
            EventArg<int> entityId,
            EventArg<int> commandId,
            EventArg<byte> slotNumber)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(commandId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(slotNumber, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 17, args));
            return this;
        }
        public EventBuilder SetEventPoint(
            EventArg<int> entityId,
            EventArg<int> entityId2,
            EventArg<float> reactionDistance)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId2, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionDistance, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 18, args));
            return this;
        }
        public EventBuilder SetCharacterAiId(
            EventArg<int> entityId,
            EventArg<int> aiId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(aiId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 19, args));
            return this;
        }
        public EventBuilder RequestCharacterAiRePlan(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 20, args));
            return this;
        }
        public EventBuilder ClearSpeffect(
            EventArg<int> entityId,
            EventArg<int> speffectId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(speffectId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 21, args));
            return this;
        }
        public EventBuilder CreateNpcPart(
            EventArg<int> entityId,
            EventArg<short> npcPartId,
            EventArg<EventEnum.NpcPartType> npcPartGroupIdx,
            EventArg<int> npcPartHp,
            EventArg<float> damageCorrection,
            EventArg<float> bodyDamageCompensation,
            EventArg<bool> isInvincible,
            EventArg<bool> startInStoppedState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(npcPartId, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(npcPartGroupIdx, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(npcPartHp, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(damageCorrection, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(bodyDamageCompensation, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(isInvincible, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(startInStoppedState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 22, args));
            return this;
        }
        public EventBuilder SetNpcPartHp(
            EventArg<int> entityId,
            EventArg<int> npcPartId,
            EventArg<int> desiredHp,
            EventArg<bool> shouldAllowHpAboveMaxHp)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(npcPartId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(desiredHp, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldAllowHpAboveMaxHp, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 23, args));
            return this;
        }
        public EventBuilder SetNpcPartSeAndSfx(
            EventArg<int> entityId,
            EventArg<int> npcPartId,
            EventArg<int> defenseMaterialSeId,
            EventArg<int> defenseMaterialSfxId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(npcPartId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(defenseMaterialSeId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(defenseMaterialSfxId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 24, args));
            return this;
        }
        public EventBuilder SetNpcPartBulletDamageMultiplier(
            EventArg<int> entityId,
            EventArg<int> npcPartId,
            EventArg<float> bulletDamageMultiplier)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(npcPartId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(bulletDamageMultiplier, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 25, args));
            return this;
        }
        public EventBuilder ChangeCharacterDispmask(
            EventArg<int> entityId,
            EventArg<byte> bitNumber,
            EventArg<EventEnum.OnOffChange> switchType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(bitNumber, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(switchType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 26, args));
            return this;
        }
        public EventBuilder ChangeCharacterHitmask(
            EventArg<int> entityId,
            EventArg<byte> bitNumber,
            EventArg<EventEnum.OnOffChange> switchType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(bitNumber, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(switchType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 27, args));
            return this;
        }
        public EventBuilder SetNetworkUpdateAuthority(
            EventArg<int> entityId,
            EventArg<EventEnum.AuthorityLevel> authorityLevel)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(authorityLevel, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 28, args));
            return this;
        }
        public EventBuilder SetCharacterBackreadState(
            EventArg<int> entityId,
            EventArg<bool> removed)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(removed, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 29, args));
            return this;
        }
        public EventBuilder SetCharacterHpBarDisplay(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 30, args));
            return this;
        }
        public EventBuilder SetCharacterMaphit(
            EventArg<int> entityId,
            EventArg<bool> disabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 31, args));
            return this;
        }
        public EventBuilder RequestCharacterAiCommandEventValue(
            EventArg<int> entityId,
            EventArg<int> commandId,
            EventArg<byte> slotNumber,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(commandId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(slotNumber, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(startingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(endingTargetEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 32, args));
            return this;
        }
        public EventBuilder CreateReferredDamagePair(
            EventArg<int> sourceEntityId,
            EventArg<int> targetEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(sourceEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 33, args));
            return this;
        }
        public EventBuilder SetNetworkUpdateRate(
            EventArg<int> entityId,
            EventArg<bool> useFixedFrequency,
            EventArg<EventEnum.CharacterUpdateFrequency> updateFrequency)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(useFixedFrequency, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(updateFrequency, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 34, args));
            return this;
        }
        public EventBuilder SetCharacterDefaultBackreadState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 35, args));
            return this;
        }
        public EventBuilder HellkiteBreathControl(
            EventArg<int> characterEntityId,
            EventArg<int> objectEntityId,
            EventArg<int> characterAnimationId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(characterEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(objectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(characterAnimationId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 36, args));
            return this;
        }
        public EventBuilder ForceCharacterTreasure(
            EventArg<int> characterEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(characterEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 37, args));
            return this;
        }
        public EventBuilder BetrayCharacterSCurrentCovenant(
            EventArg<byte> dummy)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(dummy, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 38, args));
            return this;
        }
        public EventBuilder SetCharacterAnimationState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 39, args));
            return this;
        }
        public EventBuilder WarpCharacterAndSetFloor(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> warpDestinationHitEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(warpType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(warpDestinationEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(warpDestinationHitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 40, args));
            return this;
        }
        public EventBuilder IssueShortWarpRequest(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(warpType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(warpDestinationEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 41, args));
            return this;
        }
        public EventBuilder WarpCharacterAndCopyFloor(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> entityIdToCopyFloorFrom)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(warpType, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(warpDestinationEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityIdToCopyFloorFrom, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 42, args));
            return this;
        }
        public EventBuilder RequestCharacterAnimationReset(
            EventArg<int> entityId,
            EventArg<EventEnum.Interpolation> interpolationMethod)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(interpolationMethod, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 43, args));
            return this;
        }
        public EventBuilder ChangeCharacterTeamTypeAndForceExitSpecialAnimations(
            EventArg<int> entityId,
            EventArg<EventEnum.TeamType> teamType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(teamType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 44, args));
            return this;
        }
        public EventBuilder RegisterNpcHumanityEventValue(
            EventArg<int> npcEntityId,
            EventArg<int> humanityEventValueBaseEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(npcEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(humanityEventValueBaseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 45, args));
            return this;
        }
        public EventBuilder IncrementPlayerPvpSin(
            EventArg<byte> dummy)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(dummy, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 46, args));
            return this;
        }
        public EventBuilder BonfireLikeRecovery(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(2004, 47));
            return this;
        }
        public EventBuilder ChangeCharacterSCloth(
            EventArg<int> entityId,
            EventArg<byte> bitNumber,
            EventArg<byte> stateId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(bitNumber, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(stateId, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 48, args));
            return this;
        }
        public EventBuilder ChangeCharacterPatrolBehavior(
            EventArg<int> characterEntityId,
            EventArg<int> patrolInformationEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(characterEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(patrolInformationEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 49, args));
            return this;
        }
        public EventBuilder SetLockOnPoint(
            EventArg<int> entityId,
            EventArg<int> lockOnDummypolyId,
            EventArg<EventEnum.DisabledEnabled> desiredState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(lockOnDummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(desiredState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 50, args));
            return this;
        }
        public EventBuilder TestRequestRagdollRestraint(
            EventArg<int> entityIdRecipientSide,
            EventArg<int> targetRigidIndexRecipientSide,
            EventArg<int> dummypolyIdRecipientSide,
            EventArg<int> entityIdAttachmentSide,
            EventArg<int> targetRigidIndexAttachmentSide,
            EventArg<int> dummypolyIdAttachmentSide)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityIdRecipientSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetRigidIndexRecipientSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyIdRecipientSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityIdAttachmentSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(targetRigidIndexAttachmentSide, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyIdAttachmentSide, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 51, args));
            return this;
        }
        public EventBuilder ChangeCharacter(
            EventArg<int> charinitparamId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(charinitparamId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 52, args));
            return this;
        }
        public EventBuilder AdaptHpChangingSpeffectToNpcPartOfTarget(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 53, args));
            return this;
        }
        public EventBuilder ImmediateActivateUnknown200454(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 54, args));
            return this;
        }
        public EventBuilder SetCharacterTalkRange(
            EventArg<int> entityId,
            EventArg<float> range)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(range, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 55, args));
            return this;
        }
        public EventBuilder IncrementCharacterGameCycle(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 57, args));
            return this;
        }
        public EventBuilder SetMultiplayerDependentBuffsNonBoss(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 58, args));
            return this;
        }
        public EventBuilder Unknown200459(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disabledEnabled, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 59, args));
            return this;
        }
        public EventBuilder SetCharacterRemainingYoelLevels(
            EventArg<byte> levelCount)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(levelCount, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2004, 60, args));
            return this;
        }
        public EventBuilder RequestObjectDestruction(
            EventArg<int> entityId,
            EventArg<sbyte> slotNumber)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(slotNumber, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 1, args));
            return this;
        }
        public EventBuilder RequestObjectRestoration(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 2, args));
            return this;
        }
        public EventBuilder DeActivateObject(
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 3, args));
            return this;
        }
        public EventBuilder SetObjectTreasureState(
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 4, args));
            return this;
        }
        public EventBuilder InitializeObjact(
            EventArg<int> entityId,
            EventArg<int> objectParameterId,
            EventArg<int> relativeTargetIdx)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(objectParameterId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(relativeTargetIdx, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 5, args));
            return this;
        }
        public EventBuilder SetObjactState(
            EventArg<int> entityId,
            EventArg<int> objectParameterId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(objectParameterId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 6, args));
            return this;
        }
        public EventBuilder ReproduceObjectAnimation(
            EventArg<int> targetObjectEntityId,
            EventArg<int> animationId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(animationId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 7, args));
            return this;
        }
        public EventBuilder ReproduceObjectDestruction(
            EventArg<int> entityId,
            EventArg<sbyte> slotNumber)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(slotNumber, EMEVD.Instruction.ArgType.SByte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 8, args));
            return this;
        }
        public EventBuilder CreateDamagingObject(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<int> dummypolyId,
            EventArg<int> behaviorId,
            EventArg<EventEnum.DamageTargetType> target,
            EventArg<float> radius,
            EventArg<float> lifespan,
            EventArg<float> repetitionTimeS)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(behaviorId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(target, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(radius, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(lifespan, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(repetitionTimeS, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 9, args));
            return this;
        }
        public EventBuilder RegisterStatue(
            EventArg<int> entityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<EventEnum.StatueType> statueType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(statueType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 10, args));
            return this;
        }
        public EventBuilder WarpObjectToCharacter(
            EventArg<int> entityId,
            EventArg<int> characterEntityId,
            EventArg<short> dummypolyId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(characterEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int16, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 11, args));
            return this;
        }
        public EventBuilder DeleteObjectEvent(
            EventArg<int> eventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 12, args));
            return this;
        }
        public EventBuilder SetObjectInvulnerability(
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 13, args));
            return this;
        }
        public EventBuilder DeActivateObjactAssignIdx(
            EventArg<int> entityId,
            EventArg<int> objectParameterId,
            EventArg<int> relativeTargetIdx,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(objectParameterId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(relativeTargetIdx, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 14, args));
            return this;
        }
        public EventBuilder RerollObjectTreasure(
            EventArg<int> targetObjectEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 15, args));
            return this;
        }
        public EventBuilder ExtinguishBurningObjects(
            )
        {
            event_.Instructions.Add(new EMEVD.Instruction(2005, 16));
            return this;
        }
        public EventBuilder ShowObjectByMapCeremony(
            EventArg<int> targetObjectEntityId,
            EventArg<uint> ceremonyId,
            EventArg<int> unknown)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(ceremonyId, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 17, args));
            return this;
        }
        public EventBuilder DestroyObject(
            EventArg<int> targetObjectEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(targetObjectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2005, 19, args));
            return this;
        }
        public EventBuilder DeleteMapSfx(
            EventArg<int> entityId,
            EventArg<bool> onlyDeleteRoot)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(onlyDeleteRoot, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2006, 1, args));
            return this;
        }
        public EventBuilder SpawnMapSfx(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2006, 2, args));
            return this;
        }
        public EventBuilder SpawnOneshotSfx(
            EventArg<EventEnum.TargetEntityType> type,
            EventArg<int> entityId,
            EventArg<int> dummypolyId,
            EventArg<int> sfxId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(type, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(sfxId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2006, 3, args));
            return this;
        }
        public EventBuilder CreateObjectFollowingSfx(
            EventArg<int> objectEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> sfxId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(objectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(sfxId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2006, 4, args));
            return this;
        }
        public EventBuilder DeleteObjectFollowingSfx(
            EventArg<int> objectEntityId,
            EventArg<bool> shouldDeleteRoot)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(objectEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(shouldDeleteRoot, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2006, 5, args));
            return this;
        }
        public EventBuilder DisplayGenericDialog(
            EventArg<int> messageId,
            EventArg<EventEnum.PromptType> dialogType,
            EventArg<EventEnum.NumberOfOptions> numberOfOptions,
            EventArg<int> entityId,
            EventArg<float> displayDistance)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dialogType, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(numberOfOptions, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(displayDistance, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 1, args));
            return this;
        }
        public EventBuilder DisplayBanner(
            EventArg<EventEnum.TextBannerType> bannerType)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(bannerType, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 2, args));
            return this;
        }
        public EventBuilder DisplayStatusMessage(
            EventArg<int> messageId,
            EventArg<EventEnum.DisabledEnabled> padState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(padState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 3, args));
            return this;
        }
        public EventBuilder DisplayMessage(
            EventArg<int> messageId,
            EventArg<byte> screenLocationIndex)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(screenLocationIndex, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 4, args));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagA(
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 5, args));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagB(
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 6, args));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagC(
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 7, args));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagD(
            EventArg<int> playerEntityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(playerEntityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 8, args));
            return this;
        }
        public EventBuilder DisplayBattleOfStocismDissolutionMessage(
            EventArg<int> messageId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 9, args));
            return this;
        }
        public EventBuilder DisplayGenericDialogAndSetEventFlags(
            EventArg<int> messageId,
            EventArg<EventEnum.PromptType> dialogType,
            EventArg<EventEnum.NumberOfOptions> numberOfOptions,
            EventArg<int> entityId,
            EventArg<float> displayDistance,
            EventArg<int> leftResponseEventFlagId,
            EventArg<int> rightResponseEventFlagId,
            EventArg<int> cancelResponseEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dialogType, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(numberOfOptions, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(displayDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(leftResponseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(rightResponseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(cancelResponseEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 10, args));
            return this;
        }
        public EventBuilder DisplayAreaWelcomeMessage(
            EventArg<int> areaNameMessageId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(areaNameMessageId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 11, args));
            return this;
        }
        public EventBuilder DisplayHollowArenaPvpMessage(
            EventArg<int> messageId,
            EventArg<short> unknown,
            EventArg<EventEnum.DisabledEnabled> padState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(messageId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int16, ref offset),
                CreateArgObject(padState, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2007, 12, args));
            return this;
        }
        public EventBuilder ChangeCamera(
            EventArg<int> normalCameraId,
            EventArg<int> lockedCameraId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(normalCameraId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(lockedCameraId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2008, 1, args));
            return this;
        }
        public EventBuilder SetCameraVibration(
            EventArg<int> vibrationId,
            EventArg<EventEnum.TargetEntityType> type,
            EventArg<int> entityId,
            EventArg<int> dummypolyId,
            EventArg<float> decayStartDistance,
            EventArg<float> decayEndDistance)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(vibrationId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(type, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(dummypolyId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(decayStartDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(decayEndDistance, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2008, 2, args));
            return this;
        }
        public EventBuilder SetLockcamSlotNumber(
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<ushort> lockcamSlotNumber)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(areaId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(blockId, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(lockcamSlotNumber, EMEVD.Instruction.ArgType.UInt16, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2008, 3, args));
            return this;
        }
        public EventBuilder RegisterLadder(
            EventArg<int> disableTopEventFlagId,
            EventArg<int> disableBottomEventFlagId,
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(disableTopEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(disableBottomEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 0, args));
            return this;
        }
        public EventBuilder InitializeWanderingDemon(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<int> appearenceEventFlagId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(appearenceEventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 1, args));
            return this;
        }
        public EventBuilder RegisterWanderingDemon(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<int> entityId2)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId2, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 2, args));
            return this;
        }
        public EventBuilder RegisterBonfire(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<float> reactionDistance,
            EventArg<float> reactionAngle,
            EventArg<int> setStandardKindlingLevel)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(reactionAngle, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(setStandardKindlingLevel, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 3, args));
            return this;
        }
        public EventBuilder ActivateMultiplayerDependantBuffs(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 4, args));
            return this;
        }
        public EventBuilder RegisterHealingFountain(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<float> reactionDistance,
            EventArg<float> reactionAngle,
            EventArg<int> initialSwordNumber,
            EventArg<int> swordLevel)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(eventFlagId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(reactionDistance, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(reactionAngle, EMEVD.Instruction.ArgType.Single, ref offset),
                CreateArgObject(initialSwordNumber, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(swordLevel, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 5, args));
            return this;
        }
        public EventBuilder IssueBossRoomEntryNotification(
            EventArg<byte> dummy)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(dummy, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 6, args));
            return this;
        }
        public EventBuilder SendInvadingPhantomsHome(
            EventArg<int> unknown)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 8, args));
            return this;
        }
        public EventBuilder SendAllPhantomsHomeAndUpdateServerPvpStats(
            EventArg<int> unknown)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 10, args));
            return this;
        }
        public EventBuilder SendAllPhantomsHome(
            EventArg<int> unknown)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(unknown, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2009, 11, args));
            return this;
        }
        public EventBuilder PlayBgm(
            EventArg<EventEnum.OnOff> shouldPlay,
            EventArg<ushort> slotNumber,
            EventArg<int> entityId,
            EventArg<EventEnum.SoundType> soundType,
            EventArg<int> soundId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(shouldPlay, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(slotNumber, EMEVD.Instruction.ArgType.UInt16, ref offset),
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(soundType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(soundId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2010, 1, args));
            return this;
        }
        public EventBuilder PlaySe(
            EventArg<int> entityId,
            EventArg<EventEnum.SoundType> soundType,
            EventArg<int> soundId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(soundType, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(soundId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2010, 2, args));
            return this;
        }
        public EventBuilder SetMapSoundState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> enableDisable)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(enableDisable, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2010, 3, args));
            return this;
        }
        public EventBuilder EnableBossMapSound(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> enableDisable)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(enableDisable, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2010, 4, args));
            return this;
        }
        public EventBuilder NotifySoundDampeningOfDoorEvent(
            EventArg<int> entityId,
            EventArg<EventEnum.DoorState> doorState)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(doorState, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2010, 5, args));
            return this;
        }
        public EventBuilder EnableMapSoundWithFade(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> enableDisable,
            EventArg<float> fadeTime)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(enableDisable, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(fadeTime, EMEVD.Instruction.ArgType.Single, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2010, 6, args));
            return this;
        }
        public EventBuilder Unknown201007(
            EventArg<int> entityId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(entityId, EMEVD.Instruction.ArgType.Int32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2010, 7, args));
            return this;
        }
        public EventBuilder ActivateHit(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2011, 1, args));
            return this;
        }
        public EventBuilder ActivateHitBackreadMask(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2011, 2, args));
            return this;
        }
        public EventBuilder ActivateHitres(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2011, 3, args));
            return this;
        }
        public EventBuilder ActivateHitAndCreateNavimesh(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(hitEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2011, 4, args));
            return this;
        }
        public EventBuilder ActivateMapPart(
            EventArg<int> mapPartEntityId,
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(mapPartEntityId, EMEVD.Instruction.ArgType.Int32, ref offset),
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2012, 1, args));
            return this;
        }
        public EventBuilder SetAreaWelcomeMessageState(
            EventArg<EventEnum.DisabledEnabled> state)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(state, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2012, 8, args));
            return this;
        }
        public EventBuilder CreatePlaylog(
            EventArg<uint> playlogName)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(playlogName, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2013, 1, args));
            return this;
        }
        public EventBuilder StartTimeMeasurement(
            EventArg<uint> measurementId,
            EventArg<uint> playlogName,
            EventArg<EventEnum.DisabledEnabled> overwrite)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(measurementId, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(playlogName, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(overwrite, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2013, 2, args));
            return this;
        }
        public EventBuilder EndTimeMeasurement(
            EventArg<uint> measurementId)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(measurementId, EMEVD.Instruction.ArgType.UInt32, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2013, 3, args));
            return this;
        }
        public EventBuilder ParameterOutput(
            EventArg<EventEnum.PlayerPlaylogParameter> category,
            EventArg<uint> playlogName,
            EventArg<EventEnum.PlaylogMultiplayerType> outputMultiplayerStatus)
        {
            int offset = 0;
            object[] args =
            {
                CreateArgObject(category, EMEVD.Instruction.ArgType.Byte, ref offset),
                CreateArgObject(playlogName, EMEVD.Instruction.ArgType.UInt32, ref offset),
                CreateArgObject(outputMultiplayerStatus, EMEVD.Instruction.ArgType.Byte, ref offset)
            };
            event_.Instructions.Add(new EMEVD.Instruction(2013, 4, args));
            return this;
        }
    }
}
