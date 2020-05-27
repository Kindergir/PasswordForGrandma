using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordForGrandma
{
    public class Solver
    {
        public (string password, int cost) GetPassword(IVocabulary vocabulary)
        {
            var keyboard = new[]
            {
                new[] {'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',},
                new[] {'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l',},
                new[] {'z', 'x', 'c', 'v', 'b', 'n', 'm',},
            };
            distancies = new Dictionary<(char firstLetter, char secondLetter), int>();
            for (int i = 0; i < keyboard.Length; i++)
            {
                for (int j = 0; j < keyboard[i].Length; j++)
                {
                    var startLetter = keyboard[i][j];
                    for (int k = 0; k < keyboard.Length; k++)
                    {
                        for (int l = 0; l < keyboard[k].Length; l++)
                        {
                            var destinationLetter = keyboard[k][l];
                            var distance = Math.Abs(i - k) + Math.Abs(j - l);
                            distancies.Add((startLetter, destinationLetter), distance);
                        }
                    }
                }
            }
            
            wordsCosts = new Dictionary<string, int>();
            foreach (var word in vocabulary.Words)
            {
                var cost = 0;
                for (int i = 0; i < word.Length - 2; i++)
                {
                    cost += distancies[(word[i], word[i + 1])];
                }
                wordsCosts.Add(word, cost);
            }

            words = vocabulary.Words;
            int bestOfTheBestPasswordCost = int.MaxValue;
            List<string> bestOfTheBestPassword = new List<string>();
            foreach (var word in words)
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
                    bestOfTheBestPassword = bestPassword.ToList();
                }
            }

            var answer = bestOfTheBestPassword[0] + bestOfTheBestPassword[1] + bestOfTheBestPassword[2] +
                         bestOfTheBestPassword[3];
            return (answer, bestOfTheBestPasswordCost);
        }

        private Dictionary<string, int> wordsCosts;
        private Dictionary<(char firstLetter, char secondLetter), int> distancies;
        private IEnumerable<string> words;
        private List<string> bestPassword;
        
        private void Dfs(Dictionary<string, int> wordColors, 
            string currentWord, 
            ref int currentPasswordLength,
            ref int currentPasswordCost,
            List<string> currentPassword,
            ref int bestPasswordCost)
        {
            if (currentPasswordLength + currentWord.Length > 24)
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
                var jumpToCurrentWordCost = distancies[(currentWordFirstLetter, previousWordLastLetter)];
                costsDiff = wordsCosts[currentWord] + jumpToCurrentWordCost;
            }
            else
            {
                costsDiff = wordsCosts[currentWord];
            }
            currentPasswordCost += costsDiff;

            if (currentPassword.Count == 4
                && currentPasswordCost < bestPasswordCost
                && currentPasswordLength >= 20
                && currentPasswordLength <= 24)
            {
                bestPassword = currentPassword.ToList();
                bestPasswordCost = currentPasswordCost;
            }

            if (currentPassword.Count <= 3)
            {
                foreach (var word in words)
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