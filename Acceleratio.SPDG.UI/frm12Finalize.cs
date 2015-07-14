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
    public partial class frm12Finalize : frmWizardMaster
    {
        public frm12Finalize()
        {
            InitializeComponent();

            base.lblTitle.Text = "Finalize";

            btnNext.Text = "Start >";
            btnNext.Click += btnNext_Click;
            btnBack.Click += btnBack_Click;
            ucSteps1.showStep(12);
            this.Text = Common.APP_TITLE;
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


    }
}
