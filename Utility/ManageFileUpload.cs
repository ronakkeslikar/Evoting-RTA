﻿using evoting.Domain.Models;
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
                    case ProcessType.RTA_ROMUpload:
                        getpath = FolderPaths.RTA.ROMUpload();
                     dtFileDetails= await UploadToDatabase(getpath, fjc_FileUpload, Token);
                        break;
                    case ProcessType.EvotingAgency_ROMUpload:
                        getpath = FolderPaths.EvotingAgency.ROMUpload();
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

                    }
                break;
            }
            return _return_Dt;
        }
        private async Task<DataTable> GetUserDetailsByTokenID(string TokenID)
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
