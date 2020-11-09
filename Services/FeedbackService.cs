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
    public interface IFeedbackService
    {  
       Task<DataTable> Feedback_Details(FJC_Feedback fjc_feedback);        
    }

    public class FeedbackService : IFeedbackService
    {
        //db context here
        protected readonly AppDbContext _context;
        public FeedbackService(AppDbContext context)
        {
            _context = context;
        }   
         public async Task<DataTable> Feedback_Details(FJC_Feedback fjc_feedback)
        { 
            Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
            dictRegis.Add("@name", fjc_feedback.name);
            dictRegis.Add("@email", fjc_feedback.email);
            dictRegis.Add("@contact_no", fjc_feedback.contact_no);
            dictRegis.Add("@feedback", fjc_feedback.feedback);
            dictRegis.Add("@flag", 0);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_Feedback", dictRegis);                            
            return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }
        
        
    }
}
 