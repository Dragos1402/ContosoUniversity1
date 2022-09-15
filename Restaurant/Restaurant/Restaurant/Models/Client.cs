using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace Restaurant.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public int PhoneNumber { get; set; }
       public ICollection<Menu> Menus { get; set; }

    }
}
