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
    public class PerfSimFireMage : Rotation
    {

        public override void LoadSettings()
        {


            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Ashvane's Razor Coral", "Pocket-Sized Computation Device", "Galecaller's Boon", "Shiver Venom Relic", "Lurker's Insidious Gift", "Notorious Gladiator's Badge", "Sinister Gladiator's Badge", "Sinister Gladiator's Medallion", "Notorious Gladiator's Medallion", "Vial of Animated Blood", "First Mate's Spyglass", "Jes' Howler", "Ashvane's Razor Coral", "Knot of Ancient Fury", "Ignition Mage's Fuse", "Manifesto of Madness", "Balefire Branch", "Rotcrusted Voodoo Doll", "Forbidden Obsidian Claw", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "Bloodelf", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Blaster Master Traits", 0, 3, 1));

            Settings.Add(new Setting("Lucid Dreams Minor?", false));



        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            // Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Perfect Simcraft Series: Fire Mage - v 1.0", Color.Blue);
            Aimsharp.PrintMessage("Recommended talents: 3231123", Color.Blue);
            Aimsharp.PrintMessage("These macros can be used for manual control:", Color.Blue);
            Aimsharp.PrintMessage("/xxxxx Potions", Color.Blue);
            Aimsharp.PrintMessage("--Toggles using buff potions on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx SaveCooldowns", Color.Blue);
            Aimsharp.PrintMessage("--Toggles the use of big cooldowns on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("/xxxxx AOE", Color.Blue);
            Aimsharp.PrintMessage("--Toggles auto AOE mode on/off.", Color.Blue);
            Aimsharp.PrintMessage(" ");
            // Aimsharp.PrintMessage("/xxxxx Prepull 10", Color.Blue);
            // Aimsharp.PrintMessage("--Starts the prepull actions.", Color.Blue);
            // Aimsharp.PrintMessage(" ");
            Aimsharp.PrintMessage("--Replace xxxxx with first 5 letters of your addon, lowercase.", Color.Blue);

            Aimsharp.Latency = 50;
            Aimsharp.QuickDelay = 300;

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

            Spellbook.Add("Combustion");
            Spellbook.Add("Fire Blast");
            Spellbook.Add("Meteor");
            Spellbook.Add("Mirror Image");
            Spellbook.Add("Rune of Power");
            Spellbook.Add("Living Bomb");
            Spellbook.Add("Fireball");
            Spellbook.Add("Pyroblast");
            Spellbook.Add("Scorch");
            Spellbook.Add("Flamestrike");
            Spellbook.Add("Phoenix Flames");
            Spellbook.Add("Dragon's Breath");

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

            Buffs.Add("Combustion");
            Buffs.Add("Manifesto of Madness: Chapter One");
            Buffs.Add("Blaster Master");
            Buffs.Add("Heating Up");
            Buffs.Add("Hot Streak!");
            Buffs.Add("Rune of Power");
            Buffs.Add("Pyroclasm");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Items.Add("Neural Synapse Enhancer");
            Items.Add("Hyperthread Wristwraps");
            Items.Add("Malformed Herald's Legwraps");

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));
            Macros.Add("nse", "/use Neural Synapse Enhancer");
            Macros.Add("hw", "/use Hyperthread Wristwraps");
            Macros.Add("mhl", "/use Malformed Herald's Legwraps");

            Macros.Add("remove manifesto", "/cancelaura Manifesto of Madness: Chapter One");
            Macros.Add("meteor cursor", "/cast [@cursor] Meteor");
            Macros.Add("fs cursor", "/cast [@cursor] Flamestrike");
            Macros.Add("TopTrink", "/use 13");
            Macros.Add("BotTrink", "/use 14");

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            // CustomCommands.Add("Prepull");
            // CustomCommands.Add("LightAOE");
        }



        Stopwatch MeteorFlightTimer = new Stopwatch();
        Stopwatch PyroFlightTimer = new Stopwatch();
        Stopwatch FireballFlightTimer = new Stopwatch();
        int PyroRange = 0;
        int FireballRange = 0;

        // optional override for the CombatTick which executes while in combat
        public override bool CombatTick()
        {
            bool Fighting = Aimsharp.Range("target") <= 45 && Aimsharp.TargetIsEnemy();
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

            bool TalentRuneOfPower = Aimsharp.Talent(3, 3);
            bool TalentFirestarter = Aimsharp.Talent(1, 1);
            bool TalentFlameOn = Aimsharp.Talent(4, 1);
            bool TalentMeteor = Aimsharp.Talent(7, 3);
            bool TalentFlamePatch = Aimsharp.Talent(6, 1);
            bool TalentSearingTouch = Aimsharp.Talent(1, 3);
            bool TalentPhoenixFlames = Aimsharp.Talent(4, 3);
            bool TalentAlexstraszasFury = Aimsharp.Talent(4, 2);
            bool TalentKindling = Aimsharp.Talent(7, 1);


            int BuffCombustionRemains = Aimsharp.BuffRemaining("Combustion") - GCD;
            bool BuffCombustionUp = BuffCombustionRemains > 0;
            int BuffBlasterMasterRemains = Aimsharp.BuffRemaining("Blaster Master") - GCD;
            bool BuffBlasterMasterUp = BuffBlasterMasterRemains > 0;
            int BuffHeatingUpRemains = Aimsharp.BuffRemaining("Heating Up") - GCD;
            bool BuffHeatingUpUp = BuffHeatingUpRemains > 0;
            int BuffHotStreakRemains = Aimsharp.BuffRemaining("Hot Streak!") - GCD;
            bool BuffHotStreakUp = BuffHotStreakRemains > 0;
            bool BuffRuneOfPowerUp = Aimsharp.HasBuff("Rune of Power");
            int BuffPyroclasmRemains = Aimsharp.BuffRemaining("Pyroclasm") - GCD;
            bool BuffPyroclasmUp = BuffPyroclasmRemains > 0;


            int CDCombustionRemains = Aimsharp.SpellCooldown("Combustion");
            bool CDCombustionReady = CDCombustionRemains <= 10;
            int CDFireBlastDuration = (int)((12000f - (TalentFlameOn ? 2000 : 0)) / (1f + Haste));
            int CDRuneOfPowerRemains = Aimsharp.SpellCooldown("Rune of Power") - GCD;
            bool CDRuneOfPowerReady = CDRuneOfPowerRemains <= 10;
            int CDDragonsBreathRemains = Aimsharp.SpellCooldown("Dragon's Breath") - GCD;
            bool CDDragonsBreathReady = CDDragonsBreathRemains <= 10;


            int AzeriteBlasterMasterRank = GetSlider("# Blaster Master Traits");

            float RuneOfPowerCastTime = 1500f / (1f + Haste);
            float PyroblastCastTime = 4500f / (1f + Haste);
            bool FireStarterActive = TalentFirestarter && TargetHealth >= 90;
            int FireBlastCharges = Aimsharp.SpellCharges("Fire Blast");
            int FireBlastMaxCharges = Aimsharp.MaxCharges("Fire Blast");
            int FireBlastRechargeTime = Aimsharp.RechargeTime("Fire Blast");
            int FireBlastFullRechargeTime = (int)(FireBlastRechargeTime + (12000f - (TalentFlameOn ? 2000 : 0)) / (1f + Haste)) * (FireBlastMaxCharges - FireBlastCharges - 1);
            float FireBlastChargesFractional = FireBlastCharges + (FireBlastRechargeTime) / ((12000f - (TalentFlameOn ? 2000 : 0)) / (1f + Haste));
            int RuneOfPowerCharges = Aimsharp.SpellCharges("Rune of Power");
            int RuneOfPowerMaxCharges = Aimsharp.MaxCharges("Rune of Power");
            int RuneOfPowerRechargeTime = Aimsharp.RechargeTime("Rune of Power") - GCD;
            int RuneOfPowerFullRechargeTime = (int)(RuneOfPowerRechargeTime + 40000f * (RuneOfPowerMaxCharges - RuneOfPowerCharges - 1));
            int PlayerCasting = Aimsharp.CastingID("player");
            int PlayerCastRemaining = Aimsharp.CastingRemaining("player");
            bool ScorchExecuting = PlayerCasting == 2948 ? true : false;
            bool FireballExecuting = PlayerCasting == 133 ? true : false;
            bool RuneOfPowerExecuting = PlayerCasting == 116011 ? true : false;
            bool LucidMinor = GetCheckBox("Lucid Dreams Minor?");
            int PhoenixFlamesCharges = Aimsharp.SpellCharges("Phoenix Flames");
            int PhoenixFlamesMaxCharges = Aimsharp.MaxCharges("Phoenix Flames");
            int PhoenixFlamesRechargeTime = Aimsharp.RechargeTime("Phoenix Flames");
            int PhoenixFlamesFullRechargeTime = (int)(PhoenixFlamesRechargeTime + (30000f) * (PhoenixFlamesMaxCharges - PhoenixFlamesCharges - 1));
            float PhoenixFlamesChargesFractional = PhoenixFlamesCharges + (PhoenixFlamesRechargeTime) / (30000f);

            if (Aimsharp.LastCast() == "Meteor")
            {
                if (!MeteorFlightTimer.IsRunning)
                    MeteorFlightTimer.Start();
            }
            if (MeteorFlightTimer.ElapsedMilliseconds > 3000)
                MeteorFlightTimer.Reset();

            if (Aimsharp.LastCast() == "Pyroblast")
            {
                if (!PyroFlightTimer.IsRunning)
                {
                    PyroFlightTimer.Start();
                    PyroRange = Range;
                }
            }
            if (PyroFlightTimer.ElapsedMilliseconds > (1100 * (PyroRange / 40f)))
            {
                PyroFlightTimer.Reset();
                PyroRange = 0;
            }

            if (Aimsharp.LastCast() == "Fireball")
            {
                if (!FireballFlightTimer.IsRunning)
                {
                    FireballFlightTimer.Start();
                    FireballRange = Range;
                }
            }
            if (FireballFlightTimer.ElapsedMilliseconds > (800 * (FireballRange / 40f)))
            {
                FireballFlightTimer.Reset();
                FireballRange = 0;
            }

            bool FireballInFlight = FireballFlightTimer.ElapsedMilliseconds > 0 ? true : false;
            bool PyroInFlight = PyroFlightTimer.ElapsedMilliseconds > 0 ? true : false;
            bool MeteorInFlight = MeteorFlightTimer.ElapsedMilliseconds > 0 ? true : false;

            //actions.precombat +=/ variable,name = disable_combustion,op = reset
            bool Variabledisable_combustion = NoCooldowns;
            //actions.precombat+=/variable,name=combustion_rop_cutoff,op=set,value=60
            int Variablecombustion_rop_cutoff = 60000;
            //actions.precombat+=/variable,name=combustion_on_use,op=set,value=equipped.manifesto_of_madness|equipped.gladiators_badge|equipped.gladiators_medallion|equipped.ignition_mages_fuse|equipped.tzanes_barkspines|equipped.azurethos_singed_plumage|equipped.ancient_knot_of_wisdom|equipped.shockbiters_fang|equipped.neural_synapse_enhancer|equipped.balefire_branch
            bool Variablecombustion_on_use = Aimsharp.IsEquipped("Manifesto of Madness") || Aimsharp.IsEquipped("Notorious Gladiator's Badge") || Aimsharp.IsEquipped("Notorious Gladiator's Medallion") || Aimsharp.IsEquipped("Ignition Mage's Fuse") || Aimsharp.IsEquipped("Balefire Branch") || Aimsharp.IsEquipped("Neural Synapse Enhancer");
            //actions.precombat+=/variable,name=font_double_on_use,op=set,value=equipped.azsharas_font_of_power&variable.combustion_on_use
            bool Variablefont_double_on_use = Aimsharp.IsEquipped("Azshara's Font of Power") && Variablecombustion_on_use;
            //actions.precombat+=/variable,name=font_of_power_precombat_channel,op=set,value=18,if=variable.font_double_on_use&variable.font_of_power_precombat_channel=0
            int Variablefont_of_power_precombat_channel = Variablefont_double_on_use ? 18000 : 0;
            //actions.precombat+=/variable,name=on_use_cutoff,op=set,value=20*variable.combustion_on_use&!variable.font_double_on_use+40*variable.font_double_on_use+25*equipped.azsharas_font_of_power&!variable.font_double_on_use+8*equipped.manifesto_of_madness&!variable.font_double_on_use
            int Variableon_use_cutoff = 20000 * (Variablecombustion_on_use && !Variablefont_double_on_use ? 1 : 0) + 40000 * (Variablefont_double_on_use ? 1 : 0) + 25000 * (Aimsharp.IsEquipped("Azshara's Font of Power") && !Variablefont_double_on_use ? 1 : 0) + 8000 * (Aimsharp.IsEquipped("Manifesto of Madness") && !Variablefont_double_on_use ? 1 : 0);
            //actions+=/variable,name=fire_blast_pooling,value=talent.rune_of_power.enabled&cooldown.rune_of_power.remains<cooldown.fire_blast.full_recharge_time&(cooldown.combustion.remains>variable.combustion_rop_cutoff|variable.disable_combustion|firestarter.active)&(cooldown.rune_of_power.remains<target.time_to_die|action.rune_of_power.charges>0)|!variable.disable_combustion&cooldown.combustion.remains<action.fire_blast.full_recharge_time+cooldown.fire_blast.duration*azerite.blaster_master.enabled&!firestarter.active&cooldown.combustion.remains<target.time_to_die|talent.firestarter.enabled&firestarter.active&firestarter.remains<cooldown.fire_blast.full_recharge_time+cooldown.fire_blast.duration*azerite.blaster_master.enabled
            bool Variablefire_blast_pooling = TalentRuneOfPower && CDRuneOfPowerRemains < FireBlastFullRechargeTime && (CDCombustionRemains > Variablecombustion_rop_cutoff || Variabledisable_combustion || FireStarterActive) && (CDRuneOfPowerRemains < TargetTimeToDie || RuneOfPowerCharges > 0) || !Variabledisable_combustion && CDCombustionRemains < FireBlastFullRechargeTime + CDFireBlastDuration * (AzeriteBlasterMasterRank > 0 ? 1 : 0) && !FireStarterActive && CDCombustionRemains < TargetTimeToDie || TalentFirestarter && FireStarterActive;
            //actions+=/variable,name=phoenix_pooling,value=talent.rune_of_power.enabled&cooldown.rune_of_power.remains<cooldown.phoenix_flames.full_recharge_time&(cooldown.combustion.remains>variable.combustion_rop_cutoff|variable.disable_combustion)&(cooldown.rune_of_power.remains<target.time_to_die|action.rune_of_power.charges>0)|!variable.disable_combustion&cooldown.combustion.remains<action.phoenix_flames.full_recharge_time&cooldown.combustion.remains<target.time_to_die
            bool Variablephoenix_pooling = TalentRuneOfPower && CDRuneOfPowerRemains < PhoenixFlamesFullRechargeTime && (CDCombustionRemains > Variablecombustion_rop_cutoff || Variabledisable_combustion) && (CDRuneOfPowerRemains < TargetTimeToDie || RuneOfPowerCharges > 0) || !Variabledisable_combustion && CDCombustionRemains < PhoenixFlamesFullRechargeTime && CDCombustionRemains < TargetTimeToDie;

            if (IsChanneling)
                return false;

            //actions+=/call_action_list,name=items_high_priority
            //actions.items_high_priority=call_action_list,name=items_combustion,if=!variable.disable_combustion&(talent.rune_of_power.enabled&cooldown.combustion.remains<=action.rune_of_power.cast_time|cooldown.combustion.ready)&!firestarter.active|buff.combustion.up
            if (!Variabledisable_combustion && (TalentRuneOfPower && CDCombustionRemains <= RuneOfPowerCastTime || CDCombustionReady) && !FireStarterActive || BuffCombustionUp)
            {
                //actions.items_combustion=use_item,name=ignition_mages_fuse
                if (Aimsharp.CanUseItem("Ignition Mage's Fuse"))
                {
                    Aimsharp.Cast("Ignition Mage's Fuse", true);
                    return true;
                }

                //actions.items_combustion+=/use_item,name=hyperthread_wristwraps,if=buff.combustion.up&action.fire_blast.charges=0&action.fire_blast.recharge_time>gcd.max
                if (Aimsharp.CanUseItem("Hyperthread Wristwraps"))
                {
                    if (BuffCombustionUp && FireBlastCharges == 0 && FireBlastRechargeTime > GCDMAX)
                    {
                        Aimsharp.Cast("hw", true);
                        return true;
                    }
                }

                //actions.items_combustion+=/use_item,name=manifesto_of_madness
                if (Aimsharp.CanUseItem("Manifesto of Madness"))
                {
                    Aimsharp.Cast("Manifesto of Madness", true);
                    return true;
                }

                //actions.items_combustion+=/cancel_buff,use_off_gcd=1,name=manifesto_of_madness_chapter_one,if=buff.combustion.up|action.meteor.in_flight&action.meteor.in_flight_remains<=0.5
                if (BuffCombustionUp || MeteorInFlight && MeteorFlightTimer.ElapsedMilliseconds > 2400)
                {
                    if (Aimsharp.HasBuff("Manifesto of Madness: Chapter One"))
                    {
                        Aimsharp.Cast("remove manifesto", true);
                        return true;
                    }
                }

                //actions.items_combustion+=/use_item,use_off_gcd=1,effect_name=gladiators_badge,if=buff.combustion.up|action.meteor.in_flight&action.meteor.in_flight_remains<=0.5
                if (Aimsharp.CanUseItem("Notorious Gladiator's Badge"))
                {
                    if (BuffCombustionUp || MeteorInFlight && MeteorFlightTimer.ElapsedMilliseconds > 2400)
                    {
                        Aimsharp.Cast("Notorious Gladiator's Badge", true);
                        return true;
                    }
                }

                //actions.items_combustion+=/use_item,use_off_gcd=1,effect_name=gladiators_medallion,if=buff.combustion.up|action.meteor.in_flight&action.meteor.in_flight_remains<=0.5
                if (Aimsharp.CanUseItem("Notorious Gladiator's Medallion"))
                {
                    if (BuffCombustionUp || MeteorInFlight && MeteorFlightTimer.ElapsedMilliseconds > 2400)
                    {
                        Aimsharp.Cast("Notorious Gladiator's Medallion", true);
                        return true;
                    }
                }

                //actions.items_combustion+=/use_item,use_off_gcd=1,name=balefire_branch,if=buff.combustion.up|action.meteor.in_flight&action.meteor.in_flight_remains<=0.5
                if (Aimsharp.CanUseItem("Balefire Branch"))
                {
                    if (BuffCombustionUp || MeteorInFlight && MeteorFlightTimer.ElapsedMilliseconds > 2400)
                    {
                        Aimsharp.Cast("Balefire Branch", true);
                        return true;
                    }
                }

                //actions.items_combustion+=/use_item,use_off_gcd=1,name=neural_synapse_enhancer,if=buff.combustion.up|action.meteor.in_flight&action.meteor.in_flight_remains<=0.5
                if (Aimsharp.CanUseItem("Neural Synapse Enhancer"))
                {
                    if (BuffCombustionUp || MeteorInFlight && MeteorFlightTimer.ElapsedMilliseconds > 2400)
                    {
                        Aimsharp.Cast("nse", true);
                        return true;
                    }
                }

                //actions.items_combustion+=/use_item,use_off_gcd=1,name=malformed_heralds_legwraps,if=buff.combustion.up|action.meteor.in_flight&action.meteor.in_flight_remains<=0.5
                if (Aimsharp.CanUseItem("Malformed Herald's Legwraps"))
                {
                    if (BuffCombustionUp || MeteorInFlight && MeteorFlightTimer.ElapsedMilliseconds > 2400)
                    {
                        Aimsharp.Cast("mhl", true);
                        return true;
                    }
                }
            }

            //actions.items_high_priority+=/use_item,name=manifesto_of_madness,if=!equipped.azsharas_font_of_power&cooldown.combustion.remains<8
            if (Aimsharp.CanUseItem("Manifesto of Madness"))
            {
                if (!Aimsharp.IsEquipped("Azshara's Font of Power") && CDCombustionRemains < 8000)
                {
                    Aimsharp.Cast("Manifesto of Madness", true);
                    return true;
                }
            }

            //actions.items_high_priority+=/use_item,name=azsharas_font_of_power,if=cooldown.combustion.remains<=5+15*variable.font_double_on_use&!variable.disable_combustion
            if (Aimsharp.CanUseItem("Azshara's Font of Power"))
            {
                if (CDCombustionRemains <= 5000 + 15000 * (Variablefont_double_on_use ? 1 : 0) && !Variabledisable_combustion)
                {
                    Aimsharp.Cast("Azshara's Font of Power", true);
                    return true;
                }
            }

            //actions.items_high_priority+=/use_item,name=rotcrusted_voodoo_doll,if=cooldown.combustion.remains>variable.on_use_cutoff|variable.disable_combustion
            if (Aimsharp.CanUseItem("Rotcrusted Voodoo Doll"))
            {
                if (CDCombustionRemains > Variableon_use_cutoff || Variabledisable_combustion)
                {
                    Aimsharp.Cast("Rotcrusted Voodoo Doll", true);
                    return true;
                }
            }

            //actions.items_high_priority+=/use_item,name=shiver_venom_relic,if=cooldown.combustion.remains>variable.on_use_cutoff|variable.disable_combustion
            if (Aimsharp.CanUseItem("Shiver Venom Relic"))
            {
                if (CDCombustionRemains > Variableon_use_cutoff || Variabledisable_combustion)
                {
                    Aimsharp.Cast("Shiver Venom Relic", true);
                    return true;
                }
            }

            //actions.items_high_priority+=/use_item,name=forbidden_obsidian_claw,if=cooldown.combustion.remains>variable.on_use_cutoff|variable.disable_combustion
            if (Aimsharp.CanUseItem("Forbidden Obsidian Claw"))
            {
                if (CDCombustionRemains > Variableon_use_cutoff || Variabledisable_combustion)
                {
                    Aimsharp.Cast("Forbidden Obsidian Claw", true);
                    return true;
                }
            }

            //actions.items_high_priority+=/use_item,name=malformed_heralds_legwraps,if=cooldown.combustion.remains>=55&buff.combustion.down&cooldown.combustion.remains>variable.on_use_cutoff|variable.disable_combustion
            if (Aimsharp.CanUseItem("Malformed Herald's Legwraps"))
            {
                if (CDCombustionRemains >= 55000 && !BuffCombustionUp && CDCombustionRemains > Variableon_use_cutoff || Variabledisable_combustion)
                {
                    Aimsharp.Cast("mhl", true);
                    return true;
                }
            }

            //actions.items_high_priority+=/use_item,name=neural_synapse_enhancer,if=cooldown.combustion.remains>=45&buff.combustion.down&cooldown.combustion.remains>variable.on_use_cutoff|variable.disable_combustion
            if (Aimsharp.CanUseItem("Neural Synapse Enhancer"))
            {
                if (CDCombustionRemains >= 45000 && !BuffCombustionUp && CDCombustionRemains > Variableon_use_cutoff || Variabledisable_combustion)
                {
                    Aimsharp.Cast("nse", true);
                    return true;
                }
            }

            if (CDCombustionRemains >= 45000 && !BuffCombustionUp && CDCombustionRemains > Variableon_use_cutoff || Variabledisable_combustion)
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

            //actions+=/mirror_image,if=buff.combustion.down
            if (Aimsharp.CanCast("Mirror Image", "player"))
            {
                if (!BuffCombustionUp)
                {
                    Aimsharp.Cast("Mirror Image");
                    return true;
                }
            }

            //actions+=/guardian_of_azeroth,if=(cooldown.combustion.remains<10|target.time_to_die<cooldown.combustion.remains)&!variable.disable_combustion
            if (MajorPower == "Guardian of Azeroth" && Fighting)
            {
                if ((CDCombustionRemains < 10000 || TargetTimeToDie < CDCombustionRemains) && !Variabledisable_combustion)
                {
                    if (Aimsharp.CanCast("Guardian of Azeroth", "player"))
                    {
                        Aimsharp.Cast("Guardian of Azeroth");
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

            //actions+=/reaping_flames
            if (MajorPower == "Reaping Flames")
            {
                if (Aimsharp.CanCast("Reaping Flames"))
                {
                    Aimsharp.Cast("Reaping Flames");
                    return true;
                }
            }

            //actions+=/focused_azerite_beam
            if (MajorPower == "Focused Azerite Beam" && Range < 15)
            {
                if (Aimsharp.CanCast("Focused Azerite Beam", "player"))
                {
                    Aimsharp.Cast("Focused Azerite Beam");
                    return true;
                }
            }

            //actions+=/the_unbound_force
            if (MajorPower == "The Unbound Force")
            {
                if (Aimsharp.CanCast("The Unbound Force"))
                {
                    Aimsharp.Cast("The Unbound Force");
                    return true;
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

            //actions+=/rune_of_power,if=talent.firestarter.enabled&firestarter.remains>full_recharge_time|cooldown.combustion.remains>variable.combustion_rop_cutoff&buff.combustion.down|target.time_to_die<cooldown.combustion.remains&buff.combustion.down|variable.disable_combustion
            if (Aimsharp.CanCast("Rune of Power", "player") && !Moving)
            {
                if (TalentFirestarter || CDCombustionRemains > Variablecombustion_rop_cutoff && !BuffCombustionUp || TargetTimeToDie < CDCombustionRemains && !BuffCombustionUp || Variabledisable_combustion)
                {
                    Aimsharp.Cast("Rune of Power");
                    return true;
                }
            }

            //actions+=/call_action_list,name=combustion_phase,if=!variable.disable_combustion&(talent.rune_of_power.enabled&cooldown.combustion.remains<=action.rune_of_power.cast_time|cooldown.combustion.ready)&!firestarter.active|buff.combustion.up
            if (!Variabledisable_combustion && (TalentRuneOfPower && CDCombustionRemains <= RuneOfPowerCastTime || CDCombustionReady) && !FireStarterActive || BuffCombustionUp)
            {
                //actions.combustion_phase=lights_judgment,if=buff.combustion.down
                if (RacialPower == "Lightforged Draenei" && Fighting)
                {
                    if (Aimsharp.CanCast("Light's Judgment", "player"))
                    {
                        if (!BuffCombustionUp)
                        {
                            Aimsharp.Cast("Light's Judgment", true);
                            return true;
                        }
                    }
                }

                //actions.combustion_phase+=/living_bomb,if=active_enemies>1&buff.combustion.down
                if (Aimsharp.CanCast("Living Bomb"))
                {
                    if (EnemiesNearTarget > 1 && !BuffCombustionUp)
                    {
                        Aimsharp.Cast("Living Bomb");
                        return true;
                    }
                }

                //actions.combustion_phase+=/blood_of_the_enemy
                if (MajorPower == "Blood of the Enemy" && EnemiesInMelee > 0)
                {
                    if (Aimsharp.CanCast("Blood of the Enemy", "player"))
                    {
                        Aimsharp.Cast("Blood of the Enemy");
                        return true;
                    }
                }

                //actions.combustion_phase+=/memory_of_lucid_dreams
                if (MajorPower == "Memory of Lucid Dreams" && Fighting)
                {
                    if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                    {
                        Aimsharp.Cast("Memory of Lucid Dreams");
                        return true;
                    }
                }

                //actions.combustion_phase+=/fire_blast,use_while_casting=1,use_off_gcd=1,if=charges>=1&((action.fire_blast.charges_fractional+(buff.combustion.remains-buff.blaster_master.duration)%cooldown.fire_blast.duration-(buff.combustion.remains)%(buff.blaster_master.duration-0.5))>=0|!azerite.blaster_master.enabled|!talent.flame_on.enabled|buff.combustion.remains<=buff.blaster_master.duration|buff.blaster_master.remains<0.5|equipped.hyperthread_wristwraps&cooldown.hyperthread_wristwraps_300142.remains<5)&buff.combustion.up&(!action.scorch.executing&!action.pyroblast.in_flight&buff.heating_up.up|action.scorch.executing&buff.hot_streak.down&(buff.heating_up.down|azerite.blaster_master.enabled)|azerite.blaster_master.enabled&talent.flame_on.enabled&action.pyroblast.in_flight&buff.heating_up.down&buff.hot_streak.down)
                if (Aimsharp.CanCast("Fire Blast"))
                {
                    if (FireBlastCharges >= 1 && ((FireBlastChargesFractional + (BuffCombustionRemains - 3000f) % CDFireBlastDuration - (BuffCombustionRemains) % 2500f) >= 0 || AzeriteBlasterMasterRank == 0 || !TalentFlameOn || BuffCombustionRemains <= 3000 || BuffBlasterMasterRemains < 500 || Aimsharp.IsEquipped("Hyperthread Wristwraps") && Aimsharp.ItemCooldown("Hyperthread Wristwraps") < 5000) && BuffCombustionUp && (!ScorchExecuting && !PyroInFlight && BuffHeatingUpUp || ScorchExecuting && !BuffHotStreakUp && (!BuffHeatingUpUp || AzeriteBlasterMasterRank > 0) || AzeriteBlasterMasterRank > 0 && TalentFlameOn && PyroInFlight && !BuffHeatingUpUp && !BuffHotStreakUp))
                    {
                        Aimsharp.Cast("Fire Blast", true);
                        return true;
                    }
                }

                //actions.combustion_phase +=/ rune_of_power,if= buff.combustion.down
                if (Aimsharp.CanCast("Rune of Power", "player") && !Moving)
                {
                    if (!BuffCombustionUp)
                    {
                        Aimsharp.Cast("Rune of Power");
                        return true;
                    }
                }

                //actions.combustion_phase+=/fire_blast,use_while_casting=1,if=azerite.blaster_master.enabled&(essence.memory_of_lucid_dreams.major|!essence.memory_of_lucid_dreams.minor)&talent.meteor.enabled&talent.flame_on.enabled&buff.blaster_master.down&(talent.rune_of_power.enabled&action.rune_of_power.executing&action.rune_of_power.execute_remains<0.6|(cooldown.combustion.ready|buff.combustion.up)&!talent.rune_of_power.enabled&!action.pyroblast.in_flight&!action.fireball.in_flight)
                if (Aimsharp.CanCast("Fire Blast"))
                {
                    if (AzeriteBlasterMasterRank > 0 && (MajorPower == "Memory of Lucid Dreams" || !LucidMinor) && TalentMeteor && TalentFlameOn && !BuffBlasterMasterUp && (TalentRuneOfPower && RuneOfPowerExecuting && PlayerCastRemaining < 600 || (CDCombustionReady || BuffCombustionUp) && !TalentRuneOfPower && !PyroInFlight && !FireballInFlight))
                    {
                        Aimsharp.Cast("Fire Blast", true);
                        return true;
                    }
                }

                //actions.active_talents=living_bomb,if=active_enemies>1&buff.combustion.down&(cooldown.combustion.remains>cooldown.living_bomb.duration|cooldown.combustion.ready|variable.disable_combustion)
                if (Aimsharp.CanCast("Living Bomb"))
                {
                    if (EnemiesNearTarget > 1 && !BuffCombustionUp && (CDCombustionRemains > 12000 || CDCombustionReady || Variabledisable_combustion))
                    {
                        Aimsharp.Cast("Living Bomb");
                        return true;
                    }
                }

                //actions.active_talents+=/meteor,if=buff.rune_of_power.up&(firestarter.remains>cooldown.meteor.duration|!firestarter.active)|cooldown.rune_of_power.remains>target.time_to_die&action.rune_of_power.charges<1|(cooldown.meteor.duration<cooldown.combustion.remains|cooldown.combustion.ready|variable.disable_combustion)&!talent.rune_of_power.enabled&(cooldown.meteor.duration<firestarter.remains|!talent.firestarter.enabled|!firestarter.active)
                if (Aimsharp.CanCast("Meteor", "player"))
                {
                    if (BuffRuneOfPowerUp && (FireStarterActive || !FireStarterActive) || CDRuneOfPowerRemains > TargetTimeToDie && RuneOfPowerCharges < 1 || (45000 < CDCombustionRemains || CDCombustionReady || Variabledisable_combustion) && !TalentRuneOfPower && (FireStarterActive || !TalentFirestarter || !FireStarterActive))
                    {
                        Aimsharp.Cast("meteor cursor");
                        return true;
                    }
                }

                //actions.combustion_phase+=/combustion,use_off_gcd=1,use_while_casting=1,if=((action.meteor.in_flight&action.meteor.in_flight_remains<=0.5)|!talent.meteor.enabled)&(buff.rune_of_power.up|!talent.rune_of_power.enabled)
                if (Aimsharp.CanCast("Combustion", "player"))
                {
                    if (((MeteorInFlight && MeteorFlightTimer.ElapsedMilliseconds > 2300) || !TalentMeteor) && (BuffRuneOfPowerUp || !TalentRuneOfPower))
                    {
                        Aimsharp.Cast("Combustion");
                        return true;
                    }
                }

                //actions.combustion_phase+=/potion
                if (UsePotion && Fighting)
                {
                    if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                    {
                        Aimsharp.Cast("potion", true);
                        return true;
                    }
                }

                //actions.combustion_phase+=/blood_fury
                if (RacialPower == "Orc" && Fighting)
                {
                    if (Aimsharp.CanCast("Blood Fury", "player"))
                    {
                        Aimsharp.Cast("Blood Fury", true);
                        return true;
                    }

                }

                //actions.combustion_phase+=/berserking
                if (RacialPower == "Troll" && Fighting)
                {
                    if (Aimsharp.CanCast("Berserking", "player"))
                    {
                        Aimsharp.Cast("Berserking", true);
                        return true;
                    }
                }

                //actions.combustion_phase+=/fireblood
                if (RacialPower == "Dark Iron Dwarf" && Fighting)
                {
                    if (Aimsharp.CanCast("Fireblood", "player"))
                    {
                        Aimsharp.Cast("Fireblood", true);
                        return true;
                    }
                }

                //actions.combustion_phase+=/ancestral_call
                if (RacialPower == "Mag'har Orc" && Fighting)
                {
                    if (Aimsharp.CanCast("Ancestral Call", "player"))
                    {
                        Aimsharp.Cast("Ancestral Call", true);
                        return true;
                    }
                }

                //actions.combustion_phase+=/flamestrike,if=((talent.flame_patch.enabled&active_enemies>2)|active_enemies>6)&buff.hot_streak.react&!azerite.blaster_master.enabled
                if (Aimsharp.CanCast("Flamestrike", "player"))
                {
                    if (((TalentFlamePatch && EnemiesNearTarget > 2) || EnemiesNearTarget > 6) && BuffHotStreakUp && AzeriteBlasterMasterRank == 0)
                    {
                        Aimsharp.Cast("fs cursor");
                        return true;
                    }
                }

                //actions.combustion_phase+=/pyroblast,if=buff.pyroclasm.react&buff.combustion.remains>cast_time
                if (Aimsharp.CanCast("Pyroblast") && !Moving)
                {
                    if (BuffPyroclasmUp && BuffCombustionRemains > PyroblastCastTime)
                    {
                        Aimsharp.Cast("Pyroblast");
                        return true;
                    }
                }

                //actions.combustion_phase+=/pyroblast,if=buff.hot_streak.react
                if (Aimsharp.CanCast("Pyroblast"))
                {
                    if (BuffHotStreakUp)
                    {
                        Aimsharp.Cast("Pyroblast");
                        return true;
                    }
                }

                //actions.combustion_phase+=/pyroblast,if=prev_gcd.1.scorch&buff.heating_up.up
                if (Aimsharp.CanCast("Pyroblast"))
                {
                    if (BuffHeatingUpUp && ScorchExecuting && PlayerCastRemaining < BuffCombustionRemains)
                    {
                        Aimsharp.Cast("Pyroblast");
                        return true;
                    }
                }

                //actions.combustion_phase+=/phoenix_flames
                if (Aimsharp.CanCast("Phoenix Flames"))
                {
                    Aimsharp.Cast("Phoenix Flames");
                    return true;
                }

                //actions.combustion_phase+=/scorch,if=buff.combustion.remains>cast_time&buff.combustion.up|buff.combustion.down
                if (Aimsharp.CanCast("Scorch"))
                {
                    if (BuffCombustionRemains > RuneOfPowerCastTime && BuffCombustionUp || !BuffCombustionUp)
                    {
                        Aimsharp.Cast("Scorch");
                        return true;
                    }
                }

                //actions.combustion_phase+=/living_bomb,if=buff.combustion.remains<gcd.max&active_enemies>1
                if (Aimsharp.CanCast("Living Bomb"))
                {
                    if (BuffCombustionRemains < GCDMAX && EnemiesNearTarget > 1)
                    {
                        Aimsharp.Cast("Living Bomb");
                        return true;
                    }
                }

                //actions.combustion_phase+=/dragons_breath,if=buff.combustion.remains<gcd.max&buff.combustion.up
                if (Aimsharp.CanCast("Dragon's Breath", "player"))
                {
                    if (BuffCombustionRemains < GCDMAX && BuffCombustionUp && Range <= 8)
                    {
                        Aimsharp.Cast("Dragon's Breath");
                        return true;
                    }
                }

                //actions.combustion_phase+=/scorch,if=target.health.pct<=30&talent.searing_touch.enabled
                if (Aimsharp.CanCast("Scorch"))
                {
                    if (TargetHealth <= 30 && TalentSearingTouch)
                    {
                        Aimsharp.Cast("Scorch");
                        return true;
                    }
                }
            }

            //actions+=/fire_blast,use_while_casting=1,use_off_gcd=1,if=(essence.memory_of_lucid_dreams.major|essence.memory_of_lucid_dreams.minor&azerite.blaster_master.enabled)&charges=max_charges&!buff.hot_streak.react&!(buff.heating_up.react&(buff.combustion.up&(action.fireball.in_flight|action.pyroblast.in_flight|action.scorch.executing)|target.health.pct<=30&action.scorch.executing))&!(!buff.heating_up.react&!buff.hot_streak.react&buff.combustion.down&(action.fireball.in_flight|action.pyroblast.in_flight))
            if (Aimsharp.CanCast("Fire Blast"))
            {
                if ((MajorPower == "Memory of Lucid Dreams" || LucidMinor && AzeriteBlasterMasterRank > 0) && FireBlastCharges == FireBlastMaxCharges && !BuffHotStreakUp && !(BuffHeatingUpUp && (BuffCombustionUp && (FireballInFlight || PyroInFlight || ScorchExecuting) || TargetHealth <= 30 && ScorchExecuting)) && !(!BuffHeatingUpUp && !BuffHotStreakUp && !BuffCombustionUp && (FireballInFlight || PyroInFlight)))
                {
                    Aimsharp.Cast("Fire Blast", true);
                    return true;
                }
            }

            //actions+=/fire_blast,use_while_casting=1,use_off_gcd=1,if=firestarter.active&charges>=1&(!variable.fire_blast_pooling|buff.rune_of_power.up)&(!azerite.blaster_master.enabled|buff.blaster_master.remains<0.5)&(!action.fireball.executing&!action.pyroblast.in_flight&buff.heating_up.up|action.fireball.executing&buff.hot_streak.down|action.pyroblast.in_flight&buff.heating_up.down&buff.hot_streak.down)
            if (Aimsharp.CanCast("Fire Blast"))
            {
                if (FireStarterActive && FireBlastCharges >= 1 && (!Variablefire_blast_pooling || BuffRuneOfPowerUp) && (AzeriteBlasterMasterRank == 0 || BuffBlasterMasterRemains < 500) && (!FireballExecuting && !PyroInFlight && BuffHeatingUpUp || FireballExecuting && !BuffHotStreakUp || PyroInFlight && !BuffHotStreakUp && !BuffHeatingUpUp))
                {
                    Aimsharp.Cast("Fire Blast", true);
                    return true;
                }
            }

            //actions+=/call_action_list,name=rop_phase,if=buff.rune_of_power.up&buff.combustion.down
            if (BuffRuneOfPowerUp && !BuffCombustionUp)
            {
                //actions.rop_phase=rune_of_power
                if (Aimsharp.CanCast("Rune of Power", "player") && !Moving)
                {
                    Aimsharp.Cast("Rune of Power");
                    return true;
                }

                //actions.rop_phase+=/flamestrike,if=(talent.flame_patch.enabled&active_enemies>1|active_enemies>4)&buff.hot_streak.react
                if (Aimsharp.CanCast("Flamestrike", "player"))
                {
                    if ((TalentFlamePatch && EnemiesNearTarget > 1 || EnemiesNearTarget > 4) && BuffHotStreakUp)
                    {
                        Aimsharp.Cast("fs cursor");
                        return true;
                    }
                }

                //actions.rop_phase+=/pyroblast,if=buff.hot_streak.react
                if (Aimsharp.CanCast("Pyroblast"))
                {
                    if (BuffHotStreakUp)
                    {
                        Aimsharp.Cast("Pyroblast");
                        return true;
                    }
                }

                //actions.rop_phase+=/fire_blast,use_off_gcd=1,use_while_casting=1,if=!(talent.flame_patch.enabled&active_enemies>2|active_enemies>5)&(!firestarter.active&(cooldown.combustion.remains>0|variable.disable_combustion))&(!buff.heating_up.react&!buff.hot_streak.react&!prev_off_gcd.fire_blast&(action.fire_blast.charges>=2|(action.phoenix_flames.charges>=1&talent.phoenix_flames.enabled)|(talent.alexstraszas_fury.enabled&cooldown.dragons_breath.ready)|(talent.searing_touch.enabled&target.health.pct<=30)))
                if (Aimsharp.CanCast("Fire Blast"))
                {
                    if (!(TalentFlamePatch && EnemiesNearTarget > 2 || EnemiesNearTarget > 5) && (!FireStarterActive && (CDCombustionRemains > 0 || Variabledisable_combustion)) && (!BuffHeatingUpUp && !BuffHotStreakUp && LastCast != "Fire Blast" && (FireBlastCharges >= 2 | (PhoenixFlamesCharges >= 1 && TalentPhoenixFlames) || (TalentAlexstraszasFury && CDDragonsBreathReady) || (TalentSearingTouch && TargetHealth <= 30))))
                    {
                        Aimsharp.Cast("Fire Blast", true);
                        return true;
                    }
                }

                //actions.active_talents=living_bomb,if=active_enemies>1&buff.combustion.down&(cooldown.combustion.remains>cooldown.living_bomb.duration|cooldown.combustion.ready|variable.disable_combustion)
                if (Aimsharp.CanCast("Living Bomb"))
                {
                    if (EnemiesNearTarget > 1 && !BuffCombustionUp && (CDCombustionRemains > 12000 || CDCombustionReady || Variabledisable_combustion))
                    {
                        Aimsharp.Cast("Living Bomb");
                        return true;
                    }
                }

                //actions.active_talents+=/meteor,if=buff.rune_of_power.up&(firestarter.remains>cooldown.meteor.duration|!firestarter.active)|cooldown.rune_of_power.remains>target.time_to_die&action.rune_of_power.charges<1|(cooldown.meteor.duration<cooldown.combustion.remains|cooldown.combustion.ready|variable.disable_combustion)&!talent.rune_of_power.enabled&(cooldown.meteor.duration<firestarter.remains|!talent.firestarter.enabled|!firestarter.active)
                if (Aimsharp.CanCast("Meteor", "player"))
                {
                    if (BuffRuneOfPowerUp && (FireStarterActive || !FireStarterActive) || CDRuneOfPowerRemains > TargetTimeToDie && RuneOfPowerCharges < 1 || (45000 < CDCombustionRemains || CDCombustionReady || Variabledisable_combustion) && !TalentRuneOfPower && (FireStarterActive || !TalentFirestarter || !FireStarterActive))
                    {
                        Aimsharp.Cast("meteor cursor");
                        return true;
                    }
                }

                //actions.rop_phase+=/pyroblast,if=buff.pyroclasm.react&cast_time<buff.pyroclasm.remains&buff.rune_of_power.remains>cast_time
                if (Aimsharp.CanCast("Pyroblast") && !Moving)
                {
                    if (BuffPyroclasmUp && PyroblastCastTime < BuffPyroclasmRemains && BuffRuneOfPowerUp)
                    {
                        Aimsharp.Cast("Pyroblast");
                        return true;
                    }
                }

                //actions.rop_phase+=/fire_blast,use_off_gcd=1,use_while_casting=1,if=!(talent.flame_patch.enabled&active_enemies>2|active_enemies>5)&(!firestarter.active&(cooldown.combustion.remains>0|variable.disable_combustion))&(buff.heating_up.react&(target.health.pct>=30|!talent.searing_touch.enabled))
                if (Aimsharp.CanCast("Fire Blast"))
                {
                    if (!(TalentFlamePatch && EnemiesNearTarget > 2 || EnemiesNearTarget > 5) && (!FireStarterActive && (CDCombustionRemains > 0 || Variabledisable_combustion)) && (BuffHeatingUpUp && (TargetHealth >= 30 || !TalentSearingTouch)))
                    {
                        Aimsharp.Cast("Fire Blast", true);
                        return true;
                    }
                }

                //actions.rop_phase+=/fire_blast,use_off_gcd=1,use_while_casting=1,if=!(talent.flame_patch.enabled&active_enemies>2|active_enemies>5)&(!firestarter.active&(cooldown.combustion.remains>0|variable.disable_combustion))&talent.searing_touch.enabled&target.health.pct<=30&(buff.heating_up.react&!action.scorch.executing|!buff.heating_up.react&!buff.hot_streak.react)
                if (Aimsharp.CanCast("Fire Blast"))
                {
                    if (!(TalentFlamePatch && EnemiesNearTarget > 2 || EnemiesNearTarget > 5) && (!FireStarterActive && (CDCombustionRemains > 0 || Variabledisable_combustion)) && TalentSearingTouch && TargetHealth <= 30 && (BuffHeatingUpUp && !ScorchExecuting || !BuffHeatingUpUp && !BuffHotStreakUp))
                    {
                        Aimsharp.Cast("Fire Blast", true);
                        return true;
                    }
                }

                //actions.rop_phase+=/pyroblast,if=prev_gcd.1.scorch&buff.heating_up.up&talent.searing_touch.enabled&target.health.pct<=30&(!talent.flame_patch.enabled|active_enemies=1)
                if (Aimsharp.CanCast("Pyroblast"))
                {
                    if (ScorchExecuting && BuffHeatingUpUp && TalentSearingTouch && TargetHealth < 30 && (!TalentFlamePatch || EnemiesNearTarget <= 1))
                    {
                        Aimsharp.Cast("Pyroblast");
                        return true;
                    }
                }

                //actions.rop_phase+=/phoenix_flames,if=!prev_gcd.1.phoenix_flames&buff.heating_up.react
                if (Aimsharp.CanCast("Phoenix Flames"))
                {
                    if (LastCast != "Phoenix Flames" && BuffHeatingUpUp)
                    {
                        Aimsharp.Cast("Phoenix Flames");
                        return true;
                    }
                }

                //actions.rop_phase+=/scorch,if=target.health.pct<=30&talent.searing_touch.enabled
                if (Aimsharp.CanCast("Scorch"))
                {
                    if (TargetHealth <= 30 && TalentSearingTouch)
                    {
                        Aimsharp.Cast("Scorch");
                        return true;
                    }
                }

                //actions.rop_phase+=/dragons_breath,if=active_enemies>2
                if (Aimsharp.CanCast("Dragon's Breath", "player"))
                {
                    if (EnemiesInMelee > 2)
                    {
                        Aimsharp.Cast("Dragon's Breath");
                        return true;
                    }
                }

                //actions.rop_phase+=/fire_blast,use_off_gcd=1,use_while_casting=1,if=(talent.flame_patch.enabled&active_enemies>2|active_enemies>5)&((cooldown.combustion.remains>0|variable.disable_combustion)&!firestarter.active)&buff.hot_streak.down&(!azerite.blaster_master.enabled|buff.blaster_master.remains<0.5)
                if (Aimsharp.CanCast("Fire Blast"))
                {
                    if ((TalentFlamePatch && EnemiesNearTarget > 2 || EnemiesNearTarget > 5) && ((CDCombustionRemains > 0 || Variabledisable_combustion) && !FireStarterActive) && !BuffHotStreakUp && (AzeriteBlasterMasterRank == 0 || BuffBlasterMasterRemains < 500))
                    {
                        Aimsharp.Cast("Fire Blast", true);
                        return true;
                    }
                }

                //actions.rop_phase+=/flamestrike,if=talent.flame_patch.enabled&active_enemies>2|active_enemies>5
                if (Aimsharp.CanCast("Flamestrike", "player"))
                {
                    if (TalentFlamePatch && EnemiesNearTarget > 2 || EnemiesNearTarget > 5)
                    {
                        Aimsharp.Cast("fs cursor");
                        return true;
                    }
                }

                //actions.rop_phase+=/fireball
                if (Aimsharp.CanCast("Fireball") && !Moving)
                {
                    Aimsharp.Cast("Fireball");
                    return true;
                }
            }

            //actions.standard_rotation=flamestrike,if=((talent.flame_patch.enabled&active_enemies>1&!firestarter.active)|active_enemies>4)&buff.hot_streak.react
            if (Aimsharp.CanCast("Flamestrike", "player"))
            {
                if (((TalentFlamePatch && EnemiesNearTarget > 1 && !FireStarterActive) || EnemiesNearTarget > 4) && BuffHotStreakUp)
                {
                    Aimsharp.Cast("fs cursor");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Pyroblast"))
            {
                if (BuffHotStreakUp)
                {
                    Aimsharp.Cast("Pyroblast");
                    return true;
                }
            } 

            //actions.standard_rotation+=/pyroblast,if=buff.hot_streak.react&(prev_gcd.1.fireball|firestarter.active|action.pyroblast.in_flight)
            if (Aimsharp.CanCast("Pyroblast"))
            {
                if (BuffHotStreakUp && (LastCast == "Fireball" || FireStarterActive || PyroInFlight))
                {
                    Aimsharp.Cast("Pyroblast");
                    return true;
                }
            }

            //actions.standard_rotation+=/phoenix_flames,if=charges>=3&active_enemies>2&!variable.phoenix_pooling
            if (Aimsharp.CanCast("Phoenix Flames"))
            {
                if (PhoenixFlamesCharges >= 3 && EnemiesNearTarget > 2 && !Variablephoenix_pooling)
                {
                    Aimsharp.Cast("Phoenix Flames");
                    return true;
                }
            }

            //actions.standard_rotation+=/pyroblast,if=buff.hot_streak.react&target.health.pct<=30&talent.searing_touch.enabled
            if (Aimsharp.CanCast("Pyroblast"))
            {
                if (BuffHotStreakUp && TargetHealth < 30 && TalentSearingTouch)
                {
                    Aimsharp.Cast("Pyroblast");
                    return true;
                }
            }

            //actions.standard_rotation+=/pyroblast,if=buff.pyroclasm.react&cast_time<buff.pyroclasm.remains
            if (Aimsharp.CanCast("Pyroblast") && !Moving)
            {
                if (BuffPyroclasmUp && BuffPyroclasmRemains > PyroblastCastTime)
                {
                    Aimsharp.Cast("Pyroblast");
                    return true;
                }
            }

            //actions.standard_rotation+=/fire_blast,use_off_gcd=1,use_while_casting=1,if=((cooldown.combustion.remains>0|variable.disable_combustion)&buff.rune_of_power.down&!firestarter.active)&!talent.kindling.enabled&!variable.fire_blast_pooling&(((action.fireball.executing|action.pyroblast.executing)&(buff.heating_up.react))|(talent.searing_touch.enabled&target.health.pct<=30&(buff.heating_up.react&!action.scorch.executing|!buff.hot_streak.react&!buff.heating_up.react&action.scorch.executing&!action.pyroblast.in_flight&!action.fireball.in_flight)))
            if (Aimsharp.CanCast("Fire Blast"))
            {
                if (((CDCombustionRemains > 0 || Variabledisable_combustion) && !BuffRuneOfPowerUp && !FireStarterActive) && !TalentKindling && !Variablefire_blast_pooling && (((FireballExecuting) && (BuffHeatingUpUp)) || (TalentSearingTouch && TargetHealth <= 30 && (BuffHeatingUpUp && !ScorchExecuting || !BuffHotStreakUp && !BuffHeatingUpUp && ScorchExecuting && !PyroInFlight && !FireballInFlight))))
                {
                    Aimsharp.Cast("Fire Blast", true);
                    return true;
                }
            }

            //actions.standard_rotation+=/fire_blast,if=talent.kindling.enabled&buff.heating_up.react&!firestarter.active&(cooldown.combustion.remains>full_recharge_time+2+talent.kindling.enabled|variable.disable_combustion|(!talent.rune_of_power.enabled|cooldown.rune_of_power.remains>target.time_to_die&action.rune_of_power.charges<1)&cooldown.combustion.remains>target.time_to_die)
            if (Aimsharp.CanCast("Fire Blast"))
            {
                if (TalentKindling && BuffHeatingUpUp && !FireStarterActive && (CDCombustionRemains > FireBlastFullRechargeTime + 2000 + (TalentKindling ? 1000 : 0) || Variabledisable_combustion || (!TalentRuneOfPower || CDRuneOfPowerRemains > TargetTimeToDie && RuneOfPowerCharges < 1) && CDCombustionRemains > TargetTimeToDie))
                {
                    Aimsharp.Cast("Fire Blast", true);
                    return true;
                }
            }

            //actions.standard_rotation+=/pyroblast,if=prev_gcd.1.scorch&buff.heating_up.up&talent.searing_touch.enabled&target.health.pct<=30&((talent.flame_patch.enabled&active_enemies=1&!firestarter.active)|(active_enemies<4&!talent.flame_patch.enabled))
            if (Aimsharp.CanCast("Pyroblast"))
            {
                if (ScorchExecuting && BuffHeatingUpUp && TalentSearingTouch && TargetHealth < 30 && ((TalentFlamePatch && EnemiesNearTarget <= 1 && !FireStarterActive) || (EnemiesNearTarget < 4 && !TalentFlamePatch)))
                {
                    Aimsharp.Cast("Pyroblast");
                    return true;
                }
            }

            //actions.standard_rotation+=/phoenix_flames,if=(buff.heating_up.react|(!buff.hot_streak.react&(action.fire_blast.charges>0|talent.searing_touch.enabled&target.health.pct<=30)))&!variable.phoenix_pooling
            if (Aimsharp.CanCast("Phoenix Flames"))
            {
                if ((BuffHeatingUpUp || (!BuffHotStreakUp && (FireBlastCharges > 0 || TalentSearingTouch && TargetHealth <= 30))) && !Variablephoenix_pooling)
                {
                    Aimsharp.Cast("Phoenix Flames");
                    return true;
                }
            }

            //actions.active_talents=living_bomb,if=active_enemies>1&buff.combustion.down&(cooldown.combustion.remains>cooldown.living_bomb.duration|cooldown.combustion.ready|variable.disable_combustion)
            if (Aimsharp.CanCast("Living Bomb"))
            {
                if (EnemiesNearTarget > 1 && !BuffCombustionUp && (CDCombustionRemains > 12000 || CDCombustionReady || Variabledisable_combustion))
                {
                    Aimsharp.Cast("Living Bomb");
                    return true;
                }
            }

            //actions.active_talents+=/meteor,if=buff.rune_of_power.up&(firestarter.remains>cooldown.meteor.duration|!firestarter.active)|cooldown.rune_of_power.remains>target.time_to_die&action.rune_of_power.charges<1|(cooldown.meteor.duration<cooldown.combustion.remains|cooldown.combustion.ready|variable.disable_combustion)&!talent.rune_of_power.enabled&(cooldown.meteor.duration<firestarter.remains|!talent.firestarter.enabled|!firestarter.active)
            if (Aimsharp.CanCast("Meteor", "player"))
            {
                if (BuffRuneOfPowerUp && (FireStarterActive || !FireStarterActive) || CDRuneOfPowerRemains > TargetTimeToDie && RuneOfPowerCharges < 1 || (45000 < CDCombustionRemains || CDCombustionReady || Variabledisable_combustion) && !TalentRuneOfPower && (FireStarterActive || !TalentFirestarter || !FireStarterActive))
                {
                    Aimsharp.Cast("meteor cursor");
                    return true;
                }
            }

            //actions.standard_rotation+=/dragons_breath,if=active_enemies>1
            if (Aimsharp.CanCast("Dragon's Breath", "player"))
            {
                if (EnemiesInMelee > 1)
                {
                    Aimsharp.Cast("Dragon's Breath");
                    return true;
                }
            }

            //actions.standard_rotation+=/scorch,if=target.health.pct<=30&talent.searing_touch.enabled
            if (Aimsharp.CanCast("Scorch"))
            {
                if (TargetHealth <= 30 && TalentSearingTouch)
                {
                    Aimsharp.Cast("Scorch");
                    return true;
                }
            }

            //actions.standard_rotation+=/fire_blast,use_off_gcd=1,use_while_casting=1,if=!variable.fire_blast_pooling&(talent.flame_patch.enabled&active_enemies>2|active_enemies>9)&((cooldown.combustion.remains>0|variable.disable_combustion)&!firestarter.active)&buff.hot_streak.down&(!azerite.blaster_master.enabled|buff.blaster_master.remains<0.5)
            if (Aimsharp.CanCast("Fire Blast"))
            {
                if (!Variablefire_blast_pooling && (TalentFlamePatch && EnemiesNearTarget > 2 || EnemiesNearTarget > 9) && ((CDCombustionRemains > 0 || Variabledisable_combustion) && !FireStarterActive) && !BuffHotStreakUp && (AzeriteBlasterMasterRank == 0 || BuffBlasterMasterRemains < 500))
                {
                    Aimsharp.Cast("Fire Blast", true);
                    return true;
                }
            }

            //actions.standard_rotation+=/flamestrike,if=talent.flame_patch.enabled&active_enemies>2|active_enemies>9
            if (Aimsharp.CanCast("Flamestrike", "player"))
            {
                if (TalentFlamePatch && EnemiesNearTarget > 2 || EnemiesNearTarget > 9)
                {
                    Aimsharp.Cast("fs cursor");
                    return true;
                }
            }

            //actions.standard_rotation+=/fireball
            if (Aimsharp.CanCast("Fireball") && !Moving)
            {
                Aimsharp.Cast("Fireball");
                return true;
            }

            //actions.standard_rotation+=/scorch
            if (Aimsharp.CanCast("Scorch"))
            {
                Aimsharp.Cast("Scorch");
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
