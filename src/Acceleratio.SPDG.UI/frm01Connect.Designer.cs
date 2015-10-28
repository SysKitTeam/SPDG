namespace Acceleratio.SPDG.UI
{
    partial class frm01Connect
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
            this.txtSharePointSiteURL = new System.Windows.Forms.TextBox();
            this.radioConnectSPOnline = new System.Windows.Forms.RadioButton();
            this.radioConnectSPOnPremise = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioCustomCredentials = new System.Windows.Forms.RadioButton();
            this.radioCurrentCredentials = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.ucSteps1.Size = new System.Drawing.Size(225, 461);
            this.ucSteps1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(265, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Enter URL to your SharePoint site:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtSharePointSiteURL
            // 
            this.txtSharePointSiteURL.Enabled = false;
            this.txtSharePointSiteURL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSharePointSiteURL.Location = new System.Drawing.Point(268, 173);
            this.txtSharePointSiteURL.Name = "txtSharePointSiteURL";
            this.txtSharePointSiteURL.Size = new System.Drawing.Size(368, 23);
            this.txtSharePointSiteURL.TabIndex = 9;
            // 
            // radioConnectSPOnline
            // 
            this.radioConnectSPOnline.AutoSize = true;
            this.radioConnectSPOnline.Enabled = false;
            this.radioConnectSPOnline.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioConnectSPOnline.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioConnectSPOnline.Location = new System.Drawing.Point(268, 223);
            this.radioConnectSPOnline.Name = "radioConnectSPOnline";
            this.radioConnectSPOnline.Size = new System.Drawing.Size(182, 19);
            this.radioConnectSPOnline.TabIndex = 12;
            this.radioConnectSPOnline.Text = "Connect to SharePoint Online";
            this.radioConnectSPOnline.UseVisualStyleBackColor = true;
            // 
            // radioConnectSPOnPremise
            // 
            this.radioConnectSPOnPremise.AutoSize = true;
            this.radioConnectSPOnPremise.Checked = true;
            this.radioConnectSPOnPremise.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioConnectSPOnPremise.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioConnectSPOnPremise.Location = new System.Drawing.Point(268, 246);
            this.radioConnectSPOnPremise.Name = "radioConnectSPOnPremise";
            this.radioConnectSPOnPremise.Size = new System.Drawing.Size(210, 19);
            this.radioConnectSPOnPremise.TabIndex = 13;
            this.radioConnectSPOnPremise.TabStop = true;
            this.radioConnectSPOnPremise.Text = "Connect to SharePoint On-Premise";
            this.radioConnectSPOnPremise.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioCustomCredentials);
            this.panel1.Controls.Add(this.radioCurrentCredentials);
            this.panel1.Location = new System.Drawing.Point(268, 291);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 49);
            this.panel1.TabIndex = 14;
            // 
            // radioCustomCredentials
            // 
            this.radioCustomCredentials.AutoSize = true;
            this.radioCustomCredentials.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioCustomCredentials.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioCustomCredentials.Location = new System.Drawing.Point(0, 26);
            this.radioCustomCredentials.Name = "radioCustomCredentials";
            this.radioCustomCredentials.Size = new System.Drawing.Size(127, 19);
            this.radioCustomCredentials.TabIndex = 1;
            this.radioCustomCredentials.TabStop = true;
            this.radioCustomCredentials.Text = "Custom credentials";
            this.radioCustomCredentials.UseVisualStyleBackColor = true;
            this.radioCustomCredentials.CheckedChanged += new System.EventHandler(this.radioCustomCredentials_CheckedChanged);
            // 
            // radioCurrentCredentials
            // 
            this.radioCurrentCredentials.AutoSize = true;
            this.radioCurrentCredentials.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioCurrentCredentials.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioCurrentCredentials.Location = new System.Drawing.Point(0, 3);
            this.radioCurrentCredentials.Name = "radioCurrentCredentials";
            this.radioCurrentCredentials.Size = new System.Drawing.Size(184, 19);
            this.radioCurrentCredentials.TabIndex = 0;
            this.radioCurrentCredentials.TabStop = true;
            this.radioCurrentCredentials.Text = "Credentials of the current user";
            this.radioCurrentCredentials.UseVisualStyleBackColor = true;
            this.radioCurrentCredentials.CheckedChanged += new System.EventHandler(this.radioCurrentCredentials_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(288, 356);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Specify credentials";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUserName.Location = new System.Drawing.Point(423, 384);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(213, 23);
            this.txtUserName.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(288, 387);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.Location = new System.Drawing.Point(423, 410);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(213, 23);
            this.txtPassword.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(288, 413);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Password:";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(423, 436);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(213, 20);
            this.txtDomain.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(288, 438);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "Domain:";
            // 
            // frm01Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioConnectSPOnPremise);
            this.Controls.Add(this.radioConnectSPOnline);
            this.Controls.Add(this.txtSharePointSiteURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm01Connect";
            this.Text = "frm01Connect";
            this.VisibleChanged += new System.EventHandler(this.frm01Connect_VisibleChanged);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtSharePointSiteURL, 0);
            this.Controls.SetChildIndex(this.radioConnectSPOnline, 0);
            this.Controls.SetChildIndex(this.radioConnectSPOnPremise, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtDomain, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSharePointSiteURL;
        private System.Windows.Forms.RadioButton radioConnectSPOnline;
        private System.Windows.Forms.RadioButton radioConnectSPOnPremise;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioCustomCredentials;
        private System.Windows.Forms.RadioButton radioCurrentCredentials;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label label7;
    }
}