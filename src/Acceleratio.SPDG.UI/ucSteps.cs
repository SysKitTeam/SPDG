using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acceleratio.SPDG.UI
{
    public partial class ucSteps : UserControl
    {
        public ucSteps()
        {
            InitializeComponent();

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Name.StartsWith("lblStep"))
                {
                    ((LinkLabel)ctrl).Links[0].Enabled = false;
                    ((LinkLabel)ctrl).ActiveLinkColor = System.Drawing.Color.FromArgb(88, 177, 198);
                }
            }
        }

        private List<int> _serverSteps = new List<int>() {3, 9, 10};

        public void showStep(int stepNumber)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Name.StartsWith("lblStep"))
                {
                    int lblNumber = Convert.ToInt32(ctrl.Name.Replace("lblStep", "").TrimStart('0'));
                    if (lblNumber <= stepNumber)
                    {                     
                        bool canBeEnabled = !_serverSteps.Contains(lblNumber) || !Common.WorkingDefinition.IsClientObjectModel;
                        if (canBeEnabled)
                        {
                            ctrl.Enabled = true;
                            ((LinkLabel) ctrl).Links[0].Enabled = true;
                            Control[] ctrls = this.Controls.Find("pictureBox" + lblNumber, false);
                            if (ctrls.Length > 0)
                            {
                                ((PictureBox) ctrls[0]).Image = Properties.Resources.temp_icona_12;
                            }
                        }
                    }
                }
            }


        }

        private void ucSteps_Paint(object sender, PaintEventArgs e)
        {
           // base.OnPaint(e);
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.LightGray);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.DrawLine(myPen, 220, 10, 220, 450);
            //formGraphics.DrawLine(myPen, 200, 300, 532, 286);
            myPen.Dispose();
        }

        private void lblStep01_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (((frmWizardMaster) this.Parent).RootForm != null)
            {
                ((frmWizardMaster)this.Parent).RootForm.MoveAt(1, this.ParentForm);
            }
            
        }

        private void lblStep02_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(2, this.ParentForm);
        }

        private void lblStep03_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(3, this.ParentForm);
        }

        private void lblStep04_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(4, this.ParentForm);
        }

        private void lblStep05_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(5, this.ParentForm);
        }

        private void lblStep06_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(6, this.ParentForm);
        }

        private void lblStep07_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(7, this.ParentForm);
        }

        private void lblStep08_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(8, this.ParentForm);
        }

        private void lblStep09_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(9, this.ParentForm);
        }

        private void lblStep10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(10, this.ParentForm);
        }

        private void lblStep11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(11, this.ParentForm);
        }

        private void lblStep12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((frmWizardMaster)this.Parent).RootForm.MoveAt(12, this.ParentForm);
        }

        


    }
}
