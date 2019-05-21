
namespace MerchantsGalaxyGuide.Constants
{
    /// <summary>
    /// Supported Sentence Types - Declarative, Imperative, Interrogative, Invalid.
    /// </summary>
    public enum SentenceType
    {
        Declarative, Imperative, Interrogative, Invalid
    }

    /// <summary>
    /// Supported Word Types - Roman, Number, Constant, Unit, Operator, PrimaryQuery, SupportingQuery, QueryDelimiter, None, Product.
    /// </summary>
    public enum WordType
    {
        Roman, Number, Constant, Unit, Operator, PrimaryQuery, SupportingQuery, QueryDelimiter, None, Product
    }

    /// <summary>
    /// Representation of Roman Numerals.
    /// </summary>
    public enum RomanNumber
    {
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000
    }
}
