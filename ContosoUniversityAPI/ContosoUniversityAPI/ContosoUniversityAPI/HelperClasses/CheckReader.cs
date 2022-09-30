using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ContosoUniversityAPI.HelperClasses
{
    public static class CheckReader
    {
        public static T GetValue<T>(DbDataReader reader, string dbColumn, T orderField)
        {
            if (!(reader[dbColumn] is DBNull))
            {
                return (T)Convert.ChangeType(reader[dbColumn], typeof(T));
            }
            return default(T);
        }
    }
}