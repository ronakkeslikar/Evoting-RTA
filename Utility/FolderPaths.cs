using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using evoting.Domain.Models;

namespace evoting.Utility
{
    public static class FolderPaths
    {
        public static string MainFolderPath = @"C:\evoting";//@"C:\Sites\Evoting_API"; //@"C:\evoting"; 

        public static class RTA
        {
            public static string SubCategoryPath = "RTA";

            public static string ROMUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "Agreement");
            }            
            
            public static string LogoUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Logo");
            }
             public static string ResolutionFileUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Notice");
            }

        } 

        public static class Company
        {
            public static string SubCategoryPath = "Company";

            public static string ROMUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "Agreement");
            }            
             public static string LogoUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Logo");
            }
             public static string ResolutionFileUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Notice");
            }

        } 

        public static class Custodian
        {
            public static string SubCategoryPath = "Custodian";

            public static string ROMUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "Agreement");
            }            
             public static string LogoUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Logo");
            }

            public static string ResolutionFileUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Notice");
            }

        } 

        public static class Scrutinizer
        {
            public static string SubCategoryPath = "Scrutinizer";

            public static string ROMUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "Agreement");
            }            
             public static string LogoUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Logo");
            }
            public static string ResolutionFileUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Notice");
            }

        } 

        public static class EvotingAgency
        {
            public static string SubCategoryPath = "EvotingAgency";

            public static string ROMUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return Path.Combine(MainFolderPath, SubCategoryPath, "Agreement");
            }            
             public static string LogoUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Logo");
            }
             public static string ResolutionFileUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return Path.Combine(MainFolderPath, SubCategoryPath, "Notice");
            }

        } 
 
        public static string CreateSpecificFolder(string _checkPath,string _filenamewithdatetime,FJC_FileUpload fileUpload)
        {
            //folder exists - if not then create
            //same file exists
            
                 var name = fileUpload.files;
             if (name.Length > 0)
             {
                
                if(_checkPath !=null)
                {
                    string getFullFilepath="";
                      _checkPath =  Path.Combine(_checkPath,System.DateTime.Now.ToString("yyyy-MM-dd")); 
                    if (!Directory.Exists(_checkPath))  
                        {                         
                            Directory.CreateDirectory(_checkPath);
                        }                                        
                        using (var fileStream = new FileStream(Path.Combine(_checkPath,_filenamewithdatetime), FileMode.Create,FileAccess.Write)) 
                        {                            
                            fileUpload.files.CopyTo(fileStream);
                            getFullFilepath=fileStream.Name.ToString();
                        }
                         return getFullFilepath;
                }
                else
                {
                return null;  
                }  
             } 
             else
             {
                return null;    
             }       
             
        }
          

       
        public enum ProcessType
        {
            Company_AgreementUpload,
            Company_ROMUpload,
            RTA_AgreementUpload,
            RTA_ROMUpload,
            EvotinAgency_AgreementUpload,
            EvotingAgency_ROMUpload
        }
        public enum AudienceType
        {
            Company,
            RTA,
            EvotingAgency
        }
        public enum UploadType
        {
            ROM,
            Agreement
        }
    }
}