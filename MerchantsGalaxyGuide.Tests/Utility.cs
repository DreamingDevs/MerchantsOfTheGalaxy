using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Core;
using MerchantsGalaxyGuide.Handlers;
using MerchantsGalaxyGuide.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MerchantsGuideToTheGalaxy.Test
{
    public class Utility
    {
        static Utility()
        {
            Context = new ExecutionContext();
        }

        public static ExecutionContext Context { get; set; }

        public static IConfiguration LoadConfiguration()
        {
            var config = new Configuration
            {
                ReferenceWords = JsonConvert.DeserializeObject<List<ReferenceWord>>(File.ReadAllText(@"JsonFiles/Words.json")),
                SentenceTypeDefinitions = JsonConvert.DeserializeObject<List<SentenceTypeDefinition>>(File.ReadAllText(@"JsonFiles/SentenceTypes.json"))
            };

            return config;
        }
        
        public static ISentenceHandlers LoadHandlers(IConfiguration configuration)
        {
            var validator = new Validator(Context, configuration);
            var handlers = new SentenceHandlers
            {
                Handlers = new Dictionary<SentenceType, IHandler>()
                {
                    {SentenceType.Declarative, new DeclarativeSentenceHandler(Context) },
                    {SentenceType.Imperative, new ImperativeSentenceHandler(Context, configuration, validator) },
                    {SentenceType.Invalid, new InvalidSentenceHandler(Context) },
                    {SentenceType.Interrogative, new InterrogativeSentenceHandler(Context, configuration, validator) }
                }
            };
            return handlers;
        }
    }
}
