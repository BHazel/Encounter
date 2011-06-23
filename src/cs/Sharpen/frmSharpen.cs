using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace BWHazel.Sharpen
{
    public partial class frmSharpen : Form
    {
        string sharpenCopyright;
        string sharpenTitle;
        string sharpenVersion;
        Encounter encounter;
        FileIOPermission filePerms;

        public frmSharpen()
        {
            InitializeComponent();
            this.SetSharpenDetails();
            encounter = new Encounter();
        }

        private void SetSharpenDetails()
        {
            foreach (Attribute attr in Assembly.GetExecutingAssembly().GetCustomAttributes(false))
            {
                if (attr.GetType() == typeof(AssemblyCopyrightAttribute))
                {
                    sharpenCopyright = ((AssemblyCopyrightAttribute)attr).Copyright;
                }
                if (attr.GetType() == typeof(AssemblyTitleAttribute))
                {
                    sharpenTitle = ((AssemblyTitleAttribute)attr).Title;
                }
                if (attr.GetType() == typeof(AssemblyFileVersionAttribute))
                {
                    sharpenVersion = ((AssemblyFileVersionAttribute)attr).Version;
                }
            }
        }

        private void OpenFile()
        {
            this.ResetUi();
            if (dfoOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filePerms = new FileIOPermission(FileIOPermissionAccess.Read, dfoOpen.FileName);
                    filePerms.Demand();
                    encounter.SetEnergies(dfoOpen.FileName);
                    encounter.SetInteractionEnergies();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, sharpenTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (encounter.EnergyCount >= 3) encounter.SetInteractionEnergies();
                }
                finally
                {
                    this.SetUi();
                }
            }
        }

        private void ExportFile()
        {
            if (dfsExport.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filePerms = new FileIOPermission(FileIOPermissionAccess.Write, dfsExport.FileName);
                    filePerms.Demand();
                    StreamWriter writer = new StreamWriter(dfsExport.FileName);
                    writer.WriteLine(encounter.ToCsv());
                    writer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, sharpenTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SetUi()
        {
            if (encounter.EnergyCount >= 1) txtDimerDimer.Text = encounter.Dimer.ToString();
            if (encounter.EnergyCount >= 2) txtMonADimer.Text = encounter.MonomerADimerBasis.ToString();
            if (encounter.EnergyCount >= 3)
            {
                txtMonBDimer.Text = encounter.MonomerBDimerBasis.ToString();
                if (encounter.EnergyCount >= 4) txtMonAMon.Text = encounter.MonomerAMonomerBasis.ToString();
                if (encounter.EnergyCount == 5) txtMonBMon.Text = encounter.MonomerBMonomerBasis.ToString();
                txtInteractionHartree.Text = encounter.InteractionEnergyHartrees.ToString();
                txtInteractionKjmol.Text = encounter.InteractionEnergyKJMol.ToString();
                txtBindingConstant.Text = encounter.BindingConstant.ToString();
            }
        }

        private void ResetUi()
        {
            txtDimerDimer.Text = "";
            txtMonADimer.Text = "";
            txtMonBDimer.Text = "";
            txtMonAMon.Text = "";
            txtMonBMon.Text = "";
            txtInteractionHartree.Text = "";
            txtInteractionKjmol.Text = "";
            txtBindingConstant.Text = "";
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        private void mnuFileExport_Click(object sender, EventArgs e)
        {
            this.ExportFile();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("{0} (.NET/C# Implementation) {1}\n\n{2}", sharpenTitle, sharpenVersion, sharpenCopyright), "Encounter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            this.ExportFile();
        }
    }
}
