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
            base.lblDescription.Text = "Set unique permissions density and structure for SharePoint objects.";

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
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            preventCloseMessage = true;
            RootForm.MoveNext(this);
        }

        public override void loadData()
        {
            if( Common.WorkingDefinition.PermissionsPercentOfSites > 0 ||
                Common.WorkingDefinition.PermissionsPercentOfLists > 0 ||
                Common.WorkingDefinition.PermissionsPercentOfFolders > 0 ||
                Common.WorkingDefinition.PermissionsPercentOfListItems > 0 
                )
            {
                chkAssignPermissions.Checked = true;
            }

            

            txtPercentSites.Text = Common.WorkingDefinition.PermissionsPercentOfSites.ToString();
            txtPercentLists.Text = Common.WorkingDefinition.PermissionsPercentOfLists.ToString();
            txtPercentLibFolders.Text = Common.WorkingDefinition.PermissionsPercentOfFolders.ToString();
            txtPercentListItems.Text = Common.WorkingDefinition.PermissionsPercentOfListItems.ToString();
            txtPercentDirectlyToUsers.Text = Common.WorkingDefinition.PermissionsPercentForUsers.ToString();
            txtPercentGroupCases.Text = Common.WorkingDefinition.PermissionsPercentForSPGroups.ToString();
            trackPermissionsPerObject.Value = Common.WorkingDefinition.PermissionsPerObject;
        }

        public override bool saveData()
        {
            foreach( Control ctrl in this.Controls)
            {
                if(ctrl is TextBox)
                {
                    Int16 someInt;
                    if( !Int16.TryParse( ctrl.Text, out someInt ))
                    {
                        MessageBox.Show("All values must be numeric. Between 0 and 100.");
                        return false;
                    }

                    if( someInt < 0 || someInt > 100 )
                    {
                        MessageBox.Show("All values must be numeric. Between 0 and 100.");
                        return false;
                    }
                }
            }

            Common.WorkingDefinition.PermissionsPercentOfSites = Convert.ToInt32(txtPercentSites.Text);
            Common.WorkingDefinition.PermissionsPercentOfLists = Convert.ToInt32(txtPercentLists.Text);
            Common.WorkingDefinition.PermissionsPercentOfFolders = Convert.ToInt32(txtPercentLibFolders.Text);
            Common.WorkingDefinition.PermissionsPercentOfListItems = Convert.ToInt32(txtPercentListItems.Text);
            Common.WorkingDefinition.PermissionsPercentForUsers = Convert.ToInt32(txtPercentDirectlyToUsers.Text);
            Common.WorkingDefinition.PermissionsPercentForSPGroups = Convert.ToInt32(txtPercentGroupCases.Text);
            Common.WorkingDefinition.PermissionsPerObject = trackPermissionsPerObject.Value;
            return true;
        }

        private void chkAssignPermissions_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAssignPermissions.Checked)
            {
                trackPermissionsPerObject.Enabled = true;
                txtPercentSites.Enabled = true;

                if (Common.WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite > 0)
                {
                    txtPercentLists.Enabled = true;
                }

                if (Common.WorkingDefinition.MaxNumberOfFoldersToGenerate > 0)
                {
                    txtPercentLibFolders.Enabled = true;
                }

                if (Common.WorkingDefinition.MaxNumberofItemsToGenerate > 0)
                {
                    txtPercentListItems.Enabled = true;
                }

                txtPercentDirectlyToUsers.Enabled = true;
                txtPercentGroupCases.Enabled = true;
            }
            else
            {
                trackPermissionsPerObject.Enabled = false;
                txtPercentSites.Enabled = false;
                txtPercentLists.Enabled = false;
                txtPercentLibFolders.Enabled = false;
                txtPercentListItems.Enabled = false;

                txtPercentDirectlyToUsers.Enabled = false;
                txtPercentGroupCases.Enabled = false;

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        ctrl.Text = "0";
                    }
                }
            }
        }

        private void trackPermissionsPerObject_ValueChanged(object sender, EventArgs e)
        {
            lblPermissionsPerObject.Text = trackPermissionsPerObject.Value.ToString();
        }
    }
}
