namespace PasswordForGrandma
{
    public readonly struct PasswordSettings
    {
        public PasswordSettings(int minLength, int maxLength, int wordsCount)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            WordsCount = wordsCount;
        }

        public int MinLength { get; }
        public int MaxLength { get; }
        public int WordsCount { get; }
    }
}