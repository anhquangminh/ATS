using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class KTX
    {
        public string id { set; get; }
        public string mathe { set; get; }
        public string hoten { set; get; }
        public string hotentq { set; get; }
        public string phapnhan { set; get; }
        public string khuxuong { set; get; }
        public string bu { set; get; }
        public string cft { set; get; }
        public string toanha { set; get; }
        public string bophan { set; get; }
        public string ngayvaoxuong { set; get; }
        public string phancap { set; get; }
        public string capbac { set; get; }
        public string gioitinh { set; get; }
        public string machiphi { set; get; }
        public string ngaysinh { set; get; }
        public string cmt { set; get; }
        public string ngayvaoKTX { set; get; }
        public string ngayraKTX { set; get; }
        public string quequan { set; get; }
        public string toaKTX { set; get; }
        public string sophong { set; get; }
        public string sogiuong { set; get; }
        public string ghichu { set; get; }
        public string nguoitailen { set; get; }
        public string thoigiantai { set; get; }
    }

    public class Positonx
    {
        public string Name_Positon { set; get; }
        public string Positon_intX { set; get; }
        public string Positon_intY { set; get; }
        public string Patient { set; get; }
    }

    public class KTXhelper
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
        public List<KTX> getListGA()
        {
            List<KTX> ListKTX = new List<KTX>();

            KTXhelper helper = new KTXhelper();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM KTX order by thoigiantai desc";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KTX ktx = new KTX();
                    ktx.id = dt.Rows[i][0].ToString().Trim();
                    ktx.mathe = dt.Rows[i][1].ToString().Trim();
                    ktx.hoten = dt.Rows[i][2].ToString().Trim();
                    ktx.hotentq = dt.Rows[i][3].ToString().Trim();
                    ktx.phapnhan = dt.Rows[i][4].ToString().Trim();
                    ktx.khuxuong = dt.Rows[i][5].ToString().Trim();
                    ktx.bu = dt.Rows[i][6].ToString().Trim();
                    ktx.cft = dt.Rows[i][7].ToString().Trim();
                    ktx.toanha = dt.Rows[i][8].ToString().Trim();
                    ktx.bophan = dt.Rows[i][9].ToString().Trim();
                    ktx.ngayvaoxuong = dt.Rows[i][10].ToString().Trim();
                    ktx.phancap = dt.Rows[i][11].ToString().Trim();
                    ktx.capbac = dt.Rows[i][12].ToString().Trim();
                    ktx.gioitinh = dt.Rows[i][13].ToString().Trim();
                    ktx.machiphi = dt.Rows[i][14].ToString().Trim();
                    ktx.ngaysinh = dt.Rows[i][15].ToString().Trim();
                    ktx.cmt = dt.Rows[i][16].ToString().Trim();
                    ktx.ngayvaoKTX = dt.Rows[i][17].ToString().Trim();
                    ktx.ngayraKTX = dt.Rows[i][18].ToString().Trim();
                    ktx.quequan = dt.Rows[i][19].ToString().Trim();
                    ktx.toaKTX = dt.Rows[i][20].ToString().Trim();
                    ktx.sophong = dt.Rows[i][21].ToString().Trim();
                    ktx.sogiuong = dt.Rows[i][22].ToString().Trim();
                    ktx.ghichu = dt.Rows[i][23].ToString().Trim();
                    ktx.nguoitailen = dt.Rows[i][24].ToString().Trim();
                    ktx.thoigiantai = dt.Rows[i][25].ToString().Trim();

                    ListKTX.Add(ktx);
                }
                
            }
            return ListKTX;
        }
        public List<KTX> getByID(string id)
        {
            List<KTX> ListKTX = new List<KTX>();

            KTXhelper helper = new KTXhelper();
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM KTX where id='"+id.Trim()+"'";
            dt = helper.queryDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    KTX ktx = new KTX();
                    ktx.id = dt.Rows[i][0].ToString().Trim();
                    ktx.mathe = dt.Rows[i][1].ToString().Trim();
                    ktx.hoten = dt.Rows[i][2].ToString().Trim();
                    ktx.hotentq = dt.Rows[i][3].ToString().Trim();
                    ktx.phapnhan = dt.Rows[i][4].ToString().Trim();
                    ktx.khuxuong = dt.Rows[i][5].ToString().Trim();
                    ktx.bu = dt.Rows[i][6].ToString().Trim();
                    ktx.cft = dt.Rows[i][7].ToString().Trim();
                    ktx.toanha = dt.Rows[i][8].ToString().Trim();
                    ktx.bophan = dt.Rows[i][9].ToString().Trim();
                    ktx.ngayvaoxuong = dt.Rows[i][10].ToString().Trim();
                    ktx.phancap = dt.Rows[i][11].ToString().Trim();
                    ktx.capbac = dt.Rows[i][12].ToString().Trim();
                    ktx.gioitinh = dt.Rows[i][13].ToString().Trim();
                    ktx.machiphi = dt.Rows[i][14].ToString().Trim();
                    ktx.ngaysinh = dt.Rows[i][15].ToString().Trim();
                    ktx.cmt = dt.Rows[i][16].ToString().Trim();
                    ktx.ngayvaoKTX = dt.Rows[i][17].ToString().Trim();
                    ktx.ngayraKTX = dt.Rows[i][18].ToString().Trim();
                    ktx.quequan = dt.Rows[i][19].ToString().Trim();
                    ktx.toaKTX = dt.Rows[i][20].ToString().Trim();
                    ktx.sophong = dt.Rows[i][21].ToString().Trim();
                    ktx.sogiuong = dt.Rows[i][22].ToString().Trim();
                    ktx.ghichu = dt.Rows[i][23].ToString().Trim();
                    ktx.nguoitailen = dt.Rows[i][24].ToString().Trim();
                    ktx.thoigiantai = dt.Rows[i][25].ToString().Trim();

                    ListKTX.Add(ktx);
                }
                
            }
            return ListKTX;
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

        public List<Positonx> getlistPos(string Position)
        {
            List<Positonx> list = new List<Positonx>();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("[dbo].stGetF1_2_3_FromF0", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@F0_Name_Position", Position));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Positonx ps = new Positonx();
                        ps.Name_Positon = rdr[0].ToString();
                        ps.Positon_intX = rdr[1].ToString();
                        ps.Positon_intY = rdr[2].ToString();
                        ps.Patient = rdr[3].ToString();
                        list.Add(ps);
                    }
                }

            }
            return list;
        }

    }
}