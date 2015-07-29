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
    public partial class frm08ListItems : frmWizardMaster
    {
        public frm08ListItems()
        {
            InitializeComponent();

            base.lblTitle.Text = "List Items";
            base.lblDescription.Text = "Define list items && documents number, type and size.";

            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(8);
            this.Text = Common.APP_TITLE;

            initUI();
            loadData();
        }

        private void initUI()
        {
            trackMaxNumberOfItems.Enabled = false;
            chkDOCX.Enabled = false;
            chkImages.Enabled = false;
            chkPDF.Enabled = false;
            chkXLSX.Enabled = false;

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
            chkPrefil.Checked = Common.WorkingDefinition.PrefilListAndLibrariesWithItems;
            trackMaxNumberOfItems.Value = Common.WorkingDefinition.MaxNumberofItemsToGenerate;
            chkDOCX.Checked = Common.WorkingDefinition.IncludeDocTypeDOCX;
            chkXLSX.Checked = Common.WorkingDefinition.IncludeDocTypeXLSX;
            chkPDF.Checked = Common.WorkingDefinition.IncludeDocTypePDF;
            chkImages.Checked = Common.WorkingDefinition.IncludeDocTypeImages;
            trackMinDocSize.Value = Common.WorkingDefinition.MinDocumentSizeKB > 20 ? Common.WorkingDefinition.MinDocumentSizeKB : 20;
            trackMaxDocSize.Value = Common.WorkingDefinition.MaxDocumentSizeMB;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.PrefilListAndLibrariesWithItems = chkPrefil.Checked;
            Common.WorkingDefinition.MaxNumberofItemsToGenerate = trackMaxNumberOfItems.Value;
            Common.WorkingDefinition.IncludeDocTypeDOCX = chkDOCX.Checked;
            Common.WorkingDefinition.IncludeDocTypeXLSX= chkXLSX.Checked;
            Common.WorkingDefinition.IncludeDocTypePDF= chkPDF.Checked;
            Common.WorkingDefinition.IncludeDocTypeImages = chkImages.Checked;
            Common.WorkingDefinition.MinDocumentSizeKB = trackMinDocSize.Value;
            Common.WorkingDefinition.MaxDocumentSizeMB  = trackMaxDocSize.Value;
            return true;
        }

        private void chkPrefil_CheckedChanged(object sender, EventArgs e)
        {
            if( chkPrefil.Checked )
            {
                trackMaxNumberOfItems.Enabled = true;
                if( Common.WorkingDefinition.LibTypeDocument )
                {
                    chkDOCX.Enabled = true;
                    chkImages.Enabled = true;
                    chkPDF.Enabled = true;
                    chkXLSX.Enabled = true;
                }
            }
            else
            {
                trackMaxNumberOfItems.Enabled = false;
                trackMaxNumberOfItems.Value = 0;
                chkDOCX.Enabled = false;
                chkImages.Enabled = false;
                chkPDF.Enabled = false;
                chkXLSX.Enabled = false;
            }
        }

        private void trackMaxNumberOfItems_ValueChanged(object sender, EventArgs e)
        {
            lblNumItems.Text = trackMaxNumberOfItems.Value.ToString();
        }

        private void trackMinDocSize_ValueChanged(object sender, EventArgs e)
        {
            lblMinSize.Text = trackMinDocSize.Value.ToString() + " kB";
        }

        private void trackMaxDocSize_ValueChanged(object sender, EventArgs e)
        {
            lblMaxSize.Text = trackMaxDocSize.Value.ToString() + " MB";
        }
    }
}
