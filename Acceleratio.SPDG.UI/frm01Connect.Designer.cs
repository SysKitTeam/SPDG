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
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(225, 372);
            this.ucSteps1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Enter URL to your SharePoint site:";
            // 
            // txtSharePointSiteURL
            // 
            this.txtSharePointSiteURL.Location = new System.Drawing.Point(269, 135);
            this.txtSharePointSiteURL.Name = "txtSharePointSiteURL";
            this.txtSharePointSiteURL.Size = new System.Drawing.Size(368, 20);
            this.txtSharePointSiteURL.TabIndex = 9;
            // 
            // radioConnectSPOnline
            // 
            this.radioConnectSPOnline.AutoSize = true;
            this.radioConnectSPOnline.Location = new System.Drawing.Point(269, 185);
            this.radioConnectSPOnline.Name = "radioConnectSPOnline";
            this.radioConnectSPOnline.Size = new System.Drawing.Size(165, 17);
            this.radioConnectSPOnline.TabIndex = 12;
            this.radioConnectSPOnline.Text = "Connect to SharePoint Online";
            this.radioConnectSPOnline.UseVisualStyleBackColor = true;
            // 
            // radioConnectSPOnPremise
            // 
            this.radioConnectSPOnPremise.AutoSize = true;
            this.radioConnectSPOnPremise.Checked = true;
            this.radioConnectSPOnPremise.Location = new System.Drawing.Point(269, 208);
            this.radioConnectSPOnPremise.Name = "radioConnectSPOnPremise";
            this.radioConnectSPOnPremise.Size = new System.Drawing.Size(189, 17);
            this.radioConnectSPOnPremise.TabIndex = 13;
            this.radioConnectSPOnPremise.TabStop = true;
            this.radioConnectSPOnPremise.Text = "Connect to SharePoint On-Premise";
            this.radioConnectSPOnPremise.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioCustomCredentials);
            this.panel1.Controls.Add(this.radioCurrentCredentials);
            this.panel1.Location = new System.Drawing.Point(269, 253);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 49);
            this.panel1.TabIndex = 14;
            // 
            // radioCustomCredentials
            // 
            this.radioCustomCredentials.AutoSize = true;
            this.radioCustomCredentials.Location = new System.Drawing.Point(0, 26);
            this.radioCustomCredentials.Name = "radioCustomCredentials";
            this.radioCustomCredentials.Size = new System.Drawing.Size(114, 17);
            this.radioCustomCredentials.TabIndex = 1;
            this.radioCustomCredentials.TabStop = true;
            this.radioCustomCredentials.Text = "Custom credentials";
            this.radioCustomCredentials.UseVisualStyleBackColor = true;
            this.radioCustomCredentials.CheckedChanged += new System.EventHandler(this.radioCustomCredentials_CheckedChanged);
            // 
            // radioCurrentCredentials
            // 
            this.radioCurrentCredentials.AutoSize = true;
            this.radioCurrentCredentials.Location = new System.Drawing.Point(0, 3);
            this.radioCurrentCredentials.Name = "radioCurrentCredentials";
            this.radioCurrentCredentials.Size = new System.Drawing.Size(166, 17);
            this.radioCurrentCredentials.TabIndex = 0;
            this.radioCurrentCredentials.TabStop = true;
            this.radioCurrentCredentials.Text = "Credentials of the current user";
            this.radioCurrentCredentials.UseVisualStyleBackColor = true;
            this.radioCurrentCredentials.CheckedChanged += new System.EventHandler(this.radioCurrentCredentials_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(289, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Specify credentials";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Username:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(424, 346);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(213, 20);
            this.txtUserName.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 349);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(424, 372);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(213, 20);
            this.txtPassword.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 375);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Password:";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(424, 398);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(213, 20);
            this.txtDomain.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 401);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Domain:";
            // 
            // frm01Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
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
            this.Controls.SetChildIndex(this.label1, 0);
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
            this.Controls.SetChildIndex(this.label4, 0);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label label7;
    }
}