using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Utility;

namespace Spice.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)                             //dependency injection
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Category.ToListAsync());
        }

        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]                                           //used for security purposes to stop atteck form hackers.
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //if valid 
                _db.Category.Add(category);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }

        ///GET-EDIT

        public async Task<IActionResult> Edit(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }



        //update into database and show in index

        /// <summary>
        /// Isvalid category
        /// </summary>
        /// <param name="category"></param>
        /// <returns> view (Index)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


       /// <summary>
       /// GET-DELETE
       /// </summary>
       /// <param name="Id"></param>
       /// <returns>view category</returns>
        public async Task<IActionResult> Delete(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        /// <summary>
        /// DELETE POST ACTION METHOD
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? Id)
        {
            var category = await _db.Category.FindAsync(Id);
            if (category == null)
            {
                return View();
            }
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET-DETAILS
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>category details</returns>

        public async Task<IActionResult> Details(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}