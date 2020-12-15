using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public static class ManageDatetime
    {
        public static object DateTimeHandler(string date_param)
        {
            if (date_param == null)
            {
                return DBNull.Value;

            }
            else if (date_param.Trim() == string.Empty)
            {
                return DBNull.Value;
            }
            else
            {
                return DateTime.Parse(date_param).ToString("yyyy-MM-dd HH:mm:ss:fff");
            }
        }
    }
}
