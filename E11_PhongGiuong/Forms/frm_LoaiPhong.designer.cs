﻿namespace E11_PhongGiuong
{
    partial class frm_LoaiPhong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LoaiPhong));
            this.dgvDanhMucLoaiPhong = new E00_Control.his_DataGridView();
            this.col_Chon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_Sua = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.col_Xoa = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.col_STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MaPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenLoaiPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Doituong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_GoiYGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_GiaTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSTT = new E00_Control.his_LabelX(this.components);
            this.lblTenLoaiPhong = new E00_Control.his_LabelX(this.components);
            this.itgSTT = new E00_Control.his_IntegerInput();
            this.txtTenLoaiPhong = new E00_Control.his_TextboxX();
            this.ckAll = new E00_Control.his_CheckBoxX();
            this.cPBMauSac = new E00_Control.his_ColorPickerButton();
            this.his_LabelX1 = new E00_Control.his_LabelX(this.components);
            this.usc_GiaGoiY = new E00_ControlNew.usc_SelectBox();
            this.lblTen = new E00_Control.his_LabelX(this.components);
            this.labelX1 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX2 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX3 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX17 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX18 = new E00_Control.his_LabelX(this.components);
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c = new E00_Control.his_LabelX(this.components);
            this.object_1c582d54_0ef8_4649_884d_4237be896d11 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX13 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX14 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX15 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX16 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX12 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX4 = new E00_Control.his_LabelX(this.components);
            this.slbDoiTuong = new E00_ControlNew.usc_SelectBox();
            this.txtGiaTien = new E00_Control.his_TextboxX();
            this.dataGridViewGme_dgvDanhMucLoaiPhong = new E00_ControlAdv.ViewUI.DataGridViewGme();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlButton.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlControl2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMucLoaiPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itgSTT)).BeginInit();
            this.usc_GiaGoiY.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.Size = new System.Drawing.Size(843, 45);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.panel1);
            this.pnlSearch.Location = new System.Drawing.Point(2, 137);
            this.pnlSearch.Size = new System.Drawing.Size(843, 33);
            this.pnlSearch.TabIndex = 11;
            this.pnlSearch.Controls.SetChildIndex(this.panel1, 0);
            // 
            // btnLuu
            // 
            this.btnLuu.TabIndex = 13;
            this.btnLuu.Text = "Lưu";
            // 
            // pnlControl2
            // 
            this.pnlControl2.Controls.Add(this.txtGiaTien);
            this.pnlControl2.Controls.Add(this.slbDoiTuong);
            this.pnlControl2.Controls.Add(this.his_LabelX4);
            this.pnlControl2.Controls.Add(this.usc_GiaGoiY);
            this.pnlControl2.Controls.Add(this.his_LabelX12);
            this.pnlControl2.Controls.Add(this.his_LabelX1);
            this.pnlControl2.Controls.Add(this.cPBMauSac);
            this.pnlControl2.Controls.Add(this.txtTenLoaiPhong);
            this.pnlControl2.Controls.Add(this.itgSTT);
            this.pnlControl2.Controls.Add(this.lblTenLoaiPhong);
            this.pnlControl2.Controls.Add(this.lblSTT);
            this.pnlControl2.Size = new System.Drawing.Size(843, 90);
            this.pnlControl2.TabIndex = 0;
            this.pnlControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlControl2_Paint);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.ckAll);
            this.pnlMain.Controls.Add(this.dataGridViewGme_dgvDanhMucLoaiPhong);
            this.pnlMain.Location = new System.Drawing.Point(2, 170);
            this.pnlMain.Size = new System.Drawing.Size(843, 286);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // dgvDanhMucLoaiPhong
            // 
            this.dgvDanhMucLoaiPhong.AllowUserToAddRows = false;
            this.dgvDanhMucLoaiPhong.AllowUserToDeleteRows = false;
            this.dgvDanhMucLoaiPhong.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvDanhMucLoaiPhong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhMucLoaiPhong.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhMucLoaiPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhMucLoaiPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhMucLoaiPhong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Chon,
            this.col_Sua,
            this.col_Xoa,
            this.col_STT,
            this.col_MaPhong,
            this.col_TenLoaiPhong,
            this.col_id,
            this.col_Doituong,
            this.col_GoiYGia,
            this.col_GiaTien});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhMucLoaiPhong.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDanhMucLoaiPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhMucLoaiPhong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhMucLoaiPhong.Location = new System.Drawing.Point(0, 0);
            this.dgvDanhMucLoaiPhong.MultiSelect = false;
            this.dgvDanhMucLoaiPhong.Name = "dgvDanhMucLoaiPhong";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhMucLoaiPhong.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhMucLoaiPhong.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDanhMucLoaiPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhMucLoaiPhong.Size = new System.Drawing.Size(843, 256);
            this.dgvDanhMucLoaiPhong.TabIndex = 14;
            this.dgvDanhMucLoaiPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhMucLoaiPhong_CellClick);
            this.dgvDanhMucLoaiPhong.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhMucLoaiPhong_CellValueChanged);
            this.dgvDanhMucLoaiPhong.SelectionChanged += new System.EventHandler(this.dgvDanhMucLoaiPhong_SelectionChanged);
            // 
            // col_Chon
            // 
            this.col_Chon.HeaderText = "";
            this.col_Chon.Name = "col_Chon";
            this.col_Chon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_Chon.Width = 32;
            // 
            // col_Sua
            // 
            this.col_Sua.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.col_Sua.HeaderText = "";
            this.col_Sua.HoverImage = ((System.Drawing.Image)(resources.GetObject("col_Sua.HoverImage")));
            this.col_Sua.Image = ((System.Drawing.Image)(resources.GetObject("col_Sua.Image")));
            this.col_Sua.Name = "col_Sua";
            this.col_Sua.Text = null;
            this.col_Sua.Width = 28;
            // 
            // col_Xoa
            // 
            this.col_Xoa.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.col_Xoa.HeaderText = "";
            this.col_Xoa.HoverImage = ((System.Drawing.Image)(resources.GetObject("col_Xoa.HoverImage")));
            this.col_Xoa.Image = ((System.Drawing.Image)(resources.GetObject("col_Xoa.Image")));
            this.col_Xoa.Name = "col_Xoa";
            this.col_Xoa.Text = null;
            this.col_Xoa.Width = 28;
            // 
            // col_STT
            // 
            this.col_STT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_STT.DataPropertyName = "STT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_STT.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_STT.HeaderText = "Số thứ tự";
            this.col_STT.Name = "col_STT";
            this.col_STT.ReadOnly = true;
            // 
            // col_MaPhong
            // 
            this.col_MaPhong.DataPropertyName = "MA";
            this.col_MaPhong.HeaderText = "Mã phòng";
            this.col_MaPhong.Name = "col_MaPhong";
            // 
            // col_TenLoaiPhong
            // 
            this.col_TenLoaiPhong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_TenLoaiPhong.DataPropertyName = "ten";
            this.col_TenLoaiPhong.HeaderText = "Tên loại phòng";
            this.col_TenLoaiPhong.Name = "col_TenLoaiPhong";
            this.col_TenLoaiPhong.ReadOnly = true;
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "id";
            this.col_id.HeaderText = "";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            // 
            // col_Doituong
            // 
            this.col_Doituong.HeaderText = "Đối tượng";
            this.col_Doituong.Name = "col_Doituong";
            this.col_Doituong.Visible = false;
            // 
            // col_GoiYGia
            // 
            this.col_GoiYGia.HeaderText = "Gợi ý giá";
            this.col_GoiYGia.Name = "col_GoiYGia";
            this.col_GoiYGia.Visible = false;
            // 
            // col_GiaTien
            // 
            this.col_GiaTien.HeaderText = "GIá tiền";
            this.col_GiaTien.Name = "col_GiaTien";
            this.col_GiaTien.Visible = false;
            // 
            // lblSTT
            // 
            // 
            // 
            // 
            this.lblSTT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSTT.IsNotNull = false;
            this.lblSTT.Location = new System.Drawing.Point(14, 8);
            this.lblSTT.Name = "lblSTT";
            this.lblSTT.Size = new System.Drawing.Size(75, 23);
            this.lblSTT.TabIndex = 1;
            this.lblSTT.Text = "Số thứ tự";
            // 
            // lblTenLoaiPhong
            // 
            // 
            // 
            // 
            this.lblTenLoaiPhong.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTenLoaiPhong.IsNotNull = false;
            this.lblTenLoaiPhong.Location = new System.Drawing.Point(118, 8);
            this.lblTenLoaiPhong.Name = "lblTenLoaiPhong";
            this.lblTenLoaiPhong.Size = new System.Drawing.Size(75, 23);
            this.lblTenLoaiPhong.TabIndex = 3;
            this.lblTenLoaiPhong.Text = "Tên loại phòng";
            // 
            // itgSTT
            // 
            // 
            // 
            // 
            this.itgSTT.BackgroundStyle.Class = "DateTimeInputBackground";
            this.itgSTT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itgSTT.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.itgSTT.Location = new System.Drawing.Point(64, 9);
            this.itgSTT.MinValue = 0;
            this.itgSTT.Name = "itgSTT";
            this.itgSTT.ShowUpDown = true;
            this.itgSTT.Size = new System.Drawing.Size(48, 20);
            this.itgSTT.TabIndex = 2;
            this.itgSTT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itgSTT_KeyDown);
            // 
            // txtTenLoaiPhong
            // 
            this.txtTenLoaiPhong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenLoaiPhong.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTenLoaiPhong.Border.Class = "TextBoxBorder";
            this.txtTenLoaiPhong.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTenLoaiPhong.Location = new System.Drawing.Point(199, 9);
            this.txtTenLoaiPhong.Name = "txtTenLoaiPhong";
            this.txtTenLoaiPhong.Size = new System.Drawing.Size(504, 20);
            this.txtTenLoaiPhong.TabIndex = 4;
            this.txtTenLoaiPhong.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTenLoaiPhong_KeyDown);
            // 
            // ckAll
            // 
            this.ckAll.BackColor = System.Drawing.Color.Blue;
            // 
            // 
            // 
            this.ckAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckAll.CheckSignSize = new System.Drawing.Size(17, 16);
            this.ckAll.Location = new System.Drawing.Point(46, 32);
            this.ckAll.Name = "ckAll";
            this.ckAll.Size = new System.Drawing.Size(23, 20);
            this.ckAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckAll.TabIndex = 34;
            this.ckAll.TextColor = System.Drawing.Color.Black;
            this.ckAll.CheckedChanged += new System.EventHandler(this.ckAll_CheckedChanged);
            // 
            // cPBMauSac
            // 
            this.cPBMauSac.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cPBMauSac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cPBMauSac.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cPBMauSac.Image = ((System.Drawing.Image)(resources.GetObject("cPBMauSac.Image")));
            this.cPBMauSac.Location = new System.Drawing.Point(719, 8);
            this.cPBMauSac.Name = "cPBMauSac";
            this.cPBMauSac.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.cPBMauSac.Size = new System.Drawing.Size(108, 23);
            this.cPBMauSac.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cPBMauSac.TabIndex = 10;
            this.cPBMauSac.SelectedColorChanged += new System.EventHandler(this.cPBMauSac_SelectedColorChanged);
            this.cPBMauSac.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cPBMauSac_KeyUp);
            // 
            // his_LabelX1
            // 
            // 
            // 
            // 
            this.his_LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX1.IsNotNull = false;
            this.his_LabelX1.Location = new System.Drawing.Point(14, 32);
            this.his_LabelX1.Name = "his_LabelX1";
            this.his_LabelX1.Size = new System.Drawing.Size(75, 23);
            this.his_LabelX1.TabIndex = 5;
            this.his_LabelX1.Text = "Đối tượng";
            // 
            // usc_GiaGoiY
            // 
            this.usc_GiaGoiY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usc_GiaGoiY.Controls.Add(this.lblTen);
            this.usc_GiaGoiY.Controls.Add(this.labelX1);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX2);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX3);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX17);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX18);
            this.usc_GiaGoiY.Controls.Add(this.object_26ae1b83_1091_483e_8bfb_344f86029e3c);
            this.usc_GiaGoiY.Controls.Add(this.object_1c582d54_0ef8_4649_884d_4237be896d11);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX13);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX14);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX15);
            this.usc_GiaGoiY.Controls.Add(this.his_LabelX16);
            this.usc_GiaGoiY.DataSource = null;
            this.usc_GiaGoiY.Enabled = false;
            this.usc_GiaGoiY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.usc_GiaGoiY.his_AddNew = false;
            this.usc_GiaGoiY.his_ColMa = null;
            this.usc_GiaGoiY.his_ColTen = null;
            this.usc_GiaGoiY.his_FontSize = 10F;
            this.usc_GiaGoiY.his_lblText = "Mã:";
            this.usc_GiaGoiY.his_lblTitle1_Bold = false;
            this.usc_GiaGoiY.his_lblTitle1_text = "labelX1";
            this.usc_GiaGoiY.his_lblTitle1_Visible = true;
            this.usc_GiaGoiY.his_lblTitle1_Width = 0;
            this.usc_GiaGoiY.his_lblVisible = true;
            this.usc_GiaGoiY.his_lblWidth = 0;
            this.usc_GiaGoiY.his_ShowCount = 10;
            this.usc_GiaGoiY.his_Showmin = 10;
            this.usc_GiaGoiY.his_TabLocation = 0;
            this.usc_GiaGoiY.his_TenReadonly = false;
            this.usc_GiaGoiY.his_TenReadOnly = false;
            this.usc_GiaGoiY.his_TenVisible = true;
            this.usc_GiaGoiY.his_TimeSearch = 400;
            this.usc_GiaGoiY.his_txtWidth = 0;
            this.usc_GiaGoiY.his_XoaMa = true;
            this.usc_GiaGoiY.Location = new System.Drawing.Point(396, 32);
            this.usc_GiaGoiY.Margin = new System.Windows.Forms.Padding(0);
            this.usc_GiaGoiY.Minlenght = 200;
            this.usc_GiaGoiY.Name = "usc_GiaGoiY";
            this.usc_GiaGoiY.Size = new System.Drawing.Size(307, 23);
            this.usc_GiaGoiY.TabIndex = 8;
            this.usc_GiaGoiY.HisSelectChange += new E00_ControlNew.EventHandler(this.usc_GiaGoiY_HisSelectChange);
            this.usc_GiaGoiY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usc_GiaGoiY_KeyDown);
            // 
            // lblTen
            // 
            this.lblTen.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTen.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTen.ForeColor = System.Drawing.Color.Black;
            this.lblTen.IsNotNull = false;
            this.lblTen.Location = new System.Drawing.Point(0, 0);
            this.lblTen.Margin = new System.Windows.Forms.Padding(368, 111, 368, 111);
            this.lblTen.Name = "lblTen";
            this.lblTen.SingleLineColor = System.Drawing.Color.Black;
            this.lblTen.Size = new System.Drawing.Size(0, 23);
            this.lblTen.TabIndex = 4;
            this.lblTen.Text = "Mã:";
            this.lblTen.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.IsNotNull = false;
            this.labelX1.Location = new System.Drawing.Point(0, 0);
            this.labelX1.Margin = new System.Windows.Forms.Padding(276, 2, 276, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(0, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "labelX1";
            // 
            // his_LabelX2
            // 
            this.his_LabelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.his_LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX2.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX2.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX2.IsNotNull = false;
            this.his_LabelX2.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX2.Margin = new System.Windows.Forms.Padding(491, 137, 491, 137);
            this.his_LabelX2.Name = "his_LabelX2";
            this.his_LabelX2.SingleLineColor = System.Drawing.Color.Black;
            this.his_LabelX2.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX2.TabIndex = 4;
            this.his_LabelX2.Text = "Mã:";
            this.his_LabelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // his_LabelX3
            // 
            // 
            // 
            // 
            this.his_LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX3.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.his_LabelX3.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX3.IsNotNull = false;
            this.his_LabelX3.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX3.Margin = new System.Windows.Forms.Padding(368, 2, 368, 2);
            this.his_LabelX3.Name = "his_LabelX3";
            this.his_LabelX3.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX3.TabIndex = 3;
            this.his_LabelX3.Text = "his_LabelX3";
            // 
            // his_LabelX17
            // 
            this.his_LabelX17.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.his_LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX17.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX17.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX17.IsNotNull = false;
            this.his_LabelX17.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX17.Margin = new System.Windows.Forms.Padding(873, 208, 873, 208);
            this.his_LabelX17.Name = "his_LabelX17";
            this.his_LabelX17.SingleLineColor = System.Drawing.Color.Black;
            this.his_LabelX17.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX17.TabIndex = 4;
            this.his_LabelX17.Text = "Mã:";
            this.his_LabelX17.TextAlignment = System.Drawing.StringAlignment.Far;
            this.his_LabelX17.Visible = false;
            // 
            // his_LabelX18
            // 
            // 
            // 
            // 
            this.his_LabelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX18.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.his_LabelX18.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX18.IsNotNull = false;
            this.his_LabelX18.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX18.Margin = new System.Windows.Forms.Padding(655, 169, 655, 169);
            this.his_LabelX18.Name = "his_LabelX18";
            this.his_LabelX18.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX18.TabIndex = 3;
            this.his_LabelX18.Text = "labelX1";
            this.his_LabelX18.Visible = false;
            // 
            // object_26ae1b83_1091_483e_8bfb_344f86029e3c
            // 
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.Dock = System.Windows.Forms.DockStyle.Left;
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.ForeColor = System.Drawing.Color.Black;
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.IsNotNull = false;
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.Location = new System.Drawing.Point(0, 0);
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.Margin = new System.Windows.Forms.Padding(873, 208, 873, 208);
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.Name = "object_26ae1b83_1091_483e_8bfb_344f86029e3c";
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.SingleLineColor = System.Drawing.Color.Black;
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.Size = new System.Drawing.Size(0, 23);
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.TabIndex = 4;
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.Text = "Mã:";
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.TextAlignment = System.Drawing.StringAlignment.Far;
            this.object_26ae1b83_1091_483e_8bfb_344f86029e3c.Visible = false;
            // 
            // object_1c582d54_0ef8_4649_884d_4237be896d11
            // 
            // 
            // 
            // 
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Dock = System.Windows.Forms.DockStyle.Left;
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.ForeColor = System.Drawing.Color.Black;
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.IsNotNull = false;
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Location = new System.Drawing.Point(0, 0);
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Margin = new System.Windows.Forms.Padding(655, 169, 655, 169);
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Name = "object_1c582d54_0ef8_4649_884d_4237be896d11";
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Size = new System.Drawing.Size(0, 23);
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.TabIndex = 3;
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Text = "labelX1";
            this.object_1c582d54_0ef8_4649_884d_4237be896d11.Visible = false;
            // 
            // his_LabelX13
            // 
            this.his_LabelX13.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.his_LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX13.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX13.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX13.IsNotNull = false;
            this.his_LabelX13.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX13.Margin = new System.Windows.Forms.Padding(873, 208, 873, 208);
            this.his_LabelX13.Name = "his_LabelX13";
            this.his_LabelX13.SingleLineColor = System.Drawing.Color.Black;
            this.his_LabelX13.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX13.TabIndex = 0;
            this.his_LabelX13.Text = "Mã:";
            this.his_LabelX13.TextAlignment = System.Drawing.StringAlignment.Far;
            this.his_LabelX13.Visible = false;
            // 
            // his_LabelX14
            // 
            // 
            // 
            // 
            this.his_LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX14.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.his_LabelX14.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX14.IsNotNull = false;
            this.his_LabelX14.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX14.Margin = new System.Windows.Forms.Padding(655, 169, 655, 169);
            this.his_LabelX14.Name = "his_LabelX14";
            this.his_LabelX14.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX14.TabIndex = 3;
            this.his_LabelX14.Text = "labelX1";
            this.his_LabelX14.Visible = false;
            // 
            // his_LabelX15
            // 
            this.his_LabelX15.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.his_LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX15.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX15.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX15.IsNotNull = false;
            this.his_LabelX15.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX15.Margin = new System.Windows.Forms.Padding(873, 208, 873, 208);
            this.his_LabelX15.Name = "his_LabelX15";
            this.his_LabelX15.SingleLineColor = System.Drawing.Color.Black;
            this.his_LabelX15.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX15.TabIndex = 4;
            this.his_LabelX15.Text = "Mã:";
            this.his_LabelX15.TextAlignment = System.Drawing.StringAlignment.Far;
            this.his_LabelX15.Visible = false;
            // 
            // his_LabelX16
            // 
            // 
            // 
            // 
            this.his_LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX16.Dock = System.Windows.Forms.DockStyle.Left;
            this.his_LabelX16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.his_LabelX16.ForeColor = System.Drawing.Color.Black;
            this.his_LabelX16.IsNotNull = false;
            this.his_LabelX16.Location = new System.Drawing.Point(0, 0);
            this.his_LabelX16.Margin = new System.Windows.Forms.Padding(655, 169, 655, 169);
            this.his_LabelX16.Name = "his_LabelX16";
            this.his_LabelX16.Size = new System.Drawing.Size(0, 23);
            this.his_LabelX16.TabIndex = 3;
            this.his_LabelX16.Text = "his_LabelX6";
            this.his_LabelX16.Visible = false;
            // 
            // his_LabelX12
            // 
            // 
            // 
            // 
            this.his_LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX12.IsNotNull = false;
            this.his_LabelX12.Location = new System.Drawing.Point(345, 32);
            this.his_LabelX12.Name = "his_LabelX12";
            this.his_LabelX12.Size = new System.Drawing.Size(74, 23);
            this.his_LabelX12.TabIndex = 7;
            this.his_LabelX12.Text = "Gợi ý giá";
            // 
            // his_LabelX4
            // 
            // 
            // 
            // 
            this.his_LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX4.IsNotNull = false;
            this.his_LabelX4.Location = new System.Drawing.Point(14, 57);
            this.his_LabelX4.Name = "his_LabelX4";
            this.his_LabelX4.Size = new System.Drawing.Size(75, 23);
            this.his_LabelX4.TabIndex = 9;
            this.his_LabelX4.Text = "Giá tiền";
            // 
            // slbDoiTuong
            // 
            this.slbDoiTuong.DataSource = null;
            this.slbDoiTuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.slbDoiTuong.his_AddNew = false;
            this.slbDoiTuong.his_ColMa = null;
            this.slbDoiTuong.his_ColTen = null;
            this.slbDoiTuong.his_FontSize = 9.75F;
            this.slbDoiTuong.his_lblText = "Mã:";
            this.slbDoiTuong.his_lblTitle1_Bold = false;
            this.slbDoiTuong.his_lblTitle1_text = "labelX1";
            this.slbDoiTuong.his_lblTitle1_Visible = false;
            this.slbDoiTuong.his_lblTitle1_Width = 0;
            this.slbDoiTuong.his_lblVisible = false;
            this.slbDoiTuong.his_lblWidth = 0;
            this.slbDoiTuong.his_ShowCount = 10;
            this.slbDoiTuong.his_Showmin = 10;
            this.slbDoiTuong.his_TabLocation = 0;
            this.slbDoiTuong.his_TenReadonly = false;
            this.slbDoiTuong.his_TenReadOnly = false;
            this.slbDoiTuong.his_TenVisible = true;
            this.slbDoiTuong.his_TimeSearch = 200;
            this.slbDoiTuong.his_txtWidth = 0;
            this.slbDoiTuong.his_XoaMa = true;
            this.slbDoiTuong.Location = new System.Drawing.Point(64, 33);
            this.slbDoiTuong.Margin = new System.Windows.Forms.Padding(0);
            this.slbDoiTuong.Minlenght = 200;
            this.slbDoiTuong.Name = "slbDoiTuong";
            this.slbDoiTuong.Size = new System.Drawing.Size(271, 22);
            this.slbDoiTuong.TabIndex = 6;
            this.slbDoiTuong.HisSelectChange += new E00_ControlNew.EventHandler(this.slbDoiTuong_HisSelectChange);
            // 
            // txtGiaTien
            // 
            this.txtGiaTien.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGiaTien.Border.Class = "TextBoxBorder";
            this.txtGiaTien.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGiaTien.Location = new System.Drawing.Point(64, 60);
            this.txtGiaTien.Name = "txtGiaTien";
            this.txtGiaTien.Size = new System.Drawing.Size(121, 20);
            this.txtGiaTien.TabIndex = 10;
            this.txtGiaTien.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGiaTien_KeyDown);
            this.txtGiaTien.Validated += new System.EventHandler(this.txtGiaTien_Validated);
            // 
            // dataGridViewGme_dgvDanhMucLoaiPhong
            // 
            this.dataGridViewGme_dgvDanhMucLoaiPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewGme_dgvDanhMucLoaiPhong.GridView = this.dgvDanhMucLoaiPhong;
            this.dataGridViewGme_dgvDanhMucLoaiPhong.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewGme_dgvDanhMucLoaiPhong.Name = "dataGridViewGme_dgvDanhMucLoaiPhong";
            this.dataGridViewGme_dgvDanhMucLoaiPhong.Size = new System.Drawing.Size(843, 286);
            this.dataGridViewGme_dgvDanhMucLoaiPhong.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 30);
            this.panel1.TabIndex = 1;
            // 
            // frm_LoaiPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 458);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_LoaiPhong";
            this.Text = "Khai báo loại phòng";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_LoaiPhong_FormClosed);
            this.Load += new System.EventHandler(this.frm_LoaiPhong_Load);
            this.pnlButton.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlControl2.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMucLoaiPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itgSTT)).EndInit();
            this.usc_GiaGoiY.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private E00_Control.his_DataGridView dgvDanhMucLoaiPhong;
        private E00_Control.his_TextboxX txtTenLoaiPhong;
        private E00_Control.his_IntegerInput itgSTT;
        private E00_Control.his_LabelX lblTenLoaiPhong;
        private E00_Control.his_LabelX lblSTT;
        private E00_Control.his_CheckBoxX ckAll;
        private E00_Control.his_ColorPickerButton cPBMauSac;
		private E00_ControlNew.usc_SelectBox usc_GiaGoiY;
		private E00_Control.his_LabelX lblTen;
		private E00_Control.his_LabelX labelX1;
		private E00_Control.his_LabelX his_LabelX2;
		private E00_Control.his_LabelX his_LabelX3;
		private E00_Control.his_LabelX his_LabelX17;
		private E00_Control.his_LabelX his_LabelX18;
		private E00_Control.his_LabelX object_26ae1b83_1091_483e_8bfb_344f86029e3c;
		private E00_Control.his_LabelX object_1c582d54_0ef8_4649_884d_4237be896d11;
		private E00_Control.his_LabelX his_LabelX13;
		private E00_Control.his_LabelX his_LabelX14;
		private E00_Control.his_LabelX his_LabelX15;
		private E00_Control.his_LabelX his_LabelX16;
		private E00_Control.his_LabelX his_LabelX12;
		private E00_Control.his_LabelX his_LabelX1;
		private E00_Control.his_LabelX his_LabelX4;
		private E00_ControlNew.usc_SelectBox slbDoiTuong;
		private E00_Control.his_TextboxX txtGiaTien;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_Chon;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn col_Sua;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn col_Xoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenLoaiPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Doituong;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_GoiYGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_GiaTien;
        private E00_ControlAdv.ViewUI.DataGridViewGme dataGridViewGme_dgvDanhMucLoaiPhong;
        private System.Windows.Forms.Panel panel1;
    }
}