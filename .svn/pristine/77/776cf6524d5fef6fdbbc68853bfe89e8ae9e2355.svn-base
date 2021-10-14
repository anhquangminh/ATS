using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ATS_AlarmTrackingSystem.Models
{
    public class PCIssue
    {
        public string USERNAME { set; get; }
        public string SYSTEM { set; get; }
        public string Port1433 { set; get; }
        public string Port445 { set; get; }
        public string Port3389 { set; get; }
        public string USB { set; get; }
        public string USBPROTECT { set; get; }
        public string SYMANTECTUPDATE { set; get; }
        public string VIRUS_PROTECTION { set; get; }
    }
    public class GetInforPC
    {
        DBConnection db;
        public GetInforPC()
        {
            db = new DBConnection();
        }
        public string GetQuery(string where, string column)
        {
            try {
                if (!string.IsNullOrEmpty(where) || !string.IsNullOrEmpty(column))
                {
                    string sql = "select * from MES1.C_PC_CONTROL_T Where ";
                    sql = sql + where;
                    using (var conn = new OracleConnection(db.getOracleConnection()))
                    {
                        conn.Open();
                        using (var cmd = new OracleCommand(sql, conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                var dt = new DataTable();
                                dt.Load(reader);
                                string name = "";
                                foreach (DataRow row in dt.Rows)
                                {
                                    name = row[column].ToString();
                                }
                                return name;
                            }
                        }
                    }
                }
                else
                {
                    return "";
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }

        }
    }
}