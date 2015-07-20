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
    public partial class frmDataGeneration : frmWizardMaster
    {
        DataGenerator generator = null;
        BackgroundWorker bgWorker = null;

        public frmDataGeneration()
        {
            InitializeComponent();

            base.lblTitle.Text = "Data generation in progress";

            btnNext.Visible = false;
            btnBack.Visible = false;

            btnClose.Text = "Cancel";
            btnClose.Click += btnClose_Click;

            startDataGeneration();
        }

        private void startDataGeneration()
        {
            generator = new DataGenerator(Common.WorkingDefinition);

            progressOverall.Maximum = generator.OverallProgressMaxSteps;


            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            //bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerAsync(generator);

        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressOverall.Value = generator.OverallCurrentStep;
            lblOverview.Text = generator.OverallCurrentStepDescription;
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGenerator generator = e.Argument as DataGenerator;
            bool success = generator.startDataGeneration(bgWorker);

            if (success)
            {
                MessageBox.Show("SharePoint Data Generation Done!");
            }
            else
            {
                MessageBox.Show("Error occured during data generation!");
            }
            
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }


        void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel data generation?", "SharePoint Data Generation", MessageBoxButtons.YesNo);
            if( result == System.Windows.Forms.DialogResult.Yes )
            {
                RootForm.MoveAt(12, this);
                this.Hide();
            }
        }

        private void lblDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
