using System.Security.Claims;
using BidwotaGiri_Dot_Net_Assignment.Data;
using BidwotaGiri_Dot_Net_Assignment.Models;
using BidwotaGiri_Dot_Net_Assignment.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidwotaGiri_Dot_Net_Assignment.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EventController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel viewModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case where the user is not logged in (if needed)
                return Unauthorized(); // Or any other error handling
            }
            var events = new EventsList
            {
                EventName = viewModel.EventName,
                Description = viewModel.Description,
                Location = viewModel.Location,
                StartDate = viewModel.StartDate,
            };
            await dbContext.EventList.AddAsync(events);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Event");

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List(string searchTerm, string sortOrder, int pageNumber = 1, int pageSize = 5)
        {

            var query = dbContext.EventList.AsQueryable();

            // Searching
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(e => e.EventName.Contains(searchTerm) ||
                                         e.Description.Contains(searchTerm) ||
                                         e.Location.Contains(searchTerm));
            }

            // Sorting
            ViewBag.NameSortParam = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.DateSortParam = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            query = sortOrder switch
            {
                "name_asc" => query.OrderBy(e => e.EventName),
                "name_desc" => query.OrderByDescending(e => e.EventName),
                "date_asc" => query.OrderBy(e => e.StartDate),
                "date_desc" => query.OrderByDescending(e => e.StartDate),
                _ => query.OrderBy(e => e.StartDate) // Default sorting by date ascending
            };

            // Pagination
            var totalEvents = await query.CountAsync();
            var events = await query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize);
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SortOrder = sortOrder;

            return View(events);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var events = await dbContext.EventList.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }

            var viewModel = new AddEventViewModel
            {
                Id = events.Id,
                EventName = events.EventName,
                Description = events.Description,
                Location = events.Location,
                StartDate = events.StartDate
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); 
            }

            var events = await dbContext.EventList.FindAsync(viewModel.Id);
            if (events == null)
            {
                return NotFound();
            }
            events.EventName = viewModel.EventName;
            events.Description = viewModel.Description;
            events.Location = viewModel.Location;
            events.StartDate = viewModel.StartDate;

            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Event");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EventsList viewModel)
        {
            var events = await dbContext.EventList.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == viewModel.Id);
            if(events is not null)
            {
                dbContext.EventList.Remove( viewModel);
                await dbContext.SaveChangesAsync(); 
            }
            return RedirectToAction("List", "Event"); 

        }

   
    }
}

//---------------------- code with authorization-------------------------------------
