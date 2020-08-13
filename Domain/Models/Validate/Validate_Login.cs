using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace evoting.Domain.Models.Validate
{
    public class Validate_Login
    {

        public bool CheckString(string inputString)
        {
            var regexItem = new Regex("^[a-zA-Z0-9]*$"); //returns false if contains special character
            return regexItem.IsMatch(inputString);
        }
        public bool CheckOnlyAlphabetString(string inputString)
        {
            var regexItem = new Regex("^[a-zA-Z]*$"); //returns false if contains special character
            return regexItem.IsMatch(inputString); 
        }

         public bool IsValidEmailId(string inputString)
        {
            var regexItem = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return regexItem.IsMatch(inputString);
        }


        public int checkNumber()
        {

            return 0;
        }
        public char checkChar()
        {

            return 's';
        }
        public double checkDecimal()
        {

            return 0.0;
        }
    }
}
