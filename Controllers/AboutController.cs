using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Controllers
{
    [Route("company/[controller]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "737 633 910";
        }

        [Route("[action]")]
        public string Country()
        {
            return "CZE";
        }
    }
}
