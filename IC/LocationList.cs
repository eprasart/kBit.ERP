/*TODO: 
 * msg => English and/or Khmer (use Noto font)  
*/

using System;
using System.Windows.Forms;
using kBit.ERP.SM;
using kBit.ERP.SYS;

namespace kBit.ERP.IC
{
    public partial class frmLocationList : Form
    {
        long Id = 0;
        int RowIndex = 0;   // Current gird selected row
        bool IsExpand = false;
        bool IsDirty = false;
        bool IsIgnore = true;

        public frmLocationList()
        {
            InitializeComponent();
        }

        private string GetStatus()
        {
            var status = "";
            if (mnuShowA.Checked && !mnuShowI.Checked)
                status = Type.RecordStatus_Active;
            else if (mnuShowI.Checked && !mnuShowA.Checked)
                status = Type.RecordStatus_InActive;
            return status;
        }

        private void RefreshGrid(long seq = 0)
        {
            Cursor = Cursors.WaitCursor;
            //IsIgnore = true;
            if (dgvList.SelectedRows.Count > 0) RowIndex = dgvList.SelectedRows[0].Index;
            try
            {
                dgvList.DataSource = LocationFacade.GetDataTable(txtFind.Text, GetStatus());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while retrieving data to the data grid.\n" + ex.Message, "Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogFacade.Log(ex);
                return;
            }
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
            btnUnlock.Text = l ? LabelFacade.sy_button_unlock : LabelFacade.sy_button_cancel;
            txtFind.ReadOnly = !l;
            btnFind.Enabled = l;
            btnClear.Enabled = l;
            btnFilter.Enabled = l;
        }

        private void SetStatus(string stat)
        {
            if (stat == Type.RecordStatus_Active)
            {
                if (btnActive.Text == LabelFacade.sy_button_inactive) return;
                btnActive.Text = LabelFacade.sy_button_inactive;
                if (btnActive.Image != Properties.Resources.Inactive)
                    btnActive.Image = Properties.Resources.Inactive;
            }
            else
            {
                if (btnActive.Text == LabelFacade.sy_button_active) return;
                btnActive.Text = LabelFacade.sy_button_active;
                if (btnActive.Image != Properties.Resources.Active)
                    btnActive.Image = Properties.Resources.Active;
            }
        }

