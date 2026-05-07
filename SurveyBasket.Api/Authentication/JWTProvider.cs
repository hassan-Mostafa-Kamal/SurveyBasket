
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication
{
    public class JWTProvider : IJWTProvider
    {

        //public JWTProvider(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //private readonly IConfiguration _configuration;
        public JWTProvider(IOptions<JWTOptions> options)
        {
            _options = options;
        }
        private readonly IOptions<JWTOptions> _options;

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
            //var kay = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]!));
            var kay = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

            var singingCredentions = new SigningCredentials(kay,SecurityAlgorithms.HmacSha256);

            //var expiresIn = 30;

           // var expirationDate = DateTime.Now.AddMinutes(expiresIn);

            // token parts
            var tokenParts = new JwtSecurityToken(
                // issuer: _configuration["jwt:Issuer"],
                // audience: _configuration["jwt:Audience"],
                 issuer: _options.Value.Issuer,
                 audience: _options.Value.Audience,
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(_options.Value.ExpiryMinutes),
                 signingCredentials: singingCredentions
                );
            // token generator 
            var token = new JwtSecurityTokenHandler().WriteToken(tokenParts);
            return (token , _options.Value.ExpiryMinutes);
        }
    }
}
