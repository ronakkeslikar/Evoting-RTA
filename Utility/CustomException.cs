using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public   class CustomException: Exception
    {
          public void NullValueException()
        {
           // MessageBox.Show("Please enter value");
        }
        public void MyDivideException()
        {
            //MessageBox.Show("Exception occured, divisor should not be zero");
        }
        
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
