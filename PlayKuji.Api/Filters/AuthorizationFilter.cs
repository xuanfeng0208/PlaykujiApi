using Microsoft.AspNetCore.Mvc.Filters;

namespace PlayKuji.Api.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
