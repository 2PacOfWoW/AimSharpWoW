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
    public class PerfSimFrostDK : Rotation
    {


        public override void LoadSettings()
        {


            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Ashvane's Razor Coral", "Pocket-Sized Computation Device", "Galecaller's Boon", "Shiver Venom Relic", "Lurker's Insidious Gift", "Notorious Gladiator's Badge", "Sinister Gladiator's Badge", "Sinister Gladiator's Medallion", "Notorious Gladiator's Medallion", "Vial of Animated Blood", "First Mate's Spyglass", "Jes' Howler", "Ashvane's Razor Coral", "Knot of Ancient Fury", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "Bloodelf", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Icy Citadel Traits", 0, 3, 1));
            Settings.Add(new Setting("# Frozen Tempest Traits", 0, 3, 1));



        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            // Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Perfect Simcraft Series: Frost DK - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 3122113", Color.Blue);
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

            Aimsharp.Latency = 0;
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
            if (RacialPower == "Bloodelf")
                Spellbook.Add("Arcane Torrent");

            Spellbook.Add("Breath of Sindragosa");
            Spellbook.Add("Howling Blast");
            Spellbook.Add("Glacial Advance");
            Spellbook.Add("Frost Strike");
            Spellbook.Add("Chill Streak");
            Spellbook.Add("Empower Rune Weapon");
            Spellbook.Add("Pillar of Frost");
            Spellbook.Add("Chains of Ice");
            Spellbook.Add("Frostwyrm's Fury");
            Spellbook.Add("Obliterate");
            Spellbook.Add("Frostscythe");
            Spellbook.Add("Remorseless Winter");
            Spellbook.Add("Horn of Winter");

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

            Buffs.Add("Icy Talons");
            Buffs.Add("Pillar of Frost");
            Buffs.Add("Breath of Sindragosa");
            Buffs.Add("Empower Rune Weapon");
            Buffs.Add("Cold Heart");
            Buffs.Add("Seething Rage");
            Buffs.Add("Unholy Strength");
            Buffs.Add("Icy Citadel");
            Buffs.Add("Rime");
            Buffs.Add("Killing Machine");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");
            Debuffs.Add("Frost Fever");
            Debuffs.Add("Razorice");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            // CustomCommands.Add("Prepull");
            // CustomCommands.Add("LightAOE");
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
            int BuffRecklessForceRemains = Aimsharp.BuffRemaining("Reckless Force") - GCD;
            bool BuffRecklessForceUp = BuffRecklessForceRemains > 0;
            int BuffRecklessForceStacks = Aimsharp.BuffStacks("Reckless Force");

            int RunicPower = Aimsharp.Power("player");
            int Runes = Aimsharp.PlayerSecondaryPower();
            int MaxRunicPower = Aimsharp.PlayerMaxPower();
            int RunicDefecit = MaxRunicPower - RunicPower;

            bool TalentBreathOfSindragosa = Aimsharp.Talent(7, 3);
            bool TalentColdHeart = Aimsharp.Talent(1, 3);
            bool TalentFrostwyrmsFury = Aimsharp.Talent(6, 3);
            bool TalentIcecap = Aimsharp.Talent(7, 1);
            bool TalentFrostscythe = Aimsharp.Talent(4, 3);
            bool TalentRunicAttenuation = Aimsharp.Talent(2, 1);
            bool TalentGatheringStorm = Aimsharp.Talent(6, 1);
            bool TalentObliteration = Aimsharp.Talent(7, 2);
            bool TalentFrozenPulse = Aimsharp.Talent(4, 2);

            int DebuffFrostFeverRemains = Aimsharp.DebuffRemaining("Frost Fever") - GCD;
            bool FrostFeverTicking = DebuffFrostFeverRemains > 0;
            int BuffIcyTalonsRemains = Aimsharp.BuffRemaining("Icy Talons") - GCD;
            bool BuffIcyTalonsUp = BuffIcyTalonsRemains > 0;
            int BuffPillarOfFrostRemains = Aimsharp.BuffRemaining("Pillar of Frost") - GCD;
            bool BuffPillarOfFrostUp = BuffPillarOfFrostRemains > 0;
            bool BuffBreathOfSindragosaUp = Aimsharp.HasBuff("Breath of Sindragosa");
            int BuffEmpowerRuneWeaponRemains = Aimsharp.BuffRemaining("Empower Rune Weapon") - GCD;
            bool BuffEmpowerRuneWeaponUp = BuffEmpowerRuneWeaponRemains > 0;
            int BuffEnrageRemains = Aimsharp.BuffRemaining("Enrage") - GCD;
            bool BuffEnrageUp = BuffEnrageRemains > 0;
            int BuffFuriousSlashRemains = Aimsharp.BuffRemaining("Furious Slash") - GCD;
            bool BuffFuriousSlashUp = BuffFuriousSlashRemains > 0;
            int BuffColdHeartStack = Aimsharp.BuffStacks("Cold Heart");
            int DebuffRazoriceStack = Aimsharp.DebuffStacks("Razorice");
            int DebuffRazoriceRemains = Aimsharp.DebuffRemaining("Razorice") - GCD;
            int BuffSeethingRageRemains = Aimsharp.BuffRemaining("Seething Rage") - GCD;
            bool BuffSeethingRageUp = BuffSeethingRageRemains > 0;
            int BuffUnholyStrengthRemains = Aimsharp.BuffRemaining("Unholy Strength") - GCD;
            bool BuffUnholyStrengthUp = BuffUnholyStrengthRemains > 0;
            int BuffIcyCitadelRemains = Aimsharp.BuffRemaining("Icy Citadel") - GCD;
            bool BuffIcyCitadelUp = BuffIcyCitadelRemains > 0;
            int BuffIcyCitadelStacks = Aimsharp.BuffStacks("Icy Citadel");
            int BuffRimeRemains = Aimsharp.BuffRemaining("Rime") - GCD;
            bool BuffRimeUp = BuffRimeRemains > 0;
            int BuffKillingMachineRemains = Aimsharp.BuffRemaining("Killing Machine") - GCD;
            bool BuffKillingMachineUp = BuffKillingMachineRemains > 0;
            bool DebuffFrozenPulseUp = Aimsharp.TimeUntilRunes(3) - GCD > 0;

            int CDBreathOfSindragosaRemains = Aimsharp.SpellCooldown("Breath of Sindragosa") - GCD;
            int CDEmpowerRuneWeaponRemains = Aimsharp.SpellCooldown("Empower Rune Weapon") - GCD;
            bool CDEmpowerRuneWeaponReady = CDEmpowerRuneWeaponRemains <= 10;
            int CDPillarOfFrostRemains = Aimsharp.SpellCooldown("Pillar of Frost") - GCD;
            bool CDPillarOfFrostReady = CDPillarOfFrostRemains <= 10;
            int CDFrostwyrmsFuryRemains = Aimsharp.SpellCooldown("Frostwyrm's Fury") - GCD;
            bool CDFrostwyrmsFuryReady = CDFrostwyrmsFuryRemains <= 10 && TalentFrostwyrmsFury;
            int CDRemorselessWinterRemains = Aimsharp.SpellCooldown("Remorseless Winter") - GCD;

            int AzeriteFrozenTempestRank = GetSlider("# Frozen Tempest Traits");
            int AzeriteIcyCitadelRank = GetSlider("# Icy Citadel Traits");

            if (!AOE)
            {
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }

            //actions.precombat+=/variable,name=other_on_use_equipped,value=(equipped.notorious_gladiators_badge|equipped.sinister_gladiators_badge|equipped.sinister_gladiators_medallion|equipped.vial_of_animated_blood|equipped.first_mates_spyglass|equipped.jes_howler|equipped.notorious_gladiators_medallion|equipped.ashvanes_razor_coral)
            bool VariableOtherOnUseEquipped = Aimsharp.IsEquipped("Notorious Gladiator's Badge") || Aimsharp.IsEquipped("Sinister Gladiator's Badge") || Aimsharp.IsEquipped("Sinister Gladiator's Medallion") || Aimsharp.IsEquipped("Notorious Gladiator's Medallion") || Aimsharp.IsEquipped("Vial of Animated Blood") || Aimsharp.IsEquipped("First Mate's Spyglass") || Aimsharp.IsEquipped("Jes' Howler") || Aimsharp.IsEquipped("Ashvane's Razor Coral");
           


            if (IsChanneling)
                return false;

            //actions +=/ howling_blast,if= !dot.frost_fever.ticking & (!talent.breath_of_sindragosa.enabled | cooldown.breath_of_sindragosa.remains > 15) 
            if (Aimsharp.CanCast("Howling Blast"))
            {
                if (!FrostFeverTicking && (!TalentBreathOfSindragosa || CDBreathOfSindragosaRemains > 15000))
                {
                    Aimsharp.Cast("Howling Blast");
                    return true;
                }
            }

            //actions+=/glacial_advance,if=buff.icy_talons.remains<=gcd&buff.icy_talons.up&spell_targets.glacial_advance>=2&(!talent.breath_of_sindragosa.enabled|cooldown.breath_of_sindragosa.remains>15)
            if (Aimsharp.CanCast("Glacial Advance", "player"))
            {
                if (BuffIcyTalonsRemains <= GCDMAX && BuffIcyTalonsUp && EnemiesInMelee >= 2 && (!TalentBreathOfSindragosa || CDBreathOfSindragosaRemains > 15000))
                {
                    Aimsharp.Cast("Glacial Advance");
                    return true;
                }
            }

            //actions+=/frost_strike,if=buff.icy_talons.remains<=gcd&buff.icy_talons.up&(!talent.breath_of_sindragosa.enabled|cooldown.breath_of_sindragosa.remains>15)
            if (Aimsharp.CanCast("Frost Strike"))
            {
                if (BuffIcyTalonsRemains <= GCDMAX && BuffIcyTalonsUp && (!TalentBreathOfSindragosa || CDBreathOfSindragosaRemains > 15000))
                {
                    Aimsharp.Cast("Frost Strike");
                    return true;
                }
            }

            if (!NoCooldowns)
            {
                //actions.essences=blood_of_the_enemy,if=buff.pillar_of_frost.remains<10&buff.breath_of_sindragosa.up|buff.pillar_of_frost.remains<10&!talent.breath_of_sindragosa.enabled
                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        if (BuffPillarOfFrostRemains < 10000 && BuffBreathOfSindragosaUp || BuffPillarOfFrostRemains < 10000 && !TalentBreathOfSindragosa)
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }
                }

                //actions.essences+=/guardian_of_azeroth
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                //actions.essences+=/chill_streak,if=buff.pillar_of_frost.remains<5&buff.pillar_of_frost.up|target.1.time_to_die<5
                if (Aimsharp.CanCast("Chill Streak"))
                {
                    if (BuffPillarOfFrostRemains < 5000 && BuffPillarOfFrostUp || TargetTimeToDie < 5000)
                    {
                        Aimsharp.Cast("Chill Streak");
                        return true;
                    }
                }

                //actions.essences+=/the_unbound_force,if=buff.reckless_force.up|buff.reckless_force_counter.stack<11
                if (MajorPower == "The Unbound Force")
                {
                    if (Aimsharp.CanCast("The Unbound Force"))
                    {
                        if (BuffRecklessForceUp || BuffRecklessForceStacks < 11)
                        {
                            Aimsharp.Cast("The Unbound Force");
                            return true;
                        }
                    }
                }

                //actions.essences+=/focused_azerite_beam,if=!buff.pillar_of_frost.up&!buff.breath_of_sindragosa.up
                if (MajorPower == "Focused Azerite Beam" && Range < 15)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        if (!BuffPillarOfFrostUp && !BuffBreathOfSindragosaUp)
                        {
                            Aimsharp.Cast("Focused Azerite Beam");
                            return true;
                        }
                    }
                }

                //actions.essences+=/concentrated_flame,if=!buff.pillar_of_frost.up&!buff.breath_of_sindragosa.up&dot.concentrated_flame_burn.remains=0
                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                    {
                        if (!BuffPillarOfFrostUp && !BuffBreathOfSindragosaUp)
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
                        Aimsharp.Cast("Reaping Flames");
                        return true;
                    }
                }

                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }

                //actions.essences+=/worldvein_resonance,if=!buff.pillar_of_frost.up&!buff.breath_of_sindragosa.up
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (!BuffPillarOfFrostUp && !BuffBreathOfSindragosaUp)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                //actions.essences+=/memory_of_lucid_dreams,if=buff.empower_rune_weapon.remains<5&buff.breath_of_sindragosa.up|(rune.time_to_2>gcd&runic_power<50)
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (BuffEmpowerRuneWeaponRemains < 5000 && BuffBreathOfSindragosaUp || (Aimsharp.TimeUntilRunes(2) - GCD > GCDMAX && RunicPower < 50))
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }

                //actions.cooldowns=use_item,name=azsharas_font_of_power,if=(cooldown.empowered_rune_weapon.ready&!variable.other_on_use_equipped)|(cooldown.pillar_of_frost.remains<=10&variable.other_on_use_equipped)
                if (Aimsharp.CanUseItem("Azshara's Font of Power"))
                {
                    if ((CDEmpowerRuneWeaponReady && !VariableOtherOnUseEquipped) || (CDPillarOfFrostRemains <= 10000 && VariableOtherOnUseEquipped))
                    {
                        Aimsharp.Cast("Azshara's Font of Power", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/use_item,name=lurkers_insidious_gift,if=talent.breath_of_sindragosa.enabled&((cooldown.pillar_of_frost.remains<=10&variable.other_on_use_equipped)|(buff.pillar_of_frost.up&!variable.other_on_use_equipped))|(buff.pillar_of_frost.up&!talent.breath_of_sindragosa.enabled)
                if (Aimsharp.CanUseItem("Lurker's Insidious Gift"))
                {
                    if (TalentBreathOfSindragosa && ((CDPillarOfFrostRemains <= 10000 && VariableOtherOnUseEquipped) || (BuffPillarOfFrostUp && !VariableOtherOnUseEquipped)) || (BuffPillarOfFrostUp && !TalentBreathOfSindragosa))
                    {
                        Aimsharp.Cast("Lurker's Insidious Gift", true);
                        return true;
                    }
                }

                //actions.cooldowns +=/ use_item,name = cyclotronic_blast,if= !buff.pillar_of_frost.up
                if (Aimsharp.CanUseItem("Pocket-Sized Computation Device"))
                {
                    if (!BuffPillarOfFrostUp)
                    {
                        Aimsharp.Cast("Pocket-Sized Computation Device", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/use_items,if=(cooldown.pillar_of_frost.ready|cooldown.pillar_of_frost.remains>20)&(!talent.breath_of_sindragosa.enabled|cooldown.empower_rune_weapon.remains>95)
                if (Aimsharp.CanUseItem(TopTrinket))
                {
                    if ((CDPillarOfFrostReady || CDPillarOfFrostRemains > 20000) && (!TalentBreathOfSindragosa || CDEmpowerRuneWeaponRemains > 95000))
                    {
                        Aimsharp.Cast(TopTrinket, true);
                        return true;
                    }
                }
                if (Aimsharp.CanUseItem(BotTrinket))
                {
                    if ((CDPillarOfFrostReady || CDPillarOfFrostRemains > 20000) && (!TalentBreathOfSindragosa || CDEmpowerRuneWeaponRemains > 95000))
                    {
                        Aimsharp.Cast(BotTrinket, true);
                        return true;
                    }
                }

                //actions.cooldowns+=/use_item,name=ashvanes_razor_coral,if=debuff.razor_coral_debuff.down
                if (Aimsharp.CanUseItem("Ashvane's Razor Coral"))
                {
                    if (!DebuffRazorCoralUp)
                    {
                        Aimsharp.Cast("Ashvane's Razor Coral", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/use_item,name=ashvanes_razor_coral,if=cooldown.empower_rune_weapon.remains>90&debuff.razor_coral_debuff.up&variable.other_on_use_equipped|buff.breath_of_sindragosa.up&debuff.razor_coral_debuff.up&!variable.other_on_use_equipped|buff.empower_rune_weapon.up&debuff.razor_coral_debuff.up&!talent.breath_of_sindragosa.enabled|target.1.time_to_die<21
                if (Aimsharp.CanUseItem("Ashvane's Razor Coral"))
                {
                    if (CDEmpowerRuneWeaponRemains > 90000 && DebuffRazorCoralUp && VariableOtherOnUseEquipped || BuffBreathOfSindragosaUp && DebuffRazorCoralUp && !VariableOtherOnUseEquipped || BuffEmpowerRuneWeaponUp && DebuffRazorCoralUp && !TalentBreathOfSindragosa || TargetTimeToDie < 21000)
                    {
                        Aimsharp.Cast("Ashvane's Razor Coral", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/use_item,name=jes_howler,if=(equipped.lurkers_insidious_gift&buff.pillar_of_frost.remains)|(!equipped.lurkers_insidious_gift&buff.pillar_of_frost.remains<12&buff.pillar_of_frost.up)
                if (Aimsharp.CanUseItem("Jes' Howler"))
                {
                    if ((Aimsharp.IsEquipped("Lurker's Insidious Gift") && BuffPillarOfFrostRemains > 0) || (!Aimsharp.IsEquipped("Lurker's Insidious Gift") && BuffPillarOfFrostRemains < 12000 && BuffPillarOfFrostUp))
                    {
                        Aimsharp.Cast("Jes' Howler", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/potion,if=buff.pillar_of_frost.up&buff.empower_rune_weapon.up
                if (UsePotion && Fighting)
                {
                    if (BuffPillarOfFrostUp && BuffEmpowerRuneWeaponUp)
                    {
                        if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                        {
                            Aimsharp.Cast("potion", true);
                            return true;
                        }
                    }
                }

                if (Fighting)
                {
                    if (BuffPillarOfFrostUp && BuffEmpowerRuneWeaponUp)
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
                    }
                }

                //actions.cooldowns+=/blood_fury,if=buff.pillar_of_frost.up&buff.empower_rune_weapon.up
                if (RacialPower == "Orc" && Fighting)
                {
                    if (BuffPillarOfFrostUp && BuffEmpowerRuneWeaponUp)
                    {
                        if (Aimsharp.CanCast("Blood Fury", "player"))
                        {
                            Aimsharp.Cast("Blood Fury", true);
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/berserking,if=buff.pillar_of_frost.up
                if (RacialPower == "Troll" && Fighting)
                {
                    if (BuffPillarOfFrostUp)
                    {
                        if (Aimsharp.CanCast("Berserking", "player"))
                        {
                            Aimsharp.Cast("Berserking", true);
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/lights_judgment,if=buff.pillar_of_frost.up
                if (RacialPower == "Lightforged Draenei" && Fighting)
                {
                    if (Aimsharp.CanCast("Light's Judgment", "player"))
                    {
                        if (BuffPillarOfFrostUp)
                        {
                            Aimsharp.Cast("Light's Judgment", true);
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/ancestral_call,if=buff.pillar_of_frost.up&buff.empower_rune_weapon.up
                if (RacialPower == "Mag'har Orc" && Fighting)
                {
                    if (BuffPillarOfFrostUp && BuffEmpowerRuneWeaponUp)
                    {
                        if (Aimsharp.CanCast("Ancestral Call", "player"))
                        {
                            Aimsharp.Cast("Ancestral Call", true);
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/fireblood,if=buff.pillar_of_frost.remains<=8&buff.empower_rune_weapon.up
                if (RacialPower == "Dark Iron Dwarf" && Fighting)
                {
                    if (BuffPillarOfFrostRemains <= 8000 && BuffEmpowerRuneWeaponUp)
                    {
                        if (Aimsharp.CanCast("Fireblood", "player"))
                        {
                            Aimsharp.Cast("Fireblood", true);
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/pillar_of_frost,if=cooldown.empower_rune_weapon.remains
                if (Aimsharp.CanCast("Pillar of Frost", "player") && Fighting)
                {
                    if (CDEmpowerRuneWeaponRemains > 10)
                    {
                        Aimsharp.Cast("Pillar of Frost");
                        return true;
                    }
                }

                //actions.cooldowns+=/breath_of_sindragosa,use_off_gcd=1,if=cooldown.empower_rune_weapon.remains&cooldown.pillar_of_frost.remains
                if (Aimsharp.CanCast("Breath of Sindragosa", "player") && Fighting)
                {
                    if (CDEmpowerRuneWeaponRemains > 10 && CDPillarOfFrostRemains > 10)
                    {
                        Aimsharp.Cast("Breath of Sindragosa", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/empower_rune_weapon,if=cooldown.pillar_of_frost.ready&!talent.breath_of_sindragosa.enabled&rune.time_to_5>gcd&runic_power.deficit>=10|target.1.time_to_die<20
                if (Aimsharp.CanCast("Empower Rune Weapon", "player") && Fighting)
                {
                    if (CDPillarOfFrostReady && !TalentBreathOfSindragosa && Aimsharp.TimeUntilRunes(5) - GCD > GCDMAX && RunicDefecit >= 10 || TargetTimeToDie < 20000)
                    {
                        Aimsharp.Cast("Empower Rune Weapon");
                        return true;
                    }
                }

                //actions.cooldowns+=/empower_rune_weapon,if=(cooldown.pillar_of_frost.ready|target.1.time_to_die<20)&talent.breath_of_sindragosa.enabled&runic_power>60
                if (Aimsharp.CanCast("Empower Rune Weapon", "player") && Fighting)
                {
                    if ((CDPillarOfFrostReady || TargetTimeToDie < 20000) && TalentBreathOfSindragosa && RunicPower > 60)
                    {
                        Aimsharp.Cast("Empower Rune Weapon");
                        return true;
                    }
                }

            }

            //actions.cooldowns+=/call_action_list,name=cold_heart,if=talent.cold_heart.enabled&((buff.cold_heart.stack>=10&debuff.razorice.stack=5)|target.1.time_to_die<=gcd)
            if (TalentColdHeart && ((BuffColdHeartStack >= 10 && DebuffRazoriceStack == 5) || TargetTimeToDie <= GCDMAX))
            {
                //actions.cold_heart=chains_of_ice,if=buff.cold_heart.stack>5&target.1.time_to_die<gcd
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if (BuffColdHeartStack > 5 && TargetTimeToDie < GCDMAX)
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }

                //actions.cold_heart+=/chains_of_ice,if=(buff.seething_rage.remains<gcd)&buff.seething_rage.up
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if ((BuffSeethingRageRemains < GCDMAX) && BuffSeethingRageUp)
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }

                //actions.cold_heart+=/chains_of_ice,if=(buff.pillar_of_frost.remains<=gcd*(1+cooldown.frostwyrms_fury.ready)|buff.pillar_of_frost.remains<rune.time_to_3)&buff.pillar_of_frost.up&(azerite.icy_citadel.rank<=1|buff.breath_of_sindragosa.up)&!talent.icecap.enabled
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if ((BuffPillarOfFrostRemains <= GCDMAX * (1 + (CDFrostwyrmsFuryReady ? 1 : 0)) || BuffPillarOfFrostRemains < Aimsharp.TimeUntilRunes(3) - GCD) && BuffPillarOfFrostUp && (AzeriteIcyCitadelRank <= 1 || BuffBreathOfSindragosaUp) && !TalentIcecap)
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }

                //actions.cold_heart+=/chains_of_ice,if=buff.pillar_of_frost.remains<8&buff.unholy_strength.remains<gcd*(1+cooldown.frostwyrms_fury.ready)&buff.unholy_strength.remains&buff.pillar_of_frost.up&(azerite.icy_citadel.rank<=1|buff.breath_of_sindragosa.up)&!talent.icecap.enabled
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if (BuffPillarOfFrostRemains < 8000 && BuffUnholyStrengthRemains < GCDMAX * (1 + (CDFrostwyrmsFuryReady ? 1 : 0)) && BuffUnholyStrengthRemains > 0 && BuffPillarOfFrostUp && (AzeriteIcyCitadelRank <= 1 || BuffBreathOfSindragosaUp) && !TalentIcecap)
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }

                //actions.cold_heart+=/chains_of_ice,if=(buff.icy_citadel.remains<4|buff.icy_citadel.remains<rune.time_to_3)&buff.icy_citadel.up&azerite.icy_citadel.rank>=2&!buff.breath_of_sindragosa.up&!talent.icecap.enabled
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if ((BuffIcyCitadelRemains < 4000 || BuffIcyCitadelRemains < Aimsharp.TimeUntilRunes(3) - GCD) && BuffIcyCitadelUp && AzeriteIcyCitadelRank >= 2 && !BuffBreathOfSindragosaUp && !TalentIcecap)
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }

                //actions.cold_heart+=/chains_of_ice,if=buff.icy_citadel.up&buff.unholy_strength.up&azerite.icy_citadel.rank>=2&!buff.breath_of_sindragosa.up&!talent.icecap.enabled
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if (BuffIcyCitadelUp && BuffUnholyStrengthUp && AzeriteIcyCitadelRank >= 2 && !BuffBreathOfSindragosaUp && !TalentIcecap)
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }

                //actions.cold_heart+=/chains_of_ice,if=buff.pillar_of_frost.remains<4&talent.icecap.enabled&buff.cold_heart.stack>=18&azerite.icy_citadel.rank<=1
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if (BuffPillarOfFrostRemains < 4000 && TalentIcecap && BuffColdHeartStack >= 18 && AzeriteIcyCitadelRank <= 1)
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }

                //actions.cold_heart+=/chains_of_ice,if=buff.pillar_of_frost.up&talent.icecap.enabled&azerite.icy_citadel.rank>=2&(buff.cold_heart.stack>=19&buff.icy_citadel.remains<gcd|buff.unholy_strength.up&buff.cold_heart.stack>=18)
                if (Aimsharp.CanCast("Chains of Ice"))
                {
                    if (BuffPillarOfFrostUp && TalentIcecap && AzeriteIcyCitadelRank >= 2 && (BuffColdHeartStack >= 19 && BuffIcyCitadelRemains < GCDMAX || BuffUnholyStrengthUp && BuffColdHeartStack >= 18))
                    {
                        Aimsharp.Cast("Chains of Ice");
                        return true;
                    }
                }
            }

            if (!NoCooldowns)
            {
                //actions.cooldowns+=/frostwyrms_fury,if=(buff.pillar_of_frost.remains<=gcd|(buff.pillar_of_frost.remains<8&buff.unholy_strength.remains<=gcd&buff.unholy_strength.up))&buff.pillar_of_frost.up&azerite.icy_citadel.rank<=1
                if (Aimsharp.CanCast("Frostwyrm's Fury", "player") && Fighting)
                {
                    if ((BuffPillarOfFrostRemains <= GCDMAX || (BuffPillarOfFrostRemains < 8000 && BuffUnholyStrengthRemains <= GCDMAX && BuffUnholyStrengthUp)) && BuffPillarOfFrostUp && AzeriteIcyCitadelRank <= 1)
                    {
                        Aimsharp.Cast("Frostwyrm's Fury");
                        return true;
                    }
                }

                //actions.cooldowns+=/frostwyrms_fury,if=(buff.icy_citadel.remains<=gcd|(buff.icy_citadel.remains<8&buff.unholy_strength.remains<=gcd&buff.unholy_strength.up))&buff.icy_citadel.up&azerite.icy_citadel.rank>=2
                if (Aimsharp.CanCast("Frostwyrm's Fury", "player") && Fighting)
                {
                    if ((BuffIcyCitadelRemains <= GCDMAX || (BuffIcyCitadelRemains < 8000 && BuffUnholyStrengthRemains <= GCDMAX && BuffUnholyStrengthUp)) && BuffIcyCitadelUp && AzeriteIcyCitadelRank >= 2)
                    {
                        Aimsharp.Cast("Frostwyrm's Fury");
                        return true;
                    }
                }

                //actions.cooldowns+=/frostwyrms_fury,if=target.1.time_to_die<gcd|(target.1.time_to_die<cooldown.pillar_of_frost.remains&buff.unholy_strength.up)
                if (Aimsharp.CanCast("Frostwyrm's Fury", "player") && Fighting)
                {
                    if (TargetTimeToDie < GCDMAX || (TargetTimeToDie < CDPillarOfFrostRemains && BuffUnholyStrengthUp))
                    {
                        Aimsharp.Cast("Frostwyrm's Fury");
                        return true;
                    }
                }
            }

            //actions+=/run_action_list,name=bos_pooling,if=talent.breath_of_sindragosa.enabled&((cooldown.breath_of_sindragosa.remains=0&cooldown.pillar_of_frost.remains<10)|(cooldown.breath_of_sindragosa.remains<20&target.1.time_to_die<35))
            if (!NoCooldowns && TalentBreathOfSindragosa && ((CDBreathOfSindragosaRemains <= 10 && CDPillarOfFrostRemains < 10000) || (CDBreathOfSindragosaRemains < 20000 && TargetTimeToDie < 35000)))
            {
                //actions.bos_pooling=howling_blast,if=buff.rime.up
                if (Aimsharp.CanCast("Howling Blast"))
                {
                    if (BuffRimeUp)
                    {
                        Aimsharp.Cast("Howling Blast");
                        return true;
                    }
                }

                //actions.bos_pooling+=/obliterate,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&&runic_power.deficit>=25&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && RunicDefecit >= 25 && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_pooling+=/obliterate,if=runic_power.deficit>=25
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if (RunicDefecit >= 25)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_pooling+=/glacial_advance,if=runic_power.deficit<20&spell_targets.glacial_advance>=2&cooldown.pillar_of_frost.remains>5
                if (Aimsharp.CanCast("Glacial Advance", "player") && Fighting)
                {
                    if (RunicDefecit < 20 && EnemiesInMelee >= 2 && CDPillarOfFrostRemains > 5000)
                    {
                        Aimsharp.Cast("Glacial Advance");
                        return true;
                    }
                }

                //actions.bos_pooling+=/frost_strike,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&runic_power.deficit<20&!talent.frostscythe.enabled&cooldown.pillar_of_frost.remains>5
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && RunicDefecit < 20 && !TalentFrostscythe && CDPillarOfFrostRemains > 5000)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.bos_pooling+=/frost_strike,if=runic_power.deficit<20&cooldown.pillar_of_frost.remains>5
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if (RunicDefecit < 20 && CDPillarOfFrostRemains > 5000)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.bos_pooling+=/frostscythe,if=buff.killing_machine.up&runic_power.deficit>(15+talent.runic_attenuation.enabled*3)&spell_targets.frostscythe>=2
                if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
                {
                    if (BuffKillingMachineUp && RunicDefecit > (15 + (TalentRunicAttenuation ? 1 : 0) * 3) && EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Frostscythe");
                        return true;
                    }
                }

                //actions.bos_pooling+=/frostscythe,if=runic_power.deficit>=(35+talent.runic_attenuation.enabled*3)&spell_targets.frostscythe>=2
                if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
                {
                    if (RunicDefecit >= (35 + (TalentRunicAttenuation ? 1 : 0) * 3) && EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Frostscythe");
                        return true;
                    }
                }

                //actions.bos_pooling+=/obliterate,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&runic_power.deficit>=(35+talent.runic_attenuation.enabled*3)&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains > 10000) && RunicDefecit >= (35 + (TalentRunicAttenuation ? 1 : 0) * 3) && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_pooling+=/obliterate,if=runic_power.deficit>=(35+talent.runic_attenuation.enabled*3)
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if (RunicDefecit >= (35 + (TalentRunicAttenuation ? 1 : 0) * 3))
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_pooling+=/glacial_advance,if=cooldown.pillar_of_frost.remains>rune.time_to_4&runic_power.deficit<40&spell_targets.glacial_advance>=2
                if (Aimsharp.CanCast("Glacial Advance", "player") && Fighting)
                {
                    if (CDPillarOfFrostRemains > Aimsharp.TimeUntilRunes(4) - GCD && RunicDefecit < 40 && EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Glacial Advance");
                        return true;
                    }
                }

                //actions.bos_pooling+=/frost_strike,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&cooldown.pillar_of_frost.remains>rune.time_to_4&runic_power.deficit<40&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && CDPillarOfFrostRemains > Aimsharp.TimeUntilRunes(4) - GCD && RunicDefecit < 40 && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.bos_pooling+=/frost_strike,if=cooldown.pillar_of_frost.remains>rune.time_to_4&runic_power.deficit<40
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if (BuffPillarOfFrostRemains > Aimsharp.TimeUntilRunes(4) - GCD && RunicDefecit < 40)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                return false;
            }

            //actions+=/run_action_list,name=bos_ticking,if=buff.breath_of_sindragosa.up
            if (BuffBreathOfSindragosaUp)
            {
                //actions.bos_ticking = obliterate,target_if = (debuff.razorice.stack < 5 | debuff.razorice.remains < 10) & runic_power <= 32 & !talent.frostscythe.enabled
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains > 10000) && RunicPower <= 32 && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_ticking+=/obliterate,if=runic_power<=32
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if (RunicPower <= 32)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_ticking+=/remorseless_winter,if=talent.gathering_storm.enabled
                if (Aimsharp.CanCast("Remorseless Winter", "player") && Fighting)
                {
                    if (TalentGatheringStorm)
                    {
                        Aimsharp.Cast("Remorseless Winter");
                        return true;
                    }
                }

                //actions.bos_ticking+=/howling_blast,if=buff.rime.up
                if (Aimsharp.CanCast("Howling Blast"))
                {
                    if (BuffRimeUp)
                    {
                        Aimsharp.Cast("Howling Blast");
                        return true;
                    }
                }

                //actions.bos_ticking+=/obliterate,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&rune.time_to_5<gcd|runic_power<=45&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains > 10000) && Aimsharp.TimeUntilRunes(5) - GCD < GCDMAX || RunicPower <= 45 && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_ticking+=/obliterate,if=rune.time_to_5<gcd|runic_power<=45
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if (Aimsharp.TimeUntilRunes(5) - GCD < GCDMAX || RunicPower <= 45)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_ticking+=/frostscythe,if=buff.killing_machine.up&spell_targets.frostscythe>=2
                if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
                {
                    if (BuffKillingMachineUp && EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Frostscythe");
                        return true;
                    }
                }

                //actions.bos_ticking+=/horn_of_winter,if=runic_power.deficit>=32&rune.time_to_3>gcd
                if (Aimsharp.CanCast("Horn of Winter", "player") && Fighting)
                {
                    if (RunicDefecit >= 32 && Aimsharp.TimeUntilRunes(3) - GCD > GCDMAX)
                    {
                        Aimsharp.Cast("Horn of Winter");
                        return true;
                    }
                }

                //actions.bos_ticking+=/remorseless_winter
                if (Aimsharp.CanCast("Remorseless Winter", "player") && Fighting)
                {
                    Aimsharp.Cast("Remorseless Winter");
                    return true;
                }

                //actions.bos_ticking+=/frostscythe,if=spell_targets.frostscythe>=2
                if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
                {
                    if (EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Frostscythe");
                        return true;
                    }
                }

                //actions.bos_ticking+=/obliterate,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&runic_power.deficit>25|rune>3&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains > 10000) && RunicDefecit > 25 || Aimsharp.TimeUntilRunes(3) - GCD <= 0 && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_ticking+=/obliterate,if=runic_power.deficit>25|rune>3
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if (RunicDefecit > 25 || Aimsharp.TimeUntilRunes(3) - GCD <= 0)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.bos_ticking+=/arcane_torrent,if=runic_power.deficit>50
                if (RacialPower == "Bloodelf" && Fighting)
                {
                    if (RunicDefecit > 50)
                    {
                        if (Aimsharp.CanCast("Arcane Torrent", "player"))
                        {
                            Aimsharp.Cast("Arcane Torrent");
                            return true;
                        }
                    }
                }

                return false;
            }

            //actions+=/run_action_list,name=obliteration,if=buff.pillar_of_frost.up&talent.obliteration.enabled
            if (BuffPillarOfFrostUp && TalentObliteration)
            {
                //actions.obliteration = remorseless_winter,if= talent.gathering_storm.enabled
                if (Aimsharp.CanCast("Remorseless Winter", "player") && Fighting)
                {
                    if (TalentGatheringStorm)
                    {
                        Aimsharp.Cast("Remorseless Winter");
                        return true;
                    }
                }

                //actions.obliteration+=/obliterate,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&!talent.frostscythe.enabled&!buff.rime.up&spell_targets.howling_blast>=3
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && !TalentFrostscythe && !BuffRimeUp && EnemiesInMelee >= 3)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.obliteration+=/obliterate,if=!talent.frostscythe.enabled&!buff.rime.up&spell_targets.howling_blast>=3
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if (!TalentFrostscythe && !BuffRimeUp && EnemiesInMelee >= 3)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.obliteration+=/frostscythe,if=(buff.killing_machine.react|(buff.killing_machine.up&(prev_gcd.1.frost_strike|prev_gcd.1.howling_blast|prev_gcd.1.glacial_advance)))&spell_targets.frostscythe>=2
                if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
                {
                    if ((BuffKillingMachineUp || (BuffKillingMachineUp && (LastCast == "Frost Strike" || LastCast == "Howling Blast" || LastCast == "Glacial Advance"))) && EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Frostscythe");
                        return true;
                    }
                }

                //actions.obliteration+=/obliterate,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&buff.killing_machine.react|(buff.killing_machine.up&(prev_gcd.1.frost_strike|prev_gcd.1.howling_blast|prev_gcd.1.glacial_advance))
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && BuffKillingMachineUp || (BuffKillingMachineUp && (LastCast == "Frost Strike" || LastCast == "Howling Blast" || LastCast == "Glacial Advance")))
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.obliteration+=/glacial_advance,if=(!buff.rime.up|runic_power.deficit<10|rune.time_to_2>gcd)&spell_targets.glacial_advance>=2
                if (Aimsharp.CanCast("Glacial Advance", "player") && Fighting)
                {
                    if ((!BuffRimeUp || RunicDefecit < 10 || Aimsharp.TimeUntilRunes(2) - GCD > GCDMAX) && EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Glacial Advance");
                        return true;
                    }
                }

                //actions.obliteration+=/howling_blast,if=buff.rime.up&spell_targets.howling_blast>=2
                if (Aimsharp.CanCast("Howling Blast"))
                {
                    if (BuffRimeUp && EnemiesInMelee >= 2)
                    {
                        Aimsharp.Cast("Howling Blast");
                        return true;
                    }
                }

                //actions.obliteration+=/frost_strike,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&!buff.rime.up|runic_power.deficit<10|rune.time_to_2>gcd&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && !BuffRimeUp || RunicDefecit < 10 || Aimsharp.TimeUntilRunes(2) - GCD > GCDMAX && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.obliteration+=/frost_strike,if=!buff.rime.up|runic_power.deficit<10|rune.time_to_2>gcd
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if (!BuffRimeUp || RunicDefecit < 10 || Aimsharp.TimeUntilRunes(2) - GCD > GCDMAX)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.obliteration+=/howling_blast,if=buff.rime.up
                if (Aimsharp.CanCast("Howling Blast"))
                {
                    if (BuffRimeUp)
                    {
                        Aimsharp.Cast("Howling Blast");
                        return true;
                    }
                }

                //actions.obliteration +=/ obliterate
                if (Aimsharp.CanCast("Obliterate"))
                {
                    Aimsharp.Cast("Obliterate");
                    return true;
                }

                return false;
            }

            //actions+=/run_action_list,name=aoe,if=active_enemies>=2
            if (EnemiesInMelee >= 2)
            {
                //actions.aoe=remorseless_winter,if=talent.gathering_storm.enabled|(azerite.frozen_tempest.rank&spell_targets.remorseless_winter>=3&!buff.rime.up)
                if (Aimsharp.CanCast("Remorseless Winter", "player") && Fighting)
                {
                    if (TalentGatheringStorm || (AzeriteFrozenTempestRank >= 1 && EnemiesInMelee >= 3 && !BuffRimeUp))
                    {
                        Aimsharp.Cast("Remorseless Winter");
                        return true;
                    }
                }

                //actions.aoe+=/glacial_advance,if=talent.frostscythe.enabled
                if (Aimsharp.CanCast("Glacial Advance", "player") && Fighting)
                {
                    if (TalentFrostscythe)
                    {
                        Aimsharp.Cast("Glacial Advance");
                        return true;
                    }
                }

                //actions.aoe+=/frost_strike,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&cooldown.remorseless_winter.remains<=2*gcd&talent.gathering_storm.enabled&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && CDRemorselessWinterRemains <= 2 * GCDMAX && TalentGatheringStorm && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.aoe+=/frost_strike,if=cooldown.remorseless_winter.remains<=2*gcd&talent.gathering_storm.enabled
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if (CDRemorselessWinterRemains <= 2 * GCDMAX && TalentGatheringStorm)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.aoe+=/howling_blast,if=buff.rime.up
                if (Aimsharp.CanCast("Howling Blast"))
                {
                    if (BuffRimeUp)
                    {
                        Aimsharp.Cast("Howling Blast");
                        return true;
                    }
                }

                //actions.aoe+=/frostscythe,if=buff.killing_machine.up
                if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
                {
                    if (BuffKillingMachineUp)
                    {
                        Aimsharp.Cast("Frostscythe");
                        return true;
                    }
                }

                //actions.aoe+=/glacial_advance,if=runic_power.deficit<(15+talent.runic_attenuation.enabled*3)
                if (Aimsharp.CanCast("Glacial Advance", "player") && Fighting)
                {
                    if (RunicDefecit < (15 + (TalentRunicAttenuation ? 3 : 0)))
                    {
                        Aimsharp.Cast("Glacial Advance");
                        return true;
                    }
                }

                //actions.aoe+=/frost_strike,if=runic_power.deficit<(15+talent.runic_attenuation.enabled*3)&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if (RunicDefecit < (15 + (TalentRunicAttenuation ? 3 : 0)) && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.aoe+=/remorseless_winter
                if (Aimsharp.CanCast("Remorseless Winter", "player") && Fighting)
                {
                    Aimsharp.Cast("Remorseless Winter");
                    return true;
                }

                //actions.aoe+=/frostscythe
                if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
                {
                    Aimsharp.Cast("Frostscythe");
                    return true;
                }

                //actions.aoe+=/obliterate,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&runic_power.deficit>(25+talent.runic_attenuation.enabled*3)&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && RunicDefecit > (25 + (TalentRunicAttenuation ? 3 : 0)) && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.aoe+=/obliterate,if=runic_power.deficit>(25+talent.runic_attenuation.enabled*3)
                if (Aimsharp.CanCast("Obliterate"))
                {
                    if (RunicDefecit > (25 + (TalentRunicAttenuation ? 3 : 0)))
                    {
                        Aimsharp.Cast("Obliterate");
                        return true;
                    }
                }

                //actions.aoe+=/glacial_advance
                if (Aimsharp.CanCast("Glacial Advance", "player") && Fighting)
                {
                    Aimsharp.Cast("Glacial Advance");
                    return true;
                }

                //actions.aoe+=/frost_strike,target_if=(debuff.razorice.stack<5|debuff.razorice.remains<10)&!talent.frostscythe.enabled
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    if ((DebuffRazoriceStack < 5 || DebuffRazoriceRemains < 10000) && !TalentFrostscythe)
                    {
                        Aimsharp.Cast("Frost Strike");
                        return true;
                    }
                }

                //actions.aoe+=/frost_strike
                if (Aimsharp.CanCast("Frost Strike"))
                {
                    Aimsharp.Cast("Frost Strike");
                    return true;
                }

                //actions.aoe+=/horn_of_winter
                if (Aimsharp.CanCast("Horn of Winter", "player") && Fighting)
                {
                    Aimsharp.Cast("Horn of Winter");
                    return true;
                }

                //actions.aoe+=/arcane_torrent
                if (RacialPower == "Bloodelf" && Fighting)
                {
                    if (Aimsharp.CanCast("Arcane Torrent", "player"))
                    {
                        Aimsharp.Cast("Arcane Torrent");
                        return true;
                    }
                }

                return false;
            }

            //actions.standard=remorseless_winter
            if (Aimsharp.CanCast("Remorseless Winter", "player") && Fighting)
            {
                Aimsharp.Cast("Remorseless Winter");
                return true;
            }

            //actions.standard+=/frost_strike,if=cooldown.remorseless_winter.remains<=2*gcd&talent.gathering_storm.enabled
            if (Aimsharp.CanCast("Frost Strike"))
            {
                if (CDRemorselessWinterRemains <= 2 * GCDMAX && TalentGatheringStorm)
                {
                    Aimsharp.Cast("Frost Strike");
                    return true;
                }
            }

            //actions.standard+=/howling_blast,if=buff.rime.up
            if (Aimsharp.CanCast("Howling Blast"))
            {
                if (BuffRimeUp)
                {
                    Aimsharp.Cast("Howling Blast");
                    return true;
                }
            }

            //actions.standard+=/obliterate,if=!buff.frozen_pulse.up&talent.frozen_pulse.enabled
            if (Aimsharp.CanCast("Obliterate"))
            {
                if (!DebuffFrozenPulseUp && TalentFrozenPulse)
                {
                    Aimsharp.Cast("Obliterate");
                    return true;
                }
            }

            //actions.standard+=/frost_strike,if=runic_power.deficit<(15+talent.runic_attenuation.enabled*3)
            if (Aimsharp.CanCast("Frost Strike"))
            {
                if (RunicDefecit < (15 + (TalentRunicAttenuation ? 1 : 0) * 3))
                {
                    Aimsharp.Cast("Frost Strike");
                    return true;
                }
            }

            //actions.standard+=/frostscythe,if=buff.killing_machine.up&rune.time_to_4>=gcd
            if (Aimsharp.CanCast("Frostscythe", "player") && Fighting)
            {
                if (BuffKillingMachineUp && Aimsharp.TimeUntilRunes(4) - GCD >= GCDMAX)
                {
                    Aimsharp.Cast("Frostscythe");
                    return true;
                }
            }

            //actions.standard+=/obliterate,if=runic_power.deficit>(25+talent.runic_attenuation.enabled*3)
            if (Aimsharp.CanCast("Obliterate"))
            {
                if (RunicDefecit > (25 + (TalentRunicAttenuation ? 1 : 0) * 3))
                {
                    Aimsharp.PrintMessage("Should do this a lot");
                    Aimsharp.Cast("Obliterate");
                    return true;
                }
            }

            //actions.standard+=/frost_strike
            if (Aimsharp.CanCast("Frost Strike"))
            {
                Aimsharp.Cast("Frost Strike");
                return true;
            }

            //actions.standard+=/horn_of_winter
            if (Aimsharp.CanCast("Horn of Winter", "player") && Fighting)
            {
                Aimsharp.Cast("Horn of Winter");
                return true;
            }

            //actions.standard+=/arcane_torrent
            if (RacialPower == "Bloodelf" && Fighting)
            {
                if (Aimsharp.CanCast("Arcane Torrent", "player"))
                {
                    Aimsharp.Cast("Arcane Torrent");
                    return true;
                }
            }


            return false;
        }


        public override bool OutOfCombatTick()
        {
            return false;
        }

    }
}
