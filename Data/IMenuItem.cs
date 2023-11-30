using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaParlor.Data
{
    public interface IMenuItem
    {
        /// <summary>
        /// The name of this item
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The description of this item
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The price of this item
        /// </summary>
        decimal Price { get; }
        /// <summary>
        /// The calories per each of this item
        /// </summary>
        uint CaloriesPerEach { get; }
        /// <summary>
        /// The total calories for this item
        /// </summary>
        uint CaloriesTotal { get; }
        /// <summary>
        /// A list of special instructions pertaining to this item
        /// </summary>
        IEnumerable<string> SpecialInstructions { get; }
    }
}
