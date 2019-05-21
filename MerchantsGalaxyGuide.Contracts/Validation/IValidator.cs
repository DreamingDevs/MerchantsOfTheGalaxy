using MerchantsGalaxyGuide.Model;

namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface defines the Validator API.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// This methods validates a sentence for errors.
        /// </summary>
        /// <param name="sentence">Input sentence in string format.</param>
        /// <returns>Result of the validation.</returns>
        ValidationResult Validate(Sentence sentence);
    }
}
