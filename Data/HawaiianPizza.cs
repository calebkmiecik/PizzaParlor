using System.Drawing;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the HawaiianPizza class
    /// </summary>
    public class HawaiianPizza : Pizza
    {
        /// <summary>
        /// The name of the HawaiianPizza instance
        /// </summary>
        public override string Name { get; } = "Hawaiian Pizza";

        /// <summary>
        /// The description of the HawaiianPizza instance
        /// </summary>
        public override string Description => "The pizza with pineapple";
        /// <summary>
        /// The price of this HawaiianPizza instance
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
        /// The number of calories in each slice of this HawaiianPizza instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                uint cals = 250;
                if (PizzaCrust == Crust.Thin) { cals = 150; }
                if (PizzaCrust == Crust.DeepDish) { cals = 300; }

                if (this.ContainsTopping(Topping.Pineapple)) cals += 10;
                if (this.ContainsTopping(Topping.Ham)) cals += 20;
                if (this.ContainsTopping(Topping.Onions)) cals += 5;

                if (PizzaSize == Size.Small) { cals = (cals * 3) / 4; }
                if (PizzaSize == Size.Large) { cals = (cals * 13) / 10; }
                return cals;
            }
        }
        /// <summary>
        /// Constructs a new HawaiianPizza
        /// </summary>
        public HawaiianPizza()
        {
            this.PossibleToppings.Clear();
            this.PossibleToppings.Add(new PizzaTopping(Topping.Ham, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Onions, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Pineapple, true));
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
                if (!ContainsTopping(Topping.Pineapple))
                {
                    instructions.Add("Hold Pineapple");
                }
                if (!ContainsTopping(Topping.Ham))
                {
                    instructions.Add("Hold Ham");
                }
                if (!ContainsTopping(Topping.Onions))
                {
                    instructions.Add("Hold Onions");
                }
                return instructions;
            }
        }
    }
}