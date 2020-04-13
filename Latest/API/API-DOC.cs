
//API Documentation
//Aimsharp class accessed through using AimsharpWow.API;

//AoE spells can only be casted with [@cursor] or [@player] macros. See Meteor in example rotation.
//Bot uses numpad numbers and F1-F12 and '[' and ']' keys internally so do NOT bind those to anything.
//Load and start the rotation BEFORE logging in! After logging in, type /reload in chat to automatically load the bot's macros/keybinds and you are good to go. <---- IMPORTANT

//There are a few slash commands that can be used in game manually.
//Aimsharp slash commands always start with the first 5 letters of the addon name chosen, lower case.
//For example, if your addon is named DragonHunterHelper, the commands would start with "/drago"
//Currently there is: 
//  "/drago toggle" - will pause and unpause the rotation in game. Use this to quickly pause and resume the bot.
//  "/drago wait #" - will pause the rotation for # of seconds and then automatically unpause, for example /drago wait 3 would pause the bot for 3 seconds
//  "/drago CUSTOM_COMMAND - will toggle a custom command on/off. The CUSTOM_COMMAND must be added during Initialize() with CustomCommands.Add("CUSTOM_COMMAND")
//                         - The command can then trigger actions in the rotation or plugin using Aimsharp.IsCustomCommandOn("CUSTOM_COMMAND")
//  "/drago CUSTOM_COMMAND # - does the same thing as above except will automatically toggle the custom command off after # seconds.
//      Check included example rotation to see a rotation that implements custom commands.

class Aimsharp
{
    void PrintMessage(string text, Color color);
    // Print's a message to the bot's console box
    // Should only be used in Initialize or for debugging since printing every tick can be slow.

    bool CanCast(string SpellName, string unit = "target", bool CheckRange = true, bool CheckCasting = false);
    // Returns true if the spell can be cast on the unit (target by default). Optional parameters to check different units, range, and if player is casting.
    // implemented for player, target, focus, pet, arena1-3, party1-4, raid1-40, boss1-4
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");

    void Cast(string Name, bool QuickDelay = false);
    // Tries to cast the specified spell from Spellbook or macro from the Macro list. QuickDelay can be used for the bot to spam the key faster instead of waiting for the normal key delay.
    // Spells must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");
    // Macros must be initialized in the rotation/plugin's Initialize() method with Macros.Add. For Example: Macros.Add("interrupt 1","/counterspell [@arena1]");

    int SpellCooldown(string Spell);
    // Returns the cooldown remaining of an ability in milliseconds, including any GCD or interrupted time
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");

    int SpellCharges(string Spell);
    // Returns the number of current charges of an ability that has charges.  Always 0 if ability does not use charges.
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");

    int MaxCharges(string Spell);
    // Returns the maximum number of charges an ability can have.  Always 0 if ability does not use charges.
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");

    int RechargeTime(string Spell);
    // Returns the time remaining for an ability to gain another charge.  Always 0 if ability does not use charges.
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");

    bool SpellEnabled(string Spell);
    // Returns false if the spell is active (Stealth, Shadowmeld, Presence of Mind, etc) and the cooldown will begin as soon as the spell is used/cancelled; otherwise true.
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");

    bool SpellInRange(string Spell, string Unit);
    // Returns true if Unit is in range of ability; otherwise false.
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");
    // implemented for target, focus, pet, arena1-3, party1-4, raid1-40, boss1-4

    int GetMapID();
    // Returns the current uiMapID for the player. See: https://wow.gamepedia.com/UiMapID

    bool PlayerIsPvP();
    // Returns true if player is PvP enabled.

    bool PlayerIsDead();
    // Returns true if player is dead or a ghost.

    bool PlayerIsMounted();
    // Returns true if player is mounted.

    bool PlayerHasPet();
    // Returns true if player has a pet.

    bool PlayerInVehicle();
    // Returns true if player is in a vehicle.

