using CheckSimEngine;
using ScottPlot;
using ScottPlot.WinForms;
using System.Runtime.Serialization;

namespace CheckSimGUI
{
    public partial class Form1 : Form
    {
        CheckSimEngine.CheckSimEngine engine;

        public Form1()
        {
            InitializeComponent();
            engine = new CheckSimEngine.CheckSimEngine();
            for (int i = 0; i < (int)PlayerClass.Max; i++)
            {
                this.ClassChoiceBox.Items.Add(PlayerClassHelper.ToString((PlayerClass)i));
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            CheckSimEngine.PlayerClass? chosenClass = null;
            if (this.ClassChoiceBox.SelectedIndex != -1)
            {
                chosenClass = CheckSimEngine.PlayerClassHelper.FromString(this.ClassChoiceBox.Items[this.ClassChoiceBox.SelectedIndex].ToString());
            }

            Dictionary<int, double> data = engine.RunCheck(level: 20, relevantAbility: CheckSimEngine.Ability.Dexterity, relevantSkill: CheckSimEngine.Skill.SleightOfHand, relevantTool: CheckSimEngine.Tool.Thieves, classRestriction: chosenClass);

            string output = String.Empty;
            foreach (int result in data.Keys)
            {
                output += result.ToString() + ": " + data[result].ToString() + Environment.NewLine;
            }

            List<double> xs = new List<double>();
            foreach (int result in data.Keys)
            {
                xs.Add((double)result);
            }

            this.resultsPlot.Plot.Clear();
            this.resultsPlot.Plot.Add.Bars(xs, data.Values);
            this.resultsPlot.Plot.Axes.AutoScale();
            this.resultsPlot.Refresh();
        }
    }
}
