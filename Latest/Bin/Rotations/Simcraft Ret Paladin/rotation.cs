using System.Collections.Generic;
using System.Drawing;
using AimsharpWow.API; //needed to access Aimsharp API


namespace AimsharpWow.Modules
{
    /// <summary>
    /// This is an example rotation. It is a garbage rotation.  Just trying to show some examples of using the Aimsharp API.
    /// Check API-DOC for detailed documentation.
    /// </summary>
    public class PerfectSimRetPal : Rotation
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

           // Settings.Add(new Setting("# Icy Citadel Traits", 0, 3, 1));
           // Settings.Add(new Setting("# Frozen Tempest Traits", 0, 3, 1));



        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            // Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Perfect Simcraft Series: Ret Paladin - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 3313213", Color.Blue);
            Aimsharp.PrintMessage("These macros can be used for manual control:", Color.Blue);
            Aimsharp.PrintMessage("/xxxxx Potions", Color.Blue);
            Aimsharp.PrintMessage("--Toggles using buff potions on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx SaveCooldowns", Color.Blue);
            Aimsharp.PrintMessage("--Toggles the use of big cooldowns on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx AOE", Color.Blue);
            Aimsharp.PrintMessage("--Toggles AOE mode on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
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

            Spellbook.Add("Rebuke");
            Spellbook.Add("Shield of Vengeance");
            Spellbook.Add("Crusade");
            Spellbook.Add("Avenging Wrath");
            Spellbook.Add("Inquisition");
            Spellbook.Add("Blade of Justice");
            Spellbook.Add("Judgment");
            Spellbook.Add("Execution Sentence");
            Spellbook.Add("Divine Storm");
            Spellbook.Add("Templar's Verdict");
            Spellbook.Add("Wake of Ashes");
            Spellbook.Add("Hammer of Wrath");
            Spellbook.Add("Consecration");
            Spellbook.Add("Crusader Strike");

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

            Buffs.Add("Avenging Wrath");
            Buffs.Add("Crusade");
            Buffs.Add("Seething Rage");
            Buffs.Add("Inquisition");
            Buffs.Add("Empyrean Power");
            Buffs.Add("Divine Purpose");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");
            Debuffs.Add("Judgment");

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

            int HolyPower = Aimsharp.PlayerSecondaryPower();

            bool TalentCrusade = Aimsharp.Talent(7, 2);
            bool TalentInquisition = Aimsharp.Talent(7, 3);
            bool TalentWakeOfAshes = Aimsharp.Talent(4, 3);
            bool TalentHammerOfWrath = Aimsharp.Talent(2, 3);
            bool TalentRighteousVerdict = Aimsharp.Talent(1, 2);
            bool TalentExecutionSentence = Aimsharp.Talent(1, 3);


            int BuffAvengingWrathRemains = Aimsharp.BuffRemaining("Avenging Wrath") - GCD;
            bool BuffAvengingWrathUp = BuffAvengingWrathRemains > 0;
            int BuffAvengingWrathStack = Aimsharp.BuffStacks("Avenging Wrath");
            int BuffCrusadeRemains = Aimsharp.BuffRemaining("Crusade") - GCD;
            bool BuffCrusadeUp = BuffCrusadeRemains > 0;
            int BuffCrusadeStack = Aimsharp.BuffStacks("Crusade");
            int BuffSeethingRageRemains = Aimsharp.BuffRemaining("Seething Rage") - GCD;
            bool BuffSeethingRageUp = BuffSeethingRageRemains > 0;
            int BuffInquisitionRemains = Aimsharp.BuffRemaining("Inquisition") - GCD;
            bool BuffInquisitionUp = BuffInquisitionRemains > 0;
            int BuffEmpyreanPowerRemains = Aimsharp.BuffRemaining("Empyrean Power") - GCD;
            bool BuffEmpyreanPowerUp = BuffEmpyreanPowerRemains > 0;
            int DebuffJudgmentRemains = Aimsharp.DebuffRemaining("Judgment") - GCD;
            bool DebuffJudgmentUp = DebuffJudgmentRemains > 0;
            int BuffDivinePurposeRemains = Aimsharp.BuffRemaining("Divine Purpose") - GCD;
            bool BuffDivinePurposeUp = BuffDivinePurposeRemains > 0;

            int CDAvengingWrathRemains = Aimsharp.SpellCooldown("Avenging Wrath") - GCD;
            bool CDAvengingWrathReady = CDAvengingWrathRemains <= 10;
            int CDCrusadeRemains = Aimsharp.SpellCooldown("Crusade") - GCD;
            bool CDCrusadeReady = CDCrusadeRemains <= 10;
            int CDBladeOfJusticeRemains = Aimsharp.SpellCooldown("Blade of Justice") - GCD;
            bool CDBladeOfJusticeReady = CDBladeOfJusticeRemains <= 10;
            int CDJudgmentRemains = Aimsharp.SpellCooldown("Judgment") - GCD;
            bool CDJudgmentReady = CDJudgmentRemains <= 10;
            int CDExecutionSentenceRemains = Aimsharp.SpellCooldown("Execution Sentence") - GCD;
            bool CDExecutionSentenceReady = CDExecutionSentenceRemains <= 10;
            int CDDivineStormRemains = Aimsharp.SpellCooldown("Divine Storm") - GCD;
            bool CDDivineStormReady = CDDivineStormRemains <= 10;
            int CDHammerOfWrathRemains = Aimsharp.SpellCooldown("Hammer of Wrath") - GCD;
            bool CDHammerOfWrathUp = CDHammerOfWrathRemains > 0;
            int CDConsecrationRemains = Aimsharp.SpellCooldown("Consecration") - GCD;
            bool CDConsecrationUp = CDConsecrationRemains > 0;
            float CDCrusaderStrikeChargesFractional = Aimsharp.SpellCharges("Crusader Strike") + (Aimsharp.RechargeTime("Crusader Strike") - GCD) / ((6000f) / (1f + Haste));

            if (!AOE)
            {
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }
            //int AzeriteFrozenTempestRank = GetSlider("# Frozen Tempest Traits");
            // int AzeriteIcyCitadelRank = GetSlider("# Icy Citadel Traits");

            //actions.generators = variable,name = HoW,value = (!talent.hammer_of_wrath.enabled | target.health.pct >= 20 & !(buff.avenging_wrath.up | buff.crusade.up))
            bool VariableHoW = (!TalentHammerOfWrath || TargetHealth >= 20 && !(BuffAvengingWrathUp || BuffCrusadeUp));
            //actions.finishers=variable,name=wings_pool,value=!equipped.169314&(!talent.crusade.enabled&cooldown.avenging_wrath.remains>gcd*3|cooldown.crusade.remains>gcd*3)|equipped.169314&(!talent.crusade.enabled&cooldown.avenging_wrath.remains>gcd*6|cooldown.crusade.remains>gcd*6)
            bool Variablewings_pool = !Aimsharp.IsEquipped("Azshara's Font of Power") && (!TalentCrusade && CDAvengingWrathRemains > GCDMAX * 3 || CDCrusadeRemains > GCDMAX * 3) || Aimsharp.IsEquipped("Azshara's Font of Power") && (!TalentCrusade && CDAvengingWrathRemains > GCDMAX * 6 || CDCrusadeRemains > GCDMAX * 6);
            //actions.finishers+=/variable,name=ds_castable,value=spell_targets.divine_storm>=2&!talent.righteous_verdict.enabled|spell_targets.divine_storm>=3&talent.righteous_verdict.enabled|buff.empyrean_power.up&debuff.judgment.down&buff.divine_purpose.down&buff.avenging_wrath_autocrit.down
            bool Variableds_castable = EnemiesInMelee >= 2 && !TalentRighteousVerdict || EnemiesInMelee >= 3 && TalentRighteousVerdict || BuffEmpyreanPowerUp && !DebuffJudgmentUp && !BuffDivinePurposeUp && BuffAvengingWrathStack <= 1;


            if (IsChanneling)
                return false;


            if (!NoCooldowns)
            {

                //actions.cooldowns=potion,if=(cooldown.guardian_of_azeroth.remains>90|!essence.condensed_lifeforce.major)&(buff.bloodlust.react|buff.avenging_wrath.up&buff.avenging_wrath.remains>18|buff.crusade.up&buff.crusade.remains<25)
                if (UsePotion && Fighting)
                {
                    if ((CDGuardianOfAzerothRemains > 90000 || MajorPower != "Guardian of Azeroth") && (HasLust || BuffAvengingWrathUp && BuffAvengingWrathRemains > 18000 || BuffCrusadeUp && BuffCrusadeRemains < 25000))
                    {
                        if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                        {
                            Aimsharp.Cast("potion", true);
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/lights_judgment,if=spell_targets.lights_judgment>=2|(!raid_event.adds.exists|raid_event.adds.in>75)
                if (RacialPower == "Lightforged Draenei" && Fighting)
                {
                    if (Aimsharp.CanCast("Light's Judgment", "player"))
                    {
                        if (true) //better to always use this
                        {
                            Aimsharp.Cast("Light's Judgment", true);
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/fireblood,if=buff.avenging_wrath.up|buff.crusade.up&buff.crusade.stack=10
                if (RacialPower == "Dark Iron Dwarf" && Fighting)
                {
                    if (BuffAvengingWrathUp || BuffCrusadeUp && BuffCrusadeStack == 10)
                    {
                        if (Aimsharp.CanCast("Fireblood", "player"))
                        {
                            Aimsharp.Cast("Fireblood", true);
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanUseItem(TopTrinket))
                {
                    if (BuffAvengingWrathUp || BuffCrusadeUp && BuffCrusadeStack == 10)
                    {
                        Aimsharp.Cast(TopTrinket, true);
                        return true;
                    }
                }
                if (Aimsharp.CanUseItem(BotTrinket))
                {
                    if (BuffAvengingWrathUp || BuffCrusadeUp && BuffCrusadeStack == 10)
                    {
                        Aimsharp.Cast(BotTrinket, true);
                        return true;
                    }
                }

                if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic")
                {
                    if (BuffAvengingWrathUp || BuffCrusadeUp && BuffCrusadeStack == 10)
                    {
                        Aimsharp.Cast("TopTrink", true);
                        return true;
                    }
                }

                if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic")
                {
                    if (BuffAvengingWrathUp || BuffCrusadeUp && BuffCrusadeStack == 10)
                    {
                        Aimsharp.Cast("BotTrink", true);
                        return true;
                    }
                }

                if (MajorPower == "Reaping Flames" && (TargetHealth > 80 || TargetHealth < 20))
                {
                    if (Aimsharp.CanCast("Reaping Flames"))
                    {
                        Aimsharp.Cast("Reaping Flames");
                        return true;
                    }
                }

                //actions.cooldowns+=/shield_of_vengeance,if=buff.seething_rage.down&buff.memory_of_lucid_dreams.down
                if (Aimsharp.CanCast("Shield of Vengeance", "player") && Fighting)
                {
                    if (!BuffSeethingRageUp && !BuffMemoryOfLucidDreamsUp)
                    {
                        Aimsharp.Cast("Shield of Vengeance");
                        return true;
                    }
                }

                //actions.cooldowns+=/use_item,name=ashvanes_razor_coral,if=debuff.razor_coral_debuff.down|(buff.avenging_wrath.remains>=20|buff.crusade.stack=10&buff.crusade.remains>15)&(cooldown.guardian_of_azeroth.remains>90|target.time_to_die<30|!essence.condensed_lifeforce.major)
                if (Aimsharp.CanUseItem("Ashvane's Razor Coral"))
                {
                    if (!DebuffRazorCoralUp || (BuffAvengingWrathRemains >= 20000 || BuffCrusadeStack == 10 && BuffCrusadeRemains > 15000) && (CDGuardianOfAzerothRemains > 90000 || TargetTimeToDie < 30000 || MajorPower != "Guardian of Azeroth"))
                    {
                        Aimsharp.Cast("Ashvane's Razor Coral", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/the_unbound_force,if=time<=2|buff.reckless_force.up
                if (MajorPower == "The Unbound Force")
                {
                    if (Aimsharp.CanCast("The Unbound Force"))
                    {
                        if (Time <= 2000 || BuffRecklessForceUp)
                        {
                            Aimsharp.Cast("The Unbound Force");
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/blood_of_the_enemy,if=buff.avenging_wrath.up|buff.crusade.up&buff.crusade.stack=10
                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        if (BuffAvengingWrathUp || BuffCrusadeUp && BuffCrusadeStack == 10)
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/guardian_of_azeroth,if=!talent.crusade.enabled&(cooldown.avenging_wrath.remains<5&holy_power>=3&(buff.inquisition.up|!talent.inquisition.enabled)|cooldown.avenging_wrath.remains>=45)|(talent.crusade.enabled&cooldown.crusade.remains<gcd&holy_power>=4|holy_power>=3&time<10&talent.wake_of_ashes.enabled|cooldown.crusade.remains>=45)
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (!TalentCrusade && (CDAvengingWrathRemains < 5000 && HolyPower >= 3 && (BuffInquisitionUp || !TalentInquisition) || CDAvengingWrathRemains >= 45000) || (TalentCrusade && CDCrusadeRemains < GCDMAX && HolyPower >= 4 || HolyPower >= 3 && Time < 10000 && TalentWakeOfAshes || CDCrusadeRemains >= 45000))
                    {
                        if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/worldvein_resonance,if=cooldown.avenging_wrath.remains<gcd&holy_power>=3|talent.crusade.enabled&cooldown.crusade.remains<gcd&holy_power>=4|cooldown.avenging_wrath.remains>=45|cooldown.crusade.remains>=45
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (CDAvengingWrathRemains < GCDMAX && HolyPower >= 3 || TalentCrusade && CDCrusadeRemains < GCDMAX && HolyPower >= 4 || CDAvengingWrathRemains >= 45000 || CDCrusadeRemains >= 45000)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/focused_azerite_beam,if=(!raid_event.adds.exists|raid_event.adds.in>30|spell_targets.divine_storm>=2)&!(buff.avenging_wrath.up|buff.crusade.up)&(cooldown.blade_of_justice.remains>gcd*3&cooldown.judgment.remains>gcd*3)
                if (MajorPower == "Focused Azerite Beam" && Range < 15)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        if (!(BuffAvengingWrathUp || BuffCrusadeUp) && (CDBladeOfJusticeRemains > GCDMAX * 3 && CDJudgmentRemains > GCDMAX * 3))
                        {
                            Aimsharp.Cast("Focused Azerite Beam");
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/memory_of_lucid_dreams,if=(buff.avenging_wrath.up|buff.crusade.up&buff.crusade.stack=10)&holy_power<=3
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if ((BuffAvengingWrathUp || BuffCrusadeUp && BuffCrusadeStack == 10) && HolyPower <= 3)
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }

                //actions.cooldowns+=/purifying_blast,if=(!raid_event.adds.exists|raid_event.adds.in>30|spell_targets.divine_storm>=2)

                //actions.cooldowns+=/use_item,effect_name=cyclotronic_blast,if=!(buff.avenging_wrath.up|buff.crusade.up)&(cooldown.blade_of_justice.remains>gcd*3&cooldown.judgment.remains>gcd*3)
                if (Aimsharp.CanUseItem("Pocket-Sized Computation Device"))
                {
                    if (!(BuffAvengingWrathUp || BuffCrusadeUp) && (CDBladeOfJusticeRemains > GCDMAX * 3 && CDJudgmentRemains > GCDMAX * 3))
                    {
                        Aimsharp.Cast("Pocket-Sized Computation Device", true);
                        return true;
                    }
                }

                //actions.cooldowns+=/avenging_wrath,if=(!talent.inquisition.enabled|buff.inquisition.up)&holy_power>=3
                if (Aimsharp.CanCast("Avenging Wrath", "player") && Fighting)
                {
                    if ((!TalentInquisition || BuffInquisitionUp) && HolyPower >= 3)
                    {
                        Aimsharp.Cast("Avenging Wrath");
                        return true;
                    }
                }

                //actions.cooldowns+=/crusade,if=holy_power>=4|holy_power>=3&time<10&talent.wake_of_ashes.enabled
                if (Aimsharp.CanCast("Crusade", "player") && Fighting)
                {
                    if (HolyPower >= 4 || HolyPower >= 3 && Time < 10000 && TalentWakeOfAshes)
                    {
                        Aimsharp.Cast("Crusade");
                        return true;
                    }
                }

            }

            //actions.generators+=/call_action_list,name=finishers,if=holy_power>=5|buff.memory_of_lucid_dreams.up|buff.seething_rage.up|talent.inquisition.enabled&buff.inquisition.down&holy_power>=3
            if (HolyPower >= 5 || BuffMemoryOfLucidDreamsUp || BuffSeethingRageUp || TalentInquisition && !BuffInquisitionUp && HolyPower >= 3)
            {
                //actions.finishers+=/inquisition,if=buff.avenging_wrath.down&(buff.inquisition.down|buff.inquisition.remains<8&holy_power>=3|talent.execution_sentence.enabled&cooldown.execution_sentence.remains<10&buff.inquisition.remains<15|cooldown.avenging_wrath.remains<15&buff.inquisition.remains<20&holy_power>=3)
                if (Aimsharp.CanCast("Inquisition", "player") && Fighting)
                {
                    if (!BuffAvengingWrathUp && (!BuffInquisitionUp || BuffInquisitionRemains < 8000 && HolyPower >= 3 || TalentExecutionSentence && CDExecutionSentenceRemains < 10000 && BuffInquisitionRemains < 15000 || CDAvengingWrathRemains < 15000 && BuffInquisitionRemains < 20000 && HolyPower >= 3))
                    {
                        Aimsharp.Cast("Inquisition");
                        return true;
                    }
                }

                //actions.finishers +=/ execution_sentence,if= spell_targets.divine_storm <= 2 & (!talent.crusade.enabled & cooldown.avenging_wrath.remains > 10 | talent.crusade.enabled & buff.crusade.down & cooldown.crusade.remains > 10 | buff.crusade.stack >= 7)
                if (Aimsharp.CanCast("Execution Sentence"))
                {
                    if (EnemiesInMelee <= 2 && (!TalentCrusade && CDAvengingWrathRemains > 10000 || TalentCrusade && !BuffCrusadeUp && CDCrusadeRemains > 10000 || BuffCrusadeStack >= 7))
                    {
                        Aimsharp.Cast("Execution Sentence");
                        return true;
                    }
                }

                //actions.finishers+=/divine_storm,if=variable.ds_castable&variable.wings_pool&((!talent.execution_sentence.enabled|(spell_targets.divine_storm>=2|cooldown.execution_sentence.remains>gcd*2))|(cooldown.avenging_wrath.remains>gcd*3&cooldown.avenging_wrath.remains<10|cooldown.crusade.remains>gcd*3&cooldown.crusade.remains<10|buff.crusade.up&buff.crusade.stack<10))
                if (Aimsharp.CanCast("Divine Storm", "player"))
                {
                    if (Variableds_castable && Variablewings_pool && ((!TalentExecutionSentence || (EnemiesInMelee >= 2 || CDExecutionSentenceRemains > GCDMAX * 2)) || (CDAvengingWrathRemains > GCDMAX * 3 && CDAvengingWrathRemains < 10000 || CDCrusadeRemains > GCDMAX * 3 && CDCrusadeRemains < 10000 || BuffCrusadeUp && BuffCrusadeStack < 10)))
                    {
                        Aimsharp.Cast("Divine Storm");
                        return true;
                    }
                }

                //actions.finishers+=/templars_verdict,if=variable.wings_pool&(!talent.execution_sentence.enabled|cooldown.execution_sentence.remains>gcd*2|cooldown.avenging_wrath.remains>gcd*3&cooldown.avenging_wrath.remains<10|cooldown.crusade.remains>gcd*3&cooldown.crusade.remains<10|buff.crusade.up&buff.crusade.stack<10)
                if (Aimsharp.CanCast("Templar's Verdict"))
                {
                    if (Variablewings_pool && (!TalentExecutionSentence || CDExecutionSentenceRemains > GCDMAX * 2 || CDAvengingWrathRemains > GCDMAX * 3 && CDAvengingWrathRemains < 10000 || CDCrusadeRemains > GCDMAX * 3 && CDCrusadeRemains < 10000 || BuffCrusadeUp && BuffCrusadeStack < 10))
                    {
                        Aimsharp.Cast("Templar's Verdict");
                        return true;
                    }
                }
            }

            //actions.generators+=/wake_of_ashes,if=(!raid_event.adds.exists|raid_event.adds.in>15|spell_targets.wake_of_ashes>=2)&(holy_power<=0|holy_power=1&cooldown.blade_of_justice.remains>gcd)&(cooldown.avenging_wrath.remains>10|talent.crusade.enabled&cooldown.crusade.remains>10)
            if (Aimsharp.CanCast("Wake of Ashes", "player"))
            {
                if ((HolyPower <= 0 || HolyPower == 1 && CDBladeOfJusticeRemains > GCDMAX) && (CDAvengingWrathRemains > 10000 || TalentCrusade && CDCrusadeRemains > 10000))
                {
                    Aimsharp.Cast("Wake of Ashes");
                    return true;
                }
            }

            //actions.generators+=/blade_of_justice,if=holy_power<=2|(holy_power=3&(cooldown.hammer_of_wrath.remains>gcd*2|variable.HoW))
            if (Aimsharp.CanCast("Blade of Justice"))
            {
                if (HolyPower <= 2 || (HolyPower == 3 && (CDHammerOfWrathRemains > GCDMAX * 2 || VariableHoW)))
                {
                    Aimsharp.Cast("Blade of Justice");
                    return true;
                }
            }

            //actions.generators+=/judgment,if=holy_power<=2|(holy_power<=4&(cooldown.blade_of_justice.remains>gcd*2|variable.HoW))
            if (Aimsharp.CanCast("Judgment"))
            {
                if (HolyPower <= 2 || (HolyPower <= 4 && (CDBladeOfJusticeRemains > GCDMAX * 2 || VariableHoW)))
                {
                    Aimsharp.Cast("Judgment");
                    return true;
                }
            }

            //actions.generators+=/hammer_of_wrath,if=holy_power<=4
            if (Aimsharp.CanCast("Hammer of Wrath"))
            {
                if (HolyPower <= 4)
                {
                    Aimsharp.Cast("Hammer of Wrath");
                    return true;
                }
            }

            //actions.generators+=/consecration,if=holy_power<=2|holy_power<=3&cooldown.blade_of_justice.remains>gcd*2|holy_power=4&cooldown.blade_of_justice.remains>gcd*2&cooldown.judgment.remains>gcd*2
            if (Aimsharp.CanCast("Consecration", "player"))
            {
                if (HolyPower <= 2 || HolyPower <= 3 && CDBladeOfJusticeRemains > GCDMAX * 2 || HolyPower == 4 && CDBladeOfJusticeRemains > GCDMAX * 2 && CDJudgmentRemains > GCDMAX * 2)
                {
                    Aimsharp.Cast("Consecration");
                    return true;
                }
            }

            //actions.generators+=/call_action_list,name=finishers,if=talent.hammer_of_wrath.enabled&target.health.pct<=20|buff.avenging_wrath.up|buff.crusade.up
            if (TalentHammerOfWrath && TargetHealth <= 20 || BuffAvengingWrathUp || BuffCrusadeUp)
            {
                //actions.finishers+=/inquisition,if=buff.avenging_wrath.down&(buff.inquisition.down|buff.inquisition.remains<8&holy_power>=3|talent.execution_sentence.enabled&cooldown.execution_sentence.remains<10&buff.inquisition.remains<15|cooldown.avenging_wrath.remains<15&buff.inquisition.remains<20&holy_power>=3)
                if (Aimsharp.CanCast("Inquisition", "player") && Fighting)
                {
                    if (!BuffAvengingWrathUp && (!BuffInquisitionUp || BuffInquisitionRemains < 8000 && HolyPower >= 3 || TalentExecutionSentence && CDExecutionSentenceRemains < 10000 && BuffInquisitionRemains < 15000 || CDAvengingWrathRemains < 15000 && BuffInquisitionRemains < 20000 && HolyPower >= 3))
                    {
                        Aimsharp.Cast("Inquisition");
                        return true;
                    }
                }

                //actions.finishers +=/ execution_sentence,if= spell_targets.divine_storm <= 2 & (!talent.crusade.enabled & cooldown.avenging_wrath.remains > 10 | talent.crusade.enabled & buff.crusade.down & cooldown.crusade.remains > 10 | buff.crusade.stack >= 7)
                if (Aimsharp.CanCast("Execution Sentence"))
                {
                    if (EnemiesInMelee <= 2 && (!TalentCrusade && CDAvengingWrathRemains > 10000 || TalentCrusade && !BuffCrusadeUp && CDCrusadeRemains > 10000 || BuffCrusadeStack >= 7))
                    {
                        Aimsharp.Cast("Execution Sentence");
                        return true;
                    }
                }

                //actions.finishers+=/divine_storm,if=variable.ds_castable&variable.wings_pool&((!talent.execution_sentence.enabled|(spell_targets.divine_storm>=2|cooldown.execution_sentence.remains>gcd*2))|(cooldown.avenging_wrath.remains>gcd*3&cooldown.avenging_wrath.remains<10|cooldown.crusade.remains>gcd*3&cooldown.crusade.remains<10|buff.crusade.up&buff.crusade.stack<10))
                if (Aimsharp.CanCast("Divine Storm", "player"))
                {
                    if (Variableds_castable && Variablewings_pool && ((!TalentExecutionSentence || (EnemiesInMelee >= 2 || CDExecutionSentenceRemains > GCDMAX * 2)) || (CDAvengingWrathRemains > GCDMAX * 3 && CDAvengingWrathRemains < 10000 || CDCrusadeRemains > GCDMAX * 3 && CDCrusadeRemains < 10000 || BuffCrusadeUp && BuffCrusadeStack < 10)))
                    {
                        Aimsharp.Cast("Divine Storm");
                        return true;
                    }
                }

                //actions.finishers+=/templars_verdict,if=variable.wings_pool&(!talent.execution_sentence.enabled|cooldown.execution_sentence.remains>gcd*2|cooldown.avenging_wrath.remains>gcd*3&cooldown.avenging_wrath.remains<10|cooldown.crusade.remains>gcd*3&cooldown.crusade.remains<10|buff.crusade.up&buff.crusade.stack<10)
                if (Aimsharp.CanCast("Templar's Verdict"))
                {
                    if (Variablewings_pool && (!TalentExecutionSentence || CDExecutionSentenceRemains > GCDMAX * 2 || CDAvengingWrathRemains > GCDMAX * 3 && CDAvengingWrathRemains < 10000 || CDCrusadeRemains > GCDMAX * 3 && CDCrusadeRemains < 10000 || BuffCrusadeUp && BuffCrusadeStack < 10))
                    {
                        Aimsharp.Cast("Templar's Verdict");
                        return true;
                    }
                }
            }

            //actions.generators+=/crusader_strike,if=cooldown.crusader_strike.charges_fractional>=1.75&(holy_power<=2|holy_power<=3&cooldown.blade_of_justice.remains>gcd*2|holy_power=4&cooldown.blade_of_justice.remains>gcd*2&cooldown.judgment.remains>gcd*2&cooldown.consecration.remains>gcd*2)
            if (Aimsharp.CanCast("Crusader Strike"))
            {
                if (CDCrusaderStrikeChargesFractional >= 1.75 && (HolyPower <= 2 || HolyPower <= 3 && CDBladeOfJusticeRemains > GCDMAX * 2 || HolyPower == 4 && CDBladeOfJusticeRemains > GCDMAX * 2 && CDConsecrationRemains > GCDMAX * 2))
                {
                    Aimsharp.Cast("Crusader Strike");
                    return true;
                }
            }

            //actions.generators+=/call_action_list,name=finishers
            //actions.finishers+=/inquisition,if=buff.avenging_wrath.down&(buff.inquisition.down|buff.inquisition.remains<8&holy_power>=3|talent.execution_sentence.enabled&cooldown.execution_sentence.remains<10&buff.inquisition.remains<15|cooldown.avenging_wrath.remains<15&buff.inquisition.remains<20&holy_power>=3)
            if (Aimsharp.CanCast("Inquisition", "player") && Fighting)
            {
                if (!BuffAvengingWrathUp && (!BuffInquisitionUp || BuffInquisitionRemains < 8000 && HolyPower >= 3 || TalentExecutionSentence && CDExecutionSentenceRemains < 10000 && BuffInquisitionRemains < 15000 || CDAvengingWrathRemains < 15000 && BuffInquisitionRemains < 20000 && HolyPower >= 3))
                {
                    Aimsharp.Cast("Inquisition");
                    return true;
                }
            }

            //actions.finishers +=/ execution_sentence,if= spell_targets.divine_storm <= 2 & (!talent.crusade.enabled & cooldown.avenging_wrath.remains > 10 | talent.crusade.enabled & buff.crusade.down & cooldown.crusade.remains > 10 | buff.crusade.stack >= 7)
            if (Aimsharp.CanCast("Execution Sentence"))
            {
                if (EnemiesInMelee <= 2 && (!TalentCrusade && CDAvengingWrathRemains > 10000 || TalentCrusade && !BuffCrusadeUp && CDCrusadeRemains > 10000 || BuffCrusadeStack >= 7))
                {
                    Aimsharp.Cast("Execution Sentence");
                    return true;
                }
            }

            //actions.finishers+=/divine_storm,if=variable.ds_castable&variable.wings_pool&((!talent.execution_sentence.enabled|(spell_targets.divine_storm>=2|cooldown.execution_sentence.remains>gcd*2))|(cooldown.avenging_wrath.remains>gcd*3&cooldown.avenging_wrath.remains<10|cooldown.crusade.remains>gcd*3&cooldown.crusade.remains<10|buff.crusade.up&buff.crusade.stack<10))
            if (Aimsharp.CanCast("Divine Storm", "player"))
            {
                if ((Variableds_castable && NoCooldowns) || Variableds_castable && Variablewings_pool && ((!TalentExecutionSentence || (EnemiesInMelee >= 2 || CDExecutionSentenceRemains > GCDMAX * 2)) || (CDAvengingWrathRemains > GCDMAX * 3 && CDAvengingWrathRemains < 10000 || CDCrusadeRemains > GCDMAX * 3 && CDCrusadeRemains < 10000 || BuffCrusadeUp && BuffCrusadeStack < 10)))
                {
                    Aimsharp.Cast("Divine Storm");
                    return true;
                }
            }

            //actions.finishers+=/templars_verdict,if=variable.wings_pool&(!talent.execution_sentence.enabled|cooldown.execution_sentence.remains>gcd*2|cooldown.avenging_wrath.remains>gcd*3&cooldown.avenging_wrath.remains<10|cooldown.crusade.remains>gcd*3&cooldown.crusade.remains<10|buff.crusade.up&buff.crusade.stack<10)
            if (Aimsharp.CanCast("Templar's Verdict"))
            {
                if (NoCooldowns || Variablewings_pool && (!TalentExecutionSentence || CDExecutionSentenceRemains > GCDMAX * 2 || CDAvengingWrathRemains > GCDMAX * 3 && CDAvengingWrathRemains < 10000 || CDCrusadeRemains > GCDMAX * 3 && CDCrusadeRemains < 10000 || BuffCrusadeUp && BuffCrusadeStack < 10))
                {
                    Aimsharp.Cast("Templar's Verdict");
                    return true;
                }
            }

            //actions.generators+=/concentrated_flame
            if (MajorPower == "Concentrated Flame")
            {
                if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                {
                    Aimsharp.Cast("Concentrated Flame");
                    return true;
                }
            }

            //actions.generators+=/reaping_flames
            if (MajorPower == "Reaping Flames")
            {
                if (Aimsharp.CanCast("Reaping Flames"))
                {
                    Aimsharp.Cast("Reaping Flames");
                    return true;
                }
            }

            //actions.generators+=/crusader_strike,if=holy_power<=4
            if (Aimsharp.CanCast("Crusader Strike"))
            {
                if (HolyPower <= 4)
                {
                    Aimsharp.Cast("Crusader Strike");
                    return true;
                }
            }

            //actions.generators+=/arcane_torrent,if=holy_power<=4
            if (RacialPower == "Bloodelf" && Fighting)
            {
                if (HolyPower <= 4)
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


        public override bool OutOfCombatTick()
        {
            return false;
        }

    }
}
