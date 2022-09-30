using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversityAPI.Models
{
    public class AccountOut
    {
        public int account_id { get; set; }
        public string ac_first_name { get; set; }
        public string ac_last_name { get; set; }
        public string ac_email { get; set; }
        public int ac_user_type_cod { get; set; }
        public DateTime? ac_expiration_date { get; set; } = null;
        public string ac_phone { get; set; }
        public string ac_mobile { get; set; }
        public string ac_active { get; set; }
        public string ac_note { get; set; }
        public string ac_token { get; set; }
    }

    public class AccountOut2
    {
        public int account_id { get; set; }
        public string ac_first_name { get; set; }
        public string ac_last_name { get; set; }
        public string ac_email { get; set; }
        public int ac_user_type_cod { get; set; }
        public DateTime ac_expiration_date { get; set; }
        public string ac_phone { get; set; }
        public string ac_mobile { get; set; }
        public string ac_photo_url { get; set; }
        public string ac_active { get; set; }
        public string ac_note { get; set; }
        public string tab_user_type_desc { get; set; }
    }

    public class AccountIn
    {
        public string email { get; set; }
        public string password { get; set; }
        public string ip_address { get; set; }
    }

    public class UserName
    {
        public string username { get; set; }
    }

    public class UserPassword
    {
        public string old_password { get; set; }
        public string new_password { get; set; }
        public string rep_password { get; set; }
    }

    public class AccountId
    {
        public int account_id { get; set; }
    }
}
