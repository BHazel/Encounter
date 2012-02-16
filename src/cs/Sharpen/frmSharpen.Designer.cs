namespace BWHazel.Sharpen
{
    partial class frmSharpen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnsSharpen = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tlsSharpen = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.lblMoleculeEnergy = new System.Windows.Forms.Label();
            this.lblDimerBasis = new System.Windows.Forms.Label();
            this.lblDimerDimer = new System.Windows.Forms.Label();
            this.txtDimerDimer = new System.Windows.Forms.TextBox();
            this.lblMonADimer = new System.Windows.Forms.Label();
            this.txtMonADimer = new System.Windows.Forms.TextBox();
            this.txtMonBDimer = new System.Windows.Forms.TextBox();
            this.lblMonBDimer = new System.Windows.Forms.Label();
            this.lblMonomerBasis = new System.Windows.Forms.Label();
            this.lblMonAMon = new System.Windows.Forms.Label();
            this.txtMonAMon = new System.Windows.Forms.TextBox();
            this.txtMonBMon = new System.Windows.Forms.TextBox();
            this.lblMonBMon = new System.Windows.Forms.Label();
            this.lblInteractionEnergy = new System.Windows.Forms.Label();
            this.lblInteractionHartree = new System.Windows.Forms.Label();
            this.lblInteractionKjmol = new System.Windows.Forms.Label();
            this.txtInteractionHartree = new System.Windows.Forms.TextBox();
            this.txtInteractionKjmol = new System.Windows.Forms.TextBox();
            this.lblBindingConstant = new System.Windows.Forms.Label();
            this.lblNoUnits = new System.Windows.Forms.Label();
            this.txtBindingConstant = new System.Windows.Forms.TextBox();
            this.dfoOpen = new System.Windows.Forms.OpenFileDialog();
            this.dfsExport = new System.Windows.Forms.SaveFileDialog();
            this.mnsSharpen.SuspendLayout();
            this.tlsSharpen.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsSharpen
            // 
            this.mnsSharpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.mnsSharpen.Location = new System.Drawing.Point(0, 0);
            this.mnsSharpen.Name = "mnsSharpen";
            this.mnsSharpen.Size = new System.Drawing.Size(431, 24);
            this.mnsSharpen.TabIndex = 0;
            this.mnsSharpen.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.mnuFileExport,
            this.mnuFileSeparator,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::BWHazel.Sharpen.Properties.Resources.IMG_OPEN_FILE;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(156, 22);
            this.mnuFileOpen.Text = "Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::BWHazel.Sharpen.Properties.Resources.IMG_SAVE_FILE;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileExport.Size = new System.Drawing.Size(156, 22);
            this.mnuFileExport.Text = "Export...";
            this.mnuFileExport.Click += new System.EventHandler(this.mnuFileExport_Click);
            // 
            // mnuFileSeparator
            // 
            this.mnuFileSeparator.Name = "mnuFileSeparator";
            this.mnuFileSeparator.Size = new System.Drawing.Size(153, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuFileExit.Size = new System.Drawing.Size(156, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(40, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(103, 22);
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // tlsSharpen
            // 
            this.tlsSharpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tsbExport});
            this.tlsSharpen.Location = new System.Drawing.Point(0, 24);
            this.tlsSharpen.Name = "tlsSharpen";
            this.tlsSharpen.Size = new System.Drawing.Size(431, 52);
            this.tlsSharpen.TabIndex = 1;
            this.tlsSharpen.Text = "Sharpen Toolbar";
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = global::BWHazel.Sharpen.Properties.Resources.IMG_OPEN_FILE;
            this.tsbOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(37, 49);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::BWHazel.Sharpen.Properties.Resources.IMG_SAVE_FILE;
            this.tsbExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(43, 49);
            this.tsbExport.Text = "Export";
            this.tsbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // lblMoleculeEnergy
            // 
            this.lblMoleculeEnergy.AutoSize = true;
            this.lblMoleculeEnergy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoleculeEnergy.Location = new System.Drawing.Point(13, 83);
            this.lblMoleculeEnergy.Name = "lblMoleculeEnergy";
            this.lblMoleculeEnergy.Size = new System.Drawing.Size(125, 13);
            this.lblMoleculeEnergy.TabIndex = 2;
            this.lblMoleculeEnergy.Text = "Molecule Energy /au";
            // 
            // lblDimerBasis
            // 
            this.lblDimerBasis.AutoSize = true;
            this.lblDimerBasis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDimerBasis.Location = new System.Drawing.Point(13, 110);
            this.lblDimerBasis.Name = "lblDimerBasis";
            this.lblDimerBasis.Size = new System.Drawing.Size(73, 13);
            this.lblDimerBasis.TabIndex = 3;
            this.lblDimerBasis.Text = "Dimer Basis";
            // 
            // lblDimerDimer
            // 
            this.lblDimerDimer.AutoSize = true;
            this.lblDimerDimer.Location = new System.Drawing.Point(13, 137);
            this.lblDimerDimer.Name = "lblDimerDimer";
            this.lblDimerDimer.Size = new System.Drawing.Size(34, 13);
            this.lblDimerDimer.TabIndex = 4;
            this.lblDimerDimer.Text = "Dimer";
            // 
            // txtDimerDimer
            // 
            this.txtDimerDimer.Location = new System.Drawing.Point(91, 134);
            this.txtDimerDimer.Name = "txtDimerDimer";
            this.txtDimerDimer.Size = new System.Drawing.Size(100, 20);
            this.txtDimerDimer.TabIndex = 5;
            // 
            // lblMonADimer
            // 
            this.lblMonADimer.AutoSize = true;
            this.lblMonADimer.Location = new System.Drawing.Point(13, 164);
            this.lblMonADimer.Name = "lblMonADimer";
            this.lblMonADimer.Size = new System.Drawing.Size(61, 13);
            this.lblMonADimer.TabIndex = 6;
            this.lblMonADimer.Text = "Monomer A";
            // 
            // txtMonADimer
            // 
            this.txtMonADimer.Location = new System.Drawing.Point(91, 161);
            this.txtMonADimer.Name = "txtMonADimer";
            this.txtMonADimer.Size = new System.Drawing.Size(100, 20);
            this.txtMonADimer.TabIndex = 7;
            // 
            // txtMonBDimer
            // 
            this.txtMonBDimer.Location = new System.Drawing.Point(91, 188);
            this.txtMonBDimer.Name = "txtMonBDimer";
            this.txtMonBDimer.Size = new System.Drawing.Size(100, 20);
            this.txtMonBDimer.TabIndex = 8;
            // 
            // lblMonBDimer
            // 
            this.lblMonBDimer.AutoSize = true;
            this.lblMonBDimer.Location = new System.Drawing.Point(13, 191);
            this.lblMonBDimer.Name = "lblMonBDimer";
            this.lblMonBDimer.Size = new System.Drawing.Size(61, 13);
            this.lblMonBDimer.TabIndex = 9;
            this.lblMonBDimer.Text = "Monomer B";
            // 
            // lblMonomerBasis
            // 
            this.lblMonomerBasis.AutoSize = true;
            this.lblMonomerBasis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonomerBasis.Location = new System.Drawing.Point(13, 225);
            this.lblMonomerBasis.Name = "lblMonomerBasis";
            this.lblMonomerBasis.Size = new System.Drawing.Size(92, 13);
            this.lblMonomerBasis.TabIndex = 10;
            this.lblMonomerBasis.Text = "Monomer Basis";
            // 
            // lblMonAMon
            // 
            this.lblMonAMon.AutoSize = true;
            this.lblMonAMon.Location = new System.Drawing.Point(13, 252);
            this.lblMonAMon.Name = "lblMonAMon";
            this.lblMonAMon.Size = new System.Drawing.Size(61, 13);
            this.lblMonAMon.TabIndex = 11;
            this.lblMonAMon.Text = "Monomer A";
            // 
            // txtMonAMon
            // 
            this.txtMonAMon.Location = new System.Drawing.Point(91, 249);
            this.txtMonAMon.Name = "txtMonAMon";
            this.txtMonAMon.Size = new System.Drawing.Size(100, 20);
            this.txtMonAMon.TabIndex = 12;
            // 
            // txtMonBMon
            // 
            this.txtMonBMon.Location = new System.Drawing.Point(91, 276);
            this.txtMonBMon.Name = "txtMonBMon";
            this.txtMonBMon.Size = new System.Drawing.Size(100, 20);
            this.txtMonBMon.TabIndex = 13;
            // 
            // lblMonBMon
            // 
            this.lblMonBMon.AutoSize = true;
            this.lblMonBMon.Location = new System.Drawing.Point(13, 279);
            this.lblMonBMon.Name = "lblMonBMon";
            this.lblMonBMon.Size = new System.Drawing.Size(61, 13);
            this.lblMonBMon.TabIndex = 14;
            this.lblMonBMon.Text = "Monomer B";
            // 
            // lblInteractionEnergy
            // 
            this.lblInteractionEnergy.AutoSize = true;
            this.lblInteractionEnergy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInteractionEnergy.Location = new System.Drawing.Point(252, 83);
            this.lblInteractionEnergy.Name = "lblInteractionEnergy";
            this.lblInteractionEnergy.Size = new System.Drawing.Size(111, 13);
            this.lblInteractionEnergy.TabIndex = 15;
            this.lblInteractionEnergy.Text = "Interaction Energy";
            // 
            // lblInteractionHartree
            // 
            this.lblInteractionHartree.AutoSize = true;
            this.lblInteractionHartree.Location = new System.Drawing.Point(252, 137);
            this.lblInteractionHartree.Name = "lblInteractionHartree";
            this.lblInteractionHartree.Size = new System.Drawing.Size(24, 13);
            this.lblInteractionHartree.TabIndex = 16;
            this.lblInteractionHartree.Text = "/au";
            // 
            // lblInteractionKjmol
            // 
            this.lblInteractionKjmol.AutoSize = true;
            this.lblInteractionKjmol.Location = new System.Drawing.Point(252, 164);
            this.lblInteractionKjmol.Name = "lblInteractionKjmol";
            this.lblInteractionKjmol.Size = new System.Drawing.Size(44, 13);
            this.lblInteractionKjmol.TabIndex = 17;
            this.lblInteractionKjmol.Text = "/kJ/mol";
            // 
            // txtInteractionHartree
            // 
            this.txtInteractionHartree.Location = new System.Drawing.Point(305, 134);
            this.txtInteractionHartree.Name = "txtInteractionHartree";
            this.txtInteractionHartree.Size = new System.Drawing.Size(100, 20);
            this.txtInteractionHartree.TabIndex = 18;
            // 
            // txtInteractionKjmol
            // 
            this.txtInteractionKjmol.Location = new System.Drawing.Point(305, 161);
            this.txtInteractionKjmol.Name = "txtInteractionKjmol";
            this.txtInteractionKjmol.Size = new System.Drawing.Size(100, 20);
            this.txtInteractionKjmol.TabIndex = 19;
            // 
            // lblBindingConstant
            // 
            this.lblBindingConstant.AutoSize = true;
            this.lblBindingConstant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBindingConstant.Location = new System.Drawing.Point(252, 225);
            this.lblBindingConstant.Name = "lblBindingConstant";
            this.lblBindingConstant.Size = new System.Drawing.Size(103, 13);
            this.lblBindingConstant.TabIndex = 20;
            this.lblBindingConstant.Text = "Binding Constant";
            // 
            // lblNoUnits
            // 
            this.lblNoUnits.AutoSize = true;
            this.lblNoUnits.Location = new System.Drawing.Point(252, 252);
            this.lblNoUnits.Name = "lblNoUnits";
            this.lblNoUnits.Size = new System.Drawing.Size(18, 13);
            this.lblNoUnits.TabIndex = 21;
            this.lblNoUnits.Text = "/1";
            // 
            // txtBindingConstant
            // 
            this.txtBindingConstant.Location = new System.Drawing.Point(305, 249);
            this.txtBindingConstant.Name = "txtBindingConstant";
            this.txtBindingConstant.Size = new System.Drawing.Size(100, 20);
            this.txtBindingConstant.TabIndex = 22;
            // 
            // dfoOpen
            // 
            this.dfoOpen.Filter = "Gaussian Calculation Files|*.log";
            this.dfoOpen.Title = "Open";
            // 
            // dfsExport
            // 
            this.dfsExport.Filter = "Comma Separated Values|*.csv";
            this.dfsExport.Title = "Export";
            // 
            // frmSharpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 306);
            this.Controls.Add(this.txtBindingConstant);
            this.Controls.Add(this.lblNoUnits);
            this.Controls.Add(this.lblBindingConstant);
            this.Controls.Add(this.txtInteractionKjmol);
            this.Controls.Add(this.txtInteractionHartree);
            this.Controls.Add(this.lblInteractionKjmol);
            this.Controls.Add(this.lblInteractionHartree);
            this.Controls.Add(this.lblInteractionEnergy);
            this.Controls.Add(this.lblMonBMon);
            this.Controls.Add(this.txtMonBMon);
            this.Controls.Add(this.txtMonAMon);
            this.Controls.Add(this.lblMonAMon);
            this.Controls.Add(this.lblMonomerBasis);
            this.Controls.Add(this.lblMonBDimer);
            this.Controls.Add(this.txtMonBDimer);
            this.Controls.Add(this.txtMonADimer);
            this.Controls.Add(this.lblMonADimer);
            this.Controls.Add(this.txtDimerDimer);
            this.Controls.Add(this.lblDimerDimer);
            this.Controls.Add(this.lblDimerBasis);
            this.Controls.Add(this.lblMoleculeEnergy);
            this.Controls.Add(this.tlsSharpen);
            this.Controls.Add(this.mnsSharpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnsSharpen;
            this.MaximizeBox = false;
            this.Name = "frmSharpen";
            this.Text = "Sharpen";
            this.mnsSharpen.ResumeLayout(false);
            this.mnsSharpen.PerformLayout();
            this.tlsSharpen.ResumeLayout(false);
            this.tlsSharpen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnsSharpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExport;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripSeparator mnuFileSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStrip tlsSharpen;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.Label lblMoleculeEnergy;
        private System.Windows.Forms.Label lblDimerBasis;
        private System.Windows.Forms.Label lblDimerDimer;
        private System.Windows.Forms.TextBox txtDimerDimer;
        private System.Windows.Forms.Label lblMonADimer;
        private System.Windows.Forms.TextBox txtMonADimer;
        private System.Windows.Forms.TextBox txtMonBDimer;
        private System.Windows.Forms.Label lblMonBDimer;
        private System.Windows.Forms.Label lblMonomerBasis;
        private System.Windows.Forms.Label lblMonAMon;
        private System.Windows.Forms.TextBox txtMonAMon;
        private System.Windows.Forms.TextBox txtMonBMon;
        private System.Windows.Forms.Label lblMonBMon;
        private System.Windows.Forms.Label lblInteractionEnergy;
        private System.Windows.Forms.Label lblInteractionHartree;
        private System.Windows.Forms.Label lblInteractionKjmol;
        private System.Windows.Forms.TextBox txtInteractionHartree;
        private System.Windows.Forms.TextBox txtInteractionKjmol;
        private System.Windows.Forms.Label lblBindingConstant;
        private System.Windows.Forms.Label lblNoUnits;
        private System.Windows.Forms.TextBox txtBindingConstant;
        private System.Windows.Forms.OpenFileDialog dfoOpen;
        private System.Windows.Forms.SaveFileDialog dfsExport;
    }
}

