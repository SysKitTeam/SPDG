using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Acceleratio.SPDG.Generator;

namespace Acceleratio.SPDG.UI
{
    public partial class frm02UsersGroups : frmWizardMaster
    {
        public frm02UsersGroups()
        {
            InitializeComponent();

            base.lblTitle.Text = "Users && Groups";
            base.lblDescription.Text = "Create user and group accounts in your Active Directory";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(2);

            loadData();
            chkGenerateUsers_CheckedChanged(null, EventArgs.Empty);
            cboDomains.SelectedIndexChanged += cboDomains_SelectedIndexChanged;
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
            this.Show();
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            List<string> domains = AD.GetDomainList();

            foreach(string domain in domains)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = domain;
                item.Value = domain;
                cboDomains.Items.Add(item);
            }

            chkGenerateUsers.Checked = Common.WorkingDefinition.GenerateUsersAndSecurityGroupsActiveInDirectory;
            trackNumberOfUsers.Value = Common.WorkingDefinition.NumberOfUsersToCreate;
            trackNumberOfSecGroups.Value = Common.WorkingDefinition.NumberOfSecurityGroupsToCreate;
            cboDomains.Text = Common.WorkingDefinition.ADDomainName;
            cboOrganizationalUnit.Text = Common.WorkingDefinition.ADOrganizationalUnit;

            this.Show();
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.GenerateUsersAndSecurityGroupsActiveInDirectory = chkGenerateUsers.Checked;
            Common.WorkingDefinition.NumberOfUsersToCreate = trackNumberOfUsers.Value;
            Common.WorkingDefinition.NumberOfSecurityGroupsToCreate = trackNumberOfSecGroups.Value;
            Common.WorkingDefinition.ADDomainName = cboDomains.Text;
            Common.WorkingDefinition.ADOrganizationalUnit = cboOrganizationalUnit.Text;

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

        void cboDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDomains.SelectedItem == null)
            {
                return;
            }

            fillOUs();
        }


        private void cboDomains_Leave(object sender, EventArgs e)
        {
            if(cboDomains.Text == string.Empty)
            {
                return;
            }

            fillOUs();
        }

        private void fillOUs()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            List<string> ous = AD.ListOU(cboDomains.Text);
            cboOrganizationalUnit.Items.Clear();
            foreach (string ou in ous)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = ou;
                item.Value = ou;
                cboOrganizationalUnit.Items.Add(item);
            }

            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }
}
