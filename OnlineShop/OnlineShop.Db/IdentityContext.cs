using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Db
{
    public class IdentityContext:IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base (options)
        {
            Database.Migrate();
        }
    }
}
