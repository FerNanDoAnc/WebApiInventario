using InventarioBackend.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace InventarioBackend.Services;

public class ProductService
{
    private readonly IConfiguration _config;

    public ProductService(IConfiguration config)
    {
        _config = config;
    }

    public List<Product> GetAll()
    {
        List<Product> list = new();
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("GetAllProducts", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                CategoryName = reader["CategoryName"].ToString()
            });
        }
        return list;
    }

    public Product? GetById(int id)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("GetProductById", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@pId", id);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                CategoryName = reader["CategoryName"].ToString()
            };
        }
        return null;
    }

    public void Insert(Product p)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("InsertProduct", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@pName", p.Name);
        cmd.Parameters.AddWithValue("@pDescription", p.Description);
        cmd.Parameters.AddWithValue("@pPrice", p.Price);
        cmd.Parameters.AddWithValue("@pStock", p.Stock);
        cmd.Parameters.AddWithValue("@pCategoryId", p.CategoryId);

        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public void Update(Product p)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("UpdateProduct", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@pId", p.Id);
        cmd.Parameters.AddWithValue("@pName", p.Name);
        cmd.Parameters.AddWithValue("@pDescription", p.Description);
        cmd.Parameters.AddWithValue("@pPrice", p.Price);
        cmd.Parameters.AddWithValue("@pStock", p.Stock);
        cmd.Parameters.AddWithValue("@pCategoryId", p.CategoryId);

        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("DeleteProduct", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@pId", id);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}
