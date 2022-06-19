using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts
{
    public class IdentityContext: IdentityDbContext<AppUser>
    {
        public IdentityContext()
        {

        }

        public IdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}