namespace TestApp.Interfaces;

public interface IRepository<T> where T : class
{
    Task Create(T entity);
    Task<List<T>> Read();
    Task<int> Delete(int id);
}