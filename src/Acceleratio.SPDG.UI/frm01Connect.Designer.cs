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
            this.components = new System.ComponentModel.Container();
            this.ucSteps1 = new Acceleratio.SPDG.UI.ucSteps();
            this.radioConnectSPOnline = new System.Windows.Forms.RadioButton();
            this.radioConnectSPOnPremise = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioCustomCredentials = new System.Windows.Forms.RadioButton();
            this.radioCurrentCredentials = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTenantName = new System.Windows.Forms.TextBox();
            this.lblTenantName = new System.Windows.Forms.Label();
            this.texboxToolTip = new System.Windows.Forms.ToolTip(this.components);
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
            // radioConnectSPOnline
            // 
            this.radioConnectSPOnline.AutoSize = true;
            this.radioConnectSPOnline.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioConnectSPOnline.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioConnectSPOnline.Location = new System.Drawing.Point(268, 166);
            this.radioConnectSPOnline.Name = "radioConnectSPOnline";
            this.radioConnectSPOnline.Size = new System.Drawing.Size(182, 19);
            this.radioConnectSPOnline.TabIndex = 12;
            this.radioConnectSPOnline.Text = "Connect to SharePoint Online";
            this.radioConnectSPOnline.UseVisualStyleBackColor = true;
            this.radioConnectSPOnline.CheckedChanged += new System.EventHandler(this.uiEventHandler);
            // 
            // radioConnectSPOnPremise
            // 
            this.radioConnectSPOnPremise.AutoSize = true;
            this.radioConnectSPOnPremise.Checked = true;
            this.radioConnectSPOnPremise.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioConnectSPOnPremise.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioConnectSPOnPremise.Location = new System.Drawing.Point(268, 189);
            this.radioConnectSPOnPremise.Name = "radioConnectSPOnPremise";
            this.radioConnectSPOnPremise.Size = new System.Drawing.Size(210, 19);
            this.radioConnectSPOnPremise.TabIndex = 13;
            this.radioConnectSPOnPremise.TabStop = true;
            this.radioConnectSPOnPremise.Text = "Connect to SharePoint On-Premise";
            this.radioConnectSPOnPremise.UseVisualStyleBackColor = true;
            this.radioConnectSPOnPremise.CheckedChanged += new System.EventHandler(this.uiEventHandler);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioCustomCredentials);
            this.panel1.Controls.Add(this.radioCurrentCredentials);
            this.panel1.Location = new System.Drawing.Point(268, 240);
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
            this.radioCustomCredentials.Text = "Custom credentials";
            this.radioCustomCredentials.UseVisualStyleBackColor = true;
            this.radioCustomCredentials.CheckedChanged += new System.EventHandler(this.uiEventHandler);
            // 
            // radioCurrentCredentials
            // 
            this.radioCurrentCredentials.AutoSize = true;
            this.radioCurrentCredentials.Checked = true;
            this.radioCurrentCredentials.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioCurrentCredentials.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.radioCurrentCredentials.Location = new System.Drawing.Point(0, 3);
            this.radioCurrentCredentials.Name = "radioCurrentCredentials";
            this.radioCurrentCredentials.Size = new System.Drawing.Size(184, 19);
            this.radioCurrentCredentials.TabIndex = 0;
            this.radioCurrentCredentials.TabStop = true;
            this.radioCurrentCredentials.Text = "Credentials of the current user";
            this.radioCurrentCredentials.UseVisualStyleBackColor = true;
            this.radioCurrentCredentials.CheckedChanged += new System.EventHandler(this.uiEventHandler);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(290, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Specify credentials";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUserName.Location = new System.Drawing.Point(474, 329);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(383, 23);
            this.txtUserName.TabIndex = 17;
            this.txtUserName.Enter += new System.EventHandler(this.txtUserName_Enter);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblUserName.Location = new System.Drawing.Point(290, 332);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(63, 15);
            this.lblUserName.TabIndex = 16;
            this.lblUserName.Text = "Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.Location = new System.Drawing.Point(474, 355);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(383, 23);
            this.txtPassword.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(290, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Password:";
            // 
            // txtTenantName
            // 
            this.txtTenantName.Location = new System.Drawing.Point(474, 381);
            this.txtTenantName.Name = "txtTenantName";
            this.txtTenantName.Size = new System.Drawing.Size(383, 20);
            this.txtTenantName.TabIndex = 21;
            this.txtTenantName.Enter += new System.EventHandler(this.txtTenantName_Enter);
            this.txtTenantName.Leave += new System.EventHandler(this.txtTenantName_Leave);
            // 
            // lblTenantName
            // 
            this.lblTenantName.AutoSize = true;
            this.lblTenantName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenantName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTenantName.Location = new System.Drawing.Point(290, 383);
            this.lblTenantName.Name = "lblTenantName";
            this.lblTenantName.Size = new System.Drawing.Size(81, 15);
            this.lblTenantName.TabIndex = 20;
            this.lblTenantName.Text = "Tenant Name:";
            // 
            // texboxToolTip
            // 
            this.texboxToolTip.ShowAlways = true;
            this.texboxToolTip.ToolTipTitle = "Help";
            // 
            // frm01Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.txtTenantName);
            this.Controls.Add(this.lblTenantName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioConnectSPOnPremise);
            this.Controls.Add(this.radioConnectSPOnline);
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
            this.Controls.SetChildIndex(this.radioConnectSPOnline, 0);
            this.Controls.SetChildIndex(this.radioConnectSPOnPremise, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblUserName, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.lblTenantName, 0);
            this.Controls.SetChildIndex(this.txtTenantName, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.RadioButton radioConnectSPOnline;
        private System.Windows.Forms.RadioButton radioConnectSPOnPremise;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioCustomCredentials;
        private System.Windows.Forms.RadioButton radioCurrentCredentials;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTenantName;
        private System.Windows.Forms.Label lblTenantName;
        private System.Windows.Forms.ToolTip texboxToolTip;
    }
}