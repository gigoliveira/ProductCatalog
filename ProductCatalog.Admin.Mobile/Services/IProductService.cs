using ProductCatalog.Admin.Mobile.Models;

namespace ProductCatalog.Admin.Mobile.Services;

public interface IProductService
{
    Task<List<ProductModel>> GetProducts();
    Task<ProductModel?> GetProduct(Int64 id);
}
