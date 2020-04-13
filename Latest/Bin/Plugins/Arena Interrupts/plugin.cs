using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AimsharpWow.API; //needed to access Aimsharp API

namespace AimsharpWow.Modules
{
    public class ArenaKicks : Plugin
    {

        string[] immunes = { "Divine Shield" };
        string[] physical_immunes = { "Blessing of Protection" };
        string[] spell_immunes = { "Nether Ward", "Grounding Totem Effect", "Mass Spell Reflection", "Anti-Magic Shell" };
       

        public override void LoadSettings()
        {
            Settings.Add(new Setting("Kick at milliseconds remaining", 100, 1500, 200));
            Settings.Add(new Setting("Kick channels after milliseconds", 50, 2000, 500));
            List<string> ClassList = new List<string>(new string[] { "Hunter", "Demon Hunter", "Priest", "Shaman", "Paladin", "Death Knight", "Druid", "Warlock", "Mage", "Rogue", "Monk", "Warrior" });
            Settings.Add(new Setting("Class", ClassList, "Hunter"));
        }

        List<string> Interrupts = new List<string>();
        List<string> Specials= new List<string>();
        string Class = "";

        public override void Initialize()
        {
            Class = GetDropDown("Class");

            Aimsharp.PrintMessage("Arena Interrupts Plugin");
            Aimsharp.PrintMessage("This plugin will only work inside arenas!");
            Aimsharp.PrintMessage("Do not use this together with the normal Kicks plugin!");
            Aimsharp.PrintMessage("Use this macro to hold your kicks for a number of seconds: /xxxxx SaveKicks #");
            Aimsharp.PrintMessage("For example: /xxxxx SaveKicks 5");
            Aimsharp.PrintMessage("will make the bot not kick anything for the next 5 seconds.");




            if (Class == "Hunter")
            {
                Interrupts.Add("Counter Shot");
            }

            if (Class == "Rogue")
            {
                Interrupts.Add("Kick");
            }

            if (Class == "Priest")
            {
                Interrupts.Add("Silence");
            }

            if (Class == "Demon Hunter")
            {
                Interrupts.Add("Disrupt");
            }

            if (Class == "Shaman")
            {
                Interrupts.Add("Wind Shear");
                Specials.Add("Grounding Totem");
                Specials.Add("Lightning Lasso");
            }

            if (Class == "Paladin")
            {
                Interrupts.Add("Rebuke");
            }

            if (Class == "Death Knight")
            {
                Interrupts.Add("Mind Freeze");
            }

            if (Class == "Druid")
            {
                Interrupts.Add("Skull Bash");
                Interrupts.Add("Solar Beam");
            }

            if (Class == "Warlock")
            {
                Interrupts.Add("Spell Lock");
                Specials.Add("Mortal Coil");
            }

            if (Class == "Mage")
            {
                Interrupts.Add("Counterspell");
            }

            if (Class == "Monk")
            {
                Interrupts.Add("Spear Hand Strike");
            }

            if (Class == "Warrior")
            {
                Interrupts.Add("Pummel");
            }

            foreach (string Interrupt in Interrupts)
            {
                Spellbook.Add(Interrupt);
                Macros.Add(Interrupt + "1", "/cast [@arena1] " + Interrupt);
                Macros.Add(Interrupt + "2", "/cast [@arena2] " + Interrupt);
                Macros.Add(Interrupt + "3", "/cast [@arena3] " + Interrupt);
            }

            foreach (string Special in Specials)
            {
                Spellbook.Add(Special);
                Macros.Add(Special + "1", "/cast [@arena1] " + Special);
                Macros.Add(Special + "2", "/cast [@arena2] " + Special);
                Macros.Add(Special + "3", "/cast [@arena3] " + Special);
            }

            foreach (string immune in immunes)
            {
                Buffs.Add(immune);
            }

            foreach (string spell_immune in spell_immunes)
            {
                Buffs.Add(spell_immune);
            }

            foreach (string physical_immune in physical_immunes)
            {
                Buffs.Add(physical_immune);
            }

            CustomCommands.Add("SaveKicks");
        }

        int[] CCSpells =
        {
            
            118, //polymorphs
            161355,
            161354,
            161353,
            126819,
            61780,
            161372,
            61721,
            61305,
            28272,
            28271,
            277792,
            277787,
            51514, // hexes
            211015,
            211010,
            211004,
            210873,
            269352,
            277778,
            277784,
            20066, // repentance
            113724, // Ring of Frost
            209753, // cyclones
            33786,
            605, // mind control
            198898, // song of chi-ji
            
            
        };

