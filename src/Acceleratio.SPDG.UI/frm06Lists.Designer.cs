namespace Acceleratio.SPDG.UI
{
    partial class frm06Lists
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
            this.trackMaxNumberListLibraries = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackMaxFoldersInLib = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackMaxNumberNestedFolders = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkDocLib = new System.Windows.Forms.CheckBox();
            this.chkTasks = new System.Windows.Forms.CheckBox();
            this.chkCalendar = new System.Windows.Forms.CheckBox();
            this.chkCreateFolders = new System.Windows.Forms.CheckBox();
            this.chkList = new System.Windows.Forms.CheckBox();
            this.lblNumOfLists = new System.Windows.Forms.Label();
            this.lblNumOfFolders = new System.Windows.Forms.Label();
            this.lblNumLevels = new System.Windows.Forms.Label();
            this.trackBigListsCount = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBigLists = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberListLibraries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxFoldersInLib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberNestedFolders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBigListsCount)).BeginInit();
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
            this.ucSteps1.Size = new System.Drawing.Size(238, 365);
            this.ucSteps1.TabIndex = 7;
            // 
            // trackMaxNumberListLibraries
            // 
            this.trackMaxNumberListLibraries.LargeChange = 1;
            this.trackMaxNumberListLibraries.Location = new System.Drawing.Point(260, 173);
            this.trackMaxNumberListLibraries.Maximum = 100;
            this.trackMaxNumberListLibraries.Name = "trackMaxNumberListLibraries";
            this.trackMaxNumberListLibraries.Size = new System.Drawing.Size(543, 45);
            this.trackMaxNumberListLibraries.TabIndex = 14;
            this.trackMaxNumberListLibraries.ValueChanged += new System.EventHandler(this.trackMaxNumberListLibraries_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(265, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(246, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Maximal Number of List and Libraries per Site";
            // 
            // trackMaxFoldersInLib
            // 
            this.trackMaxFoldersInLib.LargeChange = 1;
            this.trackMaxFoldersInLib.Location = new System.Drawing.Point(259, 420);
            this.trackMaxFoldersInLib.Maximum = 20;
            this.trackMaxFoldersInLib.Name = "trackMaxFoldersInLib";
            this.trackMaxFoldersInLib.Size = new System.Drawing.Size(543, 45);
            this.trackMaxFoldersInLib.TabIndex = 16;
            this.trackMaxFoldersInLib.ValueChanged += new System.EventHandler(this.trackMaxFoldersInLib_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(265, 402);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Max number of folders in Document Libraries";
            // 
            // trackMaxNumberNestedFolders
            // 
            this.trackMaxNumberNestedFolders.LargeChange = 1;
            this.trackMaxNumberNestedFolders.Location = new System.Drawing.Point(259, 484);
            this.trackMaxNumberNestedFolders.Name = "trackMaxNumberNestedFolders";
            this.trackMaxNumberNestedFolders.Size = new System.Drawing.Size(543, 45);
            this.trackMaxNumberNestedFolders.TabIndex = 18;
            this.trackMaxNumberNestedFolders.ValueChanged += new System.EventHandler(this.trackMaxNumberNestedFolders_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(265, 466);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Max number of nested folder level per Document Library";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(265, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Types of Lists/Libraries";
            // 
            // chkDocLib
            // 
            this.chkDocLib.AutoSize = true;
            this.chkDocLib.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkDocLib.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkDocLib.Location = new System.Drawing.Point(269, 332);
            this.chkDocLib.Name = "chkDocLib";
            this.chkDocLib.Size = new System.Drawing.Size(121, 19);
            this.chkDocLib.TabIndex = 20;
            this.chkDocLib.Text = "Document Library";
            this.chkDocLib.UseVisualStyleBackColor = true;
            this.chkDocLib.CheckedChanged += new System.EventHandler(this.chkDocLib_CheckedChanged);
            // 
            // chkTasks
            // 
            this.chkTasks.AutoSize = true;
            this.chkTasks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkTasks.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkTasks.Location = new System.Drawing.Point(493, 309);
            this.chkTasks.Name = "chkTasks";
            this.chkTasks.Size = new System.Drawing.Size(55, 19);
            this.chkTasks.TabIndex = 21;
            this.chkTasks.Text = "Tasks";
            this.chkTasks.UseVisualStyleBackColor = true;
            // 
            // chkCalendar
            // 
            this.chkCalendar.AutoSize = true;
            this.chkCalendar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkCalendar.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkCalendar.Location = new System.Drawing.Point(493, 332);
            this.chkCalendar.Name = "chkCalendar";
            this.chkCalendar.Size = new System.Drawing.Size(73, 19);
            this.chkCalendar.TabIndex = 22;
            this.chkCalendar.Text = "Calendar";
            this.chkCalendar.UseVisualStyleBackColor = true;
            // 
            // chkCreateFolders
            // 
            this.chkCreateFolders.AutoSize = true;
            this.chkCreateFolders.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkCreateFolders.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkCreateFolders.Location = new System.Drawing.Point(268, 373);
            this.chkCreateFolders.Name = "chkCreateFolders";
            this.chkCreateFolders.Size = new System.Drawing.Size(250, 19);
            this.chkCreateFolders.TabIndex = 23;
            this.chkCreateFolders.Text = "Create some folders in Document Libraries";
            this.chkCreateFolders.UseVisualStyleBackColor = true;
            this.chkCreateFolders.CheckedChanged += new System.EventHandler(this.chkCreateFolders_CheckedChanged);
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkList.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkList.Location = new System.Drawing.Point(269, 309);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(44, 19);
            this.chkList.TabIndex = 24;
            this.chkList.Text = "List";
            this.chkList.UseVisualStyleBackColor = true;
            // 
            // lblNumOfLists
            // 
            this.lblNumOfLists.AutoSize = true;
            this.lblNumOfLists.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNumOfLists.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumOfLists.Location = new System.Drawing.Point(810, 176);
            this.lblNumOfLists.Name = "lblNumOfLists";
            this.lblNumOfLists.Size = new System.Drawing.Size(13, 15);
            this.lblNumOfLists.TabIndex = 25;
            this.lblNumOfLists.Text = "0";
            // 
            // lblNumOfFolders
            // 
            this.lblNumOfFolders.AutoSize = true;
            this.lblNumOfFolders.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNumOfFolders.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumOfFolders.Location = new System.Drawing.Point(810, 423);
            this.lblNumOfFolders.Name = "lblNumOfFolders";
            this.lblNumOfFolders.Size = new System.Drawing.Size(13, 15);
            this.lblNumOfFolders.TabIndex = 26;
            this.lblNumOfFolders.Text = "0";
            // 
            // lblNumLevels
            // 
            this.lblNumLevels.AutoSize = true;
            this.lblNumLevels.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNumLevels.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumLevels.Location = new System.Drawing.Point(810, 488);
            this.lblNumLevels.Name = "lblNumLevels";
            this.lblNumLevels.Size = new System.Drawing.Size(13, 15);
            this.lblNumLevels.TabIndex = 27;
            this.lblNumLevels.Text = "0";
            // 
            // trackBigListsCount
            // 
            this.trackBigListsCount.LargeChange = 1;
            this.trackBigListsCount.Location = new System.Drawing.Point(261, 232);
            this.trackBigListsCount.Maximum = 5;
            this.trackBigListsCount.Name = "trackBigListsCount";
            this.trackBigListsCount.Size = new System.Drawing.Size(543, 45);
            this.trackBigListsCount.TabIndex = 29;
            this.trackBigListsCount.ValueChanged += new System.EventHandler(this.trackBigListsCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(266, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "Big Lists Count";
            // 
            // lblBigLists
            // 
            this.lblBigLists.AutoSize = true;
            this.lblBigLists.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBigLists.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblBigLists.Location = new System.Drawing.Point(811, 233);
            this.lblBigLists.Name = "lblBigLists";
            this.lblBigLists.Size = new System.Drawing.Size(13, 15);
            this.lblBigLists.TabIndex = 30;
            this.lblBigLists.Text = "0";
            // 
            // frm06Lists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 644);
            this.Controls.Add(this.lblBigLists);
            this.Controls.Add(this.trackBigListsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNumLevels);
            this.Controls.Add(this.lblNumOfFolders);
            this.Controls.Add(this.lblNumOfLists);
            this.Controls.Add(this.chkList);
            this.Controls.Add(this.chkCreateFolders);
            this.Controls.Add(this.chkCalendar);
            this.Controls.Add(this.chkTasks);
            this.Controls.Add(this.chkDocLib);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackMaxNumberNestedFolders);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackMaxFoldersInLib);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackMaxNumberListLibraries);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucSteps1);
            this.Name = "frm06Lists";
            this.Text = "frm06Lists";
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnHelp, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.ucSteps1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.trackMaxNumberListLibraries, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.trackMaxFoldersInLib, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.trackMaxNumberNestedFolders, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.chkDocLib, 0);
            this.Controls.SetChildIndex(this.chkTasks, 0);
            this.Controls.SetChildIndex(this.chkCalendar, 0);
            this.Controls.SetChildIndex(this.chkCreateFolders, 0);
            this.Controls.SetChildIndex(this.chkList, 0);
            this.Controls.SetChildIndex(this.lblNumOfLists, 0);
            this.Controls.SetChildIndex(this.lblNumOfFolders, 0);
            this.Controls.SetChildIndex(this.lblNumLevels, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.trackBigListsCount, 0);
            this.Controls.SetChildIndex(this.lblBigLists, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberListLibraries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxFoldersInLib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxNumberNestedFolders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBigListsCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSteps ucSteps1;
        private System.Windows.Forms.TrackBar trackMaxNumberListLibraries;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackMaxFoldersInLib;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackMaxNumberNestedFolders;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkDocLib;
        private System.Windows.Forms.CheckBox chkTasks;
        private System.Windows.Forms.CheckBox chkCalendar;
        private System.Windows.Forms.CheckBox chkCreateFolders;
        private System.Windows.Forms.CheckBox chkList;
        private System.Windows.Forms.Label lblNumOfLists;
        private System.Windows.Forms.Label lblNumOfFolders;
        private System.Windows.Forms.Label lblNumLevels;
        private System.Windows.Forms.TrackBar trackBigListsCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBigLists;
    }
}