using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using WebApi.Models;

namespace WebApi
{
    public partial class LoginProvider : OAuthAuthorizationServerProvider
    {
        TBL_IlcelerEntities3 db = new TBL_IlcelerEntities3();
        public User Login(string email, string password)
        {
            var user = db.User.Where(x => x.Email == email && x.Password == password && x.IsActive == true).ToList();
            if (user.Count > 0)
            {
                //giriş başarlı
                return user.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = Login(context.UserName, context.Password);
            if (user == null)
            {
                //hatalı
                context.SetError("invalid_grant", "hatalı Giriş");
            }
            else
            {
                //başarlı
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Email", context.UserName));
                identity.AddClaim(new Claim("Password", context.Password));
                identity.AddClaim(new Claim("UserId", user.ID.ToString()));
                context.Validated(identity);
            }
            return base.GrantResourceOwnerCredentials(context);
        }
    }
}