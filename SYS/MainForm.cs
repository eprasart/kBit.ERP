using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kPrasat.SYS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Text += " v " + SYS.App.version;
            Icon = Properties.Resources.Icon;             

            //var fVendor = new IC.frmuser();
            //fVendor.Show();
            //fVendor.Focus();
        }
    }
}
