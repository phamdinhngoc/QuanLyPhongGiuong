using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using E00_Common;

using E00_System;
using E00_Model;
using System.Threading;
using E00_Control;

namespace E11_PhongGiuong
{
    public partial class usc_DragControlEdit : UserControl
    {

        #region Biến toàn cục
        private bool _giuchuot;
        private string _loadtheokp;
        private bool _lastcon = false;
        private bool _term = false;
        private const int _exLenght = 420; //=giuong.Width;
        private Usc_GiuongEdit lasscon;
        private bool _islbltongclick = false;
        private Point coordinate;
        public event System.EventHandler HisChangeLocation;
        public event System.EventHandler waiting;
        public event System.EventHandler endwaiting;
        public event System.EventHandler tinhlai;
        public event System.EventHandler ReloadGiuong;
        public event System.EventHandler Reload;
        public event System.EventHandler ReloadOtherGiuong;
        public event MouseEventHandler HisMouseDown;
        public event MouseEventHandler HisSelectGiuong;
        public event EventHandler getbn;
        public int dorong = -1;
        private bool optionis3cut=false;
        private const bool _LoadGroupPhong = false;

        public bool _usingcmt = true;
        public event MouseEventHandler HisClick;
        private string _tenLoaiGiuong = "";
        string _status;
        DataTable _TableKhoaPhong;
        DataTable _dschoduyet;
        DataTable _slnam;
        DataTable _DSgiuong;
        DataTable _tbTongThe;
        private Point lstlocate;
        private Point currlocate;
        private bool _clickToMove = true;
        private bool _hisAutoSize = false;
        private bool _isShow = true;

        private bool _istinh = false;
        private bool _iswaiting = false;
        private bool _isLoai = false;
        private Panel _panelMain;
        public string _kp = "";
        public string _MaPhong = "";

        private int _lastH = 0;
        private bool _changecolor = true;
        private Color _currcolor;
        private int _key = int.MinValue;
        private int _spaceH = 10;
        private int _spaceW = 10;
        private int left;
        private DataTable _tbBTDBN = new DataTable();
        private int top;
        private bool _bd = false;
        private bool _isXuongDong = false;
        private bool _trangThaiLoc = false;
        private string _colorTrong, _colorDat, _colorCoNguoi, _colorChuaSuDung, _colorHu;

        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private string _userError, _systemError, _id = string.Empty;
        private List<string> _lst = new List<string>();
        private frmProgress objfrmShowProgress;
        private cls_ThucThiDuLieu _tT;
        private List<Usc_GiuongEdit> lstGiuong = new List<Usc_GiuongEdit>();
        private Dictionary<string, ExpandablePanel> lstphong = new Dictionary<string, ExpandablePanel>();
        private Dictionary<string, ExpandablePanel> lstphonghienthi = new Dictionary<string, ExpandablePanel>();
        private List<Usc_GiuongEdit> lstGiuonghienthi = new List<Usc_GiuongEdit>();

        // private List<Usc_GiuongEdit> lstGiuong = new List<Usc_GiuongEdit>();
        private Point _mouseLocate = new Point();
        private System.Windows.Forms.Timer timp;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private int socot = 5;

        public bool _isgiuongNoiTru { get; set; }


        #endregion

        #region Thuộc tính
        public Panel Panel_Main
        {
            set { _panelMain = value; }
        }

        public bool IsLoai
        {
            set { _isLoai = value; }
        }

        public bool IsXuongDong
        {
            set { _isXuongDong = value; }
        }

        public bool IsShow
        {
            set { _isShow = value; }
            get { return _isShow; }
        }

        public bool HisAutoSize
        {
            get { return _hisAutoSize; }
            set
            {
                _hisAutoSize = value;
                this.AutoScroll = !value;
            }
        }

        public int hisSocot
        {
            get { return socot; }
            set { socot = value; }
        }

        public string hisTieuDe
        {
            get { return this.lblTieuDe.Text; }
            set { this.lblTieuDe.Text = value; }
        }

        public LabelX LblTieuDe
        {
            get { return lblTieuDe; }
            set { this.lblTieuDe = value; }
        }
        public bool hisTrangThaiLoc
        {
            get { return _trangThaiLoc; }
            set { _trangThaiLoc = value; }
        }

        public bool hisClickToMove
        {
            get { return _clickToMove; }
            set { _clickToMove = value; }
        }

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }


        public int SpaceH
        {
            get { return _spaceH; }
            set { _spaceH = value; }
        }

        public int SpaceW
        {
            get { return _spaceW; }
            set { _spaceW = value; }
        }

        public DataTable TbBTDBN
        {
            get
            {
                return _tbBTDBN;
            }

            set
            {
                _tbBTDBN = value;
            }
        }

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

        public string TenLoaiGiuong
        {
            get
            {
                return _tenLoaiGiuong;
            }

            set
            {
                _tenLoaiGiuong = value;
            }
        }

        public bool Istinh
        {
            get
            {
                return _istinh;
            }

            set
            {
                _istinh = value;
            }
        }

        public List<Usc_GiuongEdit> LstGiuonghienthi
        {
            get
            {
                return lstGiuonghienthi;
            }

            set
            {
                lstGiuonghienthi = value;
            }
        }

        public List<Usc_GiuongEdit> LstGiuong
        {
            get
            {
                return lstGiuong;
            }

            set
            {
                lstGiuong = value;
            }
        }

        public string Loadtheokp
        {
         

            set
            {
                _loadtheokp = value;
            }
        }

        #endregion

        #region Hàm khởi tạo
        public usc_DragControlEdit()
        {

            InitializeComponent();
        }
        #endregion

        #region Phương thức

        #region public
        private void additem(Usc_GiuongEdit newitem)
        {
            newitem.HisMouseDown += new MouseEventHandler(MouseDown);
            newitem.HisMouseMove += new MouseEventHandler(MouseMove);
            newitem.HisMouseUp += new MouseEventHandler(MouseUp);
            newitem.HisMouseEnter += new EventHandler(MouseEnter);


            LstGiuong.Add(newitem);


        }

        private void additem2(Usc_GiuongEdit newitem)
        {
            newitem.Location = new Point(0, 0);

            try
            {

                LstGiuonghienthi.Add(newitem);
                //   lstGiuong.Add(newitem);

                #region Tìm vị trí mới

                if (LstGiuonghienthi.Count > socot)
                {
                    int maxY = int.MinValue;


                    int maxX = LstGiuonghienthi[socot - 1].Location.X;
                    int locate = -1;
                    for (int i = 0; i < LstGiuonghienthi.Count; i++)
                    {
                        if (maxY <= LstGiuonghienthi[i].Location.Y)
                        {
                            maxY = LstGiuonghienthi[i].Location.Y;
                            locate = i;

                        }
                    }

                    if (LstGiuonghienthi[locate].Location.X >= maxX)
                    {// add ben duoi trai

                        //Chính lại size
                        newitem.Location = new Point(_spaceW, LstGiuonghienthi[locate].Location.Y + LstGiuonghienthi[locate].Height + _spaceH);
                    }
                    else
                    {

                        newitem.Location = new Point(LstGiuonghienthi[locate].Location.X + LstGiuonghienthi[locate].Width + _spaceW, LstGiuonghienthi[locate].Location.Y);
                    }
                }
                else if (LstGiuonghienthi.Count == 1)
                {
                    newitem.Location = new Point(_spaceW, _spaceH);
                }
                else
                {
                    //Chính lại size
                    newitem.Location = new Point(LstGiuonghienthi[LstGiuonghienthi.Count - 2].Location.X + LstGiuonghienthi[LstGiuonghienthi.Count - 2].Width + _spaceW - 0, LstGiuonghienthi[LstGiuonghienthi.Count - 2].Location.Y);
                    //newitem.Location = new Point(lstGiuonghienthi[lstGiuonghienthi.Count - 2].Location.X + lstGiuonghienthi[lstGiuonghienthi.Count - 2].Width + _spaceW, lstGiuonghienthi[lstGiuonghienthi.Count - 2].Location.Y);
                }
                #endregion


                this.pnlMain.Controls.Add(newitem);

            }
            catch (Exception ex)
            {

               // throw;
            }
            // ResizeControl();
            //btnUp_Click(null, null);
        }
        private void MouseEnter(object sender, EventArgs e)
        {

        }
        public void ReloadCOlor()
        {
            try
            {
                foreach (Usc_GiuongEdit giuong in lstGiuong)
                {

                    Color tmp = lblTong.GetColorTinhTrang(giuong.Tinhtrang);
                    giuong.pnlTop.Style.BackColor1.Color = tmp;
                    giuong.pnlTop.Style.BackColor2.Color = tmp;
                }
            }
            catch (Exception)
            {

               
            }
            foreach (Usc_GiuongEdit giuong in lstGiuonghienthi)
            {

                Color tmp = lblTong.GetColorTinhTrang(giuong.Tinhtrang);
                giuong.pnlTop.Style.BackColor1.Color = tmp;
                giuong.pnlTop.Style.BackColor2.Color = tmp;
            }
        }
        public void filltergiuong(string _status = "3", string dkMaPhong = "", string tenloaiphong = "")
        {
            bool ret, ret2, ret3 = false;
            pnlMain.Controls.Clear();
            LstGiuonghienthi.Clear();
            foreach (Usc_GiuongEdit item in LstGiuong)
            {
                ret = ret2 = ret3 = false;
                if (!string.IsNullOrEmpty(dkMaPhong))
                {
                    ret = item.Maphong == dkMaPhong;
                }
                else
                {
                    ret = true;
                }
                if (!string.IsNullOrEmpty(_status))
                {
                    switch (_status)

                    {
                        case "3":
                            ret2 = true;
                            break;
                        case "4":
                            if (item.Tinhtrang == "3" || item.Tinhtrang == "4")
                            {
                                ret2 = true;
                            }
                            break;
                        default:

                            ret2 = item.Tinhtrang == _status;

                            break;
                    }
                    if (_status != "3")
                    {
                        ret2 = item.Tinhtrang == _status;
                    }
                    else
                    {
                        ret2 = true;
                    }
                }
                else
                {
                    ret2 = true;
                }
                if (!string.IsNullOrEmpty(tenloaiphong) && tenloaiphong != "TongSo")
                {

                    ret3 = item.Tenloaiphong == tenloaiphong;


                }

                else
                {
                    ret3 = true;
                }
                if (ret && ret2 && ret3)
                {
                    if (_LoadGroupPhong)
                    {
                        lstGiuonghienthi.Add(item);
                    }
                    else
                    { 
                      additem2(item);
                    }
                }
            }
            if (_LoadGroupPhong)
            {
                additemtopn();
            }
          
            if (tenloaiphong != "TongSo")
            {
                lblTong.CapNhatSoLuongGiuongTheoTungTrangThai(tenloaiphong);

            }
            else
            {
                lblTong.CapNhatSoLuongGiuongTheoTungTrangThai();
            }

        }

