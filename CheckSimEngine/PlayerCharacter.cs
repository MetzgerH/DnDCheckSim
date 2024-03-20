using System;
using System.Security.Cryptography;

namespace CheckSimEngine
{
    /// <summary>
    /// Enumerates the different playable classes in 5e.
    /// </summary>
    internal enum PlayerClass
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
    /// Enumerates different playable lineages (also known as races) in 5e.
    /// </summary>
    internal enum Lineage
    {
        Dragonborn,
        Dwarf,
        Elf,
        Gnome,
        HalfElf,
        Halfling,
        HalfOrc,
        Human,
        Orc,
        Tiefling,
        Max,
    }

    /// <summary>
    /// Simulates/represents a player's character in 5e.
    /// </summary>
    internal class PlayerCharacter
    {
        private Lineage lineage;
        private PlayerClass playerClass;
        private int level;
        private List<Skill> skillProficiencies;
        private List<Skill> skillExpertises;
        private List<Tool> toolProficiencies;
        private List<Tool> toolExpertises;
        private List<Ability> saveProficiencies;
        private List<Action<D20Test>> bonuses;
        private Dictionary<Ability, int> abilityScores;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCharacter"/> class.
        /// </summary>
        /// <param name="level">
        /// The class level of this player instance.
        /// </param>
        /// <param name="lineage">
        /// The lineage of this player instance. Randomly chosen if null.
        /// </param>
        /// <param name="playerClass">
        /// The class of this player instance. Randomly chosen if null.
        /// </param>
        public PlayerCharacter(int level, Lineage? lineage = null, PlayerClass? playerClass = null)
        {
            this.skillProficiencies = new List<Skill>();
            this.skillExpertises = new List<Skill>();
            this.toolProficiencies = new List<Tool>();
            this.toolExpertises = new List<Tool>();
            this.saveProficiencies = new List<Ability>();
            this.bonuses = new List<Action<D20Test>>();
            this.abilityScores = new Dictionary<Ability, int>();

            for (int i = 0; i < level; i++)
            {
                this.abilityScores[(Ability)i] = 0;
            }

            this.level = level;
            this.ApplyClass(this.playerClass);

            this.ApplyLineage(lineage);
        }

