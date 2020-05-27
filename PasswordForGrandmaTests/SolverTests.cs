using System;
using PasswordForGrandma;
using Xunit;

namespace PasswordForGrandmaTests
{
    public class SolverTests
    {
        [Fact]
        public void Test1()
        {
            var solver = new PasswordGenerator();
            PasswordSettings passwordSettings;
            var (password, cost) = solver.Generate(new Vocabulary(), passwordSettings);
            Console.WriteLine(password, " ", cost);
        }
    }
}