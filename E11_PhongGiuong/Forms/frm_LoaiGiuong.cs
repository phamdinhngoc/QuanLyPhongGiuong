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
using System.Collections;
using System.Text.RegularExpressions;

namespace E11_PhongGiuong
{
    public partial class frm_LoaiGiuong : E00_Base.frm_DanhMuc
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
        private bool _checkStatusUpdateTableChiTiet;
        private string _maLoai, _tenLoai = string.Empty;
        private DataTable _tbChiTiet,_tbCheckUpdate;
        public bool _statusCloseForm;
        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private string _sql = string.Empty;
        private string _mauSacGiuong = string.Empty;
        
        #endregion

        #region Khởi tạo

        public frm_LoaiGiuong()
        {
            InitializeComponent();
            _api.KetNoi();
            btnXoa.Visible = false;
            btnSua.Visible = true;//5
            btnIn.Visible = btnTienIch.Visible = false;
            slbDoiTuong.clear();
            usc_GiaGoiY.clear();
            txtGiaTien.Text = "";
            LoadDanhSachDoiTuong();
			LoadGiaGoiY();
        } 

        #endregion

        #region Phương thức

        #region Phương thức protected

        protected override void LoadData()
        {
            
            TimKiem();
            ClearData();
            base.LoadData();
            Show_ChiTiet();
        }

        protected override void Them()
        {
            _isAdd = true;
            base.Them();
            ClearData();
            pnlMain.Enabled = true;
            dgvLoaiGiuong.Enabled = false;
            //dgvLoaiGiuongCT.Enabled = true;
            _tbChiTiet = _tT.GetDataUpdateTableChiTiet().Select("MADT=1 or MADT = 2 or MADT =12").CopyToDataTable();
            //dgvLoaiGiuongCT.DataSource = _tbChiTiet;
            pnlControl2.Enabled = true;//2
            txtGiaTien.Enabled = false;
            txtTenLoaiGiuong.Focus();
        }

        protected override void Sua()
        {
            _isAdd = false;
            base.Sua();
            pnlMain.Enabled = true;
            txtGiaTien.Enabled = true;
            dgvLoaiGiuong.Enabled = false;
            //dgvLoaiGiuongCT.Enabled = true;
            pnlControl2.Enabled = true;//1
        }