        private bool IsValidated()
        {
            //todo: show all error in just a message box with scroll down            
            string Code = txtCode.Text.Trim();
            if (Code.Length == 0)
            {
                MessageBox.Show("Code cannot be empty.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCode.Focus();
                return false;
            }
            if (LocationFacade.Exists(Code, Id))
            {
                MessageBox.Show("Code already exists. Enter a unique code.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCode.Focus();
                txtCode.SelectAll();
                return false;
            }
            if (txtEmail.Text.Length > 0 && !Util.IsEmailValid(txtEmail.Text))
            {
                MessageBox.Show("Email address is not valid.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
                txtEmail.SelectAll();
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
                    SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_View, "View. Id=" + m.Id + ", Code=" + m.Code);
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

        private void SetIconDisplayType(string type)
        {
            ToolStripItemDisplayStyle ds;
            switch (type)
            {
                case "I":
                    ds = ToolStripItemDisplayStyle.Image;
                    break;
                case "T":
                    ds = ToolStripItemDisplayStyle.Text;
                    break;
                default:
                    ds = ToolStripItemDisplayStyle.ImageAndText;
                    break;
            }
            if (ds == ToolStripItemDisplayStyle.ImageAndText) return;   // If IT=ImageAndText, then do nothing (the designer already take care this)
            foreach (var c in toolStrip1.Items)
            {
                if (c is ToolStripButton)
                    ((ToolStripButton)c).DisplayStyle = ds;
            }
        }

        private void SetCodeCasing()
        {
            CharacterCasing cs;
            switch (ConfigFacade.sy_code_casing)
            {
                case "U":
                    cs = CharacterCasing.Upper;
                    break;
                case "L":
                    cs = CharacterCasing.Lower;
                    break;
                default:
                    cs = CharacterCasing.Normal;
                    break;
            }
            txtCode.CharacterCasing = cs;
        }

        private void SetSettings()
        {
            try
            {
                SetIconDisplayType(ConfigFacade.sy_toolbar_icon_display_type);
                splitContainer1.SplitterDistance = ConfigFacade.ic_location_splitter_distance;

                SetCodeCasing();
                var lo = ConfigFacade.ic_location_location;
                if (lo != new System.Drawing.Point(-1, -1))
                    Location = lo;
                var si = ConfigFacade.ic_location_size;
                if (si != new System.Drawing.Size(-1, -1))
                    Size = si;
                WindowState = (FormWindowState)ConfigFacade.ic_location_window_state;
            }
            catch (Exception ex)
            {
                ErrorLogFacade.Log(ex, "Set settings");
            }
        }

        private void SetLabels()
        {
            var funCode = "icloc";
            var lang = ConfigFacade.sy_language;

            btnNew.Text = LabelFacade.sy_button_new;
            btnCopy.Text = LabelFacade.sy_button_copy;
            btnUnlock.Text = LabelFacade.sy_button_unlock;
            btnSave.Text = LabelFacade.sy_button_save;
            btnSaveNew.Text = LabelFacade.sy_button_save_new;
            btnActive.Text = LabelFacade.sy_button_inactive;
            btnDelete.Text = LabelFacade.sy_button_delete;
            btnMode.Text = LabelFacade.sy_button_mode;
            btnExport.Text = LabelFacade.sy_button_export;
            btnFind.Text = "     " + LabelFacade.sy_button_find;
            btnClear.Text = "     " + LabelFacade.sy_button_clear;
            btnFilter.Text = "     " + LabelFacade.sy_button_filter;

            lblCode.Text = LabelFacade.GetLabel(funCode, lang, "code");
            colCode.HeaderText = lblCode.Text;
            lblDescription.Text = LabelFacade.GetLabel(funCode, lang, "description");
            colDescription.HeaderText = lblDescription.Text;
            lblAddress.Text = LabelFacade.GetLabel(funCode, lang, "address");
            colAddress.HeaderText = lblAddress.Text;
            lblName.Text = LabelFacade.GetLabel(funCode, lang, "name");
            colName.HeaderText = lblName.Text;
            lblPhone.Text = LabelFacade.GetLabel(funCode, lang, "phone");
            colPhone.HeaderText = lblPhone.Text;
            lblFax.Text = LabelFacade.GetLabel(funCode, lang, "fax");
            colFax.HeaderText = lblFax.Text;
            lblEmail.Text = LabelFacade.GetLabel(funCode, lang, "email");
            colEmail.HeaderText = lblEmail.Text;
            glbLocation.Caption = LabelFacade.GetLabel(funCode, lang, "location");
            glbContact.Caption = LabelFacade.GetLabel(funCode, lang, "contact");
            glbNote.Caption = LabelFacade.GetLabel(funCode, lang, "note");

        }

        private void frmLocationList_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Icon;
            dgvList.ShowLessColumns(true);
            SetSettings();
            SetLabels();
            SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_Open, "Opened");
            RefreshGrid();
            LoadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!Privilege.CanAccess(Type.Function_IC_Location, Type.Privilege_New))
            {
                MessageBox.Show("You don't have the privilege to perform this command.", "New", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SessionLogFacade.Log(Type.Priority_Caution, Type.Module_IC_Location, Type.Log_NoAccess, "New: No access");
                return;
            }
            if (IsExpand) picExpand_Click(sender, e);
            ClearAllBoxes();
            if (dgvList.CurrentRow != null)
                dgvList.CurrentRow.Selected = false;
            Id = 0;
            LockControls(false);

            if (dgvList.CurrentRow != null) RowIndex = dgvList.CurrentRow.Index;
            SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_New, "New clicked");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidated()) return;
            Cursor = Cursors.WaitCursor;
            var m = new Location();
            var log = new SessionLog { Module = Type.Module_IC_Location };
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
            try
            {
                m.Id = LocationFacade.Save(m);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving data.\n" + ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogFacade.Log(ex);
            }
            if (dgvList.RowCount > 0) RowIndex = dgvList.CurrentRow.Index;
            RefreshGrid(m.Id);
            LockControls();
            Cursor = Cursors.Default;
            log.Message = "Saved. Id=" + m.Id + ", Code=" + txtCode.Text;
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
            SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_SaveAndNew, "Saved and new. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
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
            var lInfo = LocationFacade.GetLock(Id);
            string msg = "";
            if (lInfo.Locked)
            {
                msg = "Record cannot be deleted because it is currently locked by '" + lInfo.Lock_By + "' since '" + lInfo.Lock_At + "'";
                if (!Privilege.CanAccess(Type.Function_IC_Location, "O"))
                {
                    MessageBox.Show(msg, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SessionLogFacade.Log(Type.Priority_Caution, Type.Module_IC_Location, Type.Log_Delete, "Cannot delete. Currently locked. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
                    return;
                }
            }
            // Delete            
            msg = "ច្បាស់ទេ​ថា Are you sure you want to delete?";
            if (lInfo.Locked) msg = "Record is currently locked by '" + lInfo.Lock_By + "' since '" + lInfo.Lock_At + "'\n" + msg;
            if (MessageBox.Show(msg, "លប់ Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;
            try
            {
                LocationFacade.SetStatus(Id, Type.RecordStatus_Deleted);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting.\n" + ex.Message, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogFacade.Log(ex);
            }
            RefreshGrid();
            // log
            SessionLogFacade.Log(Type.Priority_Warning, Type.Module_IC_Location, Type.Log_Delete, "Deleted. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!Privilege.CanAccess(Type.Function_IC_Location, Type.Privilege_New))
            {
                MessageBox.Show("You don't have the privilege for perform this command.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_NoAccess, "Copy: No access");
                return;
            }
            Id = 0;
            if (IsExpand) picExpand_Click(sender, e);
            txtCode.Focus();
            LockControls(false);
            SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_Copy, "Copy from Id=" + dgvList.Id + "Code=" + txtCode.Text);
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
                splitContainer1.SplitterDistance = ConfigFacade.ic_location_splitter_distance;
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

            string status = btnActive.Text == LabelFacade.sy_button_inactive ? Type.RecordStatus_InActive : Type.RecordStatus_Active;
            // If referenced
            //todo: check if already used in ic_item

            //If locked
            var lInfo = LocationFacade.GetLock(Id);
            if (lInfo.Locked)
            {
                string msg = "Record is currently locked by '" + lInfo.Lock_By + "' since '" + lInfo.Lock_At + "'";
                if (!Privilege.CanAccess(Type.Function_IC_Location, "O"))
                {
                    MessageBox.Show(msg, "Active/Inactive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                    if (MessageBox.Show(msg + "\nAre you sure you want to proceed?", "Active/Inactive", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        return;
            }
            try
            {
                LocationFacade.SetStatus(Id, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while active/inactive." + ex.Message, "Active/Inactive", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogFacade.Log(ex);
            }
            RefreshGrid();
            SessionLogFacade.Log(Type.Priority_Caution, Type.Module_IC_Location, status == Type.RecordStatus_InActive ? Type.Log_Inactive : Type.Log_Active, "Id=" + dgvList.Id + ", Code=" + txtCode.Text);
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (!Privilege.CanAccess(Type.Function_IC_Location, Type.Privilege_Update))
            {
                MessageBox.Show("You don't have the privilege for perform this command.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_NoAccess, "Copy: No access");
                return;
            }
            if (IsExpand) picExpand_Click(sender, e);
            Id = dgvList.Id;
            // Cancel
            if (btnUnlock.Text == LabelFacade.sy_button_cancel)
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
                try
                {
                    LocationFacade.ReleaseLock(dgvList.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while unlocking record." + ex.Message, "Unlock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLogFacade.Log(ex);
                }
                if (dgvList.CurrentRow != null && !dgvList.CurrentRow.Selected)
                    dgvList.CurrentRow.Selected = true;
                SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_Unlock, "Unlock cancel. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
                btnUnlock.ToolTipText = "Unlock (Ctrl+L)";
                IsDirty = false;
                return;
            }
            // Unlock
            if (Id == 0) return;
            var lInfo = LocationFacade.GetLock(Id);

            if (lInfo.Locked) // Check if record is locked
            {
                string msg = "Record is currently locked by '" + lInfo.Lock_By + "' since '" + lInfo.Lock_At + "'.";
                if (!Privilege.CanAccess(Type.Function_IC_Location, "O"))
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
            try
            {
                LocationFacade.Lock(dgvList.Id, txtCode.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while locking record." + ex.Message, "Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogFacade.Log(ex);
            }
            SessionLogFacade.Log(Type.Priority_Information, Type.Module_IC_Location, Type.Log_Lock, "Locked. Id=" + dgvList.Id + ", Code=" + txtCode.Text);
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

            // Set config values
            if (!IsExpand)
                ConfigFacade.ic_location_splitter_distance = splitContainer1.SplitterDistance;
            ConfigFacade.ic_location_location = Location;
            ConfigFacade.ic_location_window_state = (int)WindowState;
            if (WindowState == FormWindowState.Normal) ConfigFacade.ic_location_size = Size;
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            // Check if entered code already exists
            if (txtCode.ReadOnly) return;
            if (LocationFacade.Exists(txtCode.Text.Trim()))
            {
                MessageBox.Show("'" + txtCode.Text.Trim() + "' already exists. Enter a unique code.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            splitContainer1.IsSplitterFixed = !IsExpand;
            if (!IsExpand)
            {
                ConfigFacade.ic_location_splitter_distance = splitContainer1.SplitterDistance;
                splitContainer1.SplitterDistance = splitContainer1.Size.Width;
                splitContainer1.FixedPanel = FixedPanel.Panel2;
            }
            else
            {
                splitContainer1.SplitterDistance = ConfigFacade.ic_location_splitter_distance;
                splitContainer1.FixedPanel = FixedPanel.Panel1;
            }
            dgvList.ShowLessColumns(IsExpand);
            IsExpand = !IsExpand;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            mnuShow.Show(btnFilter, 0, 27);
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