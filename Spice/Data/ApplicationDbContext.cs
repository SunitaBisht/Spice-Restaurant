using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spice.Areas.Admin.Controllers;
using Spice.Models;

namespace Spice.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        /// <summary>
        /// name of tha table
        /// </summary>
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }

        public DbSet<MenuItem> MenuItem { get; set;}

    }
}
