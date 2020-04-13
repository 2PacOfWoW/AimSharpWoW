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
    public class PerfSimHavoc : Rotation
    {


        public override void LoadSettings()
        {


            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Ashvane's Razor Coral", "Pocket-Sized Computation Device", "Galecaller's Boon", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            Settings.Add(new Setting("Chaotic Transformation Trait?", true));

            Settings.Add(new Setting("# Revolving Blades Traits", 0, 3, 1));



        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;

        public override void Initialize()
        {
            Aimsharp.PrintMessage("Perfect Simcraft Series: Havoc DH - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 2313221", Color.Blue);
            Aimsharp.PrintMessage("These macros can be used for manual control:", Color.Blue);
            Aimsharp.PrintMessage("/xxxxx Potions", Color.Blue);
            Aimsharp.PrintMessage("--Toggles using buff potions on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx SaveCooldowns", Color.Blue);
            Aimsharp.PrintMessage("--Toggles the use of big cooldowns on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx AOE", Color.Blue);
            Aimsharp.PrintMessage("--Toggles AOE mode on/off.", Color.Blue);
            Aimsharp.PrintMessage("--Replace xxxxx with first 5 letters of your addon, lowercase.", Color.Blue);

            Aimsharp.Latency = 50;
            Aimsharp.QuickDelay = 125;

            MajorPower = GetDropDown("Major Power");
            TopTrinket = GetDropDown("Top Trinket");
            BotTrinket = GetDropDown("Bot Trinket");

            Spellbook.Add(MajorPower);
            Spellbook.Add("Blade Dance");
            Spellbook.Add("Metamorphosis");
            Spellbook.Add("Nemesis");
            Spellbook.Add("Eye Beam");
            Spellbook.Add("Dark Slash");
            Spellbook.Add("Fel Barrage");
            Spellbook.Add("Chaos Strike");
            Spellbook.Add("Death Sweep");
            Spellbook.Add("Immolation Aura");
            Spellbook.Add("Annihilation");
            Spellbook.Add("Felblade");
            Spellbook.Add("Demon's Bite");
            Spellbook.Add("Throw Glaive");

            Buffs.Add("Revolving Blades");
            Buffs.Add("Metamorphosis");
            Buffs.Add("Momentum");
            Buffs.Add("Memory of Lucid Dreams");
            Buffs.Add("Thirsting Blades");
            Buffs.Add("Prepared");
            Buffs.Add("Lifeblood");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Dark Slash");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");
            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("meta self", "/cast [@player] Metamorphosis");

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
        }





        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {
            int GCD = Aimsharp.GCD();
            float Haste = Aimsharp.Haste() / 100f;
            int TargetTimeToDie = 1000000000;
            int GCDMAX = (int)(1500f / (Haste + 1f));
            int Latency = Aimsharp.Latency;
            bool Moving = Aimsharp.PlayerIsMoving();
            bool Fighting = Aimsharp.Range("target") <= 15 && Aimsharp.TargetIsEnemy();
            bool IsChanneling = Aimsharp.IsChanneling("player");
            bool TrailOfRuinEnabled = Aimsharp.Talent(3, 1);
            int EnemiesInMelee = Aimsharp.EnemiesInMelee();
            bool FirstBloodEnabled = Aimsharp.Talent(5, 2);
            int RevolvingBladesStacks = Aimsharp.BuffStacks("Revolving Blades");
            int Fury = Aimsharp.Power("player");
            bool AOE = Aimsharp.IsCustomCodeOn("AOE");
            bool NemesisEnabled = Aimsharp.Talent(7, 3);
            bool DemonicEnabled = Aimsharp.Talent(7, 1);
            bool BlindFuryEnabled = Aimsharp.Talent(1, 1);
            bool DarkSlashEnabled = Aimsharp.Talent(5, 3);
            bool MomentumEnabled = Aimsharp.Talent(7, 2);
            bool HasMomentum = Aimsharp.HasBuff("Momentum");
            int NemesisCD = Aimsharp.SpellCooldown("Nemesis") - GCD;
            int EyeBeamCD = Aimsharp.SpellCooldown("Eye Beam") - GCD;
            int DarkSlashCD = Aimsharp.SpellCooldown("Dark Slash") - GCD;
            int BladeDanceCD = Aimsharp.SpellCooldown("Blade Dance") - GCD;
            int MetaCD = Aimsharp.SpellCooldown("Metamorphosis") - GCD;
            int FuryDefecit = Aimsharp.PlayerMaxPower() - Fury;
            bool BladeDance = (FirstBloodEnabled || EnemiesInMelee >= 3 - 1 * (TrailOfRuinEnabled ? 1 : 0));
            bool WaitingForNemesis = !((!NemesisEnabled || NemesisCD < 0 || NemesisCD > TargetTimeToDie || NemesisCD > 60000));
            bool PoolingForMeta = !DemonicEnabled && MetaCD < 6000 && FuryDefecit > 30 && (!WaitingForNemesis || NemesisCD < 10000);
            bool PoolingForBladeDance = BladeDance && Fury < 75 - (FirstBloodEnabled ? 20 : 0);
            bool PoolingForEyeBeam = DemonicEnabled && !BlindFuryEnabled && EyeBeamCD < GCDMAX * 2 && FuryDefecit > 20;
            bool WaitingForDarkSlash = DarkSlashEnabled && !PoolingForBladeDance && !PoolingForMeta && DarkSlashCD < 0;
            bool WaitingForMomentum = MomentumEnabled && !HasMomentum;
            bool NoCooldowns = Aimsharp.IsCustomCodeOn("SaveCooldowns");
            bool ChaoticTransformationEnabled = GetCheckBox("Chaotic Transformation Trait?");
            string PotionType = GetDropDown("Potion Type");
            bool UsePotion = Aimsharp.IsCustomCodeOn("Potions");
            int MetaRemains = Aimsharp.BuffRemaining("Metamorphosis") - GCD;
            bool FelBarrageEnabled = Aimsharp.Talent(3, 3);
            int FelBarrageCD = Aimsharp.SpellCooldown("Fel Barrage");
            bool MetaUp = Aimsharp.HasBuff("Metamorphosis");
            bool MemoryUp = Aimsharp.HasBuff("Memory of Lucid Dreams");
            bool CoralDebuffUp = Aimsharp.HasDebuff("Razor Coral");
            bool InkUp = Aimsharp.HasDebuff("Conductive Ink");
            int TargetHealth = Aimsharp.Health("target");
            int FlameFullRecharge = (int)(Aimsharp.RechargeTime("Concentrated Flame") - GCD + (30000f) * (1f - Aimsharp.SpellCharges("Concentrated Flame")));
            bool DarkSlashUp = Aimsharp.HasDebuff("Dark Slash");
            int ThirstingBladesStacks = Aimsharp.BuffStacks("Thirsting Blades");
            int RevolvingBladesRank = GetSlider("# Revolving Blades Traits");
            bool DemonBladesEnabled = Aimsharp.Talent(2, 2);
            int RangeToTarget = Aimsharp.Range("target");

            if (!AOE)
            {
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }

            if (IsChanneling)
                return false;

            if (!NoCooldowns)
            {
                if (Aimsharp.CanCast("Metamorphosis", "player") && RangeToTarget < 8 && Fighting)
                {
                    if (!(DemonicEnabled || PoolingForMeta || WaitingForNemesis) || TargetTimeToDie < 25000)
                    {
                        Aimsharp.Cast("meta self");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Metamorphosis", "player") && RangeToTarget < 8 && Fighting)
                {
                    if (DemonicEnabled && (!ChaoticTransformationEnabled || (EyeBeamCD > 2000 && (!BladeDance || BladeDanceCD > GCDMAX ))))
                    {
                        Aimsharp.Cast("meta self");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Nemesis"))
                {
                    Aimsharp.Cast("Nemesis");
                    return true;
                }

                if (UsePotion)
                {
                    if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                    {
                        if (MetaRemains > 25000 || TargetTimeToDie < 60000)
                        {
                            Aimsharp.Cast("potion");
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanUseItem("Galecaller's Boon") && Aimsharp.IsEquipped("Galecaller's Boon"))
                {
                    if (!FelBarrageEnabled || FelBarrageCD < 0)
                    {
                        if (TopTrinket == "Galecaller's Boon")
                        {
                            Aimsharp.Cast(TopTrinket, true);
                            return true;
                        }
                        if (BotTrinket == "Galecaller's Boon")
                        {
                            Aimsharp.Cast(BotTrinket, true);
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanUseItem("Pocket-Sized Computation Device") && Aimsharp.IsEquipped("Pocket-Sized Computation Device"))
                {
                    if (MetaUp && !MemoryUp && (!BladeDance || BladeDanceCD > 0))
                    {
                        if (TopTrinket == "Pocket-Sized Computation Device")
                        {
                            Aimsharp.Cast(TopTrinket, true);
                            return true;
                        }
                        if (BotTrinket == "Pocket-Sized Computation Device")
                        {
                            Aimsharp.Cast(BotTrinket, true);
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanUseItem("Ashvane's Razor Coral") && Aimsharp.IsEquipped("Ashvane's Razor Coral"))
                {
                    if (!CoralDebuffUp || ((InkUp || MetaRemains > 20000) && TargetHealth < 31) || TargetTimeToDie < 20000)
                    {
                        if (TopTrinket == "Ashvane's Razor Coral")
                        {
                            Aimsharp.Cast(TopTrinket, true);
                            return true;
                        }
                        if (BotTrinket == "Ashvane's Razor Coral")
                        {
                            Aimsharp.Cast(BotTrinket, true);
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanUseItem("Azshara's Font of Power") && Aimsharp.IsEquipped("Azshara's Font of Power"))
                {
                    if (MetaCD < 10000 || MetaCD > 60000)
                    {
                        if (TopTrinket == "Azshara's Font of Power")
                        {
                            Aimsharp.Cast(TopTrinket, true);
                            return true;
                        }
                        if (BotTrinket == "Azshara's Font of Power")
                        {
                            Aimsharp.Cast(BotTrinket, true);
                            return true;
                        }
                    }
                }

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

                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame"))
                    {
                        if ((FlameFullRecharge < GCDMAX) || TargetTimeToDie < 5000)
                        {
                            Aimsharp.Cast("Concentrated Flame");
                            return true;
                        }
                    }
                }

                if (MajorPower == "Blood of the Enemy")
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player") && EnemiesInMelee > 0)
                    {
                        if (MetaUp || TargetTimeToDie < 10000)
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }
                }

                if (MajorPower == "Guardian of Azeroth")
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        if ((MetaUp && MetaCD < 0) || MetaRemains > 25000 || TargetTimeToDie <= 30000)
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                    }
                }

                if (MajorPower == "Focused Azerite Beam")
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player") && Fighting)
                    {
                        if (EnemiesInMelee >= 2 || !AOE)
                        {
                            Aimsharp.Cast("Focused Azerite Beam");
                            return true;
                        }
                    }
                }

                if (MajorPower == "Worldvein Resonance")
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        if (Aimsharp.BuffStacks("Lifeblood", "player", false) < 3)
                        {
                            Aimsharp.Cast("Worldvein Resonance");
                            return true;
                        }
                    }
                }

                if (MajorPower == "Memory of Lucid Dreams")
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (Fury < 40 && MetaUp)
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }
            }

            if (DarkSlashEnabled && (WaitingForDarkSlash || DarkSlashUp))
            {
                if (Aimsharp.CanCast("Dark Slash") && Fury >= 80 && (!BladeDance || BladeDanceCD > 0) )
                {
                    Aimsharp.Cast("Dark Slash");
                    return true;
                }

                if (Aimsharp.CanCast("Chaos Strike"))
                {
                    Aimsharp.Cast("Chaos Strike");
                    return true;
                }
            }

            if (DemonicEnabled)
            {
                if (Aimsharp.CanCast("Death Sweep", "player") && BladeDance && RangeToTarget < 8)
                {
                    Aimsharp.Cast("Death Sweep");
                    return true;
                }

                if (Aimsharp.CanCast("Eye Beam", "player") && !Moving && (EnemiesInMelee >= 2 || TargetTimeToDie > 25000) && RangeToTarget < 15)
                {
                    Aimsharp.Cast("Eye Beam");
                    return true;
                }

                if (Aimsharp.CanCast("Blade Dance", "player") && RangeToTarget < 8)
                {
                    if ((BladeDance && (MetaCD > 0 || NoCooldowns) && (EyeBeamCD > (5000 - RevolvingBladesRank * 3000))))
                    {
                        Aimsharp.Cast("Blade Dance");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Immolation Aura", "player") && RangeToTarget < 8)
                {
                    Aimsharp.Cast("Immolation Aura");
                    return true;
                }

                if (Aimsharp.CanCast("Annihilation") && !PoolingForBladeDance)
                {
                    Aimsharp.Cast("Annihilation");
                    return true;
                }

                if (Aimsharp.CanCast("Felblade") && FuryDefecit >= 40)
                {
                    Aimsharp.Cast("Felblade");
                    return true;
                }

                if (Aimsharp.CanCast("Chaos Strike") && !PoolingForBladeDance && !PoolingForEyeBeam)
                {
                    Aimsharp.Cast("Chaos Strike");
                    return true;
                }

                if (Aimsharp.CanCast("Demon's Bite"))
                {
                    Aimsharp.Cast("Demon's Bite");
                    return true;
                }

                if (Aimsharp.CanCast("Throw Glaive") && RangeToTarget > 15)
                {
                    Aimsharp.Cast("Throw Glaive");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Fel Barrage", "player"))
            {
                if (!WaitingForMomentum && EnemiesInMelee > 0)
                {
                    Aimsharp.Cast("Fel Barrage");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Death Sweep", "player") && BladeDance && RangeToTarget < 8)
            {
                Aimsharp.Cast("Death Sweep");
                return true;
            }

            if (Aimsharp.CanCast("Immolation Aura", "player") && RangeToTarget < 8)
            {
                Aimsharp.Cast("Immolation Aura");
                return true;
            }

            if (Aimsharp.CanCast("Eye Beam", "player") && !Moving && EnemiesInMelee >= 2)
            {
                Aimsharp.Cast("Eye Beam");
                return true;
            }

            if (Aimsharp.CanCast("Blade Dance", "player") && BladeDance && RangeToTarget < 8)
            {
                Aimsharp.Cast("Blade Dance");
                return true;
            }

            if (Aimsharp.CanCast("Felblade") && FuryDefecit >= 40)
            {
                Aimsharp.Cast("Felblade");
                return true;
            }

            if (Aimsharp.CanCast("Eye Beam", "player") && !Moving && BlindFuryEnabled && RangeToTarget < 15)
            {
                Aimsharp.Cast("Eye Beam");
                return true;
            }

            if (Aimsharp.CanCast("Annihilation"))
            {
                if ( (DemonicEnabled || !WaitingForMomentum || FuryDefecit < 30 || MetaRemains < 5000) && !PoolingForBladeDance && !WaitingForDarkSlash )
                {
                    Aimsharp.Cast("Annihilation");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Chaos Strike"))
            {
                if ((DemonicEnabled || !WaitingForMomentum || FuryDefecit < 30 || MetaRemains < 5000) && !PoolingForMeta && !PoolingForBladeDance && !WaitingForDarkSlash)
                {
                    Aimsharp.Cast("Chaos Strike");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Demon's Bite"))
            {
                Aimsharp.Cast("Demon's Bite");
                return true;
            }

            if (Aimsharp.CanCast("Throw Glaive") && DemonBladesEnabled)
            {
                Aimsharp.Cast("Throw Glaive");
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
