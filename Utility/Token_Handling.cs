using evoting.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Utility
{
    public class Token_Handling 
    {
        public const string key = "Ll8DpaC6YmokhqrTYzgVYLebzEAvm8iVFDg2YtQvMUI=";
        
        public static string Get_Token_FromHeader(IHeaderDictionary _customHeads, ClaimsIdentity identity)
        {
            
                if (_customHeads.ContainsKey("Authorization"))
                {
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string Token = claims.Where(p => p.Type == "ChecksumID").FirstOrDefault()?.Value;
                        return Token;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new CustomException.MissingToken();
                }        
            
        }
        
        public static string Generate_token(BSC_LoginResponse loginResponse)
        {
            //normally this will be your site URL    
            var issuer = "https://evoting.bigshareonline.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("ChecksumID", loginResponse.Token));
            

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
