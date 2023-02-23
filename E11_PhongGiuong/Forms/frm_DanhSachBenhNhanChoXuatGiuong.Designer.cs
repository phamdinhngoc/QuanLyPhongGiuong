namespace E11_PhongGiuong
{
    partial class frm_DanhSachBenhNhanChoXuatGiuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DanhSachBenhNhanChoXuatGiuong));
            this.dgvDSBNDangNam = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.col_IDGIUONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenGiuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_KP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MABN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_BN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NgayVao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NgayRa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_XuatVien = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonXColumn1 = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.slbKhoa = new E00_ControlNew.usc_SelectBox();
            this.txtTimKiem = new E00_Control.his_TextboxX();
            this.his_LabelX1 = new E00_Control.his_LabelX(this.components);
            this.lblTimKiem = new E00_Control.his_LabelX(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSBNDangNam)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDSBNDangNam
            // 
            this.dgvDSBNDangNam.AllowUserToAddRows = false;
            this.dgvDSBNDangNam.AllowUserToDeleteRows = false;
            this.dgvDSBNDangNam.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDSBNDangNam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDSBNDangNam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_IDGIUONG,
            this.TenGiuong,
            this.Column2,
            this.col_KP,
            this.col_MABN,
            this.col_BN,
            this.col_NgayVao,
            this.col_NgayRa,
            this.col_DiaChi,
            this.col_XuatVien,
            this.colID});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSBNDangNam.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDSBNDangNam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDSBNDangNam.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDSBNDangNam.Location = new System.Drawing.Point(0, 39);
            this.dgvDSBNDangNam.Name = "dgvDSBNDangNam";
            this.dgvDSBNDangNam.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDSBNDangNam.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDSBNDangNam.Size = new System.Drawing.Size(930, 482);
            this.dgvDSBNDangNam.TabIndex = 1;
            this.dgvDSBNDangNam.CellBorderStyleChanged += new System.EventHandler(this.dgvDSBNDangNam_CellBorderStyleChanged);
            this.dgvDSBNDangNam.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSBNDangNam_CellClick);
            // 
            // col_IDGIUONG
            // 
            this.col_IDGIUONG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.col_IDGIUONG.DataPropertyName = "IDGIUONG";
            this.col_IDGIUONG.HeaderText = "Id Giường";
            this.col_IDGIUONG.Name = "col_IDGIUONG";
            this.col_IDGIUONG.Visible = false;
            // 
            // TenGiuong
            // 
            this.TenGiuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TenGiuong.DataPropertyName = "TEN";
            this.TenGiuong.HeaderText = "Tên Giường";
            this.TenGiuong.Name = "TenGiuong";
            this.TenGiuong.Width = 88;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MAKP";
            this.Column2.HeaderText = "Mã khoa phòng";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // col_KP
            // 
            this.col_KP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.col_KP.DataPropertyName = "TENKP";
            this.col_KP.HeaderText = "Khoa phòng";
            this.col_KP.Name = "col_KP";
            this.col_KP.ReadOnly = true;
            this.col_KP.Width = 90;
            // 
            // col_MABN
            // 
            this.col_MABN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.col_MABN.DataPropertyName = "MABN";
            this.col_MABN.HeaderText = "Mã bệnh nhân";
            this.col_MABN.Name = "col_MABN";
            this.col_MABN.ReadOnly = true;
            this.col_MABN.Width = 101;
            // 
            // col_BN
            // 
            this.col_BN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_BN.DataPropertyName = "HOTEN";
            this.col_BN.HeaderText = "Bệnh nhân";
            this.col_BN.Name = "col_BN";
            this.col_BN.ReadOnly = true;
            // 
            // col_NgayVao
            // 
            this.col_NgayVao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.col_NgayVao.DataPropertyName = "TU";
            this.col_NgayVao.HeaderText = "Ngày vào";
            this.col_NgayVao.Name = "col_NgayVao";
            this.col_NgayVao.ReadOnly = true;
            this.col_NgayVao.Width = 78;
            // 
            // col_NgayRa
            // 
            this.col_NgayRa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.col_NgayRa.DataPropertyName = "DEN";
            this.col_NgayRa.HeaderText = "Ngày ra";
            this.col_NgayRa.Name = "col_NgayRa";
            this.col_NgayRa.ReadOnly = true;
            this.col_NgayRa.Visible = false;
            // 
            // col_DiaChi
            // 
            this.col_DiaChi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.col_DiaChi.DataPropertyName = "THON";
            this.col_DiaChi.HeaderText = "Địa chỉ";
            this.col_DiaChi.Name = "col_DiaChi";
            this.col_DiaChi.ReadOnly = true;
            this.col_DiaChi.Width = 65;
            // 
            // col_XuatVien
            // 
            this.col_XuatVien.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.col_XuatVien.HeaderText = "";
            this.col_XuatVien.HoverImage = ((System.Drawing.Image)(resources.GetObject("col_XuatVien.HoverImage")));
            this.col_XuatVien.Image = ((System.Drawing.Image)(resources.GetObject("col_XuatVien.Image")));
            this.col_XuatVien.Name = "col_XuatVien";
            this.col_XuatVien.ReadOnly = true;
            this.col_XuatVien.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_XuatVien.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_XuatVien.SplitButton = true;
            this.col_XuatVien.Text = null;
            this.col_XuatVien.ToolTipText = "Xuất viện";
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = false;
            // 
            // dataGridViewButtonXColumn1
            // 
            this.dataGridViewButtonXColumn1.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.dataGridViewButtonXColumn1.HeaderText = "";
            this.dataGridViewButtonXColumn1.HoverImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewButtonXColumn1.HoverImage")));
            this.dataGridViewButtonXColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewButtonXColumn1.Image")));
            this.dataGridViewButtonXColumn1.Name = "dataGridViewButtonXColumn1";
            this.dataGridViewButtonXColumn1.ReadOnly = true;
            this.dataGridViewButtonXColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonXColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewButtonXColumn1.SplitButton = true;
            this.dataGridViewButtonXColumn1.Text = null;
            this.dataGridViewButtonXColumn1.ToolTipText = "Xuất viện";
            this.dataGridViewButtonXColumn1.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.slbKhoa);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.his_LabelX1);
            this.panel1.Controls.Add(this.lblTimKiem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 39);
            this.panel1.TabIndex = 3;
            // 
            // slbKhoa
            // 
            this.slbKhoa.DataSource = null;
            this.slbKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.slbKhoa.his_AddNew = false;
            this.slbKhoa.his_ColMa = null;
            this.slbKhoa.his_ColTen = null;
            this.slbKhoa.his_FontSize = 8.25F;
            this.slbKhoa.his_lblText = "Mã:";
            this.slbKhoa.his_lblTitle1_Bold = false;
            this.slbKhoa.his_lblTitle1_text = "labelX1";
            this.slbKhoa.his_lblTitle1_Visible = false;
            this.slbKhoa.his_lblTitle1_Width = 0;
            this.slbKhoa.his_lblVisible = false;
            this.slbKhoa.his_lblWidth = 0;
            this.slbKhoa.his_ShowCount = 10;
            this.slbKhoa.his_Showmin = 10;
            this.slbKhoa.his_TabLocation = 0;
            this.slbKhoa.his_TenReadonly = false;
            this.slbKhoa.his_TenReadOnly = false;
            this.slbKhoa.his_TenVisible = true;
            this.slbKhoa.his_TimeSearch = 200;
            this.slbKhoa.his_txtWidth = 0;
            this.slbKhoa.his_XoaMa = true;
            this.slbKhoa.Location = new System.Drawing.Point(234, 13);
            this.slbKhoa.Margin = new System.Windows.Forms.Padding(0);
            this.slbKhoa.Minlenght = 200;
            this.slbKhoa.Name = "slbKhoa";
            this.slbKhoa.Size = new System.Drawing.Size(212, 20);
            this.slbKhoa.TabIndex = 73;
            this.slbKhoa.HisSelectChange += new E00_ControlNew.EventHandler(this.slbKhoa_HisSelectChange);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTimKiem.Border.Class = "TextBoxBorder";
            this.txtTimKiem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTimKiem.Location = new System.Drawing.Point(62, 13);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(130, 20);
            this.txtTimKiem.TabIndex = 71;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // his_LabelX1
            // 
            this.his_LabelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.his_LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX1.IsNotNull = false;
            this.his_LabelX1.Location = new System.Drawing.Point(198, 13);
            this.his_LabelX1.Name = "his_LabelX1";
            this.his_LabelX1.Size = new System.Drawing.Size(75, 23);
            this.his_LabelX1.TabIndex = 72;
            this.his_LabelX1.Text = "Khoa:";
            // 
            // lblTimKiem
            // 
            // 
            // 
            // 
            this.lblTimKiem.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTimKiem.IsNotNull = false;
            this.lblTimKiem.Location = new System.Drawing.Point(12, 12);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(75, 23);
            this.lblTimKiem.TabIndex = 72;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // frm_DanhSachBenhNhanChoXuatGiuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 521);
            this.Controls.Add(this.dgvDSBNDangNam);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "frm_DanhSachBenhNhanChoXuatGiuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách chờ xuất giường";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_DanhSachBenhNhanTaiGiuong_FormClosed);
            this.Load += new System.EventHandler(this.frm_DanhSachBenhNhanTaiGiuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSBNDangNam)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDSBNDangNam;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn dataGridViewButtonXColumn1;
        private System.Windows.Forms.Panel panel1;
        private E00_ControlNew.usc_SelectBox slbKhoa;
        private E00_Control.his_TextboxX txtTimKiem;
        private E00_Control.his_LabelX his_LabelX1;
        private E00_Control.his_LabelX lblTimKiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_IDGIUONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenGiuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_KP;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MABN;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_BN;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NgayVao;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NgayRa;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_DiaChi;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn col_XuatVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
    }
}