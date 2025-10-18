using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Domain.Entities;
using StudyVera.Infrastructure.Persistence;

namespace StudyVera.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public WeatherForecastController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Seed()
        {
            await SeedData.SeedAsync(_context, _userManager);
            return Ok("Seeding completed.");

        }
    }
}
