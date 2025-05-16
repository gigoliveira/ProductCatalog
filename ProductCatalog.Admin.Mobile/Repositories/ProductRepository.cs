using ProductCatalog.Admin.Mobile.Helpers;
using ProductCatalog.Admin.Mobile.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ProductCatalog.Admin.Mobile.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ProductModel?> GetProduct(Int64 id)
    {
        using HttpClient client = _httpClientFactory.CreateClient("ProductsCatalogAdminApiClient");

        return await RetryHelper.ExecuteWithRetryAsync<ProductModel?>(
            async cancellationToken =>
            {
                var response = await client.GetAsync($"products/{id}", cancellationToken);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ProductModel>(
                    new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            },
            maxRetries: 3,
            timeoutPerTry: TimeSpan.FromSeconds(5),
            delay: TimeSpan.FromSeconds(2)
        );
    }
    public async Task<List<ProductModel>> GetProducts()
    {
        using HttpClient client = _httpClientFactory.CreateClient("ProductsCatalogAdminApiClient");

        var products = await RetryHelper.ExecuteWithRetryAsync<List<ProductModel>>(
            async cancellationToken =>
            {
                var response = await client.GetAsync("products", cancellationToken);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<ProductModel>>(
                    new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);

                return result ?? new List<ProductModel>();
            },
            maxRetries: 3,
            timeoutPerTry: TimeSpan.FromSeconds(5),
            delay: TimeSpan.FromSeconds(2)
        );

        return products ?? new List<ProductModel>();
    }

}
