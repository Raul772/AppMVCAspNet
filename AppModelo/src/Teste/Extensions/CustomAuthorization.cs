using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Teste.Extensions
{
    public class CustomAuthorization
    {


        public static bool ValidarClaimsUsuario(HttpContext context, string claimtype, string claimValues)
        {
            return context.User.Identity.IsAuthenticated &&
                    context.User.Claims.Any(c => c.Type == claimtype && c.Value.Contains(claimValues));
        }
    }


    public class AthorizeClaim : TypeFilterAttribute
    {
        public AthorizeClaim(string claimType, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            
            Arguments = new object[] { new Claim(claimType, claimValue) };

        }
    }


    public class RequisitoClaimFilter : IAuthorizationFilter
    {

        private readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }

}
