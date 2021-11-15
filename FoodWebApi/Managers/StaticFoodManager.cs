using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodClassLib;

namespace FoodWebApi.Managers
{
    public class StaticFoodManager
    {
        private static List<Food> _food = new List<Food>()
        {
            new Food() {Id = 1, Name = "Cornflakes", InStock = 5, LowLevel = 5},
            new Food() {Id = 2, Name = "Cola", InStock = 15, LowLevel = 10},
            new Food() {Id = 3, Name = "Milk, low fat", InStock = 12, LowLevel = 15},
            new Food() {Id = 4, Name = "Chocolate", InStock = 7, LowLevel = 5},
            new Food() {Id = 5, Name = "Cookie", InStock = 5, LowLevel = 10}
        };

        private static int _nextId = ++_food[^1].Id;

        public IEnumerable<Food> GetAll()
        {
            return _food;
        }

        public Food GetById(int id)
        {
            var result = _food.Find(f => f.Id == id);
            return result;
        }

        public IEnumerable<Food> GetBySubstringOfName(string substring)
        {
            var result = _food.FindAll(f => f.Name.Contains(substring, StringComparison.OrdinalIgnoreCase));
            return result;
        }

        public IEnumerable<Food> GetPurchases()
        {
            var result = _food.FindAll(f => f.InStock < f.LowLevel);
            return result;
        }
    }
}
