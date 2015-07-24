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
            trackCreateNewWebApplication.Value = Common.WorkingDefinition.CreateNewWebApplications;
            if ( !string.IsNullOrEmpty( Common.WorkingDefinition.UseExistingWebApplication) )
            {
                cboUseExistingWebApp.Text = Common.WorkingDefinition.UseExistingWebApplicationName;
            }

            if( Common.WorkingDefinition.CreateNewWebApplications > 0 )
            {
                radioCreateNewWebApp.Checked = true;
                trackCreateNewWebApplication.Enabled = true;
                radioUseExistingWebApp.Checked = false;
                cboUseExistingWebApp.Enabled = false;
            }
            else
            {
                radioUseExistingWebApp.Checked = true;
                trackCreateNewWebApplication.Enabled = false;
                
                cboUseExistingWebApp.Enabled = true;

            }
            
        }

        private void toggleRadio()
        {
            if(radioCreateNewWebApp.Checked )
            {
                trackCreateNewWebApplication.Enabled = true;
                cboUseExistingWebApp.Enabled = false;
            }
            else
            {
                trackCreateNewWebApplication.Value = 0;
                trackCreateNewWebApplication.Enabled = false;
                cboUseExistingWebApp.Enabled = true;
            }


        }


        public override bool saveData()
        {
            if (radioCreateNewWebApp.Checked && trackCreateNewWebApplication.Value == 0)
            {
                MessageBox.Show("Select at least one web application to create, if 'Create new web application' is selected.");
                return false;
            }

            if (radioUseExistingWebApp.Checked && cboUseExistingWebApp.SelectedItem == null)
            {
                MessageBox.Show("Select at least one existing web application, if 'Use existing' is selected.");
                return false;
            }


            Common.WorkingDefinition.CreateNewWebApplications = trackCreateNewWebApplication.Value;
            if (cboUseExistingWebApp.SelectedItem != null && Common.WorkingDefinition.CreateNewWebApplications == 0)
            {
                Common.WorkingDefinition.CreateNewWebApplications = 0;
                Common.WorkingDefinition.UseExistingWebApplication = ((ComboboxItem)cboUseExistingWebApp.SelectedItem).Value.ToString();
                Common.WorkingDefinition.UseExistingWebApplicationName = ((ComboboxItem)cboUseExistingWebApp.SelectedItem).Text.ToString();
            }
            else
            {
                Common.WorkingDefinition.UseExistingWebApplication = string.Empty;
                Common.WorkingDefinition.UseExistingWebApplicationName = string.Empty;
                Common.WorkingDefinition.SiteCollection = string.Empty;
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
