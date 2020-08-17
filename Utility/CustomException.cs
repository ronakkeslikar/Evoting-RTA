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
            //Overriding the Message property
            public override string Message
            {
                get
                {
                    return "Invalid User Id/Password";
                }
            }
        }
    }
}
