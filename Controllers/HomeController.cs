using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUD.Models;
using Microsoft.EntityFrameworkCore;


namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.Dishes.ToList();
            return View();
        }

        [HttpGet("/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/add")]
        public IActionResult Add(Dish newDish)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("");
        }

        [HttpGet("/read/{DishId}")]
        public IActionResult OneDish(int dishId)
        {
            Dish oneDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            ViewBag.Dish = oneDish;
            return View("Read");
        }

        [HttpGet("edit/{DishId}")]
        public IActionResult Edit(int dishId)
        {
            Dish oneDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            ViewBag.Dish = oneDish;
            return View("Edit");
        }

        [HttpPost("/update")]
        public IActionResult Update(Dish editDish)
        {
            Dish retrievedDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == editDish.DishId);

            retrievedDish.Name = editDish.Name;
            retrievedDish.Chef = editDish.Chef;
            retrievedDish.Tastiness = editDish.Tastiness;
            retrievedDish.Calories = editDish.Calories;
            retrievedDish.Description = editDish.Description;
            retrievedDish.UpdatedAt = DateTime.Now;

            _context.SaveChanges();


            return RedirectToAction("");
        }


        [HttpGet("delete/{dishId}")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish retrievedDish = _context.Dishes
                .SingleOrDefault(dish => dish.DishId == dishId);

            _context.Dishes.Remove(retrievedDish);
            
            _context.SaveChanges();

            return RedirectToAction("");
        }                     















        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
