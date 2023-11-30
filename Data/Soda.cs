using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the Soda class
    /// </summary>
    public class Soda : Drink
    {

        /// <summary>
        /// The name of the Soda instance
        /// </summary>
        public override string Name { get; } = "Soda";

        /// <summary>
        /// The description of the Soda instance
        /// </summary>
        public override string Description => "Standard fountain drink";

        private SodaFlavor _flavor = SodaFlavor.Coke;
        /// <summary>
        /// The flavor of this soda
        /// </summary>
        public SodaFlavor DrinkType
        {
            get { return _flavor; }
            set
            {
                _flavor = value;
                OnPropertyChanged(nameof(DrinkType));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(SpecialInstructions));
            }
        }
        /// <summary>
        /// The price of this Soda instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                if (DrinkSize == Size.Small) { return 1.5m; }
                if (DrinkSize == Size.Medium) { return 2m; }
                else { return 2.5m; }

            }
        }

        /// <summary>
        /// The number of calories in this Soda instance
        /// </summary>
        public override uint CaloriesPerEach
        {
            get
            {
                if (DrinkType == SodaFlavor.DietCoke) { return 0; }
                else
                {
                    if (DrinkSize == Size.Small) { return 150; }
                    if (DrinkSize == Size.Medium) { return 200; }
                    return 250;
                }
            }
        }

        /// <summary>
        /// The number of calories in this Soda instance
        /// </summary>
        public override uint CaloriesTotal
        {
            get
            {
                if (DrinkType == SodaFlavor.DietCoke) { return 0; }
                else
                {
                    if (DrinkSize == Size.Small) { return 150; }
                    if (DrinkSize == Size.Medium) { return 200; }
                    return 250;
                }
            }
        }

        /// <summary>
        /// Special instructions for the preparation of this Soda
        /// </summary>
        public override IEnumerable<string> SpecialInstructions
        {
            get
            {
                List<string> instructions = new();
                instructions.Add(DrinkSize.ToString());
                instructions.Add(DrinkType.ToString());
                if (!Ice) { instructions.Add("Hold ice"); }
                return instructions;
            }
        }
        /// <summary>
        /// Different types of sodas
        /// </summary>
        public enum SodaFlavor
        {
            Coke,
            DietCoke,
            DrPepper,
            Sprite,
            Rootbeer
        }
    }
}