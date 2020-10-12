using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace evoting.Utility
{
    public class Token_Handling
    {
        public static string Get_Token_FromHeader(IHeaderDictionary _customHeads)
        {
            
                if (_customHeads.ContainsKey("Token"))
                {
                    return _customHeads["Token"];
                }
                else
                {
                    throw new CustomException.MissingToken();
                }
            
            
        }

        public static string Generate_token(string raw_token)
        {
            string key = "Ll8DpaC6YmokhqrTYzgVYLebzEAvm8iVFDg2YtQvMUI=";     
            var issuer = "https://evoting.bigshareonline.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
