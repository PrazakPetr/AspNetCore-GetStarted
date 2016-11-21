using GetStarted.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Services
{
    public class InMemoryRestaurantRepository : IRestarantRepository
    {
        static private List<Restaurant> restaurants;

        static InMemoryRestaurantRepository()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id=1, Name ="First Restaurant"},
                new Restaurant { Id=2, Name = "Second Restaurant"},
                new Restaurant { Id=3, Name = "Third Restaurant"}
            };
        }

        public Restaurant Get(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants;
        }

        public int Add(Restaurant newRestaurant)
        {
            int newId = restaurants.Max(r => r.Id) + 1;

            restaurants.Add(new Restaurant
            {
                Id = newId,
                Name = newRestaurant.Name,
                Cuisine = newRestaurant.Cuisine
            });

            return newId;
        }

        public void Edit(Restaurant restaurant)
        {
            var rest = restaurants.FirstOrDefault(r => r.Id == restaurant.Id);

            rest.Name = restaurant.Name;
            rest.Cuisine = restaurant.Cuisine;
        }
    }
}
