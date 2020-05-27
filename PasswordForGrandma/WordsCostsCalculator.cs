using System.Collections.Generic;

namespace PasswordForGrandma
{
    internal static class WordsCostsCalculator
    {
        public static Dictionary<string, int> CalcWordsCosts(IVocabulary vocabulary)
        {
            var keyboard = new Keyboard();
            var wordsCosts = new Dictionary<string, int>();
            foreach (var word in vocabulary.Words)
            {
                var cost = 0;
                for (var i = 0; i < word.Length - 2; i++)
                    cost += keyboard.GetDistance(word[i], word[i + 1]);
                wordsCosts.Add(word, cost);
            }
            return wordsCosts;
        }
    }
}