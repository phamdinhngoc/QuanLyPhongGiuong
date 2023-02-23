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
    public partial class frm_DanhSachBenhNhanChoXuatGiuong : frm_Base
    {
        #region Khai báo biến toàn cục

        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private string _idGiuong = string.Empty;
        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private string _userError, _systemError, _id = string.Empty;
        public bool _status;
        private string _sql = string.Empty;
        public bool _statusCloseForm = false;
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
        private string _lastMaKP = "";
        public List<string> Lstid = new List<string>();


        #endregion

        #region Khởi tạo
        public frm_DanhSachBenhNhanChoXuatGiuong()
        {
            InitializeComponent();
            _api.KetNoi();
        }
        #endregion

        #region Phuowng thuc

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

        #endregion

        #region Sự kiện
        private void frm_DanhSachBenhNhanTaiGiuong_Load(object sender, EventArgs e)
        {

            DataTable tb ;
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "LoadTheoKhoaPhong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);
            string khoa = _tT.GetKPUser(E00_System.cls_System.sys_UserID);
            if ((dtt != null && dtt.Rows.Count > 0)&& !string.IsNullOrEmpty(khoa) && E00_System.cls_System.sys_UserID != "1")

            {
                tb = _tT.GetThongTinBenhNhanChoXuatGiuongfrm(khoa);

                slbKhoa.txtMa.ReadOnly = true;
                slbKhoa.txtTen.ReadOnly = true;
                slbKhoa.btnShow.Enabled = false;
            }
            else
            {
                tb = _tT.GetThongTinBenhNhanChoXuatGiuongfrm();
                slbKhoa.txtMa.ReadOnly = false;
                slbKhoa.txtTen.ReadOnly = false;
                slbKhoa.btnShow.Enabled = true;
               

            }
        
        
            if (tb != null && tb.Rows.Count > 0)
            {
                dgvDSBNDangNam.DataSource = tb;
                DataTable dt = (tb.Copy()).AsDataView().ToTable(true, cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP);
                DataRow drPhong = dt.NewRow();
                drPhong[cls_BTDKP_BV.col_MaKP] = "-1";
                drPhong[cls_BTDKP_BV.col_TenKP] = "Tất cả";
                dt.Rows.Add(drPhong);
                slbKhoa.DataSource = dt;
               
                    if (string.IsNullOrEmpty(_lastMaKP))
                    {
                        slbKhoa.SetTenByMa("-1");
                    }
                    else
                    {
                        slbKhoa.SetTenByMa(_lastMaKP);
                    }
                
                  
               


            }
        }
        #endregion

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "") (dgvDSBNDangNam.DataSource as DataTable).DefaultView.RowFilter = string.Format("{1} LIKE '{0}%' OR {1} LIKE '% {0}%' OR {2} LIKE '{0}%' OR {2} LIKE '% {0}%' OR {3} LIKE '{0}%' OR {3} LIKE '% {0}%' ", txtTimKiem.Text, "HOTEN", "MABN", "TEN");
            else (dgvDSBNDangNam.DataSource as DataTable).DefaultView.RowFilter = "";
        }

        private void slbKhoa_HisSelectChange(object sender, EventArgs e)
        {
            if (slbKhoa.txtMa.Text != "-1")
            {
                _lastMaKP = slbKhoa.txtMa.Text;
            }
            string timkiem = "";
            if (!string.IsNullOrEmpty(slbKhoa.txtMa.Text) && slbKhoa.txtMa.Text != "-1")
                timkiem += string.Format(" AND {1} = '{0}' ", slbKhoa.txtMa.Text, "MAKP");
            if (!string.IsNullOrEmpty(timkiem)) (dgvDSBNDangNam.DataSource as DataTable).DefaultView.RowFilter = timkiem.Substring(4);
            else (dgvDSBNDangNam.DataSource as DataTable).DefaultView.RowFilter = "";
        }

        private void dgvDSBNDangNam_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void dgvDSBNDangNam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dgvDSBNDangNam.Columns[e.ColumnIndex].Name;
            string idGiuong, maBN, tenbn;


            if (name == "col_XuatVien")
            {
                string userError = "", systemError = "";
                Dictionary<string, string> _lst2 = new Dictionary<string, string>();
                _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "XuatGiuong");

                _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
                DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

                if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
                {
                    maBN = dgvDSBNDangNam.Rows[e.RowIndex].Cells["col_MABN"].Value.ToString();
                    tenbn = dgvDSBNDangNam.Rows[e.RowIndex].Cells["col_BN"].Value.ToString();
                   string  ID = dgvDSBNDangNam.Rows[e.RowIndex].Cells["colID"].Value.ToString();
                    
                    if (TA_MessageBox.MessageBox.Show("Cho bệnh nhân " + maBN + " : " + tenbn + " xuất giường ?",

                                TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Cap nhat du lieu cua benh nhan trong table theo doi giuong
                        _idGiuong = dgvDSBNDangNam.Rows[e.RowIndex].Cells["col_IDGIUONG"].Value.ToString();
                        maBN = dgvDSBNDangNam.Rows[e.RowIndex].Cells["col_MABN"].Value.ToString();
                
                        {



                         
                            
                                if (!_tT.UpdateTheoDoiGiuong(ID, Den: _tT.GetSysDate().ToString("dd-MM-yyyy HH:mm:ss"), issudung: false))
                                {
                                    TA_MessageBox.MessageBox.Show("Lỗi cập nhật theo doi giường", TA_MessageBox.MessageIcon.Error);
                                    return;
                                }
                            //_sql = string.Format("update {0}.{1} set {2} = {3} where {4}='{5}' and {6}='{7}'",
                            //    _acc.Get_User(),cls_PG_TheoDoiGiuong.tb_TenBang,cls_PG_TheoDoiGiuong.col_DEN=);
                            #region  update trang thái giường 
                            DataTable dt = _tT.GetDanhSachDatVaDaDuyet(_idGiuong);
                         
                            string _tagetStatus = "0";
                            if (dt.Select("Loai = '1'").Length > 0)
                            {
                                _tagetStatus = "1";

                            }
                            if (dt.Select("Loai = '2'").Length > 0)
                            {
                                _tagetStatus = "2";
                            }
                            if (!_tT.UpdateTinhTrangDanhMucGiuongxUATgIUONG(_idGiuong, _tagetStatus))
                            {
                                TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                                    _userError)
                             , TA_MessageBox.MessageIcon.Error);
                                return;
                            }
                            #endregion

                            TA_MessageBox.MessageBox.Show("Xuất giường bệnh nhân thành công!"
                                                                , TA_MessageBox.MessageIcon.Information);
                                    Lstid.Add(_idGiuong);
                                    _statusCloseForm = true;
                                    frm_DanhSachBenhNhanTaiGiuong_Load(null, null);
                                
                            

                        }
                    }
                }
                else
                {
                    TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền Xuất giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                    return;
                }
            }
        }

        private void frm_DanhSachBenhNhanTaiGiuong_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
