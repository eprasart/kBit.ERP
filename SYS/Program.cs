using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using kPrasat.SYS;

namespace kPrasat
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //todo: splash screen
            App.Init();
            //if (new SM.frmLogin().ShowDialog() == DialogResult.OK)
                Application.Run(new frmMain());
            //else
            //    Application.Exit();
        }
    }
}