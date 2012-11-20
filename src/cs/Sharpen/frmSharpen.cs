// <copyright file="frmSharpen.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      frmSharpen: Class for the main Sharpen form.
// </summary>

using System;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;
using BWHazel.Sharpen.DataFormatters;

namespace BWHazel.Sharpen
{
    /// <summary>
    /// Sharpen form.
    /// </summary>
    public partial class frmSharpen : Form
    {
        /// <summary>Sharpen copyright details.</summary>
        private string sharpenCopyright;

        /// <summary>Sharpen title.</summary>
        private string sharpenTitle;

        /// <summary>Sharpen version.</summary>
        private string sharpenVersion;

        /// <summary>Encounter instance.</summary>
        private Encounter encounter;

        /// <summary>File IO permissions for opening and saving files.</summary>
        private FileIOPermission filePerms;

        /// <summary>
        /// Initialises a new instance of the <see cref="frmSharpen"/> class with an instance of a type implementing <see cref="IEncounter"/>.
        /// </summary>
        /// <param name="enc">Instance of type implementing <see cref="IEncounter"/>.</param>
        public frmSharpen(IEncounter enc)
        {
            this.InitializeComponent();
            this.SetSharpenDetails();
            this.encounter = enc as Encounter;
        }

        /// <summary>
        /// Sets Sharpen copyright details, title, and version via reflection.
        /// </summary>
        private void SetSharpenDetails()
        {
            foreach (Attribute attr in Assembly.GetExecutingAssembly().GetCustomAttributes(false))
            {
                if (attr.GetType() == typeof(AssemblyCopyrightAttribute))
                {
                    this.sharpenCopyright = ((AssemblyCopyrightAttribute)attr).Copyright;
                }

                if (attr.GetType() == typeof(AssemblyTitleAttribute))
                {
                    this.sharpenTitle = ((AssemblyTitleAttribute)attr).Title;
                }

                if (attr.GetType() == typeof(AssemblyFileVersionAttribute))
                {
                    this.sharpenVersion = ((AssemblyFileVersionAttribute)attr).Version;
                }
            }
        }

        /// <summary>
        /// Opens a file using an <see cref="System.Windows.Forms.OpenFileDialog"/> and sets the UI if successful.
        /// </summary>
        private void OpenFile()
        {
            if (this.dfoOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.filePerms = new FileIOPermission(FileIOPermissionAccess.Read, dfoOpen.FileName);
                    this.filePerms.Demand();
                    this.encounter.SetEnergies(this.dfoOpen.FileName);
                    this.encounter.SetInteractionEnergies();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.sharpenTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (this.encounter.EnergyCount >= 3)
                    {
                        this.encounter.SetInteractionEnergies();
                    }
                }
                finally
                {
                    this.ResetUi();
                    this.SetUi();
                }
            }
        }

        /// <summary>
        /// Exports data to a file using a <see cref="System.Windows.Forms.SaveFileDialog"/>.
        /// </summary>
        /// <exception cref="System.ApplicationException">Exception thrown if an unknown file type is selected.</exception>
        private void ExportFile()
        {
            if (dfsExport.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.filePerms = new FileIOPermission(FileIOPermissionAccess.Write, dfsExport.FileName);
                    this.filePerms.Demand();
                    IDataFormatter formatter;
                    if (dfsExport.FilterIndex == 1)
                    {
                        formatter = new CsvDataFormatter();
                    }
                    else if (dfsExport.FilterIndex == 2)
                    {
                        formatter = new JsonDataFormatter();
                    }
                    else if (dfsExport.FilterIndex == 3)
                    {
                        formatter = new XmlDataFormatter();
                    }
                    else
                    {
                        throw new ApplicationException("Unknown file type selected");
                    }

                    formatter.ExportData(this.encounter, new FileStream(dfsExport.FileName, FileMode.OpenOrCreate));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.sharpenTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Sets the text fields on the Sharpen form depending on the number of energy values in the <see cref="IEncounter"/> instance.
        /// </summary>
        private void SetUi()
        {
            this.txtDescription.Text = this.encounter.Description;
            if (this.encounter.EnergyCount >= 1)
            {
                this.txtDimerDimer.Text = this.encounter.Dimer.ToString();
            }

            if (this.encounter.EnergyCount >= 2)
            {
                this.txtMonADimer.Text = this.encounter.MonomerADimerBasis.ToString();
            }

            if (this.encounter.EnergyCount >= 3)
            {
                this.txtMonBDimer.Text = this.encounter.MonomerBDimerBasis.ToString();
                if (this.encounter.EnergyCount >= 4)
                {
                    this.txtMonAMon.Text = this.encounter.MonomerAMonomerBasis.ToString();
                }

                if (this.encounter.EnergyCount == 5)
                {
                    this.txtMonBMon.Text = this.encounter.MonomerBMonomerBasis.ToString();
                }

                this.txtInteractionHartree.Text = this.encounter.InteractionEnergyHartrees.ToString();
                this.txtInteractionKjmol.Text = this.encounter.InteractionEnergyKjmol.ToString();
                this.txtBindingConstant.Text = this.encounter.BindingConstant.ToString();
            }
        }

        /// <summary>
        /// Sets all text fields on the Sharpen form to empty.
        /// </summary>
        private void ResetUi()
        {
            txtDescription.Text = string.Empty;
            txtDimerDimer.Text = string.Empty;
            txtMonADimer.Text = string.Empty;
            txtMonBDimer.Text = string.Empty;
            txtMonAMon.Text = string.Empty;
            txtMonBMon.Text = string.Empty;
            txtInteractionHartree.Text = string.Empty;
            txtInteractionKjmol.Text = string.Empty;
            txtBindingConstant.Text = string.Empty;
        }

        /// <summary>
        /// Handles the File->Open menu command: opens a file for processing.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        /// <summary>
        /// Handles the File->Export menu command: exports data in the <see cref="IEncounter"/> instance to a file.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void mnuFileExport_Click(object sender, EventArgs e)
        {
            this.ExportFile();
        }

        /// <summary>
        /// Handles the File->Exit menu command: exits Sharpen.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the Help->About menu command: displays the About dialog.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("{0} (.NET/C# Implementation) {1}\n\n{2}", this.sharpenTitle, this.sharpenVersion, this.sharpenCopyright), "Encounter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the Open toolbar button command: opens a file for processing.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void tsbOpen_Click(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        /// <summary>
        /// Handles the Export toolbar button command: exports data in the <see cref="IEncounter"/> instance to a file.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void tsbExport_Click(object sender, EventArgs e)
        {
            this.ExportFile();
        }
    }
}
