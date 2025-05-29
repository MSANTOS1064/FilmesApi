using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFilmeRepository
{
    Task<List<Filme>> GetAllAsync();
    Task<Filme> GetByIdAsync(string id);
    Task CreateAsync(Filme filme);
    Task<bool> UpdateAsync(string id, Filme filme);
    Task<bool> DeleteAsync(string id);
}

public class FilmeRepository : IFilmeRepository
{
    private readonly IMongoCollection<Filme> _collection;

    public FilmeRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Filme>("Filmes");
    }

    public async Task<List<Filme>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Filme> GetByIdAsync(string id) =>
        await _collection.Find(f => f.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Filme filme) =>
        await _collection.InsertOneAsync(filme);

    public async Task<bool> UpdateAsync(string id, Filme filme)
    {
        var result = await _collection.ReplaceOneAsync(f => f.Id == id, filme);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(f => f.Id == id);
        return result.DeletedCount > 0;
    }
}
