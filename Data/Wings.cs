using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaParlor.Data
{
    public class Wings : Side, INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private uint _count = 5;
        public override uint SideCount
        {
            get { return _count; }
            set
            {
                if (value < 4)
                {
                    _count = 4;
                }
                else if (value > 12)
                {
                    _count = 12;
                }
                else
                {
                    _count = value;
                }

                // Raise PropertyChanged event to notify that Count has changed
                OnPropertyChanged(nameof(SideCount));
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(Price)); // Notify Price change, as it depends on Count
            }
        }

        private bool _boneIn = true;
        public bool BoneIn
        {
            get { return _boneIn; }
            set
            {
                _boneIn = value;

                OnPropertyChanged(nameof(BoneIn));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(Price)); 
            }
        }

        private WingSauce _sauce = WingSauce.Medium;
        public WingSauce Sauce
        {
            get { return _sauce; }
            set
            {
                _sauce = value;
                // Raise PropertyChanged event to notify that Sauce has changed
                OnPropertyChanged(nameof(Sauce));
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(CaloriesPerEach)); // Notify CaloriesPerEach change, as it depends on Sauce
                OnPropertyChanged(nameof(CaloriesTotal));
            }
        }

        public override string Name { get; } = "Wings";

        public override string Description { get; } = "Chicken wings tossed in sauce";

        public override decimal Price
        {
            get
            {
                if (BoneIn)
                {
                    return 1.5m * SideCount;
                }
                return 1.75m * SideCount;
            }
        }

        public override uint CaloriesPerEach
        {
            get
            {
                uint cals = 125;
                if (!BoneIn) { cals += 50; }
                if (Sauce == WingSauce.HoneyBBQ) { cals += 20; }
                return cals;
            }
        }

        public override IEnumerable<string> SpecialInstructions
        {
            get
            {
                List<string> instructions = new();
                instructions.Add(SideCount.ToString());
                instructions.Add(Sauce.ToString());
                string boneType = "Boneless";
                if (BoneIn) { boneType = "Bone-In"; }
                instructions.Add(boneType);
                return instructions;
            }
        }

        public enum WingSauce
        {
            Mild,
            Medium,
            Hot,
            HoneyBBQ
        }
        /// <summary>
        /// Property changed handler
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
