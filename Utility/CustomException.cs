﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public class CustomException: Exception
    {       
        public class InvalidUserID : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid User ID";
                }
            }
        }
        public class InvalidRegNo : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Reg No.";
                }
            }
        }
        public class MultipleRequests : Exception
        {
            public override string Message
            {
                get
                {
                    return "Multiple login requests received. Please try logging in after 5 minutes";
                }
            }
        }


        public class InvalidEventId : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Event Id";
                }
            }
        } //Record  deleted already

        public class DeletedRecord : Exception
        {
            public override string Message
            {
                get
                {
                    return "Record Deleted already";
                }
            }
        }

        public class InvalidPassword : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Password";
                }
            }
        }
         public class InvalidUserIDPWD : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid User ID OR Password";
                }
            }
        }
        public class InvalidValue : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Value";
                }
            }
        }
        public class InvalidEmailID : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Email ID";
                }
            }
        }
         public class InvalidPANID : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid PAN ID";
                }
            }
        }
        public class InvalidAttempt : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Attempt Exceed";
                }
            }
        }
        public class InvalidTokenID : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Token ID. Your Session has Expired. Please log back in again";
                }
            }
        }

         public class InvalidDuplicatePassword : Exception
        {
            public override string Message
            {
                get
                {
                    return "New Password is same as Old Password";
                }
            }
        }
        public class MissingToken : Exception
        {
            public override string Message
            {
                get
                {
                    return "Token missing. Try logging back in";
                }
            } 
        }
        public class InvalidActivity : Exception
        {
            public override string Message
            {
                get
                {
                    return "Invalid Activity. User not authorised to perform this activity";
                }
            } 
        }
        public class InvalidPanPattern : Exception
        {
            public override string Message
            {
                get
                {
                    return "PAN card pattern invalid. Please provide valid PAN again.";
                }
            }
        }
        public class EventIDExists : Exception  
        {
            public override string Message
            {
                get
                {
                    return "Event Id already exists.";
                }
            }
        }
        public class EventIDNotExists : Exception  
        {
            public override string Message
            {
                get
                {
                    return "Event Id does not exists.";
                }
            }
        }
         public class CommonInvalidCode : Exception  
        {
            public override string Message
            {
                get
                {
                    return "Invalid Request code";
                }
            }
        }
        public class InvalidPathReference : Exception
        {
            public override string Message
            {
                get
                {
                    return "Path referenced are incorrect";
                }
            }
        } //InvalidFileType
        public class InvalidFileType : Exception
        {
            public override string Message
            {
                get
                {
                    return "File provided is of not proper format";
                }
            }
        }
        public class InvalidDoCID : Exception
        {
            public override string Message
            {
                get
                {
                    return "Document ID doesn't exists";
                }
            }
        }//Invalid Document ID

        public class InvalidFileRejected: Exception
        {
            public override string Message
            {
                get
                {
                    return "File rejected due to technical reason";
                }
            }
        }
        public class InvalidFileNotUploaded: Exception
        {
            public override string Message
            {
                get
                {
                    return "File was not uploaded,please try again";
                }
            }
        }  
        public class InvalidDpclNotExists: Exception 
        {
            public override string Message
            {
                get
                {
                    return "Invalid dpcl/pan does not exits";
                }
            }
        } 
        public class InvalidVote: Exception 
        {
            public override string Message
            {
                get
                {
                    return "Vote already submitted";
                }
            }
        }
        public class ReCaptchaError: Exception
        {
            public override string Message
            {
                get 
                {
                    return "ReCaptcha Error: Please refresh the page and solve again";
                }
            }
        }
    }
}
