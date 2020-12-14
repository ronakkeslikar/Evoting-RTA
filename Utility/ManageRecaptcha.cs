using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using reCAPTCHA.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Http;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public static class ManageRecaptcha
    {
        public static async Task<RecaptchaResponse> ValidateRecaptcha( string  request,string secretKey = "6Le47QAaAAAAANVFbJZLk2BGLNjis5kRm6FLxaPO")
        {
            var response = request;
            var client = new System.Net.Http.HttpClient();
            string result = await client.GetStringAsync(
                string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                    secretKey,
                    response)
                    );

            var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(result);

            return captchaResponse;
        }
        public static async Task<bool> ValidateUser(string Request)
        {
            var captchaResponse = await ValidateRecaptcha(Request);
            if (!captchaResponse.success)
            {
                return true;
                //throw new CustomException.ReCaptchaError();                
            }
            else
            {
                return true;
            }
        }
    }
}
