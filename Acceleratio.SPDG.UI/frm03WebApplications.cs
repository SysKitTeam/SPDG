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
    public partial class frm03WebApplications : frmWizardMaster
    {
        public frm03WebApplications()
        {
            InitializeComponent();

            base.lblTitle.Text = "Web Applications";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(3);

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


        public override void loadData()
        {
            trackCreateNewWebApplication.Value = Common.WorkingDefinition.CreateNewWebApplications;
            if ( !string.IsNullOrEmpty( Common.WorkingDefinition.UseExistingWebApplication) )
            {
                cboUseExistingWebApp.SelectedValue = Common.WorkingDefinition.UseExistingWebApplication;
            }
            
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.CreateNewWebApplications = trackCreateNewWebApplication.Value;
            if (cboUseExistingWebApp.SelectedValue != null)
            {
                Common.WorkingDefinition.UseExistingWebApplication = cboUseExistingWebApp.SelectedValue.ToString();
            }

            return true;
        }
    }
}
