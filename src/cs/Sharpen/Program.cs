// <copyright file="Program.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      Program: Class containing the main entry point for the program.
// </summary>

using System;
using System.Windows.Forms;

namespace BWHazel.Sharpen
{
    /// <summary>
    /// Main program class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSharpen(new Encounter()));
        }
    }
}
