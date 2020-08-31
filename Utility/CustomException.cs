using System;
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

    }
}
