﻿namespace WinFormBlankTest.UI.Panels
{
    partial class CounterPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModell.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbTubus = new System.Windows.Forms.Label();
            this.lbLemert = new System.Windows.Forms.Label();
            this.lbLot = new System.Windows.Forms.Label();
            this.lbRoll = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btInvalidate = new System.Windows.Forms.Button();
            this.tbLot = new System.Windows.Forms.TextBox();
            this.tbNeedToMeasure = new System.Windows.Forms.TextBox();
            this.tbLemert = new System.Windows.Forms.TextBox();
            this.tbTubus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRemaining = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbTubus
            // 
            this.lbTubus.AutoSize = true;
            this.lbTubus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTubus.Location = new System.Drawing.Point(32, 15);
            this.lbTubus.Name = "lbTubus";
            this.lbTubus.Size = new System.Drawing.Size(134, 13);
            this.lbTubus.TabIndex = 2;
            this.lbTubus.Text = "Mérendő tubus száma:";
            // 
            // lbLemert
            // 
            this.lbLemert.AutoSize = true;
            this.lbLemert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLemert.Location = new System.Drawing.Point(74, 38);
            this.lbLemert.Name = "lbLemert";
            this.lbLemert.Size = new System.Drawing.Size(92, 13);
            this.lbLemert.TabIndex = 4;
            this.lbLemert.Text = "Lemért csíkok:";
            // 
            // lbLot
            // 
            this.lbLot.AutoSize = true;
            this.lbLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLot.Location = new System.Drawing.Point(117, 109);
            this.lbLot.Name = "lbLot";
            this.lbLot.Size = new System.Drawing.Size(49, 13);
            this.lbLot.TabIndex = 5;
            this.lbLot.Text = "Lot_ID:";
            // 
            // lbRoll
            // 
            this.lbRoll.AutoSize = true;
            this.lbRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRoll.Location = new System.Drawing.Point(36, 83);
            this.lbRoll.Name = "lbRoll";
            this.lbRoll.Size = new System.Drawing.Size(130, 13);
            this.lbRoll.TabIndex = 6;
            this.lbRoll.Text = "Lemérendő csíkszám:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Clear All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btInvalidate
            // 
            this.btInvalidate.Location = new System.Drawing.Point(0, 0);
            this.btInvalidate.Name = "btInvalidate";
            this.btInvalidate.Size = new System.Drawing.Size(75, 23);
            this.btInvalidate.TabIndex = 0;
            // 
            // tbLot
            // 
            this.tbLot.Location = new System.Drawing.Point(192, 106);
            this.tbLot.Name = "tbLot";
            this.tbLot.Size = new System.Drawing.Size(70, 20);
            this.tbLot.TabIndex = 8;
            // 
            // tbNeedToMeasure
            // 
            this.tbNeedToMeasure.Location = new System.Drawing.Point(192, 80);
            this.tbNeedToMeasure.Name = "tbNeedToMeasure";
            this.tbNeedToMeasure.Size = new System.Drawing.Size(70, 20);
            this.tbNeedToMeasure.TabIndex = 9;
            // 
            // tbLemert
            // 
            this.tbLemert.Enabled = false;
            this.tbLemert.Location = new System.Drawing.Point(192, 38);
            this.tbLemert.Name = "tbLemert";
            this.tbLemert.Size = new System.Drawing.Size(70, 20);
            this.tbLemert.TabIndex = 10;
            // 
            // tbTubus
            // 
            this.tbTubus.Location = new System.Drawing.Point(192, 12);
            this.tbTubus.Name = "tbTubus";
            this.tbTubus.Size = new System.Drawing.Size(70, 20);
            this.tbTubus.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Hátralévő csíkok száma:";
            // 
            // tbRemaining
            // 
            this.tbRemaining.ForeColor = System.Drawing.Color.Red;
            this.tbRemaining.Location = new System.Drawing.Point(192, 58);
            this.tbRemaining.Name = "tbRemaining";
            this.tbRemaining.Size = new System.Drawing.Size(70, 20);
            this.tbRemaining.TabIndex = 13;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 164);
            this.progressBar1.Maximum = 16;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 21);
            this.progressBar1.TabIndex = 14;
            // 
            // CounterPanel
            // 
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tbRemaining);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTubus);
            this.Controls.Add(this.tbLemert);
            this.Controls.Add(this.tbNeedToMeasure);
            this.Controls.Add(this.tbLot);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbRoll);
            this.Controls.Add(this.lbLot);
            this.Controls.Add(this.lbLemert);
            this.Controls.Add(this.lbTubus);
            this.DoubleBuffered = true;
            this.Name = "CounterPanel";
            this.Size = new System.Drawing.Size(282, 196);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTubus;
        private System.Windows.Forms.Label lbLemert;
        private System.Windows.Forms.Label lbLot;
        private System.Windows.Forms.Label lbRoll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btInvalidate;
        private System.Windows.Forms.TextBox tbLot;
        private System.Windows.Forms.TextBox tbNeedToMeasure;
        public System.Windows.Forms.TextBox tbLemert;
        private System.Windows.Forms.TextBox tbTubus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRemaining;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}