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

        public System.Collections.ObjectModel.ReadOnlyCollection<string> AvailableLineages
        {
            get
            {
                List<string> lineageOptions = new List<string>();
                if (this.SourceBooks.Contains("Player's Handbook"))
                {
                    lineageOptions.Add("Dragonborn");
                    lineageOptions.Add("Dwarf");
                    lineageOptions.Add("Elf");
                    lineageOptions.Add("Gnome");
                    lineageOptions.Add("Half-Elf");
                    lineageOptions.Add("Halfling");
                    lineageOptions.Add("Half-Orc");
                    lineageOptions.Add("Human");
                    lineageOptions.Add("Tiefling");
                }
                return lineageOptions.AsReadOnly();
            }
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
