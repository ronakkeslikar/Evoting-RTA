using evoting.Domain.Models;
using evoting.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static evoting.Utility.FolderPaths;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace evoting.Utility
{
    public class SendMail
    {
        public void SendLetterMail(int id,string EmailerType,int aud_id,int row_id )

        { 
            //get smtp credential
            SmtpDetails smt =new SmtpDetails();
            var emailsmtp=smt.getsmtpDetail();
            MailMessage mail = new MailMessage();

            //Get Email Content from  GetEmailContent method
            DataTable dtemailcontent = new DataTable();
            dtemailcontent = GetEmailContent(id,EmailerType,aud_id,row_id);
                 
                mail.From = new MailAddress("donotreply@bigshareonline.com");

                mail.To.Add(dtemailcontent.Rows[0]["Recipients"].ToString());

                mail.Subject = dtemailcontent.Rows[0]["Subject"].ToString();

                mail.Body = dtemailcontent.Rows[0]["body"].ToString();  ;

                mail.IsBodyHtml = true;
 
                try

                {

                    smt.getsmtpDetail().Send(mail);

                    mail.Dispose();

                }

                catch (SmtpException ex)
                {
                    if(ex.StatusCode!=(System.Net.Mail.SmtpStatusCode)250)
                    {
                        GetEmailContent(Convert.ToInt32(dtemailcontent.Rows[0]["id"]),dtemailcontent.Rows[0]["emailer_type"].ToString(),0,0) ;   
                    }
                    
                }

            
        } 
        public void SendLetterMail(int id,string EmailerType,int event_id )

        { 
            //get smtp credential
            SmtpDetails smt =new SmtpDetails();
            var emailsmtp=smt.getsmtpDetail();
            MailMessage mail = new MailMessage();

            //Get Email Content from  GetEmailContent method
            DataTable dtemailcontent = new DataTable();
            dtemailcontent = GetEmailContent(id,EmailerType,event_id);

                for (int i = 0; i < dtemailcontent.Rows.Count;)
                {
                MailAddress to = new MailAddress(dtemailcontent.Rows[i]["Recipients"].ToString());
                MailAddress From = new MailAddress("donotreply@bigshareonline.com");
                MailMessage message = new MailMessage(From,to);

                message.Subject = dtemailcontent.Rows[i]["Subject"].ToString();

                message.Body = dtemailcontent.Rows[i]["body"].ToString();

                message.IsBodyHtml = true;
               
                try

                {
                    smt.getsmtpDetail().Send(message);
                    i++;
                }
                catch (SmtpException ex)
                {
                    if (ex.StatusCode != (System.Net.Mail.SmtpStatusCode)250)
                    {
                        GetEmailContent(Convert.ToInt32(dtemailcontent.Rows[0]["id"]), dtemailcontent.Rows[0]["emailer_type"].ToString(), 0);
                    }

                }
               
            }
             mail.Dispose();
        } 
        private DataTable  GetEmailContent(int id,string EmailerType,int aud_id,int row_id)   
        {
             Dictionary<string, object> dictUserDetail = new Dictionary<string, object>(); 
                dictUserDetail.Add("@id", id);              
                dictUserDetail.Add("@EmailerType", EmailerType);
                dictUserDetail.Add("@aud_id", aud_id);
                dictUserDetail.Add("@row_id", row_id); 

            DataSet ds= Persistence.Contexts.AppDBCalls.GetDataSet("Evote_SendEmail", dictUserDetail).Result;
          return Reformatter.Validate_DataTable(ds.Tables[0]);
        } 
        private DataTable  GetEmailContent(int id,string EmailerType,int event_id)   
        {
             Dictionary<string, object> dictUserDetail = new Dictionary<string, object>(); 
                dictUserDetail.Add("@id", id);              
                dictUserDetail.Add("@EmailerType", EmailerType);
                dictUserDetail.Add("@event_id", event_id);
                

            DataSet ds= Persistence.Contexts.AppDBCalls.GetDataSet("Evote_SendEmail", dictUserDetail).Result;
          return Reformatter.Validate_DataTable(ds.Tables[0]);
        } 

    }
    
}