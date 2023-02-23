using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DevComponents.DotNetBar;
using E00_System;

namespace E11_PhongGiuong
{
    public partial class Usc_GiuongEdit : UserControl
    {

        #region Biến toàn cục
        private CultureInfo _ci = CultureInfo.InvariantCulture;
        private int iD = -1;
        private string key = "";
        public Size KichthuocTang;
        private bool _baoDong = false;
        private bool changecolr = true;
        private Color curcolor;
        private int _slBenhnhan = 1;
        private string tengiuong = "";
        public event MouseEventHandler HisMouseMove;
        public event EventHandler HienThiThongTinBN;
        public event EventHandler AnThongTinBN;
        public event MouseEventHandler HisMouseDown;
        public event MouseEventHandler HisMouseUp;
        public event EventHandler HisMouseEnter;
        public event EventHandler HisClick;
        public RibbonForm _frm;
        private string tinhtrang = "";
        private string maphong="";
        private string tenPhong = "";
        private string tenloaiphong = "";
        private int scrollValue = -1;
        private DataTable databenhnhan;
        private Panel _panelMain;
        cls_ThucThiDuLieu _tT;
        public usc_DragControlEdit pa;
    

        #endregion

        #region Thuộc tính

        public int ID
        {
            get { return iD; }
            set { iD = value; }
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
        public string TenGiuong
        {
            get { return tengiuong; }
            set { this.tengiuong = value; }
        }
      

        public int ScrollValue
        {
            get { return scrollValue; }
            set { scrollValue = value; }
        }
        public Panel Panel_Main
        {
            set { _panelMain = value; }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public string Tinhtrang
        {
            get
            {
                return tinhtrang;
            }

            set
            {
                tinhtrang = value;
            }
        }

        public string Maphong
        {
            get
            {
                return maphong;
            }

            set
            {
                maphong = value;
            }
        }

        public string Tenloaiphong
        {
            get
            {
                return tenloaiphong;
            }

            set
            {
                tenloaiphong = value;
            }
        }

        public int SlBenhnhan
        {
            get
            {
                return _slBenhnhan;
            }

            set
            {
                _slBenhnhan = value;
            }
        }

        public DataTable Databenhnhan
        {
          
            set
            {
                databenhnhan = value;
                PanelMain.Controls.Clear();
                _slBenhnhan = 0;
                if (databenhnhan!=null && databenhnhan.Rows.Count>0)
                {
                    usc_GiuongEdit2 giuong;
                    DateTime dt, nS, nV;
                    string ngaySinh, ngayVao, dong2, dong3;
                    TimeSpan tongNgay;
                    foreach (DataRow dr in databenhnhan.Rows)
                    {
                        giuong = new usc_GiuongEdit2();
                       
                            {
                                //#region SET THÔNG TIN BỆNH NHÂN CÓ TRẠNG THÁI BẰNG 1 Edited
                                giuong.lblMaBenhNhan.Text = dr["MABN"].ToString();//gán mã bệnh nhân
                                dt = new DateTime();
                                ngaySinh = "";
                                nS = new DateTime();
                                ngayVao = "";
                                nV = new DateTime();
                                tongNgay = new TimeSpan();
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
                                if (this.Tag.ToString().Split(',').Count() == 3)
                                {
                                this.Tag += "," + giuong.lblMaBenhNhan.Text;
                                }
                                try
                                {
                                    giuong.lblTenBenhNhan.Text = dr["HOTEN"].ToString();
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
                                    giuong.lblNgaySinh.Text = nS.ToString("dd/MM/yyyy");
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
                                    giuong.lblNgayVao.Text = nV.ToString("dd/MM/yyyy");
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
                                    giuong.lblTongNgay.Text = (int.Parse(tongNgay.Days.ToString().Replace("-", "")) + 1).ToString();//gán tổng so với hiện tại
                                }
                                catch
                                {
                                }
                                try
                                {
                                    giuong.lblGioVao.Text = DateTime.Parse(dr["TU"].ToString() == DBNull.Value.ToString() ? TT.GetSysDate().ToString() : dr["TU"].ToString()).ToShortTimeString();
                                }
                                catch
                                {
                                }
                                giuong.lblBenh.Text = "Chưa load";//gán bệnh chính 
                                giuong.lblGTinh.Text = dr["PHAI"].ToString() == "0" ? "NAM" : "Nữ";
                                //Ẩn hết thông tin mặc định
                               
                                {
                                    try
                                    {
                                        giuong.lblMaBenhNhan.Visible = giuong.lblTenBenhNhan.Visible = giuong.lblTieuDeGioiTinh.Visible = giuong.lblTieuDeNgaySinh.Visible = giuong.lblTieuDeNgayVao.Visible = giuong.lblTieuDeGio.Visible = giuong.lblSuDung.Visible =
                                        giuong.lblTieuDeNgay.Visible = giuong.lblBenh.Visible = true;
                                        giuong.lblGTinh.Visible = giuong.lblNgayVao.Visible = giuong.lblTongNgay.Visible = giuong.lblNgaySinh.Visible = giuong.lblGioVao.Visible = true;
                                    }
                                    catch //(Exception)
                                    {

                                        //throw;
                                    }
                                }
                            }
                       
                        PanelMain.Controls.Add(giuong);
                        giuong.Dock = DockStyle.Top;
                        giuong.BringToFront();
                    }
                    _slBenhnhan = databenhnhan.Rows.Count;
                  
                }
            }
        }

        public string TenPhong
        {
            get
            {
                return tenPhong;
            }

            set
            {
                tenPhong = value;
            }
        }

        //public bool BaoDong
        //{
        //    get { return _baoDong; }
        //    set
        //    {

        //        if (value)
        //        {

        //            curcolor = pnlTop.Style.BackColor1.Color;
        //            //this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Maximized;
        //            //this.FindForm().Activate();

        //            if (Form.ActiveForm != this.FindForm())
        //            {
        //                showNotification();
        //            }
        //        }
        //        else
        //        {

        //            pnlTop.Style.BackColor1.Color = curcolor;
        //            pnlTop.Style.BackColor2.Color = curcolor;
        //            if (_frm != null)
        //            {
        //                _frm.Close();
        //            }

        //        };
        //        if (this.Parent.Parent != null)
        //        {
        //            if (this.Parent.Parent is usc_DragControl)
        //            {
        //                usc_DragControl Parent = (usc_DragControl)this.Parent.Parent;

        //                Parent.BaoDong(value);
        //            }
        //        }

        //    }
        //}

        #endregion

        #region Hàm khởi tạo
        public Usc_GiuongEdit(string TenGiuong, DateTime NgaygioVao, string MaBN, string TenBN, string NgaySinh, string BenhChinh)
        {
            InitializeComponent();

            this.Height = pnlTop.Height;
            this.Width = 132;
           // this.pnlBottom.Height = 0;
        //    lblLoaiGiuongColor.Left = 105;

            this.lblGiuong.Text = TenGiuong;
           
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].MouseDown += new MouseEventHandler(Usc_Giuong_MouseDown);
                this.Controls[i].MouseUp += new MouseEventHandler(Usc_Giuong_MouseUp);
                this.Controls[i].MouseMove += new MouseEventHandler(Usc_Giuong_MouseMove);
                this.Controls[i].MouseEnter += new System.EventHandler(Usc_Giuong_MouseEnter);
                this.Controls[i].MouseClick += new MouseEventHandler(Usc_Giuong_MouseClick);
                if (this.Controls[i] is PanelEx)
                {
                    for (int j = 0; j < ((PanelEx)this.Controls[i]).Controls.Count; j++)
                    {
                        ((PanelEx)this.Controls[i]).Controls[j].MouseDown += new MouseEventHandler(Usc_Giuong_MouseDown);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseUp += new MouseEventHandler(Usc_Giuong_MouseUp);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseMove += new MouseEventHandler(Usc_Giuong_MouseMove);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseEnter += new System.EventHandler(Usc_Giuong_MouseEnter);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseClick += new MouseEventHandler(Usc_Giuong_MouseClick);
                    }
                }
            }
        }

