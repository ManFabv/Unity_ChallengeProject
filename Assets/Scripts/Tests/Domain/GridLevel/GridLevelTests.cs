using Zenject;
using NUnit.Framework;

[TestFixture]
public class GridLevelTests : ZenjectUnitTestFixture
{
    private GridLevel level;
    private ILoaderService _loaderService;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
        Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
        _loaderService = Container.Resolve<ILoaderService>();
        level = new GridLevel(_loaderService);
    }

    [Test]
    public void LevelIsNotLoaded_Test()
    {
        Assert.True(!level.IsLoaded);
    }

    [Test]
    [TestCase("testlevel")]
    public void LevelIsLoaded_Test(string fileName)
    {
        level.LoadLevel(fileName);

        Assert.True(level.IsLoaded);
    }
}