using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using E00_Common;

using E00_Model;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar.Controls;

namespace E11_PhongGiuong
{
    public partial class frm_DanhMucGiuongImport : E00_Base.frm_DanhMuc
    {
        #region Khai báo biến toàn cục

        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
        private string _userError, _systemError, _id = string.Empty;
        private int _check = 0;
        private bool _isAdd;
        private List<string> _lstCheck = new List<string>();
        private List<string> _lstCheckHoTen = new List<string>();
        private DataTable _tbDanhSachDuong = new DataTable();
        private string _maKhoaPhong = string.Empty;
        private DataTable _tpDanhMucPhong = new DataTable();
        private DataTable _dtp = new DataTable();
        private string _maKhoa, _maPhong = string.Empty;
        public bool _statusCloseForm;
        private DataTable _tbGiaGoiY;
        private string _idGiuong, _tenKhoa, _tenPhong, _soThuTu, _tenGiuong, _maKhoaR, _maPhongR;//get data show
        private string _loaigiuong;
        private string _maKhoaLoadNgoai, _tenKhoaLoadNgoai, _maPhongLoadNgoai, _tenPhongLoadNgoai;
        private string[] _thongTinGiuong;
        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private string _sql = string.Empty;
        private int  _deleteOneRecord = 0, _statusCheckAll = 0;
        BindingSource _bsNhap = new BindingSource();
        private DataTable _tbDanhMucGiuong, _tbCoppy, _tbCoppyUdp = null;
        SortedList<string, string> _tbSort = new SortedList<string, string>();
        SortedList<string, string> _tbSort2 = new SortedList<string, string>();
        private string _value = "";
        private bool _isImport = false;
        private DataTable _tbDanhMucLoaiGiuong = null;
        private DataTable _tbDanhMucGiaVienPhi = null;

        #endregion

        #region Khởi tạo

        public frm_DanhMucGiuongImport()
        {
            InitializeComponent();
            pnlSearch.Visible = false;
            btnXoa.Visible = false;
            btnXoa.Visible = btnBoQua.Visible = btnThem.Visible = true;
            dgvDanhMucGiuong.Enabled = usc_SelectBoxKhoa.Enabled = usc_SelectBoxPhong.Enabled = usc_SelectBoxLoaiGiuong.Enabled = txtTen.Enabled = itgSTT.Enabled = true;
            btnIn.Visible = btnTienIch.Visible = false;
            _api.KetNoi();
        }

        public frm_DanhMucGiuongImport(string maKhoaLoadNgoai, string tenKhoaLoadNgoai, string maPhongLoadNgoai,
            string tenPhongLoadNgoai)
        {
            InitializeComponent();
            pnlSearch.Visible = false;
            btnXoa.Visible = false;
            _maKhoaLoadNgoai = maKhoaLoadNgoai;
            _maPhongLoadNgoai = maPhongLoadNgoai;
            _tenKhoaLoadNgoai = tenKhoaLoadNgoai;
            _tenPhongLoadNgoai = tenPhongLoadNgoai;
            dgvDanhMucGiuong.Enabled = usc_SelectBoxKhoa.Enabled = usc_SelectBoxPhong.Enabled = usc_SelectBoxLoaiGiuong.Enabled = txtTen.Enabled = itgSTT.Enabled = false;
            btnIn.Visible = btnTienIch.Visible = false;

        }

        public frm_DanhMucGiuongImport(string _idGiuongChonChuyen, string maKhoa, string tenKhoa, string maPhong,
            string tenPhong, string soThuTu, string tenGiuong, string loaiGiuong)
        {
            InitializeComponent();
            _thongTinGiuong = _idGiuongChonChuyen.ToString().Split(',');
            pnlSearch.Visible = false;
            _idGiuong = _thongTinGiuong[0].ToString();
            _maKhoaR = maKhoa; _tenKhoa = tenKhoa; _maPhongR = tenPhong; _tenPhong = tenPhong; _soThuTu = soThuTu; _tenGiuong = tenGiuong;
            _loaigiuong = loaiGiuong;
            //Ẩn không cho sử dụng các chức năng như danh mục
            btnXoa.Visible = btnBoQua.Visible =btnThem.Visible = false;
            dgvDanhMucGiuong.Enabled = usc_SelectBoxKhoa.Enabled = usc_SelectBoxPhong.Enabled = usc_SelectBoxLoaiGiuong.Enabled = txtTen.Enabled = itgSTT.Enabled = btnGoiYGia.Enabled = false;
            btnIn.Visible = btnTienIch.Visible = false;
        }

