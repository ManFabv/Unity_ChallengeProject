using Zenject;
using NUnit.Framework;

[TestFixture]
public class JSonLoaderServiceTests : ZenjectUnitTestFixture
{
    private ILoaderService _loaderService;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
        Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
        _loaderService = Container.Resolve<ILoaderService>();
    }

    [Test]
    [TestCase("testlevel")]
    public void CanReadJSONFile_Test(string fileName)
    {
        var levelTileDataFile = _loaderService.Read<LevelTileData>(fileName);
        
        Assert.NotNull(levelTileDataFile);
        Assert.AreEqual(2, levelTileDataFile.LevelTilesCount);
        Assert.AreEqual(0, levelTileDataFile[0].Position);
        Assert.AreEqual(1, levelTileDataFile[1].Position);
        Assert.AreEqual(1, levelTileDataFile[0].Cost);
        Assert.AreEqual(2, levelTileDataFile[1].Cost);
    }
}