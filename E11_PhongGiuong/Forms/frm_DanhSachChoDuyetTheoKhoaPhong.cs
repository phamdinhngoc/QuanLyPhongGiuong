using System;
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

namespace E11_PhongGiuong
{
    public partial class frm_DanhSachChoDuyetTheoKhoaPhong : frm_Base
    {
        #region Khai báo biến toàn cục
        private const int _soluonbntoida = 2;
        private cls_ThucThiDuLieu _tT;
        private string _idGiuong = string.Empty;
        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private string _maBNChon;
        private string _userError, _systemError, _id = string.Empty;
        public bool _status;
        private int _loai = 0;
        private string _sql = string.Empty;
        // public bool _statusCloseForm;
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
        private string _maKhoaPhong, _idGiuongChonChuyen, _idPhong = string.Empty;
        private string _maKhoaLoad, _maPhongLoad, _indexOptionLoad = string.Empty;
        private string _tenKhoaLoad, _tenPhongLoad, _tenOptionLoad = string.Empty;
        private string[] _thongTinGiuongChonChuyen;
        private int _option;
        private int _loaibn = -1;
        private string _idGiuongLoad;
        private List<string> _lstOfAffect;
        private string _tinhtranggiuong = "";

        public cls_ThucThiDuLieu TT
        {
            get
            {

                if (_tT == null)
                {
                    _tT = new cls_ThucThiDuLieu();
                }
                return _tT;
            }

            set
            {
                _tT = value;
            }
        }

        public List<string> LstOfAffect
        {
            get
            {
                if (_lstOfAffect == null)
                {
                    _lstOfAffect = new List<string>();
                }
                return _lstOfAffect;
            }

            set
            {
                _lstOfAffect = value;
            }
        }


        #endregion

        #region Khởi tạo 

        public frm_DanhSachChoDuyetTheoKhoaPhong(int loaibn =-1)
        {
            InitializeComponent();
            _loaibn = loaibn;
        }

    
        public frm_DanhSachChoDuyetTheoKhoaPhong(string idgiuongLoad, string tinhtranggiuong,int loai, int loaibn = -1, string makp ="")
        {
            InitializeComponent();
            _idGiuongLoad = idgiuongLoad;
            _tinhtranggiuong = tinhtranggiuong;
            _loai = loai;
            _loaibn = loaibn;

            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "LoadTheoKhoaPhong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);
            string khoa = TT.GetKPUser(E00_System.cls_System.sys_UserID);
            if ((dtt != null && dtt.Rows.Count > 0) && !string.IsNullOrEmpty(khoa) && E00_System.cls_System.sys_UserID != "1")

            {

                if (makp != khoa)
                {
                    pnlBot.Visible = false;
                    return;
                }
                pnlBot.Visible = true;
            }
        }

        #endregion

        #region Phương thức 

        #region Lấy dữ liệu thông tin giường từ bảng đặt giường
        private DataRow RowThongTinHienDIen(string maBN)
        {
            //_sql = string.Format("select * from {0}.datgiuong where idgiuong = {1} and mabn='{2}'", _acc.Get_User(), idGiuong, maBN);
            _sql = string.Format("select * from {0}.{1} where {2} = '{3}' ", _acc.Get_User(), cls_HienDien.tb_TenBang, cls_HienDien.col_MaBN, maBN);
            return _acc.Get_Data(_sql).Rows[0];
        }
      
        #endregion

