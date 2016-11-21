using GetStarted.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.ViewComponents
{
    public class Greeting : ViewComponent
    {
        private IGreetingService greetingService;

        public Greeting(IGreetingService greetingService)
        {
            this.greetingService = greetingService;
        }


        public IViewComponentResult Invoke()
        {
            var model = this.greetingService.GetGreeting();

            return View("Default", model);
        }
    }
}
