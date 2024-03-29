﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using E00_Common;
using E00_Model;

using System.Windows.Forms;
using System.Reflection;

namespace E11_PhongGiuong
{
    public class cls_ThucThiDuLieu
    {
        #region Khai báo biến
        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
        private string _userError, _systemError, _id = string.Empty;
        private List<string> _lstMaTenKhoa = new List<string>();
        private List<string> _lstMaTenPhong = new List<string>();
        private string _idGiuongChonDat = string.Empty;
        private DataTable _tbBTDBN = new DataTable();
        private DataRow dr;
        public bool _status;
        private string _colorTrong, _colorDat, _colorCoNguoi, _colorChuaSuDung;
        private string _sql = string.Empty;
        #endregion

       
        #region Lấy id theo dõi giường
        public string GetIdTheoDoiGiuong()
        {
            string idmax = string.Empty;
            DataTable tb = _acc.Get_Data(string.Format("select max({0})+1 from {1}.{2}", cls_PG_TheoDoiGiuong.col_ID, _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang));
            return idmax = tb.Rows[0].ItemArray[0].ToString() == string.Empty ? "1" : tb.Rows[0].ItemArray[0].ToString();
        }
        public string getmadoituong(string mabn)
        {
            DataTable tb = _acc.Get_Data(string.Format("select {0} from {1}.{2} WHERE {3} = '{4}' ", cls_HienDien.col_MaQL, _acc.Get_User(), cls_HienDien.tb_TenBang, cls_HienDien.col_MaBN, mabn));
            if (tb !=null && tb.Rows.Count>0)
            {
                string maql
                   = tb.Rows[0][0] + "";
                DataTable tb2 = _acc.Get_Data(string.Format("select {0} from {1}.{2} WHERE {3} = '{4}' and {5} = '{6}'", cls_BenhAnDT.col_MaDoiTuong, _acc.Get_User(), cls_BenhAnDT.tb_TenBang, cls_BenhAnDT.col_MaBN, mabn, cls_BenhAnDT.col_MaQL, maql));

                if (tb2 != null && tb2.Rows.Count > 0)
                {
                    return tb2.Rows[0][cls_BenhAnDT.col_MaDoiTuong.ToUpper()] + "";
                } 
            }
            return "";

        }
        public DataRow GetRowTiepDon(string mabn ,string maql)
        {
            List<string> lst = new List<string> {
        cls_BenhNhan.col_Nam.ToUpper()
    };
            Dictionary<string, string> dicEqual = new Dictionary<string, string> {
        {
            cls_BenhNhan.col_MaBN.ToUpper(),
            mabn
        }
    };
            DataTable table = this._api.Search(ref this._userError, ref this._systemError, cls_BenhNhan.tb_TenBang, null, -1, lst, true, dicEqual, true, null, false, null, true, null, true, null, false, false);
            if ((table != null) && (table.Rows.Count > 0))
            {
                string str = table.Rows[0][cls_BenhNhan.col_Nam.ToUpper()]+"";
                char[] separator = new char[] { '+' };
                string[] strArray = ((str[str.Length - 1] == '+') ? str.Remove(str.Length - 1) : str).Split(separator);
                for (int i = strArray.Length - 1; i >= 0; i--)
                {
                    string schema = this._acc.Get_User() + strArray[i];
                    dicEqual.Clear();
                    dicEqual.Add(cls_TiepDon.col_MaBN.ToUpper(), mabn);
                    dicEqual.Add(cls_TiepDon.col_MaQL, maql);
                    Dictionary<string, string> dictionary2 = dicEqual;
                    table = this._api.Search(ref this._userError, ref this._systemError, cls_TiepDon.tb_TenBang, schema, -1, null, true, dictionary2, true, null, false, null, false, cls_TiepDon.col_Ngay, true, null, false, false);
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        return table.Rows[0];
                    }
                }
            }
            return null;
        }


        #endregion

        #region Lấy danh sách giường hiện tại

        public bool CheckMaBN(string MABN, out string tengiuong)
        {
            _sql = string.Format("select {6} from {1}.{7} where {8} in (SELECT {0} FROM {1}.{2} where {3} = '{4}'  and {9}='1' and {5}='0') ",
                                cls_PG_TheoDoiGiuong.col_IDGIUONG,//0
                                 _acc.Get_User(),//1
                                 cls_PG_TheoDoiGiuong.tb_TenBang,//2
                                 cls_PG_TheoDoiGiuong.col_MABN,//3
                                 MABN,//4
                                 cls_PG_TheoDoiGiuong.col_TRANGTHAI,//5
                                 cls_PG_DanhMucGiuong.col_TEN,//6
                                  cls_PG_DanhMucGiuong.tb_TenBang,//7
                                  cls_PG_DanhMucGiuong.col_ID//8
                                 , cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN//9
                                 );
            DataTable dt = _acc.Get_Data(_sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                tengiuong = dt.Rows[0][0] + "";
                return true;
            }
            tengiuong = "";
            return false;
        }
        public bool CheckMaBNDat(string MABN, string idgiuong)
        {
            _sql = string.Format("select {6} from {1}.{7} where {8} in (SELECT {0} FROM {1}.{2} where {3} = '{4}' and {10}='{11}'  and {9}='0' and {5}='0') ",
                                cls_PG_TheoDoiGiuong.col_IDGIUONG,//0
                                 _acc.Get_User(),//1
                                 cls_PG_TheoDoiGiuong.tb_TenBang,//2
                                 cls_PG_TheoDoiGiuong.col_MABN,//3
                                 MABN,//4
                                 cls_PG_TheoDoiGiuong.col_TRANGTHAI,//5
                                 cls_PG_DanhMucGiuong.col_TEN,//6
                                  cls_PG_DanhMucGiuong.tb_TenBang,//7
                                  cls_PG_DanhMucGiuong.col_ID//8
                                 , cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN//9
                                  , cls_PG_TheoDoiGiuong.col_IDGIUONG//10
                                   , idgiuong //11
                                 );
            DataTable dt = _acc.Get_Data(_sql);
            if (dt != null && dt.Rows.Count > 0)
            {

                return true;
            }

            return false;
        }
        public string GetTinhTrangGiuong(string IdGiuong)
        {
            _sql = string.Format("select {4} from {0}.{1} where {2} ='{3}'", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_ID, IdGiuong, cls_PG_DanhMucGiuong.col_TINHTRANG);
            DataTable dt = _acc.Get_Data(_sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0] + "";
            }

