namespace Challenger.Api.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Route = "api";
        public const string Version = "v1";
        public const string Base = Route + "/" + Version;

        public static class Migrator
        {   
            public const string CreateAppDatabase = Base + Base + "/createAppDatabase";
            public const string CreateIdentityDatabase = Base + "/createIdentityDatabase";
        }
    }
}
