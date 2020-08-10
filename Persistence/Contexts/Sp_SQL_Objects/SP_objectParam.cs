using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;

namespace evoting.Persistence.Contexts.Sp_SQL_Objects
{
    public class SP_objectParam
    {
        
        public static string Stringify_SP_Query(Type _classType, object paramList)
        {
            string param = string.Empty;
            List<string> sql_paramList = new List<string>();
            PropertyInfo[] properties = _classType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string _val = property.GetValue(paramList).ToString();
                if (property.PropertyType.Name.ToLower().Equals("string"))
                {
                    _val = "'" + _val + "'";
                }
                sql_paramList.Add(" @" + property.Name + "=" + _val);
            }
            
            param = "EXEC [dbo].[" + _classType.Name + "] " + string.Join(',', sql_paramList.ToArray());
            return param;
        }
        public class sp_SampleSPExec
        {
            public int flag { get; set; }
            public string param1 { get; set; } = "";
        }
    }
}
