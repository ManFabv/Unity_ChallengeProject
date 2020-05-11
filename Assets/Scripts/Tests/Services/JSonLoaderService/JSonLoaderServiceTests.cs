using System;
using Zenject;
using NUnit.Framework;

[TestFixture]
public class JSonLoaderServiceTests : ZenjectUnitTestFixture
{
    private ILoaderService _loaderService;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
        Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>().AsSingle();
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
        Assert.AreEqual(1, levelTileDataFile[0].Cost);
        Assert.AreEqual(2, levelTileDataFile[1].Cost);
    }

    [Test]
    [TestCase("TestLevels", 1)]
    public void CanReadJSONFileByLevel_Test(string rootPath, int levelToLoad)
    {
        var fileName =  $"{rootPath}\\Level_{levelToLoad}";
        var levelTileDataFile = _loaderService.Read<LevelTileData>(fileName);

        Assert.NotNull(levelTileDataFile);
        Assert.AreEqual(1, levelTileDataFile.LevelNumber);
        Assert.AreEqual(8, levelTileDataFile.MapSize);
        Assert.AreEqual(levelTileDataFile.MapSize * levelTileDataFile.MapSize, levelTileDataFile.Tiles.Length);
        Assert.AreEqual(1, levelTileDataFile[0].Cost);
        Assert.AreEqual(2, levelTileDataFile[1].Cost);
    }

    [Test]
    [TestCase("testlevel")]
    public void CannotConvertToInvalidType_Test(string fileName)
    {
        Assert.Throws<ArgumentException>(() => _loaderService.Read<MockInvalidClass>(fileName));
    }

    [Test]
    [TestCase("testlevelinvalid")]
    public void CannotConvertInvalidJsonToType_Test(string fileName)
    {
        Assert.Throws<ArgumentException>(() => _loaderService.Read<LevelTileData>(fileName));
    }

    [Test]
    [TestCase("testlevelasd")]
    public void CannotFindFile_Test(string fileName)
    {
        Assert.Throws<ArgumentException>(() => _loaderService.Read<MockInvalidClass>(fileName));
    }

#pragma warning disable 0649
    private class MockInvalidClass 
    {
        public int value;
    }
#pragma warning restore 0649
}