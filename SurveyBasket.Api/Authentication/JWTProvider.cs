
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication
{
    public class JWTProvider : IJWTProvider
    {

        public JWTProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly IConfiguration _configuration;

       
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
            var kay = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]!));

            var singingCredentions = new SigningCredentials(kay,SecurityAlgorithms.HmacSha256);

            var expiresIn = 30;

            var expirationDate = DateTime.Now.AddMinutes(expiresIn);

            // token parts
            var tokenParts = new JwtSecurityToken(
                 issuer: _configuration["jwt:Issuer"],
                 audience: _configuration["jwt:Audience"],
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
