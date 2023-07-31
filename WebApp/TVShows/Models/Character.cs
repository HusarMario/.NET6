using System.ComponentModel.DataAnnotations;

namespace TVShows.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public bool Active { get; set; }
    }
}
