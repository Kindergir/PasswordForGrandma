using System;
using System.Collections.Generic;

namespace PasswordForGrandma
{
    internal class Keyboard
    {
        private readonly Dictionary<(char firstLetter, char secondLetter), int> distances;

        public Keyboard()
        {
            distances = CalcDistances();
        }

        public int GetDistance(char startLetter, char destinationLetter)
        {
            return distances[(startLetter, destinationLetter)];
        }

        private Dictionary<(char firstLetter, char secondLetter), int> CalcDistances()
        {
            var keyboard = GenerateKeyboard();
            var distances = new Dictionary<(char firstLetter, char secondLetter), int>();
            for (var i = 0; i < keyboard.Length; i++)
            {
                for (var j = 0; j < keyboard[i].Length; j++)
                {
                    var startLetter = keyboard[i][j];
                    for (var k = 0; k < keyboard.Length; k++)
                    {
                        for (var l = 0; l < keyboard[k].Length; l++)
                        {
                            var destinationLetter = keyboard[k][l];
                            var distance = Math.Abs(i - k) + Math.Abs(j - l);
                            distances.Add((startLetter, destinationLetter), distance);
                        }
                    }
                }
            }
            return distances;
        }
        
        private char[][] GenerateKeyboard()
        {
            return new[]
            {
                new[] {'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',},
                new[] {'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l',},
                new[] {'z', 'x', 'c', 'v', 'b', 'n', 'm',},
            };
        }
    }
}