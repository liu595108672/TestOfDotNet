using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestForIndetity.Models;
using Microsoft.EntityFrameworkCore;

namespace TestForIndetity.Contexts
{
    public class IdentityUserContext:IdentityDbContext<IdentityUserTable>
    {
        public DbSet<IdentityUserTable> Users { set; get; } 
    }
}
