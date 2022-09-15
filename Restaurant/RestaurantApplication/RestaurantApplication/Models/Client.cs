using System.ComponentModel.DataAnnotations;

namespace RestaurantApplication.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDay { get; set; }
        public int PhoneNumber { get; set; }
        ICollection<Menu> Menus { get; set; }

    }
}
