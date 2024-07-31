namespace CheckSimEngine
{
    public class ConfigurationManager
    {
        private bool useRacialASI = true;
        private List<string> sourceBooks = new List<string> { "Player's Handbook" };
        private int sampleSize = 1000;

        /// <summary>
        /// Gets or sets a value indicating whether player characters should get an Ability Score Increase depending on their race/species/lineage (elf, dwarf, etc.).
        /// </summary>
        public bool UseRacialASI
        {
            get { return this.useRacialASI; }
            set { this.useRacialASI = value; }
        }

        /// <summary>
        /// Gets a readonly list of source books.
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<string> SourceBooks
        {
            get { return this.sourceBooks.AsReadOnly(); }
        }

        /// <summary>
        /// Gets or sets an int representing how many playercharacters should be simulated per check.
        /// </summary>
        public int SampleSize
        {
            get { return this.sampleSize; }

            set { this.sampleSize = value; }
        }
    }
}
