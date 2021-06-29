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
                eventBuilder.Seek(14).Noop() // player remove sword animation
                    .Seek(17).Remove<InstrWrapper.IfCharacterHasEventMessage>().IfParameterComparison(ConditionGroup.Or01, ComparisonType.Equal, 0, 0); // event message
            }
            else if (eventId == 14005812)
            {
                eventBuilder.Seek(1).Remove<InstrWrapper.IfCharacterHasEventMessage>(); // event message
            }
            else if (eventId == 14005810)
            {
                eventBuilder.Seek(24).Remove<InstrWrapper.IfCharacterHasEventMessage>().IfParameterComparison(ConditionGroup.Or01, ComparisonType.Equal, 0, 0); // event message
            }

            // level up
            else if (eventId == 20006005)
            {
                eventBuilder
                    .Seek(4)
                    .Modify(delegate (MyInstr myInstr) { ((InstrWrapper.IfEntityInOutsideRadiusOfEntity)myInstr).targetDistance = 5; })
                    .Seek(29)
                    .AssertType<InstrWrapper.SetEventFlag>()
                    .SetEventFlag(new EventArg<int>() { index = 1 }, OnOff.On)
                    .SetEventFlag(74000138, OnOff.On)
                    .SetEventFlag(74000392, OnOff.On)
                    .SetEventFlag(74000433, OnOff.On)
                    .SetSpeffect(new EventArg<int>() { index = 0 }, 150)
                    .Skip(15)
                    .AssertType<InstrWrapper.EndUnconditionally>()
                    .SetEventFlag(new EventArg<int>() { index = 1 }, OnOff.Off)
                    .SetEventFlag(74000138, OnOff.Off)
                    .SetEventFlag(74000392, OnOff.Off)
                    .SetEventFlag(74000433, OnOff.Off)
                    .ClearSpeffect(new EventArg<int>() { index = 0 }, 150);
            }

            // crystal sage (disabled because too jank)
            /*
            else if (eventId == 13305865)
            {
                eventBuilder
                    .Seek(1)
                    .Remove<InstrWrapper.IfEventFlag>() // remove flag check
                    .Seek(14);
                for (int i = 0; i < 4; i++)
                {
                    eventBuilder
                        .Remove<InstrWrapper.ForceAnimationPlayback>()
                        .Remove<InstrWrapper.RequestCharacterAiRePlan>(); // remove AI replans making clones stay idle
                }

                eventBuilder
                    .Seek(0)
                    .EndIfEventFlag(EventEndType.End, OnOff.On, TargetEventFlagType.EventFlag, 13300850); // prevent respawning after reload
            }
            else if (eventId == 13305860)
            {
                eventBuilder
                    .Seek(8)
                    .Remove<InstrWrapper.SetCharacterImmortality>()
                    .Remove<InstrWrapper.SetCharacterHpBarDisplay>(); // remove immortality from clones
            }
            */

            // high wall dragon
            else if (eventId == 13005481)
            {
                eventBuilder
                    .Seek(2).Modify(instr =>
                    {
                        ((InstrWrapper.IfCharacterHpRatio)instr).targetHpRatio = 0;
                    })
                    .Seek(12).Remove<InstrWrapper.ChangeCharacterEnableState>()
                    .Skip(4).Remove<InstrWrapper.ChangeCharacterEnableState>();
            }
            else if (eventId == 13005480)
            {
                eventBuilder.Seek(0).EndUnconditionally(EventEndType.End);
            }

            // lothric dragons
            else if (eventId == 13015550)
            {
                eventBuilder.Seek(10).Remove<InstrWrapper.SetCharacterImmortality>()
                    .Skip(7).Remove<InstrWrapper.SetCharacterImmortality>()
                    .Skip(5).Remove<InstrWrapper.SetCharacterImmortality>();
            }
            else if (eventId == 13015551)
            {
                eventBuilder.Seek(9).Remove<InstrWrapper.SetCharacterImmortality>();
            }

            // deacon AI
            else if (eventId == 13505810)
            {
                eventBuilder.Seek(29).Modify(myInstr =>
                {
                    ((InstrWrapper.SetCharacterAiState)myInstr).entityId = 3505800;
                }); // disable AI
            }

            // soul of cinder
            else if (eventId == 14105811)
            {
                eventBuilder.Seek(2).Remove<InstrWrapper.SetCharacterImmortality>(); // immortality
            }

            // gael
            else if (eventId == 15115810)
            {
                eventBuilder.Skip(9999).Skip(-4).Remove<InstrWrapper.CreateReferredDamagePair>() // referred damage pair
                    .Skip(9999).Skip(-2).Modify(myInstr =>
                    {
                        ((InstrWrapper.DisplayBossHealthBar)myInstr).entityId = 5110801;
                    }); // set health bar
            }
            else if (eventId == 15115811)
            {
                eventBuilder.Seek(30).AssertType<InstrWrapper.WarpCharacterAndCopyFloor>()
                    .DisplayBossHealthBar(DisabledEnabled.Enabled, 5110800, 0, 906200);
            }

            // abyss watchers
            else if (eventId == 13305810)
            {
                eventBuilder.Seek(7).SetCharacterAiState(3305800, DisabledEnabled.Disabled)
                    .Skip(9999).Skip(-9).SetCharacterAiState(3305800, DisabledEnabled.Enabled);

            }

            // wolnir
            else if (eventId == 13805820)
            {
                eventBuilder.Seek(1).Remove(); // event message
            }
            /*
            // also need to disable flag check at beginning
            // fight is more unique with just one Gael so disable this
            else if (eventId == 13804815)
            {
                eventBuilder
                    .Seek(13).Modify(instr =>
                    {
                        ((InstrWrapper.WarpCharacterAndCopyFloor)instr).dummypolyId = 260;
                    })
                    .Seek(6).Remove<InstrWrapper.IfCharacterHasEventMessage>()
                    .IfCharacterHpRatio(ConditionGroup.Main, 3800800, ComparisonType.Less, 0.5f, ComparisonType.Equal, 1);
            }
            */
            else if (eventId == 13805809)
            {
                eventBuilder.Skip(9999).Skip(-31);
                for (int i = 0; i < 9; i++)
                {
                    eventBuilder.Remove<InstrWrapper.SetSpeffect>();
                }
                eventBuilder.Skip(1);
                for (int i = 0; i < 11; i++)
                {
                    eventBuilder.Remove<InstrWrapper.SetSpeffect>();
                }
            }

            // demons
            else if (eventId == 15005800)
            {
                // end fight if both die
                eventBuilder.Skip(1).Remove().Skip(1).Remove()
                    .Skip(1).Modify(instr =>
                    {
                        ((InstrWrapper.IfConditionGroup)instr).resultConditionGroup = ConditionGroup.And03;
                    })
                    .Modify(instr =>
                    {
                        ((InstrWrapper.IfConditionGroup)instr).resultConditionGroup = ConditionGroup.And03;
                    })
                    .Modify(instr =>
                    {
                        ((InstrWrapper.IfConditionGroup)instr).targetConditionGroup = ConditionGroup.And03;
                    });
            }
            else if (eventId == 15005810)
            {
                eventBuilder.Seek(10).Remove<InstrWrapper.SetCharacterImmortality>()
                    .Seek(6).Remove<InstrWrapper.SetCharacterImmortality>() // remove immortality
                    .Skip(9999).Skip(-12).Remove<InstrWrapper.CreateReferredDamagePair>(); // referred damage
            }
            else if (eventId == 15005811)
            {
                eventBuilder.Seek(1).Remove<InstrWrapper.SetCharacterImmortality>(); // immortality
            }

            // friede
            else if (eventId == 14505811)
            {
                eventBuilder.Skip(9999).Skip(-2).Remove<InstrWrapper.CreateReferredDamagePair>() // remove referred damage
                    .Seek(38).AssertType<InstrWrapper.DisplayBossHealthBar>()
                    .DisplayBossHealthBar(DisabledEnabled.Enabled, 4500800, 1, 906022);
            }
            else if (eventId == 14500800)
            {
                // end fight after both die
                eventBuilder.Seek(2).IfCharacterHpRatio(ConditionGroup.Main, 4500802, ComparisonType.Equal, 0, ComparisonType.Equal, 1);
            }
            else if (eventId == 14505813)
            {
                eventBuilder.Seek(13).Remove<InstrWrapper.DisplayBossHealthBar>();
            }
            else if (eventId == 14505812)
            {
                eventBuilder.Seek(10).Remove<InstrWrapper.DisplayBossHealthBar>();
            }

            // siegward (demon fight)
            else if (eventId == 13105742)
            {
                eventBuilder.Seek(1).Remove<InstrWrapper.IfCharacterAiState>();
            }
            else if (eventId == 13105746)
            {
                eventBuilder.Seek(6).Remove<InstrWrapper.SetEventFlag>();
            }
            else if (eventId == 13105747)
            {
                eventBuilder.Seek(5).Remove<InstrWrapper.SetEventFlag>();
            }

            // siegward (estus soup)
            else if (eventId == 13705661)
            {
                eventBuilder.Remove<InstrWrapper.IfCharacterHasSpeffect>()
                    .Skip(5).Remove<InstrWrapper.IfCharacterHasSpeffect>();
            }
            else if (eventId == 13705662)
            {
                eventBuilder.Seek(3).Remove<InstrWrapper.ForceAnimationPlayback>()
                    .Skip(2).Remove<InstrWrapper.SetEventFlag>();
            }
            else if (eventId == 13705663)
            {
                eventBuilder.Seek(3).Remove<InstrWrapper.SetEventFlag>();
            }

            // siegward (yhorm)
            else if (eventId == 13905727)
            {
                eventBuilder.Seek(3).Remove<InstrWrapper.SetEventFlag>();
            }

            // archdragon peak summoner
            else if (eventId == 13205350)
            {
                eventBuilder.Seek(2).Remove<InstrWrapper.IfCharacterHasEventMessage>()
                    .IfCharacterAiState(ConditionGroup.And01, new EventArg<int> { index = 0 }, AiStateType.Combat, ComparisonType.Equal, 1)
                    .Skip(9999).Skip(-1).Remove<InstrWrapper.EndUnconditionally>();
            }
            else if (eventId == 13205360 || eventId == 13205370 || eventId == 13205375)
            {
                eventBuilder.Skip(9999).Skip(-1).Remove<InstrWrapper.EndUnconditionally>();
            }

            // big wolves
            else if (eventId == 14505201)
            {
                eventBuilder.Seek(15).Remove<InstrWrapper.SetCharacterImmortality>();
            }
            else if (eventId == 14505211)
            {
                eventBuilder.Seek(7).Remove<InstrWrapper.SetCharacterImmortality>();
            }

            // gravetender
            else if (eventId == 14505870)
            {
                if (aggressive)
                {
                    // start fight when character in area or damages someone instead of checking AI state
                    eventBuilder.Seek(9).Modify(instr =>
                    {
                        ((InstrWrapper.SetCharacterAiState)instr).entityId = 4505860;
                    })
                    .Skip(5)
                    .IfDamageType(ConditionGroup.Or01, 4500263, -1, DamageType.Unspecified)
                    .IfDamageType(ConditionGroup.Or01, 4500264, -1, DamageType.Unspecified)
                    .IfDamageType(ConditionGroup.Or01, 4500265, -1, DamageType.Unspecified)
                    .Remove<InstrWrapper.IfCharacterAiState>()
                    .Skip(10).Modify(instr =>
                    {
                        ((InstrWrapper.SetCharacterAiState)instr).entityId = 4505860;
                    });
                }
            }

            // yuria
            else if (eventId == 13705647)
            {
                eventBuilder.Seek(11).Remove<InstrWrapper.EndIfConditionGroupStateCompiled>();
            }
            else if (eventId == 13705644)
            {
                eventBuilder.Skip(9999).ForceCharacterDeath(3700721, true);
            }

            // dancer maiden
            else if (eventId == 13005601)
            {
                eventBuilder.Seek(1).SetCharacterImmortality(3000705, DisabledEnabled.Disabled)
                    .Skip(9999).ForceCharacterDeath(3000705, true);
            }
            else if (eventId == 13005603)
            {
                eventBuilder.Skip(5).Remove<InstrWrapper.ForceAnimationPlayback>();
            }

            // lapp
            else if (eventId == 15105715)
            {
                eventBuilder.Seek(6).Remove<InstrWrapper.SetEventFlag>();
            }

            // midir
            else if (eventId == 15105231)
            {
                eventBuilder.Seek(1).Modify(instr =>
                    {
                        ((InstrWrapper.IfCharacterHpRatio)instr).targetHpRatio = 0;
                    })
                    .Skip(9999).SetSpeffect(5100200, 5028);
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
                    .Remove<InstrWrapper.IfCharacterHasEventMessage>()
                    .IfCharacterHpRatio(ConditionGroup.Main, 3100800, ComparisonType.Equal, 0, ComparisonType.Equal, 1);
            }
            else if (eventId == 13105814)
            {
                eventBuilder
                    .Seek(3)
                    .Remove<InstrWrapper.IfCharacterHasEventMessage>()
                    .IfCharacterHpRatio(ConditionGroup.Main, 3100800, ComparisonType.Equal, 0, ComparisonType.Equal, 1);
            }

            // Crystal Sage
            /*
            if (eventId == 13305861)
            {
                eventBuilder
                    .Remove<InstrWrapper.IfCharacterHasEventMessage>() // event message
                    .IfCharacterHpRatio(ConditionGroup.Main, 3300850, ComparisonType.Equal, 0, ComparisonType.Equal, 1);
            }
            */

            // abyss watchers
            else if (eventId == 13305814)
            {
                eventBuilder.Seek(2).Remove<InstrWrapper.IfCharacterHasSpeffect>()
                    .IfCharacterHpRatio(ConditionGroup.And01, 3300801, ComparisonType.Equal, 0, ComparisonType.Equal, 1); // change sp effect to hp check
            }

            //gravetender
            else if (eventId == 14505871)
            {
                eventBuilder.Seek(12).Modify(instr =>
                {
                    InstrWrapper.IfCharacterHpRatio myInstr = (InstrWrapper.IfCharacterHpRatio)instr;
                    myInstr.comparisonType = ComparisonType.Equal;
                    myInstr.targetHpRatio = 0;
                });
            }

            //siegward (demon)
            else if (eventId == 13105742)
            {
                eventBuilder.Seek(20).AssertType<InstrWrapper.SetCharacterEventTarget>()
                    .SetCharacterAiId(new EventArg<int> { index = 0 }, Constants.GAEL_PHASE1_PARAM_ID)
                    .SetCharacterAiId(new EventArg<int> { index = 2 }, Constants.GAEL_PHASE2_PARAM_ID)
                    .Skip(9999).Skip(-5).AssertType<InstrWrapper.SetEventFlag>()
                    .SetCharacterAiId(new EventArg<int> { index = 0 }, 26201)
                    .SetCharacterAiId(new EventArg<int> { index = 2 }, 26201);
            }

            //siegward (yhorm)
            else if (eventId == 13905811)
            {
                eventBuilder.Seek(2).SetCharacterAiId(3900706, 26204);
            }

            // hodrick
            else if (eventId == 13105810)
            {
                eventBuilder.Skip(9999).SetCharacterGravity(3100952, DisabledEnabled.Disabled)
                    .WarpCharacterAndCopyFloor(3100740, TargetEntityType.Character, 3100952, 20, 3100952); // warp hodrick away when CRGW fight starts
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
                        eventBuilder.Noop().Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.AbstractDisplayBossHealthBar instr10)
                {
                    startEnabledEnemies.Remove((int)GetParamValue(initInstr, instr10.entityId));
                    if (!instr10.nameId.isParam && instr10.nameId.Value == 905022 || instr10.nameId.Value == 905023) // extra demon health bars
                        return false;
                    if (eventBuilder.event_.ID == 15105812 && !instr10.nameId.isParam && instr10.nameId.Value == 906022) // black flame friede health bar given to halflight?
                        return false;
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
                        Console.WriteLine("Warning: non-Gael boss hp bar: " + instr10.entityId.Value);
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
                        if (!instr2.targetHpRatio.isParam)
                        {
                            float hp = instr2.targetHpRatio.Value;
                            if (hp <= 0.1)
                            {
                                instr2.targetEntityId = GetGael2(gaelMap, instr2.targetEntityId);
                                instr2.modified = true;
                            }
                            /*
                            if (hp < 0.65)
                            {
                                instr2.targetEntityId = GetGael2(gaelMap, instr2.targetEntityId);
                                instr2.targetHpRatio = instr2.targetHpRatio.Value / 0.65f;
                                instr2.modified = true;
                            }
                            else if (hp < 1)
                            {
                                eventBuilder.Remove();
                                eventBuilder.IfCharacterHpRatio(instr2.resultConditionGroup, instr2.targetEntityId, instr2.comparisonType, (instr2.targetHpRatio.Value - 0.65f) / 0.35f, instr2.comparisonType2, instr2.numberOfTargetCharacters, instr2.layerMask);
                                eventBuilder.Skip(-1);
                            }
                            */
                        }
                    }
                }
                else if (myInstr is InstrWrapper.ForceCharacterDeath instr11)
                {
                    if (IsGael(initInstr, instr11.entityId))
                    {
                        eventBuilder.ShiftSkips(1)
                            .Skip(1)
                            .ForceCharacterDeath(GetGael2(gaelMap, instr11.entityId), instr11.shouldReceiveSouls, instr11.layerMask)
                            .Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.SetCharacterTeamType instr14)
                {
                    if (IsGael(initInstr, instr14.entityId))
                    {
                        if (!instr14.teamType.isParam)
                        {
                            TeamType team = (TeamType)GetParamValue(initInstr, instr14.teamType);
                            if (team == TeamType.HostileNpc || team == TeamType.HostileAlly || team == TeamType.Hostile || team == TeamType.WhitePhantom)
                            {
                                eventBuilder.ShiftSkips(3)
                                    .Skip(1)
                                    .SetCharacterAiId(instr14.entityId, 620100)
                                    .SetCharacterAiId(GetGael2(gaelMap, instr14.entityId), 620000)
                                    .SetCharacterTeamType(GetGael2(gaelMap, instr14.entityId), instr14.teamType)
                                    .Skip(-1);
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
                                eventBuilder.ShiftSkips(1)
                                    .Skip(1)
                                    .ChangeCharacterEnableState(GetGael2(gaelMap, instr12.entityId), DisabledEnabled.Disabled)
                                    .Skip(-1);
                            }
                        }
                    }
                }
                else if (myInstr is InstrWrapper.SetCharacterBackreadState instr13)
                {
                    if (IsGael(initInstr, instr13.entityId))
                    {
                        eventBuilder.ShiftSkips(1)
                            .Skip(1)
                            .SetCharacterBackreadState(GetGael2(gaelMap, instr13.entityId), instr13.removed, instr13.layerMask)
                            .Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.SetCharacterAiState instr15)
                {
                    if (IsGael(initInstr, instr15.entityId))
                    {
                        eventBuilder.ShiftSkips(1)
                            .Skip(1)
                            .SetCharacterAiState(GetGael2(gaelMap, instr15.entityId), instr15.disabledEnabled, instr15.layerMask)
                            .Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.SetCharacterImmortality instr16)
                {
                    if (IsGael(initInstr, instr16.entityId))
                    {
                        instr16.entityId = GetGael2(gaelMap, instr16.entityId);
                        instr16.modified = true;
                    }
                }

                else if (myInstr is InstrWrapper.SetSpeffect instr17)
                {
                    if (IsGael(initInstr, instr17.entityId))
                    {
                        eventBuilder.ShiftSkips(1)
                            .Skip(1)
                            .SetSpeffect(GetGael2(gaelMap, instr17.entityId), instr17.speffectId)
                            .Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.ClearSpeffect instr18)
                {
                    if (IsGael(initInstr, instr18.entityId))
                    {
                        eventBuilder.ShiftSkips(1)
                            .Skip(1)
                            .ClearSpeffect(GetGael2(gaelMap, instr18.entityId), instr18.speffectId)
                            .Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.AbstractPlaceNpcSummonSign instr19)
                {
                    if (IsGael(initInstr, instr19.summonedNpcEntityId))
                    {
                        if (instr19.signType.Value == SummonSignType.BlackSign)
                        {
                            eventBuilder.Remove().SetEventFlag(instr19.summonEventFlagId, OnOff.On).Skip(-1);
                        }
                    }
                }
                else if (myInstr is InstrWrapper.SetCharacterAiId instr20)
                {
                    if (IsGael(initInstr, instr20.entityId))
                    {
                        if (!friendlyEntityIds.Contains((int)GetParamValue(initInstr, instr20.entityId)))
                        {
                            instr20.entityId = -1;
                            instr20.modified = true;
                        }
                    }
                }
#if false
                else if (myInstr is InstrWrapper.SetSpeffect instr17)
                {
                    if (IsGael(initInstr, instr17.entityId))
                    {
                        eventBuilder.ShiftSkips(1)
                            .Skip(1)
                            .SetSpeffect(GetGael2(gaelMap, instr17.entityId), instr17.speffectId)
                            .Skip(-1);
                    }
                }
                else if (myInstr is InstrWrapper.ClearSpeffect instr18)
                {
                    if (IsGael(initInstr, instr18.entityId))
                    {
                        eventBuilder.ShiftSkips(1)
                            .Skip(1)
                            .ClearSpeffect(GetGael2(gaelMap, instr18.entityId), instr18.speffectId)
                            .Skip(-1);
                    }
                }



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
