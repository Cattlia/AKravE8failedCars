using Microsoft.EntityFrameworkCore;
using AKrav8Api.Data;
using AKrav8Api.Models;
using AKrav8Api.Controllers;

namespace AKrav8Api.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public void TestConnection()
{
    try
    {
        this.Database.CanConnect();
        Console.WriteLine("Database connection successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}
    }
}


