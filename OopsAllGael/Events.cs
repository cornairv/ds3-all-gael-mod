using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;

namespace OopsAllGael
{
    abstract partial class MyInstr
    {
        public static MyInstr FromInstr(EMEVD.Instruction instr, int index, List<EMEVD.Parameter> parameters)
        {
            InstrType instrType = (InstrType) ((instr.Bank << 8) | instr.ID);
            switch (instrType)
            {
                case InstrType.InitializeCommonEvent:
                    {
                        List<EMEVD.Instruction.ArgType> argTypes_ = InstrWrapper.InitializeCommonEvent.argTypes.ToList();
                        for (int i = 1; i < instr.ArgData.Length / 4; i++)
                        {
                            argTypes_.Add(EMEVD.Instruction.ArgType.UInt32);
                        }
                        List<object> args = instr.UnpackArgs(argTypes_);
                        int[] p = Util.UnpackParams(parameters, index, argTypes_.ToArray());
                        AbstractEventArg[] eventArgs = new AbstractEventArg[argTypes_.Count];
                        eventArgs[0] = p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] };
                        for (int i = 1; i < instr.ArgData.Length / 4; i++)
                        {
                            eventArgs[i] = p[i] == -1 ? (uint)args[i] : new EventArg<uint> { index = p[i] };
                        }
                        return new InstrWrapper.InitializeCommonEvent(eventArgs, instr.Layer);
                    }
                case InstrType.InitializeEvent:
                    {
                        List<EMEVD.Instruction.ArgType> argTypes_ = InstrWrapper.InitializeEvent.argTypes.ToList();
                        for (int i = 2; i < instr.ArgData.Length / 4; i++)
                        {
                            argTypes_.Add(EMEVD.Instruction.ArgType.UInt32);
                        }
                        List<object> args = instr.UnpackArgs(argTypes_);
                        int[] p = Util.UnpackParams(parameters, index, argTypes_.ToArray());
                        AbstractEventArg[] eventArgs = new AbstractEventArg[argTypes_.Count];
                        eventArgs[0] = p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] };
                        eventArgs[1] = p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] };
                        for (int i = 2; i < instr.ArgData.Length / 4; i++)
                        {
                            eventArgs[i] = p[i] == -1 ? (uint)args[i] : new EventArg<uint> { index = p[i] };
                        }
                        return new InstrWrapper.InitializeEvent(eventArgs, instr.Layer);
                    }



                case InstrType.IfConditionGroup:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfConditionGroup.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfConditionGroup.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionState)(byte)args[1] : new EventArg<EventEnum.ConditionState> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[2] : new EventArg<EventEnum.ConditionGroup> { index = p[2] } };
                        return new InstrWrapper.IfConditionGroup(eventArgs, instr.Layer);
                    }
                case InstrType.IfParameterComparison:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfParameterComparison.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfParameterComparison.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(sbyte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.IfParameterComparison(eventArgs, instr.Layer);
                    }
                case InstrType.IfElapsedSeconds:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfElapsedSeconds.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfElapsedSeconds.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (float)args[1] : new EventArg<float> { index = p[1] } };
                        return new InstrWrapper.IfElapsedSeconds(eventArgs, instr.Layer);
                    }
                case InstrType.IfElapsedFrames:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfElapsedFrames.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfElapsedFrames.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.IfElapsedFrames(eventArgs, instr.Layer);
                    }
                case InstrType.IfRandomElapsedSeconds:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfRandomElapsedSeconds.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfRandomElapsedSeconds.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (float)args[1] : new EventArg<float> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] } };
                        return new InstrWrapper.IfRandomElapsedSeconds(eventArgs, instr.Layer);
                    }
                case InstrType.IfRandomElapsedFrames:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfRandomElapsedFrames.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfRandomElapsedFrames.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.IfRandomElapsedFrames(eventArgs, instr.Layer);
                    }
                case InstrType.IfEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.OnOffChange)(byte)args[1] : new EventArg<EventEnum.OnOffChange> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.IfEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.IfBatchEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfBatchEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfBatchEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.LogicalOperationType)(byte)args[1] : new EventArg<EventEnum.LogicalOperationType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.IfBatchEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.IfInOutsideArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfInOutsideArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfInOutsideArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.InsideOutsideState)(byte)args[1] : new EventArg<EventEnum.InsideOutsideState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.IfInOutsideArea(eventArgs, instr.Layer);
                    }
                case InstrType.IfEntityInOutsideRadiusOfEntity:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfEntityInOutsideRadiusOfEntity.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfEntityInOutsideRadiusOfEntity.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.InsideOutsideState)(byte)args[1] : new EventArg<EventEnum.InsideOutsideState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.IfEntityInOutsideRadiusOfEntity(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerHasDoesnTHaveItem:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerHasDoesnTHaveItem.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerHasDoesnTHaveItem.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ItemType)(byte)args[1] : new EventArg<EventEnum.ItemType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.OwnershipState)(byte)args[3] : new EventArg<EventEnum.OwnershipState> { index = p[3] } };
                        return new InstrWrapper.IfPlayerHasDoesnTHaveItem(eventArgs, instr.Layer);
                    }
                case InstrType.IfActionButton:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfActionButton.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfActionButton.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(int)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (short)args[4] : new EventArg<short> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] },
                            p[7] == -1 ? (EventEnum.ReactionType)(byte)args[7] : new EventArg<EventEnum.ReactionType> { index = p[7] },
                            p[8] == -1 ? (int)args[8] : new EventArg<int> { index = p[8] } };
                        return new InstrWrapper.IfActionButton(eventArgs, instr.Layer);
                    }
                case InstrType.IfMultiplayerState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfMultiplayerState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfMultiplayerState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.MultiplayerState)(sbyte)args[1] : new EventArg<EventEnum.MultiplayerState> { index = p[1] } };
                        return new InstrWrapper.IfMultiplayerState(eventArgs, instr.Layer);
                    }
                case InstrType.IfAllPlayersInOutsideArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfAllPlayersInOutsideArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfAllPlayersInOutsideArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.InsideOutsideState)(byte)args[1] : new EventArg<EventEnum.InsideOutsideState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.IfAllPlayersInOutsideArea(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerInOutMap:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerInOutMap.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerInOutMap.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.IfPlayerInOutMap(eventArgs, instr.Layer);
                    }
                case InstrType.IfMultiplayerEvent:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfMultiplayerEvent.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfMultiplayerEvent.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] } };
                        return new InstrWrapper.IfMultiplayerEvent(eventArgs, instr.Layer);
                    }
                case InstrType.IfCountEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCountEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCountEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[1] : new EventArg<EventEnum.TargetEventFlagType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.IfCountEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.IfWorldTendency:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfWorldTendency.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfWorldTendency.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TendencyType)(byte)args[1] : new EventArg<EventEnum.TendencyType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(byte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.IfWorldTendency(eventArgs, instr.Layer);
                    }
                case InstrType.IfEventValue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfEventValue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfEventValue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(byte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (uint)args[4] : new EventArg<uint> { index = p[4] } };
                        return new InstrWrapper.IfEventValue(eventArgs, instr.Layer);
                    }
                case InstrType.IfActionButtonBossFog:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfActionButtonBossFog.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfActionButtonBossFog.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(int)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (short)args[4] : new EventArg<short> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] },
                            p[7] == -1 ? (EventEnum.ReactionType)(byte)args[7] : new EventArg<EventEnum.ReactionType> { index = p[7] },
                            p[8] == -1 ? (int)args[8] : new EventArg<int> { index = p[8] } };
                        return new InstrWrapper.IfActionButtonBossFog(eventArgs, instr.Layer);
                    }
                case InstrType.IfDroppedItemsInArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfDroppedItemsInArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfDroppedItemsInArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.IfDroppedItemsInArea(eventArgs, instr.Layer);
                    }
                case InstrType.IfDroppedItem:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfDroppedItem.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfDroppedItem.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ItemType)(int)args[1] : new EventArg<EventEnum.ItemType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.IfDroppedItem(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerHasDoesnTHaveItemIncludingBbox:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerHasDoesnTHaveItemIncludingBbox.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerHasDoesnTHaveItemIncludingBbox.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ItemType)(byte)args[1] : new EventArg<EventEnum.ItemType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.OwnershipState)(byte)args[3] : new EventArg<EventEnum.OwnershipState> { index = p[3] } };
                        return new InstrWrapper.IfPlayerHasDoesnTHaveItemIncludingBbox(eventArgs, instr.Layer);
                    }
                case InstrType.IfGameCycle:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfGameCycle.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfGameCycle.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(byte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.IfGameCycle(eventArgs, instr.Layer);
                    }
                case InstrType.IfActionButtonLineSegment:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfActionButtonLineSegment.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfActionButtonLineSegment.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(int)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (short)args[4] : new EventArg<short> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] },
                            p[7] == -1 ? (EventEnum.ReactionType)(byte)args[7] : new EventArg<EventEnum.ReactionType> { index = p[7] },
                            p[8] == -1 ? (int)args[8] : new EventArg<int> { index = p[8] },
                            p[9] == -1 ? (int)args[9] : new EventArg<int> { index = p[9] } };
                        return new InstrWrapper.IfActionButtonLineSegment(eventArgs, instr.Layer);
                    }
                case InstrType.IfActionButtonLineSegmentBossRoom:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfActionButtonLineSegmentBossRoom.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfActionButtonLineSegmentBossRoom.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(int)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (short)args[4] : new EventArg<short> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] },
                            p[7] == -1 ? (EventEnum.ReactionType)(byte)args[7] : new EventArg<EventEnum.ReactionType> { index = p[7] },
                            p[8] == -1 ? (int)args[8] : new EventArg<int> { index = p[8] },
                            p[9] == -1 ? (int)args[9] : new EventArg<int> { index = p[9] } };
                        return new InstrWrapper.IfActionButtonLineSegmentBossRoom(eventArgs, instr.Layer);
                    }
                case InstrType.IfCompareEventValues:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCompareEventValues.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCompareEventValues.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(byte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] },
                            p[5] == -1 ? (byte)args[5] : new EventArg<byte> { index = p[5] } };
                        return new InstrWrapper.IfCompareEventValues(eventArgs, instr.Layer);
                    }
                case InstrType.IfOwnsDlc:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfOwnsDlc.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfOwnsDlc.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.IfOwnsDlc(eventArgs, instr.Layer);
                    }
                case InstrType.IfOnlineMode:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfOnlineMode.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfOnlineMode.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.IfOnlineMode(eventArgs, instr.Layer);
                    }
                case InstrType.IfDamageType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfDamageType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfDamageType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.DamageType)(byte)args[3] : new EventArg<EventEnum.DamageType> { index = p[3] } };
                        return new InstrWrapper.IfDamageType(eventArgs, instr.Layer);
                    }
                case InstrType.IfActionButtonInArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfActionButtonInArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfActionButtonInArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.IfActionButtonInArea(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerIsNotInOwnWorldExcludesArena:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerIsNotInOwnWorldExcludesArena.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerIsNotInOwnWorldExcludesArena.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.IfPlayerIsNotInOwnWorldExcludesArena(eventArgs, instr.Layer);
                    }
                case InstrType.IfMapCeremonyState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfMapCeremonyState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfMapCeremonyState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] },
                            p[4] == -1 ? (ushort)args[4] : new EventArg<ushort> { index = p[4] } };
                        return new InstrWrapper.IfMapCeremonyState(eventArgs, instr.Layer);
                    }
                case InstrType.IfMultiplayerNetworkPenalized:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfMultiplayerNetworkPenalized.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfMultiplayerNetworkPenalized.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] } };
                        return new InstrWrapper.IfMultiplayerNetworkPenalized(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerGender:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerGender.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerGender.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.Gender)(byte)args[1] : new EventArg<EventEnum.Gender> { index = p[1] } };
                        return new InstrWrapper.IfPlayerGender(eventArgs, instr.Layer);
                    }
                case InstrType.IfOngoingCutsceneFinished:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfOngoingCutsceneFinished.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfOngoingCutsceneFinished.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.IfOngoingCutsceneFinished(eventArgs, instr.Layer);
                    }
                case InstrType.IfHollowArenaMatchReadyState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfHollowArenaMatchReadyState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfHollowArenaMatchReadyState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.IfHollowArenaMatchReadyState(eventArgs, instr.Layer);
                    }
                case InstrType.IfHollowArenaSoloResults:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfHollowArenaSoloResults.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfHollowArenaSoloResults.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.HollowArenaResult)(byte)args[1] : new EventArg<EventEnum.HollowArenaResult> { index = p[1] } };
                        return new InstrWrapper.IfHollowArenaSoloResults(eventArgs, instr.Layer);
                    }
                case InstrType.IfHollowArenaSoloScoreComparison:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfHollowArenaSoloScoreComparison.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfHollowArenaSoloScoreComparison.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(sbyte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.IfHollowArenaSoloScoreComparison(eventArgs, instr.Layer);
                    }
                case InstrType.IfHollowArenaTeamResults:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfHollowArenaTeamResults.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfHollowArenaTeamResults.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.HollowArenaResult)(byte)args[1] : new EventArg<EventEnum.HollowArenaResult> { index = p[1] } };
                        return new InstrWrapper.IfHollowArenaTeamResults(eventArgs, instr.Layer);
                    }
                case InstrType.IfSteamDisconnected:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfSteamDisconnected.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfSteamDisconnected.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.IfSteamDisconnected(eventArgs, instr.Layer);
                    }
                case InstrType.IfFriendlyPhantomCountComparison:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfFriendlyPhantomCountComparison.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfFriendlyPhantomCountComparison.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(sbyte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.IfFriendlyPhantomCountComparison(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterDeadAlive:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterDeadAlive.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterDeadAlive.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.DeathState)(byte)args[2] : new EventArg<EventEnum.DeathState> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfCharacterDeadAlive(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterDamagedBy:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterDamagedBy.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterDamagedBy.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.IfCharacterDamagedBy(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterHpRatio:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterHpRatio.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterHpRatio.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(sbyte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] } };
                        return new InstrWrapper.IfCharacterHpRatio(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetType)(sbyte)args[2] : new EventArg<EventEnum.TargetType> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfCharacterType(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterTargetedBy:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterTargetedBy.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterTargetedBy.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] } };
                        return new InstrWrapper.IfCharacterTargetedBy(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterHasSpeffect:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterHasSpeffect.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterHasSpeffect.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] } };
                        return new InstrWrapper.IfCharacterHasSpeffect(eventArgs, instr.Layer);
                    }
                case InstrType.IfNpcPartHp:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfNpcPartHp.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfNpcPartHp.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] } };
                        return new InstrWrapper.IfNpcPartHp(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterBackreadStatus:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterBackreadStatus.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterBackreadStatus.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] != 0 : new EventArg<bool> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfCharacterBackreadStatus(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterHasEventMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterHasEventMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterHasEventMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] } };
                        return new InstrWrapper.IfCharacterHasEventMessage(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterAiState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterAiState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterAiState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.AiStateType)(byte)args[2] : new EventArg<EventEnum.AiStateType> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfCharacterAiState(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerUsingSkullLantern:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerUsingSkullLantern.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerUsingSkullLantern.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.IfPlayerUsingSkullLantern(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerSClass:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerSClass.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerSClass.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ClassType)(byte)args[1] : new EventArg<EventEnum.ClassType> { index = p[1] } };
                        return new InstrWrapper.IfPlayerSClass(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerSCovenant:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerSCovenant.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerSCovenant.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] } };
                        return new InstrWrapper.IfPlayerSCovenant(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerSSoulLevel:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerSSoulLevel.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerSSoulLevel.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(byte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (uint)args[2] : new EventArg<uint> { index = p[2] } };
                        return new InstrWrapper.IfPlayerSSoulLevel(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterHpValue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterHpValue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterHpValue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(byte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] } };
                        return new InstrWrapper.IfCharacterHpValue(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterDrawgroupState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterDrawgroupState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterDrawgroupState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] != 0 : new EventArg<bool> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfCharacterDrawgroupState(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerRemainingYoelLevelCount:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerRemainingYoelLevelCount.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerRemainingYoelLevelCount.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(byte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.IfPlayerRemainingYoelLevelCount(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterInvadeType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterInvadeType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterInvadeType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfCharacterInvadeType(eventArgs, instr.Layer);
                    }
                case InstrType.IfCharacterChameleon:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfCharacterChameleon.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfCharacterChameleon.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] } };
                        return new InstrWrapper.IfCharacterChameleon(eventArgs, instr.Layer);
                    }
                case InstrType.IfObjectDestroyed:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfObjectDestroyed.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfObjectDestroyed.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DestructionState)(byte)args[1] : new EventArg<EventEnum.DestructionState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfObjectDestroyed(eventArgs, instr.Layer);
                    }
                case InstrType.IfObjectHitBy:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfObjectHitBy.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfObjectHitBy.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.IfObjectHitBy(eventArgs, instr.Layer);
                    }
                case InstrType.IfObjactEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfObjactEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfObjactEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.IfObjactEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.IfObjectHp:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfObjectHp.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfObjectHp.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(sbyte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.IfObjectHp(eventArgs, instr.Layer);
                    }
                case InstrType.IfObjectBurnState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfObjectBurnState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfObjectBurnState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(byte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] } };
                        return new InstrWrapper.IfObjectBurnState(eventArgs, instr.Layer);
                    }
                case InstrType.IfObjectBackread:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfObjectBackread.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfObjectBackread.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] != 0 : new EventArg<bool> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfObjectBackread(eventArgs, instr.Layer);
                    }
                case InstrType.IfObjectBackreadUnknown511:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfObjectBackreadUnknown511.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfObjectBackreadUnknown511.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] != 0 : new EventArg<bool> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.IfObjectBackreadUnknown511(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerMovingOnHit:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerMovingOnHit.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerMovingOnHit.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.IfPlayerMovingOnHit(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerAttackingOnHit:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerAttackingOnHit.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerAttackingOnHit.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.IfPlayerAttackingOnHit(eventArgs, instr.Layer);
                    }
                case InstrType.IfPlayerStandingOnHit:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IfPlayerStandingOnHit.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IfPlayerStandingOnHit.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[0] : new EventArg<EventEnum.ConditionGroup> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.IfPlayerStandingOnHit(eventArgs, instr.Layer);
                    }
                case InstrType.WaitForConditionGroupState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitForConditionGroupState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitForConditionGroupState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ConditionState)(byte)args[0] : new EventArg<EventEnum.ConditionState> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[1] : new EventArg<EventEnum.ConditionGroup> { index = p[1] } };
                        return new InstrWrapper.WaitForConditionGroupState(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfConditionGroupStateUncompiled:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfConditionGroupStateUncompiled.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfConditionGroupStateUncompiled.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionState)(byte)args[1] : new EventArg<EventEnum.ConditionState> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[2] : new EventArg<EventEnum.ConditionGroup> { index = p[2] } };
                        return new InstrWrapper.SkipIfConditionGroupStateUncompiled(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfConditionGroupStateUncompiled:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfConditionGroupStateUncompiled.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfConditionGroupStateUncompiled.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionState)(byte)args[1] : new EventArg<EventEnum.ConditionState> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[2] : new EventArg<EventEnum.ConditionGroup> { index = p[2] } };
                        return new InstrWrapper.EndIfConditionGroupStateUncompiled(eventArgs, instr.Layer);
                    }
                case InstrType.SkipUnconditionally:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipUnconditionally.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipUnconditionally.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.SkipUnconditionally(eventArgs, instr.Layer);
                    }
                case InstrType.EndUnconditionally:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndUnconditionally.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndUnconditionally.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] } };
                        return new InstrWrapper.EndUnconditionally(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfComparison:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfComparison.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfComparison.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(sbyte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.SkipIfComparison(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfComparison:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfComparison.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfComparison.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(sbyte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.EndIfComparison(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfConditionGroupStateCompiled:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfConditionGroupStateCompiled.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfConditionGroupStateCompiled.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionState)(byte)args[1] : new EventArg<EventEnum.ConditionState> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[2] : new EventArg<EventEnum.ConditionGroup> { index = p[2] } };
                        return new InstrWrapper.SkipIfConditionGroupStateCompiled(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfConditionGroupStateCompiled:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfConditionGroupStateCompiled.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfConditionGroupStateCompiled.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionState)(byte)args[1] : new EventArg<EventEnum.ConditionState> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[2] : new EventArg<EventEnum.ConditionGroup> { index = p[2] } };
                        return new InstrWrapper.EndIfConditionGroupStateCompiled(eventArgs, instr.Layer);
                    }
                case InstrType.WaitForNetworkApproval:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitForNetworkApproval.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitForNetworkApproval.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (float)args[0] : new EventArg<float> { index = p[0] } };
                        return new InstrWrapper.WaitForNetworkApproval(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfConditionGroupStateUncompiled:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfConditionGroupStateUncompiled.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfConditionGroupStateUncompiled.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionState)(byte)args[1] : new EventArg<EventEnum.ConditionState> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[2] : new EventArg<EventEnum.ConditionGroup> { index = p[2] } };
                        return new InstrWrapper.GotoIfConditionGroupStateUncompiled(eventArgs, instr.Layer);
                    }
                case InstrType.GotoUnconditionally:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoUnconditionally.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoUnconditionally.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] } };
                        return new InstrWrapper.GotoUnconditionally(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfComparison:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfComparison.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfComparison.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(sbyte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.GotoIfComparison(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfConditionGroupStateCompiled:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfConditionGroupStateCompiled.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfConditionGroupStateCompiled.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ConditionState)(byte)args[1] : new EventArg<EventEnum.ConditionState> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ConditionGroup)(sbyte)args[2] : new EventArg<EventEnum.ConditionGroup> { index = p[2] } };
                        return new InstrWrapper.GotoIfConditionGroupStateCompiled(eventArgs, instr.Layer);
                    }
                case InstrType.WaitFixedTimeSeconds:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitFixedTimeSeconds.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitFixedTimeSeconds.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (float)args[0] : new EventArg<float> { index = p[0] } };
                        return new InstrWrapper.WaitFixedTimeSeconds(eventArgs, instr.Layer);
                    }
                case InstrType.WaitFixedTimeFrames:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitFixedTimeFrames.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitFixedTimeFrames.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.WaitFixedTimeFrames(eventArgs, instr.Layer);
                    }
                case InstrType.WaitRandomTimeSeconds:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitRandomTimeSeconds.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitRandomTimeSeconds.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (float)args[0] : new EventArg<float> { index = p[0] },
                            p[1] == -1 ? (float)args[1] : new EventArg<float> { index = p[1] } };
                        return new InstrWrapper.WaitRandomTimeSeconds(eventArgs, instr.Layer);
                    }
                case InstrType.WaitRandomTimeFrames:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitRandomTimeFrames.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitRandomTimeFrames.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.WaitRandomTimeFrames(eventArgs, instr.Layer);
                    }
                case InstrType.WaitHollowArenaHalfTimeStayparamLookup:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitHollowArenaHalfTimeStayparamLookup.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitHollowArenaHalfTimeStayparamLookup.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.HollowArenaMatchType)(byte)args[0] : new EventArg<EventEnum.HollowArenaMatchType> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.WaitHollowArenaHalfTimeStayparamLookup(eventArgs, instr.Layer);
                    }
                case InstrType.WaitForEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitForEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitForEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.OnOffChange)(byte)args[0] : new EventArg<EventEnum.OnOffChange> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[1] : new EventArg<EventEnum.TargetEventFlagType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.WaitForEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.OnOff)(byte)args[1] : new EventArg<EventEnum.OnOff> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.SkipIfEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.OnOff)(byte)args[1] : new EventArg<EventEnum.OnOff> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.EndIfEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfBatchEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfBatchEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfBatchEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.LogicalOperationType)(byte)args[1] : new EventArg<EventEnum.LogicalOperationType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.SkipIfBatchEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfBatchEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfBatchEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfBatchEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.LogicalOperationType)(byte)args[1] : new EventArg<EventEnum.LogicalOperationType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.EndIfBatchEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfMultiplayerState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfMultiplayerState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfMultiplayerState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.MultiplayerState)(sbyte)args[1] : new EventArg<EventEnum.MultiplayerState> { index = p[1] } };
                        return new InstrWrapper.SkipIfMultiplayerState(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfMultiplayerState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfMultiplayerState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfMultiplayerState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.MultiplayerState)(sbyte)args[1] : new EventArg<EventEnum.MultiplayerState> { index = p[1] } };
                        return new InstrWrapper.EndIfMultiplayerState(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfPlayerInOutMap:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfPlayerInOutMap.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfPlayerInOutMap.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.SkipIfPlayerInOutMap(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfPlayerInOutMap:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfPlayerInOutMap.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfPlayerInOutMap.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.EndIfPlayerInOutMap(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfNumberOfCoOpClients:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfNumberOfCoOpClients.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfNumberOfCoOpClients.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(byte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.SkipIfNumberOfCoOpClients(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfNumberOfCoOpClients:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfNumberOfCoOpClients.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfNumberOfCoOpClients.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(byte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.EndIfNumberOfCoOpClients(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfCharacterHasSpeffect:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfCharacterHasSpeffect.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfCharacterHasSpeffect.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.GotoIfCharacterHasSpeffect(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfPlayerIsNotInOwnWorldExcludesArena:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfPlayerIsNotInOwnWorldExcludesArena.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfPlayerIsNotInOwnWorldExcludesArena.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.GotoIfPlayerIsNotInOwnWorldExcludesArena(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfPlayerIsNotInOwnWorldExcludesArena:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfPlayerIsNotInOwnWorldExcludesArena.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfPlayerIsNotInOwnWorldExcludesArena.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.EndIfPlayerIsNotInOwnWorldExcludesArena(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfNumberOfClientsOfType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfNumberOfClientsOfType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfNumberOfClientsOfType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ClientType)(byte)args[1] : new EventArg<EventEnum.ClientType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(byte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.SkipIfNumberOfClientsOfType(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfNumberOfClientsOfType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfNumberOfClientsOfType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfNumberOfClientsOfType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ClientType)(byte)args[1] : new EventArg<EventEnum.ClientType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(byte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.GotoIfNumberOfClientsOfType(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfNumberOfClientsOfType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfNumberOfClientsOfType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfNumberOfClientsOfType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ClientType)(byte)args[1] : new EventArg<EventEnum.ClientType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.ComparisonType)(byte)args[2] : new EventArg<EventEnum.ComparisonType> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.EndIfNumberOfClientsOfType(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.OnOff)(byte)args[1] : new EventArg<EventEnum.OnOff> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.GotoIfEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfBatchEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfBatchEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfBatchEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.LogicalOperationType)(byte)args[1] : new EventArg<EventEnum.LogicalOperationType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.TargetEventFlagType)(byte)args[2] : new EventArg<EventEnum.TargetEventFlagType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.GotoIfBatchEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfMultiplayerState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfMultiplayerState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfMultiplayerState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.MultiplayerState)(sbyte)args[1] : new EventArg<EventEnum.MultiplayerState> { index = p[1] } };
                        return new InstrWrapper.GotoIfMultiplayerState(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfPlayerInOutMap:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfPlayerInOutMap.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfPlayerInOutMap.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.GotoIfPlayerInOutMap(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfNumberOfCoOpClients:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfNumberOfCoOpClients.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfNumberOfCoOpClients.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ComparisonType)(byte)args[1] : new EventArg<EventEnum.ComparisonType> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.GotoIfNumberOfCoOpClients(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfCharacterHasSpeffect:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfCharacterHasSpeffect.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfCharacterHasSpeffect.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.EndIfCharacterHasSpeffect(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfCharacterHasSpeffect:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfCharacterHasSpeffect.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfCharacterHasSpeffect.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] },
                            p[4] == -1 ? (EventEnum.ComparisonType)(sbyte)args[4] : new EventArg<EventEnum.ComparisonType> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.SkipIfCharacterHasSpeffect(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfPlayerInOutsideArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfPlayerInOutsideArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfPlayerInOutsideArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.InsideOutsideState)(byte)args[1] : new EventArg<EventEnum.InsideOutsideState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.GotoIfPlayerInOutsideArea(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfPlayerInOutsideArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfPlayerInOutsideArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfPlayerInOutsideArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.InsideOutsideState)(byte)args[1] : new EventArg<EventEnum.InsideOutsideState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.EndIfPlayerInOutsideArea(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfPlayerInOutsideArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfPlayerInOutsideArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfPlayerInOutsideArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.InsideOutsideState)(byte)args[1] : new EventArg<EventEnum.InsideOutsideState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.SkipIfPlayerInOutsideArea(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfHollowArenaMatchType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfHollowArenaMatchType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfHollowArenaMatchType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.HollowArenaMatchType)(byte)args[1] : new EventArg<EventEnum.HollowArenaMatchType> { index = p[1] } };
                        return new InstrWrapper.GotoIfHollowArenaMatchType(eventArgs, instr.Layer);
                    }
                case InstrType.WaitForObjectDestroyed:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WaitForObjectDestroyed.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WaitForObjectDestroyed.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DestructionState)(byte)args[0] : new EventArg<EventEnum.DestructionState> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.WaitForObjectDestroyed(eventArgs, instr.Layer);
                    }
                case InstrType.SkipIfObjectDestroyed:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SkipIfObjectDestroyed.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SkipIfObjectDestroyed.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DestructionState)(byte)args[1] : new EventArg<EventEnum.DestructionState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.SkipIfObjectDestroyed(eventArgs, instr.Layer);
                    }
                case InstrType.EndIfObjectDestroyed:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndIfObjectDestroyed.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndIfObjectDestroyed.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.EventEndType)(byte)args[0] : new EventArg<EventEnum.EventEndType> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DestructionState)(byte)args[1] : new EventArg<EventEnum.DestructionState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.EndIfObjectDestroyed(eventArgs, instr.Layer);
                    }
                case InstrType.GotoIfObjectDestroyed:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.GotoIfObjectDestroyed.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.GotoIfObjectDestroyed.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.Label)(byte)args[0] : new EventArg<EventEnum.Label> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DestructionState)(byte)args[1] : new EventArg<EventEnum.DestructionState> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.ComparisonType)(sbyte)args[3] : new EventArg<EventEnum.ComparisonType> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.GotoIfObjectDestroyed(eventArgs, instr.Layer);
                    }
                case InstrType.Label0:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label0(eventArgs, instr.Layer);
                    }
                case InstrType.Label1:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label1(eventArgs, instr.Layer);
                    }
                case InstrType.Label2:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label2(eventArgs, instr.Layer);
                    }
                case InstrType.Label3:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label3(eventArgs, instr.Layer);
                    }
                case InstrType.Label4:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label4(eventArgs, instr.Layer);
                    }
                case InstrType.Label5:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label5(eventArgs, instr.Layer);
                    }
                case InstrType.Label6:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label6(eventArgs, instr.Layer);
                    }
                case InstrType.Label7:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label7(eventArgs, instr.Layer);
                    }
                case InstrType.Label8:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label8(eventArgs, instr.Layer);
                    }
                case InstrType.Label9:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label9(eventArgs, instr.Layer);
                    }
                case InstrType.Label10:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label10(eventArgs, instr.Layer);
                    }
                case InstrType.Label11:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label11(eventArgs, instr.Layer);
                    }
                case InstrType.Label12:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label12(eventArgs, instr.Layer);
                    }
                case InstrType.Label13:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label13(eventArgs, instr.Layer);
                    }
                case InstrType.Label14:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label14(eventArgs, instr.Layer);
                    }
                case InstrType.Label15:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label15(eventArgs, instr.Layer);
                    }
                case InstrType.Label16:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label16(eventArgs, instr.Layer);
                    }
                case InstrType.Label17:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label17(eventArgs, instr.Layer);
                    }
                case InstrType.Label18:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label18(eventArgs, instr.Layer);
                    }
                case InstrType.Label19:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label19(eventArgs, instr.Layer);
                    }
                case InstrType.Label20:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.Label20(eventArgs, instr.Layer);
                    }
                case InstrType.TerminateEvent:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.TerminateEvent.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.TerminateEvent.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] } };
                        return new InstrWrapper.TerminateEvent(eventArgs, instr.Layer);
                    }
                case InstrType.SetNetworkSyncState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNetworkSyncState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNetworkSyncState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DisabledEnabled)(byte)args[0] : new EventArg<EventEnum.DisabledEnabled> { index = p[0] } };
                        return new InstrWrapper.SetNetworkSyncState(eventArgs, instr.Layer);
                    }
                case InstrType.ClearCompiledConditionGroupState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ClearCompiledConditionGroupState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ClearCompiledConditionGroupState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.ClearCompiledConditionGroupState(eventArgs, instr.Layer);
                    }
                case InstrType.PrefetchRequest:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PrefetchRequest.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PrefetchRequest.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] } };
                        return new InstrWrapper.PrefetchRequest(eventArgs, instr.Layer);
                    }
                case InstrType.SaveRequest:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SaveRequest.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SaveRequest.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.SaveRequest(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneToAll:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneToAll.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneToAll.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] } };
                        return new InstrWrapper.PlayCutsceneToAll(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneAndWarpAllPlayers:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneAndWarpAllPlayers.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneAndWarpAllPlayers.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] },
                            p[4] == -1 ? (byte)args[4] : new EventArg<byte> { index = p[4] } };
                        return new InstrWrapper.PlayCutsceneAndWarpAllPlayers(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneToPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneToPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneToPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.PlayCutsceneToPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneAndWarpPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneAndWarpPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneAndWarpPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] },
                            p[4] == -1 ? (byte)args[4] : new EventArg<byte> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.PlayCutsceneAndWarpPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneAndRotatePlayerAroundVerticalAxis:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneAndRotatePlayerAroundVerticalAxis.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneAndRotatePlayerAroundVerticalAxis.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] } };
                        return new InstrWrapper.PlayCutsceneAndRotatePlayerAroundVerticalAxis(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneChangeTimePeriodAndWarpPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneChangeTimePeriodAndWarpPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneChangeTimePeriodAndWarpPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] },
                            p[4] == -1 ? (byte)args[4] : new EventArg<byte> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] },
                            p[6] == -1 ? (byte)args[6] : new EventArg<byte> { index = p[6] } };
                        return new InstrWrapper.PlayCutsceneChangeTimePeriodAndWarpPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneAndChangeTimePeriod:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneAndChangeTimePeriod.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneAndChangeTimePeriod.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] } };
                        return new InstrWrapper.PlayCutsceneAndChangeTimePeriod(eventArgs, instr.Layer);
                    }
                case InstrType.DummyPlayCutsceneAndWarpPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DummyPlayCutsceneAndWarpPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DummyPlayCutsceneAndWarpPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.DummyPlayCutsceneAndWarpPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneChangeMapCeremonyAndWarpPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneChangeMapCeremonyAndWarpPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneChangeMapCeremonyAndWarpPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (uint)args[2] : new EventArg<uint> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] },
                            p[5] == -1 ? (byte)args[5] : new EventArg<byte> { index = p[5] },
                            p[6] == -1 ? (byte)args[6] : new EventArg<byte> { index = p[6] },
                            p[7] == -1 ? (int)args[7] : new EventArg<int> { index = p[7] } };
                        return new InstrWrapper.PlayCutsceneChangeMapCeremonyAndWarpPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneToPlayerAndChangeCurrentMapCeremony:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneToPlayerAndChangeCurrentMapCeremony.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneToPlayerAndChangeCurrentMapCeremony.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (uint)args[2] : new EventArg<uint> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.PlayCutsceneToPlayerAndChangeCurrentMapCeremony(eventArgs, instr.Layer);
                    }
                case InstrType.PlayOngoingCutsceneAndWarpPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayOngoingCutsceneAndWarpPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayOngoingCutsceneAndWarpPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] },
                            p[4] == -1 ? (byte)args[4] : new EventArg<byte> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] },
                            p[6] == -1 ? (byte)args[6] : new EventArg<byte> { index = p[6] },
                            p[7] == -1 ? (byte)args[7] : new EventArg<byte> { index = p[7] } };
                        return new InstrWrapper.PlayOngoingCutsceneAndWarpPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.PlayCutsceneAndWarpPlayerUnknown200212:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayCutsceneAndWarpPlayerUnknown200212.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayCutsceneAndWarpPlayerUnknown200212.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.CutscenePlayMode)(uint)args[1] : new EventArg<EventEnum.CutscenePlayMode> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] : new EventArg<byte> { index = p[3] },
                            p[4] == -1 ? (byte)args[4] : new EventArg<byte> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] } };
                        return new InstrWrapper.PlayCutsceneAndWarpPlayerUnknown200212(eventArgs, instr.Layer);
                    }
                case InstrType.RequestAnimationPlayback:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RequestAnimationPlayback.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RequestAnimationPlayback.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] != 0 : new EventArg<bool> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] } };
                        return new InstrWrapper.RequestAnimationPlayback(eventArgs, instr.Layer);
                    }
                case InstrType.SetEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.OnOff)(byte)args[1] : new EventArg<EventEnum.OnOff> { index = p[1] } };
                        return new InstrWrapper.SetEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.DeActivateGenerator:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DeActivateGenerator.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DeActivateGenerator.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.DeActivateGenerator(eventArgs, instr.Layer);
                    }
                case InstrType.AwardItemLot:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.AwardItemLot.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.AwardItemLot.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.AwardItemLot(eventArgs, instr.Layer);
                    }
                case InstrType.ShootBullet:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ShootBullet.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ShootBullet.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] } };
                        return new InstrWrapper.ShootBullet(eventArgs, instr.Layer);
                    }
                case InstrType.DeActivateMapHit:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DeActivateMapHit.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DeActivateMapHit.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.DeActivateMapHit(eventArgs, instr.Layer);
                    }
                case InstrType.SetMapVisibility:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetMapVisibility.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetMapVisibility.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetMapVisibility(eventArgs, instr.Layer);
                    }
                case InstrType.SetEventState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetEventState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetEventState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.EventEndType)(byte)args[2] : new EventArg<EventEnum.EventEndType> { index = p[2] } };
                        return new InstrWrapper.SetEventState(eventArgs, instr.Layer);
                    }
                case InstrType.InvertEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.InvertEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.InvertEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.InvertEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.SetEventNavimesh:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetEventNavimesh.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetEventNavimesh.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DisabledEnabled)(short)args[0] : new EventArg<EventEnum.DisabledEnabled> { index = p[0] } };
                        return new InstrWrapper.SetEventNavimesh(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayBossHealthBar:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayBossHealthBar.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayBossHealthBar.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DisabledEnabled)(sbyte)args[0] : new EventArg<EventEnum.DisabledEnabled> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (short)args[2] : new EventArg<short> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.DisplayBossHealthBar(eventArgs, instr.Layer);
                    }
                case InstrType.HandleBossDefeat:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.HandleBossDefeat.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.HandleBossDefeat.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.HandleBossDefeat(eventArgs, instr.Layer);
                    }
                case InstrType.ModifyNavimeshConnectionBitflag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ModifyNavimeshConnectionBitflag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ModifyNavimeshConnectionBitflag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.NavimeshType)(uint)args[1] : new EventArg<EventEnum.NavimeshType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.BitopType)(byte)args[2] : new EventArg<EventEnum.BitopType> { index = p[2] } };
                        return new InstrWrapper.ModifyNavimeshConnectionBitflag(eventArgs, instr.Layer);
                    }
                case InstrType.WarpPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WarpPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WarpPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.WarpPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.HandleMinibossDefeat:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.HandleMinibossDefeat.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.HandleMinibossDefeat.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.HandleMinibossDefeat(eventArgs, instr.Layer);
                    }
                case InstrType.TriggerMultiplayerEvent:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.TriggerMultiplayerEvent.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.TriggerMultiplayerEvent.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] } };
                        return new InstrWrapper.TriggerMultiplayerEvent(eventArgs, instr.Layer);
                    }
                case InstrType.RandomlySetEventFlagInRange:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RandomlySetEventFlagInRange.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RandomlySetEventFlagInRange.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] },
                            p[2] == -1 ? (EventEnum.OnOff)(byte)args[2] : new EventArg<EventEnum.OnOff> { index = p[2] } };
                        return new InstrWrapper.RandomlySetEventFlagInRange(eventArgs, instr.Layer);
                    }
                case InstrType.ForceAnimationPlayback:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ForceAnimationPlayback.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ForceAnimationPlayback.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] != 0 : new EventArg<bool> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] },
                            p[4] == -1 ? (byte)args[4] != 0 : new EventArg<bool> { index = p[4] },
                            p[5] == -1 ? (byte)args[5] : new EventArg<byte> { index = p[5] },
                            p[6] == -1 ? (float)args[6] : new EventArg<float> { index = p[6] } };
                        return new InstrWrapper.ForceAnimationPlayback(eventArgs, instr.Layer);
                    }
                case InstrType.SetAreaDrawparamBankSlotIndex:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetAreaDrawparamBankSlotIndex.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetAreaDrawparamBankSlotIndex.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (short)args[0] : new EventArg<short> { index = p[0] },
                            p[1] == -1 ? (short)args[1] : new EventArg<short> { index = p[1] } };
                        return new InstrWrapper.SetAreaDrawparamBankSlotIndex(eventArgs, instr.Layer);
                    }
                case InstrType.SetTemporaryPlayerRespawnPoint:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetTemporaryPlayerRespawnPoint.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetTemporaryPlayerRespawnPoint.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SetTemporaryPlayerRespawnPoint(eventArgs, instr.Layer);
                    }
                case InstrType.IncrementGameCycle:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IncrementGameCycle.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IncrementGameCycle.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.IncrementGameCycle(eventArgs, instr.Layer);
                    }
                case InstrType.BatchSetEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.BatchSetEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.BatchSetEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.OnOff)(byte)args[2] : new EventArg<EventEnum.OnOff> { index = p[2] } };
                        return new InstrWrapper.BatchSetEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.SetPlayerRespawnPoint:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetPlayerRespawnPoint.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetPlayerRespawnPoint.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SetPlayerRespawnPoint(eventArgs, instr.Layer);
                    }
                case InstrType.RemoveItemFromPlayer:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RemoveItemFromPlayer.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RemoveItemFromPlayer.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ItemType)(int)args[0] : new EventArg<EventEnum.ItemType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.RemoveItemFromPlayer(eventArgs, instr.Layer);
                    }
                case InstrType.PlaceNpcSummonSign:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlaceNpcSummonSign.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlaceNpcSummonSign.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.SummonSignType)(int)args[0] : new EventArg<EventEnum.SummonSignType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.PlaceNpcSummonSign(eventArgs, instr.Layer);
                    }
                case InstrType.SetVisibilityOfMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetVisibilityOfMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetVisibilityOfMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetVisibilityOfMessage(eventArgs, instr.Layer);
                    }
                case InstrType.Missing:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.Missing.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.Missing.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.Missing(eventArgs, instr.Layer);
                    }
                case InstrType.AwardAchievement:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.AwardAchievement.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.AwardAchievement.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.AwardAchievement(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeWorldTendency:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeWorldTendency.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeWorldTendency.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.TendencyType)(byte)args[0] : new EventArg<EventEnum.TendencyType> { index = p[0] },
                            p[1] == -1 ? (sbyte)args[1] : new EventArg<sbyte> { index = p[1] } };
                        return new InstrWrapper.ChangeWorldTendency(eventArgs, instr.Layer);
                    }
                case InstrType.DisableVagrantSpawning:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisableVagrantSpawning.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisableVagrantSpawning.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] != 0 : new EventArg<bool> { index = p[0] } };
                        return new InstrWrapper.DisableVagrantSpawning(eventArgs, instr.Layer);
                    }
                case InstrType.IncrementEventValue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IncrementEventValue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IncrementEventValue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] },
                            p[2] == -1 ? (uint)args[2] : new EventArg<uint> { index = p[2] } };
                        return new InstrWrapper.IncrementEventValue(eventArgs, instr.Layer);
                    }
                case InstrType.ClearEventValue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ClearEventValue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ClearEventValue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] } };
                        return new InstrWrapper.ClearEventValue(eventArgs, instr.Layer);
                    }
                case InstrType.SetSnugglyNextTrade:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetSnugglyNextTrade.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetSnugglyNextTrade.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SetSnugglyNextTrade(eventArgs, instr.Layer);
                    }
                case InstrType.SpawnSnugglyItem:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SpawnSnugglyItem.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SpawnSnugglyItem.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.SpawnSnugglyItem(eventArgs, instr.Layer);
                    }
                case InstrType.MoveBloodstainAndDroppedItems:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.MoveBloodstainAndDroppedItems.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.MoveBloodstainAndDroppedItems.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.MoveBloodstainAndDroppedItems(eventArgs, instr.Layer);
                    }
                case InstrType.AwardItemsIncludingClients:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.AwardItemsIncludingClients.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.AwardItemsIncludingClients.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.AwardItemsIncludingClients(eventArgs, instr.Layer);
                    }
                case InstrType.BattleOfStoicism1v1RankingRequest:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.BattleOfStoicism1v1RankingRequest(eventArgs, instr.Layer);
                    }
                case InstrType.BattleOfStoicism2v2RankingRequest:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.BattleOfStoicism2v2RankingRequest(eventArgs, instr.Layer);
                    }
                case InstrType.BattleOfStoicismFfaRankingRequest:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.BattleOfStoicismFfaRankingRequest(eventArgs, instr.Layer);
                    }
                case InstrType.BattleOfStoicismExitRequest:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.BattleOfStoicismExitRequest(eventArgs, instr.Layer);
                    }
                case InstrType.EventValueOperation:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EventValueOperation.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EventValueOperation.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (uint)args[4] : new EventArg<uint> { index = p[4] },
                            p[5] == -1 ? (EventEnum.CalculationType)(sbyte)args[5] : new EventArg<EventEnum.CalculationType> { index = p[5] } };
                        return new InstrWrapper.EventValueOperation(eventArgs, instr.Layer);
                    }
                case InstrType.StoreItemAmountHeldInEventValue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.StoreItemAmountHeldInEventValue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.StoreItemAmountHeldInEventValue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ItemType)(int)args[0] : new EventArg<EventEnum.ItemType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (uint)args[3] : new EventArg<uint> { index = p[3] } };
                        return new InstrWrapper.StoreItemAmountHeldInEventValue(eventArgs, instr.Layer);
                    }
                case InstrType.DirectlyGivePlayerItem:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DirectlyGivePlayerItem.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DirectlyGivePlayerItem.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ItemType)(int)args[0] : new EventArg<EventEnum.ItemType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (uint)args[3] : new EventArg<uint> { index = p[3] } };
                        return new InstrWrapper.DirectlyGivePlayerItem(eventArgs, instr.Layer);
                    }
                case InstrType.ToggleDirectionDisplay:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ToggleDirectionDisplay.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ToggleDirectionDisplay.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DisabledEnabled)(byte)args[0] : new EventArg<EventEnum.DisabledEnabled> { index = p[0] } };
                        return new InstrWrapper.ToggleDirectionDisplay(eventArgs, instr.Layer);
                    }
                case InstrType.SetMapHitgridCorrespondence:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetMapHitgridCorrespondence.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetMapHitgridCorrespondence.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (short)args[1] : new EventArg<short> { index = p[1] },
                            p[2] == -1 ? (short)args[2] : new EventArg<short> { index = p[2] },
                            p[3] == -1 ? (short)args[3] : new EventArg<short> { index = p[3] },
                            p[4] == -1 ? (EventEnum.AddDelete)(byte)args[4] : new EventArg<EventEnum.AddDelete> { index = p[4] } };
                        return new InstrWrapper.SetMapHitgridCorrespondence(eventArgs, instr.Layer);
                    }
                case InstrType.SetMapContentImageDisplay:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetMapContentImageDisplay.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetMapContentImageDisplay.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (short)args[0] : new EventArg<short> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisplayState)(byte)args[1] : new EventArg<EventEnum.DisplayState> { index = p[1] } };
                        return new InstrWrapper.SetMapContentImageDisplay(eventArgs, instr.Layer);
                    }
                case InstrType.SetMapBoundariesDisplay:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetMapBoundariesDisplay.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetMapBoundariesDisplay.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (short)args[0] : new EventArg<short> { index = p[0] },
                            p[1] == -1 ? (short)args[1] : new EventArg<short> { index = p[1] },
                            p[2] == -1 ? (short)args[2] : new EventArg<short> { index = p[2] },
                            p[3] == -1 ? (EventEnum.DisplayState)(byte)args[3] : new EventArg<EventEnum.DisplayState> { index = p[3] } };
                        return new InstrWrapper.SetMapBoundariesDisplay(eventArgs, instr.Layer);
                    }
                case InstrType.SetAreaWind:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetAreaWind.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetAreaWind.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.SetAreaWind(eventArgs, instr.Layer);
                    }
                case InstrType.WarpPlayerToRespawnPoint:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WarpPlayerToRespawnPoint.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WarpPlayerToRespawnPoint.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.WarpPlayerToRespawnPoint(eventArgs, instr.Layer);
                    }
                case InstrType.StartEnemyGenerator:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.StartEnemyGenerator.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.StartEnemyGenerator.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.StartEnemyGenerator(eventArgs, instr.Layer);
                    }
                case InstrType.SummonNpc:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SummonNpc.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SummonNpc.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.SingleplayerSummonSignType)(int)args[0] : new EventArg<EventEnum.SingleplayerSummonSignType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.SummonNpc(eventArgs, instr.Layer);
                    }
                case InstrType.InitializeWarpObject:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.InitializeWarpObject.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.InitializeWarpObject.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.InitializeWarpObject(eventArgs, instr.Layer);
                    }
                case InstrType.MakeEnemyAppearEvent:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.MakeEnemyAppearEvent.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.MakeEnemyAppearEvent.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.MakeEnemyAppearEvent(eventArgs, instr.Layer);
                    }
                case InstrType.SetCurrentMapCeremony:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCurrentMapCeremony.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCurrentMapCeremony.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] } };
                        return new InstrWrapper.SetCurrentMapCeremony(eventArgs, instr.Layer);
                    }
                case InstrType.SetMapCeremony:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetMapCeremony.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetMapCeremony.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (uint)args[2] : new EventArg<uint> { index = p[2] } };
                        return new InstrWrapper.SetMapCeremony(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayEpitaphMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayEpitaphMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayEpitaphMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.DisplayEpitaphMessage(eventArgs, instr.Layer);
                    }
                case InstrType.SetNetworkConnectedEventFlag:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNetworkConnectedEventFlag.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNetworkConnectedEventFlag.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.OnOff)(byte)args[1] : new EventArg<EventEnum.OnOff> { index = p[1] } };
                        return new InstrWrapper.SetNetworkConnectedEventFlag(eventArgs, instr.Layer);
                    }
                case InstrType.BatchSetNetworkConnectedEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.BatchSetNetworkConnectedEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.BatchSetNetworkConnectedEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.OnOff)(byte)args[2] : new EventArg<EventEnum.OnOff> { index = p[2] } };
                        return new InstrWrapper.BatchSetNetworkConnectedEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.SetOmissionModeCounts:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetOmissionModeCounts.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetOmissionModeCounts.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.SetOmissionModeCounts(eventArgs, instr.Layer);
                    }
                case InstrType.ResetOmissionModeCountsToDefault:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.ResetOmissionModeCountsToDefault(eventArgs, instr.Layer);
                    }
                case InstrType.InitializeCrowTrade:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.InitializeCrowTrade.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.InitializeCrowTrade.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.ItemType)(uint)args[0] : new EventArg<EventEnum.ItemType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.InitializeCrowTrade(eventArgs, instr.Layer);
                    }
                case InstrType.InitializeCrowTradeArea:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.InitializeCrowTradeArea.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.InitializeCrowTradeArea.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.InitializeCrowTradeArea(eventArgs, instr.Layer);
                    }
                case InstrType.SetNetworkInteractionState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNetworkInteractionState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNetworkInteractionState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DisabledEnabled)(byte)args[0] : new EventArg<EventEnum.DisabledEnabled> { index = p[0] } };
                        return new InstrWrapper.SetNetworkInteractionState(eventArgs, instr.Layer);
                    }
                case InstrType.HideHud:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.HideHud.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.HideHud.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] != 0 : new EventArg<bool> { index = p[0] } };
                        return new InstrWrapper.HideHud(eventArgs, instr.Layer);
                    }
                case InstrType.SetBonfireWarpingState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetBonfireWarpingState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetBonfireWarpingState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.DisabledEnabled)(byte)args[2] : new EventArg<EventEnum.DisabledEnabled> { index = p[2] } };
                        return new InstrWrapper.SetBonfireWarpingState(eventArgs, instr.Layer);
                    }
                case InstrType.SetAutogeneratedEventSpecificEventFlag1Unknown200373:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetAutogeneratedEventSpecificEventFlag1Unknown200373.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetAutogeneratedEventSpecificEventFlag1Unknown200373.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] } };
                        return new InstrWrapper.SetAutogeneratedEventSpecificEventFlag1Unknown200373(eventArgs, instr.Layer);
                    }
                case InstrType.HandleBossDefeatAndDisplayBanner:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.HandleBossDefeatAndDisplayBanner.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.HandleBossDefeatAndDisplayBanner.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TextBannerType)(byte)args[1] : new EventArg<EventEnum.TextBannerType> { index = p[1] } };
                        return new InstrWrapper.HandleBossDefeatAndDisplayBanner(eventArgs, instr.Layer);
                    }
                case InstrType.SetAutogeneratedEventSpecificEventFlag2Unknown200375:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetAutogeneratedEventSpecificEventFlag2Unknown200375.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetAutogeneratedEventSpecificEventFlag2Unknown200375.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] } };
                        return new InstrWrapper.SetAutogeneratedEventSpecificEventFlag2Unknown200375(eventArgs, instr.Layer);
                    }
                case InstrType.DisableLoadingScreenTips:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisableLoadingScreenTips.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisableLoadingScreenTips.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] != 0 : new EventArg<bool> { index = p[0] } };
                        return new InstrWrapper.DisableLoadingScreenTips(eventArgs, instr.Layer);
                    }
                case InstrType.AwardGestureItem:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.AwardGestureItem.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.AwardGestureItem.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (ushort)args[0] : new EventArg<ushort> { index = p[0] },
                            p[1] == -1 ? (EventEnum.ItemType)(ushort)args[1] : new EventArg<EventEnum.ItemType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.AwardGestureItem(eventArgs, instr.Layer);
                    }
                case InstrType.SendNpcSummonHome:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SendNpcSummonHome.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SendNpcSummonHome.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SendNpcSummonHome(eventArgs, instr.Layer);
                    }
                case InstrType.Unknown200379:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.Unknown200379.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.Unknown200379.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.Unknown200379(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayDecoratedBossHealthBar:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayDecoratedBossHealthBar.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayDecoratedBossHealthBar.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DisabledEnabled)(sbyte)args[0] : new EventArg<EventEnum.DisabledEnabled> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (short)args[2] : new EventArg<short> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (byte)args[4] : new EventArg<byte> { index = p[4] } };
                        return new InstrWrapper.DisplayDecoratedBossHealthBar(eventArgs, instr.Layer);
                    }
                case InstrType.PlaceNpcSummonSignWorksForNonEmberedPlayers:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlaceNpcSummonSignWorksForNonEmberedPlayers.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlaceNpcSummonSignWorksForNonEmberedPlayers.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.SummonSignType)(int)args[0] : new EventArg<EventEnum.SummonSignType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.PlaceNpcSummonSignWorksForNonEmberedPlayers(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterAiState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterAiState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterAiState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetCharacterAiState(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterTeamType:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterTeamType.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterTeamType.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TeamType)(byte)args[1] : new EventArg<EventEnum.TeamType> { index = p[1] } };
                        return new InstrWrapper.SetCharacterTeamType(eventArgs, instr.Layer);
                    }
                case InstrType.CharacterWarpRequest:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.CharacterWarpRequest.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.CharacterWarpRequest.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(byte)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.CharacterWarpRequest(eventArgs, instr.Layer);
                    }
                case InstrType.ForceCharacterDeath:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ForceCharacterDeath.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ForceCharacterDeath.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.ForceCharacterDeath(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCharacterEnableState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCharacterEnableState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCharacterEnableState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.ChangeCharacterEnableState(eventArgs, instr.Layer);
                    }
                case InstrType.EzstateInstructionRequest:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EzstateInstructionRequest.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EzstateInstructionRequest.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.EzstateInstructionRequest(eventArgs, instr.Layer);
                    }
                case InstrType.CreateBulletOwner:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.CreateBulletOwner.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.CreateBulletOwner.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.CreateBulletOwner(eventArgs, instr.Layer);
                    }
                case InstrType.SetSpeffect:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetSpeffect.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetSpeffect.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.SetSpeffect(eventArgs, instr.Layer);
                    }
                case InstrType.SetSpecialAnimationS:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetSpecialAnimationS.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetSpecialAnimationS.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.SetSpecialAnimationS(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterGravity:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterGravity.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterGravity.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetCharacterGravity(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterEventTarget:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterEventTarget.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterEventTarget.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.SetCharacterEventTarget(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterImmortality:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterImmortality.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterImmortality.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetCharacterImmortality(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterHome:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterHome.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterHome.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.SetCharacterHome(eventArgs, instr.Layer);
                    }
                case InstrType.RotateCharacter:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RotateCharacter.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RotateCharacter.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] } };
                        return new InstrWrapper.RotateCharacter(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterInvincibility:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterInvincibility.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterInvincibility.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetCharacterInvincibility(eventArgs, instr.Layer);
                    }
                case InstrType.ClearCharacterSAiTarget:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ClearCharacterSAiTarget.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ClearCharacterSAiTarget.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.ClearCharacterSAiTarget(eventArgs, instr.Layer);
                    }
                case InstrType.RequestCharacterAiCommand:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RequestCharacterAiCommand.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RequestCharacterAiCommand.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.RequestCharacterAiCommand(eventArgs, instr.Layer);
                    }
                case InstrType.SetEventPoint:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetEventPoint.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetEventPoint.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] } };
                        return new InstrWrapper.SetEventPoint(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterAiId:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterAiId.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterAiId.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.SetCharacterAiId(eventArgs, instr.Layer);
                    }
                case InstrType.RequestCharacterAiRePlan:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RequestCharacterAiRePlan.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RequestCharacterAiRePlan.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.RequestCharacterAiRePlan(eventArgs, instr.Layer);
                    }
                case InstrType.ClearSpeffect:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ClearSpeffect.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ClearSpeffect.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.ClearSpeffect(eventArgs, instr.Layer);
                    }
                case InstrType.CreateNpcPart:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.CreateNpcPart.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.CreateNpcPart.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (short)args[1] : new EventArg<short> { index = p[1] },
                            p[2] == -1 ? (EventEnum.NpcPartType)(short)args[2] : new EventArg<EventEnum.NpcPartType> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] },
                            p[6] == -1 ? (byte)args[6] != 0 : new EventArg<bool> { index = p[6] },
                            p[7] == -1 ? (byte)args[7] != 0 : new EventArg<bool> { index = p[7] } };
                        return new InstrWrapper.CreateNpcPart(eventArgs, instr.Layer);
                    }
                case InstrType.SetNpcPartHp:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNpcPartHp.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNpcPartHp.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (byte)args[3] != 0 : new EventArg<bool> { index = p[3] } };
                        return new InstrWrapper.SetNpcPartHp(eventArgs, instr.Layer);
                    }
                case InstrType.SetNpcPartSeAndSfx:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNpcPartSeAndSfx.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNpcPartSeAndSfx.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.SetNpcPartSeAndSfx(eventArgs, instr.Layer);
                    }
                case InstrType.SetNpcPartBulletDamageMultiplier:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNpcPartBulletDamageMultiplier.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNpcPartBulletDamageMultiplier.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] } };
                        return new InstrWrapper.SetNpcPartBulletDamageMultiplier(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCharacterDispmask:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCharacterDispmask.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCharacterDispmask.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (EventEnum.OnOffChange)(byte)args[2] : new EventArg<EventEnum.OnOffChange> { index = p[2] } };
                        return new InstrWrapper.ChangeCharacterDispmask(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCharacterHitmask:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCharacterHitmask.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCharacterHitmask.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (EventEnum.OnOffChange)(byte)args[2] : new EventArg<EventEnum.OnOffChange> { index = p[2] } };
                        return new InstrWrapper.ChangeCharacterHitmask(eventArgs, instr.Layer);
                    }
                case InstrType.SetNetworkUpdateAuthority:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNetworkUpdateAuthority.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNetworkUpdateAuthority.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.AuthorityLevel)(int)args[1] : new EventArg<EventEnum.AuthorityLevel> { index = p[1] } };
                        return new InstrWrapper.SetNetworkUpdateAuthority(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterBackreadState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterBackreadState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterBackreadState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.SetCharacterBackreadState(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterHpBarDisplay:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterHpBarDisplay.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterHpBarDisplay.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetCharacterHpBarDisplay(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterMaphit:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterMaphit.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterMaphit.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.SetCharacterMaphit(eventArgs, instr.Layer);
                    }
                case InstrType.RequestCharacterAiCommandEventValue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RequestCharacterAiCommandEventValue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RequestCharacterAiCommandEventValue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.RequestCharacterAiCommandEventValue(eventArgs, instr.Layer);
                    }
                case InstrType.CreateReferredDamagePair:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.CreateReferredDamagePair.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.CreateReferredDamagePair.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.CreateReferredDamagePair(eventArgs, instr.Layer);
                    }
                case InstrType.SetNetworkUpdateRate:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetNetworkUpdateRate.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetNetworkUpdateRate.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] },
                            p[2] == -1 ? (EventEnum.CharacterUpdateFrequency)(sbyte)args[2] : new EventArg<EventEnum.CharacterUpdateFrequency> { index = p[2] } };
                        return new InstrWrapper.SetNetworkUpdateRate(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterDefaultBackreadState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterDefaultBackreadState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterDefaultBackreadState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetCharacterDefaultBackreadState(eventArgs, instr.Layer);
                    }
                case InstrType.HellkiteBreathControl:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.HellkiteBreathControl.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.HellkiteBreathControl.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.HellkiteBreathControl(eventArgs, instr.Layer);
                    }
                case InstrType.ForceCharacterTreasure:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ForceCharacterTreasure.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ForceCharacterTreasure.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.ForceCharacterTreasure(eventArgs, instr.Layer);
                    }
                case InstrType.BetrayCharacterSCurrentCovenant:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.BetrayCharacterSCurrentCovenant.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.BetrayCharacterSCurrentCovenant.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.BetrayCharacterSCurrentCovenant(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterAnimationState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterAnimationState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterAnimationState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetCharacterAnimationState(eventArgs, instr.Layer);
                    }
                case InstrType.WarpCharacterAndSetFloor:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WarpCharacterAndSetFloor.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WarpCharacterAndSetFloor.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(byte)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.WarpCharacterAndSetFloor(eventArgs, instr.Layer);
                    }
                case InstrType.IssueShortWarpRequest:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IssueShortWarpRequest.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IssueShortWarpRequest.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(byte)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.IssueShortWarpRequest(eventArgs, instr.Layer);
                    }
                case InstrType.WarpCharacterAndCopyFloor:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WarpCharacterAndCopyFloor.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WarpCharacterAndCopyFloor.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(byte)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.WarpCharacterAndCopyFloor(eventArgs, instr.Layer);
                    }
                case InstrType.RequestCharacterAnimationReset:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RequestCharacterAnimationReset.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RequestCharacterAnimationReset.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.Interpolation)(byte)args[1] : new EventArg<EventEnum.Interpolation> { index = p[1] } };
                        return new InstrWrapper.RequestCharacterAnimationReset(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCharacterTeamTypeAndForceExitSpecialAnimations:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCharacterTeamTypeAndForceExitSpecialAnimations.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCharacterTeamTypeAndForceExitSpecialAnimations.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TeamType)(byte)args[1] : new EventArg<EventEnum.TeamType> { index = p[1] } };
                        return new InstrWrapper.ChangeCharacterTeamTypeAndForceExitSpecialAnimations(eventArgs, instr.Layer);
                    }
                case InstrType.RegisterNpcHumanityEventValue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RegisterNpcHumanityEventValue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RegisterNpcHumanityEventValue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.RegisterNpcHumanityEventValue(eventArgs, instr.Layer);
                    }
                case InstrType.IncrementPlayerPvpSin:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IncrementPlayerPvpSin.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IncrementPlayerPvpSin.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.IncrementPlayerPvpSin(eventArgs, instr.Layer);
                    }
                case InstrType.BonfireLikeRecovery:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.BonfireLikeRecovery(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCharacterSCloth:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCharacterSCloth.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCharacterSCloth.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] } };
                        return new InstrWrapper.ChangeCharacterSCloth(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCharacterPatrolBehavior:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCharacterPatrolBehavior.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCharacterPatrolBehavior.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.ChangeCharacterPatrolBehavior(eventArgs, instr.Layer);
                    }
                case InstrType.SetLockOnPoint:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetLockOnPoint.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetLockOnPoint.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.DisabledEnabled)(byte)args[2] : new EventArg<EventEnum.DisabledEnabled> { index = p[2] } };
                        return new InstrWrapper.SetLockOnPoint(eventArgs, instr.Layer);
                    }
                case InstrType.TestRequestRagdollRestraint:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.TestRequestRagdollRestraint.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.TestRequestRagdollRestraint.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.TestRequestRagdollRestraint(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCharacter:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCharacter.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCharacter.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.ChangeCharacter(eventArgs, instr.Layer);
                    }
                case InstrType.AdaptHpChangingSpeffectToNpcPartOfTarget:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.AdaptHpChangingSpeffectToNpcPartOfTarget.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.AdaptHpChangingSpeffectToNpcPartOfTarget.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.AdaptHpChangingSpeffectToNpcPartOfTarget(eventArgs, instr.Layer);
                    }
                case InstrType.ImmediateActivateUnknown200454:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ImmediateActivateUnknown200454.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ImmediateActivateUnknown200454.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.ImmediateActivateUnknown200454(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterTalkRange:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterTalkRange.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterTalkRange.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (float)args[1] : new EventArg<float> { index = p[1] } };
                        return new InstrWrapper.SetCharacterTalkRange(eventArgs, instr.Layer);
                    }
                case InstrType.IncrementCharacterGameCycle:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IncrementCharacterGameCycle.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IncrementCharacterGameCycle.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.IncrementCharacterGameCycle(eventArgs, instr.Layer);
                    }
                case InstrType.SetMultiplayerDependentBuffsNonBoss:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetMultiplayerDependentBuffsNonBoss.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetMultiplayerDependentBuffsNonBoss.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetMultiplayerDependentBuffsNonBoss(eventArgs, instr.Layer);
                    }
                case InstrType.Unknown200459:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.Unknown200459.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.Unknown200459.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.Unknown200459(eventArgs, instr.Layer);
                    }
                case InstrType.SetCharacterRemainingYoelLevels:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCharacterRemainingYoelLevels.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCharacterRemainingYoelLevels.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.SetCharacterRemainingYoelLevels(eventArgs, instr.Layer);
                    }
                case InstrType.RequestObjectDestruction:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RequestObjectDestruction.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RequestObjectDestruction.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (sbyte)args[1] : new EventArg<sbyte> { index = p[1] } };
                        return new InstrWrapper.RequestObjectDestruction(eventArgs, instr.Layer);
                    }
                case InstrType.RequestObjectRestoration:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RequestObjectRestoration.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RequestObjectRestoration.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.RequestObjectRestoration(eventArgs, instr.Layer);
                    }
                case InstrType.DeActivateObject:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DeActivateObject.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DeActivateObject.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.DeActivateObject(eventArgs, instr.Layer);
                    }
                case InstrType.SetObjectTreasureState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetObjectTreasureState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetObjectTreasureState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetObjectTreasureState(eventArgs, instr.Layer);
                    }
                case InstrType.InitializeObjact:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.InitializeObjact.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.InitializeObjact.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.InitializeObjact(eventArgs, instr.Layer);
                    }
                case InstrType.SetObjactState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetObjactState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetObjactState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (EventEnum.DisabledEnabled)(byte)args[2] : new EventArg<EventEnum.DisabledEnabled> { index = p[2] } };
                        return new InstrWrapper.SetObjactState(eventArgs, instr.Layer);
                    }
                case InstrType.ReproduceObjectAnimation:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ReproduceObjectAnimation.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ReproduceObjectAnimation.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.ReproduceObjectAnimation(eventArgs, instr.Layer);
                    }
                case InstrType.ReproduceObjectDestruction:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ReproduceObjectDestruction.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ReproduceObjectDestruction.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (sbyte)args[1] : new EventArg<sbyte> { index = p[1] } };
                        return new InstrWrapper.ReproduceObjectDestruction(eventArgs, instr.Layer);
                    }
                case InstrType.CreateDamagingObject:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.CreateDamagingObject.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.CreateDamagingObject.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (EventEnum.DamageTargetType)(int)args[4] : new EventArg<EventEnum.DamageTargetType> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] },
                            p[6] == -1 ? (float)args[6] : new EventArg<float> { index = p[6] },
                            p[7] == -1 ? (float)args[7] : new EventArg<float> { index = p[7] } };
                        return new InstrWrapper.CreateDamagingObject(eventArgs, instr.Layer);
                    }
                case InstrType.RegisterStatue:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RegisterStatue.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RegisterStatue.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (byte)args[2] : new EventArg<byte> { index = p[2] },
                            p[3] == -1 ? (EventEnum.StatueType)(byte)args[3] : new EventArg<EventEnum.StatueType> { index = p[3] } };
                        return new InstrWrapper.RegisterStatue(eventArgs, instr.Layer);
                    }
                case InstrType.WarpObjectToCharacter:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.WarpObjectToCharacter.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.WarpObjectToCharacter.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (short)args[2] : new EventArg<short> { index = p[2] } };
                        return new InstrWrapper.WarpObjectToCharacter(eventArgs, instr.Layer);
                    }
                case InstrType.DeleteObjectEvent:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DeleteObjectEvent.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DeleteObjectEvent.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.DeleteObjectEvent(eventArgs, instr.Layer);
                    }
                case InstrType.SetObjectInvulnerability:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetObjectInvulnerability.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetObjectInvulnerability.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetObjectInvulnerability(eventArgs, instr.Layer);
                    }
                case InstrType.DeActivateObjactAssignIdx:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DeActivateObjactAssignIdx.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DeActivateObjactAssignIdx.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.DisabledEnabled)(byte)args[3] : new EventArg<EventEnum.DisabledEnabled> { index = p[3] } };
                        return new InstrWrapper.DeActivateObjactAssignIdx(eventArgs, instr.Layer);
                    }
                case InstrType.RerollObjectTreasure:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RerollObjectTreasure.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RerollObjectTreasure.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.RerollObjectTreasure(eventArgs, instr.Layer);
                    }
                case InstrType.ExtinguishBurningObjects:
                    {
                        AbstractEventArg[] eventArgs = { };
                        return new InstrWrapper.ExtinguishBurningObjects(eventArgs, instr.Layer);
                    }
                case InstrType.ShowObjectByMapCeremony:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ShowObjectByMapCeremony.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ShowObjectByMapCeremony.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.ShowObjectByMapCeremony(eventArgs, instr.Layer);
                    }
                case InstrType.DestroyObject:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DestroyObject.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DestroyObject.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.DestroyObject(eventArgs, instr.Layer);
                    }
                case InstrType.DeleteMapSfx:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DeleteMapSfx.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DeleteMapSfx.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.DeleteMapSfx(eventArgs, instr.Layer);
                    }
                case InstrType.SpawnMapSfx:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SpawnMapSfx.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SpawnMapSfx.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SpawnMapSfx(eventArgs, instr.Layer);
                    }
                case InstrType.SpawnOneshotSfx:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SpawnOneshotSfx.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SpawnOneshotSfx.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.TargetEntityType)(int)args[0] : new EventArg<EventEnum.TargetEntityType> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] } };
                        return new InstrWrapper.SpawnOneshotSfx(eventArgs, instr.Layer);
                    }
                case InstrType.CreateObjectFollowingSfx:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.CreateObjectFollowingSfx.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.CreateObjectFollowingSfx.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.CreateObjectFollowingSfx(eventArgs, instr.Layer);
                    }
                case InstrType.DeleteObjectFollowingSfx:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DeleteObjectFollowingSfx.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DeleteObjectFollowingSfx.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] != 0 : new EventArg<bool> { index = p[1] } };
                        return new InstrWrapper.DeleteObjectFollowingSfx(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayGenericDialog:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayGenericDialog.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayGenericDialog.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.PromptType)(short)args[1] : new EventArg<EventEnum.PromptType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.NumberOfOptions)(short)args[2] : new EventArg<EventEnum.NumberOfOptions> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] } };
                        return new InstrWrapper.DisplayGenericDialog(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayBanner:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayBanner.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayBanner.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.TextBannerType)(byte)args[0] : new EventArg<EventEnum.TextBannerType> { index = p[0] } };
                        return new InstrWrapper.DisplayBanner(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayStatusMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayStatusMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayStatusMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.DisplayStatusMessage(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] } };
                        return new InstrWrapper.DisplayMessage(eventArgs, instr.Layer);
                    }
                case InstrType.SetBattleOfStocismNametagA:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetBattleOfStocismNametagA.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetBattleOfStocismNametagA.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SetBattleOfStocismNametagA(eventArgs, instr.Layer);
                    }
                case InstrType.SetBattleOfStocismNametagB:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetBattleOfStocismNametagB.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetBattleOfStocismNametagB.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SetBattleOfStocismNametagB(eventArgs, instr.Layer);
                    }
                case InstrType.SetBattleOfStocismNametagC:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetBattleOfStocismNametagC.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetBattleOfStocismNametagC.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SetBattleOfStocismNametagC(eventArgs, instr.Layer);
                    }
                case InstrType.SetBattleOfStocismNametagD:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetBattleOfStocismNametagD.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetBattleOfStocismNametagD.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SetBattleOfStocismNametagD(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayBattleOfStocismDissolutionMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayBattleOfStocismDissolutionMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayBattleOfStocismDissolutionMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.DisplayBattleOfStocismDissolutionMessage(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayGenericDialogAndSetEventFlags:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayGenericDialogAndSetEventFlags.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayGenericDialogAndSetEventFlags.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.PromptType)(short)args[1] : new EventArg<EventEnum.PromptType> { index = p[1] },
                            p[2] == -1 ? (EventEnum.NumberOfOptions)(short)args[2] : new EventArg<EventEnum.NumberOfOptions> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] },
                            p[6] == -1 ? (int)args[6] : new EventArg<int> { index = p[6] },
                            p[7] == -1 ? (int)args[7] : new EventArg<int> { index = p[7] } };
                        return new InstrWrapper.DisplayGenericDialogAndSetEventFlags(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayAreaWelcomeMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayAreaWelcomeMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayAreaWelcomeMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.DisplayAreaWelcomeMessage(eventArgs, instr.Layer);
                    }
                case InstrType.DisplayHollowArenaPvpMessage:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.DisplayHollowArenaPvpMessage.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.DisplayHollowArenaPvpMessage.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (short)args[1] : new EventArg<short> { index = p[1] },
                            p[2] == -1 ? (EventEnum.DisabledEnabled)(byte)args[2] : new EventArg<EventEnum.DisabledEnabled> { index = p[2] } };
                        return new InstrWrapper.DisplayHollowArenaPvpMessage(eventArgs, instr.Layer);
                    }
                case InstrType.ChangeCamera:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ChangeCamera.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ChangeCamera.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] } };
                        return new InstrWrapper.ChangeCamera(eventArgs, instr.Layer);
                    }
                case InstrType.SetCameraVibration:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetCameraVibration.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetCameraVibration.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.TargetEntityType)(int)args[1] : new EventArg<EventEnum.TargetEntityType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (int)args[3] : new EventArg<int> { index = p[3] },
                            p[4] == -1 ? (float)args[4] : new EventArg<float> { index = p[4] },
                            p[5] == -1 ? (float)args[5] : new EventArg<float> { index = p[5] } };
                        return new InstrWrapper.SetCameraVibration(eventArgs, instr.Layer);
                    }
                case InstrType.SetLockcamSlotNumber:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetLockcamSlotNumber.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetLockcamSlotNumber.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] },
                            p[1] == -1 ? (byte)args[1] : new EventArg<byte> { index = p[1] },
                            p[2] == -1 ? (ushort)args[2] : new EventArg<ushort> { index = p[2] } };
                        return new InstrWrapper.SetLockcamSlotNumber(eventArgs, instr.Layer);
                    }
                case InstrType.RegisterLadder:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RegisterLadder.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RegisterLadder.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.RegisterLadder(eventArgs, instr.Layer);
                    }
                case InstrType.InitializeWanderingDemon:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.InitializeWanderingDemon.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.InitializeWanderingDemon.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.InitializeWanderingDemon(eventArgs, instr.Layer);
                    }
                case InstrType.RegisterWanderingDemon:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RegisterWanderingDemon.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RegisterWanderingDemon.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.RegisterWanderingDemon(eventArgs, instr.Layer);
                    }
                case InstrType.RegisterBonfire:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RegisterBonfire.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RegisterBonfire.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.RegisterBonfire(eventArgs, instr.Layer);
                    }
                case InstrType.ActivateMultiplayerDependantBuffs:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ActivateMultiplayerDependantBuffs.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ActivateMultiplayerDependantBuffs.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.ActivateMultiplayerDependantBuffs(eventArgs, instr.Layer);
                    }
                case InstrType.RegisterHealingFountain:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.RegisterHealingFountain.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.RegisterHealingFountain.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (int)args[1] : new EventArg<int> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] },
                            p[3] == -1 ? (float)args[3] : new EventArg<float> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] },
                            p[5] == -1 ? (int)args[5] : new EventArg<int> { index = p[5] } };
                        return new InstrWrapper.RegisterHealingFountain(eventArgs, instr.Layer);
                    }
                case InstrType.IssueBossRoomEntryNotification:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.IssueBossRoomEntryNotification.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.IssueBossRoomEntryNotification.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (byte)args[0] : new EventArg<byte> { index = p[0] } };
                        return new InstrWrapper.IssueBossRoomEntryNotification(eventArgs, instr.Layer);
                    }
                case InstrType.SendInvadingPhantomsHome:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SendInvadingPhantomsHome.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SendInvadingPhantomsHome.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SendInvadingPhantomsHome(eventArgs, instr.Layer);
                    }
                case InstrType.SendAllPhantomsHomeAndUpdateServerPvpStats:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SendAllPhantomsHomeAndUpdateServerPvpStats.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SendAllPhantomsHomeAndUpdateServerPvpStats.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SendAllPhantomsHomeAndUpdateServerPvpStats(eventArgs, instr.Layer);
                    }
                case InstrType.SendAllPhantomsHome:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SendAllPhantomsHome.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SendAllPhantomsHome.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.SendAllPhantomsHome(eventArgs, instr.Layer);
                    }
                case InstrType.PlayBgm:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlayBgm.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlayBgm.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.OnOff)(byte)args[0] : new EventArg<EventEnum.OnOff> { index = p[0] },
                            p[1] == -1 ? (ushort)args[1] : new EventArg<ushort> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] },
                            p[3] == -1 ? (EventEnum.SoundType)(int)args[3] : new EventArg<EventEnum.SoundType> { index = p[3] },
                            p[4] == -1 ? (int)args[4] : new EventArg<int> { index = p[4] } };
                        return new InstrWrapper.PlayBgm(eventArgs, instr.Layer);
                    }
                case InstrType.PlaySe:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.PlaySe.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.PlaySe.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.SoundType)(int)args[1] : new EventArg<EventEnum.SoundType> { index = p[1] },
                            p[2] == -1 ? (int)args[2] : new EventArg<int> { index = p[2] } };
                        return new InstrWrapper.PlaySe(eventArgs, instr.Layer);
                    }
                case InstrType.SetMapSoundState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetMapSoundState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetMapSoundState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.SetMapSoundState(eventArgs, instr.Layer);
                    }
                case InstrType.EnableBossMapSound:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EnableBossMapSound.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EnableBossMapSound.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.EnableBossMapSound(eventArgs, instr.Layer);
                    }
                case InstrType.NotifySoundDampeningOfDoorEvent:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.NotifySoundDampeningOfDoorEvent.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.NotifySoundDampeningOfDoorEvent.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DoorState)(int)args[1] : new EventArg<EventEnum.DoorState> { index = p[1] } };
                        return new InstrWrapper.NotifySoundDampeningOfDoorEvent(eventArgs, instr.Layer);
                    }
                case InstrType.EnableMapSoundWithFade:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EnableMapSoundWithFade.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EnableMapSoundWithFade.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] },
                            p[2] == -1 ? (float)args[2] : new EventArg<float> { index = p[2] } };
                        return new InstrWrapper.EnableMapSoundWithFade(eventArgs, instr.Layer);
                    }
                case InstrType.Unknown201007:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.Unknown201007.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.Unknown201007.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] } };
                        return new InstrWrapper.Unknown201007(eventArgs, instr.Layer);
                    }
                case InstrType.ActivateHit:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ActivateHit.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ActivateHit.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.ActivateHit(eventArgs, instr.Layer);
                    }
                case InstrType.ActivateHitBackreadMask:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ActivateHitBackreadMask.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ActivateHitBackreadMask.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.ActivateHitBackreadMask(eventArgs, instr.Layer);
                    }
                case InstrType.ActivateHitres:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ActivateHitres.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ActivateHitres.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.ActivateHitres(eventArgs, instr.Layer);
                    }
                case InstrType.ActivateHitAndCreateNavimesh:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ActivateHitAndCreateNavimesh.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ActivateHitAndCreateNavimesh.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.ActivateHitAndCreateNavimesh(eventArgs, instr.Layer);
                    }
                case InstrType.ActivateMapPart:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ActivateMapPart.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ActivateMapPart.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (int)args[0] : new EventArg<int> { index = p[0] },
                            p[1] == -1 ? (EventEnum.DisabledEnabled)(byte)args[1] : new EventArg<EventEnum.DisabledEnabled> { index = p[1] } };
                        return new InstrWrapper.ActivateMapPart(eventArgs, instr.Layer);
                    }
                case InstrType.SetAreaWelcomeMessageState:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.SetAreaWelcomeMessageState.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.SetAreaWelcomeMessageState.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.DisabledEnabled)(byte)args[0] : new EventArg<EventEnum.DisabledEnabled> { index = p[0] } };
                        return new InstrWrapper.SetAreaWelcomeMessageState(eventArgs, instr.Layer);
                    }
                case InstrType.CreatePlaylog:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.CreatePlaylog.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.CreatePlaylog.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] } };
                        return new InstrWrapper.CreatePlaylog(eventArgs, instr.Layer);
                    }
                case InstrType.StartTimeMeasurement:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.StartTimeMeasurement.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.StartTimeMeasurement.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] },
                            p[2] == -1 ? (EventEnum.DisabledEnabled)(byte)args[2] : new EventArg<EventEnum.DisabledEnabled> { index = p[2] } };
                        return new InstrWrapper.StartTimeMeasurement(eventArgs, instr.Layer);
                    }
                case InstrType.EndTimeMeasurement:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.EndTimeMeasurement.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.EndTimeMeasurement.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (uint)args[0] : new EventArg<uint> { index = p[0] } };
                        return new InstrWrapper.EndTimeMeasurement(eventArgs, instr.Layer);
                    }
                case InstrType.ParameterOutput:
                    {
                        List<object> args = instr.UnpackArgs(InstrWrapper.ParameterOutput.argTypes);
                        int[] p = Util.UnpackParams(parameters, index, InstrWrapper.ParameterOutput.argTypes);
                        AbstractEventArg[] eventArgs = {
                            p[0] == -1 ? (EventEnum.PlayerPlaylogParameter)(byte)args[0] : new EventArg<EventEnum.PlayerPlaylogParameter> { index = p[0] },
                            p[1] == -1 ? (uint)args[1] : new EventArg<uint> { index = p[1] },
                            p[2] == -1 ? (EventEnum.PlaylogMultiplayerType)(byte)args[2] : new EventArg<EventEnum.PlaylogMultiplayerType> { index = p[2] } };
                        return new InstrWrapper.ParameterOutput(eventArgs, instr.Layer);
                    }
                default:
                    throw new ArgumentException();
            }
        }
    }

    namespace InstrWrapper
    {
        class IfConditionGroup : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public IfConditionGroup(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfConditionGroup, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[1];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredConditionGroupState, targetConditionGroup }; }
        }
        class IfParameterComparison : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> leftHandSide;
            public EventArg<int> rightHandSide;
            public IfParameterComparison(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfParameterComparison, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                leftHandSide = (EventArg<int>)args[2];
                rightHandSide = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, comparisonType, leftHandSide, rightHandSide }; }
        }
        class IfElapsedSeconds : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<float> targetNumberOfSeconds;
            public IfElapsedSeconds(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfElapsedSeconds, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetNumberOfSeconds = (EventArg<float>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetNumberOfSeconds }; }
        }
        class IfElapsedFrames : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetNumberOfFrames;
            public IfElapsedFrames(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfElapsedFrames, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetNumberOfFrames = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetNumberOfFrames }; }
        }
        class IfRandomElapsedSeconds : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<float> targetSecondsMin;
            public EventArg<float> targetSecondsMax;
            public IfRandomElapsedSeconds(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfRandomElapsedSeconds, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetSecondsMin = (EventArg<float>)args[1];
                targetSecondsMax = (EventArg<float>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetSecondsMin, targetSecondsMax }; }
        }
        class IfRandomElapsedFrames : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetFramesMin;
            public EventArg<int> targetFramesMax;
            public IfRandomElapsedFrames(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfRandomElapsedFrames, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetFramesMin = (EventArg<int>)args[1];
                targetFramesMax = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetFramesMin, targetFramesMax }; }
        }
        class IfEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.OnOffChange> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> targetEventFlagId;
            public IfEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfEventFlag, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredFlagState = (EventArg<EventEnum.OnOffChange>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                targetEventFlagId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredFlagState, targetEventFlagType, targetEventFlagId }; }
        }
        class IfBatchEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.LogicalOperationType> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> startingTargetEventFlagId;
            public EventArg<int> endingTargetEventFlagId;
            public IfBatchEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfBatchEventFlags, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredFlagState = (EventArg<EventEnum.LogicalOperationType>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                startingTargetEventFlagId = (EventArg<int>)args[3];
                endingTargetEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId }; }
        }
        class IfInOutsideArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.InsideOutsideState> desiredState;
            public EventArg<int> targetEntityId;
            public EventArg<int> areaEntityId;
            public EventArg<int> numberOfTargetCharacters;
            public IfInOutsideArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfInOutsideArea, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredState = (EventArg<EventEnum.InsideOutsideState>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                areaEntityId = (EventArg<int>)args[3];
                numberOfTargetCharacters = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters }; }
        }
        class IfEntityInOutsideRadiusOfEntity : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.InsideOutsideState> desiredState;
            public EventArg<int> targetEntityAId;
            public EventArg<int> targetEntityBId;
            public EventArg<float> targetDistance;
            public EventArg<int> minimumNumberOfTargets;
            public IfEntityInOutsideRadiusOfEntity(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfEntityInOutsideRadiusOfEntity, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredState = (EventArg<EventEnum.InsideOutsideState>)args[1];
                targetEntityAId = (EventArg<int>)args[2];
                targetEntityBId = (EventArg<int>)args[3];
                targetDistance = (EventArg<float>)args[4];
                minimumNumberOfTargets = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredState, targetEntityAId, targetEntityBId, targetDistance, minimumNumberOfTargets }; }
        }
        class IfPlayerHasDoesnTHaveItem : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public EventArg<EventEnum.OwnershipState> desiredPossessionState;
            public IfPlayerHasDoesnTHaveItem(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerHasDoesnTHaveItem, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                itemType = (EventArg<EventEnum.ItemType>)args[1];
                itemId = (EventArg<int>)args[2];
                desiredPossessionState = (EventArg<EventEnum.OwnershipState>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, itemType, itemId, desiredPossessionState }; }
        }
        class IfActionButton : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.TargetEntityType> targetEntityType;
            public EventArg<int> targetEntityId;
            public EventArg<float> reactionAngleDeg;
            public EventArg<short> dummypolyId;
            public EventArg<float> reactionDistance;
            public EventArg<int> helpMessageId;
            public EventArg<EventEnum.ReactionType> reactionType;
            public EventArg<int> padId;
            public IfActionButton(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfActionButton, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityType = (EventArg<EventEnum.TargetEntityType>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                reactionAngleDeg = (EventArg<float>)args[3];
                dummypolyId = (EventArg<short>)args[4];
                reactionDistance = (EventArg<float>)args[5];
                helpMessageId = (EventArg<int>)args[6];
                reactionType = (EventArg<EventEnum.ReactionType>)args[7];
                padId = (EventArg<int>)args[8];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId }; }
        }
        class IfMultiplayerState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.MultiplayerState> desiredMultiplayerState;
            public IfMultiplayerState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfMultiplayerState, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredMultiplayerState = (EventArg<EventEnum.MultiplayerState>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredMultiplayerState }; }
        }
        class IfAllPlayersInOutsideArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.InsideOutsideState> desiredState;
            public EventArg<int> areaEntityId;
            public IfAllPlayersInOutsideArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfAllPlayersInOutsideArea, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredState = (EventArg<EventEnum.InsideOutsideState>)args[1];
                areaEntityId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredState, areaEntityId }; }
        }
        class IfPlayerInOutMap : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> shouldPlayerBeInside;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public IfPlayerInOutMap(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerInOutMap, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                shouldPlayerBeInside = (EventArg<bool>)args[1];
                areaId = (EventArg<byte>)args[2];
                blockId = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, shouldPlayerBeInside, areaId, blockId }; }
        }
        class IfMultiplayerEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<uint> multiplayerEventId;
            public IfMultiplayerEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfMultiplayerEvent, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                multiplayerEventId = (EventArg<uint>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, multiplayerEventId }; }
        }
        class IfCountEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> startingTargetEventFlagId;
            public EventArg<int> endingTargetEventFlagId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> countThreshold;
            public IfCountEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCountEventFlags, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[1];
                startingTargetEventFlagId = (EventArg<int>)args[2];
                endingTargetEventFlagId = (EventArg<int>)args[3];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[4];
                countThreshold = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId, comparisonType, countThreshold }; }
        }
        class IfWorldTendency : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.TendencyType> tendencyType;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> tendencyThreshold;
            public IfWorldTendency(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfWorldTendency, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                tendencyType = (EventArg<EventEnum.TendencyType>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                tendencyThreshold = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, tendencyType, comparisonType, tendencyThreshold }; }
        }
        class IfEventValue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> baseEventFlagId;
            public EventArg<byte> numberOfUsedFlagBits;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<uint> thresholdValue;
            public IfEventValue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfEventValue, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                baseEventFlagId = (EventArg<int>)args[1];
                numberOfUsedFlagBits = (EventArg<byte>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                thresholdValue = (EventArg<uint>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, baseEventFlagId, numberOfUsedFlagBits, comparisonType, thresholdValue }; }
        }
        class IfActionButtonBossFog : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.TargetEntityType> targetEntityType;
            public EventArg<int> targetEntityId;
            public EventArg<float> reactionAngleDeg;
            public EventArg<short> dummypolyId;
            public EventArg<float> reactionDistance;
            public EventArg<int> helpMessageId;
            public EventArg<EventEnum.ReactionType> reactionType;
            public EventArg<int> padId;
            public IfActionButtonBossFog(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfActionButtonBossFog, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityType = (EventArg<EventEnum.TargetEntityType>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                reactionAngleDeg = (EventArg<float>)args[3];
                dummypolyId = (EventArg<short>)args[4];
                reactionDistance = (EventArg<float>)args[5];
                helpMessageId = (EventArg<int>)args[6];
                reactionType = (EventArg<EventEnum.ReactionType>)args[7];
                padId = (EventArg<int>)args[8];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId }; }
        }
        class IfDroppedItemsInArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> areaEntityId;
            public IfDroppedItemsInArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfDroppedItemsInArea, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                areaEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, areaEntityId }; }
        }
        class IfDroppedItem : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public IfDroppedItem(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfDroppedItem, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                itemType = (EventArg<EventEnum.ItemType>)args[1];
                itemId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, itemType, itemId }; }
        }
        class IfPlayerHasDoesnTHaveItemIncludingBbox : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public EventArg<EventEnum.OwnershipState> desiredPossessionState;
            public IfPlayerHasDoesnTHaveItemIncludingBbox(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerHasDoesnTHaveItemIncludingBbox, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                itemType = (EventArg<EventEnum.ItemType>)args[1];
                itemId = (EventArg<int>)args[2];
                desiredPossessionState = (EventArg<EventEnum.OwnershipState>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, itemType, itemId, desiredPossessionState }; }
        }
        class IfGameCycle : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> completedGameCyclesThreshold;
            public IfGameCycle(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfGameCycle, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                completedGameCyclesThreshold = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, comparisonType, completedGameCyclesThreshold }; }
        }
        class IfActionButtonLineSegment : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.TargetEntityType> targetEntityType;
            public EventArg<int> targetEntityId;
            public EventArg<float> reactionAngleDeg;
            public EventArg<short> dummypolyId;
            public EventArg<float> reactionDistance;
            public EventArg<int> helpMessageId;
            public EventArg<EventEnum.ReactionType> reactionType;
            public EventArg<int> padId;
            public EventArg<int> lineSegmentDefinedByEntityId;
            public IfActionButtonLineSegment(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfActionButtonLineSegment, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityType = (EventArg<EventEnum.TargetEntityType>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                reactionAngleDeg = (EventArg<float>)args[3];
                dummypolyId = (EventArg<short>)args[4];
                reactionDistance = (EventArg<float>)args[5];
                helpMessageId = (EventArg<int>)args[6];
                reactionType = (EventArg<EventEnum.ReactionType>)args[7];
                padId = (EventArg<int>)args[8];
                lineSegmentDefinedByEntityId = (EventArg<int>)args[9];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId, lineSegmentDefinedByEntityId }; }
        }
        class IfActionButtonLineSegmentBossRoom : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.TargetEntityType> targetEntityType;
            public EventArg<int> targetEntityId;
            public EventArg<float> reactionAngleDeg;
            public EventArg<short> dummypolyId;
            public EventArg<float> reactionDistance;
            public EventArg<int> helpMessageId;
            public EventArg<EventEnum.ReactionType> reactionType;
            public EventArg<int> padId;
            public EventArg<int> lineSegmentDefinedByEntityId;
            public IfActionButtonLineSegmentBossRoom(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfActionButtonLineSegmentBossRoom, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityType = (EventArg<EventEnum.TargetEntityType>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                reactionAngleDeg = (EventArg<float>)args[3];
                dummypolyId = (EventArg<short>)args[4];
                reactionDistance = (EventArg<float>)args[5];
                helpMessageId = (EventArg<int>)args[6];
                reactionType = (EventArg<EventEnum.ReactionType>)args[7];
                padId = (EventArg<int>)args[8];
                lineSegmentDefinedByEntityId = (EventArg<int>)args[9];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId, lineSegmentDefinedByEntityId }; }
        }
        class IfCompareEventValues : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> leftSideBaseEventFlagId;
            public EventArg<byte> leftSideNumberOfUsedFlagBits;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> rightSideBaseEventFlagId;
            public EventArg<byte> rightSideNumberOfUsedFlagBits;
            public IfCompareEventValues(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCompareEventValues, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                leftSideBaseEventFlagId = (EventArg<int>)args[1];
                leftSideNumberOfUsedFlagBits = (EventArg<byte>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                rightSideBaseEventFlagId = (EventArg<int>)args[4];
                rightSideNumberOfUsedFlagBits = (EventArg<byte>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, leftSideBaseEventFlagId, leftSideNumberOfUsedFlagBits, comparisonType, rightSideBaseEventFlagId, rightSideNumberOfUsedFlagBits }; }
        }
        class IfOwnsDlc : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> ownsDlc;
            public IfOwnsDlc(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfOwnsDlc, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                ownsDlc = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, ownsDlc }; }
        }
        class IfOnlineMode : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> onlineMode;
            public IfOnlineMode(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfOnlineMode, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                onlineMode = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, onlineMode }; }
        }
        class IfDamageType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<int> attackerEntityId;
            public EventArg<EventEnum.DamageType> damageType;
            public IfDamageType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfDamageType, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                attackerEntityId = (EventArg<int>)args[2];
                damageType = (EventArg<EventEnum.DamageType>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, attackerEntityId, damageType }; }
        }
        class IfActionButtonInArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> actionButtonParameterId;
            public EventArg<int> startingPointAreaEntityId;
            public IfActionButtonInArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfActionButtonInArea, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                actionButtonParameterId = (EventArg<int>)args[1];
                startingPointAreaEntityId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, actionButtonParameterId, startingPointAreaEntityId }; }
        }
        class IfPlayerIsNotInOwnWorldExcludesArena : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> isNotIn;
            public IfPlayerIsNotInOwnWorldExcludesArena(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerIsNotInOwnWorldExcludesArena, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                isNotIn = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, isNotIn }; }
        }
        class IfMapCeremonyState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.UInt16 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.DisabledEnabled> desiredState;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<ushort> ceremonyId;
            public IfMapCeremonyState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfMapCeremonyState, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredState = (EventArg<EventEnum.DisabledEnabled>)args[1];
                areaId = (EventArg<byte>)args[2];
                blockId = (EventArg<byte>)args[3];
                ceremonyId = (EventArg<ushort>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredState, areaId, blockId, ceremonyId }; }
        }
        class IfMultiplayerNetworkPenalized : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public IfMultiplayerNetworkPenalized(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfMultiplayerNetworkPenalized, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup }; }
        }
        class IfPlayerGender : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.Gender> gender;
            public IfPlayerGender(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerGender, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                gender = (EventArg<EventEnum.Gender>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, gender }; }
        }
        class IfOngoingCutsceneFinished : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> cutsceneId;
            public IfOngoingCutsceneFinished(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfOngoingCutsceneFinished, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                cutsceneId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, cutsceneId }; }
        }
        class IfHollowArenaMatchReadyState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> ready;
            public IfHollowArenaMatchReadyState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfHollowArenaMatchReadyState, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                ready = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, ready }; }
        }
        class IfHollowArenaSoloResults : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.HollowArenaResult> result;
            public IfHollowArenaSoloResults(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfHollowArenaSoloResults, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                result = (EventArg<EventEnum.HollowArenaResult>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, result }; }
        }
        class IfHollowArenaSoloScoreComparison : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> targetScore;
            public EventArg<int> unknown;
            public IfHollowArenaSoloScoreComparison(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfHollowArenaSoloScoreComparison, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                targetScore = (EventArg<byte>)args[2];
                unknown = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, comparisonType, targetScore, unknown }; }
        }
        class IfHollowArenaTeamResults : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.HollowArenaResult> result;
            public IfHollowArenaTeamResults(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfHollowArenaTeamResults, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                result = (EventArg<EventEnum.HollowArenaResult>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, result }; }
        }
        class IfSteamDisconnected : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> disconnected;
            public IfSteamDisconnected(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfSteamDisconnected, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                disconnected = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, disconnected }; }
        }
        class IfFriendlyPhantomCountComparison : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> desiredComparisonState;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> targetCount;
            public IfFriendlyPhantomCountComparison(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfFriendlyPhantomCountComparison, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                desiredComparisonState = (EventArg<bool>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                targetCount = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, desiredComparisonState, comparisonType, targetCount }; }
        }
        class IfCharacterDeadAlive : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<EventEnum.DeathState> desiredLifeState;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterDeadAlive(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterDeadAlive, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                desiredLifeState = (EventArg<EventEnum.DeathState>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetCharacters = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, desiredLifeState, comparisonType, numberOfTargetCharacters }; }
        }
        class IfCharacterDamagedBy : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<int> attackerEntityId;
            public IfCharacterDamagedBy(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterDamagedBy, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                attackerEntityId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, attackerEntityId }; }
        }
        class IfCharacterHpRatio : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> targetHpRatio;
            public EventArg<EventEnum.ComparisonType> comparisonType2;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterHpRatio(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterHpRatio, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                targetHpRatio = (EventArg<float>)args[3];
                comparisonType2 = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetCharacters = (EventArg<float>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, targetHpRatio, comparisonType2, numberOfTargetCharacters }; }
        }
        class IfCharacterType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<EventEnum.TargetType> targetType;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterType, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                targetType = (EventArg<EventEnum.TargetType>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetCharacters = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, targetType, comparisonType, numberOfTargetCharacters }; }
        }
        class IfCharacterTargetedBy : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> aggressorEntityId;
            public EventArg<int> targetEntityId;
            public EventArg<bool> shouldHave;
            public IfCharacterTargetedBy(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterTargetedBy, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                aggressorEntityId = (EventArg<int>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                shouldHave = (EventArg<bool>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, aggressorEntityId, targetEntityId, shouldHave }; }
        }
        class IfCharacterHasSpeffect : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<int> speffectId;
            public EventArg<bool> shouldHave;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterHasSpeffect(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterHasSpeffect, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                speffectId = (EventArg<int>)args[2];
                shouldHave = (EventArg<bool>)args[3];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetCharacters = (EventArg<float>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters }; }
        }
        class IfNpcPartHp : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<int> npcPartId;
            public EventArg<int> hpThreshold;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public IfNpcPartHp(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfNpcPartHp, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                npcPartId = (EventArg<int>)args[2];
                hpThreshold = (EventArg<int>)args[3];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, npcPartId, hpThreshold, comparisonType }; }
        }
        class IfCharacterBackreadStatus : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<bool> isBackread;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterBackreadStatus(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterBackreadStatus, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                isBackread = (EventArg<bool>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetCharacters = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, isBackread, comparisonType, numberOfTargetCharacters }; }
        }
        class IfCharacterHasEventMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<int> targetEventMessageId;
            public EventArg<bool> shouldHave;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterHasEventMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterHasEventMessage, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                targetEventMessageId = (EventArg<int>)args[2];
                shouldHave = (EventArg<bool>)args[3];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetCharacters = (EventArg<float>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, targetEventMessageId, shouldHave, comparisonType, numberOfTargetCharacters }; }
        }
        class IfCharacterAiState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<EventEnum.AiStateType> aiState;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterAiState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterAiState, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                aiState = (EventArg<EventEnum.AiStateType>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetCharacters = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, aiState, comparisonType, numberOfTargetCharacters }; }
        }
        class IfPlayerUsingSkullLantern : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<bool> usingSkullLantern;
            public IfPlayerUsingSkullLantern(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerUsingSkullLantern, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                usingSkullLantern = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, usingSkullLantern }; }
        }
        class IfPlayerSClass : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ClassType> classType;
            public IfPlayerSClass(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerSClass, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                classType = (EventArg<EventEnum.ClassType>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, classType }; }
        }
        class IfPlayerSCovenant : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<byte> covenantIndex;
            public IfPlayerSCovenant(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerSCovenant, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                covenantIndex = (EventArg<byte>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, covenantIndex }; }
        }
        class IfPlayerSSoulLevel : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<uint> targetSoulLevel;
            public IfPlayerSSoulLevel(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerSSoulLevel, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                targetSoulLevel = (EventArg<uint>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, comparisonType, targetSoulLevel }; }
        }
        class IfCharacterHpValue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> targetHp;
            public EventArg<EventEnum.ComparisonType> comparisonType2;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterHpValue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterHpValue, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                targetHp = (EventArg<int>)args[3];
                comparisonType2 = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetCharacters = (EventArg<float>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, targetHp, comparisonType2, numberOfTargetCharacters }; }
        }
        class IfCharacterDrawgroupState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetCharacterEntityId;
            public EventArg<bool> insideDrawgroup;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterDrawgroupState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterDrawgroupState, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetCharacterEntityId = (EventArg<int>)args[1];
                insideDrawgroup = (EventArg<bool>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetCharacters = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetCharacterEntityId, insideDrawgroup, comparisonType, numberOfTargetCharacters }; }
        }
        class IfPlayerRemainingYoelLevelCount : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> targetLevelCount;
            public IfPlayerRemainingYoelLevelCount(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerRemainingYoelLevelCount, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                targetLevelCount = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, comparisonType, targetLevelCount }; }
        }
        class IfCharacterInvadeType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<byte> invadeTypeUnknown;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetCharacters;
            public IfCharacterInvadeType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterInvadeType, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                invadeTypeUnknown = (EventArg<byte>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetCharacters = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, invadeTypeUnknown, comparisonType, numberOfTargetCharacters }; }
        }
        class IfCharacterChameleon : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetCharacterEntityId;
            public EventArg<int> chameleonSfxId;
            public EventArg<bool> isTransformed;
            public IfCharacterChameleon(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfCharacterChameleon, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetCharacterEntityId = (EventArg<int>)args[1];
                chameleonSfxId = (EventArg<int>)args[2];
                isTransformed = (EventArg<bool>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetCharacterEntityId, chameleonSfxId, isTransformed }; }
        }
        class IfObjectDestroyed : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<EventEnum.DestructionState> damageState;
            public EventArg<int> targetObjectEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetObjects;
            public IfObjectDestroyed(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfObjectDestroyed, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                damageState = (EventArg<EventEnum.DestructionState>)args[1];
                targetObjectEntityId = (EventArg<int>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetObjects = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects }; }
        }
        class IfObjectHitBy : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<int> attackerEntityId;
            public IfObjectHitBy(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfObjectHitBy, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                attackerEntityId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, attackerEntityId }; }
        }
        class IfObjactEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> objactEventFlag;
            public IfObjactEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfObjactEventFlag, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                objactEventFlag = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, objactEventFlag }; }
        }
        class IfObjectHp : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> hpThreshold;
            public IfObjectHp(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfObjectHp, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                hpThreshold = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, hpThreshold }; }
        }
        class IfObjectBurnState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> burnStateUnknown;
            public EventArg<EventEnum.ComparisonType> comparisonType2;
            public EventArg<float> numberOfTargetObjects;
            public IfObjectBurnState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfObjectBurnState, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                burnStateUnknown = (EventArg<byte>)args[3];
                comparisonType2 = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetObjects = (EventArg<float>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, burnStateUnknown, comparisonType2, numberOfTargetObjects }; }
        }
        class IfObjectBackread : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<bool> isBackread;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetObjects;
            public IfObjectBackread(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfObjectBackread, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                isBackread = (EventArg<bool>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetObjects = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, isBackread, comparisonType, numberOfTargetObjects }; }
        }
        class IfObjectBackreadUnknown511 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> targetEntityId;
            public EventArg<bool> isBackread;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetObjects;
            public IfObjectBackreadUnknown511(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfObjectBackreadUnknown511, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                isBackread = (EventArg<bool>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetObjects = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, targetEntityId, isBackread, comparisonType, numberOfTargetObjects }; }
        }
        class IfPlayerMovingOnHit : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> hitEntityId;
            public IfPlayerMovingOnHit(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerMovingOnHit, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                hitEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, hitEntityId }; }
        }
        class IfPlayerAttackingOnHit : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> hitEntityId;
            public IfPlayerAttackingOnHit(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerAttackingOnHit, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                hitEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, hitEntityId }; }
        }
        class IfPlayerStandingOnHit : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ConditionGroup> resultConditionGroup;
            public EventArg<int> hitEntityId;
            public IfPlayerStandingOnHit(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IfPlayerStandingOnHit, layerMask)
            {
                _argTypes = argTypes;
                resultConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[0];
                hitEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { resultConditionGroup, hitEntityId }; }
        }
        class WaitForConditionGroupState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public WaitForConditionGroupState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitForConditionGroupState, layerMask)
            {
                _argTypes = argTypes;
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[0];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { desiredConditionGroupState, targetConditionGroup }; }
        }
        class SkipIfConditionGroupStateUncompiled : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public SkipIfConditionGroupStateUncompiled(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfConditionGroupStateUncompiled, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[1];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, desiredConditionGroupState, targetConditionGroup }; }
        }
        class EndIfConditionGroupStateUncompiled : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public EndIfConditionGroupStateUncompiled(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfConditionGroupStateUncompiled, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[1];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, desiredConditionGroupState, targetConditionGroup }; }
        }
        class SkipUnconditionally : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> numberOfSkippedLines;
            public SkipUnconditionally(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipUnconditionally, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines }; }
        }
        class EndUnconditionally : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EndUnconditionally(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndUnconditionally, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType }; }
        }
        class SkipIfComparison : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> leftHandSide;
            public EventArg<int> rightHandSide;
            public SkipIfComparison(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfComparison, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                leftHandSide = (EventArg<int>)args[2];
                rightHandSide = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, comparisonType, leftHandSide, rightHandSide }; }
        }
        class EndIfComparison : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> leftHandSide;
            public EventArg<int> rightHandSide;
            public EndIfComparison(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfComparison, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                leftHandSide = (EventArg<int>)args[2];
                rightHandSide = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, comparisonType, leftHandSide, rightHandSide }; }
        }
        class SkipIfConditionGroupStateCompiled : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public SkipIfConditionGroupStateCompiled(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfConditionGroupStateCompiled, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[1];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, desiredConditionGroupState, targetConditionGroup }; }
        }
        class EndIfConditionGroupStateCompiled : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public EndIfConditionGroupStateCompiled(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfConditionGroupStateCompiled, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[1];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, desiredConditionGroupState, targetConditionGroup }; }
        }
        class WaitForNetworkApproval : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Single };
            public EventArg<float> timeoutS;
            public WaitForNetworkApproval(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitForNetworkApproval, layerMask)
            {
                _argTypes = argTypes;
                timeoutS = (EventArg<float>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { timeoutS }; }
        }
        class GotoIfConditionGroupStateUncompiled : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public GotoIfConditionGroupStateUncompiled(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfConditionGroupStateUncompiled, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[1];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, desiredConditionGroupState, targetConditionGroup }; }
        }
        class GotoUnconditionally : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.Label> label;
            public GotoUnconditionally(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoUnconditionally, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label }; }
        }
        class GotoIfComparison : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> leftHandSide;
            public EventArg<int> rightHandSide;
            public GotoIfComparison(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfComparison, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                leftHandSide = (EventArg<int>)args[2];
                rightHandSide = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, comparisonType, leftHandSide, rightHandSide }; }
        }
        class GotoIfConditionGroupStateCompiled : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.ConditionState> desiredConditionGroupState;
            public EventArg<EventEnum.ConditionGroup> targetConditionGroup;
            public GotoIfConditionGroupStateCompiled(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfConditionGroupStateCompiled, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                desiredConditionGroupState = (EventArg<EventEnum.ConditionState>)args[1];
                targetConditionGroup = (EventArg<EventEnum.ConditionGroup>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, desiredConditionGroupState, targetConditionGroup }; }
        }
        class WaitFixedTimeSeconds : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Single };
            public EventArg<float> numberOfSeconds;
            public WaitFixedTimeSeconds(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitFixedTimeSeconds, layerMask)
            {
                _argTypes = argTypes;
                numberOfSeconds = (EventArg<float>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSeconds }; }
        }
        class WaitFixedTimeFrames : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> numberOfFrames;
            public WaitFixedTimeFrames(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitFixedTimeFrames, layerMask)
            {
                _argTypes = argTypes;
                numberOfFrames = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfFrames }; }
        }
        class WaitRandomTimeSeconds : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single };
            public EventArg<float> minNumberOfSeconds;
            public EventArg<float> maxNumberOfSeconds;
            public WaitRandomTimeSeconds(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitRandomTimeSeconds, layerMask)
            {
                _argTypes = argTypes;
                minNumberOfSeconds = (EventArg<float>)args[0];
                maxNumberOfSeconds = (EventArg<float>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { minNumberOfSeconds, maxNumberOfSeconds }; }
        }
        class WaitRandomTimeFrames : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> minNumberOfFrames;
            public EventArg<int> maxNumberOfFrames;
            public WaitRandomTimeFrames(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitRandomTimeFrames, layerMask)
            {
                _argTypes = argTypes;
                minNumberOfFrames = (EventArg<int>)args[0];
                maxNumberOfFrames = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { minNumberOfFrames, maxNumberOfFrames }; }
        }
        class WaitHollowArenaHalfTimeStayparamLookup : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.HollowArenaMatchType> matchType;
            public EventArg<bool> isSecondHalf;
            public WaitHollowArenaHalfTimeStayparamLookup(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitHollowArenaHalfTimeStayparamLookup, layerMask)
            {
                _argTypes = argTypes;
                matchType = (EventArg<EventEnum.HollowArenaMatchType>)args[0];
                isSecondHalf = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { matchType, isSecondHalf }; }
        }
        class WaitForEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.OnOffChange> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> targetEventFlagId;
            public WaitForEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitForEventFlag, layerMask)
            {
                _argTypes = argTypes;
                desiredFlagState = (EventArg<EventEnum.OnOffChange>)args[0];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[1];
                targetEventFlagId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { desiredFlagState, targetEventFlagType, targetEventFlagId }; }
        }
        class SkipIfEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.OnOff> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> targetEventFlagId;
            public SkipIfEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfEventFlag, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                desiredFlagState = (EventArg<EventEnum.OnOff>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                targetEventFlagId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, desiredFlagState, targetEventFlagType, targetEventFlagId }; }
        }
        class EndIfEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.OnOff> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> targetEventFlagId;
            public EndIfEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfEventFlag, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                desiredFlagState = (EventArg<EventEnum.OnOff>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                targetEventFlagId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, desiredFlagState, targetEventFlagType, targetEventFlagId }; }
        }
        class SkipIfBatchEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.LogicalOperationType> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> startingTargetEventFlagId;
            public EventArg<int> endingTargetEventFlagId;
            public SkipIfBatchEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfBatchEventFlags, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                desiredFlagState = (EventArg<EventEnum.LogicalOperationType>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                startingTargetEventFlagId = (EventArg<int>)args[3];
                endingTargetEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId }; }
        }
        class EndIfBatchEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.LogicalOperationType> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> startingTargetEventFlagId;
            public EventArg<int> endingTargetEventFlagId;
            public EndIfBatchEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfBatchEventFlags, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                desiredFlagState = (EventArg<EventEnum.LogicalOperationType>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                startingTargetEventFlagId = (EventArg<int>)args[3];
                endingTargetEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId }; }
        }
        class SkipIfMultiplayerState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.MultiplayerState> desiredMultiplayerState;
            public SkipIfMultiplayerState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfMultiplayerState, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                desiredMultiplayerState = (EventArg<EventEnum.MultiplayerState>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, desiredMultiplayerState }; }
        }
        class EndIfMultiplayerState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.MultiplayerState> desiredMultiplayerState;
            public EndIfMultiplayerState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfMultiplayerState, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                desiredMultiplayerState = (EventArg<EventEnum.MultiplayerState>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, desiredMultiplayerState }; }
        }
        class SkipIfPlayerInOutMap : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<bool> shouldPlayerBeInside;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public SkipIfPlayerInOutMap(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfPlayerInOutMap, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                shouldPlayerBeInside = (EventArg<bool>)args[1];
                areaId = (EventArg<byte>)args[2];
                blockId = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, shouldPlayerBeInside, areaId, blockId }; }
        }
        class EndIfPlayerInOutMap : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<bool> shouldPlayerBeInside;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EndIfPlayerInOutMap(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfPlayerInOutMap, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                shouldPlayerBeInside = (EventArg<bool>)args[1];
                areaId = (EventArg<byte>)args[2];
                blockId = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, shouldPlayerBeInside, areaId, blockId }; }
        }
        class SkipIfNumberOfCoOpClients : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> targetNumberOfClients;
            public SkipIfNumberOfCoOpClients(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfNumberOfCoOpClients, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                targetNumberOfClients = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, comparisonType, targetNumberOfClients }; }
        }
        class EndIfNumberOfCoOpClients : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> targetNumberOfClients;
            public EndIfNumberOfCoOpClients(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfNumberOfCoOpClients, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                targetNumberOfClients = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, comparisonType, targetNumberOfClients }; }
        }
        class GotoIfCharacterHasSpeffect : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.Label> label;
            public EventArg<int> targetEntityId;
            public EventArg<int> speffectId;
            public EventArg<bool> shouldHave;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> numberOfTargetCharacters;
            public GotoIfCharacterHasSpeffect(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfCharacterHasSpeffect, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                speffectId = (EventArg<int>)args[2];
                shouldHave = (EventArg<bool>)args[3];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetCharacters = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters }; }
        }
        class GotoIfPlayerIsNotInOwnWorldExcludesArena : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.Label> label;
            public EventArg<bool> isNotIn;
            public GotoIfPlayerIsNotInOwnWorldExcludesArena(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfPlayerIsNotInOwnWorldExcludesArena, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                isNotIn = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, isNotIn }; }
        }
        class EndIfPlayerIsNotInOwnWorldExcludesArena : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<bool> isNotIn;
            public EndIfPlayerIsNotInOwnWorldExcludesArena(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfPlayerIsNotInOwnWorldExcludesArena, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                isNotIn = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, isNotIn }; }
        }
        class SkipIfNumberOfClientsOfType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.ClientType> clientType;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> targetNumberOfClients;
            public SkipIfNumberOfClientsOfType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfNumberOfClientsOfType, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                clientType = (EventArg<EventEnum.ClientType>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                targetNumberOfClients = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, clientType, comparisonType, targetNumberOfClients }; }
        }
        class GotoIfNumberOfClientsOfType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.ClientType> clientType;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> targetNumberOfClients;
            public GotoIfNumberOfClientsOfType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfNumberOfClientsOfType, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                clientType = (EventArg<EventEnum.ClientType>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                targetNumberOfClients = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, clientType, comparisonType, targetNumberOfClients }; }
        }
        class EndIfNumberOfClientsOfType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.ClientType> clientType;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> targetNumberOfClients;
            public EndIfNumberOfClientsOfType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfNumberOfClientsOfType, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                clientType = (EventArg<EventEnum.ClientType>)args[1];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[2];
                targetNumberOfClients = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, clientType, comparisonType, targetNumberOfClients }; }
        }
        class GotoIfEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.OnOff> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> targetEventFlagId;
            public GotoIfEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfEventFlag, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                desiredFlagState = (EventArg<EventEnum.OnOff>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                targetEventFlagId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, desiredFlagState, targetEventFlagType, targetEventFlagId }; }
        }
        class GotoIfBatchEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.LogicalOperationType> desiredFlagState;
            public EventArg<EventEnum.TargetEventFlagType> targetEventFlagType;
            public EventArg<int> startingTargetEventFlagId;
            public EventArg<int> endingTargetEventFlagId;
            public GotoIfBatchEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfBatchEventFlags, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                desiredFlagState = (EventArg<EventEnum.LogicalOperationType>)args[1];
                targetEventFlagType = (EventArg<EventEnum.TargetEventFlagType>)args[2];
                startingTargetEventFlagId = (EventArg<int>)args[3];
                endingTargetEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId }; }
        }
        class GotoIfMultiplayerState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.MultiplayerState> desiredMultiplayerState;
            public GotoIfMultiplayerState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfMultiplayerState, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                desiredMultiplayerState = (EventArg<EventEnum.MultiplayerState>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, desiredMultiplayerState }; }
        }
        class GotoIfPlayerInOutMap : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.Label> label;
            public EventArg<bool> shouldPlayerBeInside;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public GotoIfPlayerInOutMap(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfPlayerInOutMap, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                shouldPlayerBeInside = (EventArg<bool>)args[1];
                areaId = (EventArg<byte>)args[2];
                blockId = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, shouldPlayerBeInside, areaId, blockId }; }
        }
        class GotoIfNumberOfCoOpClients : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<byte> targetNumberOfClients;
            public GotoIfNumberOfCoOpClients(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfNumberOfCoOpClients, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[1];
                targetNumberOfClients = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, comparisonType, targetNumberOfClients }; }
        }
        class EndIfCharacterHasSpeffect : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<int> targetEntityId;
            public EventArg<int> speffectId;
            public EventArg<bool> shouldHave;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> numberOfTargetCharacters;
            public EndIfCharacterHasSpeffect(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfCharacterHasSpeffect, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                speffectId = (EventArg<int>)args[2];
                shouldHave = (EventArg<bool>)args[3];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetCharacters = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters }; }
        }
        class SkipIfCharacterHasSpeffect : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<int> targetEntityId;
            public EventArg<int> speffectId;
            public EventArg<bool> shouldHave;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<int> numberOfTargetCharacters;
            public SkipIfCharacterHasSpeffect(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfCharacterHasSpeffect, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                targetEntityId = (EventArg<int>)args[1];
                speffectId = (EventArg<int>)args[2];
                shouldHave = (EventArg<bool>)args[3];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[4];
                numberOfTargetCharacters = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters }; }
        }
        class GotoIfPlayerInOutsideArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.InsideOutsideState> desiredState;
            public EventArg<int> targetEntityId;
            public EventArg<int> areaEntityId;
            public EventArg<int> numberOfTargetCharacters;
            public GotoIfPlayerInOutsideArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfPlayerInOutsideArea, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                desiredState = (EventArg<EventEnum.InsideOutsideState>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                areaEntityId = (EventArg<int>)args[3];
                numberOfTargetCharacters = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters }; }
        }
        class EndIfPlayerInOutsideArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.InsideOutsideState> desiredState;
            public EventArg<int> targetEntityId;
            public EventArg<int> areaEntityId;
            public EventArg<int> numberOfTargetCharacters;
            public EndIfPlayerInOutsideArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfPlayerInOutsideArea, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                desiredState = (EventArg<EventEnum.InsideOutsideState>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                areaEntityId = (EventArg<int>)args[3];
                numberOfTargetCharacters = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters }; }
        }
        class SkipIfPlayerInOutsideArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.InsideOutsideState> desiredState;
            public EventArg<int> targetEntityId;
            public EventArg<int> areaEntityId;
            public EventArg<int> numberOfTargetCharacters;
            public SkipIfPlayerInOutsideArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfPlayerInOutsideArea, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                desiredState = (EventArg<EventEnum.InsideOutsideState>)args[1];
                targetEntityId = (EventArg<int>)args[2];
                areaEntityId = (EventArg<int>)args[3];
                numberOfTargetCharacters = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters }; }
        }
        class GotoIfHollowArenaMatchType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.HollowArenaMatchType> matchType;
            public GotoIfHollowArenaMatchType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfHollowArenaMatchType, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                matchType = (EventArg<EventEnum.HollowArenaMatchType>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, matchType }; }
        }
        class WaitForObjectDestroyed : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.DestructionState> damageState;
            public EventArg<int> targetObjectEntityId;
            public WaitForObjectDestroyed(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WaitForObjectDestroyed, layerMask)
            {
                _argTypes = argTypes;
                damageState = (EventArg<EventEnum.DestructionState>)args[0];
                targetObjectEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { damageState, targetObjectEntityId }; }
        }
        class SkipIfObjectDestroyed : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<byte> numberOfSkippedLines;
            public EventArg<EventEnum.DestructionState> damageState;
            public EventArg<int> targetObjectEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetObjects;
            public SkipIfObjectDestroyed(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SkipIfObjectDestroyed, layerMask)
            {
                _argTypes = argTypes;
                numberOfSkippedLines = (EventArg<byte>)args[0];
                damageState = (EventArg<EventEnum.DestructionState>)args[1];
                targetObjectEntityId = (EventArg<int>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetObjects = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { numberOfSkippedLines, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects }; }
        }
        class EndIfObjectDestroyed : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.EventEndType> executionEndType;
            public EventArg<EventEnum.DestructionState> damageState;
            public EventArg<int> targetObjectEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetObjects;
            public EndIfObjectDestroyed(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndIfObjectDestroyed, layerMask)
            {
                _argTypes = argTypes;
                executionEndType = (EventArg<EventEnum.EventEndType>)args[0];
                damageState = (EventArg<EventEnum.DestructionState>)args[1];
                targetObjectEntityId = (EventArg<int>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetObjects = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { executionEndType, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects }; }
        }
        class GotoIfObjectDestroyed : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Single };
            public EventArg<EventEnum.Label> label;
            public EventArg<EventEnum.DestructionState> damageState;
            public EventArg<int> targetObjectEntityId;
            public EventArg<EventEnum.ComparisonType> comparisonType;
            public EventArg<float> numberOfTargetObjects;
            public GotoIfObjectDestroyed(AbstractEventArg[] args, uint? layerMask) : base(InstrType.GotoIfObjectDestroyed, layerMask)
            {
                _argTypes = argTypes;
                label = (EventArg<EventEnum.Label>)args[0];
                damageState = (EventArg<EventEnum.DestructionState>)args[1];
                targetObjectEntityId = (EventArg<int>)args[2];
                comparisonType = (EventArg<EventEnum.ComparisonType>)args[3];
                numberOfTargetObjects = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { label, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects }; }
        }
        class Label0 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label0(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label0, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label1 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label1(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label1, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label2 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label2(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label2, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label3 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label3(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label3, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label4 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label4(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label4, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label5 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label5(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label5, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label6 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label6(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label6, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label7 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label7(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label7, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label8 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label8(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label8, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label9 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label9(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label9, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label10 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label10(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label10, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label11 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label11(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label11, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label12 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label12(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label12, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label13 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label13(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label13, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label14 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label14(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label14, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label15 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label15(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label15, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label16 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label16(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label16, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label17 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label17(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label17, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label18 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label18(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label18, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label19 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label19(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label19, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class Label20 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public Label20(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Label20, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class InitializeEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<int> eventSlotId;
            public EventArg<uint> eventId;
            public EventArg<uint>[] parameters;
            public InitializeEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InitializeEvent, layerMask)
            {
                _argTypes = Enumerable.Repeat(EMEVD.Instruction.ArgType.UInt32, args.Length).ToArray();
                argTypes.CopyTo(_argTypes, 0);
                eventSlotId = (EventArg<int>)args[0];
                eventId = (EventArg<uint>)args[1];
                parameters = new EventArg<uint>[args.Length - 2];
                for (int i = 0; i < args.Length - 2; i++)
                {
                    parameters[i] = (EventArg<uint>)args[i + 2];
                }
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventSlotId, eventId }.Concat(parameters).ToArray(); }
        }
        class TerminateEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<int> eventSlotId;
            public EventArg<uint> eventId;
            public TerminateEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.TerminateEvent, layerMask)
            {
                _argTypes = argTypes;
                eventSlotId = (EventArg<int>)args[0];
                eventId = (EventArg<uint>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventSlotId, eventId }; }
        }
        class SetNetworkSyncState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetNetworkSyncState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNetworkSyncState, layerMask)
            {
                _argTypes = argTypes;
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disabledEnabled }; }
        }
        class ClearCompiledConditionGroupState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> dummy;
            public ClearCompiledConditionGroupState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ClearCompiledConditionGroupState, layerMask)
            {
                _argTypes = argTypes;
                dummy = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { dummy }; }
        }
        class PrefetchRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<uint> requestId;
            public PrefetchRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PrefetchRequest, layerMask)
            {
                _argTypes = argTypes;
                requestId = (EventArg<uint>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { requestId }; }
        }
        class SaveRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> dummy;
            public SaveRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SaveRequest, layerMask)
            {
                _argTypes = argTypes;
                dummy = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { dummy }; }
        }
        class InitializeCommonEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<uint> eventId;
            public EventArg<uint>[] parameters;
            public InitializeCommonEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InitializeCommonEvent, layerMask)
            {
                _argTypes = Enumerable.Repeat(EMEVD.Instruction.ArgType.UInt32, args.Length).ToArray();
                argTypes.CopyTo(_argTypes, 0);
                eventId = (EventArg<uint>)args[0];
                parameters = new EventArg<uint>[args.Length - 1];
                for (int i = 0; i < args.Length - 1; i++)
                {
                    parameters[i] = (EventArg<uint>)args[i + 1];
                }
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventId }.Concat(parameters).ToArray(); }
        }
        class PlayCutsceneToAll : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public PlayCutsceneToAll(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneToAll, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod }; }
        }
        class PlayCutsceneAndWarpAllPlayers : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<int> pointEntityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public PlayCutsceneAndWarpAllPlayers(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneAndWarpAllPlayers, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                pointEntityId = (EventArg<int>)args[2];
                areaId = (EventArg<byte>)args[3];
                blockId = (EventArg<byte>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId }; }
        }
        class PlayCutsceneToPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<int> playerEntityId;
            public PlayCutsceneToPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneToPlayer, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                playerEntityId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, playerEntityId }; }
        }
        class PlayCutsceneAndWarpPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<int> pointEntityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<int> playerEntityId;
            public PlayCutsceneAndWarpPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneAndWarpPlayer, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                pointEntityId = (EventArg<int>)args[2];
                areaId = (EventArg<byte>)args[3];
                blockId = (EventArg<byte>)args[4];
                playerEntityId = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId }; }
        }
        class PlayCutsceneAndRotatePlayerAroundVerticalAxis : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<float> yAxisXCoordM;
            public EventArg<float> yAxisZCoordM;
            public EventArg<int> rotationDeg;
            public EventArg<float> verticalTranslationM;
            public EventArg<int> playerEntityId;
            public PlayCutsceneAndRotatePlayerAroundVerticalAxis(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneAndRotatePlayerAroundVerticalAxis, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                yAxisXCoordM = (EventArg<float>)args[2];
                yAxisZCoordM = (EventArg<float>)args[3];
                rotationDeg = (EventArg<int>)args[4];
                verticalTranslationM = (EventArg<float>)args[5];
                playerEntityId = (EventArg<int>)args[6];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, yAxisXCoordM, yAxisZCoordM, rotationDeg, verticalTranslationM, playerEntityId }; }
        }
        class PlayCutsceneChangeTimePeriodAndWarpPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<int> pointEntityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<int> playerEntityId;
            public EventArg<byte> timePeriodId;
            public PlayCutsceneChangeTimePeriodAndWarpPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneChangeTimePeriodAndWarpPlayer, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                pointEntityId = (EventArg<int>)args[2];
                areaId = (EventArg<byte>)args[3];
                blockId = (EventArg<byte>)args[4];
                playerEntityId = (EventArg<int>)args[5];
                timePeriodId = (EventArg<byte>)args[6];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId, timePeriodId }; }
        }
        class PlayCutsceneAndChangeTimePeriod : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<int> playerEntityId;
            public EventArg<byte> timePeriodId;
            public PlayCutsceneAndChangeTimePeriod(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneAndChangeTimePeriod, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                playerEntityId = (EventArg<int>)args[2];
                timePeriodId = (EventArg<byte>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, playerEntityId, timePeriodId }; }
        }
        class DummyPlayCutsceneAndWarpPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> pointEntityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public DummyPlayCutsceneAndWarpPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DummyPlayCutsceneAndWarpPlayer, layerMask)
            {
                _argTypes = argTypes;
                pointEntityId = (EventArg<int>)args[0];
                areaId = (EventArg<byte>)args[1];
                blockId = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { pointEntityId, areaId, blockId }; }
        }
        class PlayCutsceneChangeMapCeremonyAndWarpPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<uint> ceremonyId;
            public EventArg<int> unknown;
            public EventArg<int> pointEntityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<int> playerEntityId;
            public PlayCutsceneChangeMapCeremonyAndWarpPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneChangeMapCeremonyAndWarpPlayer, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                ceremonyId = (EventArg<uint>)args[2];
                unknown = (EventArg<int>)args[3];
                pointEntityId = (EventArg<int>)args[4];
                areaId = (EventArg<byte>)args[5];
                blockId = (EventArg<byte>)args[6];
                playerEntityId = (EventArg<int>)args[7];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, ceremonyId, unknown, pointEntityId, areaId, blockId, playerEntityId }; }
        }
        class PlayCutsceneToPlayerAndChangeCurrentMapCeremony : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<uint> ceremonyId;
            public EventArg<int> unknown;
            public EventArg<int> playerEntityId;
            public PlayCutsceneToPlayerAndChangeCurrentMapCeremony(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneToPlayerAndChangeCurrentMapCeremony, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                ceremonyId = (EventArg<uint>)args[2];
                unknown = (EventArg<int>)args[3];
                playerEntityId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, ceremonyId, unknown, playerEntityId }; }
        }
        class PlayOngoingCutsceneAndWarpPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<int> pointEntityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<int> playerEntityId;
            public EventArg<byte> unknown;
            public EventArg<byte> unknown2;
            public PlayOngoingCutsceneAndWarpPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayOngoingCutsceneAndWarpPlayer, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                pointEntityId = (EventArg<int>)args[2];
                areaId = (EventArg<byte>)args[3];
                blockId = (EventArg<byte>)args[4];
                playerEntityId = (EventArg<int>)args[5];
                unknown = (EventArg<byte>)args[6];
                unknown2 = (EventArg<byte>)args[7];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId, unknown, unknown2 }; }
        }
        class PlayCutsceneAndWarpPlayerUnknown200212 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> cutsceneId;
            public EventArg<EventEnum.CutscenePlayMode> playbackMethod;
            public EventArg<int> pointEntityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<int> playerEntityId;
            public EventArg<int> pointEntityId2;
            public PlayCutsceneAndWarpPlayerUnknown200212(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayCutsceneAndWarpPlayerUnknown200212, layerMask)
            {
                _argTypes = argTypes;
                cutsceneId = (EventArg<int>)args[0];
                playbackMethod = (EventArg<EventEnum.CutscenePlayMode>)args[1];
                pointEntityId = (EventArg<int>)args[2];
                areaId = (EventArg<byte>)args[3];
                blockId = (EventArg<byte>)args[4];
                playerEntityId = (EventArg<int>)args[5];
                pointEntityId2 = (EventArg<int>)args[6];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId, pointEntityId2 }; }
        }
        class RequestAnimationPlayback : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> animationId;
            public EventArg<bool> shouldLoop;
            public EventArg<bool> shouldWaitForCompletion;
            public RequestAnimationPlayback(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RequestAnimationPlayback, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                animationId = (EventArg<int>)args[1];
                shouldLoop = (EventArg<bool>)args[2];
                shouldWaitForCompletion = (EventArg<bool>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, animationId, shouldLoop, shouldWaitForCompletion }; }
        }
        class SetEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> eventFlagId;
            public EventArg<EventEnum.OnOff> flagState;
            public SetEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetEventFlag, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
                flagState = (EventArg<EventEnum.OnOff>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId, flagState }; }
        }
        class DeActivateGenerator : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public DeActivateGenerator(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DeActivateGenerator, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class AwardItemLot : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> itemLotId;
            public AwardItemLot(AbstractEventArg[] args, uint? layerMask) : base(InstrType.AwardItemLot, layerMask)
            {
                _argTypes = argTypes;
                itemLotId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { itemLotId }; }
        }
        class ShootBullet : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> bulletTeamEntityId;
            public EventArg<int> bulletProducerEntityId;
            public EventArg<int> dummypolyId;
            public EventArg<int> behaviorId;
            public EventArg<int> firingAngleX;
            public EventArg<int> firingAngleY;
            public EventArg<int> firingAngleZ;
            public ShootBullet(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ShootBullet, layerMask)
            {
                _argTypes = argTypes;
                bulletTeamEntityId = (EventArg<int>)args[0];
                bulletProducerEntityId = (EventArg<int>)args[1];
                dummypolyId = (EventArg<int>)args[2];
                behaviorId = (EventArg<int>)args[3];
                firingAngleX = (EventArg<int>)args[4];
                firingAngleY = (EventArg<int>)args[5];
                firingAngleZ = (EventArg<int>)args[6];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { bulletTeamEntityId, bulletProducerEntityId, dummypolyId, behaviorId, firingAngleX, firingAngleY, firingAngleZ }; }
        }
        class DeActivateMapHit : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public DeActivateMapHit(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DeActivateMapHit, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetMapVisibility : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetMapVisibility(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetMapVisibility, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetEventState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> eventId;
            public EventArg<int> eventSlotId;
            public EventArg<EventEnum.EventEndType> eventState;
            public SetEventState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetEventState, layerMask)
            {
                _argTypes = argTypes;
                eventId = (EventArg<int>)args[0];
                eventSlotId = (EventArg<int>)args[1];
                eventState = (EventArg<EventEnum.EventEndType>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventId, eventSlotId, eventState }; }
        }
        class InvertEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> eventFlagId;
            public InvertEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InvertEventFlag, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId }; }
        }
        class SetEventNavimesh : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int16 };
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetEventNavimesh(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetEventNavimesh, layerMask)
            {
                _argTypes = argTypes;
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disabledEnabled }; }
        }
        class DisplayBossHealthBar : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public EventArg<int> entityId;
            public EventArg<short> slotNumber;
            public EventArg<int> nameId;
            public DisplayBossHealthBar(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayBossHealthBar, layerMask)
            {
                _argTypes = argTypes;
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[0];
                entityId = (EventArg<int>)args[1];
                slotNumber = (EventArg<short>)args[2];
                nameId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disabledEnabled, entityId, slotNumber, nameId }; }
        }
        class HandleBossDefeat : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public HandleBossDefeat(AbstractEventArg[] args, uint? layerMask) : base(InstrType.HandleBossDefeat, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class ModifyNavimeshConnectionBitflag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.NavimeshType> navimeshType;
            public EventArg<EventEnum.BitopType> typeBitOperation;
            public ModifyNavimeshConnectionBitflag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ModifyNavimeshConnectionBitflag, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                navimeshType = (EventArg<EventEnum.NavimeshType>)args[1];
                typeBitOperation = (EventArg<EventEnum.BitopType>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, navimeshType, typeBitOperation }; }
        }
        class WarpPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<int> initialAreaId;
            public WarpPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WarpPlayer, layerMask)
            {
                _argTypes = argTypes;
                areaId = (EventArg<byte>)args[0];
                blockId = (EventArg<byte>)args[1];
                initialAreaId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { areaId, blockId, initialAreaId }; }
        }
        class HandleMinibossDefeat : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public HandleMinibossDefeat(AbstractEventArg[] args, uint? layerMask) : base(InstrType.HandleMinibossDefeat, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class TriggerMultiplayerEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<uint> multiplayerEventId;
            public TriggerMultiplayerEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.TriggerMultiplayerEvent, layerMask)
            {
                _argTypes = argTypes;
                multiplayerEventId = (EventArg<uint>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { multiplayerEventId }; }
        }
        class RandomlySetEventFlagInRange : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<uint> eventFlagIdMin;
            public EventArg<uint> eventFlagIdMax;
            public EventArg<EventEnum.OnOff> flagState;
            public RandomlySetEventFlagInRange(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RandomlySetEventFlagInRange, layerMask)
            {
                _argTypes = argTypes;
                eventFlagIdMin = (EventArg<uint>)args[0];
                eventFlagIdMax = (EventArg<uint>)args[1];
                flagState = (EventArg<EventEnum.OnOff>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagIdMin, eventFlagIdMax, flagState }; }
        }
        class ForceAnimationPlayback : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> entityId;
            public EventArg<int> animationId;
            public EventArg<bool> shouldLoop;
            public EventArg<bool> shouldWaitForCompletion;
            public EventArg<bool> ignoreWaitForTransition;
            public EventArg<byte> unknown;
            public EventArg<float> unknown2;
            public ForceAnimationPlayback(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ForceAnimationPlayback, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                animationId = (EventArg<int>)args[1];
                shouldLoop = (EventArg<bool>)args[2];
                shouldWaitForCompletion = (EventArg<bool>)args[3];
                ignoreWaitForTransition = (EventArg<bool>)args[4];
                unknown = (EventArg<byte>)args[5];
                unknown2 = (EventArg<float>)args[6];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, animationId, shouldLoop, shouldWaitForCompletion, ignoreWaitForTransition, unknown, unknown2 }; }
        }
        class SetAreaDrawparamBankSlotIndex : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16 };
            public EventArg<short> areaId;
            public EventArg<short> areaDrawparamBankSlotIndex;
            public SetAreaDrawparamBankSlotIndex(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetAreaDrawparamBankSlotIndex, layerMask)
            {
                _argTypes = argTypes;
                areaId = (EventArg<short>)args[0];
                areaDrawparamBankSlotIndex = (EventArg<short>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { areaId, areaDrawparamBankSlotIndex }; }
        }
        class SetTemporaryPlayerRespawnPoint : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> respawnPointEntityId;
            public SetTemporaryPlayerRespawnPoint(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetTemporaryPlayerRespawnPoint, layerMask)
            {
                _argTypes = argTypes;
                respawnPointEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { respawnPointEntityId }; }
        }
        class IncrementGameCycle : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> dummy;
            public IncrementGameCycle(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IncrementGameCycle, layerMask)
            {
                _argTypes = argTypes;
                dummy = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { dummy }; }
        }
        class BatchSetEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> eventFlagIdStart;
            public EventArg<int> eventFlagIdEnd;
            public EventArg<EventEnum.OnOff> flagState;
            public BatchSetEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BatchSetEventFlags, layerMask)
            {
                _argTypes = argTypes;
                eventFlagIdStart = (EventArg<int>)args[0];
                eventFlagIdEnd = (EventArg<int>)args[1];
                flagState = (EventArg<EventEnum.OnOff>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagIdStart, eventFlagIdEnd, flagState }; }
        }
        class SetPlayerRespawnPoint : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> respawnPointEntityId;
            public SetPlayerRespawnPoint(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetPlayerRespawnPoint, layerMask)
            {
                _argTypes = argTypes;
                respawnPointEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { respawnPointEntityId }; }
        }
        class RemoveItemFromPlayer : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public EventArg<int> number;
            public RemoveItemFromPlayer(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RemoveItemFromPlayer, layerMask)
            {
                _argTypes = argTypes;
                itemType = (EventArg<EventEnum.ItemType>)args[0];
                itemId = (EventArg<int>)args[1];
                number = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { itemType, itemId, number }; }
        }
        class PlaceNpcSummonSign : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.SummonSignType> signType;
            public EventArg<int> summonedNpcEntityId;
            public EventArg<int> spawnPointEntityId;
            public EventArg<int> summonEventFlagId;
            public EventArg<int> dismissalEventFlagId;
            public PlaceNpcSummonSign(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlaceNpcSummonSign, layerMask)
            {
                _argTypes = argTypes;
                signType = (EventArg<EventEnum.SummonSignType>)args[0];
                summonedNpcEntityId = (EventArg<int>)args[1];
                spawnPointEntityId = (EventArg<int>)args[2];
                summonEventFlagId = (EventArg<int>)args[3];
                dismissalEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { signType, summonedNpcEntityId, spawnPointEntityId, summonEventFlagId, dismissalEventFlagId }; }
        }
        class SetVisibilityOfMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> messageEntityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetVisibilityOfMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetVisibilityOfMessage, layerMask)
            {
                _argTypes = argTypes;
                messageEntityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageEntityId, disabledEnabled }; }
        }
        class Missing : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> dummy;
            public Missing(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Missing, layerMask)
            {
                _argTypes = argTypes;
                dummy = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { dummy }; }
        }
        class AwardAchievement : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> achievementId;
            public AwardAchievement(AbstractEventArg[] args, uint? layerMask) : base(InstrType.AwardAchievement, layerMask)
            {
                _argTypes = argTypes;
                achievementId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { achievementId }; }
        }
        class ChangeWorldTendency : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<EventEnum.TendencyType> tendencyType;
            public EventArg<sbyte> changeValue;
            public ChangeWorldTendency(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeWorldTendency, layerMask)
            {
                _argTypes = argTypes;
                tendencyType = (EventArg<EventEnum.TendencyType>)args[0];
                changeValue = (EventArg<sbyte>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { tendencyType, changeValue }; }
        }
        class DisableVagrantSpawning : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<bool> disabled;
            public DisableVagrantSpawning(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisableVagrantSpawning, layerMask)
            {
                _argTypes = argTypes;
                disabled = (EventArg<bool>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disabled }; }
        }
        class IncrementEventValue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<int> baseEventFlagId;
            public EventArg<uint> numberOfUsedFlagBits;
            public EventArg<uint> maximumAllowedValue;
            public IncrementEventValue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IncrementEventValue, layerMask)
            {
                _argTypes = argTypes;
                baseEventFlagId = (EventArg<int>)args[0];
                numberOfUsedFlagBits = (EventArg<uint>)args[1];
                maximumAllowedValue = (EventArg<uint>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { baseEventFlagId, numberOfUsedFlagBits, maximumAllowedValue }; }
        }
        class ClearEventValue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<int> baseEventFlagId;
            public EventArg<uint> numberOfUsedFlagBits;
            public ClearEventValue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ClearEventValue, layerMask)
            {
                _argTypes = argTypes;
                baseEventFlagId = (EventArg<int>)args[0];
                numberOfUsedFlagBits = (EventArg<uint>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { baseEventFlagId, numberOfUsedFlagBits }; }
        }
        class SetSnugglyNextTrade : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> eventFlagId;
            public SetSnugglyNextTrade(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetSnugglyNextTrade, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId }; }
        }
        class SpawnSnugglyItem : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> itemLotId;
            public EventArg<int> placementAreaEntityId;
            public EventArg<int> eventFlagId;
            public EventArg<int> hitEntityId;
            public SpawnSnugglyItem(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SpawnSnugglyItem, layerMask)
            {
                _argTypes = argTypes;
                itemLotId = (EventArg<int>)args[0];
                placementAreaEntityId = (EventArg<int>)args[1];
                eventFlagId = (EventArg<int>)args[2];
                hitEntityId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { itemLotId, placementAreaEntityId, eventFlagId, hitEntityId }; }
        }
        class MoveBloodstainAndDroppedItems : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> sourceAreaEntityId;
            public EventArg<int> destinationAreaEntityId;
            public MoveBloodstainAndDroppedItems(AbstractEventArg[] args, uint? layerMask) : base(InstrType.MoveBloodstainAndDroppedItems, layerMask)
            {
                _argTypes = argTypes;
                sourceAreaEntityId = (EventArg<int>)args[0];
                destinationAreaEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { sourceAreaEntityId, destinationAreaEntityId }; }
        }
        class AwardItemsIncludingClients : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> itemLotId;
            public AwardItemsIncludingClients(AbstractEventArg[] args, uint? layerMask) : base(InstrType.AwardItemsIncludingClients, layerMask)
            {
                _argTypes = argTypes;
                itemLotId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { itemLotId }; }
        }
        class BattleOfStoicism1v1RankingRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public BattleOfStoicism1v1RankingRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BattleOfStoicism1v1RankingRequest, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class BattleOfStoicism2v2RankingRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public BattleOfStoicism2v2RankingRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BattleOfStoicism2v2RankingRequest, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class BattleOfStoicismFfaRankingRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public BattleOfStoicismFfaRankingRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BattleOfStoicismFfaRankingRequest, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class BattleOfStoicismExitRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public BattleOfStoicismExitRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BattleOfStoicismExitRequest, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class EventValueOperation : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.SByte };
            public EventArg<int> baseEventFlagId;
            public EventArg<uint> numberOfUsedFlagBits;
            public EventArg<int> operand;
            public EventArg<int> baseEventFlagIdOperand;
            public EventArg<uint> numberOfUsedEventFlagBitsOperand;
            public EventArg<EventEnum.CalculationType> calculationType;
            public EventValueOperation(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EventValueOperation, layerMask)
            {
                _argTypes = argTypes;
                baseEventFlagId = (EventArg<int>)args[0];
                numberOfUsedFlagBits = (EventArg<uint>)args[1];
                operand = (EventArg<int>)args[2];
                baseEventFlagIdOperand = (EventArg<int>)args[3];
                numberOfUsedEventFlagBitsOperand = (EventArg<uint>)args[4];
                calculationType = (EventArg<EventEnum.CalculationType>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { baseEventFlagId, numberOfUsedFlagBits, operand, baseEventFlagIdOperand, numberOfUsedEventFlagBitsOperand, calculationType }; }
        }
        class StoreItemAmountHeldInEventValue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public EventArg<int> baseEventFlagId;
            public EventArg<uint> numberOfUsedFlagBits;
            public StoreItemAmountHeldInEventValue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.StoreItemAmountHeldInEventValue, layerMask)
            {
                _argTypes = argTypes;
                itemType = (EventArg<EventEnum.ItemType>)args[0];
                itemId = (EventArg<int>)args[1];
                baseEventFlagId = (EventArg<int>)args[2];
                numberOfUsedFlagBits = (EventArg<uint>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { itemType, itemId, baseEventFlagId, numberOfUsedFlagBits }; }
        }
        class DirectlyGivePlayerItem : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public EventArg<int> baseEventFlagId;
            public EventArg<uint> numberOfUsedFlagBits;
            public DirectlyGivePlayerItem(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DirectlyGivePlayerItem, layerMask)
            {
                _argTypes = argTypes;
                itemType = (EventArg<EventEnum.ItemType>)args[0];
                itemId = (EventArg<int>)args[1];
                baseEventFlagId = (EventArg<int>)args[2];
                numberOfUsedFlagBits = (EventArg<uint>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { itemType, itemId, baseEventFlagId, numberOfUsedFlagBits }; }
        }
        class ToggleDirectionDisplay : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public ToggleDirectionDisplay(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ToggleDirectionDisplay, layerMask)
            {
                _argTypes = argTypes;
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disabledEnabled }; }
        }
        class SetMapHitgridCorrespondence : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> hitEntityId;
            public EventArg<short> level;
            public EventArg<short> gridCoordX;
            public EventArg<short> gridCoordY;
            public EventArg<EventEnum.AddDelete> process;
            public SetMapHitgridCorrespondence(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetMapHitgridCorrespondence, layerMask)
            {
                _argTypes = argTypes;
                hitEntityId = (EventArg<int>)args[0];
                level = (EventArg<short>)args[1];
                gridCoordX = (EventArg<short>)args[2];
                gridCoordY = (EventArg<short>)args[3];
                process = (EventArg<EventEnum.AddDelete>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { hitEntityId, level, gridCoordX, gridCoordY, process }; }
        }
        class SetMapContentImageDisplay : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Byte };
            public EventArg<short> contentImagePartId;
            public EventArg<EventEnum.DisplayState> state;
            public SetMapContentImageDisplay(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetMapContentImageDisplay, layerMask)
            {
                _argTypes = argTypes;
                contentImagePartId = (EventArg<short>)args[0];
                state = (EventArg<EventEnum.DisplayState>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { contentImagePartId, state }; }
        }
        class SetMapBoundariesDisplay : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Byte };
            public EventArg<short> hierarchy;
            public EventArg<short> gridCoordX;
            public EventArg<short> gridCoordY;
            public EventArg<EventEnum.DisplayState> state;
            public SetMapBoundariesDisplay(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetMapBoundariesDisplay, layerMask)
            {
                _argTypes = argTypes;
                hierarchy = (EventArg<short>)args[0];
                gridCoordX = (EventArg<short>)args[1];
                gridCoordY = (EventArg<short>)args[2];
                state = (EventArg<EventEnum.DisplayState>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { hierarchy, gridCoordX, gridCoordY, state }; }
        }
        class SetAreaWind : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> areaEntityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public EventArg<float> changeTimeS;
            public EventArg<int> windParameterId;
            public SetAreaWind(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetAreaWind, layerMask)
            {
                _argTypes = argTypes;
                areaEntityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
                changeTimeS = (EventArg<float>)args[2];
                windParameterId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { areaEntityId, disabledEnabled, changeTimeS, windParameterId }; }
        }
        class WarpPlayerToRespawnPoint : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> respawnPointParameterId;
            public WarpPlayerToRespawnPoint(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WarpPlayerToRespawnPoint, layerMask)
            {
                _argTypes = argTypes;
                respawnPointParameterId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { respawnPointParameterId }; }
        }
        class StartEnemyGenerator : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> enemySpawnerId;
            public StartEnemyGenerator(AbstractEventArg[] args, uint? layerMask) : base(InstrType.StartEnemyGenerator, layerMask)
            {
                _argTypes = argTypes;
                enemySpawnerId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { enemySpawnerId }; }
        }
        class SummonNpc : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.SingleplayerSummonSignType> signType;
            public EventArg<int> summonedNpcEntityId;
            public EventArg<int> spawnPointEntityId;
            public EventArg<int> summonEventFlagId;
            public EventArg<int> dismissalEventFlagId;
            public SummonNpc(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SummonNpc, layerMask)
            {
                _argTypes = argTypes;
                signType = (EventArg<EventEnum.SingleplayerSummonSignType>)args[0];
                summonedNpcEntityId = (EventArg<int>)args[1];
                spawnPointEntityId = (EventArg<int>)args[2];
                summonEventFlagId = (EventArg<int>)args[3];
                dismissalEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { signType, summonedNpcEntityId, spawnPointEntityId, summonEventFlagId, dismissalEventFlagId }; }
        }
        class InitializeWarpObject : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> warpObjectEntityId;
            public InitializeWarpObject(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InitializeWarpObject, layerMask)
            {
                _argTypes = argTypes;
                warpObjectEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { warpObjectEntityId }; }
        }
        class MakeEnemyAppearEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public MakeEnemyAppearEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.MakeEnemyAppearEvent, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class SetCurrentMapCeremony : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<uint> ceremonyId;
            public SetCurrentMapCeremony(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCurrentMapCeremony, layerMask)
            {
                _argTypes = argTypes;
                ceremonyId = (EventArg<uint>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { ceremonyId }; }
        }
        class SetMapCeremony : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<uint> areaId;
            public EventArg<byte> blockId;
            public EventArg<uint> ceremonyId;
            public SetMapCeremony(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetMapCeremony, layerMask)
            {
                _argTypes = argTypes;
                areaId = (EventArg<uint>)args[0];
                blockId = (EventArg<byte>)args[1];
                ceremonyId = (EventArg<uint>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { areaId, blockId, ceremonyId }; }
        }
        class DisplayEpitaphMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> messageId;
            public DisplayEpitaphMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayEpitaphMessage, layerMask)
            {
                _argTypes = argTypes;
                messageId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageId }; }
        }
        class SetNetworkConnectedEventFlag : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> eventFlagId;
            public EventArg<EventEnum.OnOff> flagState;
            public SetNetworkConnectedEventFlag(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNetworkConnectedEventFlag, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
                flagState = (EventArg<EventEnum.OnOff>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId, flagState }; }
        }
        class BatchSetNetworkConnectedEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> eventFlagIdStart;
            public EventArg<int> eventFlagIdEnd;
            public EventArg<EventEnum.OnOff> flagState;
            public BatchSetNetworkConnectedEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BatchSetNetworkConnectedEventFlags, layerMask)
            {
                _argTypes = argTypes;
                eventFlagIdStart = (EventArg<int>)args[0];
                eventFlagIdEnd = (EventArg<int>)args[1];
                flagState = (EventArg<EventEnum.OnOff>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagIdStart, eventFlagIdEnd, flagState }; }
        }
        class SetOmissionModeCounts : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> level1Count;
            public EventArg<int> level2Count;
            public SetOmissionModeCounts(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetOmissionModeCounts, layerMask)
            {
                _argTypes = argTypes;
                level1Count = (EventArg<int>)args[0];
                level2Count = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { level1Count, level2Count }; }
        }
        class ResetOmissionModeCountsToDefault : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public ResetOmissionModeCountsToDefault(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ResetOmissionModeCountsToDefault, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class InitializeCrowTrade : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public EventArg<int> itemLotId;
            public EventArg<int> tradeCompletedEventFlagId;
            public EventArg<int> crowDialogueResponseEventFlagId;
            public InitializeCrowTrade(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InitializeCrowTrade, layerMask)
            {
                _argTypes = argTypes;
                itemType = (EventArg<EventEnum.ItemType>)args[0];
                itemId = (EventArg<int>)args[1];
                itemLotId = (EventArg<int>)args[2];
                tradeCompletedEventFlagId = (EventArg<int>)args[3];
                crowDialogueResponseEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { itemType, itemId, itemLotId, tradeCompletedEventFlagId, crowDialogueResponseEventFlagId }; }
        }
        class InitializeCrowTradeArea : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> areaEntityId;
            public InitializeCrowTradeArea(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InitializeCrowTradeArea, layerMask)
            {
                _argTypes = argTypes;
                areaEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { areaEntityId }; }
        }
        class SetNetworkInteractionState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetNetworkInteractionState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNetworkInteractionState, layerMask)
            {
                _argTypes = argTypes;
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disabledEnabled }; }
        }
        class HideHud : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<bool> shouldBeHidden;
            public HideHud(AbstractEventArg[] args, uint? layerMask) : base(InstrType.HideHud, layerMask)
            {
                _argTypes = argTypes;
                shouldBeHidden = (EventArg<bool>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { shouldBeHidden }; }
        }
        class SetBonfireWarpingState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> bonfireEntityId;
            public EventArg<int> animationId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetBonfireWarpingState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetBonfireWarpingState, layerMask)
            {
                _argTypes = argTypes;
                bonfireEntityId = (EventArg<int>)args[0];
                animationId = (EventArg<int>)args[1];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { bonfireEntityId, animationId, disabledEnabled }; }
        }
        class SetAutogeneratedEventSpecificEventFlag1Unknown200373 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> unknown;
            public EventArg<byte> unknown2;
            public SetAutogeneratedEventSpecificEventFlag1Unknown200373(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetAutogeneratedEventSpecificEventFlag1Unknown200373, layerMask)
            {
                _argTypes = argTypes;
                unknown = (EventArg<byte>)args[0];
                unknown2 = (EventArg<byte>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { unknown, unknown2 }; }
        }
        class HandleBossDefeatAndDisplayBanner : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.TextBannerType> bannerType;
            public HandleBossDefeatAndDisplayBanner(AbstractEventArg[] args, uint? layerMask) : base(InstrType.HandleBossDefeatAndDisplayBanner, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                bannerType = (EventArg<EventEnum.TextBannerType>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, bannerType }; }
        }
        class SetAutogeneratedEventSpecificEventFlag2Unknown200375 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> unknown;
            public EventArg<byte> unknown2;
            public SetAutogeneratedEventSpecificEventFlag2Unknown200375(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetAutogeneratedEventSpecificEventFlag2Unknown200375, layerMask)
            {
                _argTypes = argTypes;
                unknown = (EventArg<byte>)args[0];
                unknown2 = (EventArg<byte>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { unknown, unknown2 }; }
        }
        class DisableLoadingScreenTips : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<bool> shouldBeDisabled;
            public DisableLoadingScreenTips(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisableLoadingScreenTips, layerMask)
            {
                _argTypes = argTypes;
                shouldBeDisabled = (EventArg<bool>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { shouldBeDisabled }; }
        }
        class AwardGestureItem : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt16, EMEVD.Instruction.ArgType.UInt16, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<ushort> gestureIndex;
            public EventArg<EventEnum.ItemType> itemType;
            public EventArg<int> itemId;
            public AwardGestureItem(AbstractEventArg[] args, uint? layerMask) : base(InstrType.AwardGestureItem, layerMask)
            {
                _argTypes = argTypes;
                gestureIndex = (EventArg<ushort>)args[0];
                itemType = (EventArg<EventEnum.ItemType>)args[1];
                itemId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { gestureIndex, itemType, itemId }; }
        }
        class SendNpcSummonHome : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> summonEntityId;
            public SendNpcSummonHome(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SendNpcSummonHome, layerMask)
            {
                _argTypes = argTypes;
                summonEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { summonEntityId }; }
        }
        class Unknown200379 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> unknown;
            public Unknown200379(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Unknown200379, layerMask)
            {
                _argTypes = argTypes;
                unknown = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { unknown }; }
        }
        class DisplayDecoratedBossHealthBar : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.SByte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public EventArg<int> entityId;
            public EventArg<short> slotNumber;
            public EventArg<int> nameId;
            public EventArg<byte> decorationId;
            public DisplayDecoratedBossHealthBar(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayDecoratedBossHealthBar, layerMask)
            {
                _argTypes = argTypes;
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[0];
                entityId = (EventArg<int>)args[1];
                slotNumber = (EventArg<short>)args[2];
                nameId = (EventArg<int>)args[3];
                decorationId = (EventArg<byte>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disabledEnabled, entityId, slotNumber, nameId, decorationId }; }
        }
        class PlaceNpcSummonSignWorksForNonEmberedPlayers : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.SummonSignType> signType;
            public EventArg<int> summonedNpcEntityId;
            public EventArg<int> spawnPointEntityId;
            public EventArg<int> summonEventFlagId;
            public EventArg<int> dismissalEventFlagId;
            public PlaceNpcSummonSignWorksForNonEmberedPlayers(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlaceNpcSummonSignWorksForNonEmberedPlayers, layerMask)
            {
                _argTypes = argTypes;
                signType = (EventArg<EventEnum.SummonSignType>)args[0];
                summonedNpcEntityId = (EventArg<int>)args[1];
                spawnPointEntityId = (EventArg<int>)args[2];
                summonEventFlagId = (EventArg<int>)args[3];
                dismissalEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { signType, summonedNpcEntityId, spawnPointEntityId, summonEventFlagId, dismissalEventFlagId }; }
        }
        class SetCharacterAiState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetCharacterAiState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterAiState, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetCharacterTeamType : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.TeamType> teamType;
            public SetCharacterTeamType(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterTeamType, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                teamType = (EventArg<EventEnum.TeamType>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, teamType }; }
        }
        class CharacterWarpRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<EventEnum.TargetEntityType> warpType;
            public EventArg<int> warpDestinationEntityId;
            public EventArg<int> dummypolyId;
            public CharacterWarpRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.CharacterWarpRequest, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                warpType = (EventArg<EventEnum.TargetEntityType>)args[1];
                warpDestinationEntityId = (EventArg<int>)args[2];
                dummypolyId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId }; }
        }
        class ForceCharacterDeath : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<bool> shouldReceiveSouls;
            public ForceCharacterDeath(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ForceCharacterDeath, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                shouldReceiveSouls = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, shouldReceiveSouls }; }
        }
        class ChangeCharacterEnableState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public ChangeCharacterEnableState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCharacterEnableState, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class EzstateInstructionRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> command;
            public EventArg<byte> slot;
            public EzstateInstructionRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EzstateInstructionRequest, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                command = (EventArg<int>)args[1];
                slot = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, command, slot }; }
        }
        class CreateBulletOwner : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public CreateBulletOwner(AbstractEventArg[] args, uint? layerMask) : base(InstrType.CreateBulletOwner, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class SetSpeffect : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> speffectId;
            public SetSpeffect(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetSpeffect, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                speffectId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, speffectId }; }
        }
        class SetSpecialAnimationS : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> waitingAnimation;
            public EventArg<int> damageAnimation;
            public EventArg<int> cancelAnimation;
            public EventArg<int> deathAnimation;
            public EventArg<int> returnAnimation;
            public SetSpecialAnimationS(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetSpecialAnimationS, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                waitingAnimation = (EventArg<int>)args[1];
                damageAnimation = (EventArg<int>)args[2];
                cancelAnimation = (EventArg<int>)args[3];
                deathAnimation = (EventArg<int>)args[4];
                returnAnimation = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, waitingAnimation, damageAnimation, cancelAnimation, deathAnimation, returnAnimation }; }
        }
        class SetCharacterGravity : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetCharacterGravity(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterGravity, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetCharacterEventTarget : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> entityId2;
            public SetCharacterEventTarget(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterEventTarget, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                entityId2 = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, entityId2 }; }
        }
        class SetCharacterImmortality : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetCharacterImmortality(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterImmortality, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetCharacterHome : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> areaEntityId;
            public SetCharacterHome(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterHome, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                areaEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, areaEntityId }; }
        }
        class RotateCharacter : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> entityId2;
            public EventArg<int> playAnimationId;
            public EventArg<bool> shouldWaitForCompletion;
            public RotateCharacter(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RotateCharacter, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                entityId2 = (EventArg<int>)args[1];
                playAnimationId = (EventArg<int>)args[2];
                shouldWaitForCompletion = (EventArg<bool>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, entityId2, playAnimationId, shouldWaitForCompletion }; }
        }
        class SetCharacterInvincibility : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetCharacterInvincibility(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterInvincibility, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class ClearCharacterSAiTarget : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public ClearCharacterSAiTarget(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ClearCharacterSAiTarget, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class RequestCharacterAiCommand : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> commandId;
            public EventArg<byte> slotNumber;
            public RequestCharacterAiCommand(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RequestCharacterAiCommand, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                commandId = (EventArg<int>)args[1];
                slotNumber = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, commandId, slotNumber }; }
        }
        class SetEventPoint : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> entityId;
            public EventArg<int> entityId2;
            public EventArg<float> reactionDistance;
            public SetEventPoint(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetEventPoint, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                entityId2 = (EventArg<int>)args[1];
                reactionDistance = (EventArg<float>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, entityId2, reactionDistance }; }
        }
        class SetCharacterAiId : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> aiId;
            public SetCharacterAiId(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterAiId, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                aiId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, aiId }; }
        }
        class RequestCharacterAiRePlan : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public RequestCharacterAiRePlan(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RequestCharacterAiRePlan, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class ClearSpeffect : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> speffectId;
            public ClearSpeffect(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ClearSpeffect, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                speffectId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, speffectId }; }
        }
        class CreateNpcPart : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<short> npcPartId;
            public EventArg<EventEnum.NpcPartType> npcPartGroupIdx;
            public EventArg<int> npcPartHp;
            public EventArg<float> damageCorrection;
            public EventArg<float> bodyDamageCompensation;
            public EventArg<bool> isInvincible;
            public EventArg<bool> startInStoppedState;
            public CreateNpcPart(AbstractEventArg[] args, uint? layerMask) : base(InstrType.CreateNpcPart, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                npcPartId = (EventArg<short>)args[1];
                npcPartGroupIdx = (EventArg<EventEnum.NpcPartType>)args[2];
                npcPartHp = (EventArg<int>)args[3];
                damageCorrection = (EventArg<float>)args[4];
                bodyDamageCompensation = (EventArg<float>)args[5];
                isInvincible = (EventArg<bool>)args[6];
                startInStoppedState = (EventArg<bool>)args[7];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, npcPartId, npcPartGroupIdx, npcPartHp, damageCorrection, bodyDamageCompensation, isInvincible, startInStoppedState }; }
        }
        class SetNpcPartHp : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> npcPartId;
            public EventArg<int> desiredHp;
            public EventArg<bool> shouldAllowHpAboveMaxHp;
            public SetNpcPartHp(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNpcPartHp, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                npcPartId = (EventArg<int>)args[1];
                desiredHp = (EventArg<int>)args[2];
                shouldAllowHpAboveMaxHp = (EventArg<bool>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, npcPartId, desiredHp, shouldAllowHpAboveMaxHp }; }
        }
        class SetNpcPartSeAndSfx : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> npcPartId;
            public EventArg<int> defenseMaterialSeId;
            public EventArg<int> defenseMaterialSfxId;
            public SetNpcPartSeAndSfx(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNpcPartSeAndSfx, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                npcPartId = (EventArg<int>)args[1];
                defenseMaterialSeId = (EventArg<int>)args[2];
                defenseMaterialSfxId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, npcPartId, defenseMaterialSeId, defenseMaterialSfxId }; }
        }
        class SetNpcPartBulletDamageMultiplier : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> entityId;
            public EventArg<int> npcPartId;
            public EventArg<float> bulletDamageMultiplier;
            public SetNpcPartBulletDamageMultiplier(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNpcPartBulletDamageMultiplier, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                npcPartId = (EventArg<int>)args[1];
                bulletDamageMultiplier = (EventArg<float>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, npcPartId, bulletDamageMultiplier }; }
        }
        class ChangeCharacterDispmask : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<byte> bitNumber;
            public EventArg<EventEnum.OnOffChange> switchType;
            public ChangeCharacterDispmask(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCharacterDispmask, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                bitNumber = (EventArg<byte>)args[1];
                switchType = (EventArg<EventEnum.OnOffChange>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, bitNumber, switchType }; }
        }
        class ChangeCharacterHitmask : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<byte> bitNumber;
            public EventArg<EventEnum.OnOffChange> switchType;
            public ChangeCharacterHitmask(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCharacterHitmask, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                bitNumber = (EventArg<byte>)args[1];
                switchType = (EventArg<EventEnum.OnOffChange>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, bitNumber, switchType }; }
        }
        class SetNetworkUpdateAuthority : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<EventEnum.AuthorityLevel> authorityLevel;
            public SetNetworkUpdateAuthority(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNetworkUpdateAuthority, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                authorityLevel = (EventArg<EventEnum.AuthorityLevel>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, authorityLevel }; }
        }
        class SetCharacterBackreadState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<bool> removed;
            public SetCharacterBackreadState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterBackreadState, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                removed = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, removed }; }
        }
        class SetCharacterHpBarDisplay : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetCharacterHpBarDisplay(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterHpBarDisplay, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetCharacterMaphit : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<bool> disabled;
            public SetCharacterMaphit(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterMaphit, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabled = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabled }; }
        }
        class RequestCharacterAiCommandEventValue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> commandId;
            public EventArg<byte> slotNumber;
            public EventArg<int> startingTargetEventFlagId;
            public EventArg<int> endingTargetEventFlagId;
            public RequestCharacterAiCommandEventValue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RequestCharacterAiCommandEventValue, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                commandId = (EventArg<int>)args[1];
                slotNumber = (EventArg<byte>)args[2];
                startingTargetEventFlagId = (EventArg<int>)args[3];
                endingTargetEventFlagId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, commandId, slotNumber, startingTargetEventFlagId, endingTargetEventFlagId }; }
        }
        class CreateReferredDamagePair : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> sourceEntityId;
            public EventArg<int> targetEntityId;
            public CreateReferredDamagePair(AbstractEventArg[] args, uint? layerMask) : base(InstrType.CreateReferredDamagePair, layerMask)
            {
                _argTypes = argTypes;
                sourceEntityId = (EventArg<int>)args[0];
                targetEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { sourceEntityId, targetEntityId }; }
        }
        class SetNetworkUpdateRate : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.SByte };
            public EventArg<int> entityId;
            public EventArg<bool> useFixedFrequency;
            public EventArg<EventEnum.CharacterUpdateFrequency> updateFrequency;
            public SetNetworkUpdateRate(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetNetworkUpdateRate, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                useFixedFrequency = (EventArg<bool>)args[1];
                updateFrequency = (EventArg<EventEnum.CharacterUpdateFrequency>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, useFixedFrequency, updateFrequency }; }
        }
        class SetCharacterDefaultBackreadState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetCharacterDefaultBackreadState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterDefaultBackreadState, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class HellkiteBreathControl : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> characterEntityId;
            public EventArg<int> objectEntityId;
            public EventArg<int> characterAnimationId;
            public HellkiteBreathControl(AbstractEventArg[] args, uint? layerMask) : base(InstrType.HellkiteBreathControl, layerMask)
            {
                _argTypes = argTypes;
                characterEntityId = (EventArg<int>)args[0];
                objectEntityId = (EventArg<int>)args[1];
                characterAnimationId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { characterEntityId, objectEntityId, characterAnimationId }; }
        }
        class ForceCharacterTreasure : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> characterEntityId;
            public ForceCharacterTreasure(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ForceCharacterTreasure, layerMask)
            {
                _argTypes = argTypes;
                characterEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { characterEntityId }; }
        }
        class BetrayCharacterSCurrentCovenant : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> dummy;
            public BetrayCharacterSCurrentCovenant(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BetrayCharacterSCurrentCovenant, layerMask)
            {
                _argTypes = argTypes;
                dummy = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { dummy }; }
        }
        class SetCharacterAnimationState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetCharacterAnimationState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterAnimationState, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class WarpCharacterAndSetFloor : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<EventEnum.TargetEntityType> warpType;
            public EventArg<int> warpDestinationEntityId;
            public EventArg<int> dummypolyId;
            public EventArg<int> warpDestinationHitEntityId;
            public WarpCharacterAndSetFloor(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WarpCharacterAndSetFloor, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                warpType = (EventArg<EventEnum.TargetEntityType>)args[1];
                warpDestinationEntityId = (EventArg<int>)args[2];
                dummypolyId = (EventArg<int>)args[3];
                warpDestinationHitEntityId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId, warpDestinationHitEntityId }; }
        }
        class IssueShortWarpRequest : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<EventEnum.TargetEntityType> warpType;
            public EventArg<int> warpDestinationEntityId;
            public EventArg<int> dummypolyId;
            public IssueShortWarpRequest(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IssueShortWarpRequest, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                warpType = (EventArg<EventEnum.TargetEntityType>)args[1];
                warpDestinationEntityId = (EventArg<int>)args[2];
                dummypolyId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId }; }
        }
        class WarpCharacterAndCopyFloor : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<EventEnum.TargetEntityType> warpType;
            public EventArg<int> warpDestinationEntityId;
            public EventArg<int> dummypolyId;
            public EventArg<int> entityIdToCopyFloorFrom;
            public WarpCharacterAndCopyFloor(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WarpCharacterAndCopyFloor, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                warpType = (EventArg<EventEnum.TargetEntityType>)args[1];
                warpDestinationEntityId = (EventArg<int>)args[2];
                dummypolyId = (EventArg<int>)args[3];
                entityIdToCopyFloorFrom = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId, entityIdToCopyFloorFrom }; }
        }
        class RequestCharacterAnimationReset : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.Interpolation> interpolationMethod;
            public RequestCharacterAnimationReset(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RequestCharacterAnimationReset, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                interpolationMethod = (EventArg<EventEnum.Interpolation>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, interpolationMethod }; }
        }
        class ChangeCharacterTeamTypeAndForceExitSpecialAnimations : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.TeamType> teamType;
            public ChangeCharacterTeamTypeAndForceExitSpecialAnimations(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCharacterTeamTypeAndForceExitSpecialAnimations, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                teamType = (EventArg<EventEnum.TeamType>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, teamType }; }
        }
        class RegisterNpcHumanityEventValue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> npcEntityId;
            public EventArg<int> humanityEventValueBaseEventFlagId;
            public RegisterNpcHumanityEventValue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RegisterNpcHumanityEventValue, layerMask)
            {
                _argTypes = argTypes;
                npcEntityId = (EventArg<int>)args[0];
                humanityEventValueBaseEventFlagId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { npcEntityId, humanityEventValueBaseEventFlagId }; }
        }
        class IncrementPlayerPvpSin : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> dummy;
            public IncrementPlayerPvpSin(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IncrementPlayerPvpSin, layerMask)
            {
                _argTypes = argTypes;
                dummy = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { dummy }; }
        }
        class BonfireLikeRecovery : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public BonfireLikeRecovery(AbstractEventArg[] args, uint? layerMask) : base(InstrType.BonfireLikeRecovery, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class ChangeCharacterSCloth : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<byte> bitNumber;
            public EventArg<byte> stateId;
            public ChangeCharacterSCloth(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCharacterSCloth, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                bitNumber = (EventArg<byte>)args[1];
                stateId = (EventArg<byte>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, bitNumber, stateId }; }
        }
        class ChangeCharacterPatrolBehavior : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> characterEntityId;
            public EventArg<int> patrolInformationEntityId;
            public ChangeCharacterPatrolBehavior(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCharacterPatrolBehavior, layerMask)
            {
                _argTypes = argTypes;
                characterEntityId = (EventArg<int>)args[0];
                patrolInformationEntityId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { characterEntityId, patrolInformationEntityId }; }
        }
        class SetLockOnPoint : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> lockOnDummypolyId;
            public EventArg<EventEnum.DisabledEnabled> desiredState;
            public SetLockOnPoint(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetLockOnPoint, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                lockOnDummypolyId = (EventArg<int>)args[1];
                desiredState = (EventArg<EventEnum.DisabledEnabled>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, lockOnDummypolyId, desiredState }; }
        }
        class TestRequestRagdollRestraint : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityIdRecipientSide;
            public EventArg<int> targetRigidIndexRecipientSide;
            public EventArg<int> dummypolyIdRecipientSide;
            public EventArg<int> entityIdAttachmentSide;
            public EventArg<int> targetRigidIndexAttachmentSide;
            public EventArg<int> dummypolyIdAttachmentSide;
            public TestRequestRagdollRestraint(AbstractEventArg[] args, uint? layerMask) : base(InstrType.TestRequestRagdollRestraint, layerMask)
            {
                _argTypes = argTypes;
                entityIdRecipientSide = (EventArg<int>)args[0];
                targetRigidIndexRecipientSide = (EventArg<int>)args[1];
                dummypolyIdRecipientSide = (EventArg<int>)args[2];
                entityIdAttachmentSide = (EventArg<int>)args[3];
                targetRigidIndexAttachmentSide = (EventArg<int>)args[4];
                dummypolyIdAttachmentSide = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityIdRecipientSide, targetRigidIndexRecipientSide, dummypolyIdRecipientSide, entityIdAttachmentSide, targetRigidIndexAttachmentSide, dummypolyIdAttachmentSide }; }
        }
        class ChangeCharacter : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> charinitparamId;
            public ChangeCharacter(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCharacter, layerMask)
            {
                _argTypes = argTypes;
                charinitparamId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { charinitparamId }; }
        }
        class AdaptHpChangingSpeffectToNpcPartOfTarget : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public AdaptHpChangingSpeffectToNpcPartOfTarget(AbstractEventArg[] args, uint? layerMask) : base(InstrType.AdaptHpChangingSpeffectToNpcPartOfTarget, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class ImmediateActivateUnknown200454 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public ImmediateActivateUnknown200454(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ImmediateActivateUnknown200454, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetCharacterTalkRange : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> entityId;
            public EventArg<float> range;
            public SetCharacterTalkRange(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterTalkRange, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                range = (EventArg<float>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, range }; }
        }
        class IncrementCharacterGameCycle : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public IncrementCharacterGameCycle(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IncrementCharacterGameCycle, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class SetMultiplayerDependentBuffsNonBoss : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public SetMultiplayerDependentBuffsNonBoss(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetMultiplayerDependentBuffsNonBoss, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class Unknown200459 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> disabledEnabled;
            public Unknown200459(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Unknown200459, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                disabledEnabled = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, disabledEnabled }; }
        }
        class SetCharacterRemainingYoelLevels : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> levelCount;
            public SetCharacterRemainingYoelLevels(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCharacterRemainingYoelLevels, layerMask)
            {
                _argTypes = argTypes;
                levelCount = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { levelCount }; }
        }
        class RequestObjectDestruction : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte };
            public EventArg<int> entityId;
            public EventArg<sbyte> slotNumber;
            public RequestObjectDestruction(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RequestObjectDestruction, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                slotNumber = (EventArg<sbyte>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, slotNumber }; }
        }
        class RequestObjectRestoration : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public RequestObjectRestoration(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RequestObjectRestoration, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class DeActivateObject : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> targetObjectEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public DeActivateObject(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DeActivateObject, layerMask)
            {
                _argTypes = argTypes;
                targetObjectEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { targetObjectEntityId, state }; }
        }
        class SetObjectTreasureState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> targetObjectEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public SetObjectTreasureState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetObjectTreasureState, layerMask)
            {
                _argTypes = argTypes;
                targetObjectEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { targetObjectEntityId, state }; }
        }
        class InitializeObjact : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<int> objectParameterId;
            public EventArg<int> relativeTargetIdx;
            public InitializeObjact(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InitializeObjact, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                objectParameterId = (EventArg<int>)args[1];
                relativeTargetIdx = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, objectParameterId, relativeTargetIdx }; }
        }
        class SetObjactState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> objectParameterId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public SetObjactState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetObjactState, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                objectParameterId = (EventArg<int>)args[1];
                state = (EventArg<EventEnum.DisabledEnabled>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, objectParameterId, state }; }
        }
        class ReproduceObjectAnimation : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> targetObjectEntityId;
            public EventArg<int> animationId;
            public ReproduceObjectAnimation(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ReproduceObjectAnimation, layerMask)
            {
                _argTypes = argTypes;
                targetObjectEntityId = (EventArg<int>)args[0];
                animationId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { targetObjectEntityId, animationId }; }
        }
        class ReproduceObjectDestruction : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.SByte };
            public EventArg<int> entityId;
            public EventArg<sbyte> slotNumber;
            public ReproduceObjectDestruction(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ReproduceObjectDestruction, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                slotNumber = (EventArg<sbyte>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, slotNumber }; }
        }
        class CreateDamagingObject : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> eventFlagId;
            public EventArg<int> entityId;
            public EventArg<int> dummypolyId;
            public EventArg<int> behaviorId;
            public EventArg<EventEnum.DamageTargetType> target;
            public EventArg<float> radius;
            public EventArg<float> lifespan;
            public EventArg<float> repetitionTimeS;
            public CreateDamagingObject(AbstractEventArg[] args, uint? layerMask) : base(InstrType.CreateDamagingObject, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
                entityId = (EventArg<int>)args[1];
                dummypolyId = (EventArg<int>)args[2];
                behaviorId = (EventArg<int>)args[3];
                target = (EventArg<EventEnum.DamageTargetType>)args[4];
                radius = (EventArg<float>)args[5];
                lifespan = (EventArg<float>)args[6];
                repetitionTimeS = (EventArg<float>)args[7];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId, entityId, dummypolyId, behaviorId, target, radius, lifespan, repetitionTimeS }; }
        }
        class RegisterStatue : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<EventEnum.StatueType> statueType;
            public RegisterStatue(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RegisterStatue, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                areaId = (EventArg<byte>)args[1];
                blockId = (EventArg<byte>)args[2];
                statueType = (EventArg<EventEnum.StatueType>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, areaId, blockId, statueType }; }
        }
        class WarpObjectToCharacter : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16 };
            public EventArg<int> entityId;
            public EventArg<int> characterEntityId;
            public EventArg<short> dummypolyId;
            public WarpObjectToCharacter(AbstractEventArg[] args, uint? layerMask) : base(InstrType.WarpObjectToCharacter, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                characterEntityId = (EventArg<int>)args[1];
                dummypolyId = (EventArg<short>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, characterEntityId, dummypolyId }; }
        }
        class DeleteObjectEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> eventFlagId;
            public DeleteObjectEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DeleteObjectEvent, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId }; }
        }
        class SetObjectInvulnerability : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> targetObjectEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public SetObjectInvulnerability(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetObjectInvulnerability, layerMask)
            {
                _argTypes = argTypes;
                targetObjectEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { targetObjectEntityId, state }; }
        }
        class DeActivateObjactAssignIdx : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<int> objectParameterId;
            public EventArg<int> relativeTargetIdx;
            public EventArg<EventEnum.DisabledEnabled> state;
            public DeActivateObjactAssignIdx(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DeActivateObjactAssignIdx, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                objectParameterId = (EventArg<int>)args[1];
                relativeTargetIdx = (EventArg<int>)args[2];
                state = (EventArg<EventEnum.DisabledEnabled>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, objectParameterId, relativeTargetIdx, state }; }
        }
        class RerollObjectTreasure : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> targetObjectEntityId;
            public RerollObjectTreasure(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RerollObjectTreasure, layerMask)
            {
                _argTypes = argTypes;
                targetObjectEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { targetObjectEntityId }; }
        }
        class ExtinguishBurningObjects : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { };
            public ExtinguishBurningObjects(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ExtinguishBurningObjects, layerMask)
            {
                _argTypes = argTypes;
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { }; }
        }
        class ShowObjectByMapCeremony : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> targetObjectEntityId;
            public EventArg<uint> ceremonyId;
            public EventArg<int> unknown;
            public ShowObjectByMapCeremony(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ShowObjectByMapCeremony, layerMask)
            {
                _argTypes = argTypes;
                targetObjectEntityId = (EventArg<int>)args[0];
                ceremonyId = (EventArg<uint>)args[1];
                unknown = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { targetObjectEntityId, ceremonyId, unknown }; }
        }
        class DestroyObject : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> targetObjectEntityId;
            public DestroyObject(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DestroyObject, layerMask)
            {
                _argTypes = argTypes;
                targetObjectEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { targetObjectEntityId }; }
        }
        class DeleteMapSfx : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<bool> onlyDeleteRoot;
            public DeleteMapSfx(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DeleteMapSfx, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                onlyDeleteRoot = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, onlyDeleteRoot }; }
        }
        class SpawnMapSfx : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public SpawnMapSfx(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SpawnMapSfx, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class SpawnOneshotSfx : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.TargetEntityType> type;
            public EventArg<int> entityId;
            public EventArg<int> dummypolyId;
            public EventArg<int> sfxId;
            public SpawnOneshotSfx(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SpawnOneshotSfx, layerMask)
            {
                _argTypes = argTypes;
                type = (EventArg<EventEnum.TargetEntityType>)args[0];
                entityId = (EventArg<int>)args[1];
                dummypolyId = (EventArg<int>)args[2];
                sfxId = (EventArg<int>)args[3];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { type, entityId, dummypolyId, sfxId }; }
        }
        class CreateObjectFollowingSfx : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> objectEntityId;
            public EventArg<int> dummypolyId;
            public EventArg<int> sfxId;
            public CreateObjectFollowingSfx(AbstractEventArg[] args, uint? layerMask) : base(InstrType.CreateObjectFollowingSfx, layerMask)
            {
                _argTypes = argTypes;
                objectEntityId = (EventArg<int>)args[0];
                dummypolyId = (EventArg<int>)args[1];
                sfxId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { objectEntityId, dummypolyId, sfxId }; }
        }
        class DeleteObjectFollowingSfx : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> objectEntityId;
            public EventArg<bool> shouldDeleteRoot;
            public DeleteObjectFollowingSfx(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DeleteObjectFollowingSfx, layerMask)
            {
                _argTypes = argTypes;
                objectEntityId = (EventArg<int>)args[0];
                shouldDeleteRoot = (EventArg<bool>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { objectEntityId, shouldDeleteRoot }; }
        }
        class DisplayGenericDialog : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> messageId;
            public EventArg<EventEnum.PromptType> dialogType;
            public EventArg<EventEnum.NumberOfOptions> numberOfOptions;
            public EventArg<int> entityId;
            public EventArg<float> displayDistance;
            public DisplayGenericDialog(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayGenericDialog, layerMask)
            {
                _argTypes = argTypes;
                messageId = (EventArg<int>)args[0];
                dialogType = (EventArg<EventEnum.PromptType>)args[1];
                numberOfOptions = (EventArg<EventEnum.NumberOfOptions>)args[2];
                entityId = (EventArg<int>)args[3];
                displayDistance = (EventArg<float>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageId, dialogType, numberOfOptions, entityId, displayDistance }; }
        }
        class DisplayBanner : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.TextBannerType> bannerType;
            public DisplayBanner(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayBanner, layerMask)
            {
                _argTypes = argTypes;
                bannerType = (EventArg<EventEnum.TextBannerType>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { bannerType }; }
        }
        class DisplayStatusMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> messageId;
            public EventArg<EventEnum.DisabledEnabled> padState;
            public DisplayStatusMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayStatusMessage, layerMask)
            {
                _argTypes = argTypes;
                messageId = (EventArg<int>)args[0];
                padState = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageId, padState }; }
        }
        class DisplayMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> messageId;
            public EventArg<byte> screenLocationIndex;
            public DisplayMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayMessage, layerMask)
            {
                _argTypes = argTypes;
                messageId = (EventArg<int>)args[0];
                screenLocationIndex = (EventArg<byte>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageId, screenLocationIndex }; }
        }
        class SetBattleOfStocismNametagA : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> playerEntityId;
            public SetBattleOfStocismNametagA(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetBattleOfStocismNametagA, layerMask)
            {
                _argTypes = argTypes;
                playerEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { playerEntityId }; }
        }
        class SetBattleOfStocismNametagB : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> playerEntityId;
            public SetBattleOfStocismNametagB(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetBattleOfStocismNametagB, layerMask)
            {
                _argTypes = argTypes;
                playerEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { playerEntityId }; }
        }
        class SetBattleOfStocismNametagC : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> playerEntityId;
            public SetBattleOfStocismNametagC(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetBattleOfStocismNametagC, layerMask)
            {
                _argTypes = argTypes;
                playerEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { playerEntityId }; }
        }
        class SetBattleOfStocismNametagD : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> playerEntityId;
            public SetBattleOfStocismNametagD(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetBattleOfStocismNametagD, layerMask)
            {
                _argTypes = argTypes;
                playerEntityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { playerEntityId }; }
        }
        class DisplayBattleOfStocismDissolutionMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> messageId;
            public DisplayBattleOfStocismDissolutionMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayBattleOfStocismDissolutionMessage, layerMask)
            {
                _argTypes = argTypes;
                messageId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageId }; }
        }
        class DisplayGenericDialogAndSetEventFlags : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> messageId;
            public EventArg<EventEnum.PromptType> dialogType;
            public EventArg<EventEnum.NumberOfOptions> numberOfOptions;
            public EventArg<int> entityId;
            public EventArg<float> displayDistance;
            public EventArg<int> leftResponseEventFlagId;
            public EventArg<int> rightResponseEventFlagId;
            public EventArg<int> cancelResponseEventFlagId;
            public DisplayGenericDialogAndSetEventFlags(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayGenericDialogAndSetEventFlags, layerMask)
            {
                _argTypes = argTypes;
                messageId = (EventArg<int>)args[0];
                dialogType = (EventArg<EventEnum.PromptType>)args[1];
                numberOfOptions = (EventArg<EventEnum.NumberOfOptions>)args[2];
                entityId = (EventArg<int>)args[3];
                displayDistance = (EventArg<float>)args[4];
                leftResponseEventFlagId = (EventArg<int>)args[5];
                rightResponseEventFlagId = (EventArg<int>)args[6];
                cancelResponseEventFlagId = (EventArg<int>)args[7];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageId, dialogType, numberOfOptions, entityId, displayDistance, leftResponseEventFlagId, rightResponseEventFlagId, cancelResponseEventFlagId }; }
        }
        class DisplayAreaWelcomeMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> areaNameMessageId;
            public DisplayAreaWelcomeMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayAreaWelcomeMessage, layerMask)
            {
                _argTypes = argTypes;
                areaNameMessageId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { areaNameMessageId }; }
        }
        class DisplayHollowArenaPvpMessage : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int16, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> messageId;
            public EventArg<short> unknown;
            public EventArg<EventEnum.DisabledEnabled> padState;
            public DisplayHollowArenaPvpMessage(AbstractEventArg[] args, uint? layerMask) : base(InstrType.DisplayHollowArenaPvpMessage, layerMask)
            {
                _argTypes = argTypes;
                messageId = (EventArg<int>)args[0];
                unknown = (EventArg<short>)args[1];
                padState = (EventArg<EventEnum.DisabledEnabled>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { messageId, unknown, padState }; }
        }
        class ChangeCamera : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> normalCameraId;
            public EventArg<int> lockedCameraId;
            public ChangeCamera(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ChangeCamera, layerMask)
            {
                _argTypes = argTypes;
                normalCameraId = (EventArg<int>)args[0];
                lockedCameraId = (EventArg<int>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { normalCameraId, lockedCameraId }; }
        }
        class SetCameraVibration : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> vibrationId;
            public EventArg<EventEnum.TargetEntityType> type;
            public EventArg<int> entityId;
            public EventArg<int> dummypolyId;
            public EventArg<float> decayStartDistance;
            public EventArg<float> decayEndDistance;
            public SetCameraVibration(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetCameraVibration, layerMask)
            {
                _argTypes = argTypes;
                vibrationId = (EventArg<int>)args[0];
                type = (EventArg<EventEnum.TargetEntityType>)args[1];
                entityId = (EventArg<int>)args[2];
                dummypolyId = (EventArg<int>)args[3];
                decayStartDistance = (EventArg<float>)args[4];
                decayEndDistance = (EventArg<float>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { vibrationId, type, entityId, dummypolyId, decayStartDistance, decayEndDistance }; }
        }
        class SetLockcamSlotNumber : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.UInt16 };
            public EventArg<byte> areaId;
            public EventArg<byte> blockId;
            public EventArg<ushort> lockcamSlotNumber;
            public SetLockcamSlotNumber(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetLockcamSlotNumber, layerMask)
            {
                _argTypes = argTypes;
                areaId = (EventArg<byte>)args[0];
                blockId = (EventArg<byte>)args[1];
                lockcamSlotNumber = (EventArg<ushort>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { areaId, blockId, lockcamSlotNumber }; }
        }
        class RegisterLadder : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> disableTopEventFlagId;
            public EventArg<int> disableBottomEventFlagId;
            public EventArg<int> entityId;
            public RegisterLadder(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RegisterLadder, layerMask)
            {
                _argTypes = argTypes;
                disableTopEventFlagId = (EventArg<int>)args[0];
                disableBottomEventFlagId = (EventArg<int>)args[1];
                entityId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { disableTopEventFlagId, disableBottomEventFlagId, entityId }; }
        }
        class InitializeWanderingDemon : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> eventFlagId;
            public EventArg<int> entityId;
            public EventArg<int> appearenceEventFlagId;
            public InitializeWanderingDemon(AbstractEventArg[] args, uint? layerMask) : base(InstrType.InitializeWanderingDemon, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
                entityId = (EventArg<int>)args[1];
                appearenceEventFlagId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId, entityId, appearenceEventFlagId }; }
        }
        class RegisterWanderingDemon : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> eventFlagId;
            public EventArg<int> entityId;
            public EventArg<int> entityId2;
            public RegisterWanderingDemon(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RegisterWanderingDemon, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
                entityId = (EventArg<int>)args[1];
                entityId2 = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId, entityId, entityId2 }; }
        }
        class RegisterBonfire : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> eventFlagId;
            public EventArg<int> entityId;
            public EventArg<float> reactionDistance;
            public EventArg<float> reactionAngle;
            public EventArg<int> setStandardKindlingLevel;
            public RegisterBonfire(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RegisterBonfire, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
                entityId = (EventArg<int>)args[1];
                reactionDistance = (EventArg<float>)args[2];
                reactionAngle = (EventArg<float>)args[3];
                setStandardKindlingLevel = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId, entityId, reactionDistance, reactionAngle, setStandardKindlingLevel }; }
        }
        class ActivateMultiplayerDependantBuffs : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public ActivateMultiplayerDependantBuffs(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ActivateMultiplayerDependantBuffs, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class RegisterHealingFountain : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Single, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> eventFlagId;
            public EventArg<int> entityId;
            public EventArg<float> reactionDistance;
            public EventArg<float> reactionAngle;
            public EventArg<int> initialSwordNumber;
            public EventArg<int> swordLevel;
            public RegisterHealingFountain(AbstractEventArg[] args, uint? layerMask) : base(InstrType.RegisterHealingFountain, layerMask)
            {
                _argTypes = argTypes;
                eventFlagId = (EventArg<int>)args[0];
                entityId = (EventArg<int>)args[1];
                reactionDistance = (EventArg<float>)args[2];
                reactionAngle = (EventArg<float>)args[3];
                initialSwordNumber = (EventArg<int>)args[4];
                swordLevel = (EventArg<int>)args[5];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { eventFlagId, entityId, reactionDistance, reactionAngle, initialSwordNumber, swordLevel }; }
        }
        class IssueBossRoomEntryNotification : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<byte> dummy;
            public IssueBossRoomEntryNotification(AbstractEventArg[] args, uint? layerMask) : base(InstrType.IssueBossRoomEntryNotification, layerMask)
            {
                _argTypes = argTypes;
                dummy = (EventArg<byte>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { dummy }; }
        }
        class SendInvadingPhantomsHome : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> unknown;
            public SendInvadingPhantomsHome(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SendInvadingPhantomsHome, layerMask)
            {
                _argTypes = argTypes;
                unknown = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { unknown }; }
        }
        class SendAllPhantomsHomeAndUpdateServerPvpStats : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> unknown;
            public SendAllPhantomsHomeAndUpdateServerPvpStats(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SendAllPhantomsHomeAndUpdateServerPvpStats, layerMask)
            {
                _argTypes = argTypes;
                unknown = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { unknown }; }
        }
        class SendAllPhantomsHome : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> unknown;
            public SendAllPhantomsHome(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SendAllPhantomsHome, layerMask)
            {
                _argTypes = argTypes;
                unknown = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { unknown }; }
        }
        class PlayBgm : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.UInt16, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<EventEnum.OnOff> shouldPlay;
            public EventArg<ushort> slotNumber;
            public EventArg<int> entityId;
            public EventArg<EventEnum.SoundType> soundType;
            public EventArg<int> soundId;
            public PlayBgm(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlayBgm, layerMask)
            {
                _argTypes = argTypes;
                shouldPlay = (EventArg<EventEnum.OnOff>)args[0];
                slotNumber = (EventArg<ushort>)args[1];
                entityId = (EventArg<int>)args[2];
                soundType = (EventArg<EventEnum.SoundType>)args[3];
                soundId = (EventArg<int>)args[4];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { shouldPlay, slotNumber, entityId, soundType, soundId }; }
        }
        class PlaySe : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<EventEnum.SoundType> soundType;
            public EventArg<int> soundId;
            public PlaySe(AbstractEventArg[] args, uint? layerMask) : base(InstrType.PlaySe, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                soundType = (EventArg<EventEnum.SoundType>)args[1];
                soundId = (EventArg<int>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, soundType, soundId }; }
        }
        class SetMapSoundState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> enableDisable;
            public SetMapSoundState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetMapSoundState, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                enableDisable = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, enableDisable }; }
        }
        class EnableBossMapSound : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> enableDisable;
            public EnableBossMapSound(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EnableBossMapSound, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                enableDisable = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, enableDisable }; }
        }
        class NotifySoundDampeningOfDoorEvent : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DoorState> doorState;
            public NotifySoundDampeningOfDoorEvent(AbstractEventArg[] args, uint? layerMask) : base(InstrType.NotifySoundDampeningOfDoorEvent, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                doorState = (EventArg<EventEnum.DoorState>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, doorState }; }
        }
        class EnableMapSoundWithFade : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.Single };
            public EventArg<int> entityId;
            public EventArg<EventEnum.DisabledEnabled> enableDisable;
            public EventArg<float> fadeTime;
            public EnableMapSoundWithFade(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EnableMapSoundWithFade, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
                enableDisable = (EventArg<EventEnum.DisabledEnabled>)args[1];
                fadeTime = (EventArg<float>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId, enableDisable, fadeTime }; }
        }
        class Unknown201007 : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32 };
            public EventArg<int> entityId;
            public Unknown201007(AbstractEventArg[] args, uint? layerMask) : base(InstrType.Unknown201007, layerMask)
            {
                _argTypes = argTypes;
                entityId = (EventArg<int>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { entityId }; }
        }
        class ActivateHit : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> hitEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public ActivateHit(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ActivateHit, layerMask)
            {
                _argTypes = argTypes;
                hitEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { hitEntityId, state }; }
        }
        class ActivateHitBackreadMask : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> hitEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public ActivateHitBackreadMask(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ActivateHitBackreadMask, layerMask)
            {
                _argTypes = argTypes;
                hitEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { hitEntityId, state }; }
        }
        class ActivateHitres : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> hitEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public ActivateHitres(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ActivateHitres, layerMask)
            {
                _argTypes = argTypes;
                hitEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { hitEntityId, state }; }
        }
        class ActivateHitAndCreateNavimesh : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> hitEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public ActivateHitAndCreateNavimesh(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ActivateHitAndCreateNavimesh, layerMask)
            {
                _argTypes = argTypes;
                hitEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { hitEntityId, state }; }
        }
        class ActivateMapPart : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Int32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<int> mapPartEntityId;
            public EventArg<EventEnum.DisabledEnabled> state;
            public ActivateMapPart(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ActivateMapPart, layerMask)
            {
                _argTypes = argTypes;
                mapPartEntityId = (EventArg<int>)args[0];
                state = (EventArg<EventEnum.DisabledEnabled>)args[1];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { mapPartEntityId, state }; }
        }
        class SetAreaWelcomeMessageState : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.DisabledEnabled> state;
            public SetAreaWelcomeMessageState(AbstractEventArg[] args, uint? layerMask) : base(InstrType.SetAreaWelcomeMessageState, layerMask)
            {
                _argTypes = argTypes;
                state = (EventArg<EventEnum.DisabledEnabled>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { state }; }
        }
        class CreatePlaylog : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<uint> playlogName;
            public CreatePlaylog(AbstractEventArg[] args, uint? layerMask) : base(InstrType.CreatePlaylog, layerMask)
            {
                _argTypes = argTypes;
                playlogName = (EventArg<uint>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { playlogName }; }
        }
        class StartTimeMeasurement : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<uint> measurementId;
            public EventArg<uint> playlogName;
            public EventArg<EventEnum.DisabledEnabled> overwrite;
            public StartTimeMeasurement(AbstractEventArg[] args, uint? layerMask) : base(InstrType.StartTimeMeasurement, layerMask)
            {
                _argTypes = argTypes;
                measurementId = (EventArg<uint>)args[0];
                playlogName = (EventArg<uint>)args[1];
                overwrite = (EventArg<EventEnum.DisabledEnabled>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { measurementId, playlogName, overwrite }; }
        }
        class EndTimeMeasurement : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.UInt32 };
            public EventArg<uint> measurementId;
            public EndTimeMeasurement(AbstractEventArg[] args, uint? layerMask) : base(InstrType.EndTimeMeasurement, layerMask)
            {
                _argTypes = argTypes;
                measurementId = (EventArg<uint>)args[0];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { measurementId }; }
        }
        class ParameterOutput : MyInstr
        {
            public static EMEVD.Instruction.ArgType[] argTypes = new EMEVD.Instruction.ArgType[] { EMEVD.Instruction.ArgType.Byte, EMEVD.Instruction.ArgType.UInt32, EMEVD.Instruction.ArgType.Byte };
            public EventArg<EventEnum.PlayerPlaylogParameter> category;
            public EventArg<uint> playlogName;
            public EventArg<EventEnum.PlaylogMultiplayerType> outputMultiplayerStatus;
            public ParameterOutput(AbstractEventArg[] args, uint? layerMask) : base(InstrType.ParameterOutput, layerMask)
            {
                _argTypes = argTypes;
                category = (EventArg<EventEnum.PlayerPlaylogParameter>)args[0];
                playlogName = (EventArg<uint>)args[1];
                outputMultiplayerStatus = (EventArg<EventEnum.PlaylogMultiplayerType>)args[2];
            }
            public override AbstractEventArg[] gatherArgs() { return new AbstractEventArg[] { category, playlogName, outputMultiplayerStatus }; }
        }
    }

    ////////// Instruction functions (generated from DarkScript3's json) //////////
    partial class EventBuilder
    {
        public EventBuilder IfConditionGroup(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.IfConditionGroup(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfParameterComparison(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, comparisonType, leftHandSide, rightHandSide };
            Insert(new InstrWrapper.IfParameterComparison(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfElapsedSeconds(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<float> targetNumberOfSeconds,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetNumberOfSeconds };
            Insert(new InstrWrapper.IfElapsedSeconds(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfElapsedFrames(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetNumberOfFrames,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetNumberOfFrames };
            Insert(new InstrWrapper.IfElapsedFrames(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfRandomElapsedSeconds(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<float> targetSecondsMin,
            EventArg<float> targetSecondsMax,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetSecondsMin, targetSecondsMax };
            Insert(new InstrWrapper.IfRandomElapsedSeconds(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfRandomElapsedFrames(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetFramesMin,
            EventArg<int> targetFramesMax,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetFramesMin, targetFramesMax };
            Insert(new InstrWrapper.IfRandomElapsedFrames(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfEventFlag(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.OnOffChange> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredFlagState, targetEventFlagType, targetEventFlagId };
            Insert(new InstrWrapper.IfEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfBatchEventFlags(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId };
            Insert(new InstrWrapper.IfBatchEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfInOutsideArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfInOutsideArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfEntityInOutsideRadiusOfEntity(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityAId,
            EventArg<int> targetEntityBId,
            EventArg<float> targetDistance,
            EventArg<int> minimumNumberOfTargets,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredState, targetEntityAId, targetEntityBId, targetDistance, minimumNumberOfTargets };
            Insert(new InstrWrapper.IfEntityInOutsideRadiusOfEntity(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerHasDoesnTHaveItem(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<EventEnum.OwnershipState> desiredPossessionState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, itemType, itemId, desiredPossessionState };
            Insert(new InstrWrapper.IfPlayerHasDoesnTHaveItem(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<int> padId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId };
            Insert(new InstrWrapper.IfActionButton(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfMultiplayerState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredMultiplayerState };
            Insert(new InstrWrapper.IfMultiplayerState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfAllPlayersInOutsideArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> areaEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredState, areaEntityId };
            Insert(new InstrWrapper.IfAllPlayersInOutsideArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerInOutMap(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, shouldPlayerBeInside, areaId, blockId };
            Insert(new InstrWrapper.IfPlayerInOutMap(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfMultiplayerEvent(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<uint> multiplayerEventId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, multiplayerEventId };
            Insert(new InstrWrapper.IfMultiplayerEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCountEventFlags(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> countThreshold,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId, comparisonType, countThreshold };
            Insert(new InstrWrapper.IfCountEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfWorldTendency(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.TendencyType> tendencyType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> tendencyThreshold,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, tendencyType, comparisonType, tendencyThreshold };
            Insert(new InstrWrapper.IfWorldTendency(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfEventValue(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> baseEventFlagId,
            EventArg<byte> numberOfUsedFlagBits,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<uint> thresholdValue,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, baseEventFlagId, numberOfUsedFlagBits, comparisonType, thresholdValue };
            Insert(new InstrWrapper.IfEventValue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<int> padId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId };
            Insert(new InstrWrapper.IfActionButtonBossFog(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfDroppedItemsInArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> areaEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, areaEntityId };
            Insert(new InstrWrapper.IfDroppedItemsInArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfDroppedItem(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, itemType, itemId };
            Insert(new InstrWrapper.IfDroppedItem(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerHasDoesnTHaveItemIncludingBbox(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<EventEnum.OwnershipState> desiredPossessionState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, itemType, itemId, desiredPossessionState };
            Insert(new InstrWrapper.IfPlayerHasDoesnTHaveItemIncludingBbox(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfGameCycle(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> completedGameCyclesThreshold,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, comparisonType, completedGameCyclesThreshold };
            Insert(new InstrWrapper.IfGameCycle(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<int> lineSegmentDefinedByEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId, lineSegmentDefinedByEntityId };
            Insert(new InstrWrapper.IfActionButtonLineSegment(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<int> lineSegmentDefinedByEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityType, targetEntityId, reactionAngleDeg, dummypolyId, reactionDistance, helpMessageId, reactionType, padId, lineSegmentDefinedByEntityId };
            Insert(new InstrWrapper.IfActionButtonLineSegmentBossRoom(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCompareEventValues(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> leftSideBaseEventFlagId,
            EventArg<byte> leftSideNumberOfUsedFlagBits,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> rightSideBaseEventFlagId,
            EventArg<byte> rightSideNumberOfUsedFlagBits,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, leftSideBaseEventFlagId, leftSideNumberOfUsedFlagBits, comparisonType, rightSideBaseEventFlagId, rightSideNumberOfUsedFlagBits };
            Insert(new InstrWrapper.IfCompareEventValues(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfOwnsDlc(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> ownsDlc,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, ownsDlc };
            Insert(new InstrWrapper.IfOwnsDlc(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfOnlineMode(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> onlineMode,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, onlineMode };
            Insert(new InstrWrapper.IfOnlineMode(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfDamageType(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> attackerEntityId,
            EventArg<EventEnum.DamageType> damageType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, attackerEntityId, damageType };
            Insert(new InstrWrapper.IfDamageType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfActionButtonInArea(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> actionButtonParameterId,
            EventArg<int> startingPointAreaEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, actionButtonParameterId, startingPointAreaEntityId };
            Insert(new InstrWrapper.IfActionButtonInArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerIsNotInOwnWorldExcludesArena(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> isNotIn,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, isNotIn };
            Insert(new InstrWrapper.IfPlayerIsNotInOwnWorldExcludesArena(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfMapCeremonyState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.DisabledEnabled> desiredState,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<ushort> ceremonyId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredState, areaId, blockId, ceremonyId };
            Insert(new InstrWrapper.IfMapCeremonyState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfMultiplayerNetworkPenalized(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup };
            Insert(new InstrWrapper.IfMultiplayerNetworkPenalized(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerGender(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.Gender> gender,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, gender };
            Insert(new InstrWrapper.IfPlayerGender(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfOngoingCutsceneFinished(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> cutsceneId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, cutsceneId };
            Insert(new InstrWrapper.IfOngoingCutsceneFinished(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfHollowArenaMatchReadyState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> ready,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, ready };
            Insert(new InstrWrapper.IfHollowArenaMatchReadyState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfHollowArenaSoloResults(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.HollowArenaResult> result,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, result };
            Insert(new InstrWrapper.IfHollowArenaSoloResults(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfHollowArenaSoloScoreComparison(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetScore,
            EventArg<int> unknown,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, comparisonType, targetScore, unknown };
            Insert(new InstrWrapper.IfHollowArenaSoloScoreComparison(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfHollowArenaTeamResults(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.HollowArenaResult> result,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, result };
            Insert(new InstrWrapper.IfHollowArenaTeamResults(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfSteamDisconnected(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> disconnected,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, disconnected };
            Insert(new InstrWrapper.IfSteamDisconnected(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfFriendlyPhantomCountComparison(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> desiredComparisonState,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> targetCount,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, desiredComparisonState, comparisonType, targetCount };
            Insert(new InstrWrapper.IfFriendlyPhantomCountComparison(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterDeadAlive(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.DeathState> desiredLifeState,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, desiredLifeState, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterDeadAlive(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterDamagedBy(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> attackerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, attackerEntityId };
            Insert(new InstrWrapper.IfCharacterDamagedBy(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterHpRatio(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> targetHpRatio,
            EventArg<EventEnum.ComparisonType> comparisonType2,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, targetHpRatio, comparisonType2, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterHpRatio(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterType(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.TargetType> targetType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, targetType, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterTargetedBy(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> aggressorEntityId,
            EventArg<int> targetEntityId,
            EventArg<bool> shouldHave,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, aggressorEntityId, targetEntityId, shouldHave };
            Insert(new InstrWrapper.IfCharacterTargetedBy(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterHasSpeffect(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterHasSpeffect(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfNpcPartHp(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> npcPartId,
            EventArg<int> hpThreshold,
            EventArg<EventEnum.ComparisonType> comparisonType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, npcPartId, hpThreshold, comparisonType };
            Insert(new InstrWrapper.IfNpcPartHp(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterBackreadStatus(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<bool> isBackread,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, isBackread, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterBackreadStatus(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterHasEventMessage(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> targetEventMessageId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, targetEventMessageId, shouldHave, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterHasEventMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterAiState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.AiStateType> aiState,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, aiState, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterAiState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerUsingSkullLantern(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<bool> usingSkullLantern,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, usingSkullLantern };
            Insert(new InstrWrapper.IfPlayerUsingSkullLantern(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerSClass(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ClassType> classType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, classType };
            Insert(new InstrWrapper.IfPlayerSClass(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerSCovenant(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<byte> covenantIndex,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, covenantIndex };
            Insert(new InstrWrapper.IfPlayerSCovenant(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerSSoulLevel(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<uint> targetSoulLevel,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, comparisonType, targetSoulLevel };
            Insert(new InstrWrapper.IfPlayerSSoulLevel(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterHpValue(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> targetHp,
            EventArg<EventEnum.ComparisonType> comparisonType2,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, targetHp, comparisonType2, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterHpValue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterDrawgroupState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetCharacterEntityId,
            EventArg<bool> insideDrawgroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetCharacterEntityId, insideDrawgroup, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterDrawgroupState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerRemainingYoelLevelCount(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> targetLevelCount,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, comparisonType, targetLevelCount };
            Insert(new InstrWrapper.IfPlayerRemainingYoelLevelCount(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterInvadeType(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<byte> invadeTypeUnknown,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, invadeTypeUnknown, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.IfCharacterInvadeType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfCharacterChameleon(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetCharacterEntityId,
            EventArg<int> chameleonSfxId,
            EventArg<bool> isTransformed,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetCharacterEntityId, chameleonSfxId, isTransformed };
            Insert(new InstrWrapper.IfCharacterChameleon(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfObjectDestroyed(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects };
            Insert(new InstrWrapper.IfObjectDestroyed(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfObjectHitBy(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<int> attackerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, attackerEntityId };
            Insert(new InstrWrapper.IfObjectHitBy(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfObjactEventFlag(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> objactEventFlag,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, objactEventFlag };
            Insert(new InstrWrapper.IfObjactEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfObjectHp(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> hpThreshold,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, hpThreshold };
            Insert(new InstrWrapper.IfObjectHp(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfObjectBurnState(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> burnStateUnknown,
            EventArg<EventEnum.ComparisonType> comparisonType2,
            EventArg<float> numberOfTargetObjects,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, comparisonType, burnStateUnknown, comparisonType2, numberOfTargetObjects };
            Insert(new InstrWrapper.IfObjectBurnState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfObjectBackread(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<bool> isBackread,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, isBackread, comparisonType, numberOfTargetObjects };
            Insert(new InstrWrapper.IfObjectBackread(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfObjectBackreadUnknown511(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> targetEntityId,
            EventArg<bool> isBackread,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, targetEntityId, isBackread, comparisonType, numberOfTargetObjects };
            Insert(new InstrWrapper.IfObjectBackreadUnknown511(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerMovingOnHit(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> hitEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, hitEntityId };
            Insert(new InstrWrapper.IfPlayerMovingOnHit(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerAttackingOnHit(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> hitEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, hitEntityId };
            Insert(new InstrWrapper.IfPlayerAttackingOnHit(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IfPlayerStandingOnHit(
            EventArg<EventEnum.ConditionGroup> resultConditionGroup,
            EventArg<int> hitEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { resultConditionGroup, hitEntityId };
            Insert(new InstrWrapper.IfPlayerStandingOnHit(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitForConditionGroupState(
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.WaitForConditionGroupState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfConditionGroupStateUncompiled(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.SkipIfConditionGroupStateUncompiled(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfConditionGroupStateUncompiled(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.EndIfConditionGroupStateUncompiled(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipUnconditionally(
            EventArg<byte> numberOfSkippedLines,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines };
            Insert(new InstrWrapper.SkipUnconditionally(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndUnconditionally(
            EventArg<EventEnum.EventEndType> executionEndType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType };
            Insert(new InstrWrapper.EndUnconditionally(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfComparison(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, comparisonType, leftHandSide, rightHandSide };
            Insert(new InstrWrapper.SkipIfComparison(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfComparison(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, comparisonType, leftHandSide, rightHandSide };
            Insert(new InstrWrapper.EndIfComparison(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfConditionGroupStateCompiled(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.SkipIfConditionGroupStateCompiled(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfConditionGroupStateCompiled(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.EndIfConditionGroupStateCompiled(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitForNetworkApproval(
            EventArg<float> timeoutS,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { timeoutS };
            Insert(new InstrWrapper.WaitForNetworkApproval(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfConditionGroupStateUncompiled(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.GotoIfConditionGroupStateUncompiled(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoUnconditionally(
            EventArg<EventEnum.Label> label,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label };
            Insert(new InstrWrapper.GotoUnconditionally(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfComparison(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> leftHandSide,
            EventArg<int> rightHandSide,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, comparisonType, leftHandSide, rightHandSide };
            Insert(new InstrWrapper.GotoIfComparison(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfConditionGroupStateCompiled(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ConditionState> desiredConditionGroupState,
            EventArg<EventEnum.ConditionGroup> targetConditionGroup,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, desiredConditionGroupState, targetConditionGroup };
            Insert(new InstrWrapper.GotoIfConditionGroupStateCompiled(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitFixedTimeSeconds(
            EventArg<float> numberOfSeconds,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSeconds };
            Insert(new InstrWrapper.WaitFixedTimeSeconds(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitFixedTimeFrames(
            EventArg<int> numberOfFrames,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfFrames };
            Insert(new InstrWrapper.WaitFixedTimeFrames(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitRandomTimeSeconds(
            EventArg<float> minNumberOfSeconds,
            EventArg<float> maxNumberOfSeconds,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { minNumberOfSeconds, maxNumberOfSeconds };
            Insert(new InstrWrapper.WaitRandomTimeSeconds(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitRandomTimeFrames(
            EventArg<int> minNumberOfFrames,
            EventArg<int> maxNumberOfFrames,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { minNumberOfFrames, maxNumberOfFrames };
            Insert(new InstrWrapper.WaitRandomTimeFrames(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitHollowArenaHalfTimeStayparamLookup(
            EventArg<EventEnum.HollowArenaMatchType> matchType,
            EventArg<bool> isSecondHalf,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { matchType, isSecondHalf };
            Insert(new InstrWrapper.WaitHollowArenaHalfTimeStayparamLookup(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitForEventFlag(
            EventArg<EventEnum.OnOffChange> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { desiredFlagState, targetEventFlagType, targetEventFlagId };
            Insert(new InstrWrapper.WaitForEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfEventFlag(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.OnOff> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, desiredFlagState, targetEventFlagType, targetEventFlagId };
            Insert(new InstrWrapper.SkipIfEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfEventFlag(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.OnOff> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, desiredFlagState, targetEventFlagType, targetEventFlagId };
            Insert(new InstrWrapper.EndIfEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfBatchEventFlags(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId };
            Insert(new InstrWrapper.SkipIfBatchEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfBatchEventFlags(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId };
            Insert(new InstrWrapper.EndIfBatchEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfMultiplayerState(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, desiredMultiplayerState };
            Insert(new InstrWrapper.SkipIfMultiplayerState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfMultiplayerState(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, desiredMultiplayerState };
            Insert(new InstrWrapper.EndIfMultiplayerState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfPlayerInOutMap(
            EventArg<byte> numberOfSkippedLines,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, shouldPlayerBeInside, areaId, blockId };
            Insert(new InstrWrapper.SkipIfPlayerInOutMap(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfPlayerInOutMap(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, shouldPlayerBeInside, areaId, blockId };
            Insert(new InstrWrapper.EndIfPlayerInOutMap(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfNumberOfCoOpClients(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, comparisonType, targetNumberOfClients };
            Insert(new InstrWrapper.SkipIfNumberOfCoOpClients(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfNumberOfCoOpClients(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, comparisonType, targetNumberOfClients };
            Insert(new InstrWrapper.EndIfNumberOfCoOpClients(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfCharacterHasSpeffect(
            EventArg<EventEnum.Label> label,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.GotoIfCharacterHasSpeffect(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfPlayerIsNotInOwnWorldExcludesArena(
            EventArg<EventEnum.Label> label,
            EventArg<bool> isNotIn,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, isNotIn };
            Insert(new InstrWrapper.GotoIfPlayerIsNotInOwnWorldExcludesArena(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfPlayerIsNotInOwnWorldExcludesArena(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<bool> isNotIn,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, isNotIn };
            Insert(new InstrWrapper.EndIfPlayerIsNotInOwnWorldExcludesArena(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfNumberOfClientsOfType(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.ClientType> clientType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, clientType, comparisonType, targetNumberOfClients };
            Insert(new InstrWrapper.SkipIfNumberOfClientsOfType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfNumberOfClientsOfType(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ClientType> clientType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, clientType, comparisonType, targetNumberOfClients };
            Insert(new InstrWrapper.GotoIfNumberOfClientsOfType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfNumberOfClientsOfType(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.ClientType> clientType,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, clientType, comparisonType, targetNumberOfClients };
            Insert(new InstrWrapper.EndIfNumberOfClientsOfType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfEventFlag(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.OnOff> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> targetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, desiredFlagState, targetEventFlagType, targetEventFlagId };
            Insert(new InstrWrapper.GotoIfEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfBatchEventFlags(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.LogicalOperationType> desiredFlagState,
            EventArg<EventEnum.TargetEventFlagType> targetEventFlagType,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, desiredFlagState, targetEventFlagType, startingTargetEventFlagId, endingTargetEventFlagId };
            Insert(new InstrWrapper.GotoIfBatchEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfMultiplayerState(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.MultiplayerState> desiredMultiplayerState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, desiredMultiplayerState };
            Insert(new InstrWrapper.GotoIfMultiplayerState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfPlayerInOutMap(
            EventArg<EventEnum.Label> label,
            EventArg<bool> shouldPlayerBeInside,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, shouldPlayerBeInside, areaId, blockId };
            Insert(new InstrWrapper.GotoIfPlayerInOutMap(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfNumberOfCoOpClients(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<byte> targetNumberOfClients,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, comparisonType, targetNumberOfClients };
            Insert(new InstrWrapper.GotoIfNumberOfCoOpClients(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfCharacterHasSpeffect(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.EndIfCharacterHasSpeffect(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfCharacterHasSpeffect(
            EventArg<byte> numberOfSkippedLines,
            EventArg<int> targetEntityId,
            EventArg<int> speffectId,
            EventArg<bool> shouldHave,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<int> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, targetEntityId, speffectId, shouldHave, comparisonType, numberOfTargetCharacters };
            Insert(new InstrWrapper.SkipIfCharacterHasSpeffect(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfPlayerInOutsideArea(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters };
            Insert(new InstrWrapper.GotoIfPlayerInOutsideArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfPlayerInOutsideArea(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters };
            Insert(new InstrWrapper.EndIfPlayerInOutsideArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfPlayerInOutsideArea(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.InsideOutsideState> desiredState,
            EventArg<int> targetEntityId,
            EventArg<int> areaEntityId,
            EventArg<int> numberOfTargetCharacters,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, desiredState, targetEntityId, areaEntityId, numberOfTargetCharacters };
            Insert(new InstrWrapper.SkipIfPlayerInOutsideArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfHollowArenaMatchType(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.HollowArenaMatchType> matchType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, matchType };
            Insert(new InstrWrapper.GotoIfHollowArenaMatchType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WaitForObjectDestroyed(
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { damageState, targetObjectEntityId };
            Insert(new InstrWrapper.WaitForObjectDestroyed(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SkipIfObjectDestroyed(
            EventArg<byte> numberOfSkippedLines,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { numberOfSkippedLines, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects };
            Insert(new InstrWrapper.SkipIfObjectDestroyed(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndIfObjectDestroyed(
            EventArg<EventEnum.EventEndType> executionEndType,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { executionEndType, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects };
            Insert(new InstrWrapper.EndIfObjectDestroyed(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder GotoIfObjectDestroyed(
            EventArg<EventEnum.Label> label,
            EventArg<EventEnum.DestructionState> damageState,
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.ComparisonType> comparisonType,
            EventArg<float> numberOfTargetObjects,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { label, damageState, targetObjectEntityId, comparisonType, numberOfTargetObjects };
            Insert(new InstrWrapper.GotoIfObjectDestroyed(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label0(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label0(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label1(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label1(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label2(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label2(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label3(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label3(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label4(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label4(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label5(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label5(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label6(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label6(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label7(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label7(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label8(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label8(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label9(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label9(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label10(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label10(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label11(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label11(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label12(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label12(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label13(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label13(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label14(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label14(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label15(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label15(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label16(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label16(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label17(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label17(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label18(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label18(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label19(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label19(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Label20(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.Label20(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InitializeEvent(
            EventArg<int> eventSlotId,
            EventArg<uint> eventId,
            uint? layerMask,
            params EventArg<uint>[] parameters)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventSlotId, eventId }.Concat(parameters).ToArray();
            Insert(new InstrWrapper.InitializeEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder TerminateEvent(
            EventArg<int> eventSlotId,
            EventArg<uint> eventId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventSlotId, eventId };
            Insert(new InstrWrapper.TerminateEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNetworkSyncState(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disabledEnabled };
            Insert(new InstrWrapper.SetNetworkSyncState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ClearCompiledConditionGroupState(
            EventArg<byte> dummy,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { dummy };
            Insert(new InstrWrapper.ClearCompiledConditionGroupState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PrefetchRequest(
            EventArg<uint> requestId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { requestId };
            Insert(new InstrWrapper.PrefetchRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SaveRequest(
            EventArg<byte> dummy,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { dummy };
            Insert(new InstrWrapper.SaveRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InitializeCommonEvent(
            EventArg<uint> eventId,
            uint? layerMask,
            params EventArg<uint>[] parameters)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventId }.Concat(parameters).ToArray();
            Insert(new InstrWrapper.InitializeCommonEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneToAll(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod };
            Insert(new InstrWrapper.PlayCutsceneToAll(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneAndWarpAllPlayers(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId };
            Insert(new InstrWrapper.PlayCutsceneAndWarpAllPlayers(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneToPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, playerEntityId };
            Insert(new InstrWrapper.PlayCutsceneToPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneAndWarpPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId };
            Insert(new InstrWrapper.PlayCutsceneAndWarpPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneAndRotatePlayerAroundVerticalAxis(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<float> yAxisXCoordM,
            EventArg<float> yAxisZCoordM,
            EventArg<int> rotationDeg,
            EventArg<float> verticalTranslationM,
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, yAxisXCoordM, yAxisZCoordM, rotationDeg, verticalTranslationM, playerEntityId };
            Insert(new InstrWrapper.PlayCutsceneAndRotatePlayerAroundVerticalAxis(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneChangeTimePeriodAndWarpPlayer(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId,
            EventArg<byte> timePeriodId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId, timePeriodId };
            Insert(new InstrWrapper.PlayCutsceneChangeTimePeriodAndWarpPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneAndChangeTimePeriod(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> playerEntityId,
            EventArg<byte> timePeriodId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, playerEntityId, timePeriodId };
            Insert(new InstrWrapper.PlayCutsceneAndChangeTimePeriod(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DummyPlayCutsceneAndWarpPlayer(
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { pointEntityId, areaId, blockId };
            Insert(new InstrWrapper.DummyPlayCutsceneAndWarpPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, ceremonyId, unknown, pointEntityId, areaId, blockId, playerEntityId };
            Insert(new InstrWrapper.PlayCutsceneChangeMapCeremonyAndWarpPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneToPlayerAndChangeCurrentMapCeremony(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<uint> ceremonyId,
            EventArg<int> unknown,
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, ceremonyId, unknown, playerEntityId };
            Insert(new InstrWrapper.PlayCutsceneToPlayerAndChangeCurrentMapCeremony(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<byte> unknown2,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId, unknown, unknown2 };
            Insert(new InstrWrapper.PlayOngoingCutsceneAndWarpPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayCutsceneAndWarpPlayerUnknown200212(
            EventArg<int> cutsceneId,
            EventArg<EventEnum.CutscenePlayMode> playbackMethod,
            EventArg<int> pointEntityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> playerEntityId,
            EventArg<int> pointEntityId2,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { cutsceneId, playbackMethod, pointEntityId, areaId, blockId, playerEntityId, pointEntityId2 };
            Insert(new InstrWrapper.PlayCutsceneAndWarpPlayerUnknown200212(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RequestAnimationPlayback(
            EventArg<int> entityId,
            EventArg<int> animationId,
            EventArg<bool> shouldLoop,
            EventArg<bool> shouldWaitForCompletion,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, animationId, shouldLoop, shouldWaitForCompletion };
            Insert(new InstrWrapper.RequestAnimationPlayback(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetEventFlag(
            EventArg<int> eventFlagId,
            EventArg<EventEnum.OnOff> flagState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId, flagState };
            Insert(new InstrWrapper.SetEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DeActivateGenerator(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.DeActivateGenerator(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder AwardItemLot(
            EventArg<int> itemLotId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { itemLotId };
            Insert(new InstrWrapper.AwardItemLot(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ShootBullet(
            EventArg<int> bulletTeamEntityId,
            EventArg<int> bulletProducerEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> behaviorId,
            EventArg<int> firingAngleX,
            EventArg<int> firingAngleY,
            EventArg<int> firingAngleZ,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { bulletTeamEntityId, bulletProducerEntityId, dummypolyId, behaviorId, firingAngleX, firingAngleY, firingAngleZ };
            Insert(new InstrWrapper.ShootBullet(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DeActivateMapHit(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.DeActivateMapHit(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetMapVisibility(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetMapVisibility(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetEventState(
            EventArg<int> eventId,
            EventArg<int> eventSlotId,
            EventArg<EventEnum.EventEndType> eventState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventId, eventSlotId, eventState };
            Insert(new InstrWrapper.SetEventState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InvertEventFlag(
            EventArg<int> eventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId };
            Insert(new InstrWrapper.InvertEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetEventNavimesh(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disabledEnabled };
            Insert(new InstrWrapper.SetEventNavimesh(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayBossHealthBar(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            EventArg<int> entityId,
            EventArg<short> slotNumber,
            EventArg<int> nameId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disabledEnabled, entityId, slotNumber, nameId };
            Insert(new InstrWrapper.DisplayBossHealthBar(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder HandleBossDefeat(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.HandleBossDefeat(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ModifyNavimeshConnectionBitflag(
            EventArg<int> entityId,
            EventArg<EventEnum.NavimeshType> navimeshType,
            EventArg<EventEnum.BitopType> typeBitOperation,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, navimeshType, typeBitOperation };
            Insert(new InstrWrapper.ModifyNavimeshConnectionBitflag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WarpPlayer(
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<int> initialAreaId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { areaId, blockId, initialAreaId };
            Insert(new InstrWrapper.WarpPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder HandleMinibossDefeat(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.HandleMinibossDefeat(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder TriggerMultiplayerEvent(
            EventArg<uint> multiplayerEventId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { multiplayerEventId };
            Insert(new InstrWrapper.TriggerMultiplayerEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RandomlySetEventFlagInRange(
            EventArg<uint> eventFlagIdMin,
            EventArg<uint> eventFlagIdMax,
            EventArg<EventEnum.OnOff> flagState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagIdMin, eventFlagIdMax, flagState };
            Insert(new InstrWrapper.RandomlySetEventFlagInRange(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ForceAnimationPlayback(
            EventArg<int> entityId,
            EventArg<int> animationId,
            EventArg<bool> shouldLoop,
            EventArg<bool> shouldWaitForCompletion,
            EventArg<bool> ignoreWaitForTransition,
            EventArg<byte> unknown,
            EventArg<float> unknown2,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, animationId, shouldLoop, shouldWaitForCompletion, ignoreWaitForTransition, unknown, unknown2 };
            Insert(new InstrWrapper.ForceAnimationPlayback(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetAreaDrawparamBankSlotIndex(
            EventArg<short> areaId,
            EventArg<short> areaDrawparamBankSlotIndex,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { areaId, areaDrawparamBankSlotIndex };
            Insert(new InstrWrapper.SetAreaDrawparamBankSlotIndex(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetTemporaryPlayerRespawnPoint(
            EventArg<int> respawnPointEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { respawnPointEntityId };
            Insert(new InstrWrapper.SetTemporaryPlayerRespawnPoint(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IncrementGameCycle(
            EventArg<byte> dummy,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { dummy };
            Insert(new InstrWrapper.IncrementGameCycle(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BatchSetEventFlags(
            EventArg<int> eventFlagIdStart,
            EventArg<int> eventFlagIdEnd,
            EventArg<EventEnum.OnOff> flagState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagIdStart, eventFlagIdEnd, flagState };
            Insert(new InstrWrapper.BatchSetEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetPlayerRespawnPoint(
            EventArg<int> respawnPointEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { respawnPointEntityId };
            Insert(new InstrWrapper.SetPlayerRespawnPoint(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RemoveItemFromPlayer(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> number,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { itemType, itemId, number };
            Insert(new InstrWrapper.RemoveItemFromPlayer(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlaceNpcSummonSign(
            EventArg<EventEnum.SummonSignType> signType,
            EventArg<int> summonedNpcEntityId,
            EventArg<int> spawnPointEntityId,
            EventArg<int> summonEventFlagId,
            EventArg<int> dismissalEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { signType, summonedNpcEntityId, spawnPointEntityId, summonEventFlagId, dismissalEventFlagId };
            Insert(new InstrWrapper.PlaceNpcSummonSign(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetVisibilityOfMessage(
            EventArg<int> messageEntityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageEntityId, disabledEnabled };
            Insert(new InstrWrapper.SetVisibilityOfMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Missing(
            EventArg<byte> dummy,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { dummy };
            Insert(new InstrWrapper.Missing(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder AwardAchievement(
            EventArg<int> achievementId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { achievementId };
            Insert(new InstrWrapper.AwardAchievement(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeWorldTendency(
            EventArg<EventEnum.TendencyType> tendencyType,
            EventArg<sbyte> changeValue,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { tendencyType, changeValue };
            Insert(new InstrWrapper.ChangeWorldTendency(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisableVagrantSpawning(
            EventArg<bool> disabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disabled };
            Insert(new InstrWrapper.DisableVagrantSpawning(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IncrementEventValue(
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits,
            EventArg<uint> maximumAllowedValue,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { baseEventFlagId, numberOfUsedFlagBits, maximumAllowedValue };
            Insert(new InstrWrapper.IncrementEventValue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ClearEventValue(
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { baseEventFlagId, numberOfUsedFlagBits };
            Insert(new InstrWrapper.ClearEventValue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetSnugglyNextTrade(
            EventArg<int> eventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId };
            Insert(new InstrWrapper.SetSnugglyNextTrade(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SpawnSnugglyItem(
            EventArg<int> itemLotId,
            EventArg<int> placementAreaEntityId,
            EventArg<int> eventFlagId,
            EventArg<int> hitEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { itemLotId, placementAreaEntityId, eventFlagId, hitEntityId };
            Insert(new InstrWrapper.SpawnSnugglyItem(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder MoveBloodstainAndDroppedItems(
            EventArg<int> sourceAreaEntityId,
            EventArg<int> destinationAreaEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { sourceAreaEntityId, destinationAreaEntityId };
            Insert(new InstrWrapper.MoveBloodstainAndDroppedItems(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder AwardItemsIncludingClients(
            EventArg<int> itemLotId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { itemLotId };
            Insert(new InstrWrapper.AwardItemsIncludingClients(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BattleOfStoicism1v1RankingRequest(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.BattleOfStoicism1v1RankingRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BattleOfStoicism2v2RankingRequest(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.BattleOfStoicism2v2RankingRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BattleOfStoicismFfaRankingRequest(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.BattleOfStoicismFfaRankingRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BattleOfStoicismExitRequest(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.BattleOfStoicismExitRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EventValueOperation(
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits,
            EventArg<int> operand,
            EventArg<int> baseEventFlagIdOperand,
            EventArg<uint> numberOfUsedEventFlagBitsOperand,
            EventArg<EventEnum.CalculationType> calculationType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { baseEventFlagId, numberOfUsedFlagBits, operand, baseEventFlagIdOperand, numberOfUsedEventFlagBitsOperand, calculationType };
            Insert(new InstrWrapper.EventValueOperation(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder StoreItemAmountHeldInEventValue(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { itemType, itemId, baseEventFlagId, numberOfUsedFlagBits };
            Insert(new InstrWrapper.StoreItemAmountHeldInEventValue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DirectlyGivePlayerItem(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> baseEventFlagId,
            EventArg<uint> numberOfUsedFlagBits,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { itemType, itemId, baseEventFlagId, numberOfUsedFlagBits };
            Insert(new InstrWrapper.DirectlyGivePlayerItem(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ToggleDirectionDisplay(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disabledEnabled };
            Insert(new InstrWrapper.ToggleDirectionDisplay(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetMapHitgridCorrespondence(
            EventArg<int> hitEntityId,
            EventArg<short> level,
            EventArg<short> gridCoordX,
            EventArg<short> gridCoordY,
            EventArg<EventEnum.AddDelete> process,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { hitEntityId, level, gridCoordX, gridCoordY, process };
            Insert(new InstrWrapper.SetMapHitgridCorrespondence(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetMapContentImageDisplay(
            EventArg<short> contentImagePartId,
            EventArg<EventEnum.DisplayState> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { contentImagePartId, state };
            Insert(new InstrWrapper.SetMapContentImageDisplay(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetMapBoundariesDisplay(
            EventArg<short> hierarchy,
            EventArg<short> gridCoordX,
            EventArg<short> gridCoordY,
            EventArg<EventEnum.DisplayState> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { hierarchy, gridCoordX, gridCoordY, state };
            Insert(new InstrWrapper.SetMapBoundariesDisplay(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetAreaWind(
            EventArg<int> areaEntityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            EventArg<float> changeTimeS,
            EventArg<int> windParameterId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { areaEntityId, disabledEnabled, changeTimeS, windParameterId };
            Insert(new InstrWrapper.SetAreaWind(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WarpPlayerToRespawnPoint(
            EventArg<int> respawnPointParameterId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { respawnPointParameterId };
            Insert(new InstrWrapper.WarpPlayerToRespawnPoint(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder StartEnemyGenerator(
            EventArg<int> enemySpawnerId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { enemySpawnerId };
            Insert(new InstrWrapper.StartEnemyGenerator(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SummonNpc(
            EventArg<EventEnum.SingleplayerSummonSignType> signType,
            EventArg<int> summonedNpcEntityId,
            EventArg<int> spawnPointEntityId,
            EventArg<int> summonEventFlagId,
            EventArg<int> dismissalEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { signType, summonedNpcEntityId, spawnPointEntityId, summonEventFlagId, dismissalEventFlagId };
            Insert(new InstrWrapper.SummonNpc(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InitializeWarpObject(
            EventArg<int> warpObjectEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { warpObjectEntityId };
            Insert(new InstrWrapper.InitializeWarpObject(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder MakeEnemyAppearEvent(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.MakeEnemyAppearEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCurrentMapCeremony(
            EventArg<uint> ceremonyId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { ceremonyId };
            Insert(new InstrWrapper.SetCurrentMapCeremony(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetMapCeremony(
            EventArg<uint> areaId,
            EventArg<byte> blockId,
            EventArg<uint> ceremonyId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { areaId, blockId, ceremonyId };
            Insert(new InstrWrapper.SetMapCeremony(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayEpitaphMessage(
            EventArg<int> messageId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageId };
            Insert(new InstrWrapper.DisplayEpitaphMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNetworkConnectedEventFlag(
            EventArg<int> eventFlagId,
            EventArg<EventEnum.OnOff> flagState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId, flagState };
            Insert(new InstrWrapper.SetNetworkConnectedEventFlag(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BatchSetNetworkConnectedEventFlags(
            EventArg<int> eventFlagIdStart,
            EventArg<int> eventFlagIdEnd,
            EventArg<EventEnum.OnOff> flagState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagIdStart, eventFlagIdEnd, flagState };
            Insert(new InstrWrapper.BatchSetNetworkConnectedEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetOmissionModeCounts(
            EventArg<int> level1Count,
            EventArg<int> level2Count,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { level1Count, level2Count };
            Insert(new InstrWrapper.SetOmissionModeCounts(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ResetOmissionModeCountsToDefault(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.ResetOmissionModeCountsToDefault(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InitializeCrowTrade(
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            EventArg<int> itemLotId,
            EventArg<int> tradeCompletedEventFlagId,
            EventArg<int> crowDialogueResponseEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { itemType, itemId, itemLotId, tradeCompletedEventFlagId, crowDialogueResponseEventFlagId };
            Insert(new InstrWrapper.InitializeCrowTrade(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InitializeCrowTradeArea(
            EventArg<int> areaEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { areaEntityId };
            Insert(new InstrWrapper.InitializeCrowTradeArea(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNetworkInteractionState(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disabledEnabled };
            Insert(new InstrWrapper.SetNetworkInteractionState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder HideHud(
            EventArg<bool> shouldBeHidden,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { shouldBeHidden };
            Insert(new InstrWrapper.HideHud(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetBonfireWarpingState(
            EventArg<int> bonfireEntityId,
            EventArg<int> animationId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { bonfireEntityId, animationId, disabledEnabled };
            Insert(new InstrWrapper.SetBonfireWarpingState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetAutogeneratedEventSpecificEventFlag1Unknown200373(
            EventArg<byte> unknown,
            EventArg<byte> unknown2,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { unknown, unknown2 };
            Insert(new InstrWrapper.SetAutogeneratedEventSpecificEventFlag1Unknown200373(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder HandleBossDefeatAndDisplayBanner(
            EventArg<int> entityId,
            EventArg<EventEnum.TextBannerType> bannerType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, bannerType };
            Insert(new InstrWrapper.HandleBossDefeatAndDisplayBanner(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetAutogeneratedEventSpecificEventFlag2Unknown200375(
            EventArg<byte> unknown,
            EventArg<byte> unknown2,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { unknown, unknown2 };
            Insert(new InstrWrapper.SetAutogeneratedEventSpecificEventFlag2Unknown200375(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisableLoadingScreenTips(
            EventArg<bool> shouldBeDisabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { shouldBeDisabled };
            Insert(new InstrWrapper.DisableLoadingScreenTips(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder AwardGestureItem(
            EventArg<ushort> gestureIndex,
            EventArg<EventEnum.ItemType> itemType,
            EventArg<int> itemId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { gestureIndex, itemType, itemId };
            Insert(new InstrWrapper.AwardGestureItem(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SendNpcSummonHome(
            EventArg<int> summonEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { summonEntityId };
            Insert(new InstrWrapper.SendNpcSummonHome(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Unknown200379(
            EventArg<byte> unknown,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { unknown };
            Insert(new InstrWrapper.Unknown200379(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayDecoratedBossHealthBar(
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            EventArg<int> entityId,
            EventArg<short> slotNumber,
            EventArg<int> nameId,
            EventArg<byte> decorationId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disabledEnabled, entityId, slotNumber, nameId, decorationId };
            Insert(new InstrWrapper.DisplayDecoratedBossHealthBar(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlaceNpcSummonSignWorksForNonEmberedPlayers(
            EventArg<EventEnum.SummonSignType> signType,
            EventArg<int> summonedNpcEntityId,
            EventArg<int> spawnPointEntityId,
            EventArg<int> summonEventFlagId,
            EventArg<int> dismissalEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { signType, summonedNpcEntityId, spawnPointEntityId, summonEventFlagId, dismissalEventFlagId };
            Insert(new InstrWrapper.PlaceNpcSummonSignWorksForNonEmberedPlayers(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterAiState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetCharacterAiState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterTeamType(
            EventArg<int> entityId,
            EventArg<EventEnum.TeamType> teamType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, teamType };
            Insert(new InstrWrapper.SetCharacterTeamType(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder CharacterWarpRequest(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId };
            Insert(new InstrWrapper.CharacterWarpRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ForceCharacterDeath(
            EventArg<int> entityId,
            EventArg<bool> shouldReceiveSouls,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, shouldReceiveSouls };
            Insert(new InstrWrapper.ForceCharacterDeath(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCharacterEnableState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.ChangeCharacterEnableState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EzstateInstructionRequest(
            EventArg<int> entityId,
            EventArg<int> command,
            EventArg<byte> slot,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, command, slot };
            Insert(new InstrWrapper.EzstateInstructionRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder CreateBulletOwner(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.CreateBulletOwner(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetSpeffect(
            EventArg<int> entityId,
            EventArg<int> speffectId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, speffectId };
            Insert(new InstrWrapper.SetSpeffect(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetSpecialAnimationS(
            EventArg<int> entityId,
            EventArg<int> waitingAnimation,
            EventArg<int> damageAnimation,
            EventArg<int> cancelAnimation,
            EventArg<int> deathAnimation,
            EventArg<int> returnAnimation,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, waitingAnimation, damageAnimation, cancelAnimation, deathAnimation, returnAnimation };
            Insert(new InstrWrapper.SetSpecialAnimationS(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterGravity(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetCharacterGravity(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterEventTarget(
            EventArg<int> entityId,
            EventArg<int> entityId2,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, entityId2 };
            Insert(new InstrWrapper.SetCharacterEventTarget(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterImmortality(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetCharacterImmortality(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterHome(
            EventArg<int> entityId,
            EventArg<int> areaEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, areaEntityId };
            Insert(new InstrWrapper.SetCharacterHome(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RotateCharacter(
            EventArg<int> entityId,
            EventArg<int> entityId2,
            EventArg<int> playAnimationId,
            EventArg<bool> shouldWaitForCompletion,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, entityId2, playAnimationId, shouldWaitForCompletion };
            Insert(new InstrWrapper.RotateCharacter(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterInvincibility(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetCharacterInvincibility(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ClearCharacterSAiTarget(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.ClearCharacterSAiTarget(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RequestCharacterAiCommand(
            EventArg<int> entityId,
            EventArg<int> commandId,
            EventArg<byte> slotNumber,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, commandId, slotNumber };
            Insert(new InstrWrapper.RequestCharacterAiCommand(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetEventPoint(
            EventArg<int> entityId,
            EventArg<int> entityId2,
            EventArg<float> reactionDistance,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, entityId2, reactionDistance };
            Insert(new InstrWrapper.SetEventPoint(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterAiId(
            EventArg<int> entityId,
            EventArg<int> aiId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, aiId };
            Insert(new InstrWrapper.SetCharacterAiId(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RequestCharacterAiRePlan(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.RequestCharacterAiRePlan(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ClearSpeffect(
            EventArg<int> entityId,
            EventArg<int> speffectId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, speffectId };
            Insert(new InstrWrapper.ClearSpeffect(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<bool> startInStoppedState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, npcPartId, npcPartGroupIdx, npcPartHp, damageCorrection, bodyDamageCompensation, isInvincible, startInStoppedState };
            Insert(new InstrWrapper.CreateNpcPart(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNpcPartHp(
            EventArg<int> entityId,
            EventArg<int> npcPartId,
            EventArg<int> desiredHp,
            EventArg<bool> shouldAllowHpAboveMaxHp,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, npcPartId, desiredHp, shouldAllowHpAboveMaxHp };
            Insert(new InstrWrapper.SetNpcPartHp(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNpcPartSeAndSfx(
            EventArg<int> entityId,
            EventArg<int> npcPartId,
            EventArg<int> defenseMaterialSeId,
            EventArg<int> defenseMaterialSfxId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, npcPartId, defenseMaterialSeId, defenseMaterialSfxId };
            Insert(new InstrWrapper.SetNpcPartSeAndSfx(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNpcPartBulletDamageMultiplier(
            EventArg<int> entityId,
            EventArg<int> npcPartId,
            EventArg<float> bulletDamageMultiplier,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, npcPartId, bulletDamageMultiplier };
            Insert(new InstrWrapper.SetNpcPartBulletDamageMultiplier(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCharacterDispmask(
            EventArg<int> entityId,
            EventArg<byte> bitNumber,
            EventArg<EventEnum.OnOffChange> switchType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, bitNumber, switchType };
            Insert(new InstrWrapper.ChangeCharacterDispmask(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCharacterHitmask(
            EventArg<int> entityId,
            EventArg<byte> bitNumber,
            EventArg<EventEnum.OnOffChange> switchType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, bitNumber, switchType };
            Insert(new InstrWrapper.ChangeCharacterHitmask(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNetworkUpdateAuthority(
            EventArg<int> entityId,
            EventArg<EventEnum.AuthorityLevel> authorityLevel,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, authorityLevel };
            Insert(new InstrWrapper.SetNetworkUpdateAuthority(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterBackreadState(
            EventArg<int> entityId,
            EventArg<bool> removed,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, removed };
            Insert(new InstrWrapper.SetCharacterBackreadState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterHpBarDisplay(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetCharacterHpBarDisplay(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterMaphit(
            EventArg<int> entityId,
            EventArg<bool> disabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabled };
            Insert(new InstrWrapper.SetCharacterMaphit(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RequestCharacterAiCommandEventValue(
            EventArg<int> entityId,
            EventArg<int> commandId,
            EventArg<byte> slotNumber,
            EventArg<int> startingTargetEventFlagId,
            EventArg<int> endingTargetEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, commandId, slotNumber, startingTargetEventFlagId, endingTargetEventFlagId };
            Insert(new InstrWrapper.RequestCharacterAiCommandEventValue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder CreateReferredDamagePair(
            EventArg<int> sourceEntityId,
            EventArg<int> targetEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { sourceEntityId, targetEntityId };
            Insert(new InstrWrapper.CreateReferredDamagePair(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetNetworkUpdateRate(
            EventArg<int> entityId,
            EventArg<bool> useFixedFrequency,
            EventArg<EventEnum.CharacterUpdateFrequency> updateFrequency,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, useFixedFrequency, updateFrequency };
            Insert(new InstrWrapper.SetNetworkUpdateRate(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterDefaultBackreadState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetCharacterDefaultBackreadState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder HellkiteBreathControl(
            EventArg<int> characterEntityId,
            EventArg<int> objectEntityId,
            EventArg<int> characterAnimationId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { characterEntityId, objectEntityId, characterAnimationId };
            Insert(new InstrWrapper.HellkiteBreathControl(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ForceCharacterTreasure(
            EventArg<int> characterEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { characterEntityId };
            Insert(new InstrWrapper.ForceCharacterTreasure(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BetrayCharacterSCurrentCovenant(
            EventArg<byte> dummy,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { dummy };
            Insert(new InstrWrapper.BetrayCharacterSCurrentCovenant(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterAnimationState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetCharacterAnimationState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WarpCharacterAndSetFloor(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> warpDestinationHitEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId, warpDestinationHitEntityId };
            Insert(new InstrWrapper.WarpCharacterAndSetFloor(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IssueShortWarpRequest(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId };
            Insert(new InstrWrapper.IssueShortWarpRequest(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WarpCharacterAndCopyFloor(
            EventArg<int> entityId,
            EventArg<EventEnum.TargetEntityType> warpType,
            EventArg<int> warpDestinationEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> entityIdToCopyFloorFrom,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, warpType, warpDestinationEntityId, dummypolyId, entityIdToCopyFloorFrom };
            Insert(new InstrWrapper.WarpCharacterAndCopyFloor(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RequestCharacterAnimationReset(
            EventArg<int> entityId,
            EventArg<EventEnum.Interpolation> interpolationMethod,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, interpolationMethod };
            Insert(new InstrWrapper.RequestCharacterAnimationReset(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCharacterTeamTypeAndForceExitSpecialAnimations(
            EventArg<int> entityId,
            EventArg<EventEnum.TeamType> teamType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, teamType };
            Insert(new InstrWrapper.ChangeCharacterTeamTypeAndForceExitSpecialAnimations(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RegisterNpcHumanityEventValue(
            EventArg<int> npcEntityId,
            EventArg<int> humanityEventValueBaseEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { npcEntityId, humanityEventValueBaseEventFlagId };
            Insert(new InstrWrapper.RegisterNpcHumanityEventValue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IncrementPlayerPvpSin(
            EventArg<byte> dummy,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { dummy };
            Insert(new InstrWrapper.IncrementPlayerPvpSin(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder BonfireLikeRecovery(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.BonfireLikeRecovery(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCharacterSCloth(
            EventArg<int> entityId,
            EventArg<byte> bitNumber,
            EventArg<byte> stateId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, bitNumber, stateId };
            Insert(new InstrWrapper.ChangeCharacterSCloth(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCharacterPatrolBehavior(
            EventArg<int> characterEntityId,
            EventArg<int> patrolInformationEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { characterEntityId, patrolInformationEntityId };
            Insert(new InstrWrapper.ChangeCharacterPatrolBehavior(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetLockOnPoint(
            EventArg<int> entityId,
            EventArg<int> lockOnDummypolyId,
            EventArg<EventEnum.DisabledEnabled> desiredState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, lockOnDummypolyId, desiredState };
            Insert(new InstrWrapper.SetLockOnPoint(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder TestRequestRagdollRestraint(
            EventArg<int> entityIdRecipientSide,
            EventArg<int> targetRigidIndexRecipientSide,
            EventArg<int> dummypolyIdRecipientSide,
            EventArg<int> entityIdAttachmentSide,
            EventArg<int> targetRigidIndexAttachmentSide,
            EventArg<int> dummypolyIdAttachmentSide,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityIdRecipientSide, targetRigidIndexRecipientSide, dummypolyIdRecipientSide, entityIdAttachmentSide, targetRigidIndexAttachmentSide, dummypolyIdAttachmentSide };
            Insert(new InstrWrapper.TestRequestRagdollRestraint(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCharacter(
            EventArg<int> charinitparamId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { charinitparamId };
            Insert(new InstrWrapper.ChangeCharacter(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder AdaptHpChangingSpeffectToNpcPartOfTarget(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.AdaptHpChangingSpeffectToNpcPartOfTarget(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ImmediateActivateUnknown200454(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.ImmediateActivateUnknown200454(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterTalkRange(
            EventArg<int> entityId,
            EventArg<float> range,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, range };
            Insert(new InstrWrapper.SetCharacterTalkRange(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IncrementCharacterGameCycle(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.IncrementCharacterGameCycle(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetMultiplayerDependentBuffsNonBoss(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.SetMultiplayerDependentBuffsNonBoss(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Unknown200459(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> disabledEnabled,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, disabledEnabled };
            Insert(new InstrWrapper.Unknown200459(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCharacterRemainingYoelLevels(
            EventArg<byte> levelCount,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { levelCount };
            Insert(new InstrWrapper.SetCharacterRemainingYoelLevels(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RequestObjectDestruction(
            EventArg<int> entityId,
            EventArg<sbyte> slotNumber,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, slotNumber };
            Insert(new InstrWrapper.RequestObjectDestruction(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RequestObjectRestoration(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.RequestObjectRestoration(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DeActivateObject(
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { targetObjectEntityId, state };
            Insert(new InstrWrapper.DeActivateObject(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetObjectTreasureState(
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { targetObjectEntityId, state };
            Insert(new InstrWrapper.SetObjectTreasureState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InitializeObjact(
            EventArg<int> entityId,
            EventArg<int> objectParameterId,
            EventArg<int> relativeTargetIdx,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, objectParameterId, relativeTargetIdx };
            Insert(new InstrWrapper.InitializeObjact(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetObjactState(
            EventArg<int> entityId,
            EventArg<int> objectParameterId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, objectParameterId, state };
            Insert(new InstrWrapper.SetObjactState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ReproduceObjectAnimation(
            EventArg<int> targetObjectEntityId,
            EventArg<int> animationId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { targetObjectEntityId, animationId };
            Insert(new InstrWrapper.ReproduceObjectAnimation(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ReproduceObjectDestruction(
            EventArg<int> entityId,
            EventArg<sbyte> slotNumber,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, slotNumber };
            Insert(new InstrWrapper.ReproduceObjectDestruction(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<float> repetitionTimeS,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId, entityId, dummypolyId, behaviorId, target, radius, lifespan, repetitionTimeS };
            Insert(new InstrWrapper.CreateDamagingObject(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RegisterStatue(
            EventArg<int> entityId,
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<EventEnum.StatueType> statueType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, areaId, blockId, statueType };
            Insert(new InstrWrapper.RegisterStatue(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder WarpObjectToCharacter(
            EventArg<int> entityId,
            EventArg<int> characterEntityId,
            EventArg<short> dummypolyId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, characterEntityId, dummypolyId };
            Insert(new InstrWrapper.WarpObjectToCharacter(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DeleteObjectEvent(
            EventArg<int> eventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId };
            Insert(new InstrWrapper.DeleteObjectEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetObjectInvulnerability(
            EventArg<int> targetObjectEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { targetObjectEntityId, state };
            Insert(new InstrWrapper.SetObjectInvulnerability(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DeActivateObjactAssignIdx(
            EventArg<int> entityId,
            EventArg<int> objectParameterId,
            EventArg<int> relativeTargetIdx,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, objectParameterId, relativeTargetIdx, state };
            Insert(new InstrWrapper.DeActivateObjactAssignIdx(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RerollObjectTreasure(
            EventArg<int> targetObjectEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { targetObjectEntityId };
            Insert(new InstrWrapper.RerollObjectTreasure(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ExtinguishBurningObjects(uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { };
            Insert(new InstrWrapper.ExtinguishBurningObjects(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ShowObjectByMapCeremony(
            EventArg<int> targetObjectEntityId,
            EventArg<uint> ceremonyId,
            EventArg<int> unknown,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { targetObjectEntityId, ceremonyId, unknown };
            Insert(new InstrWrapper.ShowObjectByMapCeremony(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DestroyObject(
            EventArg<int> targetObjectEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { targetObjectEntityId };
            Insert(new InstrWrapper.DestroyObject(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DeleteMapSfx(
            EventArg<int> entityId,
            EventArg<bool> onlyDeleteRoot,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, onlyDeleteRoot };
            Insert(new InstrWrapper.DeleteMapSfx(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SpawnMapSfx(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.SpawnMapSfx(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SpawnOneshotSfx(
            EventArg<EventEnum.TargetEntityType> type,
            EventArg<int> entityId,
            EventArg<int> dummypolyId,
            EventArg<int> sfxId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { type, entityId, dummypolyId, sfxId };
            Insert(new InstrWrapper.SpawnOneshotSfx(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder CreateObjectFollowingSfx(
            EventArg<int> objectEntityId,
            EventArg<int> dummypolyId,
            EventArg<int> sfxId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { objectEntityId, dummypolyId, sfxId };
            Insert(new InstrWrapper.CreateObjectFollowingSfx(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DeleteObjectFollowingSfx(
            EventArg<int> objectEntityId,
            EventArg<bool> shouldDeleteRoot,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { objectEntityId, shouldDeleteRoot };
            Insert(new InstrWrapper.DeleteObjectFollowingSfx(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayGenericDialog(
            EventArg<int> messageId,
            EventArg<EventEnum.PromptType> dialogType,
            EventArg<EventEnum.NumberOfOptions> numberOfOptions,
            EventArg<int> entityId,
            EventArg<float> displayDistance,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageId, dialogType, numberOfOptions, entityId, displayDistance };
            Insert(new InstrWrapper.DisplayGenericDialog(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayBanner(
            EventArg<EventEnum.TextBannerType> bannerType,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { bannerType };
            Insert(new InstrWrapper.DisplayBanner(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayStatusMessage(
            EventArg<int> messageId,
            EventArg<EventEnum.DisabledEnabled> padState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageId, padState };
            Insert(new InstrWrapper.DisplayStatusMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayMessage(
            EventArg<int> messageId,
            EventArg<byte> screenLocationIndex,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageId, screenLocationIndex };
            Insert(new InstrWrapper.DisplayMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagA(
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { playerEntityId };
            Insert(new InstrWrapper.SetBattleOfStocismNametagA(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagB(
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { playerEntityId };
            Insert(new InstrWrapper.SetBattleOfStocismNametagB(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagC(
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { playerEntityId };
            Insert(new InstrWrapper.SetBattleOfStocismNametagC(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetBattleOfStocismNametagD(
            EventArg<int> playerEntityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { playerEntityId };
            Insert(new InstrWrapper.SetBattleOfStocismNametagD(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayBattleOfStocismDissolutionMessage(
            EventArg<int> messageId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageId };
            Insert(new InstrWrapper.DisplayBattleOfStocismDissolutionMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
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
            EventArg<int> cancelResponseEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageId, dialogType, numberOfOptions, entityId, displayDistance, leftResponseEventFlagId, rightResponseEventFlagId, cancelResponseEventFlagId };
            Insert(new InstrWrapper.DisplayGenericDialogAndSetEventFlags(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayAreaWelcomeMessage(
            EventArg<int> areaNameMessageId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { areaNameMessageId };
            Insert(new InstrWrapper.DisplayAreaWelcomeMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder DisplayHollowArenaPvpMessage(
            EventArg<int> messageId,
            EventArg<short> unknown,
            EventArg<EventEnum.DisabledEnabled> padState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { messageId, unknown, padState };
            Insert(new InstrWrapper.DisplayHollowArenaPvpMessage(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ChangeCamera(
            EventArg<int> normalCameraId,
            EventArg<int> lockedCameraId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { normalCameraId, lockedCameraId };
            Insert(new InstrWrapper.ChangeCamera(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetCameraVibration(
            EventArg<int> vibrationId,
            EventArg<EventEnum.TargetEntityType> type,
            EventArg<int> entityId,
            EventArg<int> dummypolyId,
            EventArg<float> decayStartDistance,
            EventArg<float> decayEndDistance,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { vibrationId, type, entityId, dummypolyId, decayStartDistance, decayEndDistance };
            Insert(new InstrWrapper.SetCameraVibration(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetLockcamSlotNumber(
            EventArg<byte> areaId,
            EventArg<byte> blockId,
            EventArg<ushort> lockcamSlotNumber,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { areaId, blockId, lockcamSlotNumber };
            Insert(new InstrWrapper.SetLockcamSlotNumber(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RegisterLadder(
            EventArg<int> disableTopEventFlagId,
            EventArg<int> disableBottomEventFlagId,
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { disableTopEventFlagId, disableBottomEventFlagId, entityId };
            Insert(new InstrWrapper.RegisterLadder(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder InitializeWanderingDemon(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<int> appearenceEventFlagId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId, entityId, appearenceEventFlagId };
            Insert(new InstrWrapper.InitializeWanderingDemon(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RegisterWanderingDemon(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<int> entityId2,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId, entityId, entityId2 };
            Insert(new InstrWrapper.RegisterWanderingDemon(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RegisterBonfire(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<float> reactionDistance,
            EventArg<float> reactionAngle,
            EventArg<int> setStandardKindlingLevel,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId, entityId, reactionDistance, reactionAngle, setStandardKindlingLevel };
            Insert(new InstrWrapper.RegisterBonfire(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ActivateMultiplayerDependantBuffs(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.ActivateMultiplayerDependantBuffs(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder RegisterHealingFountain(
            EventArg<int> eventFlagId,
            EventArg<int> entityId,
            EventArg<float> reactionDistance,
            EventArg<float> reactionAngle,
            EventArg<int> initialSwordNumber,
            EventArg<int> swordLevel,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { eventFlagId, entityId, reactionDistance, reactionAngle, initialSwordNumber, swordLevel };
            Insert(new InstrWrapper.RegisterHealingFountain(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder IssueBossRoomEntryNotification(
            EventArg<byte> dummy,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { dummy };
            Insert(new InstrWrapper.IssueBossRoomEntryNotification(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SendInvadingPhantomsHome(
            EventArg<int> unknown,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { unknown };
            Insert(new InstrWrapper.SendInvadingPhantomsHome(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SendAllPhantomsHomeAndUpdateServerPvpStats(
            EventArg<int> unknown,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { unknown };
            Insert(new InstrWrapper.SendAllPhantomsHomeAndUpdateServerPvpStats(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SendAllPhantomsHome(
            EventArg<int> unknown,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { unknown };
            Insert(new InstrWrapper.SendAllPhantomsHome(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlayBgm(
            EventArg<EventEnum.OnOff> shouldPlay,
            EventArg<ushort> slotNumber,
            EventArg<int> entityId,
            EventArg<EventEnum.SoundType> soundType,
            EventArg<int> soundId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { shouldPlay, slotNumber, entityId, soundType, soundId };
            Insert(new InstrWrapper.PlayBgm(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder PlaySe(
            EventArg<int> entityId,
            EventArg<EventEnum.SoundType> soundType,
            EventArg<int> soundId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, soundType, soundId };
            Insert(new InstrWrapper.PlaySe(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetMapSoundState(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> enableDisable,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, enableDisable };
            Insert(new InstrWrapper.SetMapSoundState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EnableBossMapSound(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> enableDisable,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, enableDisable };
            Insert(new InstrWrapper.EnableBossMapSound(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder NotifySoundDampeningOfDoorEvent(
            EventArg<int> entityId,
            EventArg<EventEnum.DoorState> doorState,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, doorState };
            Insert(new InstrWrapper.NotifySoundDampeningOfDoorEvent(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EnableMapSoundWithFade(
            EventArg<int> entityId,
            EventArg<EventEnum.DisabledEnabled> enableDisable,
            EventArg<float> fadeTime,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId, enableDisable, fadeTime };
            Insert(new InstrWrapper.EnableMapSoundWithFade(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder Unknown201007(
            EventArg<int> entityId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { entityId };
            Insert(new InstrWrapper.Unknown201007(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ActivateHit(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { hitEntityId, state };
            Insert(new InstrWrapper.ActivateHit(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ActivateHitBackreadMask(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { hitEntityId, state };
            Insert(new InstrWrapper.ActivateHitBackreadMask(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ActivateHitres(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { hitEntityId, state };
            Insert(new InstrWrapper.ActivateHitres(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ActivateHitAndCreateNavimesh(
            EventArg<int> hitEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { hitEntityId, state };
            Insert(new InstrWrapper.ActivateHitAndCreateNavimesh(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ActivateMapPart(
            EventArg<int> mapPartEntityId,
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { mapPartEntityId, state };
            Insert(new InstrWrapper.ActivateMapPart(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder SetAreaWelcomeMessageState(
            EventArg<EventEnum.DisabledEnabled> state,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { state };
            Insert(new InstrWrapper.SetAreaWelcomeMessageState(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder CreatePlaylog(
            EventArg<uint> playlogName,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { playlogName };
            Insert(new InstrWrapper.CreatePlaylog(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder StartTimeMeasurement(
            EventArg<uint> measurementId,
            EventArg<uint> playlogName,
            EventArg<EventEnum.DisabledEnabled> overwrite,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { measurementId, playlogName, overwrite };
            Insert(new InstrWrapper.StartTimeMeasurement(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder EndTimeMeasurement(
            EventArg<uint> measurementId,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { measurementId };
            Insert(new InstrWrapper.EndTimeMeasurement(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
        public EventBuilder ParameterOutput(
            EventArg<EventEnum.PlayerPlaylogParameter> category,
            EventArg<uint> playlogName,
            EventArg<EventEnum.PlaylogMultiplayerType> outputMultiplayerStatus,
            uint? layerMask = null)
        {
            AbstractEventArg[] args = new AbstractEventArg[] { category, playlogName, outputMultiplayerStatus };
            Insert(new InstrWrapper.ParameterOutput(args, layerMask).ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }
    }
}
