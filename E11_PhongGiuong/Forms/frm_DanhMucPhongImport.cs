using System;
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
using System.Text.RegularExpressions;

namespace E11_PhongGiuong
{
    public partial class frm_DanhMucPhongImport : frm_DanhMuc
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
        BindingSource _bsNhap = new BindingSource();
        private DataTable _tbDanhMucPhong, _tbCoppy, _tbCoppyUdp = null;
        SortedList<string, string> _tbSort = new SortedList<string, string>();
        SortedList<string, string> _tbSort2 = new SortedList<string, string>();
        private string _value = "";
        private bool _isImport = false;
        private bool _isStatus;

        #endregion

        #region Khởi tạo

        public frm_DanhMucPhongImport()
        {
            InitializeComponent();
            _api.KetNoi();
            btnIn.Visible = btnTienIch.Visible = false;
        }

        #endregion

        #region Phương thức protected

        protected override void LoadData()
        {
            TimKiem();
            LoaiVP();
            LoadLoai();
            LoadDanhMucKhoaPhong();
            base.LoadData();
        }

        protected override void Them()
        {
            _isAdd = true;
            ClearData();
            pnlControl2.Enabled = true;//2
            //pnlControl2.Focus();
            //usc_SelectBoxKhoa.txtTen.Focus();
            usc_SelectBoxKhoa.txtTen.Focus();
            base.Them();
            dgvDanhMucPhong.Enabled = true;


        }

        protected override void Sua()
        {
            _isAdd = false;
            base.Sua();
            pnlControl2.Enabled = true;//1
        }

