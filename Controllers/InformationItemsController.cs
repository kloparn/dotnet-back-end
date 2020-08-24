using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationItemsController : ControllerBase
    {
        private readonly InformationContext _context;

        public InformationItemsController(InformationContext context)
        {
            _context = context;
            _context.Add(AboutInformation.Frontend(_context));
            _context.Add(AboutInformation.User(_context));
            _context.Add(AboutInformation.Backend(_context));
            _context.Add(AboutInformation.VueFrontend(_context));
            _context.SaveChangesAsync();
        }

        // GET: api/InformationItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformationItem>>> GetInformationItems()
        {
            return await _context.InformationItems.ToListAsync();
        }

        // GET: api/InformationItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationItem>> GetInformationItem(long id)
        {
            var informationItem = await _context.InformationItems.FindAsync(id);

            if (informationItem == null)
            {
                return NotFound();
            }

            return informationItem;
        }

        // PUT: api/InformationItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformationItem(long id, InformationItem informationItem)
        {
            if (id != informationItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(informationItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InformationItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<InformationItem>> PostInformationItem(InformationItem informationItem)
        {
            _context.InformationItems.Add(informationItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInformationItem), new { id = informationItem.Id }, informationItem);
        }

        // DELETE: api/InformationItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InformationItem>> DeleteInformationItem(long id)
        {
            var informationItem = await _context.InformationItems.FindAsync(id);
            if (informationItem == null)
            {
                return NotFound();
            }

            _context.InformationItems.Remove(informationItem);
            await _context.SaveChangesAsync();

            return informationItem;
        }

        private bool InformationItemExists(long id)
        {
            return _context.InformationItems.Any(e => e.Id == id);
        }
    }
}
