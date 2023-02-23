using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_Control;

using E00_Model;

namespace E11_PhongGiuong
{
	public partial class usc_lableTong : UserControl
	{
        #region Biến toàn cục
        private List<int> lststt = new List<int> { 4, 2, 3,1, 5,6 };
        private Dictionary<string, Color> _dicMacDinh = new Dictionary<string, Color> { { "Trống", Color.Green }, { "Đặt trước", Color.Gray }, { "Có người", Color.Orange }, { "Tất cả", Color.Blue }, { "Không sử dụng", Color.Black }, { "Chờ xuất giường", Color.Yellow } };
        public cls_ThucThiDuLieu _tT;
		public event EventHandler LBLDoubleClick;
        private DataTable _data = new DataTable();
        public event EventHandler reload;
        public event EventHandler reloadGiuong;
        public E00_Common.Api_Common _api;
        private bool _isload = false;
        public DataTable _tb;
        private Dictionary<string, Color> _diccolotinhtrang = new Dictionary<string, Color>();
        public E00_Common.Acc_Oracle _acc;
		#endregion

		#region Khởi tạo
		public usc_lableTong()
		{
			InitializeComponent();
		}

		public DataTable DataTinh
		{
			get
			{
				return _data;
			}
			set
			{
				if (value != null)
				{
					_data = value;
					CapNhatSoLuongGiuongTheoTungTrangThai();
				}
				else
				{
					foreach (his_LabelX item in pnlMain.Controls)
					{
							item.Text = "0:"+ " " + item.Tag;
					}
				}
			}
		}

        public string Where
        {
            get
            {
                string where = "";
                foreach (his_LabelX item in pnlMain.Controls)
                {
                    if (!item.EnableMarkup)
                    {
                        where += " AND " + cls_PG_DanhMucGiuong.col_TINHTRANG + " <> '" + item.Name + "' ";
                    }
                   
                }

                if (!string.IsNullOrEmpty(where))
                {
                    return where.Substring(4);
                }
                return "";
            }

          
        }

        #endregion

