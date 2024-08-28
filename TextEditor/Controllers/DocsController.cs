using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TextEditor.Data;
using TextEditor.Models;

namespace TextEditor.Controllers
{
    // Ensure that the user is authenticated before accessing any actions in this controller
    [Authorize]
    public class DocsController : Controller
    {
        // Database context for interacting with the database
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the context
        public DocsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Docs - Retrieves a list of documents for the currently logged-in user
        public async Task<IActionResult> Index()
        {
            // Filter documents by the UserId of the currently logged-in user
            var applicationDbContext = _context.Docs
                .Where(d => d.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Return the view with the filtered list of documents
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Docs/Create - Displays the form to create a new document
        public IActionResult Create()
        {
            return View();
        }

        // POST: Docs/Create - Handles the submission of the new document form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,UserId")] Doc doc)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Set the UserId to the currently logged-in user's ID
                doc.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // Add the new document to the database
                _context.Add(doc);
                await _context.SaveChangesAsync();
                // Redirect to the index page after saving
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, return the view with the current document data
            return View(doc);
        }

        // GET: Docs/Edit/5 - Displays the form to edit an existing document
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is provided
            if (id == null)
            {
                return NotFound();
            }

            // Find the document by id
            var doc = await _context.Docs.FindAsync(id);
            if (doc == null)
            {
                return NotFound();
            }

            // Ensure that the document belongs to the currently logged-in user
            if (doc.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }

            // Return the view with the document data
            return View(doc);
        }

        // POST: Docs/Edit/5 - Handles the submission of the edited document form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,UserId")] Doc doc)
        {
            // Check if the id in the request matches the document's id
            if (id != doc.Id)
            {
                return NotFound();
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the document in the database
                    _context.Update(doc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the document still exists
                    if (!DocExists(doc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirect to the index page after saving
                return RedirectToAction(nameof(Index));
            }
            // Populate the UserId dropdown list and return the view with the current document data
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", doc.UserId);
            return View(doc);
        }

        // GET: Docs/Delete/5 - Displays the confirmation page to delete a document
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is provided
            if (id == null)
            {
                return NotFound();
            }

            // Find the document by id and include the User details
            var doc = await _context.Docs
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doc == null)
            {
                return NotFound();
            }

            // Ensure that the document belongs to the currently logged-in user
            if (doc.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }

            // Return the view with the document data
            return View(doc);
        }

        // POST: Docs/Delete/5 - Handles the submission to delete a document
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the document by id
            var doc = await _context.Docs.FindAsync(id);
            if (doc != null)
            {
                // Remove the document from the database
                _context.Docs.Remove(doc);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
            // Redirect to the index page after deletion
            return RedirectToAction(nameof(Index));
        }

        // Checks if a document with the given id exists
        private bool DocExists(int id)
        {
            return _context.Docs.Any(e => e.Id == id);
        }
    }
}
