using MenuApi.Models;
using MenuApiData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MenuController : ControllerBase
	{
		private readonly MenuContext _context;

		public MenuController(MenuContext context)
		{
			_context = context;
		}

		// GET: api/menu
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
		{
			return await _context.MenuList.ToListAsync();
		}

		// GET: api/menu/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Menu>> GetDish(int id)
		{
			var dish = await _context.MenuList.FindAsync(id);

			if (dish == null)
			{
				return NotFound();
			}

			return dish;
		}

		// POST api/menu
		[HttpPost]
		public async Task<ActionResult<Menu>> PostDish(Menu dish)
		{
			_context.MenuList.Add(dish);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetDish), new { id = dish.Id }, dish);
		}

		// PUT api/menu/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutDish(int id, Menu dish)
		{
			if (id != dish.Id)
			{
				return BadRequest();
			}

			_context.Entry(dish).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE api/menu/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDish(int id)
		{
			var dish = await _context.MenuList.FindAsync(id);

			if (dish == null)
			{
				return NotFound();
			}

			_context.MenuList.Remove(dish);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// dummy endpoint to test the database connection
		[HttpGet("test")]
		public string Test()
		{
			return "Hello World!";
		}
	}
}
