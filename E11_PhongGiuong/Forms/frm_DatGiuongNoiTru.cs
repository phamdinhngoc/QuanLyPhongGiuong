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
using E00_API;

namespace E11_PhongGiuong
{
    public partial class frm_DatGiuongNoiTru : E00_Base.frm_Base
    {

        #region Khai báo

        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        //private string _maKhoa = "";
        //private string _maPhong = "";
        //private string _iDGiuong = "";
        //private string _tenGiuong = "";
        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private DataTable _tbBTDBN = new DataTable();

        //public string MaKhoa
        //{
        //    get
        //    {
        //        return _maKhoa;
        //    }

        //    set
        //    {
        //        _maKhoa = value;
        //    }
        //}

        //public string MaPhong
        //{
        //    get
        //    {
        //        return _maPhong;
        //    }

        //    set
        //    {
        //        _maPhong = value;
        //    }
        //}

        //public string IDGiuong
        //{
        //    get
        //    {
        //        return _iDGiuong;
        //    }

        //    set
        //    {
        //        _iDGiuong = value;
        //    }
        //}

        #region Thuộc tính tên giường
        //Nguyễn Văn Long 21/05/2019
        //public string TenGiuong
        //{
        //    get
        //    {
        //        return _tenGiuong;
        //    }

        //    set
        //    {
        //        _tenGiuong = value;
        //    }
        //}
        #endregion

        //public string MaKhoa { get  _maKhoa; set => _maKhoa = value; }
        //public string MaPhong { get => _maPhong; private set => _maPhong = value; }

        //public string IDGiuong { get => _iDGiuong; private set => _iDGiuong = value; }
        public string TenGiuong { get; set; }
        public string IDGiuong { get; set; }
        public string MaPhong { get; set; }
        public string MaKhoa { get; set; }

        #endregion

        #region Khởi tạo

        #region Khởi tạo không tham số
        public frm_DatGiuongNoiTru()
        {
            InitializeComponent();
        }


        #endregion

        #region Khởi tạo có tham số


        public frm_DatGiuongNoiTru(string makhoa, string magiuong = "", DataTable dtBTDBN = null)
        {
            InitializeComponent();
            MaKhoa = makhoa;
            IDGiuong = magiuong;
            if (!string.IsNullOrEmpty(magiuong)) MaPhong = _tT.GetMaPhongByMaGiuong(magiuong);
            _tbBTDBN = dtBTDBN;
        }


        #endregion

        #endregion

        #region Phương thức

        #region Load dữ liệu

        #endregion

        #region Kiểm tra thông tin nhập vào
        public bool KiemTraThongTin()
        {
            if (string.IsNullOrEmpty(slbKhoa.txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Chọn khoa", TA_MessageBox.MessageIcon.Error);
                slbKhoa.txtTen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(slbPhong.txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Chọn phòng", TA_MessageBox.MessageIcon.Error);
                slbPhong.txtTen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(slbGiuong.txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Chọn giường", TA_MessageBox.MessageIcon.Error);
                slbGiuong.txtTen.Focus();
                return false;
            }

            return true;
        }


        #endregion

        #endregion

        #region Sự kiện

        private void frm_DatGiuongNoiTru_Load(object sender, EventArgs e)
        {
            slbKhoa.DataSource = _tT.GetDanhMucKhoaPhongCoGiuong();
            slbKhoa.HisSelectChange += (s, ee) => {
                if (string.IsNullOrEmpty(slbKhoa.txtMa.Text)) return;
                slbPhong.clear();
                slbGiuong.clear();
                slbPhong.DataSource = _tT.GetDataDanhMucPhongCoGiuong(slbKhoa.txtMa.Text);
                slbGiuong.DataSource = null;
            };

            slbPhong.HisSelectChange += (s, ee) =>
            {
                if (string.IsNullOrEmpty(slbPhong.txtMa.Text)) return;
                slbGiuong.clear();
                slbGiuong.DataSource = _tT.GetDataDanhMucGiuongTrong(slbPhong.txtMa.Text);
            };

            if (!string.IsNullOrEmpty(MaKhoa)) slbKhoa.SetTenByMa(MaKhoa);
            if (!string.IsNullOrEmpty(MaPhong)) slbPhong.SetTenByMa(MaPhong);
            if (!string.IsNullOrEmpty(IDGiuong)) slbGiuong.SetTenByMa(IDGiuong);

            btnQuanLyGiuong.Click += (s, ee) =>
            {
                frm_QuanLyGiuongNoiTru f = new frm_QuanLyGiuongNoiTru(slbKhoa.txtMa.Text, _tbBTDBN);
                f.ShowDialog();
                if(!string.IsNullOrEmpty(f._idgiuongNoiTru))
                {
                    DataTable dttt  = _tT.GetTBGiuong(idGiuong :  f._idgiuongNoiTru);
                    if(dttt != null && dttt.Rows.Count > 0)
                    {
                        slbPhong.SetMaTen(dttt.Rows[0][cls_DanhMucPhong.col_MA] +"", dttt.Rows[0][cls_DanhMucPhong.col_TEN] + "");
                        slbGiuong.SetMaTen(dttt.Rows[0][cls_PG_DanhMucGiuong.col_ID] + "", dttt.Rows[0][cls_PG_DanhMucGiuong.col_TEN] + "");
                        IDGiuong = f._idgiuongNoiTru;
                        #region Lấy tên giường đang chọn
                        //Nguyễn Văn Long 21/05/2019
                        TenGiuong = slbGiuong.txtTen.Text;
                        #endregion
                    }
                }
                else
                {
                    //slbPhong.clear();
                    //slbGiuong.clear();
                }
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            IDGiuong = slbGiuong.txtMa.Text;
            #region Lấy tên giường đang chọn
            //Nguyễn Văn Long 21/05/2019
            TenGiuong = slbGiuong.txtTen.Text;
            #endregion
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
