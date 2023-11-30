using System.Drawing;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the MeatsPizza class
    /// </summary>
    public class MeatsPizza : Pizza
    {
        /// <summary>
        /// The name of the MeatsPizza instance
        /// </summary>
        public override string Name { get; } = "Meats Pizza";

        /// <summary>
        /// The description of the MeatsPizza instance
        /// </summary>
        public override string Description => "All the meats";
        /// <summary>
        /// The price of this MeatsPizza instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 13.99m;
                if (PizzaSize == Size.Small) { price -= 2.00m; }
                if (PizzaSize == Size.Large) { price += 2.00m; }
                if (PizzaCrust == Crust.DeepDish) { price += 1.00m; }
                return price;
            }
        }
        /// <summary>
        /// The number of calories in each slice of this MeatsPizza instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                uint cals = 250;
                if (PizzaCrust == Crust.Thin) { cals = 150; }
                if (PizzaCrust == Crust.DeepDish) { cals = 300; }

                if (this.ContainsTopping(Topping.Sausage)) cals += 30;
                if (this.ContainsTopping(Topping.Pepperoni)) cals += 20;
                if (this.ContainsTopping(Topping.Ham)) cals += 20;
                if (this.ContainsTopping(Topping.Bacon)) cals += 20;

                if (PizzaSize == Size.Small) { cals = (cals * 3) / 4; }
                if (PizzaSize == Size.Large) { cals = (cals * 13) / 10; }
                return cals;
            }
        }
        /// <summary>
        /// Constructor for a MeatsPizza
        /// </summary>
        public MeatsPizza()
        {
            this.PossibleToppings.Clear();
            this.PossibleToppings.Add(new PizzaTopping(Topping.Sausage, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Pepperoni, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Ham, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Bacon, true));
        }

        /// <summary>
        /// Special instructions for the preparation of this FlyingSaucer
        /// </summary>
        public override IEnumerable<string> SpecialInstructions
        {
            get
            {
                List<string> instructions = new();

                instructions.Add(PizzaSize.ToString());
                instructions.Add(PizzaCrust.ToString());
                if (!ContainsTopping(Topping.Sausage))
                {
                    instructions.Add("Hold Sausage");
                }
                if (!ContainsTopping(Topping.Pepperoni))
                {
                    instructions.Add("Hold Pepperoni");
                }
                if (!ContainsTopping(Topping.Ham))
                {
                    instructions.Add("Hold Ham");
                }
                if (!ContainsTopping(Topping.Bacon))
                {
                    instructions.Add("Hold Bacon");
                }
                return instructions;
            }
        }
    }
}