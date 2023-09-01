using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Faves.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurant_Faves.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        RestaurantDbContext _dBContext = new RestaurantDbContext();

        //api/Order
        //api/Order?restaurant=Taco Bell
        //api/Order?orderAgain=true
        //api/Order?orderAgain=true&restaurant=Taco Bell
        [HttpGet]
        public List<Order> GetOrder(bool? orderAgain = null, string restaurant = "")
        {
            //no params
            if(restaurant == "" && orderAgain == null)
            {
                return _dBContext.Orders.ToList();
            }
            //Params
            List<Order> Result = _dBContext.Orders.Where(o => o.Restaurant.Contains(restaurant)).ToList();
            if(orderAgain != null)
            {
                Result = Result.Where(o => o.OrderAgain == orderAgain).ToList();
            }

            return Result;
        }

        //api/Order/1
        [HttpGet("{id}")]
        public Order GetByID(int id)
        {
            return _dBContext.Orders.Find(id);
        }

        //api/Order?description=FamousBowl&restaurant=KFC&rating=4&OrderAgain=true
        //[HttpPost]
        //public Order AddOrder(string description, string restaurant, int rating, bool orderAgain)
        //{
        //    Order newOrder = new Order();
        //    newOrder.Description = description;
        //    newOrder.Restaurant = restaurant;
        //    newOrder.Rating = rating;
        //    newOrder.OrderAgain = orderAgain;

        //    _dBContext.Orders.Add(newOrder);
        //    _dBContext.SaveChanges();

        //    return newOrder;
        //}

        //Alternate HttpPost Method
        //api/Order
        [HttpPost]
        public Order AddOrder([FromBody] Order newOrder)
        {
            _dBContext.Orders.Add(newOrder);
            _dBContext.SaveChanges();
            return newOrder;
        }

        //api/Order/3
        [HttpPut ("{id}")]
        public Order UpdateOrder(int id, [FromBody] Order newOrder)
        {
            Order o = _dBContext.Orders.Find(id);
            o.Description = newOrder.Description;
            o.Restaurant = newOrder.Restaurant;
            o.Rating = newOrder.Rating;
            o.OrderAgain = newOrder.OrderAgain;

            _dBContext.Orders.Update(o);
            _dBContext.SaveChanges();
            return o;
        }

        //api/Order/3
        [HttpDelete("{id}")]
        public Order DeleteById(int id)
        {
            Order deleted = _dBContext.Orders.Find(id);
            _dBContext.Orders.Remove(deleted);
            _dBContext.SaveChanges();

            return deleted;
        }
    }
}

