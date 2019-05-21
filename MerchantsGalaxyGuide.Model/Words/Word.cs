using MerchantsGalaxyGuide.Constants;

namespace MerchantsGalaxyGuide.Model
{
    /// <summary>
    /// Word class defines a word object.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// This property defines the content of the word.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// This property defines the type of word.
        /// </summary>
        public WordType Type { get; set; }
    }
}
