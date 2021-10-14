using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class Isolation
    {
        public string id { set; get; }
        public string NhaXuong { set; get; }
        public string PhapNhan { set; get; }
        public string ToaXuong { set; get; }
        public string BU { set; get; }
        public string CFT { set; get; }
        public string BoPhan { set; get; }
        public string Chuyen { set; get; }
        public string CaLV { set; get; }
        public string IDL { set; get; }
        public string MaNhanVien { set; get; }
        public string TenTV { set; get; }
        public string TenTQ { set; get; }
        public string GioiTinh { set; get; }
        public string NgaySinh { set; get; }
        public string DienThoai { set; get; }
        public string PhuongTien { set; get; }
        public string DCTamTru { set; get; }
        public string DCThuongTru { set; get; }
        public string LoaiHinhCNV { set; get; }
        public string NgayLVCuoi { set; get; }
        public string NgayTiepXuc { set; get; }
        public string CachLyNgay { set; get; }
        public string DuKienKetThucCL { set; get; }
        public string ChonDoTX { set; get; }
        public string NguyenNhanCT { set; get; }
        public string LoaiHinhCL { set; get; }
        public string PhuongThucCL { set; get; }
        public string NgayXN { set; get; }
        public string DiaDiemXN { set; get; }
        public string TiemVaccien { set; get; }
        public string ThoiGianTiem { set; get; }
        public string DiaDiemTiem { set; get; }
        public string DaKetThucCL { set; get; }
        public string TinhTrangCL { set; get; }
        public string NgayKTCL { set; get; }
        public string TinhTrang { set; get; }
        public string NguoiTaiLen { set; get; }
        public string NgayTaiLen { set; get; }

    }

    public class ScanPort
    {
        public string id { set; get; }
        public string MaThe { set; get; }
        public string HoTen { set; get; }
        public string Site { set; get; }
        public string PhanNhan { set; get; }
        public string BU { set; get; }
        public string CFT { set; get; }
        public string BoPhan { set; get; }
        public string XuongLV { set; get; }
        public string ViTriQuet { set; get; } 
        public string NguoiTaiLen { set; get; }
        public string NgayTaiLen { set; get; }
    }
    public class VACCINE
    {
        public string emp_no { set; get; }
        public string emp_name { set; get; }
        public string mac { set; get; }
        public string ip { set; get; }
        public string fbdesc { set; get; }
        public string scan_out_time { set; get; }
    }

    public class DBOhelper
    {
        string conString = @"Data Source=10.224.81.131,3000;Initial Catalog=OWNCLOUD_DATA;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
        public Boolean Execute(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public List<Isolation> getListIsolation()
        {
            List<Isolation> ListIsolation = new List<Isolation>();

            DBOhelper helper = new DBOhelper();
            DataTable dt = new DataTable();
            string sql = "select * from [dbo].[ThongTinCL] order by NgayTaiLen desc";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Isolation iso = new Isolation();
                    //NhaXuong,PhapNhan,ToaXuong,BU,CFT,BoPhan,Chuyen,CaLV,IDL,MaNhanVien,TenTV,TenTQ,GioiTinh,NgaySinh,DienThoai,
                   
                    iso.id = dt.Rows[i][0].ToString().Trim();
                    iso.NhaXuong = dt.Rows[i][1].ToString().Trim();
                    iso.PhapNhan = dt.Rows[i][2].ToString().Trim();
                    iso.ToaXuong = dt.Rows[i][3].ToString().Trim();
                    iso.BU = dt.Rows[i][4].ToString().Trim();
                    iso.CFT = dt.Rows[i][5].ToString().Trim();
                    iso.BoPhan = dt.Rows[i][6].ToString().Trim();
                    iso.Chuyen = dt.Rows[i][7].ToString().Trim();
                    iso.CaLV = dt.Rows[i][8].ToString().Trim();
                    iso.IDL = dt.Rows[i][9].ToString().Trim();
                    iso.MaNhanVien = dt.Rows[i][10].ToString().Trim();
                    iso.TenTV = dt.Rows[i][11].ToString().Trim();
                    iso.TenTQ = dt.Rows[i][12].ToString().Trim();
                    iso.GioiTinh = dt.Rows[i][13].ToString().Trim();
                    iso.NgaySinh = dt.Rows[i][14].ToString().Trim();
                    iso.DienThoai = dt.Rows[i][15].ToString().Trim();
                    //PhuongTien,DCTamTru,DCThuongTru,LoaiHinhCNV,NgayLVCuoi
                    
                    iso.PhuongTien = dt.Rows[i][16].ToString().Trim();
                    iso.DCTamTru = dt.Rows[i][17].ToString().Trim();
                    iso.DCThuongTru = dt.Rows[i][18].ToString().Trim();
                    iso.LoaiHinhCNV = dt.Rows[i][19].ToString().Trim();
                    iso.NgayLVCuoi = dt.Rows[i][20].ToString().Trim();
                    //,NgayTiepXuc,CachLyNgay,DuKienKetThucCL,ChonDoTX,NguyenNhanCT,LoaiHinhCL,PhuongThucCL,
                    iso.NgayTiepXuc = dt.Rows[i][21].ToString().Trim();
                    iso.CachLyNgay = dt.Rows[i][22].ToString().Trim();
                    iso.DuKienKetThucCL = dt.Rows[i][23].ToString().Trim();
                    iso.ChonDoTX = dt.Rows[i][24].ToString().Trim();
                    iso.NguyenNhanCT = dt.Rows[i][25].ToString().Trim();
                    iso.LoaiHinhCL = dt.Rows[i][26].ToString().Trim();
                    iso.PhuongThucCL = dt.Rows[i][27].ToString().Trim();
                    //DiaDiemXN,TiemVaccien,ThoiGianTiem,DiaDiemTiem,DaKetThucCL,TinhTrangCL,NgayKTCL,TinhTrang,NguoiTaiLen,NgayTaiLen
                    iso.NgayXN = dt.Rows[i][28].ToString().Trim();
                    iso.DiaDiemXN = dt.Rows[i][29].ToString().Trim();
                    iso.TiemVaccien = dt.Rows[i][30].ToString().Trim();
                    iso.ThoiGianTiem = dt.Rows[i][31].ToString().Trim();
                    iso.DiaDiemTiem = dt.Rows[i][32].ToString().Trim();

                    iso.DaKetThucCL = dt.Rows[i][33].ToString().Trim();
                    iso.TinhTrangCL = dt.Rows[i][34].ToString().Trim();
                    iso.NgayKTCL = dt.Rows[i][35].ToString().Trim();

                    iso.TinhTrang = dt.Rows[i][36].ToString().Trim();

                    iso.NguoiTaiLen = dt.Rows[i][37].ToString().Trim();
                    iso.NgayTaiLen = dt.Rows[i][38].ToString().Trim();
                   
                    ListIsolation.Add(iso);
                }

            }
            return ListIsolation;
        }
        public List<Isolation> getByID(string id)
        {
            List<Isolation> ListIsolation = new List<Isolation>();

            DBOhelper helper = new DBOhelper();
            DataTable dt = new DataTable();
            string sql = "select * from [dbo].[ThongTinCL] where id='"+id+"'";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Isolation iso = new Isolation();

                    iso.id = dt.Rows[i][0].ToString().Trim();
                    iso.NhaXuong = dt.Rows[i][1].ToString().Trim();
                    iso.PhapNhan = dt.Rows[i][2].ToString().Trim();
                    iso.ToaXuong = dt.Rows[i][3].ToString().Trim();
                    iso.BU = dt.Rows[i][4].ToString().Trim();
                    iso.CFT = dt.Rows[i][5].ToString().Trim();
                    iso.BoPhan = dt.Rows[i][6].ToString().Trim();
                    iso.Chuyen = dt.Rows[i][7].ToString().Trim();
                    iso.CaLV = dt.Rows[i][8].ToString().Trim();
                    iso.IDL = dt.Rows[i][9].ToString().Trim();
                    iso.MaNhanVien = dt.Rows[i][10].ToString().Trim();
                    iso.TenTV = dt.Rows[i][11].ToString().Trim();
                    iso.TenTQ = dt.Rows[i][12].ToString().Trim();
                    iso.GioiTinh = dt.Rows[i][13].ToString().Trim();
                    iso.NgaySinh = dt.Rows[i][14].ToString().Trim();
                    iso.DienThoai = dt.Rows[i][15].ToString().Trim();
                    //PhuongTien,DCTamTru,DCThuongTru,LoaiHinhCNV,NgayLVCuoi

                    iso.PhuongTien = dt.Rows[i][16].ToString().Trim();
                    iso.DCTamTru = dt.Rows[i][17].ToString().Trim();
                    iso.DCThuongTru = dt.Rows[i][18].ToString().Trim();
                    iso.LoaiHinhCNV = dt.Rows[i][19].ToString().Trim();
                    iso.NgayLVCuoi = dt.Rows[i][20].ToString().Trim();
                    //,NgayTiepXuc,CachLyNgay,DuKienKetThucCL,ChonDoTX,NguyenNhanCT,LoaiHinhCL,PhuongThucCL,
                    iso.NgayTiepXuc = dt.Rows[i][21].ToString().Trim();
                    iso.CachLyNgay = dt.Rows[i][22].ToString().Trim();
                    iso.DuKienKetThucCL = dt.Rows[i][23].ToString().Trim();
                    iso.ChonDoTX = dt.Rows[i][24].ToString().Trim();
                    iso.NguyenNhanCT = dt.Rows[i][25].ToString().Trim();
                    iso.LoaiHinhCL = dt.Rows[i][26].ToString().Trim();
                    iso.PhuongThucCL = dt.Rows[i][27].ToString().Trim();
                    //DiaDiemXN,TiemVaccien,ThoiGianTiem,DiaDiemTiem,DaKetThucCL,TinhTrangCL,NgayKTCL,TinhTrang,NguoiTaiLen,NgayTaiLen
                    iso.NgayXN = dt.Rows[i][28].ToString().Trim();
                    iso.DiaDiemXN = dt.Rows[i][29].ToString().Trim();
                    iso.TiemVaccien = dt.Rows[i][30].ToString().Trim();
                    iso.ThoiGianTiem = dt.Rows[i][31].ToString().Trim();
                    iso.DiaDiemTiem = dt.Rows[i][32].ToString().Trim();

                    iso.DaKetThucCL = dt.Rows[i][33].ToString().Trim();
                    iso.TinhTrangCL = dt.Rows[i][34].ToString().Trim();
                    iso.NgayKTCL = dt.Rows[i][35].ToString().Trim();

                    iso.TinhTrang = dt.Rows[i][36].ToString().Trim();

                    iso.NguoiTaiLen = dt.Rows[i][37].ToString().Trim();
                    iso.NgayTaiLen = dt.Rows[i][38].ToString().Trim();

                    ListIsolation.Add(iso);
                }

            }
            return ListIsolation;
        }
        public DataTable queryDatatable(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                    conn.Close();
                    da.Dispose();
                    conn.Close();
                    return table;
                }

            }

        }

        public List<ScanPort> getListScanPort()
        {
            List<ScanPort> ListScanPort = new List<ScanPort>();

            DBOhelper helper = new DBOhelper();
            DataTable dt = new DataTable();
            string sql = "select * from ViTriQuetThe";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ScanPort sp = new ScanPort();
                    //id,MaThe,HoTen,Site,PhanNhan,BU,CFT,BoPhan,XuongLV,ViTriQuet,NguoiTaiLen,NgayTaiLen

                    sp.id = dt.Rows[i][0].ToString().Trim();
                    sp.MaThe = dt.Rows[i][1].ToString().Trim();
                    sp.HoTen = dt.Rows[i][2].ToString().Trim();
                    sp.Site = dt.Rows[i][3].ToString().Trim();
                    sp.PhanNhan = dt.Rows[i][4].ToString().Trim();
                    sp.BU = dt.Rows[i][5].ToString().Trim();
                    sp.CFT = dt.Rows[i][6].ToString().Trim();
                    sp.BoPhan = dt.Rows[i][7].ToString().Trim();
                    sp.XuongLV = dt.Rows[i][8].ToString().Trim();
                    sp.ViTriQuet = dt.Rows[i][9].ToString().Trim();
                    sp.NguoiTaiLen = dt.Rows[i][10].ToString().Trim();
                    sp.NgayTaiLen = dt.Rows[i][11].ToString().Trim();
                    
                    ListScanPort.Add(sp);
                }

            }
            return ListScanPort;
        }

        public List<ScanPort> getListScanPortByID(string id)
        {
            List<ScanPort> ListScanPort = new List<ScanPort>();

            DBOhelper helper = new DBOhelper();
            DataTable dt = new DataTable();
            string sql = "select id,MaThe,HoTen,Site,PhanNhan,BU,CFT,BoPhan,XuongLV,ViTriQuet,NguoiTaiLen,NgayTaiLen from ViTriQuetThe where id ="+id;
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ScanPort sp = new ScanPort();
                    //id,MaThe,HoTen,Site,PhanNhan,BU,CFT,BoPhan,XuongLV,ViTriQuet,NguoiTaiLen,NgayTaiLen

                    sp.id = dt.Rows[i][0].ToString().Trim();
                    sp.MaThe = dt.Rows[i][1].ToString().Trim();
                    sp.HoTen = dt.Rows[i][2].ToString().Trim();
                    sp.Site = dt.Rows[i][3].ToString().Trim();
                    sp.PhanNhan = dt.Rows[i][4].ToString().Trim();
                    sp.CFT = dt.Rows[i][5].ToString().Trim();
                    sp.BoPhan = dt.Rows[i][6].ToString().Trim();
                    sp.XuongLV = dt.Rows[i][7].ToString().Trim();
                    sp.ViTriQuet = dt.Rows[i][8].ToString().Trim();
                    sp.NguoiTaiLen = dt.Rows[i][9].ToString().Trim();
                    sp.NguoiTaiLen = dt.Rows[i][10].ToString().Trim();

                    ListScanPort.Add(sp);
                }

            }
            return ListScanPort;
        }
    }

    class DBCMMHelper
    {
        string conString = @"Data Source=10.224.52.12,1433;Initial Catalog=VN_Door_162;Persist Security Info=True;User ID=FG_IT;Password=FG_IT";
        public Boolean Execute(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public DataTable queryDatatable(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                    conn.Close();
                    da.Dispose();
                    conn.Close();
                    return table;
                }

            }
        }

    }
    public class VitriQuet {
        public string id { set; get; }
        public string EmpNo { set; get; }
        public string Lat { set; get; }
        public string Lon { set; get; }
        public string Province { set; get; }
        public string City { set; get; }
        public string Date { set; get; }
        public string DateCheck { set; get; }
        public string  DayNight { set; get; }
    }

    public class DBWechat
    {
        string conString = @"Data Source=10.224.81.131,3000;Initial Catalog=WechatDB;Persist Security Info=True;User ID=sa;Password=foxconn168!!";
        public Boolean Execute(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public List<VitriQuet> getList(string sql)
        {
            List<VitriQuet> ListIsolation = new List<VitriQuet>();

            DBWechat helper = new DBWechat();
            DataTable dt = new DataTable();
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VitriQuet iso = new VitriQuet();
                    //NhaXuong,PhapNhan,ToaXuong,BU,CFT,BoPhan,Chuyen,CaLV,IDL,MaNhanVien,TenTV,TenTQ,GioiTinh,NgaySinh,DienThoai,

                    iso.id = dt.Rows[i][0].ToString().Trim();
                    iso.EmpNo = dt.Rows[i][1].ToString().Trim();
                    iso.Lat = dt.Rows[i][2].ToString().Trim();
                    iso.Lon = dt.Rows[i][3].ToString().Trim();
                    iso.Province = dt.Rows[i][4].ToString().Trim();
                    iso.City = dt.Rows[i][5].ToString().Trim();
                    iso.Date = dt.Rows[i][6].ToString().Trim();
                    iso.DateCheck = dt.Rows[i][7].ToString().Trim();
                    iso.DayNight = dt.Rows[i][8].ToString().Trim();

                    ListIsolation.Add(iso);
                }

            }
            return ListIsolation;
        }

        public DataTable queryDatatable(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                    conn.Close();
                    da.Dispose();
                    conn.Close();
                    return table;
                }

            }

        }

    }
}