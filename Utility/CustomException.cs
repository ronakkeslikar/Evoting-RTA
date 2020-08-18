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
                    return "Invalid User Id";
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
    }
}
