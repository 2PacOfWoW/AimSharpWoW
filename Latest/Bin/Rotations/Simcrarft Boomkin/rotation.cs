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
    public class PerfectSimBoomkin : Rotation
    {


        public override void LoadSettings()
        {


            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Pocket-Sized Computation Device", "Shiver Venom Relic", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power","Potion of Focused Resolve", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "Bloodelf", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Streaking Stars Traits", 0, 3, 1));
            Settings.Add(new Setting("# Arcanic Pulsar Traits", 0, 3, 1));
            Settings.Add(new Setting("# Lively Spirit Traits", 0, 3, 1));



        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            // Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Perfect Simcraft Series: Boomkin - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 1333331", Color.Blue);
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

            Aimsharp.Latency = 50;
            Aimsharp.QuickDelay = 100;
            Aimsharp.SlowDelay = 300;

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

            Spellbook.Add("Moonkin Form");
            Spellbook.Add("Celestial Alignment");
            Spellbook.Add("Incarnation: Chosen of Elune");
            Spellbook.Add("Moonfire");
            Spellbook.Add("Sunfire");
            Spellbook.Add("Stellar Flare");
            Spellbook.Add("Thorns");
            Spellbook.Add("Warrior of Elune");
            Spellbook.Add("Innervate");
            Spellbook.Add("Force of Nature");
            Spellbook.Add("Fury of Elune");
            Spellbook.Add("Starfall");
            Spellbook.Add("Starsurge");
            Spellbook.Add("New Moon");
            Spellbook.Add("Half Moon");
            Spellbook.Add("Full Moon");
            Spellbook.Add("Lunar Strike");
            Spellbook.Add("Solar Wrath");

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

            Buffs.Add("Moonkin Form");
            Buffs.Add("Celestial Alignment");
            Buffs.Add("Incarnation: Chosen of Elune");
            Buffs.Add("Starlord");
            Buffs.Add("Warrior of Elune");
            Buffs.Add("Lively Spirit");
            Buffs.Add("Blessing of Elune");
            Buffs.Add("Arcanic Pulsar");
            Buffs.Add("Lunar Empowerment");
            Buffs.Add("Solar Empowerment");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");
            Debuffs.Add("Moonfire");
            Debuffs.Add("Sunfire");
            Debuffs.Add("Stellar Flare");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));
            Items.Add("Neural Synapse Enhancer");

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");
            Macros.Add("neural", "/use Neural Synapse Enhancer");

            Macros.Add("force cursor", "/cast [@cursor] Force of Nature");
            Macros.Add("cancel star", "/cancelaura Starlord");
            Macros.Add("starfall cursor", "/cast [@cursor] Starfall");
            //Macros.Add("inn player", "/cast [@player] Innervate"); //doesn't work can only hit healers
            Macros.Add("thorns player", "/cast [@player] Thorns");

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            // CustomCommands.Add("Prepull");
            // CustomCommands.Add("LightAOE");
        }





        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {

            bool Fighting = Aimsharp.Range("target") <= 40 && Aimsharp.TargetIsEnemy();
            bool Moving = Aimsharp.PlayerIsMoving();
            float Haste = Aimsharp.Haste() / 100f;
            float HasteFactor = 1 / (1 + Haste);
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
            int CDMemoryOfLucidDreamsRemains = Aimsharp.SpellCooldown("Memory of Lucid Dreams") - GCD;
            bool DebuffRazorCoralUp = Aimsharp.HasDebuff("Razor Coral");
            bool DebuffConductiveInkUp = Aimsharp.HasDebuff("Conductive Ink");
            int BuffRecklessForceRemains = Aimsharp.BuffRemaining("Reckless Force") - GCD;
            bool BuffRecklessForceUp = BuffRecklessForceRemains > 0;
            int BuffRecklessForceStacks = Aimsharp.BuffStacks("Reckless Force");

            bool CastingSW = Aimsharp.CastingID("player") == 190984 && Aimsharp.CastingRemaining("player") < 400;
            bool CastingLT = Aimsharp.CastingID("player") == 194153 && Aimsharp.CastingRemaining("player") < 400;
            bool CastingSF = Aimsharp.CastingID("player") == 202347 && Aimsharp.CastingRemaining("player") < 400;
            if (CastingSW)
                LastCast = "Solar Wrath";
            if (CastingLT)
                LastCast = "Lunar Strike";
            if (CastingSF)
                LastCast = "Stellar Flare";
            int AstralPower = Aimsharp.Power("player") + (CastingSW ? 8 : 0) + (CastingLT ? 12 : 0) + (CastingSF ? 8 : 0);
            int Runes = Aimsharp.PlayerSecondaryPower();
            int MaxAstralPower = Aimsharp.PlayerMaxPower();
            int AstralDefecit = MaxAstralPower - AstralPower;


            bool TalentStarlord = Aimsharp.Talent(5, 2);
            bool TalentTwinMoons = Aimsharp.Talent(6, 2);
            bool TalentStellarFlare = Aimsharp.Talent(6, 3);

            int BuffCelestialAlignmentRemains = Aimsharp.BuffRemaining("Celestial Alignment") - GCD;
            bool BuffCelestialAlignmentUp = BuffCelestialAlignmentRemains > 0;
            int BuffIncarnationRemains = Aimsharp.BuffRemaining("Incarnation: Chosen of Elune") - GCD;
            bool BuffIncarnationUp = BuffIncarnationRemains > 0;
            bool Buffca_incUp = BuffCelestialAlignmentUp || BuffIncarnationUp;
            int Buffca_incRemains = Math.Max(BuffIncarnationRemains, BuffCelestialAlignmentRemains);
            int DebuffMoonfireRemains = Aimsharp.DebuffRemaining("Moonfire") - GCD;
            bool DebuffMoonfireTicking = DebuffMoonfireRemains > 0;
            bool DebuffMoonfireRefreshable = DebuffMoonfireRemains < 6600;
            int DebuffSunfireRemains = Aimsharp.DebuffRemaining("Sunfire") - GCD;
            bool DebuffSunfireTicking = DebuffSunfireRemains > 0;
            bool DebuffSunfireRefreshable = DebuffSunfireRemains < 5400;
            int DebuffStellarFlareRemains = CastingSF ? 24000 : (Aimsharp.DebuffRemaining("Stellar Flare") - GCD);
            bool DebuffStellarFlareTicking = DebuffStellarFlareRemains > 0 || CastingSF;
            bool DebuffStellarFlareRefreshable = DebuffStellarFlareRemains < 7400 && !CastingSF;
            int BuffStarlordRemains = Aimsharp.BuffRemaining("Starlord") - GCD;
            bool BuffStarlordUp = BuffStarlordRemains > 0;
            int BuffStarlordStacks = Aimsharp.BuffStacks("Starlord");
            int BuffLivelySpiritRemains = Aimsharp.BuffRemaining("Lively Spirit") - GCD;
            bool BuffLivelySpiritUp = BuffLivelySpiritRemains > 0;
            bool BuffBlessingOfEluneUp = Aimsharp.HasBuff("Blessing of Eline");
            int BuffArcanicPulsarStacks = Aimsharp.BuffStacks("Arcanic Pulsar");
            int BuffArcanicPulsarRemains = Aimsharp.BuffRemaining("Arcanic Pulsar") - GCD;
            bool BuffArcanicPulsarUp = BuffArcanicPulsarRemains > 0;
            int BuffLunarEmpowermentStacks = Aimsharp.BuffStacks("Lunar Empowerment") - (CastingLT ? 1 : 0);
            int BuffLunarEmpowermentRemains = Aimsharp.BuffRemaining("Lunar Empowerment") - GCD;
            bool BuffLunarEmpowermentUp = BuffLunarEmpowermentRemains > 0 && BuffLunarEmpowermentStacks > 0;
            int BuffSolarEmpowermentStacks = Aimsharp.BuffStacks("Solar Empowerment") - (CastingSW ? 1 : 0);
            int BuffSolarEmpowermentRemains = Aimsharp.BuffRemaining("Solar Empowerment") - GCD;
            bool BuffSolarEmpowermentUp = BuffSolarEmpowermentRemains > 0 && BuffSolarEmpowermentStacks > 0;
            bool BuffWarriorOfEluneUp = Aimsharp.HasBuff("Warrior of Elune");

            int CDIncarnationRemains = Aimsharp.SpellCooldown("Incarnation: Chosen of Elune") - GCD;
            bool CDIncarnationReady = CDIncarnationRemains <= 10;
            int CDCelestialAlignmentRemains = Aimsharp.SpellCooldown("Celestial Alignment") - GCD;
            bool CDCelestialAlignmentReady = CDCelestialAlignmentRemains <= 10;
            int CDca_incRemains = Math.Max(CDIncarnationRemains, CDCelestialAlignmentRemains);
            bool CDca_incReady = CDca_incRemains <= 10;

            int VariableAZ_SS = GetSlider("# Streaking Stars Traits");
            int VariableAZ_AP = GetSlider("# Arcanic Pulsar Traits");
            int AzeriteLivelySpiritRank = GetSlider("# Lively Spirit Traits");

            int VariableSF_TARGETS = 4;
            VariableSF_TARGETS = VariableSF_TARGETS + (VariableAZ_AP > 0 ? 1 : 0);
            VariableSF_TARGETS = VariableSF_TARGETS + (TalentStarlord ? 1 : 0);
            VariableSF_TARGETS = VariableSF_TARGETS + (VariableAZ_SS > 2 && VariableAZ_AP > 0 ? 1 : 0);
            VariableSF_TARGETS = VariableSF_TARGETS + (!TalentTwinMoons ? 1 : 0);

            if (IsChanneling)
                return false;

            if (!Aimsharp.HasBuff("Moonkin Form") && Aimsharp.CanCast("Moonkin Form", "player"))
            {
                Aimsharp.Cast("Moonkin Form");
            }

            if (!NoCooldowns)
            {
                //actions=potion,if=buff.celestial_alignment.remains>13|buff.incarnation.remains>16.5
                if (UsePotion && Fighting)
                {
                    if (BuffCelestialAlignmentRemains > 13000 || BuffIncarnationRemains > 16500)
                    {
                        if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                        {
                            Aimsharp.Cast("potion", true);
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanUseItem("Neural Synapse Enhancer") && Fighting)
                {
                    Aimsharp.Cast("neural", true);
                    return true;
                }

                if (RacialPower == "Troll" && Fighting)
                {
                    if (Buffca_incUp)
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
                    if (Buffca_incUp)
                    {
                        if (Aimsharp.CanCast("Blood Fury", "player"))
                        {
                            Aimsharp.Cast("Blood Fury", true);
                            return true;
                        }
                    }
                }

                if (RacialPower == "Troll" && Fighting)
                {
                    if (Buffca_incUp)
                    {
                        if (Aimsharp.CanCast("Berserking", "player"))
                        {
                            Aimsharp.Cast("Berserking", true);
                            return true;
                        }
                    }
                }

                if (RacialPower == "Lightforged Draenei" && Fighting)
                {
                    if (Aimsharp.CanCast("Light's Judgment", "player"))
                    {
                        if (Buffca_incUp)
                        {
                            Aimsharp.Cast("Light's Judgment", true);
                            return true;
                        }
                    }
                }

                if (RacialPower == "Mag'har Orc" && Fighting)
                {
                    if (Buffca_incUp)
                    {
                        if (Aimsharp.CanCast("Ancestral Call", "player"))
                        {
                            Aimsharp.Cast("Ancestral Call", true);
                            return true;
                        }
                    }
                }

                if (RacialPower == "Dark Iron Dwarf" && Fighting)
                {
                    if (Buffca_incUp)
                    {
                        if (Aimsharp.CanCast("Fireblood", "player"))
                        {
                            Aimsharp.Cast("Fireblood", true);
                            return true;
                        }
                    }
                }

                if (Buffca_incUp && Fighting)
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

                //actions+=/use_item,name=azsharas_font_of_power,if=!buff.ca_inc.up,target_if=dot.moonfire.ticking&dot.sunfire.ticking&(!talent.stellar_flare.enabled|dot.stellar_flare.ticking)
                if (Aimsharp.CanUseItem("Azshara's Font of Power"))
                {
                    if (!Buffca_incUp && DebuffMoonfireTicking && DebuffSunfireTicking && (!TalentStellarFlare || DebuffStellarFlareTicking))
                    {
                        Aimsharp.Cast("Azshara's Font of Power", true);
                        return true;
                    }
                }

                //actions+=/guardian_of_azeroth,if=(!talent.starlord.enabled|buff.starlord.up)&!buff.ca_inc.up,target_if=dot.moonfire.ticking&dot.sunfire.ticking&(!talent.stellar_flare.enabled|dot.stellar_flare.ticking)
                if (MajorPower == "Guardian of Azeroth" && Fighting)
                {
                    if ((!TalentStarlord || BuffStarlordUp) && !Buffca_incUp && DebuffMoonfireTicking && DebuffSunfireTicking && (!TalentStellarFlare || DebuffStellarFlareTicking))
                        if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                        {
                            Aimsharp.Cast("Guardian of Azeroth");
                            return true;
                        }
                }

                //actions+=/use_item,effect_name=cyclotronic_blast,if=!buff.ca_inc.up,target_if=dot.moonfire.ticking&dot.sunfire.ticking&(!talent.stellar_flare.enabled|dot.stellar_flare.ticking)
                if (Aimsharp.CanUseItem("Pocket-Sized Computation Device"))
                {
                    if (!Buffca_incUp && DebuffMoonfireTicking && DebuffSunfireTicking && (!TalentStellarFlare || DebuffStellarFlareTicking))
                    {
                        Aimsharp.Cast("Pocket-Sized Computation Device", true);
                        return true;
                    }
                }

                //actions+=/use_item,name=shiver_venom_relic,if=!buff.ca_inc.up&!buff.bloodlust.up,target_if=dot.shiver_venom.stack>=5
                if (Aimsharp.CanUseItem("Shiver Venom Relic"))
                {
                    if (!Buffca_incUp && !HasLust && ShiverVenomStacks >= 5)
                    {
                        Aimsharp.Cast("Shiver Venom Relic", true);
                        return true;
                    }
                }

                //actions+=/blood_of_the_enemy,if=cooldown.ca_inc.remains>30
                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        if (CDca_incRemains > 30000)
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }
                }

                //actions+=/memory_of_lucid_dreams,if=!buff.ca_inc.up&(astral_power<25|cooldown.ca_inc.remains>30),target_if=dot.sunfire.remains>10&dot.moonfire.remains>10&(!talent.stellar_flare.enabled|dot.stellar_flare.remains>10)
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        if (!Buffca_incUp && (AstralPower < 25 || CDca_incRemains > 30000) && DebuffSunfireRemains > 10000 && DebuffMoonfireRemains > 10000 && (!TalentStellarFlare || DebuffStellarFlareRemains > 10000))
                        {
                            Aimsharp.Cast("Memory of Lucid Dreams");
                            return true;
                        }
                    }
                }

                //actions+=/concentrated_flame
                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                    {
                        Aimsharp.Cast("Concentrated Flame");
                        return true;
                    }
                }

                //actions+=/the_unbound_force,if=buff.reckless_force.up,target_if=dot.moonfire.ticking&dot.sunfire.ticking&(!talent.stellar_flare.enabled|dot.stellar_flare.ticking)
                if (MajorPower == "The Unbound Force")
                {
                    if (Aimsharp.CanCast("The Unbound Force"))
                    {
                        if (BuffRecklessForceUp && DebuffMoonfireTicking && DebuffSunfireTicking && (!TalentStellarFlare || DebuffStellarFlareTicking))
                        {
                            Aimsharp.Cast("The Unbound Force");
                            return true;
                        }
                    }
                }

                //actions+=/worldvein_resonance
                if (MajorPower == "Worldvein Resonance" && Fighting)
                {
                    if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                    {
                        Aimsharp.Cast("Worldvein Resonance");
                        return true;
                    }
                }

                //actions+=/focused_azerite_beam,if=(!variable.az_ss|!buff.ca_inc.up),target_if=dot.moonfire.ticking&dot.sunfire.ticking&(!talent.stellar_flare.enabled|dot.stellar_flare.ticking)
                if (MajorPower == "Focused Azerite Beam" && Range < 15)
                {
                    if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                    {
                        if ((VariableAZ_SS == 0 || !Buffca_incUp) && DebuffMoonfireTicking && DebuffSunfireTicking && (!TalentStellarFlare || DebuffStellarFlareTicking))
                        {
                            Aimsharp.Cast("Focused Azerite Beam");
                            return true;
                        }
                    }
                }

                //actions+=/thorns
                if (Aimsharp.CanCast("Thorns", "player") && Fighting)
                {
                    Aimsharp.Cast("thorns player");
                    return true;
                }

                //actions+=/use_items PLACEHOLDER

                //actions +=/ warrior_of_elune
                if (Aimsharp.CanCast("Warrior of Elune", "player") && Fighting)
                {
                    Aimsharp.Cast("Warrior of Elune");
                    return true;
                }

                //FIX THIS TO HIT HEALERS
                //actions+=/innervate,if=azerite.lively_spirit.enabled&(cooldown.incarnation.remains<2|cooldown.celestial_alignment.remains<12)
              /*  if (Aimsharp.CanCast("Innervate", "player") && Fighting)
                {
                    if (AzeriteLivelySpiritRank > 0 && (CDIncarnationRemains < 2000 || CDCelestialAlignmentRemains < 12000))
                    {
                        Aimsharp.Cast("inn player");
                        return true;
                    }
                } */

                //actions+=/force_of_nature,if=(variable.az_ss&!buff.ca_inc.up|!variable.az_ss&(buff.ca_inc.up|cooldown.ca_inc.remains>30))&ap_check
                if (Aimsharp.CanCast("Force of Nature", "player") && Fighting)
                {
                    if ((VariableAZ_SS > 0 && !Buffca_incUp || VariableAZ_SS == 0 && (Buffca_incUp || CDca_incRemains > 30000)) && AstralDefecit > 20)
                    {
                        Aimsharp.Cast("force cursor");
                        return true;
                    }
                }

                //actions+=/incarnation,if=!buff.ca_inc.up&(buff.memory_of_lucid_dreams.up|((cooldown.memory_of_lucid_dreams.remains>20|!essence.memory_of_lucid_dreams.major)&ap_check))&(buff.memory_of_lucid_dreams.up|ap_check),target_if=dot.sunfire.remains>8&dot.moonfire.remains>12&(dot.stellar_flare.remains>6|!talent.stellar_flare.enabled)
                if (Aimsharp.CanCast("Incarnation: Chosen of Elune", "player") && Fighting)
                {
                    if (!Buffca_incUp && (BuffMemoryOfLucidDreamsUp || ((CDMemoryOfLucidDreamsRemains > 20000 || MajorPower != "Memory of Lucid Dreams") && AstralDefecit >= 40)) && (BuffMemoryOfLucidDreamsUp || AstralDefecit > 40) && DebuffSunfireRemains > 8000 && DebuffMoonfireRemains > 12000 && (DebuffStellarFlareRemains > 6000 || !TalentStellarFlare))
                    {
                        Aimsharp.Cast("Incarnation: Chosen of Elune");
                        return true;
                    }
                }

                //actions+=/celestial_alignment,if=!buff.ca_inc.up&(!talent.starlord.enabled|buff.starlord.up)&(buff.memory_of_lucid_dreams.up|((cooldown.memory_of_lucid_dreams.remains>20|!essence.memory_of_lucid_dreams.major)&ap_check))&(!azerite.lively_spirit.enabled|buff.lively_spirit.up),target_if=(dot.sunfire.remains>2&dot.moonfire.ticking&(dot.stellar_flare.ticking|!talent.stellar_flare.enabled))
                if (Aimsharp.CanCast("Celestial Alignment", "player") && Fighting)
                {
                    if (!Buffca_incUp && (!TalentStarlord || BuffStarlordUp) && (BuffMemoryOfLucidDreamsUp || ((CDMemoryOfLucidDreamsRemains > 20000 || MajorPower != "Memory of Lucid Dreams") && AstralDefecit >= 40)) && (AzeriteLivelySpiritRank == 0 || BuffLivelySpiritUp) && (DebuffSunfireRemains > 2000 && DebuffMoonfireTicking && (DebuffStellarFlareTicking || !TalentStellarFlare)))
                    {
                        Aimsharp.Cast("Celestial Alignment");
                        return true;
                    }
                }

                //actions+=/fury_of_elune,if=(buff.ca_inc.up|cooldown.ca_inc.remains>30)&solar_wrath.ap_check
                if (Aimsharp.CanCast("Fury of Elune"))
                {
                    if ((Buffca_incUp || CDca_incRemains > 30000) && AstralDefecit >= 8 + (2 * (BuffBlessingOfEluneUp ? 1 : 0)))
                    {
                        Aimsharp.Cast("Fury of Elune");
                        return true;
                    }
                }
            }

            //actions+=/cancel_buff,name=starlord,if=buff.starlord.remains<3&!solar_wrath.ap_check
            if (BuffStarlordRemains < 3000 && BuffStarlordUp && AstralDefecit < 8 + (2 * (BuffBlessingOfEluneUp ? 1 : 0)))
            {
                Aimsharp.Cast("cancel star", true);
                return true;
            }

            //actions+=/starfall,if=(buff.starlord.stack<3|buff.starlord.remains>=8)&spell_targets>=variable.sf_targets&(target.time_to_die+1)*spell_targets>cost%2.5
            if (Aimsharp.CanCast("Starfall", "player"))
            {
                if ((BuffStarlordStacks < 3 || BuffStarlordRemains >= 8000) && (AOE && EnemiesNearTarget >= VariableSF_TARGETS && (TargetTimeToDie + 1000) * EnemiesNearTarget > 50 % 2.5))
                {
                    Aimsharp.Cast("starfall cursor");
                    return true;
                }
            }

            //actions+=/starsurge,if=(talent.starlord.enabled&(buff.starlord.stack<3|buff.starlord.remains>=5&buff.arcanic_pulsar.stack<8)|!talent.starlord.enabled&(buff.arcanic_pulsar.stack<8|buff.ca_inc.up))&spell_targets.starfall<variable.sf_targets&buff.lunar_empowerment.stack+buff.solar_empowerment.stack<4&buff.solar_empowerment.stack<3&buff.lunar_empowerment.stack<3&(!variable.az_ss|!buff.ca_inc.up|!prev.starsurge)|target.time_to_die<=execute_time*astral_power%40|!solar_wrath.ap_check
            if (Aimsharp.CanCast("Starsurge"))
            {
                if ((TalentStarlord && (BuffStarlordStacks < 3 || BuffStarlordRemains >= 5000 && BuffArcanicPulsarStacks < 8) || !TalentStarlord && (BuffArcanicPulsarStacks < 8 || Buffca_incUp)) && (!AOE || AOE && EnemiesNearTarget < VariableSF_TARGETS) && BuffLunarEmpowermentStacks + BuffSolarEmpowermentStacks < 4 && BuffSolarEmpowermentStacks < 3 && BuffLunarEmpowermentStacks < 3 && (VariableAZ_SS == 0 || !Buffca_incUp || LastCast != "Starsurge") || TargetTimeToDie <= Time * AstralPower % 40 || AstralDefecit < 8 + (2 * (BuffBlessingOfEluneUp ? 1 : 0)))
                {
                    Aimsharp.Cast("Starsurge");
                    return true;
                }
            }

            //actions+=/sunfire,if=buff.ca_inc.up&buff.ca_inc.remains<gcd.max&variable.az_ss&dot.moonfire.remains>remains
            if (Aimsharp.CanCast("Sunfire"))
            {
                if (Buffca_incUp && Buffca_incRemains < GCDMAX && VariableAZ_SS > 0 && DebuffMoonfireRemains > DebuffSunfireRemains)
                {
                    Aimsharp.Cast("Sunfire");
                    return true;
                }
            }

            //actions+=/moonfire,if=buff.ca_inc.up&buff.ca_inc.remains<gcd.max&variable.az_ss
            if (Aimsharp.CanCast("Moonfire"))
            {
                if (Buffca_incUp && Buffca_incRemains < GCDMAX && VariableAZ_SS > 0)
                {
                    Aimsharp.Cast("Moonfire");
                    return true;
                }
            }

            //not completely implemented in aoe situations
            //actions+=/sunfire,target_if=refreshable,if=ap_check&floor(target.time_to_die%(2*spell_haste))*spell_targets>=ceil(floor(2%spell_targets)*1.5)+2*spell_targets&(spell_targets>1+talent.twin_moons.enabled|dot.moonfire.ticking)&(!variable.az_ss|!buff.ca_inc.up|!prev.sunfire)&(buff.ca_inc.remains>remains|!buff.ca_inc.up)
            if (Aimsharp.CanCast("Sunfire"))
            {
                if (DebuffSunfireRefreshable && AstralDefecit >= 3 && (DebuffMoonfireTicking) && (VariableAZ_SS == 0 || !Buffca_incUp || LastCast != "Sunfire") && (Buffca_incRemains > DebuffSunfireRemains || !Buffca_incUp))
                {
                    Aimsharp.Cast("Sunfire");
                    return true;
                }
            }

            //not completely implemented in aoe situations
            //actions+=/moonfire,target_if=refreshable,if=ap_check&floor(target.time_to_die%(2*spell_haste))*spell_targets>=6&(!variable.az_ss|!buff.ca_inc.up|!prev.moonfire)&(buff.ca_inc.remains>remains|!buff.ca_inc.up)
            if (Aimsharp.CanCast("Moonfire"))
            {
                if (DebuffMoonfireRefreshable && AstralDefecit >= 3 && (VariableAZ_SS == 0 || !Buffca_incUp || LastCast != "Moonfire") && (Buffca_incRemains > DebuffMoonfireRemains || !Buffca_incUp))
                {
                    Aimsharp.Cast("Moonfire");
                    return true;
                }
            }

            //not completely implemented in aoe situations
            //actions+=/stellar_flare,target_if=refreshable,if=ap_check&floor(target.time_to_die%(2*spell_haste))>=5&(!variable.az_ss|!buff.ca_inc.up|!prev.stellar_flare)
            if (Aimsharp.CanCast("Stellar Flare") && !Moving)
            {
                if (DebuffStellarFlareRefreshable && AstralDefecit >= 8 && (VariableAZ_SS == 0 || !Buffca_incUp || (LastCast != "Stellar Flare" && !CastingSF)))
                {
                    Aimsharp.Cast("Stellar Flare");
                    return true;
                }
            }

            //actions+=/new_moon,if=ap_check
            if (Aimsharp.CanCast("New Moon") && !Moving)
            {
                if (AstralDefecit >= 10)
                {
                    Aimsharp.Cast("New Moon");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Half Moon") && !Moving)
            {
                if (AstralDefecit >= 20)
                {
                    Aimsharp.Cast("Half Moon");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Full Moon") && !Moving)
            {
                if (AstralDefecit >= 40)
                {
                    Aimsharp.Cast("Full Moon");
                    return true;
                }
            }

            //actions+=/lunar_strike,if=buff.solar_empowerment.stack<3&(ap_check|buff.lunar_empowerment.stack=3)&((buff.warrior_of_elune.up|buff.lunar_empowerment.up|spell_targets>=2&!buff.solar_empowerment.up)&(!variable.az_ss|!buff.ca_inc.up)|variable.az_ss&buff.ca_inc.up&prev.solar_wrath)
            if (Aimsharp.CanCast("Lunar Strike") && !Moving)
            {
                if (BuffSolarEmpowermentStacks < 3 && (AstralDefecit >= (BuffCelestialAlignmentUp ? 15 : 12) || BuffLunarEmpowermentStacks == 3) && ((BuffWarriorOfEluneUp || BuffLunarEmpowermentUp || AOE && !BuffSolarEmpowermentUp) && (VariableAZ_SS == 0 || !Buffca_incUp) || VariableAZ_SS > 0 && Buffca_incUp && (LastCast == "Solar Wrath" || CastingSW)))
                {
                    Aimsharp.Cast("Lunar Strike");
                    return true;
                }
            }

            //actions+=/solar_wrath,if=variable.az_ss<3|!buff.ca_inc.up|!prev.solar_wrath
            if (Aimsharp.CanCast("Solar Wrath") && !Moving)
            {
                if (VariableAZ_SS < 3 || !Buffca_incUp || (LastCast != "Solar Wrath" && !CastingSW))
                {
                    Aimsharp.Cast("Solar Wrath");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Sunfire"))
            {
                Aimsharp.Cast("Sunfire");
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
