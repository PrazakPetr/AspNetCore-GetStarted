using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetStarted.Entities
{
    public class GetStartedIdentityDbConxtext : IdentityDbContext<User>
    {
        public GetStartedIdentityDbConxtext(DbContextOptions<GetStartedIdentityDbConxtext> options)
            :base(options)
        {
        }
    }
}
