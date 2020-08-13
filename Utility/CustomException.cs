using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public class CustomException:ApplicationException
    {
          public void NullValueException()
        {
           // MessageBox.Show("Please enter value");
        }
        public void MyDivideException()
        {
            //MessageBox.Show("Exception occured, divisor should not be zero");
        }
    }
}
