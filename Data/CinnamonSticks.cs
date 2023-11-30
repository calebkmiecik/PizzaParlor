using System.Drawing;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the CinnamonSticks class
    /// </summary>
    public class CinnamonSticks : Side
    {
        /// <summary>
        /// The name of the CinnamonSticks instance
        /// </summary>
        public override string Name { get; } = "Cinnamon Sticks";

        /// <summary>
        /// The description of the CinnamonSticks instance
        /// </summary>
        public override string Description => "Like breadsticks but for dessert";

        private bool _frosting = true;
        /// <summary>
        /// Whether this CinnamonSticks instance contains frosting
        /// </summary>
        public bool Frosting
        {
            get { return _frosting; }
            set
            {
                _frosting = value;
                OnPropertyChanged(nameof(Frosting));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(Price));
            }
        }


        /// <summary>
        /// The price of this CinnamonSticks instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                if (Frosting) { return 0.90m*SideCount; }
                else return 0.75m*SideCount;
            }
        }

        /// <summary>
        /// The number of calories in each slice of this CinnamonSticks instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                uint cals = 160;

                if (Frosting) cals += 30;

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

                instructions.Add(SideCount.ToString() + " Cinnamon Sticks");
                if (!Frosting) instructions.Add("Hold Frosting");
                return instructions;
            }
        }
    }
}