namespace E11_PhongGiuong
{
    partial class frm_LoaiPhongImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LoaiPhongImport));
            this.dgvDanhMucLoaiPhong = new E00_Control.his_DataGridView();
            this.lblSTT = new E00_Control.his_LabelX(this.components);
            this.lblTenLoaiPhong = new E00_Control.his_LabelX(this.components);
            this.itgSTT = new E00_Control.his_IntegerInput();
            this.txtTenLoaiPhong = new E00_Control.his_TextboxX();
            this.col_STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenLoaiPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlButton.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlControl2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMucLoaiPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itgSTT)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTimKiem
            // 
            // 
            // 
            // 
            this.txtTimKiem.Border.Class = "TextBoxBorder";
            this.txtTimKiem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTimKiem.Location = new System.Drawing.Point(1351, 6);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(1558, 6);
            // 
            // pnlButton
            // 
            this.pnlButton.Size = new System.Drawing.Size(843, 45);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Location = new System.Drawing.Point(2, 85);
            this.pnlSearch.Size = new System.Drawing.Size(843, 33);
            this.pnlSearch.Visible = false;
            // 
            // lblKetQua
            // 
            // 
            // 
            // 
            this.lblKetQua.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblKetQua.Location = new System.Drawing.Point(1234, 9);
            this.lblKetQua.Size = new System.Drawing.Size(111, 15);
            // 
            // btnLuu
            // 
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            // 
            // btnXoa
            // 
            this.btnXoa.Visible = false;
            // 
            // btnSua
            // 
            this.btnSua.Visible = false;
            // 
            // pnlControl2
            // 
            this.pnlControl2.Controls.Add(this.txtTenLoaiPhong);
            this.pnlControl2.Controls.Add(this.itgSTT);
            this.pnlControl2.Controls.Add(this.lblTenLoaiPhong);
            this.pnlControl2.Controls.Add(this.lblSTT);
            this.pnlControl2.Size = new System.Drawing.Size(843, 38);
            this.pnlControl2.TabIndex = 0;
            this.pnlControl2.Visible = false;
            this.pnlControl2.Controls.SetChildIndex(this.lblSTT, 0);
            this.pnlControl2.Controls.SetChildIndex(this.lblTenLoaiPhong, 0);
            this.pnlControl2.Controls.SetChildIndex(this.itgSTT, 0);
            this.pnlControl2.Controls.SetChildIndex(this.txtTenLoaiPhong, 0);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvDanhMucLoaiPhong);
            this.pnlMain.Location = new System.Drawing.Point(2, 118);
            this.pnlMain.Size = new System.Drawing.Size(843, 338);
            // 
            // btnTienIch
            // 
            this.btnTienIch.Visible = false;
            // 
            // btnIn
            // 
            this.btnIn.Visible = false;
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // dgvDanhMucLoaiPhong
            // 
            this.dgvDanhMucLoaiPhong.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvDanhMucLoaiPhong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhMucLoaiPhong.BackgroundColor = System.Drawing.SystemColors.Control;
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
            this.col_STT,
            this.col_TenLoaiPhong});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhMucLoaiPhong.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDanhMucLoaiPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhMucLoaiPhong.GridColor = System.Drawing.Color.Thistle;
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
            this.dgvDanhMucLoaiPhong.Size = new System.Drawing.Size(843, 338);
            this.dgvDanhMucLoaiPhong.TabIndex = 32;
            this.dgvDanhMucLoaiPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhMucLoaiPhong_CellClick);
            this.dgvDanhMucLoaiPhong.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhMucLoaiPhong_CellEnter);
            this.dgvDanhMucLoaiPhong.SelectionChanged += new System.EventHandler(this.dgvDanhMucLoaiPhong_SelectionChanged);
            this.dgvDanhMucLoaiPhong.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDanhMucLoaiPhong_KeyDown);
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
            this.lblSTT.TabIndex = 0;
            this.lblSTT.Text = "Số thứ tự";
            // 
            // lblTenLoaiPhong
            // 
            // 
            // 
            // 
            this.lblTenLoaiPhong.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTenLoaiPhong.IsNotNull = false;
            this.lblTenLoaiPhong.Location = new System.Drawing.Point(139, 8);
            this.lblTenLoaiPhong.Name = "lblTenLoaiPhong";
            this.lblTenLoaiPhong.Size = new System.Drawing.Size(75, 23);
            this.lblTenLoaiPhong.TabIndex = 4;
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
            this.itgSTT.TabIndex = 1;
            this.itgSTT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itgSTT_KeyDown);
            // 
            // txtTenLoaiPhong
            // 
            this.txtTenLoaiPhong.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTenLoaiPhong.Border.Class = "TextBoxBorder";
            this.txtTenLoaiPhong.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTenLoaiPhong.Location = new System.Drawing.Point(215, 9);
            this.txtTenLoaiPhong.Name = "txtTenLoaiPhong";
            this.txtTenLoaiPhong.Size = new System.Drawing.Size(620, 20);
            this.txtTenLoaiPhong.TabIndex = 5;
            this.txtTenLoaiPhong.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itgSTT_KeyDown);
            // 
            // col_STT
            // 
            this.col_STT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_STT.DataPropertyName = "STT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_STT.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_STT.HeaderText = "Số thứ tự";
            this.col_STT.Name = "col_STT";
            // 
            // col_TenLoaiPhong
            // 
            this.col_TenLoaiPhong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_TenLoaiPhong.DataPropertyName = "TEN";
            this.col_TenLoaiPhong.HeaderText = "Tên loại phòng";
            this.col_TenLoaiPhong.Name = "col_TenLoaiPhong";
            // 
            // frm_LoaiPhongImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 458);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_LoaiPhongImport";
            this.Text = "Khai báo loại phòng";
            this.Load += new System.EventHandler(this.frm_LoaiPhongImport_Load);
            this.pnlButton.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlControl2.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhMucLoaiPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itgSTT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private E00_Control.his_DataGridView dgvDanhMucLoaiPhong;
        private E00_Control.his_TextboxX txtTenLoaiPhong;
        private E00_Control.his_IntegerInput itgSTT;
        private E00_Control.his_LabelX lblTenLoaiPhong;
        private E00_Control.his_LabelX lblSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenLoaiPhong;
    }
}