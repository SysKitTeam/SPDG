namespace Acceleratio.SPDG.UI
{
    partial class frm09ContentTypes
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
            this.chkCreateSomeConentTypes = new System.Windows.Forms.CheckBox();
            this.trackMaxNumberContentTypes = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAddSiteColumns = new System.Windows.Forms.CheckBox();
            this.trackAddSiteColumns = new System.Windows.Forms.TrackBar();
            this.chkContentTypesCanInherit = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberContentTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackAddSiteColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(232, 368);
            this.ucSteps1.TabIndex = 7;
            // 
            // chkCreateSomeConentTypes
            // 
            this.chkCreateSomeConentTypes.AutoSize = true;
            this.chkCreateSomeConentTypes.Location = new System.Drawing.Point(265, 115);
            this.chkCreateSomeConentTypes.Name = "chkCreateSomeConentTypes";
            this.chkCreateSomeConentTypes.Size = new System.Drawing.Size(318, 17);
            this.chkCreateSomeConentTypes.TabIndex = 8;
            this.chkCreateSomeConentTypes.Text = "Create some content types and attach them to list and libraries";
            this.chkCreateSomeConentTypes.UseVisualStyleBackColor = true;
            // 
            // trackMaxNumberContentTypes
            // 
            this.trackMaxNumberContentTypes.Location = new System.Drawing.Point(257, 178);
            this.trackMaxNumberContentTypes.Name = "trackMaxNumberContentTypes";
            this.trackMaxNumberContentTypes.Size = new System.Drawing.Size(424, 45);
            this.trackMaxNumberContentTypes.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(262, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(229, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Max number of content types per site collection";
            // 
            // chkAddSiteColumns
            // 
            this.chkAddSiteColumns.AutoSize = true;
            this.chkAddSiteColumns.Location = new System.Drawing.Point(265, 243);
            this.chkAddSiteColumns.Name = "chkAddSiteColumns";
            this.chkAddSiteColumns.Size = new System.Drawing.Size(190, 17);
            this.chkAddSiteColumns.TabIndex = 32;
            this.chkAddSiteColumns.Text = "Add site columns to Content Types";
            this.chkAddSiteColumns.UseVisualStyleBackColor = true;
            // 
            // trackAddSiteColumns
            // 
            this.trackAddSiteColumns.Location = new System.Drawing.Point(257, 266);
            this.trackAddSiteColumns.Name = "trackAddSiteColumns";
            this.trackAddSiteColumns.Size = new System.Drawing.Size(424, 45);
            this.trackAddSiteColumns.TabIndex = 33;
            // 
            // chkContentTypesCanInherit
            // 
            this.chkContentTypesCanInherit.AutoSize = true;
            this.chkContentTypesCanInherit.Location = new System.Drawing.Point(265, 329);
            this.chkContentTypesCanInherit.Name = "chkContentTypesCanInherit";
            this.chkContentTypesCanInherit.Size = new System.Drawing.Size(301, 17);
            this.chkContentTypesCanInherit.TabIndex = 34;
            this.chkContentTypesCanInherit.Text = "Content Types can inherit from other custom content types";
            this.chkContentTypesCanInherit.UseVisualStyleBackColor = true;
            // 
            // frm09ContentTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.chkContentTypesCanInherit);
            this.Controls.Add(this.trackAddSiteColumns);
            this.Controls.Add(this.chkAddSiteColumns);
            this.Controls.Add(this.trackMaxNumberContentTypes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkCreateSomeConentTypes);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm09ContentTypes";
            this.Text = "frm09ContentTypes";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.chkCreateSomeConentTypes, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.trackMaxNumberContentTypes, 0);
            this.Controls.SetChildIndex(this.chkAddSiteColumns, 0);
            this.Controls.SetChildIndex(this.trackAddSiteColumns, 0);
            this.Controls.SetChildIndex(this.chkContentTypesCanInherit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberContentTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackAddSiteColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.CheckBox chkCreateSomeConentTypes;
        private System.Windows.Forms.TrackBar trackMaxNumberContentTypes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAddSiteColumns;
        private System.Windows.Forms.TrackBar trackAddSiteColumns;
        private System.Windows.Forms.CheckBox chkContentTypesCanInherit;
    }
}