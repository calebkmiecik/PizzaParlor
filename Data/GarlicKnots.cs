using System.Drawing;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the GarlicKnots class
    /// </summary>
    public class GarlicKnots : Side
    {
        /// <summary>
        /// The name of the GarlicKnots instance
        /// </summary>
        public override string Name { get; } = "Garlic Knots";

        /// <summary>
        /// The description of the GarlicKnots instance
        /// </summary>
        public override string Description => "Twisted rolls with garlic and butter";

        /// <summary>
        /// The price of this GarlicKnots instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                return 0.75m*SideCount;
            }
        }

        /// <summary>
        /// The number of calories in each slice of this GarlicKnots instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                return 175;
            }
        }

        /// <summary>
        /// Special instructions for the preparation of this FlyingSaucer
        /// </summary>
        public override IEnumerable<string> SpecialInstructions
        {
            get
            {
                List<string> instructions = new();

                instructions.Add(SideCount.ToString() + " Garlic Knots");
                return instructions;
            }
        }
    }
}