namespace RechargeSharp.v2021_11.Endpoints.Products;

public partial class ProductService
{
    public static class SharedTypes
    {
        public class Product
        {
            public string Brand { get; set; }

            public string Description { get; set; }

            public DateTimeOffset ExternalCreatedAt { get; set; }

            public long ExternalProductId { get; set; }

            public DateTimeOffset ExternalUpdatedAt { get; set; }

            public Image[] Images { get; set; }

            public Option[] Options { get; set; }

            public DateTimeOffset PublishedAt { get; set; }

            public bool RequiresShipping { get; set; }

            public string Title { get; set; }

            public Variant[] Variants { get; set; }

            public string Vendor { get; set; }
        }

        public class Image
        {
            public string Large { get; set; }

            public string Medium { get; set; }

            public string Original { get; set; }

            public string Small { get; set; }

            public long? SortOrder { get; set; }
        }

        public class Option
        {
            public string Name { get; set; }

            public long Position { get; set; }

            public Value[] Values { get; set; }
        }

        public class Value
        {
            public string Label { get; set; }

            public long Position { get; set; }
        }

        public class Variant
        {
            public Dimensions Dimensions { get; set; }

            public long ExternalVariantId { get; set; }

            public Image Image { get; set; }

            public OptionValue[] OptionValues { get; set; }

            public Prices Prices { get; set; }

            public string Sku { get; set; }

            public string TaxCode { get; set; }

            public bool Taxable { get; set; }

            public string Title { get; set; }

            public bool RequiresShipping { get; set; }
        }

        public class Dimensions
        {
            public double Weight { get; set; }

            public string WeightUnit { get; set; }
        }

        public class OptionValue
        {
            public string Label { get; set; }
        }

        public class Prices
        {
            public string CompareAtPrice { get; set; }

            public string UnitPrice { get; set; }
        }
    }

    public static class GetProductsTypes
    {
        public record Request
        {
            public List<long>? ExternalProductIds { get; set; }
        }

        public record Response
        {
            public string? NextCursor { get; set; }
            public string? PreviousCursor { get; set; }
            public IReadOnlyList<SharedTypes.Product> Products { get; init; }
        }
    }
    
    public static class GetProductTypes
    {
        public record Response
        {
            public SharedTypes.Product? Product { get; init; }
        }
    }
}