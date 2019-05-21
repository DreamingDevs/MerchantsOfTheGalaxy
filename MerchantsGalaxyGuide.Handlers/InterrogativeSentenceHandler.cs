using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using MerchantsGalaxyGuide.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MerchantsGalaxyGuide.Handlers
{
    /// <summary>
    /// InterrogativeSentenceHandler class defines the logic to process a interrogative sentence.
    /// </summary>
    public class InterrogativeSentenceHandler : BaseSentenceHandler
    {
        private readonly IExecutionContext _executionContext;
        private readonly IConfiguration _configuration;
        private readonly IValidator _validator;
        public InterrogativeSentenceHandler(IExecutionContext executionContext, IConfiguration configuration, IValidator validator) : base(executionContext)
        {
            _executionContext = executionContext;
            _configuration = configuration;
            _validator = validator;
        }
        /// <summary>
        /// This method is used to process a interrogative sentence.
        /// As a result of the process, Execution Context is updated with the result of the sentence parsing.
        /// This handler uses data from Execution Context to obtain the result.
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

            var product = sentence.Words.FirstOrDefault(p => p.Type == WordType.Product);
            var unit = sentence.Words.FirstOrDefault(p => p.Type == WordType.Unit);
            var constants = sentence.Words.Where(p => p.Type == WordType.Constant).Select(p => p.Content).ToList();
            var constantsString = string.Join(" ", constants);
            var totalValue = CalculateTotalProductUnitsValue(product?.Content, unit?.Content, constants);

            string output = string.Empty;
            switch(sentence.TypeDefinition.Result.Arguments)
            {
                case 2:
                    output = string.Format(sentence.TypeDefinition.Result.Format, constantsString, totalValue);
                    break;
                case 4:
                    output = string.Format(sentence.TypeDefinition.Result.Format, constantsString, product.Content, totalValue, unit.Content);
                    break;
                default:
                    output = "";
                    break;
            }

            _executionContext.Results.Add(new Sentence(output));
        }

        private decimal CalculateTotalProductUnitsValue(string product, string unit, List<string>  constants)
        {
            var romanNumbers = constants
                .Select(p => _executionContext.ConstantsToRomanMap[p])
                .ToArray();

            decimal totalValue = 0;
            if (product != null)
            {
                var unitValue = _executionContext.ProductUnitValues.FirstOrDefault(p => p.Product == product && p.Unit == unit);
                totalValue = CalculateUnitValue(romanNumbers, unitValue.Value);
            }
            else
                totalValue = CalculateUnitValue(romanNumbers, 1);

            return totalValue;
        }

        private decimal CalculateUnitValue(RomanNumber[] romanNumbers, decimal value)
        {
            return RomanNumbersUtility.ConvertRomantoNumber(romanNumbers) * value;
        }
    }
}
