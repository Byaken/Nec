using System.Collections.Generic;
using System.Linq;
using Nec.API.Controllers;
using Nec.Model;
using NUnit.Framework;
using Rhino.Mocks;

namespace Nec.API.Tests.Controllers
{
    /// <summary>
    /// Some example test, not full coverage
    /// </summary>
    [TestFixture]
    public class FunFactsControllerTest
    {
        private static IList<FunFact> TestFunFacts => new List<FunFact>
        {
            new FunFact
            {
                Description = "Fun fact 1",
                FunFactId = 1,
                Popularity = 10
            },
            new FunFact
            {
                Description = "Fun fact 2",
                FunFactId = 2,
                Popularity = 20
            }
        };

        private FunFactsController _funFactController;

        [SetUp]
        public void SetUp()
        {
            this._funFactController = new FunFactsController();
        }

        [Test]
        public void WhenGettingRandomFunFact_ExistingOneFunFact_ShouldReturnFunFactDescription()
        {
            // Arrange
            this._funFactController.FunFactManager = MockRepository.GenerateStub<IFunFactManager>();
            this._funFactController.FunFactManager.Stub(x => x.GetRandomFunFact()).Return(new FunFact {Description = "FunFact"});

            // Act
            var result = this._funFactController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "FunFact");
        }

        [Test]
        public void WhenGettingTop10FunFacts_ExistingTwoFunFacts_ShouldReturnBothFacts()
        {
            // Arrange
            var testFunFacts = TestFunFacts;
            this._funFactController.FunFactManager = MockRepository.GenerateMock<IFunFactManager>();
            this._funFactController.FunFactManager.Expect(x => x.GetMostPopularFunFacts(10)).Return(testFunFacts.OrderByDescending(x => x.Popularity).ToList());

            // Act
            var result = this._funFactController.Get(10).ToList();

            // Assert
            this._funFactController.FunFactManager.VerifyAllExpectations();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, testFunFacts.Count);
            Assert.AreEqual(result[0], testFunFacts[1].Description);
            Assert.AreEqual(result[1], testFunFacts[0].Description);
        }

        [Test]
        public void WhenDeletingFunFact_GivingValidId_ShouldReturnTrue()
        {
            // Arrange
            this._funFactController.FunFactManager = MockRepository.GenerateMock<IFunFactManager>();
            this._funFactController.FunFactManager.Expect(x => x.DeleteFunFact(1)).Return(true);

            // Act
            var result = this._funFactController.Delete(1);

            // Assert
            this._funFactController.FunFactManager.VerifyAllExpectations();
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenCreatingFunFact_GivingValidData_ShouldReturnTrue()
        {
            // Arrange
            this._funFactController.FunFactManager = MockRepository.GenerateMock<IFunFactManager>();
            this._funFactController.FunFactManager.Expect(x => x.AddFunFact("FactDescription")).Return(true);

            // Act
            var result = this._funFactController.Post("FactDescription");

            // Assert
            this._funFactController.FunFactManager.VerifyAllExpectations();
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenModifyingFunFact_GivingValidData_ShouldReturnTrue()
        {
            // Arrange
            this._funFactController.FunFactManager = MockRepository.GenerateMock<IFunFactManager>();
            this._funFactController.FunFactManager.Expect(x => x.ModifyFunFact(1, "FactDescription")).Return(true);

            // Act
            var result = this._funFactController.Put(1, "FactDescription");

            // Assert
            this._funFactController.FunFactManager.VerifyAllExpectations();
            Assert.IsTrue(result);
        }
    }
}
