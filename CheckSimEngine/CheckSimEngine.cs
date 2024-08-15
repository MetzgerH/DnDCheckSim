namespace CheckSimEngine
{
    /// <summary>
    /// The wrapper for CheckSim's engine.
    /// </summary>
    public class CheckSimEngine
    {
        private ConfigurationManager config;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckSimEngine"/> class.
        /// </summary>
        public CheckSimEngine()
        {
            this.config = new ConfigurationManager();
        }

        /// <summary>
        /// Gets the configuration manager for this engine.
        /// </summary>
        public ConfigurationManager Config
        {
            get { return this.config; }
        }

        public Dictionary<int,double> RunCheck(bool isSave = false, int level = 1, Ability? relevantAbility = null, Skill? relevantSkill = null, Tool? relevantTool = null, PlayerClass? classRestriction = null, string? lineageRestriction = null)
        {
            Dictionary<int,double> output = new Dictionary<int,double>();

            for (int i = 0; i < this.config.SampleSize; i++)
            {
                D20Test d20Test;
                if (isSave)
                {
                    d20Test = new Save(relevantAbility);
                }
                else
                {
                    d20Test = new Check(relevantAbility, relevantSkill, relevantTool);
                }

                new PlayerCharacter(this.config, level, lineageRestriction, classRestriction).PerformTest(d20Test);

                foreach (int result in d20Test.Odds.Keys)
                {
                    if (!output.TryAdd(result, d20Test.Odds[result]))
                    {
                        output[result] = output[result] + d20Test.Odds[result];
                    }
                }
            }

            foreach (int result in output.Keys)
            {
                output[result] = output[result] / 1000;
            }

            return output;
        }
    }
}
