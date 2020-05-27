using System.Collections.Generic;
using System.Linq;

namespace PasswordForGrandma
{
    internal class DeepFirstSearcher
    {
        private readonly IVocabulary _vocabulary;
        private readonly Dictionary<string, int> _wordsCosts;
        private readonly Keyboard _keyboard;

        private List<string> _bestPassword;
        private PasswordSettings _settings;

        public DeepFirstSearcher(IVocabulary vocabulary)
        {
            _vocabulary = vocabulary;
            _keyboard = new Keyboard();
            _wordsCosts = WordsCostsCalculator.CalcWordsCosts(_vocabulary);
        }

        public (List<string> bestPassword, int bestPasswordCost) Search(PasswordSettings settings)
        {
            _settings = settings;
            int bestOfTheBestPasswordCost = int.MaxValue;
            List<string> bestOfTheBestPassword = new List<string>();
            foreach (var word in _vocabulary.Words)
            {
                var currentPasswordLength = 0;
                var currentPasswordCost = 0;
                var bestPasswordCost = int.MaxValue;
                Dfs(new Dictionary<string, int>(),
                    word,
                    ref currentPasswordLength,
                    ref currentPasswordCost,
                    new List<string>(),
                    ref bestPasswordCost);

                if (bestPasswordCost < bestOfTheBestPasswordCost)
                {
                    bestOfTheBestPasswordCost = bestPasswordCost;
                    bestOfTheBestPassword = _bestPassword.ToList();
                }
            }
            return (bestOfTheBestPassword, bestOfTheBestPasswordCost);
        }

        private void Dfs(Dictionary<string, int> wordColors, 
            string currentWord, 
            ref int currentPasswordLength,
            ref int currentPasswordCost,
            List<string> currentPassword,
            ref int bestPasswordCost)
        {
            if (currentPasswordLength + currentWord.Length > _settings.MaxLength)
                return;

            wordColors.Add(currentWord, 1);
            currentPasswordLength += currentWord.Length;
            currentPassword.Add(currentWord);

            int costsDiff;
            if (currentPassword.Count > 1)
            {
                var previousWord = currentPassword[currentPassword.Count - 2];
                var previousWordLastLetter = previousWord.Last();
                var currentWordFirstLetter = currentWord.First();
                var jumpToCurrentWordCost = _keyboard.GetDistance(currentWordFirstLetter, previousWordLastLetter);
                costsDiff = _wordsCosts[currentWord] + jumpToCurrentWordCost;
            }
            else
                costsDiff = _wordsCosts[currentWord];

            currentPasswordCost += costsDiff;

            if (currentPassword.Count == _settings.WordsCount
                && currentPasswordCost < bestPasswordCost
                && currentPasswordLength >= _settings.MinLength
                && currentPasswordLength <= _settings.MaxLength)
            {
                _bestPassword = currentPassword.ToList();
                bestPasswordCost = currentPasswordCost;
            }

            if (currentPassword.Count <= _settings.WordsCount - 1)
            {
                foreach (var word in _vocabulary.Words)
                {
                    if (!wordColors.ContainsKey(word))
                        Dfs(wordColors, 
                            word, 
                            ref currentPasswordLength,
                            ref currentPasswordCost,
                            currentPassword,
                            ref bestPasswordCost);
                }
            }

            wordColors.Remove(currentWord);
            currentPasswordLength -= currentWord.Length;
            currentPasswordCost -= costsDiff;
            currentPassword.Remove(currentWord);
        }
    }
}