using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using E00_System;
using E00_Common;
using E00_Model;
using E00_ControlNew;

using System.Threading;
using System.Threading.Tasks;

namespace E11_PhongGiuong
{
    public partial class frm_ChuyenGiuong : E00_Base.frm_Base
    {

        #region Khai báo biến toàn cục

        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private string _maBN;
        DialogResult sr = DialogResult.No;
        private string _idGiuong;
        private DataTable _tbBenhNhan;
        public bool _status = false;
        private DataTable _dschoduyet;
        private DataTable _slnam;
        private DataTable _tbTongThe;
        private DataTable _tbGiuong;
        private Task taskrun;
        private Thread thr;
        private string _idgiuongNhan = "";
        private string _trangthai;
        private string _curKhoa = "";
        private string _Id = "";
        private bool _isusserclick = true;
        private CancellationTokenSource tokenSource2 = new CancellationTokenSource();

        cls_ThucThiDuLieu _tT;
        #endregion

        #region Khởi tạo
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

        public string IdgiuongNhan
        {
            get
            {
                return _idgiuongNhan;
            }

            set
            {
                _idgiuongNhan = value;
            }
        }

        #region Khởi tạo không tham số
        public frm_ChuyenGiuong()
        {
            InitializeComponent();

        }


        #endregion

        #region Khởi tạo có tham số

        public frm_ChuyenGiuong(string MaBN, string IdGiuong, string trangthai,string ID)
        {
            InitializeComponent();
            _maBN = MaBN;
            _idGiuong = IdGiuong;
            _trangthai = trangthai;
            _Id = ID;
        }
        #endregion

        #endregion

        #region Phương thức

        #region Phương thức private
        private void loaddatangam()
        {
            _dschoduyet = TT.GetDanhSachChoDuyet();
            _slnam = TT.GetSoLuongNguoiNamTaiGiuong();
            _tbTongThe = _acc.Get_Data(TT.QueryGetGiuongTuTableDatGiuong());
            _tbGiuong = TT.GetTBGiuong("", "", "");
        }
        #region Gán source cho control khoa phòng
        public void LoadKhoaPhong()
        {
            DataTable tbKhoa = TT.GetDanhMucKhoaPhongCoGiuong();

            slbKhoa.DataSource = tbKhoa;
        }
        #endregion

        #endregion

        #region Load giường theo từng khoa phòng

        #endregion

        #region Phương thức public

        #endregion

        #endregion

        #region Sự kiện


        #endregion

