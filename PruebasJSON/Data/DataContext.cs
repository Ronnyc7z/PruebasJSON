using Microsoft.EntityFrameworkCore;
using PruebasJSON.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasJSON.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<LookupTable> LookupTable { get; set; }
        public DbSet<LookupTable> LookupTable3 { get; set; }

    }
    public static class MyExtensions 
    {
        public static IQueryable<object> Set (this DbContext _context, Type t)
        {
            return (IQueryable<object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }
    }
}
