using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gaming.Services
{
    public interface ICategoriesSevice
    {
        IEnumerable<SelectListItem> GetSelectListItems();
    }
}
