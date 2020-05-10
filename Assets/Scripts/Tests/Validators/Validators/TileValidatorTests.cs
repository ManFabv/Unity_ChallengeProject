using Zenject;
using NUnit.Framework;

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
            Cost = -1,
            Position = 0
        };
        var validationResults = tileValidator.Validate(tileWithNegativeCost);
        Assert.AreEqual(1, validationResults.Errors.Count);
    }

    [Test]
    public void TileIsInvalidPosition_Test()
    {
        var tileWithNegativePosition = new Tile()
        {
            Cost = 0,
            Position = -1
        };
        var validationResults = tileValidator.Validate(tileWithNegativePosition);
        Assert.AreEqual(1, validationResults.Errors.Count);
    }

    [Test]
    public void TileIsEmptyCostAndPosition_Test()
    {
        var tileWithNegativePosition = new Tile();
        var validationResults = tileValidator.Validate(tileWithNegativePosition);
        Assert.AreEqual(0, validationResults.Errors.Count);
    }
}