    bool PlayerIsMoving();
    // Returns true if player is moving.

    bool PlayerIsOutdoors();
    // Returns true if player is outdoors.

    bool InRaid();
    // Returns true if player is in a raid.

    bool InParty();
    // Returns true if player is in a party/group

    bool TargetIsEnemy();
    // Returns true if target is hostile to player.

    int GetPlayerLevel();
    // Returns player's level.

    string GetPlayerRace();
    // Returns player's race.
    // "human", "dwarf", "nightelf", "gnome", "draenei", "pandaren", "orc", "scourge", "tauren", "troll", "bloodelf", "goblin", "worgen", "voidelf", "lightforgeddraenei", "highmountaintauren", "nightborne", "zandalaritroll", "magharorc", "kultiran", "darkirondwarf", "vulpera", "mechagnome"

    bool InCombat(string unit = "player");
    // Returns true if unit is in combat. 
    // implemented for "player", "target", "focus", "pet", "party1-4", "raid1-40", "arena1-3"

    string GetSpec(string unit = "player");
    // Returns unit's specialization in the format of "class: spec". For example: "Death Knight: Blood" or "Paladin: Holy". 
    // implemented for "player", "target", "focus", "party1-4", "raid1-40", "arena1-3"

    int Health(string unit = "player");
    // Returns unit's health percentage, rounds up if under 1% but not 0 (dead)
    // implemented for "player", "target", "focus", "pet", "party1-4", "raid1-40", "arena1-3", "boss1-4"

    int Power(string unit = "player");
    // Returns unit's power (rage, energy etc.)
    // implemented for "player", "target", "focus", "pet", "party1-4", "raid1-40", "arena1-3"

    int TargetCurrentHP();
    // Returns the target's current HP in thousands (NOT percentage, rounds up if under 1000)
    // 3000 HP would return 3

    int TargetMaxHP();
    // Returns the target's max HP in thousands (NOT percentage, rounds up if under 1000)

    int PlayerSecondaryPower();
    // Returns player's current secondary power (how many combo points, chi, etc..)

    int EnemiesInMelee();
    // Returns the number of hostile enemies in melee range

    int PlayerMaxPower();
    // Returns player's max power (max energy, max rage etc..)

    int Mana(string unit = "player");
    // Returns unit's mana percentage. 
    // implemented for "player", "target", "focus", "pet", "party1-4", "raid1-40", "arena1-3", "boss1-4"

    int Range(string unit = "target");
    // Returns the range to the unit. 
    // implemented for "target", "focus", "pet", "party1-4", "raid1-40", "arena1-3", "boss1-4"

    int GroupSize();
    // Returns the number of players including the player in the current group or raid.
    // Returns 0 if not in a group or raid.

    string LastCast();
    // Returns the name of the last successfully casted ability by "player" if it is in the bot's Spellbook.
    // Spell must be initialized in the rotation/plugin's Initialize() method with Spellbook.Add. For Example: Spellbook.Add("Fists of Fury");

    string EnemySpellCast();
    // Returns the name of the last successfully casted ability by any hostile enemies if it is in the bot's EnemySpells list.
    // Spell must be initialized in the rotation/plugin's Initialize() method with EnemySpells.Add. For Example: EnemySpells.Add("Freezing Trap");

    int CastingID(string unit = "target");
    // returns the WoW spellid the unit is currently casting or channeling.
    // implemented for "player","target","focus","pet","arena1-3","party1-4","boss1-4"

    bool IsInterruptable(string unit = "target");
    // returns true if unit is currently interruptable
    // implemented for "player","target","focus","pet","arena1-3","party1-4","boss1-4"

    bool IsChanneling(string unit = "target");
    // returns true if unit is currently channeling
    // implemented for "player","target","focus","pet","arena1-3","party1-4","boss1-4"

