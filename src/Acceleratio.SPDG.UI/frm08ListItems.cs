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
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            preventCloseMessage = true;
            RootForm.MoveNext(this);
        }

        public override void loadData()
        {
            grpDocumentLibrarySettings.Enabled = Common.WorkingDefinition.LibTypeDocument;
            chkPrefil.Checked = Common.WorkingDefinition.PrefilListAndLibrariesWithItems;
            NumberOfItemsToGenerate = Common.WorkingDefinition.MaxNumberofItemsToGenerate;
            DocLibItemsToGenerate = Common.WorkingDefinition.MaxNumberofDocumentLibraryItemsToGenerate;
            chkDOCX.Checked = Common.WorkingDefinition.IncludeDocTypeDOCX;
            chkXLSX.Checked = Common.WorkingDefinition.IncludeDocTypeXLSX;
            chkPDF.Checked = Common.WorkingDefinition.IncludeDocTypePDF;
            chkImages.Checked = Common.WorkingDefinition.IncludeDocTypeImages;
            trackMinDocSize.Value = Common.WorkingDefinition.MinDocumentSizeKB > 20 ? Common.WorkingDefinition.MinDocumentSizeKB : 20;
            trackMaxDocSize.Value = Common.WorkingDefinition.MaxDocumentSizeMB;
        }

        public override bool saveData()
        {
            if (Common.WorkingDefinition.LibTypeDocument && trackMaxNumberOfItems.Value > 0 &&
                    (chkDOCX.Checked == false && chkXLSX.Checked == false && chkPDF.Checked == false && chkImages.Checked == false))
            {
                MessageBox.Show("Select at least one document type to prefil document library!");
                return false;
            }

            if(trackMinDocSize.Value > 20 && trackMaxDocSize.Value == 0)
            {
                MessageBox.Show("Minimum document size cannot be greater than maximum document size!");
                return false;
            }

            Common.WorkingDefinition.PrefilListAndLibrariesWithItems = chkPrefil.Checked;
            Common.WorkingDefinition.MaxNumberofItemsToGenerate = NumberOfItemsToGenerate;
            Common.WorkingDefinition.MaxNumberofDocumentLibraryItemsToGenerate = DocLibItemsToGenerate;
            
            Common.WorkingDefinition.IncludeDocTypeDOCX = chkDOCX.Checked;
            Common.WorkingDefinition.IncludeDocTypeXLSX = chkXLSX.Checked;
            Common.WorkingDefinition.IncludeDocTypePDF = chkPDF.Checked;
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


        int DocLibItemsToGenerate
        {
            get { return trackMaxNumberOrDocLibItems.Value; }
            set { trackMaxNumberOrDocLibItems.Value = value; }
        }

        int NumberOfItemsToGenerate
        {
            get
            {
                return trackMaxNumberOfItems.Value;
            }
            set { trackMaxNumberOrDocLibItems.Value = value; }
        }

        private void trackMaxNumberOfItems_ValueChanged(object sender, EventArgs e)
        {
            lblNumItems.Text = NumberOfItemsToGenerate.ToString();
        }

        private void trackMinDocSize_ValueChanged(object sender, EventArgs e)
        {
            lblMinSize.Text = trackMinDocSize.Value.ToString() + " kB";
        }

        private void trackMaxDocSize_ValueChanged(object sender, EventArgs e)
        {
            lblMaxSize.Text = trackMaxDocSize.Value.ToString() + " MB";
        }

        private void trackMaxNumberOrDocLibItems_ValueChanged(object sender, EventArgs e)
        {
            lblNumDocLibItems.Text = DocLibItemsToGenerate.ToString();
        }
    }
}
