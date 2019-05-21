using MerchantsGalaxyGuide.Constants;
using System.Text.RegularExpressions;

namespace MerchantsGalaxyGuide.Utilities
{
    /// <summary>
    /// The Roman Number Utility class.
    /// Holds all the methods to perform computation on Roman Numerals.
    /// </summary>
    public static class RomanNumbersUtility
    {
        /// <summary>
        /// Converts a Roman Number to its equivalent Decimal number.
        /// </summary>
        /// <param name="romanNumbers">Array of Roman Numerals representing the Roman number.</param>
        /// <returns>Decimal number which is equivalent to the Roman number.</returns>
        public static decimal ConvertRomantoNumber(RomanNumber[] romanNumbers)
        {
            decimal totalValue = 0;
            for (int i = 0; i < romanNumbers.Length; i++)
            {
                var current = (int)romanNumbers[i];
                var next = i + 1 < romanNumbers.Length ? (int)romanNumbers[i + 1] : 0;
                current = next > current ? -current : current;
                totalValue += current;
            }

            return totalValue;
        }

        /// <summary>
        /// Validate the format of a given Roman Number.
        /// </summary>
        /// <param name="romanNumber">Roman Number in string format.</param>
        /// <returns>Returns true, if roman number format is valid, otherwise false.</returns>
        public static bool IsValid(string romanNumber)
        {
            var match = Regex.Match(romanNumber, @"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");
            return match.Success;
        }
    }
}