        public Usc_GiuongEdit()
        {
            InitializeComponent();
            this.Height = pnlTop.Height;
            this.Width = 132;
            this.pnlBottom.Height = 0;
            lblLoaiGiuongColor.Left = 105;
        
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].MouseDown += new MouseEventHandler(Usc_Giuong_MouseDown);
                this.Controls[i].MouseUp += new MouseEventHandler(Usc_Giuong_MouseUp);
                this.Controls[i].MouseMove += new MouseEventHandler(Usc_Giuong_MouseMove);
                this.Controls[i].MouseEnter += new System.EventHandler(Usc_Giuong_MouseEnter);
                this.Controls[i].MouseClick += new MouseEventHandler(Usc_Giuong_MouseClick);
                if (this.Controls[i] is PanelEx)
                {
                    for (int j = 0; j < ((PanelEx)this.Controls[i]).Controls.Count; j++)
                    {
                        ((PanelEx)this.Controls[i]).Controls[j].MouseDown += new MouseEventHandler(Usc_Giuong_MouseDown);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseUp += new MouseEventHandler(Usc_Giuong_MouseUp);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseMove += new MouseEventHandler(Usc_Giuong_MouseMove);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseEnter += new System.EventHandler(Usc_Giuong_MouseEnter);
                        ((PanelEx)this.Controls[i]).Controls[j].MouseClick += new MouseEventHandler(Usc_Giuong_MouseClick);
                    }

                }
            }
        }
        #endregion

        #region Phương thức
        public void showNotification()
        {
            _frm = new RibbonForm();
            _frm.TopMost = true;
            Usc_GiuongEdit newc = new Usc_GiuongEdit();
            newc.TenGiuong = this.TenGiuong;
         
            newc.HisMouseDown += new MouseEventHandler(turnoffFrm);
            _frm.Controls.Add(newc);
            newc.Location = new Point(0, 0);
            _frm.Height = newc.Height;
            _frm.Width = newc.Width;
            _frm.ShowInTaskbar = false;
            _frm.Show();
            _frm.Location = new Point(SystemInformation.VirtualScreen.Width - _frm.Width, SystemInformation.VirtualScreen.Height - _frm.Height - 40);

            //FlashWindow.Flash(this.FindForm()); 
        }
        public void hideNotification()
        {
            this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.FindForm().Activate();

            if (this.pa.pnlMain.Height > 1)
            {
                _panelMain.ScrollControlIntoView(this);
            }
            else
            {
                _panelMain.ScrollControlIntoView(this.pa);
            }
            if (_frm != null)
            {
                _frm.Close();
            }

        }
        #endregion

        #region Sự kiện

        private void Usc_Giuong_MouseUp(object sender, MouseEventArgs e)
        {
            MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, Cursor.Position.X, Cursor.Position.Y, e.Delta);
            if (HisMouseUp != null)
            {
                HisMouseUp(this, e1);
            }
        }

        private void turnoffFrm(object sender, EventArgs e)
        {
            this.hideNotification();

        }

        private void Usc_Giuong_MouseDown(object sender, MouseEventArgs e)
        {

            MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, Cursor.Position.X, Cursor.Position.Y, e.Delta);
            if (HisMouseDown != null)
            {
                HisMouseDown(this, e1);
            }
        }

        private void Usc_Giuong_MouseMove(object sender, MouseEventArgs e)
        {

            MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, Cursor.Position.X, Cursor.Position.Y, e.Delta);
            if (HisMouseMove != null)
            {
                HisMouseMove(this, e1);
            }

        }

        private void Usc_Giuong_MouseEnter(object sender, EventArgs e)
        {
            if (HisMouseEnter != null)
            {
                HisMouseEnter(this, e);
            }

        }

        private void BaoDong_Tick(object sender, EventArgs e)
        {


            if (changecolr)
            {
                pnlTop.Style.BackColor1.Color = Color.Green;
                pnlTop.Style.BackColor2.Color = Color.Green;

            }
            else
            {
                pnlTop.Style.BackColor1.Color = Color.Red;
                pnlTop.Style.BackColor2.Color = Color.Red;
            }
            changecolr = !changecolr;

        }

        private void Usc_Giuong_MouseClick(object sender, MouseEventArgs e)
        {

            if (HisClick != null)
            {
                HisClick(this, e);
            }
        }

     

        private void lblGTinh_Click(object sender, EventArgs e)
        {
        }

        private void lblGiuong_DoubleClick(object sender, EventArgs e)
        {
        }

        private void Usc_GiuongEdit_Load(object sender, EventArgs e)
        {
            this.Height = pnlTop.Height;
            this.Width = 132;
         //   this.pnlBottom.Height = 0;
           // lblLoaiGiuongColor.Left = 105;
        }

        #endregion

        private void lblGiuong_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Height =  pnlTop.Height + (_slBenhnhan * 120)+pnlBottom.Height;
            this.Width = 274;
            if (HienThiThongTinBN!=null)
            {
              
                this.KichthuocTang = new Size(this.Width - 132, this.Height - pnlTop.Height);
                HienThiThongTinBN(this, null);
            }
            //foreach (usc_GiuongEdit2 item in PanelMain.Controls)
            //{
            //    item.Width = 100;
            //}
        //    lblLoaiGiuongColor.Left = 247;

        }

        private void lblGiuong_MouseLeave(object sender, EventArgs e)
        {
         
            if (AnThongTinBN != null)
            {
                this.KichthuocTang  = new Size(this.Width - 132, this.Height - pnlTop.Height);
                AnThongTinBN(this, null);
            }
            this.Height = pnlTop.Height;
            this.Width = 132;
            //  lblLoaiGiuongColor.Left = 105;
        }

        private void Usc_GiuongEdit_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
