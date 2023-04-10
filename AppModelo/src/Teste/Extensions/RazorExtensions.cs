using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Teste.Extensions
{
    public static class RazorExtensions
    {
        // para validar algo
        public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue);
        }

        // desabilitar um botão caso o usuário não possua uma claim
        public static string IfClaimShow(this RazorPage page, string claimValue, string claimType)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimType, claimValue) ? "" : "disabled" ;
        }
        // Esconder algo da página caso o usuário não tenha uma claim
        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimType, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(context, claimType, claimValue) ? page : null;
        }


    }
}
