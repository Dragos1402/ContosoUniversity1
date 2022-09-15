using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Menu
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string PizzaName { get; set; }
        public float Price { get; set; }

        public Client Client { get; set; }
        
    }
    public class ViewMenu
    {
        IEnumerable<Menu> PizzaNameVM { get; set; }
        IEnumerable<Menu> PriceVM { get; set; }
    }
}
