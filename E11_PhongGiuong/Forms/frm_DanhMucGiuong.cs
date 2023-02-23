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
    public partial class frm_DanhMucGiuong : E00_Base.frm_DanhMuc
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
		private bool _isediting = false;
        private DataTable _tbGiaGiuong = null;
        public bool ThayDoi = false;
		string _id2 = String.Empty;
		string _idDMG = String.Empty;
        private string _curID = string.Empty;
        private bool _isChangeMa = false;
        private bool _canrunagain = true;
        
        private string _lastMa = string.Empty;
        #endregion

        #region Khởi tạo

        public frm_DanhMucGiuong()
        {
            InitializeComponent();
            //pnlSearch.Visible = false;
            btnXoa.Visible = false;
            btnXoa.Visible = btnBoQua.Visible = btnThem.Visible = true;
            dgvDanhMucGiuong.Enabled = usc_SelectBoxKhoa.Enabled = usc_SelectBoxPhong.Enabled = usc_SelectBoxLoaiGiuong.Enabled = txtTen.Enabled = itgSTT.Enabled = true;
            btnIn.Visible =  false;
            _api.KetNoi();
			LoadDanhSachDoiTuong();
        }

        public frm_DanhMucGiuong(string maKhoaLoadNgoai, string tenKhoaLoadNgoai, string maPhongLoadNgoai,
            string tenPhongLoadNgoai)
        {
            InitializeComponent();
            //pnlSearch.Visible = false;
            btnXoa.Visible = false;
            _maKhoaLoadNgoai = maKhoaLoadNgoai;
            _maPhongLoadNgoai = maPhongLoadNgoai;
            _tenKhoaLoadNgoai = tenKhoaLoadNgoai;
            _tenPhongLoadNgoai = tenPhongLoadNgoai;
            dgvDanhMucGiuong.Enabled = usc_SelectBoxKhoa.Enabled = usc_SelectBoxPhong.Enabled = usc_SelectBoxLoaiGiuong.Enabled = txtTen.Enabled = itgSTT.Enabled = true;
            btnIn.Visible =  false;
            _api.KetNoi();

        }

        public frm_DanhMucGiuong(string _idGiuongChonChuyen, string maKhoa, string tenKhoa, string maPhong,
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
            btnIn.Visible =  false;
            _api.KetNoi();
        }

        #endregion

        #region Phương thức

        #region Phương thức protected

        protected override void LoadData()
        {
            TimKiem();
            base.LoadData();
        }

        protected override void Them()
        {
            ClearData();
			itgSTT.Enabled = true;
			_isAdd = true;
			_isediting = true;

			usc_SelectBoxKhoa.Enabled = true;
           
            base.Them();
            if (string.IsNullOrEmpty(usc_SelectBoxPhong.txtMa.Text))
            {
                usc_SelectBoxPhong.txtTen.Focus();
            }
            try
            {
                //itgSTT.Value = int.Parse(_acc.Get_Data(_tT.QueryGetSTTDanhMucGiuong(usc_SelectBoxPhong.txtMa.Text)).Rows[0].ItemArray[0].ToString()) + 1;
                itgSTT.Value = int.Parse(_tT.GetMaxIDDanhMucGiuong()) + 1;
            }
            catch { itgSTT.Value = 1; }
            dgvDanhMucGiuong.Enabled = false;
            dgvGiaGoiY.DataSource = null;
            pnlControl2.Enabled = true;//2
            //dgvGiaGoiY.Enabled = true;
            //ControlSetFocus(usc_SelectBoxKhoa);
            usc_SelectBoxKhoa.txtTen.Focus();
            txtMaGiuong.Enabled = true;
            
        }

        protected override void Sua()
        {
            dgvDanhMucGiuong.Enabled = false;
            dgvGiaGoiY.Enabled = true;
            _isAdd = false;
			_isediting = true;
            _isChangeMa = false;
            itgSTT.Enabled = true;
            try
            {
                _curID = dgvDanhMucGiuong.SelectedRows[0].Cells["col_IdG"].Value + "";
            }
            catch (Exception)
            {

                
            }
            base.Sua();

            pnlControl2.Enabled = true;//1
            txtMaGiuong.Enabled = true;
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
            try
            {
                if (_isAdd && string.IsNullOrEmpty(_idGiuong))
                {
                    if (NhapDuLieu())
                    {
                        btnGoiYGia.Enabled = true;

                        #region Insert vào table v_giavp
                        //ClearList();
                        //string sql = string.Format("select max(ID)+1 from {0}.v_giavp", _acc.Get_User());
                        //int idGiaVP = int.Parse(_acc.Get_Data(sql).Rows[0].ItemArray[0].ToString());
                        //string donGia = "0", baoHiem = "0", chinhSach = "0", dichVu = "0", nuocNgoai = "0";
                        //string maxMaSo = _acc.Get_Data(string.Format("select max(ma) from {0}.PG_DANHMUCGIUONG", _acc.Get_User())).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(string.Format("select max(ma)+1 from {0}.PG_DANHMUCGIUONG", _acc.Get_User())).Rows[0].ItemArray[0].ToString();
                        //sql = string.Format("insert into {0}.{1}(ID,ID_LOAI,STT,MA,TEN,GIA_TH,GIA_BH,GIA_CS,GIA_DV,GIA_NN,NGAYUD) values({2},{3},{4},'{5}','{6}',{7},{8},{9},{10},{11},TO_DATE('{12}', 'dd/mm/yyyy hh24:mi:ss'))",
                        //    _acc.Get_User(), cls_V_GiaVienPhi.tb_TenBang, idGiaVP, 1, itgSTT.Value.ToString(), maxMaSo, txtTen.Text, donGia, baoHiem, chinhSach, dichVu, nuocNgoai, TT.GetSysDate().ToString("dd/MM/yyyy hh:mm:ss tt").Substring(0, 10));
                        //if (!_acc.Execute_Data(ref _userError, ref _systemError, sql))
                        //{
                        //    TA_MessageBox.MessageBox.Show("Không thể thêm giá viện phí!", TA_MessageBox.MessageIcon.Error);
                        //    txtTen.Focus();
                        //    return;
                        //}
                        #endregion

                        #region Insert vào table v_giavpct
                        ////List<string> lst3 = new List<string>();
                        ////List<string> lst4 = new List<string>();
                        //_sql = string.Empty;
                        //string GetIdVpCTAuto = _acc.Get_Data(string.Format("select max(ID)+1 from {0}.v_giavpct", _acc.Get_User())).Rows[0][0].ToString();
                        //int idGiaVPCT = GetIdVpCTAuto == "" ? 1 : int.Parse(GetIdVpCTAuto);
                        //int gia;
                        //foreach (DataRow item in _tbGiaGoiY.Rows)
                        //{
                        //    #region Code cũ
                        //    //int gia = item["GIA"].ToString() == "" ? 0 : int.Parse(item["GIA"].ToString());
                        //    //ClearList();
                        //    //_lst2.Add(cls_GiaVienPhiCT.col_ID_GIAVP, idGiaVP.ToString());
                        //    //_lst2.Add(cls_GiaVienPhiCT.col_MADOITUONG, item["MADOITUONG"].ToString());
                        //    //_lst2.Add(cls_GiaVienPhiCT.col_ID_BANGGIA, _tT.GetIDBangGiaMoiNhat());
                        //    //_lst2.Add(cls_GiaVienPhiCT.col_GIA, gia.ToString());
                        //    //lst3.Add(cls_GiaVienPhiCT.col_ID_GIAVP);
                        //    //lst3.Add(cls_GiaVienPhiCT.col_MADOITUONG);
                        //    //lst3.Add(cls_GiaVienPhiCT.col_ID_BANGGIA);
                        //    //lst3.Add(cls_GiaVienPhiCT.col_GIA);
                        //    //lst4.Add(cls_GiaVienPhiCT.col_ID_GIAVP);
                        //    //lst4.Add(cls_GiaVienPhiCT.col_MADOITUONG);
                        //    //lst4.Add(cls_GiaVienPhiCT.col_ID_BANGGIA);
                        //    //lst4.Add(cls_GiaVienPhiCT.col_GIA);
                        //    //if (!_api.Insert(ref _userError, ref _systemError, cls_GiaVienPhiCT.tb_TenBang, _lst2, lst3, lst4))
                        //    //{
                        //    //    TA_MessageBox.MessageBox.Show("Không thể thêm giá viện phí chi tiết!", TA_MessageBox.MessageIcon.Error);
                        //    //    btnLuu.Focus();
                        //    //    return;
                        //    //} 
                        //    #endregion

                        //    gia = item["GIA"].ToString() == "" ? 0 : int.Parse(item["GIA"].ToString());
                        //    _sql += string.Format("insert into {0}.V_GIAVPCT(ID,ID_GIAVP,MADOITUONG,ID_BANGGIA,GIA) values({1},{2},{3},{4},{5}); ",
                        //                        _acc.Get_User(), idGiaVPCT, idGiaVP.ToString(), item["MADOITUONG"].ToString(), _tT.GetIDBangGiaMoiNhat(), gia.ToString()) + Environment.NewLine;
                        //    idGiaVPCT += 1;
                        //}
                        //if (!DatabaseV2.Database.Execute_Insert(ref _userError, ref _systemError, _sql))
                        //{
                        //    TA_MessageBox.MessageBox.Show("Lỗi thực thi đa luồng!"
                        //                  , TA_MessageBox.MessageIcon.Error);
                        //    return;
                        //}
                        #endregion

                        #region Insert vào table dmgiuong
                        //int idGiaVP = int.Parse(_acc.Get_Data(sql).Rows[0].ItemArray[0].ToString());
                        //string idGiuong = "1";
                        //string sql = string.Format("select max(ID)+1 from {0}.{1}", _acc.Get_User(),cls_PG_DanhMucGiuong.tb_TenBang);
                        //DataTable tb = _acc.Get_Data(_sql);
                        //if (tb.Rows[0][0].ToString()!="")
                        //{
                        //    idGiuong = tb.Rows[0][0].ToString();
                        //}
                        string maxMaSo = _acc.Get_Data(string.Format("select max({0}) from {1}.{2}", cls_PG_DanhMucGiuong.col_MA, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang)).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(string.Format("select max({0})+1 from {1}.{2}", cls_PG_DanhMucGiuong.col_MA, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang)).Rows[0].ItemArray[0].ToString();
                        string toado = "";
                        string donGia = "0", baoHiem = "0", chinhSach = "0", dichVu = "0", nuocNgoai = "0";

					

						ClearList();
                        _lst2.Add(cls_PG_DanhMucGiuong.col_IDPHONG, usc_SelectBoxPhong.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_ID, maxMaSo.ToString());
                        _lst2.Add(cls_PG_DanhMucGiuong.col_STT, itgSTT.Value.ToString());
                        _lst2.Add(cls_PG_DanhMucGiuong.col_MA, maxMaSo);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_TEN, txtTen.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_TH, donGia);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_BH, baoHiem);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_CS, chinhSach);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_DV, dichVu);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_GIA_NN, nuocNgoai);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_SOLUONG, "1");
                        _lst2.Add(cls_PG_DanhMucGiuong.col_TINHTRANG, tinhTrangGiuong ==""? "0" : tinhTrangGiuong);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_IDGIAVP, usc_GiaGoiY.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_MAPHONG, usc_SelectBoxPhong.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_VITRI, toado);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_NGAYUD, _tT.GetSysDate().ToString());
                        _lst2.Add(cls_PG_DanhMucGiuong.col_LOAIGIUONG, usc_SelectBoxLoaiGiuong.txtMa.Text);
						_lst2.Add(cls_PG_DanhMucGiuong.col_CHUAN, slbChuan.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_BLOCK, chkGiuongKoDem.Checked ? "1" : "0");
                        if (!TrungMaGiuong(txtMaGiuong.Text))
                        {
                            _lst2.Add(cls_PG_DanhMucGiuong.col_MAGIUONG, txtMaGiuong.Text);
                        }
                        else
                        {
                            TA_MessageBox.MessageBox.Show("Trùng mã giường không thể thêm giường!", TA_MessageBox.MessageIcon.Error);
                            txtMaGiuong.Focus();
                            return;
                        }
                        _lst2.Add(cls_PG_DanhMucGiuong.col_MACHINEID, GetMachine());
                        _lst.Add(cls_PG_DanhMucGiuong.col_ID);
						
						if (!_api.Insert(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang, _lst2, _lst, _lst))
						{
							TA_MessageBox.MessageBox.Show("Không thể thêm giường!", TA_MessageBox.MessageIcon.Error);
							txtTen.Focus();
							return;
						}
                        try
                        {

                            _tT.InsertGiaGiuong(maxMaSo, "3", slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);

                        }
                        catch (Exception)
                        {

                            
                        }
						TA_MessageBox.MessageBox.Show("Thêm giường thành công!", TA_MessageBox.MessageIcon.Information);
						txtMaGiuong.Enabled = false;
						_isediting = false;
						#endregion

						#region Set lại khoa và phòng
						if (usc_SelectBoxKhoa.txtMa.Text != "" && !string.IsNullOrEmpty(usc_SelectBoxPhong.txtMa.Text))
                        {
                            usc_SelectBoxKhoa.txtMa.Text = _maKhoa;
                            usc_SelectBoxPhong.txtMa.Text = _maPhong;
                        }
                        #endregion

                        _check = 1;
                        if (_check == 1)
                        {
                            dgvDanhMucGiuong.Enabled = true;
                            dgvGiaGoiY.Enabled = false;
                            base.Luu();
                            LoadData();
                            btnThem.Focus();
                            ThayDoi = true;
                        }

                    }
                    
                }
                if (!string.IsNullOrEmpty(_idGiuong))
                {
                    if (NhapDuLieu())
                    {
                        #region Cập nhật giá giường
                        //Dictionary<string, string> lst3 = new Dictionary<string, string>();
                        //DataTable tbGiaUpdate = ((DataTable)dgvGiaGoiY.DataSource).GetChanges();
                        //if (tbGiaUpdate != null)
                        //{
                        //    foreach (DataRow item in tbGiaUpdate.Rows)
                        //    {
                        //        int gia = item["GIA"].ToString() == "" ? 0 : int.Parse(item["GIA"].ToString());
                        //        string idGiaVP = item["ID"].ToString();
                        //        ClearList();
                        //        lst3.Clear();
                        //        _lst2.Add(cls_GiaVienPhiCT.col_ID, idGiaVP.ToString());
                        //        _lst2.Add(cls_GiaVienPhiCT.col_MADOITUONG, item["MADOITUONG"].ToString());
                        //        _lst2.Add(cls_GiaVienPhiCT.col_ID_BANGGIA, _tT.GetIDBangGiaMoiNhat());
                        //        _lst2.Add(cls_GiaVienPhiCT.col_GIA, gia.ToString());
                        //        lst3.Add(cls_GiaVienPhiCT.col_ID, idGiaVP.ToString());
                        //        if (!_api.Update(ref _userError, ref _systemError, cls_GiaVienPhiCT.tb_TenBang,
                        //             _lst2, new List<string>(), lst3))
                        //        {
                        //            TA_MessageBox.MessageBox.Show("Không thể cập nhật giá viện phí chi tiết!", TA_MessageBox.MessageIcon.Error);
                        //            btnLuu.Focus();
                        //            return;
                        //        }
                        //        btnGoiYGia.Enabled = false;
                        //    }
                        //}
                        #endregion

                        #region Update số thứ tự
                        ClearList();
                        _lst2.Add(cls_PG_DanhMucGiuong.col_STT, itgSTT.Value.ToString());
                        _lst2.Add(cls_PG_DanhMucGiuong.col_TINHTRANG, tinhTrangGiuong == "" ? "0" : tinhTrangGiuong);
                         _lst2.Add(cls_PG_DanhMucGiuong.col_IDGIAVP, usc_GiaGoiY.txtMa.Text);
						_lst2.Add(cls_PG_DanhMucGiuong.col_CHUAN, slbChuan.txtMa.Text);
                        _lst3.Add(cls_PG_DanhMucGiuong.col_ID, _idGiuong);

						//_tT.UpdateGiaGiuong(_idDMG, usc_SelectBoxLoaiGiuong.txtMa.Text, slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);
						if (!_api.Update(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang,  _lst2,  new List<string>(),  _lst3))
                        {
                            TA_MessageBox.MessageBox.Show("Không thể cập nhật số thứ tự giường!", TA_MessageBox.MessageIcon.Error);
                            itgSTT.Focus();
                            return;
                        }
						#endregion

						_check = 1;
                        if (_check == 1)
                        {
                            dgvDanhMucGiuong.Enabled = true;
                            dgvGiaGoiY.Enabled = false;
                            base.Luu();
                            LoadData();
                            btnThem.Focus();
                            ThayDoi = true;
                        }
                    }
                }
                else if (string.IsNullOrEmpty(_idGiuong) && !_isAdd)
                {
                    if (NhapDuLieu())
                    {
                        #region Cập nhật giá giường
                        //Dictionary<string, string> lst3 = new Dictionary<string, string>();
                        //DataTable tbGiaUpdate = ((DataTable)dgvGiaGoiY.DataSource).GetChanges();
                        //if (tbGiaUpdate != null)
                        //{
                        //    foreach (DataRow item in tbGiaUpdate.Rows)
                        //    {
                        //        int gia = item["GIA"].ToString() == "" ? 0 : int.Parse(item["GIA"].ToString());
                        //        string idGiaVP = item["ID"].ToString();
                        //        ClearList();
                        //        lst3.Clear();
                        //        _lst2.Add(cls_GiaVienPhiCT.col_ID, idGiaVP.ToString());
                        //        _lst2.Add(cls_GiaVienPhiCT.col_MADOITUONG, item["MADOITUONG"].ToString());
                        //        _lst2.Add(cls_GiaVienPhiCT.col_ID_BANGGIA, _tT.GetIDBangGiaMoiNhat());
                        //        _lst2.Add(cls_GiaVienPhiCT.col_GIA, gia.ToString());
                        //        lst3.Add(cls_GiaVienPhiCT.col_ID, idGiaVP.ToString());
                        //        if (!_api.Update(ref _userError, ref _systemError, cls_GiaVienPhiCT.tb_TenBang,
                        //             _lst2,  new List<string>(),  lst3))
                        //        {
                        //            TA_MessageBox.MessageBox.Show("Không thể cập nhật giá viện phí chi tiết!", TA_MessageBox.MessageIcon.Error);
                        //            btnLuu.Focus();
                        //            return;
                        //        }
                        //        btnGoiYGia.Enabled = false;
                        //    }
                        //}
                        #endregion

                        #region Update all thông tin của giuòng
                        ClearList();
                        string idupdate = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_IdG"].Value + "";
                        //_lst2.Add(cls_PG_DanhMucGiuong.col_IDPHONG, usc_SelectBoxPhong.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_MAPHONG, usc_SelectBoxPhong.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_LOAIGIUONG, usc_SelectBoxLoaiGiuong.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_TEN, txtTen.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_STT, itgSTT.Value.ToString());
                        _lst2.Add(cls_PG_DanhMucGiuong.col_TINHTRANG, tinhTrangGiuong == "" ? "0" : tinhTrangGiuong);
                        _lst3.Add(cls_PG_DanhMucGiuong.col_ID, idupdate);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_IDGIAVP, usc_GiaGoiY.txtMa.Text);
						_lst2.Add(cls_PG_DanhMucGiuong.col_CHUAN, slbChuan.txtMa.Text);
                        _lst2.Add(cls_PG_DanhMucGiuong.col_BLOCK, chkGiuongKoDem.Checked ? "1" : "0");
                        //_lst2.Add("MAPHONG", usc_SelectBoxPhong.txtMa.Text);
                        if (_isChangeMa)
                        {
                            _lst2.Add(cls_PG_DanhMucGiuong.col_MAGIUONG, txtMaGiuong.Text);
                        }

                        if (!_api.Update(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang, _lst2, new List<string>(), _lst3))
						{
							TA_MessageBox.MessageBox.Show("Không thể cập nhật thông tin giường!", TA_MessageBox.MessageIcon.Error);
							itgSTT.Focus();
							return;
						}

                        try
                        {

                            _tT.UpdateGiaGiuong(idupdate, usc_SelectBoxLoaiGiuong.txtMa.Text, slbDoiTuong.txtMa.Text, usc_GiaGoiY.txtMa.Text, txtGiaTien.Text);

                        }
                        catch (Exception)
                        {
                            
                        }
                        #endregion

                        _check = 1;
                        if (_check == 1)
                        {
                            dgvDanhMucGiuong.Enabled = true;
                            dgvGiaGoiY.Enabled = false;
                            base.Luu();
                            LoadData();
                            ThayDoi = true;
                        }
                    }
                   
                }
                txtTimKiem.Focus();
            }

            catch
            {
                //return;   
            }
        }

        protected override void Xoa()
        {
            try
            {
                if (!_canrunagain)
                {
                    return;
                }
                ClearList();
                #region Delete each record
                if (_deleteOneRecord == 1)
                {
                    _curID = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex+1].Cells["col_idG"].Value.ToString();
                    string id = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_idG"].Value.ToString();
                    _lst2.Add(cls_PG_DanhMucGiuong.col_ID.ToUpper(), id.ToUpper());
                    if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTen.Text,
                       
                         TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
						//_tT.DeleleGiaGiuong(_id2);
                        if (!_api.Delete(ref _userError, ref _systemError, cls_PG_DanhMucGiuong.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTen.Text, _userError)
                              , TA_MessageBox.MessageIcon.Error);
                            dgvDanhMucGiuong.Focus();
                            return;
                        }
						else
						{
							_lst2.Clear();
							_lst2.Add(cls_PG_DanhMucGiuong.col_MA.ToUpper(), id);
                            try
                            {
                                bool bl = _api.Delete(ref _userError, ref _systemError, cls_GiaGiuong.tb_TenBang, _lst2, null);
                            }
                            catch (Exception)
                            {

                            }
						}
					}
                }
                #endregion
                else
                {
                    #region Delete all record have checked
                    string str = "", strHoTen = string.Empty;
                    ///append id
                    _curID = _lstCheck[_lstCheck.Count-1];
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
                ThayDoi = true;
                ClearData();
                _canrunagain = false;
                timer1.Start();
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
			_isediting = false;
			base.BoQua();
            pnlControl2.Enabled = true;//4
        }

        protected override void TimKiem()
        {
            try
            {
                ClearList();
                LoadDanhMucKhoa();
                LoaiDanhMucPhong();
                LoadChuan();
                ClearList();
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI);
                _lst.Add(cls_D_DanhMucLoaiGiuong.col_TENLOAI);
                _lst2.Add(cls_D_DanhMucLoaiGiuong.col_MALOAI, "is not null");
                usc_SelectBoxLoaiGiuong.DataSource = _api.Search(ref _userError,
                    ref _systemError, cls_D_DanhMucLoaiGiuong.tb_TenBang, lst: _lst, dicDifferent: _lst2);
                DataTable tb;
                _chuoiTimKiem = txtTimKiem.Text;
                try
                {
                    if (!string.IsNullOrEmpty(_chuoiTimKiem))
                    {
                        tb = _tT.SourcePanelGiuongTheoDanhMucGiuong().Select(cls_PG_DanhMucGiuong.col_TEN+" like '%" + _chuoiTimKiem + "%'").CopyToDataTable();
                    }
                    else
                    {
                        tb = _tT.SourcePanelGiuongTheoDanhMucGiuong();
                    }
                }
                catch
                {
                    tb = null;
                }
                if (tb!=null && tb.Rows.Count>0)
                {
                    DataView dv = tb.DefaultView;
                    dv.Sort = "STT asc";
                    tb = dv.ToTable(); 
                }
                dataGridViewGme1_dgvDanhMucGiuong.DataSource = tb;
                if (tb != null && tb.Rows.Count > 0)
                {
                    DataTable dt = (tb.Copy()).AsDataView().ToTable(true, cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP);
                    DataRow drPhong = dt.NewRow();
                    drPhong[cls_BTDKP_BV.col_MaKP] = "-1";
                    drPhong[cls_BTDKP_BV.col_TenKP] = "Tất cả";
                    dt.Rows.Add(drPhong);
                    slbKhoaTimKiem.DataSource = dt;

                }
                col_SttG.DataPropertyName = cls_PG_DanhMucGiuong.col_STT;
                col_TenGiuongG.DataPropertyName = cls_PG_DanhMucGiuong.col_TEN;
                col_IdG.DataPropertyName = cls_PG_DanhMucGiuong.col_ID;
                col_IdPhongG.DataPropertyName = cls_PG_DanhMucGiuong.col_MAPHONG;
                col_TenPhongG.DataPropertyName = "TENPHONG";
                col_MaKPG.DataPropertyName = cls_DanhMucPhong.col_MAKP;
                col_TenKP.DataPropertyName = cls_BTDKP_BV.col_TenKP;
                col_LoaiGiuongG.DataPropertyName = cls_PG_DanhMucGiuong.col_LOAIGIUONG;
                col_TenLoaiG.DataPropertyName = cls_D_DanhMucLoaiGiuong.col_TENLOAI;
                col_Chuan.DataPropertyName = cls_PG_DanhMucGiuong.col_CHUAN;
                _count = dgvDanhMucGiuong.Rows.Count;
                base.TimKiem();
                pnlControl2.Enabled = true;//3

				usc_SelectBoxKhoa.Enabled = usc_SelectBoxPhong.Enabled = usc_SelectBoxLoaiGiuong.Enabled = true;
				lblSoLuong.Text = dgvDanhMucGiuong.Rows.Count.ToString();

                Filter();
                dgvDanhMucGiuong.ClearSelection();
                if (dgvDanhMucGiuong != null && dgvDanhMucGiuong.Rows.Count > 0)
                {
                    foreach (DataGridViewRow ritem in dgvDanhMucGiuong.Rows)
                    {
                        if (ritem.Cells["col_IdG"].Value + "" == _curID)
                        {
                           ritem.Selected = true;
                            return;
                        }
                    }
                }
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

        #region Lọc
        private void Filter()
        {
            string timkiem = "";
            if (!string.IsNullOrEmpty(slbKhoaTimKiem.txtMa.Text) && slbKhoaTimKiem.txtMa.Text != "-1")
                timkiem += string.Format(" AND {1} = '{0}' ", slbKhoaTimKiem.txtMa.Text, "MAKP");
            if (!string.IsNullOrEmpty(slbPhongTimKiem.txtMa.Text) && slbPhongTimKiem.txtMa.Text != "-1")
                timkiem += string.Format(" AND {1} = '{0}' ", slbPhongTimKiem.txtMa.Text, "MAPHONG");

            try
            {
                if (!string.IsNullOrEmpty(timkiem)) (dataGridViewGme1_dgvDanhMucGiuong.DataSource as DataTable).DefaultView.RowFilter = timkiem.Substring(4);
                else (dataGridViewGme1_dgvDanhMucGiuong.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            catch (Exception)
            {

              
            }
            dgvDanhMucGiuong.ClearSelection();
        } 
        #endregion

        #region Lấy tên máy
        private string GetMachine()
        {
            _sql = string.Format("select SYS_CONTEXT('USERENV','IP_ADDRESS')||'+'||Userenv('TERMINAL')||'+'||SYS_CONTEXT('USERENV','MODULE') from dual");
            DataTable tb = _acc.Get_Data(_sql);
            string kq = string.Empty;
            if (tb.Rows.Count>0)
            {
                kq = tb.Rows[0][0].ToString() == string.Empty ? "" : tb.Rows[0][0].ToString();
            }
            return kq;
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
                _lst.Add(cls_DanhMucPhong.col_MAPHONG);
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
                TA_MessageBox.MessageBox.Show("Nhập tên giường", TA_MessageBox.MessageIcon.Information);
                txtTen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(usc_SelectBoxPhong.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Nhập Phòng ", TA_MessageBox.MessageIcon.Information);
                usc_SelectBoxPhong.Focus();
                return false;
            }
            //if (dgvGiaGoiY.Rows.Count == 0)
            //{
            //    TA_MessageBox.MessageBox.Show("Nhập giá cho giường", TA_MessageBox.MessageIcon.Information);
            //    dgvGiaGoiY.Focus();
            //    return false;
            //}
            return true;
        } 
        #endregion

        #region Xóa text để nhập mới
        private void ClearData()
        {
            usc_SelectBoxKhoa.txtTen.Text = usc_SelectBoxPhong.txtTen.Text = usc_SelectBoxLoaiGiuong.txtTen.Text = string.Empty;
            txtTen.Text = txtMaGiuong.Text=string.Empty;
            usc_SelectBoxKhoa.txtTen.Focus();
        } 
        #endregion

        #region Check trung ma giuong
        private bool TrungMaGiuong(string maGiuong)
        {
            _sql = string.Format("select * from {0}.{1} where {2}='{3}'", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAGIUONG, maGiuong);
            DataTable tb = _acc.Get_Data(_sql);
            if (tb.Rows.Count > 0)
            {
                TA_MessageBox.MessageBox.Show(string.Format("Trùng mã giường {0} vui lòng kiểm tra lại!  Lỗi: {1} !!!!",
                                txtMaGiuong.Text, _userError)
                              , TA_MessageBox.MessageIcon.Error);
                txtMaGiuong.Focus();
                return true;
            }
            return false;
        }
		#endregion

		#region Lấy danh sách đối tượng

		private void LoadDanhSachDoiTuong()
		{
			slbDoiTuong.DataSource = _tT.GetDataDanhSachDoiTuong();
		}
		#endregion

		#region Set source cho danh mục Chuẩn
		private void LoadChuan()
        {
            try
            {
                DataTable tb = new DataTable();
                DataColumn cl;
                for (int i = 0; i < 2; i++)
                {
                    cl = new DataColumn();
                    cl.ColumnName = "cl" + i;
                    tb.Columns.Add(cl);
                }
                DataRow dr = tb.NewRow();
                dr[0] = "T";
                dr[1] = "T";
                tb.Rows.Add(dr);
                dr = tb.NewRow();
                dr[0] = "G";
                dr[1] = "G";
                tb.Rows.Add(dr);
				slbChuan.DataSource = tb;
            }
            catch
            {
                return;
            }
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

		#region Lấy ID từ dòng được chọn
		public string GetID()
		{
			string id = null;
			if (dgvDanhMucGiuong.CurrentRow != null)
			{
				var row = dgvDanhMucGiuong.CurrentRow.DataBoundItem as DataRowView;
				if (row != null)
				{
					id = "" + row.Row["ID"];
				}
			}
			return id;

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
            dgvDanhMucGiuong.Enabled = true;
            dgvGiaGoiY.Enabled = false;
            dataGridViewGme1_dgvDanhMucGiuong.Initialize();
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
                    DataRow drPhong = getRowByID(usc_SelectBoxPhong.DataSource, "MA = '" + _thongTinGiuong[6].ToString()+"'");
                    usc_SelectBoxPhong.txtTen.Text = drPhong["TEN"].ToString();
                    txtTen.Focus();
					
                }
            }
            //Lấy giá của giường
            string maLoaiG = "28";
            _tbGiaGiuong=_tT.GetDataGoiYGiaVienPhi(maLoaiG);
            usc_GiaGoiY.DataSource = _tbGiaGiuong;

			//Lấy chuẩn của giường
			LoadChuan();

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
                _sql = _tT.QueryGoiYGia(txtTen.Text, usc_SelectBoxLoaiGiuong.txtMa.Text);
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
                if (!string.IsNullOrEmpty(txtMaGiuong.Text))
                {
                    _lastMa = txtMaGiuong.Text;

                }
                _maPhong = usc_SelectBoxPhong.txtMa.Text;
                _sql = _tT.Queryusc_SelectBoxPhongChange(usc_SelectBoxKhoa.txtMa.Text, _maPhong);
                if (!_isAdd)
                {
                    string[] tmp = txtTen.Text.Split(' ');
                    if (tmp.Length > 1)
                    {
                        string[] tmp2 = tmp[1].Split('-');
                        if (tmp2.Length > 1)
                        {
                            if (!string.IsNullOrEmpty(usc_SelectBoxPhong.txtMa.Text))
                            {
                                txtMaGiuong.Text = usc_SelectBoxPhong.txtMa.Text + "-" + tmp2[1]; 
                                 txtTen.Text = usc_SelectBoxPhong.txtTen.Text + "-" + tmp2[1];
                            }
                           
                            _isChangeMa = true;
                        }
                    }


                }
                DataTable tb = _acc.Get_Data(_sql);
				//if (_isAdd)
				//{
				//    itgSTT.Value = tb.Rows.Count + 1;// cboBHYT.Text = string.Empty;
				//}
				//if (!_isediting)
				//{
				//	tim();
				//}
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

        private void usc_SelectBoxKhoa_HisMaTextChanged(object sender, EventArgs e)
        {
            try
            {
            
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

		private void usc_SelectBoxKhoa_HisSelectChange(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
				{
					ClearList();
					_lst.Add(cls_DanhMucPhong.col_MAPHONG);
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
				//if (!_isediting)
				//{
				//	tim();
				//}
			}
			catch
			{
				return;
			}
		}
		private void tim()
		{
			string timkiem = "";
			if (!string.IsNullOrEmpty(usc_SelectBoxKhoa.txtMa.Text))
				timkiem += string.Format(" OR {1} LIKE '{0}%' OR {1} LIKE '%{0}%' ", usc_SelectBoxKhoa.txtMa.Text, "MAKP");
			if (!string.IsNullOrEmpty(usc_SelectBoxPhong.txtMa.Text))
				timkiem += string.Format(" OR {1} LIKE '{0}%' OR {1} LIKE '%{0}%' ", usc_SelectBoxPhong.txtMa.Text, "MAPHONG");
			if (!string.IsNullOrEmpty(usc_SelectBoxLoaiGiuong.txtMa.Text))
				timkiem += string.Format(" OR {1} LIKE '{0}%' OR {1} LIKE '%{0}%' ", usc_SelectBoxLoaiGiuong.txtMa.Text, "LOAIGIUONG");
            try
            {

                if (!string.IsNullOrEmpty(timkiem)) (dataGridViewGme1_dgvDanhMucGiuong.DataSource as DataTable).DefaultView.RowFilter = timkiem.Remove(0, 3);
                else (dataGridViewGme1_dgvDanhMucGiuong.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            catch (Exception)
            {

                
            }
		}

		private void usc_SelectBoxLoaiGiuong_HisSelectChange(object sender, EventArgs e)
		{
			//if (!_isediting)
			//{
			//	tim();
			//}
		}

		private void his_LabelX26_Click(object sender, EventArgs e)
		{

		}

		private void his_LabelX25_Click(object sender, EventArgs e)
		{

		}

		private void usc_SelectBoxLoaiGiuong_Load(object sender, EventArgs e)
		{

		}

		private void his_LabelX27_Click(object sender, EventArgs e)
		{

		}

		private void txtMaGiuong_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{Tab}");
			}
		}

		private void usc_GiaGoiY_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{Tab}");
			}
		}

		private void txtGiaTien_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{Tab}");
			}
		}

		private void slbChuan_HisSelectChange(object sender, EventArgs e)
		{
		}

		private void slbChuan_KeyDown(object sender, KeyEventArgs e)
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

                string id, sTT, tenGiuong, maLoaiGiuong, tenLoaiGiuong, maKP, tenKP, maPhong, tenPhong, idGiaVP, maGiuong = string.Empty;
                id = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_IdG"].Value.ToString();
                sTT = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_SttG"].Value.ToString();
                tenGiuong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenGiuongG"].Value.ToString();
                maLoaiGiuong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_LoaiGiuongG"].Value.ToString();
                tenLoaiGiuong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenLoaiG"].Value.ToString();
                maKP = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_MaKPG"].Value.ToString();
                tenKP = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenKP"].Value.ToString();
                maPhong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_IdPhongG"].Value.ToString();
                tenPhong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TenPhongG"].Value.ToString();
                idGiaVP = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_IDGIAVP"].Value.ToString();
                maGiuong = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_MaGiuong"].Value.ToString();

                ckChuaSuDung.Checked = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TinhTrang"].Value.ToString() == "4";
                ckGiuongHu.Checked = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_TinhTrang"].Value.ToString() == "3";
                chkGiuongKoDem.Checked = dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["IsDem"].Value + "" == "1";

                txtTen.Text = tenGiuong;
                txtMaGiuong.Text = maGiuong;
                slbChuan.SetTenByMa( dgvDanhMucGiuong.Rows[dgvDanhMucGiuong.CurrentCell.RowIndex].Cells["col_Chuan"].Value.ToString());
                DataRow drGia = getRowByID(_tbGiaGiuong, "ID='" + idGiaVP + "'");
                if (drGia != null)
                {
                    usc_GiaGoiY.SetTenByMa( drGia["ID"].ToString());
                    
                  //  txtGiaTH.Text = drGia["GIA_TH"].ToString();
                }

                itgSTT.Value = 0; itgSTT.Text = string.Empty; itgSTT.Text = sTT; itgSTT.Value = int.Parse(sTT);
                usc_SelectBoxKhoa.txtMa.Text = maKP; usc_SelectBoxKhoa.txtTen.Text = tenKP;
                usc_SelectBoxLoaiGiuong.txtMa.Text = maLoaiGiuong; usc_SelectBoxLoaiGiuong.txtTen.Text = tenLoaiGiuong;
                usc_SelectBoxPhong.txtMa.Text = maPhong; usc_SelectBoxPhong.txtTen.Text = tenPhong;
                txtTen.Text = tenGiuong;
                txtTen.Text = tenGiuong;
                //Kết thúc 
                string command = dgvDanhMucGiuong.Columns[e.ColumnIndex].Name;
                if (command == "col_SuaG")
                {
                    Sua();
                   // usc_SelectBoxKhoa.Enabled = false;
                }
                if (command == "col_XoaG")
                {
                    //180407
                    try
                    {
                        _deleteOneRecord = 1;
                        Xoa();
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
            catch (Exception Ex)
            {


            }
        }


        private void dgvDanhMucGiuong_SelectionChanged(object sender, EventArgs e)
		{
		//	slbDoiTuong.SetTenByMa(dgvDanhMucGiuong[colDoituong.Name, dgvDanhMucGiuong.SelectedRows[0].Index].Value + "");
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

        private void dgvDanhMucGiuong_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        
            e.Cancel = true;
        
    }

        private void txtMaGiuong_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaGiuong.Text))
            {
                _lastMa = txtMaGiuong.Text;

            }
        }

        private void txtMaGiuong_Validated(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_lastMa) && _lastMa != txtMaGiuong.Text)
            {
                _isChangeMa = true;
            }
        }

        private void slbDoiTuong_HisSelectChange(object sender, EventArgs e)
        {
            GetGia();
        }
        private void GetGia()
        {
            if (!string.IsNullOrEmpty(usc_GiaGoiY.txtMa.Text) && !string.IsNullOrEmpty(slbDoiTuong.txtMa.Text) )
            {
                txtGiaTien.Text = double.Parse(_tT.GetGiaTienTuGoiY(usc_GiaGoiY.txtMa.Text, slbDoiTuong.txtMa.Text, _curID,3, usc_SelectBoxLoaiGiuong.txtMa.Text, usc_SelectBoxPhong.txtMa.Text)).ToString("#,##0");
                return;
            }
            txtGiaTien.Text = "";
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

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
               _canrunagain = true;
                 timer1.Stop();
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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frm_DanhMucGiuongImport frm = new frm_DanhMucGiuongImport();
            frm.ShowDialog();
            if (frm._statusCloseForm)
            {
                LoadData();
            }
        }

        private void usc_GiaGoiY_HisKeyUpEnter(object sender, KeyEventArgs e)
        {

        }

        private void usc_GiaGoiY_HisSelectChange(object sender, EventArgs e)
        {
            //        try
            //        {
            //            DataRow dr = getRowByID(_tbGiaGiuong, "ID=" + usc_GiaGoiY.txtMa.Text + "");
            ////txtGiaTien.Text = double.Parse(txtGiaTien.Text).ToString("#,##0");
            //btnLuu.Focus();
            //        }
            //        catch //(Exception)
            //        {

            //            //throw;
            //        }

            //txtGiaTien.DataBindings.Clear();
            //DataTable dt = _tT.GetDataGia(usc_GiaGoiY.txtMa.Text);
            //txtGiaTien.DataBindings.Add("Text", dt, "GIA_TH");
            //txtGiaTien.Text = double.Parse(txtGiaTien.Text).ToString("#,##0");
            GetGia();



        }

		private void usc_GiaGoiY_KeyUp(object sender, KeyEventArgs e)
        {

        }

        #endregion

    }
}
