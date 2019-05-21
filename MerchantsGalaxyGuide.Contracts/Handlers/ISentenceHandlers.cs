using MerchantsGalaxyGuide.Constants;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface which defines different sentence handlers.
    /// </summary>
    public interface ISentenceHandlers
    {
        /// <summary>
        /// This property maps different sentence handlers to their respective sentence types.
        /// </summary>
        Dictionary<SentenceType, IHandler> Handlers { get; set; }
    }
}
