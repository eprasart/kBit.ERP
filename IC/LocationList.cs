/*TODO: 
 * msg => English and/or Khmer (use both font in rtf to make it render nice)
 * spliterDistance: save in table by user
*/

using System;
using System.Windows.Forms;
using kBit.ERP.SM;

namespace kBit.ERP.IC
{
    public partial class frmLocationList : Form
    {
        private long Id = 0;
        private int RowIndex = 0;   // Current gird selected row
        private bool IsExpand = false;
        private bool IsDirty = false;
        private bool IsIgnore = true;
        private const string Module = "Location";   // Log module
        public static string Function = "IC_LOC";

        public frmLocationList()
        {
            InitializeComponent();
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
            Cursor = Cursors.WaitCursor;
            //IsIgnore = true;
            if (dgvList.SelectedRows.Count > 0) RowIndex = dgvList.SelectedRows[0].Index;
            dgvList.DataSource = LocationFacade.GetDataTable(txtFind.Text, GetStatus());
            if (dgvList.RowCount > 0)
            {
                if (seq == 0)
                {
                    if (RowIndex >= dgvList.RowCount) RowIndex = dgvList.RowCount - 1;
                    dgvList.CurrentCell = dgvList[1, RowIndex];
                }
                else
                    foreach (DataGridViewRow row in dgvList.Rows)
                        if ((long)row.Cells[0].Value == seq)
                        {
                            Id = (int)seq;
                            dgvList.CurrentCell = dgvList[1, row.Index];
                            break;
                        }
            }
            else
            {
                btnCopy.Enabled = false;
                btnUnlock.Enabled = false;
                btnActive.Enabled = false;
                btnDelete.Enabled = false;
                ClearAllBoxes();
            }
            IsIgnore = false;
            //LoadData();
            Cursor = Cursors.Default;
        }

        private void LockControls(bool l = true)
        {
            if (Id != 0 && l == false)
                txtCode.ReadOnly = true;
            else
                txtCode.ReadOnly = l;
            txtDesc.ReadOnly = l;
            txtAddress.ReadOnly = l;
            txtName.ReadOnly = l;
            txtPhone.ReadOnly = l;
            txtFax.ReadOnly = l;
            txtEmail.ReadOnly = l;
            txtNote.ReadOnly = l;

            btnNew.Enabled = l;
            btnCopy.Enabled = dgvList.Id != 0 && l;
            btnSave.Enabled = !l;
            btnSaveNew.Enabled = !l;
            btnActive.Enabled = dgvList.Id != 0 && l;
            btnDelete.Enabled = dgvList.Id != 0 && l;
            splitContainer1.Panel1.Enabled = l;
            btnUnlock.Enabled = !l || dgvList.RowCount > 0;
            btnUnlock.Text = l ? "Un&lock" : "Cance&l";
            txtFind.ReadOnly = !l;
            btnFind.Enabled = l;
            btnClear.Enabled = l;
            btnFilter.Enabled = l;
        }

