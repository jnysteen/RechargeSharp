using RechargeSharp.v2021_11.Utilities;

namespace RechargeSharp.v2021_11.Endpoints.Products;

public interface IProductService
{
    /// <summary>
    ///     Retrieves a Product from store’s product catalog using the external_product_id as lookup id.
    /// </summary>
    /// <param name="externalProductId">The product's externalProductId</param>
    /// <returns></returns>
    Task<ProductService.GetProductTypes.Response> GetProductAsync(long externalProductId);
    Task<ProductService.GetProductsTypes.Response> GetProductsAsync(ProductService.GetProductsTypes.Request request);
}

public partial class ProductService : IProductService
{
    private readonly IRechargeApiCaller _rechargeApiCaller;

    public ProductService(IRechargeApiCaller rechargeApiCaller)
    {
        _rechargeApiCaller = rechargeApiCaller;
    }

    public Task<GetProductTypes.Response> GetProductAsync(long externalProductId)
    {
        throw new NotImplementedException();
    }

    public Task<GetProductsTypes.Response> GetProductsAsync(GetProductsTypes.Request request)
    {
        throw new NotImplementedException();
    }
}







