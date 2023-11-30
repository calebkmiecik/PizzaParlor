using System.Drawing;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the Iced Tea class
    /// </summary>
    public class IcedTea : Drink
    {
        /// <summary>
        /// The name of the Iced Tea instance
        /// </summary>
        public override string Name { get; } = "Iced Tea";

        /// <summary>
        /// The description of the Iced Tea instance
        /// </summary>
        public override string Description => "Freshly brewed sweet tea";

        /// <summary>
        /// The price of this Iced Tea instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                if (DrinkSize == Size.Small) { return 2.0m; }
                if (DrinkSize == Size.Medium) { return 2.5m; }
                else { return 3.0m; }

            }
        }

        /// <summary>
        /// The number of calories in this Iced Tea instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                if (DrinkSize == Size.Small) { return 175; }
                if (DrinkSize == Size.Medium) { return 220; }
                return 275;
              
            }
        }

        /// <summary>
        /// The number of calories in this Iced Tea instance
        /// </summary>
        public override uint CaloriesTotal
        {
            get
            {
                if (DrinkSize == Size.Small) { return 175; }
                if (DrinkSize == Size.Medium) { return 220; }
                return 275;

            }
        }

        /// <summary>
        /// Special instructions for the preparation of this Iced Tea
        /// </summary>
        public override IEnumerable<string> SpecialInstructions
        {
            get
            {
                List<string> instructions = new();
                instructions.Add(DrinkSize.ToString());
                if (!Ice) { instructions.Add("Hold ice"); }
                return instructions;
            }
        }
    }
}