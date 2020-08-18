using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace evoting.Domain.Models.Validate
{
    public static class Validate_Login
    {

        public static bool CheckString(string inputString)
        {
            if(inputString!=null)
            {
                var regexItem = new Regex("^[a-zA-Z0-9]*$"); //returns false if contains special character
                return regexItem.IsMatch(inputString);
            }
            else
            {
                return false;
            }
            
        }
        public static bool CheckOnlyAlphabetString(string inputString)
        {
            var regexItem = new Regex("^[a-zA-Z]*$"); //returns false if contains special character
            return regexItem.IsMatch(inputString); 
        }

         public static bool IsValidEmailId(string inputString)
        {
            var regexItem = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return regexItem.IsMatch(inputString);
        }


        public static int checkNumber()
        {

            return 0;
        }
        public static char checkChar()
        {

            return 's';
        }
        public static double checkDecimal()
        {

            return 0.0;
        }
    }
}
