using Api_1.DTO;

namespace Api_1.InterFace
{
    public interface IproductService
    {
        Task<ProductResponse> AddProduct(ProductRequest Request);
        Task<List<ProductResponse>> GetAllProduct();
        Task<ProductResponse> GetById(Guid id);
        Task<ProductResponse> DeleteByID(Guid id);
        Task<ProductResponse> UpdateProductAsync(Guid id, ProductRequest productRequest);
    }
}