    int CastingElapsed(string unit = "target");
    // returns the elapsed time in milliseconds of the unit's current spell cast or channel.
    // implemented for "player","target","focus","pet","arena1-3","party1-4","boss1-4"

    int CastingRemaining(string unit = "target");
    // returns the remaining time in milliseconds of the unit's current spell cast or channel.
    // implemented for "player","target","focus","pet","arena1-3","party1-4","boss1-4"

    float Haste();
    // returns the player's haste percentage (eg 39.23652)

    float Crit();
    // returns the player's crit percentage

    bool LineOfSighted();
    // returns true if the last spell cast by player got line of sight error.

    bool NotFacing();
    // returns true if the last spell cast by player got not facing error.

    int GCD();
    // returns the remaining time in milliseconds of the player's current active global cooldown

    bool HasBuff(string BuffName, string unit = "player", bool ByPlayer = true, string type = "");
    // returns true if unit has the buff. can match for only buffs applied by the player and also types "magic", "disease", "poison", "curse", "enrage", "physical"
    // BuffName must be initialized in the rotation/plugin's Initialize() method with Buffs.Add. For Example: Buffs.Add("Blessing of Protection");
    // implemented for "player", "target", "focus", "pet", "arena1-3", "party1-4", "raid1-40", "boss1-4"

    int BuffStacks(string BuffName, string unit = "player", bool ByPlayer = true);
    // returns the number of stacks of a buff a unit has. 0 if unit does not have the buff. can match for only buffs applied by the player.
    // BuffName must be initialized in the rotation/plugin's Initialize() method with Buffs.Add. For Example: Buffs.Add("Blessing of Protection");
    // implemented for "player", "target", "focus", "pet", "arena1-3", "party1-4", "raid1-40", "boss1-4"
    // *note*: This IS ABLE to return the number of stacks for Buffs that stack separately, like Barbed Shot focus regen buff.

    int BuffRemaining(string BuffName, string unit = "player", bool ByPlayer = true, string type = "");
    // returns the remaining duration in milliseconds of a buff on a unit.
    // BuffName must be initialized in the rotation/plugin's Initialize() method with Buffs.Add. For Example: Buffs.Add("Blessing of Protection");
    // implemented for "player", "target", "focus", "pet", "arena1-3", "party1-4", "raid1-40", "boss1-4"

    bool HasDebuff(string DebuffName, string unit = "target", bool ByPlayer = true, string type = "");
    // exactly same as HasBuff except for debuffs. see above

    int DebuffStacks(string DebuffName, string unit = "target", bool ByPlayer = true);
    // exactly same as BuffStacks except for debuffs. see above

    int DebuffRemaining(string DebuffName, string unit = "target", bool ByPlayer = true, string type = "");
    // exactly same as BuffRemaining except for debuffs. see above

    bool Talent(int Row, int Column);
    // Returns true if the talent on the row and column is selected.

    List<int> PvpTalentIDs();
    // Returns a list with the selected pvp spellIDs
    // For example Cover of Darkness; https://www.wowhead.com/spell=227635/ It's spell id would be 227635

    bool CanUseItem(string ItemName, bool CheckIfEquipped = true);
    // Returns true if item is ready to be used. 
    // Item must be initialized in rotation/plugin's Initialize() method with Items.Add. For Example: Items.Add("Hyperthread Wristwraps");

    int ItemCooldown(string ItemName);
    // Returns the item's cooldown remaining in milliseconds.
    // Item must be initialized in rotation/plugin's Initialize() method with Items.Add. For Example: Items.Add("Hyperthread Wristwraps");

    bool IsEquipped(string ItemName);
    // Returns true if the item is equipped.
    // Item must be initialized in rotation/plugin's Initialize() method with Items.Add. For Example: Items.Add("Hyperthread Wristwraps");

    void TargetSelf();
    // Targets the player.

    void TargetParty1(); void TargetParty2(); void TargetParty3(); void TargetParty4();
    // Targets party member 1-4.

