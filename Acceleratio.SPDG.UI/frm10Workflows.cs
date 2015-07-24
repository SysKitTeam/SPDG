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
    public partial class frm10Workflows : frmWizardMaster
    {
        public frm10Workflows()
        {
            InitializeComponent();

            base.lblTitle.Text = "Workflows";
            base.lblDescription.Text = "Attach some workflow definitions.";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(10);
            this.Text = Common.APP_TITLE;

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
            chkCreateSomeOutOfTheBoxSPworkflows.Checked = Common.WorkingDefinition.CreateOutOfTheBoxWorkflowsToList;
            chkAttachCustomWF.Checked = Common.WorkingDefinition.AttachCustomWorkflowToList;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.CreateOutOfTheBoxWorkflowsToList = chkCreateSomeOutOfTheBoxSPworkflows.Checked;
            Common.WorkingDefinition.AttachCustomWorkflowToList = chkAttachCustomWF.Checked;

            return true;
        }
    }
}
