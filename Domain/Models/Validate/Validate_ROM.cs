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
using evoting.Services;

namespace evoting.Domain.Models.Validate
{
    public class Header
    {
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid BatchNo.")] //Only Numbers 
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string Batch_No { get; set; } //int   

        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Invalid ISIN.")] //Numbers and Alphabet
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StringValidate))]
        public string ISIN { get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        // [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid EventNo.")] //Only numbers
        public string Event_No { get; set; } //int

    }

    public class Detail
    {
        public string Sr_no { get; set; }  //^[a-zA-Z0-9 !@#&\(\)]*$  //int
        public string Unkn1 { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid DPCL")]
        // [StringLength(16, MinimumLength = 16, ErrorMessage = "DPCL must be 16 characters")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StringValidate))]
        public string DPCL { get; set; }

        // [RegularExpression(@"[A-Z]{5}\d{4}[A-Z]{1}", ErrorMessage = "* Invalid PAN")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.PanValidate))]
        public string PAN { get; set; }

        // [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid AccNo.")] //Only Numbers
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string AccNo { get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StringValidate))]
        public string Unkn2 { get; set; }

        // [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Shares.")] //Only Numbers
        // [Required(ErrorMessage = "Shares is required.")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmptyStringValidate))]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.NumberValidate))]
        public string shares { get; set; }  //int

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid NAME")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string Name { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid JT1")] 
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string JT1 { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid JT2")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string JT2 { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid ADD1")] 
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string ADD1 { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid ADD2")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string ADD2 { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid ADD3")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string ADD3 { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid City")] 
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string City { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid State")] 
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string state { get; set; }

        // [RegularExpression(@"^[a-zA-Z0-9 !@#&\(\)]*$", ErrorMessage = "* Invalid Country")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string Country { get; set; }

        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.StirngAndSpecialCharValidate))]
        public string Pin { get; set; }
        public string DOB { get; set; } //datetime
        public string Unkn3 { get; set; }

        // [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.MobileNoValidate))]
        public string Mobile { get; set; }

        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        [CustomValidation(typeof(CommonValidation), nameof(CommonValidation.EmailValidate))]
        public string Email { get; set; }
        public string Unkn4 { get; set; }
    }

    public class Validate_ROM
    {
        public bool Validate_File(string _fileName,string Token,int _event_id, string upload_id,string forIntimation="")
        {
            // List<string>ErrorFile = new List<string>();
            int LineNum = 1;
            List<CommonValidation.ErrorFile_list> _ErrorFile = new List<CommonValidation.ErrorFile_list>();

            foreach (string Line in File.ReadAllLines(_fileName))
            {
                string record_type = Line.Substring(0, 2);
                string NewLine = Line.Remove(0, 3);
                try
                {
                    switch (record_type)
                    {
                        case "00":
                            string[] _obj_array = NewLine.Split('~');
                            Header _objHeader = new Header()
                            {
                                Batch_No = _obj_array[0],
                                ISIN = _obj_array[1],
                                Event_No = _obj_array[2]
                            };

                            // ErrorFile.AddRange(CommonValidation.GetHeaderErrors(_obj));
                            if(_event_id!=Convert.ToInt32(_obj_array[2]))
                            {
                                    throw new CustomException.InvalidFileRejected();
                                    
                            }
                            else
                            {
                                CommonValidation.ErrorFile_list new_objHeader = new CommonValidation.ErrorFile_list();
                                new_objHeader.LineNum = LineNum;
                                new_objHeader.ErrorResponse = CommonValidation.GetHeaderErrors(_objHeader);
                                if (new_objHeader.ErrorResponse.Count > 0)
                                {
                                _ErrorFile.Add(new_objHeader);
                                }
                                break;
                            }
                           

                        case "01":
                            // var checkDetail = NewLine.Split('~').Cast<Detail>();
                            string[] _obj_DetailArray = NewLine.Split('~');
                            Detail _ObjDetail = new Detail()
                            {
                                Sr_no = _obj_DetailArray[0],
                                Unkn1 = _obj_DetailArray[1],
                                DPCL = _obj_DetailArray[2],
                                PAN = _obj_DetailArray[3],
                                AccNo = _obj_DetailArray[4],
                                Unkn2 = _obj_DetailArray[5],
                                shares = _obj_DetailArray[6],
                                Name = _obj_DetailArray[7],
                                JT1 = _obj_DetailArray[8],
                                JT2 = _obj_DetailArray[9],
                                ADD1 = _obj_DetailArray[10],
                                ADD2 = _obj_DetailArray[11],
                                ADD3 = _obj_DetailArray[12],
                                City = _obj_DetailArray[13],
                                state = _obj_DetailArray[14],
                                Country = _obj_DetailArray[15],
                                Pin = _obj_DetailArray[16],
                                DOB = _obj_DetailArray[17],
                                Unkn3 = _obj_DetailArray[18],
                                Mobile = _obj_DetailArray[19],
                                Email = _obj_DetailArray[20],
                                Unkn4 = _obj_DetailArray[21],
                            };

                            CommonValidation.ErrorFile_list new_objDetail = new CommonValidation.ErrorFile_list();
                            new_objDetail.LineNum = LineNum;
                            new_objDetail.ErrorResponse = CommonValidation.GetDetailErrors(_ObjDetail);
                            if (new_objDetail.ErrorResponse.Count > 0)
                            {
                                _ErrorFile.Add(new_objDetail);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    CommonValidation.ErrorFile_list new_objDetail = new CommonValidation.ErrorFile_list();
                    new_objDetail.LineNum = LineNum;
                    new_objDetail.ErrorResponse = new List<string>() { ex.Message };
                }
                finally
                {
                    LineNum++;
                }
            }

            if (_ErrorFile.Count > 0)
            {
                //WriteErrorFile(_ErrorFile, Token);
                //return false;
                int getreturnint = 0;
                getreturnint = WriteErrorFile(_ErrorFile, Token, _fileName, _event_id, upload_id, forIntimation);
                if (getreturnint == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        public int WriteErrorFile(List<CommonValidation.ErrorFile_list> _error,string Token, string _fileName, int _event_id, string upload_id,string forIntimation)
        {
            //string default_path = @"D:\Evoting\ErrorFile\Error.txt"; 
            DataTable dtUserType = new DataTable();
            dtUserType = (DataTable)(new ManageFileUpload()).GetUserDetailsByTokenID(Token).Result;
            string default_path = string.Empty;
            string error_log_file_name = System.DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "-Error.txt";

            switch (dtUserType.Rows[0]["type"])
            {
                case "Issuer Company":
                    default_path = FolderPaths.Company.ROMFileError() + "\\" + error_log_file_name;

                    break;
                case "RTA":
                    default_path = FolderPaths.RTA.ROMFileError() + "\\" + error_log_file_name;
                    break;            

            }

            //string default_path = FolderPaths.RTA.ROMFileError() + "\\" + System.DateTime.Now.ToString("yyyyMMdd-hhmmssfff") + "-Error.txt";


            //-Start-Error file created
            if (!File.Exists(default_path))
            {
                FileStream fs = File.Create(default_path);
                fs.Flush();
                fs.Close();
            }
            //-End-Error file created
            DataTable dt3 = new DataTable();
            if(forIntimation=="Yes")
            {
                dt3 = UpdateIntimataionROM(_event_id, upload_id, error_log_file_name, default_path, Token, 0);
            }
            else
            {
                dt3 = UpdateRegisterROM(_event_id, upload_id, error_log_file_name, default_path, Token, 0);
            }
            //dt3 = UpdateRegisterROM(_event_id, upload_id, error_log_file_name, default_path, Token, 0);//int Event_No,int DocID, string Token,int flag

            StringBuilder bs = new StringBuilder();
            foreach (var item in _error)
            {
                if (item.ErrorResponse.Count > 0)
                {
                    bs.Append("Error on Line No." + item.LineNum);
                    foreach (var item1 in item.ErrorResponse)
                    {
                        bs.Append(" Error description : " + item1);
                        if (item.ErrorResponse.Count > 1)
                        {
                            bs.Append(",");
                        }
                    }
                    bs.AppendLine();
                }
            }
            File.WriteAllText(default_path, bs.ToString());
            if (dt3.Rows[0]["status_id"].ToString() == "4")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        private DataTable UpdateRegisterROM(int Event_No, string upload_id, string error_log_file_name, string default_path, string Token, int flag)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            dictUserDetail.Add("@upload_id", Convert.ToInt32(upload_id));
            dictUserDetail.Add("@error_log_file_name", error_log_file_name);
            dictUserDetail.Add("@default_path", default_path);
            dictUserDetail.Add("@event_id", Event_No);
            dictUserDetail.Add("@token", Token);
            dictUserDetail.Add("@flag", flag);

            DataSet ds = Persistence.Contexts.AppDBCalls.GetDataSet("Evote_Sp_ROM_Register", dictUserDetail).Result;
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        private DataTable UpdateIntimataionROM(int Event_No, string upload_id, string error_log_file_name, string default_path, string Token, int flag)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            dictUserDetail.Add("@uploadid", Convert.ToInt32(upload_id));
            dictUserDetail.Add("@error_log_file_name", error_log_file_name);
            dictUserDetail.Add("@default_path", default_path);
            dictUserDetail.Add("@event_id", Event_No);
            dictUserDetail.Add("@token", Token);
            dictUserDetail.Add("@flag", flag);

            return Persistence.Contexts.AppDBCalls.GetDataSet("Evote_Sp_ROM_Intimation_Register", dictUserDetail).Result.Tables[0];
            //return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        private void createAndappendDateFolder(string v1, string v2, object checkPath)
        {
            throw new NotImplementedException();
        }
    }

    public class CommonValidation
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
            return (_str != "" ? ValidationResult.Success : new ValidationResult("Field Required"));
        }
        public static List<string> GetHeaderErrors(Header _obj)
        {
            List<string> ErrorResult = new List<string>();
            ValidationContext _context = new ValidationContext(_obj);
            List<ValidationResult> _results = new List<ValidationResult>();
            bool Valid = Validator.TryValidateObject(_obj, _context, _results, true);
            if (!Valid)
            {
                foreach (var item in _results)
                {
                    ErrorResult.Add(item.ErrorMessage);
                }
            }
            return ErrorResult;

        }

        public static List<string> GetDetailErrors(Detail _obj)
        {
            List<string> ErrorResult = new List<string>();
            ValidationContext _context = new ValidationContext(_obj);
            List<ValidationResult> _results = new List<ValidationResult>();
            bool Valid = Validator.TryValidateObject(_obj, _context, _results, true);
            if (!Valid)
            {
                foreach (var item in _results)
                {
                    ErrorResult.Add(item.ErrorMessage);
                }
            }
            return ErrorResult;

        }

        public class ErrorFile_list
        {
            public int LineNum { get; set; }
            public List<string> ErrorResponse { get; set; }
        }
    }
}
