using System;
using System.Text.Json.Serialization;

namespace FoodClassLib
{
    /// <summary>
    /// This class represents a food item.
    /// </summary>
    public class Food
    {
        /// <summary>
        /// Unique identifier for the food item.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the food item.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Amount of this food item in stock.
        /// </summary>
        [JsonPropertyName("inStock")]
        public int InStock { get; set; }

        /// <summary>
        /// Specifies the lowest limit of this item.
        /// </summary>
        [JsonPropertyName("lowLevel")]
        public int LowLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Information about this food item as a string.</returns>
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, In stock: {InStock}, Low level: {LowLevel}";
        }
    }
}
