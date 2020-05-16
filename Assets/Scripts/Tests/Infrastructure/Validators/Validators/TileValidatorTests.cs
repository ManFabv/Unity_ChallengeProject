using NUnit.Framework;
using PPop.Domain.Tiles;
using PPop.Infrastructure.Validators.Validators;
using Zenject;

namespace PPop.Tests.Infrastructure.Validators.Validators
{
    [TestFixture]
    public class TileValidatorTests : ZenjectUnitTestFixture
    {
        private TileValidator tileValidator;

        [SetUp]
        public void CommonSetup()
        {
            tileValidator = new TileValidator();
        }

        [Test]
        public void TileIsInvalidCost_Test()
        {
            var tileWithNegativeCost = new TileNode()
            {
                Cost = -2,
                Representation = "desert"
            };
            var validationResults = tileValidator.Validate(tileWithNegativeCost);
            Assert.AreEqual(1, validationResults.Errors.Count);
        }

        [Test]
        public void TileIsInvalidRepresentation_Test()
        {
            var tileWithNegativePosition = new TileNode()
            {
                Cost = -1,
                Representation = string.Empty
            };
            var validationResults = tileValidator.Validate(tileWithNegativePosition);
            Assert.AreEqual(1, validationResults.Errors.Count);
        }

        [Test]
        public void TileIsEmptyCostAndPosition_Test()
        {
            var tileInvalid = new TileNode();
            tileInvalid.Representation = string.Empty;
            tileInvalid.Cost = -2;
            var validationResults = tileValidator.Validate(tileInvalid);
            Assert.AreEqual(2, validationResults.Errors.Count);
        }
    }
}