        /// <summary>
        /// Applies a lineage to this PlayerCharacter, including all proficiencies and bonuses granted by the lineage.
        /// </summary>
        /// <param name="lineage">
        /// The lineage to apply. Can be null, to select a random lineage.
        /// </param>
        private void ApplyLineage(Lineage? lineage)
        {
            Lineage lineageToApply;
            Random randy = new Random();

            if (lineage == null)
            {
                // Set lineageToApply to a random lineage
                lineageToApply = (Lineage)randy.Next((int)Lineage.Max);
            }
            else
            {
                lineageToApply = (Lineage)lineage;
            }

            // Grant proficiences and bonuses relevant to lineage.
            int prev = 0;
            switch (lineageToApply)
            {
                case Lineage.Dragonborn:
                    // Dragonborn doesn't get any proficiencies or relevant features.
                    break;
                case Lineage.Dwarf:
                    // Assign random tool proficiency from short list.
                    List<Tool> toolList = new List<Tool>() { Tool.Smiths, Tool.Brewers, Tool.Masons };
                    this.AddProficiencyOutOf(toolList);

                    // Increase Constitution score by 2.
                    this.abilityScores.TryGetValue(Ability.Constitution, out prev);
                    this.abilityScores[Ability.Constitution] = prev + 2;

                    // Randomly choose subrace.
                    switch (randy.Next(2))
                    {
                        // Hill Dwarf
                        case 0:
                            // Increase Wisdom score by 1.
                            this.abilityScores.TryGetValue(Ability.Wisdom, out prev);
                            this.abilityScores[Ability.Wisdom] = prev + 1;
                            break;
                        case 1:
                            // Increase Strength score by 2.
                            this.abilityScores.TryGetValue(Ability.Strength, out prev);
                            this.abilityScores[Ability.Strength] = prev + 2;
                            break;
                    }

                    break;
                case Lineage.Elf:
                    throw new NotImplementedException("Elf Lineage Not Implemented Yet");
                    break;
                case Lineage.Gnome:
                    throw new NotImplementedException("Gnome Lineage Not Implemented Yet");
                    break;
                case Lineage.HalfElf:
                    throw new NotImplementedException("HalfELf Lineage Not Implemented Yet");
                    break;
                case Lineage.Halfling:
                    throw new NotImplementedException("Halfling Lineage Not Implemented Yet");
                    break;
                case Lineage.HalfOrc:
                    throw new NotImplementedException("HalfOrc Lineage Not Implemented Yet");
                    break;
                case Lineage.Human:
                    throw new NotImplementedException("Human Lineage Not Implemented Yet");
                    break;
                case Lineage.Orc:
                    throw new NotImplementedException("Orc Lineage Not Implemented Yet");
                    break;
                case Lineage.Tiefling:
                    throw new NotImplementedException("Tiefling Lineage Not Implemented Yet");
                    break;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Applies a lineage to this PlayerCharacter, including all proficiencies, expertises, and bonuses granted by the lineage.
        /// </summary>
        /// <param name="playerClass">
        /// The class to be applied. Can be null, to select a random lineage.
        /// </param>
        private void ApplyClass(PlayerClass? playerClass)
        {
            PlayerClass classToApply;
            if (playerClass == null)
            {
                // Set classToApply to a random lineage
                Random randy = new Random();

                classToApply = (PlayerClass)randy.Next((int)PlayerClass.Max);
            }
            else
            {
                classToApply = (PlayerClass)playerClass;
            }

            this.playerClass = classToApply;

            // Grant proficiencies, expertises, and bonuses relevant to class.
            switch (classToApply)
            {
                case PlayerClass.Barbarian:
                    this.AllocateStandardArray(Ability.Strength);
                    break;
                case PlayerClass.Bard:
                    this.AllocateStandardArray(Ability.Charisma);
                    break;
                case PlayerClass.Cleric:
                    this.AllocateStandardArray(Ability.Wisdom);
                    break;
                case PlayerClass.Druid:
                    this.AllocateStandardArray(Ability.Wisdom);
                    break;
                case PlayerClass.Fighter:
                    this.AllocateStandardArray(Ability.Strength, Ability.Dexterity);
                    break;
                case PlayerClass.Monk:
                    this.AllocateStandardArray(Ability.Dexterity, Ability.Wisdom);
                    break;
                case PlayerClass.Paladin:
                    this.AllocateStandardArray(Ability.Charisma, Ability.Strength);
                    break;
                case PlayerClass.Ranger:
                    this.AllocateStandardArray(Ability.Dexterity, Ability.Wisdom);
                    break;
                case PlayerClass.Rogue:
                    this.AllocateStandardArray(Ability.Dexterity);
                    break;
                case PlayerClass.Sorcerer:
                    this.AllocateStandardArray(Ability.Charisma);
                    break;
                case PlayerClass.Warlock:
                    this.AllocateStandardArray(Ability.Charisma);
                    break;
                case PlayerClass.Wizard:
                    this.AllocateStandardArray(Ability.Intelligence);
                    break;
            }

            throw new NotImplementedException("Only ability scores have been implemented. Still missing features, and starting proficiencies");
        }

        private void AllocateStandardArray(Ability? primary = null, Ability? alsoPrimary = null)
        {
            List<int> scorePool = new List<int> { 8, 10, 12, 13, 14, 15 };
            Random randy = new Random();

            if (primary != null)
            {
                if (alsoPrimary == null)
                {
                    int prev = 0;
                    this.abilityScores.TryGetValue((Ability)primary, out prev);
                    this.abilityScores[(Ability)primary] = 15 + prev;
                    scorePool.Remove(15);
                }
                else
                {
                    if (randy.Next(2) == 1)
                    {
                        int prev = 0;
                        this.abilityScores.TryGetValue((Ability)primary, out prev);
                        this.abilityScores[(Ability)primary] = 15 + prev;
                        scorePool.Remove(15);
                        this.abilityScores.TryGetValue((Ability)alsoPrimary, out prev);
                        this.abilityScores[(Ability)alsoPrimary] = 14 + prev;
                        scorePool.Remove(14);
                    }
                    else
                    {
                        int prev = 0;
                        this.abilityScores.TryGetValue((Ability)alsoPrimary, out prev);
                        this.abilityScores[(Ability)alsoPrimary] = 15 + prev;
                        scorePool.Remove(15);
                        this.abilityScores.TryGetValue((Ability)primary, out prev);
                        this.abilityScores[(Ability)primary] = 14 + prev;
                        scorePool.Remove(14);
                    }
                }
            }

            foreach (Ability ability in this.abilityScores.Keys)
            {
                if (ability != primary && ability != alsoPrimary)
                {
                    int scoreIndex = randy.Next(scorePool.Count);
                    this.abilityScores[ability] = scorePool[scoreIndex];
                    scorePool.RemoveAt(scoreIndex);
                }
            }
        }

        private void AddProficiencyOutOf(List<Tool> toolList)
        {
            List<Tool> tools = new List<Tool>(toolList);
            foreach (Tool tool in tools)
            {
                if (this.toolProficiencies.Contains(tool))
                {
                    tools.Remove(tool);
                }
            }

            Random randy = new Random();
            this.toolProficiencies.Add(tools[randy.Next(tools.Count)]);
        }
    }
}
