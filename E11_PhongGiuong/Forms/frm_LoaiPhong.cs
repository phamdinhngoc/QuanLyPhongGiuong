﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_Base;
using E00_Common;
using E00_Model;

using System.Text.RegularExpressions;
using DevComponents.DotNetBar.Controls;
using E00_Model;

namespace E11_PhongGiuong
{
	public partial class frm_LoaiPhong : frm_DanhMuc
	{
		#region Khai báo biến toàn cục

		private Acc_Oracle _acc = new Acc_Oracle();
		private Api_Common _api = new Api_Common();
		private List<string> _lst = new List<string>();
		private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
		private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
		private string _userError, _systemError, _id = string.Empty;
		private int _index, _check = 0, _deleteOneRecord = 0, _statusCheckAll = 0;
		private bool _isAdd;
		private List<string> _lstCheck = new List<string>();
        private List<string> _lstCheckMa = new List<string>();
        private List<string> _lstCheckHoTen = new List<string>();
		private DataTable _tbDanhSachDuong = new DataTable();
		public bool _statusCloseForm;
		private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
		private string _sql = string.Empty;
		private string _mauSacPhong = string.Empty;
		private DataTable _tbGiaGoiY = new DataTable();
		string _id2 = String.Empty;
		string _idDMLP = String.Empty;
		private string _str3 = String.Empty;
		private Dictionary<string, string> _lstGiaGiuong = new Dictionary<string, string>();

		#endregion

		#region Khởi tạo
		public frm_LoaiPhong()
		{
			InitializeComponent();
			_api.KetNoi();
			btnSua.Visible = true;//5
			btnIn.Visible = false;
			LoadDanhSachDoiTuong();
			LoadGiaGoiY();
		}
		#endregion

		#region Phương thức protected

		protected override void LoadData()
		{
			TimKiem();
			base.LoadData();
		}

		protected override void Them()
		{

			_isAdd = true;
			base.Them();
			ClearData();
			txtTenLoaiPhong.Focus();
			pnlControl2.Enabled = true;//2
			txtTenLoaiPhong.Focus();
		}

		protected override void Sua()
		{
			pnlControl2.Enabled = true;//1
			_isAdd = false;
			dgvDanhMucLoaiPhong_SelectionChanged(null, null);
			base.Sua();
		}

