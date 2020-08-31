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
            else if(ex is CustomException.MissingToken)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidTokenID)
            {
                return StatusCode(401, new { status = false, message = ex.Message });
            }
            else if(ex is CustomException.InvalidEventId)
            {
                return Unauthorized(ex.Message);
            }
            else if(ex is CustomException.DeletedRecord)
            {
                return StatusCode(500, new { status = false, message = ex.Message });
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
            }
        }
    }
}
