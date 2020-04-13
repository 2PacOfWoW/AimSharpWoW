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
    public class PerfSimFury : Rotation
    {


        public override void LoadSettings()
        {


            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Ashvane's Razor Coral", "Pocket-Sized Computation Device", "Galecaller's Boon", "Shiver Venom Relic", "Lurker's Insidious Gift", "Notorious Gladiator's Badge", "Sinister Gladiator's Badge", "Sinister Gladiator's Medallion", "Notorious Gladiator's Medallion", "Vial of Animated Blood", "First Mate's Spyglass", "Jes' Howler", "Ashvane's Razor Coral", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Cold Steel Hot Blood Traits", 0, 3, 1));



        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            Aimsharp.PrintMessage("Perfect Simcraft Series: Fury Warrior - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 2123123", Color.Blue);
            Aimsharp.PrintMessage("These macros can be used for manual control:", Color.Blue);
            Aimsharp.PrintMessage("/xxxxx Potions", Color.Blue);
            Aimsharp.PrintMessage("--Toggles using buff potions on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx SaveCooldowns", Color.Blue);
            Aimsharp.PrintMessage("--Toggles the use of big cooldowns on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx AOE", Color.Blue);
            Aimsharp.PrintMessage("--Toggles AOE mode on/off.", Color.Blue);
            // Aimsharp.PrintMessage(" ");         
            // Aimsharp.PrintMessage("/xxxxx Prepull 10", Color.Blue);
            // Aimsharp.PrintMessage("--Starts the prepull actions.", Color.Blue);
            // Aimsharp.PrintMessage(" ");
            // Aimsharp.PrintMessage("--Replace xxxxx with first 5 letters of your addon, lowercase.", Color.Blue);

            Aimsharp.Latency = 100;
            Aimsharp.QuickDelay = 125;

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

            Spellbook.Add("Charge");
            Spellbook.Add("Heroic Leap");
            Spellbook.Add("Rampage");
            Spellbook.Add("Recklessness");
            Spellbook.Add("Siegebreaker");
            Spellbook.Add("Whirlwind");
            Spellbook.Add("Execute");
            Spellbook.Add("Furious Slash");
            Spellbook.Add("Bladestorm");
            Spellbook.Add("Bloodthirst");
            Spellbook.Add("Dragon Roar");
            Spellbook.Add("Raging Blow");

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

            Buffs.Add("Recklessness");
            Buffs.Add("Meat Cleaver");
            Buffs.Add("Enrage");
            Buffs.Add("Furious Slash");
            Buffs.Add("Whirlwind");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");
            Debuffs.Add("Siegebreaker");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("leap cursor", "/cast [@cursor] Heroic Leap");
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
        }





        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {

            bool Fighting = Aimsharp.Range("target") <= 8 && Aimsharp.TargetIsEnemy();
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
            int GCDMAX = (int)(1500f / (Haste + 1f));
            int GCD = Aimsharp.GCD();
            int Latency = Aimsharp.Latency;
            int TargetTimeToDie = 1000000000;
            bool HasLust = Aimsharp.HasBuff("Bloodlust", "player", false) || Aimsharp.HasBuff("Heroism", "player", false) || Aimsharp.HasBuff("Time Warp", "player", false) || Aimsharp.HasBuff("Ancient Hysteria", "player", false) || Aimsharp.HasBuff("Netherwinds", "player", false) || Aimsharp.HasBuff("Drums of Rage", "player", false);
            int FlameFullRecharge = (int)(Aimsharp.RechargeTime("Concentrated Flame") - GCD + (30000f) * (1f - Aimsharp.SpellCharges("Concentrated Flame")));
            int ShiverVenomStacks = Aimsharp.DebuffStacks("Shiver Venom");

            int CDGuardianOfAzerothRemains = Aimsharp.SpellCooldown("Guardian of Azeroth") - GCD;
            bool BuffGuardianOfAzerothUp = Aimsharp.HasBuff("Guardian of Azeroth");
            int CDBloodOfTheEnemyRemains = Aimsharp.SpellCooldown("Blood of the Enemy") - GCD;
            int BuffMemoryOfLucidDreamsRemains = Aimsharp.BuffRemaining("Memory of Lucid Dreams") - GCD;
            bool BuffMemoryOfLucidDreamsUp = BuffMemoryOfLucidDreamsRemains > 0;
            bool DebuffRazorCoralUp = Aimsharp.HasDebuff("Razor Coral");
            bool DebuffConductiveInkUp = Aimsharp.HasDebuff("Conductive Ink");

            int Rage = Aimsharp.Power("player");

            int CDRecklessnessRemains = Aimsharp.SpellCooldown("Recklessness") - GCD;
            int BuffRecklessnessRemains = Aimsharp.BuffRemaining("Recklessness") - GCD;
            bool BuffRecklessnessUp = BuffRecklessnessRemains > 0;
            int BuffSiegebreakerRemains = Aimsharp.DebuffRemaining("Siegebreaker") - GCD;
            bool BuffSiegebreakerUp = BuffSiegebreakerRemains > 0;
            int BuffRecklessForceRemains = Aimsharp.BuffRemaining("Reckless Force") - GCD;
            bool BuffRecklessForceUp = BuffRecklessForceRemains > 0;
            int BuffWhirlwindRemains = Aimsharp.BuffRemaining("Whirlwind") - GCD;
            bool BuffWhirlwindUp = BuffWhirlwindRemains > 0;
            int BuffEnrageRemains = Aimsharp.BuffRemaining("Enrage") - GCD;
            bool BuffEnrageUp = BuffEnrageRemains > 0;
            int BuffFuriousSlashRemains = Aimsharp.BuffRemaining("Furious Slash") - GCD;
            bool BuffFuriousSlashUp = BuffFuriousSlashRemains > 0;


            bool TalentFrothingBerserker = Aimsharp.Talent(5, 3);
            bool TalentCarnage = Aimsharp.Talent(5, 1);
            bool TalentMassacre = Aimsharp.Talent(5, 2);
            bool TalentMeatCleaver = Aimsharp.Talent(6, 1);

            int AzeriteColdSteelHotBloodRank = GetSlider("# Cold Steel Hot Blood Traits");

            int RagingBlowCharges = Aimsharp.SpellCharges("Raging Blow");

            if (!AOE)
            {
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }




            if (IsChanneling)
                return false;


            if (UsePotion && Fighting)
            {
                if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                {

                    Aimsharp.Cast("potion", true);
                    return true;

                }
            }

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

            //actions+=/rampage,if=cooldown.recklessness.remains<3
            if (Aimsharp.CanCast("Rampage"))
            {
                if (CDRecklessnessRemains < 3000)
                {
                    Aimsharp.Cast("Rampage");
                    return true;
                }
            }

            //actions+=/blood_of_the_enemy,if=buff.recklessness.up
            if (MajorPower == "Blood of the Enemy" && !NoCooldowns && EnemiesInMelee > 0)
            {
                if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                {
                    if (BuffRecklessnessUp)
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }
            }

            //actions+=/worldvein_resonance,if=!buff.recklessness.up&!buff.siegebreaker.up
            if (MajorPower == "Worldvein Resonance" && !NoCooldowns)
            {
                if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                {
                    if (!BuffRecklessnessUp && !BuffSiegebreakerUp)
                    {
                        Aimsharp.Cast("Worldvein Resonance");
                        return true;
                    }
                }
            }

            //actions+=/focused_azerite_beam,if=!buff.recklessness.up&!buff.siegebreaker.up
            if (MajorPower == "Focused Azerite Beam" && !NoCooldowns && Range < 15)
            {
                if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                {
                    if (!BuffRecklessnessUp && !BuffSiegebreakerUp)
                    {
                        Aimsharp.Cast("Focused Azerite Beam");
                        return true;
                    }
                }
            }

            //actions+=/concentrated_flame,if=!buff.recklessness.up&!buff.siegebreaker.up&dot.concentrated_flame_burn.remains=0  
            //NOTE: improved conc flame logic?
            if (MajorPower == "Concentrated Flame")
            {
                if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                {
                    if (!BuffRecklessnessUp && !BuffSiegebreakerUp)
                    {
                        Aimsharp.Cast("Concentrated Flame");
                        return true;
                    }
                }
            }

            if (MajorPower == "Reaping Flames")
            {
                if (Aimsharp.CanCast("Reaping Flames"))
                {
                    if (!BuffRecklessnessUp && !BuffSiegebreakerUp)
                    {
                        Aimsharp.Cast("Reaping Flames");
                        return true;
                    }
                }
            }

            //actions +=/ the_unbound_force,if= buff.reckless_force.up
            if (MajorPower == "The Unbound Force" && !NoCooldowns)
            {
                if (Aimsharp.CanCast("The Unbound Force"))
                {
                    if (BuffRecklessForceUp)
                    {
                        Aimsharp.Cast("The Unbound Force");
                        return true;
                    }
                }
            }

            //actions+=/guardian_of_azeroth,if=!buff.recklessness.up
            if (MajorPower == "Guardian of Azeroth" && !NoCooldowns && Fighting)
            {
                if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                {
                    if (!BuffRecklessnessUp)
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }
            }

            //actions+=/memory_of_lucid_dreams,if=!buff.recklessness.up
            if (MajorPower == "Memory of Lucid Dreams" && !NoCooldowns && Fighting)
            {
                if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                {
                    if (!BuffRecklessnessUp)
                    {
                        Aimsharp.Cast("Memory of Lucid Dreams");
                        return true;
                    }
                }
            }

            //actions+=/recklessness,if=!essence.condensed_lifeforce.major&!essence.blood_of_the_enemy.major|cooldown.guardian_of_azeroth.remains>20|buff.guardian_of_azeroth.up|cooldown.blood_of_the_enemy.remains<gcd
            if (Aimsharp.CanCast("Recklessness", "player") && Fighting && !NoCooldowns)
            {
                if (MajorPower != "Guardian of Azeroth" && MajorPower != "Blood of the Enemy" || CDGuardianOfAzerothRemains > 20000 || BuffGuardianOfAzerothUp || CDBloodOfTheEnemyRemains < GCDMAX)
                {
                    Aimsharp.Cast("Recklessness");
                    return true;
                }
            }

            //actions+=/whirlwind,if=spell_targets.whirlwind>1&!buff.meat_cleaver.up
            if (Aimsharp.CanCast("Whirlwind", "player") && Fighting)
            {
                if (EnemiesInMelee > 1 && (!BuffWhirlwindUp))
                {
                    Aimsharp.Cast("Whirlwind");
                    return true;
                }
            }

            //actions +=/ use_item,name = ashvanes_razor_coral,if= !debuff.razor_coral_debuff.up | (target.health.pct < 30.1 & debuff.conductive_ink_debuff.up) | (!debuff.conductive_ink_debuff.up & buff.memory_of_lucid_dreams.up | prev_gcd.2.guardian_of_azeroth | prev_gcd.2.recklessness & (!essence.memory_of_lucid_dreams.major & !essence.condensed_lifeforce.major))
            if (Aimsharp.CanUseItem("Ashvane's Razor Coral") && Fighting)
            {
                if (!DebuffRazorCoralUp || (TargetHealth <= 30 && DebuffConductiveInkUp) || (!DebuffConductiveInkUp && BuffMemoryOfLucidDreamsUp || CDGuardianOfAzerothRemains > 180000 - GCDMAX * 2 || CDRecklessnessRemains > 90000 - GCDMAX * 2 && (MajorPower != "Memory of Lucid Dreams" && MajorPower != "Guardian of Azeroth")))
                {
                    Aimsharp.Cast("Ashvane's Razor Coral", true);
                    return true;
                }
            }

            //actions +=/ blood_fury
            if (RacialPower == "Orc" && Fighting)
            {
                if (Aimsharp.CanCast("Blood Fury", "player"))
                {
                    Aimsharp.Cast("Blood Fury", true);
                    return true;
                }
            }

            //actions+=/berserkin
            if (RacialPower == "Troll" && Fighting)
            {
                if (Aimsharp.CanCast("Berserking", "player"))
                {
                    Aimsharp.Cast("Berserking", true);
                    return true;
                }
            }

            //actions+=/lights_judgment,if=buff.recklessness.down
            if (RacialPower == "Lightforged Draenei" && Fighting)
            {
                if (Aimsharp.CanCast("Light's Judgment", "player"))
                {
                    if (!BuffRecklessnessUp)
                    {
                        Aimsharp.Cast("Light's Judgment", true);
                        return true;
                    }
                }
            }

            //actions+=/fireblood
            if (RacialPower == "Dark Iron Dwarf" && Fighting)
            {
                if (Aimsharp.CanCast("Fireblood", "player"))
                {
                    Aimsharp.Cast("Fireblood", true);
                    return true;
                }
            }

            //actions+=/ancestral_call
            if (RacialPower == "Mag'har Orc" && Fighting)
            {
                if (Aimsharp.CanCast("Ancestral Call", "player"))
                {
                    Aimsharp.Cast("Ancestral Call", true);
                    return true;
                }
            }


            //actions.single_target=siegebreaker
            if (Aimsharp.CanCast("Siegebreaker"))
            {
                Aimsharp.Cast("Siegebreaker");
                return true;
            }

            //actions.single_target +=/ rampage,if= (buff.recklessness.up | buff.memory_of_lucid_dreams.up) | (talent.frothing_berserker.enabled | talent.carnage.enabled & (buff.enrage.remains < gcd | rage > 90) | talent.massacre.enabled & (buff.enrage.remains < gcd | rage > 90))
            if (Aimsharp.CanCast("Rampage"))
            {
                if ((BuffRecklessnessUp || BuffMemoryOfLucidDreamsUp) || (TalentFrothingBerserker || TalentCarnage && (BuffEnrageRemains < GCDMAX || Rage > 90) || TalentMassacre && (BuffEnrageRemains < GCDMAX || Rage > 90)))
                {
                    Aimsharp.Cast("Rampage");
                    return true;
                }
            }

            //actions.single_target+=/execute
            if (Aimsharp.CanCast("Execute"))
            {
                Aimsharp.Cast("Execute");
                return true;
            }

            //actions.single_target+=/furious_slash,if=!buff.bloodlust.up&buff.furious_slash.remains<3
            if (Aimsharp.CanCast("Furious Slash"))
            {
                if (!HasLust && BuffFuriousSlashRemains < 3000)
                {
                    Aimsharp.Cast("Furious Slash");
                    return true;
                }
            }

            //actions.single_target+=/bladestorm,if=prev_gcd.1.rampage
            if (Aimsharp.CanCast("Bladestorm", "player") && Fighting && BuffEnrageUp)
            {
                    Aimsharp.Cast("Bladestorm");
                    return true;
            }

            //actions.single_target+=/bloodthirst,if=buff.enrage.down|azerite.cold_steel_hot_blood.rank>1
            if (Aimsharp.CanCast("Bloodthirst"))
            {
                if (!BuffEnrageUp || AzeriteColdSteelHotBloodRank > 1)
                {
                    Aimsharp.Cast("Bloodthirst");
                    return true;
                }
            }

            //actions.single_target+=/dragon_roar,if=buff.enrage.up
            if (Aimsharp.CanCast("Dragon Roar", "player") && Fighting)
            {
                if (BuffEnrageUp)
                {
                    Aimsharp.Cast("Dragon Roar");
                    return true;
                }
            }

            //actions.single_target+=/raging_blow,if=charges=2
            if (Aimsharp.CanCast("Raging Blow"))
            {
                if (RagingBlowCharges == 2)
                {
                    Aimsharp.Cast("Raging Blow");
                    return true;
                }
            }

            //actions.single_target+=/bloodthirst
            if (Aimsharp.CanCast("Bloodthirst"))
            {
                Aimsharp.Cast("Bloodthirst");
                return true;
            }

            //actions.single_target+=/raging_blow,if=talent.carnage.enabled|(talent.massacre.enabled&rage<80)|(talent.frothing_berserker.enabled&rage<90)
            if (Aimsharp.CanCast("Raging Blow"))
            {
                if (TalentCarnage || (TalentMassacre && Rage < 80) || (TalentFrothingBerserker && Rage < 90))
                {
                    Aimsharp.Cast("Raging Blow");
                    return true;
                }
            }

            //actions.single_target+=/furious_slash,if=talent.furious_slash.enabled
            if (Aimsharp.CanCast("Furious Slash"))
            {
                Aimsharp.Cast("Furious Slash");
                return true;
            }

            //actions.single_target+=/whirlwind
            if (Aimsharp.CanCast("Whirlwind", "player") && Fighting)
            {
                Aimsharp.Cast("Whirlwind");
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