        #region Cập nhật trạng thái giường
        private void UpdateTrangThai(string id)
        {
            try
            {
                int soLuong = TT.GetSoLuongNguoiNamTaiGiuong(_idGiuong);
                //if (TT.GiuongDaCoNguoiNam(_idGiuong) && soLuong > _soluonbntoida - 1)
                //{
                //    TA_MessageBox.MessageBox.Show("Giường này đã quá số lượng người nằm!\n Không thể duyệt cho bệnh nhân này vào giường!",
                //        TA_MessageBox.MessageIcon.Information);
                //    return;
                //}
                //else
                {
                    if (dgvDSBN.Rows.Count > 0)
                    {
                        
                        {
                            //Them giuong vao theo doi giuong
                            DataRow drTTDat = TT.RowThongTinTheoDoiGiuongbyid(id).Rows[0];
                            DataRow drhiendien = RowThongTinHienDIen(_maBNChon);
                            DateTime valuehientai = TT.GetSysDate();
                            DateTime valueTu = TT.GetSysDate();
                            DateTime valueDen = DateTime.MaxValue;
                            string madoituong = TT.GETMADOITUONG(_maBNChon, drhiendien[cls_HienDien.col_MaQL] + "", drhiendien[cls_HienDien.col_MaVaoVien] + "");
                            if (!string.IsNullOrEmpty(drTTDat[cls_PG_TheoDoiGiuong.col_DEN].ToString()))
                            {
                                valueDen = DateTime.Parse(drTTDat[cls_PG_TheoDoiGiuong.col_DEN].ToString());
                            }
                            if (!string.IsNullOrEmpty(drTTDat[cls_PG_TheoDoiGiuong.col_TU].ToString()))
                            {
                                valueTu = DateTime.Parse(drTTDat[cls_PG_TheoDoiGiuong.col_TU].ToString());
                            }
                            if (valueTu < valuehientai && valuehientai < valueDen)
                            {
                                if (!TT.UpdateTheoDoiGiuong(id, Tu: valuehientai.ToString("dd/MM/yyyy HH:mm:ss"), Den: DateTime.MaxValue.ToString("dd/MM/yyyy HH:mm:ss"), trangthaibn: "1", MADT: madoituong, MAVV: drhiendien[cls_HienDien.col_MaVaoVien] + "", MAQL: drhiendien[cls_HienDien.col_MaQL] + ""))
                                {
                                    TA_MessageBox.MessageBox.Show(string.Format(" Lỗi Duyệt giường!!!!",
                                      _userError)
                               , TA_MessageBox.MessageIcon.Error);
                                    return;
                                }

                            }
                            else
                            {
                                if (TA_MessageBox.MessageBox.Show("Hiện tại đã quá giờ đặt giường của bệnh nhân này bạn vẫn muốn duyệt cho bệnh nhân vào giường ? ", TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                                {
                                    if (!TT.UpdateTheoDoiGiuong(id, issudung: false))
                                    {
                                        TA_MessageBox.MessageBox.Show("Không thể hủy bệnh nhân!", TA_MessageBox.MessageIcon.Error);
                                        _status = false;
                                        return;
                                    }

                                    if (!TT.InsertThongTinDatGiuongVaoTheoDoiGiuong(TT.GetIdTheoDoiGiuong(), drTTDat[cls_PG_TheoDoiGiuong.col_MAKP].ToString(), _maBNChon, _idGiuong,
                                                                                                  valuehientai, DateTime.MaxValue,
                                                                                                  1,
                                                                                                  "0", madoituong, drTTDat[cls_PG_TheoDoiGiuong.col_GHICHU].ToString(), "1", id, drhiendien[cls_HienDien.col_MaVaoVien] + "", drhiendien[cls_HienDien.col_MaQL] + ""))
                                    {
                                        TA_MessageBox.MessageBox.Show(string.Format(" Lỗi Duyệt giường!!!!",
                                       _userError)
                                , TA_MessageBox.MessageIcon.Error);
                                        return;
                                    }
                                }
                            }
                            //Xóa giường đã đặt trong table đặt giường
                            //   if (TT.DeleteGiuongDaDat(_maBNChon, _idGiuong))


                            //Cap nhat lai trang thai cua giuong

                            if (!TT.UpdateTinhTrangDanhMucGiuong(_idGiuong, "2"))
                            {
                                TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                                    _userError)
                             , TA_MessageBox.MessageIcon.Error);
                                return;
                            }
                            _status = true;
                            if (!LstOfAffect.Contains(_idGiuong))
                            {
                                LstOfAffect.Add(_idGiuong);
                            }
                            TA_MessageBox.MessageBox.Show("Duyệt bệnh nhân thành công!", TA_MessageBox.MessageIcon.Information);

                            if (_loai == 0)
                            {
                                btnXem_Click(null, null);
                            }
                            else
                            {
                                this.Close();
                            }


                        }
                    }
                }
            }
            catch
            {

                return;
            }

        }
        #endregion