        protected override void Xoa()
        {
            try
            {
                ClearList();
                #region Delete each record
                string id, maLoai;
                if (_deleteOneRecord == 1)
                {
                    id = dgvLoaiGiuong.Rows[_index].Cells["col_id"].Value.ToString();
                    maLoai = dgvLoaiGiuong.Rows[_index].Cells["col_MaLoai"].Value.ToString();
                    if (!_tT.TonTaiLoaiGiuongODanhMucGiuong(maLoai))
                    {
                        _lst2.Add(cls_D_DanhMucLoaiGiuong.col_ID.ToUpper(), id.ToUpper());
                        if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTenLoaiGiuong.Text, 
                            TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (!_api.Delete(ref _userError, ref _systemError, cls_D_DanhMucLoaiGiuong.tb_TenBang, _lst2, null))
                            {
                                TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                    txtTenLoaiGiuong.Text, _userError)
                                 , TA_MessageBox.MessageIcon.Error);
                                dgvLoaiGiuong.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show(string.Format("Loại giường này đã được khai báo cho giường.\n Không thể xóa! {0}. Lỗi: {1} !!!!",
                                    txtTenLoaiGiuong.Text, _userError)
                                 , TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                }
                
                #endregion
                else
                {
                    #region Delete all record have checked
                    string str = "", strHoTen = string.Empty;
                    ///append id
                    foreach (var item in _lstCheck)
                    {
                        str += item.ToString() + ",";
                    }
                    StringBuilder str1 = new StringBuilder(str);
                    str1 = str1.Remove(str.Length - 1, 1);

                    ///append name
                    foreach (var item in _lstCheckHoTen)
                    {
                        strHoTen += item.ToString() + ",";
                    }
                    StringBuilder str2 = new StringBuilder(strHoTen);
                    str2 = str2.Remove(str2.Length - 1, 1);


                    if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa  " + str2 + " không?", 
                            TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ClearList();
                        _lst2.Add(cls_D_DanhMucLoaiGiuong.col_ID.ToUpper(), str1.ToString());
                        if (!_api.DeleteAll(ref _userError, ref _systemError, cls_D_DanhMucLoaiGiuong.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTenLoaiGiuong.Text, _userError)
                             , TA_MessageBox.MessageIcon.Error);
                            dgvLoaiGiuong.Focus();
                            return;
                        }
                        _lstCheck.Clear(); _lstCheckHoTen.Clear();
                    }
                    #endregion
                }
                LoadData();
                base.Xoa();
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
                    ClearList();
                    _maLoai = _tT.GetSysDate().ToString("MMddHHmmss");
                    _lst2.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI,_maLoai);
                    _lst2.Add(cls_D_DanhMucLoaiGiuong.col_TENLOAI,txtTenLoaiGiuong.Text);
                    _lst2.Add(cls_D_DanhMucLoaiGiuong.col_TrangThai,ckTamNgung.Checked?"1":"0");
                    _lst2.Add(cls_D_DanhMucLoaiGiuong.col_MauSac, _mauSacGiuong);
                    _lst2.Add("MACHINEID", GetMachine());
                    _lst2.Add("NGAYUD", _tT.GetSysDate().ToString("yyyy-MM-dd HH:mm:ss"));
                    List<string> lst3 = new List<string>();
                    lst3.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI);
                    List<string> lst4 = new List<string>();
                    lst4.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI);
                    if (!_api.Insert(ref _userError, ref _systemError,cls_D_DanhMucLoaiGiuong.tb_TenBang,_lst2,lst3,lst4))
                    {
                        TA_MessageBox.MessageBox.Show("Không thể thêm danh mục!",  TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    //Thêm chi tiết
                    _checkStatusUpdateTableChiTiet = false;
                    _tT.InsertGiaGiuong(_maLoai, "2", slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);

                    CheckAndInsert();
                    _check = 1;
                    LoadData();

                   
                }
                catch
                {
                    return;
                }
                #endregion
                if (_check == 1)
                {
                    pnlMain.Enabled = true;
                    dgvLoaiGiuong.Enabled = true;
                    //dgvLoaiGiuongCT.Enabled = false;
                    base.Luu();
                }
            }
            else
            {
                #region Update data
                try
                {
                    ClearList();
                    _lst2.Add(cls_D_DanhMucLoaiGiuong.col_TENLOAI,txtTenLoaiGiuong.Text);
                    _lst2.Add(cls_D_DanhMucLoaiGiuong.col_TrangThai, ckTamNgung.Checked ? "1" : "0");
                    _lst2.Add(cls_D_DanhMucLoaiGiuong.col_MauSac, _mauSacGiuong);
                    Dictionary<string,string> dicWhere=new Dictionary<string,string>();
                    string id = dgvLoaiGiuong.Rows[_index].Cells["col_id"].Value.ToString();
                    dicWhere.Add(cls_D_DanhMucLoaiGiuong.col_ID,id.ToUpper());
                    if (!_api.Update(ref _userError, ref _systemError, cls_D_DanhMucLoaiGiuong.tb_TenBang,  _lst2, new List<string>(),  dicWhere))
                    {
                        TA_MessageBox.MessageBox.Show("Không thể update danh mục!",  TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    _tT.UpdateGiaGiuong(GetMaLoai(), "2", slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);
                   
                    CheckAndInsert();
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
                    pnlMain.Enabled = true;
                    dgvLoaiGiuong.Enabled = true;
                    //dgvLoaiGiuongCT.Enabled = false;
                    base.Luu();
                }
            }
        }

        protected override void BoQua()
        {
            Show_ChiTiet();
            base.BoQua();
            ClearData();
            dgvLoaiGiuong.Enabled = true;
            //dgvLoaiGiuongCT.Enabled = false;
            pnlControl2.Enabled = false;//4
            
        }

        protected override void TimKiem()
        {
            try
            {
                ClearList();
                _chuoiTimKiem = txtTimKiem.Text;
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_ID);
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI);
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_TENLOAI);
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_TrangThai);
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_MauSac);
                Dictionary<string, string> lst3 = new Dictionary<string, string>();
                lst3.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI, _chuoiTimKiem.ToUpper());
                lst3.Add(cls_D_DanhMucLoaiGiuong.col_TENLOAI, _chuoiTimKiem.ToUpper());
                dataGridViewGme_dgvLoaiGiuong.DataSource = _api.Search(ref _userError, ref _systemError, cls_D_DanhMucLoaiGiuong.tb_TenBang, lst: _lst,andLike:false, dicLike: lst3, orderByASC1: true, orderByName1: cls_D_DanhMucLoaiGiuong.col_ID);

                _count = dgvLoaiGiuong.RowCount;
                col_MaLoai.DataPropertyName = "MALOAI";
                col_TenLoai.DataPropertyName = "TENLOAI";
                col_id.DataPropertyName = "ID";
                col_Color.DataPropertyName = "MAUSAC";
                base.TimKiem();
                pnlControl2.Enabled = false;//3
            }
            catch
            {
                return;
            }
        }

        protected override void Show_ChiTiet()
        {
            try
            {
                btnLuu.Enabled = true; _isAdd = false;
                _index = dgvLoaiGiuong.CurrentRow == null ? 0 : dgvLoaiGiuong.CurrentRow.Index;
                DataTable tb = ((DataTable)dgvLoaiGiuong.DataSource);
                DataRow row = tb.Rows[_index];
                _maLoai = row["MALOAI"].ToString();
                _tenLoai = row["TENLOAI"].ToString();
                txtTenLoaiGiuong.Text = _tenLoai;
                ckTamNgung.Checked = row["TRANGTHAI"].ToString() == "1";
                string[] mauSacGiuong = row["MAUSAC"].ToString().Split(',');
                try
                {
                    his_ColorPickerButton1.SelectedColor = Color.FromArgb(int.Parse(mauSacGiuong[0].ToString()), int.Parse(mauSacGiuong[1].ToString()), int.Parse(mauSacGiuong[2].ToString()), int.Parse(mauSacGiuong[3].ToString()));
                }
                catch
                {
                }
                string command = dgvLoaiGiuong.Columns[dgvLoaiGiuong.CurrentCell.ColumnIndex].Name;
                if (command == "col_Sua")
                {
                    Sua();
                }
                else if (command == "col_Xoa")
                {
                    _deleteOneRecord = 1;
                    Xoa();
                }
                else if (command == "col_Chon")
                {
                    string id = dgvLoaiGiuong.Rows[_index].Cells["col_id"].Value.ToString();
                    //string hoTen = dgvLoaiGiuong.Rows[_index].Cells["col_Ten"].Value.ToString();
                    DataGridViewRow dr = (DataGridViewRow)dgvLoaiGiuong.Rows[_index];
                    DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell chks = (DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell)dr.Cells[0];
                    if (chks.Selected && chks.FormattedValue.Equals("False") && !_lstCheck.Contains(id))
                    {
                        _lstCheck.Add(id);
                        //_lstCheckHoTen.Add(hoTen);
                    }
                    if (chks.Selected && chks.FormattedValue.Equals("True"))
                    {
                        _lstCheck.Remove(id);
                        //_lstCheckHoTen.Remove(hoTen);
                        _statusCheckAll = 1;
                        ckAll.Checked = false;
                    }
                }
            }
            catch
            {

            }
            
            base.Show_ChiTiet();
        }

        protected override void Thoat()
        {
            base.Thoat();
        } 

        #endregion

        #region Phương thức private

        #region Lấy tên máy
        private string GetMachine()
        {
            _sql = string.Format("select SYS_CONTEXT('USERENV','IP_ADDRESS')||'+'||Userenv('TERMINAL')||'+'||SYS_CONTEXT('USERENV','MODULE') from dual");
            return _acc.Get_Data(_sql).Rows[0][0].ToString();
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

        #region Xóa dữ liệu trước khi thêm mới
        private void ClearData()
        {
            txtTenLoaiGiuong.Text = string.Empty;
            ckTamNgung.CheckState = CheckState.Unchecked;
            txtTenLoaiGiuong.Focus();
            slbDoiTuong.clear();
            usc_GiaGoiY.clear();
            txtGiaTien.Text = "";
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

        #region Get datarow by id
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

        #region Kiểm tra và thêm thông tin
        private void CheckAndInsert()
        {
            if (!_checkStatusUpdateTableChiTiet)
            {

                //Insert table danhmucloaigiuongct
                string ma = string.Empty;
                pnlMain.Enabled = true;
                int idLoaiPhongCT = _tT.GetTuDongIDLoaiGiuongCT() == "" ? 1 : int.Parse(_tT.GetTuDongIDLoaiGiuongCT());
                _sql = string.Empty;
                for (int i = 0; i < _tbChiTiet.Rows.Count; i++)
                {
                    _sql += string.Format("insert into {0}.DANHMUCLOAIGIUONGCT(ID,MALOAI,MADOITUONG,GIAGIUONG) values({1},'{2}',{3},{4}); ",
                    _acc.Get_User(), idLoaiPhongCT, _maLoai, _tbChiTiet.Rows[i]["MADT"].ToString(), _tbChiTiet.Rows[i]["GIA"].ToString()) + Environment.NewLine;
                    idLoaiPhongCT += 1;
                }
                if (!DatabaseV2.Database.Execute_Insert(ref _userError, ref _systemError, _sql))
                {
                    TA_MessageBox.MessageBox.Show("Lỗi thực thi đa luồng!"
                                 , TA_MessageBox.MessageIcon.Error);
                    return;
                }
            }
            else
            {
                //Update table danhmucloaigiuongct
                string ma = string.Empty;
                DataTable tb = _tbCheckUpdate.GetChanges();
                if (tb != null)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        ma = tb.Rows[i]["MACT"].ToString();
                        ClearList();
                        _lst2.Add(cls_DanhMucLoaiGiuongChiTiet.col_GIAGIUONG, tb.Rows[i]["GIA"].ToString());
                        _lst2.Add("TEN", tb.Rows[i]["TEN"].ToString());
                        _lst3.Add(cls_DanhMucLoaiGiuongChiTiet.col_ID, ma);
                        if (!_api.Update(ref _userError, ref _systemError, cls_DanhMucLoaiGiuongChiTiet.tb_TenBang,
                         _lst2, new List<string>(), _lst3))
                        {
                            TA_MessageBox.MessageBox.Show("Không thể cập nhật chi tiết!",  TA_MessageBox.MessageIcon.Error);
                            return;
                        }
                    }
                }
            }
        }
		#endregion

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
		}
        #endregion
        private void GetGia()
        {
            if (!string.IsNullOrEmpty(usc_GiaGoiY.txtMa.Text) && !string.IsNullOrEmpty(slbDoiTuong.txtMa.Text))
            {
                txtGiaTien.Text = double.Parse(_tT.GetGiaTienTuGoiY(usc_GiaGoiY.txtMa.Text, slbDoiTuong.txtMa.Text, GetMaLoai(), 2)).ToString("#,##0");
                return;
            }
            txtGiaTien.Text = "";
        }
        #endregion
        public string GetMaLoai()
        {
            string ma = "";
            if (dgvLoaiGiuong.CurrentRow != null && !_isAdd)
            {
                var row = dgvLoaiGiuong.CurrentRow.DataBoundItem as DataRowView;
                if (row != null)
                {
                    ma = "" + row.Row["MALOAI"];
                }
            }
            return ma;

        }
        #endregion

        #region Sự kiện

        private void frm_LoaiGiuong_FormClosed(object sender, FormClosedEventArgs e)
        {
            _statusCloseForm = true;
        }

        private void dgvLoaiGiuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Show_ChiTiet();
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckAll.Checked)
                {
                    _statusCheckAll = 0;
                    foreach (DataGridViewRow item in dgvLoaiGiuong.Rows)
                    {
                        DataGridViewCheckBoxXCell chk = (DataGridViewCheckBoxXCell)item.Cells[0];
                        chk.Value = true;
                        _lstCheck.Add(item.Cells["col_id"].Value.ToString());
                        _lstCheckHoTen.Add(item.Cells["col_TenLoai"].Value.ToString());
                    }
                }
                else if (!ckAll.Checked && _statusCheckAll == 0)
                {
                    foreach (DataGridViewRow item in dgvLoaiGiuong.Rows)
                    {
                        DataGridViewCheckBoxXCell chk = (DataGridViewCheckBoxXCell)item.Cells[0];
                        chk.Value = false;
                        _lstCheck.Remove(item.Cells["col_id"].Value.ToString());
                        _lstCheckHoTen.Remove(item.Cells["col_TenLoai"].Value.ToString());
                    }
                }
            }
            catch 
            {

                return;
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

        private void ckAll_CheckedChanged_1(object sender, EventArgs e)
		{
			try
			{
				if (ckAll.Checked)
				{
					_statusCheckAll = 0;
					foreach (DataGridViewRow item in dgvLoaiGiuong.Rows)
					{
						DataGridViewCheckBoxXCell chk = (DataGridViewCheckBoxXCell)item.Cells[0];
						chk.Value = true;
						_lstCheck.Add(item.Cells["col_id"].Value.ToString());
					}
				}
				else if (!ckAll.Checked && _statusCheckAll == 0)
				{
					foreach (DataGridViewRow item in dgvLoaiGiuong.Rows)
					{
						DataGridViewCheckBoxXCell chk = (DataGridViewCheckBoxXCell)item.Cells[0];
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

		private void frmLoaiGiuong_Load(object sender, EventArgs e)
        {
            //Them();
            dgvLoaiGiuong.Enabled = true;
            dataGridViewGme_dgvLoaiGiuong.Initialize();
        }


        private void dgvLoaiGiuong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _index = dgvLoaiGiuong.CurrentRow.Index;
                DataTable tb = ((DataTable)dgvLoaiGiuong.DataSource);
                DataRow row = tb.Rows[_index];
                _maLoai = row["MALOAI"].ToString();
                _tenLoai = row["TENLOAI"].ToString();
                txtTenLoaiGiuong.Text = row["TENLOAI"].ToString();
                ckTamNgung.Checked = row["TRANGTHAI"].ToString() == "1";
				//slbDoiTuong.SetTenByMa(dgvLoaiGiuong[col_DoiTuong.Name, dgvLoaiGiuong.SelectedRows[0].Index].Value + "");
				//usc_GiaGoiY.SetTenByMa(dgvLoaiGiuong[col_GoiYGia.Name, dgvLoaiGiuong.SelectedRows[0].Index].Value + "");
				//txtGiaTien.Text = dgvLoaiGiuong[col_GiaTien.Name, dgvLoaiGiuong.SelectedRows[0].Index].Value + "";
				_tbCheckUpdate = _tT.GetDataCheckUpdate(_maLoai);
                if (_tbCheckUpdate.Rows.Count > 0)
                {
                    _checkStatusUpdateTableChiTiet = true;
                    btnLuu.Enabled = true;_isAdd = false;
                }
                else
                {
                    _checkStatusUpdateTableChiTiet = false;
                    _tbChiTiet = _tT.GetDataUpdateTableChiTiet();
                    btnLuu.Enabled = true; _isAdd = false;
                    
                }
            }
            catch
            {
                return;
            }
        }


        private void dgvLoaiGiuongCT_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void txtMaLoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void his_ColorPickerButton1_SelectedColorChanged(object sender, EventArgs e)
        {
            //Làm sau
            Color c = his_ColorPickerButton1.SelectedColor;
            _mauSacGiuong = c.A + "," + c.R + "," + c.G + "," + c.B + "";
        }

        private void his_ColorPickerButton1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLuu.Focus();
            }
        }

        #endregion

    }
}
