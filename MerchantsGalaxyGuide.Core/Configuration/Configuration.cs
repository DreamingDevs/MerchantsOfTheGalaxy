using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Core
{
    /// <summary>
    /// Configuration class holds the application configuration data by implementing IConfiguration interface.
    /// </summary>
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            ReferenceWords = new List<ReferenceWord>();
            SentenceTypeDefinitions = new List<SentenceTypeDefinition>();
        }
        /// <summary>
        /// This property holds the Reference Words.
        /// </summary>
        public List<ReferenceWord> ReferenceWords { get; set; }
        /// <summary>
        /// This property holds the Sentence Type Defintions.
        /// </summary>
        public List<SentenceTypeDefinition> SentenceTypeDefinitions { get; set; }
    }
}
