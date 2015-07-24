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
            base.lblDescription.Text = "Select number of columns and views to create in every list";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;

            this.Text = Common.APP_TITLE;
            ucSteps1.showStep(7);

            initUI();
            loadData();
        }

        private void initUI()
        {
            trackNumColumnsPerList.Enabled = false;
            trackNumViewsPerList.Enabled = false;
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

        private void trackNumColumnsPerList_ValueChanged(object sender, EventArgs e)
        {
            lblNumColumns.Text = trackNumColumnsPerList.Value.ToString();
        }

        private void trackNumViewsPerList_ValueChanged(object sender, EventArgs e)
        {
            lblNumViews.Text = trackNumViewsPerList.Value.ToString();
        }

        private void chkCreateColumns_CheckedChanged(object sender, EventArgs e)
        {
            if( chkCreateColumns.Checked )
            {
                trackNumColumnsPerList.Enabled = true;
            }
            else
            {
                trackNumColumnsPerList.Enabled = false;
                trackNumColumnsPerList.Value = 0;
            }
            
        }

        private void chkCreateViews_CheckedChanged(object sender, EventArgs e)
        {
            if( chkCreateViews.Checked )
            {
                trackNumViewsPerList.Enabled = true;
            }
            else
            {
                trackNumViewsPerList.Enabled = false;
                trackNumViewsPerList.Value = 0;
            }
        }
    }
}
