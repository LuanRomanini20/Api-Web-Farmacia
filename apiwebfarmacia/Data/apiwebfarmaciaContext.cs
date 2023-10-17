using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using apiwebfarmacia.Models;

namespace apiwebfarmacia.Data
{
    public class apiwebfarmaciaContext : DbContext
    {
        public apiwebfarmaciaContext (DbContextOptions<apiwebfarmaciaContext> options)
            : base(options)
        {
        }

        public DbSet<apiwebfarmacia.Models.remedio> remedio { get; set; } = default!;
    }
}