        private void SetStatus(string stat)
        {
            if (stat == "A")
            {
                if (btnActive.Text.StartsWith("I")) return;
                btnActive.Text = "Inactiv&e";
                if (btnActive.Image != Properties.Resources.Inactive)
                    btnActive.Image = Properties.Resources.Inactive;
            }
            else
            {
                if (btnActive.Text.StartsWith("A")) return;
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
                MessageBox.Show("Code cannot be empty.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCode.Focus();
                return false;
            }
            if (LocationFacade.IsExist(Code, Id))
            {
                MessageBox.Show("Code already exists. Enter a unique code.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCode.Focus();
                txtCode.SelectAll();
                return false;
            }
            return true;
        }

        private void ClearAllBoxes()
        {
            txtCode.Text = "";
            txtCode.Focus();
            txtDesc.Text = "";
            txtAddress.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtNote.Text = "";
            IsDirty = false;
        }

        private void LoadData()
        {
            var Id = dgvList.Id;
            if (Id != 0)
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
                    IsDirty = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while loading data.\n" + ex.Message, "Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SYS.ErrorLogFacade.Log(ex);
                }
            else    // when delete all => disable buttons and clear all controls
            {
                if (dgvList.RowCount == 0)
                {
                    btnUnlock.Enabled = false;
                    ClearAllBoxes();
                }
            }
        }

        private void frmLocationList_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Icon;
            dgvList.ShowLessColumns(true);
            RefreshGrid();
            LoadData();
            //LockControls();
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Open, "Opened");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!Privilege.CanAccess(Function, "N"))
            {
                MessageBox.Show("You don't have the privilege to perform this command.", "New", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SessionLogFacade.Log(Type.Priority_Caution, Module, Type.Log_NoAccess, "New: No access");
                return;
            }
            if (IsExpand) picExpand_Click(sender, e);
            ClearAllBoxes();
            if (dgvList.CurrentRow != null)
                dgvList.CurrentRow.Selected = false;
            Id = 0;
            LockControls(false);

            if (dgvList.CurrentRow != null) RowIndex = dgvList.CurrentRow.Index;
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
            if (dgvList.RowCount > 0) RowIndex = dgvList.CurrentRow.Index;
            RefreshGrid(m.Id);
            LockControls();
            Cursor = Cursors.Default;
            log.Message = "Saved. Id=" + m.Id + " , Code=" + txtCode.Text;
            SessionLogFacade.Log(log);
            IsDirty = false;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnFind_Click(null, null);
            }
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if (IsIgnore) return;
            LoadData();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_SaveAndNew, "Saved and new. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
            btnSave_Click(sender, e);
            if (btnSaveNew.Enabled) return;
            btnNew_Click(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var Id = dgvList.Id;
            if (Id == 0) return;
            // If referenced
            //todo: check if exist in ic_item
            // If locked
            var lInfo = LocationFacade.GetLockInfo(Id);
            string msg = "";
            if (lInfo.IsLocked)
            {
                msg = "Record cannot be deleted because it is currently locked by '" + lInfo.LockBy + "' since '" + lInfo.LockAt + "'";
                if (!Privilege.CanAccess(Function, "O"))
                {
                    MessageBox.Show(msg, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SessionLogFacade.Log(Type.Priority_Caution, Module, Type.Log_Delete, "Cannot delete. Currently locked. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
                    return;
                }
            }
            // Delete            
            msg = "Are you sure you want to delete?";
            if (lInfo.IsLocked) msg = "Record is currently locked by '" + lInfo.LockBy + "' since '" + lInfo.LockAt + "'\n" + msg;
            if (MessageBox.Show(msg, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;
            LocationFacade.SetStatus(Id, Type.RecordStatus_Deleted);
            RefreshGrid();

            // log
            SessionLogFacade.Log(Type.Priority_Warning, Module, Type.Log_Delete, "Deleted. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!Privilege.CanAccess(Function, "N"))
            {
                MessageBox.Show("You don't have the privilege for perform this command.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_NoAccess, "Copy: No access");
                return;
            }
            Id = 0;
            if (IsExpand) picExpand_Click(sender, e);
            txtCode.Focus();
            LockControls(false);
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Copy, "Copy from Id=" + dgvList.Id + "Code=" + txtCode.Text);
            IsDirty = false;
        }

        private void picExpand_Click(object sender, EventArgs e)
        {
            splitContainer1.IsSplitterFixed = !IsExpand;
            if (!IsExpand)
            {
                splitContainer1.SplitterDistance = splitContainer1.Size.Width;
                splitContainer1.FixedPanel = FixedPanel.Panel2;
            }
            else
            {
                splitContainer1.SplitterDistance = 228; // TODO: load from var or db
                splitContainer1.FixedPanel = FixedPanel.Panel1;
            }
            dgvList.ShowLessColumns(IsExpand);
            IsExpand = !IsExpand;
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsExpand) picExpand_Click(sender, e);
            dgvList_SelectionChanged(sender, e);    // reload data since SelectionChanged will not occured on current row
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            var Id = dgvList.Id;
            if (Id == 0) return;

            string status = btnActive.Text.StartsWith("I") ? Type.RecordStatus_InActive : Type.RecordStatus_Active;
            // If referenced
            //todo: check if already used in ic_item

            // If locked
            var lInfo = LocationFacade.GetLockInfo(Id);
            if (lInfo.IsLocked)
            {
                string msg = "Record is currently locked by '" + lInfo.LockBy + "' since '" + lInfo.LockAt + "'";
                if (!Privilege.CanAccess(Function, "O"))
                {
                    MessageBox.Show(msg, "Active/Inactive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                    if (MessageBox.Show(msg + "\nAre you sure you want to proceed?", "Active/Inactive", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        return;
            }

            LocationFacade.SetStatus(Id, status);
            RefreshGrid();
            SessionLogFacade.Log(Type.Priority_Caution, Module, status == "I" ? Type.Log_Inactive : Type.Log_Active, "Id=" + dgvList.Id + ", Code=" + txtCode.Text);
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            //todo: previlege
            if (IsExpand) picExpand_Click(sender, e);
            Id = dgvList.Id;
            // Cancel
            if (btnUnlock.Text == "Cance&l")
            {
                if (IsDirty)
                {
                    var result = MessageBox.Show("Do you want to save changes?", "Cancel", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes) // Save then close
                        btnSave_Click(null, null);
                    else if (result == System.Windows.Forms.DialogResult.No)
                        LoadData(); // Load original back if changes (dirty)
                    else if (result == System.Windows.Forms.DialogResult.Cancel)
                        return;
                }
                LockControls(true);
                dgvList.Focus();
                LocationFacade.ReleaseLock(dgvList.Id);
                if (dgvList.CurrentRow != null && !dgvList.CurrentRow.Selected)
                    dgvList.CurrentRow.Selected = true;
                SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Unlock, "Unlock cancel. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
                btnUnlock.ToolTipText = "Unlock (Ctrl+L)";
                IsDirty = false;
                return;
            }
            // Unlock
            if (Id == 0) return;
            var lInfo = LocationFacade.GetLockInfo(Id);

            if (lInfo.IsLocked) // Check if record is locked
            {
                string msg = "Record is currently locked by '" + lInfo.LockBy + "' since '" + lInfo.LockAt + "'";
                if (!Privilege.CanAccess(Function, "O"))
                {
                    MessageBox.Show(msg, "Unlock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                    if (MessageBox.Show(msg + "\nDo you want to override?", "Unlock", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        return;
            }
            txtDesc.SelectionStart = txtDesc.Text.Length;
            txtDesc.Focus();
            LockControls(false);
            LocationFacade.Lock(dgvList.Id);
            SessionLogFacade.Log(Type.Priority_Information, Module, Type.Log_Lock, "Locked. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
            btnUnlock.ToolTipText = "Cancel (Esc or Ctrl+L)";
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
                case Keys.Escape:
                    if (btnUnlock.Text.StartsWith("C")) btnUnlock_Click(null, null);    // Cancel
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
                case Keys.Control | Keys.F:
                    if (!txtFind.ReadOnly) txtFind.Focus();
                    break;
                case Keys.F3:
                case Keys.F5:
                    if (btnFind.Enabled) btnFind_Click(null, null);
                    break;
                case Keys.F4:
                    if (btnClear.Enabled) btnClear_Click(null, null);
                    break;
                case Keys.F8:
                    if (btnFilter.Enabled) btnFilter_Click(null, null);
                    break;
                case Keys.F9:
                    if (btnMode.Enabled) btnMode_Click(null, null);
                    break;
                case Keys.F12:
                    if (btnExport.Enabled) btnExport_Click(null, null);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text.Length == 0) btnFind_Click(null, null);
        }

        private void mnuShow_CheckedChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (!mnuShowA.Checked && !mnuShowI.Checked)
                mnuShowA.Checked = true;
            RefreshGrid();
            Cursor = Cursors.Default;
        }

        private void Dirty_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void frmLocationList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDirty)
            {
                switch (MessageBox.Show("Do you want to save changes?", "Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes: // Save then close
                        btnSave_Click(null, null);
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            if (e.Cancel) return;
            IsDirty = false;
            if (btnUnlock.Text == "Cance&l")
                btnUnlock_Click(null, null);
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            // Check if entered code already exists
            if (txtCode.ReadOnly) return;
            if (LocationFacade.IsExist(txtCode.Text.Trim()))
            {
                MessageBox.Show("'" + txtCode.Text.Trim() + "' already exists. Enter a unique code.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            splitContainer1.IsSplitterFixed = !IsExpand;
            if (!IsExpand)
            {
                splitContainer1.SplitterDistance = splitContainer1.Size.Width;
                splitContainer1.FixedPanel = FixedPanel.Panel2;
            }
            else
            {
                splitContainer1.SplitterDistance = 228; // TODO: load from var or db
                splitContainer1.FixedPanel = FixedPanel.Panel1;
            }
            dgvList.ShowLessColumns(IsExpand);
            IsExpand = !IsExpand;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            mnuShow.Show(toolStrip1, 784, 27);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            RefreshGrid();
            txtFind.Focus();
            Cursor = Cursors.Default;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            txtFind.Focus();
        }

        private void dgvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            if (btnDelete.Enabled) btnDelete_Click(null, null);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            LocationFacade.Export();
            Cursor = Cursors.Default;
        }
    }
}