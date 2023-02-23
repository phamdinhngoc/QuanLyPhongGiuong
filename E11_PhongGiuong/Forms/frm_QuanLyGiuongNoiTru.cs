using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using E00_Common;
using E00_ControlNew;
using System.Linq;
using E00_Model;

using System.Globalization;
using System.Threading;
using E00_System;
using E00_Control;
using DevComponents.DotNetBar;
using System.IO;

namespace E11_PhongGiuong
{
    public partial class frm_QuanLyGiuongNoiTru : E00_Base.frm_Base
    {
        #region Khai báo biến toàn cục

        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
		private bool _isloadtabgiuongtrong = false;
        private string _khoaload = "";
        private bool _isloadedtabgiuongtrong = false;
        private bool _isloadtabtinhtranggiuong = false;
        private bool _isloadedtabtinhtranggiuong = false;
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
		private List<usc_DragControlEdit> _lstDragControlEdit = new List<usc_DragControlEdit>();
        private DataTable _tbThemTongSo;
        private DataTable _tbKhoaCoGiuong;
        private string _userError, _systemError, _id = string.Empty;
        private List<string> _lstCheck = new List<string>();
        private List<string> _lstCheckHoTen = new List<string>();
        private DataTable _tbDanhSachDuong = new DataTable();
		private DataTable _Datagoc = new DataTable();
		private string  _maKhoa = string.Empty;
        private string _idGiuongChonChuyen, _idPhong = string.Empty;
        private int _option;
       // private string _colorTrong, _colorDat, _colorCoNguoi, _colorChuaSuDung,_colorHu;
        private int _slTrong, _slDat, _slCoNguoi, _slChuaSuDung,_slHu;
        public string  _indexOptionLoad = string.Empty;
        public string  _tenOptionLoad = string.Empty;
        private DataTable _tbBTDBN = new DataTable();
        List<Usc_GiuongEdit> _lisGiuong = new List<Usc_GiuongEdit>();
		private List<string> lstmakp = new List<string>();
		private bool _isload = false;
        private Usc_GiuongEdit currentGiuong;
        private DataTable _tbPhong;
        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private string _sql = string.Empty;
        private bool _luuLocationGiuong = false;
        private string _idColor = string.Empty;
        private bool _statusEnd = false;
        private DataTable _tbTongThe = new DataTable();
        private bool _optionChonLoai = false;
        private string _tenLoaiGiuongLink, _maKhoaPhongLink = string.Empty;
        private DataTable _dttmp;
        private usc_DragControlEdit dragControl;
        private TreeNode _nodeCha,_nodeTB, _nodeCon;
        private string _thongKe = "";
		private ThreadStart tstabttg;
        private DataTable _dschoduyet;
        private DataTable _slnam;
        DataTable _tbTongTheG;
        Thread thrtabttg;
		private ThreadStart tstabgiuongtrong;
		Thread thrtabgiuongtrong;
		//private int _slTrongPrivate, _slDatPrivate, _slCoNguoiPrivate, _slChuaSuDungPrivate,_slHuPrivate;
		private string _maKP, _maLoaiPhong, _maPhong = string.Empty;
        private bool _optionChonGiuong = false;
        private string _maKhoaGiuong, _maPhongGiuong = string.Empty;
        private bool _loadLanDauTien = false;
        private frmProgress objfrmShowProgress;
        List<string> _lstIdGiuong = new List<string>();

        //van thêm bién
        private string _maKhoaPhongNoiTru = "";
        public string _idgiuongNoiTru { get; set; }

        #endregion

        #region Khởi tạo

