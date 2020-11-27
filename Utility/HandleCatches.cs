using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public class HandleCatches : ControllerBase
    {
        public IActionResult ManageExceptions(Exception ex)
        {
            if(ex is  CustomException.InvalidUserID)
            { 
                return Unauthorized(new { status = false, message = ex.Message });
            }
             else if(ex is CustomException.InvalidPANID)
            {              
                return StatusCode(400, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidEmailID)
            {
                return Unauthorized(new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.MissingToken)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidTokenID)
            {
                return StatusCode(401, new { status = false, message = ex.Message });
            }
            // else if(ex is CustomException.InvalidEventId)
            // {
            //     return Unauthorized(ex.Message);
            // }
             else if(ex is CustomException.InvalidEventId)
            {
                return StatusCode(401, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.EventIDExists)
            {
                return StatusCode(400, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidDoCID)
            {
                return StatusCode(400, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.CommonInvalidCode)
            {
                return StatusCode(400, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidVote)
            {
                return StatusCode(400, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.DeletedRecord)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidPanPattern)
            {
                return StatusCode(400, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidRegNo)
            {
                return StatusCode(400, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidPathReference)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidFileNotUploaded)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidFileRejected)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
            else if (ex is CustomException.InvalidFileType)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
             else if (ex is Microsoft.Data.SqlClient.SqlException)
            {
                return StatusCode(500, new { status = false, message = ex.Message });//Later we will change to sql server error
            }
            else if(ex is CustomException.InvalidDpclNotExists)
            {
                return StatusCode(416, new { status = false, message = ex.Message });
            }
            else
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
        }
        public void Raise_DB_Exceptions(string _error)
        {
            switch(_error)
            {
                case "Multiple login requests":                    
                        throw new CustomException.MultipleRequests();
                case "Invalid User ID OR Password":
                        throw new CustomException.InvalidUserID();
                case "Invalid Attempt Exceed":
                        throw new CustomException.InvalidAttempt();
                case "New Password is same as Old Password":
                        throw new CustomException.InvalidDuplicatePassword();
                case "Invalid Email ID":
                        throw new CustomException.InvalidEmailID();
                case "Invalid PAN ID":
                    throw new CustomException.InvalidPANID();
                case "Invalid User ID":
                    throw new CustomException.InvalidUserID();
                case "Invalid Event":
                    throw new CustomException.InvalidEventId();
                case "INVALID TOKEN":
                    throw new CustomException.InvalidTokenID();
                case "Invalid Activity":
                    throw new CustomException.InvalidActivity();
                case "Record  deleted already":
                    throw new CustomException.DeletedRecord();
                case "Event Id already exists":
                throw new CustomException.EventIDExists();
                case "Event Id does not exists":
                throw new CustomException.EventIDNotExists();
                case "Invalid Request code":
                throw new CustomException.CommonInvalidCode();
                case "Document ID doesn't exists":
                throw new CustomException.InvalidDoCID();
                case "File was not uploaded,please try again":
                throw new CustomException.InvalidFileRejected();
                case "File rejected due technical reason":
                throw new CustomException.InvalidFileNotUploaded();
                case "Invalid dpcl":
                throw new CustomException.InvalidDpclNotExists();
                case "Invalid Vote":
                throw new CustomException.InvalidVote();
                case "Invalid Reg. No":
                throw new CustomException.InvalidRegNo();
                
            }
        }
    }
}
