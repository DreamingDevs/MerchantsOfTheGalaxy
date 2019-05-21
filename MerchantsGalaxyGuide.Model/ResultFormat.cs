namespace MerchantsGalaxyGuide.Model
{
    /// <summary>
    /// ResultFormat class defines the format of the result as a result of parsing of a sentence.
    /// </summary>
    public class ResultFormat
    {
        /// <summary>
        /// This property defines the format of the result.
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// This property defines the number of arguments which are accomodated in the result format.
        /// </summary>
        public int Arguments { get; set; }
    }
}
