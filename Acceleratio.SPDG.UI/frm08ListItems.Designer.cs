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
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberOfItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMinDocSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxDocSize)).BeginInit();
            this.SuspendLayout();
            // 
            // ucSteps1
            // 
            this.ucSteps1.Location = new System.Drawing.Point(0, 103);
            this.ucSteps1.Name = "ucSteps1";
            this.ucSteps1.Size = new System.Drawing.Size(240, 374);
            this.ucSteps1.TabIndex = 7;
            // 
            // chkPrefil
            // 
            this.chkPrefil.AutoSize = true;
            this.chkPrefil.Location = new System.Drawing.Point(265, 115);
            this.chkPrefil.Name = "chkPrefil";
            this.chkPrefil.Size = new System.Drawing.Size(232, 17);
            this.chkPrefil.TabIndex = 8;
            this.chkPrefil.Text = "Prefil generated List and Libraries with Items";
            this.chkPrefil.UseVisualStyleBackColor = true;
            this.chkPrefil.CheckedChanged += new System.EventHandler(this.chkPrefil_CheckedChanged);
            // 
            // trackMaxNumberOfItems
            // 
            this.trackMaxNumberOfItems.Location = new System.Drawing.Point(257, 170);
            this.trackMaxNumberOfItems.Maximum = 100;
            this.trackMaxNumberOfItems.Name = "trackMaxNumberOfItems";
            this.trackMaxNumberOfItems.Size = new System.Drawing.Size(397, 45);
            this.trackMaxNumberOfItems.TabIndex = 20;
            this.trackMaxNumberOfItems.TickFrequency = 5;
            this.trackMaxNumberOfItems.ValueChanged += new System.EventHandler(this.trackMaxNumberOfItems_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Max number of items to generate per list/library";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Required Document Types";
            // 
            // chkDOCX
            // 
            this.chkDOCX.AutoSize = true;
            this.chkDOCX.Location = new System.Drawing.Point(265, 234);
            this.chkDOCX.Name = "chkDOCX";
            this.chkDOCX.Size = new System.Drawing.Size(56, 17);
            this.chkDOCX.TabIndex = 22;
            this.chkDOCX.Text = "DOCX";
            this.chkDOCX.UseVisualStyleBackColor = true;
            // 
            // chkXLSX
            // 
            this.chkXLSX.AutoSize = true;
            this.chkXLSX.Location = new System.Drawing.Point(265, 257);
            this.chkXLSX.Name = "chkXLSX";
            this.chkXLSX.Size = new System.Drawing.Size(53, 17);
            this.chkXLSX.TabIndex = 23;
            this.chkXLSX.Text = "XLSX";
            this.chkXLSX.UseVisualStyleBackColor = true;
            // 
            // chkPDF
            // 
            this.chkPDF.AutoSize = true;
            this.chkPDF.Location = new System.Drawing.Point(265, 280);
            this.chkPDF.Name = "chkPDF";
            this.chkPDF.Size = new System.Drawing.Size(47, 17);
            this.chkPDF.TabIndex = 24;
            this.chkPDF.Text = "PDF";
            this.chkPDF.UseVisualStyleBackColor = true;
            // 
            // chkImages
            // 
            this.chkImages.AutoSize = true;
            this.chkImages.Location = new System.Drawing.Point(265, 303);
            this.chkImages.Name = "chkImages";
            this.chkImages.Size = new System.Drawing.Size(60, 17);
            this.chkImages.TabIndex = 25;
            this.chkImages.Text = "Images";
            this.chkImages.UseVisualStyleBackColor = true;
            // 
            // trackMinDocSize
            // 
            this.trackMinDocSize.Location = new System.Drawing.Point(257, 355);
            this.trackMinDocSize.Maximum = 1000;
            this.trackMinDocSize.Minimum = 20;
            this.trackMinDocSize.Name = "trackMinDocSize";
            this.trackMinDocSize.Size = new System.Drawing.Size(397, 45);
            this.trackMinDocSize.TabIndex = 27;
            this.trackMinDocSize.TickFrequency = 100;
            this.trackMinDocSize.Value = 20;
            this.trackMinDocSize.ValueChanged += new System.EventHandler(this.trackMinDocSize_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 336);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Minimal Document Size (PDF)";
            // 
            // trackMaxDocSize
            // 
            this.trackMaxDocSize.Location = new System.Drawing.Point(257, 422);
            this.trackMaxDocSize.Name = "trackMaxDocSize";
            this.trackMaxDocSize.Size = new System.Drawing.Size(397, 45);
            this.trackMaxDocSize.TabIndex = 29;
            this.trackMaxDocSize.ValueChanged += new System.EventHandler(this.trackMaxDocSize_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(262, 403);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Max Document Size (PDF)";
            // 
            // lblNumItems
            // 
            this.lblNumItems.AutoSize = true;
            this.lblNumItems.Location = new System.Drawing.Point(658, 174);
            this.lblNumItems.Name = "lblNumItems";
            this.lblNumItems.Size = new System.Drawing.Size(13, 13);
            this.lblNumItems.TabIndex = 30;
            this.lblNumItems.Text = "0";
            // 
            // lblMinSize
            // 
            this.lblMinSize.AutoSize = true;
            this.lblMinSize.Location = new System.Drawing.Point(659, 358);
            this.lblMinSize.Name = "lblMinSize";
            this.lblMinSize.Size = new System.Drawing.Size(29, 13);
            this.lblMinSize.TabIndex = 31;
            this.lblMinSize.Text = "0 kB";
            // 
            // lblMaxSize
            // 
            this.lblMaxSize.AutoSize = true;
            this.lblMaxSize.Location = new System.Drawing.Point(659, 425);
            this.lblMaxSize.Name = "lblMaxSize";
            this.lblMaxSize.Size = new System.Drawing.Size(32, 13);
            this.lblMaxSize.TabIndex = 32;
            this.lblMaxSize.Text = "0 MB";
            // 
            // frm08ListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.lblMaxSize);
            this.Controls.Add(this.lblMinSize);
            this.Controls.Add(this.lblNumItems);
            this.Controls.Add(this.trackMaxDocSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackMinDocSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkImages);
            this.Controls.Add(this.chkPDF);
            this.Controls.Add(this.chkXLSX);
            this.Controls.Add(this.chkDOCX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackMaxNumberOfItems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkPrefil);
            this.Controls.Add(this.ucSteps1);
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
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.chkDOCX, 0);
            this.Controls.SetChildIndex(this.chkXLSX, 0);
            this.Controls.SetChildIndex(this.chkPDF, 0);
            this.Controls.SetChildIndex(this.chkImages, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.trackMinDocSize, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.trackMaxDocSize, 0);
            this.Controls.SetChildIndex(this.lblNumItems, 0);
            this.Controls.SetChildIndex(this.lblMinSize, 0);
            this.Controls.SetChildIndex(this.lblMaxSize, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberOfItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMinDocSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxDocSize)).EndInit();
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
    }
}