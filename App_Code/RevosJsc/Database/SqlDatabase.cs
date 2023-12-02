using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RevosJsc.Database
{
    public class SqlDatabase
    {
        private static string _connectionString = string.Empty;

        public static string ConnectionString
        {
            get
            {
                if (_connectionString.Equals(""))
                {
                    ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                    //_connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static OleDbConnection GetConnection()
        {
            Check();
            var conn = new OleDbConnection {ConnectionString = ConnectionString};
            conn.Open();
            return conn;
        }

        public static void ExecuteNoneQuery(OleDbCommand cmd)
        {
            if (cmd.Connection != null)
            {
                cmd.ExecuteNonQuery();
            }
            else
            {
                var conn = GetConnection();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
        }
        public static string ExecuteScalar(OleDbCommand cmd)
        {
            if (cmd.Connection != null)
            {
                var returnObj = cmd.ExecuteScalar();
                var returnValue = -1;
                if (returnObj != null)
                {
                    int.TryParse(returnObj.ToString(), out returnValue);
                }
                return returnValue.ToString();
            }
            else
            {
                var conn = GetConnection();
                cmd.Connection = conn;
                var returnObj = cmd.ExecuteScalar();
                var returnValue = -1;
                if (returnObj != null)
                {
                    int.TryParse(returnObj.ToString(), out returnValue);
                }
                return returnValue.ToString();
            }
        }

        public static DataTable GetData(OleDbCommand cmd)
        {
            if (cmd.Connection != null)
            {
                using (var ds = new DataSet())
                {
                    using (var da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
            using (var conn = GetConnection())
            {
                cmd.Connection = conn;
                using (var ds = new DataSet())
                {
                    using (var da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
        }

        public static DataSet GetData_OverDataset(OleDbCommand cmd)
        {
            if (cmd.Connection != null)
            {
                using (var ds = new DataSet())
                {
                    using (var da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            using (var conn = GetConnection())
            {
                cmd.Connection = conn;
                using (var ds = new DataSet())
                {
                    using (var da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public static string Build(string web)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes("dominetlicense" + web + "171006"));
            var sBuilder = new StringBuilder();
            foreach (var i in data)
            {
                sBuilder.Append(i.ToString("x3"));
            }
            return sBuilder.ToString();
        }
        public static void Check()
        {
            var key = ConfigurationManager.AppSettings["LicenseKey"];
            var web = HttpContext.Current.Request.Url.Host.ToLower().Replace("www.", "");
            if (web.Equals("localhost") || web.Contains("dominet.com.vn") || web.Contains("revos")) return;
            if (key.Split(new [] { "_" }, StringSplitOptions.RemoveEmptyEntries).Any(license => Build(web) == license))
            {
                return;
            }
            HttpContext.Current.Response.Redirect("/");
        }
    }
}