        int[] SmallCCSpells =
        {
            5782, // fear
            30283, //shadowfury
            2637, //hibernate
        };

        int[] BigDamageSpells =
        {
            116858, // chaos bolt
            274283, // full moon
            203286, // greater pyroblast
            265187, // demonic tyrant
            30451, // arcane blast
            205021, //ray of frost
            305483, //lightning lasso
            234153, //drain life
            295261, //focused azerite beams
            295258,
            299336,
            299338
        };

        int[] SmallDamageSpells =
        {
            30108, // unstable affliction
            34914, // vampiric touch
            105174, // hand of guldan
            198013, //eye beam
            8092, //mind blast
            15407, //mind flay
            258925, //fel barrage
        };

        int[] SpecialSpells =
        {
            32375, // mass dispel
            982, // revive pet
            111771, //demonic gateway
            191634, // stormkeeper
            228260, //void eruption
        };

        int[] BigHeals =
        {
            77472, // healing wave
            188070, // healing surge
            186263, // shadow mend
            8004, // healing surge
            8936, // regrowth
            82326, // holy light
            227344, // surging mist
            289022, // nourish
            1064, // chain heal
            289666, //greater heal
            740, //tranquility
            115175, //soothing mist
        };

        int[] SmallHeals =
        {
            47540, //penance
            116670, // vivify
            19750, // flash of light
        };






