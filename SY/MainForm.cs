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
            new IC.frmLocation().Show();
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
            if (!SM.Privilege.CanAccess("ICLOC", "V")) // todo: not hard code
            {
                MessageBox.Show("You don't have the privilege to access this function.");
                return;
            }
            Cursor = Cursors.WaitCursor;
            if (App.fLocation == null || App.fLocation.IsDisposed == true)
            {
                App.fLocation = new IC.frmLocation();
                App.fLocation.Show();
            }
            App.fLocation.Focus();
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
            App.Close();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            if (!SM.Privilege.CanAccess("ICCAT", "V")) // todo: not hard code
            {
                MessageBox.Show("You don't have the privilege to access this function.");
                return;
            }
            Cursor = Cursors.WaitCursor;
            if (App.fCategory == null || App.fCategory.IsDisposed == true)
            {
                App.fCategory = new IC.frmCategory();
                App.fCategory.Show();
            }
            App.fCategory.Focus();
            Cursor = Cursors.Default;
        }

        private void btnUnitMeasure_Click(object sender, EventArgs e)
        {
            if (!SM.Privilege.CanAccess("ICCAT", "V")) // todo: not hard code
            {
                MessageBox.Show("You don't have the privilege to access this function.");
                return;
            }
            Cursor = Cursors.WaitCursor;
            if (App.fUnitMeasure == null || App.fUnitMeasure.IsDisposed == true)
            {
                App.fUnitMeasure = new IC.frmUnitMeasure();
                App.fUnitMeasure.Show();
            }
            App.fUnitMeasure.Focus();
            Cursor = Cursors.Default;
        }

        private void btnLabelMessage_Click(object sender, EventArgs e)
        {

        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            if (!SM.Privilege.CanAccess("ICITM", "V"))
            {
                MessageBox.Show("You don't have the privilege to access this function.");
                return;
            }
            Cursor = Cursors.WaitCursor;
            if (App.fItem == null || App.fItem.IsDisposed == true)
            {
                App.fItem = new IC.frmItem();
                App.fItem.Show();
            }
            App.fItem.Focus();
            Cursor = Cursors.Default;
        }
    }
}
