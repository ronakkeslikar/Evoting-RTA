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
    }
}
