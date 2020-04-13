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
    public class PerfSimEle : Rotation
    {


        public override void LoadSettings()
        {


            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Ashvane's Razor Coral", "Pocket-Sized Computation Device", "Galecaller's Boon", "Shiver Venom Relic", "Lurker's Insidious Gift", "Notorious Gladiator's Badge", "Sinister Gladiator's Badge", "Sinister Gladiator's Medallion", "Notorious Gladiator's Medallion", "Vial of Animated Blood", "First Mate's Spyglass", "Jes' Howler", "Ashvane's Razor Coral", "Forbidden Obsidian Claw", "Manifesto of Madness", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Igneous Potential Traits", 0, 3, 3));
            Settings.Add(new Setting("# Natural Harmony Traits", 0, 3, 3));
            Settings.Add(new Setting("# Lava Shock Traits", 0, 3, 0));
            Settings.Add(new Setting("# Tectonic Thunder Traits", 0, 3, 0));
            Settings.Add(new Setting("# Echo of the Elementals Traits", 0, 3, 0));



        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            //Aimsharp.DebugMode();
            Aimsharp.PrintMessage("Perfect Simcraft Series: Elemental Shaman - v 1.0", Color.Blue);
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
            Aimsharp.PrintMessage("/xxxxx LightAOE", Color.Blue);
            Aimsharp.PrintMessage("--Toggles light AOE mode on/off (3 targets).", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx Prepull 10", Color.Blue);
            Aimsharp.PrintMessage("--Starts the prepull actions.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("--Replace xxxxx with first 5 letters of your addon, lowercase.", Color.Blue);

            Aimsharp.Latency = 150;
            Aimsharp.QuickDelay = 100;
            Aimsharp.SlowDelay = 350;

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

            Spellbook.Add("Totem Mastery");
            Spellbook.Add("Fire Elemental");
            Spellbook.Add("Storm Elemental");
            Spellbook.Add("Earth Elemental");
            Spellbook.Add("Blood Fury");
            Spellbook.Add("Berserking");
            Spellbook.Add("Fireblood");
            Spellbook.Add("Ancestral Call");
            Spellbook.Add("Icefury");
            Spellbook.Add("Ascendance");
            Spellbook.Add("Stormkeeper");
            Spellbook.Add("Flame Shock");
            Spellbook.Add("Lava Burst");
            Spellbook.Add("Elemental Blast");
            Spellbook.Add("Liquid Magma Totem");
            Spellbook.Add("Lightning Bolt");
            Spellbook.Add("Earthquake");
            Spellbook.Add("Earth Shock");
            Spellbook.Add("Lightning Lasso");
            Spellbook.Add("Frost Shock");
            Spellbook.Add("Lava Beam");
            Spellbook.Add("Chain Lightning");
            Spellbook.Add("Call Lightning");
            Spellbook.Add("Eye of the Storm");

            Buffs.Add("Bloodlust");
            Buffs.Add("Heroism");
            Buffs.Add("Time Warp");
            Buffs.Add("Ancient Hysteria");
            Buffs.Add("Netherwinds");
            Buffs.Add("Drums of Rage");
            Buffs.Add("Resonance Totem");
            Buffs.Add("Stormkeeper");
            Buffs.Add("Lifeblood");
            Buffs.Add("Memory of Lucid Dreams");
            Buffs.Add("Icefury");
            Buffs.Add("Ascendance");
            Buffs.Add("Wind Gust");
            Buffs.Add("Lava Surge");
            Buffs.Add("Surge of Power");
            Buffs.Add("Master of the Elements");
            Buffs.Add("Lava Shock");
            Buffs.Add("Call Lightning");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Flame Shock");
            Debuffs.Add("Shiver Venom");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));
            Items.Add("Neural Synapse Enhancer");

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("eq cursor", "/cast [@cursor] Earthquake");
            Macros.Add("Magma Cursor", "/cast [@cursor] Liquid Magma Totem");
            Macros.Add("nse", "/use Neural Synapse Enhancer");
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            CustomCommands.Add("Prepull");
            CustomCommands.Add("LightAOE");
        }



        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {
            int ShiverVenomStacks = Aimsharp.DebuffStacks("Shiver Venom");
            bool Fighting = Aimsharp.Range("target") <= 45 && Aimsharp.TargetIsEnemy();
            bool Moving = Aimsharp.PlayerIsMoving();
            float Haste = Aimsharp.Haste() / 100f;
            int Time = Aimsharp.CombatTime();
            bool IsChanneling = Aimsharp.IsChanneling("player");
            string PotionType = GetDropDown("Potion Type");
            bool UsePotion = Aimsharp.IsCustomCodeOn("Potions");
            bool NoCooldowns = Aimsharp.IsCustomCodeOn("SaveCooldowns");
            bool AOE = Aimsharp.IsCustomCodeOn("AOE");
            bool LightAOE = Aimsharp.IsCustomCodeOn("LightAOE");
            int EnemiesInMelee = Aimsharp.EnemiesInMelee();
            int EnemiesNearTarget = Aimsharp.EnemiesNearTarget();
            int GCDMAX = (int)(1500f / (Haste + 1f));
            int GCD = Aimsharp.GCD();
            int Latency = Aimsharp.Latency;
            int TargetTimeToDie = 1000000000;
            bool HasLust = Aimsharp.HasBuff("Bloodlust", "player", false) || Aimsharp.HasBuff("Heroism", "player", false) || Aimsharp.HasBuff("Time Warp", "player", false) || Aimsharp.HasBuff("Ancient Hysteria", "player", false) || Aimsharp.HasBuff("Netherwinds", "player", false) || Aimsharp.HasBuff("Drums of Rage", "player", false);
            int FlameFullRecharge = (int)(Aimsharp.RechargeTime("Concentrated Flame") - GCD + (30000f) * (1f - Aimsharp.SpellCharges("Concentrated Flame")));

            int FireEleCD = Aimsharp.SpellCooldown("Fire Elemental") - GCD;
            int StormEleCD = Aimsharp.SpellCooldown("Storm Elemental") - GCD;
            bool TotemMasteryUp = Aimsharp.HasBuff("Resonance Totem");
            bool CastingLB = Aimsharp.CastingID("player") == 51505 && Aimsharp.CastingRemaining("player") < 500;
            bool CastingIF = Aimsharp.CastingID("player") == 210714 && Aimsharp.CastingRemaining("player") < 500;
            bool CastingLtB = Aimsharp.CastingID("player") == 188196 && Aimsharp.CastingRemaining("player") < 500;
            bool StormElementalEnabled = Aimsharp.Talent(4, 2);
            bool PrimalElementalistEnabled = Aimsharp.Talent(6, 2);
            int Maelstrom = Aimsharp.Power("player") + (CastingLB ? 12 : 0) + (CastingIF ? 30 : 0) + (CastingLtB ? 10 : 0);
            bool IceFuryEnabled = Aimsharp.Talent(6, 3);
            int IceFuryCD = Aimsharp.SpellCooldown("Icefury") - GCD;
            int IceFuryBuffRemains = Aimsharp.BuffRemaining("Icefury") - GCD;
            bool BuffIceFuryUp = IceFuryBuffRemains > 0;
            bool CDIceFuryUp = IceFuryCD < 20;
            int BuffIceFuryStacks = Aimsharp.BuffStacks("Icefury");
            int GuardianCD = Aimsharp.SpellCooldown("Guardian of Azeroth") - GCD;
            bool TalentUnlimitedPower = Aimsharp.Talent(7, 1);
            bool TalentAscendance = Aimsharp.Talent(7, 3);
            bool TalentElementalBlast = Aimsharp.Talent(1, 3);
            bool TalentMasterOfTheElements = Aimsharp.Talent(4, 1);
            bool TalentSurgeOfPower = Aimsharp.Talent(6, 1);
            bool TalentLiquidMagmaTotem = Aimsharp.Talent(4, 3);
            bool TalentCallTheThunder = Aimsharp.Talent(2, 2);
            bool TalentEchoOfTheElements = Aimsharp.Talent(1, 2);
            bool TalentPrimalElementalist = Aimsharp.Talent(6, 2);
            int BuffStormkeeperRemains = Aimsharp.BuffRemaining("Stormkeeper") - GCD;
            int BuffStormkeeperStacks = Aimsharp.BuffStacks("Stormkeeper");
            bool BuffStormkeeperUp = BuffStormkeeperRemains > 0;
            int CDAscendanceRemains = Aimsharp.SpellCooldown("Ascendance") - GCD;
            bool CDAscendanceUp = CDAscendanceRemains < 20;
            int CDStormkeeperRemains = Aimsharp.SpellCooldown("Stormkeeper") - GCD;
            bool CDStormkeeperUp = CDStormkeeperRemains < 20;
            bool TalentStormkeeper = Aimsharp.Talent(7, 2);
            int BuffAscendanceRemains = Aimsharp.BuffRemaining("Ascendance") - GCD;
            bool BuffAscendanceUp = BuffAscendanceRemains > 0;
            int FlameShockRemains = Aimsharp.DebuffRemaining("Flame Shock") - GCD;
            bool FlameShockTicking = FlameShockRemains > 0;
            bool FlameShockRefreshable = FlameShockRemains < 7200;
            int WindGustStacks = Aimsharp.BuffStacks("Wind Gust");
            int AzeriteIgneousPotentialRank = GetSlider("# Igneous Potential Traits");
            int AzeriteNaturalHarmonyRank = GetSlider("# Natural Harmony Traits");
            int AzeriteLavaShockRank = GetSlider("# Lava Shock Traits");
            int AzeriteTectonicThunderRank = GetSlider("# Tectonic Thunder Traits");
            int AzeriteEchoOfTheElementalsRank = GetSlider("# Echo of the Elementals Traits");
            int BuffLavaSurgeRemains = Aimsharp.BuffRemaining("Lava Surge") - GCD;
            bool BuffLavaSurgeUp = BuffLavaSurgeRemains > 0;
            int BuffSurgeOfPowerRemains = Aimsharp.BuffRemaining("Surge of Power") - GCD;
            bool BuffSurgeOfPowerUp = BuffSurgeOfPowerRemains > 0 && !CastingLB && !CastingLtB;
            int BuffMasterOfTheElementsRemains = Aimsharp.BuffRemaining("Master of the Elements") - GCD;
            bool BuffMasterOfTheElementsUp = (CastingLB ? true : BuffMasterOfTheElementsRemains > 0) && !CastingIF && !CastingLtB;
            int CDLavaBurstRemains = Aimsharp.SpellCooldown("Lava Burst") - GCD;
            int BuffLavaShockStacks = Aimsharp.BuffStacks("Lava Shock");
            int LavaBurstCharges = Aimsharp.SpellCharges("Lava Burst") - (CastingLB ? 1 : 0);
            bool PetCastingCL = Aimsharp.CastingID("pet") == 157348;
            bool HasPet = Aimsharp.PlayerHasPet();
            bool PetBuffed = Aimsharp.HasBuff("Call Lightning", "pet", false);


            if (!AOE)
            {
                EnemiesNearTarget = 1;
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }


            if (IsChanneling || Aimsharp.CastingID("player") == 299338)
                return false;

            if (UsePotion && Fighting)
            {
                if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                {
                    if (MajorPower == "Guardian of Azeroth")
                    {
                        if (GuardianCD < 30000 || TargetTimeToDie < 60000)
                        {
                            Aimsharp.Cast("potion", true);
                            return true;
                        }
                    }
                    else if (FireEleCD < 30000 || StormEleCD < 30000 || TargetTimeToDie < 60000)
                    {
                        Aimsharp.Cast("potion", true);
                        return true;
                    }
                }
            }

            if (Aimsharp.CanCast("Totem Mastery", "player") && Fighting)
            {
                if (!TotemMasteryUp || Aimsharp.TotemTimer() < 2000)
                {
                    Aimsharp.Cast("Totem Mastery");
                    return true;
                }
            }

            if (Aimsharp.CanUseItem("Shiver Venom Relic") && Fighting)
            {
                if (ShiverVenomStacks == 5)
                {
                    Aimsharp.Cast("Shiver Venom Relic", true);
                    return true;
                }
            }

            if (MajorPower == "Guardian of Azeroth" && !NoCooldowns && Fighting)
            {
                if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                {
                    if (FlameShockTicking && (!StormElementalEnabled && (120000 < FireEleCD || TargetTimeToDie > 190000 || TargetTimeToDie < 32000 || !(FireEleCD + 30000 < TargetTimeToDie) || FireEleCD < 2000) || StormElementalEnabled && (90000 < StormEleCD || TargetTimeToDie > 190000 || TargetTimeToDie < 35000 || !(StormEleCD + 30000 < TargetTimeToDie) || StormEleCD < 2000)))
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }
            }

            if (Aimsharp.CanCast("Fire Elemental", "player") && !NoCooldowns && Fighting)
            {
                if (!StormElementalEnabled && (MajorPower != "Guardian of Azeroth" || GuardianCD > 150000 || TargetTimeToDie < 30000 || TargetTimeToDie > 155000 || !(GuardianCD + 30000 < TargetTimeToDie)))
                {
                    Aimsharp.Cast("Fire Elemental");
                    return true;
                }
            }

            if (MajorPower == "Focused Azerite Beam" && !NoCooldowns && Fighting)
            {
                if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                {
                    Aimsharp.Cast("Focused Azerite Beam");
                    return true;
                }
            }

            if (MajorPower == "The Unbound Force" && !NoCooldowns && Fighting)
            {
                if (Aimsharp.CanCast("The Unbound Force"))
                {
                    Aimsharp.Cast("The Unbound Force");
                    return true;
                }
            }

            if (MajorPower == "Memory of Lucid Dreams" && !NoCooldowns && Fighting)
            {
                if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                {
                    Aimsharp.Cast("Memory of Lucid Dreams");
                    return true;
                }
            }

            if (MajorPower == "Worldvein Resonance" && !NoCooldowns)
            {
                if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                {
                    if ((TalentUnlimitedPower || BuffStormkeeperUp || TalentAscendance && ((StormElementalEnabled && StormEleCD < (12000) && StormEleCD > 15000 || !StormElementalEnabled) && (!IceFuryEnabled || !BuffIceFuryUp && !CDIceFuryUp)) || !CDAscendanceUp))
                    {
                        Aimsharp.Cast("Worldvein Resonance");
                        return true;
                    }
                }
            }

            if (MajorPower == "Blood of the Enemy" && !NoCooldowns && ((AOE && EnemiesInMelee > 2) || (!AOE && Aimsharp.Range("target") <= 10)))
            {
                if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                {
                    if (StormElementalEnabled && StormEleCD > 90000)
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }
            }



            if (Aimsharp.CanCast("Storm Elemental", "player") && Fighting && !NoCooldowns && ((EnemiesNearTarget > 3) || !AOE))
            {
                if (StormElementalEnabled && (!CDStormkeeperUp || !TalentStormkeeper) && (!IceFuryEnabled || !BuffIceFuryUp && !CDIceFuryUp) && (!TalentAscendance || !BuffAscendanceUp || TargetTimeToDie < 32000) && (MajorPower != "Guardian of Azeroth" || GuardianCD > 150000 || TargetTimeToDie < 30000 || TargetTimeToDie > 155000 || !(GuardianCD + 30000 < TargetTimeToDie)))
                {
                    Aimsharp.Cast("Storm Elemental");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Call Lightning","target",false) && Fighting && !PetCastingCL && HasPet && (EnemiesNearTarget > 2 || !AOE) && StormElementalEnabled)
            {
                Aimsharp.Cast("Call Lightning");
                return true;
            }

            if (Aimsharp.CanCast("Eye of the Storm","target",false) && Fighting && PetBuffed && HasPet && (EnemiesNearTarget > 2 || !AOE) && StormElementalEnabled)
            {
                Aimsharp.Cast("Eye of the Storm");
                return true;
            }


            if (RacialPower == "Orc" && Fighting)
            {
                if (Aimsharp.CanCast("Blood Fury", "player"))
                {
                    if (!TalentAscendance || BuffAscendanceUp || CDAscendanceRemains > 50000)
                    {
                        Aimsharp.Cast("Blood Fury", true);
                        return true;
                    }
                }
            }

            if (RacialPower == "Troll" && Fighting)
            {
                if (Aimsharp.CanCast("Berserking", "player"))
                {
                    if (!TalentAscendance || BuffAscendanceUp)
                    {
                        Aimsharp.Cast("Berserking", true);
                        return true;
                    }
                }
            }

            if (RacialPower == "Dark Iron Dwarf" && Fighting)
            {
                if (Aimsharp.CanCast("Fireblood", "player"))
                {
                    if (!TalentAscendance || BuffAscendanceUp || CDAscendanceRemains > 50000)
                    {
                        Aimsharp.Cast("Fireblood", true);
                        return true;
                    }
                }
            }

            if (RacialPower == "Mag'har Orc" && Fighting)
            {
                if (Aimsharp.CanCast("Ancestral Call", "player"))
                {
                    if (!TalentAscendance || BuffAscendanceUp || CDAscendanceRemains > 50000)
                    {
                        Aimsharp.Cast("Ancestral Call", true);
                        return true;
                    }
                }
            }

            if (Aimsharp.CanUseItem("Neural Synapse Enhancer") && Fighting)
            {
                Aimsharp.Cast("nse", true);
                return true;
            }

            if (Aimsharp.CanUseItem("Forbidden Obsidian Claw") && Fighting)
            {
                Aimsharp.Cast("Forbidden Obsidian Claw", true);
                return true;
            }

            if (Aimsharp.CanUseItem("Manifesto of Madness") && Fighting)
            {
                Aimsharp.Cast("Manifesto of Madness", true);
                return true;
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

            if (EnemiesNearTarget <= 1)
            {

                if (Aimsharp.CanCast("Flame Shock"))
                {
                    if ((!FlameShockTicking || FlameShockRemains <= GCDMAX || TalentAscendance && FlameShockRemains < (CDAscendanceRemains + 15000) && CDAscendanceRemains < 4000 && (!StormElementalEnabled || StormElementalEnabled && StormEleCD < 90000)) && (WindGustStacks < 14 || AzeriteIgneousPotentialRank >= 2 || BuffLavaSurgeUp || !HasLust) && !BuffSurgeOfPowerUp)
                    {
                        Aimsharp.Cast("Flame Shock");
                        return true;
                    }
                }

                if (MajorPower == "Blood of the Enemy" && !NoCooldowns && ((AOE && EnemiesInMelee > 2) || (!AOE && Aimsharp.Range("target") <= 10)))
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        if (!TalentAscendance && !StormElementalEnabled || TalentAscendance && (Time >= 60000 || HasLust) && CDLavaBurstRemains > 0 && (StormEleCD < (90000) || !StormElementalEnabled) && (!IceFuryEnabled || !BuffIceFuryUp && CDIceFuryUp))
                        {
                            Aimsharp.Cast("Blood of the Enemy");
                            return true;
                        }
                    }
                }

                if (Aimsharp.CanCast("Ascendance", "player") && Fighting)
                {
                    if (TalentAscendance && (Time >= 60000 || HasLust) && CDLavaBurstRemains > 0 && (StormEleCD < (90000) || !StormElementalEnabled) && (!IceFuryEnabled || !BuffIceFuryUp && !CDIceFuryUp))
                    {
                        Aimsharp.Cast("Ascendance");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Elemental Blast") && Fighting && !Moving)
                {
                    if (TalentElementalBlast && (TalentMasterOfTheElements && BuffMasterOfTheElementsUp && Maelstrom < 60 || !TalentMasterOfTheElements) && (!(StormEleCD > 90000 && StormElementalEnabled) || AzeriteNaturalHarmonyRank == 3 && WindGustStacks < 14))
                    {
                        Aimsharp.Cast("Elemental Blast");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Stormkeeper", "player") && Fighting && !Moving)
                {
                    if (TalentStormkeeper && (!TalentSurgeOfPower || BuffSurgeOfPowerUp || Maelstrom >= 44))
                    {
                        Aimsharp.Cast("Stormkeeper");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Liquid Magma Totem", "player") && Fighting)
                {
                    if (TalentLiquidMagmaTotem)
                    {
                        Aimsharp.Cast("Magma Cursor");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lightning Bolt") && (!Moving || BuffStormkeeperUp))
                {
                    if (BuffStormkeeperUp && (AzeriteLavaShockRank * BuffLavaShockStacks) < 26 && (BuffMasterOfTheElementsUp && !TalentSurgeOfPower || BuffSurgeOfPowerUp))
                    {
                        Aimsharp.Cast("Lightning Bolt");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Earthquake", "player") && Fighting)
                {
                    if ((AzeriteTectonicThunderRank >= 3 && !TalentSurgeOfPower && AzeriteLavaShockRank < 1) && AzeriteLavaShockRank * BuffLavaShockStacks < (36 + 3 * AzeriteTectonicThunderRank) && (!TalentSurgeOfPower || !FlameShockRefreshable || StormEleCD > (120000)) && (!TalentMasterOfTheElements || BuffMasterOfTheElementsUp || CDLavaBurstRemains > 0 && Maelstrom >= 92 + 30 * (TalentCallTheThunder ? 1 : 0)))
                    {
                        Aimsharp.Cast("eq cursor");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Earth Shock"))
                {
                    if (!BuffSurgeOfPowerUp && TalentMasterOfTheElements && (BuffMasterOfTheElementsUp || CDLavaBurstRemains > 0 && Maelstrom >= 90 + 30 * (TalentCallTheThunder ? 1 : 0) || (AzeriteLavaShockRank * BuffLavaShockStacks < 26) && BuffStormkeeperUp && CDLavaBurstRemains <= GCDMAX))
                    {
                        Aimsharp.Cast("Earth Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Earth Shock"))
                {
                    if (!TalentMasterOfTheElements && !(AzeriteIgneousPotentialRank > 2 && BuffAscendanceUp) && (BuffStormkeeperUp || Maelstrom >= 88 + 30 * (TalentCallTheThunder ? 1 : 0) || !(StormEleCD > 90000 && StormElementalEnabled && TalentSurgeOfPower)))
                    {
                        Aimsharp.Cast("Earth Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Earth Shock"))
                {
                    if (TalentSurgeOfPower && !BuffSurgeOfPowerUp && CDLavaBurstRemains <= GCDMAX && (!StormElementalEnabled && (Math.Floor((TargetTimeToDie - FireEleCD) % 150000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0)) || Math.Floor((1.16 * TargetTimeToDie - FireEleCD) % 150000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0))) || StormElementalEnabled && !(StormEleCD > 90000) && (Math.Floor((TargetTimeToDie - StormEleCD) % 120000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0)) || Math.Floor((1.16 * TargetTimeToDie - StormEleCD) % 120000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0)))))
                    {
                        Aimsharp.Cast("Earth Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lightning Lasso") && Aimsharp.Range("target") <= 20)
                {
                    Aimsharp.Cast("Lightning Lasso");
                    return true;
                }

                if (Aimsharp.CanCast("Lightning Bolt") && (!Moving || BuffStormkeeperUp))
                {
                    if (StormEleCD > 90000 && StormElementalEnabled && (AzeriteIgneousPotentialRank < 2 || !BuffLavaSurgeUp && HasLust))
                    {
                        Aimsharp.Cast("Lightning Bolt");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lightning Bolt") && (!Moving || BuffStormkeeperUp))
                {
                    if ((BuffStormkeeperUp && BuffStormkeeperRemains < 1.1 * GCDMAX * BuffStormkeeperStacks || BuffStormkeeperUp && BuffMasterOfTheElementsUp))
                    {
                        Aimsharp.Cast("Lightning Bolt");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Frost Shock"))
                {
                    if (IceFuryEnabled && TalentMasterOfTheElements && BuffIceFuryUp && BuffMasterOfTheElementsUp)
                    {
                        Aimsharp.Cast("Frost Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Burst") && (!Moving || BuffLavaSurgeUp))
                {
                    if (BuffAscendanceUp)
                    {
                        Aimsharp.Cast("Lava Burst");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Burst") && (!Moving || BuffLavaSurgeUp))
                {
                    if (StormElementalEnabled && BuffSurgeOfPowerUp && (Math.Floor((TargetTimeToDie - StormEleCD) % 120000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0)) || Math.Floor((1.16 * TargetTimeToDie - StormEleCD) % 120000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0))))
                    {
                        Aimsharp.Cast("Lava Burst");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Burst") && (!Moving || BuffLavaSurgeUp))
                {
                    if (!StormElementalEnabled && BuffSurgeOfPowerUp && (Math.Floor((TargetTimeToDie - FireEleCD) % 150000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0)) || Math.Floor((1.16 * TargetTimeToDie - FireEleCD) % 150000f) < 30 * (1 + (AzeriteEchoOfTheElementalsRank >= 2 ? 1 : 0))))
                    {
                        Aimsharp.Cast("Lava Burst");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lightning Bolt") && (!Moving || BuffStormkeeperUp))
                {
                    if (BuffSurgeOfPowerUp)
                    {
                        Aimsharp.Cast("Lightning Bolt");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Burst") && (!Moving || BuffLavaSurgeUp))
                {
                    if (!TalentMasterOfTheElements)
                    {
                        Aimsharp.Cast("Lava Burst");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Icefury") && !Moving)
                {
                    if (IceFuryEnabled && !(Maelstrom > 75 && CDLavaBurstRemains <= 20) && (!StormElementalEnabled || StormEleCD < 90000))
                    {
                        Aimsharp.Cast("Icefury");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Burst") && (!Moving || BuffLavaSurgeUp))
                {
                    if (LavaBurstCharges == (TalentEchoOfTheElements ? 2 : 1))
                    {
                        Aimsharp.Cast("Lava Burst");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Frost Shock"))
                {
                    if (IceFuryEnabled && BuffIceFuryUp && (double)IceFuryBuffRemains < (double)1.3 * (double)GCDMAX * (double)BuffIceFuryStacks)
                    {
                        Aimsharp.Cast("Frost Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Burst") && (!Moving || BuffLavaSurgeUp))
                {
                    Aimsharp.Cast("Lava Burst");
                    return true;
                }

                if (MajorPower == "Concentrated Flame")
                {
                    if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                    {
                        Aimsharp.Cast("Concentrated Flame");
                        return true;
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

                if (Aimsharp.CanCast("Flame Shock"))
                {
                    if (FlameShockRefreshable && !BuffSurgeOfPowerUp)
                    {
                        Aimsharp.Cast("Flame Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Frost Shock"))
                {
                    if (IceFuryEnabled && BuffIceFuryUp && (IceFuryBuffRemains < GCDMAX * 4 * BuffIceFuryStacks || BuffStormkeeperUp || !TalentMasterOfTheElements))
                    {
                        Aimsharp.Cast("Frost Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lightning Bolt") && !Moving)
                {
                    Aimsharp.Cast("Lightning Bolt");
                    return true;
                }

                if (Aimsharp.CanCast("Frost Shock"))
                {
                    Aimsharp.Cast("Frost Shock");
                    return true;
                }

            }

            if (EnemiesNearTarget > 3)
            {

                if (Aimsharp.CanCast("Stormkeeper", "player") && Fighting && !Moving)
                {
                    Aimsharp.Cast("Stormkeeper");
                    return true;
                }

                if (Aimsharp.CanCast("Flame Shock"))
                {
                    if (FlameShockRefreshable && (!StormElementalEnabled && (FireEleCD > (120000 + 14000 * (1 / (Haste + 1))) || FireEleCD < (24000 - 14 * (1 / (Haste + 1))))) && (!StormElementalEnabled || StormEleCD < (90000) || WindGustStacks < 14))
                    {
                        Aimsharp.Cast("Flame Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Ascendance", "player") && Fighting)
                {
                    if (TalentAscendance && (StormElementalEnabled && StormEleCD < (90000) && StormEleCD > 15000 || !StormElementalEnabled) && (!IceFuryEnabled || !BuffIceFuryUp && IceFuryCD > 20))
                    {
                        Aimsharp.Cast("Ascendance");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Liquid Magma Totem", "player") && Fighting)
                {
                    if (TalentLiquidMagmaTotem)
                    {
                        Aimsharp.Cast("Magma Cursor");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Earthquake", "player") && Fighting)
                {
                    if (!TalentMasterOfTheElements || BuffStormkeeperUp || Maelstrom >= (100 - 20) || BuffMasterOfTheElementsUp || true)
                    {
                        Aimsharp.Cast("eq cursor");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Blood of the Enemy", "player") && ((AOE && EnemiesInMelee > 2) || (!AOE && Aimsharp.Range("target") <= 10)))
                {
                    if (!TalentPrimalElementalist || !StormElementalEnabled)
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Chain Lightning") && !Moving)
                {
                    if (BuffStormkeeperUp && BuffStormkeeperRemains < 3 * GCDMAX * BuffStormkeeperStacks)
                    {
                        Aimsharp.Cast("Chain Lightning");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Beam") && !Moving)
                {
                    if (TalentAscendance && BuffAscendanceUp)
                    {
                        Aimsharp.Cast("Lava Beam");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Chain Lightning") && !Moving)
                {
                    Aimsharp.Cast("Chain Lightning");
                    return true;
                }

                if (Aimsharp.CanCast("Lava Burst") && TalentAscendance && (!Moving || BuffLavaSurgeUp))
                {
                    Aimsharp.Cast("Lava Burst");
                    return true;
                }

                if (Aimsharp.CanCast("Flame Shock") && FlameShockRefreshable)
                {
                    Aimsharp.Cast("Flame Shock");
                    return true;
                }

                if (Aimsharp.CanCast("Frost Shock"))
                {
                    Aimsharp.Cast("Frost Shock");
                    return true;
                }

            }

            if (EnemiesNearTarget > 1 && EnemiesNearTarget <= 3)
            {
                if (Aimsharp.CanCast("Stormkeeper", "player") && Fighting && !Moving)
                {
                    Aimsharp.Cast("Stormkeeper");
                    return true;
                }

                if (Aimsharp.CanCast("Flame Shock"))
                {
                    if (FlameShockRefreshable && (true || !StormElementalEnabled && (FireEleCD > (120000 + 14000 * (1 / (Haste + 1))) || FireEleCD < (24000 - 14000 * (1 / (Haste + 1))))) && (!StormElementalEnabled || StormEleCD < (120000) || WindGustStacks < 14))
                    {
                        Aimsharp.Cast("Flame Shock");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Ascendance", "player") && Fighting)
                {
                    if (TalentAscendance && (StormElementalEnabled && StormEleCD < (90000) && StormEleCD > 15000 || !StormElementalEnabled) && (!IceFuryEnabled || !BuffIceFuryUp && IceFuryCD > 20))
                    {
                        Aimsharp.Cast("Ascendance");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Liquid Magma Totem", "player") && Fighting)
                {
                    if (TalentLiquidMagmaTotem)
                    {
                        Aimsharp.Cast("Magma Cursor");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Earthquake", "player") && Fighting)
                {
                    if (!TalentMasterOfTheElements || BuffStormkeeperUp || Maelstrom >= (100 - 12) || BuffMasterOfTheElementsUp)
                    {
                        Aimsharp.Cast("eq cursor");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Blood of the Enemy", "player") && ((AOE && EnemiesInMelee > 2) || (!AOE && Aimsharp.Range("target") <= 10)))
                {
                    if (!TalentPrimalElementalist || !StormElementalEnabled)
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Chain Lightning") && !Moving)
                {
                    if (BuffStormkeeperUp && BuffStormkeeperRemains < 3 * GCDMAX * BuffStormkeeperStacks)
                    {
                        Aimsharp.Cast("Chain Lightning");
                        return true;
                    }
                }

                //actions.aoe+=/lava_burst,if=buff.lava_surge.up&spell_targets.chain_lightning<4&(!talent.storm_elemental.enabled|cooldown.storm_elemental.remains<(cooldown.storm_elemental.duration-30))&dot.flame_shock.ticking
                if (Aimsharp.CanCast("Lava Burst") && (!Moving || BuffLavaSurgeUp))
                {
                    if (BuffLavaSurgeUp && (!StormElementalEnabled || StormEleCD < (90000)) && FlameShockTicking)
                    {
                        Aimsharp.Cast("Lava Burst");
                        return true;
                    }
                }

                //actions.aoe+=/icefury,if=spell_targets.chain_lightning<4&!buff.ascendance.up
                if (Aimsharp.CanCast("Icefury") && !Moving)
                {
                    if (!BuffAscendanceUp)
                    {
                        Aimsharp.Cast("Icefury");
                        return true;
                    }
                }

                //actions.aoe+=/frost_shock,if=spell_targets.chain_lightning<4&buff.icefury.up&!buff.ascendance.up
                if (Aimsharp.CanCast("Frost Shock"))
                {
                    if (BuffIceFuryUp && !BuffAscendanceUp)
                    {
                        Aimsharp.Cast("Frost Shock");
                        return true;
                    }
                }
                //actions.aoe+=/elemental_blast,if=talent.elemental_blast.enabled&spell_targets.chain_lightning<4&(!talent.storm_elemental.enabled|cooldown.storm_elemental.remains<(cooldown.storm_elemental.duration-30))
                if (Aimsharp.CanCast("Elemental Blast") && !Moving)
                {
                    if (TalentElementalBlast && (!StormElementalEnabled || StormEleCD < (90000)))
                    {
                        Aimsharp.Cast("Elemental Blast");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Beam") && !Moving)
                {
                    if (TalentAscendance && BuffAscendanceUp)
                    {
                        Aimsharp.Cast("Lava Beam");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Chain Lightning") && !Moving)
                {
                    Aimsharp.Cast("Chain Lightning");
                    return true;
                }

                if (Aimsharp.CanCast("Lava Burst") && TalentAscendance && !Moving)
                {
                    Aimsharp.Cast("Lava Burst");
                    return true;
                }

                if (Aimsharp.CanCast("Flame Shock") && FlameShockRefreshable)
                {
                    Aimsharp.Cast("Flame Shock");
                    return true;
                }

                if (Aimsharp.CanCast("Frost Shock"))
                {
                    Aimsharp.Cast("Frost Shock");
                    return true;
                }

            }




            return false;
        }


        public override bool OutOfCombatTick()
        {

            bool Prepull = Aimsharp.IsCustomCodeOn("Prepull");
            bool TotemMasteryUp = Aimsharp.HasBuff("Resonance Totem");
            bool UsePotion = Aimsharp.IsCustomCodeOn("Potions");
            bool NoCooldowns = Aimsharp.IsCustomCodeOn("SaveCooldowns");
            string PotionType = GetDropDown("Potion Type");
            bool CastingLB = Aimsharp.CastingID("player") == 51505 && Aimsharp.CastingRemaining("player") < 500;

            if (CastingLB)
            {
                if (Aimsharp.CanCast("Flame Shock"))
                {
                    Aimsharp.Cast("Flame Shock");
                    return true;
                }
            }

            if (Prepull)
            {
                if (Aimsharp.CanCast("Totem Mastery", "player"))
                {
                    if (!TotemMasteryUp)
                    {
                        Aimsharp.Cast("Totem Mastery");
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Stormkeeper", "player"))
                {
                    Aimsharp.Cast("Stormkeeper");
                    return true;
                }

                if (Aimsharp.CanUseItem("Azshara's Font of Power"))
                {
                    Aimsharp.Cast("Azshara's Font of Power", true);
                    return true;
                }

                if (Aimsharp.CanCast("Fire Elemental", "player"))
                {
                    Aimsharp.Cast("Fire Elemental");
                    return true;
                }

                if (MajorPower == "Guardian of Azeroth" && !NoCooldowns)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
                        return true;
                    }
                }

                if (UsePotion)
                {
                    if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                    {
                        Aimsharp.Cast("potion", true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast("Lava Burst"))
                {
                    Aimsharp.Cast("Lava Burst");
                    return true;
                }

                return false;
            }

            return false;
        }

    }
}