		protected override void Xoa()
		{
			try
			{
				ClearList();
				#region Delete each record
				if (_lstCheck.Count == 1 || _deleteOneRecord == 1)
				{

					string id = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_id"].Value.ToString();
					string idDelete = GetMaLoai();
					if (!CheckConCuaDanhMucLoaiPhong(id)) //Edited 300418
					{
						_lst2.Add(cls_DanhMucLoaiPhong.col_ID.ToUpper(), id.ToUpper());

						if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTenLoaiPhong.Text,

							TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						{
							if (!_api.Delete(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _lst2, null))
							{
								TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
										txtTenLoaiPhong.Text, _userError)
									 , TA_MessageBox.MessageIcon.Error);
								dgvDanhMucLoaiPhong.Focus();
								return;
							}
							
								_lst2.Clear();
								_lst2.Add(cls_GiaGiuong.col_MA.ToUpper(), idDelete.ToUpper());
								bool bl = _api.Delete(ref _userError, ref _systemError, cls_GiaGiuong.tb_TenBang, _lst2, null);
							
						}
					}
					else
					{
						TA_MessageBox.MessageBox.Show(string.Format("Loại phòng này đã được khai báo cho phòng.\n Không thể xóa! {0}. Lỗi: {1} !!!!",
									txtTenLoaiPhong.Text, _userError)
								 , TA_MessageBox.MessageIcon.Error);
						return;
					}
				}
				#endregion
				else
				{
                    #region Delete all record have checked

                    string strma = ""; string str = "", strHoTen = string.Empty, strKhongXoa ="";
					for (int i = 0; i < _lstCheck.Count; i++)
					{
						if (!CheckConCuaDanhMucLoaiPhong(_lstCheck[i])) //Edited 300418
						{
                            strma += _lstCheckMa[i].ToString() + ",";
                            str += _lstCheck[i].ToString() + ",";
							strHoTen += _lstCheckHoTen[i].ToString() + ",";
						}
						else
						{
							strKhongXoa += _lstCheckHoTen[i].ToString() + ",";
						}
					}
					StringBuilder str1 = new StringBuilder(str);
					StringBuilder str2 = new StringBuilder(strHoTen);
                    StringBuilder str3 = new StringBuilder(strma);
                    #region code cũ
                    /////append id
                    //foreach (var item in _lstCheck)
                    //{
                    //	str += item.ToString() + ",";
                    //	//strGiaGiuong = 

                    //}
                    //StringBuilder str1 = new StringBuilder(str);
                    //str1 = str1.Remove(str.Length - 1, 1);


                    /////append name
                    //foreach (var item in _lstCheckHoTen)
                    //{
                    //	strHoTen += item.ToString() + ",";
                    //}
                    //StringBuilder str2 = new StringBuilder(strHoTen);
                    //str2 = str2.Remove(str2.Length - 1, 1);
                    #endregion

                    if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa  " + str2.ToString().Trim(',') + " không?",

							TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						ClearList();
						_lst2.Add(cls_DanhMucLoaiPhong.col_ID.ToUpper(), str1.ToString().Trim(','));
						if (_api.DeleteAll(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _lst2, null))
						{
							ClearList();
							_lst2.Add(cls_GiaGiuong.col_MA.ToUpper(), str3.ToString().Trim(','));
							if (_api.DeleteAll(ref _userError, ref _systemError, cls_GiaGiuong.tb_TenBang, _lst2))
							{
								_lstCheck.Clear(); _lstCheckHoTen.Clear();_lstCheckMa.Clear();
								if (!string.IsNullOrEmpty(strKhongXoa))
								{
									TA_MessageBox.MessageBox.Show(string.Format("Đã xóa: {0} \n Không thể xóa: {1}. Vì Loại phòng đã được khai báo phòng!",
												str2, strKhongXoa)
											 , TA_MessageBox.MessageIcon.Error);
								}
							}
						}
						else
						{
							TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
									txtTenLoaiPhong.Text, _userError)
								 , TA_MessageBox.MessageIcon.Error);
							dgvDanhMucLoaiPhong.Focus();
							return;
						}
					}
					#endregion
				}
				LoadData();
				ClearData();
				base.Xoa();
			}
			catch
			{

				return;
			}

		}

		protected override void Luu()
		{

			if (_isAdd)
			{
				#region Insert data
				try
				{
					if (CheckDuLieu())
					{
						if (!TrungSoTT())
						{
							ClearList();
							ClearList();
							_idDMLP = _tT.GetTuDongIDDanhMucLoaiPhong();
							_lst2.Add(cls_DanhMucLoaiPhong.col_MA, _idDMLP);
							_lst2.Add(cls_DanhMucLoaiPhong.col_STT, itgSTT.Value.ToString());
							_lst2.Add(cls_DanhMucLoaiPhong.col_TEN, txtTenLoaiPhong.Text);
							_lst2.Add(cls_DanhMucLoaiPhong.col_NGAYUD, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
							_lst2.Add(cls_DanhMucLoaiPhong.col_MACHINEID, GetMachine());
							_lst2.Add(cls_DanhMucLoaiPhong.col_MAUSAC, _mauSacPhong);
							_lst.Add(cls_DanhMucLoaiPhong.col_MA);


							if (!_api.Insert(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang,
								_lst2, _lst, _lst))
							{
								TA_MessageBox.MessageBox.Show("Không thể thêm!"
								 , TA_MessageBox.MessageIcon.Error);
								return;
							}

							
							_tT.InsertGiaGiuong( _idDMLP, "0", slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);

							_check = 1;
							LoadData();
						}
						else
						{
							TA_MessageBox.MessageBox.Show("Trùng số thứ tự!"
							 , TA_MessageBox.MessageIcon.Error);
							return;
						}
					}
				}
				catch
				{
					return;
				}
				#endregion
				if (_check == 1)
				{
					base.Luu();
				}
			}
			else
			{
				#region Update data
				try
				{
					if (CheckDuLieu())
					{
						ClearList();
						_lst2.Add(cls_DanhMucLoaiPhong.col_STT, itgSTT.Value.ToString());
						_lst2.Add(cls_DanhMucLoaiPhong.col_TEN, txtTenLoaiPhong.Text);
						_lst2.Add(cls_DanhMucLoaiPhong.col_MAUSAC, _mauSacPhong);
						string id = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_id"].Value.ToString();
						_lst3.Add(cls_DanhMucLoaiPhong.col_ID, id.ToUpper());
                        string idUpdate = GetMaLoai();

						if (idUpdate == null)
						{
							TA_MessageBox.MessageBox.Show(string.Format("Không thể lấy thông tin giuong", txtTenLoaiPhong.Text, _userError)
								 , TA_MessageBox.MessageIcon.Error);
                            return;
						}

						_tT.UpdateGiaGiuong(idUpdate,"0", slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);

						if (!_api.Update(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _lst2, new List<string>(), _lst3))
						{
							TA_MessageBox.MessageBox.Show(string.Format("Không thể cập nhật {0}. Lỗi: {1} !!!!", txtTenLoaiPhong.Text, _userError)
							 , TA_MessageBox.MessageIcon.Error);
							txtTenLoaiPhong.Focus();
							return;
						}
						LoadData();
						_check = 1;
					}
				}
				catch
				{
					return;
				}
				#endregion
				if (_check == 1)
				{
					base.Luu();
				}
			}
		}

		protected override void BoQua()
		{
			pnlControl2.Enabled = false;//4
			base.BoQua();
		}

		protected override void TimKiem()
		{
			try
			{
				ClearList();
				_chuoiTimKiem = txtTimKiem.Text;
				_lst.Add(cls_DanhMucLoaiPhong.col_ID);
				_lst.Add(cls_DanhMucLoaiPhong.col_STT);
				_lst.Add(cls_DanhMucLoaiPhong.col_MA);
				_lst.Add(cls_DanhMucLoaiPhong.col_TEN);
				Dictionary<string, string> lst3 = new Dictionary<string, string>();
				lst3.Add(cls_DanhMucLoaiPhong.col_MA, _chuoiTimKiem);
				lst3.Add(cls_DanhMucLoaiPhong.col_TEN, _chuoiTimKiem);
                dataGridViewGme_dgvDanhMucLoaiPhong.DataSource = _tT.GetDataTimKiem(_chuoiTimKiem.ToUpper());
				_count = dgvDanhMucLoaiPhong.RowCount;
				col_MaPhong.DataPropertyName = "MA";
				col_TenLoaiPhong.DataPropertyName = "TEN";
				col_id.DataPropertyName = "ID";
				col_STT.DataPropertyName = "STT";
				base.TimKiem();
				pnlControl2.Enabled = false;//3
			}
			catch
			{
				return;
			}
		}

		protected override void Thoat()
		{
			base.Thoat();
		}

		#endregion

		#region Phương thức private

		#region Kiểm tra trùng số thứ tự

		private bool TrungSoTT()
		{
			_sql = string.Format("select STT from {0}.{1} where STT = {2}", _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang, itgSTT.Value);
			if (_acc.Get_Data(_sql).Rows.Count > 0)
			{
				return true;
			}
			return false;
		}

		#endregion

		#region Xóa dữ liệu trước khi thêm
		private void ClearData()
		{
			txtTenLoaiPhong.Text = string.Empty;
			try
			{
				string sql = string.Format("select max(stt)+1 from {0}.{1}", _acc.Get_User(),
					cls_DanhMucLoaiPhong.tb_TenBang);
				DataTable tb = _acc.Get_Data(sql);
				itgSTT.Value = int.Parse(_tT.GetTuDongSTTDanhMucLoaiPhong());
			}
			catch { itgSTT.Value = 1; }
			itgSTT.Focus();
            slbDoiTuong.clear();
            usc_GiaGoiY.clear();
            txtGiaTien.Text = "";
        }
		#endregion

		#region Xóa list
		private void ClearList()
		{
			_lst.Clear();
			_lst2.Clear();
			_lst3.Clear();
		}
		#endregion

		#region Kiểm tra dữ liệu nhập vào
		private bool CheckDuLieu()
		{
			if (itgSTT.Value == 0 || itgSTT.Value.ToString() == string.Empty)
			{
				TA_MessageBox.MessageBox.Show("Nhập số thứ tự!"
			 , TA_MessageBox.MessageIcon.Error);
				return false;
			}
			if (string.IsNullOrEmpty(txtTenLoaiPhong.Text))
			{
				TA_MessageBox.MessageBox.Show("Nhập tên loại phòng!"
			 , TA_MessageBox.MessageIcon.Error);
				return false;
			}
			return true;
		}
		#endregion

		#region Lấy tên máy
		private string GetMachine()
		{
			_sql = string.Format("select SYS_CONTEXT('USERENV','IP_ADDRESS')||'+'||Userenv('TERMINAL')||'+'||SYS_CONTEXT('USERENV','MODULE') from dual");
			return _acc.Get_Data(_sql).Rows[0][0].ToString();
		}
		#endregion

		#region Kiểm tra ràng buộc của loại phòng
		private bool CheckConCuaDanhMucLoaiPhong(string idLoaiPhong)
		{
			_sql = string.Format("select  * from {0}.{2} where {3}='{1}'", _acc.Get_User(), idLoaiPhong,cls_DanhMucPhong.tb_TenBang, cls_DanhMucPhong.col_LOAI);
			if (_acc.Get_Data(_sql).Rows.Count > 0)
			{
				return true;
			}
			return false;
		}
		#endregion

		#region Hàm bỏ dấu, ký tự đặc biệt
		public string ExportNameControl(string name)
		{
			string NameColumn = "";
			NameColumn = E00_Common.cls_Common.BoDau(name.Replace(" ", "").Replace("-", "")).Trim();
			NameColumn = Regex.Replace(NameColumn, "[(|,.:'?<>+*/)]", "_");
			long n;
			if (NameColumn.Length > 0 && long.TryParse(NameColumn.Substring(0, 1), out n))
			{
				NameColumn = "_" + NameColumn;
			}
			return NameColumn;
		}
		#endregion

		#region Lấy danh sách đối tượng

		private void LoadDanhSachDoiTuong()
		{
			slbDoiTuong.DataSource = _tT.GetDataDanhSachDoiTuong();
		}
		#endregion

		#region Lấy giá gợi ý
		private void LoadGiaGoiY()
		{
			usc_GiaGoiY.Enabled = true;
			_tbGiaGoiY = _tT.GetDataGiaGoiY();
			usc_GiaGoiY.DataSource = _tbGiaGoiY;
		}
		#endregion

		#endregion

		#region Lấy mã loại từ dòng được chọn
		public string GetMaLoai()
		{
			string ma = "";
			if (dgvDanhMucLoaiPhong.CurrentRow != null&&!_isAdd)
			{
				var row = dgvDanhMucLoaiPhong.CurrentRow.DataBoundItem as DataRowView;
				if (row != null)
				{
					ma = "" + row.Row["MA"];
				}
			}
			return ma;

		}
		#endregion

		#region Sự kiện

		private void dgvDanhMucLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				_index = dgvDanhMucLoaiPhong.CurrentRow.Index;

				string command = dgvDanhMucLoaiPhong.Columns[e.ColumnIndex].Name;
				if (command == "col_Sua")
				{
					Sua();
				}
				if (command == "col_Xoa")
				{
					//_deleteOneRecord = _lstCheck.Count;
					_deleteOneRecord = 1;
					Xoa();
				}
				
				if (command == "col_Chon")
				{
					DataGridViewRow dr = (DataGridViewRow)dgvDanhMucLoaiPhong.Rows[_index];
					DataGridViewCheckBoxCell chks = (DataGridViewCheckBoxCell)dr.Cells[0];
					string id = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_id"].Value.ToString();
                    string Ma = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_MaPhong"].Value.ToString();
                    string ten = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_TenLoaiPhong"].Value.ToString();

					if (chks.Selected && chks.FormattedValue + "" == "True" && !_lstCheck.Contains(id))
					{
						_lstCheck.Add(id);
                        _lstCheckMa.Add(Ma);

                        _lstCheckHoTen.Add(ten);
					}
					if (!chks.Selected && chks.FormattedValue + "" == "False")
					{
						_lstCheck.Remove(id);
						_lstCheckHoTen.Remove(ten);
                        _lstCheckMa.Remove(Ma);
                        _statusCheckAll = 1;
						ckAll.Checked = false;
					}
				}
			}
			catch
			{
				return;
			}
		}

		private void dgvDanhMucLoaiPhong_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				_index = dgvDanhMucLoaiPhong.CurrentRow.Index;
				DataTable tb = ((DataTable)dgvDanhMucLoaiPhong.DataSource);
				DataRow row = tb.Rows[_index];
				txtTenLoaiPhong.Text = dgvDanhMucLoaiPhong[col_TenLoaiPhong.Name, dgvDanhMucLoaiPhong.SelectedRows[0].Index].Value + "";
				itgSTT.Value = int.Parse(dgvDanhMucLoaiPhong[col_STT.Name, dgvDanhMucLoaiPhong.SelectedRows[0].Index].Value + "");
				//slbDoiTuong.SetTenByMa(dgvDanhMucLoaiPhong[col_Doituong.Name, dgvDanhMucLoaiPhong.SelectedRows[0].Index].Value + "");
				//usc_GiaGoiY.SetTenByMa(dgvDanhMucLoaiPhong[col_GoiYGia.Name, dgvDanhMucLoaiPhong.SelectedRows[0].Index].Value + "");
				//txtGiaTien.Text = dgvDanhMucLoaiPhong[col_GiaTien.Name, dgvDanhMucLoaiPhong.SelectedRows[0].Index].Value + "";

				string[] mauSacPhong = row["MAUSAC"].ToString().Split(',');
				try
				{
					cPBMauSac.SelectedColor = Color.FromArgb(int.Parse(mauSacPhong[0].ToString()), int.Parse(mauSacPhong[1].ToString()), int.Parse(mauSacPhong[2].ToString()), int.Parse(mauSacPhong[3].ToString()));
				}
				catch
				{
				}
			}
			catch
			{
				return;
			}
		}

		private void itgSTT_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				SendKeys.Send("{Tab}");
			}
		}

		private void usc_GiaGoiY_HisSelectChange(object sender, EventArgs e)
		{
            GetGia();
            //txtGiaTien.DataBindings.Clear();
            //DataTable dt = _tT.GetDataGia(usc_GiaGoiY.txtMa.Text);
            //txtGiaTien.DataBindings.Add("Text", dt, "GIA_TH");
            //txtGiaTien.Text = double.Parse(txtGiaTien.Text).ToString("#,##0");
        }
        private void GetGia()
        {
            if (!string.IsNullOrEmpty(usc_GiaGoiY.txtMa.Text) && !string.IsNullOrEmpty(slbDoiTuong.txtMa.Text))
            {
                txtGiaTien.Text = double.Parse(_tT.GetGiaTienTuGoiY(usc_GiaGoiY.txtMa.Text, slbDoiTuong.txtMa.Text, GetMaLoai(),0)).ToString("#,##0");
                return;
            }
            txtGiaTien.Text = "";
        }

        private void usc_GiaGoiY_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				SendKeys.Send("{Tab}");
			}
		}

		private void txtGiaTien_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				SendKeys.Send("{Tab}");
			}
		}

		private void txtTenLoaiPhong_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				SendKeys.Send("{Tab}");
			}
		}

		private void pnlControl2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void dgvDanhMucLoaiPhong_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == col_Chon.Index)
				{
					string id = dgvDanhMucLoaiPhong.Rows[e.RowIndex].Cells["col_id"].Value.ToString();
					string ten = dgvDanhMucLoaiPhong.Rows[e.RowIndex].Cells["col_TenLoaiPhong"].Value.ToString();
                    string Ma = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_MaPhong"].Value.ToString();
                    DataGridViewRow dr = (DataGridViewRow)dgvDanhMucLoaiPhong.Rows[_index];
					DataGridViewCheckBoxCell chks = (DataGridViewCheckBoxCell)dr.Cells[0];
					if (dgvDanhMucLoaiPhong[col_Chon.Index, e.RowIndex].Value + "" == "True" && !_lstCheck.Contains(id))
					{
						_lstCheck.Add(id);
                        _lstCheckMa.Add(Ma);

                        _lstCheckHoTen.Add(ten);
					}
					else
					{
						_lstCheck.Remove(id);
						_lstCheckHoTen.Remove(ten);
                        _lstCheckMa.Remove(Ma);
                        _statusCheckAll = 1;
						ckAll.Checked = false;
					}
				}
			}
			catch
			{ }
		}

        private void slbDoiTuong_HisSelectChange(object sender, EventArgs e)
        {
            GetGia();
        }

        private void txtGiaTien_Validated(object sender, EventArgs e)
        {
            try
            {
                txtGiaTien.Text = double.Parse(txtGiaTien.Text).ToString("#,##0");
            }
            catch (Exception ex)
            {


            }
        }

        private void frm_LoaiPhong_Load(object sender, EventArgs e)
        {
            dataGridViewGme_dgvDanhMucLoaiPhong.Initialize();
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (ckAll.Checked)
				{
					_statusCheckAll = 0;
					foreach (DataGridViewRow item in dgvDanhMucLoaiPhong.Rows)
					{
						DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)item.Cells[0];
						chk.Value = true;
						_lstCheck.Add(item.Cells["col_id"].Value.ToString());
                        _lstCheckMa.Add(item.Cells["col_MaPhong"].Value.ToString());

                    }
				}
				else if (!ckAll.Checked && _statusCheckAll == 0)
				{
					foreach (DataGridViewRow item in dgvDanhMucLoaiPhong.Rows)
					{
						DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)item.Cells[0];
						chk.Value = false;
						_lstCheck.Remove(item.Cells["col_id"].Value.ToString());
                        _lstCheckMa.Remove(item.Cells["col_MaPhong"].Value.ToString());
                    }
				}
			}
			catch
			{

				return;
			}
		}

		private void btnImportExcel_Click(object sender, EventArgs e)
		{
			frm_LoaiPhongImport frm = new frm_LoaiPhongImport();
			frm.ShowDialog();
		}

		private void cPBMauSac_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnLuu.Focus();
			}
		}

		private void cPBMauSac_SelectedColorChanged(object sender, EventArgs e)
		{
			//Làm sau
			Color c = cPBMauSac.SelectedColor;
			_mauSacPhong = c.A + "," + c.R + "," + c.G + "," + c.B + "";
		}

		private void frm_LoaiPhong_FormClosed(object sender, FormClosedEventArgs e)
		{
			_statusCloseForm = true;
		}

		#endregion
	}
}