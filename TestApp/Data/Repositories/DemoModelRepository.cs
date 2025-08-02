using SQLite;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Data.Repositories;

public class DemoModelRepository : IRepository<DemoModel>
{

    private readonly SQLiteAsyncConnection _database;

    public DemoModelRepository(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<DemoModel>().Wait();
    }

    public async Task<int> Create(DemoModel model)
    {                                            
        return await _database.InsertAsync(model);
    }

    public async Task<List<DemoModel>> Read()
    {
        return await _database.Table<DemoModel>().ToListAsync();
    }

    public async Task<int?> Update(DemoModel model)
    {
        return await _database.UpdateAsync(model);
    }

    public async Task<int> Delete(int id)
    {
        return await _database.Table<DemoModel>()
            .Where(x => x.Id == id)
            .DeleteAsync();
    }
}