using Gaming.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly ApplicationDbContext _context;

        public DevicesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return _context.Devices.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.DeviceId.ToString()
            }).OrderBy(c => c.Text).AsNoTracking();
        }
    }
}
