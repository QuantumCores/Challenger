using QuantumCore.DbUp;

namespace Challenger.DbUp
{
    internal class Program : DbUpProgramBase
    {
        static void Main(string[] args)
        {
            Configure(args);
            RunDbUp();
        }
    }
}
