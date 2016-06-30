using System;
using System.Windows.Forms;
using Acceleratio.SPDG.Generator;

namespace Acceleratio.SPDG.UI
{
    public partial class frm01Connect : frmWizardMaster
    {
        public frm01Connect(bool appStart)
        {
            InitializeComponent();

            base.lblTitle.Text = "Connect";
            base.lblDescription.Text = "Connect to your SharePoint environment";

            btnBack.Enabled = false;
            btnNext.Click += btnNext_Click;
            ucSteps1.showStep(1);
            this.Text = Common.APP_TITLE;
            DataGenerator.SessionID = "Session " + DateTime.Now.ToString("yy-MM-dd") + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString();

            if (appStart)
            {
                if (DataGenerator.SupportsServer)
                {
                    Common.InitServerDefinition();
                }
                else if (DataGenerator.SupportsClient)
                {
                    Common.InitClientDefinition();
                }
                else
                {
                    MessageBox.Show("To use SPDG you must either run it on a >=SharePoint 2010 machine, or on a machine that has .Net 4.5 framework installed");
                    Environment.Exit(0);
                }
                if (!DataGenerator.SupportsServer)
                {
                    radioConnectSPOnPremise.Enabled = false;
                    radioConnectSPOnPremise.Checked = false;
                    radioConnectSPOnline.Checked = true;
                }
                else if (!DataGenerator.SupportsClient)
                {
                    radioConnectSPOnline.Enabled = false;
                    radioConnectSPOnline.Checked = false;
                    radioConnectSPOnPremise.Checked = true;
                }
            }

            loadData();
            setUIState();
        }

        private bool saveForm(object form)
        {
            try
            {
                return ((frmWizardMaster)form).saveData();
            }
            catch(Exception ex)
            {
                if (ex.Message.IndexOf("Could not load file or assembly 'Microsoft.SharePoint") > -1)
                {
                    MessageBox.Show("Missing 'Microsoft.SharePoint.dll'." + Environment.NewLine + "Check if SharePoint is installed on current machine!");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }

                return false;
            }
            
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

                frmWizardMaster frm = null;
                frm = new frm02UsersGroups();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm02UsersGroups)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frmWizardMaster frm = null;
                if (WorkingDefinition.IsClientObjectModel)
                {
                    frm = new frm04Collections();

                }
                else
                {
                    frm = new frm03WebApplications();
                }
                // frm03WebApplications frm = new frm03WebApplications();
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

                var frm = new frm06Lists();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm06Lists)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }
                var frm = new frm07ViewsColumns();
                frm = new frm07ViewsColumns();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm07ViewsColumns)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }
                var frm = new frm08ListItems();
                frm.RootForm = this;
                frm.Show();
            }
            else if (fromForm is frm08ListItems)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }

                frmWizardMaster frm = null;
                if (WorkingDefinition.IsClientObjectModel)
                {
                    frm = new frm11Permissions();
                }
                else
                {
                    frm = new frm09ContentTypes();
                }
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
                Common.PreventAppClosing = true;
                ((Form)fromForm).Close();
                Common.PreventAppClosing = false;

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

                frmWizardMaster frm = null;
                if (WorkingDefinition.IsClientObjectModel)
                {
                    frm = new frm02UsersGroups();
                }
                else
                {
                    frm = new frm03WebApplications();
                }
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
            else if (fromForm is frm11Permissions)
            {
                if (!saveForm(fromForm))
                {
                    return;
                }
                frmWizardMaster frm;
                if (WorkingDefinition.IsClientObjectModel)
                {
                    frm = new frm08ListItems();
                }
                else
                {
                    frm = new frm10Workflows();
                }
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

            Common.PreventAppClosing = true;
            ((Form)fromForm).Close();
            Common.PreventAppClosing = false;
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

            if (currentForm != null)
            {
                currentForm.Hide();
            }

        }

        private void frm01Connect_VisibleChanged(object sender, EventArgs e)
        {
        }

        public override void loadData()
        {
            SampleData.PrepareSampleCollections();

            lblTenantName.Visible = Common.WorkingDefinition.IsClientObjectModel;
            txtTenantName.Visible = Common.WorkingDefinition.IsClientObjectModel;
            radioConnectSPOnPremise.Checked = !WorkingDefinition.IsClientObjectModel;
            radioConnectSPOnline.Checked = WorkingDefinition.IsClientObjectModel;
            if (Common.WorkingDefinition.CredentialsOfCurrentUser)
            {
                radioCurrentCredentials.Checked = true;
                radioCustomCredentials.Checked = false;
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtTenantName.Text = string.Empty;
            }
            else
            {
                radioCurrentCredentials.Checked = false;
                radioCustomCredentials.Checked = true;
                txtUserName.Text = Common.WorkingDefinition.Username;
                txtPassword.Text = Common.WorkingDefinition.Password;
                if (WorkingDefinition.IsClientObjectModel)
                {
                    txtTenantName.Text = ((ClientGeneratorDefinition) WorkingDefinition).TenantName;
                }
                else
                {
                    txtTenantName.Text = Common.WorkingDefinition.Domain;
                }
            }

            setUIState();

        }   

        public override bool saveData()
        {
            try
            {
                WorkingDefinition.Username = txtUserName.Text.Trim();
                WorkingDefinition.Password = txtPassword.Text.Trim();
                WorkingDefinition.CredentialsOfCurrentUser = radioCurrentCredentials.Checked;                
                if (WorkingDefinition.IsClientObjectModel)
                {
                    ((ClientGeneratorDefinition) WorkingDefinition).TenantName = txtTenantName.Text.Trim();                    
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;                    
                    var helper = SPDGDataHelper.Create(WorkingDefinition);
                    helper.ValidateCredentials();
                }
                catch (CredentialValidationException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                return true;
            }
            catch(Exception ex)
            {
                if( ex.Message.IndexOf("Could not load file or assembly 'Microsoft.SharePoint") > -1 )
                {
                    MessageBox.Show("Missing 'Microsoft.SharePoint.dll'. Check if SharePoint is installed on current machine!");
                    Errors.Log(ex);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                    Errors.Log(ex);
                }

                return false;
            }
        }

   

        private void uiEventHandler(object sender, EventArgs e)
        {
            if (sender.Equals(radioConnectSPOnline))
            {
                if (!radioConnectSPOnline.Checked && WorkingDefinition.IsClientObjectModel)
                {
                    Common.InitServerDefinition();                    
                }
                else if (radioConnectSPOnline.Checked && !WorkingDefinition.IsClientObjectModel)
                {
                    Common.InitClientDefinition();
                }
            }
            setUIState();
        }


        private bool _eventEnabled = false;
        private void setUIState()
        {
            bool isSPOnline = radioConnectSPOnline.Checked;            

            txtTenantName.Visible = isSPOnline;
            lblTenantName.Visible = isSPOnline;
            lblUserName.Text = isSPOnline ? "Username:" : "Username (domain\\username)";
            if (isSPOnline)
            {
                radioCustomCredentials.Checked = true;
                
            }
            radioCurrentCredentials.Enabled = !isSPOnline;
            var customCredentials = radioCustomCredentials.Checked;

            txtTenantName.Enabled = customCredentials;
            txtPassword.Enabled = customCredentials;
            txtUserName.Enabled = customCredentials;
            
        }
    }
}
