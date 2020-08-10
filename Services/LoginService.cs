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
using evoting.Utility;

namespace evoting.Services
{
    public interface ILoginService
    {     
        
        Task<DataTable> ChangePasswordData(FJC_ChangePassword fJC_changePwd);
        Task<DataTable> ForgotPasswordData(FJC_ForgotPassword fJC_forgot);
        Task<DataTable> ForgotPassword_DOB_Data(FJC_ForgotPassword fJC_forgot);
        Task<DataTable> ForgotPassword_PAN_ID_Data(FJC_ForgotPassword fJC_forgot);
        Task<DataTable> ForgotPassword_EmailID_Data(FJC_ForgotPassword fJC_forgot);
        Task<DataTable> GetInvestorEmailIDData(FJC_ForgotPassword fJC_forgot);
    }

    public class LoginService : ILoginService
    {
        //db context here
        protected readonly AppDbContext _context;
        public LoginService(AppDbContext context)
        {
            _context = context;
        } 
        public async Task<DataTable> ChangePasswordData(FJC_ChangePassword fJC_changePwd)
        {
            try
            {
                Dictionary<string, object> dictChangePwd = new Dictionary<string, object>();
                dictChangePwd.Add("@DPIIDCLID", fJC_changePwd.UserID);
                dictChangePwd.Add("@CurrPassword", DecryptPassword.Decrypt_Password(fJC_changePwd.encrypt_OldPassword));
                dictChangePwd.Add("@NewPassword", DecryptPassword.Decrypt_Password(fJC_changePwd.encrypt_NewPassword));
                
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ChangePassword", dictChangePwd);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_ChangePassword", dictChangePwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataTable> ForgotPasswordData(FJC_ForgotPassword fJC_forgot)
        {
            try
            {
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();                
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID);               
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataTable> ForgotPassword_DOB_Data(FJC_ForgotPassword fJC_forgot)
        {
            try
            {
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID);
                dictForgotPwd.Add("@DOB", fJC_forgot.DOB);
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DataTable> ForgotPassword_PAN_ID_Data(FJC_ForgotPassword fJC_forgot)
        {
            try
            {
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID);
                dictForgotPwd.Add("@PANID", fJC_forgot.PAN_ID);
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DataTable> ForgotPassword_EmailID_Data(FJC_ForgotPassword fJC_forgot)
        {
            try
            {
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID);
                dictForgotPwd.Add("@EMAILID", fJC_forgot.EmailID);
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DataTable> GetInvestorEmailIDData(FJC_ForgotPassword fJC_forgot)
        {
            try
            {
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();               
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID);               
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetInvestorEmailID", dictForgotPwd);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_GetInvestorEmailID", dictForgotPwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
 