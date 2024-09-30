using Api_1.DTO;
using Api_1.Entity;
using Api_1.InterFace;
using Api_1.Repository;

namespace Api_1.Service
{
    public class ProductService : IproductService
    {
        private readonly IproductRepository _repository;

        public ProductService(IproductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse> AddProduct(ProductRequest Request)
        {
            var data = new Product()
            {
                Name = Request.Name,
                Price = Request.Price,
                Category = Request.Category,
            };
           
            var addedProduct = await _repository.AddProduct(data);
            //map
            var ProductResponse = new ProductResponse()
            {
                Id = addedProduct.Id,
                Name = addedProduct.Name,
                Price = addedProduct.Price,
                Category = addedProduct.Category,
            };
           return ProductResponse;

        }
        public async Task<List<ProductResponse>> GetAllProduct()
        {
            var data = await _repository.GetAllProduct();
            //response
            var response = new List<ProductResponse>();
            foreach (var item in data)
            {
                var res = new ProductResponse()
                {
                    Id= item.Id,
                    Name= item.Name,
                    Price= item.Price,
                    Category = item.Category,
                };
                response.Add(res);
            }
            return response;
        }
        public async Task<ProductResponse> GetById(Guid id)
        {
            var data = await _repository.GetById(id);
            var response = new ProductResponse()
            {
                Id = data.Id,
                Name = data.Name,
                Price = data.Price,
                Category = data.Category,
            };
            return response;
        }

        public async Task<ProductResponse> DeleteByID(Guid id)
        {
            var data = await _repository.DeleteByID(id);
            //response
            var response = new ProductResponse()
            {
                Id = data.Id,
                Name = data.Name,
                Price = data.Price,
                Category = data.Category,
            };
            return response;

        }

        //update
        public async Task<ProductResponse> UpdateProductAsync(Guid id, ProductRequest productRequest)
        {
            // Find the product by ID
            var existingProduct = await _repository.FindByIdAsync(id);

            if (existingProduct == null)
            {
                // Handle the case where the product does not exist
                return null; // Or throw an exception, e.g., throw new KeyNotFoundException("Product not found");
            }

            // Update product details
            existingProduct.Name = productRequest.Name;
            existingProduct.Price = productRequest.Price;
            existingProduct.Category = productRequest.Category;

            // Save updated product
            var updatedProduct = await _repository.UpdateProductAsync(existingProduct);

            if (updatedProduct == null)
            {
                // Handle the case where update failed
                return null; // Or throw an exception
            }

            // Map the updated product to ProductResponse
            var response = new ProductResponse
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Price = updatedProduct.Price,
                Category = updatedProduct.Category
            };

            return response;
        }
    }
}
