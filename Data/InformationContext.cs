using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class InfcormationContext : DbContext
{
    public InfcormationContext(DbContextOptions<InfcormationContext> options)
        : base(options)
    {
    }

    public DbSet<InformationItem> InformationItem { get; set; }
}
