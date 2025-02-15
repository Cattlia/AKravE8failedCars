using AKrav8Api.Data;
using AKrav8Api.Models;
using AKrav8Api.Controllers;
using System.ComponentModel.DataAnnotations;


namespace AKrav8Api.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string ? Type { get; set; }

        [Required]
        public string ? Color { get; set; }
        public string ? WindowType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

