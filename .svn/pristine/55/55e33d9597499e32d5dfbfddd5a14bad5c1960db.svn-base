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

using System.Text.RegularExpressions;
using DevComponents.DotNetBar.Controls;
using E00_Model;

namespace E11_PhongGiuong
{
    public partial class frm_LoaiPhongImport : frm_DanhMuc
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
        private DataTable _tbLoaiPhong, _tbCoppy, _tbCoppyUdp = null;
        SortedList<string, string> _tbSort = new SortedList<string, string>();
        SortedList<string, string> _tbSort2 = new SortedList<string, string>();
        private string _value = "";
        BindingSource _bsNhap = new BindingSource();
        private bool _isImport = false;

        #endregion

        #region Khởi tạo
        public frm_LoaiPhongImport()
        {
            InitializeComponent();
            _api.KetNoi();
            btnSua.Visible = true;//5
            btnIn.Visible =  false;
        } 
        #endregion

        #region Phương thức protected

        protected override void LoadData()
        {
            TimKiem();
            base.LoadData();
        }

        protected override void Them()
        {
            
            _isAdd = true;
            base.Them();
            ClearData();
            txtTenLoaiPhong.Focus();
            pnlControl2.Enabled = true;//2
            txtTenLoaiPhong.Focus();
        }

        protected override void Sua()
        {
            pnlControl2.Enabled = true;//1
            _isAdd = false;
            dgvDanhMucLoaiPhong_SelectionChanged(null, null);
            base.Sua();
        }

