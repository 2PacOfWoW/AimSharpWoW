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
    public class SimcraftProtWar : Rotation
    {


        public override void LoadSettings()
        {
            List<string> MajorAzeritePower = new List<string>(new string[] { "Guardian of Azeroth", "Focused Azerite Beam", "Concentrated Flame", "Worldvein Resonance", "Memory of Lucid Dreams", "Blood of the Enemy", "The Unbound Force", "Reaping Flames", "Anima of Death", "None" });
            Settings.Add(new Setting("Major Power", MajorAzeritePower, "None"));

            List<string> Trinkets = new List<string>(new string[] { "Azshara's Font of Power", "Ashvane's Razor Coral", "Pocket-Sized Computation Device", "Galecaller's Boon", "Shiver Venom Relic", "Lurker's Insidious Gift", "Notorious Gladiator's Badge", "Sinister Gladiator's Badge", "Sinister Gladiator's Medallion", "Notorious Gladiator's Medallion", "Vial of Animated Blood", "First Mate's Spyglass", "Jes' Howler", "Ashvane's Razor Coral", "Knot of Ancient Fury", "Ignition Mage's Fuse", "Manifesto of Madness", "Balefire Branch", "Rotcrusted Voodoo Doll", "Forbidden Obsidian Claw", "Generic", "None" });
            Settings.Add(new Setting("Top Trinket", Trinkets, "None"));
            Settings.Add(new Setting("Bot Trinket", Trinkets, "None"));

            List<string> Potions = new List<string>(new string[] { "Potion of Unbridled Fury", "Potion of Empowered Proximity", "Superior Battle Potion of Agility", "Potion of Prolonged Power", "None" });
            Settings.Add(new Setting("Potion Type", Potions, "Potion of Unbridled Fury"));

            List<string> Race = new List<string>(new string[] { "Orc", "Troll", "Dark Iron Dwarf", "Mag'har Orc", "Lightforged Draenei", "Bloodelf", "None" });
            Settings.Add(new Setting("Racial Power", Race, "None"));

            Settings.Add(new Setting("# Icy Citadel Traits", 0, 3, 1));

        }

        string MajorPower;
        string TopTrinket;
        string BotTrinket;
        string RacialPower;

        public override void Initialize()
        {
            // Aimsharp.DebugMode();

            Aimsharp.PrintMessage("Perfect Simcraft Series: Protection Warrior - v 1.0", Color.Blue);
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

            Spellbook.Add("Intercept");
            Spellbook.Add("Avatar");
            Spellbook.Add("Ignore Pain");
            Spellbook.Add("Demoralizing Shout");
            Spellbook.Add("Last Stand");
            Spellbook.Add("Thunder Clap");
            Spellbook.Add("Ravager");
            Spellbook.Add("Shield Block");
            Spellbook.Add("Shield Slam");
            Spellbook.Add("Devastate");
            Spellbook.Add("Victory Rush");
            Spellbook.Add("Revenge");

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

            Buffs.Add("Victorious");
            Buffs.Add("Avatar");
            Buffs.Add("Ignore Pain");
            Buffs.Add("Shield Block");

            Debuffs.Add("Razor Coral");
            Debuffs.Add("Conductive Ink");
            Debuffs.Add("Shiver Venom");

            Items.Add(TopTrinket);
            Items.Add(BotTrinket);
            Items.Add(GetDropDown("Potion Type"));

            Macros.Add(TopTrinket, "/use " + TopTrinket);
            Macros.Add(BotTrinket, "/use " + BotTrinket);
            Macros.Add("potion", "/use " + GetDropDown("Potion Type"));

            CustomCommands.Add("Potions");
            CustomCommands.Add("SaveCooldowns");
            CustomCommands.Add("AOE");
            // CustomCommands.Add("Prepull");

            Macros.Add("RavageSelf", "/cast [@player] Ravager");
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
            bool HasLust = Aimsharp.HasBuff("Bloodlust", "player", false) || Aimsharp.HasBuff("Heroism", "player", false) || Aimsharp.HasBuff("Time Warp", "player", false) || Aimsharp.HasBuff("Ancient Hysteria", "player", false) || Aimsharp.HasBuff("Netherwinds", "player", false) || Aimsharp.HasBuff("Drums of Rage", "player", false);
            int FlameFullRecharge = (int)(Aimsharp.RechargeTime("Concentrated Flame") - GCD + (30000f) * (1f - Aimsharp.SpellCharges("Concentrated Flame")));
            int ShiverVenomStacks = Aimsharp.DebuffStacks("Shiver Venom");

            if (!AOE)
            {
                EnemiesNearTarget = 1;
                EnemiesInMelee = EnemiesInMelee > 0 ? 1 : 0;
            }

            int CDAnimaOfDeathRemains = Aimsharp.SpellCooldown("Anima of Death") - GCD;
            int CDGuardianOfAzerothRemains = Aimsharp.SpellCooldown("Guardian of Azeroth") - GCD;
            bool BuffGuardianOfAzerothUp = Aimsharp.HasBuff("Guardian of Azeroth");
            int CDBloodOfTheEnemyRemains = Aimsharp.SpellCooldown("Blood of the Enemy") - GCD;
            int BuffMemoryOfLucidDreamsRemains = Aimsharp.BuffRemaining("Memory of Lucid Dreams") - GCD;
            bool BuffMemoryOfLucidDreamsUp = BuffMemoryOfLucidDreamsRemains > 0;
            bool DebuffRazorCoralUp = Aimsharp.HasDebuff("Razor Coral");
            int DebuffRazorCoralStacks = Aimsharp.DebuffStacks("Razor Coral");
            bool DebuffConductiveInkUp = Aimsharp.HasDebuff("Conductive Ink");
            int BuffRecklessForceRemains = Aimsharp.BuffRemaining("Reckless Force") - GCD;
            bool BuffRecklessForceUp = BuffRecklessForceRemains > 0;
            int BuffRecklessForceStacks = Aimsharp.BuffStacks("Reckless Force");

            int Rage = Aimsharp.Power("player");
            int MaxRage = Aimsharp.PlayerMaxPower();
            int RageDefecit = MaxRage - Rage;

            bool TalentBoomingVoice = Aimsharp.Talent(6, 1);
            bool TalentUnstoppableForce = Aimsharp.Talent(3, 2);

            int BuffAvatarRemains = Aimsharp.BuffRemaining("Avatar") - GCD;
            bool BuffAvatarUp = BuffAvatarRemains > 0;
            int BuffLastStandRemains = Aimsharp.BuffRemaining("Last Stand") - GCD;
            bool BuffLastStandUp = BuffLastStandRemains > 0;
            int BuffShieldBlockRemains = Aimsharp.BuffRemaining("Shield Block") - GCD;
            bool BuffShieldBlockUp = BuffShieldBlockRemains > 0;

            int CDAvatarRemains = Aimsharp.SpellCooldown("Avatar") - GCD;
            bool CDAvatarReady = CDAvatarRemains <= 10;
            int CDShieldSlamRemains = Aimsharp.SpellCooldown("Shield Slam") - GCD;
            bool CDShieldSlamReady = CDShieldSlamRemains <= 10;
            int CDDemoralizingShoutRemains = Aimsharp.SpellCooldown("Demoralizing Shout") - GCD;
            bool CDDemoralizingShoutReady = CDDemoralizingShoutRemains <= 10;


            if (IsChanneling)
                return false;

            //actions +=/ intercept,if= time = 0
            if (Aimsharp.CanCast("Intercept"))
            {
                if (Time < 1000)
                {
                    Aimsharp.Cast("Intercept");
                    return true;
                }
            }

            //actions +=/ use_items,if= cooldown.avatar.remains <= gcd | buff.avatar.up
            if (Aimsharp.CanUseItem(TopTrinket))
            {
                if (CDAvatarRemains <= GCD || BuffAvatarUp)
                {
                    Aimsharp.Cast(TopTrinket, true);
                    return true;
                }
            }
            if (Aimsharp.CanUseItem(BotTrinket))
            {
                if (CDAvatarRemains <= GCD || BuffAvatarUp)
                {
                    Aimsharp.Cast(BotTrinket, true);
                    return true;
                }
            }

            //generic trinket usage
            if (Aimsharp.CanUseTrinket(0) && TopTrinket == "Generic")
            {
                if (CDAvatarRemains <= GCD || BuffAvatarUp)
                {
                    Aimsharp.Cast("TopTrink", true);
                    return true;
                }
            }

            if (Aimsharp.CanUseTrinket(1) && BotTrinket == "Generic")
            {
                if (CDAvatarRemains <= GCD || BuffAvatarUp)
                {
                    Aimsharp.Cast("BotTrink", true);
                    return true;
                }
            }

            if (RacialPower == "Orc" && Fighting)
            {
                if (Aimsharp.CanCast("Blood Fury", "player"))
                {
                    Aimsharp.Cast("Blood Fury", true);
                    return true;
                }
            }

            if (RacialPower == "Troll" && Fighting)
            {
                if (Aimsharp.CanCast("Berserking", "player"))
                {
                    Aimsharp.Cast("Berserking", true);
                    return true;
                }
            }

            if (RacialPower == "Bloodelf" && Fighting)
            {
                if (Aimsharp.CanCast("Arcane Torrent", "player"))
                {
                    Aimsharp.Cast("Arcane Torrent");
                    return true;
                }
            }

            if (RacialPower == "Lightforged Draenei" && Fighting)
            {
                if (Aimsharp.CanCast("Light's Judgment", "player"))
                {
                    Aimsharp.Cast("Light's Judgment", true);
                    return true;
                }
            }

            if (RacialPower == "Dark Iron Dwarf" && Fighting)
            {
                if (Aimsharp.CanCast("Fireblood", "player"))
                {
                    Aimsharp.Cast("Fireblood", true);
                    return true;
                }
            }

            if (RacialPower == "Mag'har Orc" && Fighting)
            {
                if (Aimsharp.CanCast("Ancestral Call", "player"))
                {
                    Aimsharp.Cast("Ancestral Call", true);
                    return true;
                }
            }

            if (UsePotion && Fighting)
            {
                if (BuffAvatarUp || TargetTimeToDie<25000)
                {
                    if (Aimsharp.CanUseItem(PotionType, false)) // don't check if equipped
                    {
                        Aimsharp.Cast("potion", true);
                        return true;
                    }
                }
            }

            //actions+=/ignore_pain,if=rage.deficit<25+20*talent.booming_voice.enabled*cooldown.demoralizing_shout.ready
            if (Aimsharp.CanCast("Ignore Pain", "player"))
            {
                if (RageDefecit<25+20*(TalentBoomingVoice ? 1 : 0)*(CDDemoralizingShoutReady ? 1 : 0))
                {
                    Aimsharp.Cast("Ignore Pain");
                    return true;
                }
            }

            //actions+=/worldvein_resonance,if=cooldown.avatar.remains<=2
            if (MajorPower == "Worldvein Resonance" && Fighting)
            {
                if (Aimsharp.CanCast("Worldvein Resonance", "player"))
                {
                    if (CDAvatarRemains <= 2000)
                    {
                        Aimsharp.Cast("Worldvein Resonance");
                        return true;
                    }
                }
            }

            if (MajorPower == "Memory of Lucid Dreams" && Fighting)
            {
                if (Aimsharp.CanCast("Memory of Lucid Dreams", "player"))
                {
                    Aimsharp.Cast("Memory of Lucid Dreams");
                    return true;
                }
            }

            if (MajorPower == "Concentrated Flame")
            {
                if (Aimsharp.CanCast("Concentrated Flame") && FlameFullRecharge < GCDMAX)
                {
                    Aimsharp.Cast("Concentrated Flame");
                    return true;
                }
            }

            //actions+=/last_stand,if=cooldown.anima_of_death.remains<=2
            if (MajorPower == "Anima of Death")
            {
                if (Aimsharp.CanCast("Last Stand", "player") && CDAnimaOfDeathRemains<= 2000)
                {
                    Aimsharp.Cast("Last Stand");
                    return true;
                }
            }

            if (Aimsharp.CanCast("Avatar", "player"))
            {
                Aimsharp.Cast("Avatar");
                return true;
            }

            if (Aimsharp.CanCast("Victory Rush") && Aimsharp.HasBuff("Victorious"))
            {
                Aimsharp.Cast("Victory Rush");
                return true;
            }

            if (EnemiesInMelee >= 3)
            {
                //actions.aoe = thunder_clap
                if (Aimsharp.CanCast("Thunder Clap", "player"))
                {
                    Aimsharp.Cast("Thunder Clap");
                    return true;
                }

                //actions.aoe+=/demoralizing_shout,if=talent.booming_voice.enabled
                if (Aimsharp.CanCast("Demoralizing Shout", "player") && TalentBoomingVoice)
                {
                    Aimsharp.Cast("Demoralizing Shout");
                    return true;
                }

                //actions.aoe+=/anima_of_death,if=buff.last_stand.up
                if (MajorPower == "Anima of Death" && Fighting)
                {
                    if (Aimsharp.CanCast("Anima of Death", "player"))
                    {
                        if (BuffLastStandUp)
                        {
                            Aimsharp.Cast("Anima of Death");
                            return true;
                        }
                    }
                }

                //actions.aoe+=/dragon_roar
                if (Aimsharp.CanCast("Dragon Roar", "player"))
                {
                    Aimsharp.Cast("Dragon Roar");
                    return true;
                }

                //actions.aoe+=/revenge
                if (Aimsharp.CanCast("Revenge", "player"))
                {
                    Aimsharp.Cast("Revenge");
                    return true;
                }

                //actions.aoe+=/ravager
                if (Aimsharp.CanCast("Ravager", "player"))
                {
                    Aimsharp.Cast("RavageSelf");
                    return true;
                }

                //actions.aoe+=/shield_block,if=cooldown.shield_slam.ready&buff.shield_block.down
                if (Aimsharp.CanCast("Shield Block", "player") && CDShieldSlamReady && !BuffShieldBlockUp)
                {
                    Aimsharp.Cast("Shield Block");
                    return true;
                }

                //actions.aoe+=/shield_slam
                if (Aimsharp.CanCast("Shield Slam"))
                {
                    Aimsharp.Cast("Shield Slam");
                    return true;
                }
            }

            //actions.st=thunder_clap,if=spell_targets.thunder_clap=2&talent.unstoppable_force.enabled&buff.avatar.up
            if (Aimsharp.CanCast("Thunder Clap", "player"))
            {
                if (EnemiesInMelee == 2 && BuffAvatarUp && TalentUnstoppableForce)
                {
                    Aimsharp.Cast("Thunder Clap");
                    return true;
                }
            }

            //actions.st+=/shield_block,if=cooldown.shield_slam.ready&buff.shield_block.down
            if (Aimsharp.CanCast("Shield Block", "player") && CDShieldSlamReady && !BuffShieldBlockUp)
            {
                Aimsharp.Cast("Shield Block");
                return true;
            }

            //actions.st+=/shield_slam,if=buff.shield_block.up
            if (Aimsharp.CanCast("Shield Slam") && BuffShieldBlockUp)
            {
                Aimsharp.Cast("Shield Slam");
                return true;
            }

            //actions.st+=/thunder_clap,if=(talent.unstoppable_force.enabled&buff.avatar.up)
            if (Aimsharp.CanCast("Thunder Clap", "player"))
            {
                if (BuffAvatarUp && TalentUnstoppableForce)
                {
                    Aimsharp.Cast("Thunder Clap");
                    return true;
                }
            }

            //actions.st+=/demoralizing_shout,if=talent.booming_voice.enabled
            if (Aimsharp.CanCast("Demoralizing Shout", "player") && TalentBoomingVoice)
            {
                Aimsharp.Cast("Demoralizing Shout");
                return true;
            }

            //actions.st+=/anima_of_death,if=buff.last_stand.up
            if (MajorPower == "Anima of Death" && Fighting)
            {
                if (Aimsharp.CanCast("Anima of Death", "player"))
                {
                    if (BuffLastStandUp)
                    {
                        Aimsharp.Cast("Anima of Death");
                        return true;
                    }
                }
            }

            //actions.st+=/shield_slam
            if (Aimsharp.CanCast("Shield Slam"))
            {
                Aimsharp.Cast("Shield Slam");
                return true;
            }

            //actions.st+=/use_item,name=ashvanes_razor_coral,target_if=debuff.razor_coral_debuff.stack=0
            if (Aimsharp.CanUseItem("Ashvane's Razor Coral"))
            {
                if (!DebuffRazorCoralUp)
                {
                    Aimsharp.Cast("Ashvane's Razor Coral", true);
                    return true;
                }
            }

            //actions.st+=/use_item,name=ashvanes_razor_coral,if=debuff.razor_coral_debuff.stack>7&(cooldown.avatar.remains<5|buff.avatar.up)
            if (Aimsharp.CanUseItem("Ashvane's Razor Coral"))
            {
                if (DebuffRazorCoralStacks > 7 && (CDAvatarRemains < 5000 || BuffAvatarUp))
                {
                    Aimsharp.Cast("Ashvane's Razor Coral", true);
                    return true;
                }
            }

            //actions.st+=/dragon_roar
            if (Aimsharp.CanCast("Dragon Roar", "player"))
            {
                Aimsharp.Cast("Dragon Roar");
                return true;
            }

            if (Aimsharp.CanCast("Thunder Clap", "player"))
            {
                Aimsharp.Cast("Thunder Clap");
                return true;
            }

            if (Aimsharp.CanCast("Revenge", "player"))
            {
                Aimsharp.Cast("Revenge");
                return true;
            }

            if (Aimsharp.CanCast("Ravager", "player"))
            {
                Aimsharp.Cast("RavageSelf");
                return true;
            }

            if (Aimsharp.CanCast("Devastate"))
            {
                Aimsharp.Cast("Devastate");
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
