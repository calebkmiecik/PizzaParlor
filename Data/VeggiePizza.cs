using System.Drawing;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the VeggiePizza class
    /// </summary>
    public class VeggiePizza : Pizza
    { 
        /// <summary>
        /// The name of the VeggiePizza instance
        /// </summary>
        public override string Name { get; } = "Veggie Pizza";

        /// <summary>
        /// The description of the VeggiePizza instance
        /// </summary>
        public override string Description => "All the veggies";
        /// <summary>
        /// The price of this VeggiePizza instance
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
        /// The number of calories in each slice of this VeggiePizza instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                uint cals = 250;
                if (PizzaCrust == Crust.Thin) { cals = 150; }
                if (PizzaCrust == Crust.DeepDish) { cals = 300; }

                if (this.ContainsTopping(Topping.Olives)) cals += 5;
                if (this.ContainsTopping(Topping.Peppers)) cals += 5;
                if (this.ContainsTopping(Topping.Onions)) cals += 5;
                if (this.ContainsTopping(Topping.Mushrooms)) cals += 5;

                if (PizzaSize == Size.Small) { cals = (cals * 3) / 4; }
                if (PizzaSize == Size.Large) { cals = (cals * 13) / 10; }
                return cals;
            }
        }
        /// <summary>
        /// Constructs a new VeggiePizza
        /// </summary>
        public VeggiePizza()
        {
            this.PossibleToppings.Clear();
            this.PossibleToppings.Add(new PizzaTopping(Topping.Olives, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Onions, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Peppers, true));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Mushrooms, true));
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
                if (!ContainsTopping(Topping.Olives))
                {
                    instructions.Add("Hold Olives");
                }
                if (!ContainsTopping(Topping.Peppers))
                {
                    instructions.Add("Hold Peppers");
                }
                if (!ContainsTopping(Topping.Onions))
                {
                    instructions.Add("Hold Onions");
                }
                if (!ContainsTopping(Topping.Mushrooms))
                {
                    instructions.Add("Hold Mushrooms");
                }
                return instructions;
            }
        }
    }
}