using GmailTestApp.Context;
using GmailTestApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GmailTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LabelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _context.Labels.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Label label)
        {
            _context.Labels.Add(label);
            await _context.SaveChangesAsync();
            return Ok(label);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Label label)
        {
            var existing = await _context.Labels.FindAsync(id);
            if (existing == null) return NotFound();

            existing.LableName = label.LableName;
            existing.ColorCode = label.ColorCode;
            existing.Description = label.Description;
            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null) return NotFound();

            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
