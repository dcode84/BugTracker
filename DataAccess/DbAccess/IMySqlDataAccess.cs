
namespace DataAccess.DbAccess;

public interface IMySqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionString = "Default");
    Task SaveData<T>(string storedProcedure, T parameters, string connectionString = "Default");
}
