using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using E00_Common;

using E00_Model;
using System.Linq;
using E00_System;
using System.Threading;

namespace E11_PhongGiuong
{
    public partial class frm_NhapThongTinDatGiuongBenhNhan : E00_Base.frm_Base
    {

        #region Khai báo

        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
        private string _userError, _systemError, _id = string.Empty;
        private string makhoa = "";
        private string maphong = "";
        private string magiuong = "";

        private DataTable _tbBTDBN = new DataTable();
        private DataRow dr;
        public bool _status = false;
        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private string _sql = string.Empty;

        #endregion

        #region Khởi tạo

        #region Khởi tạo không tham số
        public frm_NhapThongTinDatGiuongBenhNhan()
        {
            InitializeComponent();
        }
        #endregion

        #region Khởi tạo có tham số
        public frm_NhapThongTinDatGiuongBenhNhan(string makhoa, string maphong, string magiuong, DataTable tbBTDBN)
        {
            InitializeComponent();
            this.makhoa = makhoa;
            this.maphong = maphong;
            this.magiuong = magiuong;
            _tbBTDBN = tbBTDBN;

        }
        #endregion

        #endregion

        #region Phương thức
        private void GetBTDBN()
        {


            _tbBTDBN = _acc.Get_Data(_tT.QueryBTDBN());
        }
        #region Xóa list
        private void ClearList()
        {
            _lst.Clear();
            _lst2.Clear();
            _lst3.Clear();
        }
        #endregion