        protected override void Xoa()
        {
            try
            {
                ClearList();
                #region Delete each record
                if (_deleteOneRecord == 1)
                {

                    string id = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_id"].Value.ToString();
                    if (!CheckConCuaDanhMucLoaiPhong(id)) //Edited 300418
                    {
                        _lst2.Add(cls_DanhMucLoaiPhong.col_ID.ToUpper(), id.ToUpper());
                        if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTenLoaiPhong.Text,
                         
                            TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (!_api.Delete(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _lst2, null))
                            {
                                TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                    txtTenLoaiPhong.Text, _userError)
                                 , TA_MessageBox.MessageIcon.Error);
                                dgvDanhMucLoaiPhong.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show(string.Format("Loại phòng này đã được khai báo cho phòng.\n Không thể xóa! {0}. Lỗi: {1} !!!!",
                                    txtTenLoaiPhong.Text, _userError)
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
                        _lst2.Add(cls_DanhMucLoaiPhong.col_ID.ToUpper(), str1.ToString());
                        if (!_api.DeleteAll(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTenLoaiPhong.Text, _userError)
                             , TA_MessageBox.MessageIcon.Error);
                            dgvDanhMucLoaiPhong.Focus();
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
            if (_isAdd)
            {
                #region Insert data
                try
                {
                    if (_isImport)
                    {
                        //Import danh mục loại phòng
                        _sql = string.Empty;
                        int id = int.Parse( _tT.GetTuDongIDDanhMucLoaiPhong());
                        foreach (DataRow item in _tbCoppyUdp.Rows)
                        {
                            _sql += string.Format("insert into {0}.dmloaiphong(ID,STT,MA,TEN,NGAYUD,MACHINEID) values({1},{2},'{3}','{4}',to_date('{5}','dd/MM/yyyy hh24:mi:ss'),'{6}'); ",
                                                _acc.Get_User(),id, item["STT"].ToString(), id,
                                                item["TEN"].ToString(), _tT.GetSysDate().ToString("dd/MM/yyyy HH:mm:ss"),GetMachine()) + Environment.NewLine;
                            id++;
                        }
                        if (!DatabaseV2.Database.Execute_Insert(ref _userError, ref _systemError, _sql))
                        {
                            TA_MessageBox.MessageBox.Show("Lỗi thực thi đa luồng!"
                                         , TA_MessageBox.MessageIcon.Error);
                            return;
                        }
                    }
                    //if (CheckDuLieu())
                    //{
                    //    if (!TrungSoTT())
                    //    {
                           
                    //        ClearList();
                    //        _lst2.Add(cls_DanhMucLoaiPhong.col_MA, _tT.GetTuDongIDDanhMucLoaiPhong());
                    //        _lst2.Add(cls_DanhMucLoaiPhong.col_STT, itgSTT.Value.ToString());
                    //        _lst2.Add(cls_DanhMucLoaiPhong.col_TEN, txtTenLoaiPhong.Text);
                    //        _lst2.Add(cls_DanhMucLoaiPhong.col_NGAYUD, TT.GetSysDate().ToString("yyyy-MM-dd HH:mm:ss"));
                    //        _lst2.Add(cls_DanhMucLoaiPhong.col_MACHINEID, GetMachine());
                    //        _lst.Add(cls_DanhMucLoaiPhong.col_MA);

                    //        if (!_api.Insert(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang,
                    //            _lst2, _lst, _lst))
                    //        {
                    //            TA_MessageBox.MessageBox.Show("Không thể thêm!", "Lỗi", "Đồng ý",
                    //             , TA_MessageBox.MessageIcon.Error);
                    //            return;
                    //        }
                    //        _check = 1;
                    //        LoadData();
                    //    }
                    //    else
                    //    {
                    //        TA_MessageBox.MessageBox.Show("Trùng số thứ tự!", "Lỗi", "Đồng ý",
                    //         , TA_MessageBox.MessageIcon.Error);
                    //        return;
                    //    }
                    //}
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
            //else
            //{
            //    #region Update data
            //    try
            //    {
            //        if (CheckDuLieu())
            //        {
            //                ClearList();
            //                _lst2.Add(cls_DanhMucLoaiPhong.col_STT, itgSTT.Value.ToString());
            //                _lst2.Add(cls_DanhMucLoaiPhong.col_TEN, txtTenLoaiPhong.Text);
            //                string id = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_id"].Value.ToString();
            //                _lst3.Add(cls_DanhMucLoaiPhong.col_ID, id.ToUpper());
            //                if (!_api.Update(ref _userError, ref _systemError, cls_DanhMucLoaiPhong.tb_TenBang, _lst2, new List<string>(), _lst3))
            //                {
            //                    TA_MessageBox.MessageBox.Show(string.Format("Không thể cập nhật {0}. Lỗi: {1} !!!!", txtTenLoaiPhong.Text, _userError),
            //                       
            //                     , TA_MessageBox.MessageIcon.Error);
            //                    txtTenLoaiPhong.Focus();
            //                    return;
            //                }
            //                LoadData();
            //                _check = 1;
            //        }
            //    }
            //    catch
            //    {
            //        return;
            //    }
            //    #endregion
            //    if (_check == 1)
            //    {
            //        base.Luu();
            //    }
            //}
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
                _lst.Add(cls_DanhMucLoaiPhong.col_ID);
                _lst.Add(cls_DanhMucLoaiPhong.col_STT);
                _lst.Add(cls_DanhMucLoaiPhong.col_MA);
                _lst.Add(cls_DanhMucLoaiPhong.col_TEN);
                Dictionary<string, string> lst3 = new Dictionary<string, string>();
                lst3.Add(cls_DanhMucLoaiPhong.col_MA, _chuoiTimKiem);
                lst3.Add(cls_DanhMucLoaiPhong.col_TEN, _chuoiTimKiem);
                _tbLoaiPhong = _tT.GetDataTimKiem(_chuoiTimKiem.ToUpper());
                if (_bsNhap == null)
                {
                    _bsNhap = new BindingSource();
                }
                _bsNhap.DataSource = _tbLoaiPhong;
                dgvDanhMucLoaiPhong.DataSource = _bsNhap;
                _count = dgvDanhMucLoaiPhong.RowCount;

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

        #region Kiểm tra trùng số thứ tự
       
        private bool TrungSoTT()
        {
            _sql = string.Format("select STT from {0}.{1} where STT = {2}", _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang, itgSTT.Value);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        }  

        #endregion

        #region Xóa dữ liệu trước khi thêm
        private void ClearData()
        {
            txtTenLoaiPhong.Text = string.Empty;
            try
            {
                string sql = string.Format("select max(stt)+1 from {0}.{1}", _acc.Get_User(),
                    cls_DanhMucLoaiPhong.tb_TenBang);
                DataTable tb = _acc.Get_Data(sql);
                itgSTT.Value = int.Parse(_tT.GetTuDongSTTDanhMucLoaiPhong());
            }
            catch { itgSTT.Value = 1; }
            itgSTT.Focus();
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

        #region Kiểm tra dữ liệu nhập vào
        private bool CheckDuLieu()
        {
            if (itgSTT.Value == 0 || itgSTT.Value.ToString() == string.Empty)
            {
                TA_MessageBox.MessageBox.Show("Nhập số thứ tự!"
             , TA_MessageBox.MessageIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtTenLoaiPhong.Text))
            {
                TA_MessageBox.MessageBox.Show("Nhập tên loại phòng!"
             , TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        } 
        #endregion

        #region Lấy tên máy
        private string GetMachine()
        {
            _sql = string.Format("select SYS_CONTEXT('USERENV','IP_ADDRESS')||'+'||Userenv('TERMINAL')||'+'||SYS_CONTEXT('USERENV','MODULE') from dual");
            return _acc.Get_Data(_sql).Rows[0][0].ToString();
        } 
        #endregion

        #region Kiểm tra ràng buộc của loại phòng
        private bool CheckConCuaDanhMucLoaiPhong(string idLoaiPhong)
        {
            _sql = string.Format("select  * from {0}.DanhMucPhong where LOAI='{1}'", _acc.Get_User(), idLoaiPhong);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        } 
        #endregion

        #region Hàm bỏ dấu, ký tự đặc biệt
        public string ExportNameControl(string name)
        {
            string NameColumn = "";
            NameColumn = E00_Common.cls_Common.BoDau(name.Replace(" ", "").Replace("-", "")).Trim();
            NameColumn = Regex.Replace(NameColumn, "[(|,.:'?<>+*/)]", "_");
            long n;
            if (NameColumn.Length > 0 && long.TryParse(NameColumn.Substring(0, 1), out n))
            {
                NameColumn = "_" + NameColumn;
            }
            return NameColumn;
        } 
        #endregion

        #endregion

        #region Sự kiện

        private void dgvDanhMucLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _index = dgvDanhMucLoaiPhong.CurrentRow.Index;
                string command = dgvDanhMucLoaiPhong.Columns[e.ColumnIndex].Name;
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
                    string id = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_id"].Value.ToString();
                    string ten = dgvDanhMucLoaiPhong.Rows[_index].Cells["col_TenLoaiPhong"].Value.ToString();
                    DataGridViewRow dr = (DataGridViewRow)dgvDanhMucLoaiPhong.Rows[_index];
                    DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell chks = (DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXCell)dr.Cells[0];
                    if (chks.Selected && chks.FormattedValue.Equals("False") && !_lstCheck.Contains(id))
                    {
                        _lstCheck.Add(id);
                        _lstCheckHoTen.Add(ten);
                    }
                    if (chks.Selected && chks.FormattedValue.Equals("True"))
                    {
                        _lstCheck.Remove(id);
                        _lstCheckHoTen.Remove(ten);
                        _statusCheckAll = 1;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void dgvDanhMucLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _index = dgvDanhMucLoaiPhong.CurrentRow.Index;
                DataTable tb = ((DataTable)dgvDanhMucLoaiPhong.DataSource);
                DataRow row = tb.Rows[_index];
                txtTenLoaiPhong.Text = row["TEN"].ToString();
                itgSTT.Value = int.Parse(row["STT"].ToString());

            }
            catch
            {
                return;
            }
        }

        private void itgSTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        #endregion

        

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            _isImport = true;
            //dgvDanhMucLoaiPhong.DataSource = null;
            //dgvDanhMucLoaiPhong.Refresh();
            PasteClipboard(dgvDanhMucLoaiPhong);
        }

        private void PasteClipboard(DataGridView myDataGridView)
        {
            try
            {
                _isImport = true;
                //_stlV.Clear();
                //_dtCopy = _dtNgang.Clone();
                _tbSort.Clear();
                _tbCoppy = _tbLoaiPhong.Clone();
                _tbCoppyUdp = _tbLoaiPhong.Clone();
                //for (int duyet1 = 0; duyet1 < _tbCoppy.Columns.Count; duyet1++)
                //{
                //    _tbCoppy.Columns[duyet1].DataType = typeof(string);
                //    _tbCoppyUdp.Columns[duyet1].DataType = typeof(string);
                   
                //}
                //foreach (DataGridViewColumn clgv in dgvDanhMucLoaiPhong.Columns)
                //{
                //    clgv.CellTemplate.ValueType = typeof(string);
                //}

                foreach (DataGridViewCell cell in dgvDanhMucLoaiPhong.SelectedCells)
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
                                DataRow dr = _tbLoaiPhong.NewRow();
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
                            dr2["STT"] = dgvDanhMucLoaiPhong.Rows[rowIndex + i - minR].Cells["col_STT"].Value;
                            for (int j = minC; j <= maxC; j++)
                            {
                                _value = _tbSort2[string.Format("{0}-{1}", i.ToString().PadLeft(5, '0'), j.ToString().PadLeft(5, '0'))];
                                if (rowIndex + i - minR >= dgvDanhMucLoaiPhong.Rows.Count - 1)
                                {
                                    if (_bsNhap == null)
                                    {
                                        _bsNhap = new BindingSource();
                                    }
                                    _bsNhap.AddNew();
                                    themmoi = true;
                                }
                                if (themmoi)
                                {
                                    dr1[columnIndex + j - minC] = _value;
                                }
                                else
                                {
                                    dr2[columnIndex + j - minC] = _value;
                                }
                                dgvDanhMucLoaiPhong.Rows[rowIndex + i - minR].Cells[columnIndex + j - minC].ValueType = typeof(string);

                                dgvDanhMucLoaiPhong.Rows[rowIndex + i - minR].Cells[columnIndex + j - minC].Value = _value;
                            }
                            if (themmoi)
                            {
                                _tbCoppy.Rows.Add(dr1);
                            }
                            else
                            {
                                _tbCoppyUdp.Rows.Add(dr2);
                            }
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

        private void dgvDanhMucLoaiPhong_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDanhMucLoaiPhong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode==Keys.V)
            {
                PasteClipboard(dgvDanhMucLoaiPhong);
            }
        }

        private void frm_LoaiPhongImport_Load(object sender, EventArgs e)
        {
            btnSua.Visible = false;
        }

    }
}
