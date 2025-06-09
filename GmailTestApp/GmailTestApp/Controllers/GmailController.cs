using GmailTestApp.Context;
using GmailTestApp.DTO;
using GmailTestApp.Model;
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
        public async Task<IActionResult> GetGmails()
        {
            var gmails = await _context.Gmails
            .Include(e => e.GmailLabelMaps)
            .ThenInclude(m => m.Label)
            .ToListAsync();

            return Ok(gmails.Select(e => new {
                e.Id,
                e.Subject,
                e.Sender,
                e.Date,
                Labels = e.GmailLabelMaps.Select(lm => new {
                    lm.Label.Id,
                    lm.Label.LableName,
                    lm.Label.ColorCode
                })
            }));
        }

        [HttpPost("add-label")]
        public async Task<IActionResult> AddLabel([FromBody] AddGmailLableMapDTO map)
        {
            // Validate existence (optional but safe)
            var emailExists = await _context.Gmails.AnyAsync(e => e.Id == map.GmailId);
            var labelExists = await _context.Labels.AnyAsync(l => l.Id == map.LabelId);

            if (!emailExists || !labelExists)
                return NotFound("Email or Label does not exist.");

            var alreadyExists = await _context.GmailLabelMap
                .AnyAsync(m => m.GmailId == map.GmailId && m.LabelId == map.LabelId);

            if (alreadyExists)
                return BadRequest("Label already assigned to this email.");

            var newMap = new GmailLabelMap
            {
                GmailId = map.GmailId,
                LabelId = map.LabelId
            };

            _context.GmailLabelMap.Add(newMap);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("{gmailId}/remove-label/{labelId}")]
        public async Task<IActionResult> RemoveLabel(int gmailId, int labelId)
        {
            var item = await _context.GmailLabelMap
                .FirstOrDefaultAsync(m => m.GmailId == gmailId && m.LabelId == labelId);

            if (item != null)
            {
                _context.GmailLabelMap.Remove(item);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("bulk-add-label")]
        public async Task<IActionResult> BulkAddLabel([FromBody] BulkLabelDto dto)
        {
            foreach (var gmailId in dto.GmailIds)
            {
                bool exists = await _context.GmailLabelMap
                    .AnyAsync(x => x.GmailId == gmailId && x.LabelId == dto.LabelId);
                if (!exists)
                {
                    _context.GmailLabelMap.Add(new GmailLabelMap { GmailId = gmailId, LabelId = dto.LabelId });
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("bulk-remove-label")]
        public async Task<IActionResult> RemoveLabel([FromBody] BulkLabelDto request)
        {
            var mappings = _context.GmailLabelMap
                .Where(m => request.GmailIds.Contains(m.GmailId) && m.LabelId == request.LabelId);

            _context.GmailLabelMap.RemoveRange(mappings);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Gmail gmail)
        {
            _context.Gmails.Add(gmail);
            await _context.SaveChangesAsync();
            return Ok(gmail);
        }
    }
}
