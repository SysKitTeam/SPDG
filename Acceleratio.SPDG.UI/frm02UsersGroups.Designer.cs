namespace Acceleratio.SPDG.UI
{
    partial class frm02UsersGroups
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
            this.chkGenerateUsers = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackNumberOfSecGroups = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackNumberOfUsers = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOrganizationalUnit = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumberOfSecGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumberOfUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(235, 366);
            this.ucSteps1.TabIndex = 7;
            // 
            // chkGenerateUsers
            // 
            this.chkGenerateUsers.AutoSize = true;
            this.chkGenerateUsers.Location = new System.Drawing.Point(265, 115);
            this.chkGenerateUsers.Name = "chkGenerateUsers";
            this.chkGenerateUsers.Size = new System.Drawing.Size(288, 17);
            this.chkGenerateUsers.TabIndex = 8;
            this.chkGenerateUsers.Text = "Generate Users and Security Groups in Active Directory";
            this.chkGenerateUsers.UseVisualStyleBackColor = true;
            this.chkGenerateUsers.CheckedChanged += new System.EventHandler(this.chkGenerateUsers_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackNumberOfSecGroups);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.trackNumberOfUsers);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboOrganizationalUnit);
            this.groupBox1.Location = new System.Drawing.Point(265, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 313);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Users";
            // 
            // trackNumberOfSecGroups
            // 
            this.trackNumberOfSecGroups.Location = new System.Drawing.Point(10, 230);
            this.trackNumberOfSecGroups.Name = "trackNumberOfSecGroups";
            this.trackNumberOfSecGroups.Size = new System.Drawing.Size(419, 45);
            this.trackNumberOfSecGroups.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Number of Security Groups to create:";
            // 
            // trackNumberOfUsers
            // 
            this.trackNumberOfUsers.Location = new System.Drawing.Point(10, 141);
            this.trackNumberOfUsers.Name = "trackNumberOfUsers";
            this.trackNumberOfUsers.Size = new System.Drawing.Size(419, 45);
            this.trackNumberOfUsers.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number of users to create:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Organizational Unit:";
            // 
            // cboOrganizationalUnit
            // 
            this.cboOrganizationalUnit.FormattingEnabled = true;
            this.cboOrganizationalUnit.Location = new System.Drawing.Point(18, 53);
            this.cboOrganizationalUnit.Name = "cboOrganizationalUnit";
            this.cboOrganizationalUnit.Size = new System.Drawing.Size(411, 21);
            this.cboOrganizationalUnit.TabIndex = 0;
            // 
            // frm02UsersGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkGenerateUsers);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm02UsersGroups";
            this.Text = "frm02UsersGroups";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.chkGenerateUsers, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumberOfSecGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumberOfUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.CheckBox chkGenerateUsers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOrganizationalUnit;
        private System.Windows.Forms.TrackBar trackNumberOfSecGroups;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackNumberOfUsers;
        private System.Windows.Forms.Label label3;
    }
}