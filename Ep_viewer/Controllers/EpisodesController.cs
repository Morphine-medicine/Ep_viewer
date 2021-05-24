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
    public class EpisodesController : Controller
    {
        private readonly DBLab1Context _context;

        public EpisodesController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Episodes
        public async Task<IActionResult> IndexDoctors(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Doctors", "Index");
                ViewBag.DoctorsId = id;
                ViewBag.DoctorsName = name;
                var doctorEpisodes = _context.Episodes.Where(e => e.DoctorId == id).Include(e => e.Doctor).Include(e => e.Patient).Include(e => e.EpisodeDefinitionNavigation);
                return View("IndexDoctors", await doctorEpisodes.ToListAsync());
            
        }
        public async Task<IActionResult> IndexPatients(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Patients", "Index");
            ViewBag.PatientsId = id;
            ViewBag.PatientsName = name;
            var PatientEpisodes = _context.Episodes.Where(e => e.PatientId == id).Include(e => e.Doctor).Include(e => e.Patient).Include(e => e.EpisodeDefinitionNavigation);
            return View("IndexPatients", await PatientEpisodes.ToListAsync());

        }
        public async Task<IActionResult> IndexDefinitions(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Definitions", "Index");
            ViewBag.DefinitionsId = id;
            ViewBag.DefinitionsName = name;
            var definitionEpisodes = _context.Episodes.Where(e => e.EpisodeDefinition == id).Include(e => e.Doctor).Include(e => e.Patient).Include(e => e.EpisodeDefinitionNavigation);
            return View("IndexDefinitions", await definitionEpisodes.ToListAsync());

        }

        public async Task<IActionResult> IndexCommon()
        {
            var dBLab1Context = _context.Episodes.Include(e => e.Doctor).Include(e => e.EpisodeDefinitionNavigation).Include(e => e.Patient);
            return View("Index", await dBLab1Context.ToListAsync());
        }


        // GET: Episodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .Include(e => e.Doctor)
                .Include(e => e.EpisodeDefinitionNavigation)
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // GET: Episodes/Create
        public IActionResult CreateCommon()
        {
            
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name");
            
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name");
            
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition");
            return View();

        }

        // POST: Episodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCommon([Bind("Id,PatientId,DoctorId,Date,Payment,Info,SecondVisit,Status,EpisodeDefinition")] Episode episode)
        {
            episode.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(episode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", episode.DoctorId);
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition", episode.EpisodeDefinition);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", episode.PatientId);
            return View(episode);
        }

        public IActionResult CreateDoctors(int? id, string? name)
        {
            ViewBag.DoctorsId = id;
            ViewBag.DoctorsName = name;
            ViewData["DoctorId"] = new SelectList(new[] { new { id = id, name = name } }, "id", "name");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name");
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition");
            return View();
        }

        // POST: Episodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDoctors([Bind("Id,PatientId,DoctorId,Date,Payment,Info,SecondVisit,Status,EpisodeDefinition")] Episode episode)
        {
            episode.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(episode);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexDoctors", "Episodes", new { id = episode.DoctorId, name = _context.Doctors.Where(c => c.Id == episode.DoctorId).FirstOrDefault().Name });
            }
            ViewData["DoctorId"] = new SelectList(new[] { new { id = episode.DoctorId, name = _context.Doctors.Where(c => c.Id == episode.DoctorId).FirstOrDefault().Name } }, "id", "name");
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition", episode.EpisodeDefinition);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", episode.PatientId);
            return View(episode);
        }

        public IActionResult CreatePatients(int? id, string? name)
        {
            ViewBag.PatientsId = id;
            ViewBag.PatientsName = name;
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name");
            ViewData["PatientId"] = new SelectList(new[] { new { id = id, name = name } }, "id", "name");
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition");

            return View();
        }

        // POST: Episodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePatients([Bind("Id,PatientId,DoctorId,Date,Payment,Info,SecondVisit,Status,EpisodeDefinition")] Episode episode)
        {
            episode.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(episode);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexPatients", "Episodes", new { id = episode.PatientId, name = _context.Patients.Where(c => c.Id == episode.PatientId).FirstOrDefault().Name });
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", episode.DoctorId);
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition", episode.EpisodeDefinition);
            ViewData["PatientId"] = new SelectList(new[] { new { id = episode.PatientId, name = _context.Patients.Where(c => c.Id == episode.PatientId).FirstOrDefault().Name } }, "id", "name");
            return View(episode);
        }

        public IActionResult CreateDefinitions(int? id, string? name)
        {
            ViewBag.DefinitionsId = id;
            ViewBag.DefinitionsName = name;
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name");
            ViewData["EpisodeDefinition"] = new SelectList(new[] { new { id = id, name = name } }, "id", "name");
            return View();
        }

        // POST: Episodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDefinitions([Bind("Id,PatientId,DoctorId,Date,Payment,Info,SecondVisit,Status,EpisodeDefinition")] Episode episode)
        {
            episode.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(episode);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexDefinitions", "Episodes", new { id = episode.EpisodeDefinition, name = _context.EpisodeDefinitions.Where(c => c.Id == episode.EpisodeDefinition).FirstOrDefault().Definition });
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", episode.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", episode.PatientId);
            ViewData["EpisodeDefinition"] = new SelectList(new[] { new { id = episode.EpisodeDefinition, name = _context.EpisodeDefinitions.Where(c => c.Id == episode.EpisodeDefinition).FirstOrDefault().Definition } }, "id", "name");
            return View(episode);
        }

        // GET: Episodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", episode.DoctorId);
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition", episode.EpisodeDefinition);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", episode.PatientId);
            return View(episode);
        }

        // POST: Episodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,DoctorId,Date,Payment,Info,SecondVisit,Status,EpisodeDefinition")] Episode episode)
        {
            if (id != episode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodeExists(episode.Id))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", episode.DoctorId);
            ViewData["EpisodeDefinition"] = new SelectList(_context.EpisodeDefinitions, "Id", "Definition", episode.EpisodeDefinition);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", episode.PatientId);
            return View(episode);
        }

        // GET: Episodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .Include(e => e.Doctor)
                .Include(e => e.EpisodeDefinitionNavigation)
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // POST: Episodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);
            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodeExists(int id)
        {
            return _context.Episodes.Any(e => e.Id == id);
        }
    }
}
