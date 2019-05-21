using MerchantsGalaxyGuide.Constants;
using MerchantsGalaxyGuide.Contracts;
using MerchantsGalaxyGuide.Model;
using MerchantsGalaxyGuide.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace MerchantsGalaxyGuide.Core
{
    /// <summary>
    /// Validator class implements IValidator Interface and provides the Validator API to validate sentences.
    /// </summary>
    public class Validator : IValidator
    {
        private readonly IExecutionContext _executionContext;
        private readonly IConfiguration _configuration;
        public Validator(IExecutionContext executionContext, IConfiguration configuration)
        {
            _executionContext = executionContext;
            _configuration = configuration;
        }

        /// <summary>
        /// This methods validates a sentence for errors.
        /// </summary>
        /// <param name="sentence">Input sentence in string format.</param>
        /// <returns>Result of the validation.</returns>
        public ValidationResult Validate(Sentence sentence)
        {
            ValidationResult result;
            switch (sentence.TypeDefinition.Type)
            {
                case SentenceType.Interrogative:
                    result = InterrogativeSentenceValidation(sentence);
                    break;
                case SentenceType.Imperative:
                    result = ImperativeSentenceValidation(sentence);
                    break;
                default:
                    result = new ValidationResult { IsValid = true };
                    break;
            }

            return result;
        }

        private ValidationResult InterrogativeSentenceValidation(Sentence sentence)
        {
            ValidationResult result = new ValidationResult { IsValid = true };
            var constants = sentence.Words.Where(p => p.Type == WordType.Constant).Select(p => p.Content).ToList();

            if (CheckConstantToRomanMappings(constants))
                return CreateInvalidResult();
            
            if (!IsValidRomanNumberFormation(constants))
                return CreateInvalidResult();

            if (!CheckProductUnitValue(sentence))
                return CreateInvalidResult();

            return result;
        }

        private ValidationResult ImperativeSentenceValidation(Sentence sentence)
        {
            ValidationResult result = new ValidationResult { IsValid = true };
            var product = sentence.Words.FirstOrDefault(p => p.Type == WordType.Product);
            var unit = sentence.Words.FirstOrDefault(p => p.Type == WordType.Unit);
            var constants = sentence.Words.Where(p => p.Type == WordType.Constant).Select(p => p.Content).ToList();

            if (CheckConstantToRomanMappings(constants))
                return CreateInvalidResult();

            if (!IsValidRomanNumberFormation(constants))
                return CreateInvalidResult();

            return result;
        }

        private bool IsValidRomanNumberFormation(List<string> constants)
        {
            var romanNumbers = constants
                .Select(p => _executionContext.ConstantsToRomanMap[p])
                .ToArray();

            return RomanNumbersUtility.IsValid(string.Join("", romanNumbers));
        }

        private bool CheckProductUnitValue(Sentence sentence)
        {
            var product = sentence.Words.FirstOrDefault(p => p.Type == WordType.Product);
            var unit = sentence.Words.FirstOrDefault(p => p.Type == WordType.Unit);
            if (product != null)
            {
                var unitValue = _executionContext.ProductUnitValues.FirstOrDefault(p => p.Product == product.Content && p.Unit == unit.Content);
                if (unitValue == null)
                    return false;
            }

            return true;
        }

        private bool CheckConstantToRomanMappings(List<string> constants)
        {
            var declaredConstants = _executionContext.ConstantsToRomanMap.Keys.ToList();
            return constants.Except(declaredConstants).Any();
        }

        private ValidationResult CreateInvalidResult()
        {
            return new ValidationResult { IsValid = false };
        }
    }
}
