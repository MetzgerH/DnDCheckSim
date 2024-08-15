using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckSimEngine
{
    /// <summary>
    /// Enumerates the different playable classes in 5e.
    /// </summary>
    public enum PlayerClass
    {
        Barbarian,
        Bard,
        Cleric,
        Druid,
        Fighter,
        Monk,
        Paladin,
        Ranger,
        Rogue,
        Sorcerer,
        Warlock,
        Wizard,
        Max,
    }

    /// <summary>
    /// A set of helper functions to use with PlayerClass.
    /// </summary>
    public static class PlayerClassHelper
    {
        /// <summary>
        /// Converts a PlayerClass enum to a string of the name of it.
        /// </summary>
        /// <param name="playerClass">
        /// The convertee.
        /// </param>
        /// <returns>
        /// <paramref name="playerClass"/> as a string.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The "Max" enum option is for utility, and is such not a real playerclass to be converted into a string.
        /// </exception>
        public static string ToString(PlayerClass playerClass)
        {
            switch (playerClass)
            {
                case PlayerClass.Barbarian:
                    return "Barbarian";
                case PlayerClass.Bard:
                    return "Bard";
                case PlayerClass.Cleric:
                    return "Cleric";
                case PlayerClass.Druid:
                    return "Druid";
                case PlayerClass.Fighter:
                    return "Fighter";
                case PlayerClass.Monk:
                    return "Monk";
                case PlayerClass.Paladin:
                    return "Paladin";
                case PlayerClass.Ranger:
                    return "Ranger";
                case PlayerClass.Rogue:
                    return "Rogue";
                case PlayerClass.Sorcerer:
                    return "Sorcerer";
                case PlayerClass.Warlock:
                    return "Warlock";
                case PlayerClass.Wizard:
                    return "Wizard";
                case PlayerClass.Max:
                    throw new ArgumentOutOfRangeException("Max should not be converted to string");
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Converts a string to a PlayerClass of the same name.
        /// </summary>
        /// <param name="str">
        /// The convertee.
        /// </param>
        /// <returns>
        /// The PlayerClass that <paramref name="str"/> spells out.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If a string is entered that does not match a player class, an error is thrown.
        /// </exception>
        public static PlayerClass FromString(string str)
        {
            if (str == "Barbarian")
                return PlayerClass.Barbarian;
            if (str == "Bard")
                return PlayerClass.Bard;
            if (str == "Cleric")
                return PlayerClass.Cleric;
            if (str == "Druid")
                return PlayerClass.Druid;
            if (str == "Fighter")
                return PlayerClass.Fighter;
            if (str == "Monk")
                return PlayerClass.Monk;
            if (str == "Paladin")
                return PlayerClass.Paladin;
            if (str == "Ranger")
                return PlayerClass.Ranger;
            if (str == "Rogue")
                return PlayerClass.Rogue;
            if (str == "Sorcerer")
                return PlayerClass.Sorcerer;
            if (str == "Warlock")
                return PlayerClass.Warlock;
            if (str == "Wizard")
                return PlayerClass.Wizard;
            throw new ArgumentException("\'" + str + "\' does not represent a valid player class");
        }
    }
}
