using Microsoft.AspNetCore.Authorization;

namespace ThousandAcreWoods.UI.Security;

public static class AuthorizationPolicies
{

    public const string MemberPolicy= "TAWMember";
    public const string AdminPolicy = "TAWAdmin";


    public static void AddMemberPolicy(this AuthorizationOptions opts)
    {
        opts.AddPolicy(MemberPolicy, (AuthorizationPolicyBuilder builder) =>
        {
            builder.RequireClaim(MemberPolicy, true.ToString());
        });
    }

    public static void AddAdminPolicy(this AuthorizationOptions opts)
    {
        opts.AddPolicy(AdminPolicy, (AuthorizationPolicyBuilder builder) =>
        {
            builder.RequireClaim(AdminPolicy, true.ToString());
        });

    }


}
