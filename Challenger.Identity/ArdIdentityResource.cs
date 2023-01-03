using IdentityServer4.Models;

namespace Challenger.Identity
{
    public class ArdIdentityResource : IdentityResource
    {
        public ArdIdentityResource()
        {
            base.Name = "ard";
            base.DisplayName = "Ard lifter";
            base.Description = "Says that profile is the best gym lifter";
            base.Emphasize = true;
            base.UserClaims = new []{ "ard_claim" };
        }
    }
}
