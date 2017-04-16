using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Nec.Model;
using Nec.Model.DAL;
using NUnit.Framework;
using Rhino.Mocks;
namespace Nec.Tests
{
    /// <summary>
    /// Some tests examples, not full coverage implemented
    /// </summary>
    [TestFixture]
    public class ChuckNorrisFunFactManagerTest
    {
        private ChuckNorrisFunFactManager _chuckNorrisFunFactManager;

        private readonly IList<FunFact> _testFunFacts = new List<FunFact>
        {
            new FunFact
            {
                Description = "Fun fact 1",
                FunFactId = 1,
                Popularity = 10,
                Tags = new List<Tag>()
            },
            new FunFact
            {
                Description = "Fun fact 2",
                FunFactId = 2,
                Popularity = 20,
                Tags = new List<Tag>()
            }
        };

        private readonly IList<Tag> _testTags = new List<Tag>
        {
            new Tag
            {
                TagId = 1,
                Description = "Chuck Norris",
                FunFacts = new List<FunFact>()
            }
        };
        
        [SetUp]
        public void SetUp()
        {
            this._chuckNorrisFunFactManager = new ChuckNorrisFunFactManager();
            this._chuckNorrisFunFactManager.NecContext = MockRepository.GenerateMock<NecContext>();
            this.InitializeTestData();
            this._chuckNorrisFunFactManager.NecContext = CreateCustomerDataContextTestDouble(this._testFunFacts, this._testTags);
        }

        [Test]
        public void WhenGettingTop3FunFacts_ExistingTwoFunFacts_ShouldReturnBothFunFacts()
        {
            // Arrange
            
            // Act
            var result = this._chuckNorrisFunFactManager.GetMostPopularFunFacts(3);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, _testFunFacts.Count);
            Assert.AreEqual(result[0].Description, this._testFunFacts[1].Description);
            Assert.AreEqual(result[1].Description, this._testFunFacts[0].Description);
        }

        [Test]
        public void WhenGettingRandomFunFact_ExistingTwoFunFacts_ShouldReturnOneValidFunFact()
        {
            // Arrange

            // Act
            var result = this._chuckNorrisFunFactManager.GetRandomFunFact();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void WhenModifyingFunFact_GivingValidFunFactId_ShouldReturnTrue()
        {
            // Arrange

            // Act
            var result = this._chuckNorrisFunFactManager.ModifyFunFact(1, "New fun fact description");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenModifyingFunFact_GivingInValidFunFactId_ShouldReturnFalse()
        {
            // Arrange

            // Act
            var result = this._chuckNorrisFunFactManager.ModifyFunFact(100, "New fun fact description");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenModifyingFunFact_GivingNullDescription_ShouldReturnFalse()
        {
            // Arrange

            // Act
            var result = this._chuckNorrisFunFactManager.ModifyFunFact(100, null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenModifyingFunFact_GivingEmptyDescription_ShouldReturnFalse()
        {
            // Arrange

            // Act
            var result = this._chuckNorrisFunFactManager.ModifyFunFact(100, string.Empty);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenDeletingFunFact_GivingValidFunFactId_ShouldReturnTrue()
        {
            // Arrange

            // Act
            var result = this._chuckNorrisFunFactManager.DeleteFunFact(1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenDeletingFunFact_GivingInValidFunFactId_ShouldReturnFalse()
        {
            // Arrange

            // Act
            var result = this._chuckNorrisFunFactManager.DeleteFunFact(100);

            // Assert
            Assert.IsFalse(result);
        }

        private void InitializeTestData()
        {
            foreach (var tag in _testTags)
            {
                foreach (var funFact in _testFunFacts)
                {
                    tag.FunFacts.Add(funFact);
                    funFact.Tags.Add(tag);
                }
            }
        }

        private IDbSet<T> GetDbSetTestDouble<T>(IList<T> data) where T : class
        {
            IQueryable<T> queryable = data.AsQueryable();

            IDbSet<T> dbSet = MockRepository.GenerateMock<IDbSet<T>, IQueryable>();

            dbSet.Stub(m => m.Provider).Return(queryable.Provider);
            dbSet.Stub(m => m.Expression).Return(queryable.Expression);
            dbSet.Stub(m => m.ElementType).Return(queryable.ElementType);
            dbSet.Stub(m => m.GetEnumerator()).Return(queryable.GetEnumerator());

            return dbSet;
        }

        private NecContext CreateCustomerDataContextTestDouble(IList<FunFact> testFunFacts, IList<Tag> testTags)
        {
            var dataContextTestDouble = MockRepository.GenerateMock<NecContext>();

            dataContextTestDouble.Stub(x => x.FunFacts).PropertyBehavior();
            dataContextTestDouble.FunFacts = GetDbSetTestDouble(testFunFacts);
            dataContextTestDouble.Stub(x => x.Tags).PropertyBehavior();
            dataContextTestDouble.Tags = GetDbSetTestDouble(testTags);
            return dataContextTestDouble;
        }
    }
}
