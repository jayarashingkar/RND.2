using RNDSystems.Console.Models;
using System.Data.Entity;

namespace RNDSystems.Console.Context
{
    public class LocalContext : DbContext
    {

        public LocalContext() : base("RND")
        {

        }
        public DbSet<RNDWorkStudy> RNDWorkStudies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RNDWorkStudy>().MapToStoredProcedures();
        }
    }
}
