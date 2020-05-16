using NUnit.Framework;
using Zenject;

namespace Assets.Scripts.Tests.Infrastructure.Validators.Validators
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
            var tileWithNegativeCost = new Tile()
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
            var tileWithNegativePosition = new Tile()
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
            var tileInvalid = new Tile();
            tileInvalid.Representation = string.Empty;
            tileInvalid.Cost = -2;
            var validationResults = tileValidator.Validate(tileInvalid);
            Assert.AreEqual(2, validationResults.Errors.Count);
        }
    }
}