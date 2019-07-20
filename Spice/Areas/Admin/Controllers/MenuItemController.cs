using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models.ViewModels;

namespace Spice.Areas.Admin.Controllers
{
    [Area ("Admin")]
  
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
       // private readonly IHostingEnviornment _hostingEnviornment;
       [BindProperty]
        public MenuItemViewModel MenuItemVM { get; set; }
        public MenuItemController(ApplicationDbContext db)
        {
            _db = db;
            // _hostingEnviornment = hostingEnviornment;
            MenuItemVM = new MenuItemViewModel()
            {
                Category = _db.Category,
                MenuItem = new Models.MenuItem()
            };
        }
        public async Task<IActionResult>Index()
        {
            var menuItem = await _db.MenuItem.ToListAsync();
            return View(menuItem);
        }


        //GET-CREATE
        public IActionResult Create()
        {
            return View(MenuItemVM);

        }
    }
}
