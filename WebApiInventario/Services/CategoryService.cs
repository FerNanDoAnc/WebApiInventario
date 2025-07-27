using MySql.Data.MySqlClient;
using System.Data;
using InventarioBackend.Models;

namespace InventarioBackend.Services;

public class CategoryService
{
    private readonly IConfiguration _config;

    public CategoryService(IConfiguration config)
    {
        _config = config;
    }

    public List<Category> GetAll()
    {
        List<Category> list = new();
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("GetAllCategories", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Category
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()
            });
        }
        return list;
    }

    public Category? GetById(int id)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("GetCategoryById", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@cId", id);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Category
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()
            };
        }
        return null;
    }

    public void Insert(Category category)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("InsertCategory", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@cName", category.Name);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public void Update(Category category)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("UpdateCategory", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@cId", category.Id);
        cmd.Parameters.AddWithValue("@cName", category.Name);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
    
    public void Delete(int id)
    {
        using var conn = new MySqlConnection(_config.GetConnectionString("MySqlConnection"));
        using var cmd = new MySqlCommand("DeleteCategory", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@cId", id);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}
