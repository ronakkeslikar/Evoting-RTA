using evoting.Domain.Models;
using evoting.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static evoting.Utility.FolderPaths;

namespace evoting.Utility
{
    public class ManageFileUpload
    {
        

        private async Task<DataTable> SaveFile(FJC_FileUpload fjc_FileUpload, ProcessType process,  string Token)
        {
            if (fjc_FileUpload.files.FileName.Length > 0)
            {              
                string getpath = string.Empty;
                DataTable dtFileDetails=new DataTable();
                switch (process)
                {
                    case ProcessType.Company_AgreementUpload:
                        getpath = FolderPaths.Company.AgreementUpload();
                     dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.RTA_AgreementUpload:
                        getpath = FolderPaths.RTA.AgreementUpload();
                     dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.EvotinAgency_AgreementUpload:
                        getpath = FolderPaths.EvotingAgency.AgreementUpload();
                     dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.Company_ROMUpload:
                        getpath = FolderPaths.Company.ROMUpload();
                    dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.Company_ROM_IntimationUpload:
                        getpath = FolderPaths.Company.ROMUpload();
                    dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.RTA_ROMUpload:
                        getpath = FolderPaths.RTA.ROMUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.Custodian_ROMUpload:
                        getpath = FolderPaths.Custodian.ROMUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                    break;
                    case ProcessType.Custodian_POA:
                        getpath = FolderPaths.Custodian.POA();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                    break;
                    case ProcessType.EvotingAgency_ROMUpload:
                        getpath = FolderPaths.EvotingAgency.ROMUpload();
                    dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.Company_Logo:
                        getpath = FolderPaths.Company.LogoUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.Comapny_ResolutionFile:
                        getpath = FolderPaths.Company.ResolutionFileUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.Company_Notice:
                        getpath = FolderPaths.Company.NoticeUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.RTA_Logo:
                        getpath = FolderPaths.RTA.LogoUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.RTA_ResolutionFile:
                        getpath = FolderPaths.RTA.ResolutionFileUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.RTA_Notice:
                        getpath = FolderPaths.RTA.NoticeUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.Scrutinizer_AgreementUpload:
                        getpath = FolderPaths.Scrutinizer.AgreementUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.VCFileUpload:
                        getpath = FolderPaths.EvotingAgency.VCFileUpload();
                        dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                }
                return dtFileDetails;
            }
            else
            {
                throw new CustomException.InvalidFileType();
            }
        }
        //////////////////////////////////////////Get User Login Detail using Token  ////////////////////////////////////////////////////

        public async Task<DataTable> SaveFile_FromToken(FJC_FileUpload fJC_FileUpload, string Token, UploadType uploadType)
        {
            DataTable dt = await GetUserDetailsByTokenID(Token);
            DataTable _return_Dt = new DataTable();
            switch (uploadType)
            {
                case UploadType.ROM: 
                    switch (dt.Rows[0]["type"])
                    {
                        case "Issuer Company":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Company_ROMUpload, Token);
                                    break;
                        case "RTA":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.RTA_ROMUpload, Token);
                                    break;
                       //Need to confirm for Custodian ROM upload             
                        case "Custodian":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Custodian_ROMUpload, Token);
                                break;

                    }
                break;
                case UploadType.ROM_Intimation: 
                    switch (dt.Rows[0]["type"])
                    {
                        case "Issuer Company":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Company_ROM_IntimationUpload, Token);
                                    break;
                        case "RTA":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.RTA_ROM_IntimationUpload, Token);
                                    break;
                    }
                break;
                case UploadType.Agreement: 
                    switch (dt.Rows[0]["type"])
                    {
                        case "Issuer Company":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Company_AgreementUpload, Token);
                                    break;
                        case "RTA":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.RTA_AgreementUpload, Token);
                        break;
                        case "Evoting Agency":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.EvotinAgency_AgreementUpload, Token);
                        break;
                         case "Scrutinizer":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Scrutinizer_AgreementUpload, Token);
                        break;

                    }
                break;
                case UploadType.POA: 
                    switch (dt.Rows[0]["type"])
                    {
                         case "Custodian":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Custodian_POA, Token);
                                break;
                    }
                break;
                case UploadType.Logo: 
                    switch (dt.Rows[0]["type"])
                    {
                         case "Issuer Company":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Company_Logo, Token);
                                break;
                        case "RTA":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.RTA_Logo, Token);
                                break;
                    }
                break;
                case UploadType.ResolutionFile: 
                    switch (dt.Rows[0]["type"])
                    {
                         case "Issuer Company":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Comapny_ResolutionFile, Token);
                                break;
                        case "RTA":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.RTA_ResolutionFile, Token);
                                break;
                    }
                break;
                case UploadType.Notice: 
                    switch (dt.Rows[0]["type"])
                    {
                         case "Issuer Company":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.Company_Notice, Token);
                                break;
                        case "RTA":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.RTA_Notice, Token);
                                break;
                    }
                break;
                  case UploadType.VC_File: 
                    switch (dt.Rows[0]["type"])
                    {
                         case "Evoting Agency":
                                _return_Dt = await SaveFile(fJC_FileUpload, ProcessType.VCFileUpload, Token);
                                break;
                    }
                break;
            }
            return _return_Dt;
        }
        public async Task<DataTable> GetUserDetailsByTokenID(string TokenID)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            dictUserDetail.Add("@Token", TokenID);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_GetLoginDetails", dictUserDetail);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        private async Task<DataTable> UploadToDatabase(string getpath, FJC_FileUpload fjc_FileUpload, string Token)
        {
            string filenamewithdatetime, SaveToFolder;
            //File name with time stamp and Event no 
            filenamewithdatetime = System.DateTime.Now.ToString("yyyyMMdd-hhmmssfff") + "-" + fjc_FileUpload.files.FileName;
            //Return Full file path to save to database
            SaveToFolder = FolderPaths.CreateSpecificFolder(getpath, filenamewithdatetime.ToString(), fjc_FileUpload);

            //-end-create folder directory- 

            //Saving file details to Database               
            if (SaveToFolder != null)
            {
                Dictionary<string, object> dictfileUpld = new Dictionary<string, object>();               
                dictfileUpld.Add("@File_Name", filenamewithdatetime);
                dictfileUpld.Add("@File_Path", SaveToFolder);
                dictfileUpld.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_spFileUpload", dictfileUpld);
                return Reformatter.Validate_DataTable(ds.Tables[0]);
            }
            else
            {
                throw new CustomException.InvalidPathReference();
            }
        }
    }
}
