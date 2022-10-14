using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversityAPI.HelperClasses
{
    public class ErrorMessage
    {
        public string msg_type { set; get; } = string.Empty;
        public int msg_code { set; get; } = 0;
        public string msg_text { set; get; } = string.Empty;
    }
}