using System;
using System.Collections.Generic;
using System.Linq;
using Nec.Model;

namespace Nec
{
    /// <summary>
    /// Implementation of FunFactManager for ChuckNorris fun facts
    /// </summary>
    public class ChuckNorrisFunFactManager : BaseFunFactManager
    {
        private const string CHUCK_NORRIS_TAG = "Chuck Norris";

        protected override IList<Tag> GetRelatedTags()
        {
           return NecContext.Tags.Where(x => x.Description.Equals(CHUCK_NORRIS_TAG, StringComparison.InvariantCultureIgnoreCase)).ToList();          
        }
    }
}
