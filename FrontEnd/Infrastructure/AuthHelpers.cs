using FrontEnd.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Infrastructure
{
    public static class AuthConstants
    {
        public static readonly string isAdmin = nameof(isAdmin);
        public static readonly string isAttendee = nameof(isAttendee);
        public static readonly string TrueValue = "true";
    }
}

namespace System.Security.Claims
{
    public static class AuthnHelpers
    {
        public static bool isAdmin(this ClaimsPrincipal principal) =>
            principal.HasClaim(AuthConstants.isAdmin, AuthConstants.TrueValue);
        public static void MakeAdmin(this ClaimsPrincipal principal) =>
            principal.Identities.First().MakeAdmin();
        public static void MakeAdmin(this ClaimsIdentity identity) =>
            identity.AddClaim(new Claim(AuthConstants.isAdmin, AuthConstants.TrueValue));
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthzHelpers
    {
        public static AuthorizationPolicyBuilder RequireIsAdminClaim(this AuthorizationPolicyBuilder builder) =>
            builder.RequireClaim(AuthConstants.isAdmin, AuthConstants.TrueValue);
    }
}