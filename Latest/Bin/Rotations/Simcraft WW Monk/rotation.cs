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
    public class PerfSimWW : Rotation
    {
        public override void LoadSettings()
        {
            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Pocket-Sized Computation Device", "Ashvane's Razor Coral", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "Bloodelf", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("Use Flying Serpent Kick for DPS?", false));

            //Settings.Add(new Setting("# Icy Citadel Traits", 0, 3, 1));

        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            //Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Perfect Simcraft Series: Windwalker Monk - v 1.0", Color.Blue);
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

            Spellbook.Add("Serenity");
            Spellbook.Add("Touch of Death");
            Spellbook.Add("Rising Sun Kick");
            Spellbook.Add("Fists of Fury");
            Spellbook.Add("Spinning Crane Kick");
            Spellbook.Add("Fist of the White Tiger");
            Spellbook.Add("Blackout Kick");
            Spellbook.Add("Tiger Palm");
            Spellbook.Add("Chi Wave");
            Spellbook.Add("Invoke Xuen, the White Tiger");
            Spellbook.Add("Storm, Earth, and Fire");
            Spellbook.Add("Whirling Dragon Punch");
            Spellbook.Add("Rushing Jade Wind");
            Spellbook.Add("Energizing Elixir");
            Spellbook.Add("Chi Burst");
            Spellbook.Add("Flying Serpent Kick");

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

            Buffs.Add("Serenity");
            Buffs.Add("Storm, Earth, and Fire");
            Buffs.Add("Rushing Jade Wind");
            Buffs.Add("Energizing Elixir");
            Buffs.Add("Dance of Chi-Ji");
            Buffs.Add("Blackout Kick!");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");

            Debuffs.Add("Touch of Death");
            Debuffs.Add("Mark of the Crane");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            // CustomCommands.Add("Prepull");
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
            int EnemiesNearTarget = Aimsharp.EnemiesNearTarget();
            int GCDMAX = (int)(1500f / (Haste + 1f));
            int GCD = Aimsharp.GCD();
            int Latency = Aimsharp.Latency;
            int TargetTimeToDie = 1000000000;
            int TargetTimeTo30 = 1000000000;
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
            int CDWorldveinRemains = Aimsharp.SpellCooldown("Worldvein Resonance") - GCD;
            bool CDWorldveinReady = CDWorldveinRemains <= 10;
            int CDBloodOfTheEnemyRemains = Aimsharp.SpellCooldown("Blood of the Enemy") - GCD;
            int BuffMemoryOfLucidDreamsRemains = Aimsharp.BuffRemaining("Memory of Lucid Dreams") - GCD;
            bool BuffMemoryOfLucidDreamsUp = BuffMemoryOfLucidDreamsRemains > 0;
            bool DebuffRazorCoralUp = Aimsharp.HasDebuff("Razor Coral");
            bool DebuffConductiveInkUp = Aimsharp.HasDebuff("Conductive Ink");
            int BuffRecklessForceRemains = Aimsharp.BuffRemaining("Reckless Force") - GCD;
            bool BuffRecklessForceUp = BuffRecklessForceRemains > 0;
            int BuffRecklessForceStacks = Aimsharp.BuffStacks("Reckless Force");

            int Energy = Aimsharp.Power("player");
            int Chi = Aimsharp.PlayerSecondaryPower();
            int MaxEnergy = Aimsharp.PlayerMaxPower();
            int EnergyDefecit = MaxEnergy - Energy;

            bool TalentSerenity = Aimsharp.Talent(7, 3);
            bool TalentHitCombo = Aimsharp.Talent(6, 1);
            bool TalentAscension = Aimsharp.Talent(3, 1);
            bool TalentFistOfTheWhiteTiger = Aimsharp.Talent(3, 2);
            bool TalentWhirlingDragonPunch = Aimsharp.Talent(7, 2);

            int DebuffTouchOfDeathRemains = Aimsharp.DebuffRemaining("Touch of Death") - GCD;
            bool DebuffTouchOfDeathUp = DebuffTouchOfDeathRemains > 0;
            int DebuffMarkOfTheCraneRemains = Aimsharp.DebuffRemaining("Mark of the Crane") - GCD;
            bool DebuffMarkOfTheCraneUp = DebuffMarkOfTheCraneRemains > 0;
            int BuffSerenityRemains = Aimsharp.BuffRemaining("Serenity") - GCD;
            bool BuffSerenityUp = BuffSerenityRemains > 0;
            bool BuffWorldVeinUp = Aimsharp.HasBuff("Lifeblood");
            int BuffStormEarthFireRemains = Aimsharp.BuffRemaining("Storm, Earth, and Fire") - GCD;
            bool BuffStormEarthFireUp = BuffStormEarthFireRemains > 0;
            int BuffRushingJadeWindRemains = Aimsharp.BuffRemaining("Rushing Jade Wind") - GCD;
            bool BuffRushingJadeWindUp = BuffRushingJadeWindRemains > 0;
            int BuffEnergizingElixirRemains = Aimsharp.BuffRemaining("Energizing Elixir") - GCD;
            bool BuffEnergizingElixirUp = BuffEnergizingElixirRemains > 0;
            int BuffDanceOfChiJiRemains = Aimsharp.BuffRemaining("Dance of Chi-Ji") - GCD;
            bool BuffDanceOfChiJiUp = BuffDanceOfChiJiRemains > 0;
            bool BokProcUp = Aimsharp.HasBuff("Blackout Kick!");

            float EnergyRegen = 10f * (1f + Haste) * (TalentAscension ? 1.1f : 1f) + (BuffEnergizingElixirRemains > 1000 ? 15f : BuffEnergizingElixirRemains * 15f / 1000f);
            int TimeUntilMaxEnergy = (int)((EnergyDefecit * 1000f) / EnergyRegen);
            int MaxChi = TalentAscension ? 6 : 5;

            int CDSerenityRemains = Aimsharp.SpellCooldown("Serenity") - GCD;
            bool CDSerenityReady = CDSerenityRemains <= 10;
            int CDFistsOfFuryRemains = Aimsharp.SpellCooldown("Fists of Fury") - GCD;
            bool CDFistsOfFuryReady = CDFistsOfFuryRemains <= 10;
            int CDTouchOfDeathRemains = Aimsharp.SpellCooldown("Touch of Death") - GCD;
            bool CDTouchOfDeathReady = CDTouchOfDeathRemains <= 10;
            int CDWhirlingDragonPunchRemains = Aimsharp.SpellCooldown("Whirling Dragon Punch") - GCD;
            bool CDWhirlingDragonPunchReady = CDWhirlingDragonPunchRemains <= 10;
            int CDCyclotronicBlastRemains = Aimsharp.ItemCooldown("Pocket-Sized Computation Device") - GCD;
            bool CDCyclotronicBlastReady = CDCyclotronicBlastRemains <= 10;
            int CDStormEarthFireCharges = Aimsharp.SpellCharges("Storm, Earth, and Fire");
            int CDStormEarthFireRemains = Aimsharp.SpellCooldown("Storm, Earth, and Fire") - GCD;
            bool CDStormEarthFireReady = CDStormEarthFireRemains <= 10;
            int CDStormEarthFireMaxCharges = Aimsharp.MaxCharges("Storm, Earth, and Fire");
            int CDStormEarthFireRechargeTime = Aimsharp.RechargeTime("Storm, Earth, and Fire");
            int CDStormEarthFireFullRechargeTime = (int)(CDStormEarthFireRechargeTime + (90000f) * (CDStormEarthFireMaxCharges - CDStormEarthFireCharges - 1));
            int CDRisingSunKickRemains = Aimsharp.SpellCooldown("Rising Sun Kick") - GCD;
            bool CDRisingSunKickReady = CDRisingSunKickRemains <= 10;

            bool UseFlying = GetCheckBox("Use Flying Serpent Kick for DPS?");

            //int AzeriteFrozenTempestRank = GetSlider("# Frozen Tempest Traits");
            //int AzeriteIcyCitadelRank = GetSlider("# Icy Citadel Traits");

            //int RagingBlowCharges = Aimsharp.SpellCharges("Raging Blow");

            //actions.precombat +=/ variable,name = coral_double_tod_on_use,op = set,value = equipped.ashvanes_razor_coral & (equipped.cyclotronic_blast | equipped.lustrous_golden_plumage | equipped.gladiators_badge | equipped.gladiators_medallion)
            bool Variable_coral_double_tod_on_use = Aimsharp.IsEquipped("Ashvane's Razor Coral") && (Aimsharp.IsEquipped("Pocket-Sized Computation Device") || Aimsharp.IsEquipped("Lustrous Golden Plumage") || Aimsharp.IsEquipped("Notorious Gladiator's Badge") || Aimsharp.IsEquipped("Notorious Gladiator's Medallion"));

            if (IsChanneling)
                return false;

            //actions+=/potion,if=buff.serenity.up|dot.touch_of_death.remains|!talent.serenity.enabled&trinket.proc.agility.react|buff.bloodlust.react|target.time_to_die<=60
            if (UsePotion && Fighting)
            {
                if (BuffSerenityUp || DebuffTouchOfDeathRemains > 0 || HasLust || TargetTimeToDie <= 60)
                {
                    if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                    {
                        Aimsharp.Cast("potion", true);
                        return true;
                    }
                }
            }

            //actions+=/call_action_list,name=serenity,if=buff.serenity.up
            if (BuffSerenityUp)
            {
                //actions.serenity=rising_sun_kick,target_if=min:debuff.mark_of_the_crane.remains,if=active_enemies<3|prev_gcd.1.spinning_crane_kick
                if (Aimsharp.CanCast("Rising Sun Kick"))
                {
                    if ((EnemiesInMelee < 3 || LastCast == "Spinning Crane Kick"))
                    {
                        Aimsharp.Cast("Rising Sun Kick");
                        return true;
                    }
                }

                //actions.serenity+=/fists_of_fury,if=(buff.bloodlust.up&prev_gcd.1.rising_sun_kick)|buff.serenity.remains<1|(active_enemies>1&active_enemies<5)
                if (Aimsharp.CanCast("Fists of Fury", "player") && Fighting)
                {
                    if ((HasLust && LastCast == "Rising Sun Kick") || BuffSerenityRemains < 1000 || (EnemiesInMelee > 1 && EnemiesInMelee < 5))
                    {
                        Aimsharp.Cast("Fists of Fury");
                        return true;
                    }
                }

                //actions.serenity+=/fist_of_the_white_tiger,if=talent.hit_combo.enabled&energy.time_to_max<2&prev_gcd.1.blackout_kick&chi<=2
                if (Aimsharp.CanCast("Fist of the White Tiger"))
                {
                    if (TalentHitCombo && TimeUntilMaxEnergy < 2000 && LastCast == "Blackout Kick" && Chi <= 2)
                    {
                        Aimsharp.Cast("Fist of the White Tiger");
                        return true;
                    }
                }

                //actions.serenity+=/tiger_palm,if=talent.hit_combo.enabled&energy.time_to_max<1&prev_gcd.1.blackout_kick&chi.max-chi>=2
                if (Aimsharp.CanCast("Tiger Palm"))
                {
                    if (TalentHitCombo && TimeUntilMaxEnergy < 1000 && LastCast == "Blackout Kick" && MaxChi - Chi >= 2)
                    {
                        Aimsharp.Cast("Tiger Palm");
                        return true;
                    }
                }

                //actions.serenity+=/spinning_crane_kick,if=combo_strike&(active_enemies>=3|(talent.hit_combo.enabled&prev_gcd.1.blackout_kick)|(active_enemies=2&prev_gcd.1.blackout_kick))
                if (Aimsharp.CanCast("Spinning Crane Kick", "player") && Fighting)
                {
                    if (LastCast != "Spinning Crane Kick" && (EnemiesInMelee >= 3 || (TalentHitCombo && LastCast == "Blackout Kick") || (EnemiesInMelee == 2 && LastCast == "Blackout Kick")))
                    {
                        Aimsharp.Cast("Spinning Crane Kick");
                        return true;
                    }
                }

                //actions.serenity+=/blackout_kick,target_if=min:debuff.mark_of_the_crane.remains
                if (Aimsharp.CanCast("Blackout Kick"))
                {
                    Aimsharp.Cast("Blackout Kick");
                    return true;
                }
            }

            //actions+=/fist_of_the_white_tiger,if=(energy.time_to_max<1|(talent.serenity.enabled&cooldown.serenity.remains<2)|(energy.time_to_max<4&cooldown.fists_of_fury.remains<1.5))&chi.max-chi>=3
            if (Aimsharp.CanCast("Fist of the White Tiger"))
            {
                if ((TimeUntilMaxEnergy < 1000 || (TalentSerenity && CDSerenityRemains < 2000) || (TimeUntilMaxEnergy < 4000 && CDFistsOfFuryRemains < 1500)) && MaxChi - Chi >= 3)
                {
                    Aimsharp.Cast("Fist of the White Tiger");
                    return true;
                }
            }

            //actions+=/tiger_palm,target_if=min:debuff.mark_of_the_crane.remains,if=!combo_break&(energy.time_to_max<1|(talent.serenity.enabled&cooldown.serenity.remains<2)|(energy.time_to_max<4&cooldown.fists_of_fury.remains<1.5))&chi.max-chi>=2&!dot.touch_of_death.remains
            if (Aimsharp.CanCast("Tiger Palm"))
            {
                if ((LastCast != "Tiger Palm" && (TimeUntilMaxEnergy < 1000 || (TalentSerenity && CDSerenityRemains < 2000) || (TimeUntilMaxEnergy < 4000 && CDFistsOfFuryRemains < 1500)) && MaxChi - Chi >= 2 && !DebuffTouchOfDeathUp))
                {
                    Aimsharp.Cast("Tiger Palm");
                    return true;
                }
            }

            //actions+=/chi_wave,if=!talent.fist_of_the_white_tiger.enabled&time<=3
            if (Aimsharp.CanCast("Chi Wave"))
            {
                if (!TalentFistOfTheWhiteTiger && Time <= 3000)
                {
                    Aimsharp.Cast("Chi Wave");
                    return true;
                }
            }

            if (!NoCooldowns)
            {
                //actions.cd=invoke_xuen_the_white_tiger
                if (Aimsharp.CanCast("Invoke Xuen, the White Tiger"))
                {
                    Aimsharp.Cast("Invoke Xuen, the White Tiger");
                    return true;
                }

                //actions.cd+=/guardian_of_azeroth,if=target.time_to_die>185|(!equipped.dribbling_inkpod|equipped.cyclotronic_blast|target.health.pct<30)&cooldown.touch_of_death.remains<=14|equipped.dribbling_inkpod&target.time_to_pct_30.remains<20|target.time_to_die<35
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (TargetTimeToDie > 185000 || (!Aimsharp.IsEquipped("Dribbling Inkpod") || Aimsharp.IsEquipped("Pocket-Sized Computation Device") || TargetHealth < 30) && CDTouchOfDeathRemains <= 14000 || Aimsharp.IsEquipped("Dribbling Inkpod") && TargetTimeTo30 < 20000 || TargetTimeToDie < 35000)
                    {
                        if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }
                }

                //actions.cd+=/worldvein_resonance,if=cooldown.touch_of_death.remains>58|cooldown.touch_of_death.remains<2|target.time_to_die<20
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (CDTouchOfDeathRemains > 58000 || CDTouchOfDeathRemains < 2000 || TargetTimeToDie < 20000)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                //actions.cd+=/blood_fury
                if (RacialPower == "Orc" && Fighting)
                {
                    if (Aimsharp.CanCast("Blood Fury", "player"))
                    {
                        Aimsharp.Cast("Blood Fury", true);
                        return true;
                    }
                }

                //actions.cd+=/arcane_torrent,if=chi.max-chi>=1&energy.time_to_max>=0.5
                if (RacialPower == "Bloodelf" && Fighting)
                {
                    if (MaxChi - Chi >= 1 && TimeUntilMaxEnergy >= 500)
                    {
                        if (Aimsharp.CanCast("Arcane Torrent", "player"))
                        {
                            Aimsharp.Cast("Arcane Torrent");
                            return true;
                        }
                    }
                }

                //actions.cd+=/lights_judgment
                if (RacialPower == "Lightforged Draenei" && Fighting)
                {
                    if (Aimsharp.CanCast("Light's Judgment", "player"))
                    {
                        Aimsharp.Cast("Light's Judgment", true);
                        return true;
                    }
                }

                //actions.tod=touch_of_death,if=equipped.cyclotronic_blast&target.time_to_die>9&cooldown.cyclotronic_blast.remains<=1
                if (Aimsharp.CanCast("Touch of Death"))
                {
                    if (Aimsharp.IsEquipped("Pocket-Sized Computation Device") && TargetTimeToDie > 9000 && CDCyclotronicBlastRemains <= 1000)
                    {
                        Aimsharp.Cast("Touch of Death");
                        return true;
                    }
                }

                //actions.tod+=/touch_of_death,if=!equipped.cyclotronic_blast&equipped.dribbling_inkpod&target.time_to_die>9&(target.time_to_pct_30.remains>=130|target.time_to_pct_30.remains<8)
                if (Aimsharp.CanCast("Touch of Death"))
                {
                    if (!Aimsharp.IsEquipped("Pocket-Sized Computation Device") && Aimsharp.IsEquipped("Dribbling Inkpod") && TargetTimeToDie > 9000 && (TargetTimeTo30 >= 130000 || TargetTimeTo30 < 8000))
                    {
                        Aimsharp.Cast("Touch of Death");
                        return true;
                    }
                }

                //actions.tod+=/touch_of_death,if=!equipped.cyclotronic_blast&!equipped.dribbling_inkpod&target.time_to_die>9
                if (Aimsharp.CanCast("Touch of Death"))
                {
                    if (!Aimsharp.IsEquipped("Pocket-Sized Computation Device") && !Aimsharp.IsEquipped("Dribbling Inkpod") && TargetTimeToDie > 9000)
                    {
                        Aimsharp.Cast("Touch of Death");
                        return true;
                    }
                }

                //actions.cd+=/storm_earth_and_fire,,if=cooldown.storm_earth_and_fire.charges=2|(!essence.worldvein_resonance.major|(buff.worldvein_resonance.up|cooldown.worldvein_resonance.remains>cooldown.storm_earth_and_fire.full_recharge_time))&(cooldown.touch_of_death.remains>cooldown.storm_earth_and_fire.full_recharge_time|cooldown.touch_of_death.remains>target.time_to_die)&cooldown.fists_of_fury.remains<=9&chi>=3&cooldown.whirling_dragon_punch.remains<=13|dot.touch_of_death.remains|target.time_to_die<20
                if (Aimsharp.CanCast("Storm, Earth, and Fire", "player") && !BuffStormEarthFireUp && Fighting)
                {
                    if (CDStormEarthFireCharges == 2 || (MajorPower != "Worldvein Resonance" || (BuffWorldVeinUp || CDWorldveinRemains > CDStormEarthFireFullRechargeTime)) && (CDTouchOfDeathRemains > CDStormEarthFireFullRechargeTime || CDTouchOfDeathRemains > TargetTimeToDie) && CDFistsOfFuryRemains <= 9000 && Chi >= 3 && CDWhirlingDragonPunchRemains <= 13000 || DebuffTouchOfDeathUp || TargetTimeToDie < 20000)
                    {
                        Aimsharp.Cast("Storm, Earth, and Fire");
                        return true;
                    }
                }

                //actions.cd+=/blood_of_the_enemy,if=dot.touch_of_death.remains|target.time_to_die<12
                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        if (DebuffTouchOfDeathUp || TargetTimeToDie < 12000)
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }
                }

                //actions.cd+=/ancestral_call,if=dot.touch_of_death.remains|target.time_to_die<16
                if (RacialPower == "Mag'har Orc" && Fighting)
                {
                    if (DebuffTouchOfDeathUp || TargetTimeToDie < 16000)
                    {
                        if (Aimsharp.CanCast("Ancestral Call", "player"))
                        {
                            Aimsharp.Cast("Ancestral Call", true);
                            return true;
                        }
                    }
                }

                //actions.cd+=/fireblood,if=dot.touch_of_death.remains|target.time_to_die<9
                if (RacialPower == "Dark Iron Dwarf" && Fighting)
                {
                    if (DebuffTouchOfDeathUp || TargetTimeToDie < 9000)
                    {
                        if (Aimsharp.CanCast("Fireblood", "player"))
                        {
                            Aimsharp.Cast("Fireblood", true);
                            return true;
                        }
                    }
                }

                //actions.cd+=/concentrated_flame,if=!dot.concentrated_flame_burn.remains&(cooldown.concentrated_flame.remains<=cooldown.touch_of_death.remains&(talent.whirling_dragon_punch.enabled&cooldown.whirling_dragon_punch.remains)&cooldown.rising_sun_kick.remains&cooldown.fists_of_fury.remains&buff.storm_earth_and_fire.down|dot.touch_of_death.remains)|target.time_to_die<8
                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                    {
                        Aimsharp.Cast("Concentrated Flame");
                        return true;
                    }
                }

                //actions.cd+=/berserking,if=target.time_to_die>183|dot.touch_of_death.remains|target.time_to_die<13
                if (RacialPower == "Troll" && Fighting)
                {
                    if (TargetTimeToDie > 183000 || DebuffTouchOfDeathUp || TargetTimeToDie < 13000)
                    {
                        if (Aimsharp.CanCast("Berserking", "player"))
                        {
                            Aimsharp.Cast("Berserking", true);
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic")
                {
                    if (TargetTimeToDie > 183000 || DebuffTouchOfDeathUp || TargetTimeToDie < 13000)
                    {
                        Aimsharp.Cast("TopTrink", true);
                        return true;
                    }
                }

                if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic")
                {
                    if (TargetTimeToDie > 183000 || DebuffTouchOfDeathUp || TargetTimeToDie < 13000)
                    {
                        Aimsharp.Cast("BotTrink", true);
                        return true;
                    }
                }

                //actions.cd+=/use_item,name=pocketsized_computation_device,if=dot.touch_of_death.remains
                if (Aimsharp.CanUseItem("Pocket-Sized Computation Device"))
                {
                    if (DebuffTouchOfDeathUp)
                    {
                        Aimsharp.Cast("Pocket-Sized Computation Device");
                        return true;
                    }
                }

                //actions.cd+=/use_item,name=ashvanes_razor_coral,if=variable.coral_double_tod_on_use&cooldown.touch_of_death.remains>=23&(debuff.razor_coral_debuff.down|buff.storm_earth_and_fire.remains>13|target.time_to_die-cooldown.touch_of_death.remains<40&cooldown.touch_of_death.remains<23|target.time_to_die<25)
                if (Aimsharp.CanUseItem("Ashvane's Razor Coral"))
                {
                    if (Variable_coral_double_tod_on_use && CDTouchOfDeathRemains >= 23000 && (!DebuffRazorCoralUp || BuffStormEarthFireRemains > 13000 || TargetTimeToDie - CDTouchOfDeathRemains < 40000 && CDTouchOfDeathRemains < 23000 || TargetTimeToDie < 25000))
                    {
                        Aimsharp.Cast("Ashvane's Razor Coral", true);
                        return true;
                    }
                }

                //actions.cd+=/use_item,name=ashvanes_razor_coral,if=!variable.coral_double_tod_on_use&(debuff.razor_coral_debuff.down|(!equipped.dribbling_inkpod|target.time_to_pct_30.remains<8)&(dot.touch_of_death.remains|cooldown.touch_of_death.remains+9>target.time_to_die&buff.storm_earth_and_fire.up|target.time_to_die<25))
                if (Aimsharp.CanUseItem("Ashvane's Razor Coral"))
                {
                    if (!Variable_coral_double_tod_on_use && (!DebuffRazorCoralUp || (!Aimsharp.IsEquipped("Dribbling Inkpod") || TargetTimeTo30 < 8000) && (DebuffTouchOfDeathUp || CDTouchOfDeathRemains + 9000 > TargetTimeToDie && BuffStormEarthFireUp || TargetTimeToDie < 25000)))
                    {
                        Aimsharp.Cast("Ashvane's Razor Coral", true);
                        return true;
                    }
                }

                //actions.cd+=/the_unbound_force
                if (MajorPower == "The Unbound Force")
                {
                    if (Aimsharp.CanCast("The Unbound Force"))
                    {
                        Aimsharp.Cast("The Unbound Force");
                        return true;
                    }
                }

                //actions.cd+=/reaping_flames
                if (MajorPower == "Reaping Flames")
                {
                    if (Aimsharp.CanCast("Reaping Flames"))
                    {
                        Aimsharp.Cast("Reaping Flames");
                        return true;
                    }
                }

                //actions.cd+=/focused_azerite_beam
                if (MajorPower == "Focused Azerite Beam" && Range < 15)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        Aimsharp.Cast("Focused Azerite Beam");
                        return true;
                    }
                }

                //actions.cd+=/serenity,if=cooldown.rising_sun_kick.remains<=2|target.time_to_die<=12
                if (Aimsharp.CanCast("Serenity", "player") && Fighting)
                {
                    if (CDRisingSunKickRemains <= 2000 || TargetTimeToDie <= 12000)
                    {
                        Aimsharp.Cast("Serenity");
                        return true;
                    }
                }

                //actions.cd+=/memory_of_lucid_dreams,if=energy<40&buff.storm_earth_and_fire.up
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (Energy < 40 && BuffStormEarthFireUp)
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }
            }

            //actions+=/call_action_list,name=st,if=active_enemies<3
            if (EnemiesInMelee < 3)
            {
                //actions.st=whirling_dragon_punch
                if (Aimsharp.CanCast("Whirling Dragon Punch", "player") && Fighting)
                {
                    Aimsharp.Cast("Whirling Dragon Punch");
                    return true;
                }

                //actions.st+=/fists_of_fury,if=energy.time_to_max>3
                if (Aimsharp.CanCast("Fists of Fury", "player") && Fighting)
                {
                    if (TimeUntilMaxEnergy > 3000)
                    {
                        Aimsharp.Cast("Fists of Fury");
                        return true;
                    }
                }

                //actions.st+=/rising_sun_kick,target_if=min:debuff.mark_of_the_crane.remains,if=chi>=5
                if (Aimsharp.CanCast("Rising Sun Kick"))
                {
                    if ((Chi >= 5))
                    {
                        Aimsharp.Cast("Rising Sun Kick");
                        return true;
                    }
                }

                //actions.st+=/rising_sun_kick,target_if=min:debuff.mark_of_the_crane.remains
                if (Aimsharp.CanCast("Rising Sun Kick"))
                {
                    Aimsharp.Cast("Rising Sun Kick");
                    return true;
                }

                //actions.st+=/rushing_jade_wind,if=buff.rushing_jade_wind.down&active_enemies>1
                if (Aimsharp.CanCast("Rushing Jade Wind", "player") && Fighting)
                {
                    if (!BuffRushingJadeWindUp && EnemiesInMelee > 1)
                    {
                        Aimsharp.Cast("Rushing Jade Wind");
                        return true;
                    }
                }

                //actions.st+=/fist_of_the_white_tiger,if=chi<=2
                if (Aimsharp.CanCast("Fist of the White Tiger"))
                {
                    if (Chi <= 2)
                    {
                        Aimsharp.Cast("Fist of the White Tiger");
                        return true;
                    }
                }

                //actions.st+=/energizing_elixir,if=chi<=3&energy<50
                if (Aimsharp.CanCast("Energizing Elixir", "player") && Fighting)
                {
                    if (Chi <= 3 && Energy < 50)
                    {
                        Aimsharp.Cast("Energizing Elixir");
                        return true;
                    }
                }

                //actions.st+=/spinning_crane_kick,if=combo_strike&buff.dance_of_chiji.react
                if (Aimsharp.CanCast("Spinning Crane Kick", "player") && Fighting)
                {
                    if (LastCast != "Spinning Crane Kick" && BuffDanceOfChiJiUp)
                    {
                        Aimsharp.Cast("Spinning Crane Kick");
                        return true;
                    }
                }

                //actions.st+=/blackout_kick,target_if=min:debuff.mark_of_the_crane.remains,if=combo_strike&(cooldown.rising_sun_kick.remains>3|chi>=3)&(cooldown.fists_of_fury.remains>4|chi>=4|(chi=2&prev_gcd.1.tiger_palm))
                if (Aimsharp.CanCast("Blackout Kick"))
                {
                    if (LastCast != "Blackout Kick" && (CDRisingSunKickRemains > 3000 || Chi >= 3) && (CDFistsOfFuryRemains > 4000 || Chi >= 4 || (Chi == 2 && LastCast == "Tiger Palm")))
                    {
                        Aimsharp.Cast("Blackout Kick");
                        return true;
                    }
                }

                //actions.st+=/chi_wave
                if (Aimsharp.CanCast("Chi Wave"))
                {
                    Aimsharp.Cast("Chi Wave");
                    return true;
                }

                //actions.st+=/chi_burst,if=chi.max-chi>=1&active_enemies=1|chi.max-chi>=2
                if (Aimsharp.CanCast("Chi Burst", "player") && Fighting)
                {
                    if (MaxChi - Chi >= 1 && EnemiesInMelee <= 1 || MaxChi - Chi >= 2)
                    {
                        Aimsharp.Cast("Chi Burst");
                        return true;
                    }
                }

                //actions.st+=/tiger_palm,target_if=min:debuff.mark_of_the_crane.remains,if=combo_strike&chi.max-chi>=2
                if (Aimsharp.CanCast("Tiger Palm"))
                {
                    if (LastCast != "Tiger Palm" && MaxChi - Chi >= 2)
                    {
                        Aimsharp.Cast("Tiger Palm");
                        return true;
                    }
                }

                if (UseFlying)
                {
                    //actions.st+=/flying_serpent_kick,if=prev_gcd.1.blackout_kick&chi>3,interrupt=1
                    if (Aimsharp.CanCast("Flying Serpent Kick", "player") && Fighting)
                    {
                        if (LastCast == "Blackout Kick" && Chi > 3)
                        {
                            Aimsharp.Cast("Flying Serpent Kick");
                            return true;
                        }
                    }
                    if (Aimsharp.CanCast("Flying Serpent Kick", "player"))
                    {
                        if (LastCast == "Flying Serpent Kick")
                        {
                            Aimsharp.Cast("Flying Serpent Kick", true);
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanCast("Tiger Palm"))
                {
                    Aimsharp.Cast("Tiger Palm");
                    return true;
                }

            }

            if (EnemiesInMelee >= 3)
            {
                //actions.aoe=rising_sun_kick,target_if=min:debuff.mark_of_the_crane.remains,if=(talent.whirling_dragon_punch.enabled&cooldown.whirling_dragon_punch.remains<5)&cooldown.fists_of_fury.remains>3
                if (Aimsharp.CanCast("Rising Sun Kick"))
                {
                    if ((TalentWhirlingDragonPunch && CDWhirlingDragonPunchRemains < 5000) && CDFistsOfFuryRemains > 3000)
                    {
                        Aimsharp.Cast("Rising Sun Kick");
                        return true;
                    }
                }

                //actions.aoe+=/whirling_dragon_punch
                if (Aimsharp.CanCast("Whirling Dragon Punch", "player") && Fighting)
                {
                    Aimsharp.Cast("Whirling Dragon Punch");
                    return true;
                }

                //actions.aoe+=/energizing_elixir,if=!prev_gcd.1.tiger_palm&chi<=1&energy<50
                if (Aimsharp.CanCast("Energizing Elixir", "player") && Fighting)
                {
                    if (LastCast != "Tiger Palm" && Chi <= 1 && Energy < 50)
                    {
                        Aimsharp.Cast("Energizing Elixir");
                        return true;
                    }
                }

                //actions.aoe+=/fists_of_fury,if=energy.time_to_max>3
                if (Aimsharp.CanCast("Fists of Fury", "player") && Fighting)
                {
                    if (TimeUntilMaxEnergy > 3000)
                    {
                        Aimsharp.Cast("Fists of Fury");
                        return true;
                    }
                }

                //actions.aoe+=/rushing_jade_wind,if=buff.rushing_jade_wind.down
                if (Aimsharp.CanCast("Rushing Jade Wind", "player") && Fighting)
                {
                    if (!BuffRushingJadeWindUp)
                    {
                        Aimsharp.Cast("Rushing Jade Wind");
                        return true;
                    }
                }

                //actions.aoe+=/spinning_crane_kick,if=combo_strike&(((chi>3|cooldown.fists_of_fury.remains>6)&(chi>=5|cooldown.fists_of_fury.remains>2))|energy.time_to_max<=3)
                if (Aimsharp.CanCast("Spinning Crane Kick", "player") && Fighting)
                {
                    if (LastCast != "Spinning Crane Kick" && (((Chi > 3 || CDFistsOfFuryRemains > 6000) && (Chi >= 5 || CDFistsOfFuryRemains > 2)) || TimeUntilMaxEnergy <= 3000))
                    {
                        Aimsharp.Cast("Spinning Crane Kick");
                        return true;
                    }
                }

                //actions.aoe+=/chi_burst,if=chi<=3
                if (Aimsharp.CanCast("Chi Burst", "player") && Fighting)
                {
                    if (MaxChi - Chi >= 1 && EnemiesInMelee <= 1 || MaxChi - Chi >= 2)
                    {
                        Aimsharp.Cast("Chi Burst");
                        return true;
                    }
                }

                //actions.aoe+=/fist_of_the_white_tiger,if=chi.max-chi>=3
                if (Aimsharp.CanCast("Fist of the White Tiger"))
                {
                    if (MaxChi - Chi >= 3)
                    {
                        Aimsharp.Cast("Fist of the White Tiger");
                        return true;
                    }
                }

                //actions.aoe+=/tiger_palm,target_if=min:debuff.mark_of_the_crane.remains,if=chi.max-chi>=2&(!talent.hit_combo.enabled|!combo_break)
                if (Aimsharp.CanCast("Tiger Palm"))
                {
                    if (MaxChi - Chi >= 2 && (!TalentHitCombo || LastCast != "Tiger Palm"))
                    {
                        Aimsharp.Cast("Tiger Palm");
                        return true;
                    }
                }

                //actions.aoe+=/chi_wave,if=!combo_break
                if (Aimsharp.CanCast("Chi Wave"))
                {
                    if (LastCast != "Chi Wave")
                    {
                        Aimsharp.Cast("Chi Wave");
                        return true;
                    }
                }

                if (UseFlying)
                {
                    //actions.aoe+=/flying_serpent_kick,if=buff.bok_proc.down,interrupt=1
                    if (Aimsharp.CanCast("Flying Serpent Kick", "player") && Fighting)
                    {
                        if (!BokProcUp)
                        {
                            Aimsharp.Cast("Flying Serpent Kick");
                            return true;
                        }
                    }
                    if (Aimsharp.CanCast("Flying Serpent Kick", "player"))
                    {
                        if (LastCast == "Flying Serpent Kick")
                        {
                            Aimsharp.Cast("Flying Serpent Kick", true);
                            return true;
                        }
                    }
                }

                //actions.aoe+=/blackout_kick,target_if=min:debuff.mark_of_the_crane.remains,if=combo_strike&(buff.bok_proc.up|(talent.hit_combo.enabled&prev_gcd.1.tiger_palm&chi<4))
                if (Aimsharp.CanCast("Blackout Kick"))
                {
                    if (LastCast != "Blackout Kick" && (BokProcUp || (TalentHitCombo && LastCast == "Tiger Palm" && Chi < 4)))
                    {
                        Aimsharp.Cast("Blackout Kick");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Tiger Palm"))
                {
                    Aimsharp.Cast("Tiger Palm");
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
