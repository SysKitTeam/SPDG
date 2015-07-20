using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
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
    public partial class frm04Collections : frmWizardMaster
    {
        public frm04Collections()
        {
            InitializeComponent();

            base.lblTitle.Text = "Site Collections";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(4);
            
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

        void changeRadio()
        {
            if (radioCreateNewSiteColl.Checked)
            {
                trackNumSiteColls.Enabled = true;
                cboSiteCollection.Enabled = false;
            }
            else
            {
                trackNumSiteColls.Enabled = false;
                cboSiteCollection.Enabled = true;
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
            if (Common.WorkingDefinition.UseExistingWebApplication != string.Empty)
            {
                SPWebService spWebService = SPWebService.ContentService;
                SPWebApplication webApp = spWebService.WebApplications.First(a => a.Id == new Guid( Common.WorkingDefinition.UseExistingWebApplication));

                foreach (SPSite siteColl in webApp.Sites)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = siteColl.Url;
                    item.Value = siteColl.Url;
                    cboSiteCollection.Items.Add(item);
                }
            }
        }

        public override void loadData()
        {
            if (Common.WorkingDefinition.CreateNewWebApplications > 0 )
            {
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

            trackNumSiteColls.Value = Common.WorkingDefinition.CreateNewSiteCollections;
            radioUseExisting.Checked = Common.WorkingDefinition.UseExistingSiteCollection;
            radioCreateNewSiteColl.Checked = !Common.WorkingDefinition.UseExistingSiteCollection;
            if (!string.IsNullOrEmpty(Common.WorkingDefinition.SiteCollection)) 
            {
                cboSiteCollection.Text = Common.WorkingDefinition.SiteCollection;
            }

        }

        public override bool saveData()
        {
            if( radioCreateNewSiteColl.Checked )
            {
                Common.WorkingDefinition.CreateNewSiteCollections = trackNumSiteColls.Value;
                Common.WorkingDefinition.UseExistingSiteCollection = false;
                Common.WorkingDefinition.SiteCollection = string.Empty;
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
            loadSiteCollections();
            loadData();
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }
}
