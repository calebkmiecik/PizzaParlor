using System.ComponentModel;
using System.Drawing;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the Breadsticks class
    /// </summary>
    public class Breadsticks : Side
    {
        /// <summary>
        /// The name of the Breadsticks instance
        /// </summary>
        public override string Name { get; } = "Breadsticks";

        /// <summary>
        /// The description of the Breadsticks instance
        /// </summary>
        public override string Description => "Soft, buttery breadsticks";

        private bool _cheese = false;

        /// <summary>
        /// Whether this Breadsticks instance contains cheese
        /// </summary>
        public bool Cheese
        {
            get
            {
                return _cheese;
            }
            set
            {
                _cheese = value;
                OnPropertyChanged(nameof(Cheese));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(Price));
            }
        }
        

        /// <summary>
        /// The price of this Breadsticks instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                if (Cheese) { return 1.00m*SideCount; }
                else return 0.75m*SideCount;
            }
        }

        /// <summary>
        /// The number of calories in each slice of this Breadsticks instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                uint cals = 150;

                if (Cheese) cals += 50;

                return cals;
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

                if (Cheese) instructions.Add(SideCount.ToString() + " Cheesesticks");
                if (!Cheese) instructions.Add(SideCount.ToString() + " Breadsticks");
                return instructions;
            }
        }
    }
}