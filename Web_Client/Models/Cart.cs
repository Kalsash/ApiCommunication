namespace Web_Client.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Id_u { get; set; }
        public List<Dish> ShoppingCart { get; set; } = new();
    }
}
