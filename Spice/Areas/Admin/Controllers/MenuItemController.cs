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

		[BindProperty]
		public MenuItemViewModel MenuItemVm { get; set; }

		public MenuItemController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
		{
			_db = db;
			_hostingEnviornment = hostingEnvironment;
			MenuItemVm = new MenuItemViewModel()
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
			return View(MenuItemVm);
		}

		[HttpPost, ActionName("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreatePOST()
		{
			MenuItemVm.MenuItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

			if (!ModelState.IsValid)
			{
				return View(MenuItemVm);
			}
			_db.MenuItem.Add(MenuItemVm.MenuItem);
			await _db.SaveChangesAsync();

			//work  on the image page saving section

			string webRootPath = _hostingEnviornment.WebRootPath;
			var files = HttpContext.Request.Form.Files;

			var menuItemFromDb = await _db.MenuItem.FindAsync(MenuItemVm.MenuItem.Id);
			if (files.Count > 0)
			{
				//files has been uploaded

				var uploads = Path.Combine(webRootPath, "Images");
				var extension = Path.GetExtension(files[0].FileName);

				using (var filesStream = new FileStream(Path.Combine(uploads, MenuItemVm.MenuItem.Id + extension), FileMode.Create))
				{
					files[0].CopyTo(filesStream);
				}
				menuItemFromDb.Image = @"\Images\" + MenuItemVm.MenuItem.Id + extension;
			}
			else
			{
				//no file was uploaded,so use default

				var uploads = Path.Combine(webRootPath, @"Images\" + SD.DefaultFoodImage);
				System.IO.File.Copy(uploads, webRootPath + @"\Images\" + MenuItemVm.MenuItem.Id + ".png");
				menuItemFromDb.Image = @"\Images\" + MenuItemVm.MenuItem.Id + ".png";
			}
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		/// <summary>
		/// GET-EDIT
		/// </summary>
		/// <returns>Display Item After edit</returns>
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			MenuItemVm.MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
			MenuItemVm.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == MenuItemVm.MenuItem.CategoryId).ToListAsync();

			if (MenuItemVm.MenuItem == null)
			{
				return NotFound();
			}
			return View(MenuItemVm);
		}

		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPOST(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			MenuItemVm.MenuItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

			if (!ModelState.IsValid)
			{
				MenuItemVm.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == MenuItemVm.MenuItem.CategoryId).ToListAsync();
				return View(MenuItemVm);
			}

			//work  on the image page saving section

			string webRootPath = _hostingEnviornment.WebRootPath;
			var files = HttpContext.Request.Form.Files;

			var menuItemFromDb = await _db.MenuItem.FindAsync(MenuItemVm.MenuItem.Id);
			if (files.Count > 0)
			{
				//new Image has been uploaded

				var uploads = Path.Combine(webRootPath, "Images");
				var extension_new = Path.GetExtension(files[0].FileName);

				//Delete the orignal file 
				var imagePath = Path.Combine(webRootPath, menuItemFromDb.Image.TrimStart('\\'));

				if (System.IO.File.Exists(imagePath))
				{
					System.IO.File.Delete(imagePath);
				}


				//we will uploade a new file
				using (var filesStream = new FileStream(Path.Combine(uploads, MenuItemVm.MenuItem.Id + extension_new), FileMode.Create))
				{
					files[0].CopyTo(filesStream);
				}
				menuItemFromDb.Image = @"\Images\" + MenuItemVm.MenuItem.Id + extension_new;
			}

			menuItemFromDb.Name = MenuItemVm.MenuItem.Name;
			menuItemFromDb.Description = MenuItemVm.MenuItem.Description;
			menuItemFromDb.Price = MenuItemVm.MenuItem.Price;
			menuItemFromDb.Spicyness = MenuItemVm.MenuItem.Name;
			menuItemFromDb.CategoryId = MenuItemVm.MenuItem.CategoryId;
			menuItemFromDb.SubCategoryId = MenuItemVm.MenuItem.SubCategoryId;

			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}

		/// <summary>
		/// GET_DETAILS MenuItem
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Show Details</returns>
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			MenuItemVm.MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
			if (MenuItemVm.MenuItem == null)

			{
				return NotFound();
			}
			return View(MenuItemVm);
		}


		//GET:DELETE
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			MenuItemVm.MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
			if (MenuItemVm.MenuItem == null)
			{
				return NotFound();
			}
			return View(MenuItemVm);
		}


		//POST Delete MenuItem
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			string webRootPath = _hostingEnviornment.WebRootPath;
			MenuItem menuItem = await _db.MenuItem.FindAsync(id);

			if (menuItem != null)
			{
				var imagePath = Path.Combine(webRootPath, menuItem.Image.TrimStart('\\'));

				if (System.IO.File.Exists(imagePath))
				{
					System.IO.File.Delete(imagePath);
				}
				_db.MenuItem.Remove(menuItem);
				await _db.SaveChangesAsync();

			}

			return RedirectToAction(nameof(Index));
		}
	}
}