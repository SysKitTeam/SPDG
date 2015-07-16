using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acceleratio.SPDG.UI
{
    public partial class frm06Lists : frmWizardMaster
    {
        public frm06Lists()
        {
            InitializeComponent();

            base.lblTitle.Text = "Lists";


            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(6);
            this.Text = Common.APP_TITLE;

            loadData();
        }

        void btnBack_Click(object sender, EventArgs e)
        {
            preventCloseMessage = true;
            RootForm.MovePrevious(this);
            this.Close();
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            preventCloseMessage = true;
            RootForm.MoveNext(this);
            this.Close();
        }

        public override void loadData()
        {
            trackMaxNumberListLibraries.Value = Common.WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite;
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
            Common.WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite = trackMaxNumberListLibraries.Value;
            Common.WorkingDefinition.LibTypeDocument = chkDocLib.Checked;
            Common.WorkingDefinition.LibTypeTasks = chkTasks.Checked;
            Common.WorkingDefinition.LibTypeCalendar = chkCalendar.Checked;
            Common.WorkingDefinition.LibTypeList = chkList.Checked;
            Common.WorkingDefinition.CreateSomeFoldersInDocumentLibraries = chkCreateFolders.Checked;
            Common.WorkingDefinition.MaxNumberOfFoldersToGenerate = trackMaxFoldersInLib.Value;
            Common.WorkingDefinition.MaxNumberOfNestedFolderLevelPerLibrary = trackMaxNumberNestedFolders.Value;
            return true;
        }

        private void trackMaxNumberListLibraries_ValueChanged(object sender, EventArgs e)
        {
            lblNumOfLists.Text = trackMaxNumberListLibraries.Value.ToString();
        }
    }
}
