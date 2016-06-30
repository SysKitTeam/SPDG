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
    public partial class frm09ContentTypes : frmWizardMaster
    {
        public frm09ContentTypes()
        {
            InitializeComponent();

            base.lblTitle.Text = "Content Types";
            base.lblDescription.Text = "Define Content Types number and structure.";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(9);
            this.Text = Common.APP_TITLE;

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

        public override void loadData()
        {
            chkCreateSomeConentTypes.Checked = Common.WorkingDefinition.CreateContentTypes;
            trackMaxNumberContentTypes.Value = Common.WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection;
            chkAddSiteColumns.Checked = Common.WorkingDefinition.AddSiteColumnsToContentTypes;
            trackAddSiteColumns.Value = Common.WorkingDefinition.NumberSiteColumnsPerContentType;
            chkContentTypesCanInherit.Checked = Common.WorkingDefinition.ContentTypesCanInheritFromOtherContentType;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.CreateContentTypes = chkCreateSomeConentTypes.Checked;
            Common.WorkingDefinition.MaxNumberOfContentTypesPerSiteCollection = trackMaxNumberContentTypes.Value;
            Common.WorkingDefinition.AddSiteColumnsToContentTypes = chkAddSiteColumns.Checked;
            Common.WorkingDefinition.NumberSiteColumnsPerContentType = trackAddSiteColumns.Value;
            Common.WorkingDefinition.ContentTypesCanInheritFromOtherContentType = chkContentTypesCanInherit.Checked;
            
            return true;
        }

        private void trackMaxNumberContentTypes_ValueChanged(object sender, EventArgs e)
        {
            lblMaxContentTypes.Text = trackMaxNumberContentTypes.Value.ToString();
        }

        private void trackAddSiteColumns_ValueChanged(object sender, EventArgs e)
        {
            lblSiteCols.Text = trackAddSiteColumns.Value.ToString();
        }
    }
}
