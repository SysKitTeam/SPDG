using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Acceleratio.SPDG.Generator;
using System.Diagnostics;


namespace Acceleratio.SPDG.UI
{
    public partial class frmDataGeneration : frmWizardMaster
    {
        DataGenerator generator = null;
        BackgroundWorker bgWorker = null;
        bool isRunning = false;

        public frmDataGeneration()
        {
            InitializeComponent();

            this.Text = Common.APP_TITLE;

            base.lblTitle.Text = "Data generation in progress";
            base.lblDescription.Text = "SharePoint data based on prepared definition is being generated ...";
            btnOpenLog.BackColor = System.Drawing.ColorTranslator.FromHtml("#898989");

            progressOverall.ForeColor = System.Drawing.Color.FromArgb(253, 163, 36);
            progressDetails.ForeColor = System.Drawing.Color.FromArgb(253, 163, 36);

            btnNext.Visible = false;
            btnBack.Visible = false;

            btnClose.Text = "Cancel";
            btnClose.Click += btnClose_Click;

            btnOpenLog.Click += btnOpenLog_Click;

            startDataGeneration();

        }

        void btnOpenLog_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", DataGenerator.SessionID + ".log");
        }

        private void startDataGeneration()
        {
            generator = DataGenerator.Create(Common.WorkingDefinition);

            progressOverall.Maximum = generator.OverallProgressMaxSteps;

            this.Cursor = Cursors.WaitCursor;
            isRunning = true;

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            //bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerAsync(generator);

        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if( e.ProgressPercentage == 1 )
            {
                if (generator.OverallCurrentStep <= progressOverall.Maximum)
                {
                    progressOverall.Value = generator.OverallCurrentStep;
                }
                lblOverview.Text = generator.OverallCurrentStepDescription;

                lblDetails.Text = string.Empty;
                progressDetails.Value = 0;
                progressDetails.Maximum = generator.DetailProgressMaxSteps;
            }
            else if (e.ProgressPercentage == 2)
            {
                if (generator.DetailCurrentStep <= progressDetails.Maximum)
                { 
                    progressDetails.Value = generator.DetailCurrentStep;
                }
                lblDetails.Text = generator.DetailCurrentStepDescription;
            }
            else if (e.ProgressPercentage == 3)
            {
                this.Cursor = Cursors.Default;
                btnOpenLog.Visible = true;
                progressOverall.Value = progressOverall.Maximum;
                if (progressDetails.Maximum == 0) progressDetails.Maximum = 1;
                progressDetails.Value = progressDetails.Maximum;
            }

            Application.DoEvents();
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGenerator generator = e.Argument as DataGenerator;
            bool success = generator.startDataGeneration(bgWorker);
           
            isRunning = false;
            //btnClose.Text = "Close";

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
            if( isRunning )
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel data generation?", "SharePoint Data Generation", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    RootForm.MoveAt(12, this);
                    this.Hide();
                }
            }
            else
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
