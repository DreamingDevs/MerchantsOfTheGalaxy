namespace MerchantsGalaxyGuide.Model
{
    /// <summary>
    /// ProductUnitValue class defines the value of a product in particular units.
    /// </summary>
    public class ProductUnitValue
    {
        /// <summary>
        /// This property defines the Product.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// This property defines the Unit which is used to evaulate the product.
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// This property defines the value of the product.
        /// </summary>
        public decimal Value { get; set; }
    }
}
