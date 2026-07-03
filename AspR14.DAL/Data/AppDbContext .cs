using System;
using System.Collections.Generic;
using System.Text;
using AspR14.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AspR14.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}