using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations; 

namespace evoting.Domain.Models
{

public class FJC_FileUpload 
    {    
        public int DOC_NO { get; set;}
        public string File_Name { get; set;}
        public string File_Path { get; set;}
        public int ROWID { get; set;}
        public string Token_No { get; set;}
        public string CREATEDBY { get; set;}
        public string MODIFIEDBY { get; set;}
        public DateTime CREATED_DATE { get; set;}
        public DateTime MODIFIED_DATE { get; set;}
    }
        public class FJC_RTA_FileUpload 
    {    
        public int DOC_NO { get; set;}
        public string File_Name { get; set;}
        public string File_Path { get; set;}
        public int ROWID { get; set;}
        public string Token_No { get; set;}
        public string CREATEDBY { get; set;}
        public string MODIFIEDBY { get; set;}
        public DateTime CREATED_DATE { get; set;}
        public DateTime MODIFIED_DATE { get; set;}
    }

    public class FJC_Company_FileUpload 
    {    
        public int DOC_NO { get; set;}
        public string File_Name { get; set;}
        public string File_Path { get; set;}
        public int ROWID { get; set;}
        public string Token_No { get; set;}
        public string CREATEDBY { get; set;}
        public string MODIFIEDBY { get; set;}
        public DateTime CREATED_DATE { get; set;}
        public DateTime MODIFIED_DATE { get; set;}
    }

    public class FJC_Custodian_FileUpload 
    {    
        public int DOC_NO { get; set;}
        public string File_Name { get; set;}
        public string File_Path { get; set;}
        public int ROWID { get; set;}
        public string Token_No { get; set;}
        public string CREATEDBY { get; set;}
        public string MODIFIEDBY { get; set;}
        public DateTime CREATED_DATE { get; set;}
        public DateTime MODIFIED_DATE { get; set;}
    }

    public class FJC_Scrutinizer_FileUpload 
    {    
        public int DOC_NO { get; set;}
        public string File_Name { get; set;}
        public string File_Path { get; set;}
        public int ROWID { get; set;}
        public string Token_No { get; set;}
        public string CREATEDBY { get; set;}
        public string MODIFIEDBY { get; set;}
        public DateTime CREATED_DATE { get; set;}
        public DateTime MODIFIED_DATE { get; set;}
    }

    public class FJC_EvotingAgency_FileUpload 
    {    
        public int DOC_NO { get; set;}
        public string File_Name { get; set;}
        public string File_Path { get; set;}
        public int ROWID { get; set;}
        public string Token_No { get; set;}
        public string CREATEDBY { get; set;}
        public string MODIFIEDBY { get; set;}
        public DateTime CREATED_DATE { get; set;}
        public DateTime MODIFIED_DATE { get; set;}
    }
 
}