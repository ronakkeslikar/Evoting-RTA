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
        public static string ConStr = "Data Source=192.168.0.251;Initial Catalog=evoting;Persist Security Info=True;User ID=sa;Password=bigshare@123";
        public static string ErrorMsg;
        public static Task<DataSet> GetDataSet(string procname, Dictionary<string,object> keyValues)
        {            
            try
            {
                return Task.Run(() =>
                {                    
                    DataSet _dt = new DataSet();
                    SqlConnection con = new SqlConnection(ConStr);
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


    }
}
