using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaTonno2.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Naam { get; set; }
        public Decimal Price { get; set; }
        public virtual List<Pizza> Pizzas { get; set; }

    }
}