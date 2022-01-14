using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mirza_Lorena_practic.Data;
using Mirza_Lorena_practic.Models;

namespace Mirza_Lorena_practic.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieShopContext _context;

        public MoviesController(MovieShopContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var movies = from b in _context.Movies
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    movies = movies.OrderByDescending(b => b.Title);
                    break;
                case "Price":
                    movies = movies.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    movies = movies.OrderByDescending(b => b.Price);
                    break;
                default:
                    movies = movies.OrderBy(b => b.Title);
                    break;
            }

            int pageSize = 2;
            return View(await PaginatedList<Movie>.CreateAsync(movies.AsNoTracking(), pageNumber ??
           1, pageSize));
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
             .Include(s => s.Orders)
             .ThenInclude(e => e.Customer)
             .AsNoTracking()
             .FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Director,Price")] Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Movies.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Movie>(
            studentToUpdate,
            "",
            s => s.Director, s => s.Title, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}
