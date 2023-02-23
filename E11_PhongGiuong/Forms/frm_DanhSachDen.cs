using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using E00_Common;
using E00_Model;

namespace E11_PhongGiuong
{
    public partial class frm_DanhSachDen : E00_Base.frm_DanhMuc
    {
        #region Khai báo
        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
        private string _userError, _systemError, _id = string.Empty;
        private List<string> _lstCheck = new List<string>();
        private List<string> _lstCheckHoTen = new List<string>();
        private DataTable _tbDanhSachDuong = new DataTable();
        private string  _maKhoa = string.Empty;
        private string _idPhong = string.Empty;
        public string _maKhoaLoad, _maPhongLoad, _indexOptionLoad = string.Empty;
        public string _tenKhoaLoad, _tenPhongLoad, _tenOptionLoad = string.Empty;
        private string _maBN;
        private bool _isAdd;
        private cls_ThucThiDuLieu _tT = new cls_ThucThiDuLieu();
        private string _sql = string.Empty;
        private int _deleteOneRecord = 0, _statusCheckAll = 0;
       
        #endregion

        #region Khởi tạo
        public frm_DanhSachDen()
        {
            InitializeComponent();
            //lblCount.Visible = false;
            btnXoa.Visible = false;
            btnIn.Visible = btnTienIch.Visible = false;
        }

        #region Khởi tạo có tham số
        public frm_DanhSachDen(string maBN)
        {
            InitializeComponent();
            //lblCount.Visible = false;
            btnXoa.Visible = false;
            _maBN = maBN;
            btnIn.Visible = btnTienIch.Visible = false;
        }  
        #endregion
        #endregion

        #region Phương thức

        #region Phương thức protected

        protected override void LoadData()
        {
            TimKiem();
            base.LoadData();
            pnlControl2.Enabled = true;//2
        }

        protected override void Them()
        {
            _isAdd = true;
           // usc_SelectBoxBenhNhan.txtMa.Text = usc_SelectBoxBenhNhan.txtTen.Text = 
            txtNoiDung.Text = string.Empty;
            base.Them();
            pnlControl2.Enabled = true;//2
            usc_SelectBoxBenhNhan.txtTen.Focus();
            dgvDanhSachDen.Enabled = false;
        }

