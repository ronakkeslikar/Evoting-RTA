using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using evoting.Persistence.Contexts;
using System.Data;
using evoting.Domain.Models;
using evoting.Utility;

namespace evoting.Services
{
    public interface ILoginService
    {     
        Task<DataTable> LoginDataUser(FJC_LoginRequest fJC_Login); 
        Task<DataTable> ChangePasswordData(FJC_ChangePassword fJC_changePwd, string token);
        Task<DataTable> ForgotPasswordData(FJC_ForgotPassword fJC_forgot);
        Task<DataTable> ForgotPassword_DOB_Data(FJC_ForgotPassword fJC_forgot);        
        Task<DataTable> GetInvestorEmailIDData(string UserID);
        Task<DataTable> ForgotPassword_BANK_ACC_Data(FJC_ForgotPassword fJC_forgot);
    }

    public class LoginService : ILoginService
    {
        //db context here
        protected readonly AppDbContext _context;        
        public LoginService(AppDbContext context)
        {
            _context = context;
        } 
         public async Task<DataTable> LoginDataUser(FJC_LoginRequest fJC_Login)
        {
            
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@DPIIDCLID", fJC_Login.UserID);               
                dictLogin.Add("@Password", fJC_Login.encrypt_Password);               
                dictLogin.Add("@IP_Address", fJC_Login.system_ip);                                 
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_LoginSession_Details", dictLogin);
            return ds.Tables[0];            
        }
        public async Task<DataTable> ChangePasswordData(FJC_ChangePassword fJC_changePwd, string token)
        {
           
                Dictionary<string, object> dictChangePwd = new Dictionary<string, object>();
                dictChangePwd.Add("@DPIIDCLID", fJC_changePwd.UserID);
                dictChangePwd.Add("@CurrPassword", fJC_changePwd.encrypt_OldPassword);
                dictChangePwd.Add("@NewPassword", fJC_changePwd.encrypt_NewPassword); 
                dictChangePwd.Add("@token", token);               
   
                // dictChangePwd.Add("@CurrPassword", DecryptPassword.Decrypt_Password(fJC_changePwd.encrypt_OldPassword));
                // dictChangePwd.Add("@NewPassword", DecryptPassword.Decrypt_Password(fJC_changePwd.encrypt_NewPassword));
                
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ChangePassword", dictChangePwd);
                //mailing contents are here 
                if(ds.Tables[0].Columns.Contains("rowid"))
                    {
                        SendMail sendmail = new SendMail();
                        string EmailerType = "ChangePasswordEmailer";
                        int row_id = Convert.ToInt32(ds.Tables[0].Rows[0]["rowid"]);
                        sendmail.SendLetterMail(0, EmailerType, 0, row_id);  //aud_id missing changes done
                    }
                
               
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        public async Task<DataTable> ForgotPasswordData(FJC_ForgotPassword fJC_forgot)
        {
            
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();                
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID); 
                dictForgotPwd.Add("@EMAILID", fJC_forgot.EmailID);                            
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
                 
                 //mailing contents are here 
                if(ds.Tables[0].Columns.Contains("rowid"))
               {
                 SendMail sendmail=new SendMail();
                 string EmailerType="ForgotPasswordEmailer";
                 int row_id= Convert.ToInt32(ds.Tables[0].Rows[0]["rowid"]) ; 
                 sendmail.SendLetterMail(0,EmailerType,0,row_id);                             
                }
                return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        public async Task<DataTable> ForgotPassword_DOB_Data(FJC_ForgotPassword fJC_forgot)
        {           
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID);
                dictForgotPwd.Add("@PANID", fJC_forgot.PAN_ID);
                dictForgotPwd.Add("@DOB", fJC_forgot.DOB);
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
                //mailing contents are here 
                if(ds.Tables[0].Columns.Contains("rowid"))
                {
                SendMail sendmail = new SendMail();
                string EmailerType = "ForgotPasswordEmailer";
                int row_id = Convert.ToInt32(ds.Tables[0].Rows[0]["rowid"]);
                sendmail.SendLetterMail(0, EmailerType, 0, row_id);
                }
                return Reformatter.Validate_DataTable(ds.Tables[0]);             
        }
         public async Task<DataTable> ForgotPassword_BANK_ACC_Data(FJC_ForgotPassword fJC_forgot)
        {
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();               
                dictForgotPwd.Add("@DPIIDCLID", fJC_forgot.UserID);
                dictForgotPwd.Add("@PANID", fJC_forgot.PAN_ID);
                dictForgotPwd.Add("@Bank_AccNo", fJC_forgot.Bank_AccNo);             
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_ForgotPassword", dictForgotPwd);
                //mailing contents are here 
                if(ds.Tables[0].Columns.Contains("rowid"))
                {
                SendMail sendmail = new SendMail();
                string EmailerType = "ForgotPasswordEmailer";
                int row_id = Convert.ToInt32(ds.Tables[0].Rows[0]["rowid"]);
                sendmail.SendLetterMail(0, EmailerType, 0, row_id);
                }
                return Reformatter.Validate_DataTable(ds.Tables[0]);
             
        }
        
        public async Task<DataTable> GetInvestorEmailIDData(string UserID)
        {
                Dictionary<string, object> dictForgotPwd = new Dictionary<string, object>();               
                dictForgotPwd.Add("@DPIIDCLID", UserID);               
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetInvestorEmailID", dictForgotPwd);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
            
        }      
    
    }
}

 