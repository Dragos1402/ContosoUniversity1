using ContosoUniversityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversityAPI.Services
{
    public interface IUserServ
    {
        AccountOut AccountOut { get; set; }
        List<AccountOut2> AccountsOut2 { get; set; }
        AccountOut2 AccountOut2 { get; set; }
        int ResultId { get; set; }

        string Login(AccountIn accountIn);
        string ResetPassword(string username);
        string ChangePassword(UserPassword userPassword, string token);
        string GetAccounts();
        string GetAccountById(int id);
        string SaveAccount(AccountOut2 account, string token);
        string DeleteAccount(AccountId accountId, string token);
    }
}
