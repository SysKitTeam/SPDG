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
    public partial class frm01Connect : frmWizardMaster
    {
        public frm01Connect(bool appStart)
        {
            InitializeComponent();

            base.lblTitle.Text = "Connect";

            btnBack.Enabled = false;
            btnNext.Click += btnNext_Click;
            ucSteps1.showStep(1);
            this.Text = Common.APP_TITLE;

            if(appStart)
            {
                Common.InitEmptyDefinition();
            }

            loadData();
        }

        private bool saveForm(object form)
        {
            return ((frmWizardMaster)form).saveData();
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            MoveNext(this);
        }

        internal void MoveNext(object fromForm)
        {
            if (fromForm is frm01Connect)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm02UsersGroups frm = new frm02UsersGroups();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm02UsersGroups)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm03WebApplications frm = new frm03WebApplications();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm03WebApplications)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm04Collections frm = new frm04Collections();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm04Collections)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm05Sites frm = new frm05Sites();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm05Sites)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm06Lists frm = new frm06Lists();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm06Lists)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm07ViewsColumns frm = new frm07ViewsColumns();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm07ViewsColumns)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm08ListItems frm = new frm08ListItems();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm08ListItems)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm09ContentTypes frm = new frm09ContentTypes();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm09ContentTypes)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm10Workflows frm = new frm10Workflows();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm10Workflows)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm11Permissions frm = new frm11Permissions();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm11Permissions)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm12Finalize frm = new frm12Finalize();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm12Finalize)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frmDataGeneration frm = new frmDataGeneration();
                frm.RootForm = this;
                frm.Show();
            }

            this.Hide();
            if (fromForm is frm01Connect)
            {

            }
            else
            {
                ((Form)fromForm).Close();
            }
            
        }

        internal void MovePrevious(object fromForm)
        {
            this.Hide();

            if (fromForm is frm03WebApplications)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm02UsersGroups frm = new frm02UsersGroups();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm02UsersGroups)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm01Connect frm = new frm01Connect(false);
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm04Collections)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm03WebApplications frm = new frm03WebApplications();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm05Sites)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm04Collections frm = new frm04Collections();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm06Lists)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm05Sites frm = new frm05Sites();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm07ViewsColumns)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm06Lists frm = new frm06Lists();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm08ListItems)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm07ViewsColumns frm = new frm07ViewsColumns();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm09ContentTypes)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm08ListItems frm = new frm08ListItems();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm10Workflows)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm09ContentTypes frm = new frm09ContentTypes();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm11Permissions )
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm10Workflows frm = new frm10Workflows();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm12Finalize)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frm11Permissions frm = new frm11Permissions();
                frm.RootForm = this;
                frm.Show();
            }

            
        }

        internal void MoveAt(int stepNumber, Form currentForm)
        {
            if (stepNumber == 1)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm01Connect frm = new frm01Connect(false);
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 2)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm02UsersGroups frm = new frm02UsersGroups();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 3)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm03WebApplications frm = new frm03WebApplications();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 4)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm04Collections frm = new frm04Collections();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 5)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm05Sites frm = new frm05Sites();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 6)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm06Lists frm = new frm06Lists();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 7)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm07ViewsColumns frm = new frm07ViewsColumns();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 8)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm08ListItems frm = new frm08ListItems();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 9)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm09ContentTypes frm = new frm09ContentTypes();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 10)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm10Workflows frm = new frm10Workflows();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 11)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm11Permissions frm = new frm11Permissions();
                frm.RootForm = this;
                frm.Show();
            }
            else if (stepNumber == 12)
            {
                if (currentForm != null && !saveForm(currentForm))
                {
                    return;
                }

                frm12Finalize frm = new frm12Finalize();
                frm.RootForm = this;
                frm.Show();
            }

            if( currentForm != null )
            {
                currentForm.Hide();
            }
            
        }

        private void radioCustomCredentials_CheckedChanged(object sender, EventArgs e)
        {
            changeCredentialsState();
        }

        private void changeCredentialsState()
        {
            if (radioCurrentCredentials.Checked)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtDomain.Enabled = false;
            }
            else
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                txtDomain.Enabled = true;
            }
        }

        private void radioCurrentCredentials_CheckedChanged(object sender, EventArgs e)
        {
            changeCredentialsState();
        }

        private void frm01Connect_VisibleChanged(object sender, EventArgs e)
        {
        }

        public override void loadData()
        {
            txtSharePointSiteURL.Text = Common.WorkingDefinition.SharePointURL;
            radioConnectSPOnPremise.Checked = Common.WorkingDefinition.ConnectToSPOnPremise;
            radioConnectSPOnline.Checked = !Common.WorkingDefinition.ConnectToSPOnPremise;
            if (Common.WorkingDefinition.CredentialsOfCurrentUser)
            {
                radioCurrentCredentials.Checked = true;
                radioCustomCredentials.Checked = false;
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtDomain.Text = string.Empty;
            }
            else
            {
                radioCurrentCredentials.Checked = false;
                radioCustomCredentials.Checked = true;
                txtUserName.Text = Common.WorkingDefinition.Username;
                txtPassword.Text = Common.WorkingDefinition.Password;
                txtDomain.Text = Common.WorkingDefinition.Domain;
            }

        }

        public override bool saveData()
        {
            Common.WorkingDefinition.SharePointURL = txtSharePointSiteURL.Text;
            Common.WorkingDefinition.ConnectToSPOnPremise = radioConnectSPOnPremise.Checked;
            Common.WorkingDefinition.CredentialsOfCurrentUser = radioCurrentCredentials.Checked;
            Common.WorkingDefinition.Username = txtUserName.Text;
            Common.WorkingDefinition.Password = txtPassword.Text;
            Common.WorkingDefinition.Domain = txtDomain.Text;

            return true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void loadDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

    }
}
