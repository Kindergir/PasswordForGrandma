using System.Collections.Generic;
using PasswordForGrandma;

namespace PasswordForGrandmaTests
{
    internal class Vocabulary : IVocabulary
    {
        public Vocabulary()
        {
            Words = new List<string>
            {
                "aba", "abacaba", "caba", "abacabadaba", "daba",
                "dabanaha", "kiy", "okofie", "ldk", "ks", "pmdwu",
                "kdjfiewu", "dlfjew", "wyyw", "pfkkq", "qsqekoc",
                "ldkfowkf", "kjui", "dkso", "lsdkos"
            };
        }
        
        public IEnumerable<string> Words { get; }
    }
}