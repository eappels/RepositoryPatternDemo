using SQLite;
using TestApp.Data.Models;
using TestApp.Interfaces;

namespace TestApp.Data.Repositories;

public class DemoModelRepository : IRepository<DemoModel>
{

    private readonly SQLiteAsyncConnection _database;

    public DemoModelRepository(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<DemoModel>().Wait();
        _database.CreateTableAsync<Item>().Wait();
    }

    public async Task<int> Create(DemoModel model)
    {
        await _database.InsertAsync(model);
        var id = model.Id;
        foreach (var item in model.Items)
        {
            item.DemoModelId = id;
            await _database.InsertAsync(item);
        }
        await Read();
        return id;
    }

    public async Task<int> Create(Item item)
    {
        return await _database.InsertAsync(item);
    }

    public async Task<List<DemoModel>> Read()
    {
        var list = await _database.Table<DemoModel>().ToListAsync();
        foreach (var model in list)
        {
            model.Items = await _database.Table<Item>()
                .Where(x => x.DemoModelId == model.Id)
                .ToListAsync();
        }
        return list;
    }

    public async Task<int> Update(int id)
    {
        var sql = $"UPDATE Item SET DemoModelId = {id} WHERE DemoModelId = -1";
        return await _database.ExecuteAsync(sql, id);
    }

    public async Task<int> Delete(int id)
    {
        await _database.Table<Item>()
            .Where(x => x.DemoModelId == id)
            .DeleteAsync();
        return await _database.Table<DemoModel>()
            .Where(x => x.Id == id)
            .DeleteAsync();
    }
}