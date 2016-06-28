using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Acceleratio.SPDG.Generator.UI;

namespace Acceleratio.SPDG.UI
{
    public partial class frm06Lists : frmWizardMaster
    {
        public frm06Lists()
        {
            InitializeComponent();

            base.lblTitle.Text = "Lists, libraries && folders";
            base.lblDescription.Text = "Define prefered combination of lists, folders and libraries to create.";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(6);
            this.Text = Common.APP_TITLE;

            initUI();
            loadData();
        }

        private void initUI()
        {
            chkCalendar.Enabled = false;
            chkCreateFolders.Enabled = false;
            chkDocLib.Enabled = false;
            chkList.Enabled = false;
            chkTasks.Enabled = false;
            trackMaxFoldersInLib.Enabled = false;
            trackMaxNumberNestedFolders.Enabled = false;
        }

        void btnBack_Click(object sender, EventArgs e)
        {
            preventCloseMessage = true;
            RootForm.MovePrevious(this);
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            preventCloseMessage = true;
            RootForm.MoveNext(this);
        }

        public override void loadData()
        {
            trackMaxNumberListLibraries.Value = Common.WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite;
            trackBigListsCount.Value = Common.WorkingDefinition.NumberOfBigListsPerSite;
            chkDocLib.Checked = Common.WorkingDefinition.LibTypeDocument;
            chkTasks.Checked = Common.WorkingDefinition.LibTypeTasks;
            chkCalendar.Checked = Common.WorkingDefinition.LibTypeCalendar;
            chkList.Checked = Common.WorkingDefinition.LibTypeList;
            chkCreateFolders.Checked = Common.WorkingDefinition.CreateSomeFoldersInDocumentLibraries;
            trackMaxFoldersInLib.Value = Common.WorkingDefinition.MaxNumberOfFoldersToGenerate;
            trackMaxNumberNestedFolders.Value = Common.WorkingDefinition.MaxNumberOfNestedFolderLevelPerLibrary;
        }

        public override bool saveData()
        {
            if (trackMaxNumberListLibraries.Value > 0 && !chkDocLib.Checked && !chkList.Checked && !chkTasks.Checked && !chkCalendar.Checked)
            {
                MessageBox.Show("If number of lists to create is greater than zero, type of list must be selected.");
                return false;
            }

            Common.WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite = trackMaxNumberListLibraries.Value;
            Common.WorkingDefinition.LibTypeDocument = chkDocLib.Checked;
            Common.WorkingDefinition.LibTypeTasks = chkTasks.Checked;
            Common.WorkingDefinition.LibTypeCalendar = chkCalendar.Checked;
            Common.WorkingDefinition.LibTypeList = chkList.Checked;
            Common.WorkingDefinition.CreateSomeFoldersInDocumentLibraries = chkCreateFolders.Checked;
            Common.WorkingDefinition.MaxNumberOfFoldersToGenerate = trackMaxFoldersInLib.Value;
            Common.WorkingDefinition.MaxNumberOfNestedFolderLevelPerLibrary = trackMaxNumberNestedFolders.Value;
            Common.WorkingDefinition.NumberOfBigListsPerSite = trackBigListsCount.Value;
            return true;
        }

        private void trackMaxNumberListLibraries_ValueChanged(object sender, EventArgs e)
        {
            lblNumOfLists.Text = trackMaxNumberListLibraries.Value.ToString();
            if( trackMaxNumberListLibraries.Value > 0)
            {
                chkCalendar.Enabled = true;
                chkDocLib.Enabled = true;
                chkList.Enabled = true;
                chkTasks.Enabled = true;
            }
            else
            {
                chkCalendar.Enabled = false;
                chkDocLib.Enabled = false;
                chkList.Enabled = false;
                chkTasks.Enabled = false;
                trackMaxFoldersInLib.Value = 0;
                trackMaxNumberNestedFolders.Value = 0;
            }
        }

        private void trackMaxFoldersInLib_ValueChanged(object sender, EventArgs e)
        {
            lblNumOfFolders.Text = trackMaxFoldersInLib.Value.ToString();
        }

        private void trackMaxNumberNestedFolders_ValueChanged(object sender, EventArgs e)
        {
            lblNumLevels.Text = trackMaxNumberNestedFolders.Value.ToString();
        }

        private void chkDocLib_CheckedChanged(object sender, EventArgs e)
        {
            if( chkDocLib.Checked )
            {
                chkCreateFolders.Enabled = true;
            }
            else
            {
                chkCreateFolders.Enabled = false;
                trackMaxFoldersInLib.Value = 0;
                trackMaxNumberNestedFolders.Value = 0;
            }
        }

        private void chkCreateFolders_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCreateFolders.Checked)
            {
                trackMaxFoldersInLib.Enabled = true;
                trackMaxNumberNestedFolders.Enabled = true;
            }
            else
            {
                trackMaxFoldersInLib.Enabled = false;
                trackMaxNumberNestedFolders.Enabled = false;
                trackMaxFoldersInLib.Value = 0;
                trackMaxNumberNestedFolders.Value = 0;
            }
        }

        private void trackBigListsCount_ValueChanged(object sender, EventArgs e)
        {
            lblBigLists.Text = trackBigListsCount.Value.ToString();
        }
    }
}
