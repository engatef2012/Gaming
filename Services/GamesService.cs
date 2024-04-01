using Gaming.Data;
using Gaming.Models;
using Gaming.Settings;
using Gaming.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Services
{
    public class GamesService : IGamesService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private readonly string _imagesPath;
        public GamesService(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _environment = environment;
            _context = context;
            _imagesPath = $"{_environment.WebRootPath}/{FileSettings.ImagesPath}";
        }

        public async Task CreateAsync(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                GameDevices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            await _context.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var game = _context.Games.Find(id);
            if (game is null)
                return false;
            _context.Remove(game);
            var affectedRows = _context.SaveChanges();
            if (affectedRows > 0)
            {
                var coverPath = Path.Combine(_imagesPath, game.Cover);
                File.Delete(coverPath);
                return true;
            }
            return false;
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.GameDevices)
                .ThenInclude(d => d.Device)
                .AsNoTracking().ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.GameDevices)
                .ThenInclude(d => d.Device)
                .FirstOrDefault(g => g.GameId == id);
        }

        public async Task<Game?> UpdateAsync(EditGameFormViewModel model)
        {
            var game = await _context.Games
                .Include(g => g.GameDevices)
                .FirstOrDefaultAsync(g => g.GameId == model.Id);
            if (game is null)
                return null;
            var hasNewCover = model.Cover is not null;
            var currentCover = game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.GameDevices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            var affectedRows = _context.SaveChanges();
            if (affectedRows >= 0)
            {
                if (hasNewCover)
                {
                    var oldCoverPath = Path.Combine(_imagesPath, currentCover);
                    File.Delete(oldCoverPath);
                }
                return game;
            }
            else
            {
                var newCoverPath = Path.Combine(_imagesPath, game.Cover);
                File.Delete(newCoverPath);
                return null;
            }
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }
    }
}
