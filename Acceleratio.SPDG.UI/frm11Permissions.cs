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
    public partial class frm11Permissions : frmWizardMaster
    {
        public frm11Permissions()
        {
            InitializeComponent();

            base.lblTitle.Text = "Permissions";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(11);
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
            chkAssignPermissions.Checked = Common.WorkingDefinition.AssignPermissions;
            chkPercentOfSites.Checked = Common.WorkingDefinition.CreateUniquePermissionsForPercentOfSites;
            chkPercentOfLists.Checked = Common.WorkingDefinition.CreateUniquePermissionsForPercentOfLists;
            chkPercentOfLibFolders.Checked = Common.WorkingDefinition.CreateUniquePermissionsForPercentOfLibraries;
            chkPercentOfListItems.Checked = Common.WorkingDefinition.CreateUniquePermissionsForPercentOfListItems;
            txtPercentSites.Text = Common.WorkingDefinition.PermissionsPercentOfSites.ToString();
            txtPercentLists.Text = Common.WorkingDefinition.PermissionsPercentOfLists.ToString();
            txtPercentLibFolders.Text = Common.WorkingDefinition.PermissionsPercentOfFolders.ToString();
            txtPercentListItems.Text = Common.WorkingDefinition.PermissionsPercentOfListItems.ToString();
            txtPercentDirectlyToUsers.Text = Common.WorkingDefinition.PermissionsPercentForUsers.ToString();
            txtPercentGroupCases.Text = Common.WorkingDefinition.PermissionsPercentForSPGroups.ToString();
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.AssignPermissions = chkAssignPermissions.Checked;
            Common.WorkingDefinition.CreateUniquePermissionsForPercentOfSites = chkPercentOfSites.Checked;
            Common.WorkingDefinition.CreateUniquePermissionsForPercentOfLists = chkPercentOfLists.Checked;
            Common.WorkingDefinition.CreateUniquePermissionsForPercentOfLibraries = chkPercentOfLibFolders.Checked;
            Common.WorkingDefinition.CreateUniquePermissionsForPercentOfListItems = chkPercentOfListItems.Checked;
            Common.WorkingDefinition.PermissionsPercentOfSites = Convert.ToInt32(txtPercentSites.Text);
            Common.WorkingDefinition.PermissionsPercentOfLists = Convert.ToInt32(txtPercentLists.Text);
            Common.WorkingDefinition.PermissionsPercentOfFolders = Convert.ToInt32(txtPercentLibFolders.Text);
            Common.WorkingDefinition.PermissionsPercentOfListItems = Convert.ToInt32(txtPercentListItems.Text);
            Common.WorkingDefinition.PermissionsPercentForUsers = Convert.ToInt32(txtPercentDirectlyToUsers.Text);
            Common.WorkingDefinition.PermissionsPercentForSPGroups = Convert.ToInt32(txtPercentGroupCases.Text);
            return true;
        }
    }
}
