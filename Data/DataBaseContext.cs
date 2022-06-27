using camera.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace camera.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<ImageStore> image { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: "DataSource=cameraDb.db;Cache=Shared");
    }
}
