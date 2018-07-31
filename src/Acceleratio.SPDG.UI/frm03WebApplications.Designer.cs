namespace Acceleratio.SPDG.UI
{
    partial class frm03WebApplications
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
            this.radioCreateNewWebApp = new System.Windows.Forms.RadioButton();
            this.cboUseExistingWebApp = new System.Windows.Forms.ComboBox();
            this.radioUseExistingWebApp = new System.Windows.Forms.RadioButton();
            this.trackCreateNewWebApplication = new System.Windows.Forms.TrackBar();
            this.lblCreateNewApps = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOwnerUserName = new System.Windows.Forms.TextBox();
            this.txtOwnerPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOwnerEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackCreateNewWebApplication)).BeginInit();
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
            this.ucSteps1.Size = new System.Drawing.Size(238, 463);
            this.ucSteps1.TabIndex = 7;
            // 
            // radioCreateNewWebApp
            // 
            this.radioCreateNewWebApp.AutoSize = true;
            this.radioCreateNewWebApp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioCreateNewWebApp.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioCreateNewWebApp.Location = new System.Drawing.Point(265, 155);
            this.radioCreateNewWebApp.Name = "radioCreateNewWebApp";
            this.radioCreateNewWebApp.Size = new System.Drawing.Size(180, 19);
            this.radioCreateNewWebApp.TabIndex = 8;
            this.radioCreateNewWebApp.TabStop = true;
            this.radioCreateNewWebApp.Text = "Create new Web Applications";
            this.radioCreateNewWebApp.UseVisualStyleBackColor = true;
            this.radioCreateNewWebApp.CheckedChanged += new System.EventHandler(this.radioCreateNewWebApp_CheckedChanged);
            // 
            // cboUseExistingWebApp
            // 
            this.cboUseExistingWebApp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboUseExistingWebApp.FormattingEnabled = true;
            this.cboUseExistingWebApp.Location = new System.Drawing.Point(265, 392);
            this.cboUseExistingWebApp.Name = "cboUseExistingWebApp";
            this.cboUseExistingWebApp.Size = new System.Drawing.Size(538, 23);
            this.cboUseExistingWebApp.TabIndex = 11;
            // 
            // radioUseExistingWebApp
            // 
            this.radioUseExistingWebApp.AutoSize = true;
            this.radioUseExistingWebApp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioUseExistingWebApp.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioUseExistingWebApp.Location = new System.Drawing.Point(265, 367);
            this.radioUseExistingWebApp.Name = "radioUseExistingWebApp";
            this.radioUseExistingWebApp.Size = new System.Drawing.Size(87, 19);
            this.radioUseExistingWebApp.TabIndex = 9;
            this.radioUseExistingWebApp.TabStop = true;
            this.radioUseExistingWebApp.Text = "Use Existing";
            this.radioUseExistingWebApp.UseVisualStyleBackColor = true;
            this.radioUseExistingWebApp.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // trackCreateNewWebApplication
            // 
            this.trackCreateNewWebApplication.LargeChange = 1;
            this.trackCreateNewWebApplication.Location = new System.Drawing.Point(257, 179);
            this.trackCreateNewWebApplication.Name = "trackCreateNewWebApplication";
            this.trackCreateNewWebApplication.Size = new System.Drawing.Size(546, 45);
            this.trackCreateNewWebApplication.TabIndex = 10;
            this.trackCreateNewWebApplication.ValueChanged += new System.EventHandler(this.trackCreateNewWebApplication_ValueChanged);
            // 
            // lblCreateNewApps
            // 
            this.lblCreateNewApps.AutoSize = true;
            this.lblCreateNewApps.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCreateNewApps.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCreateNewApps.Location = new System.Drawing.Point(812, 183);
            this.lblCreateNewApps.Name = "lblCreateNewApps";
            this.lblCreateNewApps.Size = new System.Drawing.Size(13, 15);
            this.lblCreateNewApps.TabIndex = 12;
            this.lblCreateNewApps.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(266, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Owner UserName:";
            // 
            // txtOwnerUserName
            // 
            this.txtOwnerUserName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOwnerUserName.Location = new System.Drawing.Point(401, 229);
            this.txtOwnerUserName.Name = "txtOwnerUserName";
            this.txtOwnerUserName.Size = new System.Drawing.Size(163, 23);
            this.txtOwnerUserName.TabIndex = 14;
            // 
            // txtOwnerPassword
            // 
            this.txtOwnerPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOwnerPassword.Location = new System.Drawing.Point(401, 255);
            this.txtOwnerPassword.Name = "txtOwnerPassword";
            this.txtOwnerPassword.PasswordChar = '*';
            this.txtOwnerPassword.Size = new System.Drawing.Size(163, 23);
            this.txtOwnerPassword.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(266, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Owner Password:";
            // 
            // txtOwnerEmail
            // 
            this.txtOwnerEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOwnerEmail.Location = new System.Drawing.Point(401, 281);
            this.txtOwnerEmail.Name = "txtOwnerEmail";
            this.txtOwnerEmail.Size = new System.Drawing.Size(163, 23);
            this.txtOwnerEmail.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(266, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Owner Email:";
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSQLServer.Location = new System.Drawing.Point(401, 307);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Size = new System.Drawing.Size(163, 23);
            this.txtSQLServer.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(266, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "SQL Server:";
            // 
            // frm03WebApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.txtSQLServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOwnerEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOwnerPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOwnerUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCreateNewApps);
            this.Controls.Add(this.trackCreateNewWebApplication);
            this.Controls.Add(this.cboUseExistingWebApp);
            this.Controls.Add(this.radioUseExistingWebApp);
            this.Controls.Add(this.radioCreateNewWebApp);
            this.Controls.Add(this.ucSteps1);
            this.MaximumSize = new System.Drawing.Size(937, 683);
            this.MinimumSize = new System.Drawing.Size(937, 683);
            this.Name = "frm03WebApplications";
            this.Text = "frm03WebApplications";
            this.Load += new System.EventHandler(this.frm03WebApplications_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.radioCreateNewWebApp, 0);
            this.Controls.SetChildIndex(this.radioUseExistingWebApp, 0);
            this.Controls.SetChildIndex(this.cboUseExistingWebApp, 0);
            this.Controls.SetChildIndex(this.trackCreateNewWebApplication, 0);
            this.Controls.SetChildIndex(this.lblCreateNewApps, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtOwnerUserName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtOwnerPassword, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtOwnerEmail, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtSQLServer, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackCreateNewWebApplication)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.RadioButton radioCreateNewWebApp;
        private System.Windows.Forms.ComboBox cboUseExistingWebApp;
        private System.Windows.Forms.RadioButton radioUseExistingWebApp;
        private System.Windows.Forms.TrackBar trackCreateNewWebApplication;
        private System.Windows.Forms.Label lblCreateNewApps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOwnerUserName;
        private System.Windows.Forms.TextBox txtOwnerPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOwnerEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSQLServer;
        private System.Windows.Forms.Label label4;
    }
}