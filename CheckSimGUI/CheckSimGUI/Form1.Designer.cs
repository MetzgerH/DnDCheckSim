﻿namespace CheckSimGUI
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(resultsPlot);
            Controls.Add(runButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button runButton;
        private ScottPlot.WinForms.FormsPlot resultsPlot;
    }
}
