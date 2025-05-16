using ProductCatalog.Admin.Mobile.Models;

namespace ProductCatalog.Admin.Mobile.Repositories;

public interface IProductRepository
{
    Task<List<ProductModel>> GetProducts();
    Task<ProductModel?> GetProduct(Int64 id);
}
