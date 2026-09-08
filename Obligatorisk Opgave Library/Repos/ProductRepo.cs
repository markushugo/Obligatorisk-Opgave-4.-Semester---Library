using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Obligatorisk_Opgave_4._Semester.IRepos;
using Obligatorisk_Opgave_4._Semester.Models;

namespace Obligatorisk_Opgave_4._Semester.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly string _connectionString;

        public ProductRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT ProductId, Name, Description, Price, Stock, ImageUrl FROM Product", connection);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var p = new Product()
                {
                    Id = Convert.ToInt32(reader["ProductId"]),
                    Name = reader["Name"]?.ToString(),
                    Description = reader["Description"]?.ToString(),
                    Price = Convert.ToDouble(reader["Price"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                    ImageUrl = reader["ImageUrl"] == DBNull.Value ? null : reader["ImageUrl"].ToString()
                };
                products.Add(p);
            }

            return products;
        }

        public Product GetProductById(int id)
        {
            Product? product = null;
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT ProductId, Name, Description, Price, Stock, ImageUrl FROM Product WHERE ProductId = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                product = new Product()
                {
                    Id = Convert.ToInt32(reader["ProductId"]),
                    Name = reader["Name"]?.ToString(),
                    Description = reader["Description"]?.ToString(),
                    Price = Convert.ToDouble(reader["Price"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                    ImageUrl = reader["ImageUrl"] == DBNull.Value ? null : reader["ImageUrl"].ToString()
                };
            }

            return product!;
        }

        public void AddProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("INSERT INTO Product (Name, Description, Price, Stock, ImageUrl) VALUES (@Name, @Description, @Price, @Stock, @ImageUrl)", connection);

            command.Parameters.AddWithValue("@Name", product.Name ?? string.Empty);
            command.Parameters.AddWithValue("@Description", product.Description ?? string.Empty);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@ImageUrl", (object?)product.ImageUrl ?? DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void UpdateProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("UPDATE Product SET Name = @Name, Description = @Description, Price = @Price, Stock = @Stock, ImageUrl = @ImageUrl WHERE ProductId = @Id", connection);

            command.Parameters.AddWithValue("@Name", product.Name ?? string.Empty);
            command.Parameters.AddWithValue("@Description", product.Description ?? string.Empty);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@ImageUrl", (object?)product.ImageUrl ?? DBNull.Value);
            command.Parameters.AddWithValue("@Id", product.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeleteProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("DELETE FROM Product WHERE ProductId = @Id", connection);
            command.Parameters.AddWithValue("@Id", product.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
