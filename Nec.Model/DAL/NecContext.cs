using System.Data.Entity;

namespace Nec.Model.DAL
{
    public class NecContext : DbContext
    {
        public DbSet<FunFact> FunFacts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public NecContext() : base("NecDB")
        {
            
        }
    }
}
