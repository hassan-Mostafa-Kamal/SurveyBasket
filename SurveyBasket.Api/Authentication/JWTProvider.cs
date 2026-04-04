
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication
{
    public class JWTProvider : IJWTProvider
    {
        public (string token, int expiresIn) GenerateToken(ApplicationUser user)
        {
            //our Claims
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.Id),
                new Claim(JwtRegisteredClaimNames.Email , user.Email!),
                new Claim(JwtRegisteredClaimNames.Name , user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName , user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            //the Kay for encode and decode the token 
            var kay = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("677e7db633adcf33cb2b6e5a5fab359a3f8f33b3c93272d8f8cec0c2a6bcbf21"));

            var singingCredentions = new SigningCredentials(kay,SecurityAlgorithms.HmacSha256);

            var expiresIn = 30;
            var expirationDate = DateTime.Now.AddMinutes(expiresIn);
            // token parts
            var tokenParts = new JwtSecurityToken(
                 issuer: "ServeyBasketApp",
                 audience: "ServeyBasketAppUsers",
                 claims: claims,
                 expires: expirationDate,
                 signingCredentials: singingCredentions
                );
            // token generator 
            var token = new JwtSecurityTokenHandler().WriteToken(tokenParts);
            return (token , expiresIn);
        }
    }
}
