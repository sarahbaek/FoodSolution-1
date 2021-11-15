using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodClassLib;
using FoodWebApi.Managers;

namespace FoodWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly StaticFoodManager _manager;

        public FoodsController()
        {
            _manager = new StaticFoodManager();
        }
        
        [HttpGet]
        public IEnumerable<Food> GetAll()
        {
            return _manager.GetAll();
        }

        [HttpGet("{id}")]
        public Food GetById(int id)
        {
            return _manager.GetById(id);
        }

        [HttpGet("name/{substring}")]
        public IEnumerable<Food> GetBySubstringOfName(string substring)
        {
            return _manager.GetBySubstringOfName(substring);
        }

        [HttpGet("purchases")]
        public IEnumerable<Food> GetPurchases()
        {
            return _manager.GetPurchases();
        }

    }
}
