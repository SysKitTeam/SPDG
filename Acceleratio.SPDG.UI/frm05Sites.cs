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
    public partial class frm05Sites : frmWizardMaster
    {
        public frm05Sites()
        {
            InitializeComponent();

            base.lblTitle.Text = "Sites";


            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(5);
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
            trackNumSitesToCreate.Value = Common.WorkingDefinition.NumberOfSitesToCreate;
            trackMaxNumberLevels.Value = Common.WorkingDefinition.MaxNumberOfLevelsForSites;
        }

        public override bool saveData()
        {
            Common.WorkingDefinition.NumberOfSitesToCreate = trackNumSitesToCreate.Value;
            Common.WorkingDefinition.MaxNumberOfLevelsForSites = trackMaxNumberLevels.Value;

            return true;
        }
    }
}
