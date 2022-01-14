using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mirza_Lorena_practic.Data;
using Mirza_Lorena_practic.Models;

using Mirza_Lorena_practic.Models.MovieShopViewModels;

namespace Mirza_Lorena_practic.Controllers
{
    public class PublishersController : Controller
    {
        private readonly MovieShopContext _context;

        public PublishersController(MovieShopContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index(int? id, int? bookID)
        {
            var viewModel = new PublisherIndexData();
            viewModel.Publishers = await _context.Publishers
            .Include(i => i.PublishesMovies)
            .ThenInclude(i => i.Movie)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.PublisherName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["PublisherID"] = id.Value;
                Publisher publisher = viewModel.Publishers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Movies = publisher.PublishesMovies.Select(s => s.Movie);
            }
            if (bookID != null)
            {
                ViewData["BoookID"] = bookID.Value;
                viewModel.Orders = viewModel.Movies.Where(
                x => x.ID == bookID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PublisherName,Adress")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var publisher = await _context.Publishers
            .Include(i => i.PublishesMovies).ThenInclude(i => i.Movie)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }
            PopulatePublishedMovieData(publisher);
            return View(publisher);
        }

        private void PopulatePublishedMovieData(Publisher publisher)
        {
            var allMovies = _context.Movies;
            var publisherMovies = new HashSet<int>(publisher.PublishesMovies.Select(c => c.MovieID));
            var viewModel = new List<PublishedMovieData>();
            foreach (var movie in allMovies)
            {
                viewModel.Add(new PublishedMovieData
                {
                    MovieID = movie.ID,
                    Title = movie.Title,
                    IsPublished = publisherMovies.Contains(movie.ID)
                });
            }
            ViewData["Books"] = viewModel;
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedMovies)
        {
            if (id == null)
            {
                return NotFound();
            }
            var publisherToUpdate = await _context.Publishers
            .Include(i => i.PublishesMovies)
            .ThenInclude(i => i.Movie)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Publisher>(
            publisherToUpdate,
            "",
            i => i.PublisherName, i => i.Adress))
            {
                UpdatePublishedBooks(selectedMovies, publisherToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePublishedBooks(selectedMovies, publisherToUpdate);
            PopulatePublishedMovieData(publisherToUpdate);
            return View(publisherToUpdate);
        }
        private void UpdatePublishedBooks(string[] selectedBooks, Publisher publisherToUpdate)
        {
            if (selectedBooks == null)
            {
                publisherToUpdate.PublishesMovies = new List<PublishedMovie>();
                return;
            }
            var selectedBooksHS = new HashSet<string>(selectedBooks);
            var publishedBooks = new HashSet<int>
            (publisherToUpdate.PublishesMovies.Select(c => c.Movie.ID));
            foreach (var book in _context.Movies)
            {
                if (selectedBooksHS.Contains(book.ID.ToString()))
                {
                    if (!publishedBooks.Contains(book.ID))
                    {
                        publisherToUpdate.PublishesMovies.Add(new PublishedMovie
                        {
                            PublisherID =
                       publisherToUpdate.ID,
                            MovieID = book.ID
                        });
                    }
                }
                else
                {
                    if (publishedBooks.Contains(book.ID))
                    {
                        PublishedMovie bookToRemove = publisherToUpdate.PublishesMovies.FirstOrDefault(i
                       => i.MovieID == book.ID);
                        _context.Remove(bookToRemove);
                    }
                }
            }
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.ID == id);
        }
    }
}
