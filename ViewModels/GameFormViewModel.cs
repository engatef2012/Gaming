using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Gaming.ViewModels
{
    public abstract class GameFormViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(250)]
        public string Name { get; set; }
        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = new();
    }
}
