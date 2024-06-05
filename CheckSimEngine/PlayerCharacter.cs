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
        Tiefling,
        Max,
    }

    /// <summary>
    /// Enumerates the different phases of modifying a D20Test.
    /// </summary>
    internal enum ModifierPriority
    {
        Rerolls,
        BeforeAdvantage,
        Advantage,
        AfterAdvantage,
        AfterTotal,
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
        private Dictionary<Ability, int> abilityScores;
        private Dictionary<Ability, int> abilityScoreMaximums;
        private PriorityQueue<Action<D20Test>, ModifierPriority> modifiers;
        private bool useRacialASI = true;

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
            this.abilityScores = new Dictionary<Ability, int>();
            this.abilityScoreMaximums = new Dictionary<Ability, int>();
            this.modifiers = new PriorityQueue<Action<D20Test>, ModifierPriority>();

            for (int i = 0; i < 6; i++)
            {
                this.abilityScores[(Ability)i] = 0;
                this.abilityScoreMaximums[(Ability)i] = 20;
            }

            this.level = level;
            this.ApplyClass(this.playerClass);

            this.ApplyLineage(lineage);
        }

        private int ProficiencyBonus
        {
            get
            {
                return ((this.level - 1) / 4) + 2;
            }
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
            switch (lineageToApply)
            {
                case Lineage.Dragonborn:
                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Strength, 2);

                        this.IncreaseAbilityScore(Ability.Charisma, 1);
                    }

                    break;
                case Lineage.Dwarf:
                    // Assign random tool proficiency from short list.
                    List<Tool> toolList = new List<Tool>() { Tool.Smiths, Tool.Brewers, Tool.Masons };
                    this.AddProficiencyOutOf(toolList);

                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Constitution, 2);

                        // Randomly choose subrace.
                        switch (randy.Next(2))
                        {
                            // Hill Dwarf
                            case 0:
                                this.IncreaseAbilityScore(Ability.Wisdom, 1);
                                break;

                            // Mountain Dwarf
                            case 1:
                                this.IncreaseAbilityScore(Ability.Strength, 2);
                                break;
                        }
                    }

                    break;
                case Lineage.Elf:
                    // "Keen Senses": Perception proficiency
                    if (!this.skillProficiencies.Contains(Skill.Perception))
                    {
                        this.skillProficiencies.Add(Skill.Perception);
                    }

                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Dexterity, 2);

                        // Randomly choose subrace.
                        switch (randy.Next(3))
                        {
                            // Eladrin
                            case 0:
                                this.IncreaseAbilityScore(Ability.Intelligence, 1);
                                break;

                            // High Elf
                            case 1:
                                this.IncreaseAbilityScore(Ability.Intelligence, 1);
                                break;

                            // Wood Elf
                            case 2:
                                this.IncreaseAbilityScore(Ability.Wisdom, 1);
                                break;
                        }
                    }

                    break;
                case Lineage.Gnome:
                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Intelligence, 2);

                        // Randomly choose subrace
                        switch (randy.Next(2))
                        {
                            // Deep Gnome
                            case 0:
                                this.IncreaseAbilityScore(Ability.Dexterity, 1);
                                break;

                            // Rock Gnome
                            case 1:
                                this.IncreaseAbilityScore(Ability.Constitution, 1);
                                break;
                        }
                    }

                    break;
                case Lineage.HalfElf:
                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Charisma, 2);

                        // Randomly choose two other ability scores to increase by 1
                        List<Ability> abilityList = new List<Ability>()
                            { Ability.Constitution, Ability.Dexterity, Ability.Intelligence, Ability.Strength, Ability.Wisdom };
                        Ability abilityToIncrease = abilityList[randy.Next(5)];
                        this.IncreaseAbilityScore(abilityToIncrease, 1);
                        abilityList.Remove(abilityToIncrease);
                        abilityToIncrease = abilityList[randy.Next(4)];
                        this.IncreaseAbilityScore(abilityToIncrease, 1);
                    }

                    // Two random skills
                    // Start by constructing list of all skills
                    List<Skill> list = new List<Skill>();
                    for (int i = 0; i < (int)Skill.Max; i++)
                    {
                        list.Add((Skill)i);
                    }

                    // Now choose two
                    this.AddProficiencyOutOf(list);
                    this.AddProficiencyOutOf(list);
                    break;
                case Lineage.Halfling:
                    // Create the "Lucky" modifier, which rerolls the d20 when it rolls a 1. If a 1 is rolled on the reroll, it does not get rerolled again.
                    Action<D20Test> lucky = d20 =>
                    {
                        Dictionary<int, double> oldOdds = new Dictionary<int, double>(d20.Odds);

                        // Get rid of old odds of rolling 1, because that is rerolled.
                        d20.Odds[1] = 0;

                        // Redistribute the odds we just got rid of.
                        foreach (int side in d20.Odds.Keys)
                        {
                            d20.Odds[side] += oldOdds[side] * oldOdds[1];
                        }
                    };

                    // Add the modifier to this player character.
                    this.modifiers.Enqueue(lucky, ModifierPriority.Rerolls);

                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Dexterity, 2);

                        // Randomly choose subrace
                        switch (randy.Next(2))
                        {
                            // Lightfoot
                            case 0:
                                this.IncreaseAbilityScore(Ability.Charisma, 1);
                                break;

                            // Stout
                            case 1:
                                this.IncreaseAbilityScore(Ability.Constitution, 1);
                                break;
                        }
                    }

                    break;
                case Lineage.HalfOrc:
                    if (!this.skillProficiencies.Contains(Skill.Intimidation))
                    {
                        this.skillProficiencies.Add(Skill.Intimidation);
                    }

                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Strength, 2);
                        this.IncreaseAbilityScore(Ability.Constitution, 2);
                    }

                    break;
                case Lineage.Human:
                    for (int i = 0; i < (int) Skill.Max; i++)
                    {
                        this.IncreaseAbilityScore((Ability)i, 1);
                    }

                    break;
                case Lineage.Tiefling:
                    if (this.useRacialASI)
                    {
                        this.IncreaseAbilityScore(Ability.Charisma, 2);
                        this.IncreaseAbilityScore(Ability.Intelligence, 2);
                    }

                    break;
            }
        }

        /// <summary>
        /// Applies a class to this PlayerCharacter, including all proficiencies, expertises, and bonuses granted by the class.
        /// </summary>
        /// <param name="playerClass">
        /// The class to be applied. Can be null, to select a random class.
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
                    this.AddProficiencyOutOf(new List<Skill> { Skill.AnimalHandling, Skill.Athletics, Skill.Intimidation, Skill.Nature, Skill.Perception, Skill.Survival });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.AnimalHandling, Skill.Athletics, Skill.Intimidation, Skill.Nature, Skill.Perception, Skill.Survival });
                    this.AllocateStandardArray(Ability.Strength);

                    for (int i = 4; i < this.level; i += 4)
                    {
                        this.AbilityScoreImprovement();
                    }

                    if (this.level >= 18)
                    {
                        Action<D20Test> indomitableMight = d20 =>
                        {
                            foreach (int roll in d20.Odds.Keys)
                            {
                                if (roll < this.abilityScores[Ability.Strength])
                                {
                                    if (d20.Odds.ContainsKey(this.abilityScores[Ability.Strength]))
                                    {
                                        d20.Odds[this.abilityScores[Ability.Strength]] += d20.Odds[roll];
                                    }
                                    else
                                    {
                                        d20.Odds[this.abilityScores[Ability.Strength]] = d20.Odds[roll];
                                    }

                                    d20.Odds[roll] = 0;
                                }
                            }
                        };

                        this.modifiers.Enqueue(indomitableMight, ModifierPriority.AfterTotal);
                    }

                    if (this.level == 20)
                    {
                        this.abilityScoreMaximums[Ability.Strength] = 24;
                        this.abilityScoreMaximums[Ability.Constitution] = 24;

                        this.IncreaseAbilityScore(Ability.Strength, 4);
                        this.IncreaseAbilityScore(Ability.Constitution, 4);
                    }

                    throw new NotImplementedException("Still missing subclasses");
                    break;
                case PlayerClass.Bard:
                    // Three random skills
                    // Start by constructing list of all skills
                    List<Skill> list = new List<Skill>();
                    for (int i = 0; i < (int)Skill.Max; i++)
                    {
                        list.Add((Skill)i);
                    }

                    // Now choose three
                    for (int i = 0; i < 3; i++)
                    {
                        this.AddProficiencyOutOf(list);
                    }

                    this.AllocateStandardArray(Ability.Charisma);

                    for (int i = 4; i < this.level; i += 4)
                    {
                        this.AbilityScoreImprovement();
                    }

                    if (this.level >= 2)
                    {
                        // Jack of All Trades is a feature that adds half proficiency bonus (rounded down) to checks where the proficiency bonus is not already added.
                        Action<D20Test> jackOfAllTrades = d20 =>
                        {
                            // First check that this is a skill.
                            if (d20 is Check)
                            {
                                // Now check that proficiency bonus is not already added based on skill
                                if (((d20 as Check).RelevantSkill is not null && this.skillProficiencies.Contains((Skill)(d20 as Check).RelevantSkill))
                                    | (d20 as Check).RelevantSkill is null)
                                {
                                    if (((d20 as Check).RelevantTool is not null && this.toolProficiencies.Contains((Tool)(d20 as Check).RelevantTool))
                                    | (d20 as Check).RelevantTool is null)
                                    {
                                        d20.ApplyBonus(this.ProficiencyBonus / 2);
                                    }
                                }
                            }
                        };

                        this.modifiers.Enqueue(jackOfAllTrades, ModifierPriority.AfterAdvantage);
                    }

                    if (this.level >= 3)
                    {
                        this.AddRandomExpertise();
                        this.AddRandomExpertise();
                    }

                    if (this.level >= 10)
                    {
                        this.AddRandomExpertise();
                        this.AddRandomExpertise();
                    }

                    throw new NotImplementedException("Still missing subclasses");
                    break;
                case PlayerClass.Cleric:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.History, Skill.Insight, Skill.Medicine, Skill.Persuasion, Skill.Religion });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.History, Skill.Insight, Skill.Medicine, Skill.Persuasion, Skill.Religion });
                    this.AllocateStandardArray(Ability.Wisdom);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Druid:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.AnimalHandling, Skill.Insight, Skill.Medicine, Skill.Nature, Skill.Perception, Skill.Religion, Skill.Survival });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.AnimalHandling, Skill.Insight, Skill.Medicine, Skill.Nature, Skill.Perception, Skill.Religion, Skill.Survival });
                    this.AllocateStandardArray(Ability.Wisdom);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Fighter:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Acrobatics, Skill.AnimalHandling, Skill.Athletics, Skill.History, Skill.Insight, Skill.Intimidation, Skill.Perception });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Acrobatics, Skill.AnimalHandling, Skill.Athletics, Skill.History, Skill.Insight, Skill.Intimidation, Skill.Perception });
                    this.AllocateStandardArray(Ability.Strength, Ability.Dexterity);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Monk:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Acrobatics, Skill.Athletics, Skill.History, Skill.Insight, Skill.Religion, Skill.Stealth });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Acrobatics, Skill.Athletics, Skill.History, Skill.Insight, Skill.Religion, Skill.Stealth });
                    this.AllocateStandardArray(Ability.Dexterity, Ability.Wisdom);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Paladin:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Athletics, Skill.Insight, Skill.Intimidation, Skill.Medicine, Skill.Persuasion, Skill.Religion });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Athletics, Skill.Insight, Skill.Intimidation, Skill.Medicine, Skill.Persuasion, Skill.Religion });
                    this.AllocateStandardArray(Ability.Charisma, Ability.Strength);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Ranger:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.AnimalHandling, Skill.Athletics, Skill.Insight, Skill.Investigation, Skill.Nature, Skill.Perception, Skill.Stealth, Skill.Survival });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.AnimalHandling, Skill.Athletics, Skill.Insight, Skill.Investigation, Skill.Nature, Skill.Perception, Skill.Stealth, Skill.Survival });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.AnimalHandling, Skill.Athletics, Skill.Insight, Skill.Investigation, Skill.Nature, Skill.Perception, Skill.Stealth, Skill.Survival });
                    this.AllocateStandardArray(Ability.Dexterity, Ability.Wisdom);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Rogue:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Acrobatics, Skill.Athletics, Skill.Deception, Skill.Insight, Skill.Intimidation, Skill.Investigation, Skill.Perception, Skill.Performance, Skill.Persuasion, Skill.SleightOfHand, Skill.Stealth });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Acrobatics, Skill.Athletics, Skill.Deception, Skill.Insight, Skill.Intimidation, Skill.Investigation, Skill.Perception, Skill.Performance, Skill.Persuasion, Skill.SleightOfHand, Skill.Stealth });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Acrobatics, Skill.Athletics, Skill.Deception, Skill.Insight, Skill.Intimidation, Skill.Investigation, Skill.Perception, Skill.Performance, Skill.Persuasion, Skill.SleightOfHand, Skill.Stealth });
                    this.AllocateStandardArray(Ability.Dexterity);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Sorcerer:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.Deception, Skill.Insight, Skill.Intimidation, Skill.Persuasion, Skill.Religion });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.Deception, Skill.Insight, Skill.Intimidation, Skill.Persuasion, Skill.Religion });
                    this.AllocateStandardArray(Ability.Charisma);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Warlock:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.Deception, Skill.History, Skill.Intimidation, Skill.Investigation, Skill.Nature, Skill.Religion });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.Deception, Skill.History, Skill.Intimidation, Skill.Investigation, Skill.Nature, Skill.Religion });
                    this.AllocateStandardArray(Ability.Charisma);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
                case PlayerClass.Wizard:
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.History, Skill.Insight, Skill.Investigation, Skill.Medicine, Skill.Religion });
                    this.AddProficiencyOutOf(new List<Skill> { Skill.Arcana, Skill.History, Skill.Insight, Skill.Investigation, Skill.Medicine, Skill.Religion });
                    this.AllocateStandardArray(Ability.Intelligence);
                    throw new NotImplementedException("Still missing features and subclasses");
                    break;
            }

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

        /// <summary>
        /// Selects a Tool from a <paramref name="toolList"/> to gain proficiency in.
        /// Will only select a Tool that this player character is not already proficient in.
        /// </summary>
        /// <param name="toolList">
        /// List of Tools to choose from.
        /// </param>
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

        /// <summary>
        /// Selects a Skill from a <paramref name="skillList"/> to gain proficiency in.
        /// Will only select a Skill that this player character is not already proficient in.
        /// </summary>
        /// <param name="skillList">
        /// List of Skills to choose from.
        /// </param>
        private void AddProficiencyOutOf(List<Skill> skillList)
        {
            List<Skill> skills = new List<Skill>(skillList);
            foreach (Skill skill in skills)
            {
                if (this.skillProficiencies.Contains(skill))
                {
                    skills.Remove(skill);
                }
            }

            Random randy = new Random();
            this.skillProficiencies.Add(skills[randy.Next(skills.Count)]);
        }

        /// <summary>
        /// Adds expertise in a random skill that this player is already proficient in.
        /// </summary>
        private void AddRandomExpertise()
        {
            if (this.skillProficiencies.Count == 0)
            {
                return;
            }

            List<Skill> candidates = this.skillProficiencies[..];

            foreach (Skill skill in this.skillExpertises)
            {
                candidates.Remove(skill);
            }

            Random randy = new Random();
            this.skillExpertises.Add(candidates[randy.Next(candidates.Count)]);
        }

        private void IncreaseAbilityScore(Ability ability, int amount)
        {
            int prev = 0;
            this.abilityScores.TryGetValue(ability, out prev);
            this.abilityScores[ability] = Math.Min(prev + amount, this.abilityScoreMaximums[ability]);
        }

        /// <summary>
        /// This is a feature that every class gets at certain levels (the levels aren't the same between classes), which increases 2 ability scores by 1, or 1 by 2.
        /// </summary>
        private void AbilityScoreImprovement()
        {
            Random randy = new Random();
            if (randy.Next(2) == 0)
            {
                this.IncreaseAbilityScore((Ability)randy.Next(6), 1);
                this.IncreaseAbilityScore((Ability)randy.Next(6), 1);
            }
            else
            {
                this.IncreaseAbilityScore((Ability)randy.Next(6), 2);
            }
        }
    }
}
