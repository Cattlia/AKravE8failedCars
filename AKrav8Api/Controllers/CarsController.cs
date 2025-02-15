using AKrav8Api.Data;
using AKrav8Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;

namespace AKrav8Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly VehicleContext _context;

        public CarsController(VehicleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Car>> GetCars(
            [FromQuery, SwaggerParameter(Description = "The type of the car. Example: Standard Car, Pickup Truck")] string type,
            [FromQuery, SwaggerParameter(Description = "The color of the car. Example: Red, Blue")] string color,
            [FromQuery, SwaggerParameter(Description = "The window type of the car. Example: Tinted, Clear")] string windowType)
        {
            try
            {
                var query = _context.Cars.AsQueryable();

                if (!string.IsNullOrEmpty(type))
                    query = query.Where(car => car.Type != null && car.Type.ToLower() == type.ToLower());

                if (!string.IsNullOrEmpty(color))
                    query = query.Where(car => car.Color != null && car.Color.ToLower() == color.ToLower());

                if (!string.IsNullOrEmpty(windowType))
                    query = query.Where(car => car.WindowType != null && car.WindowType.ToLower() == windowType.ToLower());

                var result = query.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult AddCar([FromBody] Car car)
        {
            if (car == null)
                return BadRequest("Car object is null.");

            if (string.IsNullOrWhiteSpace(car.Type) || string.IsNullOrWhiteSpace(car.Color) || string.IsNullOrWhiteSpace(car.WindowType))
                return BadRequest("Type, Color, and WindowType are required.");

            try
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Failed to add car: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCar(int id, [FromBody] Car car)
        {
            if (id != car.Id)
                return BadRequest("Car ID mismatch.");

            if (car == null || string.IsNullOrWhiteSpace(car.Type) || string.IsNullOrWhiteSpace(car.Color) || string.IsNullOrWhiteSpace(car.WindowType))
                return BadRequest("Invalid car data.");

            var existingCar = _context.Cars.Find(id);
            if (existingCar == null)
            {
                return NotFound("Car not found.");
            }

            existingCar.Type = car.Type;
            existingCar.Color = car.Color;
            existingCar.WindowType = car.WindowType;

            try
            {
                _context.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Failed to update car: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound("Car not found.");
            }

            try
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Failed to delete car: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
