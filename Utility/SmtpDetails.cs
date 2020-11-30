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
    public class SmtpDetails
    {
        public SmtpClient getsmtpDetail()
        {
            SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.rediffmailpro.com";

                smtp.Port = 587;

                smtp.EnableSsl = false;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Credentials = new NetworkCredential("donotreply@bigshareonline.com", "Abcde@54321");
                 
                return smtp;
        }
                
    }
}
    
 