        #endregion

        #region Phương thức

        #region Phương thức protected

        protected override void LoadData()
        {
            TimKiem();
            base.LoadData();
            dgvDanhMucGiuong.Enabled = false;
        }

        protected override void Them()
        {
            ClearData();
            _isAdd = true;
            usc_SelectBoxKhoa.Enabled = true;
           
            base.Them();
            if (string.IsNullOrEmpty(usc_SelectBoxPhong.txtMa.Text))
            {
                usc_SelectBoxPhong.txtTen.Focus();
            }
            try
            {
                itgSTT.Value = int.Parse(_acc.Get_Data(_tT.QueryGetSTTDanhMucGiuong(usc_SelectBoxPhong.txtMa.Text)).Rows[0].ItemArray[0].ToString()) + 1;
            }
            catch { itgSTT.Value = 1; }
            dgvDanhMucGiuong.Enabled = true;
            dgvGiaGoiY.DataSource = null;
            pnlControl2.Enabled = true;//2
            usc_SelectBoxKhoa.txtTen.Focus();
        }

        protected override void Sua()
        {
            dgvDanhMucGiuong.Enabled = false;
            _isAdd = false;
            base.Sua();
            pnlControl2.Enabled = true;//1
        }

        protected override void Luu()
        {

            string tinhTrangGiuong = string.Empty;
            if (ckGiuongHu.Checked)
            {
                tinhTrangGiuong = "3";
            }
            if (ckChuaSuDung.Checked)
            {
                tinhTrangGiuong = "4";
            }
            if (DuLieuDung(_tbCoppyUdp))//Check dữ liệu trước khi import
            {
                for (int duyetGiuong = 0; duyetGiuong < _tbCoppyUdp.Rows.Count; duyetGiuong++)
                {
                    #region Insert vào table v_giavp
                    //ClearList();
                    //string sql = string.Format("select max(ID)+1 from {0}.v_giavp", _acc.Get_User());
                    //int idGiaVP = int.Parse(_acc.Get_Data(sql).Rows[0].ItemArray[0].ToString());
                    //string donGia = "0", baoHiem = "0", chinhSach = "0", dichVu = "0", nuocNgoai = "0";
                    //string maxMaSo = _acc.Get_Data(string.Format("select max(ma) from {0}.PG_DANHMUCGIUONG", _acc.Get_User())).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(string.Format("select max(ma)+1 from {0}.PG_DANHMUCGIUONG", _acc.Get_User())).Rows[0].ItemArray[0].ToString();
                    //sql = string.Format("insert into {0}.{1}(ID,ID_LOAI,STT,MA,TEN,GIA_TH,GIA_BH,GIA_CS,GIA_DV,GIA_NN,NGAYUD) values({2},{3},{4},'{5}','{6}',{7},{8},{9},{10},{11},TO_DATE('{12}', 'dd/mm/yyyy hh24:mi:ss'))",
                    //    _acc.Get_User(), cls_V_GiaVienPhi.tb_TenBang, idGiaVP, 1, _tbCoppyUdp.Rows[duyetGiuong]["STT"].ToString(), maxMaSo, _tbCoppyUdp.Rows[duyetGiuong]["TEN"].ToString(), donGia, baoHiem, chinhSach, dichVu, nuocNgoai, TT.GetSysDate().ToString("dd/MM/yyyy hh:mm:ss tt").Substring(0, 10));
                    //if (!_acc.Execute_Data(ref _userError, ref _systemError, sql))//OK giáVP
                    //{
                    //    TA_MessageBox.MessageBox.Show("Không thể thêm giá viện phí!", TA_MessageBox.MessageIcon.Error);
                    //    txtTen.Focus();
                    //    return;
                    //}
                    #endregion

                    #region Insert vào table v_giavpct
                    //_sql = string.Empty;
                    //_sql = _tT.QueryGoiYGia(_tbCoppyUdp.Rows[duyetGiuong]["TEN"].ToString());
                    //_tbGiaGoiY = _acc.Get_Data(_sql);
                    //string GetIdVpCTAuto = _acc.Get_Data(string.Format("select max(ID)+1 from {0}.v_giavpct", _acc.Get_User())).Rows[0][0].ToString();
                    //int idGiaVPCT = GetIdVpCTAuto == "" ? 1 : int.Parse(GetIdVpCTAuto);
                    ////int gia;
                    //foreach (DataRow item in _tbGiaGoiY.Rows)
                    //{

                    //    //gia = item["GIA"].ToString() == "" ? 0 : int.Parse(item["GIA"].ToString());
                    //    _sql += string.Format("insert into {0}.V_GIAVPCT(ID,ID_GIAVP,MADOITUONG,ID_BANGGIA) values({1},{2},{3},{4}); ",
                    //                        _acc.Get_User(), idGiaVPCT, idGiaVP.ToString(), item["MADOITUONG"].ToString(), _tT.GetIDBangGiaMoiNhat()) + Environment.NewLine;
                    //    idGiaVPCT += 1;
                    //}
                    //if (!DatabaseV2.Database.Execute_Insert(ref _userError, ref _systemError, _sql))
                    //{
                    //    TA_MessageBox.MessageBox.Show("Lỗi thực thi đa luồng!"
                    //                 , TA_MessageBox.MessageIcon.Error);
                    //    return;
                    //}
                    #endregion

                    #region Insert vào table dmgiuong
                    string toado = "";
                    string donGia = "0", baoHiem = "0", chinhSach = "0", dichVu = "0", nuocNgoai = "0";
                    string maxMaSo = _acc.Get_Data(string.Format("select max(ma) from {0}.{1}", _acc.Get_User(),cls_PG_DanhMucGiuong.tb_TenBang)).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(string.Format("select max(ma)+1 from {0}.{1}", _acc.Get_User(),cls_PG_DanhMucGiuong.tb_TenBang)).Rows[0].ItemArray[0].ToString();
                    ClearList();
                    _lst2.Add(cls_PG_DanhMucGiuong.col_MAPHONG, _tbCoppyUdp.Rows[duyetGiuong]["MAPHONG"].ToString());
                    _lst2.Add(cls_PG_DanhMucGiuong.col_ID, maxMaSo.ToString());
                    _lst2.Add(cls_PG_DanhMucGiuong.col_STT, _tbCoppyUdp.Rows[duyetGiuong]["STT"].ToString());
                    _lst2.Add(cls_PG_DanhMucGiuong.col_MA, maxMaSo);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_TEN, _tbCoppyUdp.Rows[duyetGiuong]["TEN"].ToString());
                    _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_TH, donGia);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_BH, baoHiem);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_CS, chinhSach);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_DV, dichVu);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_NN, nuocNgoai);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_SOLUONG, "1");
                    _lst2.Add(cls_PG_DanhMucGiuong.col_TINHTRANG, tinhTrangGiuong == "" ? "0" : tinhTrangGiuong);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_VITRI, toado);
                    _lst2.Add(cls_PG_DanhMucGiuong.col_NGAYUD, _tT.GetSysDate().ToString());
                    _lst2.Add(cls_PG_DanhMucGiuong.col_LOAIGIUONG,GetMaLoaiGiuong(_tbCoppyUdp.Rows[duyetGiuong]["TENLOAI"].ToString()));
                    _lst2.Add(cls_PG_DanhMucGiuong.col_IDGIAVP,GetIdGiaVP(_tbCoppyUdp.Rows[duyetGiuong]["TENGIAVP"].ToString()));
                    _lst2.Add(cls_PG_DanhMucGiuong.col_MAGIUONG, _tbCoppyUdp.Rows[duyetGiuong][cls_PG_DanhMucGiuong.col_MAGIUONG].ToString());
                    _lst2.Add(cls_PG_DanhMucGiuong.col_MACHINEID, GetMachine());
                    _lst2.Add(cls_PG_DanhMucGiuong.col_CHUAN, _tbCoppyUdp.Rows[duyetGiuong][cls_PG_DanhMucGiuong.col_CHUAN].ToString());
                    _lst.Add(cls_PG_DanhMucGiuong.col_ID);
                    if (!_api.Insert(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang, _lst2, _lst, _lst))
                    {
                        TA_MessageBox.MessageBox.Show("Không thể thêm giường!", TA_MessageBox.MessageIcon.Error);
                        txtTen.Focus();
                        return;
                    }
                    #endregion
                }
                TA_MessageBox.MessageBox.Show("Import thành công!", TA_MessageBox.MessageIcon.Information);
                this.Close();
            }
            
        }

        protected override void Xoa()
        {
            try
            {
                ClearList();
                #region Delete each record
                if (_deleteOneRecord == 1)
                {
                    string id = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_idG"].Value.ToString();
                    _lst2.Add(cls_PG_DanhMucGiuong.col_ID.ToUpper(), id.ToUpper());
                    if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTen.Text,
                     
                        TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!_api.Delete(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTen.Text, _userError)
                             , TA_MessageBox.MessageIcon.Error);
                            dgvDanhMucGiuong.Focus();
                            return;
                        }
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
                        _lst2.Add(cls_PG_DanhMucGiuong.col_ID.ToUpper(), str1.ToString());
                        if (!_api.DeleteAll(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTen.Text, _userError)
                             , TA_MessageBox.MessageIcon.Error);
                            dgvDanhMucGiuong.Focus();
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

        protected override void BoQua()
        {
            dgvDanhMucGiuong.Enabled = true;
            dgvGiaGoiY.Enabled = false;
            base.BoQua();
            pnlControl2.Enabled = false;//4
        }

        protected override void TimKiem()
        {
            try
            {
                ClearList();
                LoadDanhMucKhoa();
                LoaiDanhMucPhong();
                ClearList();
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI);
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_TENLOAI);
                _lst2.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI, "is not null");
                usc_SelectBoxLoaiGiuong.DataSource = _api.Search(ref _userError,
                    ref _systemError, cls_D_DanhMucLoaiGiuong.tb_TenBang, lst: _lst, dicDifferent: _lst2);
                _tbDanhMucGiuong = _tT.SourceGiuongImport();
                _tbDanhMucGiuong.Clear();
                if (_bsNhap == null)
                {
                    _bsNhap = new BindingSource();
                }
                _bsNhap.DataSource = _tbDanhMucGiuong;
               
                dgvDanhMucGiuong.DataSource = _bsNhap;
                col_SttG.DataPropertyName = "STT";
                col_TenGiuongG.DataPropertyName = "TEN";
                col_IdPhongG.DataPropertyName = "MAPHONG";
                col_LoaiGiuongG.DataPropertyName = "LOAIGIUONG";
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

        #region Lấy tên máy
        private string GetMachine()
        {
            _sql = string.Format("select SYS_CONTEXT('USERENV','IP_ADDRESS')||'+'||Userenv('TERMINAL')||'+'||SYS_CONTEXT('USERENV','MODULE') from dual");
            return _acc.Get_Data(_sql).Rows[0][0].ToString();
        }
        #endregion

        #region Load danh mục khoa
        private void LoadDanhMucKhoa()
        {
            try
            {
                usc_SelectBoxKhoa.DataSource = _tT.GetDataDanhMucKhoa();
            }
            catch
            {
                return;
            }
        } 
        #endregion

        #region Load danh mục phòng
        private void LoaiDanhMucPhong()
        {
            try
            {
                ClearList();
                _lst.Add(cls_DanhMucPhong.col_ID);
                _lst.Add(cls_DanhMucPhong.col_TEN);
                _lst2.Add(cls_DanhMucPhong.col_MAKP, usc_SelectBoxKhoa.txtMa.Text);
                DataTable tb = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang,
                    _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2,
                orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
                if (usc_SelectBoxKhoa.txtMa.Text != "")
                {
                    _tpDanhMucPhong = tb;
                    usc_SelectBoxPhong.DataSource = _tpDanhMucPhong;
                }
            }
            catch
            {
                return;
            }
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

        #region Kiểm tra nhập liệu
        private bool NhapDuLieu()
        {
            if (string.IsNullOrEmpty(itgSTT.Value.ToString()))
            {
                TA_MessageBox.MessageBox.Show("Nhập số thứ tự", TA_MessageBox.MessageIcon.Information);
                itgSTT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Nhập tên giường" , TA_MessageBox.MessageIcon.Information);
                txtTen.Focus();
                return false;
            }
            if (dgvGiaGoiY.Rows.Count == 0)
            {
                TA_MessageBox.MessageBox.Show("Nhập giá cho giường" , TA_MessageBox.MessageIcon.Information);
                dgvGiaGoiY.Focus();
                return false;
            }
            return true;
        } 
        #endregion

        #region Xóa text để nhập mới
        private void ClearData()
        {
            usc_SelectBoxKhoa.txtTen.Text = usc_SelectBoxPhong.txtTen.Text = usc_SelectBoxLoaiGiuong.txtTen.Text = string.Empty;
            txtTen.Text = string.Empty;
            usc_SelectBoxKhoa.txtTen.Focus();
        } 
        #endregion

        #region Kiểm tra tồn tại dữ liệu
        private bool DuLieuDung(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                //Mã giường
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAGIUONG, tb.Rows[i][cls_PG_DanhMucGiuong.col_MAGIUONG].ToString());
                if (_acc.Get_Data(_sql).Rows.Count > 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Mã giường {0} đã được khai báo", tb.Rows[i][cls_PG_DanhMucGiuong.col_MAGIUONG].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                //Tên giường
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_TEN, tb.Rows[i]["TEN"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count > 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Giường {0} đã được khai báo", tb.Rows[i]["TEN"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                //Mã loại giường
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_D_DanhMucLoaiGiuong.col_TENLOAI, tb.Rows[i]["TENLOAI"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count == 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Loại giường {0} chưa được khai báo", tb.Rows[i]["TENLOAI"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                //Mã phòng
                _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, "MAPHONG", tb.Rows[i]["MAPHONG"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count == 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Mã loại {0} chưa được khai báo", tb.Rows[i]["MAPHONG"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                _sql = string.Format("select * from {0}.{1} where {2}='{3}' and {4}='{5}' and {6}='{7}'", _acc.Get_User(),
                    cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_TEN, tb.Rows[i]["TEN"].ToString(), cls_PG_DanhMucGiuong.col_LOAIGIUONG, 
                    GetMaLoaiGiuong(tb.Rows[i]["TENLOAI"].ToString()),
                    "MAPHONG", tb.Rows[i]["MAPHONG"].ToString());
                if (_acc.Get_Data(_sql).Rows.Count > 0)
                {
                    TA_MessageBox.MessageBox.Show(string.Format("Trùng dữ liệu {0} - {1} - {2}", tb.Rows[i]["TEN"].ToString(), tb.Rows[i]["TENLOAI"].ToString(), tb.Rows[i]["MAPHONG"].ToString()), TA_MessageBox.MessageIcon.Error);
                    return false;
                }
            }

            return true;
        } 
        #endregion

        #region Lấy mã loại giường

        private string GetMaLoaiGiuong(string tenLoai)
        {
            string maLoai=string.Empty;
            DataRow dr=null;
            try 
	        {	        
		        dr= getRowByID(_tbDanhMucLoaiGiuong, "TENLOAI='" + tenLoai + "'");
                maLoai=dr["MALOAI"].ToString();
	        }
	        catch 
	        {
		        maLoai="";
	        }
            return maLoai;
        }

        #endregion

        #region Lấy idgiavp

        private string GetIdGiaVP(string tenGiaVP)
        {
            string maGiaVP = string.Empty;
            DataRow dr = null;
            try
            {
                dr = getRowByID(_tbDanhMucGiaVienPhi, "TEN='" + tenGiaVP + "'");
                maGiaVP = dr["ID"].ToString();
            }
            catch
            {
                maGiaVP = "";
            }
            return maGiaVP;
        }

        #endregion

        #endregion

        #region Phương thức public

        #region Set focus cho control
        public void ControlSetFocus(E00_ControlNew.usc_SelectBox control)
        {
            // Set focus to the control, if it can receive focus.
            if (control.CanFocus)
            {
                control.Focus();
            }
        } 
        #endregion

        #region Get datarow từ datatable
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

        #endregion

        #endregion

        #region Sự kiện

        private void txtTen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
        }

        private void frmDanhMucGiuong_Load(object sender, EventArgs e)
        {
            _tbDanhMucLoaiGiuong = _tT.GetDanhMucLoaiGiuong();
            _tbDanhMucGiaVienPhi = _tT.GetDanhMucGiaVienPhi();
            //dgvDanhMucGiuong.Enabled = true;
            dgvGiaGoiY.Enabled = false;

            if (!string.IsNullOrEmpty(_idGiuong))
            {
                _isAdd = true;
                base.Them();
                txtTen.Text = string.Empty;
                dgvDanhMucGiuong.Enabled = false;
                //dgvGiaGoiY.Enabled = true;
                usc_SelectBoxLoaiGiuong.txtMa.Text = _loaigiuong;
                usc_SelectBoxLoaiGiuong.txtTen.Text = _tT.GetTenLoaiGiuong(_loaigiuong);
                itgSTT.Value = int.Parse(_soThuTu);

                //Gán khoa phòng cho form khởi tạo
                if (string.IsNullOrEmpty(_maKhoa) && string.IsNullOrEmpty(_maPhong))
                {
                    usc_SelectBoxKhoa.txtMa.Text = _thongTinGiuong[5].ToString();
                    DataRow drKhoa = getRowByID(usc_SelectBoxKhoa.DataSource, "MAKP = " + _thongTinGiuong[5].ToString());
                    usc_SelectBoxKhoa.txtTen.Text = drKhoa["TENKP"].ToString();
                    usc_SelectBoxPhong.txtMa.Text = _thongTinGiuong[6].ToString();
                    DataRow drPhong = getRowByID(usc_SelectBoxPhong.DataSource, "MA = " + _thongTinGiuong[6].ToString());
                    usc_SelectBoxPhong.txtTen.Text = drPhong["TEN"].ToString();
                    txtTen.Focus();
                }
                DataTable tb = _acc.Get_Data(_tT.QueryGiaVienPhiCT(_idGiuong));
                dgvGiaGoiY.DataSource = tb;
                col_DoiTuong.DataPropertyName = "DOITUONG";
                col_MaDoiTUong.DataPropertyName = "MADOITUONG";
                col_TenGiuong.DataPropertyName = "TEN";
                col_Gia.DataPropertyName = "GIA";
                col_id.DataPropertyName = "ID";
                txtTen.Text = _tenGiuong;
            }
            if (!string.IsNullOrEmpty(_tenKhoaLoadNgoai))
            {
                usc_SelectBoxKhoa.txtMa.Text = _maKhoaLoadNgoai;
                usc_SelectBoxKhoa.txtTen.Text = _tenKhoaLoadNgoai;
                usc_SelectBoxPhong.txtTen.Focus();
            }
            if (!string.IsNullOrEmpty(_tenPhongLoadNgoai))
            {
                usc_SelectBoxPhong.txtMa.Text = _maPhongLoadNgoai;
                usc_SelectBoxPhong.txtTen.Text = _tenPhongLoadNgoai;
                usc_SelectBoxLoaiGiuong.txtTen.Focus();
            }

        }

        private void frmDanhMucGiuong_FormClosed(object sender, FormClosedEventArgs e)
        {
            _statusCloseForm = true;
        }

        private void btnGoiYGia_Click(object sender, EventArgs e)
        {
            try
            {
                _sql = _tT.QueryGoiYGia(txtTen.Text);
                _tbGiaGoiY = _acc.Get_Data(_sql);
                dgvGiaGoiY.DataSource = _tbGiaGoiY;
                col_TenGiuong.DataPropertyName = "TENGIUONG";
                col_DoiTuong.DataPropertyName = "DOITUONG";
                col_MaDoiTUong.DataPropertyName = "MADOITUONG";
                col_Gia.DataPropertyName = "GIA";
                col_id.DataPropertyName = "ID";
            }
            catch
            {
                return;
            }
        }

        private void usc_SelectBoxPhong_HisSelectChange(object sender, EventArgs e)
        {
            try
            {
                _maPhong = usc_SelectBoxPhong.txtMa.Text;
                _sql = _tT.Queryusc_SelectBoxPhongChange(usc_SelectBoxKhoa.txtMa.Text, _maPhong);
                txtTen.Text = "";
                DataTable tb = _acc.Get_Data(_sql);
                if (_isAdd)
                {
                    itgSTT.Value = tb.Rows.Count + 1;// cboBHYT.Text = string.Empty;
                }
            }
            catch
            {
                return;
            }
        }

        private void usc_SelectBoxKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void dgvDanhMucGiuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string id, sTT, tenGiuong, maLoaiGiuong, tenLoaiGiuong, maKP, tenKP, maPhong, tenPhong = string.Empty;
                id = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_IdG"].Value.ToString();
                sTT = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_SttG"].Value.ToString();
                tenGiuong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenGiuongG"].Value.ToString();
                maLoaiGiuong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_LoaiGiuongG"].Value.ToString();
                tenLoaiGiuong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenLoaiG"].Value.ToString();
                maKP = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_MaKPG"].Value.ToString();
                tenKP = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenKP"].Value.ToString();
                maPhong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_IdPhongG"].Value.ToString();
                tenPhong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenPhongG"].Value.ToString();
                txtTen.Text = tenGiuong;

                itgSTT.Value = 0; itgSTT.Text = string.Empty; itgSTT.Text = sTT; itgSTT.Value = int.Parse(sTT);
                usc_SelectBoxKhoa.txtMa.Text = maKP; usc_SelectBoxKhoa.txtTen.Text = tenKP;
                usc_SelectBoxLoaiGiuong.txtMa.Text = maLoaiGiuong; usc_SelectBoxLoaiGiuong.txtTen.Text = tenLoaiGiuong;
                usc_SelectBoxPhong.txtMa.Text = maPhong; usc_SelectBoxPhong.txtTen.Text = tenPhong;
                txtTen.Text = tenGiuong;

                //btnGoiYGia_Click(null, null);
                //Hiển thị giá của giương
                DataTable tb = _acc.Get_Data(_tT.QueryGiaVienPhiCT(id));
                dgvGiaGoiY.DataSource = tb;
                col_DoiTuong.DataPropertyName = "DOITUONG";
                col_MaDoiTUong.DataPropertyName = "MADOITUONG";
                col_TenGiuong.DataPropertyName = "TEN";
                col_Gia.DataPropertyName = "GIA";
                col_id.DataPropertyName = "ID";
                txtTen.Text = tenGiuong;
                //Kết thúc 
                string command = dgvDanhMucGiuong.Columns[e.ColumnIndex].Name;
                if (command == "col_SuaG")
                {
                    Sua();
                    usc_SelectBoxKhoa.Enabled = false;
                }
                if (command == "col_XoaG")
                {
                    //180407
                    try
                    {
                        int dem = 0;
                        for (int i = 0; i < tb.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(tb.Rows[i]["GIA"].ToString()) && tb.Rows[i]["GIA"].ToString()!="0")
                            {
                                dem++;
                            }
                        }
                        if (dem == 0)
                        {
                            _deleteOneRecord = 1;
                            Xoa();
                        }
                        else
                        {
                            //Không thể xóa
                            TA_MessageBox.MessageBox.Show("Không thể xóa!\n Giường này đã được khai báo giá!" , TA_MessageBox.MessageIcon.Error);
                            usc_SelectBoxKhoa.txtTen.Focus();
                            return;
                        }
                    }
                    catch
                    {
                    }


                }
                if (command == "col_ChonG")
                {
                    DataGridViewRow dr = (DataGridViewRow)dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex];
                    DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell chks = (DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell)dr.Cells[0];
                    if (chks.Selected && chks.FormattedValue.Equals("False") && !_lstCheck.Contains(id))
                    {
                        _lstCheck.Add(id);
                    }
                    if (chks.Selected && chks.FormattedValue.Equals("True"))
                    {
                        _lstCheck.Remove(id);
                        _statusCheckAll = 1;
                    }
                }
            }
            catch
            {


            }
        }

        private void usc_SelectBoxKhoa_HisSelectChange(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
                {
                    ClearList();
                    _lst.Add(cls_DanhMucPhong.col_ID);
                    _lst.Add(cls_DanhMucPhong.col_TEN);
                    _lst.Add(cls_DanhMucPhong.col_MAKP);
                    _lst.Add(cls_DanhMucPhong.col_MA);
                    _lst2.Add(cls_DanhMucPhong.col_MAKP, usc_SelectBoxKhoa.txtMa.Text);
                    DataTable tb = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
                    _maKhoa = usc_SelectBoxKhoa.txtMa.Text;
                    if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
                    {
                        _dtp = tb;
                        usc_SelectBoxPhong.DataSource = _dtp;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void dgvDanhMucGiuong_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void usc_SelectBoxKhoa_HisMaTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
                {
                    ClearList();
                    _lst.Add(cls_DanhMucPhong.col_ID);
                    _lst.Add(cls_DanhMucPhong.col_TEN);
                    _lst.Add(cls_DanhMucPhong.col_MAKP);
                    _lst.Add(cls_DanhMucPhong.col_MA);
                    _lst2.Add(cls_DanhMucPhong.col_MAKP, usc_SelectBoxKhoa.txtMa.Text);
                    DataTable tb = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
                    _maKhoa = usc_SelectBoxKhoa.txtMa.Text;
                    if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
                    {
                        _dtp = tb;
                        usc_SelectBoxPhong.DataSource = _dtp;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void btnGoiYGia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvGiaGoiY.DataSource != null)
            {
                btnLuu.Focus();
            }
        }

        private void usc_SelectBoxKhoa_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void usc_SelectBoxKhoa_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }

        private void usc_SelectBoxKhoa_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void usc_SelectBoxLoaiGiuong_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void usc_SelectBoxPhong_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void usc_SelectBoxLoaiGiuong_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }

        private void usc_SelectBoxPhong_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }

        private void ckGiuongHu_CheckedChanged(object sender, EventArgs e)
        {
            ckChuaSuDung.Checked = false;
        }

        private void ckChuaSuDung_CheckedChanged(object sender, EventArgs e)
        {
            ckGiuongHu.Checked = false;
        }

        private void dgvDanhMucGiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboard(dgvDanhMucGiuong);
            }
        }

        private void PasteClipboard(DataGridView myDataGridView)
        {
            try
            {
                _isImport = true;
                _tbSort.Clear();
                _tbCoppy = _tbDanhMucGiuong.Clone();
                _tbCoppyUdp = _tbDanhMucGiuong.Clone();

                foreach (DataGridViewCell cell in dgvDanhMucGiuong.SelectedCells)
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
                                DataRow dr = _tbDanhMucGiuong.NewRow();
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
                    }

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
                            dr2["STT"] = dgvDanhMucGiuong.Rows[rowIndex + i - minR].Cells["col_SttG"].Value;
                            for (int j = minC; j <= maxC; j++)
                            {
                                _value = _tbSort2[string.Format("{0}-{1}", i.ToString().PadLeft(5, '0'), j.ToString().PadLeft(5, '0'))];
                                if (rowIndex + i - minR >= dgvDanhMucGiuong.Rows.Count - 1)
                                {
                                    if (_bsNhap == null)
                                    {
                                        _bsNhap = new BindingSource();
                                    }
                                    _bsNhap.AddNew();
                                    themmoi = true;
                                }
                                    dr2[columnIndex + j - minC] = _value;
                                dgvDanhMucGiuong.Rows[rowIndex + i - minR].Cells[columnIndex + j - minC].ValueType = typeof(string);

                                dgvDanhMucGiuong.Rows[rowIndex + i - minR].Cells[columnIndex + j - minC].Value = _value;
                            }
                                _tbCoppyUdp.Rows.Add(dr2);
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

        private void dgvDanhMucGiuong_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //
            try
            {
                int index = dgvDanhMucGiuong.CurrentRow.Index;
                _tbCoppyUdp.Rows.RemoveAt(index);
            }
            catch //(Exception)
            {
                
                //throw;
            }

        }

        #endregion

    }
}
