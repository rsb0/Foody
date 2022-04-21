#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foody.Models;

namespace Foody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly FoodContext _context;

        public FoodItemsController(FoodContext context)
        {
            _context = context;
        }

        // GET: api/FoodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItemDTO>>> GetFoodItem()
        {
            return await _context.FoodItems.
            Select(x => ItemToDTO(x)).
            ToListAsync();
        }

        // GET: api/FoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemDTO>> GetFoodItem(long id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(foodItem);
        }

        // PUT: api/FoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(long id, FoodItemDTO foodItemDTO)
        {
            if (id != foodItemDTO.Id)
            {
                return BadRequest();
            }

            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            foodItem.Name = foodItemDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!FoodItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/FoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodItemDTO>> PostFoodItem(FoodItemDTO foodItemDTO)
        {
            var foodItem = new FoodItem
            {
                Id = foodItemDTO.Id,
                Name = foodItemDTO.Name,
                Brand = foodItemDTO.Brand,
                Calories = foodItemDTO.Calories,
                Fat = foodItemDTO.Fat,
                Carbs = foodItemDTO.Carbs,
                Protein = foodItemDTO.Protein
            };
            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetFoodItem),
                new { id = foodItem.Id },
                ItemToDTO(foodItem));
        }

        // DELETE: api/FoodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodItem(long id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(long id)
        {
            return _context.FoodItems.Any(e => e.Id == id);
        }
        public static FoodItemDTO ItemToDTO(FoodItem foodItem) =>
            new FoodItemDTO
            {
                Id = foodItem.Id,
                Name = foodItem.Name,
                Brand = foodItem.Brand,
                Calories = foodItem.Calories,
                Fat = foodItem.Fat,
                Carbs = foodItem.Carbs,
                Protein = foodItem.Protein,
            };
    }
}
