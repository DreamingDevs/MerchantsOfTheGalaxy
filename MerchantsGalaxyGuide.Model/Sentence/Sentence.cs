
using MerchantsGalaxyGuide.Constants;
using System.Collections.Generic;
using System.Linq;

namespace MerchantsGalaxyGuide.Model
{
    /// <summary>
    /// Sentence class holds the type definition, content and metadata of a given sentence.
    /// </summary>
    public class Sentence
    {
        private readonly List<ContentWord> _words;
        private readonly string _content;
        /// <summary>
        /// Initializes a Sentence Object with defined set of Content Words.
        /// </summary>
        /// <param name="words"></param>
        public Sentence(List<ContentWord> words)
        {
            _words = words;
        }

        /// <summary>
        /// Initializes a Sentence Object with string content.
        /// </summary>
        /// <param name="content"></param>
        public Sentence(string content)
        {
            _content = content;
        }

        /// <summary>
        /// This property defines the Type Defition of a Sentence.
        /// </summary>
        public SentenceTypeDefinition TypeDefinition { get; set; }

        /// <summary>
        /// This property returns the list of content words of a sentence.
        /// </summary>
        public List<ContentWord> Words
        {
            get
            {
                return _words;
            }
        }

        /// <summary>
        /// Override default ToString method to combine all the words of the sentence.
        /// </summary>
        /// <returns>Fully formed sentence string.</returns>
        public override string ToString()
        {
            if(_words != null && _words.Any())
                return string.Join(Keywords.SpaceDelimiter, _words.Select(p => p.Content));
            
            return _content;
        }
    }
}
