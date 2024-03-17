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

            this.ApplyLineage(lineage);

            this.level = level;
            this.ApplyClass(this.playerClass);
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
            if (lineage == null)
            {
                // Set lineageToApply to a random lineage
                throw new NotImplementedException();
            }
            else
            {
                lineageToApply = (Lineage)lineage;
            }

            // Grant proficiences and bonuses relevant to lineage.
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
                throw new NotImplementedException();
            }
            else
            {
                classToApply = (PlayerClass)playerClass;
            }

            // Grant proficiencies, expertises, and bonuses relevant to class.
            throw new NotImplementedException();
        }
    }
}
