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
            var solver = new Solver();
            var solution = solver.GetPassword(new Vocabulary());
            Console.WriteLine(solution.password, " ", solution.cost);
        }
    }
}