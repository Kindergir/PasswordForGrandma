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
            PasswordSettings passwordSettings = new PasswordSettings(20, 24, 4);
            var (password, cost) = PasswordGenerator.Generate(new Vocabulary(), passwordSettings);
            Console.WriteLine(password, " ", cost);
        }
    }
}