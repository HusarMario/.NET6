using Microsoft.AspNetCore.Identity;

namespace RunApp.Models
{
    public class AppUser 
    {
        public Address Address { get; set; }
        public int Pace { get; set; }
        public int Mileage { get; set; }
    }
}