        #region Hủy đặt giường
        private void HuyDatGiuong(string idGiuong, string id)
        {
            try
            {
               
                if (_loai != 0)
                {
                    string tenBN = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_TenBN"].Value + "";
                    DialogResult rt = TA_MessageBox.MessageBox.Show(string.Format("Bản muốn huỷ đặt giường của bệnh nhân {0} không ? ", tenBN), TA_MessageBox.MessageIcon.Question);
                    if (rt == DialogResult.No )
                    {
                        this.Close();
                        return;
                    } 
                }
                    //_sql = string.Format("delete {0}.{1} where {2}='{3}' ", _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_ID,id);
                    if (!_tT.UpdateTheoDoiGiuong(id, issudung: false))
                    {
                        TA_MessageBox.MessageBox.Show("Không thể hủy bệnh nhân!", TA_MessageBox.MessageIcon.Error);
                        _status = false;
                        return;
                    }


                    //Cập nhật trạng thái
                    //Kiểm tra nếu giường này hết người đặt thì cập nhật trạng thái về 0, còn có người đặt thì cập nhật trạng thái về 1
                    string slBNCho = TT.GetDanhSachChoDuyet(_idGiuong).Rows.Count.ToString();
                    if (!string.IsNullOrEmpty(slBNCho) && int.Parse(slBNCho) > 0 && !TT.GiuongDaCoNguoiNam(_idGiuong))
                    {
                        if (!_tT.UpdateTinhTrangDanhMucGiuong(_idGiuong, "1"))
                        {
                            TA_MessageBox.MessageBox.Show("Lỗi cập nhật trạng thái giường"
                         , TA_MessageBox.MessageIcon.Error); _status = false;
                            return;
                        }
                    }
                    else if (!string.IsNullOrEmpty(slBNCho) && int.Parse(slBNCho) == 0 && !TT.GiuongDaCoNguoiNam(_idGiuong))
                    {
                        if (!_tT.UpdateTinhTrangDanhMucGiuong(_idGiuong, "0"))
                        {
                            TA_MessageBox.MessageBox.Show("Lỗi cập nhật trạng thái giường"
                         , TA_MessageBox.MessageIcon.Error); _status = false;
                            return;
                        }
                    }
                    else if (!_tT.UpdateTinhTrangDanhMucGiuong(_idGiuong, "2"))
                    {
                        TA_MessageBox.MessageBox.Show("Lỗi cập nhật trạng thái giường"
                     , TA_MessageBox.MessageIcon.Error); _status = false;
                        return;
                    }
                    if (!LstOfAffect.Contains(idGiuong))
                    {
                        LstOfAffect.Add(idGiuong);
                    }
                    TA_MessageBox.MessageBox.Show("Đã hủy bệnh nhân!", TA_MessageBox.MessageIcon.Information);
                    _status = true;
                    if (_loai == 0)
                    {
                        btnXem_Click(null, null);
                    }
                    else
                    {
                        this.Close();
                    } 
                
           
            }
            catch
            {
                _status = false;
                return;
            }

        }
        #endregion
        #region Hủy Duyet giường
        private void HuyDuyetGiuong(string idGiuong, string id)
        {
            if (_loai != 0)
            {
                string tenBN = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_TenBN"].Value + "";
                DialogResult rt = TA_MessageBox.MessageBox.Show(string.Format("Bản muốn huỷ duyệt giường của bệnh nhân {0} không ? ", tenBN), TA_MessageBox.MessageIcon.Question);
                if (rt == DialogResult.No)
                {
                    this.Close();
                    return;
                }
            }

            if (dgvDSBN.Rows.Count > 0)
            {

                {
                    //Them giuong vao theo doi giuong
                    DataTable dtgiuong = TT.RowThongTinTheoDoiGiuong(idGiuong);
                    DataRow drtheodoigiuong = dtgiuong.NewRow();
                    try
                    {
                        drtheodoigiuong = dtgiuong.Select(cls_PG_TheoDoiGiuong.col_MABN + " = '" + _maBNChon + "' ")[0];
                    }
                    catch (Exception)
                    {
                    }

                    #region thêm lại bn vào đặt giường

                    if (TT.UpdateTheoDoiGiuong(id, trangthaibn: "0"))
                    {


                        if (dtgiuong.Rows.Count <= 1)
                        {
                            if (!TT.UpdateTinhTrangDanhMucGiuong(_idGiuong, "1"))
                            {
                                TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                                    _userError)
                             , TA_MessageBox.MessageIcon.Error);

                                return;

                            }
                        }

                        _status = true;
                        if (!LstOfAffect.Contains(idGiuong))
                        {
                            LstOfAffect.Add(idGiuong);
                        }
                        TA_MessageBox.MessageBox.Show("Hủy duyệt bệnh nhân thành công!", TA_MessageBox.MessageIcon.Information);
                        if (_loai == 0)
                        {
                            btnXem_Click(null, null);
                        }
                        else
                        {
                            this.Close();
                        }


                    }


                    #endregion
                }
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtTimKiem.Text)) (dgvDSBN.DataSource as DataTable).DefaultView.RowFilter = string.Format("{1} LIKE '{0}%' OR {1} LIKE '% {0}%' OR {2} LIKE '{0}%' OR {2} LIKE '% {0}%' OR {3} LIKE '{0}%' OR {3} LIKE '% {0}%' ", txtTimKiem.Text, "HOTEN", "MABN", "TENGIUONG");
                else (dgvDSBN.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            catch (Exception)
            {

               
            }
        }

        private void dgvDSBN_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if ("1" == dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["ColMaTrangThai"].Value + "")
                {
                    btnHuyDuyet.Enabled = false;
                    btnHuyDat.Enabled = true;
                    btnDuyet.Enabled = true;
                    btnChuyen.Enabled = true;
                    btnXuat.Enabled = false;
                    btnChoXuatVien.Enabled = false;
                }
                if ("2" == dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["ColMaTrangThai"].Value + "")
                {
                    btnHuyDuyet.Enabled = true;
                    btnHuyDat.Enabled = false;
                    btnDuyet.Enabled = false;
                    btnChuyen.Enabled = true;
                    btnXuat.Enabled = false;
                    btnChoXuatVien.Enabled = true;
                }
                if ("3" == dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["ColMaTrangThai"].Value + "")
                {
                    btnHuyDuyet.Enabled = false;
                    btnHuyDat.Enabled = false;
                    btnDuyet.Enabled = false;
                    btnChuyen.Enabled = false;
                    btnXuat.Enabled = true;
                    btnChoXuatVien.Enabled = false;
                }
            }
            catch (Exception)
            {

              
            }
        }
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
        private void btnXuat_Click(object sender, EventArgs e)
        {
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "XuatGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            string ID = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_ID"].Value + "";
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                _maBNChon = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaBN"].Value.ToString();
                _idGiuong = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaGiuong"].Value + "";
                #region Xuất Giường

                if (kiemtraXuat())
                {

                    string tenbn  = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_TenBN"].Value + "";
                    if (TA_MessageBox.MessageBox.Show("Cho bệnh nhân " + _maBNChon + " : " + tenbn + " xuất giường ?",

                                TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Cap nhat du lieu cua benh nhan trong table theo doi giuong
                        if (!TT.UpdateTheoDoiGiuong(ID, Den: TT.GetSysDate().ToString("dd-MM-yyyy HH:mm:ss"), issudung: false))
                        {
                            TA_MessageBox.MessageBox.Show("Lỗi cập nhật theo doi giường", TA_MessageBox.MessageIcon.Error);
                            return;
                        }
                        //_sql = string.Format("update {0}.{1} set {2} = {3} where {4}='{5}' and {6}='{7}'",
                        //    _acc.Get_User(),cls_PG_TheoDoiGiuong.tb_TenBang,cls_PG_TheoDoiGiuong.col_DEN=);

                        #region  update trang thái giường 
                        DataTable dt = TT.GetDanhSachDatVaDaDuyet(_idGiuong);
                        if (!LstOfAffect.Contains(_idGiuong))
                        {
                            LstOfAffect.Add(_idGiuong);
                        }
                        string _tagetStatus = "0";
                        if (dt.Select("Loai = '1'").Length > 0)
                        {
                            _tagetStatus = "1";

                        }
                        if (dt.Select("Loai = '2'").Length > 0)
                        {
                            _tagetStatus = "2";
                        }
                        if (!TT.UpdateTinhTrangDanhMucGiuongxUATgIUONG(_idGiuong, _tagetStatus))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                                _userError)
                         , TA_MessageBox.MessageIcon.Error);
                            return;
                        }
                        #endregion
                        TA_MessageBox.MessageBox.Show("Xuất giường bệnh nhân thành công!"
                                                    , TA_MessageBox.MessageIcon.Information);
                      
                        _status = true;
                        if (_loai == 0)
                        {
                            btnXem_Click(null, null);
                        }
                        else
                        {
                            this.Close();
                        }






                    }
                }

                


                #endregion

            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền xuất giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }
        #endregion

        #endregion

        #region Sự kiện

        private void frm_DanhSachChoDuyetTheoKhoaPhong_Load(object sender, EventArgs e)
        {
            col_MaBN.DataPropertyName = "MABN";
            col_TenBN.DataPropertyName = "HOTEN";
            col_MaGiuong.DataPropertyName = "IDGIUONG";
            col_TenGiuong.DataPropertyName = "TENGIUONG";
            col_MaPhong.DataPropertyName = "MAPHONG";
            col_TenPhong.DataPropertyName = "TENPHONG";
            col_MaKhoa.DataPropertyName = "MAKP";
            col_TenKhoa.DataPropertyName = "TENKP";
            ColNgayDat.DataPropertyName = "TU";
            ColTrangThai.DataPropertyName = "TenLoai";
            ColDiaChi.DataPropertyName = "diachi";
            ColMaTrangThai.DataPropertyName = "Loai";
            col_ID.DataPropertyName = cls_PG_TheoDoiGiuong.col_ID;
            dgvDSBN.AutoGenerateColumns = false;

            pnlTop.Visible = true;
            pnlTop.Height = 35;
            //Set Source cho Khoa
            DataTable dt;
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "LoadTheoKhoaPhong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);
            string khoa = TT.GetKPUser(E00_System.cls_System.sys_UserID);
            if ((dtt != null && dtt.Rows.Count > 0) && !string.IsNullOrEmpty(khoa) && E00_System.cls_System.sys_UserID != "1")

            {
                try
                {
                    dt = TT.SetSourceKhoaPhongconguoi().Select(cls_DanhMucPhong.col_MAKP + " = '" + khoa + "' ").CopyToDataTable();

                }
                catch (Exception)
                {
                    dt = null;
                }
                usc_KhoaPhong.txtMa.Enabled = false;
                usc_KhoaPhong.txtTen.Enabled = false;
                usc_KhoaPhong.btnShow.Enabled = false;
            }
            else
            {
                dt = TT.SetSourceKhoaPhongconguoi();
                if (dt != null)
                {
                    DataRow drPhong = dt.NewRow();
                drPhong[cls_BTDKP_BV.col_MaKP] = "";
                drPhong[cls_BTDKP_BV.col_TenKP] = "Tất cả";
                dt.Rows.Add(drPhong);
                }
                usc_KhoaPhong.txtMa.Enabled = true;
                usc_KhoaPhong.txtTen.Enabled = true;
                usc_KhoaPhong.btnShow.Enabled = true;


            }
            if (dt!=null)
            {
               
                usc_KhoaPhong.DataSource = dt;
                if (dt.Rows.Count==1)
                {
                    usc_KhoaPhong.his_SetSelectedIndex = 0; 
                }
                btnXem.Enabled = true;
                btnXem_Click(null, null);
            }
            else
            {
                btnXem.Enabled = false;
            }
           
            //       usc_KhoaPhong.his_SetSelectedIndex = 0; 

        }

        private void pnlBot_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHuyDat_Click(object sender, EventArgs e)
        {
            try
            {
               
                _maBNChon = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaBN"].Value + "";
                    _idGiuong = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaGiuong"].Value + "";
                    string ID = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_ID"].Value + "";
                    if ("1" == dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["ColMaTrangThai"].Value + "")
                    {
                        string userError = "", systemError = "";
                        Dictionary<string, string> _lst2 = new Dictionary<string, string>();
                        _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "HuyDatGiuong");
                        _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
                        DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

                        if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
                        {
                            HuyDatGiuong(_idGiuong, ID);
                        }

                    }
                
            }
            catch (Exception)
            {

                
            }
           
        }

        private void btnChoXuatVien_Click(object sender, EventArgs e)
        {

            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "ChoXuatVien");

            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);


            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                if (kiemtraXuat())
                {
                    _maBNChon = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaBN"].Value.ToString();
                _idGiuong = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaGiuong"].Value + "";
                string ID = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_ID"].Value + "";
              
                    if (!TT.UpdateTheoDoiGiuong(ID, trangthaibn: "2"))
                    {
                        TA_MessageBox.MessageBox.Show(string.Format(" Lỗi Không thể cập nhật trạng thái bệnh nhân !",
                          _userError)
                   , TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    if (!TT.UpdateTinhTrangDanhMucGiuongxUATgIUONG(_idGiuong, "5"))
                    {
                        TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                            _userError)
                     , TA_MessageBox.MessageIcon.Error);
                        return;
                    } 
               
                _status = true;
                if (!LstOfAffect.Contains(_idGiuong))
                {
                    LstOfAffect.Add(_idGiuong);
                }
                TA_MessageBox.MessageBox.Show("Cập nhật chờ xuất viện cho bệnh nhân thành công!", TA_MessageBox.MessageIcon.Information);
                }
                if (_loai == 0)
                {
                    btnXem_Click(null, null);
                }
                else
                {
                    this.Close();
                }
               
            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền duyệt giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "DuyetGiuong");

            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);


            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                _maBNChon = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaBN"].Value.ToString();
                _idGiuong = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaGiuong"].Value + "";
                string ID = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_ID"].Value + "";
                if (kiemtra())
                {
                    UpdateTrangThai(ID);

                }
            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền duyệt giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }
        private bool kiemtra()
        {
            bool ret = true;

            if (!TT.TonTaihiendien(_maBNChon))
            {
                TA_MessageBox.MessageBox.Show("Bện nhân chưa vào viện ",

                                TA_MessageBox.MessageIcon.Error);
                return false;
            }
            string tengiuong = "";

            if (TT.CheckMaBN(_maBNChon, out tengiuong))
            {
                TA_MessageBox.MessageBox.Show("MaBN này đã duyệt giường " + tengiuong + " rồi không thể duyệt thêm nữa ",
                    TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return ret;
        }
        private bool kiemtraXuat()
        {
            bool ret = true;

            if (!TT.KiemTraXuatGiuong(_maBNChon))
            {
                TA_MessageBox.MessageBox.Show("Bện nhân không có trong danh sách chờ xuất viện ",

                                TA_MessageBox.MessageIcon.Error);
                return false;
            }

            return ret;
        }
        private void frm_DanhSachChoDuyetTheoKhoaPhong_FormClosed(object sender, FormClosedEventArgs e)
        {
            //  _statusCloseForm = true;
        }

        private void usc_KhoaPhong_HisSelectChange(object sender, EventArgs e)
        {
            //SetSourcePhong
            usc_SelectBoxPhongTimKiem.clear();
            DataTable tb = TT.SetSourcePhongconguoi(usc_KhoaPhong.txtMa.Text);

            //_api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
            if (!string.IsNullOrEmpty(usc_KhoaPhong.txtMa.Text))
            {
                DataRow drPhong = tb.NewRow();
                drPhong[cls_DanhMucPhong.col_ID] = -1;
                drPhong[cls_DanhMucPhong.col_TEN] = "Tất cả";
                tb.Rows.Add(drPhong);
                usc_SelectBoxPhongTimKiem.DataSource = tb;
                usc_SelectBoxPhongTimKiem.Enabled = true;
                //  usc_KhoaPhong.his_SetSelectedIndex = 0;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_idGiuongLoad))
            {

                DataTable dt = TT.GetDanhSachDatVaDaDuyet(_idPhong, usc_KhoaPhong.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text,_loaibn!=-1?_loaibn+"":"");
                dgvDSBN.AutoGenerateColumns = false;
                dgvDSBN.DataSource = dt;// _tT.GetDataDanhSachChoDuyet(usc_KhoaPhong.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text);
            }
            else
            {
                pnlTop.Visible = false;
                pnlTop.Height = 0;
               DataTable dt = TT.GetDanhSachDatVaDaDuyet(_idGiuongLoad,loai: _loaibn != -1 ? _loaibn + "" : "");
                dgvDSBN.AutoGenerateColumns = false;
                dgvDSBN.DataSource = dt;// _tT.GetDataDanhSachChoDuyet(usc_KhoaPhong.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text);
            }
            switch (_loai)
            {
                case 1:
                    if (dgvDSBN.Rows.Count == 1)
                    {
                        dgvDSBN.Rows[0].Selected = true;
                       btnHuyDat_Click(null, null);
                    }
                    break;
                case 2:
                    if (dgvDSBN.Rows.Count==1)
                    {
                        dgvDSBN.Rows[0].Selected = true;
                        btnDuyet_Click(null, null);
                    }

                    break;
                case 3:
                    if (dgvDSBN.Rows.Count == 1)
                    {
                        dgvDSBN.Rows[0].Selected = true;
                        btnHuy_Click(null, null);
                    }
                    break;
                case 4:
                    if (dgvDSBN.Rows.Count == 1)
                    {
                        dgvDSBN.Rows[0].Selected = true;
                        btnChuyen_Click(null, null);
                    }
                    break;
                case 5:
                    if (dgvDSBN.Rows.Count == 1)
                    {
                        dgvDSBN.Rows[0].Selected = true;
                       btnXuat_Click(null, null);
                    }
                    break;
                case 6:
                    if (dgvDSBN.Rows.Count == 1)
                    {
                        dgvDSBN.Rows[0].Selected = true;
                        btnChoXuatVien_Click(null, null);
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                _maBNChon = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaBN"].Value + "";
                _idGiuong = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaGiuong"].Value + "";
                string ID = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_ID"].Value + "";
            if ("2" == dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["ColMaTrangThai"].Value + "")
                {
                    string userError = "", systemError = "";
                    Dictionary<string, string> _lst2 = new Dictionary<string, string>();
                    _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "HuyDuyetGiuong");
                    _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
                    DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

                    if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
                    {
                        HuyDuyetGiuong(_idGiuong, ID);
                    }
                }




            }
            catch
            {

                //throw;
            }
            // this.Close();

        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            _maBNChon = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaBN"].Value + "";
            _idGiuong = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_MaGiuong"].Value + "";
            string ID = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["col_ID"].Value + "";
            string trangthai = dgvDSBN.Rows[dgvDSBN.CurrentRow.Index].Cells["ColMaTrangThai"].Value + "";
            //       Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            //Có thay đổi về giao diện
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "ChuyenGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region Chuyển giường
                frm_ChuyenGiuong frm = new frm_ChuyenGiuong(_maBNChon, _idGiuong, trangthai, ID);
                frm.ShowDialog();
                if (frm._status)
                {
                    #region  update trang thái giường chuyển
                    DataTable dt = TT.GetDanhSachDatVaDaDuyet(_idGiuong);
                    if (!LstOfAffect.Contains(_idGiuong))
                    {
                        LstOfAffect.Add(_idGiuong);
                    }
                    string _tagetStatus = "0";
                    if (dt.Select("Loai = '1'").Length > 0)
                    {
                        _tagetStatus = "1";

                    }
                    if (dt.Select("Loai = '2'").Length > 0)
                    {
                        _tagetStatus = "2";
                    }
                    if (!TT.UpdateTinhTrangDanhMucGiuong(_idGiuong, _tagetStatus))
                    {
                        TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                            _userError)
                     , TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    #endregion

                    #region  update trang thái giường nhan
                    string idgiuongnhan = frm.IdgiuongNhan;
                    if (!LstOfAffect.Contains(idgiuongnhan))
                    {
                        LstOfAffect.Add(idgiuongnhan);
                    }
                    DataTable dtnhan = TT.GetDanhSachDatVaDaDuyet(idgiuongnhan);
                    string tagetStatusNhan = "0";
                    if (dtnhan.Select("Loai = '1'").Length > 0)
                    {
                        tagetStatusNhan = "1";

                    }
                    if (dtnhan.Select("Loai = '2'").Length > 0)
                    {
                        tagetStatusNhan = "2";
                    }
                    if (!TT.UpdateTinhTrangDanhMucGiuong(idgiuongnhan, tagetStatusNhan))
                    {
                        TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                            _userError)
                     , TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    #endregion
                    TA_MessageBox.MessageBox.Show(string.Format("Chuyển giường thành công",
                           _userError)
                    , TA_MessageBox.MessageIcon.Information);
                  
                    _status = true;
                    if (_loai == 0)
                    {
                        btnXem_Click(null, null);
                    }
                    else
                    {
                        this.Close();
                    }


                }
                #endregion
            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền Chuyển giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        #endregion

    }
}
    