        public frm_QuanLyGiuongNoiTru()
        {
            InitializeComponent();
            _api.KetNoi();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public frm_QuanLyGiuongNoiTru(string makp,DataTable dtBTDBN)
        {
            InitializeComponent();
            _api.KetNoi();
            Control.CheckForIllegalCrossThreadCalls = false;
            _maKhoaPhongNoiTru = makp;
            _tbBTDBN = dtBTDBN;
        }

        #endregion

        #region Phuong thuc

        #region Phương thức protected

        private void LoadData()
        {
            try
            {
                LoadPanelGiuong();
                KiemTraCapNhatTRangThaiChoXuatVien();
               // GetDataTableTheoDoiGiuong("");
                //DataTable tbKhoa = (DataTable)usc_SelectBoxKhoa.DataSource;
              //  LoadKhoaPhong();//load all khoa phòng
            }
            catch
            {
                return;
            }
        }

        #endregion

        #region Phương thức private

        #region Load tất cả loại giường lên panel
        private void LoadPanleLoaiPhong()
        {
           
                #region Load tất cả loại giường lên panel
                his_LabelX lb;
                string[] mS;
                DataTable tbTatCaLoaiPhong = _tT.GetDanhMucLoaiPhong();
                for (int i = 0; i < tbTatCaLoaiPhong.Rows.Count; i++)
                {
                try
                {
                    mS = tbTatCaLoaiPhong.Rows[i]["MAUSAC"].ToString().Split(',');
                    lb = new his_LabelX();
                    lb.AutoSize = true;
                    lb.Text = tbTatCaLoaiPhong.Rows[i]["TEN"].ToString();
                    lb.ForeColor = Color.White;
                    lb.BackColor = Color.FromArgb(int.Parse(mS[0]), int.Parse(mS[1]), int.Parse(mS[2]), int.Parse(mS[3]));

                    pnlLoai.Controls.Add(lb);
                }
                catch
                {
                }
            }
                #endregion
          
        } 
        #endregion

        #region Lay toan bo giuong theo phong ban va loai giuong
        private string GetAllBedOfDepartment(string maKp, string maLoaiGiuong)
        {
            _sql = string.Format(" select btd.{0},btd.{1},count(g.{2}) as SoLuong "
                                + " from {3}.{4} lp right join {3}.{5} p on lp.{6}=p.{7}"
                                + " right join {3}.{8} g on g.{9}=p.{10}   and g.tinhtrang <> '3' and g.tinhtrang <> '4'  "
                                + " left join {3}.{11} lg  on g.{12}=lg.{13}  "
                                + " left join {3}.{14} btd on btd.{15}=p.{16}  "
                                + " where btd.{17}='{18}' and lp.{19}='{20}' "
                                + " group by btd.{0},btd.{1}", cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP, cls_PG_DanhMucGiuong.col_ID, _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang,
                                cls_DanhMucPhong.tb_TenBang, cls_DanhMucLoaiPhong.col_ID, cls_DanhMucPhong.col_LOAI, cls_PG_DanhMucGiuong.tb_TenBang, "MAPHONG", "MAPHONG",//Sửa mã
                                cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_D_DanhMucLoaiGiuong.col_MALOAI, cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_MaKP, maKp,
                                cls_DanhMucLoaiPhong.col_MA, maLoaiGiuong);
            string tongSo = "0"; DataTable tb = _acc.Get_Data(_sql);
            if (tb!=null && tb.Rows.Count > 0)
            {
                tongSo = string.IsNullOrEmpty(tb.Rows[0]["SoLuong"].ToString())?"0" : tb.Rows[0]["SoLuong"].ToString();
            }
            return tongSo;
        }

        private string GetAllsumBedOfDepartment(string maKp, string maLoaiGiuong)
        {
            _sql = string.Format(" select  sum(count(g.{2})) as SoLuong "
                                + " from {3}.{4} lp right join {3}.{5} p on lp.{6}=p.{7}"
                                + " right join {3}.{8} g on g.{9}=p.{10}   and g.tinhtrang <> '3' and g.tinhtrang <> '4' "
                                + " left join {3}.{11} lg  on g.{12}=lg.{13}  "
                                + " left join {3}.{14} btd on btd.{15}=p.{16}  "
                                + " where  lp.{19}='{20}' "
                                + " group by btd.{0},btd.{1}", cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP, cls_PG_DanhMucGiuong.col_ID, _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang,
                                cls_DanhMucPhong.tb_TenBang, cls_DanhMucLoaiPhong.col_ID, cls_DanhMucPhong.col_LOAI, cls_PG_DanhMucGiuong.tb_TenBang, "MAPHONG", "MAPHONG",//Sửa mã
                                cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_D_DanhMucLoaiGiuong.col_MALOAI, cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_MaKP, maKp,
                                cls_DanhMucLoaiPhong.col_MA, maLoaiGiuong);
            string tongSo = "0"; DataTable tb = _acc.Get_Data(_sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                tongSo = string.IsNullOrEmpty(tb.Rows[0]["SoLuong"].ToString()) ? "0" : tb.Rows[0]["SoLuong"].ToString();
            }
            return tongSo;
        }
        #endregion

        #region Get bộ từ điển bệnh nhân
        private void GetBTDBN()
        {
			

            _tbBTDBN = _acc.Get_Data(_tT.QueryBTDBN());
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
                    _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
                DataRow dr = tb.NewRow();
                dr["ID"] = "0";
                dr["TEN"] = "Tất cả";
                tb.Rows.Add(dr);
                usc_SelectBoxPhongTimKiem.DataSource = tb;
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

        #region Load panel giường
        private void LoadPanelGiuong()
        {
            try
            {
            
                if (!_loadLanDauTien)
                {
                    _tbDanhSachDuong = _tT.GetTBGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text, "");
                  
                   

                        DataTable tbKhoa = _tbDanhSachDuong.DefaultView.ToTable(true, "MAKP", "TENKP").Copy();
                        DataRow dr = tbKhoa.NewRow();
                        dr["MAKP"] = "";
                        dr["TENKP"] = "Tất cả";
                        tbKhoa.Rows.Add(dr);
                        usc_SelectBoxKhoa.DataSource = tbKhoa;
                    

                    _loadLanDauTien = true;
                }

            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion
        private void KiemTraCapNhatTRangThaiChoXuatVien()
        {
            //return;
            
                      DataTable dt = _tT.GetThongTinBenhNhanChoXuatGiuong();

            foreach (DataRow dr in dt.Rows)
            {
                string idgiuong = dr[cls_PG_TheoDoiGiuong.col_IDGIUONG] + "";
                string ID = dr[cls_PG_TheoDoiGiuong.col_ID] + "";
                if (_tT.GetTinhTrangGiuong(idgiuong) == "2")
                {
                   // _tT.UpdateTinhTrangDanhMucGiuong(idgiuong, "5");
                    if (!_tT.UpdateTheoDoiGiuong(ID, trangthaibn: "2"))
                    {
                        TA_MessageBox.MessageBox.Show(string.Format(" Lỗi Không thể cập nhật trạng thái bệnh nhân !",
                          _userError)
                   , TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    if (!_tT.UpdateTinhTrangDanhMucGiuongxUATgIUONG(idgiuong, "5"))
                    {
                        TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                            _userError)
                     , TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                }

            }

        }
        #region Get data theo dõi giường
        private DataTable GetDataTableTheoDoiGiuong(string doiTuong)
        {
            DataTable tb = new DataTable();
            try
            {
                string maKhoa, maPhong = string.Empty;
                maKhoa = usc_SelectBoxKhoa.txtMa.Text;
                maPhong = usc_SelectBoxPhongTimKiem.txtMa.Text;
                if (!string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(maPhong))
                {
                    _sql = _tT.QueryGetGiuongTuTableTheoDoiGiuong(maKhoa, maPhong);
                }
                if (string.IsNullOrEmpty(maKhoa) && string.IsNullOrEmpty(maPhong) || !string.IsNullOrEmpty(maKhoa) && string.IsNullOrEmpty(maPhong)
                    || string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(maPhong))
                {
                    _sql = _tT.QueryGetGiuongTuTableTheoDoiGiuong1(doiTuong);
                }
                tb = _acc.Get_Data(_sql);
            }
            catch
            {
            }
            return tb;
        }
        #endregion

        #region Get thong tin giuong dat
        private DataTable GetThongTinGiuongDat()
        {
            DataTable tb = new DataTable();
            try
            {
                string maKhoa, maPhong = string.Empty;
                maKhoa = usc_SelectBoxKhoa.txtMa.Text;
                maPhong = usc_SelectBoxPhongTimKiem.txtMa.Text;
                if (!string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(maPhong))
                {
                    //_sql = _tT.QueryGetGiuongTuTableTheoDoiGiuong(maKhoa, maPhong);
                    _sql = _tT.QueryGetGiuongTuTableDatGiuong(maKhoa, maPhong);
                }
                if (string.IsNullOrEmpty(maKhoa) && string.IsNullOrEmpty(maPhong) || !string.IsNullOrEmpty(maKhoa) && string.IsNullOrEmpty(maPhong)
                    || string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(maPhong))
                {
                    //_sql = _tT.QueryGetGiuongTuTableTheoDoiGiuong1(maKhoa, maPhong);
                    _sql = _tT.QueryGetGiuongTuTableDatGiuong1();
                }
                tb = _acc.Get_Data(_sql);
            }
            catch
            {
            }
            return tb;
        }
        #endregion

        #region Viết hoa chữ cái đầu
        private string ToTitleCase(string text)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        }
        #endregion

        #region Cập nhật vị trí giường
        private void UdpateLocationGiuong()
        {
       //     for (int m = 0; m < this.pnlMain2.Controls.Count; m++)
       //     {
       //         if (this.pnlMain2.Controls[m] is usc_DragControlEdit)
       //         {
       //             usc_DragControlEdit control = (usc_DragControlEdit)this.pnlMain2.Controls[m];
       //             List<Usc_GiuongEdit> lstgiuong = control.lstGiuong;
       //             for (int i = 0; i < lstgiuong.Count; i++)
       //             {
       //                 ClearList();
       //                 _lst2.Add(cls_PG_DanhMucGiuong.col_VITRI, lstgiuong[i].Location.X + ", " + lstgiuong[i].Location.Y);
       //                 _lst3.Add(cls_PG_DanhMucGiuong.col_ID, lstgiuong[i].ID.ToString());
       //                 if (!_api.Update(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang, _lst2, new List<string>(), _lst3))
       //                 {
       //                     TA_MessageBox.MessageBox.Show("Không update được!", TA_MessageBox.MessageIcon.Error);
							

							//return;
       //                 }
       //             }
       //         }
       //     }
        }
        #endregion

        #region Load dữ liệu
        private void LoadDuLieuAgain()
        {
            try
            {
               
               
                if (!string.IsNullOrEmpty(usc_SelectBoxPhongTimKiem.txtMa.Text))
                {
                    usc_SelectBoxPhongTimKiem_HisSelectChange(null, null);
                }
                LoadPanelGiuong();
                //lblTatCa_Click(null, null);
            }
            catch
            {
                return;
            }

        }
        #endregion

        #region Kiểm tra đã chọn khoa phòng hay chưa
        private bool DaChonKhoaPhong()
        {
            if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text) && string.IsNullOrEmpty(_idGiuongChonChuyen))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(usc_SelectBoxPhongTimKiem.txtMa.Text) && string.IsNullOrEmpty(_idGiuongChonChuyen))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Get mã màu từ mã loại giường
        private string GetMaMauTuMaLoaiPhong(string maLoaiPhong)
        {
            _sql = string.Format("select {0} from {1}.{2} where {3} = '{4}'",cls_DanhMucLoaiPhong.col_MAUSAC, _acc.Get_User(),cls_DanhMucLoaiPhong.tb_TenBang,cls_DanhMucLoaiPhong.col_MA, maLoaiPhong);
            DataTable tb = _acc.Get_Data(_sql);
            if (tb.Rows.Count > 0)
            {
                return tb.Rows[0]["MAUSAC"].ToString();
            }
            return "";
        }

		
		#endregion

		#region Get tên loại dựa theo mã loại
		private string GetTenLoaiDuaVaoMaLoai(string maLoaiGiuong)
        {
            _sql = string.Format("select {0} from {1}.{2} where {3} = '{4}'",cls_D_DanhMucLoaiGiuong.col_TENLOAI, _acc.Get_User(),cls_D_DanhMucLoaiGiuong.tb_TenBang,cls_D_DanhMucLoaiGiuong.col_MALOAI, maLoaiGiuong);
            DataTable tb = _acc.Get_Data(_sql);
            if (tb.Rows.Count > 0)
            {
                return tb.Rows[0]["TENLOAI"].ToString();
            }
            return "";
        }
        #endregion

        #region Load danh mục giường theo điều kiện
        private void ReloadDataGIuong()
        {
            _dschoduyet = _tT.GetDanhSachChoDuyet();
            _slnam = _tT.GetSoLuongNguoiNamTaiGiuong();
            _tbTongTheG = _acc.Get_Data(_tT.QueryGetGiuongTuTableDatGiuong());
            _tbDanhSachDuong = _tT.GetTBGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text, "");
        }
        private void LoadGiuongGomNhomTheoKhoa(string _status= "3", string dkMaKhoa="", string dkMaPhong ="",string tenloaigiuong ="",bool Isfillter = false)
        {
            if (!Isfillter)
            {
                pnlMain2.Visible = false;
            List<Usc_GiuongEdit> lstgiuong = new List<Usc_GiuongEdit>();
            List<Usc_GiuongEdit> lstGiuongKoCoViTri = new List<Usc_GiuongEdit>();
            DataTable tbGiuong = new DataTable();
            _slTrong = _slDat = _slCoNguoi = _slChuaSuDung = 0;
            DataTable tbDanhSachGiuongTheoTungKhoa = new DataTable();//Tất cả các khoa và tất cả các trạng thái
            DataTable tbDanhSachGiuongTrong = new DataTable();//Tất cả danh sách giường trống theo từng khoa
            DataTable tbDanhSachGiuongCoNguoi = new DataTable();//Tất cả danh sách giường có người theo từng khoa
            DataTable tbDanhSachGiuongDaDat = new DataTable();//Tất cả danh sách giường có người theo từng khoa
            DataTable tbDanhSachGiuongChuaSuDung = new DataTable();//Tất cả danh sách giường chưa sử dụng theo từng khoa
			 _dschoduyet = _tT.GetDanhSachChoDuyet();
			 _slnam = _tT.GetSoLuongNguoiNamTaiGiuong();
            usc_DragControlEdit usd1;
             _tbTongTheG = _acc.Get_Data(_tT.QueryGetGiuongTuTableDatGiuong());

                if (_tbDanhSachDuong==null || _tbDanhSachDuong.Rows.Count==0)
                {
                    LoadPanelGiuong();
                }


                _lisGiuong.Clear();
                //foreach (Control c in pnlMain2.Controls)
                //    c.Dispose();
                pnlMain2.Controls.Clear();
                try
                {
                    if (string.IsNullOrEmpty(dkMaKhoa)
                        || dkMaKhoa == "0")
                    {
                        _sql = _tT.QueryGomNhomKhoaPhong();
                    }
                    else
                    {
                        _sql = _tT.QueryGomNhomKhoaPhong1(dkMaKhoa);
                    }

                    #region Lấy danh sách khoa phòng theo thứ tự Giường trống nhiều lên trước
                    DataTable dtGomNhom = _acc.Get_Data(_sql);

               

                    #endregion

                    if (dtGomNhom != null)
                    {
                        for (int i = 0; i < dtGomNhom.Rows.Count; i++)
                        {
                            if (_tbDanhSachDuong != null)
                            {
                                if (_tbDanhSachDuong.Rows.Count > 0)
                                {
                                    DataView dv = _tbDanhSachDuong.DefaultView;
                                    dv.Sort = "STT asc";
                                    _tbDanhSachDuong = dv.ToTable();

                                    usd1 = new usc_DragControlEdit();
                                    usd1.pnlTop.Height = 20;
                                    usd1._isgiuongNoiTru = true;
                                    usd1.waiting += Usd1_waiting;
                                    usd1.endwaiting += Usd1_endwaiting;
                                    usd1.pnlMain.BorderStyle = BorderStyle.FixedSingle;
                                    usd1.Reload += usc_lableTong1_reload;
                                    usd1.ReloadOtherGiuong += Usd1_ReloadGiuong1;
                                        usd1.LblTieuDe.Text = "" + dtGomNhom.Rows[i][1].ToString();

                                        _isload = false;
                                    usd1.TbBTDBN = _tbBTDBN;
                                    usd1.getbn += Usd1_getbn;
                                    usd1.HisAutoSize = true;
                                    usd1.tinhlai += Usd1_tinhlai;
                                    usd1.Dock = DockStyle.Top;
                                    usd1.pnlMain.MouseHover += new System.EventHandler(pnlMain_MouseHover);
                                    usd1.SpaceH = 10;
                                    usd1.SpaceW = 10;
                                    usd1.ReloadGiuong += Usd1_ReloadGiuong;
                                    usd1.Panel_Main = this.pnlMain2;
                                    usd1.Name = "" + dtGomNhom.Rows[i][0].ToString();
                                    this.pnlMain2.Controls.Add(usd1);
                                    string textDSBNDat = string.Empty;
                                    //Usc_GiuongEdit giuong = new Usc_GiuongEdit();



                                    #region
                                    usd1._kp = dtGomNhom.Rows[i].ItemArray[0].ToString();
                                    usd1._MaPhong = dkMaPhong;
                                   
                                    usd1.Tag = dtGomNhom.Rows[i].ItemArray[0].ToString();
                                    if (_loadLanDauTien)
                                    {
                                        usd1.IsShow = false;
                                    }
                                    #endregion

                                    #region Check tuy chon giuong : tat ca, da dat, co nguoi, trong, chưa sử dụng,hư


                                    usd1.LoadDaTa(_status, _tbDanhSachDuong, _dschoduyet, _slnam, _tbTongTheG, usc_lableTong1);

                                    #endregion
                                    usd1.LblTieuDe.DoubleClick += new System.EventHandler(lblTieuDe_DoubleClick);
                                    _lstDragControlEdit.Add(usd1);
                                }

                            }
                            else
                            {
                                usd1 = new usc_DragControlEdit();
                                usd1._isgiuongNoiTru = true;
                                usd1.pnlTop.Height = 20;
                                usd1.pnlMain.BorderStyle = BorderStyle.FixedSingle;
                                usd1.LblTieuDe.Text = "" + dtGomNhom.Rows[i][1].ToString();
                                usd1.Reload += usc_lableTong1_reload; usd1.ReloadOtherGiuong += Usd1_ReloadGiuong1;
                                usd1.HisAutoSize = true;
                                usd1.Dock = DockStyle.Top;
                                usd1.pnlMain.MouseHover += new System.EventHandler(pnlMain_MouseHover);
                                usd1.SpaceH = 10;
                                usd1.SpaceW = 10;
                                usd1.Panel_Main = this.pnlMain2;
                                usd1.Name = "" + dtGomNhom.Rows[i][0].ToString();
                                this.pnlMain2.Controls.Add(usd1);
                                usd1.Tag = dtGomNhom.Rows[i].ItemArray[0].ToString();
                                // usd1.LblTieuDe.DoubleClick += new System.EventHandler(lblTieuDe_DoubleClick);
                            }

                            lstmakp.Add(dtGomNhom.Rows[i].ItemArray[0].ToString());

                        }

                        //Load lại view tổng thể
                        dgvThongKe.DataSource = _tT.SourceViewTongThe(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text,dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
                        dgvThongKe.Refresh();

                    }
                    string userError = "", systemError = "";
                    Dictionary<string, string> _lst2 = new Dictionary<string, string>();
                    _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "LoadTheoKhoaPhong");
                    _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
                    DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);
                    string khoa = _tT.GetKPUser(E00_System.cls_System.sys_UserID);
                    if ((dtt != null && dtt.Rows.Count > 0) && !string.IsNullOrEmpty(khoa) && E00_System.cls_System.sys_UserID != "1")

                    {

                        _khoaload = khoa;
                    }
                    Thread thr = new Thread(tinhcontrol);
                    thr.Start();
                  
                    foreach (usc_DragControlEdit item in _lstDragControlEdit)
                    {

                        if (_khoaload == item._kp)
                        {
                            item.btnDown_Click(null, null);
                        }
                        else
                        {
                            item.btnUp_Click(null, null);
                        }
                    }

                }
                catch( Exception ex)
                {

                }
                pnlMain2.Visible = true;
            }
            else
            {
                foreach (usc_DragControlEdit item in _lstDragControlEdit)
                {
                    if (item.IsShow)
                    {
                        item.btnUp_Click(null, null);
                    }
                    if (item._kp == dkMaKhoa)
                    {
                        String[] tmp = item.LblTieuDe.Text.Split('-');

                        if (tmp.Length > 1)
                        {
                            item.LblTieuDe.Text = tmp[0].Trim();
                        }
                        if (_tenLoaiGiuongLink != "TongSo")
                        {
                            item.LblTieuDe.Text = item.LblTieuDe.Text + " - " + _tenLoaiGiuongLink;
                            item.filltergiuong(_status, dkMaPhong, tenloaigiuong);

                        }
                        else
                        {
                            item.filltergiuong(_status, dkMaPhong, "");
                        }
                        item.TenLoaiGiuong = _tenLoaiGiuongLink;
                        

                        item.showpn();
                        pnlMain2.ScrollControlIntoView(item);
                    }
                }


            }
        }

