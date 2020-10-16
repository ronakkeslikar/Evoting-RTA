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
    public interface IVideoConfService
    {  
       Task<DataTable> VideoConf_Update(FJC_VideoConf FJC_VideoConf, string Token);
        Task<DataTable> Get_VideoConf(int event_id, string Token);

    }

    public class VideoConfService : IVideoConfService
    {
        //db context here
        protected readonly AppDbContext _context;
        public VideoConfService(AppDbContext context)
        {
            _context = context;
        }  

         /////////////////////////Get Serach using POST method/////////////////////
         public async Task<DataTable> VideoConf_Update(FJC_VideoConf FJC_VideoConf, string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();               
               
                dictRegis.Add("@event_id", FJC_VideoConf.event_id);
                dictRegis.Add("@vc_url", FJC_VideoConf.vc_url);
                dictRegis.Add("@investor_url", FJC_VideoConf.investor_url);
                dictRegis.Add("@vc_title", FJC_VideoConf.vc_title);
                dictRegis.Add("@vc_datetime", FJC_VideoConf.vc_datetime);
                dictRegis.Add("@vc_file", FJC_VideoConf.vc_file);
                dictRegis.Add("@vc_handler", FJC_VideoConf.vc_handler);
                dictRegis.Add("@flag", 1);
                dictRegis.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_VideoConf", dictRegis);                            
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }

        public async Task<DataTable> Get_VideoConf(int event_id, string Token)
        {
            Dictionary<string, object> dictRegis = new Dictionary<string, object>();

            dictRegis.Add("@event_id", event_id);
            dictRegis.Add("@flag", 2);
            dictRegis.Add("@token", Token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_VideoConf", dictRegis);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }


    }
}
 