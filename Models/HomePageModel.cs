using GetStarted.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Models
{
    public class HomePageModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}
