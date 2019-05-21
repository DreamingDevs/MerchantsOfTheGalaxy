using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using MerchantsGalaxyGuide.Utilities;
using System;
using System.Linq;

namespace MerchantsGalaxyGuide.Handlers
{
    /// <summary>
    /// ImperativeSentenceHandler class defines the logic to process a imperative sentence.
    /// </summary>
    public class ImperativeSentenceHandler : BaseSentenceHandler
    {
        private readonly IExecutionContext _executionContext;
        private readonly IConfiguration _configuration;
        private readonly IValidator _validator;
        public ImperativeSentenceHandler(IExecutionContext executionContext, IConfiguration configuration, IValidator validator) : base(executionContext)
        {
            _executionContext = executionContext;
            _configuration = configuration;
            _validator = validator;
        }
        /// <summary>
        /// This method is used to process a imperative sentence.
        /// As a result of the process, Execution Context is updated with Product-Unit-Value mappings.
        /// </summary>
        /// <param name="sentence">Sentence object to be processed.</param>
        public override void Process(Sentence sentence)
        {
            var validationResult = _validator.Validate(sentence);
            if (!validationResult.IsValid)
            {
                var invalidResult = _configuration.SentenceTypeDefinitions.FirstOrDefault(p => p.Type == SentenceType.Invalid);
                _executionContext.Results.Add(new Sentence(invalidResult.Result.Format));
                return;
            }

            var product = sentence.Words.First(p => p.Type == WordType.Product).Content;
            var unit = sentence.Words.First(p => p.Type == WordType.Unit).Content;
            var value = sentence.Words.First(p => p.Type == WordType.Number).Content;
            var romanNumbers = sentence.Words
                .Where(p => p.Type == WordType.Constant)
                .Select(p => _executionContext.ConstantsToRomanMap[p.Content])
                .ToArray();
           
            var unitValue = CalculateUnitValue(romanNumbers, Convert.ToDecimal(value));
            var existingUnitValue = _executionContext.ProductUnitValues.FirstOrDefault(p => p.Product == product && p.Unit == unit);
            if (existingUnitValue == null)
                _executionContext.ProductUnitValues.Add(new ProductUnitValue { Product = product, Unit = unit, Value = unitValue });
            else
                existingUnitValue.Value = unitValue;
        }

        private decimal CalculateUnitValue(RomanNumber[] romanNumbers, decimal value)
        {
            return value / RomanNumbersUtility.ConvertRomantoNumber(romanNumbers);
        }
    }
}
