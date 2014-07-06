﻿/*TODO: 
 * msg => English and/or Khmer (use both font in rtf to make it render nice)
 * spliterDistance: save in table by user
 * datagridview sort by column header
 * show/search inactive
*/
using System;
using System.Windows.Forms;
using kPrasat.SM;

namespace kPrasat.IC
{
    public partial class frmLocationList : Form
    {
        private long Id = 0;
        private int rowIndex = 0;
        private bool IsExpand = false;
        private bool IsDirty = false;
        private string Module = "Location";   // Log module

        public frmLocationList()
        {
            InitializeComponent();
            MessageBox.Show(Environment.MachineName + "\n" + Environment.UserDomainName + "\n" + Environment.UserName);
        }

        private string GetStatus()
        {
            var status = "";
            if (mnuShowA.Checked && !mnuShowI.Checked)
                status = "A";
            else if (mnuShowI.Checked && !mnuShowA.Checked)
                status = "I";
            return status;
        }

        private void RefreshGrid(long seq = 0)
        {
            if (dgvList.RowCount > 0) rowIndex = dgvList.CurrentRow.Index;

            dgvList.DataSource = LocationFacade.GetDataTable(txtSearch.Text, GetStatus());
            if (dgvList.RowCount > 0)
                if (seq == 0)
                {
                    if (rowIndex >= dgvList.RowCount) rowIndex = dgvList.RowCount - 1;
                    dgvList.CurrentCell = dgvList[1, rowIndex];
                }
                else
                    foreach (DataGridViewRow row in dgvList.Rows)
                        if ((long)row.Cells[0].Value == seq)
                        {
                            Id = (int)seq;
                            dgvList.CurrentCell = dgvList[1, row.Index];
                            break;
                        }
            LoadData();
        }

