using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Models.ViewModels;
using Spice.Utility;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnviornment;
        private object fileMode;
        private object menuItemFromDb;
        private Stream filesStream;
        private string extension;

        [BindProperty]
        public MenuItemViewModel MenuItemVM { get; set; }
        public object MenuItemVm { get; private set; }

        public MenuItemController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnviornment = hostingEnvironment;
            MenuItemVM = new MenuItemViewModel()
            {
                Category = _db.Category,
                MenuItem = new Models.MenuItem()
            };
        }
        public async Task<IActionResult> Index()
        {
            var menuItems = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();
            return View(menuItems);
        }


        /// <summary>
        /// GET-CREATE
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            //var vm = new MenuItemViewModel();
            //vm.Category = new List<Category>();
            return View(MenuItemVM);
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            MenuItemVM.MenuItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                return View(MenuItemVm);
            }
            _db.MenuItem.Add(MenuItemVM.MenuItem);
            await _db.SaveChangesAsync();

            //work  on the image page saving section

            string webRootPath = _hostingEnviornment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.MenuItem.FindAsync(MenuItemVM.MenuItem.Id);
            if (files.Count > 0)
            {
                //files has been uploaded

                var uploads = Path.Combine(webRootPath, "Images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, MenuItemVM.MenuItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                menuItemFromDb.Image = @"\Images\" + MenuItemVM.MenuItem.Id + extension;
            }
            else
            {
                //no file was uploaded,so use default

                var uploads = Path.Combine(webRootPath, @"Images\" + SD.DefaultFoodImage);
                System.IO.File.Copy(uploads, webRootPath + @"\Images\" + MenuItemVM.MenuItem.Id + ".png");
                menuItemFromDb.Image = @"\Images\" + MenuItemVM.MenuItem.Id + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        /// <summary>
        /// GET-EDIT
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuItemVM.MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
            MenuItemVM.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == MenuItemVM.MenuItem.CategoryId).ToListAsync();

            if (MenuItemVM.MenuItem == null)
            {
              return NotFound();
            }
              return View(MenuItemVM);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuItemVM.MenuItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                return View(MenuItemVm);
            }
            _db.MenuItem.Add(MenuItemVM.MenuItem);
            await _db.SaveChangesAsync();

            //work  on the image page saving section

            string webRootPath = _hostingEnviornment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemFromDb = await _db.MenuItem.FindAsync(MenuItemVM.MenuItem.Id);
            if (files.Count > 0)
            {
                //files has been uploaded

                var uploads = Path.Combine(webRootPath, "Images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, MenuItemVM.MenuItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                menuItemFromDb.Image = @"\Images\" + MenuItemVM.MenuItem.Id + extension;
            }
            else
            {
                //no file was uploaded,so use default

                var uploads = Path.Combine(webRootPath, @"Images\" + SD.DefaultFoodImage);
                System.IO.File.Copy(uploads, webRootPath + @"\Images\" + MenuItemVM.MenuItem.Id + ".png");
                menuItemFromDb.Image = @"\Images\" + MenuItemVM.MenuItem.Id + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
