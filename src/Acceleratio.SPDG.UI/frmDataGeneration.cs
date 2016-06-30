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
        private Timer _timer;
        public frmDataGeneration()
        {
            InitializeComponent();
            _timer=new Timer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = 200;
            _timer.Enabled = true;
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
            progressOverall.Maximum = 100;
            progressDetails.Maximum = 100;
            startDataGeneration();

        }

      

        void btnOpenLog_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", DataGenerator.SessionID + ".log");
        }

        private void startDataGeneration()
        {
            generator = DataGenerator.Create(Common.WorkingDefinition);
            generator.ProgressChanged += Generator_ProgressChanged;            
            this.Cursor = Cursors.WaitCursor;
            isRunning = true;

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.DoWork += bgWorker_DoWork;            
            bgWorker.RunWorkerAsync(generator);

        }


        private Generator.ProgressChangedEventArgs _lastDetailsArgs = null;        
        private void Generator_ProgressChanged(object sender, Generator.ProgressChangedEventArgs e)
        {
            if (e.ChangeType == ProgressChangeType.Details)
            {
                //details will be updated periodically so we need to save them
                //if we were to update them here and now, we could freeze the UI if they are generated to quickly
                _lastDetailsArgs = e;                
            }
            else
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    if (e.ChangeType == ProgressChangeType.Overall)
                    {
                        lblOverview.Text = e.Message;
                        lblDetails.Text = "";
                        progressDetails.Value = 0;
                        progressOverall.Value = e.ProgressPctValue;
                        _lastDetailsArgs = null;
                    }
                }));
            }
            
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var args = _lastDetailsArgs;
            if (args != null)
            {
                lblDetails.Text = args.Message;
                progressDetails.Value = args.ProgressPctValue;
            }
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGenerator generator = e.Argument as DataGenerator;
            bool success = generator.startDataGeneration();
           
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
            this.Cursor = Cursors.Default;
            btnOpenLog.Visible = true;
            progressOverall.Value = progressOverall.Maximum;
            if (progressDetails.Maximum == 0) progressDetails.Maximum = 1;
            progressDetails.Value = progressDetails.Maximum;
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
