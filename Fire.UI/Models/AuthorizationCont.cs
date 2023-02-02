using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Fire.UI.Models
{
    public class AuthorizationCont
    {
        public static TokenKeys Authorization(string Token)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                var handler = new JwtSecurityTokenHandler();
                var decodedValue = handler.ReadJwtToken(Token.Replace("Bearer ", ""));
                TokenKeys mytoken = new TokenKeys
                {
                    email = decodedValue.Payload.Where(x => x.Key == "email").FirstOrDefault().Value.ToString(),
                    userid = decodedValue.Payload.Where(x => x.Key == "userId").FirstOrDefault().Value.ToString(),
                    userroleid = decodedValue.Payload.Where(x => x.Key == "roleid").FirstOrDefault().Value.ToString(),
                    branchid = decodedValue.Payload.Where(x => x.Key == "branchid").FirstOrDefault().Value.ToString(),
                    companyId = decodedValue.Payload.Where(x => x.Key == "companyid").FirstOrDefault().Value.ToString(),
                };
                return mytoken;
            }
            return null;
        }
        public class TokenKeys
        {
            public string userid { get; set; }
            public string? email { get; set; }
            public string userroleid { get; set; }
            public string branchid { get; set; }
            public string companyId { get; set; }

        }
    }
}
