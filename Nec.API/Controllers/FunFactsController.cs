using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;

namespace Nec.API.Controllers
{
    public class FunFactsController : ApiController
    {
        [Dependency]
        public IFunFactManager FunFactManager { get; set; }

        // GET: api/FunFacts
        public IEnumerable<string> Get(int numberOfFacts)
        {
            return FunFactManager.GetMostPopularFunFacts(numberOfFacts).Select(x => x.Description);
        }

        // GET: api/FunFacts/5
        public string Get()
        {
            return FunFactManager.GetRandomFunFact().Description;
        }

        // POST: api/FunFacts
        public bool Post([FromBody]string value)
        {
            return FunFactManager.AddFunFact(value);
        }

        // PUT: api/FunFacts/5
        public bool Put(int id, [FromBody]string value)
        {
            return FunFactManager.ModifyFunFact(id, value);
        }

        // DELETE: api/FunFacts/5
        public bool Delete(int id)
        {
            return FunFactManager.DeleteFunFact(id);
        }
    }
}
