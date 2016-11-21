using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Services
{
    public interface IGreetingService
    {
        string GetGreeting();
    }

    public class JsonGreetingService : IGreetingService
    {
        private string greeting;

        public JsonGreetingService(IConfiguration configuration)
        {
            this.greeting = configuration["greeting"];
        }

        public string GetGreeting()
        {
            return this.greeting;
        }
    }
}
