using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using AimsharpWow.API;

namespace AimsharpWow.Modules
{
    public class AutoSelfHeal : Plugin
    {
        public override void LoadSettings()
        {
            Settings.Add(new Setting("Healthstone HP %", 0, 100, 25));
            Settings.Add(new Setting("Healing Potion HP %", 0, 100, 15));
            Settings.Add(new Setting("Use Class Abilities", true));
            List<string> ClassList = new List<string>(new string[] { "Hunter", "Demon Hunter", "Priest", "Shaman", "Paladin", "Death Knight", "Druid", "Warlock", "Mage", "Rogue", "Monk", "Warrior" });
            Settings.Add(new Setting("Class", ClassList, "Hunter"));
            Settings.Add(new Setting("Class Ability HP %", 0, 100, 35));
        }

        public override void Initialize()
        {
            Aimsharp.PrintMessage("Simple self-heal plugin loaded.");

            if (GetDropDown("Class") == "Hunter")
            {
                Spellbook.Add("Exhilaration");
            }

            if (GetDropDown("Class") == "Rogue")
            {
                Spellbook.Add("Crimson Vial");
                Spellbook.Add("Evasion");
            }

            if (GetDropDown("Class") == "Priest")
            {
                Spellbook.Add("Desperate Prayer");
            }

            if (GetDropDown("Class") == "Death Knight")
            {
                Spellbook.Add("Death Strike");
            }

            if (GetDropDown("Class") == "Warrior")
            {
                Spellbook.Add("Last Stand");
                Spellbook.Add("Enraged Regeneration");
            }

            Macros.Add("hstone", "/use healthstone");
            Macros.Add("abyssalhp", "/use Abyssal Healing Potion");
            Items.Add("Healthstone");
            Items.Add("Abyssal Healing Potion");
        }

        //Stopwatches to fix wow api bug with potions/healthstones and only using once per combat.
        Stopwatch PotTimer = new Stopwatch();
        Stopwatch HSTimer = new Stopwatch();

        public override bool CombatTick()
        {
            int Health = Aimsharp.Health("player");

            if (GetDropDown("Class") == "Hunter")
            {
                if (Health < GetSlider("Class Ability HP %"))
                {
                    if (Aimsharp.CanCast("Exhilaration", "player"))
                    {
                        Aimsharp.Cast("Exhilaration");
                        return true;
                    }
                }
            }

            if (GetDropDown("Class") == "Rogue")
            {
                if (Health < GetSlider("Class Ability HP %"))
                {
                    if (Aimsharp.CanCast("Crimson Vial", "player"))
                    {
                        Aimsharp.Cast("Crimson Vial");
                        return true;
                    }
                }

                if (Health < GetSlider("Class Ability HP %"))
                {
                    if (Aimsharp.CanCast("Evasion", "player"))
                    {
                        Aimsharp.Cast("Evasion");
                        return true;
                    }
                }
            }

            if (GetDropDown("Class") == "Priest")
            {
                if (Health < GetSlider("Class Ability HP %"))
                {
                    if (Aimsharp.CanCast("Desperate Prayer", "player"))
                    {
                        Aimsharp.Cast("Desperate Prayer");
                        return true;
                    }
                }
            }

            if (GetDropDown("Class") == "Death Knight")
            {
                if (Health < GetSlider("Class Ability HP %"))
                {
                    if (Aimsharp.CanCast("Death Strike"))
                    {
                        Aimsharp.Cast("Death Strike");
                        return true;
                    }
                }
            }

            if (GetDropDown("Class") == "Warrior")
            {
                if (Health < GetSlider("Class Ability HP %"))
                {
                    if (Aimsharp.CanCast("Last Stand", "player"))
                    {
                        Aimsharp.Cast("Last Stand");
                        return true;
                    }
                }

                if (Health < GetSlider("Class Ability HP %"))
                {
                    if (Aimsharp.CanCast("Enraged Regeneration", "player"))
                    {
                        Aimsharp.Cast("Enraged Regeneration");
                        return true;
                    }
                }
            }

            if (Health < GetSlider("Healthstone HP %"))
            {
                if (Aimsharp.CanUseItem("Healthstone", false))
                {
                    if (!HSTimer.IsRunning)
                    {
                        HSTimer.Restart();
                    }
                    if (HSTimer.ElapsedMilliseconds < 1500)
                    {
                        Aimsharp.Cast("hstone", true);
                        return true;
                    }
                }
            }

            if (Health < GetSlider("Healing Potion HP %"))
            {
                if (Aimsharp.CanUseItem("Abyssal Healing Potion", false))
                {
                    if (!PotTimer.IsRunning)
                    {
                        PotTimer.Restart();
                    }
                    if (PotTimer.ElapsedMilliseconds < 1500)
                    {
                        Aimsharp.Cast("abyssalhp", true);
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool OutOfCombatTick()
        {
            if (PotTimer.IsRunning)
                PotTimer.Reset();

            if (HSTimer.IsRunning)
                HSTimer.Reset();

            return false;
        }

    }
}
