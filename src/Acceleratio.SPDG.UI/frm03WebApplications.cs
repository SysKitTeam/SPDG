using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Acceleratio.SPDG.Generator;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace Acceleratio.SPDG.UI
{
    public partial class frm03WebApplications : frmWizardMaster
    {
        

        

        public frm03WebApplications()
        {
            InitializeComponent();

            base.lblTitle.Text = "Web Applications";
            base.lblDescription.Text = "Create new or use existing SharePoint Web Applications";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(3);

            
        }

        public new ServerGeneratorDefinition WorkingDefinition
        {
            get { return (ServerGeneratorDefinition) base.WorkingDefinition; }
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

        private void loadWebApplications()
        {
            try
            {
                SPWebService spWebService = SPWebService.ContentService;
                SPWebApplicationCollection webAppColl = spWebService.WebApplications;

                foreach (SPWebApplication webApplication in webAppColl)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = webApplication.Name;
                    item.Value = webApplication.Id;
                    cboUseExistingWebApp.Items.Add(item);
                }
            }
            catch(Exception ex )
            {
                Errors.Log(ex);
            }

        }


        public override void loadData()
        {
            trackCreateNewWebApplication.Value = WorkingDefinition.CreateNewWebApplications;
            if ( !string.IsNullOrEmpty( WorkingDefinition.UseExistingWebApplication) )
            {
                cboUseExistingWebApp.Text = WorkingDefinition.UseExistingWebApplicationName;
            }

            if( WorkingDefinition.CreateNewWebApplications > 0 )
            {
                radioCreateNewWebApp.Checked = true;
                trackCreateNewWebApplication.Enabled = true;
                radioUseExistingWebApp.Checked = false;
                cboUseExistingWebApp.Enabled = false;

                label1.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
                label4.Enabled = true;
                txtOwnerUserName.Enabled = true;
                txtOwnerPassword.Enabled = true;
                txtOwnerEmail.Enabled = true;
                txtSQLServer.Enabled = true;

                txtOwnerUserName.Text = WorkingDefinition.WebAppOwnerLogin;
                txtOwnerPassword.Text = WorkingDefinition.WebAppOwnerPassword;
                txtOwnerEmail.Text =  WorkingDefinition.WebAppOwnerEmail;
                txtSQLServer.Text = WorkingDefinition.DatabaseServer;
            }
            else
            {
                radioUseExistingWebApp.Checked = true;
                trackCreateNewWebApplication.Enabled = false;
                
                cboUseExistingWebApp.Enabled = true;

                label1.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                txtOwnerUserName.Enabled = false;
                txtOwnerPassword.Enabled = false;
                txtOwnerEmail.Enabled = false;
                txtSQLServer.Enabled = false;

            }
            
        }

        private void toggleRadio()
        {
            if(radioCreateNewWebApp.Checked )
            {
                trackCreateNewWebApplication.Enabled = true;
                cboUseExistingWebApp.Enabled = false;

                label1.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
                label4.Enabled = true;
                txtOwnerUserName.Enabled = true;
                txtOwnerPassword.Enabled = true;
                txtOwnerEmail.Enabled = true;
                txtSQLServer.Enabled = true;
            }
            else
            {
                trackCreateNewWebApplication.Value = 0;
                trackCreateNewWebApplication.Enabled = false;
                cboUseExistingWebApp.Enabled = true;

                label1.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                txtOwnerUserName.Enabled = false;
                txtOwnerPassword.Enabled = false;
                txtOwnerEmail.Enabled = false;
                txtSQLServer.Enabled = false;
            }


        }


        public override bool saveData()
        {
            if (radioCreateNewWebApp.Checked && trackCreateNewWebApplication.Value == 0)
            {
                MessageBox.Show("Select at least one web application to create, if 'Create new web application' is selected.");
                return false;
            }

            if (radioCreateNewWebApp.Checked && (txtOwnerUserName.Text == string.Empty || txtOwnerPassword.Text == string.Empty || txtOwnerEmail.Text == string.Empty || txtSQLServer.Text == string.Empty))
            {
                MessageBox.Show("Provide all details to create new web application.");
                return false;
            }


            if (radioUseExistingWebApp.Checked && cboUseExistingWebApp.SelectedItem == null)
            {
                MessageBox.Show("Select at least one existing web application, if 'Use existing' is selected.");
                return false;
            }


            WorkingDefinition.CreateNewWebApplications = trackCreateNewWebApplication.Value;
            if (cboUseExistingWebApp.SelectedItem != null && WorkingDefinition.CreateNewWebApplications == 0)
            {
                WorkingDefinition.CreateNewWebApplications = 0;
                WorkingDefinition.UseExistingWebApplication = ((ComboboxItem)cboUseExistingWebApp.SelectedItem).Value.ToString();
                WorkingDefinition.UseExistingWebApplicationName = ((ComboboxItem)cboUseExistingWebApp.SelectedItem).Text.ToString();
            }
            else
            {
                WorkingDefinition.UseExistingWebApplication = string.Empty;
                WorkingDefinition.UseExistingWebApplicationName = string.Empty;
                WorkingDefinition.SiteCollection = string.Empty;
                WorkingDefinition.WebAppOwnerLogin = txtOwnerUserName.Text;
                WorkingDefinition.WebAppOwnerPassword = txtOwnerPassword.Text;
                WorkingDefinition.WebAppOwnerEmail = txtOwnerEmail.Text;
                WorkingDefinition.DatabaseServer = txtSQLServer.Text;
            }

            return true;
        }

        private void radioCreateNewWebApp_CheckedChanged(object sender, EventArgs e)
        {
            toggleRadio();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            toggleRadio();
        }

        private void trackCreateNewWebApplication_ValueChanged(object sender, EventArgs e)
        {
            lblCreateNewApps.Text = trackCreateNewWebApplication.Value.ToString();
        }

        private void frm03WebApplications_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();            
            loadWebApplications();            
            loadData();
            this.Enabled = true;
            this.Cursor = Cursors.Default;

        }

        
    }
}
