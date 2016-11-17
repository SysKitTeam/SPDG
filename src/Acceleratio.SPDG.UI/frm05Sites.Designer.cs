namespace Acceleratio.SPDG.UI
{
    partial class frm05Sites
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.ucSteps1 = new Acceleratio.SPDG.UI.ucSteps();
            this.label2 = new System.Windows.Forms.Label();
            this.trackNumSitesToCreate = new System.Windows.Forms.TrackBar();
            this.trackMaxNumberLevels = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumSites = new System.Windows.Forms.Label();
            this.lblNumberLevels = new System.Windows.Forms.Label();
            this.cboSiteTemplates = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSiteOptions = new System.Windows.Forms.Panel();
            this.chkUseOnlyExistingSites = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumSitesToCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberLevels)).BeginInit();
            this.panelSiteOptions.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderSize = 0;
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            // 
            // btnHelp
            // 
            this.btnHelp.FlatAppearance.BorderSize = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 135);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(232, 480);
            this.ucSteps1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(16, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Number of Sites to Create";
            // 
            // trackNumSitesToCreate
            // 
            this.trackNumSitesToCreate.LargeChange = 50;
            this.trackNumSitesToCreate.Location = new System.Drawing.Point(11, 34);
            this.trackNumSitesToCreate.Maximum = 500;
            this.trackNumSitesToCreate.Minimum = 1;
            this.trackNumSitesToCreate.Name = "trackNumSitesToCreate";
            this.trackNumSitesToCreate.Size = new System.Drawing.Size(543, 45);
            this.trackNumSitesToCreate.TabIndex = 12;
            this.trackNumSitesToCreate.Value = 50;
            this.trackNumSitesToCreate.ValueChanged += new System.EventHandler(this.trackNumSitesToCreate_ValueChanged);
            // 
            // trackMaxNumberLevels
            // 
            this.trackMaxNumberLevels.LargeChange = 1;
            this.trackMaxNumberLevels.Location = new System.Drawing.Point(11, 131);
            this.trackMaxNumberLevels.Minimum = 1;
            this.trackMaxNumberLevels.Name = "trackMaxNumberLevels";
            this.trackMaxNumberLevels.Size = new System.Drawing.Size(543, 45);
            this.trackMaxNumberLevels.TabIndex = 14;
            this.trackMaxNumberLevels.Value = 3;
            this.trackMaxNumberLevels.ValueChanged += new System.EventHandler(this.trackMaxNumberLevels_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(16, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Maximal Number of Site Levels";
            // 
            // lblNumSites
            // 
            this.lblNumSites.AutoSize = true;
            this.lblNumSites.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNumSites.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumSites.Location = new System.Drawing.Point(569, 38);
            this.lblNumSites.Name = "lblNumSites";
            this.lblNumSites.Size = new System.Drawing.Size(19, 15);
            this.lblNumSites.TabIndex = 15;
            this.lblNumSites.Text = "50";
            // 
            // lblNumberLevels
            // 
            this.lblNumberLevels.AutoSize = true;
            this.lblNumberLevels.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNumberLevels.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumberLevels.Location = new System.Drawing.Point(570, 134);
            this.lblNumberLevels.Name = "lblNumberLevels";
            this.lblNumberLevels.Size = new System.Drawing.Size(13, 15);
            this.lblNumberLevels.TabIndex = 16;
            this.lblNumberLevels.Text = "3";
            // 
            // cboSiteTemplates
            // 
            this.cboSiteTemplates.FormattingEnabled = true;
            this.cboSiteTemplates.Location = new System.Drawing.Point(19, 228);
            this.cboSiteTemplates.Name = "cboSiteTemplates";
            this.cboSiteTemplates.Size = new System.Drawing.Size(526, 21);
            this.cboSiteTemplates.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(16, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Use Site Template";
            // 
            // panelSiteOptions
            // 
            this.panelSiteOptions.Controls.Add(this.label2);
            this.panelSiteOptions.Controls.Add(this.label1);
            this.panelSiteOptions.Controls.Add(this.trackNumSitesToCreate);
            this.panelSiteOptions.Controls.Add(this.cboSiteTemplates);
            this.panelSiteOptions.Controls.Add(this.label3);
            this.panelSiteOptions.Controls.Add(this.lblNumberLevels);
            this.panelSiteOptions.Controls.Add(this.trackMaxNumberLevels);
            this.panelSiteOptions.Controls.Add(this.lblNumSites);
            this.panelSiteOptions.Location = new System.Drawing.Point(3, 26);
            this.panelSiteOptions.Name = "panelSiteOptions";
            this.panelSiteOptions.Size = new System.Drawing.Size(620, 321);
            this.panelSiteOptions.TabIndex = 19;
            // 
            // chkUseOnlyExistingSites
            // 
            this.chkUseOnlyExistingSites.AutoSize = true;
            this.chkUseOnlyExistingSites.Location = new System.Drawing.Point(3, 3);
            this.chkUseOnlyExistingSites.Name = "chkUseOnlyExistingSites";
            this.chkUseOnlyExistingSites.Size = new System.Drawing.Size(129, 17);
            this.chkUseOnlyExistingSites.TabIndex = 19;
            this.chkUseOnlyExistingSites.Text = "Use only existing sites";
            this.chkUseOnlyExistingSites.UseVisualStyleBackColor = true;
            this.chkUseOnlyExistingSites.CheckedChanged += new System.EventHandler(this.chkUseOnlyExistingSites_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chkUseOnlyExistingSites);
            this.flowLayoutPanel1.Controls.Add(this.panelSiteOptions);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(265, 155);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(633, 383);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // frm05Sites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm05Sites";
            this.Text = "frm05Sites";
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackNumSitesToCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberLevels)).EndInit();
            this.panelSiteOptions.ResumeLayout(false);
            this.panelSiteOptions.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackNumSitesToCreate;
        private System.Windows.Forms.TrackBar trackMaxNumberLevels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumSites;
        private System.Windows.Forms.Label lblNumberLevels;
        private System.Windows.Forms.ComboBox cboSiteTemplates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSiteOptions;
        private System.Windows.Forms.CheckBox chkUseOnlyExistingSites;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}