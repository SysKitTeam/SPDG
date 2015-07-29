namespace Acceleratio.SPDG.UI
{
    partial class frm04Collections
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
            this.radioCreateNewSiteColl = new System.Windows.Forms.RadioButton();
            this.radioUseExisting = new System.Windows.Forms.RadioButton();
            this.trackNumSiteColls = new System.Windows.Forms.TrackBar();
            this.cboSiteCollection = new System.Windows.Forms.ComboBox();
            this.ucSteps1 = new Acceleratio.SPDG.UI.ucSteps();
            this.lblCreateSiteColls = new System.Windows.Forms.Label();
            this.txtOwnerEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOwnerUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumSiteColls)).BeginInit();
            this.SuspendLayout();
            // 
            // radioCreateNewSiteColl
            // 
            this.radioCreateNewSiteColl.AutoSize = true;
            this.radioCreateNewSiteColl.Location = new System.Drawing.Point(265, 115);
            this.radioCreateNewSiteColl.Name = "radioCreateNewSiteColl";
            this.radioCreateNewSiteColl.Size = new System.Drawing.Size(154, 17);
            this.radioCreateNewSiteColl.TabIndex = 7;
            this.radioCreateNewSiteColl.TabStop = true;
            this.radioCreateNewSiteColl.Text = "Create new Site Collections";
            this.radioCreateNewSiteColl.UseVisualStyleBackColor = true;
            this.radioCreateNewSiteColl.CheckedChanged += new System.EventHandler(this.radioCreateNewSiteColl_CheckedChanged);
            // 
            // radioUseExisting
            // 
            this.radioUseExisting.AutoSize = true;
            this.radioUseExisting.Location = new System.Drawing.Point(265, 295);
            this.radioUseExisting.Name = "radioUseExisting";
            this.radioUseExisting.Size = new System.Drawing.Size(153, 17);
            this.radioUseExisting.TabIndex = 8;
            this.radioUseExisting.TabStop = true;
            this.radioUseExisting.Text = "Use Existing Site Collection";
            this.radioUseExisting.UseVisualStyleBackColor = true;
            this.radioUseExisting.CheckedChanged += new System.EventHandler(this.radioUseExisting_CheckedChanged);
            // 
            // trackNumSiteColls
            // 
            this.trackNumSiteColls.Location = new System.Drawing.Point(257, 140);
            this.trackNumSiteColls.Name = "trackNumSiteColls";
            this.trackNumSiteColls.Size = new System.Drawing.Size(396, 45);
            this.trackNumSiteColls.TabIndex = 11;
            this.trackNumSiteColls.ValueChanged += new System.EventHandler(this.trackNumSiteColls_ValueChanged);
            // 
            // cboSiteCollection
            // 
            this.cboSiteCollection.FormattingEnabled = true;
            this.cboSiteCollection.Location = new System.Drawing.Point(265, 320);
            this.cboSiteCollection.Name = "cboSiteCollection";
            this.cboSiteCollection.Size = new System.Drawing.Size(409, 21);
            this.cboSiteCollection.TabIndex = 14;
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(231, 372);
            this.ucSteps1.TabIndex = 16;
            // 
            // lblCreateSiteColls
            // 
            this.lblCreateSiteColls.AutoSize = true;
            this.lblCreateSiteColls.Location = new System.Drawing.Point(657, 143);
            this.lblCreateSiteColls.Name = "lblCreateSiteColls";
            this.lblCreateSiteColls.Size = new System.Drawing.Size(13, 13);
            this.lblCreateSiteColls.TabIndex = 17;
            this.lblCreateSiteColls.Text = "0";
            // 
            // txtOwnerEmail
            // 
            this.txtOwnerEmail.Enabled = false;
            this.txtOwnerEmail.Location = new System.Drawing.Point(400, 217);
            this.txtOwnerEmail.Name = "txtOwnerEmail";
            this.txtOwnerEmail.Size = new System.Drawing.Size(163, 20);
            this.txtOwnerEmail.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(265, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Owner Email:";
            // 
            // txtOwnerUserName
            // 
            this.txtOwnerUserName.Enabled = false;
            this.txtOwnerUserName.Location = new System.Drawing.Point(400, 191);
            this.txtOwnerUserName.Name = "txtOwnerUserName";
            this.txtOwnerUserName.Size = new System.Drawing.Size(163, 20);
            this.txtOwnerUserName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(265, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Owner UserName:";
            // 
            // frm04Collections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.txtOwnerEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOwnerUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCreateSiteColls);
            this.Controls.Add(this.ucSteps1);
            this.Controls.Add(this.cboSiteCollection);
            this.Controls.Add(this.trackNumSiteColls);
            this.Controls.Add(this.radioUseExisting);
            this.Controls.Add(this.radioCreateNewSiteColl);
            this.Name = "frm04Collections";
            this.Text = "frm04Collections";
            this.Load += new System.EventHandler(this.frm04Collections_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.radioCreateNewSiteColl, 0);
            this.Controls.SetChildIndex(this.radioUseExisting, 0);
            this.Controls.SetChildIndex(this.trackNumSiteColls, 0);
            this.Controls.SetChildIndex(this.cboSiteCollection, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.lblCreateSiteColls, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtOwnerUserName, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtOwnerEmail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackNumSiteColls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioCreateNewSiteColl;
        private System.Windows.Forms.RadioButton radioUseExisting;
        private System.Windows.Forms.TrackBar trackNumSiteColls;
        private System.Windows.Forms.ComboBox cboSiteCollection;
        private ucSteps ucSteps1;
        private System.Windows.Forms.Label lblCreateSiteColls;
        private System.Windows.Forms.TextBox txtOwnerEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOwnerUserName;
        private System.Windows.Forms.Label label1;
    }
}