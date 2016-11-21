using GetStarted.Entities;
using GetStarted.Models;
using GetStarted.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Controllers
{
    public class HomeController : Controller
    {
        private IGreetingService greeter;
        private IRestarantRepository restaurantRepo;

        public HomeController(IRestarantRepository repo, IGreetingService greeter)
        {
            this.restaurantRepo = repo;
            this.greeter = greeter;
        }

        public IActionResult Index()
        {
            var model = new HomePageModel
            {
                Restaurants = this.restaurantRepo.GetAll(),
                CurrentGreeting = this.greeter.GetGreeting()
            };

            return base.View(model);
        }

        public IActionResult Detail(int id)
        {
            var model = this.restaurantRepo.Get(id);

            if (model == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return base.View();
        }

        [HttpPost]
        public IActionResult Create(CreateRestaurantModel restaurantModel)
        {

            if (ModelState.IsValid)
            {
                var restaurant = new Restaurant
                {
                    Name = restaurantModel.Name,
                    Cuisine = restaurantModel.Cuisine
                };

                int id = this.restaurantRepo.Add(restaurant);

                restaurant.Id = id;

                return this.RedirectToAction("Detail", new { id = id });
            }

            return base.View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.restaurantRepo.Get(id);

            if (model == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateRestaurantModel restaurantModel)
        {

            if (ModelState.IsValid)
            {
                var restaurant = this.restaurantRepo.Get(id);


                restaurant.Name = restaurantModel.Name;
                restaurant.Cuisine = restaurantModel.Cuisine;
                

                 this.restaurantRepo.Edit(restaurant);

                return this.RedirectToAction("Detail", new { id = id });
            }

            return base.View();
        }
    }
}
