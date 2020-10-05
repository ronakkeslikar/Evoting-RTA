using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using System.ComponentModel.DataAnnotations;
using evoting.Domain.Models;
using evoting.Utility;
using System.Data;
using evoting.Persistence.Contexts;

namespace evoting.Domain.Models.Validate
{
    public class ROM_Header
    {         
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid BatchNo.")] //Only Numbers 
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string Batch_No { get; set; } //int   
  
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]        
        // [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid EventNo.")] //Only numbers
        public string Event_No { get; set; } //int

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        public string Count { get; set; } 

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        public string Shares { get; set; }                 
    }

    public class ROM_Detail
    {
        public string Sr_no { get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StringValidate))]
        public string DPCL { get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string ResolutionId {get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string InFavourShares {get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string NotInFavourShares {get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string AbstainShares {get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string TotalShares {get; set; }
    }

    public class ROM_Transactioin
    {
        public string Sr_no { get; set; }

        public string ResolutionId { get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string TransResolutionId {get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]  
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string TransInFavShares {get; set; }

         [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]  
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string TransNotInFavShares {get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]  
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string TransAbstainShares {get; set; }

    }

    public class ValidateCustodian_ROM
    {
        public bool Validate_File(string _fileName)
        {
            // List<string>ErrorFile = new List<string>();
            int LineNum = 1;
            List<CommonValidation.ErrorFile_list> _ErrorFile = new List<CommonValidation.ErrorFile_list>();
            // Object _obj_Header;
            
            foreach(string Line in File.ReadAllLines(_fileName))
            {
                string record_type = Line.Substring(0,1);
                string NewLine = Line.Remove(0, 2);
                try{
                    switch(record_type)
                    {
                        case "0" :
                        string[] _obj_array = NewLine.Split('~');
                            ROM_Header _objHeader = new ROM_Header()
                            {
                                Batch_No = _obj_array[0],
                                Event_No = _obj_array[1],                                
                                Count = _obj_array[2],
                                Shares = _obj_array[3]
                                
                            };
                        // _obj_Header = _objHeader;
                        // ErrorFile.AddRange(CommonValidation.GetHeaderErrors(_obj));
                        CommonValidation.ErrorFile_list new_objHeader = new CommonValidation.ErrorFile_list();
                        new_objHeader.LineNum = LineNum;
                        new_objHeader.ErrorResponse = CommonCustodianValidation.GetCustodianHeaderErrors(_objHeader);                        
                        if (new_objHeader.ErrorResponse.Count>0)
                        {
                            _ErrorFile.Add(new_objHeader);
                        }
                        break;

                        case "1" : 
                            // var checkDetail = NewLine.Split('~').Cast<Detail>();
                            string[] _obj_DetailArray = NewLine.Split('~');
                            ROM_Detail _ObjDetail = new ROM_Detail()
                            {
                                Sr_no = _obj_DetailArray[0],
                                DPCL = _obj_DetailArray[1],
                                ResolutionId = _obj_DetailArray[2],
                                InFavourShares = _obj_DetailArray[3],
                                NotInFavourShares = _obj_DetailArray[4],
                                AbstainShares = _obj_DetailArray[5],
                                TotalShares = _obj_DetailArray[6],
                              
                            };

                            CommonValidation.ErrorFile_list new_objDetail = new CommonValidation.ErrorFile_list();
                            new_objDetail.LineNum = LineNum;
                            new_objDetail.ErrorResponse = CommonCustodianValidation.GetCustodianDetailErrors(_ObjDetail);                            
                            if (new_objDetail.ErrorResponse.Count>0)
                            {
                                _ErrorFile.Add(new_objDetail);
                            }                            
                        break;

                        case "2" : 
                            // var checkDetail = NewLine.Split('~').Cast<Detail>();
                            string[] _obj_TransactionArray = NewLine.Split('~');
                            ROM_Transactioin _ObjTransaction = new ROM_Transactioin()
                            {
                                Sr_no = _obj_TransactionArray[0],                                
                                TransResolutionId = _obj_TransactionArray[1],
                                TransInFavShares = _obj_TransactionArray[2],
                                TransNotInFavShares = _obj_TransactionArray[3],
                                TransAbstainShares = _obj_TransactionArray[4],                                
                              
                            };

                            CommonValidation.ErrorFile_list new_objTransaction = new CommonValidation.ErrorFile_list();
                            new_objTransaction.LineNum = LineNum;
                            new_objTransaction.ErrorResponse = CommonCustodianValidation.GetCustodianTransactionErrors(_ObjTransaction);
                            if (new_objTransaction.ErrorResponse.Count>0)
                            {
                                _ErrorFile.Add(new_objTransaction);
                            }                            
                        break;
                    }
                }
                catch(Exception ex)
                {
                    CommonValidation.ErrorFile_list new_objDetail = new CommonValidation.ErrorFile_list();
                    new_objDetail.LineNum = LineNum;
                    new_objDetail.ErrorResponse = new List<string>(){ex.Message}; 
                }
                finally
                {                    
                    LineNum++;
                }
            }

            if (_ErrorFile.Count > 0)
            {
                WriteErrorFile(_ErrorFile);
                return false;
            }
            else
            {
                return true;
            }
               
        }  

        public void WriteErrorFile(List<CommonValidation.ErrorFile_list> _error)
        {
            string default_path = @"D:\Evoting\ErrorFile\CustodianError.txt"; 
            StringBuilder bs = new StringBuilder();
            foreach(var item in _error)
            {
                if (item.ErrorResponse.Count > 0)
                {
                    bs.Append("Error on Line No."+ item.LineNum);
                    foreach(var item1 in item.ErrorResponse)
                    {
                        bs.Append(" Error description : "+item1);
                        if (item.ErrorResponse.Count>1)
                        {
                            bs.Append(",");
                        }
                    }
                bs.AppendLine();
                }                
            }
            File.WriteAllText(default_path, bs.ToString());
        }  
    }

    public class CommonCustodianValidation
    {     
        public static ValidationResult StringValidate(string _str)
        {
            return Regex.IsMatch(_str, @"^[a-zA-Z0-9]*$") == true ? ValidationResult.Success : new ValidationResult("String not in correct format");
        }
        public static ValidationResult StirngAndSpecialCharValidate(string _str)
        {
            return Regex.IsMatch(_str, @"^[a-zA-Z0-9!@#$&()-_+{}|\\`.,;:'\ ""]*$") == true ? ValidationResult.Success : new ValidationResult("String not in correct format");
        }
        public static ValidationResult NumberValidate(string _str)
        {
            return Regex.IsMatch(_str, @"^[0-9X]+$|^$") == true ? ValidationResult.Success : new ValidationResult("Numeric not in correct format");
        }
        public static ValidationResult PanValidate(string _str)
        {
            return Regex.IsMatch(_str, @"[A-Z]{5}\d{4}[A-Z]{1}") == true ? ValidationResult.Success : new ValidationResult("PAN not in correct format");
        }
        public static ValidationResult MobileNoValidate(string _str)
        {
            return Regex.IsMatch(_str, @"^([0-9]{10})$|^(?![\s\S])") == true ? ValidationResult.Success : new ValidationResult("Mobile No. not in correct format");
        }
        public static ValidationResult EmailValidate(string _str)
        {
            return Regex.IsMatch(_str, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$|^(?![\s\S])") == true ? ValidationResult.Success : new ValidationResult("Email not in correct format");
        }        
        public static ValidationResult EmptyStringValidate(string _str)
        {
            return (_str != "" ? ValidationResult.Success : new ValidationResult("Field Required, Please Enter 0 if field empty"));
        }
        public static List<string> GetCustodianHeaderErrors(ROM_Header _obj)
        {
            List<string> ErrorResult = new List<string>();
            ValidationContext _context = new ValidationContext(_obj);
            List<ValidationResult> _results = new List<ValidationResult>();
            bool Valid = Validator.TryValidateObject(_obj, _context, _results, true);
            if(!Valid)
            {
                foreach(var item in _results)
                {
                        ErrorResult.Add(item.ErrorMessage);
                }
            }
            return ErrorResult;

        }

        public static List<string> GetCustodianDetailErrors(ROM_Detail _obj)
        {
            List<string> ErrorResult = new List<string>();
            ValidationContext _context = new ValidationContext(_obj);
            List<ValidationResult> _results = new List<ValidationResult>();
            bool Valid = Validator.TryValidateObject(_obj, _context, _results, true);
            if(!Valid)
            {
                foreach(var item in _results)
                {
                        ErrorResult.Add(item.ErrorMessage);
                }
            }
            return ErrorResult;

        }

        public static List<string> GetCustodianTransactionErrors(ROM_Transactioin _obj)
        {
            List<string> ErrorResult = new List<string>();
            ValidationContext _context = new ValidationContext(_obj);
            List<ValidationResult> _results = new List<ValidationResult>();
            bool Valid = Validator.TryValidateObject(_obj, _context, _results, true);
            if(!Valid)
            {
                foreach(var item in _results)
                {
                        ErrorResult.Add(item.ErrorMessage);
                }
            }
            return ErrorResult;

        }        

        public class ErrorFile_list
        {
            public int LineNum {get;set;}
            public List<string> ErrorResponse {get;set;}
        } 
    }

    public class Check_CountandShares
    {
        public async Task<DataTable> Check(string File_Path)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@FilePath", File_Path);            
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("SP_CHK_COUNT_SHARES", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }        
    }    
}
