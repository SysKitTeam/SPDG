using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Acceleratio.SPDG.Generator;
using Acceleratio.SPDG.Generator.UI;

namespace Acceleratio.SPDG.UI
{
    public partial class frm04Collections : frmWizardMaster
    {
        public frm04Collections()
        {
            InitializeComponent();

            base.lblTitle.Text = "Site Collections";
            base.lblDescription.Text = "Create new or use existing SharePoint Site Collections";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(4);
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

        void changeRadio()
        {
            if (radioCreateNewSiteColl.Checked)
            {
                trackNumSiteColls.Enabled = true;
                cboSiteCollection.Enabled = false;

                label1.Enabled = true;
                label3.Enabled = true;
                txtOwnerEmail.Enabled = true;
                txtOwnerUserName.Enabled = true;
            }
            else
            {
                trackNumSiteColls.Enabled = false;
                cboSiteCollection.Enabled = true;

                label1.Enabled = false;
                label3.Enabled = false;
                txtOwnerEmail.Enabled = false;
                txtOwnerUserName.Enabled = false;
            }
        }

        private void radioUseExisting_CheckedChanged(object sender, EventArgs e)
        {
            changeRadio();
        }

        private void radioCreateNewSiteColl_CheckedChanged(object sender, EventArgs e)
        {
            changeRadio();
        }
             
        private void loadSiteCollections()
        {
            var helper = SPDGDataHelper.Create(WorkingDefinition);

            ServerGeneratorDefinition serverDefinition = WorkingDefinition as ServerGeneratorDefinition;
            IEnumerable<string> siteCollections;
            if (serverDefinition == null)
            {
                siteCollections = helper.GetAllSiteCollections(Guid.Empty);
            }
            else if (serverDefinition.UseExistingWebApplication != string.Empty)
            {
                siteCollections = helper.GetAllSiteCollections(new Guid(serverDefinition.UseExistingWebApplication));
            }
            else
            {
                return;
            }
            foreach (var siteColl in siteCollections)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = siteColl;
                item.Value = siteColl;
                cboSiteCollection.Items.Add(item);
            }
        }

        public override void loadData()
        {
            ServerGeneratorDefinition serverDefinition = WorkingDefinition as ServerGeneratorDefinition;

            if (serverDefinition != null)
            {
                if (serverDefinition.CreateNewWebApplications > 0)
                {
                    //TODO:rf zamijeniti sve Common.WorkingDefinition s WorkingDefinition property
                    //dodatno, ovo se moze unutar getera za propertye rijesiti za ovaj if
                    Common.WorkingDefinition.UseExistingSiteCollection = false;
                    trackNumSiteColls.Minimum = 1;
                    Common.WorkingDefinition.CreateNewSiteCollections = 1;
                    radioCreateNewSiteColl.Checked = true;
                    radioUseExisting.Enabled = false;                                     
                   
                    
                }
                else
                {
                    trackNumSiteColls.Minimum = 0;
                    radioUseExisting.Enabled = true;
                }
            }

            label3.Visible= !Common.WorkingDefinition.IsClientObjectModel;
            txtOwnerEmail.Visible = !Common.WorkingDefinition.IsClientObjectModel;
            txtOwnerUserName.Text = Common.WorkingDefinition.SiteCollOwnerLogin;
            if (string.IsNullOrEmpty(Common.WorkingDefinition.SiteCollOwnerLogin) && !string.IsNullOrEmpty(Common.WorkingDefinition.Username))
            {
                txtOwnerUserName.Text = Common.WorkingDefinition.Username;
            }
            if (!Common.WorkingDefinition.IsClientObjectModel)
            {
                txtOwnerEmail.Text = Common.WorkingDefinition.SiteCollOwnerEmail;
            }
            if (!Common.WorkingDefinition.UseExistingSiteCollection)
            {
                trackNumSiteColls.Value = Common.WorkingDefinition.CreateNewSiteCollections;
            }
            radioUseExisting.Checked = Common.WorkingDefinition.UseExistingSiteCollection;
            radioCreateNewSiteColl.Checked = !Common.WorkingDefinition.UseExistingSiteCollection;
            if (!string.IsNullOrEmpty(Common.WorkingDefinition.SiteCollection)) 
            {
                cboSiteCollection.Text = Common.WorkingDefinition.SiteCollection;
            }

        }

        public override bool saveData()
        {
            if (trackNumSiteColls.Value == 0 && cboSiteCollection.SelectedItem == null)
            {
                MessageBox.Show("Select number of new Site Collections or existing one!");
                return false;
            }

            if( radioCreateNewSiteColl.Checked )
            {
                if (txtOwnerUserName.Text == string.Empty || !WorkingDefinition.IsClientObjectModel && txtOwnerEmail.Text == string.Empty )
                {
                    MessageBox.Show("Missing user details for site collection creation!");
                    return false;
                }

                Common.WorkingDefinition.CreateNewSiteCollections = trackNumSiteColls.Value;
                Common.WorkingDefinition.UseExistingSiteCollection = false;
                Common.WorkingDefinition.SiteCollection = string.Empty;

                Common.WorkingDefinition.SiteCollOwnerLogin = txtOwnerUserName.Text;
                Common.WorkingDefinition.SiteCollOwnerEmail = txtOwnerEmail.Text;
            }
            else
            {
                Common.WorkingDefinition.CreateNewSiteCollections = 0;
                Common.WorkingDefinition.UseExistingSiteCollection = true;
                if (cboSiteCollection.SelectedItem != null)
                {
                    Common.WorkingDefinition.SiteCollection = ((ComboboxItem)cboSiteCollection.SelectedItem).Value.ToString();
                }
            }

            return true;
        }

        private void trackNumSiteColls_ValueChanged(object sender, EventArgs e)
        {
            lblCreateSiteColls.Text = trackNumSiteColls.Value.ToString();
        }

        private void frm04Collections_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            loadData();
            //TODO:rf return impersonation validation
            //if (!string.IsNullOrEmpty(Common.impersonateUserName))
            //{
            //    if (Common.impersonateValidUser(Common.impersonateUserName, Common.impersonateDomain, Common.impersonatePassword))
            //    {
            //        //Insert your code that runs under the security context of a specific user here.
            //        loadSiteCollections();
            //        Common.undoImpersonation();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Impersonation Failed!");
            //    }
            //}
            //else
            {
                loadSiteCollections();
            }
            
         
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }
}
