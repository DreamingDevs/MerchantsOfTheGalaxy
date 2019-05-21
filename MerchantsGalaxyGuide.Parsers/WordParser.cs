using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MerchantsGalaxyGuide.Parsers
{
    /// <summary>
    /// Word Parser class primarily dismantles a sentence to 
    /// set of words and identify each word with a specific type.
    /// </summary>
    public class WordParser : IWordParser
    {
        private readonly IConfiguration _configuration;
        private List<ContentWord> _contentWords = new List<ContentWord>();
        public WordParser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Parse method breaks a sentence into words and associates each word with specific type.
        /// </summary>
        /// <param name="content">Input sentence in string format.</param>
        /// <returns>List of contentWords.</returns>
        public List<ContentWord> Parse(string content)
        {
            var words = content.Split(new string[] { Keywords.SpaceDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            _contentWords = new List<ContentWord>();

            int position = 0;
            _contentWords.AddRange(words.Select(p => new ContentWord
            {
                Content = p,
                Position = position++,
                Type = DetermineInitialWordType(p)
            }));

            ProcessWords(_contentWords);

            return _contentWords;
        }

        /// <summary>
        /// This method performs initial processing of words and assigns the priliminary types like 
        /// Number, Operator, PrimaryQuery, SupportingQuery, RomanNumber to each word of the sentence.
        /// For unknown category of words which require further processing, it assigns None.
        /// </summary>
        /// <param name="word">Word content</param>
        /// <returns>Type of the Word.</returns>
        private WordType DetermineInitialWordType(string word)
        {
            var referenceWord = _configuration.ReferenceWords.FirstOrDefault(p => p.Content == word);
            if (referenceWord != null)
                return referenceWord.Type;

            double number;
            if (double.TryParse(word, out number))
                return WordType.Number;

            RomanNumber roman;
            if (Enum.TryParse<RomanNumber>(word, out roman))
                return WordType.Roman;
            
            return WordType.None;
        }

        /// <summary>
        /// This methods identifies the types of words which are Constants, Units and Products.
        /// </summary>
        /// <param name="words">List of ContentWords.</param>
        private void ProcessWords(List<ContentWord> words)
        {
            for (int index = 0; index < words.Count; index++)
            {
                var word = words[index];

                if (word.Type != WordType.None)
                    continue;

                if (index == 0 && words[index + 1].Type == WordType.Operator)
                {
                    word.Type = WordType.Constant;
                    continue;
                }

                if (DetermineUnitType(words, index))
                {
                    word.Type = WordType.Unit;
                    continue;
                }

                if (DetermineProductType(words, index))
                {
                    word.Type = WordType.Product;
                    continue;
                }

                word.Type = WordType.Constant;
            }

        }

        private bool DetermineUnitType(List<ContentWord> words, int index)
        {
            if (index == (words.Count - 1))
                return true;

            if (index - 1 < 0)
                return false;

            var previousWordType = words[index - 1].Type;
            var nextWordType = words[index + 1].Type;
            if (previousWordType == WordType.SupportingQuery && nextWordType == WordType.Operator)
                return true;

            return false;
        }

        private bool DetermineProductType(List<ContentWord> words, int index)
        {
            if (index - 1 < 0)
                return false;

            var previousWordType = words[index - 1].Type;
            var nextWordType = words[index + 1].Type;
            
            if (previousWordType == WordType.Constant && nextWordType == WordType.QueryDelimiter && words.Any(p => p.Type == WordType.Unit))
                return true;

            if (previousWordType == WordType.Constant && nextWordType == WordType.Operator)
                return true;

            return false;
        }
    }
}
