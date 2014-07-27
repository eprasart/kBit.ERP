using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kBit.ERP.SYS
{
    public partial class frmMain : Form
    {
        private string Module = "Main Form";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblVersion.Text = " v " + SYS.App.version;
            Icon = Properties.Resources.Icon;

            App.fSplash.ShowMsg("");
            App.fSplash.StartTimer();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            new IC.frmLocationList().Show();
            Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            new SM.frmUserList().Show();
            Cursor = Cursors.Default;
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            if (!SM.Privilege.CanAccess("IC_LOC", "V"))
            {
                MessageBox.Show("You don't have the privilege to access this function.");
                return;
            }
            Cursor = Cursors.WaitCursor;
            if (App.fLocationList == null || App.fLocationList.IsDisposed == true)
            {
                App.fLocationList = new IC.frmLocationList();
                App.fLocationList.Show();
            }
            App.fLocationList.Focus();
            Cursor = Cursors.Default;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (App.fUserList == null || App.fUserList.IsDisposed == true)
            {
                App.fUserList = new SM.frmUserList();
                App.fUserList.Show();
            }
            App.fUserList.Focus();
            Cursor = Cursors.Default;
            SM.SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Open, "User List opened.");
        }

        private void btnAuditLog_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SM.SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Open, "Audit Log opened.");
            if (App.fAuditLog == null || App.fAuditLog.IsDisposed == true)
            {
                App.fAuditLog = new SM.frmAuditLog();
                App.fAuditLog.Show();
            }
            App.fAuditLog.Focus();
            Cursor = Cursors.Default;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.SessionFacade.UpdateLogout(App.session);
            App.SaveSettings(); // Save settings to file
            App.AccessLog.Write(DateTime.Now.ToString("yyy-MM-dd ddd hh:mm:ss tt") + " Application quit. Process Id: " + App.ProcessID);
        }
    }
}
