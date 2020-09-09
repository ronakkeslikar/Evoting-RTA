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
                return createAndappendDateFolder( SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "Upload_Agreement");
            }  
             public static string AgreementDownload()
            {
                return createAndappendDateFolder( SubCategoryPath, "Download_Agreement");
            }           
            
            public static string LogoUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Logo");
            }
             public static string ResolutionFileUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Notice");
            }

        } 

        public static class Company
        {
            public static string SubCategoryPath = "Company";

            public static string ROMUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {               
                return createAndappendDateFolder( SubCategoryPath, "Upload_Agreement");
            }  
             public static string AgreementDownload()
            {
                return createAndappendDateFolder( SubCategoryPath, "Download_Agreement");
            }             
             public static string LogoUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Logo");
            }
             public static string ResolutionFileUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Notice");
            }

        } 

        public static class Custodian
        {
            public static string SubCategoryPath = "Custodian";

            public static string ROMUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "Agreement");
            }            
             public static string LogoUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Logo");
            }

            public static string ResolutionFileUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Notice");
            }

        } 

        public static class Scrutinizer
        {
            public static string SubCategoryPath = "Scrutinizer";

            public static string ROMUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "Agreement");
            }            
             public static string LogoUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Logo");
            }
            public static string ResolutionFileUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Notice");
            }

        } 

        public static class EvotingAgency
        {
            public static string SubCategoryPath = "EvotingAgency";

            public static string ROMUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "ROM");
            }
            public static string AgreementUpload()
            {
                return createAndappendDateFolder( SubCategoryPath, "Agreement");
            }            
             public static string LogoUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Logo");
            }
             public static string ResolutionFileUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "ResolutionFile");
            }
              public static string NoticeUpload()
            {
                 return createAndappendDateFolder( SubCategoryPath, "Notice");
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
                   createAndappendDateFolder("","", _checkPath);
                  
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

        private static string createAndappendDateFolder( string SubCategoryPath, string process_type, string pathformatted = "")
        {
            //MainFolderPath, SubCategoryPath, "Download_Agreement"
           string _checkPath = "";
           if(pathformatted.Trim()!="")
           {
               _checkPath = pathformatted;
           }
           else
           {
               _checkPath =   Path.Combine(MainFolderPath, SubCategoryPath, process_type ,System.DateTime.Now.ToString("yyyy-MM-dd")); 
           }           
                    if (!Directory.Exists(_checkPath))  
                        {                         
                            Directory.CreateDirectory(_checkPath);
                        } 
            return _checkPath;
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