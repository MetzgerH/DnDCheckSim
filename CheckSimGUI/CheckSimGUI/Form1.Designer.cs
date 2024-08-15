namespace CheckSimGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            runButton = new Button();
            resultsPlot = new ScottPlot.WinForms.FormsPlot();
            ClassChoiceBox = new ComboBox();
            ClassLabel = new TextBox();
            LineageLabel = new TextBox();
            LineageChoiceBox = new ComboBox();
            SuspendLayout();
            // 
            // runButton
            // 
            runButton.Location = new Point(648, 391);
            runButton.Name = "runButton";
            runButton.Size = new Size(75, 23);
            runButton.TabIndex = 0;
            runButton.Text = "Run Check";
            runButton.UseVisualStyleBackColor = true;
            runButton.Click += runButton_Click;
            // 
            // resultsPlot
            // 
            resultsPlot.DisplayScale = 1F;
            resultsPlot.Location = new Point(12, 128);
            resultsPlot.Name = "resultsPlot";
            resultsPlot.Size = new Size(589, 310);
            resultsPlot.TabIndex = 1;
            // 
            // ClassChoiceBox
            // 
            ClassChoiceBox.FormattingEnabled = true;
            ClassChoiceBox.Location = new Point(607, 352);
            ClassChoiceBox.Name = "ClassChoiceBox";
            ClassChoiceBox.Size = new Size(121, 23);
            ClassChoiceBox.TabIndex = 2;
            // 
            // ClassLabel
            // 
            ClassLabel.Location = new Point(607, 323);
            ClassLabel.Name = "ClassLabel";
            ClassLabel.ReadOnly = true;
            ClassLabel.Size = new Size(100, 23);
            ClassLabel.TabIndex = 3;
            ClassLabel.Text = "Class:";
            // 
            // LineageLabel
            // 
            LineageLabel.Location = new Point(607, 265);
            LineageLabel.Name = "LineageLabel";
            LineageLabel.ReadOnly = true;
            LineageLabel.Size = new Size(100, 23);
            LineageLabel.TabIndex = 5;
            LineageLabel.Text = "Race:";
            // 
            // LineageChoiceBox
            // 
            LineageChoiceBox.FormattingEnabled = true;
            LineageChoiceBox.Location = new Point(607, 294);
            LineageChoiceBox.Name = "LineageChoiceBox";
            LineageChoiceBox.Size = new Size(121, 23);
            LineageChoiceBox.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LineageLabel);
            Controls.Add(LineageChoiceBox);
            Controls.Add(ClassLabel);
            Controls.Add(ClassChoiceBox);
            Controls.Add(resultsPlot);
            Controls.Add(runButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button runButton;
        private ScottPlot.WinForms.FormsPlot resultsPlot;
        private ComboBox ClassChoiceBox;
        private TextBox ClassLabel;
        private TextBox LineageLabel;
        private ComboBox LineageChoiceBox;
    }
}