        private void Usd1_ReloadGiuong1(object sender, EventArgs e)
        {

            try
            {
                usc_DragControlEdit main = sender as usc_DragControlEdit;
                foreach (usc_DragControlEdit item in _lstDragControlEdit)
                {
                    if ( item != main)
                    {
                        item.reloadgiuong();
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void Usd1_ReloadGiuong(object sender, EventArgs e)
        {
            string _idguong = sender + "";
            _dschoduyet = _tT.GetDanhSachChoDuyet();
            _slnam = _tT.GetSoLuongNguoiNamTaiGiuong();
            _tbTongTheG = _acc.Get_Data(_tT.QueryGetGiuongTuTableDatGiuong());
            _tbDanhSachDuong = _tT.GetTBGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text, "");
            foreach (usc_DragControlEdit item in _lstDragControlEdit)
            {
                 item.ReloadDataGIuong(_tbDanhSachDuong, _dschoduyet, _slnam, _tbTongTheG, usc_lableTong1, _idguong);
             
            }
            usc_lableTong1.DataTinh = _tbDanhSachDuong;
        }

        private void Usd1_tinhlai(object sender, EventArgs e)
        {
            string tmp = (string)sender;
            string[] tmpl = tmp.Split(':');
            if (tmpl.Length>2)
            {
                string tu = tmpl[0];
                string den = tmpl[1];
                int sl = int.Parse(tmpl[2]);
                usc_lableTong1.ChangeTinhTrang(tu,den,sl);
                _isloadedtabgiuongtrong = false;
                dgvGiuongTrong.DataSource = null;
                thrtabttg = new Thread(LoadDataGridViewGiuongTrong);
                thrtabttg.Start();
                _isloadedtabtinhtranggiuong = false;
                    thrtabgiuongtrong = new Thread(LoadTreeView);
                    thrtabgiuongtrong.Start();
                


                

            }
        }

        private void Usd1_getbn(object sender, EventArgs e)
        {
            usc_DragControlEdit us = (usc_DragControlEdit)sender;
            if (_tbBTDBN==null|| _tbBTDBN.Rows.Count==0)
            {
                GetBTDBN();
            }
            us.TbBTDBN = _tbBTDBN;
        }

        private void Usd1_endwaiting(object sender, EventArgs e)
        {
            CloseProgress();
        }

        private void Usd1_waiting(object sender, EventArgs e)
        {
            StartProgress("Loading...");
            
          
        }

        private void tinhcontrol()
        {
            foreach (usc_DragControlEdit item in _lstDragControlEdit)
            {
                item.Loadtheokp = _khoaload;
                    item.load();
                
            }
        }
     

        #endregion

        #region Các hàm hiển thị form Load

        private void StartProgress(String strStatusText)
        {
            objfrmShowProgress = new frmProgress();
            objfrmShowProgress.lblText.Text = strStatusText;
            ShowProgress();
        }

        private void CloseProgress()
        {
            try
            {
                Thread.Sleep(200);
                objfrmShowProgress.Invoke(new Action(objfrmShowProgress.Close));
            }
            catch (Exception)
            {

              
            }
        }

        private void ShowProgress()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    try
                    {
                        objfrmShowProgress.ShowDialog();
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    Thread th = new Thread(ShowProgress);
                    th.IsBackground = false;
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                TA_MessageBox.MessageBox.Show(ex.Message);
            }
        }
        
        #endregion

       

        #region Set số lượng cho từng khoa phòng
      
	    #endregion

        #region Check trung vi tri
        private bool TrungViTri(string vitri)
        {
            _sql = string.Format(" SELECT * FROM {0}.{1} WHERE {2} ='{3}'", _acc.Get_User(),cls_PG_DanhMucGiuong.tb_TenBang,cls_PG_DanhMucGiuong.col_VITRI, vitri);
            DataTable tb = _acc.Get_Data(_sql);
            if (tb.Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        } 
        #endregion

        #region Load danh mục giường tổng thể

        private void LoadDuLieuTongThe()
        {
            try
            {
                _tbTongThe = _tT.SourceViewTongThe(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text,dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
                DataRow drTongThe = _tbTongThe.NewRow();
                drTongThe["IDGIUONG"] = 0;
                _tbTongThe.Rows.InsertAt(drTongThe, 0);

              
            }
            catch
            {
            }

        }

        #endregion

       
        #endregion

        #region Phương thức public

        #region Gán source cho control khoa phòng
  
        public DataTable LoadKhoaPhong(string makp = null)
        {
            string where = "";
            if (!string.IsNullOrEmpty(makp))
            {
                where = " AND MAKP ='" + makp + "' ";
            }
            string sql = string.Format("select  MAKP,  TENKP from {0}.btdkp_bv where makp<>'01' and loai in (0,4)" + where + " order by loai,makp", _acc.Get_User());
            DataTable tbKhoa = _acc.Get_Data(sql);
            return tbKhoa;
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
            catch { return null; }
        }
        #endregion

      
        public void CapNhatValueTypeOfField()
        {
            _sql = string.Format("select * from {0}.{1}",_acc.Get_User(),cls_PG_DanhMucGiuong.tb_TenBang);
            if (_acc.Get_Data(_sql).Rows.Count>0)
            {
                //xóa dữ liệu
                _sql = string.Format("delete {0}.{1} ", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang);
                _acc.Execute_Data(ref _userError, ref _systemError, _sql);
                //Modify data
                _sql = string.Format("ALTER TABLE {0}.{1} MODIFY IDPHONG nvarchar2(20)",_acc.Get_User(),cls_PG_DanhMucGiuong.tb_TenBang);
                _acc.Execute_Data(ref _userError, ref _systemError, _sql);
            }

            _sql = string.Format("select * from {0}.{1}", _acc.Get_User(), cls_DanhMucPhong.tb_TenBang);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                //xóa dữ liệu
                _sql = string.Format("delete {0}.{1} ", _acc.Get_User(), cls_DanhMucPhong.tb_TenBang);
                _acc.Execute_Data(ref _userError, ref _systemError, _sql);
                //Modify data
                _sql = string.Format("ALTER TABLE {0}.{1} MODIFY MA nvarchar2(20)", _acc.Get_User(), cls_DanhMucPhong.tb_TenBang);
                _acc.Execute_Data(ref _userError, ref _systemError, _sql);
            }
        }

        #endregion

        #endregion

        #region Sự kiện

        void giuong_HisMouseUp(object sender, MouseEventArgs e)
        {
            Usc_GiuongEdit giuong = (Usc_GiuongEdit)sender;

            if (e.Button == MouseButtons.Right)
            {
                _idGiuongChonChuyen = giuong.Tag.ToString();
				string tinhtrang = "";
				string[] tmp = _idGiuongChonChuyen.Split(',');
				if (tmp.Length>3)
				{
					tinhtrang = tmp[2];
				}
				if ( tinhtrang !="3" && _tT.TrangThaiGiuong(_idGiuongChonChuyen.Split(',').First()) != "4")
                {
                    _idPhong = usc_SelectBoxKhoa.txtMa.Text;
                    contextMenuStrip1.Tag = giuong;
                    contextMenuStrip1.Show(Cursor.Position);
                    contextMenuStrip1.Show();
                }
            }
        }

        private void frm_QuanLyGiuong_Load(object sender, EventArgs e)
        {
            #region Vannq 05042019
            if (!string.IsNullOrEmpty(_maKhoaPhongNoiTru))
            {
                usc_SelectBoxKhoa.SetTenByMa(_maKhoaPhongNoiTru);
            } 
            #endregion

            LoadPanleLoaiPhong();
				LoadData();
			if (usc_lableTong1!=null)
			{
				usc_lableTong1.DataTinh = _tbDanhSachDuong;
				usc_lableTong1.frmLoad();

			}


			//lblTatCa_Click(null, null);
			_thongKe = "TatCa";
				

				//Load tieu de giuong theo tung khoa phong
				
				
            
           
        }

        private void LoadTreeView()
        {
            trVThongKe.Nodes.Clear();
            try
            {
                _isloadtabtinhtranggiuong = true;
                List<string> lstLoaiPhong = new List<string>();
         //   trVThongKe.BeginUpdate();
            trVThongKe.Nodes.Clear();
                //Fill all Department to TreeView
                //   DataTable tbKhoaPhong = LoadKhoaPhong(usc_SelectBoxKhoa.txtMa.Text);
                if (_tbDanhSachDuong == null|| _tbDanhSachDuong.Rows.Count==0)
                {
                    _tbDanhSachDuong = _tT.GetTBGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text, "");
                }
                DataTable tbKhoaPhong = _tbDanhSachDuong.DefaultView.ToTable(true, "MAKP", "TENKP");
                if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
                {
                    tbKhoaPhong = tbKhoaPhong.Select(" MAKP = '" + usc_SelectBoxKhoa.txtMa.Text + "' ").CopyToDataTable(); 
                }
                _lst.Add(cls_DanhMucPhong.col_MA);
            _lst.Add(cls_DanhMucPhong.col_TEN);
            trVThongKe.Nodes.Add("TatCa", "Tất cả");//Tất cả
            lstLoaiPhong.Add(cls_DanhMucLoaiPhong.col_MA);
            lstLoaiPhong.Add(cls_DanhMucLoaiPhong.col_TEN);
            DataTable tbLoaiPhong = _api.Search(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang,
                               _acc.Get_User(), -1, lst: lstLoaiPhong, dicEqual: null, orderByASC1: true, orderByName1: cls_DanhMucLoaiPhong.col_STT);
            for (int i = 0; i < tbKhoaPhong.Rows.Count; i++)//Khoa phòng
            {
                trVThongKe.Nodes["TatCa"].Nodes.Add(tbKhoaPhong.Rows[i]["MAKP"].ToString(), tbKhoaPhong.Rows[i]["TENKP"].ToString());
                _lst2.Clear();

                if (tbLoaiPhong.Rows.Count > 0)
                {
                    for (int j = 0; j < tbLoaiPhong.Rows.Count; j++)//Loại phòng
                    {
                        _lst2.Clear();
                        _lst2.Add(cls_DanhMucPhong.col_MAKP, tbKhoaPhong.Rows[i]["MAKP"].ToString());
                        _lst2.Add(cls_DanhMucPhong.col_LOAI, tbLoaiPhong.Rows[j]["MA"].ToString());
                        DataTable tbPhong = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang,
                                        _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);

                        if (tbPhong.Rows.Count > 0)
                        {
                            trVThongKe.Nodes["TatCa"].Nodes[tbKhoaPhong.Rows[i]["MAKP"].ToString()].Nodes.Add(tbLoaiPhong.Rows[j]["MA"].ToString(), tbLoaiPhong.Rows[j]["TEN"].ToString() +"("+ tbPhong.Rows.Count+")");
                        }
                        else
                        {
                            if (tbKhoaPhong.Rows[i]["MAKP"].ToString()!="")
                            {
                                trVThongKe.Nodes["TatCa"].Nodes[tbKhoaPhong.Rows[i]["MAKP"].ToString()].Nodes.Add(tbLoaiPhong.Rows[j]["MA"].ToString(), tbLoaiPhong.Rows[j]["TEN"].ToString() + "(0)");
                                
                            }
                        }
                    }
                }
            }

            Cursor.Current = Cursors.Default;
            //trVThongKe.EndUpdate();
             
                if (_dttmp == null)
                {
                    _dttmp = _tT.SourceViewTongThe(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text,dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
                }
                _dttmp = _tT.SourceViewTongThe(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text,dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
                //lbltongtinhtrang.DataTinh = (DataTable)dgvThongKe.DataSource;
				lbltongtinhtrang.frmLoad(usc_lableTong1);
                _isloadedtabtinhtranggiuong = true;
                trVThongKe.Refresh();



            }
            catch (Exception ex)
            {
                //throw;
            }
            _isloadtabtinhtranggiuong = false;

        }

        private void LoadDataGridViewGiuongTrong()
		{
            try
            {
                _isloadtabgiuongtrong = true;
                #region Load tổng thể tất cả loại giường (trạng thái trống).

                DataTable tbTieuDe = _tT.GetDanhMucLoaiPhong();
                DataTable tbTongTheTrong = new DataTable();
                DataTable tbEdit = new DataTable();
                List<string> lstKP = new List<string>();
                DataRow dr;
                DataColumn cl;

                cl = new DataColumn();
                cl.ColumnName = "MAKP";
                tbTongTheTrong.Columns.Add(cl);

                cl = new DataColumn();
                cl.ColumnName = "TENKP";
                tbTongTheTrong.Columns.Add(cl);
                //
                cl = new DataColumn();
                cl.ColumnName = "TongSo";
                cl.DataType = typeof(int);
                tbTongTheTrong.Columns.Add(cl);
                //
                for (int j = 0; j < tbTieuDe.Rows.Count; j++)
                {
                    cl = new DataColumn();
                    cl.ColumnName = tbTieuDe.Rows[j]["MA"].ToString() + "," + tbTieuDe.Rows[j]["TEN"].ToString();
                    cl.DataType = typeof(int);
                    tbTongTheTrong.Columns.Add(cl);
                }
                DataRow drBV = tbTongTheTrong.NewRow();
                drBV["MAKP"] = "999";
                drBV["TENKP"] = "Bệnh viện";
                DataTable tbtmp = _tT.GetDanhMucKhoaPhongCoGiuongTrongTheoLoai(usc_SelectBoxKhoa.txtMa.Text,maPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
                for (int ad = 0; ad < tbTieuDe.Rows.Count; ad++)
                {
                    //_sql = string.Format("select btd.MAKP,btd.TENKP,count(g.id) as SoLuong"
                    //                    + " from {0}.dmloaiphong lp left join {0}.danhmucphong p on lp.id=p.LOAI "
                    //                    + " left join {0}.PG_DANHMUCGIUONG g on g.maphong=p.maphong"//Sửa mã phòng
                    //                    + " left join {0}.danhmucloaigiuong lg  on g.loaigiuong=lg.maloai "
                    //                    + " left join {0}.btdkp_bv btd on btd.makp=p.MAKP "
                    //                    + " where g.tinhtrang = 0 and lp.MA ='{2}' and btd.makp is not null"
                    //                    + " group by btd.MAKP,btd.TENKP", _acc.Get_User(), "0", tbTieuDe.Rows[ad]["MA"].ToString().ToString());
                    //tbEdit = _acc.Get_Data(_sql);

                    try
                    {
                        tbEdit = tbtmp.Select("MA ='" + tbTieuDe.Rows[ad]["MA"] + "'").CopyToDataTable();
                    }
                    catch (Exception)
                    {
                        tbEdit = new DataTable();


                    }
                    for (int k = 0; k < tbEdit.Rows.Count; k++)
                    {
                        dr = tbTongTheTrong.NewRow();
                        if (tbEdit != null && tbEdit.Rows.Count + 1 > 0)
                        {
                            if (lstKP.IndexOf(tbEdit.Rows[k]["MAKP"].ToString()) == -1)
                            {
                                lstKP.Add(tbEdit.Rows[k]["MAKP"].ToString());
                            }
                            dr["MAKP"] = tbEdit.Rows[k]["MAKP"].ToString();
                            dr["TENKP"] = tbEdit.Rows[k]["TENKP"].ToString();
                            dr[tbTieuDe.Rows[ad]["MA"].ToString() + "," + tbTieuDe.Rows[ad]["TEN"].ToString()] = tbEdit.Rows[k]["SOLUONG"].ToString();
                            drBV[tbTieuDe.Rows[ad]["MA"].ToString() + "," + tbTieuDe.Rows[ad]["TEN"].ToString()] = int.Parse(drBV[tbTieuDe.Rows[ad]["MA"].ToString() + "," + tbTieuDe.Rows[ad]["TEN"].ToString()].ToString() == "" ? "0" : drBV[tbTieuDe.Rows[ad]["MA"].ToString() + "," + tbTieuDe.Rows[ad]["TEN"].ToString()].ToString()) + int.Parse(tbEdit.Rows[k]["SOLUONG"].ToString());
                            tbTongTheTrong.Rows.Add(dr);
                        }
                    }
                }
                if (string.IsNullOrEmpty( usc_SelectBoxKhoa.txtMa.Text))
                {
                    lstKP.Add("999"); 
                }
                tbTongTheTrong.Rows.Add(drBV);

                #region Get all column
                List<string> lstTenCot = new List<string>();
                for (int i = 2; i < tbTongTheTrong.Columns.Count; i++)
                {
                    lstTenCot.Add(tbTongTheTrong.Columns[i].ColumnName.ToString());
                }
                #endregion

                // dgvGiuongTrong.DataSource = tbTongTheTrong;

                #region Tao table group by so luong
                DataTable dtKP;
                DataRow drGroupBy;
                DataTable dtGroupBy = tbTongTheTrong.Copy();

                dtGroupBy.Clear();
                for (int kp = 0; kp < lstKP.Count; kp++)
                {
                    dtKP = tbTongTheTrong.Select("MAKP =" + lstKP[kp].ToString() + "").CopyToDataTable();
                    drGroupBy = dtGroupBy.NewRow();
                    drGroupBy["MAKP"] = dtKP.Rows[0]["MAKP"].ToString();
                    drGroupBy["TENKP"] = dtKP.Rows[0]["TENKP"].ToString();
                    for (int sum = 0; sum < lstTenCot.Count; sum++)
                    {
                        object sumObject = null;
                        try
                        {
                            sumObject = dtKP.AsEnumerable()
                                    .Sum(x => x.Field<int?>(lstTenCot[sum].ToString()));
                        }
                        catch //(Exception)
                        {

                            sumObject = "0";
                        }
                        drGroupBy[lstTenCot[sum].ToString()] = sumObject.ToString();

                    }

                    dtGroupBy.Rows.Add(drGroupBy);
                }
                #endregion

                #region Tao table moi(them tong so)
                 _tbThemTongSo = dtGroupBy.Clone();
                DataRow drThemTongSo = null;
                string themTongSo = string.Empty;
                int tongSo = 0, tongSoTren = 0, TongSoBV = 0;

                //change datatype of column in datagridview
                for (int kd = 0; kd < _tbThemTongSo.Columns.Count; kd++)
                {
                    _tbThemTongSo.Columns[kd].DataType = typeof(string);
                    _tbThemTongSo.Columns[kd].ColumnName = _tbThemTongSo.Columns[kd].ColumnName.ToString().Split(',').Last();
                }
                int tongsogiuong = 0; string tonsokhoa = "";

                for (int i = 0; i < dtGroupBy.Rows.Count; i++)
                {
                    tongSo = 0; tongSoTren = 0;
                    drThemTongSo = _tbThemTongSo.NewRow();
                    drThemTongSo[0] = dtGroupBy.Rows[i][0].ToString();
                    drThemTongSo[1] = dtGroupBy.Rows[i][1].ToString();
                    for (int j = 3; j < dtGroupBy.Columns.Count; j++)
                    {

                        themTongSo = GetAllBedOfDepartment(dtGroupBy.Rows[i][0].ToString(), dtGroupBy.Columns[j].ColumnName.ToString().Split(',').First());
                        tongSo += int.Parse(dtGroupBy.Rows[i][j].ToString());
                        drThemTongSo[j] = dtGroupBy.Rows[i][j].ToString() + "/" + themTongSo;
                        tongSoTren += int.Parse(themTongSo);
                        if (dtGroupBy.Rows[i][0].ToString() == "999")
                        {
                            tonsokhoa = GetAllsumBedOfDepartment(dtGroupBy.Rows[i][0].ToString(), dtGroupBy.Columns[j].ColumnName.ToString().Split(',').First());
                            drThemTongSo[j] = dtGroupBy.Rows[i][j].ToString() + "/" + tonsokhoa;
                            //tongSoTren += int.Parse(tonsokhoa);
                        }
                     
                    }
                    TongSoBV = TongSoBV + tongSoTren;
                    tongsogiuong += tongSoTren;
                    drThemTongSo[2] = tongSo + "/" + tongSoTren;
                    if (dtGroupBy.Rows[i][0].ToString() == "999")
                    {
                        drThemTongSo[2] = tongSo + "/" + tongsogiuong;

                    }
                    _tbThemTongSo.Rows.Add(drThemTongSo);
                }
                #endregion
                //
                #endregion

                #region Gán data source cho datagridview va thay doi tieu de




              
                #endregion

                _isloadedtabgiuongtrong = true;
            }
            catch (Exception ex)
            {


            }
            _isloadtabgiuongtrong = false;
        }

        private void usc_SelectBoxPhongTimKiem_HisSelectChange(object sender, EventArgs e)
        {
            try
            {
                //Cập nhật location
             
                LoadPanelGiuong();
				usc_lableTong1.DataTinh = _tbDanhSachDuong;
			
            }
            catch 
            {
            }
        }

        private void btnKhaiBaoPhong_Click(object sender, EventArgs e)
        {
            frm_DanhMucPhong f = new frm_DanhMucPhong();
            f.ShowDialog();
        }

        private void btnKhaiBaoGiuong_Click(object sender, EventArgs e)
        {
            if (DaChonKhoaPhong())
            {
                frm_DanhMucGiuong f = new frm_DanhMucGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxKhoa.txtTen.Text,
                    usc_SelectBoxPhongTimKiem.txtMa.Text, usc_SelectBoxPhongTimKiem.txtTen.Text);
                f.ShowDialog();
                if (f.ThayDoi)
                {
                    _tbDanhSachDuong = _tT.GetTBGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text, "");
                    Reload();
                    
                }

            }
            else
            {
                frm_DanhMucGiuong f = new frm_DanhMucGiuong();
                f.ShowDialog();
                if (f.ThayDoi)
                {
                    _tbDanhSachDuong = _tT.GetTBGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text, "");
                    Reload();
                    
                }
            }
        }

        private void btnKhaiBaoLoaiPhong_Click(object sender, EventArgs e)
        {
            frm_LoaiGiuong frm = new frm_LoaiGiuong();
            frm.ShowDialog();
            if (frm._statusCloseForm)
            {
                pnlLoai.Controls.Clear();
                LoadPanleLoaiPhong();
            }
        }

        
       
        
        private void usc_SelectBoxKhoa_HisSelectChange(object sender, EventArgs e)
        {
            try
            {
           
                //            LoadPanelGiuong();
                //usc_lableTong1.DataTinh = _tbDanhSachDuong;
                //            ClearList();
                //            _lst.Add(cls_DanhMucPhong.col_ID);
                //            _lst.Add(cls_DanhMucPhong.col_TEN);
                //            _lst2.Add(cls_DanhMucPhong.col_MAKP, usc_SelectBoxKhoa.txtMa.Text);
                //            _tbPhong = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang,
                //                _acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
                //            DataRow drPhong = _tbPhong.NewRow();
                //            drPhong["ID"] = "0";
                //            drPhong["TEN"] = "Tất cả";
                //            _tbPhong.Rows.Add(drPhong);
                //            usc_SelectBoxPhongTimKiem.DataSource = _tbPhong;
                //  _loadLanDauTien = false;
                // frm_QuanLyGiuong_Load(null, null);
                Reload();
            }
            catch(Exception ex)
            {
                return;
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            //frm_ThongKe frm = new frm_ThongKe();
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowDialog();
        }

        private void btnKhaiBaoLoaiPhong_Click_1(object sender, EventArgs e)
        {
            frm_LoaiPhong frm = new frm_LoaiPhong();
            frm.ShowDialog();
            if (frm._statusCloseForm)
            {
                pnlLoai.Controls.Clear();
                LoadPanleLoaiPhong();
                //LoadDuLieuAgain();
            }
        }

        private void frm_QuanLyGiuong_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_luuLocationGiuong)
            {
                //Cập nhật location
               // UdpateLocationGiuong();
            }
        }

