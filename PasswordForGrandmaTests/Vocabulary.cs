using System.Collections.Generic;
using PasswordForGrandma;

namespace PasswordForGrandmaTests
{
    internal class Vocabulary : IVocabulary
    {
        public IEnumerable<string> Words { get; set; }
    }
}