        protected override void Xoa()
        {
            try
            {
                ClearList();
                #region Delete each record
                if (_deleteOneRecord == 1)
                {
                    string id = dgvDanhSachDen.Rows[dgvDanhSachDen.CurrentCell.RowIndex].Cells["col_id"].Value.ToString();
                    _lst2.Add(cls_BlackList.col_ID.ToUpper(), id.ToUpper());
                    if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + usc_SelectBoxBenhNhan.txtTen.Text,
                     
                        TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!_api.Delete(ref _userError, ref _systemError, cls_BlackList.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                usc_SelectBoxBenhNhan.txtTen.Text, _userError)
                             , TA_MessageBox.MessageIcon.Error);
                            dgvDanhSachDen.Focus();
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
                        _lst2.Add(cls_BlackList.col_ID.ToUpper(), str1.ToString());
                        if (!_api.DeleteAll(ref _userError, ref _systemError, cls_BlackList.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                usc_SelectBoxBenhNhan.txtTen.Text, _userError)
                             , TA_MessageBox.MessageIcon.Error);
                            dgvDanhSachDen.Focus();
                            return;
                        }
                        _lstCheck.Clear(); _lstCheckHoTen.Clear();
                    }
                    #endregion
                }
                LoadData();
                base.Xoa();
            }
            catch
            {

                return;
            }
        }

        protected override void Sua()
        {
            _isAdd = false;
            base.Sua();
            pnlControl2.Enabled = true;//1
            dgvDanhSachDen.Enabled = false;
        }

        protected override void Luu()
        {
            if (!string.IsNullOrEmpty(_maBN))
            {
                if (_isAdd)
                {
                    _lst2.Add(cls_BlackList.col_MABN, _maBN);
                    _lst2.Add(cls_BlackList.col_NOIDUNG, txtNoiDung.Text);
                    _lst.Add(cls_BlackList.col_MABN);
                    List<string> lst3 = new List<string>();
                    lst3.Add(cls_BlackList.col_MABN);
                    if (!_api.Insert(ref _userError, ref _systemError, cls_BlackList.tb_TenBang, _lst2, _lst, lst3))
                    {
                        TA_MessageBox.MessageBox.Show("Bệnh nhân này đã được thêm, không thể thêm!",  TA_MessageBox.MessageIcon.Error);
                        usc_SelectBoxBenhNhan.txtTen.Focus();
                        return;
                    }
                    //TA_MessageBox.MessageBox.Show("Thêm thành công!"  TA_MessageBox.MessageIcon.Information);
                    LoadData();
                    dgvDanhSachDen.Enabled = true;
                }
                else
                {
                    ClearList();
                    string maBN = dgvDanhSachDen.Rows[dgvDanhSachDen.CurrentCell.RowIndex].Cells["col_MaBN"].Value.ToString();
                    _lst2.Add(cls_BlackList.col_NOIDUNG, txtNoiDung.Text);
                    _lst3.Add(cls_BlackList.col_MABN, maBN);
                    if (!_api.Update(ref _userError, ref _systemError, cls_BlackList.tb_TenBang, _lst2, new List<string>(),_lst3))
                    {
                        TA_MessageBox.MessageBox.Show("Không thể cập nhật thông tin!",  TA_MessageBox.MessageIcon.Error);
                        usc_SelectBoxBenhNhan.txtTen.Focus();
                        return;
                    }
                    LoadData();
                    dgvDanhSachDen.Enabled = true;
                }
            }
            base.Luu();
        }

        protected override void BoQua()
        {
            pnlControl2.Enabled = false;//4
            base.BoQua();
            dgvDanhSachDen.Enabled = true;
        }

        

        protected override void Thoat()
        {
            base.Thoat();
        }

        protected override void TimKiem()
        {
            try
            {
                usc_SelectBoxBenhNhan.DataSource = _tT.GetDataTimKiem();
                DataRow dr = _tT.GetDanhSachBlackList(_maBN).Rows[0];
                usc_SelectBoxBenhNhan.txtMa.Text = dr["MABN"].ToString();
                usc_SelectBoxBenhNhan.txtTen.Text = dr["HOTEN"].ToString();
                _lst.Add(cls_BlackList.col_ID);
                _lst.Add(cls_BlackList.col_MABN);
                _lst.Add(cls_BlackList.col_NOIDUNG);
                dgvDanhSachDen.DataSource = _api.Search(ref _userError, ref _systemError, cls_BlackList.tb_TenBang, _acc.Get_User(), -1);
                col_id.DataPropertyName = "ID";
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
            string maBN = dgvDanhSachDen.Rows[dgvDanhSachDen.CurrentCell.RowIndex].Cells["col_MaBN"].Value.ToString();
            string noiDung = dgvDanhSachDen.Rows[dgvDanhSachDen.CurrentCell.RowIndex].Cells["col_NoiDung"].Value.ToString();
            DataRow bN = getRowByID(usc_SelectBoxBenhNhan.DataSource, "MABN='" + maBN + "'");
            if (bN!=null)
            {
                usc_SelectBoxBenhNhan.txtMa.Text = bN["MABN"].ToString();
                usc_SelectBoxBenhNhan.txtTen.Text = bN["HOTEN"].ToString();
                txtNoiDung.Text = noiDung;
            }
            string command = dgvDanhSachDen.Columns[dgvDanhSachDen.CurrentCell.ColumnIndex].Name;
            base.Show_ChiTiet();
        }

        #endregion

        #region Phương thức private

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

        #region Xóa list
        private void ClearList()
        {
            _lst.Clear();
            _lst2.Clear();
            _lst3.Clear();
        }
        #endregion

        #endregion

        #endregion

        #region Sự kiện

        private void frmDanhSachDen_Load(object sender, EventArgs e)
        {
            Them();
            base.Them();
        }

        private void dgvDanhSachDen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Show_ChiTiet();
            if (e.ColumnIndex == 1)
            {
                Sua();
            }
            else if (e.ColumnIndex == 2)
            {
                _deleteOneRecord = 1;
                Xoa();
            }
        }

        #endregion

    }
}
