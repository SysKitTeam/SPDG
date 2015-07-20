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
            this.SuspendLayout();
            // 
            // progressOverall
            // 
            this.progressOverall.Location = new System.Drawing.Point(101, 263);
            this.progressOverall.Name = "progressOverall";
            this.progressOverall.Size = new System.Drawing.Size(530, 23);
            this.progressOverall.TabIndex = 7;
            // 
            // lblOverview
            // 
            this.lblOverview.Location = new System.Drawing.Point(103, 233);
            this.lblOverview.Name = "lblOverview";
            this.lblOverview.Size = new System.Drawing.Size(528, 17);
            this.lblOverview.TabIndex = 9;
            this.lblOverview.Text = "Overview progress";
            this.lblOverview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDataGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.lblOverview);
            this.Controls.Add(this.progressOverall);
            this.Name = "frmDataGeneration";
            this.Text = "frmDataGeneration";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.progressOverall, 0);
            this.Controls.SetChildIndex(this.lblOverview, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressOverall;
        private System.Windows.Forms.Label lblOverview;
    }
}