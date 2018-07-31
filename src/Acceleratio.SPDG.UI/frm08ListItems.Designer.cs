namespace Acceleratio.SPDG.UI
{
    partial class frm08ListItems
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
            this.chkPrefil = new System.Windows.Forms.CheckBox();
            this.trackMaxNumberOfItems = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDOCX = new System.Windows.Forms.CheckBox();
            this.chkXLSX = new System.Windows.Forms.CheckBox();
            this.chkPDF = new System.Windows.Forms.CheckBox();
            this.chkImages = new System.Windows.Forms.CheckBox();
            this.trackMinDocSize = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackMaxDocSize = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNumItems = new System.Windows.Forms.Label();
            this.lblMinSize = new System.Windows.Forms.Label();
            this.lblMaxSize = new System.Windows.Forms.Label();
            this.grpDocumentLibrarySettings = new System.Windows.Forms.GroupBox();
            this.lblNumDocLibItems = new System.Windows.Forms.Label();
            this.trackMaxNumberOrDocLibItems = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarBigListItemsCount = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBigListCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberOfItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMinDocSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxDocSize)).BeginInit();
            this.grpDocumentLibrarySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberOrDocLibItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBigListItemsCount)).BeginInit();
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
            this.ucSteps1.Size = new System.Drawing.Size(240, 469);
            this.ucSteps1.TabIndex = 7;
            // 
            // chkPrefil
            // 
            this.chkPrefil.AutoSize = true;
            this.chkPrefil.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkPrefil.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkPrefil.Location = new System.Drawing.Point(262, 155);
            this.chkPrefil.Name = "chkPrefil";
            this.chkPrefil.Size = new System.Drawing.Size(258, 19);
            this.chkPrefil.TabIndex = 8;
            this.chkPrefil.Text = "Prefil generated List and Libraries with Items";
            this.chkPrefil.UseVisualStyleBackColor = true;
            this.chkPrefil.CheckedChanged += new System.EventHandler(this.chkPrefil_CheckedChanged);
            // 
            // trackMaxNumberOfItems
            // 
            this.trackMaxNumberOfItems.Location = new System.Drawing.Point(257, 209);
            this.trackMaxNumberOfItems.Maximum = 200;
            this.trackMaxNumberOfItems.Name = "trackMaxNumberOfItems";
            this.trackMaxNumberOfItems.Size = new System.Drawing.Size(543, 45);
            this.trackMaxNumberOfItems.TabIndex = 20;
            this.trackMaxNumberOfItems.TickFrequency = 5;
            this.trackMaxNumberOfItems.Value = 25;
            this.trackMaxNumberOfItems.ValueChanged += new System.EventHandler(this.trackMaxNumberOfItems_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(262, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Max number of items to generate per list";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Required Document Types";
            // 
            // chkDOCX
            // 
            this.chkDOCX.AutoSize = true;
            this.chkDOCX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkDOCX.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkDOCX.Location = new System.Drawing.Point(9, 106);
            this.chkDOCX.Name = "chkDOCX";
            this.chkDOCX.Size = new System.Drawing.Size(58, 19);
            this.chkDOCX.TabIndex = 22;
            this.chkDOCX.Text = "DOCX";
            this.chkDOCX.UseVisualStyleBackColor = true;
            // 
            // chkXLSX
            // 
            this.chkXLSX.AutoSize = true;
            this.chkXLSX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkXLSX.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkXLSX.Location = new System.Drawing.Point(73, 106);
            this.chkXLSX.Name = "chkXLSX";
            this.chkXLSX.Size = new System.Drawing.Size(52, 19);
            this.chkXLSX.TabIndex = 23;
            this.chkXLSX.Text = "XLSX";
            this.chkXLSX.UseVisualStyleBackColor = true;
            // 
            // chkPDF
            // 
            this.chkPDF.AutoSize = true;
            this.chkPDF.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkPDF.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkPDF.Location = new System.Drawing.Point(131, 105);
            this.chkPDF.Name = "chkPDF";
            this.chkPDF.Size = new System.Drawing.Size(47, 19);
            this.chkPDF.TabIndex = 24;
            this.chkPDF.Text = "PDF";
            this.chkPDF.UseVisualStyleBackColor = true;
            // 
            // chkImages
            // 
            this.chkImages.AutoSize = true;
            this.chkImages.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkImages.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkImages.Location = new System.Drawing.Point(184, 106);
            this.chkImages.Name = "chkImages";
            this.chkImages.Size = new System.Drawing.Size(64, 19);
            this.chkImages.TabIndex = 25;
            this.chkImages.Text = "Images";
            this.chkImages.UseVisualStyleBackColor = true;
            // 
            // trackMinDocSize
            // 
            this.trackMinDocSize.LargeChange = 1;
            this.trackMinDocSize.Location = new System.Drawing.Point(2, 145);
            this.trackMinDocSize.Maximum = 1000;
            this.trackMinDocSize.Minimum = 20;
            this.trackMinDocSize.Name = "trackMinDocSize";
            this.trackMinDocSize.Size = new System.Drawing.Size(543, 45);
            this.trackMinDocSize.TabIndex = 27;
            this.trackMinDocSize.TickFrequency = 100;
            this.trackMinDocSize.Value = 20;
            this.trackMinDocSize.ValueChanged += new System.EventHandler(this.trackMinDocSize_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(7, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 15);
            this.label4.TabIndex = 26;
            this.label4.Text = "Minimal Document Size (PDF)";
            // 
            // trackMaxDocSize
            // 
            this.trackMaxDocSize.LargeChange = 1;
            this.trackMaxDocSize.Location = new System.Drawing.Point(2, 212);
            this.trackMaxDocSize.Name = "trackMaxDocSize";
            this.trackMaxDocSize.Size = new System.Drawing.Size(543, 45);
            this.trackMaxDocSize.TabIndex = 29;
            this.trackMaxDocSize.Value = 1;
            this.trackMaxDocSize.ValueChanged += new System.EventHandler(this.trackMaxDocSize_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(7, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 15);
            this.label5.TabIndex = 28;
            this.label5.Text = "Max Document Size (PDF)";
            // 
            // lblNumItems
            // 
            this.lblNumItems.AutoSize = true;
            this.lblNumItems.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNumItems.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumItems.Location = new System.Drawing.Point(808, 212);
            this.lblNumItems.Name = "lblNumItems";
            this.lblNumItems.Size = new System.Drawing.Size(19, 15);
            this.lblNumItems.TabIndex = 30;
            this.lblNumItems.Text = "25";
            // 
            // lblMinSize
            // 
            this.lblMinSize.AutoSize = true;
            this.lblMinSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMinSize.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblMinSize.Location = new System.Drawing.Point(554, 148);
            this.lblMinSize.Name = "lblMinSize";
            this.lblMinSize.Size = new System.Drawing.Size(29, 15);
            this.lblMinSize.TabIndex = 31;
            this.lblMinSize.Text = "0 kB";
            // 
            // lblMaxSize
            // 
            this.lblMaxSize.AutoSize = true;
            this.lblMaxSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaxSize.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblMaxSize.Location = new System.Drawing.Point(554, 215);
            this.lblMaxSize.Name = "lblMaxSize";
            this.lblMaxSize.Size = new System.Drawing.Size(34, 15);
            this.lblMaxSize.TabIndex = 32;
            this.lblMaxSize.Text = "1 MB";
            // 
            // grpDocumentLibrarySettings
            // 
            this.grpDocumentLibrarySettings.Controls.Add(this.lblNumDocLibItems);
            this.grpDocumentLibrarySettings.Controls.Add(this.trackMaxNumberOrDocLibItems);
            this.grpDocumentLibrarySettings.Controls.Add(this.label6);
            this.grpDocumentLibrarySettings.Controls.Add(this.label3);
            this.grpDocumentLibrarySettings.Controls.Add(this.lblMaxSize);
            this.grpDocumentLibrarySettings.Controls.Add(this.chkDOCX);
            this.grpDocumentLibrarySettings.Controls.Add(this.lblMinSize);
            this.grpDocumentLibrarySettings.Controls.Add(this.chkXLSX);
            this.grpDocumentLibrarySettings.Controls.Add(this.chkPDF);
            this.grpDocumentLibrarySettings.Controls.Add(this.trackMaxDocSize);
            this.grpDocumentLibrarySettings.Controls.Add(this.chkImages);
            this.grpDocumentLibrarySettings.Controls.Add(this.label5);
            this.grpDocumentLibrarySettings.Controls.Add(this.label4);
            this.grpDocumentLibrarySettings.Controls.Add(this.trackMinDocSize);
            this.grpDocumentLibrarySettings.Location = new System.Drawing.Point(237, 303);
            this.grpDocumentLibrarySettings.Name = "grpDocumentLibrarySettings";
            this.grpDocumentLibrarySettings.Size = new System.Drawing.Size(614, 287);
            this.grpDocumentLibrarySettings.TabIndex = 33;
            this.grpDocumentLibrarySettings.TabStop = false;
            this.grpDocumentLibrarySettings.Text = "Document Libraries";
            // 
            // lblNumDocLibItems
            // 
            this.lblNumDocLibItems.AutoSize = true;
            this.lblNumDocLibItems.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNumDocLibItems.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumDocLibItems.Location = new System.Drawing.Point(552, 50);
            this.lblNumDocLibItems.Name = "lblNumDocLibItems";
            this.lblNumDocLibItems.Size = new System.Drawing.Size(19, 15);
            this.lblNumDocLibItems.TabIndex = 35;
            this.lblNumDocLibItems.Text = "25";
            // 
            // trackMaxNumberOrDocLibItems
            // 
            this.trackMaxNumberOrDocLibItems.Location = new System.Drawing.Point(3, 40);
            this.trackMaxNumberOrDocLibItems.Maximum = 100;
            this.trackMaxNumberOrDocLibItems.Name = "trackMaxNumberOrDocLibItems";
            this.trackMaxNumberOrDocLibItems.Size = new System.Drawing.Size(543, 45);
            this.trackMaxNumberOrDocLibItems.TabIndex = 34;
            this.trackMaxNumberOrDocLibItems.TickFrequency = 5;
            this.trackMaxNumberOrDocLibItems.Value = 25;
            this.trackMaxNumberOrDocLibItems.ValueChanged += new System.EventHandler(this.trackMaxNumberOrDocLibItems_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(297, 15);
            this.label6.TabIndex = 33;
            this.label6.Text = "Max number of items to generate per document library";
            // 
            // trackBarBigListItemsCount
            // 
            this.trackBarBigListItemsCount.Location = new System.Drawing.Point(258, 259);
            this.trackBarBigListItemsCount.Maximum = 200;
            this.trackBarBigListItemsCount.Name = "trackBarBigListItemsCount";
            this.trackBarBigListItemsCount.Size = new System.Drawing.Size(543, 45);
            this.trackBarBigListItemsCount.TabIndex = 39;
            this.trackBarBigListItemsCount.TickFrequency = 5;
            this.trackBarBigListItemsCount.ValueChanged += new System.EventHandler(this.trackBarBigListItemsCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(260, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Max number of items to generate per Big List";
            // 
            // lblBigListCount
            // 
            this.lblBigListCount.AutoSize = true;
            this.lblBigListCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBigListCount.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblBigListCount.Location = new System.Drawing.Point(809, 261);
            this.lblBigListCount.Name = "lblBigListCount";
            this.lblBigListCount.Size = new System.Drawing.Size(13, 15);
            this.lblBigListCount.TabIndex = 40;
            this.lblBigListCount.Text = "0";
            // 
            // frm08ListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.lblBigListCount);
            this.Controls.Add(this.trackBarBigListItemsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpDocumentLibrarySettings);
            this.Controls.Add(this.lblNumItems);
            this.Controls.Add(this.trackMaxNumberOfItems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkPrefil);
            this.Controls.Add(this.ucSteps1);
            this.MaximumSize = new System.Drawing.Size(937, 683);
            this.MinimumSize = new System.Drawing.Size(937, 683);
            this.Name = "frm08ListItems";
            this.Text = "frm08ListItems";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.chkPrefil, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.trackMaxNumberOfItems, 0);
            this.Controls.SetChildIndex(this.lblNumItems, 0);
            this.Controls.SetChildIndex(this.grpDocumentLibrarySettings, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.trackBarBigListItemsCount, 0);
            this.Controls.SetChildIndex(this.lblBigListCount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberOfItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMinDocSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxDocSize)).EndInit();
            this.grpDocumentLibrarySettings.ResumeLayout(false);
            this.grpDocumentLibrarySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberOrDocLibItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBigListItemsCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.CheckBox chkPrefil;
        private System.Windows.Forms.TrackBar trackMaxNumberOfItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkDOCX;
        private System.Windows.Forms.CheckBox chkXLSX;
        private System.Windows.Forms.CheckBox chkPDF;
        private System.Windows.Forms.CheckBox chkImages;
        private System.Windows.Forms.TrackBar trackMinDocSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackMaxDocSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblNumItems;
        private System.Windows.Forms.Label lblMinSize;
        private System.Windows.Forms.Label lblMaxSize;
        private System.Windows.Forms.GroupBox grpDocumentLibrarySettings;
        private System.Windows.Forms.Label lblNumDocLibItems;
        private System.Windows.Forms.TrackBar trackMaxNumberOrDocLibItems;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarBigListItemsCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBigListCount;
    }
}