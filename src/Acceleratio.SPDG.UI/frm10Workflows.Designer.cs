namespace Acceleratio.SPDG.UI
{
    partial class frm10Workflows
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
            this.chkCreateSomeOutOfTheBoxSPworkflows = new System.Windows.Forms.CheckBox();
            this.chkAttachCustomWF = new System.Windows.Forms.CheckBox();
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
            this.ucSteps1.Size = new System.Drawing.Size(235, 367);
            this.ucSteps1.TabIndex = 7;
            // 
            // chkCreateSomeOutOfTheBoxSPworkflows
            // 
            this.chkCreateSomeOutOfTheBoxSPworkflows.AutoSize = true;
            this.chkCreateSomeOutOfTheBoxSPworkflows.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkCreateSomeOutOfTheBoxSPworkflows.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkCreateSomeOutOfTheBoxSPworkflows.Location = new System.Drawing.Point(265, 155);
            this.chkCreateSomeOutOfTheBoxSPworkflows.Name = "chkCreateSomeOutOfTheBoxSPworkflows";
            this.chkCreateSomeOutOfTheBoxSPworkflows.Size = new System.Drawing.Size(388, 19);
            this.chkCreateSomeOutOfTheBoxSPworkflows.TabIndex = 8;
            this.chkCreateSomeOutOfTheBoxSPworkflows.Text = "Create some out-of-the-box SharePoint workflows and attach to lists";
            this.chkCreateSomeOutOfTheBoxSPworkflows.UseVisualStyleBackColor = true;
            // 
            // chkAttachCustomWF
            // 
            this.chkAttachCustomWF.AutoSize = true;
            this.chkAttachCustomWF.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAttachCustomWF.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkAttachCustomWF.Location = new System.Drawing.Point(265, 193);
            this.chkAttachCustomWF.Name = "chkAttachCustomWF";
            this.chkAttachCustomWF.Size = new System.Drawing.Size(210, 19);
            this.chkAttachCustomWF.TabIndex = 9;
            this.chkAttachCustomWF.Text = "Attach declarative worklfow to lists";
            this.chkAttachCustomWF.UseVisualStyleBackColor = true;
            // 
            // frm10Workflows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.chkAttachCustomWF);
            this.Controls.Add(this.chkCreateSomeOutOfTheBoxSPworkflows);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm10Workflows";
            this.Text = "frm10Workflows";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.chkCreateSomeOutOfTheBoxSPworkflows, 0);
            this.Controls.SetChildIndex(this.chkAttachCustomWF, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.CheckBox chkCreateSomeOutOfTheBoxSPworkflows;
        private System.Windows.Forms.CheckBox chkAttachCustomWF;
    }
}