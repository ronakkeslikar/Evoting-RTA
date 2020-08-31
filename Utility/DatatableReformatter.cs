﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace evoting.Utility
{
    public class Reformatter
    {
        private static object First_FetchColumns(DataTable _dt, string[] _column_names) //This will remain until dynamic class creation utility is developed.
        {
            string format_columns = "new (" +  string.Join(',', _column_names) + ")";
            var check =  _dt.AsEnumerable().Select(format_columns);
            return check;
        }
        private static string Return_RowElement_Datatable(DataTable _dt)
        {
            string Regex_Condition = @"]$|^\[";
            string return_value = JsonConvert.SerializeObject(_dt);
            return_value = Regex.Replace(return_value, Regex_Condition, "").Replace("\\","");
            return return_value;
        }
        private static object Return_DynamicType_RowElement(DataTable dt)
        {
            return dt.Select().Select(x => x.ItemArray.Select((a, i) => 
            new { Name = dt.Columns[i].ColumnName, Value = a })
                    .ToDictionary(a => a.Name, a => a.Value)).First();
        }
        private static object Return_DynamicType_ListElement(DataTable dt)
        {
            return dt.Select().Select(x => x.ItemArray.Select((a, i) =>
            new { Name = dt.Columns[i].ColumnName, Value = a })
                    .ToDictionary(a => a.Name, a => a.Value));
        }

        public static object Response_Object(string _response_message,ref DataTable _dt)
        {
            if (_dt.Rows.Count == 1)
            {
                return new { StatusCode = 200, message = _response_message, data = Return_DynamicType_RowElement(_dt) };
            }
            else
            {
                return new { StatusCode = 200, message = _response_message, data = Return_DynamicType_ListElement(_dt) };
            }
        }
        public static DataTable Validate_DataTable(DataTable _dt)
        {
            if (!_dt.Columns.Contains("Error"))
            {
                return _dt;  
            }
            else
            {
                 (new HandleCatches()).Raise_DB_Exceptions(_dt.Rows[0][0].ToString());
                return null;
            }
        }
    }
}
