using DbUp;
using DbUp.Engine;

namespace Challenger.DbUp
{
    public class ScriptRunner
    {
        private const string _IdentityScripts = "./DB/Identity";
        private const string _GameScripts = "./DB/Scripts/01";
        private readonly string _identityConnectionString;
        private readonly string _gameConnectionString;

        public ScriptRunner(string identityConnectionString, string gameConnectionString)
        {
            _identityConnectionString = identityConnectionString;
            _gameConnectionString = gameConnectionString;
        }

        public static string GetConnectionString(string server, string database)
        => $"Server={server}; Initial Catalog={database}; Trusted_Connection=true";

        public DatabaseUpgradeResult RunGameDbScripts()
        {
            EnsureDatabase.For.SqlDatabase(_gameConnectionString);

            var upgrader = DeployChanges.To
                                        .SqlDatabase(_gameConnectionString)
                                        .WithScriptsFromFileSystem(_GameScripts)
                                        //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                                        .LogToTrace()
                                        .Build();

            var result = upgrader.PerformUpgrade();

            return result;
        }

        public DatabaseUpgradeResult RunIdentityDbScripts()
        {
            EnsureDatabase.For.SqlDatabase(_identityConnectionString);

            var upgrader = DeployChanges.To
                                        .SqlDatabase(_identityConnectionString)
                                        .WithScriptsFromFileSystem(_IdentityScripts)
                                        //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                                        .LogToTrace()
                                        .Build();

            var result = upgrader.PerformUpgrade();

            return result;
        }
    }
}
