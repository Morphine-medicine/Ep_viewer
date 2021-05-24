using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ep_viewer;

namespace Ep_viewer.Controllers
{
    public class EpisodeDefinitionsController : Controller
    {
        private readonly DBLab1Context _context;

        public EpisodeDefinitionsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: EpisodeDefinitions
        public async Task<IActionResult> Index()
        {
            return View(await _context.EpisodeDefinitions.ToListAsync());
        }

        // GET: EpisodeDefinitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeDefinition = await _context.EpisodeDefinitions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodeDefinition == null)
            {
                return NotFound();
            }

            return RedirectToAction("IndexDefinitions", "Episodes", new { id = episodeDefinition.Id, name = episodeDefinition.Definition });
        }

        // GET: EpisodeDefinitions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EpisodeDefinitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Definition,Cost")] EpisodeDefinition episodeDefinition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(episodeDefinition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(episodeDefinition);
        }

        // GET: EpisodeDefinitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeDefinition = await _context.EpisodeDefinitions.FindAsync(id);
            if (episodeDefinition == null)
            {
                return NotFound();
            }
            return View(episodeDefinition);
        }

        // POST: EpisodeDefinitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Definition,Cost")] EpisodeDefinition episodeDefinition)
        {
            if (id != episodeDefinition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodeDefinition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodeDefinitionExists(episodeDefinition.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(episodeDefinition);
        }

        // GET: EpisodeDefinitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeDefinition = await _context.EpisodeDefinitions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodeDefinition == null)
            {
                return NotFound();
            }

            return View(episodeDefinition);
        }

        // POST: EpisodeDefinitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episodeDefinition = await _context.EpisodeDefinitions.FindAsync(id);
            _context.EpisodeDefinitions.Remove(episodeDefinition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodeDefinitionExists(int id)
        {
            return _context.EpisodeDefinitions.Any(e => e.Id == id);
        }
    }
}
