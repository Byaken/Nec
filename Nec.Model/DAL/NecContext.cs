using System.Data.Entity;

namespace Nec.Model.DAL
{
    public class NecContext : DbContext
    {
        public virtual IDbSet<FunFact> FunFacts { get; set; }
        public virtual IDbSet<Tag> Tags { get; set; }

        public NecContext() : base("NecDB")
        {
            
        }
    }
}