            return "";
        }
        public DataTable GetTBGiuong(string maKP = "", string maPhong = "", string maLoaiGIuong = "", string idGiuong = "")
        {
            string where = "";
            if (!string.IsNullOrEmpty(maKP))
            {
                where += " AND b." + cls_DanhMucPhong.col_MAKP + " = '" + maKP + "' ";
            }
            if (!string.IsNullOrEmpty(maLoaiGIuong))
            {
                where += " AND a." + cls_PG_DanhMucGiuong.col_LOAIGIUONG + " = '" + maLoaiGIuong + "' ";
            }
            if (!string.IsNullOrEmpty(maPhong))
            {
                where += " AND b." + cls_DanhMucPhong.col_MA + " = '" + maPhong + "' ";
            }
            if (!string.IsNullOrEmpty(idGiuong))
            {
                where += " AND a." + cls_PG_DanhMucGiuong.col_ID + " = '" + idGiuong + "' ";
            }

            if (!string.IsNullOrEmpty(where))
            {
                where = " WHERE " + where.Remove(0, 4);
            }
            _sql = string.Format("select a.{0},a.{1},a.{2},a.{3} as ten,a.{4},a.{5},a.{6},a.{7},a.{8},lp." + cls_DanhMucLoaiPhong.col_MAUSAC + ","
                  + " a.{9},a.{10},a.{11},c.{12},a.{13},a.{14},b.{15},btd.{16},a.{17} as MAPHONG,b.TEN as TENPHONG,lg.{18},lp.{19} as MALOAIPHONG ,lp.TEN as TENLOAIPHONG,a.magiuong "
                  + " from {20}.{21} a left join {20}.{22} b on a.{23}=b.{24} left join {20}.{25} c on a.{26}=c.{27} left join {20}.{28} btd on btd.{29}=b.{30} left join {20}.{31} lg on lg.{32}=a.{33} "
                  + " left join {20}.{34} lp on lp.{35}= b.{36} "
                  + where
                  + "  order by b.{37},a.{38}",
                  cls_PG_DanhMucGiuong.col_ID, cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_MA, cls_PG_DanhMucGiuong.col_TEN, cls_PG_DanhMucGiuong.col_TINHTRANG,
                  cls_PG_DanhMucGiuong.col_SOLUONG, cls_PG_DanhMucGiuong.col_GIA_TH, cls_PG_DanhMucGiuong.col_GIA_BH, cls_PG_DanhMucGiuong.col_GIA_CS, cls_PG_DanhMucGiuong.col_GIA_DV,
                  cls_PG_DanhMucGiuong.col_GIA_NN, cls_PG_DanhMucGiuong.col_BHYT, cls_V_GiaVienPhi.col_CHENHLECH, cls_PG_DanhMucGiuong.col_VITRI, cls_PG_DanhMucGiuong.col_LOAIGIUONG,
                  cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_TenKP, cls_PG_DanhMucGiuong.col_MAPHONG, cls_D_DanhMucLoaiGiuong.col_TENLOAI, cls_DanhMucLoaiPhong.col_MA,
                  _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG,
                  cls_V_GiaVienPhi.tb_TenBang, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_ID, cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_D_DanhMucLoaiGiuong.tb_TenBang,
                  cls_D_DanhMucLoaiGiuong.col_MALOAI, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_DanhMucLoaiPhong.tb_TenBang, cls_DanhMucLoaiPhong.col_MA, cls_DanhMucPhong.col_LOAI,//
                   cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                  );

            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy tất cả giường, bệnh nhân và trạng thái hiện thời của giường

        public DataTable SourcePanelGiuongTheoDanhMucGiuong()
        {
            _sql = string.Format("select a.BLOCK,a.{0},a.{1},a.{2},a.{3} as ten,a.{4},lg.{5} as TENLOAI,b.{6},btd.{7},a.{8} as MAPHONG,b.{9} as TENPHONG,a.{10},c.{11} as TENVIENPHI,a.{12},a.{13},ttn."+ cls_DanhMucColorTrangThaiNew.col_Loai+ " "
                               + " from {14}.{15} a left join {14}.{16} b on a.{17}=b.{18} left join {14}.{19} c on a.{20}=c.{21} left join {14}.{22} btd on btd.{23}=b.{24} left join {14}.{25} lg on lg.{26} = a.{27} "
                               + " left join {14}."+ cls_DanhMucColorTrangThaiNew.tb_TenBang+ " ttn on ttn."+ cls_DanhMucColorTrangThaiNew.col_Id+ " = a.{12} "
                               + " order by b.{28},a.{29} ", cls_PG_DanhMucGiuong.col_ID, cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_MAGIUONG, cls_PG_DanhMucGiuong.col_TEN, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_D_DanhMucLoaiGiuong.col_TENLOAI,
                               cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_TenKP, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_TEN, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_TEN, cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_CHUAN,
                               _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_V_GiaVienPhi.tb_TenBang, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_DanhMucPhong.col_ID,
                               cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_D_DanhMucLoaiGiuong.col_MALOAI, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                               );
            return _acc.Get_Data(_sql);
        }

        public DataTable SourceGiuongImport()
        {
            _sql = string.Format("select a.{0},a.{1},a.{2} as ten,lg.{3},a.{4} as MAPHONG,c.{5}  as TENGIAVP,a.{6}"
                               + " from {7}.{8} a left join {7}.{9} b on a.{10}=b.{11} left join {7}.{12} c on a.{13}=c.{14} left join {7}.{15} btd on btd.{16}=b.{17} left join {7}.{18} lg on lg.{19} = a.{20} "//Sua loi hien thi
                               + " order by b.{21},a.{22}",
                               cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_MAGIUONG, cls_PG_DanhMucGiuong.col_TEN, cls_D_DanhMucLoaiGiuong.col_TENLOAI, cls_PG_DanhMucGiuong.col_MAPHONG, cls_V_GiaVienPhi.col_TEN, cls_PG_DanhMucGiuong.col_CHUAN,
                               _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_V_GiaVienPhi.tb_TenBang, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_ID,
                               cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_D_DanhMucLoaiGiuong.col_MALOAI, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                                   );
            return _acc.Get_Data(_sql);
        }

        #endregion

        #region Lấy thông tin bệnh nhân đặt giường theo điều kiện: Khoa phòng và phòng
        public string QueryGetGiuongTuTableDatGiuong(string maKhoa = "", string maPhong = "", string idgiuong = "", bool isincludeDatGiuong = false)
        {
            string where = "";
            if (!string.IsNullOrEmpty(maKhoa))
            {
                where += " AND b." + cls_DanhMucPhong.col_MAKP + " = '" + maKhoa + "' ";
            }
            if (!string.IsNullOrEmpty(idgiuong))
            {
                where += " AND tdg." + cls_PG_TheoDoiGiuong.col_IDGIUONG + " = '" + idgiuong + "' ";
            }
            if (!string.IsNullOrEmpty(maPhong))
            {
                where += " AND b." + cls_DanhMucPhong.col_MA + " = '" + maPhong + "' ";
            }

            //if (!string.IsNullOrEmpty(where))
            //{
            //    where = " WHERE " + where.Remove(0, 4);
            //}
            if (!isincludeDatGiuong)
            {
                where += " AND tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = '1' ";
            }
            string str = "", str2 = "";
            if (!isincludeDatGiuong)
            {
                str = "tdg.*,";
                str2 = " order by b.{38},a.{39}";
            }
            _sql = string.Format("select  tdg.IDGIUONG,tdg.MABN,tdg.TU, btd.{0},btd.{1},btd.{2},btd.{3},btd.{4},btd.{5},"
                            + "btd.{6},tdg.makp,a.maphong,btd.{7},a.{8},a.{9},a.{10} as ten,a.{11},dt.DOITUONG,a.LOAIGIUONG "
                            + " ,a.{12},a.{13},a.{14}, a.{15},a.{16},a.{17},c.{18},a.{19}"
                            + " from  {20}.{28} tdg"
                            + " left join {20}.{21} a  on a.{29}=tdg.{30} "
                            + " left join {20}.{22} b on a.{23}=b.{24} "//sửa mã
                            + " left join {20}.{25} c on a.{26}=c.{27}  "//Sua loi hien thi
                                                                         // + " right join {20}.{28} tdg on a.{29}=tdg.{30}  "
                            + " left join {20}.{31} btd on tdg.{32}=btd.{33} "
                            + " left join " + _acc.Get_User() + ".TIEPDON td on td.MABN = tdg.MABN "
                            + " left join " + _acc.Get_User() + ".DOITUONG dt on  td.MADOITUONG = dt.MADOITUONG "
                            + " WHERE   tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAI + "='0' " + where
                            + str2
                            + " ",
                            cls_BTDBN.col_HoTen, cls_BTDBN.col_NgaySinh, cls_BTDBN.col_NamSinh, cls_BTDBN.col_Phai, cls_BTDBN.col_MaNN, cls_BTDBN.col_MaDanToc, cls_BTDBN.col_SoNha, cls_BTDBN.col_Thon, cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_MA, cls_PG_DanhMucGiuong.col_TEN,
                            cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_GIA_TH, cls_PG_DanhMucGiuong.col_GIA_BH, cls_PG_DanhMucGiuong.col_GIA_CS, cls_PG_DanhMucGiuong.col_GIA_DV, cls_PG_DanhMucGiuong.col_GIA_NN, cls_PG_DanhMucGiuong.col_BHYT, cls_V_GiaVienPhi.col_CHENHLECH,
                            cls_PG_DanhMucGiuong.col_VITRI, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_V_GiaVienPhi.tb_TenBang, cls_PG_DanhMucGiuong.col_IDGIAVP,
                            cls_V_GiaVienPhi.col_ID, cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_ID, cls_PG_TheoDoiGiuong.col_IDGIUONG, cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN, cls_BTDBN.col_MaBN,
                            cls_DanhMucPhong.col_MAKP, maKhoa, cls_DanhMucPhong.col_MA, maPhong,
                            cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                            );


                return _sql;



        }
        #endregion

        #region Lấy thông tin bệnh nhân có người theo điều kiện: Khoa phòng và phòng
        public string QueryGetGiuongTuTableTheoDoiGiuong(string maKhoa, string maPhong)
        {
            if (maPhong == "0")
            {

                return _sql = string.Format("select "
                           + " tdg.*,btd.HOTEN,btd.NGAYSINH,btd.NAMSINH,btd.PHAI,btd.MANN,btd.MADANTOC,"
                           + "btd.SONHA,btd.THON,a.stt,a.ma,a.ten as ten,a.tinhtrang"
                           + " ,a.gia_th,a.gia_bh,a.gia_cs, a.gia_dv,a.gia_nn,a.bhyt,c.chenhlech,a.vitri"
                           + " from {0}.{1} a  "
                           + " inner join {0}.{2} b on a.maphong=b.maphong "//Sửa mã
                           + " inner join {0}.{3} c on a.idgiavp=c.id  "//Sua loi hien thi
                           + " left join {0}.{4} tdg on a.id=tdg.idgiuong AND tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAI + "='0'  AND tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = '1' "
                           + " inner join {0}.{5} btd on tdg.mabn=btd.mabn "
                           + " where  b.makp={6} order by b.stt,a.stt ", _acc.Get_User()
                           , cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_PG_TheoDoiGiuong.tb_TenBang,
                           cls_BTDBN.tb_TenBang, maKhoa);
            }
            else
            {

                return _sql = string.Format("select  "
                           + " tdg.*,btd.HOTEN,btd.NGAYSINH,btd.NAMSINH,btd.PHAI,btd.MANN,btd.MADANTOC,"
                           + "btd.SONHA,btd.THON,a.stt,a.ma,a.ten as ten,a.tinhtrang"
                           + " ,a.gia_th,a.gia_bh,a.gia_cs, a.gia_dv,a.gia_nn,a.bhyt,c.chenhlech,a.vitri"
                           + " from {0}.{1} a  "
                           + " inner join {0}.{2} b on a.maphong=b.maphong "//Sửa mã
                           + " inner join {0}.{3} c on a.idgiavp=c.id  "//Sua loi hien thi
                           + " left join {0}.{4} tdg on a.id=tdg.idgiuong AND tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAI + "='0'  AND tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = '1'  "
                           + " inner join {0}.{5} btd on tdg.mabn=btd.mabn "
                           + " where  b.makp={6} and b.ma='{7}' order by b.stt,a.stt ", _acc.Get_User()
                           , cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_PG_TheoDoiGiuong.tb_TenBang,
                           cls_BTDBN.tb_TenBang, maKhoa, maPhong);
            }

        }
        #endregion

        #region Lấy thông tin bệnh nhân có người 
        public string QueryGetGiuongTuTableTheoDoiGiuong1(string doiTuong)
        {
            //return _sql = string.Format("select * from (select  ROW_NUMBER() OVER (PARTITION BY tdg.IDGIUONG ORDER BY "
            //                           + " tdg.IDGIUONG,tdg.trangthai ASC ) AS rn, "
            //                           + " tdg.*,btd.HOTEN,btd.NGAYSINH,btd.NAMSINH,btd.PHAI,btd.MANN,btd.MADANTOC,"
            //                           + "btd.SONHA,btd.THON,a.stt,a.ma,a.ten as ten,a.tinhtrang"
            //                           + " ,a.gia_th,a.gia_bh,a.gia_cs, a.gia_dv,a.gia_nn,a.bhyt,c.chenhlech,a.vitri"
            //                           + " from {0}.PG_DANHMUCGIUONG a  "
            //                           + " inner join {0}.DANHMUCPHONG b on a.idphong=b.id "
            //                           + " inner join {0}.V_GIAVP c on a.id=c.id  "
            //                           + " left join {0}.theodoigiuong tdg on a.id=tdg.idgiuong  "
            //                           + " inner join {0}.btdbn btd on tdg.mabn=btd.mabn "
            //                           + " order by b.stt,a.stt ) kq"
            //                           + " where kq.rn = 1", _acc.Get_User(), maKhoa, maPhong);
            _sql = "select  tdg.*,btd.HOTEN,btd.NGAYSINH,btd.NAMSINH,btd.PHAI,btd.MANN,btd.MADANTOC,"
                                        + "btd.SONHA,btd.THON,a.stt,a.ma,a.ten as ten,a.tinhtrang"
                                        + " ,a.gia_th,a.gia_bh,a.gia_cs, a.gia_dv,a.gia_nn,a.bhyt,c.chenhlech,a.vitri"
                                        + " from {0}.PG_DANHMUCGIUONG a  "
                                        + " left join {0}.DANHMUCPHONG b on a.maphong=b.maphong "//Sửa mã
                                        + " left join {0}.V_GIAVP c on a.idgiavp=c.id  "//Sua loi hien thi
                                        + " left join {0}.theodoigiuong tdg on a.id=tdg.idgiuong  "
                                        + " left join {0}.btdbn btd on tdg.mabn=btd.mabn ";
            if (doiTuong != "")
            {
                _sql += " where tdg.TrangThai= '" + doiTuong + "'";
            }
            _sql += " order by b.stt,a.stt ";
            _sql = string.Format(_sql, _acc.Get_User());
            return _sql;

        }
        #endregion

        #region Lấy thông tin bệnh nhân đặt giường
        public string QueryGetGiuongTuTableDatGiuong1()
        {

            return _sql = string.Format("select  tdg.*,btd.{0},btd.{1},btd.{2},btd.{3},btd.{4},btd.{5},"
                           + "btd.{6},btd.{7},a.{8},a.{9},a.{10} as ten,a.{11}"
                           + " ,a.{12},a.{13},a.{14}, a.{15},a.{16},a.{17},c.{18},a.{19}"
                           + " from {20}.{21} a  "
                           + " left join {20}.{22} b on a.{23}=b.{24} "//sửa mã
                           + " left join {20}.{25} c on a.{26}=c.{27}  "//Sua loi hien thi
                           + " left join {20}.{28} tdg on a.{29}=tdg.{30}  "
                           + " left join {20}.{31} btd on tdg.{32}=btd.{33} "
                           + " WHERE tdg.{36}='0' "
                           + " order by b.{34},a.{35}"
                           + " ",
                           cls_BTDBN.col_HoTen,
                           cls_BTDBN.col_NgaySinh,
                           cls_BTDBN.col_NamSinh,
                           cls_BTDBN.col_Phai,
                           cls_BTDBN.col_MaNN,
                           cls_BTDBN.col_MaDanToc,
                           cls_BTDBN.col_SoNha,
                           cls_BTDBN.col_Thon,
                           cls_PG_DanhMucGiuong.col_STT,
                           cls_PG_DanhMucGiuong.col_MA,
                           cls_PG_DanhMucGiuong.col_TEN,//10
                           cls_PG_DanhMucGiuong.col_TINHTRANG,
                           cls_PG_DanhMucGiuong.col_GIA_TH,
                           cls_PG_DanhMucGiuong.col_GIA_BH,
                           cls_PG_DanhMucGiuong.col_GIA_CS,
                           cls_PG_DanhMucGiuong.col_GIA_DV,
                           cls_PG_DanhMucGiuong.col_GIA_NN,
                           cls_PG_DanhMucGiuong.col_BHYT,
                           cls_V_GiaVienPhi.col_CHENHLECH,
                           cls_PG_DanhMucGiuong.col_VITRI,
                           _acc.Get_User(),  //20
                           cls_PG_DanhMucGiuong.tb_TenBang,
                           cls_DanhMucPhong.tb_TenBang,
                           cls_PG_DanhMucGiuong.col_MAPHONG,
                           cls_DanhMucPhong.col_MAPHONG,
                           cls_V_GiaVienPhi.tb_TenBang, //25
                           cls_PG_DanhMucGiuong.col_IDGIAVP,
                           cls_V_GiaVienPhi.col_ID,
                           cls_PG_TheoDoiGiuong.tb_TenBang,
                           cls_PG_DanhMucGiuong.col_ID,
                           cls_PG_TheoDoiGiuong.col_IDGIUONG, //30
                           cls_BTDBN.tb_TenBang,
                           cls_PG_TheoDoiGiuong.col_MABN,
                           cls_BTDBN.col_MaBN,
                           cls_DanhMucPhong.col_STT,
                           cls_PG_DanhMucGiuong.col_STT, //35
                               cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN //36
                           );


        }
        #endregion

        #region Lấy thông tin màu sắc
        public DataTable GetDataColorTrangThai()
        {
            return _acc.Get_Data(string.Format("select * from {0}.{1}", _acc.Get_User(), cls_DanhMucColorTrangThai.tb_TenBang));
        }
        #endregion

        #region Get thông tin màu sắc theo mã 
        public DataTable GetDataColorTrangThai(int id)
        {
            return _acc.Get_Data(string.Format("select * from {0}.{1} where {2} = {3}", _acc.Get_User(), cls_DanhMucColorTrangThai.tb_TenBang, cls_DanhMucColorTrangThai.col_Id, id));
        }

        #endregion

        #region Thêm danh mục màu sắc
        public void InsertTableColorTrangThai(string mauDat, int id, string loai)
        {
            _sql = string.Format("insert into {0}.{1}({2},{3},{4}) values({5},'{6}','{7}')", _acc.Get_User(), cls_DanhMucColorTrangThai.tb_TenBang, cls_DanhMucColorTrangThai.col_Id, cls_DanhMucColorTrangThai.col_Loai, cls_DanhMucColorTrangThai.col_Color, id, loai, mauDat);
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show("Không thể ins nhật màu!", TA_MessageBox.MessageIcon.Error);
                return;
            }
        }
        #endregion

        #region Khởi tạo màu sắc cho từng trạng thái của giường
        public void KhoiTaoColor()
        {
            if (_acc.Get_Data(string.Format("select * from {0}.{1}", _acc.Get_User(), cls_DanhMucColorTrangThai.tb_TenBang)).Rows.Count == 0)
            {
                _colorTrong = "255,0,0,0";
                _colorDat = "255,255,128,64";
                _colorCoNguoi = "255,0,128,255";
                _colorChuaSuDung = "255,192,192,192";
                List<string> lst = new List<string>();
                lst.Add(_colorTrong);
                lst.Add(_colorDat);
                lst.Add(_colorCoNguoi);
                lst.Add(_colorChuaSuDung);
                for (int i = 1; i < 4; i++)
                {
                    string sql = string.Format("insert into {0}.{1}({2},{3},{4})"
                                + " values(" + i + ",'" + i + "','" + lst[i - 1].ToString() + "')", _acc.Get_User(), cls_DanhMucColorTrangThai.tb_TenBang, cls_DanhMucColorTrangThai.col_Id, cls_DanhMucColorTrangThai.col_Loai, cls_DanhMucColorTrangThai.col_Color);
                    if (!_acc.Execute_Data(ref _userError, ref _systemError, sql))
                    {
                        TA_MessageBox.MessageBox.Show("Không thể ins màu giường!", TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                }
            }
        }

        #endregion

        #region Lấy mã, tên khoa phòng
        public string QueryGomNhomKhoaPhong()
        {
            return _sql = string.Format("select distinct btd.{0},btd.{1}"
                                     + " from {2}.{3} p "
                                     + " inner join {2}.{4} g on g.{5} =p.{6}"//Sửa mã
                                     + " inner join {2}.{7} btd on btd.{8}=p.{9} ", cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP, _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG,
                                     cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP);
        }
        #endregion

        #region Lấy mã, tên khoa phòng theo khoa phòng
        public string QueryGomNhomKhoaPhong1(string dkMaKhoa)
        {

            return _sql = string.Format("select distinct btd.{0},btd.{1}"
                         + " from {2}.{3} p "
                         + " inner join {2}.{4} g on g.{5} =p.{6}"//Sửa mã
                         + " inner join {2}.{7} btd on btd.{8}=p.{9} where btd.{10}='{11}'", cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP, _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG,
                         cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_MaKP, dkMaKhoa);
        }
        #endregion



        #region Xóa giường theo bệnh nhân

        #endregion



        #region Lấy thông tin giường để đặt giường
        public DataTable GetDataDatGiuong(string id, string maKhoa, string maPhong)
        {
            _sql = string.Format("select a.id,a.ten,a.stt"
                                     + " from {0}.{1} a,{0}.{2} b,{0}.{3} c where a.MAPHONG=b.MAPHONG and a.idgiavp=c.id and b.makp={4}"//Sua loi hien thi
                                     + " and b.ma='{5}' and a.id = " + id + "  order by b.stt,a.stt",
                      _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, maKhoa,
                      maPhong);//Sửa mã

            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy thông tin bệnh nhân từ bộ từ điển bệnh nhân
        public string QueryBTDBN(string mabn = "")
        {
            return _sql = string.Format("Select a.{0},a.{1},a.{2},a.{3},a.{4},b.{9} from {5}.{6} a left join {7}.{8} b on a.{0} = b.{0} ", cls_BTDBN.col_MaBN, cls_BTDBN.col_HoTen, cls_BTDBN.col_NgaySinh, cls_BTDBN.col_Phai, cls_BTDBN.col_Thon, _acc.Get_User(), cls_BTDBN.tb_TenBang, _acc.Get_UserMMYY(), cls_TiepDon.tb_TenBang, cls_TiepDon.col_MaDoiTuong) + (string.IsNullOrEmpty(mabn) ? "" : " WHERE a.MABN='" + mabn + "' ");

        }

        #endregion

        #region Hiển thị tất cả giường


        #region Lấy source cho datagridview tổng thể
        public DataTable SourceViewTongThe(string tinhtrang = "", string dkMaKhoa = "", string dkMaPhong = "", string tenloai = "")
        {
            string where = "";
            //where += (string.IsNullOrEmpty(tinhtrang) ? "" : (" AND a." + cls_PG_DanhMucGiuong.col_TINHTRANG + " = '" + tinhtrang + "'"));
            where += (string.IsNullOrEmpty(dkMaKhoa) ? "" : (" AND b." + cls_DanhMucPhong.col_MAKP + " = '" + dkMaKhoa + "'"));
            where += (string.IsNullOrEmpty(dkMaPhong) ? "" : (" AND a." + cls_PG_DanhMucGiuong.col_MAPHONG + " = '" + dkMaPhong + "'"));
            if (tenloai != "TongSo")
            {
                where += (string.IsNullOrEmpty(tenloai) ? "" : (" AND lp." + cls_DanhMucLoaiPhong.col_TEN + " = '" + tenloai + "'"));
            }
            if (!string.IsNullOrEmpty(where))
            {
                where = " WHERE " + where.Remove(0, 4);
            }

            _sql = string.Format(" select distinct a.ID, b.{0},tdkp.{1},lp.{37},lp.{38},a.{2} as MAPHONG,a.{3},b.{4} as TenPhong, lg.{5},a.{6} as IDGIUONG,a.{7} as tenGiuong,tdg.{8},"
                                + "dmtt.LOAI  as TENTINHTRANG"
                                + ", a.{9},"
                                + "tdg.MABN,"
                                + " tdg.{10} as TENBENHNHAN ,"
                                + "tdg.GIOITINH , "
                                    + " tdg.{12} as NGAYSINH,tdg.{13} as NGAYVAO "
                                + " from {14}.{15} a   "//Danh mục giường
                                + " left join {14}.{16} b on a.{17}=b.{18}  "//danh muc phòng
                                + " left join {14}.{34} lp on lp.{35}=b.{36}  "//danh muc loại phòng
                                + " left join {14}.{19} c on a.{20}=c.{21}   "//gia_vp
                                + " left join ( select "
                                + " tdg.{24}, "
                                + " tdg.{8}, "
                                + " Rtrim(Xmlagg (Xmlelement (e,   TO_CHAR(tdg.{13},'DD/MM/YYYY') "
                                + " || ' /n ')).extract('//text()'), ' /n ') as TU, "
                                + " Rtrim(Xmlagg (Xmlelement (e,   tdg.MABN "
                                + " || ' /n ')).extract('//text()'), ' /n ') as MABN, "
                                + "  Rtrim(Xmlagg (Xmlelement (e, btd.{10} "
                                + " || ' /n ')).extract('//text()'), ' /n ') as HOTEN, "
                                + " Rtrim(Xmlagg(Xmlelement(e,case btd.{11} "
                                + " when 1 "
                                + " then 'Nữ' "
                                + " when 0 "
                                + " then 'Nam' end "
                                + " || ' /n ')).extract('//text()'), ' /n ') as GIOITINH, "
                                + " Rtrim(Xmlagg(Xmlelement(e, TO_CHAR(btd.{12}, 'DD/MM/YYYY') "
                                + " || ' /n ')).extract('//text()'), ' /n ') as NGAYSINH "
                                + " from {14}." + cls_PG_TheoDoiGiuong.tb_TenBang + " tdg left join hisbvnhitp.BTDBN btd on tdg.MABN = btd.MABN where ( " + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + "='1' ) and " + cls_PG_TheoDoiGiuong.col_TRANGTHAI + "='0' "
                                + " group by   tdg.{24}, tdg.{8} "
                                + " ) tdg on a.{23}=tdg.{24}  "//theo dõi giường
                                + " left join {14}.{28} tdkp on b.{29} =  tdkp.{30}"//btdkp_bv
                                + " left join {14}.{31} lg on lg.{32} = a.{33}    "
                                + " left join HISBVNHITP.D_DMCTRANGTHAI_NEW dmtt on  a.TINHTRANG = dmtt.ID "
                                ,//danhmucloaigiuong
                                cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_TenKP, "MAPHONG", cls_PG_DanhMucGiuong.col_LOAIGIUONG,//Sửa mã phòng
                                cls_DanhMucPhong.col_TEN, cls_D_DanhMucLoaiGiuong.col_TENLOAI, cls_DanhMucPhong.col_ID, cls_PG_DanhMucGiuong.col_TEN, "TrangThai",
                                cls_PG_DanhMucGiuong.col_TINHTRANG, cls_BTDBN.col_HoTen, cls_BTDBN.col_Phai, cls_BTDBN.col_NgaySinh, cls_PG_TheoDoiGiuong.col_TU,
                                _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, "MAPHONG", "MAPHONG",//Sửa mã phòng
                                cls_V_GiaVienPhi.tb_TenBang, cls_PG_DanhMucGiuong.col_ID, cls_V_GiaVienPhi.col_ID, "",
                                cls_PG_DanhMucGiuong.col_ID, cls_PG_TheoDoiGiuong.col_IDGIUONG, cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN, cls_BTDBN.col_MaBN
                                , cls_BTDKP_BV.tb_TenBang, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_MaKP, cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_D_DanhMucLoaiGiuong.col_MALOAI,
                                cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_DanhMucLoaiPhong.tb_TenBang, cls_DanhMucLoaiPhong.col_MA, cls_DanhMucPhong.col_LOAI,
                                cls_DanhMucLoaiPhong.col_MA, cls_DanhMucLoaiPhong.col_TEN);
            _sql += where;
            DataTable tbViewTongThe = _acc.Get_Data(_sql);
            for (int i = 0; i < tbViewTongThe.Rows.Count; i++)
            {
                if (tbViewTongThe.Rows[i]["TrangThai"].ToString() == "1" && tbViewTongThe.Rows[i]["TinhTrang"].ToString() == "Có người")
                {
                    tbViewTongThe.Rows[i]["TinhTrang"] = "Chờ duyệt";
                }
            }
            return tbViewTongThe;
        }
        #endregion

        #region Kiểm tra sự tồn tại của loại giường
        public bool TonTaiLoaiGiuongODanhMucGiuong(string maLoaiGiuong)
        {
            _sql = string.Format("select * from {0}.{1} where {2} ='{3}'", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_LOAIGIUONG, maLoaiGiuong);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Kiểm tra bệnh nhân đã vào viện chưa 
        public bool TonTaihiendien(string MaBN)
        {
            _sql = string.Format("select * from {0}.{1} where {2} ='{3}'", _acc.Get_User(), cls_HienDien.tb_TenBang, cls_HienDien.col_MaBN, MaBN);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public bool KiemTraXuatGiuong(string MaBN)
        {
            DataTable dt = GetThongTinBenhNhanChoXuatGiuongfrm();
            try
            {

                if (dt.Select("MABN = '" + MaBN + "'").Length > 0)
                {
                    return true;
                }
            }
            catch (Exception ez)
            {

                return false;
            }
            return false;
        }
        #endregion
        #endregion

        #region Truy vấn form ChuyenGiuong

        #region Query lấy thông tin giường để load lên panel
        public string QueryPanelGiuong(string idGiuong)
        {
            //return _sql = string.Format("select a.id,a.stt,a.ma,a.ten as ten,a.tinhtrang,a.soluong,a.gia_th,a.gia_bh,a.gia_cs,"
            // + " a.gia_dv,a.gia_nn,a.bhyt,c.chenhlech,a.vitri,a.loaigiuong,b.MAKP,btd.TENKP,a.MAPHONG as MAPHONG,lp.MA as MALOAIPHONG  from {0}.{1} a,{0}.{2} b,{0}.{3} c,{0}.{4} btd,{0}.{7} lp"
            // + " where a.MAPHONG=b.MAPHONG and a.idgiavp=c.id and btd.MAKP=b.MAKP and a.id<>{5}  order by b.stt,a.stt",
            // _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_BTDKP_BV.tb_TenBang, idGiuong, cls_DanhMucLoaiPhong.tb_TenBang);

            return _sql = string.Format("select a.{0},a.{1},a.{2},a.{3} as ten,a.{4},a.{5},a.{6},a.{7},a.{8},"
                       + " a.{9},a.{10},a.{11},c.{12},a.{13},a.{14},b.{15},btd.{16},a.{17} as MAPHONG,lp.{18} as MALOAIPHONG  from {19}.{20} a,{19}.{21} b,{19}.{22} c,{19}.{23} btd,{19}.{24} lp"
                       + " where a.{25}=b.{26} and a.{27}=c.{28} and btd.{29}=b.{30} and a.{31}<>{32}  order by b.{33},a.{34}",
                       cls_PG_DanhMucGiuong.col_ID, cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_MA, cls_PG_DanhMucGiuong.col_TEN, cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_SOLUONG, cls_PG_DanhMucGiuong.col_GIA_TH, cls_PG_DanhMucGiuong.col_GIA_BH, cls_PG_DanhMucGiuong.col_GIA_CS,
                       cls_PG_DanhMucGiuong.col_GIA_DV, cls_PG_DanhMucGiuong.col_GIA_NN, cls_PG_DanhMucGiuong.col_BHYT, cls_V_GiaVienPhi.col_CHENHLECH, cls_PG_DanhMucGiuong.col_VITRI, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_TenKP,
                       cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucLoaiPhong.col_MA, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_BTDKP_BV.tb_TenBang, cls_DanhMucLoaiPhong.tb_TenBang,
                       cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_ID, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_PG_DanhMucGiuong.col_ID,
                       idGiuong, cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                       );
        }
        #endregion

        #region Query lấy thông tin giường theo điều kiện để load lên panel
        public string QueryPanelGiuong(string maKhoaPhong, string idGiuong)
        {
            return _sql = string.Format("select a.{0},a.{1},a.{2},a.{3} as ten,a.{4},a.{5},a.{6},a.{7},a.{8},"
                                       + " a.{9},a.{10},a.{11},c.{12},a.{13},a.{14},b.{15},btd.{16},a.{17} as MAPHONG,lp.{18} as MALOAIPHONG  from {19}.{20} a,{19}.{21} b,{19}.{22} c,{19}.{23} btd,{19}.{24} lp"
                                       + " where a.{25}=b.{26} and a.{27}=c.{28} and btd.{29}=b.{30} and a.{31}<>{32} AND lp.{33}= b.{34}  and b.{35}={36} order by b.{37},a.{38}",
                                       cls_PG_DanhMucGiuong.col_ID, cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_MA, cls_PG_DanhMucGiuong.col_TEN, cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_SOLUONG, cls_PG_DanhMucGiuong.col_GIA_TH, cls_PG_DanhMucGiuong.col_GIA_BH, cls_PG_DanhMucGiuong.col_GIA_CS,
                                       cls_PG_DanhMucGiuong.col_GIA_DV, cls_PG_DanhMucGiuong.col_GIA_NN, cls_PG_DanhMucGiuong.col_BHYT, cls_V_GiaVienPhi.col_CHENHLECH, cls_PG_DanhMucGiuong.col_VITRI, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_TenKP,
                                       cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucLoaiPhong.col_MA, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_BTDKP_BV.tb_TenBang, cls_DanhMucLoaiPhong.tb_TenBang,
                                       cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_ID, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_PG_DanhMucGiuong.col_ID,
                                       idGiuong,
                                       cls_DanhMucLoaiPhong.col_MA, cls_DanhMucPhong.col_LOAI, cls_DanhMucPhong.col_MAKP, maKhoaPhong,
                                       cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                                       );
        }
        #endregion

        #region Lấy query thông tin giường theo điều kiện khoa và phòng
        public string QueryPanelGiuong(string maKhoaPhong, string maKhoa, string idGiuong)
        {

            return _sql = string.Format("select a.{0},a.{1},a.{2},a.{3} as ten,a.{4},a.{5},a.{6},a.{7},a.{8},"
                           + " a.{9},a.{10},a.{11},c.{12},a.{13},a.{14},b.{15},btd.{16},a.{17} as MAPHONG,lp.{18} as MALOAIPHONG  from {19}.{20} a,{19}.{21} b,{19}.{22} c,{19}.{23} btd,{19}.{24} lp"
                           + " where a.{25}=b.{26} and a.{27}=c.{28} and btd.{29}=b.{30} and a.{31}<>{32} AND lp.{33}= b.{34}  and b.{35}={36} and a.{37}='{38}' order by b.{39},a.{40}",
                           cls_PG_DanhMucGiuong.col_ID, cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_MA, cls_PG_DanhMucGiuong.col_TEN, cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_SOLUONG, cls_PG_DanhMucGiuong.col_GIA_TH, cls_PG_DanhMucGiuong.col_GIA_BH, cls_PG_DanhMucGiuong.col_GIA_CS,
                           cls_PG_DanhMucGiuong.col_GIA_DV, cls_PG_DanhMucGiuong.col_GIA_NN, cls_PG_DanhMucGiuong.col_BHYT, cls_V_GiaVienPhi.col_CHENHLECH, cls_PG_DanhMucGiuong.col_VITRI, cls_PG_DanhMucGiuong.col_LOAIGIUONG, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_TenKP,
                           cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucLoaiPhong.col_MA, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_BTDKP_BV.tb_TenBang, cls_DanhMucLoaiPhong.tb_TenBang,
                           cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_ID, cls_BTDKP_BV.col_MaKP, cls_DanhMucPhong.col_MAKP, cls_PG_DanhMucGiuong.col_ID,
                           idGiuong,
                           cls_DanhMucLoaiPhong.col_MA, cls_DanhMucPhong.col_LOAI, cls_DanhMucPhong.col_MAKP, maKhoaPhong,
                           cls_DanhMucPhong.col_MA, maKhoa,
                           cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                           );
        }
        #endregion

        #region Lấy query get mã, tên khoa phòng từ bộ từ điển khoa phòng bệnh viện có gường
        public string QueryBTDKP_BV()
        {
            return _sql = string.Format("select distinct  dmkp.{0}, dmkp.{1} from"
                + " {2}.{9} dmg "
                + "left join {2}.{10} dmp on dmg.{12}=dmp.{11}"
                + " left join {2}.{3} dmkp on dmkp.{0} = dmp.{13} where dmkp.{4}<>'{5}' and dmkp.{6} in (0,4) order by dmkp.{8} ASC",
                cls_BTDKP_BV.col_MaKP, //0
                cls_BTDKP_BV.col_TenKP,//1
                _acc.Get_User(),//2
                cls_BTDKP_BV.tb_TenBang,//3
                cls_BTDKP_BV.col_MaKP,//4
                "01",//5
                cls_BTDKP_BV.col_Loai,//6
                cls_BTDKP_BV.col_Loai,//7
                cls_BTDKP_BV.col_MaKP//8
                , cls_PG_DanhMucGiuong.tb_TenBang //9
                , cls_DanhMucPhong.tb_TenBang //10
                , cls_DanhMucPhong.col_MAPHONG //11
                , cls_PG_DanhMucGiuong.col_MAPHONG //12
                , cls_DanhMucPhong.col_MAKP //13
                );
        }
        #endregion
        #region Lấy query phòng từ bộ từ điển khoa phòng bệnh viện
        public string QueryKP_BV(string makp)
        {
            return _sql = string.Format("select distinct  dmp.{0}, dmp.{1} from"
                + " {2}.{9} dmg "
                + "left join {2}.{10} dmp on dmg.{12}=dmp.{11}"
                + " where dmp.{8} = '{7}' "
                + " ORDER BY dmp.{0} ASC ",
                cls_DanhMucPhong.col_ID, //0
                cls_DanhMucPhong.col_TEN,//1
                _acc.Get_User(),//2
                cls_BTDKP_BV.tb_TenBang,//3
                cls_BTDKP_BV.col_MaKP,//4
                "01",//5
                cls_DanhMucPhong.col_STT,//6
                makp  //7
                , cls_DanhMucPhong.col_MAKP//8
                , cls_PG_DanhMucGiuong.tb_TenBang //9
                , cls_DanhMucPhong.tb_TenBang //10
                , cls_DanhMucPhong.col_MAPHONG //11
                , cls_PG_DanhMucGiuong.col_MAPHONG //12
                );
        }
        #endregion
        #region Lấy query get tất cả thông tin khoa phòng theo điều kiện phòng
        public string QueryBTDKP_BV(string idPhong)
        {
            return _sql = string.Format("select * from {0}.{1} where {2} <> {3}", _acc.Get_User(), cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, idPhong);
        }
        #endregion



        #endregion

        #region Truy vấn form DanhMucGiuong

        #region Lấy max số thứ tự danh mục giường
        public string QueryGetSTTDanhMucGiuong(string maPhong)
        {
            return _sql = string.Format("select max({0}) from {1}.{2} where {3}={4}", cls_PG_DanhMucGiuong.col_STT, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, int.Parse(maPhong));
        }
        #endregion

        #region Lấy danh mục khoa phòng từ bộ từ điển khoa phòng bệnh viện
        public DataTable GetDataDanhMucKhoa()
        {
            return _acc.Get_Data(string.Format("select * from {0}.{1} where {2}<>'{3}' and {4} in (0,4) order by {5},{6}", _acc.Get_User(), cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, "01", cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_MaKP));
        }
        #endregion

        #region Lấy max id danh mục bảng giá mới nhất
        public string GetIDBangGiaMoiNhat()
        {
            string kq = string.Empty;
            DataTable tb = _acc.Get_Data(string.Format("select max(id) from {0}.v_dmbanggia", _acc.Get_User()));
            if (tb != null)
            {
                kq = tb.Rows[0].ItemArray[0].ToString();
            }
            return kq;
        }
        #endregion

        #region Lấy tên loại giường từ mã loại
        public string GetTenLoaiGiuong(string maLoai)
        {
            string sql = string.Format("select {0} from {1}.{2} where {3}='{4}'", cls_D_DanhMucLoaiGiuong.col_TENLOAI, _acc.Get_User(), cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_D_DanhMucLoaiGiuong.col_MALOAI, maLoai);
            string tenLoai = string.Empty;
            DataTable tb = _acc.Get_Data(sql);
            try
            {
                if (tb != null)
                {
                    tenLoai = _acc.Get_Data(sql).Rows[0].ItemArray[0].ToString();
                }
            }
            catch
            {
                return "";
            }
            return tenLoai;
        }
        #endregion

        #region Lấy query giá viện phí chi tiết theo giường
        public string QueryGiaVienPhiCT(string idGiuong)
        {
            return _sql = string.Format("select dmgi.Ten,vpct.madoituong,dt.doituong,vpct.gia,vpct.id "
                        + " from {0}.v_giavp vp left join {0}.v_giavpct vpct on vp.id =vpct.id_giavp"
                        + " left join {0}.PG_DANHMUCGIUONG dmgi on dmgi.id=vp.id"
                        + " left join {0}.doituong dt on dt.madoituong=vpct.madoituong"
                        + " where dmgi.id='{1}'", _acc.Get_User(), idGiuong);
        }
        #endregion

        #region Lấy query giá gợi ý 
        public string QueryGoiYGia(string ten, string maLoaiGiuong)
        {
            return _sql = string.Format("select '{0}' as TenGiuong,lgct.{1},dt.{2},lgct.{3} as GIA"
                    + " from {4}.{5} dt "
                    + " inner join {4}.{6} lgct on lgct.{7}=dt.{8}"
                    + " where lgct.{9}='{10}'", ten, cls_DanhMucLoaiGiuongChiTiet.col_MADOITUONG, cls_DoiTuong.col_DoiTuong, cls_DanhMucLoaiGiuongChiTiet.col_GIAGIUONG,
                    _acc.Get_User(), cls_DoiTuong.tb_TenBang, cls_DanhMucLoaiGiuongChiTiet.tb_TenBang, cls_DanhMucLoaiGiuongChiTiet.col_MADOITUONG, cls_DoiTuong.col_MaDoiTuong, cls_DanhMucLoaiGiuongChiTiet.col_MALOAI,
                    maLoaiGiuong
                    );
        }

        public string QueryGoiYGia(string ten)
        {
            return _sql = string.Format("select distinct '{0}' as TenGiuong,dt.{1},dt.{2},'0' as GIA"
                    + " from {3}.{4} dt "
                    + ""
                    + "", ten, cls_DoiTuong.col_MaDoiTuong, cls_DoiTuong.col_DoiTuong, _acc.Get_User(), cls_DoiTuong.tb_TenBang);
        }
        #endregion

        #region Lấy query danh mục giường
        public string Queryusc_SelectBoxPhongChange(string maKhoa, string maPhong)
        {
            return _sql = string.Format("select a.id,a.stt,a.ma,a.ten as ten,a.tinhtrang,a.soluong,a.gia_th,a.gia_bh,a.gia_cs, a.gia_dv,a.gia_nn,a.bhyt,c.chenhlech,a.vitri"
                                        + " from {0}.PG_DANHMUCGIUONG a,{0}.DANHMUCPHONG b,{0}.V_GIAVP c "
                                        + "where a.MAPHONG=b.MAPHONG and a.idgiavp=c.id and b.makp={1} and b.ma='{2}' order by b.stt,a.stt", _acc.Get_User(), maKhoa, maPhong);
            //Sửa mã
        }
        #endregion

        #region Kiểm tra trạng thái của giường
        public string TrangThaiGiuong(string idGiuong)
        {
            string kq = "";
            _sql = string.Format("select {0} from {1}.{2} where {3}={4}", cls_PG_DanhMucGiuong.col_TINHTRANG, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_ID, idGiuong);
            DataTable tb = _acc.Get_Data(_sql);
            if (tb.Rows.Count > 0)
            {
                kq = tb.Rows[0][0].ToString();
            }
            return kq;
        }
        #endregion

        #region Lấy danh mục giá viện phí 
        public DataTable GetDanhMucGiaVienPhi()
        {
            _sql = string.Format("select * from {0}.{1}  "
                    + " ", _acc.Get_User(), cls_V_GiaVienPhi.tb_TenBang);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Thay đổi giá tiền trong Danh mục giường
        public string GetGiaTienTuGoiY(string idgiavp, string madoituong, string ma, int Loai = 3, string op1 = null, string op2 = null)
        {
            string kq = String.Empty;
            DataTable tb = null;
            int lanload = 0;

            string magiuong = "";
            //   for (; 0 <= Loai||(tb!=null && tb.Rows.Count>0); Loai--)
            if (!string.IsNullOrEmpty(ma))
            {

                DataTable dttmp = null;
                while (true)
                {
                    if (Loai == 3)
                    {
                        magiuong = ma;
                    }
                    if (lanload > 0)
                    {
                        if (Loai == 2)
                        {
                            if (lanload > 1 && !string.IsNullOrEmpty(op1))
                            {
                                #region lấy loại giường
                                _sql = String.Format("select {0} " +
                                                "from {1}.{2} where {3} = '{4}'  ",
                                                cls_PG_DanhMucGiuong.col_LOAIGIUONG,    //0
                                                _acc.Get_User(),         //1
                                                cls_PG_DanhMucGiuong.tb_TenBang,//2 
                                                cls_PG_DanhMucGiuong.col_ID,//3
                                                magiuong
                                                );
                                dttmp = _acc.Get_Data(_sql);
                                if (dttmp != null && dttmp.Rows.Count > 0)
                                {
                                    ma = dttmp.Rows[0][0] + "";
                                }
                                else
                                {
                                    TA_MessageBox.MessageBox.Show("Không thể tìm thấy mã Phòng " + ma + " !", TA_MessageBox.MessageIcon.Error);
                                    break;
                                }
                                #endregion

                            }
                            else
                            {
                                ma = op1;
                            }
                        }
                        if (Loai == 1)
                        {
                            if (lanload > 2 && !string.IsNullOrEmpty(op2))
                            {
                                #region lấy phòng
                                _sql = String.Format("select {0} " +
                                              "from {1}.{2} where {3} in ( select  {4} from {1}.{5} where {6} = '{7}' )  ",
                                              cls_DanhMucPhong.col_ID,    //0
                                              _acc.Get_User(),         //1
                                              cls_DanhMucPhong.tb_TenBang,//2 
                                              cls_DanhMucPhong.col_MAPHONG,//3
                                               cls_PG_DanhMucGiuong.col_MAPHONG,//4
                                                 cls_PG_DanhMucGiuong.tb_TenBang,//5
                                                      cls_PG_DanhMucGiuong.col_ID,//6
                                               magiuong
                                              );
                                dttmp = _acc.Get_Data(_sql);
                                if (dttmp != null && dttmp.Rows.Count > 0)
                                {
                                    ma = dttmp.Rows[0][0] + "";
                                }
                                else
                                {
                                    TA_MessageBox.MessageBox.Show("Không thể tìm thấy mã Phòng " + ma + " !", TA_MessageBox.MessageIcon.Error);
                                    break;
                                }
                                #endregion
                            }
                            else
                            {
                                _sql = String.Format("select {0} " +
                                              "from {1}.{2} where {3} = '{4}' ",
                                              cls_DanhMucPhong.col_ID,    //0
                                              _acc.Get_User(),         //1
                                              cls_DanhMucPhong.tb_TenBang,//2 
                                              cls_DanhMucPhong.col_MAPHONG,//3
                                               op2
                                              );
                                dttmp = _acc.Get_Data(_sql);
                                if (dttmp != null && dttmp.Rows.Count > 0)
                                {
                                    ma = dttmp.Rows[0][0] + "";
                                }
                                else
                                {
                                    TA_MessageBox.MessageBox.Show("Không thể tìm thấy mã Phòng " + ma + " !", TA_MessageBox.MessageIcon.Error);
                                    break;
                                }

                            }
                        }
                        if (Loai == 0)
                        {
                            if (lanload > 1 && !string.IsNullOrEmpty(op1))
                            {
                                #region Lấy loại Phòng
                                _sql = String.Format("select {0} " +
                                              "from {1}.{2} where {3} = '{4}'  ",
                                              cls_DanhMucPhong.col_LOAI,    //0
                                              _acc.Get_User(),         //1
                                              cls_DanhMucPhong.tb_TenBang,//2 
                                              cls_DanhMucPhong.col_ID,//3
                                              ma
                                              );
                                dttmp = _acc.Get_Data(_sql);
                                if (dttmp != null && dttmp.Rows.Count > 0)
                                {
                                    ma = dttmp.Rows[0][0] + "";
                                }
                                else
                                {
                                    TA_MessageBox.MessageBox.Show("Không thể tìm thấy mã loại phòng " + ma + " !", TA_MessageBox.MessageIcon.Error);
                                    break;
                                }
                                #endregion
                            }
                            else
                            {
                                ma = op1;
                            }
                        }

                    }



                    _sql = String.Format("select {0} " +
                           "from {1}.{2} where {3} = '{4}' AND {5} = '{10}' AND {6} = '{7}' AND {8} = '{9}' ",
                           cls_GiaGiuong.col_GIA,    //0
                           _acc.Get_User(),         //1
                           cls_GiaGiuong.tb_TenBang,//2 
                           cls_GiaGiuong.col_IDGIAVP,//3
                           idgiavp,                 //4 
                           cls_GiaGiuong.col_LOAI,  //5
                           cls_GiaGiuong.col_DOITUONG,//6
                           madoituong,                //7
                           cls_GiaGiuong.col_MA,    //8
                           ma,                       //9
                           Loai + ""                  //10
                           );
                    tb = _acc.Get_Data(_sql);
                    if (Loai == 0 || (tb != null && tb.Rows.Count > 0) || (Loai == 2 && lanload == 0))
                    {
                        break;
                    }
                    lanload++;
                    Loai--;
                }
            }
            if (tb != null && tb.Rows.Count > 0)
            {
                kq = tb.Rows[0][0].ToString();
            }
            else
            {
                tb = GetDataGia(idgiavp, madoituong);
                kq = tb.Rows[0][0].ToString();
            }
            return kq;

        }
        #endregion

        #endregion

        #region Truy vấn form DanhMucPhong

        #region Lấy max id danh mục phòng
        public string GetIdDanhMucPhongTuDong()
        {
            string kq = string.Empty;
            DataTable tb = _acc.Get_Data(string.Format("select max({0})+1 from {1}.{2}", cls_DanhMucPhong.col_ID, _acc.Get_User(), cls_DanhMucPhong.tb_TenBang));
            if (tb != null)
            {
                kq = tb.Rows[0].ItemArray[0].ToString() == "" ? "1" : tb.Rows[0].ItemArray[0].ToString();
            }
            return kq;

        }
        #endregion


        #region Lấy ten phòng
        public string GetPhongByGiuong(string idgiuong)
        {
            string kq = string.Empty;
            DataTable tb = _acc.Get_Data(string.Format("select max({0})+1 from {1}.{2}", cls_DanhMucPhong.col_ID, _acc.Get_User(), cls_DanhMucPhong.tb_TenBang));
            if (tb != null)
            {
                kq = tb.Rows[0].ItemArray[0].ToString() == "" ? "1" : tb.Rows[0].ItemArray[0].ToString();
            }
            return kq;

        }
        #endregion

        #region Lấy mã phòng theo mã giường vannq 02042019
        public string GetMaPhongByMaGiuong(string magiuong)
        {
            string kq = string.Empty;

            try
            {
                DataTable tb = _acc.Get_Data($"select {cls_PG_DanhMucGiuong.col_MAPHONG} from {_acc.Get_User()}.{cls_PG_DanhMucGiuong.tb_TenBang} where {cls_PG_DanhMucGiuong.col_ID} = '{magiuong}'");
                if (tb != null)
                {
                    kq = tb.Rows[0].ItemArray[0].ToString();
                }
                return kq;
            }
            catch
            {
                return kq;
            }

        }
        #endregion

        #region Lấy id giuong theo mã giường vannq 02042019
        public string GetIDGiuongByMaGiuong(string magiuong)
        {
            string kq = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(magiuong)) return kq;
                DataTable tb = _acc.Get_Data($"select {cls_DanhMucGiuong.col_ID} from {_acc.Get_User()}.{cls_DanhMucGiuong.tb_TenBang} where {cls_DanhMucGiuong.col_MAGIUONG} = '{magiuong}'");
                if (tb != null)
                {
                    kq = tb.Rows[0].ItemArray[0].ToString();
                }
                return kq;
            }
            catch
            {
                return kq;
            }
        }
        #endregion

        #region Lấy max mã danh mục phòng
        public string GetMaDanhMucPhongTuDong()
        {
            string kq = string.Empty;
            DataTable tb = _acc.Get_Data(string.Format("select max({0})+1 from {1}.{2}", cls_DanhMucPhong.col_MA, _acc.Get_User(), cls_DanhMucPhong.tb_TenBang));
            if (tb != null)
            {
                kq = tb.Rows[0].ItemArray[0].ToString() == "" ? "1" : tb.Rows[0].ItemArray[0].ToString();
            }
            return kq;
        }
        #endregion

        #region Thêm danh mục phòng
        public void InsertDanhMucPhong(string maKP, string id, string sTT, string ma, string ten, string loai, string loaiVP, string giaPhong, DateTime ngayUd, string machineId, string maPhong)
        {
            if (loaiVP == string.Empty)
            {
                loaiVP = "28";
            }
            //Chuyển cột mã sang cột mã phòng
            _sql = string.Format("insert into {0}.danhmucphong (makp,id,stt,ma,ten,loai,loaivp,giaphong,ngayud,machineid,maphong)"
                                + " values ('{1}',{2},{3},'{4}','{5}',{6},{7},'{8}',to_date('{9}','dd/MM/yyyy hh24:mi:ss'),'{10}','{11}')",
                                _acc.Get_User(), maKP, id, sTT,
                                ma, ten, loai, loaiVP, giaPhong, ngayUd.ToString("dd/MM/yyyy HH:mm:ss"), machineId, maPhong);

            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show("Không thể thêm!", TA_MessageBox.MessageIcon.Error);
                return;
            }
        }
        #endregion

        #region Lấy query tìm kiếm phòng
        public string QueryTimKiemDanhMucPhong(string maKhoa, string chuoiTimKiem)
        {
            if (!string.IsNullOrEmpty(chuoiTimKiem))
            {
                if (!string.IsNullOrEmpty(maKhoa))
                {
                    return _sql = string.Format("select dmp.ID,  dmp.STT,  dmp.MAPHONG,  dmp.TEN,  dmp.LOAI,lp.TEN as TenLoaiPhong, dmp.LOAIVP,lvp.TEN as TenLoaiVienPhi,dmp.GIAPHONG,dmp.MAKP,kp.TENKP,dmp.CHUAN FROM {0}.{1} dmp"
                                + " left join {0}.dmloaiphong lp on lp.ID=dmp.LOAI "
                                + " left join {0}.v_loaivp lvp on lvp.ID=dmp.LOAIVP "
                                + " left join {0}.btdkp_bv kp on kp.MAKP = dmp.MAKP "
                                + " WHERE dmp.MAKP like '%{2}%' and (dmp.TEN like '%{3}%' or dmp.MA like '%{3}%') order by dmp.stt",
                                _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, maKhoa, chuoiTimKiem);
                }
                else
                {
                    return _sql = string.Format("select dmp.ID,  dmp.STT,  dmp.MAPHONG,  dmp.TEN,  dmp.LOAI,lp.TEN as TenLoaiPhong, dmp.LOAIVP,lvp.TEN as TenLoaiVienPhi,dmp.GIAPHONG,dmp.MAKP,kp.TENKP,dmp.CHUAN FROM {0}.{1} dmp"
                                + " left join {0}.dmloaiphong lp on lp.ID=dmp.LOAI "
                                + " left join {0}.v_loaivp lvp on lvp.ID=dmp.LOAIVP "
                                + " left join {0}.btdkp_bv kp on kp.MAKP = dmp.MAKP "
                                + " WHERE dmp.TEN like '%{2}%' or dmp.MA like '%{2}%' order by dmp.stt",
                                _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, chuoiTimKiem);

                    //          return _sql = string.Format("select dmp.ID, dmp.STT, dmp.MAPHONG,dmp.TEN,dmp.LOAI,lp.TEN as TenLoaiPhong,dmp.LOAIVP,lvp.TEN as TenLoaiVienPhi,dmp.GIAPHONG,dmp.MAKP,kp.TENKP,dmp.CHUAN"
                    //+ " FROM hisbvnhitp.DANHMUCPHONG dmp "
                    //+ " left join hisbvnhitp.dmloaiphong lp on lp.ID = dmp.LOAI "
                    //+ " left join hisbvnhitp.v_loaivp lvp on lvp.ID = dmp.LOAIVP "
                    //+ " left join hisbvnhitp.btdkp_bv kp on kp.MAKP = dmp.MAKP "
                    //+ " left join HISBVNHITP.GIAGIUONG gg on gg.MA = dmp.MA "
                    //+ " order by dmp.stt ", _acc.Get_User(), chuoiTimKiem);
                }


            }
            else
            {
                return _sql = string.Format("select dmp.ID,  dmp.STT,  dmp.MAPHONG,  dmp.TEN,  dmp.LOAI,lp.TEN as TenLoaiPhong, dmp.LOAIVP,lvp.TEN as TenLoaiVienPhi,dmp.GIAPHONG,dmp.MAKP,kp.TENKP,dmp.CHUAN FROM {0}.{1} dmp"
                                + " left join {0}.dmloaiphong lp on lp.ID=dmp.LOAI "
                                + " left join {0}.v_loaivp lvp on lvp.ID=dmp.LOAIVP "
                                + " left join {0}.btdkp_bv kp on kp.MAKP = dmp.MAKP "
                                + " order by dmp.stt",
                                _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, maKhoa, chuoiTimKiem);
            }

        }
        #endregion

        #region Lấy query tìm kiếm phòng Import
        public string QueryTimKiemDanhMucPhongImport(string maKhoa, string chuoiTimKiem)
        {
            return _sql = string.Format("select dmp.STT,  dmp.MAPHONG,  dmp.TEN,  dmp.LOAI,dmp.MAKP,dmp.CHUAN  FROM {0}.{1} dmp"
                                + " left join {0}.dmloaiphong lp on lp.ID=dmp.LOAI "
                                + " left join {0}.v_loaivp lvp on lvp.ID=dmp.LOAIVP "
                                + " left join {0}.btdkp_bv kp on kp.MAKP = dmp.MAKP "
                                + " WHERE dmp.MAKP like '%{2}%' and (dmp.TEN like '%{3}%' or dmp.MA like '%{3}%') order by dmp.stt",
                                _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, maKhoa, chuoiTimKiem);
        }
        #endregion

        #region Kiểm tra trùng số thứ tự
        public bool TrungSoTTKhoaPhong(string maKhoa, string stt)
        {
            _sql = string.Format("select dmp.ID,  dmp.STT,  dmp.MA,  dmp.TEN,  dmp.LOAI,lp.TEN as TenLoaiPhong, dmp.LOAIVP,lvp.TEN as TenLoaiVienPhi,dmp.GIAPHONG,dmp.MAKP,kp.TENKP FROM {0}.{1} dmp"
                                           + " left join {0}.dmloaiphong lp on lp.ID=dmp.LOAI "
                                           + " left join {0}.v_loaivp lvp on lvp.ID=dmp.LOAIVP "
                                           + " left join {0}.btdkp_bv kp on kp.MAKP = dmp.MAKP "
                                           + " WHERE dmp.MAKP like '%{2}%' and dmp.STT = '{3}' order by dmp.stt",
                                           _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, maKhoa, stt);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Lấy query danh mục bộ từ điển khoa phòng
        public string QueryDanhMucKhoaPhong()
        {
            return _sql = string.Format("select * from {0}.{1} where {2}<>'01' and {3} in (0,4) order by {4},{5}", _acc.Get_User(), cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_MaKP);
        }
        #endregion

        #region Lấy danh mục phòng từ khoa
        public DataTable GetDataDanhMucPhong(string maKhoa)
        {
            return _acc.Get_Data(string.Format("select {0},  {1},  {2},  {3},  {4},  {5},{6} FROM {7}.{8} WHERE {9} ='{10}' order by {11}", cls_DanhMucPhong.col_ID, cls_DanhMucPhong.col_STT,
                cls_DanhMucPhong.col_MA, cls_DanhMucPhong.col_TEN, cls_DanhMucPhong.col_LOAI, cls_DanhMucPhong.col_LOAIVP, cls_DanhMucPhong.col_GIAPHONG, _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, cls_DanhMucPhong.col_MAKP, maKhoa, cls_DanhMucPhong.col_STT));
        }
        #endregion

        #region Lấy danh mục phòng (co giuong) từ khoa vannq 02042019
        public DataTable GetDataDanhMucPhongCoGiuong(string maKhoa)
        {
            _sql = $@"select {cls_DanhMucPhong.col_MAPHONG},  {cls_DanhMucPhong.col_TEN} 
                      FROM {_acc.Get_User()}.{cls_DanhMucPhong.tb_TenBang} 
                      WHERE {cls_DanhMucPhong.col_MAKP} ='{maKhoa}' 
                      and {cls_DanhMucPhong.col_MAPHONG} in (select distinct {cls_DanhMucGiuong.col_MAPHONG} 
                                                             from {_acc.Get_User()}.{cls_DanhMucGiuong.tb_TenBang}
                                                             where {cls_DanhMucGiuong.col_TINHTRANG} in ('0','1','2','5')
                                                            )
                      order by {cls_DanhMucPhong.col_ID}";
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy danh mục giuong (trong) từ khoa vannq 02042019
        public DataTable GetDataDanhMucGiuongTrong(string maPhong)
        {
            _sql = $@"select {cls_PG_DanhMucGiuong.col_ID}, {cls_PG_DanhMucGiuong.col_TEN} 
                      from {_acc.Get_User()}.{cls_DanhMucGiuong.tb_TenBang}
                      where {cls_PG_DanhMucGiuong.col_MAPHONG} = '{maPhong}'
                      and {cls_PG_DanhMucGiuong.col_TINHTRANG}  in ('0','1','2','5')

                      order by {cls_PG_DanhMucGiuong.col_ID}";
            //                      and {cls_DanhMucGiuong.col_ID} not in ( select {cls_PG_TheoDoiGiuong.col_IDGIUONG} 
            //from { _acc.Get_User()}.{ cls_PG_TheoDoiGiuong.tb_TenBang}
            //where { cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN}
            //not in (1, 2)
            //                                                )
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy thông tin loại viện phí
        public DataTable GetDatausc_SelectBoxLoaiVienPhi(string maLoaiVienPhi)
        {
            return _acc.Get_Data(string.Format("select gvp.{0},gvp.{1},gvp.{2} from {3}.{4} lvp"
                              + " inner join {3}.{5} gvp on lvp.{6}=gvp.{7} where gvp.{8} ={9}",
                              cls_V_GiaVienPhi.col_ID, cls_V_GiaVienPhi.col_TEN, cls_V_GiaVienPhi.col_GIA_TH, _acc.Get_User(), cls_V_LoaiVienPhi.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_V_GiaVienPhi.col_ID_LOAI,
                              cls_V_LoaiVienPhi.col_ID, cls_V_GiaVienPhi.col_ID_LOAI, maLoaiVienPhi
                              ));
        }
        #endregion

        #endregion

        #region Truy vấn form DanhSachBenhNhanChoDuyet

        #region Lấy số lượng bệnh nhân chờ duyệt 
        public DataTable GetDanhSachChoDuyet(string idGiuong)
        {
            _sql = string.Format("select bt.{0},bt.{1} "
            + " from  {2}.{4} td left join {2}.{3} bt on td.{5}=bt.{0}"
            + " where td.{6}='{7}' AND td.{8}='0' and  td.{9}='0' ", cls_BTDBN.col_MaBN, cls_BTDBN.col_HoTen, _acc.Get_User(), cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN,
            cls_PG_TheoDoiGiuong.col_IDGIUONG, idGiuong, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI);
            return _acc.Get_Data(_sql);
        }

        public DataTable RowThongTinTheoDoiGiuong(string idGiuong)
        {
            _sql = string.Format("select * from {0}.{2} where {3} = {1} and {4} ='1' and {5} ='0' ", _acc.Get_User(), idGiuong, cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_IDGIUONG, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI);
            return _acc.Get_Data(_sql);
        }
        public DataTable RowThongTinTheoDoiGiuongbyid(string id)
        {
            _sql = string.Format("select * from {0}.{2} where {3} = {1} ", _acc.Get_User(), id, cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_ID);
            return _acc.Get_Data(_sql);
        }
        public DataTable GetDanhSachDaDuyet(string idGiuong)
        {
            _sql = string.Format("select bt.{0},bt.{1} "
            + " from {2}.{4} td left join {2}.{3} bt on td.{5}=bt.{0}"
            + " where td.{6}={7} and td.{4} ='1' and td.{5} ='0' ", cls_BTDBN.col_MaBN, cls_BTDBN.col_HoTen, _acc.Get_User(), cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN,
            cls_PG_TheoDoiGiuong.col_IDGIUONG, idGiuong, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI);
            return _acc.Get_Data(_sql);
        }

        public DataTable GetDanhSachDatVaDaDuyet(string idGiuong = "", string makhoa = "", string IDPHONG = "",string loai="")
        {
            string where = " WHERE td." + cls_PG_TheoDoiGiuong.col_TRANGTHAI + "='0' ";
            if (!string.IsNullOrEmpty(makhoa))
            {
                where += " AND dmp.{14} = '" + makhoa + "' ";
            }
            if (!string.IsNullOrEmpty(IDPHONG) && IDPHONG != "-1")
            {
                where += " AND dmp.{21} = '" + IDPHONG + "' ";
            }
            if (!string.IsNullOrEmpty(idGiuong))
            {
                where += " AND td.{6} = '" + idGiuong + "' ";
            }
            if (!string.IsNullOrEmpty(loai))
            {
                where += " AND td."+cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN+ " = '" + loai + "' ";
            }
            //if (!string.IsNullOrEmpty(where))
            //{
            //    where= " where " + where.Substring(4);
            //}

            _sql = string.Format("select td.{22}, bt.{0},bt.{1}, td.{6} as IDGIUONG, dmp.{13} as MAPHONG , dmp.{20} as TENPHONG, dmg.{17} as TENGIUONG ,tdkp.{16} , tdkp.{18},"
               +" tdbn.sonha || ' ' || tdbn.thon || ' ' || pxa.tenpxa || ', ' || quan.tenquan || ', ' || tt.tentt as diachi, "
               + "  case when td." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " =0   then 1 "
               + " when  td." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = 1   then 2 "
               + " when  td." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = 2   then 3 "
               + " end "
               + " as Loai, "
               + "  case when td." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = 0   then 'Đã Đặt' "
               + " when  td." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = 1   then 'Đã Duyệt' "
               + " when  td." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + " = 2   then 'Chờ Xuất Viện' "
               + " end "
               + " as TenLoai "
                + ",td.{19}  "
            + " from {2}.{3} bt inner join {2}.{4} td on td.{5}=bt.{0} "
                 + " left join {2}.{8} dmg on dmg.{9}=td.{10} "
                 + " left join {2}.{11} dmp on dmg.{12}=dmp.{13} "
                 + " left join {2}.{15} tdkp on tdkp.{16}=dmp.{14} "
                   + "  left join hisbvnhitp.BTDBN tdbn on tdbn.MABN = td.MABN "
                                + " LEFT JOIN hisbvnhitp.btdpxa pxa on tdbn.maphuongxa =  pxa.maphuongxa "
                                + "  LEFT JOIN hisbvnhitp.btdquan quan on tdbn.maqu =  quan.maqu "
                                + "  LEFT JOIN hisbvnhitp.btdtt tt on tdbn.matt =  tt.matt "
            + where, cls_BTDBN.col_MaBN,//0
            cls_BTDBN.col_HoTen,
            _acc.Get_User(),
            cls_BTDBN.tb_TenBang,
            cls_PG_TheoDoiGiuong.tb_TenBang,
            cls_PG_TheoDoiGiuong.col_MABN,//5
            cls_PG_TheoDoiGiuong.col_IDGIUONG,
            ""
            , cls_PG_DanhMucGiuong.tb_TenBang
            , cls_PG_DanhMucGiuong.col_ID
            , cls_PG_TheoDoiGiuong.col_IDGIUONG//10
            , cls_DanhMucPhong.tb_TenBang
            , cls_PG_DanhMucGiuong.col_MAPHONG
            , cls_DanhMucPhong.col_MAPHONG
            , cls_DanhMucPhong.col_MAKP
            , cls_BTDKP_BV.tb_TenBang//15
            , cls_BTDKP_BV.col_MaKP
            , cls_PG_DanhMucGiuong.col_TEN
            , cls_BTDKP_BV.col_TenKP
            , cls_PG_TheoDoiGiuong.col_TU
            , cls_DanhMucPhong.col_TEN//20
            , cls_DanhMucPhong.col_ID
            , cls_PG_TheoDoiGiuong.col_ID//22
            );



            return _acc.Get_Data(_sql);
        }
        public DataTable GetDanhSachChoDuyet()
        {
            _sql = string.Format("select bt.{0},bt.{1}, td.{6} "
            + " from  {2}.{4} td left join {2}.{3} bt on td.{5}=bt.{0}"
            + " where  td.{7} ='0' and  td.{8} ='0' ", cls_BTDBN.col_MaBN, cls_BTDBN.col_HoTen, _acc.Get_User(), cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN,
            cls_PG_TheoDoiGiuong.col_IDGIUONG, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI);
            return _acc.Get_Data(_sql);
        }
        #endregion


        #region Kiểm tra trạng thái giường đã có người hay chưa

        public bool GiuongDaCoNguoiNam(string idGiuong)
        {
            _sql = string.Format("SELECT {0} FROM {1}.{2} where {3} = '{4}' AND {5} = '1' AND {6} = '0'", cls_PG_TheoDoiGiuong.col_ID,
                                 _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_IDGIUONG, idGiuong, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI);
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        #endregion



        #region Lấy số lượng người nằm trên giường

        public int GetSoLuongNguoiNamTaiGiuong(string idGiuong)
        {
            int soLuong = 0;
            DataTable tb;
            _sql = string.Format("SELECT count(*) FROM {0}.{1} where ({2} = '{3}' OR {4} = '1') AND {5} = '0' ",
                                 _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_IDGIUONG, idGiuong, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI);
            tb = _acc.Get_Data(_sql);

            if (tb.Rows.Count > 0)
            {
                int.TryParse(tb.Rows[0][0].ToString(), out soLuong);
            }
            return soLuong;
        }
        public DataTable GetSoLuongNguoiNamTaiGiuong()
        {

            DataTable tb;
            _sql = string.Format("SELECT {2} FROM {0}.{1} Where ({3} = '2' OR {3} = '1' ) AND {4} = '0' ",
                _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_PG_TheoDoiGiuong.col_IDGIUONG, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI);
            return _acc.Get_Data(_sql);
        }

        #endregion



        #region Cập nhật tình trạng cho giường
        public bool UpdateTinhTrangDanhMucGiuong(string idGiuong, string tinhTrang)
        {
            _sql = string.Format("select {2} from {0}.{1} where {4}={5} AND {2} = 5 AND {2} <> 4", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_TINHTRANG, tinhTrang, cls_PG_DanhMucGiuong.col_ID, idGiuong);
            DataTable dt = _acc.Get_Data(_sql);
            if (dt!=null && dt.Rows.Count>0)
            {
              
                return true;

            }
            _sql = string.Format("update {0}.{1} set {2} = {3} where {4}={5} AND {2} <> 5 AND {2} <> 4", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_TINHTRANG, tinhTrang, cls_PG_DanhMucGiuong.col_ID, idGiuong);
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                
                    TA_MessageBox.MessageBox.Show("Không thể duyệt cho bệnh nhân này!", TA_MessageBox.MessageIcon.Error);
                    return false; 
                
            }
            return true;
        }
        public bool UpdateTinhTrangDanhMucGiuongxUATgIUONG(string idGiuong, string tinhTrang)
        {
            _sql = string.Format("update {0}.{1} set {2} = {3} where {4}={5} AND {2} <> 4", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_TINHTRANG, tinhTrang, cls_PG_DanhMucGiuong.col_ID, idGiuong);
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show("Không thể duyệt cho bệnh nhân này!", TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        #endregion



        public bool UpdateTheoDoiGiuong(string id, string mabn = "", string idgiuong = "", string makp = "", string trangthaibn = "", string Tu = "", string Den = "", string MAQL = "", string MAVV = "", string MADT = "", bool issudung = true)
        {
            string update = cls_PG_TheoDoiGiuong.col_TRANGTHAI + "= '" + (issudung ? "0" : "1") + "' ";
            if (!string.IsNullOrEmpty(mabn))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_MABN + "= '" + mabn + "' ";
            }
            if (!string.IsNullOrEmpty(trangthaibn))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + "= '" + trangthaibn + "' ";
            }
            if (!string.IsNullOrEmpty(Tu))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_TU + "= TO_DATE('" + Tu + "','dd/MM/yyyy hh24:mi:ss')";
            }
            if (!string.IsNullOrEmpty(Den))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_DEN + "= TO_DATE('" + Den + "','dd/MM/yyyy hh24:mi:ss')";
            }
            if (!string.IsNullOrEmpty(MAQL))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_MAQL + "= '" + MAQL + "' ";

            }
            if (!string.IsNullOrEmpty(MAVV))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_MAVAOVIEN + "= '" + MAVV + "' ";
            }
            if (!string.IsNullOrEmpty(MADT))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_MADOITUONG + "= '" + MADT + "' ";
            }
            if (!string.IsNullOrEmpty(idgiuong))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_IDGIUONG + "= '" + idgiuong + "' ";
            }
            if (!string.IsNullOrEmpty(makp))
            {
                update += ", " + cls_PG_TheoDoiGiuong.col_MAKP + "= '" + makp + "' ";
            }

            _sql = string.Format("update {0}.{1} set " + update + "  where {4}='{5}'", _acc.Get_User()
                , cls_PG_TheoDoiGiuong.tb_TenBang//1
                , cls_PG_TheoDoiGiuong.col_MAKP//2
                , makp//3
                , cls_PG_TheoDoiGiuong.col_ID//4
                , id//5
                , cls_PG_TheoDoiGiuong.col_IDGIUONG//6
                , idgiuong//7
                );
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show("Không thể update trạng thái bệnh nhân trong table theodoigiuong!"
                   , TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        public DateTime GetSysDate()
        {
            _sql = "select sysdate from dual";
            return DateTime.Parse((_acc.Get_Data(_sql)).Rows[0][0] + "");
        }
        #endregion


        #region Truy vấn form DanhSachDen

        #region Lấy danh sách bệnh nhân có trong danh sách theo dõi giường
        public DataTable GetDataTimKiem()
        {

            return _acc.Get_Data(string.Format("select btd.{0},btd.{1} from {2}.{3} tdg inner join {2}.{4} btd on tdg.{5}=btd.{0} and  tdg.{6}='1' and  tdg.{7}='0' ",
                                 cls_BTDBN.col_MaBN, cls_BTDBN.col_HoTen, _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI));
        }
        #endregion

        #region Lấy thông tin bệnh nhân trong danh sách đen theo bệnh nhân
        public DataTable GetDanhSachBlackList(string maBN)
        {
            return _acc.Get_Data(string.Format("select btd.{0},btd.{1} from {2}.{3} tdg inner join {2}.{4} btd"
                                + " on tdg.{5}=btd.{0} where btd.{0}={6} and tdg.{7}='1' and tdg.{8}='0' ", cls_BTDBN.col_MaBN, cls_BTDBN.col_HoTen, _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_BTDBN.tb_TenBang,
                                cls_PG_TheoDoiGiuong.col_MABN, maBN, cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN, cls_PG_TheoDoiGiuong.col_TRANGTHAI));
        }
        #endregion

        #endregion

        #region Lấy max id danh mục giuong
        public string GetMaxIDDanhMucGiuong()
        {
            string kq = string.Empty;
            DataTable tb = _acc.Get_Data(string.Format("select max(id)+1 from {0}.{1}", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang));
            if (tb != null)
            {
                kq = tb.Rows[0].ItemArray[0].ToString() == "" ? "1" : tb.Rows[0].ItemArray[0].ToString();
            }
            return kq;
        }
        #endregion

        #region Truy vấn form LoaiGiuong

        #region Lấy max id danh mục chi tiết loại giường
        public string GetTuDongIDLoaiGiuongCT()
        {
            string kq = string.Empty;
            _sql = string.Format("Select max({0})+1 from {1}.{2}", cls_DanhMucLoaiGiuongChiTiet.col_ID, _acc.Get_User(), cls_DanhMucLoaiGiuongChiTiet.tb_TenBang);
            DataTable tb = _acc.Get_Data(_sql);
            if (tb != null)
            {
                kq = tb.Rows[0].ItemArray[0].ToString() == "" ? "1" : tb.Rows[0].ItemArray[0].ToString();
            }
            return kq;
        }
        #endregion

        #region Lấy dữ liệu gợi ý để nhập giá tiền theo loại viện phí là giường
        public DataTable GetDataGoiY()
        {
            //_sql = string.Format("select gvp.{0},gvp.{1},gvp.{2} from {3}.{4} lvp"
            //                + " inner join {3}.{5} gvp on lvp.{6}=gvp.{7} where gvp.{8} ={9}", cls_V_GiaVienPhi.col_ID, cls_V_GiaVienPhi.col_TEN, cls_V_GiaVienPhi.col_GIA_TH,
            //                 _acc.Get_User(), cls_V_LoaiVienPhi.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_V_LoaiVienPhi.col_ID, cls_V_GiaVienPhi.col_ID_LOAI, cls_V_GiaVienPhi.col_ID_LOAI, "28");
            //Sửa giá theo thông tư 15
            _sql = string.Format(" select gvp.TEN,b.GIA,gvp.MA "
                                + " from {0}.v_dmbanggia a inner join  {0}.v_giavpct b  on a.id=b.id_banggia"
                                + " inner join {0}.V_GIAVP gvp on gvp.id=b.id_giavp"
                                + " where gvp.ID_LOAI='28' and b.id_banggia=(SELECT ID  FROM {0}.V_DMBANGGIA WHERE a.APDUNG = 1 AND a.NGAYAD < SYSDATE  AND rownum = 1) and b.GIA>0 Group by gvp.TEN,b.GIA,gvp.MA ", _acc.Get_User());


            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy id gợi ý để nhập giá tiền không theo từng loại viện phí
        public DataTable GetDataGoiYGiaVienPhi(string maLoaiGiuong)
        {
            _sql = string.Format("select gvp.{0},gvp.{1},gvp.{2},gvp.{3},gvp.{4} from {5}.{6} lvp"
                            + " inner join {5}.{7} gvp on lvp.{8}=gvp.{9} where {9}=28", cls_V_GiaVienPhi.col_ID, cls_V_GiaVienPhi.col_TEN, cls_V_GiaVienPhi.col_GIA_TH,
                            cls_V_GiaVienPhi.col_GIA_BH, cls_V_GiaVienPhi.col_GIA_DV,
                             _acc.Get_User(), cls_V_LoaiVienPhi.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_V_LoaiVienPhi.col_ID, cls_V_GiaVienPhi.col_ID_LOAI,
                             cls_V_GiaVienPhi.col_ID_LOAI
                             );
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy data để kiểm tra cập nhật theo mã loại
        public DataTable GetDataCheckUpdate(string maLoai)
        {
            _sql = string.Format("select lpct.{0} as MADT,dt.{1} as TENDT,lpct.{2} as GIA,lpct.{3} as MACT,lpct.TEN  "
                            + " from {4}.{5} lpct "
                            + " inner join {4}.{6} lp on lp.{7}=lpct.{8} "
                            + " inner join {4}.{9} dt on dt.{10} = lpct.{0} "
                            + " where lpct.{11} = '{12}' and lpct.MADOITUONG in (1,2,12) order by dt.{13}", cls_DanhMucLoaiGiuongChiTiet.col_MADOITUONG, cls_DoiTuong.col_DoiTuong, cls_DanhMucLoaiGiuongChiTiet.col_GIAGIUONG, cls_DanhMucLoaiGiuongChiTiet.col_ID, _acc.Get_User()
                            , cls_DanhMucLoaiGiuongChiTiet.tb_TenBang, cls_D_DanhMucLoaiGiuong.tb_TenBang, cls_D_DanhMucLoaiGiuong.col_MALOAI, cls_DanhMucLoaiGiuongChiTiet.col_MALOAI,
                            cls_DoiTuong.tb_TenBang, cls_DoiTuong.col_MaDoiTuong, cls_DanhMucLoaiGiuongChiTiet.col_MALOAI, maLoai, cls_DoiTuong.col_MaDoiTuong);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy danh sách loại giường chi tiết
        public DataTable GetDataUpdateTableChiTiet()
        {
            _sql = string.Format("Select distinct dt.{0} as MADT,dt.{1} as TENDT, 0 as GIA,'' as MACT "
                               + "  from {2}.{3} dt "
                               + " left join {2}.{4} lgct on dt.{5} = lgct.{6} and dt.{7} in (1,2,12)"
                               + " order by dt.{7}", cls_DoiTuong.col_MaDoiTuong, cls_DoiTuong.col_DoiTuong, _acc.Get_User(), cls_DoiTuong.tb_TenBang, cls_DanhMucLoaiGiuongChiTiet.tb_TenBang,
                               cls_DoiTuong.col_MaDoiTuong, cls_DanhMucLoaiGiuongChiTiet.col_MADOITUONG, cls_DoiTuong.col_MaDoiTuong);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy danh sách loại phòng
        public DataTable GetDanhMucLoaiPhong()
        {
            _sql = string.Format("Select " + cls_DanhMucLoaiPhong.col_MA + ", " + cls_DanhMucLoaiPhong.col_TEN + ", " + cls_DanhMucLoaiPhong.col_MAUSAC + ", " + cls_DanhMucLoaiPhong.col_ID + ", " + cls_DanhMucLoaiPhong.col_STT + " "
                               + "  from {0}.{1} "
                               + " ", _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy danh sách khoa phong có giường
        public DataTable GetDanhMucKhoaPhongCoGiuong()
        {
            string sql = string.Format("select btd.MAKP,btd.TENKP,count(g.id) as SoLuong"
                                       + " from {0}.dmloaiphong lp left join {0}.danhmucphong p on lp.id=p.LOAI "
                                       + " left join {0}.PG_DANHMUCGIUONG g on g.maphong=p.maphong"//Sửa mã phòng
                                       + " left join {0}.danhmucloaigiuong lg  on g.loaigiuong=lg.maloai "
                                       + " left join {0}.btdkp_bv btd on btd.makp=p.MAKP "
                                       + " where btd.makp is not null"
                                       + " group by btd.MAKP,btd.TENKP "
                                       + " having count(g.id) >0"
                                       , _acc.Get_User());

            return _acc.Get_Data(sql);
        }

        public DataTable GetDanhMucKhoaPhongCoGiuongTrongTheoLoai(string maKP = "", string maPhong = "")
        {
            string where = " WHERE g.TINHTRANG is not null ";
            if (!string.IsNullOrEmpty(maKP))
            {
                where += " AND p." + cls_DanhMucPhong.col_MAKP + " = '" + maKP + "' ";
            }
            if (!string.IsNullOrEmpty(maPhong))
            {
                where += " AND p." + cls_DanhMucPhong.col_MA + " = '" + maPhong + "' ";
            }

            string _sql = string.Format("select btd.MAKP,btd.TENKP, lp.MA, g.TINHTRANG, count(case g.TINHTRANG when 0 then 1 else null end ) as SoLuong"
                                       + " from {0}.dmloaiphong lp right join {0}.danhmucphong p on lp.id=p.LOAI "
                                       + " right join {0}.PG_DANHMUCGIUONG g on g.maphong=p.maphong "//Sửa mã phòng
                                       + " left join {0}.danhmucloaigiuong lg  on g.loaigiuong=lg.maloai "
                                       + " left join {0}.btdkp_bv btd on btd.makp=p.MAKP "
                                    + where   // + " where g.TINHTRANG is not null "
                                       + " group by btd.MAKP,btd.TENKP ,lp.MA, g.TINHTRANG "
                                       // + "  having count(g.id) >0 "
                                       , _acc.Get_User());

            return _acc.Get_Data(_sql);
        }

        public void CheckGiuongKoPhong()
        {
            string _sql = string.Format("select " + cls_PG_DanhMucGiuong.col_ID
                                       + " from {0}." + cls_PG_DanhMucGiuong.tb_TenBang + " "
                                       + " where " + cls_PG_DanhMucGiuong.col_MAPHONG + " is null OR " + cls_PG_DanhMucGiuong.col_MAPHONG + " not in "
                                         + " ( "
                                         + " select " + cls_DanhMucPhong.col_MAPHONG + " FROM {0}." + cls_DanhMucPhong.tb_TenBang + " "
                                         + " ) "
                                       , _acc.Get_User());

            DataTable dt = _acc.Get_Data(_sql);
            if (dt != null && dt.Rows.Count > 0)
            {

            }
        }
        public string GetKPUser(string userID)
        {
            string _sql = string.Format("select " + cls_DLogin.col_MaKP
                                       + " from {0}." + cls_DLogin.tb_TenBang + " "
                                       + " where " + cls_DLogin.col_TaiKhoan + " = '" + userID + "' "
                                       , _acc.Get_User());

            DataTable dt = _acc.Get_Data(_sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                string makp = (dt.Rows[0][0] + "");
                if (!string.IsNullOrEmpty(makp))
                {
                    return makp.Split(',')[0];
                }
            }
            return "";
        }
        #endregion  

        #region Lấy danh sách loại giường
        public DataTable GetDanhMucLoaiGiuong()
        {
            _sql = string.Format("Select * "
                               + "  from {0}.{1} "
                               + " ", _acc.Get_User(), cls_D_DanhMucLoaiGiuong.tb_TenBang);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #endregion

        #region Truy vấn form LoaiPhong

        #region Lấy danh sách loại phòng theo điều kiện
        public DataTable GetDataTimKiem(string chuoiTimKiem)
        {
            _sql = string.Format("select {0},{1},{2},{3} FROM {4}.{5} "
                + " WHERE {2} like '%{6}%' or {3} like '%{6}%' order by {0}", cls_DanhMucLoaiPhong.col_ID, cls_DanhMucLoaiPhong.col_STT, cls_DanhMucLoaiPhong.col_MA, cls_DanhMucLoaiPhong.col_TEN,
                _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang, chuoiTimKiem, cls_DanhMucLoaiPhong.col_MAUSAC);
            // _sql = "select a.ID,a.STT,a.MA,a.TEN,b.DOITUONG,b.IDGIAVP,b.GIA ";
            //_sql += "FROM hisbvnhitp.DMLOAIPHONG a ";
            //_sql += "  left join HISBVNHITP.GIAGIUONG b on a.id = b.ma ";
            //_sql += "WHERE a.MA like '%%' or TEN like '%%' order by a.ID";
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy max stt danh mục loại phòng
        public string GetTuDongSTTDanhMucLoaiPhong()
        {
            _sql = string.Format("select max({0})+1 from {1}.{2}", cls_DanhMucLoaiPhong.col_STT, _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang);
            return _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString();
        }
        #endregion

        #region Lấy max id danh mục loại phòng
        public string GetTuDongIDDanhMucLoaiPhong()
        {
            _sql = string.Format("select max({0})+1 from {1}.{2}", cls_DanhMucLoaiPhong.col_ID, _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang);
            return _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString();
        }
        #endregion

        #region Lây danh sách đối tượng
        public DataTable GetDataDanhSachDoiTuong()
        {
            return _acc.Get_Data(string.Format("select * from {0}.{1}", _acc.Get_User(), cls_DoiTuong.tb_TenBang));
        }



        #endregion

        #region Lấy giá gợi ý
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataGiaGoiY()
        {
            return _acc.Get_Data(string.Format("select id,ten,gia_th from {0}.V_GIAVP where id_loai = 28", _acc.Get_User()));
        }
        public DataTable GetDataGia(string id, string maDoiTuong)
        {

            string ID_Banggia = "-1";
            string sqlBanggia = "select id from " + _acc.Get_User() + ".v_dmbanggia where apdung = 1 and sudung = 1";

            ID_Banggia = _acc.Get_Data(sqlBanggia).Rows[0][0] + "";

            return _acc.Get_Data(string.Format("select GIA from {0}.V_GIAVPCT where ID_BANGGIA = '{1}' and ID_GIAVP = '{2}' and MADOITUONG = '{3}' "
                , _acc.Get_User()
                , ID_Banggia
                , id
                , maDoiTuong
                ));



        }

        #endregion

        #region Bảng GIAGIUONG

        #region Lấy ID Bảng Giá giường
        public string GetTuDongIDGiaGiuong()
        {
            _sql = string.Format("select max({0})+1 from {1}.{2}", cls_GiaGiuong.col_ID, _acc.Get_User(), cls_GiaGiuong.tb_TenBang);
            return _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString();
        }
        #endregion

        #region Thêm dữ liệu bảng Giá giường
        public bool InsertGiaGiuong(string ma, string loai, string doiTuong, string idGiaVP, string gia)
        {
            string id = GetTuDongIDGiaGiuong();
            _sql = string.Format("INSERT INTO {0}.{8} VALUES('{1}','{2}','{3}','{4}','{5}','{6}',{7})"
                , _acc.Get_User()
                , id
                , ma
                , loai
                , doiTuong
                , idGiaVP
                , gia.Replace(",", "")
                , "sysdate"
                , cls_GiaGiuong.tb_TenBang
                );
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show(string.Format("Không thể thêm {0}.{1} !!!!", _userError, _systemError),
                   TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Cập nhật bảng Giá giường
        public bool UpdateGiaGiuong(string ma, string loai, string doiTuong, string idGiaVP, string gia)
        {
            _sql = string.Format("UPDATE {0}.{6} SET {10} = '{4}' where {11}='{5}' and {7}='{1}' and {8}='{2}' and {9}='{3}' ",
                _acc.Get_User() //0
                , loai           //1
                , doiTuong      //2
                , idGiaVP       //3
                , gia.Replace(",", "")  //4
                , ma        //5
                , cls_GiaGiuong.tb_TenBang //6
                , cls_GiaGiuong.col_LOAI //7
                , cls_GiaGiuong.col_DOITUONG //8
                 , cls_GiaGiuong.col_IDGIAVP//9
                 , cls_GiaGiuong.col_GIA//10
                 , cls_GiaGiuong.col_MA//11
                );
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                return InsertGiaGiuong(ma, loai, doiTuong, idGiaVP, gia);

            }
            return true;
        }

        #endregion

        #region Xóa dữ liệu bảng Giá giường
        public bool DeleleGiaGiuong(string id)
        {
            _sql = string.Format("DELETE  FROM {0}.GIAGIUONG WHERE ID='{1}'", _acc.Get_User(), id);
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}.{1} !!!!", _userError, _systemError),
                   TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Lấy ID giá giường từ mã loại trong Danh mục loại phòng
        public string LayIDGiaGuongTuMaLoai(string id)
        {
            _sql = string.Format("select a.ID from {0}.{1}, {2}.{3} where {2}='{3}'", _acc.Get_User(), cls_DanhMucLoaiPhong.tb_TenBang,
                _acc.Get_User(), cls_GiaGiuong.tb_TenBang, cls_DanhMucLoaiPhong.col_ID, id);
            return _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString() == "" ? "1" : _acc.Get_Data(_sql).Rows[0].ItemArray[0].ToString();
        }
        #endregion

        #endregion

        #region Truy vấn form NhapThongTinDatGiuongCuaBenhNhan

        #region Lấy danh mục đối tượng
        public DataTable GetDataDoiTuong()
        {
            //_sql = string.Format("select MADOITUONG,DOITUONG from {0}.doituong", _acc.Get_User());
            _sql = string.Format("select {0},{1} from {2}.{3}", cls_DoiTuong.col_MaDoiTuong, cls_DoiTuong.col_DoiTuong, _acc.Get_User(), cls_DoiTuong.tb_TenBang);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy danh mục khoa phòng 
        public DataTable SetSourceKhoaPhong()
        {
            //_sql = string.Format("select  MAKP,  TENKP from {0}.btdkp_bv where makp<>'01' and loai in (0,4) order by loai,makp", _acc.Get_User());
            _sql = string.Format("select  {0},  {1} from {2}.{3} where {4}<>'01' and {5} in (0,4)  "

                + " order by {6},{7} "
                , cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP,
                _acc.Get_User(), cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_MaKP

                );
            return _acc.Get_Data(_sql);
        }
        public DataTable SetSourceKhoaPhongconguoi()
        {
            //_sql = string.Format("select  MAKP,  TENKP from {0}.btdkp_bv where makp<>'01' and loai in (0,4) order by loai,makp", _acc.Get_User());
            _sql = string.Format("select  {0},  {1} from {2}.{3} where {4}<>'01' and {5} in (0,4) AND {0} in (select {8} from {2}.{9} where {10} in (select {11} from {2}.{12}   "
                + " WHERE {13} in (select {16} from {2}.{17} where {18} ='0') "
                + " )  ) order by {6},{7} "
                , cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_TenKP,
                _acc.Get_User(), cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_MaKP
                , cls_DanhMucPhong.col_MAKP //8
                , cls_DanhMucPhong.tb_TenBang //9
                , cls_DanhMucPhong.col_MAPHONG//10
                , cls_PG_DanhMucGiuong.col_MAPHONG//11
                , cls_PG_DanhMucGiuong.tb_TenBang//12
                , cls_PG_DanhMucGiuong.col_ID//13
                , "" //14
                , "" //15
                , cls_PG_TheoDoiGiuong.col_IDGIUONG //16
                , cls_PG_TheoDoiGiuong.tb_TenBang //17
                       , cls_PG_TheoDoiGiuong.col_TRANGTHAI //18
                );
            return _acc.Get_Data(_sql);
        }

        public DataTable SetSourcePhongconguoi(string makhoa)
        {
            //_sql = string.Format("select  MAKP,  TENKP from {0}.btdkp_bv where makp<>'01' and loai in (0,4) order by loai,makp", _acc.Get_User());
            _sql = string.Format("select  {0},  {1} from {2}.{9} where {10} in (select {11} from {2}.{12}   "
                + " WHERE {13} in (select {16} from {2}.{17} where {18} ='0') "
                + " )  AND {8} = '" + makhoa + "'"
                , cls_DanhMucPhong.col_ID, cls_DanhMucPhong.col_TEN,
                _acc.Get_User(), cls_BTDKP_BV.tb_TenBang, cls_BTDKP_BV.col_MaKP, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_Loai, cls_BTDKP_BV.col_MaKP
                , cls_DanhMucPhong.col_MAKP //8
                , cls_DanhMucPhong.tb_TenBang //9
                , cls_DanhMucPhong.col_MAPHONG//10
                , cls_PG_DanhMucGiuong.col_MAPHONG//11
                , cls_PG_DanhMucGiuong.tb_TenBang//12
                , cls_PG_DanhMucGiuong.col_ID//13
                , "" //14
                , "" //15
                , cls_PG_TheoDoiGiuong.col_IDGIUONG //16
                , cls_PG_TheoDoiGiuong.tb_TenBang //17
                    , cls_PG_TheoDoiGiuong.col_TRANGTHAI //18
                );
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Lấy danh mục bộ từ điển bệnh nhân
        public DataTable GetAllFieldBTDBN()
        {
            _sql = string.Format("select * from {0}.{1}", _acc.Get_User(), cls_BTDBN.tb_TenBang);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Cấp Mã bệnh nhân tự động

        public bool ExistMaBN(string owner, string name, string type)
        {

            var sql = string.Format(@"SELECT * FROM all_objects
                                    WHERE UPPER(owner) = '{0}' 
                                        AND UPPER(object_name) = '{1}'
                                        AND UPPER(object_type) = '{2}'"
                        , owner.ToUpper()
                        , name.ToUpper()
                        , type.ToUpper());
            using (DataTable dt = _acc.Get_Data(sql))
            {
                return (dt != null && dt.Rows.Count > 0);
            }
        }
        public bool ExistMaphong(string maphong)
        {

            var sql = string.Format(@"SELECT id FROM {0}.{1}
                                    WHERE UPPER({2}) = '{3}' 
                                      "
                        , _acc.Get_User()
                        , cls_DanhMucPhong.tb_TenBang
                        , cls_DanhMucPhong.col_MAPHONG
                        , maphong);
            using (DataTable dt = _acc.Get_Data(sql))
            {
                return (dt != null && dt.Rows.Count > 0);
            }
        }
        public bool Execute(ref string userError, ref string systemError, List<string> lst)
        {
            if (string.IsNullOrWhiteSpace((string.Concat(lst))))//không có query
                return true;
            #region NET 4.0
            #endregion
            string query = string.Join(" "
                                    , (from x in lst where !string.IsNullOrWhiteSpace(x) select string.Format("EXECUTE IMMEDIATE '{0}';", x.Replace("'", "''"))).ToArray());


            var sql = @"BEGIN " + query + "END;";

            return _acc.Execute_Data(ref userError, ref systemError, sql);

        }

        public bool KiemTraFunc()
        {
            string _userError = string.Empty;
            string _systemError = string.Empty;
            var sql = !ExistMaBN(_acc.Get_User(), "CAPTUDONGMABN", "FUNCTION")
                                ? string.Format(@"create or replace FUNCTION {0}.{1}
                                                      ( 
                                                      in_num  number,
                                                        in_yy number)
                
                                                    RETURN number AS
                                                    c number;
                                                    d number;
                                                    begin
                                                            c := 1;
                                                            d := in_num;
                                                           loop
                                                            select count(*)  into c from btdbn where mabn=in_yy || LPAD(d,6,'0');
                                                            exit when c = 0;
                                                            d := d+ 1;
                                                            end loop;
                                                            return d;

                                                   END;"
                                                , _acc.Get_User(), "CAPTUDONGMABN")
                                 : "";
            {
                if (!string.IsNullOrWhiteSpace(sql)
                    && !Execute(ref _userError, ref _systemError, new List<string> { sql }))
                {
                    TA_MessageBox.MessageBox.Show("Error when creating function CAPTUDONGMABN "
                                                    + Environment.NewLine
                                                    + _systemError
                                                   , TA_MessageBox.MessageIcon.Error);
                    return false;
                }
                return true;
            }
        }

        public int CapMaBN(int yy, int loai, int userid, bool update)
        {
            string sql = string.Empty;
            int ma = 0;
            userid = int.Parse(userid.ToString() + 1);

            DataTable dt = _acc.Get_Data("select stt from " + _acc.Get_User() + ".capmabn where yy=" + yy + " and loai=" + loai + " and userid=" + userid);
            if (dt.Rows.Count > 0)
            {
                ma = int.Parse(dt.Rows[0]["stt"].ToString());

            }
            else
            {
                ma++;
            }
            sql = string.Format("SELECT {0}.{1}({2},{3}) FROM DUAL"
                                   , _acc.Get_User(), "CAPTUDONGMABN"
                                   , ma, yy);
            using (DataTable dtResult = _acc.Get_Data(sql))
            {
                ma = int.Parse(dtResult.Rows[0][0].ToString());
                // if (update) _aDal.upd_capmabn(yy, loai, userid, ma);
                return ma;
            }
        }

        #endregion

        #region Lấy query get thông tin giường điều kiện khoa
        public string GetDataPhongTimKiem(string maKhoaPhong, string maphong = "")
        {

            string where = "";
            if (!string.IsNullOrEmpty(maKhoaPhong))
            {
                where += " AND b." + cls_DanhMucPhong.col_MAKP + " = '" + maKhoaPhong + "' ";
            }
            if (!string.IsNullOrEmpty(maphong))
            {
                where += " AND b." + cls_DanhMucPhong.col_MAPHONG + " = '" + maphong + "' ";
            }

            if (!string.IsNullOrEmpty(where))
            {
                where = " WHERE " + where.Remove(0, 4);
            }
            _sql = string.Format("select a.{0},a.{1},a.{2},a.{3} as ten,a.{4},a.{5},a.{6},a.{7},a.{8},"
                                  + " a.{9},a.{10},a.{11},c.{12},a.{13}"
                                  + " from {14}.{15} a "
                                  + " left join {14}.{16} b on a.{18}=b.{19} "
                                  + " left join {14}.{17} c on a.{20}=c.{21} "
                                  + where
                                  + " order by b.{24},a.{25} ",//Sua loi hien thi
                                    cls_PG_DanhMucGiuong.col_ID, cls_PG_DanhMucGiuong.col_TEN, cls_PG_DanhMucGiuong.col_MA, cls_PG_DanhMucGiuong.col_STT, cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_SOLUONG,
                                    cls_PG_DanhMucGiuong.col_GIA_TH, cls_PG_DanhMucGiuong.col_GIA_BH, cls_PG_DanhMucGiuong.col_GIA_CS, cls_PG_DanhMucGiuong.col_GIA_DV, cls_PG_DanhMucGiuong.col_GIA_NN, cls_PG_DanhMucGiuong.col_BHYT,
                                    cls_V_GiaVienPhi.col_CHENHLECH, cls_PG_DanhMucGiuong.col_VITRI, _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang, cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG,
                                    cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_ID, cls_DanhMucPhong.col_MAKP, maKhoaPhong, cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                                    );
            return _sql;
        }

        public string GetDataLoaiGiuong(string maKhoaPhong, string maphong = "")
        {

            string where = "";
            if (!string.IsNullOrEmpty(maKhoaPhong) && maKhoaPhong != "-1")
            {
                where += " AND dmp." + cls_DanhMucPhong.col_MAKP + " = '" + maKhoaPhong + "' ";
            }
            if (!string.IsNullOrEmpty(maphong) && maphong != "-1")
            {
                where += " AND dmp." + cls_DanhMucPhong.col_MAPHONG + " = '" + maphong + "' ";
            }

            if (!string.IsNullOrEmpty(where))
            {
                where = " WHERE " + where.Remove(0, 4);
            }
            _sql = string.Format("select distinct dmlg.{2},dmlg.{3}"
                                  + " from {0}.{4} dmg "
                                  + " left join {0}.{1} dmlg on dmg.{5} = dmlg.{6}"
                                  + " left join {0}.{7} dmp on dmg.{8} = dmp.{9}"
                                  + where,
                                   _acc.Get_User(),//0
                                   "DANHMUCLOAIGIUONG",//1
                                   "MALOAI",//2
                                   "TENLOAI",//3
                                   cls_PG_DanhMucGiuong.tb_TenBang,//4
                                   cls_PG_DanhMucGiuong.col_LOAIGIUONG,//5
                                   "MALOAI",//6
                                   cls_DanhMucPhong.tb_TenBang, //7
                                   cls_PG_DanhMucGiuong.col_MAPHONG, //8
                                   cls_DanhMucPhong.col_MAPHONG//9
                                    );
            return _sql;
        }
        public string GetDataPhong(string maKhoaPhong)
        {

            string where = "";
            if (!string.IsNullOrEmpty(maKhoaPhong) && maKhoaPhong != "-1")
            {
                where += " AND dmp." + cls_DanhMucPhong.col_MAKP + " = '" + maKhoaPhong + "' ";
            }

            if (!string.IsNullOrEmpty(where))
            {
                where = " WHERE " + where.Remove(0, 4);
            }
            _sql = string.Format("select distinct dmp.{2},dmp.{3}"
                                   + " from {0}.{1} dmg "
                                   + " left join {0}.{7} dmp on dmg.{8} = dmp.{9}"
                                   + where,
                                    _acc.Get_User(),//0
                                    cls_PG_DanhMucGiuong.tb_TenBang,//1
                                   cls_DanhMucPhong.col_MAPHONG,//2
                                    cls_DanhMucPhong.col_TEN,//3
                                     "",//4
                                    cls_PG_DanhMucGiuong.col_LOAIGIUONG,//5
                                    "",//6
                                    cls_DanhMucPhong.tb_TenBang, //7
                                    cls_PG_DanhMucGiuong.col_MAPHONG, //8
                                    cls_DanhMucPhong.col_MAPHONG//9
                                     );
            return _sql;
        }
        public string GetDataGiuong(string maKhoaPhong, string maphong = "", string maloaigiuong = "", string magiuong = "")
        {

            string where = "";
            if (!string.IsNullOrEmpty(maKhoaPhong) && maKhoaPhong != "-1")
            {
                where += " AND dmp." + cls_DanhMucPhong.col_MAKP + " = '" + maKhoaPhong + "' ";
            }
            if (!string.IsNullOrEmpty(magiuong) && magiuong != "-1")
            {
                where += " AND dmg." + cls_PG_DanhMucGiuong.col_ID + " = '" + magiuong + "' ";
            }
            if (!string.IsNullOrEmpty(maphong) && maphong != "-1")
            {
                where += " AND dmp." + cls_DanhMucPhong.col_MAPHONG + " = '" + maphong + "' ";
            }
            if (!string.IsNullOrEmpty(maloaigiuong) && maloaigiuong != "-1")
            {
                where += " AND dmg." + cls_PG_DanhMucGiuong.col_LOAIGIUONG + " = '" + maloaigiuong + "' ";
            }
            if (!string.IsNullOrEmpty(where))
            {
                where = " WHERE " + where.Remove(0, 4);
            }
            _sql = string.Format("select dmg.{2},dmg.{3}"
                                  + " from {0}.{1} dmg "
                                  + " left join {0}.{7} dmp on dmg.{8} = dmp.{9}"
                                  + where,
                                   _acc.Get_User(),//0
                                   cls_PG_DanhMucGiuong.tb_TenBang,//1
                                  cls_PG_DanhMucGiuong.col_ID,//2
                                   cls_PG_DanhMucGiuong.col_TEN,//3
                                    "",//4
                                   cls_PG_DanhMucGiuong.col_LOAIGIUONG,//5
                                   "",//6
                                   cls_DanhMucPhong.tb_TenBang, //7
                                   cls_PG_DanhMucGiuong.col_MAPHONG, //8
                                   cls_DanhMucPhong.col_MAPHONG//9
                                    );
            return _sql;
        }
        #endregion

        #region Lấy query get mã và tên giường từ điều kiện khoa và phòng
        public string GetDataPhongTimKiem1(string id, string maKhoa, string maPhong)
        {
            _sql = string.Format("select a.{0},a.{1}"
                         + " from {2}.{3} a left join {2}.{4} b on a.{6}=b.{7} left join {2}.{5} c on a.{8}=c.{9} "
                         + " where  b.{10}='{11}' and b.{12}='{13}' and a.{15} = '{16}' order by b.{17},a.{18}",//Sửa mã
                         cls_PG_DanhMucGiuong.col_ID, cls_PG_DanhMucGiuong.col_TEN,
                         _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_DanhMucPhong.tb_TenBang, cls_V_GiaVienPhi.tb_TenBang,
                         cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_PG_DanhMucGiuong.col_IDGIAVP, cls_V_GiaVienPhi.col_ID, cls_DanhMucPhong.col_MAKP, maKhoa,
                         cls_DanhMucPhong.col_MAPHONG, maPhong, cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_ID, id, cls_DanhMucPhong.col_STT, cls_PG_DanhMucGiuong.col_STT
                         );
            return _sql;
        }
        #endregion

        #region Lấy giá viện phí chi tiết theo giường và đối tượng
        public DataTable GetGiaGiaVienPhiCT(string maGiuong, string maDoiTuong)
        {
            _sql = string.Format("select GIA from {0}.v_giavpct  "
                    + " where id_giavp={1} and madoituong={2}", _acc.Get_User(),
                    maGiuong, maDoiTuong);
            return _acc.Get_Data(_sql);
        }
        #endregion

        #region Thêm thông tin vào bộ từ điển bệnh nhân nếu bệnh nhân đặt giường chưa từng vào bệnh viện
        public bool InsertTableBTDBN(string maBN, string tenBN, string ngaySinh, string namSinh, string phai, string diaChi)
        {
            _sql = string.Format("insert into {0}.{1}({2},{3},{4},{5},{6},{7}) values("
                         + "{8},'{9}',TO_DATE('{10}','dd/MM/yyyy'),'{11}',{12},'{13}')",
                         _acc.Get_User(), cls_BTDBN.tb_TenBang, cls_BTDBN.col_MaBN, cls_BTDBN.col_HoTen,
                         cls_BTDBN.col_NgaySinh, cls_BTDBN.col_NamSinh, cls_BTDBN.col_Phai, cls_BTDBN.col_Thon, maBN,
                          tenBN, ngaySinh,
                          namSinh, phai.IndexOf("Nam") != -1 ? "0" : "1",
                          diaChi);
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                //TA_MessageBox.MessageBox.Show("Thêm dữ liệu vào table btdbn bị lỗi!", TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Thêm thông tin đặt giường vào table theo dõi giường(đã vào giường)
        public string GETMADOITUONG(string maBN, string MAQL, string MAVV)
        {
            //_sql = string.Format("select * from {0}.datgiuong where idgiuong = {1} and mabn='{2}'", _acc.Get_User(), idGiuong, maBN);
            _sql = string.Format("select " + cls_BenhAnDT.col_MaDoiTuong + " from {0}.{1} where {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}' ", _acc.Get_User(), cls_BenhAnDT.tb_TenBang, cls_BenhAnDT.col_MaBN, maBN, cls_BenhAnDT.col_MaQL, MAQL, cls_BenhAnDT.col_MaVaOVien, MAVV);
            DataTable dtt = _acc.Get_Data(_sql);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                return dtt.Rows[0][0] + "";
            }
            return "";
        }
        public bool InsertThongTinDatGiuongVaoTheoDoiGiuong(string idTDG, string maKP, string maBN, string maGiuong, DateTime tu, DateTime den, int soLuong, string donGia, string madoituong, string ghiChu, string trangThai, string idtu, string mavaovien, string maql)
        {
            if (den != DateTime.MinValue)
            {
                _sql = string.Format("insert into {0}.{1}({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{28},{30})"
                                            + " values({15},'{16}','{17}',{18},TO_DATE('{19}','dd/MM/yyyy hh24:mi:ss'),TO_DATE('{20}','dd/MM/yyyy hh24:mi:ss'),{21},"
                                            + "{22},'{23}','{24}','{25}',TO_DATE('{26}','dd/MM/yyyy hh24:mi:ss'),'{27}','{29}','{31}')",
                                            _acc.Get_User() //0
                                            , cls_PG_TheoDoiGiuong.tb_TenBang,
                                            cls_PG_TheoDoiGiuong.col_ID,
                                            cls_PG_TheoDoiGiuong.col_MAKP,
                                            cls_PG_TheoDoiGiuong.col_MABN,
                                            cls_PG_TheoDoiGiuong.col_IDGIUONG,//5
                                            cls_PG_TheoDoiGiuong.col_TU,
                                            cls_PG_TheoDoiGiuong.col_DEN,
                                            cls_PG_TheoDoiGiuong.col_SOLUONG,
                                            cls_PG_TheoDoiGiuong.col_DONGIA,
                                            cls_PG_TheoDoiGiuong.col_MADOITUONG,//10
                                            cls_PG_TheoDoiGiuong.col_GHICHU,
                                            cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN,
                                            cls_PG_TheoDoiGiuong.col_NGAYUD,
                                            cls_PG_TheoDoiGiuong.col_IDTU,
                                            idTDG,//15
                                               maKP,
                                               maBN,
                                               maGiuong,
                                               tu.ToString("dd/MM/yyyy HH:mm:ss"),
                                               den.ToString("dd/MM/yyyy HH:mm:ss"),//20
                                               soLuong,
                                               donGia,
                                               madoituong,
                                               ghiChu,
                                               trangThai, //25
                                               GetSysDate().ToString("dd/MM/yyyy HH:mm:ss")
                                               , idtu
                                               , cls_PG_TheoDoiGiuong.col_MAVAOVIEN
                                               , mavaovien
                                               , cls_PG_TheoDoiGiuong.col_MAQL //30
                                               , maql
                                               , "" //32
                                               , ""
                                               );
            }
            else
            {
                _sql = string.Format("insert into {0}.{1}({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{26},{28})"
                            + " values({14},'{15}','{16}',{17},TO_DATE('{18}','dd/MM/yyyy hh24:mi:ss'),{19},"
                            + "{20},'{21}','{22}','{23}',TO_DATE('{24}','dd/MM/yyyy hh24:mi:ss'),'{25}','{27}','{29}')",
                            _acc.Get_User(),//0
                            cls_PG_TheoDoiGiuong.tb_TenBang,
                            cls_PG_TheoDoiGiuong.col_ID,
                            cls_PG_TheoDoiGiuong.col_MAKP,
                            cls_PG_TheoDoiGiuong.col_MABN,
                            cls_PG_TheoDoiGiuong.col_IDGIUONG, //5
                            cls_PG_TheoDoiGiuong.col_TU,
                            cls_PG_TheoDoiGiuong.col_SOLUONG,
                            cls_PG_TheoDoiGiuong.col_DONGIA,
                            cls_PG_TheoDoiGiuong.col_MADOITUONG,
                            cls_PG_TheoDoiGiuong.col_GHICHU,//10
                            cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN,
                               cls_PG_TheoDoiGiuong.col_NGAYUD,
                               cls_PG_TheoDoiGiuong.col_IDTU,
                            idTDG,
                               maKP,//15
                               maBN,
                               maGiuong,
                               tu.ToString("dd/MM/yyyy HH:mm:ss"),
                               soLuong,
                               donGia,//20
                               madoituong,
                               ghiChu,
                               trangThai,
                               GetSysDate().ToString("dd/MM/yyyy HH:mm:ss")
                               , idtu//25
                               , cls_PG_TheoDoiGiuong.col_MAVAOVIEN
                               , mavaovien
                               , cls_PG_TheoDoiGiuong.col_MAQL
                               , maql
                               , "" //30
                               , ""
                               );
            }
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show("Thêm vào table theodoigiuong bị lỗi!", TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Xóa giường đã đặt theo điều kiện



        #endregion

        #region Xóa bệnh nhân đã đặt giường

        #endregion

        #region Thêm thông tin đặt giường
        public bool InsertThongTinDatGiuongVaoBangDatGiuong(string idTDG, string maKP, string maBN, string maGiuong, DateTime tu, DateTime den, int soLuong, string donGia, string tenBN, string ghiChu, string trangThai, string madoituong = null)
        {
            DateTime denC = den;
            if (denC.ToString("dd/MM/yyyy") == DateTime.MinValue.ToString("dd/MM/yyyy"))
            {
                _sql = string.Format("insert into {0}.{1}({2},{3},{4},{5},{6},{7},{8},{10},{11},{23})"
                           + " values({12},'{13}','{14}',{15},TO_DATE('{16}','dd/MM/yyyy hh24:mi:ss'),{17},"
                           + "{18},'{20}','{21}','{22}')", _acc.Get_User(),//0
                            cls_PG_TheoDoiGiuong.tb_TenBang,//1
                             cls_PG_TheoDoiGiuong.col_ID//2
                             , cls_PG_TheoDoiGiuong.col_MAKP//3
                             , cls_PG_TheoDoiGiuong.col_MABN//4
                             , cls_PG_TheoDoiGiuong.col_IDGIUONG//5
                             , cls_PG_TheoDoiGiuong.col_TU,//6
                           cls_PG_TheoDoiGiuong.col_SOLUONG//7
                           , cls_PG_TheoDoiGiuong.col_DONGIA//8
                           , cls_PG_TheoDoiGiuong.tb_TenBang//9
                           , cls_PG_TheoDoiGiuong.col_GHICHU//10
                           , cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN,//11
                           idTDG,//12
                              maKP,//13
                              maBN,//14
                              maGiuong,//15
                              tu.ToString("dd/MM/yyyy hh:mm:ss"),//       16    
                              soLuong,//17
                              donGia,//18
                              tenBN, //19
                              ghiChu,//20
                              trangThai//21
                              , madoituong//22
                              , cls_PG_TheoDoiGiuong.col_MADOITUONG//23
                              );
            }
            else
            {
                _sql = string.Format("insert into {0}.{1}({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{23})"
                           + " values({12},'{13}','{14}',{15},TO_DATE('{16}','dd/MM/yyyy hh24:mi:ss'),{17},"
                           + "{18},TO_DATE('{19}','dd/MM/yyyy hh24:mi:ss'),'{20}','{21}','{22}')", _acc.Get_User(),//0
                            cls_PG_TheoDoiGiuong.tb_TenBang,//1
                             cls_PG_TheoDoiGiuong.col_ID//2
                             , cls_PG_TheoDoiGiuong.col_MAKP//3
                             , cls_PG_TheoDoiGiuong.col_MABN//4
                             , cls_PG_TheoDoiGiuong.col_IDGIUONG//5
                             , cls_PG_TheoDoiGiuong.col_TU,//6
                           cls_PG_TheoDoiGiuong.col_SOLUONG//7
                           , cls_PG_TheoDoiGiuong.col_DONGIA//8
                            , cls_PG_TheoDoiGiuong.col_DEN//9
                           , cls_PG_TheoDoiGiuong.col_GHICHU//10
                           , cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN,//11
                           idTDG,//12
                              maKP,//13
                              maBN,//14
                              maGiuong,//15
                              tu.ToString("dd/MM/yyyy hh:mm:ss"),//       16    
                              soLuong,//17
                              donGia,//18
                              den.ToString("dd/MM/yyyy hh:mm:ss"), //19
                              ghiChu,//20
                              trangThai//21
                               , madoituong//22
                              , cls_PG_TheoDoiGiuong.col_MADOITUONG//23
                              );

            }
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show("Thêm vào table đặt giường bị lỗi!"
                   , TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Thêm thông tin vào table lịch sử theo dõi giường


        #endregion

        #region Cập nhật tình trạng giường
        public bool UpdateTinhTrangGiuong(string idGiuong)
        {
            _sql = string.Format("update {0}.{1} set {2} = {3} where {4} = {5}", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_TINHTRANG, "1", cls_PG_DanhMucGiuong.col_ID, idGiuong);
            if (!_acc.Execute_Data(ref _userError, ref _systemError, _sql))
            {
                TA_MessageBox.MessageBox.Show("Cập nhật bị lỗi!", TA_MessageBox.MessageIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Kiểm tra giường đã cập nhật tình trạng hay chưa
        public bool DaCapNhatTinhTrangCuaGiuong(string idGiuong)
        {
            _sql = string.Format("select ID from {0}.{1} where {2} = {3} and {4} = {5}", _acc.Get_User(), cls_PG_DanhMucGiuong.tb_TenBang, cls_PG_DanhMucGiuong.col_ID, idGiuong, cls_PG_DanhMucGiuong.col_TINHTRANG, "1");
            if (_acc.Get_Data(_sql).Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public string GetIdTheoDoiGiuong(string mabn, string maql, string makp, string idGiuong)
        {
            try
            {
                return _acc.Get_Data($@"select id from {_acc.Get_User()}.{cls_PG_TheoDoiGiuong.tb_TenBang} where mabn = '{mabn}' and maql  = '{maql}' and makp = '{makp}' and idgiuong = '{idGiuong}'").Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #endregion

        #region Truy vấn form ThongKe

        #region Lấy query số lượng tất cả giường hiện có
        public string GetQueryThongKe(string maKhoa)
        {
            return _sql = string.Format("select gi.{0},count(gi.{0}) as SoLuong from {1}.{2} dmp left join {1}.{3} btd"
                                       + " on dmp.{4}=btd.{5} left join {1}.{6} gi on gi.{7} = dmp.{8} where dmp.{9}={10} group by gi.{11}",
                                       cls_PG_DanhMucGiuong.col_TINHTRANG,
                                       _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, cls_BTDKP_BV.tb_TenBang, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_MaKP, cls_PG_DanhMucGiuong.tb_TenBang,
                                       cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_DanhMucPhong.col_MAKP, maKhoa, cls_PG_DanhMucGiuong.col_TINHTRANG);
        }
        #endregion

        #region Lấy query thống kê tất cả giường theo khoa và phòng
        public string GetQueryThongKe(string maKhoa, string maPhong)
        {
            return _sql = string.Format("select gi.{0},count(gi.{1}) as SoLuong from {2}.{3} dmp left join {2}.{4} btd"
                + " on dmp.{5}=btd.{6} left join {2}.{7} gi on gi.{8} = dmp.{9} where dmp.{10}={11} and dmp.{12}='{13}'  group by gi.{14}",
                cls_PG_DanhMucGiuong.col_TINHTRANG, cls_PG_DanhMucGiuong.col_TINHTRANG,
                _acc.Get_User(), cls_DanhMucPhong.tb_TenBang, cls_BTDKP_BV.tb_TenBang, cls_DanhMucPhong.col_MAKP, cls_BTDKP_BV.col_MaKP, cls_PG_DanhMucGiuong.tb_TenBang,
                cls_PG_DanhMucGiuong.col_MAPHONG, cls_DanhMucPhong.col_MAPHONG, cls_DanhMucPhong.col_MAKP, maKhoa, cls_DanhMucPhong.col_MA, maPhong, cls_PG_DanhMucGiuong.col_TINHTRANG);

        }
        #endregion

        #endregion



        #region Lấy danh sách bệnh nhân chờ xuất giường

        public DataTable GetThongTinBenhNhanChoXuatGiuong(string makp = "")
        {
            string where = "";
            if (!string.IsNullOrEmpty(makp))
            {
                where = " AND tdg." + cls_PG_TheoDoiGiuong.col_MAKP + " = '" + makp + "' ";
            }
            _sql = string.Format("select distinct tdg." + cls_PG_TheoDoiGiuong.col_ID + ", tdg.{0},gi." + cls_PG_DanhMucGiuong.col_TEN + ",tdg.{1},btdkp.{2},tdg.{3},btd.{4},tdg.{5},tdg.{6},btd.{7}"
                          + " from "
                          //  + "" + _acc.Get_UserMMYY() + ".V_TTRVDS vp inner join  {8}.{9} tdg on vp.{18} =  tdg.{18}"
                          + " {8}.{9} tdg "
                          + " inner join {8}.{10} btdkp on btdkp.{11}=tdg.{12}"
                          + " inner join {8}.{13} btd on tdg.{14}=btd.{15}"
                          + " inner join {8}.{17} xv on tdg.{18}=xv.{19} and ( tdg.maql = xv.maql OR tdg.maql is null ) "
                          + " inner join {8}." + cls_PG_DanhMucGiuong.tb_TenBang + " gi on tdg.{0} = gi." + cls_PG_DanhMucGiuong.col_MA + " "
                          + " WHERE ( tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + "='1' ) AND tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAI + "='0' AND tdg.{14} not in ( select " + cls_HienDien.col_MaBN + " from {8}." + cls_HienDien.tb_TenBang + ") " + where
                          , cls_PG_TheoDoiGiuong.col_IDGIUONG, cls_PG_TheoDoiGiuong.col_MAKP, cls_BTDKP_BV.col_TenKP, cls_PG_TheoDoiGiuong.col_MABN,
                          cls_BTDBN.col_HoTen, cls_PG_TheoDoiGiuong.col_TU, cls_PG_TheoDoiGiuong.col_DEN, cls_BTDBN.col_Thon, _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_BTDKP_BV.tb_TenBang,
                          cls_BTDKP_BV.col_MaKP, cls_PG_TheoDoiGiuong.col_MAKP, cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN, cls_BTDBN.col_MaBN, cls_XuatVien.col_NGAYUD, cls_XuatVien.tb_TenBang, cls_XuatVien.col_MaBN, cls_XuatVien.col_MaBN);
            DataTable tb = _acc.Get_Data(_sql);

            return tb;
        }

        public DataTable GetThongTinBenhNhanChoXuatGiuongfrm(string makp = "")
        {
            string where = "";
            if (!string.IsNullOrEmpty(makp))
            {
                where = " AND tdg." + cls_PG_TheoDoiGiuong.col_MAKP + " = '" + makp + "' ";
            }
            _sql = string.Format("select distinct tdg." + cls_PG_TheoDoiGiuong.col_ID + ", tdg.{0},gi." + cls_PG_DanhMucGiuong.col_TEN + ",tdg.{1},btdkp.{2},tdg.{3},btd.{4},tdg.{5},tdg.{6},btd.{7}"
                          + " from "
                          //  + "" + _acc.Get_UserMMYY() + ".V_TTRVDS vp inner join  {8}.{9} tdg on vp.{18} =  tdg.{18}"
                          + " {8}.{9} tdg "
                          + " inner join {8}.{10} btdkp on btdkp.{11}=tdg.{12}"
                          + " inner join {8}.{13} btd on tdg.{14}=btd.{15}"
                          + " inner join {8}.{17} xv on tdg.{18}=xv.{19} and ( tdg.maql = xv.maql OR tdg.maql is null ) "
                          + " inner join {8}." + cls_PG_DanhMucGiuong.tb_TenBang + " gi on tdg.{0} = gi." + cls_PG_DanhMucGiuong.col_MA + " "
                          + " WHERE ( tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + "='2' OR tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAIBENHNHAN + "='1' ) AND tdg." + cls_PG_TheoDoiGiuong.col_TRANGTHAI + "='0' AND tdg.{14} not in ( select " + cls_HienDien.col_MaBN + " from {8}." + cls_HienDien.tb_TenBang + ") " + where
                          , cls_PG_TheoDoiGiuong.col_IDGIUONG, cls_PG_TheoDoiGiuong.col_MAKP, cls_BTDKP_BV.col_TenKP, cls_PG_TheoDoiGiuong.col_MABN,
                          cls_BTDBN.col_HoTen, cls_PG_TheoDoiGiuong.col_TU, cls_PG_TheoDoiGiuong.col_DEN, cls_BTDBN.col_Thon, _acc.Get_User(), cls_PG_TheoDoiGiuong.tb_TenBang, cls_BTDKP_BV.tb_TenBang,
                          cls_BTDKP_BV.col_MaKP, cls_PG_TheoDoiGiuong.col_MAKP, cls_BTDBN.tb_TenBang, cls_PG_TheoDoiGiuong.col_MABN, cls_BTDBN.col_MaBN, cls_XuatVien.col_NGAYUD, cls_XuatVien.tb_TenBang, cls_XuatVien.col_MaBN, cls_XuatVien.col_MaBN);
            DataTable tb = _acc.Get_Data(_sql);

            return tb;
        }
        #endregion

        #region Lấy tên máy
        public string GetMachine()
        {
            _sql = string.Format("select SYS_CONTEXT('USERENV','IP_ADDRESS')||'+'||Userenv('TERMINAL')||'+'||SYS_CONTEXT('USERENV','MODULE') from dual");
            DataTable tb = _acc.Get_Data(_sql);
            string kq = string.Empty;
            if (tb.Rows.Count > 0)
            {
                kq = tb.Rows[0][0].ToString() == string.Empty ? "" : tb.Rows[0][0].ToString();
            }
            return kq;
        }
        #endregion

    }
}
#endregion
