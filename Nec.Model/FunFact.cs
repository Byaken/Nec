using System.Collections.Generic;

namespace Nec.Model
{
    public class FunFact
    {
        public int FunFactId { get; set; }

        public int Popularity { get; set; }

        public string Description { get; set; }

        public virtual IList<Tag> Tags { get; set; }
    }
}
