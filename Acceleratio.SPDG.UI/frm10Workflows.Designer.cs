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
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(235, 367);
            this.ucSteps1.TabIndex = 7;
            // 
            // chkCreateSomeOutOfTheBoxSPworkflows
            // 
            this.chkCreateSomeOutOfTheBoxSPworkflows.AutoSize = true;
            this.chkCreateSomeOutOfTheBoxSPworkflows.Location = new System.Drawing.Point(265, 115);
            this.chkCreateSomeOutOfTheBoxSPworkflows.Name = "chkCreateSomeOutOfTheBoxSPworkflows";
            this.chkCreateSomeOutOfTheBoxSPworkflows.Size = new System.Drawing.Size(344, 17);
            this.chkCreateSomeOutOfTheBoxSPworkflows.TabIndex = 8;
            this.chkCreateSomeOutOfTheBoxSPworkflows.Text = "Create some out-of-the-box SharePoint workflows and attach to lists";
            this.chkCreateSomeOutOfTheBoxSPworkflows.UseVisualStyleBackColor = true;
            // 
            // chkAttachCustomWF
            // 
            this.chkAttachCustomWF.AutoSize = true;
            this.chkAttachCustomWF.Enabled = false;
            this.chkAttachCustomWF.Location = new System.Drawing.Point(265, 155);
            this.chkAttachCustomWF.Name = "chkAttachCustomWF";
            this.chkAttachCustomWF.Size = new System.Drawing.Size(171, 17);
            this.chkAttachCustomWF.TabIndex = 9;
            this.chkAttachCustomWF.Text = "Attach custom worklfow to lists";
            this.chkAttachCustomWF.UseVisualStyleBackColor = true;
            // 
            // frm10Workflows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
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