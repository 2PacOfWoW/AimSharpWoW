using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Drawing;
using AimsharpWow.API; //needed to access Aimsharp API


namespace AimsharpWow.Modules
{
    /// <summary>
    /// This is an example rotation. It is a garbage rotation.  Just trying to show some examples of using the Aimsharp API.
    /// Check API-DOC for detailed documentation.
    /// </summary>
    public class DestructionLock : Rotation
    {


        public override void LoadSettings()
        {
            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Shiver Venom Relic", "Forbidden Obsidian Claw", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "Bloodelf", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Crashing Chaos Traits", 0, 3, 1));

        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            // Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Perfect Simcraft Series: Destruction Warlock - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("This rotation will use Havoc on your focus target, so please make a macro to set focus quickly.", Color.Blue);
            Aimsharp.PrintMessage("These macros can be used for manual control:", Color.Blue);
            Aimsharp.PrintMessage("/xxxxx Potions", Color.Blue);
            Aimsharp.PrintMessage("--Toggles using buff potions on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx SaveCooldowns", Color.Blue);
            Aimsharp.PrintMessage("--Toggles the use of big cooldowns on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx AOE", Color.Blue);
            Aimsharp.PrintMessage("--Toggles AOE mode on/off.", Color.Blue);
            //Aimsharp.PrintMessage(" ");
            // Aimsharp.PrintMessage("/xxxxx Prepull 10", Color.Blue);
            // Aimsharp.PrintMessage("--Starts the prepull actions.", Color.Blue);
            // Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("--Replace xxxxx with first 5 letters of your addon, lowercase.", Color.Blue);

            Aimsharp.Latency = 50;
            Aimsharp.QuickDelay = 125;
            Aimsharp.SlowDelay = 250;

            MajorPower = GetDropDown("Major Power");
            TopTrinket = GetDropDown("Top Trinket");
            BotTrinket = GetDropDown("Bot Trinket");
            RacialPower = GetDropDown("Racial Power");

            Spellbook.Add(MajorPower);

            if (RacialPower == "Orc")
                Spellbook.Add("Blood Fury");
            if (RacialPower == "Troll")
                Spellbook.Add("Berserking");
            if (RacialPower == "Dark Iron Dwarf")
                Spellbook.Add("Fireblood");
            if (RacialPower == "Mag'har Orc")
                Spellbook.Add("Ancestral Call");
            if (RacialPower == "Lightforged Draenei")
                Spellbook.Add("Light's Judgment");
            if (RacialPower == "Bloodelf")
                Spellbook.Add("Arcane Torrent");

            Spellbook.Add("Havoc");
            Spellbook.Add("Conflagrate");
            Spellbook.Add("Immolate");
            Spellbook.Add("Chaos Bolt");
            Spellbook.Add("Soul Fire");
            Spellbook.Add("Shadowburn");
            Spellbook.Add("Incinerate");
            Spellbook.Add("Cataclysm");
            Spellbook.Add("Rain of Fire");
            Spellbook.Add("Channel Demonfire");
            Spellbook.Add("Summon Infernal");
            Spellbook.Add("Dark Soul: Instability");

            Buffs.Add("Bloodlust");
            Buffs.Add("Heroism");
            Buffs.Add("Time Warp");
            Buffs.Add("Ancient Hysteria");
            Buffs.Add("Netherwinds");
            Buffs.Add("Drums of Rage");
            Buffs.Add("Lifeblood");
            Buffs.Add("Memory of Lucid Dreams");
            Buffs.Add("Reckless Force");
            Buffs.Add("Guardian of Azeroth");

            Buffs.Add("Backdraft");
            Buffs.Add("Chaotic Inferno");
            Buffs.Add("Crashing Chaos");
            Buffs.Add("Dark Soul: Instability");
            Buffs.Add("Grimoire of Supremacy");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");

