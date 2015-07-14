namespace Acceleratio.SPDG.UI
{
    partial class frm07ViewsColumns
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
            this.trackNumViewsPerList = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackNumColumnsPerList = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCreateViews = new System.Windows.Forms.CheckBox();
            this.chkCreateColumns = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumViewsPerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumColumnsPerList)).BeginInit();
            this.SuspendLayout();
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(229, 365);
            this.ucSteps1.TabIndex = 7;
            // 
            // trackNumViewsPerList
            // 
            this.trackNumViewsPerList.Location = new System.Drawing.Point(256, 169);
            this.trackNumViewsPerList.Name = "trackNumViewsPerList";
            this.trackNumViewsPerList.Size = new System.Drawing.Size(424, 45);
            this.trackNumViewsPerList.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Max number of Views per list to create";
            // 
            // trackNumColumnsPerList
            // 
            this.trackNumColumnsPerList.Location = new System.Drawing.Point(256, 297);
            this.trackNumColumnsPerList.Name = "trackNumColumnsPerList";
            this.trackNumColumnsPerList.Size = new System.Drawing.Size(424, 45);
            this.trackNumColumnsPerList.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Max number of columns per list to create";
            // 
            // chkCreateViews
            // 
            this.chkCreateViews.AutoSize = true;
            this.chkCreateViews.Location = new System.Drawing.Point(265, 115);
            this.chkCreateViews.Name = "chkCreateViews";
            this.chkCreateViews.Size = new System.Drawing.Size(88, 17);
            this.chkCreateViews.TabIndex = 19;
            this.chkCreateViews.Text = "Create Views";
            this.chkCreateViews.UseVisualStyleBackColor = true;
            // 
            // chkCreateColumns
            // 
            this.chkCreateColumns.AutoSize = true;
            this.chkCreateColumns.Location = new System.Drawing.Point(264, 244);
            this.chkCreateColumns.Name = "chkCreateColumns";
            this.chkCreateColumns.Size = new System.Drawing.Size(99, 17);
            this.chkCreateColumns.TabIndex = 20;
            this.chkCreateColumns.Text = "Create columns";
            this.chkCreateColumns.UseVisualStyleBackColor = true;
            // 
            // frm07ViewsColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.chkCreateColumns);
            this.Controls.Add(this.chkCreateViews);
            this.Controls.Add(this.trackNumColumnsPerList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackNumViewsPerList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm07ViewsColumns";
            this.Text = "frm07ViewsColumns";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.trackNumViewsPerList, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.trackNumColumnsPerList, 0);
            this.Controls.SetChildIndex(this.chkCreateViews, 0);
            this.Controls.SetChildIndex(this.chkCreateColumns, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackNumViewsPerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumColumnsPerList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.TrackBar trackNumViewsPerList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackNumColumnsPerList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCreateViews;
        private System.Windows.Forms.CheckBox chkCreateColumns;
    }
}