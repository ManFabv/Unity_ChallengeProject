using Zenject;
using NUnit.Framework;
using UnityEngine.Tilemaps;

[TestFixture]
public class GridLevelTests : ZenjectUnitTestFixture
{
    private GridLevel level;
    private ILoaderService _loaderService;
    private Tilemap tilemap;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
        Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>().AsSingle();
        Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
        Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
        _loaderService = Container.Resolve<ILoaderService>();
        level = new GridLevel(_loaderService);
        tilemap = null;
    }

    [Test]
    public void LevelIsNotLoaded_Test()
    {
        Assert.True(!level.IsLoaded);
    }

    [Test]
    [TestCase("TestLevels\\Level_1")]
    public void LevelIsLoaded_Test(string fileName)
    {
        level.LoadLevel(fileName);
        level.FillMap<Tilemap>(ref tilemap);

        Assert.True(level.IsLoaded);
    }

    [Test]
    [TestCase("TestLevels", 1)]
    public void LevelIsLoadedByLevelName_Test(string rootPath, int levelToLoad)
    {
        level.LoadLevel(rootPath, levelToLoad);
        level.FillMap<Tilemap>(ref tilemap);
        Assert.True(level.IsLoaded);
    }
}