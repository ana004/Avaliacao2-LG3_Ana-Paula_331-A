using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    public Product Save(Product product) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO Products(id, name, price, active) VALUES(@Id, @Name, @Price, @Active);", product);
        return product;
    }

    public List<Product> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        return connection.Query<Product>("SELECT * FROM Products;").ToList();
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("DELETE FROM Products WHERE id=@Id", new {Id = id});
    }

    public void Enable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("UPDATE Products SET active=true WHERE id=@Id", new {Id = id});
    }

    public void Disable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("UPDATE Products SET active=false WHERE id=@Id", new {Id = id});
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<int>("SELECT count(id) FROM Products WHERE id=@Id", new {Id = id});

        return result > 0;
    }

}