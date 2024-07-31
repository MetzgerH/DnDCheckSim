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
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            Dictionary<int, double> data = engine.RunCheck(level: 20, relevantAbility: CheckSimEngine.Ability.Dexterity, relevantSkill: CheckSimEngine.Skill.SleightOfHand, relevantTool: CheckSimEngine.Tool.Thieves, classRestriction: CheckSimEngine.PlayerClass.Rogue);

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

            this.resultsPlot.Plot.Add.Bars(xs, data.Values);
            this.resultsPlot.Plot.Axes.AutoScale();
            this.resultsPlot.Refresh();
        }
    }
}
