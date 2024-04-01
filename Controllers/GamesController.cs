using Gaming.Services;
using Gaming.Settings;
using Gaming.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.Controllers
{
    public class GamesController : Controller
    {
        private readonly IDevicesService _devicesService;
        private readonly ICategoriesSevice _categoriesSevice;
        private readonly IGamesService _gamesService;

        public GamesController(IDevicesService devicesService, ICategoriesSevice categoriesSevice, IGamesService gamesService)
        {

            _devicesService = devicesService;
            _categoriesSevice = categoriesSevice;
            _gamesService = gamesService;
        }

        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if (game is null)
                return NotFound($"No Game found with id:{id}");
            return View(game);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var Categories = _categoriesSevice.GetSelectListItems();
            var devices = _devicesService.GetSelectListItems();
            var model = new CreateGameFormViewModel { Categories = Categories, Devices = devices };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesSevice.GetSelectListItems();
                model.Devices = _devicesService.GetSelectListItems();
                return View(model);
            }
            await _gamesService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetById(id);
            if (game is null)
                return NotFound();
            var viewModel = new EditGameFormViewModel
            {
                Categories = _categoriesSevice.GetSelectListItems(),
                Devices = _devicesService.GetSelectListItems(),
                Id = game.GameId,
                Description = game.Description,
                Name = game.Name,
                CategoryId = game.CategoryId,
                SelectedDevices = game.GameDevices.Select(d => d.DeviceId).ToList(),
                CurrentCover = $"/{FileSettings.ImagesPath}/{game.Cover}"
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (model.Cover is null)
            {
                ModelState.ClearValidationState("Cover");
                ModelState.MarkFieldValid("Cover");
            }
            if (!ModelState.IsValid)
            {
                var currentCover = _gamesService.GetById(model.Id)?.Cover;
                model.Categories = _categoriesSevice.GetSelectListItems();
                model.Devices = _devicesService.GetSelectListItems();
                model.CurrentCover = $"/{FileSettings.ImagesPath}/{currentCover}";
                return View(model);
            }
            var game = await _gamesService.UpdateAsync(model);
            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gamesService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
