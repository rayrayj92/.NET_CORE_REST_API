using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly CmsShoppingCartContext _context;
        public PagesController(CmsShoppingCartContext context)
        {
            _context = context;
        }

        //GET /api/pages
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            return await _context.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }

        //GET /api/pages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPages(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            
            if(page == null)
            {
                return NotFound();
            }

            return page;
        }

        //PUT /api/pages/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Page>> PutPages(int id, Page page)
        {
            if(id != page.Id)
            {
                return BadRequest();
            }
            _context.Entry(page).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //POST /api/pages
        [HttpPost]
        public async Task<ActionResult<Page>> PostPage(Page page)
        {
            _context.Pages.Add(page);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPage), page);
        }

        //DELETE /api/pages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Page>> DeletePage(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            _context.Pages.Remove(page); 

            await _context.SaveChangesAsync();

            return NoContent(); // 2xxx or 4xx HTTP Request
        }
    }
}