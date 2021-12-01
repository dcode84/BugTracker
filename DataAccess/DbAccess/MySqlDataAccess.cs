using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DataAccess.DbAccess;

public class MySqlDataAccess : IMySqlDataAccess
{
    private readonly IConfiguration _config;

    public MySqlDataAccess(IConfiguration config)
    {
        this._config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionString = "Default")
    {
        using MySqlConnection connection = new MySqlConnection(_config.GetConnectionString(connectionString));

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure, T parameters, string connectionString = "Default")
    {
        using MySqlConnection connection = new MySqlConnection(_config.GetConnectionString(connectionString));

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