        private void LockControls(bool l = true)
        {
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).ReadOnly = l;
                else if (c is ComboBox)
                    ((ComboBox)c).Enabled = !l;
                else if (c is DateTimePicker)
                    ((DateTimePicker)c).Enabled = !l;
            }
            btnNew.Enabled = l;
            btnCopy.Enabled = l;
            btnSave.Enabled = !l;
            btnSaveNew.Enabled = !l;
            btnActive.Enabled = l;
            btnDelete.Enabled = l;
            splitContainer1.Panel1.Enabled = l;
            btnUnlock.Text = l ? "Un&lock" : "Cance&l";
        }

        private void SetStatus(string stat)
        {
            if (stat == "A")
            {
                btnActive.Text = "Inactiv&e";
                if (btnActive.Image != Properties.Resources.Inactive)
                    btnActive.Image = Properties.Resources.Inactive;
            }
            else
            {
                btnActive.Text = "Activ&e";
                if (btnActive.Image != Properties.Resources.Active)
                    btnActive.Image = Properties.Resources.Active;
            }
        }

        private bool IsValidated()
        {
            string Code = txtCode.Text.Trim();
            if (Code.Length == 0)
            {
                Common.ShowMsg("Code cannot be empty.", "Save");
                txtCode.Focus();
                return false;
            }
            if (LocationFacade.IsExist(Code, Id))
            {
                Common.ShowMsg("Code already exists. It must be unique.", "Save");
                txtCode.Focus();
                txtCode.SelectAll();
                return false;
            }
            //todo: prevent duplicate
            //todo: pwd matching
            return true;
        }

        private void LoadData()
        {
            var Id = dgvList.Id;
            if (Id == 0) return;
            try
            {
                var m = LocationFacade.Select(Id);
                txtCode.Text = m.Code;
                txtDesc.Text = m.Description;
                txtAddress.Text = m.Address;
                txtName.Text = m.Name;
                txtPhone.Text = m.Phone;
                txtFax.Text = m.Fax;
                txtEmail.Text = m.Email;
                txtNote.Text = m.Note;
                SetStatus(m.Status);
                LockControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading data.\n" + ex.Message, "Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SYS.ErrorLogFacade.Log(ex.ToString(), ex.StackTrace);
            }
        }

        private void frmLocationList_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Icon;
            dgvList.ShowLessColumns(true);
            RefreshGrid();
            Text += " v. " + SYS.App.version;
            LockControls();
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Open, "Form opened");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (IsExpand) picExpand_Click(sender, e);
            txtCode.Text = "";
            txtCode.Focus();
            txtDesc.Text = "";
            txtAddress.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtNote.Text = "";
            if (dgvList.RowCount > 0)
                dgvList.CurrentRow.Selected = false;
            Id = 0;
            LockControls(false);
            if (dgvList.RowCount > 0) rowIndex = dgvList.CurrentRow.Index;
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_New, "New clicked");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidated()) return;
            Cursor = Cursors.WaitCursor;
            var m = new Location();
            var log = new SessionLog { Module = Module };
            m.Id = Id;
            m.Code = txtCode.Text.Trim();
            m.Description = txtDesc.Text;
            m.Address = txtAddress.Text;
            m.Name = txtName.Text;
            m.Phone = txtPhone.Text;
            m.Fax = txtFax.Text;
            m.Email = txtEmail.Text;
            m.Note = txtNote.Text;
            if (m.Id == 0)
            {
                log.Priority = Type.Priority_Information;
                log.Type = Type.Log_Insert;
            }
            else
            {
                log.Priority = Type.Priority_Caution;
                log.Type = Type.Log_Update;
            }
            m.Id = LocationFacade.Save(m);
            if (dgvList.RowCount > 0) rowIndex = dgvList.CurrentRow.Index;
            RefreshGrid(m.Id);
            LockControls();
            Cursor = Cursors.Default;
            log.Message = "Save successfull. Id=" + m.Id + " , Code=" + txtCode.Text;
            SessionLogFacade.Log(log);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                RefreshGrid();
            }
        }

        private void lblClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtSearch.Text = "";
            RefreshGrid();
        }

        private void lblRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            RefreshGrid();
            Cursor = Cursors.Default;
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_SaveAndNew, "Save and new. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
            btnSave_Click(sender, e);
            btnNew_Click(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var Id = dgvList.Id;
            if (Id == 0) return;

            // If locked
            var lInfo = LocationFacade.GetLockInfo(Id);
            string msg = "";
            if (lInfo.IsLocked)
            {
                msg = "Record cannot be deleted because it is currently locked by '" + lInfo.LockBy + "' since '" + lInfo.LockAt + "'";
                new frmMsg(msg).ShowDialog();
                SessionLogFacade.Log(Type.Priority_Caution, Module, Type.Log_Delete, "Cannot delete while currently locked. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
                return;
            }
            // Delete
            msg = "Are you sure you want to delete?";
            if (MessageBox.Show(msg, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;
            LocationFacade.SetStatus(Id, Type.RecordStatus_Deleted);
            RefreshGrid();
            if (dgvList.RowCount == 0) btnNew_Click(sender, e);

            // log
            SessionLogFacade.Log(Type.Priority_Warning, Module, Type.Log_Delete, "User Id=" + dgvList.Id + ", Code=" + txtCode.Text + " has been deleted");
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Id = 0;
            txtCode.Focus();
            LockControls(false);
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Copy, "Copy from Id=" + dgvList.Id + "Code=" + txtCode.Text);
        }

        private void picExpand_Click(object sender, EventArgs e)
        {
            splitContainer1.IsSplitterFixed = !IsExpand;
            if (!IsExpand)
            {
                splitContainer1.SplitterDistance = splitContainer1.Size.Width;
                splitContainer1.FixedPanel = FixedPanel.Panel2;
                imgExpand.Image = Properties.Resources.Back;
            }
            else
            {
                splitContainer1.SplitterDistance = 300; // TODO: load from var or db
                splitContainer1.FixedPanel = FixedPanel.Panel1;
                imgExpand.Image = Properties.Resources.Next;
            }
            dgvList.ShowLessColumns(IsExpand);
            IsExpand = !IsExpand;
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsExpand) picExpand_Click(sender, e);
            dgvList_SelectionChanged(sender, e);    // reload data since SelectionChanged will not occured on current row
        }

        private void frmLocationList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (btnUnlock.Text == "Cance&l")
                btnUnlock_Click(null, null);
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            var Id = dgvList.Id;
            if (Id == 0) return;
            var lInfo = LocationFacade.GetLockInfo(Id);
            if (lInfo.IsLocked)
            {
                string msg = "Account is currently locked by '" + lInfo.LockBy + "' since '" + lInfo.LockAt + "'";
                new frmMsg(msg).ShowDialog();
                return;
            }
            string status = btnActive.Text.StartsWith("I") ? Type.RecordStatus_InActive : Type.RecordStatus_Active;
            LocationFacade.SetStatus(Id, status);
            RefreshGrid();
            SessionLogFacade.Log(Type.Priority_Caution, Module, status == "I" ? Type.Log_Inactive : Type.Log_Active, "Id=" + dgvList.Id + ", Code=" + txtCode.Text);
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (IsExpand) picExpand_Click(sender, e);
            Id = dgvList.Id;
            // Cancel
            if (btnUnlock.Text == "Cance&l")
            {
                if (IsDirty)
                {
                    //todo: reload orginal data (if dirty)

                }
                LockControls(true);
                //dgvList.CurrentCell = dgvList[1, rowIndex];
                LocationFacade.ReleaseLock(dgvList.Id);
                if (dgvList.RowCount > 0 && !dgvList.CurrentRow.Selected)
                    dgvList.CurrentRow.Selected = true;
                SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Unlock, "Cancel lock, Location=" + dgvList.Id);
                return;
            }
            // Unlock
            if (Id == 0) return;
            var lInfo = LocationFacade.GetLockInfo(Id);

            if (lInfo.IsLocked) // Check if record is locked
            {
                string msg = "Account is currently locked by '" + lInfo.LockBy + "' since '" + lInfo.LockAt + "'";
                new frmMsg(msg).ShowDialog();
                return;
            }
            LockControls(false);
            LocationFacade.Lock(dgvList.Id);
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Lock, "Lock, User=" + dgvList.Id);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.N:
                    if (btnNew.Enabled) btnNew_Click(null, null);
                    break;
                case Keys.Control | Keys.Y:
                    if (btnCopy.Enabled) btnCopy_Click(null, null);
                    break;
                case Keys.Control | Keys.L:
                    if (btnUnlock.Enabled) btnUnlock_Click(null, null);
                    break;
                case Keys.Control | Keys.S:
                    if (btnSave.Enabled) btnSave_Click(null, null);
                    break;
                case Keys.Control | Keys.W:
                    if (btnSaveNew.Enabled) btnSaveNew_Click(null, null);
                    break;
                case Keys.Control | Keys.E:
                    if (btnActive.Enabled) btnActive_Click(null, null);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                if (btnDelete.Enabled) btnDelete_Click(null, null);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            lblClear.Enabled = (txtSearch.Text.Length > 0);
        }

        private void mnuShow_CheckedChanged(object sender, EventArgs e)
        {
            if (!mnuShowA.Checked && !mnuShowI.Checked)
                mnuShowA.Checked = true;
            RefreshGrid();
        }

        private void lblFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mnuShow.Show(lblFilter, 0, 15);
        }
    }
}
