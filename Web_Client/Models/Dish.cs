﻿namespace Web_Client.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int UserId { get; set; }
    }
}
