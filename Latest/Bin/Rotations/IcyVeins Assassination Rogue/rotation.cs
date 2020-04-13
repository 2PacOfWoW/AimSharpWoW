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
    public class IcyVeinsAssaRog : Rotation
    {


        public override void LoadSettings()
        {
            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "Bloodelf", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Shrouded Suffocation Traits", 0, 3, 1));

            Settings.Add(new Setting("# Echoing Blades Traits", 0, 3, 0));

        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
             Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Ice Series: Assassination Rogue - v 1.0", Color.Blue);
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

            Spellbook.Add("Stealth");
            Spellbook.Add("Garrote");
            Spellbook.Add("Rupture");
            Spellbook.Add("Exsanguinate");
            Spellbook.Add("Toxic Blade");
            Spellbook.Add("Vendetta");
            Spellbook.Add("Marked for Death");
            Spellbook.Add("Vanish");
            Spellbook.Add("Crimson Tempest");
            Spellbook.Add("Envenom");
            Spellbook.Add("Fan of Knives");
            Spellbook.Add("Blindside");
            Spellbook.Add("Mutilate");

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

            Buffs.Add("Stealth");
            Buffs.Add("Subterfuge");
            Buffs.Add("Vanish");
            Buffs.Add("Master Assassin");
            Buffs.Add("Blindside");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");
            Debuffs.Add("Rupture");
            Debuffs.Add("Garrote");
            Debuffs.Add("Deadly Poison");
            Debuffs.Add("Crippling Poison");
            Debuffs.Add("Wound Poison");
            Debuffs.Add("Vendetta");
            Debuffs.Add("Toxic Blade");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            // CustomCommands.Add("Prepull");
        }




        bool BuffedGarrote = false;
        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {

            bool Fighting = Aimsharp.Range("target") <= 8 && Aimsharp.TargetIsEnemy();
            bool Moving = Aimsharp.PlayerIsMoving();
            float Haste = Aimsharp.Haste() / 100f;
            float SpellHaste = 1f / (1f + Haste);
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
            int CDRazorCoral = Aimsharp.ItemCooldown("Razor Coral");

            int Energy = Aimsharp.Power("player");
            int ComboPoints = Aimsharp.PlayerSecondaryPower();
            int MaxEnergy = Aimsharp.PlayerMaxPower();
            int EnergyDefecit = MaxEnergy - Energy;
            int MaxComboPoints = Aimsharp.Talent(3, 2) ? 6 : 5;
            int ComboPointsDefecit = MaxComboPoints - ComboPoints;

            bool TalentVigor = Aimsharp.Talent(3, 1);
            bool TalentNightstalker = Aimsharp.Talent(2, 1);
            bool TalentSubterfuge = Aimsharp.Talent(2, 2);
            bool TalentExsanguinate = Aimsharp.Talent(6, 3);
            bool TalentMasterAssassin = Aimsharp.Talent(2, 3);
            bool TalentToxicBlade = Aimsharp.Talent(6, 2);
            bool TalentDeeperStrategem = Aimsharp.Talent(3, 2);

            int DebuffDeadlyPoisonRemains = Aimsharp.DebuffRemaining("Deadly Poison") - GCD;
            bool DebuffDeadlyPoisonUp = DebuffDeadlyPoisonRemains > 0;
            int DebuffWoundPoisonRemains = Aimsharp.DebuffRemaining("Wound Poison") - GCD;
            bool DebuffWoundPoisonUp = DebuffWoundPoisonRemains > 0;
            int DebuffCripplingPoisonRemains = Aimsharp.DebuffRemaining("Crippling Poison") - GCD;
            bool DebuffCripplingPoisonUp = DebuffCripplingPoisonRemains > 0;
            int DebuffGarroteRemains = Aimsharp.DebuffRemaining("Garrote") - GCD;
            bool DebuffGarroteUp = DebuffGarroteRemains > 0;
            int DebuffRuptureRemains = Aimsharp.DebuffRemaining("Rupture") - GCD;
            bool DebuffRuptureUp = DebuffRuptureRemains > 0;
            int DebuffVendettaRemains = Aimsharp.DebuffRemaining("Vendetta") - GCD;
            bool DebuffVendettaUp = DebuffVendettaRemains > 0;
            int DebuffToxicBladeRemains = Aimsharp.DebuffRemaining("Toxic Blade") - GCD;
            bool DebuffToxicBladeUp = DebuffToxicBladeRemains > 0;
            int BuffSubterfugeRemains = Aimsharp.BuffRemaining("Subterfuge") - GCD;
            bool BuffSubterfugeUp = BuffSubterfugeRemains > 0;
            int BuffVanishRemains = Aimsharp.BuffRemaining("Vanish") - GCD;
            bool BuffVanishUp = BuffVanishRemains > 0;
            bool BuffStealthUp = Aimsharp.HasBuff("Stealth");
            int BuffMasterAssassinRemains = Aimsharp.BuffRemaining("Master Assassin") - GCD;
            bool BuffMasterAssassinUp = BuffMasterAssassinRemains > 0;
            int BuffBlindsideRemains = Aimsharp.BuffRemaining("Blindside") - GCD;
            bool BuffBlindsideUp = BuffBlindsideRemains > 0;

            if (!DebuffGarroteUp)
                BuffedGarrote = false;

            int CDExsanguinateRemains = Aimsharp.SpellCooldown("Exsanguinate") - GCD;
            bool CDExsanguinateReady = CDExsanguinateRemains <= 10;
            int CDToxicBladeRemains = Aimsharp.SpellCooldown("Toxic Blade") - GCD;
            bool CDToxicBladeReady = CDToxicBladeRemains <= 10;
            int CDVendettaRemains = Aimsharp.SpellCooldown("Vendetta") - GCD;
            bool CDVendettaReady = CDVendettaRemains <= 10;
            int CDVanishRemains = Aimsharp.SpellCooldown("Vanish");
            bool CDVanishReady = CDVanishRemains <= 10;
            int CDGarroteRemains = Aimsharp.SpellCooldown("Garrote") - GCD;
            bool CDGarroteReady = CDGarroteRemains <= 10;

            int AzeriteShroudedSuffocationRank = GetSlider("# Shrouded Suffocation Traits");
            int AzeriteEchoingBladesRank = GetSlider("# Echoing Blades Traits");

            float EnergyRegen = 10f * (1f + Haste) * (TalentVigor ? 1.1f : 1f);
            int TimeUntilMaxEnergy = (int)((EnergyDefecit * 1000f) / EnergyRegen);

            bool GarroteRefreshable = DebuffGarroteRemains < 5400;
            bool RuptureRefreshable = DebuffRuptureRemains < 7200;
            bool Stealthed = BuffStealthUp || BuffVanishUp || BuffSubterfugeUp;
            bool Poisoned = DebuffDeadlyPoisonUp || DebuffCripplingPoisonUp || DebuffWoundPoisonUp;
            int PoisonedBleeds = ((DebuffGarroteUp ? 1 : 0) + (DebuffRuptureUp ? 1 : 0)) * (Poisoned ? 1 : 0);
            //actions+=/variable,name=energy_regen_combined,value=energy.regen+poisoned_bleeds*7%(2*spell_haste)
            float VariableEnergyRegenCombined = EnergyRegen + PoisonedBleeds * 7 % (2 * SpellHaste);
            //actions+=/variable,name=single_target,value=spell_targets.fan_of_knives<2

            if (IsChanneling)
                return false;

            //actions=stealth
            if (Aimsharp.CanCast("Stealth", "player"))
            {
                Aimsharp.Cast("Stealth");
                return true;
            }

            if (!Stealthed && DebuffRuptureUp && !BuffMasterAssassinUp && !NoCooldowns)
            {
                //actions.essences=concentrated_flame,if=energy.time_to_max>1&!debuff.vendetta.up&(!dot.concentrated_flame_burn.ticking&!action.concentrated_flame.in_flight|full_recharge_time<gcd.max)
                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame"))
                    {
                        if (TimeUntilMaxEnergy > 1000 && !DebuffVendettaUp && (FlameFullRecharge < GCDMAX))
                        {
                            Aimsharp.Cast("Concentrated Flame");
                            return true;
                        }
                    }
                }

                //actions.essences+=/blood_of_the_enemy,if=debuff.vendetta.up&(!talent.toxic_blade.enabled|debuff.toxic_blade.up&combo_points.deficit<=1|debuff.vendetta.remains<=10)|target.time_to_die<=10
                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        if (DebuffVendettaUp && (!TalentToxicBlade || DebuffToxicBladeUp && ComboPointsDefecit <= 1 || DebuffVendettaRemains <= 10000) || TargetTimeToDie <= 10000)
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }
                }

                //actions.essences+=/guardian_of_azeroth,if=cooldown.vendetta.remains<3|debuff.vendetta.up|target.time_to_die<30
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        if (CDVendettaRemains < 3000 || DebuffVendettaUp || TargetTimeToDie < 30000)
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }
                }

                //actions.essences+=/focused_azerite_beam,if=spell_targets.fan_of_knives>=2|raid_event.adds.in>60&energy<70
                if (MajorPower == "Focused Azerite Beam" && Range < 15)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        if (EnemiesInMelee >= 2 || Energy < 70)
                        {
                            Aimsharp.Cast("Focused Azerite Beam");
                            return true;
                        }
                    }
                }

                //actions.essences+=/the_unbound_force,if=buff.reckless_force.up|buff.reckless_force_counter.stack<10
                if (MajorPower == "The Unbound Force")
                {
                    if (Aimsharp.CanCast("The Unbound Force"))
                    {
                        if (BuffRecklessForceUp || BuffRecklessForceStacks < 10)
                        {
                            Aimsharp.Cast("The Unbound Force");
                            return true;
                        }
                    }
                }

                //actions.essences+=/worldvein_resonance
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        Aimsharp.Cast("Worldvein Resonance");
                        return true;
                    }
                }

                //actions.essences+=/memory_of_lucid_dreams,if=energy<50&!cooldown.vendetta.up
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (Energy < 50 && !CDVendettaReady)
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }
            }

            if (Aimsharp.CanCast("Rupture"))
            {
                if (ComboPoints >= 4 && !DebuffRuptureUp)
                {
                    Aimsharp.Cast("Rupture");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Garrote"))
            {
                if (!DebuffGarroteUp)
                {
                    Aimsharp.Cast("Garrote");
                    return true;
                }
            }

            //generic trinket usage

            if (!NoCooldowns)
            {
                if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic")
                {
                    Aimsharp.Cast("TopTrink", true);
                    return true;
                }

                if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic")
                {
                    Aimsharp.Cast("BotTrink", true);
                    return true;
                }

                if (RacialPower == "Troll" && Fighting)
                {
                    if (Aimsharp.CanCast("Berserking", "player"))
                    {
                        Aimsharp.Cast("Berserking", true);
                        return true;
                    }
                }

                if (RacialPower == "Orc" && Fighting)
                {
                    if (Aimsharp.CanCast("Blood Fury", "player"))
                    {
                        Aimsharp.Cast("Blood Fury", true);
                        return true;
                    }
                }

                if (RacialPower == "Troll" && Fighting)
                {
                    if (Aimsharp.CanCast("Berserking", "player"))
                    {
                        Aimsharp.Cast("Berserking", true);
                        return true;
                    }
                }

                if (RacialPower == "Lightforged Draenei" && Fighting)
                {
                    Aimsharp.Cast("Light's Judgment", true);
                    return true;
                }

                if (RacialPower == "Mag'har Orc" && Fighting)
                {
                    if (Aimsharp.CanCast("Ancestral Call", "player"))
                    {
                        Aimsharp.Cast("Ancestral Call", true);
                        return true;
                    }
                }

                if (RacialPower == "Dark Iron Dwarf" && Fighting)
                {
                    if (Aimsharp.CanCast("Fireblood", "player"))
                    {
                        Aimsharp.Cast("Fireblood", true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Vendetta"))
                {
                    Aimsharp.Cast("Vendetta");
                    return true;
                }
            }

          /*  if (Aimsharp.CanCast("Vanish","player"))
            {
                if (TalentSubterfuge && !DebuffGarroteUp)
                {
                    Aimsharp.Cast("Vanish");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Vanish", "player"))
            {
                if (TalentMasterAssassin && ComboPoints >= 5)
                {
                    Aimsharp.Cast("Vanish");
                    return true;
                }
            } */

            if (Aimsharp.CanCast("Toxic Blade") && EnemiesInMelee < 3)
            {
                Aimsharp.Cast("Toxic Blade");
                return true;
            }

            if (Aimsharp.CanCast("Crimson Tempest", "player") && EnemiesInMelee >= 3 && ComboPointsDefecit <= 1) 
            {
                Aimsharp.Cast("Crimson Tempest");
                return true;
            }


            if (Aimsharp.CanCast("Envenom"))
            {
                if (ComboPointsDefecit <= 1)
                {
                    Aimsharp.Cast("Envenom");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Fan of Knives", "player") && EnemiesInMelee >= 3)
            {
                Aimsharp.Cast("Fan of Knives");
                return true;
            }

            if (Aimsharp.CanCast("Mutilate"))
            {
                Aimsharp.Cast("Mutilate");
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
