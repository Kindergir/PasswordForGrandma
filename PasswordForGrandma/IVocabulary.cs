using System.Collections.Generic;

namespace PasswordForGrandma
{
    public interface IVocabulary
    {
        IEnumerable<string> Words { get; }
    }
}