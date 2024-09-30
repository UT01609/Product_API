using Api_1.Entity;

namespace Api_1.InterFace
{
    public interface IproductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<List<Product>> GetAllProduct();
        Task<Product> GetById(Guid id);
        Task<Product> DeleteByID(Guid id);
        Task<Product> FindByIdAsync(Guid id);
        Task<Product> UpdateProductAsync(Product product);
    }
}
