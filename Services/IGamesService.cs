using Gaming.Models;
using Gaming.ViewModels;

namespace Gaming.Services
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task CreateAsync(CreateGameFormViewModel model);
        Task<Game?> UpdateAsync(EditGameFormViewModel model);
        bool Delete(int id);
    }
}
