using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gaming.Services
{
    public interface IDevicesService
    {
        IEnumerable<SelectListItem> GetSelectListItems();
    }
}
