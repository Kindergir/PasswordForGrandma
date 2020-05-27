using System;
using System.Collections.Generic;

namespace PasswordForGrandma
{
    public static class PasswordGenerator
    {
        public static (string password, int cost) Generate(IVocabulary vocabulary, PasswordSettings settings)
        {
            if (vocabulary?.Words == null)
                throw new ArgumentNullException("Vocabulary");

            var dfs = new DeepFirstSearcher(vocabulary);
            var (bestPassword, bestPasswordCost) = dfs.Search(settings);
            var password = GetPasswordFromWords(bestPassword);
            return (password, bestPasswordCost);
        }

        private static string GetPasswordFromWords(IEnumerable<string> words)
        {
            return words == null ? null : string.Join(string.Empty, words);
        }
    }
}