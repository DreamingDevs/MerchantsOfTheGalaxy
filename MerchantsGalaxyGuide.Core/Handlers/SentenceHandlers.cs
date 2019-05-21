using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using System.Collections.Generic;

namespace MerchantsGalaxyGuide.Core
{
    /// <summary>
    /// SentenceHandlers class implememented ISentenceHander interface tol hold the different sentence types and their associated handlers.
    /// </summary>
    public class SentenceHandlers : ISentenceHandlers
    {
        public SentenceHandlers()
        {
            Handlers = new Dictionary<SentenceType, IHandler>();
        }
        /// <summary>
        /// This property maps different sentence handlers to their respective sentence types.
        /// </summary>
        public Dictionary<SentenceType, IHandler> Handlers { get; set; }
    }
}
