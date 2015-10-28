using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.UI
{
    public partial class frm05Sites : frmWizardMaster
    {
        public frm05Sites()
        {
            InitializeComponent();

            base.lblTitle.Text = "Sites";
            base.lblDescription.Text = "Define number of SharePoint sites and number of site levels";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(5);
            this.Text = Common.APP_TITLE;

            initTemplates();
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

        private void initTemplates()
        {
            string t = "Team Site;Blank Site;Global template;Document Workspace;Basic Meeting Workspace;Blank Meeting Workspace;Decision Meeting Workspace;Social Meeting Workspace;Multipage Meeting Workspace;Central Admin Site;Wiki Site;Blog;Group Work Site;Tenant Admin Site;App Template;App Catalog Site;Access Services Site;Access Services Site Internal;Access Services Site;Document Center;Developer Site;Academic Library;eDiscovery Center;eDiscovery Case;(obsolete) Records Center;Records Center;Shared Services Administration Site;PerformancePoint;Business Intelligence Center;SharePoint Portal Server Site;SharePoint Portal Server Personal Space;Storage And Social SharePoint Portal Server Personal Space;Storage Only SharePoint Portal Server Personal Space;Social Only SharePoint Portal Server Personal Space;Empty SharePoint Portal Server Personal Space;Personalization Site;Contents area Template;Topic area template;News Site;Publishing Site;Publishing Site;Press Releases Site;Publishing Site with Workflow;News Site;Site Directory;Community area template;Report Center;Collaboration Portal;Enterprise Search Center;Profiles;Publishing Portal;My Site Host;Enterprise Wiki;Project Site;Product Catalog;Community Site;Community Portal;Basic Search Center;Basic Search Center;Visio Process Repository";
            string[] templates = t.Split(';');

            foreach (string template in templates)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = template;
                item.Value = template;
                cboSiteTemplates.Items.Add(item);
            }
        }

        public override void loadData()
        {
            trackNumSitesToCreate.Value = Common.WorkingDefinition.NumberOfSitesToCreate;
            trackMaxNumberLevels.Value = Common.WorkingDefinition.MaxNumberOfLevelsForSites;

            if (!string.IsNullOrEmpty(Common.WorkingDefinition.SiteTemplate))
            {
                cboSiteTemplates.Text = Common.WorkingDefinition.SiteTemplate;
            }
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.NumberOfSitesToCreate = trackNumSitesToCreate.Value;
            Common.WorkingDefinition.MaxNumberOfLevelsForSites = trackMaxNumberLevels.Value;

            if (cboSiteTemplates.SelectedItem != null)
            {
                Common.WorkingDefinition.SiteTemplate = ((ComboboxItem)cboSiteTemplates.SelectedItem).Value.ToString();
            }

            return true;
        }

        private void trackNumSitesToCreate_ValueChanged(object sender, EventArgs e)
        {
            lblNumSites.Text = trackNumSitesToCreate.Value.ToString();
        }

        private void trackMaxNumberLevels_ValueChanged(object sender, EventArgs e)
        {
            lblNumberLevels.Text = trackMaxNumberLevels.Value.ToString();
        }
    }
}
