﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kBit.ERP.SYS;
using kBit.ERP.SM;

namespace kBit.ERP.SM
{
    public partial class frmLogin : Form
    {
        private string Module = "Login";

        public frmLogin()
        {
            InitializeComponent();
        }

        private bool IsValidated()
        {
            string sFlag = "";
            string sMsg = "";
            if (txtUsername.Text.Trim().Length == 0)
                sFlag = "U";
            if (txtPassword.Text.Length == 0)
                sFlag += "P";
            switch (sFlag)
            {
                case "U":
                    sMsg = "username";
                    txtUsername.Focus();
                    break;
                case "P":
                    sMsg = "password";
                    txtPassword.Focus();
                    break;
                case "UP":
                    sMsg = "username and password";
                    txtUsername.Focus();
                    break;
            }
            if (sMsg.Length > 0)
            {
                lblMsg.Text = "Please enter " + sMsg;
                return false;
            }
            return true;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Icon;
            lblMsg.Text = "";

            txtUsername.Text = App.setting.Get("Username");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!IsValidated()) return;
            // Check Username
            var usr = UserFacade.Select(txtUsername.Text.Trim());
            if (usr == null)  // Username not exist
            {
                lblMsg.Text = "Invalid username or password";
                txtUsername.Focus();
                SessionLogFacade.Log(Type.Priority_Warning, Module, Type.Log_Login, "Username=" + txtUsername.Text.Trim() + " not exist");
                return;
            }
            // Check password
            if (!UserFacade.IsPwdCorrect(usr.Id, txtPassword.Text))
            {
                lblMsg.Text = "Invalid username or password";
                txtUsername.Focus(); 
                SessionLogFacade.Log(Type.Priority_Warning, Module, Type.Log_Login, "Password not correct");
                return;
            }
            //todo: validate start on, end on, status

            App.session.UserId = usr.Id;
            App.session.Username = usr.Username;
            SessionFacade.Save(App.session);

            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Login,  "Username=" + txtUsername.Text.Trim() + " authenticates OK");
            // Save username
            App.setting.Set("Username", txtUsername.Text.Trim());
            App.setting.Save();

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length > 0)
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            btnLogin_Click(null, null);
        }

        private void lblMsg_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(lblMsg.Text);
            }
            catch { }

        }

    }
}
