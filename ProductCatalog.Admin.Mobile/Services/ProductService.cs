using ProductCatalog.Admin.Mobile.Models;
using ProductCatalog.Admin.Mobile.Repositories;

namespace ProductCatalog.Admin.Mobile.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<List<ProductModel>> GetProducts()
        => _productRepository.GetProducts();

    public Task<ProductModel?> GetProduct(Int64 id)
        => _productRepository.GetProduct(id);
}
