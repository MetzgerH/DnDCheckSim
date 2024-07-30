using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckSimEngine
{
    public class ConfigurationManager
    {
        private bool useRacialASI = true;
        private List<string> sourceBooks = new List<string> { "Player's Handbook" };

        /// <summary>
        /// Gets or sets a value indicating whether player characters should get an Ability Score Increase depending on their race/species/lineage (elf, dwarf, etc.).
        /// </summary>
        public bool UseRacialASI
        {
            get { return this.useRacialASI; }
            set { this.useRacialASI = value; }
        }

        /// <summary>
        /// Gets 
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<string> SourceBooks
        {
            get { return this.sourceBooks.AsReadOnly(); }
    }
}
