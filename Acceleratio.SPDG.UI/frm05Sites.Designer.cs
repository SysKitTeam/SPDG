namespace Acceleratio.SPDG.UI
{
    partial class frm05Sites
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
            this.trackNumSitesToCreate = new System.Windows.Forms.TrackBar();
            this.trackMaxNumberLevels = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumSitesToCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(232, 372);
            this.ucSteps1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Number of Sites to Create";
            // 
            // trackNumSitesToCreate
            // 
            this.trackNumSitesToCreate.Location = new System.Drawing.Point(260, 131);
            this.trackNumSitesToCreate.Name = "trackNumSitesToCreate";
            this.trackNumSitesToCreate.Size = new System.Drawing.Size(424, 45);
            this.trackNumSitesToCreate.TabIndex = 12;
            // 
            // trackMaxNumberLevels
            // 
            this.trackMaxNumberLevels.Location = new System.Drawing.Point(261, 224);
            this.trackMaxNumberLevels.Name = "trackMaxNumberLevels";
            this.trackMaxNumberLevels.Size = new System.Drawing.Size(424, 45);
            this.trackMaxNumberLevels.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Maximal Number of Levels";
            // 
            // frm05Sites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.trackMaxNumberLevels);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackNumSitesToCreate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm05Sites";
            this.Text = "frm05Sites";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.trackNumSitesToCreate, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.trackMaxNumberLevels, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackNumSitesToCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackNumSitesToCreate;
        private System.Windows.Forms.TrackBar trackMaxNumberLevels;
        private System.Windows.Forms.Label label3;
    }
}