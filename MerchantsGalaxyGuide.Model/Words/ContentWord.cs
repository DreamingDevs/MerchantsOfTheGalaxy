namespace MerchantsGalaxyGuide.Model
{
    /// <summary>
    /// ContentWord class defines the word which is part of a sentence.
    /// </summary>
    public class ContentWord : Word
    {
        /// <summary>
        /// This property holds the position of the word within a sentence.
        /// </summary>
        public int Position { get; set; }
    }
}
