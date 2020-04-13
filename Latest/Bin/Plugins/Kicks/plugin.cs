using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AimsharpWow.API; //needed to access Aimsharp API

namespace AimsharpWow.Modules
{
    public class Kicks : Plugin
    {
        public override void LoadSettings()
        {
            Settings.Add(new Setting("Kick at milliseconds remaining", 50, 1500, 200));
            Settings.Add(new Setting("Kick channels after milliseconds", 50, 1500, 500));
            List<string> ClassList = new List<string>(new string[] { "Hunter", "Demon Hunter", "Priest", "Shaman", "Paladin", "Death Knight", "Druid", "Warlock", "Mage", "Rogue", "Monk", "Warrior" });
            Settings.Add(new Setting("Class", ClassList, "Hunter"));
        }

        string Interrupt = "none";
        string InterruptTwo = "none";
        public override void Initialize()
        {
            Aimsharp.PrintMessage("Simple kick plugin.");
            Aimsharp.PrintMessage("Do not use this together with the Arena Kicks plugin!");
            Aimsharp.PrintMessage("Use this macro to hold your kicks for a number of seconds: /xxxxx SaveKicks #");
            Aimsharp.PrintMessage("For example: /xxxxx SaveKicks 5");
            Aimsharp.PrintMessage("will make the bot not kick anything for the next 5 seconds.");

            if (GetDropDown("Class") == "Hunter")
            {
                Interrupt = "Counter Shot";
            }

            if (GetDropDown("Class") == "Rogue")
            {
                Interrupt = "Kick";
            }

            if (GetDropDown("Class") == "Priest")
            {
                Interrupt = "Silence";
            }

            if (GetDropDown("Class") == "Demon Hunter")
            {
                Interrupt = "Disrupt";
            }

            if (GetDropDown("Class") == "Shaman")
            {
                Interrupt = "Wind Shear";
            }

            if (GetDropDown("Class") == "Paladin")
            {
                Interrupt = "Rebuke";
            }

            if (GetDropDown("Class") == "Death Knight")
            {
                Interrupt = "Mind Freeze";
            }

            if (GetDropDown("Class") == "Druid")
            {
                Interrupt = "Skull Bash";
                InterruptTwo = "Solar Beam";
            }

            if (GetDropDown("Class") == "Warlock")
            {
                Interrupt = "Spell Lock";
            }

            if (GetDropDown("Class") == "Mage")
            {
                Interrupt = "Counterspell";
            }

            if (GetDropDown("Class") == "Monk")
            {
                Interrupt = "Spear Hand Strike";
            }

            if (GetDropDown("Class") == "Warrior")
            {
                Interrupt = "Pummel";
            }

            Spellbook.Add(Interrupt);
            Spellbook.Add(InterruptTwo);

            CustomCommands.Add("SaveKicks");
        }

        public override bool CombatTick()
        {
            bool NoKicks = Aimsharp.IsCustomCodeOn("SaveKicks");

            bool IsInterruptable = Aimsharp.IsInterruptable("target");
            int CastingRemaining = Aimsharp.CastingRemaining("target");
            int CastingElapsed = Aimsharp.CastingElapsed("target");
            bool IsChanneling = Aimsharp.IsChanneling("target");
            int KickValue = GetSlider("Kick at milliseconds remaining");
            int KickChannelsAfter = GetSlider("Kick channels after milliseconds");


            if (!NoKicks)
            {
                if (Aimsharp.CanCast(Interrupt))
                {
                    if (IsInterruptable && !IsChanneling && CastingRemaining < KickValue && CastingElapsed > 500)
                    {
                        Aimsharp.Cast(Interrupt, true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast(Interrupt))
                {
                    if (IsInterruptable && IsChanneling && CastingElapsed > KickChannelsAfter)
                    {
                        Aimsharp.Cast(Interrupt, true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast(InterruptTwo))
                {
                    if (IsInterruptable && !IsChanneling && CastingRemaining < KickValue && CastingElapsed > 500)
                    {
                        Aimsharp.Cast(InterruptTwo, true);
                        return true;
                    }
                }

                if (Aimsharp.CanCast(InterruptTwo))
                {
                    if (IsInterruptable && IsChanneling && CastingElapsed > KickChannelsAfter)
                    {
                        Aimsharp.Cast(InterruptTwo, true);
                        return true;
                    }
                }
            }

            return false;
        }

    }
}