using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Nec.ExtensionMethods;
using Nec.Model;
using Nec.Model.DAL;

namespace Nec
{
    public abstract class BaseFunFactManager : IFunFactManager
    {
        /// <summary>
        /// TODO: Repository pattern could be implemented, so we decouple from the DB
        /// </summary>
        protected NecContext NecContext;



        protected BaseFunFactManager()
        {
            NecContext = new NecContext();
        }

        public bool AddFunFact(string description)
        {
            try
            {
                var funFact = new FunFact
                {
                    Description = description,
                    Tags = this.GetRelatedTags(),
                    Popularity = 0 // Default popularity will be 0, could be initialized to another value
                };

                NecContext.FunFacts.Add(funFact);
                NecContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                // Handle exception, if needed at this level, otherwise, remove try catch block
                return false;
            }
        }

        public bool DeleteFunFact(int funFactId)
        {
            try
            {
                var funFactToRemove = NecContext.FunFacts.FirstOrDefault(x => x.FunFactId == funFactId);
                if(funFactToRemove == null)
                {
                    return false;
                }
                NecContext.FunFacts.Remove(funFactToRemove);
                NecContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                // Handle exception, if needed at this level, otherwise, remove try catch block
                return false;
            }
        }

        public IList<FunFact> GetMostPopularFunFacts(int numberOfFunFacts)
        {
            try
            {
                var relatedFunFacts = this.GetRelatedFunFacts();
                return relatedFunFacts.OrderByDescending(x => x.Popularity).Take(numberOfFunFacts).ToList();               
            }
            catch (Exception e)
            {
                // Handle exception, if needed at this level, otherwise, remove try catch block
                return new List<FunFact>();
            }
        }

        public FunFact GetRandomFunFact()
        {
            try
            {
                return this.GetRelatedFunFacts().Shuffle().FirstOrDefault();
            }
            catch (Exception e)
            {
                // Handle exception, if needed at this level, otherwise, remove try catch block
                return null;
            }
        }

        public bool ModifyFunFact(int funFactId, string description)
        {
            try
            {
                var funFactToModify = NecContext.FunFacts.FirstOrDefault(x => x.FunFactId == funFactId);
                if (funFactToModify == null)
                {
                    return false;
                }
                funFactToModify.Description = description;
                NecContext.FunFacts.Attach(funFactToModify);
                NecContext.Entry(funFactToModify).State = EntityState.Modified;
                NecContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                // Handle exception, if needed at this level, otherwise, remove try catch block
                return false;
            }
        }

        /// <summary>
        /// Default implementation in the base: facts to be considered are facts who have at least one related tag
        // that equels one of the _tags that the manager has instantiated. If another rule is needed, specifc manager should
        // override this method
        // TODO: Consider overriding Equals for _tags class
        /// </summary>
        /// <returns>List of related fun facts based on tags</returns>
        protected IList<FunFact> GetRelatedFunFacts()
        {
            try
            {
                var relatedTags = this.GetRelatedTags();
                var relatedFunFactList = new List<FunFact>();
                foreach (var funFact in NecContext.FunFacts)
                {
                    relatedFunFactList.AddRange(from tag in relatedTags where funFact.Tags.Any(x => x.Description.Equals(tag.Description, StringComparison.InvariantCultureIgnoreCase)) select funFact);
                }
                return relatedFunFactList;
            }          
            catch (Exception e)
            {
                return new List<FunFact>();
            }
        }

        /// <summary>
        /// Get the related tags to use for this FunFactManager. Implementation is delegated on specific implementations
        /// </summary>
        /// <returns></returns>
        protected abstract IList<Tag> GetRelatedTags();
    }
}
