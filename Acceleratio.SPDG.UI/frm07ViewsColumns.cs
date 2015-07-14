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
    public partial class frm07ViewsColumns : frmWizardMaster
    {
        public frm07ViewsColumns()
        {
            InitializeComponent();

            base.lblTitle.Text = "View Columns";


            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(7);

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
            chkCreateViews.Checked = Common.WorkingDefinition.CreateViews;
            trackNumViewsPerList.Value = Common.WorkingDefinition.MaxNumberOfViewsPerList;
            chkCreateColumns.Checked = Common.WorkingDefinition.CreateColumns;
            trackNumColumnsPerList.Value = Common.WorkingDefinition.MaxNumberOfColumnsPerList;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.CreateViews = chkCreateViews.Checked;
            Common.WorkingDefinition.MaxNumberOfViewsPerList = trackNumViewsPerList.Value;
            Common.WorkingDefinition.CreateColumns = chkCreateColumns.Checked;
            Common.WorkingDefinition.MaxNumberOfColumnsPerList = trackNumColumnsPerList.Value;
            return true;
        }
    }
}