        private void btnDanhSachChoDuyet_Click(object sender, EventArgs e)
        {
            frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(0);
            frm.ShowDialog();
            if (frm._status)
            {
               
            }
        }

        private void lblTrong_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _idColor = "1";
                contextMenuStrip2.Show(Cursor.Position);
                contextMenuStrip2.Show();
            }
        }

        private void lblDatTruoc_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _idColor = "2";
                contextMenuStrip2.Show(Cursor.Position);
                contextMenuStrip2.Show();
            }
        }

        private void lblCoNguoi_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _idColor = "3";
                contextMenuStrip2.Show(Cursor.Position);
                contextMenuStrip2.Show();
            }
        }

      
        private void dgvGiuongTrong_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in dgvGiuongTrong.Rows)
            {
                for (int i = 2; i < dgvGiuongTrong.Columns.Count; i++)
                {
                    r.Cells[i] = new DataGridViewLinkCell();
                    DataGridViewLinkCell c = r.Cells[i] as DataGridViewLinkCell;
                    c.LinkColor = Color.Green;
                }
            }
        }

        private void dgvGiuongTrong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 0 && e.ColumnIndex != 1)
                {
                    _tenLoaiGiuongLink = dgvGiuongTrong.Columns[e.ColumnIndex].Name.ToString();
                    _maKhoaPhongLink = dgvGiuongTrong.Rows[e.RowIndex].Cells[cls_DanhMucPhong.col_MAKP].Value.ToString();
                    LoadGiuongGomNhomTheoKhoa("0", _maKhoaPhongLink,"", _tenLoaiGiuongLink,true);
                    stcMain.SelectedTab = superTabItem2;
					
					

					//stcMain.SelectPreviousTab();
					
                }
            }
            catch
            {
            }
           
        }

        private void SelectIndex(int tab) 
        {
            //try
            //{
                if (InvokeRequired)
                {
                    MethodInvoker invoker = () => SelectIndex(tab);
                    Invoke(invoker);
                    return;
                }
                stcMain.SelectedTabIndex = tab;
            //}
            //catch (Exception ex)
            //{
            //    TA_MessageBox.MessageBox.Show(ex.Message);
            //}
            
        }

    
        private void lblChuaSuDung_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _idColor = "4";
                contextMenuStrip2.Show(Cursor.Position);
                contextMenuStrip2.Show();
            }
        }

     
        private void LoadValueGiuong(Usc_GiuongEdit giuongDuocChuyen)
        {
            //Load giuong duoc chuyen o day
            List<Usc_GiuongEdit> lstgiuong = new List<Usc_GiuongEdit>();
            List<Usc_GiuongEdit> lstGiuongKoCoViTri = new List<Usc_GiuongEdit>();
            DataTable tbGiuong = new DataTable();
            _slTrong = _slDat = _slCoNguoi = _slChuaSuDung = 0;
            DataTable tbDanhSachGiuongTheoTungKhoa = new DataTable();//Tất cả các khoa và tất cả các trạng thái
            LoadPanelGiuong();


            string vitri, tinhtrang, tenGiuong, dong1, dong2, dong3, temp = string.Empty;
            string textDSBNDat = string.Empty;
            string[] colorCoNguoi, colorTrong, colorDat, colorChuaSuDung, dSBNDat, toado;

            #region Get all giuong

            try
            {
                tbDanhSachGiuongTheoTungKhoa = _tbDanhSachDuong.Select("ID ='" + giuongDuocChuyen.ID + "'").CopyToDataTable();
            }
            catch
            {
                tbDanhSachGiuongTheoTungKhoa = null;
            }

            #endregion

            #region Gán thông tin giường
            if (tbDanhSachGiuongTheoTungKhoa != null)
            {
                try
                {
                    #region Tất cả các giường trong tất cả các khoa phòng
                    foreach (DataRow item in tbDanhSachGiuongTheoTungKhoa.Rows)
                    {
                        vitri = item["vitri"].ToString();
                        toado = vitri.Split(',');
                        tinhtrang = item["tinhtrang"].ToString();
                       
                        dong1 = item["Ten"].ToString();//Ẩn tên phòng trên thanh tiêu đề
                        giuongDuocChuyen.TenGiuong = dong1.ToUpper();//Gan tên giường
                        giuongDuocChuyen.Tag = item["id"].ToString() + "," + item["ten"].ToString() + "," + item["tinhtrang"].ToString() + "";//Gán id,tên, tình trạng để chuyển giường và đặt giường.
                        try
                        {
                            giuongDuocChuyen.ID = int.Parse(giuongDuocChuyen.Tag.ToString().Split(',').First().ToString());//gan id giuong
                        }
                        catch
                        {
                        }

                        #region Cập nhật màu và trạng thái cho từng giường trong phòng

                        if (tinhtrang == "2")
                        {
                            try
                            {
                                giuongDuocChuyen.pnlTop.Style.BackColor1.Color = usc_lableTong1.GetColorTinhTrang("2");// Color.FromArgb(int.Parse(colorCoNguoi[0].ToString()), int.Parse(colorCoNguoi[1].ToString()), int.Parse(colorCoNguoi[2].ToString()), int.Parse(colorCoNguoi[3].ToString()));
                            }
                            catch
                            {
                            }
                            try
                            {
                                giuongDuocChuyen.pnlTop.Style.BackColor2.Color = usc_lableTong1.GetColorTinhTrang("2");//Color.FromArgb(int.Parse(colorCoNguoi[0].ToString()), int.Parse(colorCoNguoi[1].ToString()), int.Parse(colorCoNguoi[2].ToString()), int.Parse(colorCoNguoi[3].ToString()));
                            }
                            catch
                            {
                            }
                            //Hiện hết thông tin mặc định
                          

                        }
                        else if (tinhtrang == "1")
                        {
                            _slDat++;
                            try
                            {
                                giuongDuocChuyen.pnlTop.Style.BackColor1.Color = usc_lableTong1.GetColorTinhTrang("1");//Color.FromArgb(int.Parse(colorDat[0].ToString()), int.Parse(colorDat[1].ToString()), int.Parse(colorDat[2].ToString()), int.Parse(colorDat[3].ToString()));
                            }
                            catch
                            {
                            }
                            try
                            {
                                giuongDuocChuyen.pnlTop.Style.BackColor2.Color = usc_lableTong1.GetColorTinhTrang("1");//Color.FromArgb(int.Parse(colorDat[0].ToString()), int.Parse(colorDat[1].ToString()), int.Parse(colorDat[2].ToString()), int.Parse(colorDat[3].ToString()));
                            }
                            catch
                            {
                            }
                            //Ẩn hết thông tin mặc định
                       
                        }
                        else if (tinhtrang == "0")
                        {

                            try
                            {
                                giuongDuocChuyen.pnlTop.Style.BackColor1.Color = usc_lableTong1.GetColorTinhTrang("0");//Color.FromArgb(int.Parse(colorTrong[0].ToString()), int.Parse(colorTrong[1].ToString()), int.Parse(colorTrong[2].ToString()), int.Parse(colorTrong[3].ToString()));
                            }
                            catch
                            {
                            }
                            try
                            {
                                giuongDuocChuyen.pnlTop.Style.BackColor2.Color = usc_lableTong1.GetColorTinhTrang("0");//Color.FromArgb(int.Parse(colorTrong[0].ToString()), int.Parse(colorTrong[1].ToString()), int.Parse(colorTrong[2].ToString()), int.Parse(colorTrong[3].ToString()));
                            }
                            catch
                            {
                            }
                            //Ẩn hết thông tin mặc định
                        
                        }

                        #endregion

                        #region Check dữ liệu table theodoigiuong
                        DataTable tbTongThe;
                        DataTable tb = new DataTable();

                        #region Thông tin giuong dat
                        tbTongThe = GetThongTinGiuongDat();
                        if (tbTongThe.Rows.Count > 0)
                        {
                            try
                            {
                                tb = GetThongTinGiuongDat().Select("IDGIUONG = '" + giuongDuocChuyen.ID + "'").CopyToDataTable();
                            }
                            catch
                            {
                            }
                            if (tb.Rows.Count > 0)
                            {
                                foreach (DataRow dr in tb.Rows)
                                {
                                   
                                    
                                }
                            }
                        }
                        #endregion

                        #region Thong tin giuong co nguoi
                        tbTongThe = GetDataTableTheoDoiGiuong("2");

                        if (tbTongThe.Rows.Count > 0)
                        {
                            try
                            {
                                tb = tbTongThe.Select("IDGIUONG = '" + giuongDuocChuyen.ID + "'").CopyToDataTable();
                            }
                            catch
                            {
                            }
                            if (tb.Rows.Count > 0)
                            {
                                foreach (DataRow dr in tb.Rows)
                                {

                                  
                                     
                                }
                                  #endregion
                            }
                        }
                        #endregion
                        #endregion

                        giuongDuocChuyen.HisMouseUp += new MouseEventHandler(giuong_HisMouseUp);
                        //Thêm loại giường
                        string[] kq = giuongDuocChuyen.Tag.ToString().Split(',');
                        
                        giuongDuocChuyen.Tag += "," + item["MALOAIPHONG"].ToString() + "," + item["MAKP"].ToString() + "," + item["MAPHONG"].ToString();

                        #region Get ma mau loai phong va set cho giuong
                        //string[] loaiPhong = giuongDuocChuyen.Tag.ToString().Split(',');
                        string maMauLoaiPhong = "";
                        string[] maMauDetail = null;
                        Color color = new Color();
                        try
                        {
                            maMauLoaiPhong = GetMaMauTuMaLoaiPhong(item["MALOAIPHONG"].ToString());
                        }
                        catch
                        {
                        }
                        try
                        {
                            maMauDetail = maMauLoaiPhong.Split(',');
                        }
                        catch
                        {
                        }
                        try
                        {
                            color = Color.FromArgb(int.Parse(maMauDetail[0].ToString()), int.Parse(maMauDetail[1].ToString()), int.Parse(maMauDetail[2].ToString()), int.Parse(maMauDetail[3].ToString()));
                        }
                        catch
                        {
                        }
                        try
                        {
                            giuongDuocChuyen.lblLoaiGiuongColor.BackColor = color;
                        }
                        catch
                        {
                        }
                        #endregion

                        #region Set tên loại giường

                        try
                        {
                            giuongDuocChuyen.lblLoaiGiuongTen.Text = GetTenLoaiDuaVaoMaLoai(item["MALOAIPHONG"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region Set số lượng bệnh nhân chờ vào giường

                        try
                        {
                            //string slBNChoDuyet= _tT.GetDataDanhSachChoDuyetTheoTungGiuong(giuong.ID.ToString()).Rows.Count.ToString();
                            string slBNChoDuyet = _tT.GetDanhSachChoDuyet(giuongDuocChuyen.ID.ToString()).Rows.Count.ToString();
                            giuongDuocChuyen.lblLoaiGiuongColor.Text = slBNChoDuyet == "0" ? "" : slBNChoDuyet;
                            giuongDuocChuyen.lblLoaiGiuongColor.TextAlignment = StringAlignment.Center;
                            giuongDuocChuyen.Tag += "," + giuongDuocChuyen.lblLoaiGiuongColor.Text;
                        }
                        catch
                        {
                        }

                        #endregion


                        #region Set số lượng bệnh nhân đang nằm tại giường

                        int sLBNDD = _tT.GetSoLuongNguoiNamTaiGiuong(giuongDuocChuyen.ID.ToString());
                        giuongDuocChuyen.TenGiuong += " - (" + sLBNDD.ToString() + ")";

                        #endregion

                        #region Thêm giường vào panel
                        giuongDuocChuyen.pa = dragControl;

                        #endregion
                    }
                    #endregion
                }
                catch
                {
                }
            }
            #endregion

        }

        private void trVThongKe_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
            try
            {
                _nodeCha = e.Node.Parent.Parent;//Tất cả
            }
            catch //(Exception)
            {
            }
            try
            {
                _nodeTB = e.Node.Parent;//Khoa
            }
            catch //(Exception)
            {
            }

            try
            {
                _nodeCon = e.Node;//Loại phòng
            }
            catch //(Exception)
            {
            }
                if (_dttmp==null)
                {
                    _dttmp = _tT.SourceViewTongThe(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text,dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
                }
                DataTable tb = _dttmp;

            if (_nodeCon.Name=="TatCa")
            {
					dgvThongKe.DataSource = tb;
                    dgvPhong.DataSource = tb.DefaultView.ToTable(true, "MAKP", "TENKP", "MA", "TEN", "MAPHONG", "TENPHONG");
					lbltongtinhtrang.DataTinh = (DataTable)dgvThongKe.DataSource;
				

				return;
            }
                if (_nodeCha == null)
                {
                    try
                    {
                        dgvPhong.DataSource = tb.Select("MAKP = " + _nodeCon.Name + "").CopyToDataTable().DefaultView.ToTable(true, "MAKP", "TENKP", "MA", "TEN", "MAPHONG", "TENPHONG");
                        dgvThongKe.DataSource = tb.Select("MAKP = " + _nodeCon.Name + "").CopyToDataTable();
					
					}
                    catch (Exception ex)
                    {
                        dgvPhong.DataSource = null;
                        dgvThongKe.DataSource = null;
                       
                    }
                }
                else
                {
                    if (_nodeTB != null)
                    {
                        try
                        {
                            dgvPhong.DataSource = tb.Select("MAKP = " + _nodeTB.Name + " and MA='" + _nodeCon.Name + "'").CopyToDataTable().DefaultView.ToTable(true, "MAKP", "TENKP", "MA", "TEN", "MAPHONG", "TENPHONG");
                            dgvThongKe.DataSource = tb.Select("MAKP = " + _nodeTB.Name + " and MA='" + _nodeCon.Name + "'").CopyToDataTable();
							
						}
						catch (Exception ex)
                        {
                            dgvThongKe.DataSource = null;
                            dgvPhong.DataSource = null;
                           
                        }
                    }
                    else
                    {
                        try
                        {
                            dgvPhong.DataSource = tb.Select("MAKP = " + _nodeCon.Name + "").CopyToDataTable().DefaultView.ToTable(true, "MAKP", "TENKP", "MA", "TEN", "MAPHONG", "TENPHONG");
                            //dgvThongKe.DataSource = tb.Select("TinhTrang='Trống' and MAKP = " + _nodeTB.Name + " and MAPHONG = " + _nodeCon.Name).CopyToDataTable();
                        }
                        catch (Exception ex)
                        {
                            dgvPhong.DataSource = null;
                            dgvThongKe.DataSource = null;
                         
                        }
                    }
					
				}
				lbltongtinhtrang.DataTinh = (DataTable)dgvThongKe.DataSource;
			}
            catch (Exception ex)
            {

                //throw;
            }
            if (dgvPhong.DataSource != null)
                dgvPhong.ClearSelection();
            pnlMain2.Visible = true;
			if (dgvThongKe.DataSource != null)
				dgvThongKe.ClearSelection();
			
        }

        private void btnDSChoXuatGiuong_Click(object sender, EventArgs e)
        {
            frm_DanhSachBenhNhanChoXuatGiuong frm = new frm_DanhSachBenhNhanChoXuatGiuong();
            frm.ShowDialog();
            if (frm._statusCloseForm)
            {
                _dschoduyet = _tT.GetDanhSachChoDuyet();
                _slnam = _tT.GetSoLuongNguoiNamTaiGiuong();
                _tbTongTheG = _acc.Get_Data(_tT.QueryGetGiuongTuTableDatGiuong());
                _tbDanhSachDuong = _tT.GetTBGiuong(usc_SelectBoxKhoa.txtMa.Text, usc_SelectBoxPhongTimKiem.txtMa.Text, "");
                foreach (usc_DragControlEdit item in _lstDragControlEdit)
                {
                    foreach (string idg in frm.Lstid)
                    {
                        item.ReloadDataGIuong(_tbDanhSachDuong, _dschoduyet, _slnam, _tbTongTheG, usc_lableTong1, idg);
                    }
                  

                }
                KiemTraCapNhatTRangThaiChoXuatVien();
            }
        }

        private void Reload()
        {
            KiemTraCapNhatTRangThaiChoXuatVien();
            pnlMain2.Controls.Clear();
            LoadGiuongGomNhomTheoKhoa(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text, dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
            _isloadedtabgiuongtrong = false;
            _isloadtabgiuongtrong = false;
            sTIGiuongTrong_Click(null, null);
            _isloadedtabtinhtranggiuong = false;
            _isloadtabtinhtranggiuong = false;
            sTICayThongKe_Click(null, null);
       
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

            if (txtTimKiem.Text != "") (dgvThongKe.DataSource as DataTable).DefaultView.RowFilter = string.Format("{1} LIKE '{0}%' OR {1} LIKE '% {0}%' OR {2} LIKE '{0}%' OR {2} LIKE '% {0}%' OR {3} LIKE '{0}%' OR {3} LIKE '% {0}%' ", txtTimKiem.Text,"TENBENHNHAN","MABN", "TENGIUONG");
            else (dgvThongKe.DataSource as DataTable).DefaultView.RowFilter = "";
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
			_loadLanDauTien = true;
			LoadGiuongGomNhomTheoKhoa(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text,dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
			pnlMain2.HorizontalScroll.Visible = true;
			
			Thread thr1 = new Thread(GetBTDBN);
			thr1.Start();
			ThreadStart ts = new ThreadStart(loadp1);
			Thread thr = new Thread(ts);
			thr.Start();
			
			timer1.Stop();
		}
		private void loadp1()
		{

			if (!_isloadtabgiuongtrong)
			{
				// tstabttg = new ThreadStart(LoadDataGridViewGiuongTrong);
				thrtabttg = new Thread(LoadDataGridViewGiuongTrong);
				thrtabttg.Start();
				
			}
			if (!_isloadtabtinhtranggiuong)
			{
				
				thrtabgiuongtrong = new Thread(LoadTreeView);
				thrtabgiuongtrong.Start();
				

			}



		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			//GetBTDBN();
			//LoadDataGridViewGiuongTrong();
			//LoadTreeView();
		
			//timer2.Stop();
		}

		private void dgvPhong_DataSourceChanged(object sender, EventArgs e)
		{
            int i = 0;
            foreach (DataGridViewRow  item in dgvPhong.Rows)
            {
                item.Cells["colSTT"].Value = ++i;
            }
        }

        private void dgvThongKe_DataSourceChanged(object sender, EventArgs e)
		{
           
        }



        private void usc_lableTong1_LBLDoubleClick_1(object sender, EventArgs e)
		{
            his_LabelX sourceControl = (his_LabelX)sender;
            string Loai = sourceControl.Name;
            #region Code Cu
            //his_LabelX sourceControl = (his_LabelX)sender;
            //string Loai = sourceControl.Name;

            //_optionChonLoai = false;
            //_luuLocationGiuong = false;
            //if (_luuLocationGiuong)
            //{
            //	//Cập nhật location
            //	UdpateLocationGiuong();
            //}
            ////LoadPanelGiuong();

            //#region Lọc theo điều kiện


            //if (_maKhoaLoad == "0" || string.IsNullOrEmpty(_maKhoaLoad))
            //{
            //	usc_SelectBoxPhongTimKiem.Enabled = false;
            //	//Load toan bo khoa phong va toan bo giuong(trong,conguoi,dadat)
            //	LoadGiuongGomNhomTheoKhoa(Loai, "", "");
            //}
            //else
            //{
            //	//Load Toan bo khoa va giuong trong co so du lieu theo khoa 
            //	usc_SelectBoxPhongTimKiem.Enabled = true;
            //	LoadGiuongGomNhomTheoKhoa(Loai, _maKhoaLoad, _maPhongLoad);//Thieu check theo khoa
            //																  //Gan data cho usc_selectBoxPhong
            //	ClearList();
            //	_lst.Add(cls_DanhMucPhong.col_MA);
            //	_lst.Add(cls_DanhMucPhong.col_TEN);
            //	_lst2.Add(cls_DanhMucPhong.col_MAKP, _maKhoaLoad);
            //	DataTable tb = _api.Search(ref _userError, ref _systemError, cls_DanhMucPhong.tb_TenBang,
            //		_acc.Get_User(), -1, lst: _lst, dicEqual: _lst2, orderByASC1: true, orderByName1: cls_DanhMucPhong.col_STT);
            //	DataRow drK = tb.NewRow();
            //	drK["MA"] = 0;
            //	drK["TEN"] = "Tất cả";
            //	tb.Rows.Add(drK);
            //	usc_SelectBoxPhongTimKiem.DataSource = tb;
            //}
            //string[] name = sourceControl.Text.Split(':');
            //if (name.Length>1)
            //{
            //	sourceControl.Text = _slDat.ToString() +":" +name[1];
            //}

            //LoadKhoaPhong();//load all khoa phòng
            //#endregion

            //LoadDataGridViewGiuongTrong(); 
            #endregion
          
            foreach (usc_DragControlEdit item in _lstDragControlEdit)
            {
                item.lblclick(Loai);
            } 

        }

		private void rdoTatCa_CheckedChanged(object sender, EventArgs e)
        {
            trVThongKe_AfterSelect(null, null);
        }

        private void dgvThongKe_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvThongKe.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            int i = 0;
            foreach (DataGridViewRow item in dgvThongKe.Rows)
            {
                item.Cells["STT"].Value = ++i;
                foreach (DataGridViewCell cell in item.Cells)
                    if ((cell.Value + "").Contains(" /n "))
                {
                        cell.Value = (cell.Value + "").Replace(" /n ", Environment.NewLine);
                }
            }
        }

        private void rdoTrong_CheckedChanged(object sender, EventArgs e)
        {
            trVThongKe_AfterSelect(null, null);
        }

    
        private void lblHu_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _idColor = "5";
                contextMenuStrip2.Show(Cursor.Position);
                contextMenuStrip2.Show();
            }
        }

        private void hủyĐặtGiườngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Shift | Keys.Enter))
            {
                SendKeys.Send("+{Tab}");
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void usc_lableTong1_reload(object sender, EventArgs e)
        {
            usc_lableTong1.reloadcolor();
            foreach (usc_DragControlEdit item in _lstDragControlEdit)
            {
                item.lblTong.reloadcolor();
                item.ReloadCOlor();

            }          
        }

        private void lbltongtinhtrang_LBLDoubleClick(object sender, EventArgs e)
        {
            his_LabelX sourceControl = (his_LabelX)sender;
            string Loai = sourceControl.Name;
            if (Loai!="3")
            {
                (dgvThongKe.DataSource as DataTable).DefaultView.RowFilter = "TinhTrang ='" + Loai + "'"; 
            }
            else
            {
                (dgvThongKe.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            lbltongtinhtrang.DataTinh = (DataTable)dgvThongKe.DataSource;
            dgvThongKe_DataSourceChanged(null, null);
            dgvThongKe.Refresh();
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

            if (_dttmp == null)
            {
                _dttmp = _tT.SourceViewTongThe(dkMaKhoa: usc_SelectBoxKhoa.txtMa.Text,dkMaPhong: usc_SelectBoxPhongTimKiem.txtMa.Text);
            }
            DataTable tb = _dttmp;
            _maKP = dgvPhong.Rows[dgvPhong.CurrentRow.Index].Cells["col_MAKPP"].Value.ToString();
            _maLoaiPhong = dgvPhong.Rows[dgvPhong.CurrentRow.Index].Cells["col_MALOAIPHONGP"].Value.ToString();
            _maPhong = dgvPhong.Rows[dgvPhong.CurrentRow.Index].Cells["col_MAPHONGP"].Value.ToString();
            try
            {
                dgvThongKe.DataSource = tb.Select("MAKP = " + _maKP + " and MA='" + _maLoaiPhong + "' and MAPHONG ='" + _maPhong + "'").CopyToDataTable();
				lbltongtinhtrang.DataTinh = (DataTable)dgvThongKe.DataSource;
			}
            catch //(Exception)
            {
                dgvThongKe.DataSource = null;
                //throw;
            }
			

          
            
        }

        private void sTICayThongKe_Click(object sender, EventArgs e)
        {
            usc_lableTong1.Visible = false;

            try
            {
                if (!_isloadedtabtinhtranggiuong)
                {


                    if (!_isloadtabtinhtranggiuong)
                    {
                     
                            StartProgress("Loading...");
                            LoadTreeView();

                            CloseProgress();
                            dgvThongKe_DataSourceChanged(null, null);
                        
                    }
                
                else
                {
                        StartProgress("Loading...");
                        Thread.Sleep(1000);
                        sTICayThongKe_Click(sender, e);
                        CloseProgress();
                    }
            }
				
			}
			catch (Exception)
			{

				
			}
        }

        private void superTabItem2_Click(object sender, EventArgs e)
        {
            usc_lableTong1.Visible = true;
        }

        private void sTIGiuongTrong_Click(object sender, EventArgs e)
        {
            usc_lableTong1.Visible = false;
            try
            {
                if (!_isloadedtabgiuongtrong)
                {
                    if (!_isloadtabgiuongtrong)
                    {
                            StartProgress("Loading...");
                            LoadDataGridViewGiuongTrong();
                            CloseProgress();
                    }
                    else
                    {
                    
                        Thread.Sleep(1000);
                        sTIGiuongTrong_Click(sender, e);
                        CloseProgress();
                        return;
                    }

                }
                if ( _tbThemTongSo != null)
                {
                    dgvGiuongTrong.DataSource = _tbThemTongSo;
                   
                    dgvGiuongTrong.AutoGenerateColumns = true;
                    dgvGiuongTrong.ScrollBars = ScrollBars.Both;
                    dgvGiuongTrong.Columns[0].HeaderText = "Mã khoa phòng";
                    dgvGiuongTrong.Columns[1].HeaderText = "Khoa phòng";
                    dgvGiuongTrong.Columns[2].HeaderText = "Tổng số";
                    dgvGiuongTrong.Columns[0].Visible = false;
                    dgvGiuongTrong.Columns[1].Width = 250;
                    dgvGiuongTrong.AllowUserToAddRows = false;
                    dgvGiuongTrong.ReadOnly = false;
                    for (int i = 2; i < dgvGiuongTrong.Columns.Count; i++)
                    {
                        //dgvGiuongTrong.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dgvGiuongTrong.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvGiuongTrong.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvGiuongTrong.AutoGenerateColumns = true;

                    }
                    foreach (DataGridViewRow r in dgvGiuongTrong.Rows)
                    {
                        for (int i = 2; i < dgvGiuongTrong.Columns.Count; i++)
                        {
                            r.Cells[i] = new DataGridViewLinkCell();
                            DataGridViewLinkCell c = r.Cells[i] as DataGridViewLinkCell;
                            c.LinkColor = Color.Green;
                        }
                    }
                    CloseProgress();
                }

            }
			catch (Exception)
			{

			
			}
        }

        private void trVThongKe_Click(object sender, EventArgs e)
        {
            //trVThongKe_AfterSelect(null, null);
        }

        private void dgvThongKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
			try
            {
                string name = dgvThongKe.Columns[e.ColumnIndex].Name;
                string value = dgvThongKe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (name == "col_TinhTrang")
                {
                    string valuett = dgvThongKe.Rows[e.RowIndex].Cells["col_MaTinhTrang"].Value.ToString();
                    string valuetmp = "TatCa";
                   
                    switch (valuett)
                    {
                        case "Trống":
                            valuetmp = "0";
                            break;
                        case "Có người":
                            valuetmp = "2";
                            break;
                        case "Đặt":
                            valuetmp = "1";
                            break;
                        case "Chưa sử dụng":
                            valuetmp = "4";
                            break;
                        case "Hư":
                            valuetmp = "4";
                            break;
                        default:
                            valuetmp = "3";
                            break;
                    }
                    //Get mã khoa, mã phòng,trạng thái
                    _tenLoaiGiuongLink = dgvThongKe.Rows[e.RowIndex].Cells["col_TenLoaiPhong"].Value.ToString();
                    _maPhongGiuong = dgvThongKe.Rows[e.RowIndex].Cells["col_MaPhongLink"].Value.ToString();

                    _maKhoaPhongLink = dgvThongKe.Rows[e.RowIndex].Cells["col_MaKPLink"].Value.ToString();

                    LoadGiuongGomNhomTheoKhoa(valuett, _maKhoaPhongLink, _maPhongGiuong, _tenLoaiGiuongLink, true);
                    stcMain.SelectedTab = superTabItem2;
                    //stcMain.TabIndex(1);
                }
            }
			catch (Exception)
			{

			}
        }
        
        void lblTieuDe_DoubleClick(object sender, EventArgs e)
        {
            LabelX lb = (LabelX)sender;
            Control control = lb.Parent.Parent;
            dragControl = control as usc_DragControlEdit;
            if ((dragControl != null && dragControl.pnlMain.Controls.Count == 0)|| _isload)
            {
                StartProgress("Loading...");
			
				dragControl.pnlMain.Controls.Clear();
				string makp = dragControl.Tag.ToString(); 
				if (!string.IsNullOrEmpty(_maKhoaGiuong))
				{
					makp = _maKhoaGiuong;
					_tenLoaiGiuongLink = "";
					_maKhoaPhongLink = "";
					_maPhongGiuong = "";
					LoadGiuongGomNhomTheoKhoa("3", makp, "");
					

				}
				else
				{
					LoadGiuongGomNhomTheoKhoa("3", makp, "");
					//throw new Exception("Load Tất cả");
					//LoadLaiGiuongTrongDragControl("TatCa", makp, "");
				}
				CloseProgress();

            }
		
		}

       public void btnDown_Click(object sender, EventArgs e)
        {
            ButtonX bt = (ButtonX)sender;
            Control control = bt.Parent.Parent;
            dragControl = control as usc_DragControlEdit;
            if (dragControl != null && dragControl.pnlMain.Controls.Count == 0)
            {
                StartProgress("Loading...");
				throw new Exception("Load Tất cả");
				//  LoadLaiGiuongTrongDragControl("TatCa", dragControl.Tag.ToString(), "");
				CloseProgress();
            }

        }

       

        void lblChuaSuDung_MouseHover(object sender, EventArgs e)
        {
            Panel dr = (Panel)sender;
            Control control = dr.Parent.Parent;
            dragControl = control as usc_DragControlEdit;
        }

        void pnlMain_MouseHover(object sender, EventArgs e)
        {
            Panel dr = (Panel)sender;
            Control control = dr.Parent;
            dragControl = control as usc_DragControlEdit;
        }

       
        void lblGiuong_MouseHover(object sender, EventArgs e)
        {
            LabelX dr = (LabelX)sender;
            Control control = dr.Parent.Parent.Parent.Parent;
            dragControl = control as usc_DragControlEdit;
            Control controlGiuong = dr.Parent.Parent;
            Usc_GiuongEdit giuong = controlGiuong as Usc_GiuongEdit;
            giuong.lblGiuong.BringToFront();
            giuong.lblLoaiGiuongColor.BringToFront();
        }

        

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            //frm_BaoCao frm = new frm_BaoCao(_tbBTDBN);
            //frm.ShowDialog();
            
        }
      
    }
}
