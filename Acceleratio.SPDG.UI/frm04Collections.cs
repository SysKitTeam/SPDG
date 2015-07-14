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
            loadData();
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
                cboWebApplication.Enabled = false;
            }
            else
            {
                trackNumSiteColls.Enabled = false;
                cboSiteCollection.Enabled = true;
                cboWebApplication.Enabled = true;
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

        public override void loadData()
        {
            trackNumSiteColls.Value = Common.WorkingDefinition.CreateNewSiteCollections;
            radioUseExisting.Checked = Common.WorkingDefinition.UseExistingSiteCollection;
            radioCreateNewSiteColl.Checked = !Common.WorkingDefinition.UseExistingSiteCollection;
            if (!string.IsNullOrEmpty(Common.WorkingDefinition.SiteCollection)) cboSiteCollection.SelectedValue = Common.WorkingDefinition.SiteCollection;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.CreateNewSiteCollections = trackNumSiteColls.Value;
            Common.WorkingDefinition.UseExistingSiteCollection = radioUseExisting.Checked;
            if (cboSiteCollection.SelectedItem != null) Common.WorkingDefinition.SiteCollection = cboSiteCollection.SelectedText;

            return true;
        }
    }
}
