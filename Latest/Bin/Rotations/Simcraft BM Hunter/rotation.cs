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
    public class PerfSimBM : Rotation
    {
        public override void LoadSettings()
        {

            Aimsharp.Latency = 50;
            Aimsharp.QuickDelay = 125;

            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Ashvane's Razor Coral", "Pocket-Sized Computation Device", "Galecaller's Boon", "Shiver Venom Relic", "Lurker's Insidious Gift", "Notorious Gladiator's Badge", "Sinister Gladiator's Badge", "Sinister Gladiator's Medallion", "Notorious Gladiator's Medallion", "Vial of Animated Blood", "First Mate's Spyglass", "Jes' Howler", "Ashvane's Razor Coral", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("Primal Instincts Trait?", true));

            Settings.Add(new Setting("Feeding Frenzy Trait?", false));

            Settings.Add(new Setting("Rapid Reload Trait?", false));

            Settings.Add(new Setting("# Dance of Death Traits", 0, 3, 2));
        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {

            Aimsharp.PrintMessage("Perfect Simcraft Series: BM Hunter - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 2222212", Color.Blue);
            Aimsharp.PrintMessage("These macros can be used for manual control:", Color.Blue);
            Aimsharp.PrintMessage("/xxxxx AOE", Color.Blue);
            Aimsharp.PrintMessage("--Toggles AOE mode on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx Potions", Color.Blue);
            Aimsharp.PrintMessage("--Toggles using buff potions on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx SaveCooldowns", Color.Blue);
            Aimsharp.PrintMessage("--Toggles the use of big cooldowns on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx Prepull 6", Color.Blue);
            Aimsharp.PrintMessage("--Starts prepull rotation (hit it at around 6 seconds before)", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("--Replace xxxxx with first 5 letters of your addon, lowercase.", Color.Blue);


            MajorPower = GetDropDown("Major Power");
            TopTrinket = GetDropDown("Top Trinket");
            BotTrinket = GetDropDown("Bot Trinket");
            RacialPower = GetDropDown("Racial Power");

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

            Spellbook.Add(MajorPower);
            Spellbook.Add("Aspect of the Wild");
            Spellbook.Add("Bestial Wrath");
            Spellbook.Add("Barbed Shot");
            Spellbook.Add("Stampede");
            Spellbook.Add("Kill Command");
            Spellbook.Add("Chimaera Shot");
            Spellbook.Add("Dire Beast");
            Spellbook.Add("Barrage");
            Spellbook.Add("Cobra Shot");
            Spellbook.Add("Spitting Cobra");
            Spellbook.Add("A Murder of Crows");
            Spellbook.Add("Multi-Shot");

            Buffs.Add("Aspect of the Wild");
            Buffs.Add("Bestial Wrath");
            Buffs.Add("Frenzy");
            Buffs.Add("Lifeblood");
            Buffs.Add("Dance of Death");
            Buffs.Add("Beast Cleave");
            Buffs.Add("Barbed Shot");

            Debuffs.Add("Razor Coral");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");

            CustomCommands.Add("AOE");
            CustomCommands.Add("Prepull");
            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
        }

        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {
            int GCD = Aimsharp.GCD();
            int Latency = Aimsharp.Latency;
            bool Moving = Aimsharp.PlayerIsMoving();
            bool IsChanneling = Aimsharp.IsChanneling("player");
            bool Fighting = Aimsharp.Range("target") <= 45 && Aimsharp.TargetIsEnemy();
            bool FontEquipped = Aimsharp.IsEquipped("Azshara's Font of Power");
            bool CanUseFont = Aimsharp.CanUseItem("Azshara's Font of Power");
            bool CoralEquipped = Aimsharp.IsEquipped("Ashvane's Razor Coral");
            bool CanUseCoral = Aimsharp.CanUseItem("Ashvane's Razor Coral");
            bool CycloEquipped = Aimsharp.IsEquipped("Pocket-Sized Computation Device");
            bool CanUseCyclo = Aimsharp.CanUseItem("Pocket-Sized Computation Device");
            bool CoralDebuffUp = Aimsharp.HasDebuff("Razor Coral", "target");
            string PrevGCD = Aimsharp.LastCast();
            int AotWRemaining = Aimsharp.BuffRemaining("Aspect of the Wild") - GCD;
            bool AotWUp = Aimsharp.HasBuff("Aspect of the Wild");
            int AotWCD = Aimsharp.SpellCooldown("Aspect of the Wild") - GCD;
            int TargetHealth = Aimsharp.Health("target");
            bool KillerInstinctEnabled = Aimsharp.Talent(1, 1);
            int TargetTimeToDie = 1000000000;
            int CycloCD = Aimsharp.ItemCooldown("Cyclotronic Blast") - GCD;
            bool BeastialWrathUp = Aimsharp.BuffRemaining("Bestial Wrath") - GCD > 0;
            int BestialCD = Aimsharp.SpellCooldown("Bestial Wrath") - GCD;
            bool PetFrenzyUp = Aimsharp.BuffRemaining("Frenzy", "pet", false) - GCD > 0;
            int PetFrenzyRemains = Aimsharp.BuffRemaining("Frenzy", "pet") - GCD;
            float Haste = Aimsharp.Haste() / 100f;
            int GCDMAX = (int)((1500f / (Haste + 1f)));
            string PotionType = GetDropDown("Potion Type");
            bool UsePotion = Aimsharp.IsCustomCodeOn("Potions");
            bool AOE = Aimsharp.IsCustomCodeOn("AOE");
            int EnemiesNearTarget = Aimsharp.EnemiesNearTarget();
            int EnemiesInMelee = Aimsharp.EnemiesInMelee();
            int BarbedShotFullRecharge = (int)(Aimsharp.RechargeTime("Barbed Shot") + (12000f / (1f + Haste)) * (1f - Aimsharp.SpellCharges("Barbed Shot")));
            bool PrimalInstinctsEnabled = GetCheckBox("Primal Instincts Trait?");
            int Focus = Aimsharp.Power("player");
            int BarbedShotBuffCount = Aimsharp.BuffStacks("Barbed Shot");

            int BarbedShotCountForGCD = 0;
            for (int i = 0; i < BarbedShotBuffCount; i++)
            {
                if (Aimsharp.BuffInfoDetailed("player", "Barbed Shot", true)[i]["Remaining"] - GCD > GCDMAX)
                    BarbedShotCountForGCD++;
            }
            
            float FocusRegen = 10f * (1f + Haste) + BarbedShotCountForGCD * 2.5f;
            int FocusMax = Aimsharp.PlayerMaxPower();
            int BarbedShotCharges = Aimsharp.SpellCharges("Barbed Shot");
            int DoDCount = GetSlider("# Dance of Death Traits");
            int DoDRemaining = Aimsharp.BuffRemaining("Dance of Death") - GCD;
            bool HasDoD = Aimsharp.HasBuff("Dance of Death");
            float CritPercent = Aimsharp.Crit() / 100f;
            bool OneWithPackEnabled = Aimsharp.Talent(2, 2);
            float BarbedShotChargesFractional = Aimsharp.SpellCharges("Barbed Shot") +  (Aimsharp.RechargeTime("Barbed Shot") - GCD) / ((12000f) / (1f + Haste));
            int KillCommandCD = Aimsharp.SpellCooldown("Kill Command") - GCD;
            int BestialCDGuess = BestialCD / 2;
            float FocusTimeToMax = (FocusMax - Focus) * 1000f / FocusRegen;
            int FlameFullRecharge = (int)(Aimsharp.RechargeTime("Concentrated Flame") - GCD + (30000f) * (1f - Aimsharp.SpellCharges("Concentrated Flame")));
            int BeastCleaveRemains = Aimsharp.BuffRemaining("Beast Cleave") - GCD;
            bool FeedingFrenzyEnabled = GetCheckBox("Feeding Frenzy Trait?");
            int PetFrenzyDuration = 8000 + (FeedingFrenzyEnabled ? 1000 : 0);
            bool RapidReloadEnabled = GetCheckBox("Rapid Reload Trait?");
            bool Prepull = Aimsharp.IsCustomCodeOn("Prepull");
            bool NoCooldowns = Aimsharp.IsCustomCodeOn("SaveCooldowns");

            if (!AOE)
            {
                EnemiesNearTarget = 1;
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }


            if (IsChanneling || Aimsharp.CastingID("player") == 295261)
                return false;


            if (!NoCooldowns)
            {
                if (FontEquipped)
                {
                    if (CanUseFont && TargetTimeToDie > 10000 && !Moving && Fighting)
                    {
                        Aimsharp.Cast("Azshara's Font of Power");
                        return true;
                    }
                }

                if (CoralEquipped)
                {
                    if (CanUseCoral && Fighting)
                    {
                        if ((CoralDebuffUp && (PrevGCD == "Aspect of the Wild" || (!CycloEquipped && AotWRemaining > 5000) && (TargetHealth < 35 || MajorPower != "Guardian of Azeroth" || !KillerInstinctEnabled)) ||
                            ((!CoralDebuffUp || TargetTimeToDie < 26000) && TargetTimeToDie > (24000 * (CycloCD + 4000 < TargetTimeToDie ? 1 : 0)))))
                        {
                            Aimsharp.Cast("Ashvane's Razor Coral");
                            return true;
                        }
                    }
                }

                if (CycloEquipped)
                {
                    if (CanUseCyclo && Fighting)
                    {
                        if (!BeastialWrathUp || TargetTimeToDie < 5000)
                        {
                            Aimsharp.Cast("Pocket-Sized Computation Device");
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanCast("Ancestral Call", "player") && Fighting)
                {
                    if (BestialCD > 30000)
                    {
                        Aimsharp.Cast("Ancestral Call", true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Fireblood", "player") && Fighting)
                {
                    if (BestialCD > 30000)
                    {
                        Aimsharp.Cast("Fireblood", true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Berserking", "player") && Fighting)
                {
                    if ((AotWRemaining > 0 && (TargetTimeToDie > 192000 || (TargetHealth < 35 || !KillerInstinctEnabled))) || TargetTimeToDie < 13000)
                    {
                        Aimsharp.Cast("Berserking", true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Blood Fury", "player") && Fighting)
                {
                    if ((AotWRemaining > 0 && (TargetTimeToDie > 192000 || (TargetHealth < 35 || !KillerInstinctEnabled))) || TargetTimeToDie < 16000)
                    {
                        Aimsharp.Cast("Blood Fury", true);
                        return true;
                    }
                }

                if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic" && Fighting)
                {
                    if ((AotWRemaining > 0 && (TargetTimeToDie > 192000 || (TargetHealth < 35 || !KillerInstinctEnabled))) || TargetTimeToDie < 16000)
                    {
                        Aimsharp.Cast("TopTrink", true);
                        return true;
                    }
                }

                if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic" && Fighting)
                {
                    if ((AotWRemaining > 0 && (TargetTimeToDie > 192000 || (TargetHealth < 35 || !KillerInstinctEnabled))) || TargetTimeToDie < 16000)
                    {
                        Aimsharp.Cast("BotTrink", true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Light's Judgment", "player") && Fighting)
                {
                    if ((PetFrenzyUp && PetFrenzyRemains > GCDMAX) || !PetFrenzyUp)
                    {
                        Aimsharp.Cast("Light's Judgment", true);
                        return true;
                    }
                }

                if (UsePotion && Fighting)
                {
                    if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                    {
                        if ((BeastialWrathUp && AotWRemaining > 0 && TargetHealth < 35) || (PotionType == "Potion of Unbridled Fury" && TargetTimeToDie < 61000) || TargetTimeToDie < 26000)
                        {
                            Aimsharp.Cast("potion");
                            return true;
                        }
                    }
                }

                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (Aimsharp.BuffStacks("Lifeblood", "player", false) < 4)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }

                    }
                }

                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        if ((AotWCD < 10000 || TargetTimeToDie > 210000 || TargetTimeToDie < 30000))
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }
                }
            }

            if (NoCooldowns)
            {
                if (CoralEquipped)
                {
                    if (CanUseCoral && Fighting)
                    {
                        if ((CoralDebuffUp && (PrevGCD == "Aspect of the Wild" || (!CycloEquipped && AotWRemaining > 5000) && (TargetHealth < 35 || MajorPower != "Guardian of Azeroth" || !KillerInstinctEnabled)) ||
                            ((!CoralDebuffUp || TargetTimeToDie < 26000) && TargetTimeToDie > (24000 * (CycloCD + 4000 < TargetTimeToDie ? 1 : 0)))))
                        {
                            Aimsharp.Cast("Ashvane's Razor Coral");
                            return true;
                        }
                    }
                }
            }

            if (EnemiesNearTarget <= 1)
            {
                if (Aimsharp.CanCast("Barbed Shot"))
                {
                    if ((PetFrenzyUp && PetFrenzyRemains < GCDMAX) || (BestialCD > 0 && (BarbedShotFullRecharge < GCDMAX || (PrimalInstinctsEnabled && AotWCD < GCDMAX))))
                    {
                        Aimsharp.Cast("Barbed Shot");
                        return true;
                    }

                }

                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame"))
                    {
                        if (((float)Focus + FocusRegen * (GCDMAX+GCD) < FocusMax && !BeastialWrathUp) || (FlameFullRecharge < GCDMAX) || TargetTimeToDie < 5000)
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

                if (Aimsharp.CanCast("Aspect of the Wild", "player") && Fighting && !NoCooldowns)
                {
                    if (BarbedShotCharges < 1 || !PrimalInstinctsEnabled)
                    {
                        Aimsharp.Cast("Aspect of the Wild");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Stampede", "player") && Fighting)
                {
                    if (((AotWRemaining > 0 || NoCooldowns) && BeastialWrathUp) || TargetTimeToDie < 15000)
                    {
                        Aimsharp.Cast("Stampede");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("A Murder of Crows", "target"))
                {
                    Aimsharp.Cast("A Murder of Crows");
                    return true;
                }

                if (MajorPower == "Focused Azerite Beam" && Fighting)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        if (!BeastialWrathUp || TargetTimeToDie < 5000)
                        {
                            Aimsharp.Cast("Focused Azerite Beam");
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanCast("Bestial Wrath", "player") && Fighting)
                {
                    if ((!BeastialWrathUp && (AotWCD > 15000 || NoCooldowns)) || TargetTimeToDie < 15000 + GCDMAX+GCD)
                    {
                        Aimsharp.Cast("Bestial Wrath");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Barbed Shot", "target"))
                {
                    if ((DoDCount > 1 && DoDRemaining < GCDMAX && CritPercent > .40f))
                    {
                        Aimsharp.Cast("Barbed Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Kill Command", "target"))
                {
                    Aimsharp.Cast("Kill Command");
                    return true;
                }

                if (Aimsharp.CanCast("Chimaera Shot", "target"))
                {
                    Aimsharp.Cast("Chimaera Shot");
                    return true;
                }

                if (Aimsharp.CanCast("Dire Beast", "target"))
                {
                    Aimsharp.Cast("Dire Beast");
                    return true;
                }

                if (Aimsharp.CanCast("Barbed Shot", "target"))
                {
                    if ((OneWithPackEnabled && BarbedShotChargesFractional > 1.5f) || BarbedShotChargesFractional > 1.8f || ((AotWCD < PetFrenzyDuration - GCDMAX && !NoCooldowns) && PrimalInstinctsEnabled && PetFrenzyUp) || TargetTimeToDie < 9000)
                    {
                        Aimsharp.Cast("Barbed Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Barrage", "player") && Fighting)
                {
                    Aimsharp.Cast("Barrage");
                    return true;
                }

                if (Aimsharp.CanCast("Cobra Shot"))
                {
                    if ((((Focus - 45 + FocusRegen * (KillCommandCD - 1000)) > 30 || (KillCommandCD > 1000 + GCDMAX && BestialCDGuess > (int)FocusTimeToMax)) && KillCommandCD > 1000) || TargetTimeToDie < 3000)
                    {
                        Aimsharp.Cast("Cobra Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Spitting Cobra", "player"))
                {
                    Aimsharp.Cast("Spitting Cobra");
                    return true;
                }

                if (Aimsharp.CanCast("Barbed Shot", "target"))
                {
                    if ((PetFrenzyDuration - GCDMAX > BarbedShotFullRecharge))
                    {
                        Aimsharp.Cast("Barbed Shot");
                        return true;
                    }
                }

                return false;
            }

            if (EnemiesNearTarget > 1)
            {
                if (Aimsharp.CanCast("Barbed Shot"))
                {
                    if ((PetFrenzyUp && PetFrenzyRemains < GCDMAX))
                    {
                        Aimsharp.Cast("Barbed Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Multi-Shot"))
                {
                    if ((GCDMAX - BeastCleaveRemains) > 250)
                    {
                        Aimsharp.Cast("Multi-Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Barbed Shot"))
                {
                    if ((BarbedShotFullRecharge < GCDMAX && BestialCD > 0))
                    {
                        Aimsharp.Cast("Barbed Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Aspect of the Wild", "player") && Fighting && !NoCooldowns)
                {
                    Aimsharp.Cast("Aspect of the Wild");
                    return true;
                }

                if (Aimsharp.CanCast("Stampede", "player") && Fighting)
                {
                    if (((AotWRemaining > 0 || NoCooldowns) && BeastialWrathUp) || TargetTimeToDie < 15000)
                    {
                        Aimsharp.Cast("Stampede");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Bestial Wrath", "player") && Fighting)
                {
                    if ((AotWCD > 20000 || NoCooldowns) || OneWithPackEnabled || TargetTimeToDie < 15000)
                    {
                        Aimsharp.Cast("Bestial Wrath");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Chimaera Shot", "target"))
                {
                    Aimsharp.Cast("Chimaera Shot");
                    return true;
                }

                if (Aimsharp.CanCast("A Murder of Crows", "target"))
                {
                    Aimsharp.Cast("A Murder of Crows");
                    return true;
                }

                if (Aimsharp.CanCast("Barrage", "player") && Fighting)
                {
                    Aimsharp.Cast("Barrage");
                    return true;
                }

                if (Aimsharp.CanCast("Kill Command", "target"))
                {
                    Aimsharp.Cast("Kill Command");
                    return true;
                }

                if (Aimsharp.CanCast("Dire Beast", "target"))
                {
                    Aimsharp.Cast("Dire Beast");
                    return true;
                }

                if (Aimsharp.CanCast("Barbed Shot", "target"))
                {
                    if ((!PetFrenzyUp && (BarbedShotChargesFractional > 1.8f || BeastialWrathUp)) || ((AotWCD < PetFrenzyDuration - GCDMAX && !NoCooldowns) && PrimalInstinctsEnabled) || BarbedShotChargesFractional > 1.4 || TargetTimeToDie < 9000)
                    {
                        Aimsharp.Cast("Barbed Shot");
                        return true;
                    }
                }

                if (MajorPower == "Focused Azerite Beam" && Fighting)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {

                        Aimsharp.Cast("Focused Azerite Beam");
                        return true;

                    }
                }

                if (Aimsharp.CanCast("Multi-Shot"))
                {
                    if ((RapidReloadEnabled))
                    {
                        Aimsharp.Cast("Multi-Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Cobra Shot"))
                {
                    if ((KillCommandCD > FocusTimeToMax) && (!RapidReloadEnabled))
                    {
                        Aimsharp.Cast("Cobra Shot");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Spitting Cobra", "player"))
                {
                    Aimsharp.Cast("Spitting Cobra");
                    return true;
                }

                return false;
            }





            return false;
        }

        public override bool OutOfCombatTick()
        {
            bool IsChanneling = Aimsharp.IsChanneling("player");
            bool FontEquipped = Aimsharp.IsEquipped("Azshara's Font of Power");
            bool CanUseFont = Aimsharp.CanUseItem("Azshara's Font of Power");
            bool CycloEquipped = Aimsharp.IsEquipped("Cyclotronic Blast");
            bool CanUseCyclo = Aimsharp.CanUseItem("Cyclotronic Blast");
            string PotionType = GetDropDown("Potion Type");
            bool UsePotion = Aimsharp.IsCustomCodeOn("Potions");
            bool Prepull = Aimsharp.IsCustomCodeOn("Prepull");
            bool PrimalInstinctsEnabled = GetCheckBox("Primal Instincts Trait?");

            if (IsChanneling)
                return true;


            if (Prepull)
            {
                if (FontEquipped)
                {
                    if (CanUseFont)
                    {
                        Aimsharp.Cast("Azshara's Font of Power");
                        return true;
                    }
                }

                if (UsePotion)
                {
                    if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                    {
                        Aimsharp.Cast("potion");
                        return true;
                    }
                }

                if (MajorPower == "Worldvein Resonance")
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        Aimsharp.Cast("Worldvein Resonance");
                        return true;
                    }
                }

                if (MajorPower == "Guardian of Azeroth")
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Aspect of the Wild", "player"))
                {
                    if ((!PrimalInstinctsEnabled && MajorPower != "Focused Azerite Beam" && (FontEquipped | !CycloEquipped)))
                    {
                        Aimsharp.Cast("Aspect of the Wild");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Bestial Wrath", "player"))
                {
                    if ((PrimalInstinctsEnabled && MajorPower != "Focused Azerite Beam" && (FontEquipped | !CycloEquipped)))
                    {
                        Aimsharp.Cast("Bestial Wrath");
                        return true;
                    }
                }

            }

            return false;
        }

    }
}
