using System.Collections.Generic;

namespace Nec.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Description { get; set; }

        public virtual IList<FunFact> FunFacts { get; set; }
    }
}
