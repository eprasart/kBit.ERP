namespace kPrasat.IC
{
    partial class frmLocationList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocationList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnUnlock = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnActive = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvList = new kPrasat.UI.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new kPrasat.UI.TextBox(this.components);
            this.imgExpand = new System.Windows.Forms.PictureBox();
            this.lblClear = new System.Windows.Forms.LinkLabel();
            this.lblFilter = new System.Windows.Forms.LinkLabel();
            this.lblRefresh = new System.Windows.Forms.LinkLabel();
            this.mnuShow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowA = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowI = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupLabel2 = new kPrasat.UI.GroupLabel();
            this.groupLabel5 = new kPrasat.UI.GroupLabel();
            this.groupLabel1 = new kPrasat.UI.GroupLabel();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.groupLabel4 = new kPrasat.UI.GroupLabel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.groupLabel3 = new kPrasat.UI.GroupLabel();
            this.txtDescKH = new kPrasat.UI.TextBox(this.components);
            this.textBox3 = new kPrasat.UI.TextBox(this.components);
            this.textBox2 = new kPrasat.UI.TextBox(this.components);
            this.textBox5 = new kPrasat.UI.TextBox(this.components);
            this.textBox1 = new kPrasat.UI.TextBox(this.components);
            this.txtCode = new kPrasat.UI.TextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox4 = new kPrasat.UI.TextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescEN = new kPrasat.UI.TextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgExpand)).BeginInit();
            this.mnuShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnCopy,
            this.btnUnlock,
            this.btnSave,
            this.btnSaveNew,
            this.toolStripSeparator,
            this.btnActive,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(983, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(51, 22);
            this.btnNew.Text = "&New";
            this.btnNew.ToolTipText = "New (Ctrl+N)";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Image = global::kPrasat.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(55, 22);
            this.btnCopy.Text = "&Copy";
            this.btnCopy.ToolTipText = "Copy (Ctrl+Y)";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Image = ((System.Drawing.Image)(resources.GetObject("btnUnlock.Image")));
            this.btnUnlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(64, 22);
            this.btnUnlock.Text = "Unl&ock";
            this.btnUnlock.ToolTipText = "Unlock (Ctrl+L)";
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::kPrasat.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "&Save";
            this.btnSave.ToolTipText = "Save (Ctrl+S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Image = global::kPrasat.Properties.Resources.SaveNew;
            this.btnSaveNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(101, 22);
            this.btnSaveNew.Text = "Save and Ne&w";
            this.btnSaveNew.ToolTipText = "Save and New (Ctrl+W)";
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnActive
            // 
            this.btnActive.Image = ((System.Drawing.Image)(resources.GetObject("btnActive.Image")));
            this.btnActive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(68, 22);
            this.btnActive.Text = "Inactiv&e";
            this.btnActive.ToolTipText = "Inactive (Ctrl+E)";
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete (Del)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvList);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.groupLabel2);
            this.splitContainer1.Panel2.Controls.Add(this.groupLabel5);
            this.splitContainer1.Panel2.Controls.Add(this.groupLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox6);
            this.splitContainer1.Panel2.Controls.Add(this.txtNote);
            this.splitContainer1.Panel2.Controls.Add(this.groupLabel4);
            this.splitContainer1.Panel2.Controls.Add(this.txtAddress);
            this.splitContainer1.Panel2.Controls.Add(this.groupLabel3);
            this.splitContainer1.Panel2.Controls.Add(this.txtDescKH);
            this.splitContainer1.Panel2.Controls.Add(this.textBox3);
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Panel2.Controls.Add(this.textBox5);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.txtCode);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.textBox4);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.txtDescEN);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(983, 499);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Honeydew;
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.Column1 = "colCode";
            this.dgvList.Column2 = "colDesc1";
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.colCode,
            this.colDesc1,
            this.Column4,
            this.Column7});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvList.Location = new System.Drawing.Point(3, 56);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(296, 436);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.SelectionChanged += new System.EventHandler(this.dgvList_SelectionChanged);
            this.dgvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvList_KeyDown);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Id";
            this.Column1.HeaderText = "Id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // colCode
            // 
            this.colCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCode.DataPropertyName = "code";
            this.colCode.HeaderText = "Code";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            this.colCode.Width = 57;
            // 
            // colDesc1
            // 
            this.colDesc1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDesc1.DataPropertyName = "Desc1";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Khmer OS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDesc1.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDesc1.HeaderText = "Description 1";
            this.colDesc1.Name = "colDesc1";
            this.colDesc1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Desc2";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Khmer OS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column4.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column4.HeaderText = "Description 2";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.DataPropertyName = "Address";
            this.Column7.HeaderText = "Address";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.imgExpand);
            this.panel1.Controls.Add(this.lblClear);
            this.panel1.Controls.Add(this.lblFilter);
            this.panel1.Controls.Add(this.lblRefresh);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(303, 52);
            this.panel1.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Khmer OS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(3, 21);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(296, 30);
            this.txtSearch.TabIndex = 38;
            this.txtSearch.TabOnEnter = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // imgExpand
            // 
            this.imgExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgExpand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgExpand.Image = ((System.Drawing.Image)(resources.GetObject("imgExpand.Image")));
            this.imgExpand.Location = new System.Drawing.Point(282, 4);
            this.imgExpand.Name = "imgExpand";
            this.imgExpand.Size = new System.Drawing.Size(16, 16);
            this.imgExpand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgExpand.TabIndex = 5;
            this.imgExpand.TabStop = false;
            this.imgExpand.Click += new System.EventHandler(this.picExpand_Click);
            // 
            // lblClear
            // 
            this.lblClear.ActiveLinkColor = System.Drawing.Color.Navy;
            this.lblClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClear.AutoSize = true;
            this.lblClear.Enabled = false;
            this.lblClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClear.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblClear.LinkColor = System.Drawing.Color.Navy;
            this.lblClear.Location = new System.Drawing.Point(209, 6);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(32, 13);
            this.lblClear.TabIndex = 2;
            this.lblClear.TabStop = true;
            this.lblClear.Text = "Clear";
            this.lblClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClear_LinkClicked);
            // 
            // lblFilter
            // 
            this.lblFilter.ActiveLinkColor = System.Drawing.Color.Navy;
            this.lblFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilter.AutoSize = true;
            this.lblFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFilter.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblFilter.LinkColor = System.Drawing.Color.Navy;
            this.lblFilter.Location = new System.Drawing.Point(247, 6);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(31, 13);
            this.lblFilter.TabIndex = 1;
            this.lblFilter.TabStop = true;
            this.lblFilter.Text = "Filter";
            this.lblFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblRefresh
            // 
            this.lblRefresh.ActiveLinkColor = System.Drawing.Color.Navy;
            this.lblRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRefresh.AutoSize = true;
            this.lblRefresh.ContextMenuStrip = this.mnuShow;
            this.lblRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblRefresh.LinkColor = System.Drawing.Color.Navy;
            this.lblRefresh.Location = new System.Drawing.Point(158, 6);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(45, 13);
            this.lblRefresh.TabIndex = 1;
            this.lblRefresh.TabStop = true;
            this.lblRefresh.Text = "Refresh";
            this.lblRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblRefresh_LinkClicked);
            // 
            // mnuShow
            // 
            this.mnuShow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowA,
            this.mnuShowI});
            this.mnuShow.Name = "contextMenuStrip1";
            this.mnuShow.Size = new System.Drawing.Size(148, 48);
            this.mnuShow.Opening += new System.ComponentModel.CancelEventHandler(this.mnuShow_Opening);
            // 
            // mnuShowA
            // 
            this.mnuShowA.Checked = true;
            this.mnuShowA.CheckOnClick = true;
            this.mnuShowA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowA.Name = "mnuShowA";
            this.mnuShowA.Size = new System.Drawing.Size(147, 22);
            this.mnuShowA.Text = "Show Active";
            this.mnuShowA.CheckedChanged += new System.EventHandler(this.mnuShow_CheckedChanged);
            this.mnuShowA.Click += new System.EventHandler(this.mnuShowA_Click);
            // 
            // mnuShowI
            // 
            this.mnuShowI.CheckOnClick = true;
            this.mnuShowI.Name = "mnuShowI";
            this.mnuShowI.Size = new System.Drawing.Size(147, 22);
            this.mnuShowI.Text = "Show Inactive";
            this.mnuShowI.CheckedChanged += new System.EventHandler(this.mnuShow_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Search";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(62, 314);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Code";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Code";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Tomato;
            this.label10.Location = new System.Drawing.Point(50, 314);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 18);
            this.label10.TabIndex = 37;
            this.label10.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(40, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 18);
            this.label5.TabIndex = 37;
            this.label5.Text = "*";
            // 
            // groupLabel2
            // 
            this.groupLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLabel2.Location = new System.Drawing.Point(360, 8);
            this.groupLabel2.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.groupLabel2.Name = "groupLabel2";
            this.groupLabel2.Size = new System.Drawing.Size(260, 16);
            this.groupLabel2.TabIndex = 8;
            this.groupLabel2.TabStop = false;
            // 
            // groupLabel5
            // 
            this.groupLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLabel5.Location = new System.Drawing.Point(19, 286);
            this.groupLabel5.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.groupLabel5.Name = "groupLabel5";
            this.groupLabel5.Size = new System.Drawing.Size(281, 16);
            this.groupLabel5.TabIndex = 8;
            this.groupLabel5.TabStop = false;
            // 
            // groupLabel1
            // 
            this.groupLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLabel1.Location = new System.Drawing.Point(9, 8);
            this.groupLabel1.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.groupLabel1.Name = "groupLabel1";
            this.groupLabel1.Size = new System.Drawing.Size(281, 16);
            this.groupLabel1.TabIndex = 8;
            this.groupLabel1.TabStop = false;
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Khmer OS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(24, 390);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(606, 64);
            this.textBox6.TabIndex = 7;
            // 
            // txtNote
            // 
            this.txtNote.ContextMenuStrip = this.mnuShow;
            this.txtNote.Font = new System.Drawing.Font("Khmer OS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(14, 166);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(606, 64);
            this.txtNote.TabIndex = 7;
            // 
            // groupLabel4
            // 
            this.groupLabel4.Font = new System.Drawing.Font("Khmer OS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLabel4.Location = new System.Drawing.Point(19, 368);
            this.groupLabel4.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.groupLabel4.Name = "groupLabel4";
            this.groupLabel4.Size = new System.Drawing.Size(611, 16);
            this.groupLabel4.TabIndex = 4;
            this.groupLabel4.TabStop = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(90, 84);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 48);
            this.txtAddress.TabIndex = 6;
            // 
            // groupLabel3
            // 
            this.groupLabel3.Font = new System.Drawing.Font("Khmer OS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLabel3.Location = new System.Drawing.Point(9, 144);
            this.groupLabel3.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.groupLabel3.Name = "groupLabel3";
            this.groupLabel3.Size = new System.Drawing.Size(611, 16);
            this.groupLabel3.TabIndex = 4;
            this.groupLabel3.TabStop = false;
            // 
            // txtDescKH
            // 
            this.txtDescKH.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescKH.Location = new System.Drawing.Point(420, 30);
            this.txtDescKH.Name = "txtDescKH";
            this.txtDescKH.Size = new System.Drawing.Size(200, 21);
            this.txtDescKH.TabIndex = 3;
            this.txtDescKH.TabOnEnter = true;
            // 
            // textBox3
            // 
            this.textBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(420, 111);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 21);
            this.textBox3.TabIndex = 1;
            this.textBox3.TabOnEnter = true;
            // 
            // textBox2
            // 
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(420, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 21);
            this.textBox2.TabIndex = 1;
            this.textBox2.TabOnEnter = true;
            // 
            // textBox5
            // 
            this.textBox5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(100, 308);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(200, 21);
            this.textBox5.TabIndex = 1;
            this.textBox5.TabOnEnter = true;
            // 
            // textBox1
            // 
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(420, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabOnEnter = true;
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(90, 30);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(200, 21);
            this.txtCode.TabIndex = 1;
            this.txtCode.TabOnEnter = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(380, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Email";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(35, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Address";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(100, 335);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(200, 21);
            this.textBox4.TabIndex = 2;
            this.textBox4.TabOnEnter = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(386, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Fax";
            // 
            // txtDescEN
            // 
            this.txtDescEN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescEN.Location = new System.Drawing.Point(90, 57);
            this.txtDescEN.Name = "txtDescEN";
            this.txtDescEN.Size = new System.Drawing.Size(200, 21);
            this.txtDescEN.TabIndex = 2;
            this.txtDescEN.TabOnEnter = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(374, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Phone";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(31, 339);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(377, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // frmLocationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(983, 524);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmLocationList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Location List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLocationList_FormClosed);
            this.Load += new System.EventHandler(this.frmLocationList_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgExpand)).EndInit();
            this.mnuShow.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private kPrasat.UI.DataGridView dgvList;
        private System.Windows.Forms.Panel panel1;
        private kPrasat.UI.TextBox txtDescKH;
        private kPrasat.UI.TextBox txtDescEN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton btnSaveNew;
        private System.Windows.Forms.LinkLabel lblClear;
        private System.Windows.Forms.LinkLabel lblRefresh;
        private System.Windows.Forms.PictureBox imgExpand;
        private kPrasat.UI.TextBox txtCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label13;
        private kPrasat.UI.GroupLabel groupLabel3;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.ContextMenuStrip mnuShow;
        private System.Windows.Forms.ToolStripButton btnActive;
        private System.Windows.Forms.ToolStripButton btnUnlock;
        private kPrasat.UI.GroupLabel groupLabel2;
        private kPrasat.UI.GroupLabel groupLabel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem mnuShowA;
        private System.Windows.Forms.ToolStripMenuItem mnuShowI;
        private System.Windows.Forms.LinkLabel lblFilter;
        private kPrasat.UI.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private UI.TextBox textBox3;
        private UI.TextBox textBox2;
        private UI.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private UI.GroupLabel groupLabel5;
        private System.Windows.Forms.TextBox textBox6;
        private UI.GroupLabel groupLabel4;
        private UI.TextBox textBox5;
        private UI.TextBox textBox4;
        private System.Windows.Forms.Label label9;        
    }
}

