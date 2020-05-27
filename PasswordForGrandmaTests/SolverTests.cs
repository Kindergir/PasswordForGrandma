using System;
using System.Collections.Generic;
using PasswordForGrandma;
using Xunit;

namespace PasswordForGrandmaTests
{
    public class SolverTests
    {
        [Fact]
        public void SimpleTest()
        {
            var passwordSettings = new PasswordSettings(20, 24, 4);
            var vocabulary = new Vocabulary
            {
                Words = new List<string>
                {
                    "aba", "abacaba", "caba", "abacabadaba", "daba",
                    "let", "cat", "password", "continue", "me", "kiskiskis",
                    "twix", "kitkat", "settings", "connection", "down",
                    "up", "dictionary", "folder", "lexus"
                }
            };
            var (password, cost) = PasswordGenerator.Generate(vocabulary, passwordSettings);
            Assert.Equal("me" + "daba" + "settings" + "folder", password);
            Assert.Equal(38, cost);
        }

        [Fact]
        public void WhenImpossibleToGeneratePassword()
        {
            var passwordSettings = new PasswordSettings(20, 24, 4);
            var vocabulary = new Vocabulary
            {
                Words = new List<string>
                {
                    "aba", "abacaba", "caba",
                }
            };
            var (password, cost) = PasswordGenerator.Generate(vocabulary, passwordSettings);
            Assert.Equal(string.Empty, password);
            Assert.Equal(0, cost);
        }

        [Fact]
        public void WhenInconsistentSettings()
        {
            var passwordSettings = new PasswordSettings();
            var vocabulary = new Vocabulary
            {
                Words = new List<string>
                {
                    "aba", "abacaba", "caba", "abacabadaba", "daba",
                    "let", "cat", "password", "continue", "me", "kiskiskis",
                    "twix", "kitkat", "settings", "connection", "down",
                    "up", "dictionary", "folder", "lexus"
                }
            };
            var (password, cost) = PasswordGenerator.Generate(vocabulary, passwordSettings);
            Assert.Equal(string.Empty, password);
            Assert.Equal(0, cost);
        }

        [Fact]
        public void WhenInconsistentVocabulary()
        {
            var passwordSettings = new PasswordSettings();
            var vocabulary = new Vocabulary();
            Assert.Throws<ArgumentNullException>(() => PasswordGenerator.Generate(vocabulary, passwordSettings));
        }
    }
}