        #region Load dữ liệu
        private void LoadData()
        {
            try
            {

                //Load đối tượng
                usc_SelectBoxDoiTuong.DataSource = _tT.GetDataDoiTuong();

                //SetSource usc_KhoaPhong
                usc_KhoaPhong.DataSource = _tT.SetSourceKhoaPhong();

                //Load mã khoa, tên khoa
                usc_KhoaPhong.SetTenByMa(makhoa);
                //Load mã phòng, tên phòng
                usc_SelectBoxPhongTimKiem.SetTenByMa(maphong);

                usc_Giuong.SetTenByMa(magiuong);


            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region Gán source bộ từ điển bệnh nhân

        #endregion

        #region Kiểm tra thông tin nhập vào
        public bool KiemTraThongTin()
        {
            if (string.IsNullOrEmpty(usc_KhoaPhong.txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Chọn khoa", TA_MessageBox.MessageIcon.Error);
                usc_KhoaPhong.txtTen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(usc_SelectBoxPhongTimKiem.txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Chọn phòng", TA_MessageBox.MessageIcon.Error);
                usc_SelectBoxPhongTimKiem.txtTen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(usc_Giuong.txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Chọn giường", TA_MessageBox.MessageIcon.Error);
                usc_Giuong.txtTen.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(usc_SelectBoxDoiTuong.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Chọn đối tượng", TA_MessageBox.MessageIcon.Error);
            //    txtTenBenhNhan.Focus();
            //    return false;
            //}
            if (_tT.CheckMaBNDat(txtMaBN.Text, usc_Giuong.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("MaBN này đã Đặt giường này rồi không thể đặt thêm nữa ! ",
                    TA_MessageBox.MessageIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(txtTenBenhNhan.Text))
            {
                TA_MessageBox.MessageBox.Show("Nhập tên bệnh nhân", TA_MessageBox.MessageIcon.Error);
                txtTenBenhNhan.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                TA_MessageBox.MessageBox.Show("Nhập Mã bệnh nhân", TA_MessageBox.MessageIcon.Error);
                txtTenBenhNhan.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(his_ComboboxPhai.Text))
            {
                TA_MessageBox.MessageBox.Show("Chọn phái", TA_MessageBox.MessageIcon.Error);
                txtTenBenhNhan.Focus();
                return false;
            }
            if (dtpTuNgay.Value == DateTime.MinValue)//Edited 300418
            {
                TA_MessageBox.MessageBox.Show("Nhập ngày bắt đầu đặt giường", TA_MessageBox.MessageIcon.Error);
                dtpTuNgay.Focus();
                return false;
            }
            //if (dtpDenNgay.Value == DateTime.MinValue)//Edited 300418
            //{
            //    TA_MessageBox.MessageBox.Show("Nhập ngày kết thúc đặt giường", TA_MessageBox.MessageIcon.Error);
            //    dtpDenNgay.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(usc_SelectBoxDoiTuong.txtMa.Text))//Edited 300418
            {
                TA_MessageBox.MessageBox.Show("Vui Lòng chọ đối tượng", TA_MessageBox.MessageIcon.Error);
                usc_SelectBoxDoiTuong.Focus();
                return false;
            }
            //if (!_tT.CheckMaBN(txtMaBN.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("MaBN này đã đặt giường rồi không thể đặt thêm nữa ",
            //        TA_MessageBox.MessageIcon.Information);
            //    return false;
            //}
            //if (_tT.chekbenhnhandatgiuo(txtMaBN.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Bệnh nhân này đã đặt giường rồi vui lòng chọn bệnh nhân khác !",
            //        TA_MessageBox.MessageIcon.Information);
            //    return false;
            //}
            if (dtpTuNgay.Value != DateTime.MinValue && dtpDenNgay.Value != DateTime.MinValue)
            {
                if (dtpTuNgay.Value > dtpDenNgay.Value)
                {
                    TA_MessageBox.MessageBox.Show("Thời gian không hợp lệ!", TA_MessageBox.MessageIcon.Error);
                    dtpTuNgay.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Kiểm tra trùng bệnh nhân
        private bool CheckMaBN(string maBN)
        {
            DataRow dr = getRowByID(_tbBTDBN, "MABN = '" + maBN + "'");
            if (dr == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Lấy datarow by id
        public DataRow getRowByID(DataTable dt, string exp)
        {
            try
            {

                DataRow[] r = dt.Select(exp);
                if (r.Length > 0)
                    return r[0];
                else return null;
            }
            catch (Exception EX)
            {


                return null;
            }

        }
        #endregion

        #endregion

        #region Sự kiện

        private void frmNhapThongTinDatGiuongBenhNhan_Load(object sender, EventArgs e)
        {
            LoadData();
            usc_SelectBoxPhongTimKiem.Enabled = usc_KhoaPhong.Enabled = usc_Giuong.Enabled = false;
            txtMaBN.Select();
            dtpTuNgay.Text = _tT.GetSysDate().ToString();
            dtpDenNgay.Text = _tT.GetSysDate().ToString();
        }

        private void usc_KhoaPhong_HisSelectChange(object sender, EventArgs e)
        {
            //SetSourcePhong
            ClearList();
            _lst.Add(cls_DanhMucPhong.col_MAPHONG);
            _lst.Add(cls_DanhMucPhong.col_TEN);
            _lst2.Add(cls_DanhMucPhong.col_MAKP, usc_KhoaPhong.txtMa.Text);
            DataTable tb = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang, _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
            if (!string.IsNullOrEmpty(usc_KhoaPhong.txtMa.Text))
            {
                usc_SelectBoxPhongTimKiem.DataSource = tb;
            }
        }

        private void usc_SelectBoxPhongTimKiem_HisSelectChange(object sender, EventArgs e)
        {
            string _maKhoaPhong = usc_KhoaPhong.txtMa.Text;
            string _maKhoa = usc_SelectBoxPhongTimKiem.txtMa.Text;
            int maKp;
            int.TryParse(usc_KhoaPhong.txtMa.Text, out maKp);
            if (_maKhoa == string.Empty)
            {
                _sql = _tT.GetDataPhongTimKiem(_maKhoaPhong);
            }
            else
            {

                _sql = _tT.GetDataPhongTimKiem1(magiuong, usc_KhoaPhong.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text);
            }
            if (!string.IsNullOrEmpty(usc_KhoaPhong.txtMa.Text))
            {
                DataTable tb = _acc.Get_Data(_sql);
                usc_Giuong.DataSource = tb;
            }
        }

        private void usc_SelectBoxDoiTuong_HisSelectChange(object sender, EventArgs e)
        {
            try
            {
                //string sqlGetGia = string.Format("select GIA from {0}.v_giavpct  "
                //                    + " where id_giavp={1} and madoituong={2}", _acc.Get_User(), 
                //                    usc_Giuong.txtMa.Text, usc_SelectBoxDoiTuong.txtMa.Text);
                //if (_tT.GetGiaGiaVienPhiCT(usc_Giuong.txtMa.Text, usc_SelectBoxDoiTuong.txtMa.Text).Rows.Count == 0)
                //{
                //    txtDonGia.Text = "0";
                //}
                //else
                //{
                //    txtDonGia.Text = _tT.GetGiaGiaVienPhiCT(usc_Giuong.txtMa.Text, usc_SelectBoxDoiTuong.txtMa.Text).Rows[0].ItemArray[0].ToString();
                //}
                usc_SelectBoxPhongTimKiem.Enabled = usc_KhoaPhong.Enabled = usc_Giuong.Enabled = true;
            }
            catch
            {

                return;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraThongTin())
                {
                    //Insert vào table bộ từ điển bệnh nhân
                    if (dr == null)
                    {
                        _tT.InsertTableBTDBN(txtMaBN.Text, txtTenBenhNhan.Text, dtpNgaySinh.Value.ToString("dd/MM/yyyy"),
                         dtpNgaySinh.Value.Year.ToString(), his_ComboboxPhai.Text, txtDiaChi.Text);
                    }

                    //insert thông tin đặt giường vào table theodoigiuong
                    string valueTu, valueDen;
                    valueTu = dtpTuNgay.Value.ToString("dd/MM/yyyy hh:mm:ss");
                    valueDen = DateTime.MaxValue.ToString("dd/MM/yyyy hh:mm:ss");

                    if (_tT.InsertThongTinDatGiuongVaoBangDatGiuong(_tT.GetSysDate().ToString("ddMMyyHHmmss"), usc_KhoaPhong.txtMa.Text, txtMaBN.Text, usc_Giuong.txtMa.Text,
                     dtpTuNgay.Value, (string.IsNullOrEmpty(dtpDenNgay.Text) ? DateTime.MaxValue : dtpDenNgay.Value),
                     1,
                     "0", txtTenBenhNhan.Text, txtGhiChu.Text, "0", usc_SelectBoxDoiTuong.txtMa.Text))
                    {
                        {

                            //Check tình trạng của giường
                            String _idGiuong = usc_Giuong.txtMa.Text;
                            string slBNCho = _tT.GetDanhSachChoDuyet(_idGiuong).Rows.Count.ToString();
                            bool isnguoinam = _tT.GiuongDaCoNguoiNam(_idGiuong);
                            if (!string.IsNullOrEmpty(slBNCho) && int.Parse(slBNCho) > 0 && !isnguoinam)
                            {
                                if (!_tT.UpdateTinhTrangDanhMucGiuong(_idGiuong, "1"))
                                {
                                    TA_MessageBox.MessageBox.Show("Lỗi cập nhật trạng thái giường"
                                 , TA_MessageBox.MessageIcon.Error); _status = false;
                                    return;
                                }
                            }
                            else if (!string.IsNullOrEmpty(slBNCho) && int.Parse(slBNCho) == 0 && !isnguoinam)
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

                            TA_MessageBox.MessageBox.Show("Đặt giường thành công!"
                            , TA_MessageBox.MessageIcon.Information);

                            _status = true;
                            this.Close();
                        }
                    }


                }
            }
            catch
            {
                return;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            usc_KhoaPhong.txtTen.Text = usc_SelectBoxPhongTimKiem.txtTen.Text = usc_Giuong.txtTen.Text = string.Empty;
            txtTenBenhNhan.Text = dtpTuNgay.Text = dtpDenNgay.Text = string.Empty;
            usc_SelectBoxDoiTuong.txtTen.Text = dtpNgaySinh.Text = txtDiaChi.Text = his_ComboboxPhai.Text = string.Empty;
            this.Close();
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void frmNhapThongTinDatGiuongBenhNhan_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtMaBN_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBN.Text))
            {
                if (txtMaBN.Text.Length > 8)
                {
                    SendKeys.Send("{BACKSPACE}");

                    // txtMaBN.Text = txtMaBN.Text.Substring(0, 8);
                }


            }
        }

        private void txtMaBN_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                #region Cấp mã bn
                //string yy = _acc.Get_MMYY().Substring(2);
                //int stt = _tT.CapMaBN(int.Parse(yy), 1,
                //    int.Parse(cls_System.sys_UserID != "" ? cls_System.sys_UserID : "0"), true);
                //string _maBN = yy + stt.ToString().PadLeft(6, '0');
                //txtMaBN.Text = _maBN;
                #endregion
            }
            else
            {
                if (txtMaBN.Text.Trim().Length != 8) txtMaBN.Text = txtMaBN.Text.Substring(0, 2) + txtMaBN.Text.Substring(2).PadLeft(6, '0');




                if (_tbBTDBN == null || _tbBTDBN.Rows.Count == 0)
                {
                    DataTable tbBTDBN = _acc.Get_Data(_tT.QueryBTDBN(txtMaBN.Text));
                    dr = tbBTDBN.Rows[0];
                }
                else
                {
                    dr = getRowByID(_tbBTDBN, " MABN= '" + txtMaBN.Text + "' ");
                }
                if (dr != null)
                {
                    txtTenBenhNhan.Text = dr["HOTEN"].ToString();
                    dtpNgaySinh.Value = DateTime.Parse(dr["NGAYSINH"].ToString());
                    his_ComboboxPhai.SelectedIndex = dr["PHAI"].ToString() == "0" ? 0 : 1;
                    txtDiaChi.Text = dr["THON"].ToString();
                    string madoituong = _tT.getmadoituong(txtMaBN.Text);
                    usc_SelectBoxDoiTuong.SetTenByMa(madoituong);
                    dtpDenNgay.Text = "";
                }
                else
                {
                    MessageBox.Show("Mã bệnh nhân không tồn tại !");
                    return;
                }

            }
        }
       

        private void txtMaBN_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void usc_KhoaPhong_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }


        private void usc_SelectBoxDoiTuong_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }


        #endregion

    }
}
