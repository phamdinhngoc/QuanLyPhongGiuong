namespace E11_PhongGiuong
{
	partial class usc_lableTong
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTrong = new E00_Control.his_LabelX(this.components);
            this.lblDatTruoc = new E00_Control.his_LabelX(this.components);
            this.lblTatCa = new E00_Control.his_LabelX(this.components);
            this.cmt_ThayDoiMau = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnThayDoiMau = new System.Windows.Forms.ToolStripMenuItem();
            this.ẩnKhỏiTấtCảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAnKhoi = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain.SuspendLayout();
            this.cmt_ThayDoiMau.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblTrong);
            this.pnlMain.Controls.Add(this.lblDatTruoc);
            this.pnlMain.Controls.Add(this.lblTatCa);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(227, 23);
            this.pnlMain.TabIndex = 0;
            // 
            // lblTrong
            // 
            this.lblTrong.BackColor = System.Drawing.Color.Black;
            // 
            // 
            // 
            this.lblTrong.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTrong.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTrong.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTrong.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTrong.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTrong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTrong.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTrong.ForeColor = System.Drawing.Color.White;
            this.lblTrong.IsNotNull = false;
            this.lblTrong.Location = new System.Drawing.Point(150, 0);
            this.lblTrong.Name = "lblTrong";
            this.lblTrong.Size = new System.Drawing.Size(75, 23);
            this.lblTrong.TabIndex = 70;
            this.lblTrong.Text = "Trống";
            this.lblTrong.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblTrong.DoubleClick += new System.EventHandler(this.lbl_DoubleClick);
            this.lblTrong.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseUp);
            // 
            // lblDatTruoc
            // 
            this.lblDatTruoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.lblDatTruoc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDatTruoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDatTruoc.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDatTruoc.ForeColor = System.Drawing.Color.White;
            this.lblDatTruoc.IsNotNull = false;
            this.lblDatTruoc.Location = new System.Drawing.Point(75, 0);
            this.lblDatTruoc.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.lblDatTruoc.Name = "lblDatTruoc";
            this.lblDatTruoc.Size = new System.Drawing.Size(75, 23);
            this.lblDatTruoc.TabIndex = 69;
            this.lblDatTruoc.Text = "Đặt trước";
            this.lblDatTruoc.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblDatTruoc.DoubleClick += new System.EventHandler(this.lbl_DoubleClick);
            this.lblDatTruoc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseUp);
            // 
            // lblTatCa
            // 
            this.lblTatCa.BackColor = System.Drawing.Color.OliveDrab;
            // 
            // 
            // 
            this.lblTatCa.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTatCa.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTatCa.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTatCa.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dot;
            this.lblTatCa.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTatCa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTatCa.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTatCa.EnableMarkup = false;
            this.lblTatCa.ForeColor = System.Drawing.Color.White;
            this.lblTatCa.IsNotNull = false;
            this.lblTatCa.Location = new System.Drawing.Point(0, 0);
            this.lblTatCa.Name = "lblTatCa";
            this.lblTatCa.PaddingBottom = 10;
            this.lblTatCa.PaddingLeft = 10;
            this.lblTatCa.PaddingRight = 10;
            this.lblTatCa.PaddingTop = 10;
            this.lblTatCa.Size = new System.Drawing.Size(75, 23);
            this.lblTatCa.TabIndex = 71;
            this.lblTatCa.Text = "Tất cả";
            this.lblTatCa.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblTatCa.DoubleClick += new System.EventHandler(this.lbl_DoubleClick);
            this.lblTatCa.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseUp);
            // 
            // cmt_ThayDoiMau
            // 
            this.cmt_ThayDoiMau.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnThayDoiMau,
            this.btnAnKhoi});
            this.cmt_ThayDoiMau.Name = "contextMenuStrip2";
            this.cmt_ThayDoiMau.Size = new System.Drawing.Size(191, 48);
            // 
            // btnThayDoiMau
            // 
            this.btnThayDoiMau.Name = "btnThayDoiMau";
            this.btnThayDoiMau.Size = new System.Drawing.Size(190, 22);
            this.btnThayDoiMau.Text = "Thay đổi màu hiển thị";
            this.btnThayDoiMau.Click += new System.EventHandler(this.btnThayDoiMau_Click);
            // 
            // ẩnKhỏiTấtCảToolStripMenuItem
            // 
            this.ẩnKhỏiTấtCảToolStripMenuItem.Name = "ẩnKhỏiTấtCảToolStripMenuItem";
            this.ẩnKhỏiTấtCảToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.ẩnKhỏiTấtCảToolStripMenuItem.Text = "Ẩn khỏi tất cả";
            // 
            // btnAnKhoi
            // 
            this.btnAnKhoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAnKhoi.Name = "btnAnKhoi";
            this.btnAnKhoi.Size = new System.Drawing.Size(190, 22);
            this.btnAnKhoi.Text = "Ẩn khỏi tất cả";
            this.btnAnKhoi.Click += new System.EventHandler(this.btnAnKhoi_Click);
            // 
            // usc_lableTong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "usc_lableTong";
            this.Size = new System.Drawing.Size(227, 23);
            this.Load += new System.EventHandler(this.usc_lableTong_Load);
            this.pnlMain.ResumeLayout(false);
            this.cmt_ThayDoiMau.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlMain;
		private E00_Control.his_LabelX lblTrong;
		private E00_Control.his_LabelX lblDatTruoc;
		private E00_Control.his_LabelX lblTatCa;
		private System.Windows.Forms.ContextMenuStrip cmt_ThayDoiMau;
		private System.Windows.Forms.ToolStripMenuItem btnThayDoiMau;
        private System.Windows.Forms.ToolStripMenuItem btnAnKhoi;
        private System.Windows.Forms.ToolStripMenuItem ẩnKhỏiTấtCảToolStripMenuItem;
    }
}
