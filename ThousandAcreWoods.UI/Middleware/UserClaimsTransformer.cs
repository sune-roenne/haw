using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ThousandAcreWoods.UI.Middleware;

public class UserClaimsTransformer : IClaimsTransformation
{
    private readonly Regex _initialsRegex = new Regex("([a-x]|[0-9])+$");


    public UserClaimsTransformer()
    {
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identities.FirstOrDefault(_ => _.IsAuthenticated);
        if (identity == null)
            return principal;
        var userInitials = principal.Identity?.Name;
        if (userInitials == null)
            return principal;
        var cleanedInitials = userInitials
            .Trim()
            .ToLower();
        if (cleanedInitials.Length == 0)
            return principal;
        var matches = _initialsRegex.Matches(cleanedInitials);
        if (matches.Count == 0) return principal;
        cleanedInitials = matches[0].Value;
        if (string.IsNullOrEmpty(cleanedInitials)) return principal;
        return principal;
    }
}
