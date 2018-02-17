using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TelerikAspNetCoreApp20.Models
{
    public class CarsDb : DbContext
    {
        public CarsDb (DbContextOptions<CarsDb> options)
            : base(options)
        {
        }

        public DbSet<TelerikAspNetCoreApp20.Models.Car> Cars { get; set; }
    }
}