        private void frm_ChuyenGiuong_Load(object sender, EventArgs e)
        {
            Thread thr = new Thread(loaddatangam);
            thr.Start();

            LoadKhoaPhong();
            _tbBenhNhan = _acc.Get_Data(TT.QueryGetGiuongTuTableDatGiuong(idgiuong: _idGiuong, isincludeDatGiuong: true));
            DataRow dr = null;
            try
            {
                dr = _tbBenhNhan.Select("MABN ='" + _maBN + "'")[0];
            }
            catch (Exception)
            {


            }
            if (dr != null)
            {
                lblMaBN.Text = dr["MABN"] + "";
                DateTime dt = new DateTime();
                string ngaySinh = "";
                DateTime nS = new DateTime();
                string ngayVao = "";
                DateTime nV = new DateTime();
                string dong2;
                string dong3;
                TimeSpan tongNgay = new TimeSpan();
                try
                {
                    dt = DateTime.Parse(dr["TU"].ToString() == DBNull.Value.ToString() ? TT.GetSysDate().ToString() : dr["TU"].ToString());
                }
                catch
                {
                }
                try
                {
                    dong2 = dt.ToString(cls_System.sys_DinhDangNgay_HienThi);
                }
                catch
                {
                }
                try
                {
                    dong3 = string.Format("{0:T}", dt);
                }
                catch
                {
                }

                try
                {
                    lblHoTen.Text = dr["HOTEN"].ToString();
                }
                catch
                {
                }
                try
                {
                    ngaySinh = dr["NGAYSINH"].ToString() == DBNull.Value.ToString() ? TT.GetSysDate().ToString() : dr["NGAYSINH"].ToString();
                }
                catch
                {
                }
                try
                {
                    nS = DateTime.Parse(ngaySinh, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                }
                try
                {
                    lblNgaySinh.Text = nS.ToString("dd/MM/yyyy");
                }
                catch
                {
                }
                try
                {
                    ngayVao = dr["TU"].ToString().Substring(0, 10);//gán ngày vào
                }
                catch
                {
                }
                try
                {
                    nV = DateTime.Parse(ngayVao);
                }
                catch
                {
                }
                try
                {
                    lblNgayVao.Text = nV.ToString("dd/MM/yyyy");
                }
                catch
                {
                }
                try
                {
                    tongNgay = DateTime.Parse(dr["TU"].ToString() == DBNull.Value.ToString() ? TT.GetSysDate().ToString() : dr["TU"].ToString()).Subtract(TT.GetSysDate());
                }
                catch
                {
                }
                try
                {
                    lblNgaySD.Text = (int.Parse(tongNgay.Days.ToString().Replace("-", "")) + 1).ToString();//gán tổng so với hiện tại
                }
                catch
                {
                }
                try
                {
                    lblGioVao.Text = DateTime.Parse(dr["TU"].ToString() == DBNull.Value.ToString() ? TT.GetSysDate().ToString() : dr["TU"].ToString()).ToShortTimeString();

                }
                catch
                {
                }
                try
                {
                    lblDoiTuong.Text = dr["DOITUONG"].ToString();
                }
                catch (Exception)
                {
                    lblDoiTuong.Text = "";
                }
                // giuong.lblBenh.Text = "Chưa load";//gán bệnh chính 
                lblGioi.Text = dr["PHAI"].ToString() == "0" ? "NAM" : "Nữ";
                _curKhoa = dr["MAKP"] + "";
                slbKhoa.SetTenByMa(_curKhoa);
                //Ẩn hết thông tin mặc định 
                slbPhong.SetTenByMa(dr[cls_PG_DanhMucGiuong.col_MAPHONG] + "");
                slbLoaiGiuong.SetTenByMa(dr[cls_PG_DanhMucGiuong.col_LOAIGIUONG] + "");
            //    slbGiuong.SetTenByMa(_idGiuong);
                lblGiuonghientai.Text = slbGiuong.txtTen.Text;
                lblKhoa.Text = slbKhoa.txtTen.Text;
                lblLoaiGiuong.Text = slbLoaiGiuong.txtTen.Text;
                lblPhong.Text = slbPhong.txtTen.Text;
            }


        }

        private void slbKhoa_HisSelectChange(object sender, EventArgs e)
        {
            slbPhong.clear();
            DataTable tbPhong = _acc.Get_Data(_tT.GetDataPhong(slbKhoa.txtMa.Text));
            if (tbPhong != null && tbPhong.Rows.Count > 0)
            {
                DataRow drPhong = tbPhong.NewRow();
                drPhong[cls_DanhMucPhong.col_MAPHONG] = "-1";
                drPhong[cls_DanhMucPhong.col_TEN] = "Tất cả";
                tbPhong.Rows.Add(drPhong);
                _isusserclick = false;
                slbPhong.DataSource = tbPhong;
                _isusserclick = true;
                slbPhong.SetTenByMa("-1");
                // slbPhong_HisSelectChange(null, null);

            }
            else
            {
                slbPhong.DataSource = null;
             

            }
            loadkhoa();

        }

        private void slbKhoa_Load(object sender, EventArgs e)
        {

        }
        private void slbGiuong_HisSelectChange(object sender, EventArgs e)
        {
            loadkhoa();
        }
        private void slbPhong_HisSelectChange(object sender, EventArgs e)
        {

            DataTable tbLoaiGiuong = null;

            tbLoaiGiuong = _acc.Get_Data(_tT.GetDataLoaiGiuong(slbKhoa.txtMa.Text, slbPhong.txtMa.Text));


            if (tbLoaiGiuong != null && tbLoaiGiuong.Rows.Count > 0)
            {
                DataRow drLoaiGiuong = tbLoaiGiuong.NewRow();
                drLoaiGiuong["MALOAI"] = "-1";
                drLoaiGiuong["TENLOAI"] = "Tất cả";
                tbLoaiGiuong.Rows.Add(drLoaiGiuong);
                slbLoaiGiuong.DataSource = tbLoaiGiuong;
                slbLoaiGiuong.SetTenByMa("-1");
                // slbLoaiGiuong_HisSelectChange(null, null);
               
            }
            else
            {
                slbLoaiGiuong.DataSource = null;
                slbLoaiGiuong.clear();

            }
            DataTable dtphong = _acc.Get_Data(_tT.GetDataGiuong(slbKhoa.txtMa.Text, slbPhong.txtMa.Text));
            if (dtphong != null && dtphong.Rows.Count > 0)
            {
                DataRow drPhong = dtphong.NewRow();
                drPhong[cls_PG_DanhMucGiuong.col_ID] = "-1";
                drPhong[cls_PG_DanhMucGiuong.col_TEN] = "Tất cả";
                dtphong.Rows.Add(drPhong);
                slbGiuong.DataSource = dtphong;
                slbGiuong.SetTenByMa("-1");
            }
            else
            {
                slbLoaiGiuong.DataSource = null;
                slbLoaiGiuong.clear();

            }
         
            loadkhoa();
        }

        private void slbLoaiGiuong_HisSelectChange(object sender, EventArgs e)
        {

            loadkhoa();


        }
        private void loadkhoa()
        {
            if (!_isusserclick)
            {
                return;
            }
            string maKhoa = slbKhoa.txtMa.Text;
            string maphong = slbPhong.txtMa.Text;
            string maloaigiuong = slbLoaiGiuong.txtMa.Text;
            string magiuong = slbGiuong.txtMa.Text;
 
                if (_tbGiuong == null || (_tbGiuong != null && _tbGiuong.Rows.Count == 0))
            {
                loaddatangam();
            }
            string where = "";

            if (!string.IsNullOrEmpty(maKhoa) && maKhoa != "-1")
            {
                where += " AND " + cls_DanhMucPhong.col_MAKP + " = '" + maKhoa + "' ";
            }
            else
            {
                maKhoa = "";
            }
            if (!string.IsNullOrEmpty(maphong) && maphong != "-1")
            {
                where += " AND " + cls_DanhMucPhong.col_MAPHONG + " = '" + maphong + "' ";
            }
            else
            {
                maphong = "";
            }
            if (!string.IsNullOrEmpty(maloaigiuong) && maloaigiuong != "-1")
            {
                where += " AND " + cls_PG_DanhMucGiuong.col_LOAIGIUONG + " = '" + maloaigiuong + "' ";
            }
            if (!string.IsNullOrEmpty(magiuong) && magiuong != "-1")
            {
                where += " AND " + cls_PG_DanhMucGiuong.col_ID + " = '" + magiuong + "' ";
            }
            else
            {
                maloaigiuong = "";
            }
            if (!string.IsNullOrEmpty(where))
            {
                where = where.Remove(0, 4);
            }
            try
            {
                DataTable dttmp = _tbGiuong.Select(where).CopyToDataTable();
           
            drcMAin.pnlTop.Height = 20;
            drcMAin.pnlMain.BorderStyle = BorderStyle.FixedSingle;
            drcMAin.LblTieuDe.Text = "" + slbKhoa.txtTen.Text;//"" + dtGomNhom.Rows[i][1].ToString();
            drcMAin.HisAutoSize = true;
            drcMAin.Dock = DockStyle.Top;
            drcMAin.SpaceH = 10;
            drcMAin.SpaceW = 10;
            drcMAin._usingcmt = false;
            drcMAin.HisMouseDown += DrcMAin_HisMouseDown;
            //drcMAin.Panel_Main = this.pnlMain2;
            drcMAin.Name = "" + maKhoa;
            #region
            drcMAin._kp = maKhoa;
            drcMAin._MaPhong = maphong;

            drcMAin.Tag = maKhoa;

            #endregion

            #region Check tuy chon giuong : tat ca, da dat, co nguoi, trong, chưa sử dụng,hư
            drcMAin.dorong = this.Width;
            CancellationToken ct = tokenSource2.Token;
            drcMAin.LoadDaTa("3", dttmp, _dschoduyet, _slnam, _tbTongThe);
            }
            catch (Exception)
            {

            }
          

            if (thr != null && thr.IsAlive)
            {
                thr.Abort();
            }
            thr = new Thread(reload);
            thr.Start();
            //  drcMAin.LblTieuDe.DoubleClick += new System.EventHandler(lblTieuDe_DoubleClick);
            // reload();
        }
        private void reload()
        {
            drcMAin.Istinh = false;
            drcMAin.load();
            #endregion
        }
        private void DrcMAin_HisMouseDown(object sender, MouseEventArgs e)
        {
            Usc_GiuongEdit giuong = (Usc_GiuongEdit)sender;
           // slbGiuong.SetTenByMa(giuong.ID + "");
            btnChuyenGiuong_Click(giuong.ID + "", giuong.TenGiuong);
        }

        private void btnChuyenGiuong_Click(string idgiuong, string tengiuong)
        {
           
            if (sr != DialogResult.No)
            {
                return;
            }

            sr = TA_MessageBox.MessageBox.Show("Bạn có chắc muốn chuyển sang giường " + tengiuong + " ? ", TA_MessageBox.MessageIcon.Question);
            if (DialogResult.Yes != sr)
            {
                return;
            }
            if (KienTra(idgiuong))
            {
                if (_trangthai == "1")
                {//Bệnh nhân chỉ mới đặt giường
                    if (TT.UpdateTheoDoiGiuong( _Id,idgiuong: idgiuong, makp: slbKhoa.txtMa.Text))
                    {
                       
                        _status = true;
                        _idgiuongNhan = idgiuong;
                        TA_MessageBox.MessageBox.Show("Chuyển bệnh nhân thành công!", TA_MessageBox.MessageIcon.Information);
                        this.Close();
                    }
                }
                else if (_trangthai == "2")
                {//Bệnh nhân đang nằm
                    DataTable dtgiuong = TT.RowThongTinTheoDoiGiuongbyid(_Id);
                    DataRow drtheodoigiuong = dtgiuong.NewRow();
                    try
                    {

                        drtheodoigiuong = dtgiuong.Rows[0];
                    }
                    catch (Exception)
                    {


                    }
                    string madoituong = TT.GETMADOITUONG(_maBN, drtheodoigiuong[cls_HienDien.col_MaQL] + "", drtheodoigiuong[cls_HienDien.col_MaVaoVien] + "");
                    if (_curKhoa == slbKhoa.txtMa.Text)
                    {

                        if (!TT.InsertThongTinDatGiuongVaoTheoDoiGiuong(TT.GetIdTheoDoiGiuong(), slbKhoa.txtMa.Text, _maBN, idgiuong,
                                                                                                    TT.GetSysDate(), DateTime.MaxValue,
                                                                                                    1,
                                                                                                    "0", madoituong, drtheodoigiuong[cls_PG_TheoDoiGiuong.col_GHICHU].ToString(), "1", _Id, drtheodoigiuong[cls_HienDien.col_MaVaoVien] + "", drtheodoigiuong[cls_HienDien.col_MaQL] + ""))
                        {
                            TA_MessageBox.MessageBox.Show("Lỗi insert theo doi giường", TA_MessageBox.MessageIcon.Error);

                        }
                    }
                    else
                    {
                        if (!TT.InsertThongTinDatGiuongVaoTheoDoiGiuong(TT.GetIdTheoDoiGiuong(), slbKhoa.txtMa.Text, _maBN, idgiuong,
                                                                                                   TT.GetSysDate(), DateTime.MaxValue,
                                                                                                   1,
                                                                                                   "0", madoituong, drtheodoigiuong[cls_PG_TheoDoiGiuong.col_GHICHU].ToString(), "0", _Id, drtheodoigiuong[cls_HienDien.col_MaVaoVien] + "", drtheodoigiuong[cls_HienDien.col_MaQL] + ""))
                        {
                            TA_MessageBox.MessageBox.Show("Lỗi insert theo doi giường", TA_MessageBox.MessageIcon.Error);

                        }
                    }

                        if (!TT.UpdateTheoDoiGiuong(_Id, Den: TT.GetSysDate().ToString("dd-MM-yyyy HH:mm:ss"), issudung: false))
                        {
                            TA_MessageBox.MessageBox.Show("Lỗi cập nhật theo doi giường", TA_MessageBox.MessageIcon.Error);
                            return;
                        }

                       

                        _status = true;
                        _idgiuongNhan = idgiuong;
                        TA_MessageBox.MessageBox.Show("Chuyển bệnh nhân thành công!", TA_MessageBox.MessageIcon.Information);
                        this.Close();
                    

                }
            }
        }

        private bool KienTra(string idgiuong)
        {
            string tinhtrang = (drcMAin.GetGiuong(int.Parse(idgiuong))).Tinhtrang;
            if (tinhtrang != "1" && tinhtrang != "0" && tinhtrang != "2")
            {
                TA_MessageBox.MessageBox.Show("Không thể chuyển bệnh nhân đến giường này !", TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }

    }
}
