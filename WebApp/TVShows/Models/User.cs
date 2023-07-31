using Microsoft.AspNetCore.Identity;

namespace TVShows.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime dateOfRegister { get; set; }
    }
}
