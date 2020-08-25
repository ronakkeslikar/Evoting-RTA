using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Persistence.Contexts
{
    public class AppDBCalls
    {
        public static string ConStr1 = "Data Source=192.168.0.251;Initial Catalog=evoting;User ID=sa;Password=bigshare@123";
        public static string ConStr = "Data Source=BIGSHARE-WEBSVR;Initial Catalog=evoting;User ID=sa;Password=p@ssw0rd@321";
        public static string Rel_connection;
        public static string ErrorMsg;
        public static Task<DataSet> GetDataSet(string procname, Dictionary<string,object> keyValues)
        {            
            try
            {
                return Task.Run(() =>
                {                    
                    DataSet _dt = new DataSet();
                    SqlConnection con = new SqlConnection(Rel_connection);
                    
                    SqlCommand cmd = new SqlCommand(procname, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var item in keyValues)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    if (con.State == ConnectionState.Open) { con.Close(); } else { con.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(_dt);
                    return _dt;
                });
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message.ToString();
                return null;
            }
        }

        private static bool DBConnectionStatus()
        {
            try
            {
                using (SqlConnection sqlConn =
                    new SqlConnection(ConStr))
                {
                    sqlConn.Open();
                    return (sqlConn.State == ConnectionState.Open);
                }
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SetDBConnect()
        {
            if(DBConnectionStatus())
            {
                Rel_connection = ConStr;
            }
            else
            {
                Rel_connection = ConStr1;
            }
        }




    }
}
