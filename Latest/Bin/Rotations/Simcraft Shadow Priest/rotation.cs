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
    public class PerfectSimShadow : Rotation
    {


        public override void LoadSettings()
        {
            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Shiver Venom Relic", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            Settings.Add(new Setting("Chorus of Insanity Trait?", true));

            Settings.Add(new Setting("Searing Dialogue Trait?", false));

        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        public override void Initialize()
        {
            Aimsharp.PrintMessage("Perfect Simcraft Series: Shadow Priest - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 3111111", Color.Blue);
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
            Aimsharp.PrintMessage("--Replace xxxxx with first 5 letters of your addon, lowercase.", Color.Blue);


            Aimsharp.Latency = 0;
            Aimsharp.QuickDelay = 125;
            Aimsharp.SlowDelay = 300;

            MajorPower = GetDropDown("Major Power");
            TopTrinket = GetDropDown("Top Trinket");
            BotTrinket = GetDropDown("Bot Trinket");

            Spellbook.Add(MajorPower);
            Spellbook.Add("Void Eruption");
            Spellbook.Add("Dark Ascension");
            Spellbook.Add("Void Bolt");
            Spellbook.Add("Mind Sear");
            Spellbook.Add("Shadow Word: Death");
            Spellbook.Add("Surrender to Madness");
            Spellbook.Add("Dark Void");
            Spellbook.Add("Mindbender");
            Spellbook.Add("Shadowfiend");
            Spellbook.Add("Shadow Crash");
            Spellbook.Add("Mind Blast");
            Spellbook.Add("Void Torrent");
            Spellbook.Add("Shadow Word: Pain");
            Spellbook.Add("Vampiric Touch");
            Spellbook.Add("Mind Flay");
            Spellbook.Add("Shadowform");
            Spellbook.Add("Shadow Word: Void");

            Buffs.Add("Bloodlust");
            Buffs.Add("Heroism");
            Buffs.Add("Time Warp");
            Buffs.Add("Ancient Hysteria");
            Buffs.Add("Netherwinds");
            Buffs.Add("Drums of Rage");
            Buffs.Add("Chorus of Insanity");
            Buffs.Add("Lifeblood");
            Buffs.Add("Harvested Thoughts");
            Buffs.Add("Voidform");
            Buffs.Add("Shadowform");

            Debuffs.Add("Shadow Word: Pain");
            Debuffs.Add("Vampiric Touch");
            Debuffs.Add("Shiver Venom");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("crash cursor", "/cast [@cursor] Shadow Crash");

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
            float Haste = Aimsharp.Haste() / 100f;
            int SWPRemains = Aimsharp.DebuffRemaining("Shadow Word: Pain", "target") - GCD;
            int VTRemains = Aimsharp.DebuffRemaining("Vampiric Touch", "target") - GCD;
            bool Fighting = Aimsharp.Range("target") <= 45 && Aimsharp.TargetIsEnemy();
            bool UsePotion = Aimsharp.IsCustomCodeOn("Potions");
            string PotionType = GetDropDown("Potion Type");
            bool HasLust = Aimsharp.HasBuff("Bloodlust", "player", false) || Aimsharp.HasBuff("Heroism", "player", false) || Aimsharp.HasBuff("Time Warp", "player", false) || Aimsharp.HasBuff("Ancient Hysteria", "player", false) || Aimsharp.HasBuff("Netherwinds", "player", false) || Aimsharp.HasBuff("Drums of Rage", "player", false);
            int TargetTimeToDie = 1000000000;
            int TargetHealth = Aimsharp.Health("target");
            int EnemiesNearTarget = Aimsharp.EnemiesNearTarget();
            int EnemiesInMelee = Aimsharp.EnemiesInMelee();
            bool DotsUp = SWPRemains > 0 && VTRemains > 0;
            bool AOE = Aimsharp.IsCustomCodeOn("AOE");
            int Insanity = Aimsharp.Power("player");
            bool VoidformUp = Aimsharp.HasBuff("Voidform");
            bool NoCooldowns = Aimsharp.IsCustomCodeOn("SaveCooldowns");
            int VoidformStacks = Aimsharp.BuffStacks("Voidform");
            int GCDMAX = (int)((1500f / (Haste + 1f)) + GCD);
            float InsanityDrain = 6f + .68f * VoidformStacks;
            int MindBlastCastTime = (int)((1500f / (Haste + 1f)));
            int ChorusStacks = Aimsharp.BuffStacks("Chorus of Insanity");
            bool ChorusEnabled = GetCheckBox("Chorus of Insanity Trait?");
            bool IsMoving = Aimsharp.PlayerIsMoving();
            bool FontEquipped = Aimsharp.IsEquipped("Azshara's Font of Power");
            bool CanUseFont = Aimsharp.CanUseItem("Azshara's Font of Power");
            bool SearingDialogueEnabled = GetCheckBox("Searing Dialogue Trait?");
            bool IsChanneling = Aimsharp.IsChanneling("player");
            int PlayerCastingID = Aimsharp.CastingID("player");
            bool HasHarvestedThoughts = Aimsharp.HasBuff("Harvested Thoughts");
            int VoidBoltCD = Aimsharp.SpellCooldown("Void Bolt") - GCD;
            int SWDCharges = Aimsharp.SpellCharges("Shadow Word: Death");
            int SWDFullRecharge = (int)(Aimsharp.RechargeTime("Shadow Word: Death") - GCD + (9000f) * (1f - Aimsharp.SpellCharges("Shadow Word: Death")));
            bool SWVTalent = Aimsharp.Talent(1, 3);
            float SWVChargesFractional = Aimsharp.SpellCharges("Shadow Word: Void") + (Aimsharp.RechargeTime("Shadow Word: Void") - GCD) / (9000f);
            bool SWPRefreshable = SWPRemains < 4800;
            bool TalentMiseryEnabled = Aimsharp.Talent(3, 2);
            bool TalentDarkVoidEnabled = Aimsharp.Talent(3, 3);
            bool VTRefreshable = VTRemains < 6300;
            int FlameFullRecharge = (int)(Aimsharp.RechargeTime("Concentrated Flame") - GCD + (30000f) * (1f - Aimsharp.SpellCharges("Concentrated Flame")));
            bool LegacyEnabled = Aimsharp.Talent(7, 1);

            if (!AOE)
            {
                EnemiesNearTarget = 1;
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }

            if (IsChanneling && (PlayerCastingID == 48045 || PlayerCastingID == 296962 || PlayerCastingID == 263165)) // never interrupt mind sear or font or void torrent
                return false;

            if (UsePotion)
            {
                if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                {
                    if ((HasLust || TargetTimeToDie <= 80000 || TargetHealth < 35))
                    {
                        Aimsharp.Cast("potion", true);
                        return true;
                    }
                }
            }



            if (Aimsharp.CanCast("Void Eruption", "player") && !IsMoving && Fighting)
            {
                Aimsharp.Cast("Void Eruption");
                return true;
            }

            if (Aimsharp.CanCast("Dark Ascension", "player"))
            {
                if (!VoidformUp)
                {
                    Aimsharp.Cast("Dark Ascension");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Void Bolt"))
            {
                Aimsharp.Cast("Void Bolt");
                return true;
            }

            if (!NoCooldowns)
            {
                if (MajorPower == "Memory of Lucid Dreams")
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if ((VoidformStacks > 20 && Insanity <= 50) || (VoidformStacks > 26 + 7 * (HasLust ? 1 : 0)) || (InsanityDrain * ((GCDMAX / 1000f) * 2 + (MindBlastCastTime / 1000f))) > Insanity)
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }

                if (MajorPower == "Blood of the Enemy")
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }

                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player") && VoidformStacks > 15)
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Shadowfiend", "target"))
                {
                    if (VoidformStacks > 15)
                    {
                        Aimsharp.Cast("Shadowfiend");
                        return true;
                    }
                }

                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame") && (FlameFullRecharge < GCDMAX || TargetTimeToDie < 5000 || (ChorusStacks >= 15 && VoidformUp)))
                    {
                        Aimsharp.Cast("Concentrated Flame");
                        return true;
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

                if ((VoidformUp && ChorusStacks > 20) || !ChorusEnabled)
                {
                    if (FontEquipped)
                    {
                        if (CanUseFont && !IsMoving)
                        {
                            Aimsharp.Cast("Azshara's Font of Power");
                            return true;
                        }
                    }
                }

                if (Aimsharp.DebuffStacks("Shiver Venom") > 4)
                {
                    if (Aimsharp.IsEquipped("Shiver Venom Relic") && Aimsharp.CanUseItem("Shiver Venom Relic") && (TopTrinket == "Shiver Venom Relic" || BotTrinket == "Shiver Venom Relic"))
                    {
                            Aimsharp.Cast("Shiver Venom Relic", true);
                            return true;
                    }
                }

                if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic")
                {
                    if ((VoidformUp && ChorusStacks > 20) || !ChorusEnabled)
                    {
                        Aimsharp.Cast("TopTrink", true);
                        return true;
                    }
                }

                if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic")
                {
                    if ((VoidformUp && ChorusStacks > 20) || !ChorusEnabled)
                    {
                        Aimsharp.Cast("BotTrink", true);
                        return true;
                    }
                }
            }



            if (Aimsharp.CanCast("Mind Sear", "target") && !IsMoving)
            {
                if (HasHarvestedThoughts && VoidBoltCD >= 1500 && SearingDialogueEnabled)
                {
                    Aimsharp.Cast("Mind Sear");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Shadow Word: Death", "target") && TargetHealth <= 20)
            {
                if (TargetTimeToDie < 3000 || SWDCharges == 2 || SWDFullRecharge < GCDMAX)
                {
                    Aimsharp.Cast("Shadow Word: Death");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Surrender to Madness", "player"))
            {
                if (VoidformStacks > 10 + (10 * (HasLust ? 1 : 0)))
                {
                    Aimsharp.Cast("Surrender to Madness");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Dark Void", "target") && !IsMoving)
            {
                Aimsharp.Cast("Dark Void");
                return true;
            }

            if (Aimsharp.CanCast("Mindbender", "target"))
            {
                if (VoidformStacks > 18 || TargetTimeToDie < 18000)
                {
                    Aimsharp.Cast("Mindbender");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Shadow Word: Death", "target") && TargetHealth <= 20)
            {
                if (!VoidformUp || (SWDCharges == 2 && VoidformStacks < 15))
                {
                    Aimsharp.Cast("Shadow Word: Death");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Shadow Crash", "player"))
            {
                Aimsharp.Cast("crash cursor");
                return true;
            }

            if (Aimsharp.CanCast("Mind Blast", "target") && !IsMoving)
            {
                if (DotsUp && (!SWVTalent || !VoidformUp || (VoidformStacks > 14 && (Insanity < 70 || SWVChargesFractional > 1.33)) || (VoidformStacks <= 14 && (Insanity < 60 || SWVChargesFractional > 1.33))))
                {
                    Aimsharp.Cast("Mind Blast");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Void Torrent", "target") && !IsMoving)
            {
                if ((VoidformStacks > 15 && Insanity <= 50) || (VoidformStacks > 26 + 7 * (HasLust ? 1 : 0)) || (InsanityDrain * ((GCDMAX / 1000f) * 2 + (MindBlastCastTime / 1000f))) > Insanity)
                {
                    if (VoidformUp && (SWPRemains > 4000 && VTRemains > 4000))
                    {
                        Aimsharp.Cast("Void Torrent");
                        return true;
                    }
                }
            }

            if (Aimsharp.CanCast("Shadow Word: Pain", "target"))
            {
                if (SWPRefreshable && (TargetTimeToDie > 4000 && !TalentMiseryEnabled && !TalentDarkVoidEnabled))
                {
                    Aimsharp.Cast("Shadow Word: Pain");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Vampiric Touch", "target") && !IsMoving && PlayerCastingID != 34914) //make sure not already casting VT
            {
                if ((VTRefreshable && TargetTimeToDie > 6000) || (TalentMiseryEnabled && SWPRefreshable))
                {
                    Aimsharp.Cast("Vampiric Touch");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Mind Sear", "target") && !IsMoving && (AOE && EnemiesNearTarget > 1))
            {
                Aimsharp.Cast("Mind Sear");
                return true;
            }

            if (Aimsharp.CanCast("Mind Flay", "target") && !IsMoving && (!AOE || AOE && EnemiesNearTarget <= 1))
            {
                Aimsharp.Cast("Mind Flay");
                return true;
            }

            if (Aimsharp.CanCast("Shadow Word: Pain", "target"))
            {
                Aimsharp.Cast("Shadow Word: Pain");
                return true;
            }



            return false;
        }

        public override bool OutOfCombatTick()
        {
            if (!Aimsharp.HasBuff("Shadowform"))
            {
                if (Aimsharp.CanCast("Shadowform", "player"))
                {
                    Aimsharp.Cast("Shadowform");
                    return true;
                }
            }
            return false;
        }

    }
}
