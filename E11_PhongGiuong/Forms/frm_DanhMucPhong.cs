﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using E00_Common;

using DevComponents.DotNetBar.Controls;
using E00_Model;
using System.Globalization;
using System.Threading;
using E00_Base;

namespace E11_PhongGiuong
{
    public partial class frm_DanhMucPhong : frm_DanhMuc
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
        private List<string> _lstCheckHoTen = new List<string>();
        private DataTable _tbDanhSachDuong = new DataTable();
        public bool _statusCloseForm;
        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private string _sql = string.Empty;
		private DataTable _tbGiaGoiY = new DataTable();
		private string _curID = String.Empty;

		private Dictionary<string, string> _lstCheckID = new Dictionary<string, string>();
		private Dictionary<string, string> _lstGiaGiuong = new Dictionary<string, string>();



		#endregion

		#region Khởi tạo

		public frm_DanhMucPhong()
        {
            InitializeComponent();
            _api.KetNoi();
            btnIn.Visible = false;

			LoadDanhSachDoiTuong();
			LoadGiaGoiY();
        }

        #endregion

        #region Phương thức protected

        protected override void LoadData()
        {
            TimKiem();
            //LoaiVP();
            LoadLoai();
            LoadDanhMucKhoaPhong();
            //LoadChuan();
            base.LoadData();
        }

        protected override void Them()
        {
            _isAdd = true;
            _curID = "";
            ClearData();
            pnlControl2.Enabled = true;//2
            txtMa.Enabled = true;
            txtGiaTien.Enabled = false;
            //pnlControl2.Focus();
            //usc_SelectBoxKhoa.txtTen.Focus();
            slbKhoa.txtTen.Focus();
            base.Them();


        }

        protected override void Sua()
        {
            _isAdd = false;
            base.Sua();
            pnlControl2.Enabled = true;//1
            txtMa.Enabled = true;
            txtGiaTien.Enabled = true;
            dgvDanhMucPhong.Enabled = false;
            slbDoiTuong.clear();
            usc_GiaGoiY.clear();
            txtGiaTien.Text = "";
        }

