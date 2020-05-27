using System;
using System.Collections.Generic;
using FluentAssertions;
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
            var vocabulary = GenerateCorrectVocabulary();
            var (password, cost) = PasswordGenerator.Generate(vocabulary, passwordSettings);
            password.Should().Be("me" + "daba" + "settings" + "folder");
            cost.Should().Be(38);
        }

        [Fact]
        public void WhenImpossibleToGeneratePassword()
        {
            var passwordSettings = new PasswordSettings(20, 24, 4);
            var vocabulary = GenerateSmallVocabulary();
            var (password, cost) = PasswordGenerator.Generate(vocabulary, passwordSettings);
            password.Should().Be(string.Empty);
            cost.Should().Be(0);
        }

        [Fact]
        public void WhenInconsistentSettings()
        {
            var passwordSettings = new PasswordSettings();
            var vocabulary = GenerateCorrectVocabulary();
            var (password, cost) = PasswordGenerator.Generate(vocabulary, passwordSettings);
            password.Should().Be(string.Empty);
            cost.Should().Be(0);
        }

        [Fact]
        public void WhenInconsistentVocabulary()
        {
            var passwordSettings = new PasswordSettings();
            var vocabulary = new Vocabulary();
            var testAction = new Func<(string password, int cost)>(() => PasswordGenerator.Generate(vocabulary, passwordSettings));
            testAction.Should().Throw<ArgumentNullException>();
        }

        private IVocabulary GenerateCorrectVocabulary()
        {
            return new Vocabulary
            {
                Words = new List<string>
                {
                    "aba", "abacaba", "caba", "abacabadaba", "daba",
                    "let", "cat", "password", "continue", "me", "kiskiskis",
                    "twix", "kitkat", "settings", "connection", "down",
                    "up", "dictionary", "folder", "lexus"
                }
            };
        }

        private IVocabulary GenerateSmallVocabulary()
        {
            return new Vocabulary
            {
                Words = new List<string>
                {
                    "aba", "abacaba", "caba",
                }
            };
        }
    }
}