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
    public partial class frmWizardMaster : Form
    {
        public frmWizardMaster()
        {
            InitializeComponent();
        }

        public frm01Connect RootForm { get; set; }

        protected bool preventCloseMessage { get; set; }
        internal bool preventAppExitMessage { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmWizardMaster_LocationChanged(object sender, EventArgs e)
        {
            //if( RootForm != null)
            //{ 
            //    RootForm.Location = new Point(this.Location.X, this.Location.Y);
            //}
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if( btnClose.Text != "Cancel")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit application?", "SharePoint Data Generation", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void loadDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "openFileDialog1")
            {
                Common.DeserializeDefinition(openFileDialog1.FileName);

                if( this is frm01Connect)
                {
                    ((frm01Connect)this).loadData();
                } 
                else
                {
                    RootForm.MoveAt(1, this);
                    RootForm.loadData();
                }
                
            }
        }

        private void saveDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "saveFileDialog1" )
            {
                Common.SerializeDefinition(saveFileDialog1.FileName);
                MessageBox.Show("Definition saved successfully.");
            }
        }

        private void newDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            if (this is frm01Connect)
            {
                Common.InitEmptyDefinition();
                ((frm01Connect)this).loadData();
            }
            else
            {
                RootForm.MoveAt(1, this);
                System.Threading.Thread.Sleep(200);
                Common.InitEmptyDefinition();
                System.Threading.Thread.Sleep(200);
                RootForm.loadData();
            }
            
        }

        public virtual bool saveData()
        {
            return true;
        }

        public virtual void loadData()
        { }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
