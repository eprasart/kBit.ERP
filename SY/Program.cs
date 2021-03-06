﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using kBit.ERP.SYS;

namespace kBit
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

            if (!App.Init()) return;
            //App.fSplash.Visible = false;
            //if (new SM.frmLogin().ShowDialog() == DialogResult.OK)
            //{
            //    App.fSplash.Visible = true;
                App.fSplash.ShowMsg("Loading Main Form...");
                Application.Run(new frmMain());
            //}
            //else
            //    Application.Exit();
        }
    }
}