        private void additemtopn()
        {
            //lstphong.Clear();
          
            lstphonghienthi.Clear();
            foreach (Usc_GiuongEdit item in LstGiuonghienthi)
            {
                ExpandablePanel exphong;
                if (!lstphonghienthi.TryGetValue(item.Maphong, out exphong))
                {
                     exphong = new ExpandablePanel();


                    exphong.AutoScroll = true;
                    exphong.SuspendLayout();
                 
                    exphong.TitleText = item.TenPhong;
                    exphong.CanvasColor = System.Drawing.SystemColors.Control;
                    exphong.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                   
                    exphong.Size = new System.Drawing.Size(_exLenght, 200);
                    //exphong.Size = new System.Drawing.Size(200, 26);
                    //exphong.Expanded = false;
                    //exphong.ExpandedBounds = new System.Drawing.Rectangle(12, 12, _exLenght, 200);
                    exphong.ExpandOnTitleClick = true;
                    exphong.HideControlsWhenCollapsed = true;
                    exphong.Location = new System.Drawing.Point(12, 12);
                    exphong.Name = item.Maphong;
                    
                  
                    exphong.Style.Alignment = System.Drawing.StringAlignment.Center;
                    exphong.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    exphong.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                    exphong.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
                    exphong.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                    exphong.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                    exphong.Style.GradientAngle = 90;
                    exphong.TabIndex = 0;
                    exphong.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
                    exphong.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    exphong.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                    exphong.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
                    exphong.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                    exphong.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                    exphong.TitleStyle.GradientAngle = 90;
                    exphong.ExpandedChanging += Exphong_ExpandedChanged;
                    exphong.ResumeLayout(false);
                    exphong.Tag = new List<Usc_GiuongEdit>();
                    pblmain_add(exphong);

                }
                addgiuong(exphong, item);
            }
            //ExpandablePanel exphong2;
            //foreach (Usc_GiuongEdit item in lstGiuonghienthi)
            //{
            //    if ()
            //    {
                    
                   
            //    }
            //}


        }
        private void addgiuong(ExpandablePanel exmain, Usc_GiuongEdit giuong)
        {
            List<Usc_GiuongEdit> lstGiuong = exmain.Tag as List<Usc_GiuongEdit>;
            int socotg = 3;

            lstGiuong.Add(giuong);
            #region Tìm vị trí mới
            giuong.Location = new System.Drawing.Point(0, 0);
            if (lstGiuong.Count > socotg)
            {
                int maxY = int.MinValue;


                int maxX = lstGiuong[socotg - 1].Location.X;
                int locate = -1;
                int valuetmp;
                for (int i = 0; i < lstGiuong.Count; i++)
                {
                    valuetmp = lstGiuong[i].Location.Y;
                    if (maxY <= valuetmp)
                    {
                        maxY = valuetmp;
                        locate = i;

                    }
                }
                Usc_GiuongEdit giuongtmp = lstGiuong[locate];
                if (giuongtmp.Location.X >= maxX)
                {// add ben duoi trai


                    //Chính lại size
                    giuong.Location = new Point(_spaceW, giuongtmp.Location.Y + giuongtmp.Height + _spaceH);
                }
                else
                {

                    giuong.Location = new Point(giuongtmp.Location.X + giuongtmp.Width + _spaceW, giuongtmp.Location.Y);
                }
            }
            else if (lstGiuong.Count == 1)
            {
                giuong.Location = new Point(10, 30);
            }
            else
            {
                Usc_GiuongEdit giuongtmp = lstGiuong[lstGiuong.Count - 2];
                //Chính lại size
                giuong.Location = new Point(giuongtmp.Location.X + giuongtmp.Width + _spaceW - 0, giuongtmp.Location.Y);
                //newitem.Location = new Point(lstGiuonghienthi[lstGiuonghienthi.Count - 2].Location.X + lstGiuonghienthi[lstGiuonghienthi.Count - 2].Width + _spaceW, lstGiuonghienthi[lstGiuonghienthi.Count - 2].Location.Y);
            }
            #endregion


            exmain.Controls.Add(giuong);
        }
        private void pblmain_add(Control crl)
        {
            int ChieuCaoTang = 0;
            ExpandablePanel exphong = crl as ExpandablePanel;

            lstphonghienthi.Add(exphong.Name + "", exphong);

            #region Tìm vị trí mới
            exphong.Location = new System.Drawing.Point(0, 0);
            if (lstphonghienthi.Count > socot)
            {
                int maxY = int.MinValue;


                int maxX = lstphonghienthi.ElementAt(socot - 1).Value.Location.X;
                int locate = -1;
                int valuetmp;
                for (int i = 0; i < lstphonghienthi.Count; i++)
                {
                    valuetmp = lstphonghienthi.ElementAt(i).Value.Location.Y;
                    if (maxY <= valuetmp)
                    {
                        maxY = valuetmp;
                        locate = i;

                    }
                }
                ExpandablePanel exphongtmp = lstphonghienthi.ElementAt(locate).Value;
                if (exphongtmp.Location.X >= maxX)
                {// add ben duoi trai

                   
                    //Chính lại size
                    exphong.Location = new Point(_spaceW, exphongtmp.Location.Y + exphongtmp.Height + _spaceH);
                    ChieuCaoTang += exphong.Height + _spaceH;
                }
                else
                {

                    exphong.Location = new Point(exphongtmp.Location.X + exphongtmp.Width + _spaceW, exphongtmp.Location.Y);
                }
            }
            else if (lstphonghienthi.Count == 1)
            {
                ChieuCaoTang = 20+exphong.Height + _spaceH;
                exphong.Location = new Point(_spaceW, _spaceH);
            }
            else
            {
                ExpandablePanel exphongtmp = lstphonghienthi.ElementAt(lstphonghienthi.Count - 2).Value;
                //Chính lại size
                exphong.Location = new Point(exphongtmp.Location.X + exphongtmp.Width + _spaceW - 0, exphongtmp.Location.Y);
                //newitem.Location = new Point(lstGiuonghienthi[lstGiuonghienthi.Count - 2].Location.X + lstGiuonghienthi[lstGiuonghienthi.Count - 2].Width + _spaceW, lstGiuonghienthi[lstGiuonghienthi.Count - 2].Location.Y);
            }
            #endregion


            this.pnlMain.Controls.Add(exphong);
            this.Height = this.Height + ChieuCaoTang;
        }
        private void Exphong_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            if (e.NewExpandedValue)
            {
                (sender as ExpandablePanel).BringToFront();
                (sender as ExpandablePanel).ExpandedBounds = new System.Drawing.Rectangle(12, 12, _exLenght, 200);
            }
            else
            {
                (sender as ExpandablePanel).Size = new Size(200, 26);
            }
        }

        public bool ReloadDataGIuong(DataTable tbDanhSachDuong, DataTable dschoduyet, DataTable slnam, DataTable tbTongTheG, usc_lableTong usc_lableTong1, string idgiuong)
        {
            _DSgiuong = tbDanhSachDuong;
            _dschoduyet = dschoduyet;
            _slnam = slnam;
            _tbTongThe = tbTongTheG;

            Usc_GiuongEdit giuong = null;
            foreach (Usc_GiuongEdit uscg in LstGiuong)
            {
                if (uscg.ID == int.Parse(idgiuong))
                {
                    giuong = uscg; break;
                }
            }
            DataRow item = TT.GetTBGiuong(idGiuong: idgiuong).Rows[0];
            if (giuong != null)
            {
                string tinhtrang;
                Color tmp;
                DataTable tb;
                int sLBNDD;
                string slBNChoDuyet;
                Color color;
                tinhtrang = item["tinhtrang"] + "";
                giuong.Tinhtrang = item["tinhtrang"].ToString();
                #region Cập nhật màu và trạng thái cho từng giường trong phòng
                try
                {
                    tmp = lblTong.GetColorTinhTrang(tinhtrang);
                    giuong.pnlTop.Style.BackColor1.Color = tmp;
                    giuong.pnlTop.Style.BackColor2.Color = tmp;
                }
                catch (Exception ex)
                {
                }




                #endregion

                #region Check dữ liệu table theodoigiuong

                tb = new DataTable();



                #region Thông tin giuong dat

                //tbTongThe = _acc.Get_Data(new cls_ThucThiDuLieu().QueryGetGiuongTuTableDatGiuong());
                if (_tbTongThe.Rows.Count > 0)
                {
                    try
                    {
                        tb = _tbTongThe.Select("IDGIUONG = '" + giuong.ID + "' AND MAKP = '" + _kp + "' ").CopyToDataTable();
                    }
                    catch (Exception ex)
                    {
                    }

                    if (giuong.Tinhtrang == tinhtrang)
                    {
                        giuong.Databenhnhan = tb;
                    }


                }

                #endregion

                #endregion
                //Thêm loại giường

                #region Get ma mau loai giuong va set cho giuong
                string maMauLoaiPhong = "";
                string[] maMauDetail = null;
                color = new Color();
                try
                {
                    maMauLoaiPhong = item[cls_DanhMucLoaiPhong.col_MAUSAC].ToString();
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
                    giuong.lblLoaiGiuongColor.BackColor = color;
                }
                catch
                {
                }
                #endregion

                #region Set tên loại giường

                try
                {
                    giuong.lblLoaiGiuongTen.Text = (item[cls_D_DanhMucLoaiGiuong.col_TENLOAI].ToString());
                }
                catch
                {
                }

                #endregion

                #region Set số lượng bệnh nhân chờ vào giường

                try
                {
                    slBNChoDuyet = _dschoduyet.Select(cls_PG_TheoDoiGiuong.col_IDGIUONG + " = '" + giuong.ID + "' ").Length + ""; //_tT.GetDanhSachChoDuyet(giuong.ID.ToString()).Rows.Count.ToString();
                    giuong.lblLoaiGiuongColor.Text = slBNChoDuyet == "0" ? "" : slBNChoDuyet;
                    giuong.lblLoaiGiuongColor.TextAlignment = StringAlignment.Center;
                    giuong.Tag += "," + giuong.lblLoaiGiuongColor.Text;
                }
                catch
                {
                }

                #endregion

                #region Set số lượng bệnh nhân đang nằm tại giường

                sLBNDD = _slnam.Select(cls_PG_TheoDoiGiuong.col_IDGIUONG + " = '" + giuong.ID + "' ").Length; //_tT.GetSoLuongNguoiNamTaiGiuong(giuong.ID.ToString());
                giuong.lblGiuong.Text = giuong.TenGiuong + " - (" + sLBNDD.ToString() + ")";

                #endregion
                tinh();
                return true;
            }
            return false;

        }

        public void LoadDaTa(string status, DataTable datatable, DataTable dschoduyet, DataTable slnam, DataTable tt, usc_lableTong lbt1 = null)
        {
            _status = status;
            _DSgiuong = datatable;
            _dschoduyet = dschoduyet;
            _slnam = slnam;
            _tbTongThe = tt;
            if (lbt1 != null)
            {
                lblTong.frmLoad(lbt1);
            }
            else
            { lblTong.frmLoad(); }
            tinh();


        }
        public void tinh()
        {
            #region Get all giuong theo tung khoa
            if (string.IsNullOrEmpty(_MaPhong) || _MaPhong == "0")
            {
                try
                {
                    _TableKhoaPhong = _DSgiuong.Select("MAKP=" + _kp).CopyToDataTable();
                }
                catch
                {
                    _TableKhoaPhong = null;
                }
            }
            else
            {
                try
                {

                    _TableKhoaPhong = _DSgiuong.Select("MAKP='" + _kp + "' and MAPHONG = '" + _MaPhong + "'").CopyToDataTable();
                }
                catch
                {
                    _TableKhoaPhong = null;
                }
            }
            #endregion
            lblTong.DataTinh = _TableKhoaPhong;
            //  ThreadStart ts = new ThreadStart(load);


        }
        public void load(bool isthr = true,bool isupclick=true)
         {

            if (_istinh)
            {
                return;
            }
            _istinh = true;
            if (!isthr && waiting != null && !_iswaiting)
            {
                waiting(null, null);
                _iswaiting = true;
            }
            if (_isShow)
            {
                if (_loadtheokp != _kp)
                {
                    btnUp_Click(null, null);
                }
          
            }
            pnlMain.Controls.Clear();
            LstGiuong.Clear();
            LstGiuonghienthi.Clear();
            int sizeGiuong = 0, sizePanel = 0, sizeDoRong = 0;
            string dong1, tinhtrang, dong2, dong3;
            Usc_GiuongEdit giuong;
            Color tmp;
            DataTable tb;
            DateTime dt, nS, nV;
            int sLBNDD, iDuyet;
            string ngaySinh, ngayVao, slBNChoDuyet;
            Color color;
            TimeSpan tongNgay;

            if (_TableKhoaPhong != null)
            {
                DataTable dtkhoaphong = null;
                try
                {
                    
                    switch (_status)
                    {
                        case "3":

                            dtkhoaphong = _TableKhoaPhong.Select(lblTong.Where).CopyToDataTable();
                            break;
                        case "4":
                            dtkhoaphong = _TableKhoaPhong.Select(" TINHTRANG = '3' or TINHTRANG = '4'").CopyToDataTable();
                            break;
                        default:
                            dtkhoaphong = _TableKhoaPhong.Select(" TINHTRANG = '" + _status + "' ").CopyToDataTable();
                            break;
                    }
                    DataView dv = dtkhoaphong.DefaultView;
                    dv.Sort = "STT asc";
                    dtkhoaphong = dv.ToTable();
                }
                catch (Exception)
                {

                    
                }
                #region loadgiuong
                try
                {
                  
                    #region Tất cả các giường trống trong tất cả các khoa phòng

                    #region Tất cả các giường trong tất cả các khoa phòng
                    foreach (DataRow item in dtkhoaphong.Rows)
                    {
                        giuong = new Usc_GiuongEdit();
                        giuong.Name = item["id"] + "";
                        giuong.Panel_Main = _panelMain;
                        //	vitri = item["vitri"].ToString();
                        //toado = vitri.Split(',');
                        tinhtrang = item["tinhtrang"] + "";
                        dong1 = item["Ten"] + "";//Ẩn tên phòng trên thanh tiêu đề
                        giuong.TenGiuong = dong1.ToUpper();//Gan tên giường
                        giuong.Tinhtrang = item["tinhtrang"].ToString();
                        giuong.Maphong = item["MAPHONG"].ToString();
                        giuong.TenPhong = item["TENPHONG"].ToString();
                        giuong.Tenloaiphong = item["TENLOAIPHONG"].ToString();
                        giuong.Tag = item["id"].ToString() + "," + item["ten"].ToString() + "," + item["tinhtrang"].ToString() + "";//Gán id,tên, tình trạng để chuyển giường và đặt giường.
                        try
                        {
                            giuong.ID = int.Parse(item["id"] + "");//gan id giuong
                        }
                        catch
                        {
                        }
                        #region Cập nhật màu và trạng thái cho từng giường trong phòng
                        try
                        {
                            tmp = lblTong.GetColorTinhTrang(tinhtrang);
                            giuong.pnlTop.Style.BackColor1.Color = tmp;
                            giuong.pnlTop.Style.BackColor2.Color = tmp;
                        }
                        catch (Exception ex)
                        {
                        }




                        #endregion

                        #region Check dữ liệu table theodoigiuong

                        tb = new DataTable();



                        #region Thông tin giuong dat

                        //tbTongThe = _acc.Get_Data(new cls_ThucThiDuLieu().QueryGetGiuongTuTableDatGiuong());
                        if (_tbTongThe.Rows.Count > 0)
                        {
                            try
                            {
                                tb = _tbTongThe.Select("IDGIUONG = '" + giuong.ID + "' AND MAKP = '" + _kp + "' ").CopyToDataTable();
                            }
                            catch (Exception ex)
                            {
                            }

                            if (giuong.Tinhtrang == tinhtrang)
                            {
                                giuong.Databenhnhan = tb;
                            }


                        }

                        #endregion

                        #endregion

                        giuong.HisMouseUp += new MouseEventHandler(giuong_HisMouseUp);
                        giuong.lblGiuong.MouseHover += new System.EventHandler(lblGiuong_MouseHover);
                        giuong.lblGiuong.DoubleClick += new System.EventHandler(lblGiuong_DoubleClick);
                        giuong.HienThiThongTinBN += Giuong_HienThiThongTinBN;
                        giuong.AnThongTinBN += Giuong_AnThongTinBN; 
                        //Thêm loại giường
                        string[] kq = giuong.Tag.ToString().Split(',');

                        giuong.Tag += "," + item["MALOAIPHONG"].ToString() + "," + item["MAKP"].ToString() + "," + item["MAPHONG"].ToString();

                        #region Get ma mau loai giuong va set cho giuong
                        string maMauLoaiPhong = "";
                        string[] maMauDetail = null;
                        color = new Color();
                        try
                        {
                            maMauLoaiPhong = item[cls_DanhMucLoaiPhong.col_MAUSAC].ToString();
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
                            giuong.lblLoaiGiuongColor.BackColor = color;
                        }
                        catch
                        {
                        }
                        #endregion

                        #region Set tên loại giường

                        try
                        {
                            giuong.lblLoaiGiuongTen.Text = (item[cls_D_DanhMucLoaiGiuong.col_TENLOAI].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region Set số lượng bệnh nhân chờ vào giường

                        try
                        {
                            slBNChoDuyet = _dschoduyet.Select(cls_PG_TheoDoiGiuong.col_IDGIUONG + " = '" + giuong.ID + "' ").Length + ""; //_tT.GetDanhSachChoDuyet(giuong.ID.ToString()).Rows.Count.ToString();
                            giuong.lblLoaiGiuongColor.Text = slBNChoDuyet == "0" ? "" : slBNChoDuyet;
                            giuong.lblLoaiGiuongColor.TextAlignment = StringAlignment.Center;
                            giuong.Tag += "," + giuong.lblLoaiGiuongColor.Text;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region Set số lượng bệnh nhân đang nằm tại giường

                        sLBNDD = _slnam.Select(cls_PG_TheoDoiGiuong.col_IDGIUONG + " = '" + giuong.ID + "' ").Length; //_tT.GetSoLuongNguoiNamTaiGiuong(giuong.ID.ToString());
                        giuong.lblGiuong.Text = giuong.TenGiuong + " - (" + sLBNDD.ToString() + ")";

                        #endregion

                        #region Thêm giường vào panel
                        giuong.pa = this;
                        if (sizeDoRong == 0)
                        {
                            //Size của giường
                            if (_LoadGroupPhong)
                            {
                                sizeGiuong = _exLenght + 10;
                            }
                            else
                            {
                                sizeGiuong = giuong.Width + 10;
                            }
                              sizePanel = dorong > 0 ? dorong : Screen.PrimaryScreen.WorkingArea.Width;
                            sizeDoRong = 15;
                            iDuyet = 15;
                            while (sizePanel < sizeGiuong * iDuyet)
                            {
                                sizeDoRong -= 1;
                                iDuyet--;
                            }
                        }
                        this.hisSocot = sizeDoRong;
                        //
                        this.additem(giuong);
                        //if (!string.IsNullOrEmpty(vitri) && !TrungViTri(vitri))
                        //{
                        //	giuong.Location = new Point(int.Parse(toado[0].ToString()), int.Parse(toado[1].ToString()));
                        //	this.additem(giuong);
                        //	_lisGiuong.Add(giuong);
                        //	demgiuong++;
                        //}
                        //else
                        //{
                        //	this.additem(giuong);
                        //	demgiuong++;
                        //}
                        #endregion
                    }

                    #endregion

                    //SetSoLuongChoKhoaPhong(usd1, status, dkMaKhoa, dkMaPhong, _tenLoaiGiuongLink);
                    #endregion
                }
                catch (Exception ex)
                {
                }
                #endregion

                #region Load Phong
              

                #endregion
            }




        }

        private void Giuong_AnThongTinBN(object sender, EventArgs e)
        {
            Usc_GiuongEdit tmp = sender as Usc_GiuongEdit;
            if (tmp.Parent is ExpandablePanel)
            {
                ExpandablePanel extmp = tmp.Parent as ExpandablePanel;
                //Point cursorPos = Cursor.Position;
                //cursorPos.X = tmp.lblGiuong.PointToScreen(coordinate).X;
                //cursorPos.Y = tmp.lblGiuong.PointToScreen(coordinate).Y;
                //Cursor.Position = cursorPos;

                //extmp.Width = extmp.Width - tmp.KichthuocTang.Width;
                //extmp.Height = extmp.Height - tmp.KichthuocTang.Height;
                //   this.pnlMain.Height = this.pnlMain.Height - tmp.KichthuocTang.Height;
            }



            //   this.Width = this.Width - tmp.KichthuocTang.Width;

        }

        private void Giuong_HienThiThongTinBN(object sender, EventArgs e)
        {
            Usc_GiuongEdit tmp = sender as Usc_GiuongEdit;
            if (tmp.Parent is ExpandablePanel)
            
            {
                ExpandablePanel extmp = tmp.Parent as ExpandablePanel;
                Point cursorPos =  Cursor.Position;
                 coordinate = new Point(Cursor.Position.X - tmp.lblGiuong.PointToScreen(new Point(0,0)).X, Cursor.Position.Y - tmp.lblGiuong.PointToScreen(new Point(0, 0)).Y);
                extmp.ScrollControlIntoView(tmp);
                cursorPos.X = tmp.lblGiuong.PointToScreen(coordinate).X;
                cursorPos.Y = tmp.lblGiuong.PointToScreen(coordinate).Y;
                Cursor.Position = cursorPos;
              
                //extmp.Width = extmp.Width + tmp.KichthuocTang.Width;
                //extmp.Height = extmp.Height + tmp.KichthuocTang.Height;
                //   _panelMain.ScrollControlIntoView(extmp);
                //   this.pnlMain.Height = this.pnlMain.Height + tmp.KichthuocTang.Height;
            }
            
       
      //      this.Width = this.Width + tmp.KichthuocTang.Width;

        }

        void giuong_HisMouseUp(object sender, MouseEventArgs e)
        {
            if (_isgiuongNoiTru) return;
            Usc_GiuongEdit giuong = (Usc_GiuongEdit)sender;
            if (e.Button == MouseButtons.Right)
            {
                if (_usingcmt)
                {
                    string idGiuongChonChuyen = giuong.Tag.ToString();
                    string tinhtrang = giuong.Tinhtrang;
                   
                    
                    if (tinhtrang != "4" )
                    {
                      //  xuấtGiườngToolStripMenuItem.Visible = false;
                     //   cậpNhậtTrạngTháiToolStripMenuItem.Visible = true;
                        if (tinhtrang == "5")
                        {
                      //      xuấtGiườngToolStripMenuItem.Visible = true;
                          //  cậpNhậtTrạngTháiToolStripMenuItem.Visible = false;
                        }
                        
                        string userError = "", systemError = "";
                        Dictionary<string, string> _lst2 = new Dictionary<string, string>();
                        _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "LoadTheoKhoaPhong");
                        _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
                        DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);
                        string khoa = TT.GetKPUser(E00_System.cls_System.sys_UserID);
                        if ((dtt != null && dtt.Rows.Count > 0) && !string.IsNullOrEmpty(khoa) && E00_System.cls_System.sys_UserID != "1")

                        {

                            if (_kp != khoa) 
                            {
                                return;
                            }
                        }

                        cmt_MenuGiuong.Tag = giuong;
                        cmt_MenuGiuong.Show(Cursor.Position);
                        cmt_MenuGiuong.Show();
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (HisSelectGiuong != null)
                    {
                        HisSelectGiuong(sender, e);
                    }
                }
            }
        }
        void lblGiuong_DoubleClick(object sender, EventArgs e)
        {
            LabelX lb = (LabelX)sender;
            Control control = lb.Parent.Parent;
            Usc_GiuongEdit giuong = control as Usc_GiuongEdit;
            if (giuong != null)
            {
                if (_isgiuongNoiTru)
                {
                    frm_QuanLyGiuongNoiTru f = this.FindForm() as frm_QuanLyGiuongNoiTru;
                    if(f!=null)
                    {
                        f._idgiuongNoiTru = giuong.ID.ToString();
                        f.Close();
                    }
                }
                else
                {
                    frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(giuong.ID + "", giuong.Tinhtrang + "", 0, makp: _kp);
                    frm.ShowDialog();
                    if (frm._status)
                    {
                        if (ReloadGiuong != null)
                        {
                            foreach (string item in frm.LstOfAffect)
                            {
                                ReloadGiuong(item, e);
                            }
                        }
                        // throw new Exception("load lại 1 gường chưa làm @");
                        //int slBNChoDuyet = _tT.GetDanhSachChoDuyet(giuong.ID.ToString()).Rows.Count;
                        //int slBNDangNam = _tT.GetThongTinBenhNhanDaVaoGiuong(giuong.ID.ToString()).Rows.Count;
                        //if (slBNDangNam == 0)
                        //{
                        //	if (slBNChoDuyet == 0)
                        //	{
                        //		//Cập nhật trạng thái giường về 0(trống)
                        //		_tT.UpdateTinhTrangDanhMucGiuong(giuong.ID.ToString(), "0");
                        //	}
                        //	else
                        //	{
                        //		//Cập nhật trạng thái giường về 1(đặt)
                        //		_tT.UpdateTinhTrangDanhMucGiuong(giuong.ID.ToString(), "1");
                        //	}
                        //}
                        //LoadValueGiuong(giuong);
                    }
                }
            }
        }

        void lblGiuong_MouseHover(object sender, EventArgs e)
        {
            LabelX dr = (LabelX)sender;
            Control control = dr.Parent.Parent.Parent.Parent;
            usc_DragControlEdit dragControl = control as usc_DragControlEdit;
            Control controlGiuong = dr.Parent.Parent;
            Usc_GiuongEdit giuong = controlGiuong as Usc_GiuongEdit;
            giuong.lblGiuong.BringToFront();
            giuong.lblLoaiGiuongColor.BringToFront();
        }
        public Dictionary<string, Usc_GiuongEdit> GetDic()
        {
            return this.LstGiuong.ToDictionary(i => i.Key, i => i);
        }

        //public void BaoDong(bool bd)
        //{

        //    if (bd)
        //    {
        //        if (!_bd)
        //        {
        //            _bd = bd;
        //            _currcolor = pnlTop.BackColor;
        //        }
        //    }
        //    else
        //    {
        //        _bd = bd;
        //        bool tatbaodong = false;
        //        for (int i = 0; i < lstGiuong.Count; i++)
        //        {
        //            if (lstGiuong[i].BaoDong)
        //            {

        //                lstGiuong[i].hideNotification();
        //                tatbaodong = false;
        //                break;
        //            }
        //            else
        //            {
        //                tatbaodong = true;
        //            }
        //        }
        //        if (tatbaodong)
        //        {
        //            pnlTop.BackColor = _currcolor;
        //        }
        //    }


        //}

        #endregion

        #region private

        #region Khởi tạo color
        private void KhoiTaoColor()
        {
            if (_acc.Get_Data(string.Format("select * from {0}.{1}", _acc.Get_User(), cls_DanhMucColorTrangThai.tb_TenBang)).Rows.Count == 0)
            {
                _colorTrong = "255,0,0,0";
                _colorDat = "255,255,128,64";
                _colorCoNguoi = "255,0,128,255";
                _colorChuaSuDung = "255,192,192,192";
                _colorHu = "255,192,192,192";
                List<string> lst = new List<string>();
                lst.Add(_colorTrong);
                lst.Add(_colorDat);
                lst.Add(_colorCoNguoi);
                lst.Add(_colorChuaSuDung);
                lst.Add(_colorHu);
                for (int i = 1; i <= 5; i++)
                {
                    string sql = string.Format("insert into {0}.{1}(id,loai,color)"
                                + " values(" + i + ",'" + i + "','" + lst[i - 1].ToString() + "')", _acc.Get_User(), cls_DanhMucColorTrangThai.tb_TenBang);
                    if (!_acc.Execute_Data(ref _userError, ref _systemError, sql))
                    {
                        TA_MessageBox.MessageBox.Show("Không thể ins màu giường!", TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                }
            }
        }
        #endregion

        #region Load màu trạng thái

        #endregion

        private void ResizeControl()
        {
            int locateY = -1;
            int locateX = -1;
            if (!_hisAutoSize)
            {
                return;
            }

            int maxY = int.MinValue;
            int maxX = int.MinValue;


            for (int i = 0; i < LstGiuong.Count; i++)
            {
                if (maxY <= LstGiuong[i].Location.Y)
                {
                    maxY = LstGiuong[i].Location.Y - 10;
                    locateY = i;
                }
                if (maxX <= LstGiuong[i].Location.X)
                {
                    maxX = LstGiuong[i].Location.X;
                    locateX = i;
                }
            }

            this.pnlMain.Height = LstGiuong[locateY].Location.Y + (LstGiuong[locateY].Height) + _spaceH;
            this.pnlMain.Width = LstGiuong[locateY].Location.X + LstGiuong[locateY].Width + _spaceW;
        }

        #endregion
        public Usc_GiuongEdit GetGiuong(int _idgiuong)
        {
            foreach (Usc_GiuongEdit item in LstGiuong)
            {
                if (item.ID == _idgiuong)
                {
                    return item;
                }
            }
            return null;
        }
        public void ClearControl()
        {
            LstGiuong.Clear();
            this.pnlMain.Controls.Clear();
        }

        #endregion

        #region Sự kiện

        private void MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                //Usc_GiuongEdit giuong = (Usc_GiuongEdit)sender;
                //if (e.Button == System.Windows.Forms.MouseButtons.Left && _clickToMove && _giuchuot)
                //{
                //    mylocate = new Point(giuong.Location.X > 0 ? giuong.Location.X : 0, giuong.Location.Y > 0 ? giuong.Location.Y : 0);

                //    _giuchuot = false;
                //    currlocate = new Point(giuong.Location.X + (giuong.Width / 2), giuong.Location.Y + (giuong.Height / 2));
                //    giuong.Location = lstlocate;

                //    _term = true;
                //}
            }
            catch (Exception)
            {

            }
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                Usc_GiuongEdit giuong = (Usc_GiuongEdit)sender;
                if (HisMouseDown != null)
                {
                    HisMouseDown(sender, e);
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left && _clickToMove)
                {

                    giuong.BringToFront();
                    lstlocate = giuong.Location;
                    lasscon = giuong;

                    _giuchuot = true;
                    _mouseLocate = e.Location;
                    left = giuong.Location.X;
                    top = giuong.Location.Y;
                    _lastcon = true;

                }

            }
            catch
            {
                return;
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Usc_GiuongEdit giuong = (Usc_GiuongEdit)sender;
                // string[] tinhtrang = giuong.Tag.ToString().Split(',');
                //if (_giuchuot)
                //{

                //    this.Invalidate();
                //    giuong.Location = new Point(left + (e.X - _mouseLocate.X), top + (e.Y - _mouseLocate.Y));


                //    //ClearList();
                //    //_lst2.Add(cls_PG_DanhMucGiuong.col_VITRI, giuong.Left + ", " + giuong.Top);
                //    //_lst3.Add(cls_PG_DanhMucGiuong.col_ID, tinhtrang[0].ToString());
                //    //if (!api.Update(ref userError, ref systemError, cls_PG_DanhMucGiuong.tb_TenBang, lst2, _lst3))
                //    //{
                //    //    TA_MessageBox.MessageBox.Show("Không update được!", "Thông báo");
                //    //    return;
                //    //}
                //}
            }
            catch
            {
                return;
            }
        }

        private void pnlMain_MouseEnter(object sender, EventArgs e)
        {
            _lastcon = false;

            if (lasscon != null && _term)
            {
                int min = int.MaxValue;

                int minid = 0;
                int minX = 0;
                int miny = 0;
                for (int i = 0; i < LstGiuong.Count; i++)
                {
                    int GiuongY = LstGiuong[i].Location.Y + (LstGiuong[i].Height / 2);
                    int GiuongX = LstGiuong[i].Location.X + (LstGiuong[i].Width / 2);
                    if (i == 19)
                    { };

                    int value = (Math.Abs((GiuongY - currlocate.Y)) + Math.Abs((GiuongX - currlocate.X)));

                    if (value <= min && value != 0)
                    {

                        min = value;
                        minid = i;
                    }


                }



                if (true)
                {

                }
                int GiuongY1 = LstGiuong[minid].Location.Y + (LstGiuong[minid].Height / 2);
                int GiuongX1 = LstGiuong[minid].Location.X + (LstGiuong[minid].Width / 2);



                if (LstGiuong[minid].Location.X + LstGiuong[minid].Width < currlocate.X && LstGiuong[minid].Location.Y < currlocate.Y && currlocate.Y < LstGiuong[minid].Location.Y + LstGiuong[minid].Height)
                {     //đặt bên phải
                    lasscon.Location = new Point(LstGiuong[minid].Location.X + LstGiuong[minid].Width + _spaceW, LstGiuong[minid].Location.Y);
                    if (HisChangeLocation != null)
                    {
                        HisChangeLocation(lasscon, e);
                    }

                }
                else if (LstGiuong[minid].Location.Y + LstGiuong[minid].Height < currlocate.Y && LstGiuong[minid].Location.X < currlocate.X && currlocate.X < LstGiuong[minid].Location.X + LstGiuong[minid].Width)
                {     //đặt bên dưới
                    lasscon.Location = new Point(LstGiuong[minid].Location.X, LstGiuong[minid].Location.Y + LstGiuong[minid].Height + _spaceH);
                    if (HisChangeLocation != null)
                    {
                        HisChangeLocation(lasscon, e);
                    }
                    ResizeControl();
                }
                else if (LstGiuong[minid].Location.X > currlocate.X && LstGiuong[minid].Location.Y < currlocate.Y && currlocate.Y < LstGiuong[minid].Location.Y + LstGiuong[minid].Height)
                {       //đặt bên trái
                    if ((LstGiuong[minid].Location.X - LstGiuong[minid].Width - _spaceW) >= 0)
                    {
                        lasscon.Location = new Point(LstGiuong[minid].Location.X - LstGiuong[minid].Width - _spaceW, LstGiuong[minid].Location.Y);
                        if (HisChangeLocation != null)
                        {
                            HisChangeLocation(lasscon, e);
                        }
                    }
                }
                else if (LstGiuong[minid].Location.Y > currlocate.Y && LstGiuong[minid].Location.X < currlocate.X && currlocate.X < LstGiuong[minid].Location.X + LstGiuong[minid].Width)
                {     //đặt bên tren
                    if ((LstGiuong[minid].Location.Y - LstGiuong[minid].Height - _spaceH) >= 0)
                    {

                        lasscon.Location = new Point(LstGiuong[minid].Location.X, LstGiuong[minid].Location.Y - LstGiuong[minid].Height - _spaceH);
                        if (HisChangeLocation != null)
                        {
                            HisChangeLocation(lasscon, e);
                        }
                    }
                }

                //lstGiuong[lasscon.ID] = null;
                //lasscon.ID = lstGiuong.Count;
                //lstGiuong.Add(lasscon);



                _term = false;
            }
        }

        private void usc_DragControl_Load(object sender, EventArgs e)
        {

           
            if (_isShow)
            {
                btnUp.Visible = true;
                btnDown.Visible = false;

            }
            if (_isLoai)
            {
                btnDown_Click(sender, e);
            }
            this.FindForm().Deactivate += new System.EventHandler(usc_DragControl_Deactive);
            this.FindForm().Activated += new System.EventHandler(usc_DragControl_eactive);
            


        }

        private void usc_DragControl_Deactive(object sender, EventArgs e)
        {
            //for (int i = 0; i < lstGiuong.Count; i++)
            //{
            //    if (lstGiuong[i].BaoDong)
            //    {

            //        lstGiuong[i].showNotification();
            //    }
            //}
        }

        private void usc_DragControl_eactive(object sender, EventArgs e)
        {
            for (int i = 0; i < LstGiuong.Count; i++)
            {
                if (LstGiuong[i]._frm != null)
                {
                    LstGiuong[i].hideNotification();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usc_GiuongEdit controlgiong = new Usc_GiuongEdit("Giuong test", new DateTime(2017, 1, 11, 10, 11, 10), "BN test ", "TenBN test", "11/11/1111", "Test ");

            additem(controlgiong);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _lastcon = false;
            _term = false;
        }

        private void pnlMain_Resize(object sender, EventArgs e)
        {
            if (_hisAutoSize)
            {
             
                this.Width = pnlMain.Width;
            }
        }

        public void btnUp_Click(object sender, EventArgs e)
        {
            btnUp.Visible = false;
            btnDown.Visible = true;
            if (this.pnlMain.Height > 5)
            {
                _lastH = this.Height; 
            }
            this.Height = pnlTop.Height + 1;
          //  pnlMain.Height = 1;
            _isShow = false;
        }

        public void btnDown_Click(object sender, EventArgs e)
        {

            load(false);

            if (LstGiuong.Count == 0)
            {

              //  btnDown_Click(sender, e);
                return;
            }
            if (waiting != null)
            {
                if (_iswaiting)
                {
                    endwaiting(sender, e);
                    _iswaiting = false;
                }
            }

            try
            {
                if (pnlMain.Controls.Count == 0 && !_islbltongclick)
                {
                    pnlMain.Controls.Clear();
                    LstGiuonghienthi.Clear();
                    foreach (Usc_GiuongEdit item in LstGiuong)
                    {
                        if (_LoadGroupPhong)
                        {
                            lstGiuonghienthi.Add(item);
                        }
                        else
                        {
                            additem2(item);
                        }
                    }
                    if (_LoadGroupPhong)
                    {
                        additemtopn();
                    }
                   
                    
                }

                if (_lastH!=0)
                {
                    this.Height = _lastH; 
                }
                else
                {
                    if (!_LoadGroupPhong)
                    {
                        this.Height = 300;
                    }
                }

                _islbltongclick = false;
            }
            catch (Exception ex)
            {

            }

            btnUp.Visible = true;
            btnDown.Visible = false;
          
            _isShow = true;
            if (LstGiuong.Count > 0)
            {
                if (_panelMain != null)
                {
                   _panelMain.ScrollControlIntoView(LstGiuong[0]);
                }
            }


        }
        public void showpn()
        {
            try
            {
                if (pnlMain.Controls.Count == 0 && !_islbltongclick)
                {
                    pnlMain.Controls.Clear();
                    LstGiuonghienthi.Clear();
                    foreach (Usc_GiuongEdit item in LstGiuong)
                    {
                        if (_LoadGroupPhong)
                        {
                            lstGiuonghienthi.Add(item);
                        }
                        else
                        {
                            additem2(item);
                        }
                    }
                    if (_LoadGroupPhong)
                    {
                        additemtopn();
                    }
                   
                  
                }
                _islbltongclick = false;
            }
            catch (Exception ex)
            {

            }
            btnUp.Visible = true;
            btnDown.Visible = false;
            this.Height = _lastH;
            _isShow = true;
            if (LstGiuong.Count > 0)
            {
                if (_panelMain != null)
                {
                    _panelMain.ScrollControlIntoView(LstGiuong[0]);
                }
            }
        }
        private void StartProgress(String strStatusText)
        {
            objfrmShowProgress = new frmProgress();
            objfrmShowProgress.lblText.Text = strStatusText;
            ShowProgress();
        }
        private void CloseProgress()
        {
            Thread.Sleep(200);
            objfrmShowProgress.Invoke(new Action(objfrmShowProgress.Close));
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
        private void lblTieuDe_Resize(object sender, EventArgs e)
        {
            this.pnlTop.Height = lblTieuDe.Height;
        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTieuDe_DoubleClick(object sender, EventArgs e)
        {

        }

        private void usc_DragControl_SizeChanged(object sender, EventArgs e)
        {

            //if (this.FindForm().WindowState == System.Windows.Forms.FormWindowState.Minimized)
            //{
            //    for (int i = 0; i < lstGiuong.Count; i++)
            //    {
            //        if (lstGiuong[i].BaoDong)
            //        {

            //            lstGiuong[i].showNotification();
            //        }
            //    }
            //}
            //else
            //{

            //        for (int i = 0; i < lstGiuong.Count; i++)
            //        {
            //            if (lstGiuong[i]._frm != null)
            //            {
            //                lstGiuong[i].hideNotification();
            //            }
            //        }

            //}
        }


        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void lblTong_LBLDoubleClick(object sender, EventArgs e)
        {
            try
            {
                _islbltongclick = true;
                his_LabelX sourceControl = (his_LabelX)sender;
                string Loai = sourceControl.Name;
                if (string.IsNullOrEmpty(_tenLoaiGiuong))
                {
                    pnlMain.Controls.Clear();
                    LstGiuonghienthi.Clear();
                    foreach (Usc_GiuongEdit item in LstGiuong)
                    {
                        if (item.Tinhtrang == "5")
                        {

                        }
                        switch (Loai)

                        {
                            case "3":
                                if (_LoadGroupPhong)
                                {
                                    lstGiuonghienthi.Add(item);
                                }
                                else
                                {
                                    additem2(item);
                                }
                                break;
                            case "4":
                                if (item.Tinhtrang == "3" || item.Tinhtrang == "4")
                                {
                                    if (_LoadGroupPhong)
                                    {
                                        lstGiuonghienthi.Add(item);
                                    }
                                    else
                                    {
                                        additem2(item);
                                    }
                                }
                                break;
                            default:
                                if (item.Tinhtrang == Loai)
                                {
                                    if (_LoadGroupPhong)
                                    {
                                        lstGiuonghienthi.Add(item);
                                    }
                                    else
                                    {
                                        additem2(item);
                                    }
                                }
                                break;
                        }


                    }
                    if (_LoadGroupPhong)
                    {
                        additemtopn();
                    }


                    btnDown_Click(sender, e);
                }
                else
                {
                    if (_tenLoaiGiuong == "TongSo")
                    {
                        filltergiuong(Loai, _MaPhong, "");

                    }
                    else
                    {
                        filltergiuong(Loai, _MaPhong, _tenLoaiGiuong);
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        public void lblclick(string Loai)
        {
            pnlMain.Controls.Clear();
            LstGiuonghienthi.Clear();
            foreach (Usc_GiuongEdit item in LstGiuong)
            {
                if (item.Tinhtrang == "5")
                {

                }
                switch (Loai)

                {
                    case "3":
                        if (_LoadGroupPhong)
                        {
                            lstGiuonghienthi.Add(item);
                        }
                        else
                        {
                            additem2(item);
                        }
                        break;
                    case "4":
                        if (item.Tinhtrang == "3" || item.Tinhtrang == "4")
                        {
                            if (_LoadGroupPhong)
                            {
                                lstGiuonghienthi.Add(item);
                            }
                            else
                            {
                                additem2(item);
                            }
                        }
                        break;
                    default:
                        if (item.Tinhtrang == Loai)
                        {
                            if (_LoadGroupPhong)
                            {
                                lstGiuonghienthi.Add(item);
                            }
                            else
                            {
                                additem2(item);
                            }
                        }
                        break;
                }

            }
            if (_LoadGroupPhong)
            {
                additemtopn();
            }
           
           
        }

        private void pnlMain_ControlRemoved(object sender, ControlEventArgs e)
        {
            LstGiuonghienthi.Clear();
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
            String[] tmp = lblTieuDe.Text.Split('-');

            if (tmp.Length > 1)
            {
                lblTieuDe.Text = tmp[0].Trim();
                _tenLoaiGiuong = "";
            }
            pnlMain.Controls.Clear();
            LstGiuonghienthi.Clear();
            foreach (Usc_GiuongEdit item in LstGiuong)
            {
                if (_LoadGroupPhong)
                {
                    lstGiuonghienthi.Add(item);
                }
                else
                {
                    additem2(item);
                }
            }
            if (_LoadGroupPhong)
            {
                additemtopn();
            }
          
          
            lblTong.DataTinh = _TableKhoaPhong;
            btnDown_Click(sender, e);

        }

        private void pnlTop_DoubleClick(object sender, EventArgs e)
        {
            if (_isShow)
            {
                btnUp_Click(sender, e);
            }
            else
            {
                btnDown_Click(sender, e);
            }
        }

        private void lblTieuDe_BackColorChanged(object sender, EventArgs e)
        {
            pnlTop.BackColor = lblTieuDe.BackColor;
        }

        private void đặtGiườngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "DatGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region Đặt giường
                //Có thay đổi về giao diện
                try
                {

                  //  MessageBox.Show(giuong.Maphong+":"+ giuong.Maphong);
                        frm_NhapThongTinDatGiuongBenhNhan frm = new frm_NhapThongTinDatGiuongBenhNhan(_kp, giuong.Maphong,
                      giuong.Name, _tbBTDBN);
                        frm.ShowDialog();

                        if (frm._status == true)
                        {
                           
                            int sldat = 0;
                            int.TryParse( TT.GetDanhSachChoDuyet(giuong.ID+"").Rows.Count.ToString(), out sldat);
                            giuong.lblLoaiGiuongColor.Text = (sldat) + "";
                            giuong.lblLoaiGiuongColor.TextAlignment = StringAlignment.Center;
                            if (giuong.Tinhtrang == "0")
                            {
                                _tT.UpdateTinhTrangGiuong(giuong.ID + "");
                                giuong.Tinhtrang = "1";
                                lblTong.ChangeTinhTrang("0", "1", 1);
                                if (tinhlai != null)
                                {
                                    tinhlai("0:1:1", e);

                                }

                                try
                                {
                                    giuong.pnlTop.Style.BackColor1.Color = lblTong.GetColorTinhTrang(giuong.Tinhtrang); //usc_lableTong1.GetColorTinhTrang("1");//Color.FromArgb(int.Parse(colorDat[0].ToString()), int.Parse(colorDat[1].ToString()), int.Parse(colorDat[2].ToString()), int.Parse(colorDat[3].ToString()));
                                }
                                catch
                                {
                                }
                                try
                                {
                                    giuong.pnlTop.Style.BackColor2.Color = lblTong.GetColorTinhTrang(giuong.Tinhtrang);//Color.FromArgb(int.Parse(colorDat[0].ToString()), int.Parse(colorDat[1].ToString()), int.Parse(colorDat[2].ToString()), int.Parse(colorDat[3].ToString()));
                                }
                                catch
                                {
                                }
                            }
                        }
                    

                }
                catch (Exception ex)
                {
                    return;
                }
                #endregion
            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền đặt giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }


        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            //Có thay đổi về giao diện
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "DuyetGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region Duyệt giường5
             
                    frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(giuong.Name, giuong.Tinhtrang + "",0,loaibn:0,makp:_kp);
                    frm.ShowDialog();
                    if (frm._status)
                    {
                        if (ReloadGiuong != null)
                        {
                            foreach (string item in frm.LstOfAffect)
                            {
                                ReloadGiuong(item, e);
                            }
                        }
                     
                    }
                
                #endregion
            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền Duyệt giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void hủyĐặtGiườngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "HuyDuyetGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region Hủy Duyệt giường

               
                    frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(giuong.Name, giuong.Tinhtrang + "", 3, makp: _kp);
                    frm.ShowDialog();
                    if (frm._status == true)
                    {
                        if (ReloadGiuong != null)
                        {
                            foreach (string item in frm.LstOfAffect)
                            {
                                ReloadGiuong(item, e);
                            }
                        }


                    } 
                
                
                #endregion
            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền hủy duyệt giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void pnlMain_MouseHover(object sender, EventArgs e)
        {
            Panel pn = (Panel)sender;
            Control c = pn.Parent;
        }

        private void lblTatCa_Click(object sender, EventArgs e)
        {
            _trangThaiLoc = true;
            LabelX lb = (LabelX)sender;
            Control cl = lb.Parent.Parent;

        }

        private void lblTrong_Click(object sender, EventArgs e)
        {
            _trangThaiLoc = true;
        }

        private void lblDatTruoc_Click(object sender, EventArgs e)
        {
            _trangThaiLoc = true;
        }

        private void lblCoNguoi_Click(object sender, EventArgs e)
        {
            _trangThaiLoc = true;
        }

        private void lblChuaSuDung_Click(object sender, EventArgs e)
        {
            _trangThaiLoc = true;
        }
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Panel pnlTop;
        private E00_Control.his_BalloonTip his_BalloonTip1;
        public usc_lableTong lblTong;
        private System.Windows.Forms.ContextMenuStrip cmt_MenuGiuong;
        private System.Windows.Forms.ToolStripMenuItem đặtGiườngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hủyĐặtGiườngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem khaiBáoGiườngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem choVàoDanhSáchĐenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnDuyet;
        private DevComponents.DotNetBar.LabelX lblTieuDe;
        private DevComponents.DotNetBar.ButtonX btnUp;
        private DevComponents.DotNetBar.ButtonX btnDown;

        private void hủyĐặtGiườngToolStripMenuItem1_Click(object sender, EventArgs e)
        {


            Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            string userError = "", systemError = "";
            //Có thay đổi về giao diện string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "HuyDatGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                
                    frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(giuong.Name, giuong.Tinhtrang + "",1, makp: _kp);
                    frm.ShowDialog();
                    if (frm._status == true)
                    {

                        if (ReloadGiuong != null)
                        {
                            foreach (string item in frm.LstOfAffect)
                            {
                                ReloadGiuong(item, e);
                            }
                        }

                        //int sldat = 0;
                        //int.TryParse(giuong.lblLoaiGiuongColor.Text, out sldat);
                        //sldat = sldat - frm._numOfUd;
                        //giuong.lblLoaiGiuongColor.Text = (sldat > 0 ? sldat : 0) + "";
                        //giuong.lblLoaiGiuongColor.TextAlignment = StringAlignment.Center;

                        //if (giuong.Tinhtrang == "1")
                        //{
                        //    giuong.Tinhtrang = "0";

                        //    lblTong.ChangeTinhTrang("1", "0", frm._numOfUd);
                        //    if (tinhlai != null)
                        //    {
                        //        tinhlai("1:0:" + frm._numOfUd, e);

                        //    }

                        //    try
                        //    {
                        //        giuong.pnlTop.Style.BackColor1.Color = lblTong.GetColorTinhTrang(giuong.Tinhtrang); //usc_lableTong1.GetColorTinhTrang("1");//Color.FromArgb(int.Parse(colorDat[0].ToString()), int.Parse(colorDat[1].ToString()), int.Parse(colorDat[2].ToString()), int.Parse(colorDat[3].ToString()));
                        //    }
                        //    catch
                        //    {
                        //    }
                        //    try
                        //    {
                        //        giuong.pnlTop.Style.BackColor2.Color = lblTong.GetColorTinhTrang(giuong.Tinhtrang);//Color.FromArgb(int.Parse(colorDat[0].ToString()), int.Parse(colorDat[1].ToString()), int.Parse(colorDat[2].ToString()), int.Parse(colorDat[3].ToString()));
                        //    }
                        //    catch
                        //    {
                        //    }
                        //    //Cap nhat lai trang thai cua giuong



                        //}
                        //else
                        //{// cập nhật bệnh nhân


                        //}



                    }
               

            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền Hủy đặt giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            {
                Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
                //Có thay đổi về giao diện
                string userError = "", systemError = "";
                Dictionary<string, string> _lst2 = new Dictionary<string, string>();
                _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "ChuyenGiuong");
                _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
                DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

                if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
                {
                    #region Chuyển giường
                   
                        frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(giuong.Name, giuong.Tinhtrang + "",4, makp: _kp);
                        frm.ShowDialog();
                        if (frm._status == true)
                        {
                            if (ReloadGiuong != null)
                            {
                                foreach (string item in frm.LstOfAffect)
                                {
                                    ReloadGiuong(item, e); 
                                }
                            }
                            
                            {
                                DataTable tb = _acc.Get_Data(TT.QueryGetGiuongTuTableDatGiuong(idgiuong: giuong.ID.ToString()));
                                //#region SET THÔNG TIN BỆNH NHÂN CÓ TRẠNG THÁI BẰNG 1 Edited
                                giuong.Databenhnhan = tb;
                            }
                            giuong.lblGiuong.Text = giuong.TenGiuong + " - (" + giuong.SlBenhnhan + ")";

                        }


                 
                    #endregion
                }
                else
                {
                    TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền Chuyển giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                    return;
                }
            }
        }

        private void hủyChuyểnGiườngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "HuyChuyenGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region Hủy Chuyển Giường

                #endregion

            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền hủy chuyển giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void xuấtGiườngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "XuatGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region Xuất Giường
                frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(giuong.Name, giuong.Tinhtrang + "",5, makp: _kp);
                frm.ShowDialog();
                if (frm._status == true)
                {

                    if (ReloadGiuong != null)
                    {
                        foreach (string item in frm.LstOfAffect)
                        {
                            ReloadGiuong(item, e);
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

        private void khaiBáoGiườngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "CapNhatGiaGiuong");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region Cập nhật giá Giường
                frm_DanhMucGiuong f = new frm_DanhMucGiuong();
                f.ShowDialog();
                #endregion

            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền cập nhật giá giường!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void choVàoDanhSáchĐenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "ChoVaoDanhSachDen");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                #region cho vào ds đen
                try
                {
                    Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;

                    if (giuong.Tinhtrang == "2" || giuong.Tinhtrang == "1")
                    {
                        frm_DanhSachDen frm = new frm_DanhSachDen();
                        frm.ShowDialog();


                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show("Gường không có người!",
                            TA_MessageBox.MessageIcon.Information);

                    }
                }
                catch
                {
                    return;
                }
                #endregion

            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền cho vào danh sách đen!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void cậpNhậtTrạngTháiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usc_GiuongEdit giuong = cmt_MenuGiuong.Tag as Usc_GiuongEdit;
            string userError = "", systemError = "";
            Dictionary<string, string> _lst2 = new Dictionary<string, string>();
            _lst2.Add(cls_PhanQuyenMoi.col_MAMENU, "CapNhatTrangThai");
            _lst2.Add(cls_PhanQuyenMoi.col_MANGUOIDUNG, E00_System.cls_System.sys_UserID);
            DataTable dtt = _api.Search(ref userError, ref systemError, cls_PhanQuyenMoi.tb_TenBang, dicLike: _lst2);

            if ((dtt != null && dtt.Rows.Count > 0) || E00_System.cls_System.sys_UserID == "1")
            {
                frm_DanhSachChoDuyetTheoKhoaPhong frm = new frm_DanhSachChoDuyetTheoKhoaPhong(giuong.Name, giuong.Tinhtrang + "", 6, makp: _kp);
                frm.ShowDialog();
                if (frm._status == true)
                {

                    if (ReloadGiuong != null)
                    {
                        foreach (string item in frm.LstOfAffect)
                        {
                            ReloadGiuong(item, e);
                        }
                    }
                }
                //#region Cập nhật trạng thái
                //if (!TT.UpdateTinhTrangDanhMucGiuongxUATgIUONG(giuong.ID+"", "5"))
                //{
                //    TA_MessageBox.MessageBox.Show(string.Format("Lỗi cập nhật trạng thái {0} !!!!",
                //        _userError)
                // , TA_MessageBox.MessageIcon.Error);
                //    return;
                //}
                //if (ReloadGiuong != null)
                //{

                //        ReloadGiuong(giuong.ID + "", e);

                //}
                //#endregion

            }
            else
            {
                TA_MessageBox.MessageBox.Show(string.Format("User {0} không có quyền cập nhật trạng thái!", E00_System.cls_System.sys_UserID), TA_MessageBox.MessageIcon.Error);
                return;
            }
        }

        private void lblTong_reload(object sender, EventArgs e)
        {
            if (Reload!=null)
            {
                Reload(sender, e);
            }
        }

     

      

        private void tmp_Tick(object sender, EventArgs e)
        {
          
            timp.Stop();
        }
        public void reloadgiuong()
        {
            if (_isShow)
            {
                _istinh = false;
                load(false);
                btnDown_Click(null, null); 
            }
            else
            {
                _istinh = false;
                load(true);
            }
        }
        private void lblTong_reloadGiuong(object sender, EventArgs e)
        {
            reloadgiuong();
            if (ReloadOtherGiuong!=null)
            {
                ReloadOtherGiuong(this, e);
            }
        }

        private System.Windows.Forms.ToolStripMenuItem hủyĐặtGiườngToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xuấtGiườngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hủyChuyểnGiườngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cậpNhậtTrạngTháiToolStripMenuItem;
        private System.ComponentModel.IContainer components = null;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usc_DragControlEdit));
            this.btnUp = new DevComponents.DotNetBar.ButtonX();
            this.btnDown = new DevComponents.DotNetBar.ButtonX();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTieuDe = new DevComponents.DotNetBar.LabelX();
            this.lblTong = new E11_PhongGiuong.usc_lableTong();
            this.his_BalloonTip1 = new E00_Control.his_BalloonTip();
            this.cmt_MenuGiuong = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.đặtGiườngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hủyĐặtGiườngToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDuyet = new System.Windows.Forms.ToolStripMenuItem();
            this.hủyĐặtGiườngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hủyChuyểnGiườngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cậpNhậtTrạngTháiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xuấtGiườngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.khaiBáoGiườngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.choVàoDanhSáchĐenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timp = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop.SuspendLayout();
            this.cmt_MenuGiuong.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUp.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnUp.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btnUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageFixedSize = new System.Drawing.Size(15, 15);
            this.btnUp.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btnUp.Location = new System.Drawing.Point(910, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 22);
            this.btnUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUp.TabIndex = 4;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDown.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnDown.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btnDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.ImageFixedSize = new System.Drawing.Size(15, 15);
            this.btnDown.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btnDown.Location = new System.Drawing.Point(942, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 22);
            this.btnDown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDown.TabIndex = 2;
            this.btnDown.Visible = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 22);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(974, 0);
            this.pnlMain.TabIndex = 3;
            this.pnlMain.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.pnlMain_ControlRemoved);
            this.pnlMain.MouseEnter += new System.EventHandler(this.pnlMain_MouseEnter);
            this.pnlMain.MouseHover += new System.EventHandler(this.pnlMain_MouseHover);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            this.pnlMain.Resize += new System.EventHandler(this.pnlMain_Resize);
            // 
            // pnlTop
            // 
            this.pnlTop.AllowDrop = true;
            this.pnlTop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Controls.Add(this.lblTong);
            this.pnlTop.Controls.Add(this.btnUp);
            this.pnlTop.Controls.Add(this.btnDown);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(974, 22);
            this.pnlTop.TabIndex = 4;
            this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTop_Paint);
            this.pnlTop.DoubleClick += new System.EventHandler(this.pnlTop_DoubleClick);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.BackColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // 
            // 
            this.lblTieuDe.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblTieuDe.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblTieuDe.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblTieuDe.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblTieuDe.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTieuDe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Document, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Black;
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(117, 19);
            this.lblTieuDe.TabIndex = 1;
            this.lblTieuDe.Text = "Nhóm Phòng Khám";
            this.lblTieuDe.BackColorChanged += new System.EventHandler(this.lblTieuDe_BackColorChanged);
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            this.lblTieuDe.DoubleClick += new System.EventHandler(this.lblTieuDe_DoubleClick);
            this.lblTieuDe.Resize += new System.EventHandler(this.lblTieuDe_Resize);
            // 
            // lblTong
            // 
            this.lblTong.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTong.Location = new System.Drawing.Point(683, 0);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(227, 22);
            this.lblTong.TabIndex = 5;
            this.lblTong.LBLDoubleClick += new System.EventHandler(this.lblTong_LBLDoubleClick);
            this.lblTong.reload += new System.EventHandler(this.lblTong_reload);
            this.lblTong.reloadGiuong += new System.EventHandler(this.lblTong_reloadGiuong);
            // 
            // cmt_MenuGiuong
            // 
            this.cmt_MenuGiuong.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đặtGiườngToolStripMenuItem,
            this.hủyĐặtGiườngToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.btnDuyet,
            this.hủyĐặtGiườngToolStripMenuItem,
            this.hủyChuyểnGiườngToolStripMenuItem,
            this.cậpNhậtTrạngTháiToolStripMenuItem,
            this.xuấtGiườngToolStripMenuItem,
            this.khaiBáoGiườngToolStripMenuItem,
            this.choVàoDanhSáchĐenToolStripMenuItem});
            this.cmt_MenuGiuong.Name = "contextMenuStrip1";
            this.cmt_MenuGiuong.Size = new System.Drawing.Size(250, 224);
            // 
            // đặtGiườngToolStripMenuItem
            // 
            this.đặtGiườngToolStripMenuItem.Name = "đặtGiườngToolStripMenuItem";
            this.đặtGiườngToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.đặtGiườngToolStripMenuItem.Text = "Đặt giường";
            this.đặtGiườngToolStripMenuItem.Click += new System.EventHandler(this.đặtGiườngToolStripMenuItem_Click);
            // 
            // hủyĐặtGiườngToolStripMenuItem1
            // 
            this.hủyĐặtGiườngToolStripMenuItem1.Name = "hủyĐặtGiườngToolStripMenuItem1";
            this.hủyĐặtGiườngToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.hủyĐặtGiườngToolStripMenuItem1.Text = "Hủy đặt giường";
            this.hủyĐặtGiườngToolStripMenuItem1.Click += new System.EventHandler(this.hủyĐặtGiườngToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem1.Text = "Chuyển  sang giường mới";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // btnDuyet
            // 
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(249, 22);
            this.btnDuyet.Text = "Danh sách bệnh nhân chờ duyệt";
            this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
            // 
            // hủyĐặtGiườngToolStripMenuItem
            // 
            this.hủyĐặtGiườngToolStripMenuItem.Name = "hủyĐặtGiườngToolStripMenuItem";
            this.hủyĐặtGiườngToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.hủyĐặtGiườngToolStripMenuItem.Text = "Hủy duyệt giường";
            this.hủyĐặtGiườngToolStripMenuItem.Click += new System.EventHandler(this.hủyĐặtGiườngToolStripMenuItem_Click);
            // 
            // hủyChuyểnGiườngToolStripMenuItem
            // 
            this.hủyChuyểnGiườngToolStripMenuItem.Name = "hủyChuyểnGiườngToolStripMenuItem";
            this.hủyChuyểnGiườngToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.hủyChuyểnGiườngToolStripMenuItem.Text = "Hủy chuyển giường";
            this.hủyChuyểnGiườngToolStripMenuItem.Visible = false;
            this.hủyChuyểnGiườngToolStripMenuItem.Click += new System.EventHandler(this.hủyChuyểnGiườngToolStripMenuItem_Click);
            // 
            // cậpNhậtTrạngTháiToolStripMenuItem
            // 
            this.cậpNhậtTrạngTháiToolStripMenuItem.Name = "cậpNhậtTrạngTháiToolStripMenuItem";
            this.cậpNhậtTrạngTháiToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.cậpNhậtTrạngTháiToolStripMenuItem.Text = "Cập nhật trạng thái chờ xuất viện";
            this.cậpNhậtTrạngTháiToolStripMenuItem.Visible = false;
            this.cậpNhậtTrạngTháiToolStripMenuItem.Click += new System.EventHandler(this.cậpNhậtTrạngTháiToolStripMenuItem_Click);
            // 
            // xuấtGiườngToolStripMenuItem
            // 
            this.xuấtGiườngToolStripMenuItem.Name = "xuấtGiườngToolStripMenuItem";
            this.xuấtGiườngToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.xuấtGiườngToolStripMenuItem.Text = "Xuất giường";
            this.xuấtGiườngToolStripMenuItem.Visible = false;
            this.xuấtGiườngToolStripMenuItem.Click += new System.EventHandler(this.xuấtGiườngToolStripMenuItem_Click);
            // 
            // khaiBáoGiườngToolStripMenuItem
            // 
            this.khaiBáoGiườngToolStripMenuItem.Name = "khaiBáoGiườngToolStripMenuItem";
            this.khaiBáoGiườngToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.khaiBáoGiườngToolStripMenuItem.Text = "Cập nhật giá giường";
            this.khaiBáoGiườngToolStripMenuItem.Visible = false;
            this.khaiBáoGiườngToolStripMenuItem.Click += new System.EventHandler(this.khaiBáoGiườngToolStripMenuItem_Click);
            // 
            // choVàoDanhSáchĐenToolStripMenuItem
            // 
            this.choVàoDanhSáchĐenToolStripMenuItem.Name = "choVàoDanhSáchĐenToolStripMenuItem";
            this.choVàoDanhSáchĐenToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.choVàoDanhSáchĐenToolStripMenuItem.Text = "Cho vào danh sách đen";
            this.choVàoDanhSáchĐenToolStripMenuItem.Visible = false;
            this.choVàoDanhSáchĐenToolStripMenuItem.Click += new System.EventHandler(this.choVàoDanhSáchĐenToolStripMenuItem_Click);
            // 
            // timp
            // 
            this.timp.Interval = 5000;
            this.timp.Tick += new System.EventHandler(this.tmp_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(250, 224);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem2.Text = "Đặt giường";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem3.Text = "Hủy đặt giường";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem4.Text = "Duyệt gường";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem5.Text = "Hủy duyệt giường";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem6.Text = "Chuyển  giường";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem7.Text = "Hủy chuyển giường";
            this.toolStripMenuItem7.Visible = false;
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem8.Text = "Cập nhật trạng thái chờ xuất viện";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem9.Text = "Xuất giường";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem10.Text = "Cập nhật giá giường";
            this.toolStripMenuItem10.Visible = false;
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(249, 22);
            this.toolStripMenuItem11.Text = "Cho vào danh sách đen";
            this.toolStripMenuItem11.Visible = false;
            // 
            // usc_DragControlEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "usc_DragControlEdit";
            this.Size = new System.Drawing.Size(974, 22);
            this.Load += new System.EventHandler(this.usc_DragControl_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.cmt_MenuGiuong.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
    }
}
