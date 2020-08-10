using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using evoting.Persistence.Contexts;
using evoting.Persistence.Contexts.Sp_SQL_Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using static evoting.Persistence.Contexts.Sp_SQL_Objects.SP_objectParam;
using System.Data;
using Microsoft.Data.SqlClient;
using evoting.Domain.Models;

namespace evoting.Services
{
    public interface IUserService
    {
        Task<DataTable> LoginDataUser(FJC_LoginRequest fJC_Login);       
    }

    public class UserService : IUserService
    {
        //db context here
        protected readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }   


        public async Task<DataTable> LoginDataUser(FJC_LoginRequest fJC_Login)
        {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>();                
                dictLogin.Add("@Password", fJC_Login.encrypt_Password);
                dictLogin.Add("@DPIIDCLID", fJC_Login.UserID);
                dictLogin.Add("@IP_Address", fJC_Login.system_ip);
                // dictLogin.Add("@TokenId", TokenId);
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_LoginSession_Details", dictLogin);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_LoginSession_Detai=awals", dictLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
 