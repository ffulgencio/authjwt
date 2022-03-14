using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace authjwt.Helpers{
    public class JWTHelper{
        private readonly byte[] secret;
        public JWTHelper(string secretkey)
        {
            this.secret = Encoding.ASCII.GetBytes(@secretkey);
        }

        public string CreateToken (string @username ){
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, @username));

            var tokenDescription = new SecurityTokenDescriptor(){
                Subject = claims,
                Expires = System.DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.secret), SecurityAlgorithms.HmacSha256Signature)

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var createdTocken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(createdTocken);
        }
    }
}