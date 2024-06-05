﻿namespace CheckSimEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This enumerates the different abilities in 5e.
    /// </summary>
    internal enum Ability
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma,
    }

    /// <summary>
    /// This enumerates the different skills in 5e.
    /// </summary>
    internal enum Skill
    {
        Acrobatics,
        AnimalHandling,
        Arcana,
        Athletics,
        Deception,
        History,
        Insight,
        Intimidation,
        Investigation,
        Medicine,
        Nature,
        Perception,
        Performance,
        Persuasion,
        Religion,
        SleightOfHand,
        Stealth,
        Survival,
        Max,
    }

    /// <summary>
    /// This enumerates the different tools in 5e, including artisans' tools and other tools.
    /// </summary>
    internal enum Tool
    {
        Alchemists,
        Brewers,
        Calligraphers,
        Carpenters,
        Cartographers,
        Cobblers,
        Cooks,
        Disguise,
        Forgery,
        Glassblowers,
        Herbalism,
        Jewelers,
        Leatherworkers,
        Masons,
        Navigators,
        Painters,
        Poisoners,
        Potters,
        Smiths,
        Thieves,
        Tinkers,
        Weavers,
        Woodcarvers,
        Artisan,
        Gaming,
        LandVehicles,
        WaterVehicles,
        Instrument,
    }

    /// <summary>
    /// An encapsulation of a general D20Test. Includes the circumstances (ability) and the odds of each result. Is to be modified by a player.
    /// According to some 5e documents, this is a generalization of Saving Throws, Checks, and Attack Rolls.
    /// </summary>
    /// <param name="ability">
    /// The ability related to this Test.
    /// </param>
    internal class D20Test
    {
        private Ability? relevantAbility;
        private Dictionary<int, double> odds;


        /// <summary>
        /// Initializes a new instance of the <see cref="D20Test"/> class.
        /// </summary>
        /// <param name="ability">
        /// The ability related to this test.
        /// </param>
        public D20Test(Ability? ability = null)
        {
            this.relevantAbility = ability;
            this.odds = new Dictionary<int, double>();

            for (int i = 1;  i <= 20; i++)
            {
                this.odds[i] = 0.05;
            }
        }

        /// <summary>
        /// Gets the ability related to this test.
        /// </summary>
        public Ability? RelevantAbility
        {
            get
            {
                return this.relevantAbility;
            }
        }

        /// <summary>
        /// Gets the odds related to this test.
        /// </summary>
        public Dictionary<int,double> Odds
        {
            get
            {
                return this.odds;
            }
        }

        /// <summary>
        /// Adds <paramref name="i"/> to the roll.
        /// </summary>
        /// <param name="bonus">
        /// The bonus to add.
        /// </param>
        public void ApplyBonus(int bonus)
        {
            List<int> orderedRolls = this.odds.Keys.ToList();

            // If a bonus is positive, we must iterate highest to lowest, to avoid double-boosting.
            if (bonus > 0)
            {
                orderedRolls.Reverse();
            }

            foreach (int roll in orderedRolls)
            {
                this.odds[roll + bonus] = this.odds[roll];
            }
        }
    }

    /// <summary>
    /// An ability check or skill check (those are basically two words for the same thing in 5e).
    /// </summary>
    /// <param name="ability">
    /// The ability related to this check.
    /// </param>
    /// <param name="skill">
    /// The skill used in this check, if any. For determining relevant skill proficiency.
    /// </param>
    /// <param name="tool">
    /// The tool used in this check, if any. For determining relevant tool proficiency.
    /// </param>
    internal class Check(Ability? ability = null, Skill? skill = null, Tool? tool = null) : D20Test(ability)
    {
        private Skill? relevantSkill = skill;
        private Tool? relevantTool = tool;

        /// <summary>
        /// Gets the skill used in this check, if any.
        /// </summary>
        public Skill? RelevantSkill { get { return this.relevantSkill; } }

        /// <summary>
        /// Gets the tool used in this check, if any.
        /// </summary>
        public Tool? RelevantTool { get { return this.relevantTool; } }
    }

    internal class Save(Ability? ability = null) : D20Test(ability)
    {
    }
}