        protected override void Xoa()
        {
            try
            {

                ClearList();
                #region Delete each record
                if (_deleteOneRecord == 1 )
                {
                    string id = dgvDanhMucPhong.Rows[_index].Cells["col_id"].Value.ToString();
                    if (!CheckConCuaDanhMucPhong(id))//Edited 300418
                    {

                        _lst2.Add(cls_DanhMucPhong.col_ID.ToUpper(), id.ToUpper());
                        if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTen.Text,
                         
                            TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (!_api.Delete(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _lst2, null))
                            {
                                TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                    txtTen.Text, _userError)
                                 , TA_MessageBox.MessageIcon.Error);
                                dgvDanhMucPhong.Focus();
                                return;
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
                        _lst2.Add(cls_DanhMucPhong.col_ID.ToUpper(), str1.ToString());
                        if (!_api.DeleteAll(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTen.Text, _userError)
                             , TA_MessageBox.MessageIcon.Error);
                            dgvDanhMucPhong.Focus();
                            return;
                        }
                        _lstCheck.Clear(); _lstCheckHoTen.Clear();
                    }
                    #endregion
                }
                LoadData();
                ClearData();
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
            btnThem.Focus();
            if (_isAdd && DuLieuNhapDung(_tbCoppyUdp))
            {
                #region Insert data
                try
                {

                    //if (!_tT.TrungSoTTKhoaPhong(usc_SelectBoxKhoa.txtMa.Text, itgSTT.Value.ToString()))//Edited 180531
                    //{
                        
                    //    string id = _tT.GetIdDanhMucPhongTuDong();//TT.GetSysDate().ToString("yyyyMMddHHmmss");//
                    //_tT.InsertDanhMucPhong(usc_SelectBoxKhoa.txtMa.Text, id, itgSTT.Value.ToString(), id,
                    //    txtTen.Text, usc_SelectBoxLoai.txtMa.Text, usc_SelectBoxLoaiVienPhi.txtMa.Text, txtGiaPhong.Text.Replace(",", ""), TT.GetSysDate(), GetMachine());
                    //_check = 1;
                    //    LoadData();
                    //}
                    //Import danh mục loại phòng
                    _sql = string.Empty;
                    int id = int.Parse(_tT.GetIdDanhMucPhongTuDong());
                    cls_ThucThiDuLieu cls = new cls_ThucThiDuLieu();
                    foreach (DataRow item in _tbCoppyUdp.Rows)
                    {
                        if (cls.ExistMaphong(item["MAPHONG"].ToString()))
                        {
                            TA_MessageBox.MessageBox.Show("Mã phòng đã tồn tại!"
                              , TA_MessageBox.MessageIcon.Error);
                            return;

                        }
                        _sql += string.Format("insert into {0}.danhmucphong (makp,id,stt,maphong,ten,loai,loaivp,giaphong,ngayud,machineid)"
                                + " values ('{1}',{2},{3},'{4}','{5}',{6},{7},'{8}',to_date('{9}','dd/MM/yyyy hh24:mi:ss'),'{10}') ; ",
                                _acc.Get_User(), item["MAKP"].ToString(), id, item["STT"].ToString(),
                                item["MAPHONG"].ToString(), item["TEN"].ToString(), item["LOAI"].ToString(), "28", "", _tT.GetSysDate().ToString("dd/MM/yyyy HH:mm:ss"),
                                GetMachine()) + Environment.NewLine;
                        id++;
                    }
                    if (!DatabaseV2.Database.Execute_Insert(ref _userError, ref _systemError, _sql))
                    {
                        TA_MessageBox.MessageBox.Show("Lỗi thực thi đa luồng!"
                                     , TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    TA_MessageBox.MessageBox.Show("Thêm thành công!"
                                    , TA_MessageBox.MessageIcon.Information);
                    this.Close();
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
                _lst.Add(cls_DanhMucPhong.col_ID);
                _lst.Add(cls_DanhMucPhong.col_STT);
                _lst.Add(cls_DanhMucPhong.col_MA);
                _lst.Add(cls_DanhMucPhong.col_TEN);
                _lst.Add(cls_DanhMucPhong.col_LOAI);
                _lst.Add(cls_DanhMucPhong.col_LOAIVP);
                Dictionary<string, string> lst3 = new Dictionary<string, string>();
                lst3.Add(cls_DanhMucPhong.col_MA, _chuoiTimKiem);
                lst3.Add(cls_DanhMucPhong.col_TEN, _chuoiTimKiem);
                _tbDanhMucPhong = _acc.Get_Data(_tT.QueryTimKiemDanhMucPhongImport(usc_SelectBoxKhoa.txtMa.Text, _chuoiTimKiem.ToUpper()));
                _tbDanhMucPhong.Clear();
                if (_bsNhap == null)
                {
                    _bsNhap = new BindingSource();
                }
                _bsNhap.DataSource = _tbDanhMucPhong;
                dgvDanhMucPhong.DataSource = _bsNhap;
                _count = dgvDanhMucPhong.RowCount;
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

        #region Gán source cho control khoa phòng
        private void LoadDanhMucKhoaPhong()
        {
            try
            {
                usc_SelectBoxKhoa.DataSource = _acc.Get_Data(_tT.QueryDanhMucKhoaPhong());
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
                usc_SelectBoxLoai.DataSource = _api.Search(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _acc.Get_User(), -1, _lst, orderByASC1: true, orderByName1: cls_DanhMucLoaiPhong.col_STT);
            }
            catch
            {
                return;
            }
        } 
        #endregion

        #region Load loại viện phí
        private void LoaiVP()
        {
            try
            {
                ClearList();
                _lst.Add(cls_V_LoaiVienPhi.col_ID);
                _lst.Add(cls_V_LoaiVienPhi.col_TEN);
                usc_SelectBoxLoaiVienPhi.DataSource = _api.Search(ref _userError, ref _systemError, cls_V_LoaiVienPhi.tb_TenBang, _acc.Get_User(), -1, _lst, orderByASC1: true, orderByName1: cls_V_LoaiVienPhi.col_STT);
                DataRow dr = getRowByID((DataTable)usc_SelectBoxLoaiVienPhi.DataSource, "id = 28");
                usc_SelectBoxLoaiVienPhi.txtMa.Text = dr["ID"].ToString();
                usc_SelectBoxLoaiVienPhi.txtTen.Text = dr["TEN"].ToString();
            }
            catch
            {

                return;
            }
        } 
        #endregion

        #region Xóa trắng dữ liệu để thêm mới
        private void ClearData()
        {
            usc_SelectBoxKhoa.txtMa.Text = usc_SelectBoxKhoa.txtTen.Text = usc_SelectBoxLoai.txtMa.Text
                = usc_SelectBoxLoai.txtTen.Text = usc_SelectBoxLoaiGiaVienPhi.txtTen.Text = string.Empty;
            txtTen.Text = usc_SelectBoxLoaiVienPhi.txtMa.Text = usc_SelectBoxLoaiVienPhi.txtTen.Text
                = txtGiaPhong.Text = usc_SelectBoxLoaiGiaVienPhi.txtMa.Text = string.Empty;
            try
            {
                string sql = string.Format("select max(stt) from {0}.{1} where makp='{2}'", _acc.Get_User(),
                    cls_DanhMucPhong.tb_TenBang, usc_SelectBoxKhoa.txtMa.Text);
                DataTable tb = _acc.Get_Data(sql);
                itgSTT.Value = int.Parse(tb.Rows[0].ItemArray[0].ToString());
            }
            catch { itgSTT.Value = 1; }
            txtTen.Text = "";
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
            _sql = string.Format("select  * from {0}.PG_DANHMUCGIUONG where idphong={1}", _acc.Get_User(), idPhong);
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

        private bool DuLieuNhapDung(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                //Mã phòng
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, "MAPHONG", tb.Rows[i]["MAPHONG"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count > 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Mã phòng {0} đã được khai báo", tb.Rows[i]["MAPHONG"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                //Tên phòng
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, cls_DanhMucPhong.col_TEN, tb.Rows[i]["TEN"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count > 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Tên phòng {0} đã được khai báo", tb.Rows[i]["TEN"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                //MaLoaiPhong
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang, cls_DanhMucLoaiPhong.col_MA, tb.Rows[i]["LOAI"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count == 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Mã loại {0} chưa được khai báo", tb.Rows[i]["LOAI"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                //Kiểm tra sự tồn tại của mã khoa phòng
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, tb.Rows[i]["MAKP"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count == 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Mã khoa {0} chưa được khai báo", tb.Rows[i]["MAKP"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }

                _sql = string.Format("select * from {0}.{1} where {2}='{3}' and {4}='{5}' and {6}='{7}' and {8}='{9}'",_acc.Get_User(),cls_DanhMucPhong.tb_TenBang,
                   "MAPHONG", tb.Rows[i]["MAPHONG"].ToString(), cls_DanhMucPhong.col_TEN, tb.Rows[i]["TEN"].ToString(), cls_DanhMucPhong.col_LOAI, tb.Rows[i]["LOAI"].ToString(),
                   cls_DanhMucPhong.col_MAKP, tb.Rows[i]["MAKP"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count > 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Trùng dữ liệu {0} - {1} - {2} - {3}", tb.Rows[i]["MAPHONG"].ToString(), tb.Rows[i]["TENPHONG"].ToString(),
                        tb.Rows[i]["LOAI"].ToString(), tb.Rows[i]["MAKP"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }

            }

            return true;
        }

        #endregion

        #region Sự kiện

        private void dgvDanhMucPhong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _index = dgvDanhMucPhong.CurrentRow.Index;
                DataTable tb = ((DataTable)dgvDanhMucPhong.DataSource);
                DataRow row = tb.Rows[_index];
                txtTen.Text = row["TEN"].ToString();
                usc_SelectBoxLoai.txtMa.Text = row["LOAI"].ToString();
                usc_SelectBoxLoaiVienPhi.txtMa.Text = row["LOAIVP"].ToString();
                DataTable tbLoaiVienPhi = (DataTable)usc_SelectBoxLoaiVienPhi.DataSource;
                DataRow drLoaiVP = getRowByID(tbLoaiVienPhi, "id = " + row["LOAIVP"].ToString());
                usc_SelectBoxLoaiVienPhi.txtTen.Text = drLoaiVP["Ten"].ToString();
                itgSTT.Value = int.Parse(row["STT"].ToString());
                DataTable tbLoai = (DataTable)usc_SelectBoxLoai.DataSource;
                DataRow dr = getRowByID(tbLoai, "id = " + row["LOAI"].ToString());
                usc_SelectBoxLoai.txtMa.Text = dr["ID"].ToString();
                usc_SelectBoxLoai.txtTen.Text = dr["TEN"].ToString();
                if (string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
                {
                    usc_SelectBoxLoai.txtMa.Text = usc_SelectBoxLoai.txtTen.Text = "";
                    txtTen.Text = "";
                    itgSTT.Value = 0;
                }
            }
            catch
            {
                return;
            }
        }

        private void dgvDanhMucPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _index = dgvDanhMucPhong.CurrentRow.Index;
                txtTen.Text = dgvDanhMucPhong.Rows[_index].Cells["col_TenPhong"].Value.ToString();
                usc_SelectBoxLoai.txtMa.Text = dgvDanhMucPhong.Rows[_index].Cells["col_Loai"].Value.ToString();
                try
                {
                    DataRow drL = getRowByID(usc_SelectBoxLoai.DataSource, "ID = " + usc_SelectBoxLoai.txtMa.Text);
                    usc_SelectBoxLoai.txtTen.Text = drL["TEN"].ToString();
                }
                catch 
                {
                }
                try
                {
                    DataRow drP = getRowByID(usc_SelectBoxKhoa.DataSource, "MAKP='" + dgvDanhMucPhong.Rows[_index].Cells["col_MaKP"].Value.ToString() + "'");
                    usc_SelectBoxKhoa.txtTen.Text = drP["TENKP"].ToString();
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
            usc_SelectBoxKhoa.txtTen.Focus();
            dgvDanhMucPhong.Enabled = false;
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

        private void usc_SelectBoxLoaiVienPhi_HisSelectChange(object sender, EventArgs e)
        {
            try
            {
                usc_SelectBoxLoaiGiaVienPhi.DataSource = _tT.GetDatausc_SelectBoxLoaiVienPhi(usc_SelectBoxLoaiVienPhi.txtMa.Text);
            }
            catch
            {

                return;
            }

        }

        private void usc_SelectBoxKhoa_HisMaTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
            {
                usc_SelectBoxLoai.txtMa.Text = usc_SelectBoxLoai.txtTen.Text = "";
                txtTen.Text = "";
                itgSTT.Value = 0;
                TimKiem();
            }
            if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
            {

                dgvDanhMucPhong.DataSource = _acc.Get_Data(_tT.QueryTimKiemDanhMucPhong(usc_SelectBoxKhoa.txtMa.Text, _chuoiTimKiem));
                _count = dgvDanhMucPhong.RowCount;
                itgSTT.Value = _count + 1;
                txtTen.Text = string.Empty;
            }
            else
            {
                LoadDanhMucKhoaPhong();
            }
        }

        private void usc_SelectBoxKhoa_HisSelectChange(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
            {
                usc_SelectBoxLoai.txtMa.Text = usc_SelectBoxLoai.txtTen.Text = "";
                txtTen.Text = "";
                itgSTT.Value = 0;
                TimKiem();
            }
            if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
            {

                dgvDanhMucPhong.DataSource = _acc.Get_Data(_tT.QueryTimKiemDanhMucPhong(usc_SelectBoxKhoa.txtMa.Text, _chuoiTimKiem));
                _count = dgvDanhMucPhong.RowCount;
                itgSTT.Value = _count + 1;
                txtTen.Text = string.Empty;
            }
            else
            {
                LoadDanhMucKhoaPhong();
            }
        }

        private void usc_SelectBoxKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
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

        private void dgvDanhMucPhong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboard(dgvDanhMucPhong);
            }
        }

        private void PasteClipboard(DataGridView myDataGridView)
        {
            try
            {
                _isImport = true;
                //_stlV.Clear();
                //_dtCopy = _dtNgang.Clone();
                _tbSort.Clear();
                _tbCoppy = _tbDanhMucPhong.Clone();
                _tbCoppyUdp = _tbDanhMucPhong.Clone();
                //for (int duyet1 = 0; duyet1 < _tbCoppy.Columns.Count; duyet1++)
                //{
                //    _tbCoppy.Columns[duyet1].DataType = typeof(string);
                //    _tbCoppyUdp.Columns[duyet1].DataType = typeof(string);

                //}
                //foreach (DataGridViewColumn clgv in dgvDanhMucLoaiPhong.Columns)
                //{
                //    clgv.CellTemplate.ValueType = typeof(string);
                //}

                foreach (DataGridViewCell cell in dgvDanhMucPhong.SelectedCells)
                {
                    _tbSort.Add(cell.RowIndex.ToString().PadLeft(5, '0') + "-" + cell.ColumnIndex.ToString().PadLeft(5, '0'), "");
                }

                try
                {
                    if (_tbSort2.Count <= 0)
                    {
                        int myRowIndex = 0;
                        DataObject o = (DataObject)Clipboard.GetDataObject();
                        if (o.GetDataPresent(DataFormats.UnicodeText))
                        {
                            string[] pastedRows = Regex.Split(o.GetData(DataFormats.UnicodeText).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                            foreach (string pastedRow in pastedRows)
                            {
                                DataRow dr = _tbDanhMucPhong.NewRow();
                                DataRow dr1 = _tbCoppy.NewRow();
                                string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });
                                for (int i = 0; i < pastedRowCells.Length; i++)
                                {
                                    try
                                    {
                                        _tbSort2.Add(myRowIndex.ToString().PadLeft(5, '0') + "-" + i.ToString().PadLeft(5, '0'), pastedRowCells[i]);
                                    }
                                    catch
                                    {

                                    }
                                }
                                myRowIndex++;
                            }
                        }
                    }//

                }
                catch
                {
                }

                if (_tbSort2.Count > 0)
                {
                    int minR = int.Parse(_tbSort2.Keys[0].Split('-')[0]);
                    int minC = int.Parse(_tbSort2.Keys[0].Split('-')[1]);
                    int maxR = int.Parse(_tbSort2.Keys[_tbSort2.Count - 1].Split('-')[0]);
                    int maxC = int.Parse(_tbSort2.Keys[_tbSort2.Count - 1].Split('-')[1]);

                    int rowIndex = 0;
                    int columnIndex = 0;

                    try
                    {
                        rowIndex = int.Parse(_tbSort.Keys[0].Split('-')[0]);
                        columnIndex = int.Parse(_tbSort.Keys[0].Split('-')[1]);
                    }
                    catch
                    {
                    }
                    bool themmoi = false;
                    for (int i = minR; i <= maxR; i++)
                    {
                        DataRow dr1 = _tbCoppy.NewRow();
                        DataRow dr2 = _tbCoppyUdp.NewRow();
                        try
                        {
                            if (_bsNhap == null)
                            {
                                _bsNhap = new BindingSource();
                            }
                            _bsNhap.AddNew();
                            dr2["STT"] = dgvDanhMucPhong.Rows[rowIndex + i - minR].Cells["col_STT"].Value;
                            for (int j = minC; j <= maxC; j++)
                            {
                                _value = _tbSort2[string.Format("{0}-{1}", i.ToString().PadLeft(5, '0'), j.ToString().PadLeft(5, '0'))];
                                if (rowIndex + i - minR >= dgvDanhMucPhong.Rows.Count - 1)
                                {
                                    if (_bsNhap == null)
                                    {
                                        _bsNhap = new BindingSource();
                                    }
                                    _bsNhap.AddNew();
                                    themmoi = true;
                                }
                                //if (themmoi)
                                //{
                                //    dr1[columnIndex + j - minC] = _value;
                                //}
                                //else
                                //{
                                    dr2[columnIndex + j - minC] = _value;
                                //}
                                dgvDanhMucPhong.Rows[rowIndex + i - minR].Cells[columnIndex + j - minC].ValueType = typeof(string);

                                dgvDanhMucPhong.Rows[rowIndex + i - minR].Cells[columnIndex + j - minC].Value = _value;
                            }
                            //if (themmoi)
                            //{
                            //    _tbCoppy.Rows.Add(dr1);
                            //}
                            //else
                            //{
                                _tbCoppyUdp.Rows.Add(dr2);
                            //}
                        }
                        catch
                        {
                        }
                    }
                    if (themmoi)
                    {
                        themmoi = false;
                    }
                    _tbSort2.Clear();
                    return;
                }
            }
            catch
            {
                TA_MessageBox.MessageBox.Show("Dữ liệu Copy sai định dạng lưới, vui lòng kiểm tra lại!", TA_MessageBox.MessageIcon.Error);
            }
        }

        #endregion

    }
}
