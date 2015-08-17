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
            // radioCreateNewSiteColl
            // 
            this.radioCreateNewSiteColl.AutoSize = true;
            this.radioCreateNewSiteColl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioCreateNewSiteColl.Location = new System.Drawing.Point(265, 155);
            this.radioCreateNewSiteColl.Name = "radioCreateNewSiteColl";
            this.radioCreateNewSiteColl.Size = new System.Drawing.Size(168, 19);
            this.radioCreateNewSiteColl.TabIndex = 7;
            this.radioCreateNewSiteColl.TabStop = true;
            this.radioCreateNewSiteColl.Text = "Create new Site Collections";
            this.radioCreateNewSiteColl.UseVisualStyleBackColor = true;
            this.radioCreateNewSiteColl.CheckedChanged += new System.EventHandler(this.radioCreateNewSiteColl_CheckedChanged);
            // 
            // radioUseExisting
            // 
            this.radioUseExisting.AutoSize = true;
            this.radioUseExisting.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioUseExisting.Location = new System.Drawing.Point(261, 333);
            this.radioUseExisting.Name = "radioUseExisting";
            this.radioUseExisting.Size = new System.Drawing.Size(166, 19);
            this.radioUseExisting.TabIndex = 8;
            this.radioUseExisting.TabStop = true;
            this.radioUseExisting.Text = "Use Existing Site Collection";
            this.radioUseExisting.UseVisualStyleBackColor = true;
            this.radioUseExisting.CheckedChanged += new System.EventHandler(this.radioUseExisting_CheckedChanged);
            // 
            // trackNumSiteColls
            // 
            this.trackNumSiteColls.LargeChange = 1;
            this.trackNumSiteColls.Location = new System.Drawing.Point(256, 180);
            this.trackNumSiteColls.Name = "trackNumSiteColls";
            this.trackNumSiteColls.Size = new System.Drawing.Size(550, 45);
            this.trackNumSiteColls.TabIndex = 11;
            this.trackNumSiteColls.ValueChanged += new System.EventHandler(this.trackNumSiteColls_ValueChanged);
            // 
            // cboSiteCollection
            // 
            this.cboSiteCollection.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSiteCollection.FormattingEnabled = true;
            this.cboSiteCollection.Location = new System.Drawing.Point(261, 358);
            this.cboSiteCollection.Name = "cboSiteCollection";
            this.cboSiteCollection.Size = new System.Drawing.Size(542, 23);
            this.cboSiteCollection.TabIndex = 14;
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 135);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(231, 465);
            this.ucSteps1.TabIndex = 16;
            // 
            // lblCreateSiteColls
            // 
            this.lblCreateSiteColls.AutoSize = true;
            this.lblCreateSiteColls.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCreateSiteColls.Location = new System.Drawing.Point(819, 183);
            this.lblCreateSiteColls.Name = "lblCreateSiteColls";
            this.lblCreateSiteColls.Size = new System.Drawing.Size(13, 15);
            this.lblCreateSiteColls.TabIndex = 17;
            this.lblCreateSiteColls.Text = "0";
            // 
            // txtOwnerEmail
            // 
            this.txtOwnerEmail.Enabled = false;
            this.txtOwnerEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOwnerEmail.Location = new System.Drawing.Point(396, 255);
            this.txtOwnerEmail.Name = "txtOwnerEmail";
            this.txtOwnerEmail.Size = new System.Drawing.Size(163, 23);
            this.txtOwnerEmail.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(261, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Owner Email:";
            // 
            // txtOwnerUserName
            // 
            this.txtOwnerUserName.Enabled = false;
            this.txtOwnerUserName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOwnerUserName.Location = new System.Drawing.Point(396, 229);
            this.txtOwnerUserName.Name = "txtOwnerUserName";
            this.txtOwnerUserName.Size = new System.Drawing.Size(163, 23);
            this.txtOwnerUserName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(261, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Owner UserName:";
            // 
            // frm04Collections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
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