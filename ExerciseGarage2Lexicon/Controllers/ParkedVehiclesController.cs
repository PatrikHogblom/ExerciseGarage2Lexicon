using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExerciseGarage2Lexicon.Data;
using ExerciseGarage2Lexicon.Models;

namespace ExerciseGarage2Lexicon.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly ExerciseGarage2LexiconContext _context;

        public ParkedVehiclesController(ExerciseGarage2LexiconContext context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicle.ToListAsync());
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound("No vehicle with that id found");
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegistrationNumber,Color,Brand,Model,NumberOfWheels,ArrivalTime")] ParkedVehicle parkedVehicle)
        {
            //ViewBag.Error = null;
            FeedbackViewModel feedbackModel;

            try {
                if (ModelState.IsValid)
                {
                    _context.Add(parkedVehicle);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));

                    //feedbackModel = new FeedbackViewModel
                    //{
                    //    Message = "Your vehicle was parked!",
                    //    MessageType = "success"
                    //};
                    feedbackModel = GetFeedback("success", "Your vehicle was parked!");

                    return View("Feedback", feedbackModel);
                }
                return View(parkedVehicle);
            } 
            catch (DbUpdateException ex) 
            {
                //return BadRequest(ex.Message);
                //ViewBag.Error = "A vehicle with that registration is already parked in the garage";
                //return View();

                // If an error occurs, prepare a feedback message
                feedbackModel = new FeedbackViewModel
                {
                    Message = "A vehicle with that registration is already parked in the garage.",
                    MessageType = "danger" // Bootstrap alert type
                };
                // Return the Feedback.cshtml view with the feedback model
                return View("Feedback", feedbackModel); 
            }
        }

        private FeedbackViewModel GetFeedback(string messageType, string message)
        {
            var feedbackModel = new FeedbackViewModel
            {
                Message = message,
                MessageType = messageType
            };
            return feedbackModel;
        }


        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegistrationNumber,Color,Brand,Model,NumberOfWheels,ArrivalTime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
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
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle != null)
            {
                _context.ParkedVehicle.Remove(parkedVehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
    }
}
