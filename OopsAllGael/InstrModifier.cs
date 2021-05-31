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
        static List<int> bossGaels = new List<int>();
        static List<short> bossGaelHpBarSlots = new List<short>();
        static List<int> bossGaelNameIds = new List<int>();

        static bool PreprocessEvent(EventBuilder eventBuilder, int eventId)
        {
            if (eventId == 14005811) // Iudyx Gundyr
            {
                eventBuilder.Seek(13)
                    .Noop()
                    .Noop();
            }

            return false;
        }

        static void PostprocessEvent(EventBuilder eventBuilder, int eventId)
        {

        }

        static bool aggressive = false;

        // Modify one instruction in an event, with paramMap mapping the argument positions to their values
        static Predicate<MyInstr> InstrModifier(EventBuilder eventBuilder, Dictionary<int, object> paramMap, Dictionary<int, EventArg<int>> gaelMap)
        {
            return delegate (MyInstr myInstr)
            {
                bool saveModification = false;
                if (myInstr is InstrWrapper.ForceAnimationPlayback instr)
                {

                    if (IsGael(paramMap, instr.entityId))
                    {
                        if (aggressive)
                        {
                            eventBuilder.Noop();
                            eventBuilder.Skip(-1);
                        }
                        else
                        {
                            // looping animations are often used to "disable" enemies so replace with disabling AI
                            if (Convert.ToBoolean(GetParamValue(paramMap, instr.shouldLoop)))
                            {
                                eventBuilder.Remove();
                                eventBuilder.SetCharacterAiState(instr.entityId, DisabledEnabled.Disabled, instr.layerMask);
                                eventBuilder.Skip(-1);
                            }
                            else
                            {
                                eventBuilder.Remove();
                                eventBuilder.SetCharacterAiState(instr.entityId, DisabledEnabled.Enabled, instr.layerMask);
                                eventBuilder.Skip(-1);
                            }
                        }
                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterHpRatio instr2)
                {
                    if (IsGael(paramMap, instr2.targetEntityId))
                    {
                        if (instr2.targetHpRatio.isParam)
                        {
                            Console.Write(GetParamValue(paramMap, instr2.targetHpRatio) + " -> ");
                            Console.WriteLine(BitConverter.ToSingle(BitConverter.GetBytes((uint)GetParamValue(paramMap, instr2.targetHpRatio)), 0));
                        }
                        else
                        {
                            float hp = instr2.targetHpRatio.Value;
                            if (hp == 0)
                            {
                                eventBuilder.Remove();
                                eventBuilder.IfCharacterHpRatio(instr2.resultConditionGroup, GetGael2(gaelMap, instr2.targetEntityId), instr2.comparisonType, instr2.targetHpRatio, instr2.comparisonType2, instr2.numberOfTargetCharacters, instr2.layerMask);
                                eventBuilder.Skip(-1);
                            }
                            else if (hp < 0.65)
                            {
                                eventBuilder.Remove();
                                eventBuilder.IfCharacterHpRatio(instr2.resultConditionGroup, GetGael2(gaelMap, instr2.targetEntityId), instr2.comparisonType, instr2.targetHpRatio.Value / 0.65f, instr2.comparisonType2, instr2.numberOfTargetCharacters, instr2.layerMask);
                                eventBuilder.Skip(-1);
                            }
                            else if (hp < 1)
                            {
                                eventBuilder.Remove();
                                eventBuilder.IfCharacterHpRatio(instr2.resultConditionGroup, instr2.targetEntityId, instr2.comparisonType, instr2.targetHpRatio.Value / 0.65f, instr2.comparisonType2, instr2.numberOfTargetCharacters, instr2.layerMask);
                                eventBuilder.Skip(-1);
                            }
                        }
                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterHasSpeffect instr3)
                {
                    if (IsGael(paramMap, instr3.targetEntityId))
                    {
                        if (instr3.shouldHave.isParam)
                        {
                            Console.WriteLine(instr3);
                        }
                        else
                        {
                            // Common case: the game checks for the "sitting" special effect and forces a stand up animation if the enemy hasn't 
                            // already been knocked out of the sitting state
                            if (aggressive)
                            {
                                eventBuilder.Noop();
                                eventBuilder.Skip(-1);
                            }
                            else
                            {
                                if (instr3.shouldHave.Value)
                                {
                                    eventBuilder.Remove();
                                    eventBuilder.IfParameterComparison(instr3.resultConditionGroup, ComparisonType.Equal, 0, 0, instr3.layerMask);
                                    eventBuilder.Skip(-1);
                                }
                                else
                                {
                                    eventBuilder.Remove();
                                    eventBuilder.IfParameterComparison(instr3.resultConditionGroup, ComparisonType.Notequal, 0, 0, instr3.layerMask);
                                    eventBuilder.Skip(-1);
                                }
                            }
                        }
                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterDeadAlive instr4)
                {
                    if (IsGael(paramMap, instr4.targetEntityId))
                    {
                        instr4.targetEntityId = GetGael2(gaelMap, instr4.targetEntityId);
                        saveModification = true;

                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterAiState instr7)
                {
                    if (IsGael(paramMap, instr7.targetEntityId))
                    {
                        if (aggressive)
                        {
                            eventBuilder.Noop();
                            eventBuilder.Skip(-1);
                        }
                        else
                        {
                            eventBuilder.Remove();
                            eventBuilder.IfEntityInOutsideRadiusOfEntity(instr7.resultConditionGroup, InsideOutsideState.Inside, 10000, instr7.targetEntityId, Constants.GAEL_AGGRO_RANGE, (int)instr7.numberOfTargetCharacters.Value, instr7.layerMask);
                            eventBuilder.Skip(-1);
                        }
                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterHasEventMessage instr8)
                {
                    if (IsGael(paramMap, instr8.targetEntityId))
                    {
                        eventBuilder.Remove();
                        eventBuilder.IfParameterComparison(instr8.resultConditionGroup, ComparisonType.Equal, 0, 0, instr8.layerMask);
                        eventBuilder.Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.ChangeCharacterDispmask instr9)
                {
                    if (IsGael(paramMap, instr9.entityId))
                    {
                        eventBuilder.Noop();
                        eventBuilder.Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.DisplayBossHealthBar instr10)
                {
                    if (IsGael(paramMap, instr10.entityId))
                    {
                        if ((DisabledEnabled)Convert.ToInt32(GetParamValue(paramMap, instr10.disabledEnabled)) == DisabledEnabled.Enabled)
                        {
                            bossGaels.Add(Convert.ToInt32(GetParamValue(paramMap, instr10.entityId)));
                            bossGaelHpBarSlots.Add(Convert.ToInt16(GetParamValue(paramMap, instr10.slotNumber)));
                            bossGaelNameIds.Add(Convert.ToInt32(GetParamValue(paramMap, instr10.nameId)));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Warning: non-Gael boss hp bar");
                    }
                }
                return saveModification;
            };
        }

        static object GetParamValue<T>(Dictionary<int, object> paramMap, EventArg<T> param)
        {
            return param.isParam ? paramMap[(int)param.index] : param.Value;
        }
        static bool IsGael(Dictionary<int, object> paramMap, EventArg<int> entityId)
        {
            object paramValue = GetParamValue(paramMap, entityId);
            return gaelEntityIds.Contains(Convert.ToInt32(paramValue));
        }

        static bool IsEntity(Dictionary<int, object> paramMap, EventArg<int> entityId)
        {
            object paramValue = GetParamValue(paramMap, entityId);
            return allEntityIds.Contains(Convert.ToInt32(paramValue));
        }

        static EventArg<int> GetGael2(Dictionary<int, EventArg<int>> gaelMap, EventArg<int> entityId)
        {
            return entityId.isParam ? gaelMap[(int)entityId.index] : entityId.Value * 10;
        }
    }
}
