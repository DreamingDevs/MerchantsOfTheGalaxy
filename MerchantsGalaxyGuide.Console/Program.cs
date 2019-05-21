using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Core;
using MerchantsGalaxyGuide.Handlers;
using MerchantsGalaxyGuide.Model;
using MerchantsGalaxyGuide.Parsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Unity;

namespace MerchantsGalaxyGuide
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ConfigureContainer();
            string[] sentences = File.ReadAllLines(@"input.txt");
            Console.WriteLine("<=============== INPUT - START ===============>");
            foreach (var sentence in sentences)
            {
                Console.WriteLine(sentence);
            }

            Console.WriteLine("<=============== INPUT - END ===============>");
            Console.WriteLine();
            Console.WriteLine("<=============== OUTPUT - START ===============>");

            var interpreter = container.Resolve<IInterpreter>();
            var convertedSentences = interpreter.Process(sentences);
            foreach (var sentence in convertedSentences)
            {
                Console.WriteLine(sentence);
            }
            
            Console.WriteLine("<=============== OUTPUT - END ===============>");
            Console.ReadLine();
        }

        private static IUnityContainer ConfigureContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IInterpreter, Interpreter>();
            container.RegisterType<ISentenceParser, SentenceParser>();
            container.RegisterType<IWordParser, WordParser>();
            container.RegisterType<IValidator, Validator>();
            container.RegisterInstance<IExecutionContext>(new ExecutionContext());
            var config = LoadConfiguration(container);
            container.RegisterInstance<IConfiguration>(config);
            var handlers = LoadHandlers(container);
            container.RegisterInstance<ISentenceHandlers>(handlers);

            return container;
        }

        private static IConfiguration LoadConfiguration(IUnityContainer container)
        {
            var config = new Configuration
            {
                ReferenceWords = JsonConvert.DeserializeObject<List<ReferenceWord>>(File.ReadAllText(@"JsonFiles/Words.json")),
                SentenceTypeDefinitions = JsonConvert.DeserializeObject<List<SentenceTypeDefinition>>(File.ReadAllText(@"JsonFiles/SentenceTypes.json"))
            };

            return config;
        }

        private static ISentenceHandlers LoadHandlers(IUnityContainer container)
        {
            var handlers = new SentenceHandlers
            {
                Handlers = new Dictionary<SentenceType, IHandler>()
                {
                    {SentenceType.Declarative, new DeclarativeSentenceHandler(container.Resolve<IExecutionContext>()) },
                    {SentenceType.Imperative, new ImperativeSentenceHandler(container.Resolve<IExecutionContext>(), container.Resolve<IConfiguration>(), container.Resolve<IValidator>()) },
                    {SentenceType.Invalid, new InvalidSentenceHandler(container.Resolve<IExecutionContext>()) },
                    {SentenceType.Interrogative, new InterrogativeSentenceHandler(container.Resolve<IExecutionContext>(), container.Resolve<IConfiguration>(), container.Resolve<IValidator>()) }
                }
            };
            return handlers;
        }
    }
}
