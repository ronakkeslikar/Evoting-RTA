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
    }
}
