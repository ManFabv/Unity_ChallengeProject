using NUnit.Framework;
using PPop.Domain.Tiles;
using PPop.Infrastructure.Validators.Validators;
using UnityEngine;
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
            var tileWithNegativeCost = ScriptableObject.CreateInstance<TileNode>();
            tileWithNegativeCost.Cost = -2;
            tileWithNegativeCost.Representation = "desert";
            tileWithNegativeCost.Position = Vector3Int.zero;
            var validationResults = tileValidator.Validate(tileWithNegativeCost);
            Assert.AreEqual(1, validationResults.Errors.Count);
        }

        [Test]
        public void TileIsInvalidRepresentation_Test()
        {
            var tileWithoutPosition = ScriptableObject.CreateInstance<TileNode>();
            tileWithoutPosition.Cost = -1;
            tileWithoutPosition.Representation = string.Empty;
            var validationResults = tileValidator.Validate(tileWithoutPosition);
            Assert.AreEqual(1, validationResults.Errors.Count);
        }

        [Test]
        public void TileIsEmptyCostAndPosition_Test()
        {
            var tileInvalid = ScriptableObject.CreateInstance<TileNode>();
            tileInvalid.Representation = string.Empty;
            tileInvalid.Cost = -2;
            var validationResults = tileValidator.Validate(tileInvalid);
            Assert.AreEqual(2, validationResults.Errors.Count);
        }
    }
}