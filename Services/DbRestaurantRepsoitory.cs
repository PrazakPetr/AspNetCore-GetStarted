using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetStarted.Entities;

namespace GetStarted.Services
{
    public class DbRestaurantRepsoitory : IRestarantRepository
    {
        private GetStartedDbContext context;

        public DbRestaurantRepsoitory(GetStartedDbContext context)
        {
            this.context = context;
        } 

        public int Add(Restaurant restaurant)
        {
            var newRestaurant= new Restaurant
            {
                Name = restaurant.Name,
                Cuisine = restaurant.Cuisine
            };

            context.Restaurants.Add(newRestaurant);
            context.SaveChanges();

            return newRestaurant.Id;
        }

        public void Edit(Restaurant restaurant)
        {
            var rest = context.Restaurants.FirstOrDefault(r => r.Id == restaurant.Id);

            rest.Name = restaurant.Name;
            rest.Cuisine = restaurant.Cuisine;

            context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return this.context.Restaurants.ToList();
        }
    }
}
