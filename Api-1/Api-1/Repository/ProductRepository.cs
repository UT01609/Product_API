using Api_1.Entity;
using Api_1.InterFace;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace Api_1.Repository
{
    public class ProductRepository : IproductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        //Add product
        public async Task<Product> AddProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO Product (id, name, price, category) VALUES (@id, @name, @price, @category)", connection);

                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price",product.Price);
                command.Parameters.AddWithValue("@category",product.Category);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return product;
        }

      //  Get All
       public async Task<List<Product>> GetAllProduct()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT * FROM Product", connection))
            {
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Category = reader.GetString(3)
                        });
                    }
                }
            }
            return products;
        }

        //Get By Id
        public async Task<Product> GetById(Guid id)
        {
            Product product = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Product WHERE id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        product = new Product
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Category = reader.GetString(3)
                        };
                    }
                }
                return product;
            }
        }

        public async Task<Product> DeleteByID(Guid id)
        {
            Product product = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                // Retrieve the product before deleting
                var selectCommand = new SqlCommand("SELECT * FROM Product WHERE Id = @id", connection);
                selectCommand.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();

                using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        product = new Product
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Category = reader.GetString(3)
                        };
                    }
                }

                // If the product exists, delete it
                if (product != null)
                {
                    var deleteCommand = new SqlCommand("DELETE FROM Product WHERE Id = @id", connection);
                    deleteCommand.Parameters.AddWithValue("@id", id);

                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }

            return product;
        }

        //Update / Edit
        public async Task<Product> FindByIdAsync(Guid id)
        {
            Product product = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Product WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        product = new Product
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Category = reader.GetString(3)
                        };
                    }
                }
            }

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            Product updatedProduct = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Product SET Name = @name, Price = @price, Category = @category WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", product.Id);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@category", product.Category);

                await connection.OpenAsync();

                var affectedRows = await command.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                {
                    updatedProduct = product;
                }
            }

            return updatedProduct;
        }


    }
}
