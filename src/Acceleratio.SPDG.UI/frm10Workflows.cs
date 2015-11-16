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

        public new ServerGeneratorDefinition WorkingDefinition
        {
            get { return (ServerGeneratorDefinition)base.WorkingDefinition; }
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

        public override void loadData()
        {
            chkCreateSomeOutOfTheBoxSPworkflows.Checked = WorkingDefinition.CreateOutOfTheBoxWorkflowsToList;
            chkAttachCustomWF.Checked = WorkingDefinition.AttachCustomWorkflowToList;
        }

        public override bool saveData()
        {
            WorkingDefinition.CreateOutOfTheBoxWorkflowsToList = chkCreateSomeOutOfTheBoxSPworkflows.Checked;
            WorkingDefinition.AttachCustomWorkflowToList = chkAttachCustomWF.Checked;

            return true;
        }
    }
}