        #region Phương thức
        public void frmLoad()
		{
            if (!_isload)
            {



                _api = new E00_Common.Api_Common();
                _acc = new E00_Common.Acc_Oracle();
                _api.KetNoi();
                _tT = new cls_ThucThiDuLieu();
                KiemTraVaKhoiTaoMacDinh();
                
                _isload = true;
            }
            LoadLable();
        }
        public void frmLoad(usc_lableTong usc)
        {
            if (!_isload)
            {



                _api = usc._api;
                _acc = usc._acc;
                _api.KetNoi();
                _tT = usc._tT;
               
                _tb = usc._tb; 
                
                LoadLable();
                _isload = true;

            }
          
        }
        public void reloadcolor()
        {
            List<string> _lst = new List<string>();
            _lst.Add(cls_DanhMucColorTrangThaiNew.col_Id);
            _lst.Add(cls_DanhMucColorTrangThaiNew.col_Loai);
            _lst.Add(cls_DanhMucColorTrangThaiNew.col_Color);
            _lst.Add(cls_DanhMucColorTrangThaiNew.col_HienThi);
            string _userError = "", _systemError = "";
            _tb = _api.Search(ref _userError, ref _systemError,
                cls_DanhMucColorTrangThaiNew.tb_TenBang, _acc.Get_User(), -1, lst: _lst, orderByASC1: false, orderByName1: cls_DanhMucColorTrangThaiNew.col_Stt);
            _diccolotinhtrang.Clear();
            foreach (var item in pnlMain.Controls)
            {
                if (item is his_LabelX)
                {
                    his_LabelX lblTmp = item as his_LabelX;
                    try
                    {
                        DataRow dr = _tb.Select(cls_DanhMucColorTrangThaiNew.col_Id + " = '" + lblTmp.Name + "'")[0];
                        string mau = dr[cls_DanhMucColorTrangThaiNew.col_Color] + "";
                       string[] color = mau.Split(',');
                        _diccolotinhtrang.Add(lblTmp.Name, lblTmp.BackColor);
                        lblTmp.BackColor = Color.FromArgb(int.Parse(color[0].ToString()), int.Parse(color[1].ToString()),
                                     int.Parse(color[2].ToString()), int.Parse(color[3].ToString()));
                        lblTmp.EnableMarkup = (dr[cls_DanhMucColorTrangThaiNew.col_HienThi] + "") != "0";
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }
        public void LoadLable()
		{
			
				int Width = 0;
				int oldWidth = this.Width;
            if (_tb==null|| _tb.Rows.Count==0)
            {
                List<string> _lst = new List<string>();
                _lst.Add(cls_DanhMucColorTrangThaiNew.col_Id);
                _lst.Add(cls_DanhMucColorTrangThaiNew.col_Loai);
                _lst.Add(cls_DanhMucColorTrangThaiNew.col_Color);
                _lst.Add(cls_DanhMucColorTrangThaiNew.col_HienThi);
                string _userError = "", _systemError = "";
                _tb = _api.Search(ref _userError, ref _systemError,
                    cls_DanhMucColorTrangThaiNew.tb_TenBang, _acc.Get_User(), -1, lst: _lst, orderByASC1: false, orderByName1: cls_DanhMucColorTrangThaiNew.col_Stt); 
            }
				pnlMain.Controls.Clear();
                _diccolotinhtrang.Clear();

                foreach (DataRow item in _tb.Rows)
				{


					string[] color = (item[cls_DanhMucColorTrangThaiNew.col_Color] + "").Split(',');
					his_LabelX lblTmp = new his_LabelX();

					if (color.Length > 3)
					{
						lblTmp.BackColor = Color.FromArgb(int.Parse(color[0].ToString()), int.Parse(color[1].ToString()),
									int.Parse(color[2].ToString()), int.Parse(color[3].ToString()));
					}
					else
						lblTmp.BackColor = Color.Red;
					_diccolotinhtrang.Add(item[cls_DanhMucColorTrangThaiNew.col_Id] + "", lblTmp.BackColor);
					lblTmp.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dot;
					lblTmp.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dot;
					lblTmp.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dot;
					lblTmp.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dot;
					lblTmp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    lblTmp.ContextMenuStrip = cmt_ThayDoiMau;
                    lblTmp.Cursor = System.Windows.Forms.Cursors.Hand;
					lblTmp.Dock = System.Windows.Forms.DockStyle.Left;
					lblTmp.ForeColor = System.Drawing.Color.White;
					lblTmp.IsNotNull = false;
					lblTmp.Location = new System.Drawing.Point(150, 0);
					lblTmp.Name = item[cls_DanhMucColorTrangThaiNew.col_Id] + "";
					lblTmp.Size = new System.Drawing.Size(100, 23);
					lblTmp.TabIndex = 70;
                    lblTmp.EnableMarkup = (item[cls_DanhMucColorTrangThaiNew.col_HienThi] + "")!="0";
                if (!lblTmp.EnableMarkup)
                {

                }
                if (_data != null && _data.Rows.Count > 0)
				{
                   
					switch (item[cls_DanhMucColorTrangThaiNew.col_Id] + "")
					{
						case "3":
							lblTmp.Text = _data.Select(" TinhTrang <> '3' and TinhTrang <> '4'").Count() + " " + item[cls_DanhMucColorTrangThaiNew.col_Loai];
                        
                            break;
						case "4":
							lblTmp.Text = _data.Select(" TinhTrang = '3' or TinhTrang = '4'").Count() + " " + item[cls_DanhMucColorTrangThaiNew.col_Loai];
                        
                            break;
						default:
                           
							lblTmp.Text = _data.Select(" TinhTrang = " + item[cls_DanhMucColorTrangThaiNew.col_Id]).Count() + " " + item[cls_DanhMucColorTrangThaiNew.col_Loai];
                            
                            break;
					}
				}
				else
				{
					lblTmp.Text = "0 " + item[cls_DanhMucColorTrangThaiNew.col_Loai];
				}

                lblTmp.MouseEnter += LblTmp_MouseEnter;    
					lblTmp.Tag = item[cls_DanhMucColorTrangThaiNew.col_Loai] + "";
					lblTmp.TextAlignment = System.Drawing.StringAlignment.Center;
					lblTmp.Click += new System.EventHandler(this.lbl_DoubleClick);
					lblTmp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseUp);
					this.pnlMain.Controls.Add(lblTmp);
					Width += lblTmp.Width;
				}
				if (pnlMain.Controls.Count > 0)
				{
					this.Height = pnlMain.Controls[1].Height;
					this.Width = Width;
					this.Location = new Point(this.Location.X + oldWidth - Width, this.Location.Y);
				}
			
		}

        private void LblTmp_MouseEnter(object sender, EventArgs e)
        {
            his_LabelX lblTmp = sender as  his_LabelX;
           
                btnAnKhoi.Visible = true;
                if (lblTmp.Name == "3")
                {
                    btnAnKhoi.Visible = false;
                }
            btnAnKhoi.Checked = !lblTmp.EnableMarkup;
        }

        private void KiemTraVaKhoiTaoMacDinh()
		{
			List<string> lst;
			Dictionary<string, string> dicwhere;
			DataTable tb;
			string _userError = "", _systemError = "";
			int i = 0;
			string sql;
			foreach (var item in _dicMacDinh)
			{
				 lst = new List<string>();
				lst.Add(cls_DanhMucColorTrangThaiNew.col_Id);
				 dicwhere = new Dictionary<string, string>();
				dicwhere.Add(cls_DanhMucColorTrangThaiNew.col_Id,i+"");
				
				 tb = _api.Search(ref _userError, ref _systemError,
					cls_DanhMucColorTrangThaiNew.tb_TenBang, _acc.Get_User(), -1, lst: lst,dicEqual:dicwhere);
				if (tb==null)
				{
					TA_MessageBox.MessageBox.Show("Lỗi: Không có bảng D_DMCTRANGTHAI_NEW!"
								, TA_MessageBox.MessageIcon.Error);
					throw new Exception("Lỗi: Không có bảng D_DMCTRANGTHAI!");
				}
				if (tb.Rows.Count==0)
				{
                    sql = string.Format("insert into {0}.{1}({2},{3},{4},{8})"
                              + " values({5},N'{6}','{7}','{9}')",
                              _acc.Get_User()          //0
                              , cls_DanhMucColorTrangThaiNew.tb_TenBang //1
                              , cls_DanhMucColorTrangThaiNew.col_Id  //2
                              , cls_DanhMucColorTrangThaiNew.col_Loai   //3
                              , cls_DanhMucColorTrangThaiNew.col_Color  //4
                              , i //5
                              , item.Key //6
                              , "" + item.Value.A + "," + item.Value.R + "," + item.Value.G + "," + item.Value.B + "" //7
                              , cls_DanhMucColorTrangThaiNew.col_Stt //8
                              , lststt[i]
                               );
					if (!_acc.Execute_Data(ref _userError, ref _systemError, sql))
					{
						TA_MessageBox.MessageBox.Show("Lỗi: Không thể khởi tạo mẫu:" + item.Key
								, TA_MessageBox.MessageIcon.Error);
						return;
					}

				}
				i++;
			}

		}
		public Color GetColorTinhTrang(string tinhtrang)
		{
            try
            {
                if (_diccolotinhtrang.Count==0)
                {
                    this.frmLoad();
                }
                if (tinhtrang=="3")
                {
                    return _diccolotinhtrang["4"];
                }
                return _diccolotinhtrang[tinhtrang];
            }
            catch (Exception ex)
            {
                throw;
            }
		}
		public void CapNhatSoLuongGiuongTheoTungTrangThai(string TenLoaiPhong ="")
		{
			foreach (his_LabelX item in pnlMain.Controls)
			{
				string te = " "+item.Tag;
                string tmpstr = "";
                if (!string.IsNullOrEmpty(TenLoaiPhong))
                {
                    tmpstr += " AND TENLOAIPHONG ='"+ TenLoaiPhong + "'";
                }
				try
				{
					switch (item.Name)
					{


						case "3":
							item.Text = _data.Select(" TinhTrang <> '3' and TinhTrang <> '4'"+tmpstr).Count() + te;
							break;
						case "4":
							item.Text = _data.Select(" TinhTrang = '3' or TinhTrang = '4'" + tmpstr).Count() + te;
							break;
						default:
                           
							item.Text = _data.Select(" TinhTrang = '" + item.Name+"'" + tmpstr).Count() + te;
							break;
					}
				}
				catch (Exception)
				{

					
				}

			}
		}
        public void ChangeTinhTrang(string TTTu,string TTDen,int numchange=1)
        {
            if (TTTu == TTDen)
            {
                return;
            }
           
            foreach (his_LabelX item in pnlMain.Controls)
            {
                string te = " " + item.Tag;
                try
                {
                    switch (item.Name)
                    {
                        case "3":
                            int soluong = int.Parse(item.Text.Replace(item.Tag + "", "").Trim());
                            if (TTDen == item.Name)
                            {
                                soluong += numchange ;
                            }
                            if (TTTu == item.Name)
                            {
                                soluong = (soluong-numchange)<0?0: (soluong - numchange);
                            }
                            item.Text = soluong + te;
                            break;
                        case "4":
                            int soluong2 = int.Parse(item.Text.Replace(item.Tag + "", "").Trim());
                            if (TTDen == item.Name)
                            {
                                soluong2 += numchange;
                            }
                            if (TTTu == item.Name)
                            {
                                soluong2 = (soluong2 - numchange) < 0 ? 0 : (soluong2 - numchange);
                            }
                            item.Text = soluong2 + te;
                            break;
                        default:
                            int soluong3 = int.Parse(item.Text.Replace(item.Tag + "", "").Trim());
                            if (TTDen == item.Name)
                            {
                                soluong3 += numchange;
                            }
                            if (TTTu == item.Name)
                            {
                                soluong3 = (soluong3 - numchange) < 0 ? 0 : (soluong3 - numchange);
                            }
                            item.Text = soluong3 + te;
                            break;
                    }
                }
                catch (Exception)
                {


                }

            }
        }
        public void CapNhatSoLuongGiuongTheoTungTrangThai(List<Usc_GiuongEdit> lstcal)
        {
            foreach (his_LabelX item in pnlMain.Controls)
            {
                string te = " " + item.Tag;
                try
                {
                    switch (item.Name)
                    {

                        
                        case "3":
                            item.Text = lstcal.Count(n => n.Tinhtrang != "3" && n.Tinhtrang != "4")  + te;
                            break;
                        case "4":
                            item.Text = lstcal.Count(n => n.Tinhtrang == "3" && n.Tinhtrang == "4") + te;
                            break;
                        default:
                            item.Text = lstcal.Count(n => n.Tinhtrang == item.Name)  + te;
                            break;
                    }
                }
                catch (Exception)
                {


                }

            }
        }
        #endregion

        #region Sự kiện
        private void usc_lableTong_Load(object sender, EventArgs e)
		{
			 

		}

		private void lbl_DoubleClick(object sender, EventArgs e)
		{
			if (LBLDoubleClick != null)
			{
				LBLDoubleClick(sender, e);
			}

		}

		private void lbl_MouseUp(object sender, MouseEventArgs e)
		{
			

		}

		private void btnThayDoiMau_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();


			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				his_LabelX sourceControl = (his_LabelX)cmt_ThayDoiMau.SourceControl;
				int ID = (string.IsNullOrEmpty(sourceControl.Name) ? 1 : int.Parse(sourceControl.Name));
				string TenLoai = (sourceControl.Tag == null ? "Tất cả" : sourceControl.Tag.ToString());
				sourceControl.BackColor = colorDialog.Color;
				string _colorTrong = "" + colorDialog.Color.A + "," + colorDialog.Color.R + "," + colorDialog.Color.G + "," + colorDialog.Color.B + "";
				//update color
				Dictionary<string, string> dic2 = new Dictionary<string, string>();
				Dictionary<string, string> dic3 = new Dictionary<string, string>();
				dic2.Add(cls_DanhMucColorTrangThaiNew.col_Color, _colorTrong);
				dic3.Add(cls_DanhMucColorTrangThaiNew.col_Id, ID+"");
				string _userError = "", _systemError = "";

				if (!_api.Update(ref _userError, ref _systemError, cls_DanhMucColorTrangThaiNew.tb_TenBang, dic2, new List<string>(), dic3))
				{
					TA_MessageBox.MessageBox.Show("Không thể cập nhật màu!", TA_MessageBox.MessageIcon.Error);
					return;
				}

			}
            if (reload !=null)
            {
                reload(sender, e);
            }
            lbl_DoubleClick(lblTatCa, e);

        }

        #endregion

        private void btnAnKhoi_Click(object sender, EventArgs e)
        {
            his_LabelX sourceControl = (his_LabelX)cmt_ThayDoiMau.SourceControl;
           
            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            Dictionary<string, string> dic3 = new Dictionary<string, string>();
            dic2.Add(cls_DanhMucColorTrangThaiNew.col_HienThi, sourceControl.EnableMarkup?"0":"1");
            dic3.Add(cls_DanhMucColorTrangThaiNew.col_Id, sourceControl.Name + "");
            string _userError = "", _systemError = "";

            if (!_api.Update(ref _userError, ref _systemError, cls_DanhMucColorTrangThaiNew.tb_TenBang, dic2, new List<string>(), dic3))
            {
                TA_MessageBox.MessageBox.Show("Không thể cập nhật màu!", TA_MessageBox.MessageIcon.Error);
                return;
            }
            sourceControl.EnableMarkup = !sourceControl.EnableMarkup;
            if (reload != null)
            {
                reload(sender, e);
            }
            if (reloadGiuong != null)
            {
                reloadGiuong(sender, e);
            }
            
            //    btnAnKhoi.Checked = !btnAnKhoi.Checked;
        }
    }
}
