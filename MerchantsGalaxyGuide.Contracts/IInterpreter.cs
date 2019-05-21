namespace MerchantsGalaxyGuide.Contracts
{
    /// <summary>
    /// Interface which describes the Interpreter API.
    /// </summary>
    public interface IInterpreter
    {
        /// <summary>
        /// This method process multiple lines of string content.
        /// </summary>
        /// <param name="lines">Input array of content in string format.</param>
        /// <returns>Output array of content in string format.</returns>
        string[] Process(string[] lines);
    }
}
