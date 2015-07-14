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
    public partial class frm02UsersGroups : frmWizardMaster
    {
        public frm02UsersGroups()
        {
            InitializeComponent();

            base.lblTitle.Text = "Users & Groups";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(2);

            loadData();
            chkGenerateUsers_CheckedChanged(null, EventArgs.Empty);
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
            chkGenerateUsers.Checked = Common.WorkingDefinition.GenerateUsersAndSecurityGroupsActiveInDirectory;
            trackNumberOfUsers.Value = Common.WorkingDefinition.NumberOfUsersToCreate;
            trackNumberOfSecGroups.Value = Common.WorkingDefinition.NumberOfSecurityGroupsToCreate;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.GenerateUsersAndSecurityGroupsActiveInDirectory = chkGenerateUsers.Checked;
            Common.WorkingDefinition.NumberOfUsersToCreate = trackNumberOfUsers.Value;
            Common.WorkingDefinition.NumberOfSecurityGroupsToCreate = trackNumberOfSecGroups.Value;

            return true;
        }

        private void chkGenerateUsers_CheckedChanged(object sender, EventArgs e)
        {
            if( chkGenerateUsers.Checked)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
            }
        }
    }
}
