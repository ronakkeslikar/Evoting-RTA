using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace evoting.Utility
{
    public static class FolderPaths
    {
        public static string MainFolderPath = @"C:\evoting";

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
            
        } 

        public static string CreateSpecificFolder(string _checkPath)
        {
            //folder exists - if not then create
            //same file exists
            
           
            return "";
        }
    }
}