    void TargetArena1(); void TargetArena2(); void TargetArena3();
    // Targets arena1-3.

    void TargetBoss1(); void TargetBoss2(); void TargetBoss3(); void TargetBoss4();
    // Targets Boss1-4.

    void TargetRaid(int i);
    // targets raid member i.

    void StopCasting();
    // stops the current cast or channel

    int UnitMaxHP(string unit = "player");
    //finds max hp value (in thousands) for either "player", "boss1", or "focus"

    int UnitCurrentHP(string unit = "player");
    //finds current hp value (in thousands) for either "player", "boss1", or "focus"

    int Dampening();
    // returns the current Dampening % in arena

    int CombatTime();
    // returns the time since current combat first started in milliseconds.

    int RuneCooldown(int RuneIndex);
    // returns the time in milliseconds until the rune at rune index is ready. Only works for Death Knights.

    int TimeUntilRunes(int X);
    // returns the time in milliseconds until X number of runes is ready.

    int EnemiesNearTarget();
    // returns the number of enemies near the target's range. (includes the target)

    bool CanUseTrinket(int slot)
    // returns true if the equipped trinket is ready to be used. 0 for top slot and 1 for bottom slot.

    void DebugMode();
    // Sets Debug mode, will print all attempts to Cast an ability onto the console.

    // undocumented:
    BuffInfoDetailed(string unit, string buffname, bool byplayer); //returns a list of buffinfos
    DebuffInfoDetailed(string unit, string debuffname, bool byplayer); //returns a list of debuffinfos

}



class Rotation
{
    virtual void LoadSettings() { }
    /* Override this to use user adjustable settings for your rotation. See example rotation in Rotations folder
     * Settings.Add(new Setting("Is a mage",true)); //adds a checkbox setting named "Is a mage" that returns a bool initialized to true (checked)
     * Settings.Add(new Setting("Mana %", 1, 100, 75)); //adds a integer slider setting named "Mana %" that returns an int. Minimum slider value 1, maximum value 100, initialized to 75.
     * Settings.Add(new Setting("Race", new List<string>(new string[] { "human", "orc", "nightelf" }), "orc")); //adds a dropdown menu setting named "Race" with 3 options: human, orc, nightelf initialized to orc.
     */

    abstract void Initialize();
    /* All rotations and plugins MUST override Initialize(). 
     * Initialize() is used to add spells/auras to the rotation. 
     * Can be used to print welcome messages, initialize variables depending on settings, etc.. See example rotation in Rotations folder.
     */

    virtual bool CombatTick() { return false; }
    /* Override this to perform actions in combat.  Each tick uses the same scan of the game state.
     * If an action is performed that changes the game state, it is recommended to return true to immediately go to the next tick, updating the game state scan.
     * See example rotation in Rotations folder.
     */

    virtual bool MountedTick() { return false; }
    /* Same as CombatTick() but executes only when mounted. */

    virtual bool OutOfCombatTick() { return false; }
    /* Same as CombatTick() but executes only when out of combat */

    virtual void CleanUp() { }
    /* Always executes once each tick per rotation or plugin, at the end*/

    bool GetCheckBox(string SettingName);
    // Use to retrieve the bool value of a checkbox setting. See example rotation in Rotations folder.

    int GetSlider(string SettingName);
    // Use to retrieve the int value of a slider setting. See example rotation in Rotations folder.

    string GetDropDown(string SettingName);
    // Use to retrieve the string value of a DropDown setting. See example rotation in Rotations folder.
}


class Plugin //Same as Rotation class
{
    virtual void LoadSettings() { }

    abstract void Initialize();

    virtual bool CombatTick() { return false; }

    virtual bool MountedTick() { return false; }

    virtual bool OutOfCombatTick() { return false; }

    virtual void CleanUp() { }

    bool GetCheckBox(string SettingName);

    int GetSlider(string SettingName);

    string GetDropDown(string SettingName);
}
