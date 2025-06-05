using GmailTestApp.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GmailTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GmailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GmailController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Gmails.ToListAsync());
        }
    }
}
