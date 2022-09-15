
namespace RestaurantApplication.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string PizzaName { get; set; }
        public float Price { get; set; }
        ICollection<Client> Clients { get; set; }
    }
}
