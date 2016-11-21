using GetStarted.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Services
{
    public interface IRestarantRepository
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        int Add(Restaurant restaurant);
        void Edit(Restaurant restaurant);
    }

    
}
