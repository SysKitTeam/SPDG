namespace Acceleratio.SPDG.UI
{
    partial class frmDataGeneration
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
            this.progressOverall = new System.Windows.Forms.ProgressBar();
            this.lblOverview = new System.Windows.Forms.Label();
            this.lblDetails = new System.Windows.Forms.Label();
            this.progressDetails = new System.Windows.Forms.ProgressBar();
            this.btnOpenLog = new System.Windows.Forms.Button();
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
            // progressOverall
            // 
            this.progressOverall.Location = new System.Drawing.Point(116, 264);
            this.progressOverall.Name = "progressOverall";
            this.progressOverall.Size = new System.Drawing.Size(692, 23);
            this.progressOverall.TabIndex = 7;
            // 
            // lblOverview
            // 
            this.lblOverview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOverview.Location = new System.Drawing.Point(118, 234);
            this.lblOverview.Name = "lblOverview";
            this.lblOverview.Size = new System.Drawing.Size(528, 17);
            this.lblOverview.TabIndex = 9;
            this.lblOverview.Text = "Overview progress";
            // 
            // lblDetails
            // 
            this.lblDetails.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDetails.Location = new System.Drawing.Point(118, 324);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(528, 17);
            this.lblDetails.TabIndex = 11;
            this.lblDetails.Text = "Details progress";
            // 
            // progressDetails
            // 
            this.progressDetails.Location = new System.Drawing.Point(116, 354);
            this.progressDetails.Name = "progressDetails";
            this.progressDetails.Size = new System.Drawing.Size(692, 23);
            this.progressDetails.TabIndex = 10;
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOpenLog.Location = new System.Drawing.Point(665, 423);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(143, 23);
            this.btnOpenLog.TabIndex = 12;
            this.btnOpenLog.Text = "Open Log file";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Visible = false;
            // 
            // frmDataGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.btnOpenLog);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.progressDetails);
            this.Controls.Add(this.lblOverview);
            this.Controls.Add(this.progressOverall);
            this.Name = "frmDataGeneration";
            this.Text = "frmDataGeneration";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.progressOverall, 0);
            this.Controls.SetChildIndex(this.lblOverview, 0);
            this.Controls.SetChildIndex(this.progressDetails, 0);
            this.Controls.SetChildIndex(this.lblDetails, 0);
            this.Controls.SetChildIndex(this.btnOpenLog, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressOverall;
        private System.Windows.Forms.Label lblOverview;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.ProgressBar progressDetails;
        private System.Windows.Forms.Button btnOpenLog;
    }
}