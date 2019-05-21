using MerchantsGalaxyGuide.Model;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface for holding the application configuration.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// This property defines the Reference Words.
        /// </summary>
        List<ReferenceWord> ReferenceWords { get; set; }
        /// <summary>
        /// This property defines the Sentence Type Defintions.
        /// </summary>
        List<SentenceTypeDefinition> SentenceTypeDefinitions { get; set; }
    }
}