        public override bool CombatTick()
        {
            bool[] IsInterruptable = { false, false, false };
            bool[] IsChanneling = { false, false, false };
            int[] CastingRemaining = { 0, 0, 0 };
            int[] CastingElapsed = { 0, 0, 0 };
            int[] RangeToEnemy = { 0, 0, 0 };
            int[] RangeToAlly = { 0, 0, 0 };
            int[] AllyHP = { 0, 0, 0 };
            int[] EnemyHP = { 0, 0, 0 };
            int[] EnemyCastID = { 0, 0, 0 };
            bool[] Immune = { false, false, false };
            bool[] PhysicalImmune = { false, false, false };
            bool[] SpellImmune = { false, false, false };
            int LowestEnemyHP = 100;
            int LowestAllyHP = 100;
            bool HasPet = false;
            string Spec = "";
            string[] EnemySpecs = { "", "", "" };

            Spec = Aimsharp.GetSpec("player");

            HasPet = Aimsharp.PlayerHasPet() && Aimsharp.Health("pet") > 0;
            bool NoKicks = Aimsharp.IsCustomCodeOn("SaveKicks");
            int KickValue = GetSlider("Kick at milliseconds remaining");
            int KickChannelsAfter = GetSlider("Kick channels after milliseconds");
 
            for (int i = 0; i < 3; i++)
            {
                IsInterruptable[i] = Aimsharp.IsInterruptable("arena" + (i + 1).ToString());
                IsChanneling[i] = Aimsharp.IsChanneling("arena" + (i + 1).ToString());
                CastingRemaining[i] = Aimsharp.CastingRemaining("arena" + (i + 1).ToString());
                CastingElapsed[i] = Aimsharp.CastingElapsed("arena" + (i + 1).ToString());
                RangeToEnemy[i] = Aimsharp.Range("arena" + (i + 1).ToString());
                EnemyCastID[i] = Aimsharp.CastingID("arena" + (i + 1).ToString());
                EnemySpecs[i] = Aimsharp.GetSpec("arena" + (i + 1).ToString());

                if (i == 0)
                {
                    AllyHP[i] = Aimsharp.Health("player");
                    LowestAllyHP = AllyHP[i];
                }
                if (i > 0)
                {
                    AllyHP[i] = Aimsharp.Health("party" + i.ToString());
                    RangeToAlly[i] = Aimsharp.Range("party" + i.ToString());
                    if (AllyHP[i] > 0)
                    {
                        if (AllyHP[i] < LowestAllyHP)
                            LowestAllyHP = AllyHP[i];
                    }
                }

                EnemyHP[i] = Aimsharp.Health("arena" + (i + 1).ToString());

                if (EnemyHP[i] > 0)
                {
                    if (EnemyHP[i] < LowestEnemyHP)
                        LowestEnemyHP = EnemyHP[i];
                }


                foreach (string immune in immunes)
                {
                    if (Aimsharp.HasBuff(immune, "arena" + (i + 1).ToString(), false))
                    {
                        Immune[i] = true;
                        break;
                    }
                }

                foreach (string physical_immune in physical_immunes)
                {
                    if (Aimsharp.HasBuff(physical_immune, "arena" + (i + 1).ToString(), false))
                    {
                        PhysicalImmune[i] = true;
                        break;
                    }
                }

                foreach (string spell_immune in spell_immunes)
                {
                    if (Aimsharp.HasBuff(spell_immune, "arena" + (i + 1).ToString(), false))
                    {
                        SpellImmune[i] = true;
                        break;
                    }
                }

                if (Class == "Warlock" || Class == "Mage" || Class == "Death Knight" || Class == "Shaman" || Class == "Priest")
                {
                    if (Immune[i] || SpellImmune[i])
                    {
                        IsInterruptable[i] = false;
                    }
                }
                else if (Spec != "Druid: Balance")
                {
                    if (Immune[i] || PhysicalImmune[i])
                    {
                        IsInterruptable[i] = false;
                    }
                }
                if (Class == "Warlock")
                {
                    if (!HasPet)
                    {
                        IsInterruptable[i] = false;
                    }
                }

            }

            if (!NoKicks)
            {
                foreach (string Interrupt in Interrupts)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        //always kick cc spells, big damage spells, and special spells
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50)
                            {
                                if (CCSpells.Contains(EnemyCastID[i]) || BigDamageSpells.Contains(EnemyCastID[i]) || SpecialSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200)
                            {
                                if (CCSpells.Contains(EnemyCastID[i]) || BigDamageSpells.Contains(EnemyCastID[i]) || SpecialSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick small ccs if an enemy or ally is low hp
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && (LowestEnemyHP < 50 || LowestAllyHP < 50))
                            {
                                if (SmallCCSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && (LowestEnemyHP < 50 || LowestAllyHP < 50))
                            {
                                if (SmallCCSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick big heals if an enemy is low
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && LowestEnemyHP < 66)
                            {
                                if (BigHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && LowestEnemyHP < 66)
                            {
                                if (BigHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick small heals if an enemy is extremely low
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && LowestEnemyHP < 20)
                            {
                                if (SmallHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && LowestEnemyHP < 20)
                            {
                                if (SmallHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick small damage spells if nothing else to kick or ally is extremely low
                        bool NoPriorityCasters = !(EnemySpecs.Contains("Mage: Fire") || EnemySpecs.Contains("Mage: Frost") || EnemySpecs.Contains("Mage: Arcane") || EnemySpecs.Contains("Warlock: Destruction") || EnemySpecs.Contains("Druid: Balance"));
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && (LowestAllyHP < 10 || NoPriorityCasters))
                            {
                                if (SmallDamageSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && (LowestAllyHP < 10 || NoPriorityCasters))
                            {
                                if (SmallDamageSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }
                    }



                }
            }

            return false;
        }

        public override bool OutOfCombatTick()
        {
            

            bool[] IsInterruptable = { false, false, false };
            bool[] IsChanneling = { false, false, false };
            int[] CastingRemaining = { 0, 0, 0 };
            int[] CastingElapsed = { 0, 0, 0 };
            int[] RangeToEnemy = { 0, 0, 0 };
            int[] RangeToAlly = { 0, 0, 0 };
            int[] AllyHP = { 0, 0, 0 };
            int[] EnemyHP = { 0, 0, 0 };
            int[] EnemyCastID = { 0, 0, 0 };
            bool[] Immune = { false, false, false };
            bool[] PhysicalImmune = { false, false, false };
            bool[] SpellImmune = { false, false, false };
            int LowestEnemyHP = 100;
            int LowestAllyHP = 100;
            bool HasPet = false;
            string Spec = "";
            string[] EnemySpecs = { "", "", "" };

            Spec = Aimsharp.GetSpec("player");

            HasPet = Aimsharp.PlayerHasPet() && Aimsharp.Health("pet") > 0;
            bool NoKicks = Aimsharp.IsCustomCodeOn("SaveKicks");
            int KickValue = GetSlider("Kick at milliseconds remaining");
            int KickChannelsAfter = GetSlider("Kick channels after milliseconds");

            for (int i = 0; i < 3; i++)
            {
                IsInterruptable[i] = Aimsharp.IsInterruptable("arena" + (i + 1).ToString());
                IsChanneling[i] = Aimsharp.IsChanneling("arena" + (i + 1).ToString());
                CastingRemaining[i] = Aimsharp.CastingRemaining("arena" + (i + 1).ToString());
                CastingElapsed[i] = Aimsharp.CastingElapsed("arena" + (i + 1).ToString());
                RangeToEnemy[i] = Aimsharp.Range("arena" + (i + 1).ToString());
                EnemyCastID[i] = Aimsharp.CastingID("arena" + (i + 1).ToString());
                EnemySpecs[i] = Aimsharp.GetSpec("arena" + (i + 1).ToString());

                if (i == 0)
                {
                    AllyHP[i] = Aimsharp.Health("player");
                    LowestAllyHP = AllyHP[i];
                }
                if (i > 0)
                {
                    AllyHP[i] = Aimsharp.Health("party" + i.ToString());
                    RangeToAlly[i] = Aimsharp.Range("party" + i.ToString());
                    if (AllyHP[i] > 0)
                    {
                        if (AllyHP[i] < LowestAllyHP)
                            LowestAllyHP = AllyHP[i];
                    }
                }

                EnemyHP[i] = Aimsharp.Health("arena" + (i + 1).ToString());

                if (EnemyHP[i] > 0)
                {
                    if (EnemyHP[i] < LowestEnemyHP)
                        LowestEnemyHP = EnemyHP[i];
                }


                foreach (string immune in immunes)
                {
                    if (Aimsharp.HasBuff(immune, "arena" + (i + 1).ToString(), false))
                    {
                        Immune[i] = true;
                        break;
                    }
                }

                foreach (string physical_immune in physical_immunes)
                {
                    if (Aimsharp.HasBuff(physical_immune, "arena" + (i + 1).ToString(), false))
                    {
                        PhysicalImmune[i] = true;
                        break;
                    }
                }

                foreach (string spell_immune in spell_immunes)
                {
                    if (Aimsharp.HasBuff(spell_immune, "arena" + (i + 1).ToString(), false))
                    {
                        SpellImmune[i] = true;
                        break;
                    }
                }

                if (Class == "Warlock" || Class == "Mage" || Class == "Death Knight" || Class == "Shaman" || Class == "Priest")
                {
                    if (Immune[i] || SpellImmune[i])
                    {
                        IsInterruptable[i] = false;
                    }
                }
                else if (Spec != "Druid: Balance")
                {
                    if (Immune[i] || PhysicalImmune[i])
                    {
                        IsInterruptable[i] = false;
                    }
                }
                if (Class == "Warlock")
                {
                    if (!HasPet)
                    {
                        IsInterruptable[i] = false;
                    }
                }

            }

            if (!NoKicks)
            {
                foreach (string Interrupt in Interrupts)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        //always kick cc spells, big damage spells, and special spells
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50)
                            {
                                if (CCSpells.Contains(EnemyCastID[i]) || BigDamageSpells.Contains(EnemyCastID[i]) || SpecialSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200)
                            {
                                if (CCSpells.Contains(EnemyCastID[i]) || BigDamageSpells.Contains(EnemyCastID[i]) || SpecialSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick small ccs if an enemy or ally is low hp
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && (LowestEnemyHP < 50 || LowestAllyHP < 50))
                            {
                                if (SmallCCSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && (LowestEnemyHP < 50 || LowestAllyHP < 50))
                            {
                                if (SmallCCSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick big heals if an enemy is low
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && LowestEnemyHP < 66)
                            {
                                if (BigHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && LowestEnemyHP < 66)
                            {
                                if (BigHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick small heals if an enemy is extremely low
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && LowestEnemyHP < 20)
                            {
                                if (SmallHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && LowestEnemyHP < 20)
                            {
                                if (SmallHeals.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        //kick small damage spells if nothing else to kick or ally is extremely low
                        bool NoPriorityCasters = !(EnemySpecs.Contains("Mage: Fire") || EnemySpecs.Contains("Mage: Frost") || EnemySpecs.Contains("Mage: Arcane") || EnemySpecs.Contains("Warlock: Destruction") || EnemySpecs.Contains("Druid: Balance"));
                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (!IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > 500 && CastingRemaining[i] < KickValue && CastingRemaining[i] >= 50 && (LowestAllyHP < 10 || NoPriorityCasters))
                            {
                                if (SmallDamageSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }

                        if (Aimsharp.CanCast(Interrupt, "arena" + (i + 1).ToString()))
                        {
                            if (IsChanneling[i] && IsInterruptable[i] && CastingElapsed[i] > KickChannelsAfter && CastingRemaining[i] >= 200 && (LowestAllyHP < 10 || NoPriorityCasters))
                            {
                                if (SmallDamageSpells.Contains(EnemyCastID[i]))
                                {
                                    Aimsharp.Cast(Interrupt + (i + 1).ToString(), true);
                                    return true;
                                }
                            }
                        }
                    }



                }
            }

            return false;
        }

    }
}