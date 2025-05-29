using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFilmeService
{
    Task<List<Filme>> GetAllAsync();
    Task<Filme> GetByIdAsync(string id);
    Task CreateAsync(Filme filme);
    Task<bool> UpdateAsync(string id, Filme filme);
    Task<bool> DeleteAsync(string id);
}

public class FilmeService : IFilmeService
{
    private readonly IFilmeRepository _repository;

    public FilmeService(IFilmeRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Filme>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Filme> GetByIdAsync(string id) => _repository.GetByIdAsync(id);

    public Task CreateAsync(Filme filme) => _repository.CreateAsync(filme);

    public Task<bool> UpdateAsync(string id, Filme filme) => _repository.UpdateAsync(id, filme);

    public Task<bool> DeleteAsync(string id) => _repository.DeleteAsync(id);
}