            Debuffs.Add("Havoc");
            Debuffs.Add("Immolate");
            Debuffs.Add("Eradication");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));

            Macros.Add("havoc focus", "/cast [@focus] Havoc");
            Macros.Add("cata cursor", "/cast [@cursor] Cataclysm");
            Macros.Add("rof cursor", "/cast [@cursor] Rain of Fire");
            Macros.Add("inf cursor", "/cast [@cursor] Summon Infernal");

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            // CustomCommands.Add("Prepull");
        }





        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {

            bool Fighting = Aimsharp.Range("target") <= 40 && Aimsharp.TargetIsEnemy();
            bool Moving = Aimsharp.PlayerIsMoving();
            float Haste = Aimsharp.Haste() / 100f;
            int Time = Aimsharp.CombatTime();
            int Range = Aimsharp.Range("target");
            int TargetHealth = Aimsharp.Health("target");
            string LastCast = Aimsharp.LastCast();
            bool IsChanneling = Aimsharp.IsChanneling("player");
            string PotionType = GetDropDown("Potion Type");
            bool UsePotion = Aimsharp.IsCustomCodeOn("Potions");
            bool NoCooldowns = Aimsharp.IsCustomCodeOn("SaveCooldowns");
            bool AOE = Aimsharp.IsCustomCodeOn("AOE");
            int EnemiesInMelee = Aimsharp.EnemiesInMelee();
            int EnemiesNearTarget = Aimsharp.EnemiesNearTarget();
            int GCDMAX = (int)(1500f / (Haste + 1f));
            int GCD = Aimsharp.GCD();
            int Latency = Aimsharp.Latency;
            int TargetTimeToDie = 1000000000;
            bool HasLust = Aimsharp.HasBuff("Bloodlust", "player", false) || Aimsharp.HasBuff("Heroism", "player", false) || Aimsharp.HasBuff("Time Warp", "player", false) || Aimsharp.HasBuff("Ancient Hysteria", "player", false) || Aimsharp.HasBuff("Netherwinds", "player", false) || Aimsharp.HasBuff("Drums of Rage", "player", false);
            int FlameFullRecharge = (int)(Aimsharp.RechargeTime("Concentrated Flame") - GCD + (30000f) * (1f - Aimsharp.SpellCharges("Concentrated Flame")));
            int ShiverVenomStacks = Aimsharp.DebuffStacks("Shiver Venom");

            if (!AOE)
            {
                EnemiesNearTarget = 1;
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }

            int CDGuardianOfAzerothRemains = Aimsharp.SpellCooldown("Guardian of Azeroth") - GCD;
            bool BuffGuardianOfAzerothUp = Aimsharp.HasBuff("Guardian of Azeroth");
            int CDBloodOfTheEnemyRemains = Aimsharp.SpellCooldown("Blood of the Enemy") - GCD;
            int BuffMemoryOfLucidDreamsRemains = Aimsharp.BuffRemaining("Memory of Lucid Dreams") - GCD;
            bool BuffMemoryOfLucidDreamsUp = BuffMemoryOfLucidDreamsRemains > 0;
            bool DebuffRazorCoralUp = Aimsharp.HasDebuff("Razor Coral");
            bool DebuffConductiveInkUp = Aimsharp.HasDebuff("Conductive Ink");
            int BuffRecklessForceRemains = Aimsharp.BuffRemaining("Reckless Force") - GCD;
            bool BuffRecklessForceUp = BuffRecklessForceRemains > 0;
            int BuffRecklessForceStacks = Aimsharp.BuffStacks("Reckless Force");

            float SoulShard = Aimsharp.PlayerSecondaryPower() / 10f;

            bool TalentInfernoEnabled = Aimsharp.Talent(4, 1);
            bool TalentInternalCombustionEnabled = Aimsharp.Talent(2, 2);
            bool TalentFireAndBrimstoneEnabled = Aimsharp.Talent(4, 2);
            bool TalentGrimoireOfSupremacyEnabled = Aimsharp.Talent(6, 2);
            bool TalentCataclysmEnabled = Aimsharp.Talent(4, 3);
            bool TalentDarkSoulInstabilityEnabled = Aimsharp.Talent(7, 3);
            bool TalentFlashoverEnabled = Aimsharp.Talent(1, 1);
            bool TalentEradicationEnabled = Aimsharp.Talent(1, 2);

            int DebuffEradicationRemains = Aimsharp.DebuffRemaining("Eradication") - GCD;
            bool DebuffEradicationUp = DebuffEradicationRemains > 0;
            int DebuffImmolateRemains = Aimsharp.DebuffRemaining("Immolate") - GCD;
            bool DebuffImmolateUp = DebuffImmolateRemains > 0;
            bool DebuffImmolateRefreshable = DebuffImmolateRemains < 5400;
            int DebuffHavocRemains = Aimsharp.DebuffRemaining("Havoc", "focus") - GCD;
            bool DebuffHavocUp = DebuffHavocRemains > 0;
            int BuffBackdraftRemains = Aimsharp.BuffRemaining("Backdraft") - GCD;
            bool BuffBackdraftUp = BuffBackdraftRemains > 0;
            int BuffChaoticInfernoRemains = Aimsharp.BuffRemaining("Chaotic Inferno") - GCD;
            bool BuffChaoticInfernoUp = BuffChaoticInfernoRemains > 0;
            int BuffCrashingChaosRemains = Aimsharp.BuffRemaining("Crashing Chaos") - GCD;
            bool BuffCrashingChaosUp = BuffCrashingChaosRemains > 0;
            int BuffDarkSoulInstabilityRemains = Aimsharp.BuffRemaining("Dark Soul: Instability") - GCD;
            bool BuffDarkSoulInstabilityUp = BuffDarkSoulInstabilityRemains > 0;
            int BuffGrimoireOfSupremacyStacks = Aimsharp.BuffStacks("Grimoire of Supremacy");

            int CDHavocRemains = Aimsharp.SpellCooldown("Havoc") - GCD;
            bool CDHavocReady = CDHavocRemains <= 10;
            int CDConflagrateRemains = Aimsharp.SpellCooldown("Conflagrate") - GCD;
            bool CDConflagrateReady = CDConflagrateRemains <= 10;
            int CDCataclysmRemains = Aimsharp.SpellCooldown("Cataclysm") - GCD;
            bool CDCataclysmReady = CDCataclysmRemains <= 10;
            int CDSummonInfernalRemains = Aimsharp.SpellCooldown("Summon Infernal") - GCD;
            bool CDSummonInfernalReady = CDSummonInfernalRemains <= 10;
            int CDDarkSoulInstabilityRemains = Aimsharp.SpellCooldown("Dark Soul: Instability") - GCD;
            bool CDDarkSoulInstabilityReady = CDDarkSoulInstabilityRemains <= 10;

            int AzeriteCrashingChaosRank = GetSlider("# Crashing Chaos Traits");


            bool HavocActive = Aimsharp.DebuffRemaining("Havoc", "focus") - GCD > 0;
            float ChaosBoltCastTime = 3000f / (1f + Haste) * (BuffBackdraftUp ? .7f : 1f);
            float IncinerateCastTime = 2000f / (1f + Haste) * (BuffChaoticInfernoUp ? 0f : 1f);
            float DemonfireCastTime = 3000f / (1f + Haste);
            int InfernalRemaining = Aimsharp.TotemTimer();
            bool InfernalActive = InfernalRemaining > GCD;

            int ConflagrateCharges = Aimsharp.SpellCharges("Conflagrate");
            int ConflagrateMaxCharges = Aimsharp.MaxCharges("Conflagrate");
            int ConflagrateRechargeTime = Aimsharp.RechargeTime("Conflagrate");
            int ConflagrateFullRechargeTime = (int)(ConflagrateRechargeTime + (13000f) / (1f + Haste)) * (ConflagrateMaxCharges - ConflagrateCharges - 1);
            float ConflagrateChargesFractional = ConflagrateCharges + (ConflagrateRechargeTime) / ((13000f) / (1f + Haste));

            bool CastingImmolate = Aimsharp.CastingID("player") == 348;


            if (IsChanneling)
                return false;

            //actions=call_action_list,name=havoc,if=havoc_active&active_enemies<5-talent.inferno.enabled+(talent.inferno.enabled&talent.internal_combustion.enabled)
            if (HavocActive && EnemiesNearTarget < 5 - (TalentInfernoEnabled ? 1 : 0) + (TalentInfernoEnabled && TalentInternalCombustionEnabled ? 1 : 0))
            {
                //actions.havoc=conflagrate,if=buff.backdraft.down&soul_shard>=1&soul_shard<=4
                if (Aimsharp.CanCast("Conflagrate"))
                {
                    if (!BuffBackdraftUp && SoulShard >= 1 && SoulShard <= 4)
                    {
                        Aimsharp.Cast("Conflagrate");
                        return true;
                    }
                }

                //actions.havoc+=/immolate,if=talent.internal_combustion.enabled&remains<duration*0.5|!talent.internal_combustion.enabled&refreshable
                if (Aimsharp.CanCast("Immolate") && !Moving && !CastingImmolate)
                {
                    if (TalentInternalCombustionEnabled && DebuffImmolateRemains < 9000 || !TalentInternalCombustionEnabled && DebuffImmolateRefreshable)
                    {
                        Aimsharp.Cast("Immolate");
                        return true;
                    }
                }

                //actions.havoc+=/chaos_bolt,if=cast_time<havoc_remains
                if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
                {
                    if (ChaosBoltCastTime < DebuffHavocRemains)
                    {
                        Aimsharp.Cast("Chaos Bolt");
                        return true;
                    }
                }

                //actions.havoc+=/soul_fire
                if (Aimsharp.CanCast("Soul Fire") && !Moving)
                {
                    Aimsharp.Cast("Soul Fire");
                    return true;
                }

                //actions.havoc+=/shadowburn,if=active_enemies<3|!talent.fire_and_brimstone.enabled
                if (Aimsharp.CanCast("Shadowburn"))
                {
                    if (EnemiesNearTarget < 3 || !TalentFireAndBrimstoneEnabled)
                    {
                        Aimsharp.Cast("Shadowburn");
                        return true;
                    }
                }

                //actions.havoc+=/incinerate,if=cast_time<havoc_remains
                if (Aimsharp.CanCast("Incinerate") && (!Moving || BuffChaoticInfernoUp))
                {
                    if (IncinerateCastTime < DebuffHavocRemains)
                    {
                        Aimsharp.Cast("Incinerate");
                        return true;
                    }
                }
            }

            //actions+=/cataclysm,if=!(pet.infernal.active&dot.immolate.remains+1>pet.infernal.remains)|spell_targets.cataclysm>1|!talent.grimoire_of_supremacy.enabled
            if (Aimsharp.CanCast("Cataclysm", "player") && !Moving && Fighting)
            {
                if (!(InfernalActive && DebuffImmolateRemains + 1000 > InfernalRemaining) || EnemiesNearTarget > 1 || !TalentGrimoireOfSupremacyEnabled)
                {
                    Aimsharp.Cast("cata cursor");
                    return true;
                }
            }

            //actions+=/call_action_list,name=aoe,if=active_enemies>2
            if (EnemiesNearTarget > 2)
            {
                //actions.aoe=rain_of_fire,if=pet.infernal.active&(buff.crashing_chaos.down|!talent.grimoire_of_supremacy.enabled)&(!cooldown.havoc.ready|active_enemies>3)
                if (Aimsharp.CanCast("Rain of Fire", "player") && Fighting)
                {
                    if (InfernalActive && (!BuffChaoticInfernoUp || !TalentGrimoireOfSupremacyEnabled) && (!CDHavocReady || EnemiesNearTarget > 3))
                    {
                        Aimsharp.Cast("rof cursor");
                        return true;
                    }
                }

                //actions.aoe+=/channel_demonfire,if=dot.immolate.remains>cast_time
                if (Aimsharp.CanCast("Channel Demonfire", "player") && !Moving && Fighting)
                {
                    if (DebuffImmolateRemains > DemonfireCastTime)
                    {
                        Aimsharp.Cast("Channel Demonfire");
                        return true;
                    }
                }

                //actions.aoe+=/immolate,cycle_targets=1,if=remains<5&(!talent.cataclysm.enabled|cooldown.cataclysm.remains>remains)
                if (Aimsharp.CanCast("Immolate") && !Moving && !CastingImmolate)
                {
                    if (DebuffImmolateRemains < 5000 && (!TalentCataclysmEnabled || CDCataclysmRemains > DebuffImmolateRemains))
                    {
                        Aimsharp.Cast("Immolate");
                        return true;
                    }
                }

                //actions.aoe+=/call_action_list,name=cds
                if (!NoCooldowns)
                {
                    //actions.cds=immolate,if=talent.grimoire_of_supremacy.enabled&remains<8&cooldown.summon_infernal.remains<4.5
                    if (Aimsharp.CanCast("Immolate") && !Moving && !CastingImmolate)
                    {
                        if (TalentGrimoireOfSupremacyEnabled && DebuffImmolateRemains < 8000 && CDSummonInfernalRemains < 4500)
                        {
                            Aimsharp.Cast("Immolate");
                            return true;
                        }
                    }

                    //actions.cds+=/conflagrate,if=talent.grimoire_of_supremacy.enabled&cooldown.summon_infernal.remains<4.5&!buff.backdraft.up&soul_shard<4.3
                    if (Aimsharp.CanCast("Conflagrate"))
                    {
                        if (TalentGrimoireOfSupremacyEnabled && CDSummonInfernalRemains < 4500 && !BuffBackdraftUp && SoulShard < 4.3)
                        {
                            Aimsharp.Cast("Conflagrate");
                            return true;
                        }
                    }

                    //actions.cds+=/use_item,name=azsharas_font_of_power,if=cooldown.summon_infernal.up|cooldown.summon_infernal.remains<=4
                    if (Aimsharp.CanUseItem("Azshara's Font of Power"))
                    {
                        if (CDSummonInfernalReady || CDSummonInfernalRemains <= 4000)
                        {
                            Aimsharp.Cast("Azshara's Font of Power", true);
                            return true;
                        }
                    }

                    //actions.cds+=/summon_infernal
                    if (Aimsharp.CanCast("Summon Infernal", "player") && Fighting)
                    {
                        Aimsharp.Cast("inf cursor");
                        return true;
                    }

                    //actions.cds+=/guardian_of_azeroth,if=pet.infernal.active
                    if (MajorPower == "Guardian of Azeroth" && Fighting)
                    {
                        if (InfernalActive && Aimsharp.CanCast("Guardian of Azeroth", "player"))
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }

                    //actions.cds+=/dark_soul_instability,if=pet.infernal.active&(pet.infernal.remains<20.5|pet.infernal.remains<22&soul_shard>=3.6|!talent.grimoire_of_supremacy.enabled)
                    if (Aimsharp.CanCast("Dark Soul: Instability", "player") && Fighting)
                    {
                        if (InfernalActive && (InfernalRemaining < 20500 || InfernalRemaining < 22000 && SoulShard >= 3.6 || !TalentGrimoireOfSupremacyEnabled))
                        {
                            Aimsharp.Cast("Dark Soul: Instability");
                            return true;
                        }
                    }

                    //actions.cds+=/worldvein_resonance,if=pet.infernal.active&(pet.infernal.remains<18.5|pet.infernal.remains<20&soul_shard>=3.6|!talent.grimoire_of_supremacy.enabled)
                    if (MajorPower == "Worldvein Resonance" && Fighting)
                    {
                        if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                        {
                            if (InfernalActive && (InfernalRemaining < 18500 || InfernalRemaining < 20000 && SoulShard >= 3.6 || !TalentGrimoireOfSupremacyEnabled))
                            {
                                Aimsharp.Cast("Worldvein Resonance");
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/memory_of_lucid_dreams,if=pet.infernal.active&(pet.infernal.remains<15.5|soul_shard<3.5&(buff.dark_soul_instability.up|!talent.grimoire_of_supremacy.enabled&dot.immolate.remains>12))
                    if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                    {
                        if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                        {
                            if (InfernalActive && (InfernalRemaining < 15500 || SoulShard < 3.5 && (BuffDarkSoulInstabilityUp || !TalentGrimoireOfSupremacyEnabled && DebuffImmolateRemains > 12000)))
                            {
                                Aimsharp.Cast("Memory of Lucid Dreams");
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/summon_infernal,if=target.time_to_die>cooldown.summon_infernal.duration+30
                    if (Aimsharp.CanCast("Summon Infernal", "player") && Fighting)
                    {
                        if (TargetTimeToDie > 165000)
                        {
                            Aimsharp.Cast("inf cursor");
                            return true;
                        }
                    }

                    //actions.cds+=/guardian_of_azeroth,if=time>30&target.time_to_die>cooldown.guardian_of_azeroth.duration+30
                    if (MajorPower == "Guardian of Azeroth" && Fighting)
                    {
                        if (Aimsharp.CanCast("Guardian of Azeroth", "player") && Time > 30000 && TargetTimeToDie > 210000)
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }

                    //actions.cds+=/summon_infernal,if=talent.dark_soul_instability.enabled&cooldown.dark_soul_instability.remains>target.time_to_die
                    if (Aimsharp.CanCast("Summon Infernal", "player") && Fighting)
                    {
                        if (TalentDarkSoulInstabilityEnabled && CDDarkSoulInstabilityRemains > TargetTimeToDie)
                        {
                            Aimsharp.Cast("inf cursor");
                            return true;
                        }
                    }

                    //actions.cds+=/guardian_of_azeroth,if=cooldown.summon_infernal.remains>target.time_to_die
                    if (MajorPower == "Guardian of Azeroth" && Fighting)
                    {
                        if (Aimsharp.CanCast("Guardian of Azeroth", "player") && CDSummonInfernalRemains > TargetTimeToDie)
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }

                    //actions.cds+=/dark_soul_instability,if=cooldown.summon_infernal.remains>target.time_to_die&pet.infernal.remains<20.5
                    if (Aimsharp.CanCast("Dark Soul: Instability", "player") && Fighting)
                    {
                        if (CDSummonInfernalRemains > TargetTimeToDie && InfernalRemaining < 20500)
                        {
                            Aimsharp.Cast("Dark Soul: Instability");
                            return true;
                        }
                    }

                    //actions.cds+=/worldvein_resonance,if=cooldown.summon_infernal.remains>target.time_to_die&pet.infernal.remains<18.5
                    if (MajorPower == "Worldvein Resonance" && Fighting)
                    {
                        if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                        {
                            if (CDSummonInfernalRemains > TargetTimeToDie && InfernalRemaining < 18500)
                            {
                                Aimsharp.Cast("Worldvein Resonance");
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/memory_of_lucid_dreams,if=cooldown.summon_infernal.remains>target.time_to_die&(pet.infernal.remains<15.5|buff.dark_soul_instability.up&soul_shard<3)
                    if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                    {
                        if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                        {
                            if (CDSummonInfernalRemains > TargetTimeToDie && (InfernalRemaining < 15500 || BuffDarkSoulInstabilityUp && SoulShard < 3))
                            {
                                Aimsharp.Cast("Memory of Lucid Dreams");
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/summon_infernal,if=target.time_to_die<30

                    if (Aimsharp.CanCast("Summon Infernal", "player") && Fighting)
                    {
                        if (TargetTimeToDie < 30000)
                        {
                            Aimsharp.Cast("inf cursor");
                            return true;
                        }
                    }

                    //actions.cds +=/ guardian_of_azeroth,if= target.time_to_die < 30
                    if (MajorPower == "Guardian of Azeroth" && Fighting)
                    {
                        if (Aimsharp.CanCast("Guardian of Azeroth", "player") && TargetTimeToDie < 30000)
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }

                    //actions.cds+=/dark_soul_instability,if=target.time_to_die<21&target.time_to_die>4
                    if (Aimsharp.CanCast("Dark Soul: Instability", "player") && Fighting)
                    {
                        if (TargetTimeToDie < 21000 && TargetTimeToDie > 4000)
                        {
                            Aimsharp.Cast("Dark Soul: Instability");
                            return true;
                        }
                    }

                    //actions.cds+=/worldvein_resonance,if=target.time_to_die<19&target.time_to_die>4
                    if (MajorPower == "Worldvein Resonance" && Fighting)
                    {
                        if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                        {
                            if (TargetTimeToDie < 19000 && TargetTimeToDie > 4000)
                            {
                                Aimsharp.Cast("Worldvein Resonance");
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/memory_of_lucid_dreams,if=target.time_to_die<16&target.time_to_die>6
                    if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                    {
                        if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                        {
                            if (TargetTimeToDie < 16000 && TargetTimeToDie > 6000)
                            {
                                Aimsharp.Cast("Memory of Lucid Dreams");
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/blood_of_the_enemy
                    if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                    {
                        if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }

                    //actions.cds+=/worldvein_resonance,if=cooldown.summon_infernal.remains>=60-12&!pet.infernal.active
                    if (MajorPower == "Worldvein Resonance" && Fighting)
                    {
                        if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                        {
                            if (CDSummonInfernalRemains >= 60000 - 12000 && !InfernalActive)
                            {
                                Aimsharp.Cast("Worldvein Resonance");
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/potion,if=pet.infernal.active|target.time_to_die<30
                    if (UsePotion && Fighting)
                    {
                        if (InfernalActive || TargetTimeToDie < 30000)
                        {
                            if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                            {
                                Aimsharp.Cast("potion", true);
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/berserking,if=pet.infernal.active&(!talent.grimoire_of_supremacy.enabled|(!essence.memory_of_lucid_dreams.major|buff.memory_of_lucid_dreams.remains)&(!talent.dark_soul_instability.enabled|buff.dark_soul_instability.remains))|target.time_to_die<=15
                    if (RacialPower == "Troll" && Fighting)
                    {
                        if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || (MajorPower != "Memory of Lucid Dreams" || BuffMemoryOfLucidDreamsUp) && (!TalentDarkSoulInstabilityEnabled || BuffDarkSoulInstabilityUp)) || TargetTimeToDie <= 15000)
                        {
                            if (Aimsharp.CanCast("Berserking", "player"))
                            {
                                Aimsharp.Cast("Berserking", true);
                                return true;
                            }
                        }
                    }

                    if (RacialPower == "Orc" && Fighting)
                    {
                        if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || (MajorPower != "Memory of Lucid Dreams" || BuffMemoryOfLucidDreamsUp) && (!TalentDarkSoulInstabilityEnabled || BuffDarkSoulInstabilityUp)) || TargetTimeToDie <= 15000)
                        {
                            if (Aimsharp.CanCast("Blood Fury", "player"))
                            {
                                Aimsharp.Cast("Blood Fury", true);
                                return true;
                            }
                        }
                    }

                    if (RacialPower == "Dark Iron Dwarf" && Fighting)
                    {
                        if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || (MajorPower != "Memory of Lucid Dreams" || BuffMemoryOfLucidDreamsUp) && (!TalentDarkSoulInstabilityEnabled || BuffDarkSoulInstabilityUp)) || TargetTimeToDie <= 15000)
                        {
                            if (Aimsharp.CanCast("Fireblood", "player"))
                            {
                                Aimsharp.Cast("Fireblood", true);
                                return true;
                            }
                        }
                    }

                    //actions.cds+=/use_items,if=pet.infernal.active&(!talent.grimoire_of_supremacy.enabled|pet.infernal.remains<=20)|target.time_to_die<=20
                    if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || InfernalRemaining <= 20000) || TargetTimeToDie <= 20000)
                    {
                        if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic" && Fighting)
                        {
                            Aimsharp.Cast("TopTrink", true);
                            return true;
                        }

                        if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic" && Fighting)
                        {
                            Aimsharp.Cast("BotTrink", true);
                            return true;
                        }
                    }
                }

                //actions.aoe+=/havoc,cycle_targets=1,if=!(target=self.target)&active_enemies<4
                if (Aimsharp.CanCast("Havoc", "focus"))
                {
                    if (EnemiesNearTarget < 4)
                    {
                        Aimsharp.Cast("havoc focus");
                        return true;
                    }
                }

                //actions.aoe+=/chaos_bolt,if=talent.grimoire_of_supremacy.enabled&pet.infernal.active&(havoc_active|talent.cataclysm.enabled|talent.inferno.enabled&active_enemies<4)
                if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
                {
                    if (TalentGrimoireOfSupremacyEnabled && InfernalActive && (HavocActive || TalentCataclysmEnabled || TalentInfernoEnabled && EnemiesNearTarget < 4))
                    {
                        Aimsharp.Cast("Chaos Bolt");
                        return true;
                    }
                }

                //actions.aoe+=/rain_of_fire
                if (Aimsharp.CanCast("Rain of Fire", "player") && Fighting)
                {
                    Aimsharp.Cast("rof cursor");
                    return true;
                }

                //actions.aoe+=/focused_azerite_beam
                if (MajorPower == "Focused Azerite Beam" && Range < 15)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        Aimsharp.Cast("Focused Azerite Beam");
                        return true;
                    }
                }

                //actions.aoe+=/havoc,cycle_targets=1,if=!(target=self.target)&(!talent.grimoire_of_supremacy.enabled|!talent.inferno.enabled|talent.grimoire_of_supremacy.enabled&pet.infernal.remains<=10)
                if (Aimsharp.CanCast("Havoc", "focus"))
                {
                    if (!TalentGrimoireOfSupremacyEnabled || !TalentInfernoEnabled || TalentGrimoireOfSupremacyEnabled && InfernalRemaining <= 10000)
                    {
                        Aimsharp.Cast("havoc focus");
                        return true;
                    }
                }

                //actions.aoe+=/incinerate,if=talent.fire_and_brimstone.enabled&buff.backdraft.up&soul_shard<5-0.2*active_enemies
                if (Aimsharp.CanCast("Incinerate") && (!Moving || BuffChaoticInfernoUp))
                {
                    if (TalentFireAndBrimstoneEnabled && BuffBackdraftUp && SoulShard < 5 - .2 * EnemiesNearTarget)
                    {
                        Aimsharp.Cast("Incinerate");
                        return true;
                    }
                }

                //actions.aoe+=/soul_fire
                if (Aimsharp.CanCast("Soul Fire") && !Moving)
                {
                    Aimsharp.Cast("Soul Fire");
                    return true;
                }

                //actions.aoe+=/conflagrate,if=buff.backdraft.down
                if (Aimsharp.CanCast("Conflagrate"))
                {
                    if (!BuffBackdraftUp)
                    {
                        Aimsharp.Cast("Conflagrate");
                        return true;
                    }
                }

                //actions.aoe+=/shadowburn,if=!talent.fire_and_brimstone.enabled
                if (Aimsharp.CanCast("Shadowburn"))
                {
                    if (!TalentFireAndBrimstoneEnabled)
                    {
                        Aimsharp.Cast("Shadowburn");
                        return true;
                    }
                }

                //actions.aoe+=/concentrated_flame,if=!dot.concentrated_flame_burn.remains&!action.concentrated_flame.in_flight&active_enemies<5
                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                    {
                        if (EnemiesNearTarget < 5)
                        {
                            Aimsharp.Cast("Concentrated Flame");
                            return true;
                        }
                    }
                }

                //actions.aoe+=/incinerate
                if (Aimsharp.CanCast("Incinerate") && (!Moving || BuffChaoticInfernoUp))
                {
                    Aimsharp.Cast("Incinerate");
                    return true;
                }
            }

            //actions+=/immolate,cycle_targets=1,if=refreshable&(!talent.cataclysm.enabled|cooldown.cataclysm.remains>remains)
            if (Aimsharp.CanCast("Immolate") && !Moving && !CastingImmolate)
            {
                if (DebuffImmolateRefreshable && (!TalentCataclysmEnabled || CDCataclysmRemains > DebuffImmolateRemains))
                {
                    Aimsharp.Cast("Immolate");
                    return true;
                }
            }

            //actions+=/immolate,if=talent.internal_combustion.enabled&action.chaos_bolt.in_flight&remains<duration*0.5
            if (Aimsharp.CanCast("Immolate") && !Moving && !CastingImmolate)
            {
                if (TalentInternalCombustionEnabled && LastCast == "Chaos Bolt" && DebuffImmolateRemains < 9000)
                {
                    Aimsharp.Cast("Immolate");
                    return true;
                }
            }

            //actions.aoe+=/call_action_list,name=cds
            if (!NoCooldowns)
            {
                //actions.cds=immolate,if=talent.grimoire_of_supremacy.enabled&remains<8&cooldown.summon_infernal.remains<4.5
                if (Aimsharp.CanCast("Immolate") && !Moving && !CastingImmolate)
                {
                    if (TalentGrimoireOfSupremacyEnabled && DebuffImmolateRemains < 8000 && CDSummonInfernalRemains < 4500)
                    {
                        Aimsharp.Cast("Immolate");
                        return true;
                    }
                }

                //actions.cds+=/conflagrate,if=talent.grimoire_of_supremacy.enabled&cooldown.summon_infernal.remains<4.5&!buff.backdraft.up&soul_shard<4.3
                if (Aimsharp.CanCast("Conflagrate"))
                {
                    if (TalentGrimoireOfSupremacyEnabled && CDSummonInfernalRemains < 4500 && !BuffBackdraftUp && SoulShard < 4.3)
                    {
                        Aimsharp.Cast("Conflagrate");
                        return true;
                    }
                }

                //actions.cds+=/use_item,name=azsharas_font_of_power,if=cooldown.summon_infernal.up|cooldown.summon_infernal.remains<=4
                if (Aimsharp.CanUseItem("Azshara's Font of Power") && Fighting)
                {
                    if (CDSummonInfernalReady || CDSummonInfernalRemains <= 4000)
                    {
                        Aimsharp.Cast("Azshara's Font of Power", true);
                        return true;
                    }
                }

                //actions.cds+=/summon_infernal
                if (Aimsharp.CanCast("Summon Infernal", "player") && Fighting)
                {
                    Aimsharp.Cast("inf cursor");
                    return true;
                }

                //actions.cds+=/guardian_of_azeroth,if=pet.infernal.active
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (InfernalActive && Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                //actions.cds+=/dark_soul_instability,if=pet.infernal.active&(pet.infernal.remains<20.5|pet.infernal.remains<22&soul_shard>=3.6|!talent.grimoire_of_supremacy.enabled)
                if (Aimsharp.CanCast("Dark Soul: Instability", "player") && Fighting)
                {
                    if (InfernalActive && (InfernalRemaining < 20500 || InfernalRemaining < 22000 && SoulShard >= 3.6 || !TalentGrimoireOfSupremacyEnabled))
                    {
                        Aimsharp.Cast("Dark Soul: Instability");
                        return true;
                    }
                }

                //actions.cds+=/worldvein_resonance,if=pet.infernal.active&(pet.infernal.remains<18.5|pet.infernal.remains<20&soul_shard>=3.6|!talent.grimoire_of_supremacy.enabled)
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (InfernalActive && (InfernalRemaining < 18500 || InfernalRemaining < 20000 && SoulShard >= 3.6 || !TalentGrimoireOfSupremacyEnabled))
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                //actions.cds+=/memory_of_lucid_dreams,if=pet.infernal.active&(pet.infernal.remains<15.5|soul_shard<3.5&(buff.dark_soul_instability.up|!talent.grimoire_of_supremacy.enabled&dot.immolate.remains>12))
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (InfernalActive && (InfernalRemaining < 15500 || SoulShard < 3.5 && (BuffDarkSoulInstabilityUp || !TalentGrimoireOfSupremacyEnabled && DebuffImmolateRemains > 12000)))
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }

                //actions.cds+=/summon_infernal,if=target.time_to_die>cooldown.summon_infernal.duration+30
                if (Aimsharp.CanCast("Summon Infernal", "player") && Fighting)
                {
                    if (TargetTimeToDie > 165000)
                    {
                        Aimsharp.Cast("inf cursor");
                        return true;
                    }
                }

                //actions.cds+=/guardian_of_azeroth,if=time>30&target.time_to_die>cooldown.guardian_of_azeroth.duration+30
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player") && Time > 30000 && TargetTimeToDie > 210000)
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                //actions.cds+=/summon_infernal,if=talent.dark_soul_instability.enabled&cooldown.dark_soul_instability.remains>target.time_to_die
                if (Aimsharp.CanCast("Summon Infernal", "player"))
                {
                    if (TalentDarkSoulInstabilityEnabled && CDDarkSoulInstabilityRemains > TargetTimeToDie)
                    {
                        Aimsharp.Cast("inf cursor");
                        return true;
                    }
                }

                //actions.cds+=/guardian_of_azeroth,if=cooldown.summon_infernal.remains>target.time_to_die
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player") && CDSummonInfernalRemains > TargetTimeToDie)
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                //actions.cds+=/dark_soul_instability,if=cooldown.summon_infernal.remains>target.time_to_die&pet.infernal.remains<20.5
                if (Aimsharp.CanCast("Dark Soul: Instability", "player") && Fighting)
                {
                    if (CDSummonInfernalRemains > TargetTimeToDie && InfernalRemaining < 20500)
                    {
                        Aimsharp.Cast("Dark Soul: Instability");
                        return true;
                    }
                }

                //actions.cds+=/worldvein_resonance,if=cooldown.summon_infernal.remains>target.time_to_die&pet.infernal.remains<18.5
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (CDSummonInfernalRemains > TargetTimeToDie && InfernalRemaining < 18500)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                //actions.cds+=/memory_of_lucid_dreams,if=cooldown.summon_infernal.remains>target.time_to_die&(pet.infernal.remains<15.5|buff.dark_soul_instability.up&soul_shard<3)
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (CDSummonInfernalRemains > TargetTimeToDie && (InfernalRemaining < 15500 || BuffDarkSoulInstabilityUp && SoulShard < 3))
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }

                //actions.cds+=/summon_infernal,if=target.time_to_die<30

                if (Aimsharp.CanCast("Summon Infernal", "player") && Fighting)
                {
                    if (TargetTimeToDie < 30000)
                    {
                        Aimsharp.Cast("inf cursor");
                        return true;
                    }
                }

                //actions.cds +=/ guardian_of_azeroth,if= target.time_to_die < 30
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player") && TargetTimeToDie < 30000)
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                //actions.cds+=/dark_soul_instability,if=target.time_to_die<21&target.time_to_die>4
                if (Aimsharp.CanCast("Dark Soul: Instability", "player") && Fighting)
                {
                    if (TargetTimeToDie < 21000 && TargetTimeToDie > 4000)
                    {
                        Aimsharp.Cast("Dark Soul: Instability");
                        return true;
                    }
                }

                //actions.cds+=/worldvein_resonance,if=target.time_to_die<19&target.time_to_die>4
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (TargetTimeToDie < 19000 && TargetTimeToDie > 4000)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                //actions.cds+=/memory_of_lucid_dreams,if=target.time_to_die<16&target.time_to_die>6
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (TargetTimeToDie < 16000 && TargetTimeToDie > 6000)
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }

                //actions.cds+=/blood_of_the_enemy
                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }

                //actions.cds+=/worldvein_resonance,if=cooldown.summon_infernal.remains>=60-12&!pet.infernal.active
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (CDSummonInfernalRemains >= 60000 - 12000 && !InfernalActive)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                //actions.cds+=/potion,if=pet.infernal.active|target.time_to_die<30
                if (UsePotion && Fighting)
                {
                    if (InfernalActive || TargetTimeToDie < 30000)
                    {
                        if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                        {
                            Aimsharp.Cast("potion", true);
                            return true;
                        }
                    }
                }

                //actions.cds+=/berserking,if=pet.infernal.active&(!talent.grimoire_of_supremacy.enabled|(!essence.memory_of_lucid_dreams.major|buff.memory_of_lucid_dreams.remains)&(!talent.dark_soul_instability.enabled|buff.dark_soul_instability.remains))|target.time_to_die<=15
                if (RacialPower == "Troll" && Fighting)
                {
                    if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || (MajorPower != "Memory of Lucid Dreams" || BuffMemoryOfLucidDreamsUp) && (!TalentDarkSoulInstabilityEnabled || BuffDarkSoulInstabilityUp)) || TargetTimeToDie <= 15000)
                    {
                        if (Aimsharp.CanCast("Berserking", "player"))
                        {
                            Aimsharp.Cast("Berserking", true);
                            return true;
                        }
                    }
                }

                if (RacialPower == "Orc" && Fighting)
                {
                    if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || (MajorPower != "Memory of Lucid Dreams" || BuffMemoryOfLucidDreamsUp) && (!TalentDarkSoulInstabilityEnabled || BuffDarkSoulInstabilityUp)) || TargetTimeToDie <= 15000)
                    {
                        if (Aimsharp.CanCast("Blood Fury", "player"))
                        {
                            Aimsharp.Cast("Blood Fury", true);
                            return true;
                        }
                    }
                }

                if (RacialPower == "Dark Iron Dwarf" && Fighting)
                {
                    if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || (MajorPower != "Memory of Lucid Dreams" || BuffMemoryOfLucidDreamsUp) && (!TalentDarkSoulInstabilityEnabled || BuffDarkSoulInstabilityUp)) || TargetTimeToDie <= 15000)
                    {
                        if (Aimsharp.CanCast("Fireblood", "player"))
                        {
                            Aimsharp.Cast("Fireblood", true);
                            return true;
                        }
                    }
                }

                //actions.cds+=/use_items,if=pet.infernal.active&(!talent.grimoire_of_supremacy.enabled|pet.infernal.remains<=20)|target.time_to_die<=20
                if (InfernalActive && (!TalentGrimoireOfSupremacyEnabled || InfernalRemaining <= 20000) || TargetTimeToDie <= 20000)
                {
                    if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic" && Fighting)
                    {
                        Aimsharp.Cast("TopTrink", true);
                        return true;
                    }

                    if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic" && Fighting)
                    {
                        Aimsharp.Cast("BotTrink", true);
                        return true;
                    }
                }
            }

            //actions+=/focused_azerite_beam,if=!pet.infernal.active|!talent.grimoire_of_supremacy.enabled
            if (MajorPower == "Focused Azerite Beam" && Range < 15)
            {
                if (!InfernalActive || !TalentGrimoireOfSupremacyEnabled)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        Aimsharp.Cast("Focused Azerite Beam");
                        return true;
                    }
                }
            }

            //actions+=/concentrated_flame,if=!dot.concentrated_flame_burn.remains&!action.concentrated_flame.in_flight
            if (MajorPower == "Concentrated Flame")
            {
                if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                {
                    Aimsharp.Cast("Concentrated Flame");
                    return true;
                }
            }

            //actions+=/reaping_flames
            if (MajorPower == "Reaping Flames")
            {
                if (Aimsharp.CanCast("Reaping Flames"))
                {
                    Aimsharp.Cast("Reaping Flames");
                    return true;
                }
            }

            //actions+=/channel_demonfire
            if (Aimsharp.CanCast("Channel Demonfire", "player") && !Moving && Fighting)
            {
                Aimsharp.Cast("Channel Demonfire");
                return true;
            }

            //actions+=/havoc,cycle_targets=1,if=!(target=self.target)&(dot.immolate.remains>dot.immolate.duration*0.5|!talent.internal_combustion.enabled)&(!cooldown.summon_infernal.ready|!talent.grimoire_of_supremacy.enabled|talent.grimoire_of_supremacy.enabled&pet.infernal.remains<=10)
            if (Aimsharp.CanCast("Havoc", "focus"))
            {
                if ((DebuffImmolateRemains > 9000 || !TalentInternalCombustionEnabled) && (!CDSummonInfernalReady || !TalentGrimoireOfSupremacyEnabled || TalentGrimoireOfSupremacyEnabled && InfernalRemaining <= 10000))
                {
                    Aimsharp.Cast("havoc focus");
                    return true;
                }
            }

            //actions+=/call_action_list,name=gosup_infernal,if=talent.grimoire_of_supremacy.enabled&pet.infernal.active
            if (TalentGrimoireOfSupremacyEnabled && InfernalActive)
            {
                //actions.gosup_infernal=rain_of_fire,if=soul_shard=5&!buff.backdraft.up&buff.memory_of_lucid_dreams.up&buff.grimoire_of_supremacy.stack<=10
                if (Aimsharp.CanCast("Rain of Fire", "player") && Fighting)
                {
                    if (SoulShard == 5 && !BuffBackdraftUp && BuffMemoryOfLucidDreamsUp && BuffGrimoireOfSupremacyStacks <= 10)
                    {
                        Aimsharp.Cast("rof cursor");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/chaos_bolt,if=buff.backdraft.up
                if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
                {
                    if (BuffBackdraftUp)
                    {
                        Aimsharp.Cast("Chaos Bolt");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/chaos_bolt,if=soul_shard>=4.2-buff.memory_of_lucid_dreams.up
                if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
                {
                    if (SoulShard >= 4.2 - (BuffMemoryOfLucidDreamsUp ? 1 : 0))
                    {
                        Aimsharp.Cast("Chaos Bolt");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/chaos_bolt,if=!cooldown.conflagrate.up
                if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
                {
                    if (!CDConflagrateReady)
                    {
                        Aimsharp.Cast("Chaos Bolt");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/chaos_bolt,if=cast_time<pet.infernal.remains&pet.infernal.remains<cast_time+gcd
                if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
                {
                    if (ChaosBoltCastTime < InfernalRemaining && InfernalRemaining < ChaosBoltCastTime + GCD)
                    {
                        Aimsharp.Cast("Chaos Bolt");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/conflagrate,if=buff.backdraft.down&buff.memory_of_lucid_dreams.up&soul_shard>=1.3
                if (Aimsharp.CanCast("Conflagrate"))
                {
                    if (!BuffBackdraftUp && BuffMemoryOfLucidDreamsUp && SoulShard >= 1.3)
                    {
                        Aimsharp.Cast("Conflagrate");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/conflagrate,if=buff.backdraft.down&!buff.memory_of_lucid_dreams.up&(soul_shard>=2.8|charges_fractional>1.9&soul_shard>=1.3)
                if (Aimsharp.CanCast("Conflagrate"))
                {
                    if (!BuffBackdraftUp && !BuffMemoryOfLucidDreamsUp && (SoulShard >= 2.8 || ConflagrateChargesFractional > 1.9 && SoulShard >= 1.3))
                    {
                        Aimsharp.Cast("Conflagrate");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/conflagrate,if=pet.infernal.remains<5
                if (Aimsharp.CanCast("Conflagrate"))
                {
                    if (InfernalRemaining < 5000)
                    {
                        Aimsharp.Cast("Conflagrate");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/conflagrate,if=charges>1
                if (Aimsharp.CanCast("Conflagrate"))
                {
                    if (ConflagrateCharges > 1)
                    {
                        Aimsharp.Cast("Conflagrate");
                        return true;
                    }
                }

                //actions.gosup_infernal+=/soul_fire
                if (Aimsharp.CanCast("Soul Fire") && !Moving)
                {
                    Aimsharp.Cast("Soul Fire");
                    return true;
                }

                //actions.gosup_infernal+=/shadowburn
                if (Aimsharp.CanCast("Shadowburn"))
                {
                        Aimsharp.Cast("Shadowburn");
                        return true;
                }

                //actions.gosup_infernal+=/incinerate
                if (Aimsharp.CanCast("Incinerate") && (!Moving || BuffChaoticInfernoUp))
                {
                    Aimsharp.Cast("Incinerate");
                    return true;
                }
            }

            //actions+=/soul_fire
            if (Aimsharp.CanCast("Soul Fire") && !Moving)
            {
                Aimsharp.Cast("Soul Fire");
                return true;
            }

            //actions+=/variable,name=pool_soul_shards,value=active_enemies>1&cooldown.havoc.remains<=10|cooldown.summon_infernal.remains<=15&(talent.grimoire_of_supremacy.enabled|talent.dark_soul_instability.enabled&cooldown.dark_soul_instability.remains<=15)|talent.dark_soul_instability.enabled&cooldown.dark_soul_instability.remains<=15&(cooldown.summon_infernal.remains>target.time_to_die|cooldown.summon_infernal.remains+cooldown.summon_infernal.duration>target.time_to_die)
            bool PoolSoulShards = EnemiesNearTarget > 1 && CDHavocRemains <= 10000 || CDSummonInfernalRemains <= 15000 && (TalentGrimoireOfSupremacyEnabled || TalentDarkSoulInstabilityEnabled && CDDarkSoulInstabilityRemains <= 15000) || TalentDarkSoulInstabilityEnabled && CDDarkSoulInstabilityRemains <= 15000 && (CDSummonInfernalRemains > TargetTimeToDie || CDSummonInfernalRemains + 135000 > TargetTimeToDie);

            //actions+=/conflagrate,if=buff.backdraft.down&soul_shard>=1.5-0.3*talent.flashover.enabled&!variable.pool_soul_shards
            if (Aimsharp.CanCast("Conflagrate"))
            {
                if (!BuffBackdraftUp && SoulShard >= 1.5 * (TalentFlashoverEnabled ? 1 : 0) && !PoolSoulShards)
                {
                    Aimsharp.Cast("Conflagrate");
                    return true;
                }
            }

            //actions+=/shadowburn,if=soul_shard<2&(!variable.pool_soul_shards|charges>1)
            if (Aimsharp.CanCast("Shadowburn"))
            {
                if (SoulShard < 2 && (!PoolSoulShards || Aimsharp.SpellCharges("Shadowburn") > 1))
                {
                    Aimsharp.Cast("Shadowburn");
                    return true;
                }
            }

            //actions+=/chaos_bolt,if=(talent.grimoire_of_supremacy.enabled|azerite.crashing_chaos.enabled)&pet.infernal.active|buff.dark_soul_instability.up|buff.reckless_force.react&buff.reckless_force.remains>cast_time
            if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
            {
                if ((TalentGrimoireOfSupremacyEnabled || AzeriteCrashingChaosRank > 0) && InfernalActive || BuffDarkSoulInstabilityUp || BuffRecklessForceUp && BuffRecklessForceRemains > ChaosBoltCastTime)
                {
                    Aimsharp.Cast("Chaos Bolt");
                    return true;
                }
            }

            //actions+=/chaos_bolt,if=buff.backdraft.up&!variable.pool_soul_shards&!talent.eradication.enabled
            if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
            {
                if (BuffBackdraftUp && !PoolSoulShards && !TalentEradicationEnabled)
                {
                    Aimsharp.Cast("Chaos Bolt");
                    return true;
                }
            }

            //actions+=/chaos_bolt,if=!variable.pool_soul_shards&talent.eradication.enabled&(debuff.eradication.remains<cast_time|buff.backdraft.up)
            if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
            {
                if (!PoolSoulShards && TalentEradicationEnabled && (DebuffEradicationRemains < ChaosBoltCastTime || BuffBackdraftUp))
                {
                    Aimsharp.Cast("Chaos Bolt");
                    return true;
                }
            }

            //actions+=/chaos_bolt,if=(soul_shard>=4.5-0.2*active_enemies)&(!talent.grimoire_of_supremacy.enabled|cooldown.summon_infernal.remains>7)
            if (Aimsharp.CanCast("Chaos Bolt") && !Moving)
            {
                if ((SoulShard >= 4.5 - .2*(EnemiesNearTarget)) && (!TalentGrimoireOfSupremacyEnabled || CDSummonInfernalRemains > 7000))
                {
                    Aimsharp.Cast("Chaos Bolt");
                    return true;
                }
            }

            //actions+=/conflagrate,if=charges>1
            if (Aimsharp.CanCast("Conflagrate"))
            {
                if (ConflagrateCharges > 1)
                {
                    Aimsharp.Cast("Conflagrate");
                    return true;
                }
            }

            //actions+=/incinerate
            if (Aimsharp.CanCast("Incinerate") && (!Moving || BuffChaoticInfernoUp))
            {
                Aimsharp.Cast("Incinerate");
                return true;
            }

            return false;
        }


        public override bool OutOfCombatTick()
        {
            return false;
        }

    }
}
