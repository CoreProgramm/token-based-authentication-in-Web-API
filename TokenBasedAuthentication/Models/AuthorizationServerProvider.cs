using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
namespace TokenBasedAuthentication.Models
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        UserMasterRepository _repo = new UserMasterRepository();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _repo.ValidateUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_access", "Username and Password is Incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRoles));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("UserEmail", user.UserEmailID));
            context.Validated(identity);
        }
    }
}