        protected override void Xoa()
        {
            try
            {
                ClearList();
				#region Delete each record
				string ma = dgvDanhMucPhong.Rows[_index].Cells["col_MaKhoaPhong"].Value.ToString();
				string id = dgvDanhMucPhong.Rows[_index].Cells["col_id"].Value.ToString();
				if (_deleteOneRecord == 1)
				{
					
					if (!CheckConCuaDanhMucPhong(id))//Edited 300418
					{

						_lst2.Add(cls_DanhMucPhong.col_ID.ToUpper(), id.ToUpper());
						//_lstCheckID.Add(cls_GiaGiuong.col_MA, ma.ToUpper());
						if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTen.Text,

							TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						{
							
							if (!_api.Delete(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _lst2))
							{
									TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
										txtTen.Text, _userError)
									 , TA_MessageBox.MessageIcon.Error);
									dgvDanhMucPhong.Focus();
									return;
							}

                            _lst2.Clear();
                            _lst2.Add(cls_GiaGiuong.col_MA.ToUpper(), id.ToUpper());
                            try
                            {
                                bool bl = _api.Delete(ref _userError, ref _systemError, cls_GiaGiuong.tb_TenBang, _lst2, null);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
					}
					else
					{
						TA_MessageBox.MessageBox.Show("Ràng buộc dữ liệu.\n Không thể xóa!"
					 , TA_MessageBox.MessageIcon.Error);
						return;
					}
				}
				#endregion
				else
				{
					#region Delete all record have checked

					string str = "", strHoTen = string.Empty, strKhongXoa = "", strID = "";

					for (int i = 0; i < _lstCheck.Count; i++)
					{
						if (!CheckConCuaDanhMucPhong(_lstCheck[i])) //Edited 300418
						{
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
					StringBuilder str3 = new StringBuilder(strID);

					

					if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa  " + str2.ToString().Trim(',') + " không?",

							TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						ClearList();
						_lst2.Add(cls_DanhMucPhong.col_ID.ToUpper(), str.ToString().Trim(','));
						if (_api.DeleteAll(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _lst2, null))
						{
							ClearList();
							_lst2.Add(cls_GiaGiuong.col_MA.ToUpper(), str1.ToString().Trim(','));
                            if (_api.DeleteAll(ref _userError, ref _systemError, cls_GiaGiuong.tb_TenBang, _lst2))
							{
								_lstCheck.Clear(); _lstCheckHoTen.Clear();
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
									txtTen.Text, _userError)
                                    
                                 , TA_MessageBox.MessageIcon.Error);
							dgvDanhMucPhong.Focus();
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
            if (string.IsNullOrEmpty(slbKhoa.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn khoa!!", TA_MessageBox.MessageIcon.Information);
                slbKhoa.Select();
                return;
            }
            if (string.IsNullOrEmpty(slbLoai.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn loại phòng!!", TA_MessageBox.MessageIcon.Information);
                slbLoai.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập mã phòng!!", TA_MessageBox.MessageIcon.Information);
                txtMa.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập tên phòng!!", TA_MessageBox.MessageIcon.Information);
                txtTen.Select();
                return;
            }
            btnThem.Focus();
            if (_isAdd)
            {
                #region Insert data
                try
                {
                    string id = _tT.GetIdDanhMucPhongTuDong();
                    try
                    {
                        _tT.InsertGiaGiuong(id, "1", slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);
                    }
                    catch (Exception)
                    {

                      
                    }
					if (!_tT.TrungSoTTKhoaPhong(slbKhoa.txtMa.Text, itgSTT.Value.ToString()))//Edited 180531
                    {
                      //TT.GetSysDate().ToString("yyyyMMddHHmmss");//
                        _tT.InsertDanhMucPhong(slbKhoa.txtMa.Text, id, itgSTT.Value.ToString(), id,
                            txtTen.Text, slbLoai.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaPhong.Text.Replace(",", ""), _tT.GetSysDate(), GetMachine(), txtMa.Text);
                        _check = 1;
                        LoadData();
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
                    dgvDanhMucPhong.Enabled = true;
                }
            }
            else
            {
                #region Update data
                try
                {
                    ClearList();
                    _lst2.Add(cls_DanhMucPhong.col_TEN, txtTen.Text);
                    //_lst2.Add(cls_DanhMucPhong.col_MA, txtMa.Text);
                    if (!_tT.TrungSoTTKhoaPhong(slbKhoa.txtMa.Text, itgSTT.Value.ToString()))
                    {
                        _lst2.Add(cls_DanhMucPhong.col_STT, itgSTT.Value.ToString());
                    }
                    _lst2.Add(cls_DanhMucPhong.col_LOAI, slbLoai.txtMa.Text);
                    _lst2.Add(cls_DanhMucPhong.col_LOAIVP, usc_GiaGoiY.txtMa.Text);
                    _lst2.Add(cls_DanhMucPhong.col_MAKP, slbKhoa.txtMa.Text);


                    //_lst2.Add(cls_DanhMucPhong.col_CHUAN, usc_Chuan.txtMa.Text);
                    string id = GetMaLoai();
                    _lst3.Add(cls_DanhMucPhong.col_ID, id.ToUpper());

					
					if (!_api.Update(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _lst2, new List<string>(), _lst3))
					{
						TA_MessageBox.MessageBox.Show(string.Format("Không thể cập nhật {0}. Lỗi: {1} !!!!", txtTen.Text, _userError)
						 , TA_MessageBox.MessageIcon.Error);
						txtTen.Focus();
						return;
					}


                    try
                    {
                        _tT.UpdateGiaGiuong(id, "1", slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);
                    }
                    catch (Exception)
                    {

                        
                    }
					
                    LoadData();
                    _check = 1;

                }
                catch
                {
                    return;
                }
                #endregion
                if (_check == 1)
                {
                    base.Luu();
                    dgvDanhMucPhong.Enabled = true;
                }
            }
        }

        protected override void BoQua()
        {
            pnlControl2.Enabled = false;//4
            base.BoQua();
            dgvDanhMucPhong.Enabled = true;
        }

        protected override void TimKiem()
        {
            try
            {
                ClearList();
                _chuoiTimKiem = txtTimKiem.Text;
                _lst.Add(cls_DanhMucPhong.col_ID);
                _lst.Add(cls_DanhMucPhong.col_STT);
                _lst.Add(cls_DanhMucPhong.col_MA);
                _lst.Add(cls_DanhMucPhong.col_TEN);
                _lst.Add(cls_DanhMucPhong.col_LOAI);
                _lst.Add(cls_DanhMucPhong.col_LOAIVP);
                Dictionary<string, string> lst3 = new Dictionary<string, string>();
                lst3.Add(cls_DanhMucPhong.col_MA, _chuoiTimKiem);
                lst3.Add(cls_DanhMucPhong.col_TEN, _chuoiTimKiem);
                DataTable tb = _acc.Get_Data(_tT.QueryTimKiemDanhMucPhong(slbKhoaTimKiem.txtMa.Text, _chuoiTimKiem));

                dataGridViewGme_dgvDanhMucPhong.DataSource = tb;
                if (tb != null && tb.Rows.Count > 0)
                {
                    DataTable dt = (tb.Copy()).AsDataView().ToTable(true, cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP);
                    DataRow drPhong = dt.NewRow();
                    drPhong[cls_BTDKP_BV.col_MaKP] = "-1";
                    drPhong[cls_BTDKP_BV.col_TenKP] = "Tất cả";
                    dt.Rows.Add(drPhong);
                    slbKhoaTimKiem.DataSource = dt;

                }
                _count = dgvDanhMucPhong.RowCount;
                col_MaKhoaPhong.DataPropertyName = "MAPHONG";//Sửa mã phòng
                col_TenPhong.DataPropertyName = "TEN";
                col_Loai.DataPropertyName = "LOAI";
                col_TenLoaiPhong.DataPropertyName = "TENLOAIPHONG";
                col_LoaiVP.DataPropertyName = "LOAIVP";
                col_TenLoaiVienPhi.DataPropertyName = "TenLoaiVienPhi";
                col_id.DataPropertyName = "ID";
                col_STT.DataPropertyName = "STT";
                col_MaKP.DataPropertyName = "MAKP";
                col_TenKP.DataPropertyName = "TENKP";
                // col_Chuan.DataPropertyName = cls_DanhMucPhong.col_CHUAN;
                Filter();
                dgvDanhMucPhong.ClearSelection();
                if (dgvDanhMucPhong != null && dgvDanhMucPhong.Rows.Count > 0)
                {
                    foreach (DataGridViewRow ritem in dgvDanhMucPhong.Rows)
                    {
                        if (ritem.Cells["col_id"].Value + "" == _curID)
                        {
                            ritem.Selected = true;
                            return;
                        }
                    }
                }
                base.TimKiem();
                //lblCount.Text = _count.ToString();
                pnlControl2.Enabled = false;//3
            }
            catch
            {
                return;
            }
        }
        private void Filter()
        {
            string timkiem = "";
            if (!string.IsNullOrEmpty(slbKhoaTimKiem.txtMa.Text) && slbKhoaTimKiem.txtMa.Text != "-1")
                timkiem += string.Format(" AND {1} = '{0}' ", slbKhoaTimKiem.txtMa.Text, "MAKP");

            if (!string.IsNullOrEmpty(slbPhongTimKiem.txtMa.Text) && slbPhongTimKiem.txtMa.Text != "-1")
                timkiem += string.Format(" AND {1} = '{0}' ", slbPhongTimKiem.txtMa.Text, "MAPHONG");

            if (!string.IsNullOrEmpty(timkiem)) (dgvDanhMucPhong.DataSource as DataTable).DefaultView.RowFilter = timkiem.Substring(4);
            else (dgvDanhMucPhong.DataSource as DataTable).DefaultView.RowFilter = "";
        }
        protected override void Thoat()
        {
            base.Thoat();
        }

        #endregion

        #region Phương thức private

        #region Gán source cho control khoa phòng
        private void LoadDanhMucKhoaPhong()
        {
            try
            {
                slbKhoa.DataSource = _acc.Get_Data(_tT.QueryDanhMucKhoaPhong());
            }
            catch
            {
                return;
            }

        } 
        #endregion

        #region Load loại phòng
        private void LoadLoai()
        {
            try
            {
                ClearList();
                _lst.Add(cls_DanhMucLoaiPhong.col_ID);
                _lst.Add(cls_DanhMucLoaiPhong.col_TEN);
                slbLoai.DataSource = _api.Search(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _acc.Get_User(), -1, _lst, orderByASC1: true, orderByName1: cls_DanhMucLoaiPhong.col_STT);
            }
            catch
            {
                return;
            }
        }
		#endregion

		//#region Set source cho danh mục Chuẩn
		//private void LoadChuan()
		//{
		//    try
		//    {
		//        DataTable tb = new DataTable();
		//        DataColumn cl;
		//        for (int i = 0; i < 2; i++)
		//        {
		//            cl = new DataColumn();
		//            cl.ColumnName = "cl" + i;
		//            tb.Columns.Add(cl);
		//        }
		//        DataRow dr = tb.NewRow();
		//        dr[0] = "T";
		//        dr[1] = "T";
		//        tb.Rows.Add(dr);
		//        dr = tb.NewRow();
		//        dr[0] = "G";
		//        dr[1] = "G";
		//        tb.Rows.Add(dr);
		//        usc_Chuan.DataSource = tb;
		//    }
		//    catch
		//    {
		//        return;
		//    }
		//}
		//#endregion

		#region Load danh sách đối tượng
		private void LoadDanhSachDoiTuong()
		{
			slbDoiTuong.DataSource = _tT.GetDataDanhSachDoiTuong();
		}
		#endregion

		#region Lấy giá gợi ý
		private void LoadGiaGoiY()
		{
			usc_GiaGoiY.Enabled = true;
			usc_GiaGoiY.DataSource = _tT.GetDataGiaGoiY();
			//DataTable dt = _tT.GetDataGiaGoiY();
			//txtGiaTien.Text = dt.Columns["gia"].ToString();
		}
		#endregion

		#region Load loại viện phí
		
        #endregion

        #region Xóa trắng dữ liệu để thêm mới
        private void ClearData()
        {
            slbKhoa.txtMa.Text = slbKhoa.txtTen.Text = slbLoai.txtMa.Text
                = slbLoai.txtTen.Text = usc_SelectBoxLoaiGiaVienPhi.txtTen.Text = string.Empty;
            usc_GiaGoiY.clear();
            txtTen.Text = txtGiaPhong.Text = usc_SelectBoxLoaiGiaVienPhi.txtMa.Text = string.Empty;
            try
            {

                string sql = string.Format("select max(stt) from {0}.{1}", _acc.Get_User(),
                    cls_DanhMucPhong.tb_TenBang);
                DataTable tb = _acc.Get_Data(sql);
                itgSTT.Value = int.Parse(tb.Rows[0].ItemArray[0].ToString())+1;
            }
            catch { itgSTT.Value = 1; }
            txtTen.Text = "";
            slbDoiTuong.clear();
            usc_GiaGoiY.clear();
            txtGiaTien.Text = "";
            //itgSTT.Focus();
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

        #region Kiểm tra tồn tại của chi tiết danh mục
        private bool CheckConCuaDanhMucPhong(string idPhong)
        {
            _sql = string.Format("select  * from {0}.PG_DANHMUCGIUONG where idphong='{1}'", _acc.Get_User(), idPhong);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        } 
        #endregion

        #region Chuyển chữ cái đầu viết hoa
        private string ToTitleCase(string text)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        } 
        #endregion

        #region Lấy datarow theo điều kiện
        public DataRow getRowByID(DataTable dt, string exp)
        {
            try
            {
                DataRow[] r = dt.Select(exp);
                if (r.Length > 0)
                    return r[0];
                else return null;
            }
            catch { return null; }
        }
        
        #endregion

        #region Lấy tên máy
        private string GetMachine()
        {
            _sql = string.Format("select SYS_CONTEXT('USERENV','IP_ADDRESS')||'+'||Userenv('TERMINAL')||'+'||SYS_CONTEXT('USERENV','MODULE') from dual");
            return _acc.Get_Data(_sql).Rows[0][0].ToString();
        }
        #endregion

        #endregion

        #region Sự kiện

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
			//try
			//{
			//    if (ckAll.Checked)
			//    {
			//        _statusCheckAll = 0;
			//        foreach (DataGridViewRow item in dgvDanhMucPhong.Rows)
			//        {
			//            DataGridViewCheckBoxXCell chk = (DataGridViewCheckBoxXCell)item.Cells[0];
			//            chk.Value = true;
			//            _lstCheck.Add(item.Cells["col_id"].Value.ToString());
			//            _lstCheckHoTen.Add(item.Cells["col_TenPhong"].Value.ToString());
			//        }
			//    }
			//    else if (!ckAll.Checked && _statusCheckAll == 0)
			//    {
			//        foreach (DataGridViewRow item in dgvDanhMucPhong.Rows)
			//        {
			//            DataGridViewCheckBoxXCell chk = (DataGridViewCheckBoxXCell)item.Cells[0];
			//            chk.Value = false;
			//            _lstCheck.Remove(item.Cells["col_id"].Value.ToString());
			//            _lstCheckHoTen.Remove(item.Cells["col_TenPhong"].Value.ToString());
			//        }
			//    }
			//}
			//catch
			//{

			//    return;
			//}

			

		}

        private void dgvDanhMucPhong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _index = dgvDanhMucPhong.CurrentRow.Index;
                DataTable tb = ((DataTable)dgvDanhMucPhong.DataSource);
                DataRow row = tb.Rows[_index];
                txtTen.Text = row["TEN"].ToString();
                slbLoai.SetTenByMa (row["LOAI"].ToString());
                //usc_GiaGoiY.txtMa.Text = row["LOAIVP"].ToString();
                //DataTable tbLoaiVienPhi = (DataTable)usc_GiaGoiY.DataSource;
                //DataRow drLoaiVP = getRowByID(tbLoaiVienPhi, "id = " + row["LOAIVP"].ToString());
                //usc_GiaGoiY.txtTen.Text = drLoaiVP["Ten"].ToString();
                itgSTT.Value = int.Parse(row["STT"].ToString());
                DataTable tbLoai = (DataTable)slbLoai.DataSource;
				DataRow dr = getRowByID(tbLoai, "id = " + row["LOAI"].ToString());
				//slbLoai.SetTenByMa(dr["ID"].ToString());
				slbLoai.SetTenByMa(dr["ID"].ToString());

				DataTable tbDoiTuong = (DataTable)slbDoiTuong.DataSource;
				DataRow rowDT = tb.Rows[_index];
				//DataRow drDoiTuong = getRowByID(tb, "id = " + row["doituong"] + "");
				//slbDoiTuong.SetTenByMa(dr["doituong"] + "");
				


				txtTen.Text = dgvDanhMucPhong[col_TenPhong.Name, dgvDanhMucPhong.SelectedRows[0].Index].Value + "";
				itgSTT.Value = int.Parse(dgvDanhMucPhong[col_STT.Name, dgvDanhMucPhong.SelectedRows[0].Index].Value + "");

				//slbDoiTuong.SetTenByMa(dgvDanhMucPhong[col_Doituong.Name, dgvDanhMucPhong.SelectedRows[0].Index].Value + "");
				//usc_GiaGoiY.SetTenByMa(dgvDanhMucPhong[col_GoiYGia.Name, dgvDanhMucPhong.SelectedRows[0].Index].Value + "");
				//txtGiaTien.Text = dgvDanhMucPhong[col_GiaTien.Name, dgvDanhMucPhong.SelectedRows[0].Index].Value + "";

				


				if (string.IsNullOrEmpty(slbKhoa.txtMa.Text))
                {
                    slbLoai.txtMa.Text = slbLoai.txtTen.Text = "";
                    txtTen.Text = "";
                    itgSTT.Value = 0;
                }
            }
            catch(Exception ex)
            {
                return;
            }
        }

        //Lấy giá giường
        public string GetMaLoai()
        {
            string ma = "";
            if (dgvDanhMucPhong.CurrentRow != null && !_isAdd)
            {
                var row = dgvDanhMucPhong.CurrentRow.DataBoundItem as DataRowView;
                if (row != null)
                {
                    ma = "" + row.Row["ID"];
                }
            }
            return ma;

        }

        private void dgvDanhMucPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

			
               
                _index = dgvDanhMucPhong.CurrentRow.Index;
				slbKhoa.txtTen.Text = dgvDanhMucPhong.Rows[_index].Cells["col_TenKP"].Value.ToString();
				txtMa.Text = dgvDanhMucPhong.Rows[_index].Cells["col_MaKhoaPhong"].Value.ToString();
				txtTen.Text = dgvDanhMucPhong.Rows[_index].Cells["col_TenPhong"].Value.ToString();

				slbLoai.SetTenByMa(dgvDanhMucPhong.Rows[_index].Cells["col_Loai"].Value.ToString());
				
                try
                {
                  
					slbKhoa.SetTenByMa(dgvDanhMucPhong.Rows[_index].Cells["col_MaKP"].Value + "");

				}
                catch 
                {
                }

                try
                {
                    txtMa.Text = dgvDanhMucPhong.Rows[_index].Cells["col_MaKhoaPhong"].Value.ToString();
                }
                catch 
                {
                }

                itgSTT.Value = int.Parse(dgvDanhMucPhong.Rows[_index].Cells["col_STT"].Value.ToString());

                string command = dgvDanhMucPhong.Columns[e.ColumnIndex].Name;
                if (command == "col_Sua")
                {
                    Sua();
                }
                if (command == "col_Xoa")
                {
                    _deleteOneRecord = 1;
                    Xoa();
                }
                if (command == "col_Chon")
                {
                    string id = dgvDanhMucPhong.Rows[_index].Cells["col_id"].Value.ToString();
                    string hoTen = dgvDanhMucPhong.Rows[_index].Cells["col_Ten"].Value.ToString();
                    DataGridViewRow dr = (DataGridViewRow)dgvDanhMucPhong.Rows[_index];
                    DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell chks = (DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell)dr.Cells[0];
                    if (chks.Selected && chks.FormattedValue.Equals("False") && !_lstCheck.Contains(id))
                    {
                        _lstCheck.Add(id);
                        _lstCheckHoTen.Add(hoTen);
                    }
                    if (chks.Selected && chks.FormattedValue.Equals("True"))
                    {
                        _lstCheck.Remove(id);
                        _lstCheckHoTen.Remove(hoTen);
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

        private void frmDanhMucPhong_Load(object sender, EventArgs e)
        {
            txtTen.Text = string.Empty;
            slbKhoa.txtTen.Focus();
            dataGridViewGme_dgvDanhMucPhong.Initialize();

        }

        private void cboMaKP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void frmDanhMucPhong_FormClosed(object sender, FormClosedEventArgs e)
        {
            _statusCloseForm = true;
        }

        private void cboLoaiGiaVienPhi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbGiaVienPhi = (DataTable)usc_SelectBoxLoaiGiaVienPhi.DataSource;
            DataRow dr = getRowByID(tbGiaVienPhi, "id=" + usc_SelectBoxLoaiGiaVienPhi.txtMa.Text);
            int gia = int.Parse(dr["GIA_TH"].ToString());
            txtGiaPhong.Text = gia.ToString("#,##0");
        }

        private void usc_SelectBoxLoaiGiaVienPhi_HisSelectChange(object sender, EventArgs e)
        {
            try
            {
                DataTable tbGiaVienPhi = (DataTable)usc_SelectBoxLoaiGiaVienPhi.DataSource;
                DataRow dr = getRowByID(tbGiaVienPhi, "id=" + usc_SelectBoxLoaiGiaVienPhi.txtMa.Text);
                int gia = int.Parse(dr["GIA_TH"].ToString());
                txtGiaPhong.Text = gia.ToString("#,##0");
            }
            catch
            {

                return;
            }

        }

        private void txtTen_Validated(object sender, EventArgs e)
        {
            string ten = ToTitleCase(txtTen.Text);
            txtTen.Text = ten;
          
        }


       

        private void usc_SelectBoxKhoa_HisMaTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(slbKhoa.txtMa.Text))
            {
               // slbLoai.txtMa.Text = slbLoai.txtTen.Text = "";
               //// txtTen.Text = "";
               // itgSTT.Value = 0;
               // TimKiem();

            }
            if (!string.IsNullOrEmpty(slbKhoa.txtMa.Text))
            {
                //dataGridViewGme_dgvDanhMucPhong.DataSource _acc.Get_Data(_tT.QueryTimKiemDanhMucPhong(usc_SelectBoxKhoa.txtMa.Text, _chuoiTimKiem));
                //_count = dgvDanhMucPhong.RowCount;
                //itgSTT.Value = _count + 1;
               // txtTen.Text = string.Empty;
                //col_MaKhoaPhong.DataPropertyName = "MA";
                //col_TenPhong.DataPropertyName = "TEN";
                //col_Loai.DataPropertyName = "LOAI";
                //col_TenLoaiPhong.DataPropertyName = "TENLOAIPHONG";
                //col_LoaiVP.DataPropertyName = "LOAIVP";
                //col_TenLoaiVienPhi.DataPropertyName = "TenLoaiVienPhi";
                //col_id.DataPropertyName = "ID";
                //col_STT.DataPropertyName = "STT";
                //col_MaKP.DataPropertyName = "MAKP";
                //col_TenKP.DataPropertyName = "TENKP";
            }
            else
            {
                //LoadDanhMucKhoaPhong();
            }
        }

        private void usc_SelectBoxKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void txtMa_Validated(object sender, EventArgs e)
        {
            if ((new cls_ThucThiDuLieu()).ExistMaphong(txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Mã phòng đã tồn tại!"
                  , TA_MessageBox.MessageIcon.Error);
                txtMa.Focus();

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

		private void ckAll_CheckedChanged_1(object sender, EventArgs e)
		{
			try
			{
				if (ckAll.Checked)
				{
					_statusCheckAll = 0;
					foreach (DataGridViewRow item in dgvDanhMucPhong.Rows)
					{
						DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)item.Cells[0];
						chk.Value = true;
						_lstCheck.Add(item.Cells["col_id"].Value.ToString());
					}
				}
				else if (!ckAll.Checked && _statusCheckAll == 0)
				{
					foreach (DataGridViewRow item in dgvDanhMucPhong.Rows)
					{
						DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)item.Cells[0];
						chk.Value = false;
						_lstCheck.Remove(item.Cells["col_id"].Value.ToString());
					}
				}
			}
			catch
			{

				return;
			}
		}

		private void dgvDanhMucPhong_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == col_Chon.Index)
				{
					//string id = dgvDanhMucPhong.Rows[e.RowIndex].Cells["col_MaKhoaPhong"].Value.ToString();
					string id = dgvDanhMucPhong.Rows[e.RowIndex].Cells["col_id"].Value.ToString();
					string ten = dgvDanhMucPhong.Rows[e.RowIndex].Cells["col_TenPhong"].Value.ToString();
					DataGridViewRow dr = (DataGridViewRow)dgvDanhMucPhong.Rows[_index];
					DataGridViewCheckBoxCell chks = (DataGridViewCheckBoxCell)dr.Cells[0];
					if (dgvDanhMucPhong[col_Chon.Index, e.RowIndex].Value + "" == "True" && !_lstCheck.Contains(id))
					{
						_lstCheck.Add(id);
						_lstCheckHoTen.Add(ten);
						//_lstCheckID.Add(id);
					}
					else
					{
						_lstCheck.Remove(id);
						_lstCheckHoTen.Remove(ten);
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
        private void GetGia()
        {
            if (!string.IsNullOrEmpty(usc_GiaGoiY.txtMa.Text) && !string.IsNullOrEmpty(slbDoiTuong.txtMa.Text))
            {
                txtGiaTien.Text = double.Parse(_tT.GetGiaTienTuGoiY(usc_GiaGoiY.txtMa.Text, slbDoiTuong.txtMa.Text, GetMaLoai(), 1)).ToString("#,##0");
                return;
            }
            txtGiaTien.Text = "";
        }

        private void slbKhoaTimKiem_HisSelectChange(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(slbKhoaTimKiem.txtMa.Text) || (slbKhoaTimKiem.txtMa.Text != "-1"))
            {
                ClearList();
                _lst.Add(cls_DanhMucPhong.col_MAPHONG);
                _lst.Add(cls_DanhMucPhong.col_TEN);
                _lst2.Add(cls_DanhMucPhong.col_MAKP, slbKhoaTimKiem.txtMa.Text);
                DataTable tb = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
                if (tb != null && tb.Rows.Count > 0)
                {
                    DataRow drPhong = tb.NewRow();
                    drPhong[cls_DanhMucPhong.col_MAPHONG] = "-1";
                    drPhong[cls_DanhMucPhong.col_TEN] = "Tất cả";
                    tb.Rows.Add(drPhong);
                    slbPhongTimKiem.DataSource = tb;
                }
            }

            if (slbPhongTimKiem.DataSource != null)
            {
                slbPhongTimKiem.SetTenByMa("-1");
            }

            Filter();
        }

        private void slbPhongTimKiem_HisSelectChange(object sender, EventArgs e)
        {
            Filter();
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

        private void usc_SelectBoxKhoa_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }

        private void usc_SelectBoxKhoa_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void usc_SelectBoxLoai_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }

        private void usc_SelectBoxLoai_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frm_DanhMucPhongImport frm = new frm_DanhMucPhongImport();
            frm.ShowDialog();
            if (frm._statusCloseForm)
            {
                LoadData();
            }
        }

        #endregion

    }
}