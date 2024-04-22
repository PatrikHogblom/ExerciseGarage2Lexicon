using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExerciseGarage2Lexicon.Data;
using ExerciseGarage2Lexicon.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ExerciseGarage2Lexicon.Models.ViewModels;


namespace ExerciseGarage2Lexicon.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly ExerciseGarage2LexiconContext _context;

        public ParkedVehiclesController(ExerciseGarage2LexiconContext context)
        {
            _context = context;
        }

        //filter/search a regnumber
        [HttpGet]
        public async Task<IActionResult> SearchRegNumber(string regNumber)
        {
            //filter out vehicles according to the reg number
            var model = string.IsNullOrWhiteSpace(regNumber) ? _context.ParkedVehicle :
                        _context.ParkedVehicle.Where(currVal => currVal.RegistrationNumber.Contains(regNumber.ToUpper().Trim()));

            if (!model.Any())
            {
                ViewBag.NoVehiclesFound = "No vehicles found with the specified registration number.";
            }

            return View(nameof(Index), await model.ToListAsync());
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
            try {
                if (ModelState.IsValid)
                {
                    parkedVehicle.RegistrationNumber = parkedVehicle.RegistrationNumber.ToUpper();
                    _context.Add(parkedVehicle);
                    await _context.SaveChangesAsync();
                
                    SetFeedback("success", "Your vehicle was parked!");
                    return View();
                }
                return View(parkedVehicle);
            } 
            catch (DbUpdateException) 
            {
                SetFeedback("danger", "A vehicle with that registration is already parked in the garage");
                return View();
            }
        }

        // HELPER METHODS TO BE REMOVED TO OTHER FILE??
        private FeedbackViewModel GetFeedback(string messageType, string message)
        {
            // Prepare a feedback message
            var feedbackModel = new FeedbackViewModel
            {
                Message = message,
                MessageType = messageType
            };
            return feedbackModel;
        }
        private void SetFeedback(string messageType, string message)
        {
            ViewBag.Message = message;
            ViewBag.MessageType = messageType;
        }
        ////////////////

        // GET: Summary
        public async Task<IActionResult> Summary()
        {
            var vehicles = _context.ParkedVehicle.Select(vehicle => new SummaryViewModel
            {
                VehicleType = vehicle.VehicleType,
                RegistrationNumber = vehicle.RegistrationNumber,
                ArrivalTime = vehicle.ArrivalTime,
                TotalParkingTime = (DateTime.Now - vehicle.ArrivalTime).TotalMinutes
        });

            return View(await vehicles.ToListAsync());
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
                await _context.SaveChangesAsync();

                // Lägg till ett meddelande i TempData för att visa på nästa vy
                TempData["Message"] = "Vehicle successfully unparked.";
            }
            else
            {
                // Lägg till felmeddelande i TempData om fordonet inte hittades
                TempData["Error"] = "Failed to unpark vehicle.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }

        // Adding Kvitto Generation Logic
        public async Task<IActionResult> CheckoutAndGenerateReceipt(int? id)
        {
            if (id == null) return NotFound();

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null) return NotFound();

            var checkOutTime = DateTime.Now;
            var totalParkingMinutes = Math.Round((checkOutTime - parkedVehicle.ArrivalTime).TotalMinutes, 2);
            var price = Math.Round(totalParkingMinutes, 2); // Assuming 1kr per minute

            var viewModel = new KvittoViewModel
            {
                Id = parkedVehicle.Id, // Tilldela id här
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                CheckInTime = parkedVehicle.ArrivalTime,
                CheckOutTime = checkOutTime,
                TotalParkingTimeInMinutes = totalParkingMinutes,
                Price = price
            };

            // Delete the parked vehicle after generating the receipt
            await DeleteConfirmed(id.Value);


            return View("Kvitto", viewModel); // Directing to the "Kvitto" view with the ViewModel
        }


        // Delete with regnummer
        //[HttpPost]
        /*[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteByRegistrationNumber(string registrationNumber)
        {
            var parkedVehicle = await _context.ParkedVehicle.FirstOrDefaultAsync(v => v.RegistrationNumber == registrationNumber);
            if (parkedVehicle == null) return NotFound();

            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

    }
}
