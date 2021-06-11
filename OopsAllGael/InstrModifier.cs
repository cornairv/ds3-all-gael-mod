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
            // Iudyx Gundyr
            if (eventId == 14005811)
            {
                eventBuilder.Seek(14).Noop(); // player remove sword animation
                eventBuilder.Seek(17).Remove().IfParameterComparison(ConditionGroup.Or01, ComparisonType.Equal, 0, 0); // event message
            }
            else if (eventId == 14005812)
            {
                eventBuilder.Seek(1).Noop(); // event message
            }
            else if (eventId == 14005810)
            {
                eventBuilder.Seek(24).Remove().IfParameterComparison(ConditionGroup.Or01, ComparisonType.Equal, 0, 0); // event message
            }

            // level up
            else if (eventId == 20006005)
            {
                eventBuilder
                    .Seek(4)
                    .Modify(delegate (MyInstr myInstr) { ((InstrWrapper.IfEntityInOutsideRadiusOfEntity)myInstr).targetDistance = 5; })
                    .Seek(29)
                    .SetEventFlag(new EventArg<int>() { index = 1 }, OnOff.On)
                    .SetEventFlag(74000138, OnOff.On)
                    .SetEventFlag(74000392, OnOff.On)
                    .SetEventFlag(74000433, OnOff.On)
                    .SetSpeffect(new EventArg<int>() { index = 0 }, 150)
                    .Skip(15)
                    .SetEventFlag(new EventArg<int>() { index = 1 }, OnOff.Off)
                    .SetEventFlag(74000138, OnOff.Off)
                    .SetEventFlag(74000392, OnOff.Off)
                    .SetEventFlag(74000433, OnOff.Off)
                    .ClearSpeffect(new EventArg<int>() { index = 0 }, 150);
            }

            return false;
        }

        static void PostprocessEvent(EventBuilder eventBuilder, int eventId)
        {
            // CRGW
            if (eventId == 13105811)
            {
                eventBuilder
                    .Seek(1)
                    .Remove()
                    .IfCharacterHpRatio(ConditionGroup.Main, 3100800, ComparisonType.Equal, 0, ComparisonType.Equal, 1);
            }
            else if (eventId == 13105814)
            {
                eventBuilder
                    .Seek(3)
                    .Remove()
                    .IfCharacterHpRatio(ConditionGroup.Main, 3100800, ComparisonType.Equal, 0, ComparisonType.Equal, 1);
            }
        }

        // Modify one instruction in an event, with paramMap mapping the argument positions to their values
        // paramMap: location in event parameters => parameter value
        // gaelMap: location in event parameters => gael phase 2 id
        static Predicate<MyInstr> InstrModifier(EventBuilder eventBuilder, Dictionary<int, EventArg<int>> gaelMap, InstrWrapper.AbstractInitializeEvent initInstr, bool alreadyVisited)
        {
            return delegate (MyInstr myInstr)
            {
                if (myInstr is InstrWrapper.ForceAnimationPlayback instr)
                {
                    if (IsGael(initInstr, instr.entityId))
                    {
                        if (instr.animationId.isParam)
                        {
                            int animId = initInstr.parameters[(int)instr.animationId.index].Value;
                            if (!gaelAnimIds.Contains(animId))
                            {
                                initInstr.parameters[(int)instr.animationId.index] = (animId % Constants.ANIM_OFFSET) + Constants.ANIM_OFFSET;
                                initInstr.modified = true;
                            }
                        }
                        else
                        {
                            int animId = instr.animationId.Value;
                            if (!gaelAnimIds.Contains(animId))
                            {
                                instr.animationId = (animId % Constants.ANIM_OFFSET) + Constants.ANIM_OFFSET;
                                instr.modified = true;
                            }
                        }
                    }

                }
                if (alreadyVisited)
                {
                    return myInstr.modified;
                }

                // everything else only happens once per event
                
                if (myInstr is InstrWrapper.ChangeCharacterDispmask instr9)
                {
                    if (IsGael(initInstr, instr9.entityId))
                    {
                        eventBuilder.Noop();
                        eventBuilder.Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.DisplayBossHealthBar instr10)
                {
                    if (IsGael(initInstr, instr10.entityId))
                    {
                        if ((DisabledEnabled)GetParamValue(initInstr, instr10.disabledEnabled) == DisabledEnabled.Enabled)
                        {
                            bossGaels.Add((int)GetParamValue(initInstr, instr10.entityId));
                            bossGaelHpBarSlots.Add((short)GetParamValue(initInstr, instr10.slotNumber));
                            bossGaelNameIds.Add((int)GetParamValue(initInstr, instr10.nameId));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Warning: non-Gael boss hp bar");
                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterDeadAlive instr4)
                {
                    if (IsGael(initInstr, instr4.targetEntityId))
                    {
                        instr4.targetEntityId = GetGael2(gaelMap, instr4.targetEntityId);
                        instr4.modified = true;
                    }
                }
                else if (myInstr is InstrWrapper.IfCharacterHpRatio instr2)
                {
                    if (IsGael(initInstr, instr2.targetEntityId))
                    {
                        if (instr2.targetHpRatio.isParam)
                        {
                            Console.Write(GetParamValue(initInstr, instr2.targetHpRatio) + " -> ");
                            Console.WriteLine(BitConverter.ToSingle(BitConverter.GetBytes((int)GetParamValue(initInstr, instr2.targetHpRatio)), 0));
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
                else if (myInstr is InstrWrapper.ForceCharacterDeath instr11)
                {
                    if (IsGael(initInstr, instr11.entityId))
                    {
                        eventBuilder.Skip(1);
                        eventBuilder.ForceCharacterDeath(GetGael2(gaelMap, instr11.entityId), instr11.shouldReceiveSouls, instr11.layerMask);
                        eventBuilder.Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.SetCharacterTeamType instr14)
                {
                    if (IsGael(initInstr, instr14.entityId))
                    {
                        if (!instr14.teamType.isParam)
                        {
                            TeamType team = (TeamType)GetParamValue(initInstr, instr14.teamType);
                            if (team == TeamType.HostileNpc || team == TeamType.HostileAlly || team == TeamType.Hostile)
                            {
                                eventBuilder.Skip(1);
                                eventBuilder.SetCharacterAiId(instr14.entityId, 620100);
                                eventBuilder.Skip(-1);
                            }
                        }
                    }
                }
                else if (myInstr is InstrWrapper.ChangeCharacterEnableState instr12)
                {
                    if (IsGael(initInstr, instr12.entityId))
                    {
                        if (!instr12.disabledEnabled.isParam)
                        {
                            if (instr12.disabledEnabled.Value == DisabledEnabled.Disabled)
                            {
                                eventBuilder.Skip(1);
                                eventBuilder.ChangeCharacterEnableState(GetGael2(gaelMap, instr12.entityId), DisabledEnabled.Disabled);
                                eventBuilder.Skip(-1);
                            }
                        }
                    }
                }
                else if (myInstr is InstrWrapper.SetCharacterBackreadState instr13)
                {
                    if (IsGael(initInstr, instr13.entityId))
                    {
                        eventBuilder.Skip(1);
                        eventBuilder.SetCharacterBackreadState(GetGael2(gaelMap, instr13.entityId), instr13.removed, instr13.layerMask);
                        eventBuilder.Skip(-1);
                    }
                }
#if false
                else if (myInstr is InstrWrapper.IfCharacterHpRatio instr2)
                {
                    if (IsGael(paramMap, instr2.targetEntityId))
                    {
                        if (instr2.targetHpRatio.isParam)
                        {
                            Console.Write(GetParamValue(paramMap, instr2.targetHpRatio) + " -> ");
                            Console.WriteLine(BitConverter.ToSingle(BitConverter.GetBytes((int)GetParamValue(paramMap, instr2.targetHpRatio)), 0));
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
                        if ((int)GetParamValue(paramMap, instr8.targetEntityId) == 4000800) // iudyx gundyr
                        {
                            eventBuilder.Remove();
                            eventBuilder.IfParameterComparison(instr8.resultConditionGroup, ComparisonType.Equal, 0, 0, instr8.layerMask);
                            eventBuilder.Skip(-1);
                        }
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
                        if ((DisabledEnabled)GetParamValue(paramMap, instr10.disabledEnabled) == DisabledEnabled.Enabled)
                        {
                            bossGaels.Add((int)GetParamValue(paramMap, instr10.entityId));
                            bossGaelHpBarSlots.Add((short)GetParamValue(paramMap, instr10.slotNumber));
                            bossGaelNameIds.Add((int)GetParamValue(paramMap, instr10.nameId));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Warning: non-Gael boss hp bar");
                    }
                }
                else if (myInstr is InstrWrapper.ForceCharacterDeath instr11)
                {
                    if (IsGael(paramMap, instr11.entityId))
                    {
                        eventBuilder.Skip(1);
                        eventBuilder.ForceCharacterDeath(GetGael2(gaelMap, instr11.entityId), instr11.shouldReceiveSouls, instr11.layerMask);
                        eventBuilder.Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.ChangeCharacterEnableState instr12)
                {
                    /*
                    if (IsGael(paramMap, instr12.entityId))
                    {
                        if (!instr12.disabledEnabled.isParam)
                        {
                            if (instr12.disabledEnabled.Value == DisabledEnabled.Disabled)
                            {
                                eventBuilder.Skip(1);
                                eventBuilder.ChangeCharacterEnableState(GetGael2(gaelMap, instr12.entityId), DisabledEnabled.Disabled);
                                eventBuilder.Skip(-1);
                            }
                        }
                    }
                    */
                }
                else if (myInstr is InstrWrapper.RotateCharacter instr13)
                {
                    /*
                    eventBuilder.Remove();
                    eventBuilder.ActivateHit(instr13.entityId, DisabledEnabled.Disabled, instr13.layerMask);
                    eventBuilder.ActivateHit(instr13.entityId2, DisabledEnabled.Disabled, instr13.layerMask);
                    eventBuilder.RotateCharacter(instr13.entityId, instr13.entityId2, instr13.playAnimationId, true, instr13.layerMask);
                    eventBuilder.ActivateHit(instr13.entityId, DisabledEnabled.Enabled, instr13.layerMask);
                    eventBuilder.ActivateHit(instr13.entityId2, DisabledEnabled.Enabled, instr13.layerMask);
                    eventBuilder.Skip(-1);
                    */
                    eventBuilder.Noop();
                    eventBuilder.Skip(-1);
                }
                else if (myInstr is InstrWrapper.SetCharacterTeamType instr14)
                {
                    if (IsGael(paramMap, instr14.entityId))
                    {
                        if (!instr14.teamType.isParam)
                        {
                            TeamType team = (TeamType)GetParamValue(paramMap, instr14.teamType);
                            if (team == TeamType.HostileNpc || team == TeamType.HostileAlly || team == TeamType.Hostile)
                            {
                                eventBuilder.Skip(1);
                                eventBuilder.SetCharacterAiId(instr14.entityId, 620100);
                                eventBuilder.Skip(-1);
                            }
                        }
                    }
                }
#endif
                return myInstr.modified;
            };
        }

        static object GetParamValue<T>(InstrWrapper.AbstractInitializeEvent initInstr, EventArg<T> param)
        {
            return param.isParam ? (object)initInstr.parameters[(int)param.index].Value : param.Value;
        }

        static bool IsGael(InstrWrapper.AbstractInitializeEvent initInstr, EventArg<int> entityId)
        {
            object paramValue = GetParamValue(initInstr, entityId);
            return gaelEntityIds.ContainsKey(Convert.ToInt32(paramValue));
        }

        static bool IsEntity(InstrWrapper.AbstractInitializeEvent initInstr, EventArg<int> entityId)
        {
            object paramValue = GetParamValue(initInstr, entityId);
            return allEntityIds.Contains(Convert.ToInt32(paramValue));
        }

        static EventArg<int> GetGael2(Dictionary<int, EventArg<int>> gaelMap, EventArg<int> entityId)
        {
            return entityId.isParam ? gaelMap[(int)entityId.index] : gaelEntityIds[entityId.Value];
        }
    }
}
