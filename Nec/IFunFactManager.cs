using System.Collections.Generic;
using Nec.Model;

namespace Nec
{
    public interface IFunFactManager
    {
        /// <summary>
        /// Get a number of the most popular fun facts. If there are not enough facts to satisfy the param, all the facts will be retrieved,
        /// order by popularity desc (most popular to less popular)
        /// </summary>
        /// <param name="numberOfFunFacts">Number the most popular fun facts to retrieve</param>
        /// <returns>The top "numberOfFunFacts" fun facts</returns>
        IList<FunFact> GetMostPopularFunFacts(int numberOfFunFacts);

        /// <summary>
        /// Retrieves a random fun fact
        /// </summary>
        /// <returns>Random fun fact</returns>
        FunFact GetRandomFunFact();

        /// <summary>
        /// Adds a fun fact to the fun facts
        /// </summary>
        /// <param name="description">Description of the fun fact</param>
        /// <returns>True in case of success, false otherwise</returns>
        bool AddFunFact(string description);

        /// <summary>
        /// Deletes a fun fact
        /// </summary>
        /// <param name="funFactId">Id of the fun fact to delete</param>
        /// <returns>True in case of success, false otherwise</returns>
        bool DeleteFunFact(int funFactId);

        /// <summary>
        /// Modifies a fun fact (only description is able to be modified)
        /// </summary>
        /// <param name="funFactId">Id of the fun fact to modify</param>
        /// <param name="description">New description of the fun fact</param>
        /// <returns></returns>
        bool ModifyFunFact(int funFactId, string description);
    }
}
