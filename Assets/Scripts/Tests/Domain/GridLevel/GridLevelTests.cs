using NUnit.Framework;
using PPop.Domain.Levels;
using PPop.Game.GridLevels;
using PPop.Infrastructure.Helpers.FileAndDirectory;
using PPop.Infrastructure.Services.Loader;
using PPop.Infrastructure.Validators.SchemaBuilder;
using PPop.Infrastructure.Validators.Validators;
using PPops.Domain.Statics.LevelStatics;
using Zenject;

namespace PPop.Tests.Domain.GridLevels
{
    [TestFixture]
    public class GridLevelTests : ZenjectUnitTestFixture
    {
        private GridLevel level;
        private ILoaderService _loaderService;
        private IReader _reader;
        private IGameStaticsLevelValues _gameStaticsLevelValues;

        [SetUp]
        public void CommonInstall()
        {
            Container.Bind<IGameStaticsLevelValues>().To<GameStaticsLevelValues>().AsSingle();
            Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
            Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>().AsSingle();
            Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
            Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();

            _gameStaticsLevelValues = Container.Resolve<IGameStaticsLevelValues>();
            _loaderService = Container.Resolve<ILoaderService>();
            _reader = Container.Resolve<IReader>();

            level = new GridLevel(_loaderService, _reader, _gameStaticsLevelValues);
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
            Assert.DoesNotThrow(() => level.GetFilledMap());
            Assert.True(level.IsLoaded);
        }

        [Test]
        [TestCase("TestLevels", 1)]
        public void LevelIsNotLoadedByLevelName_Test(string rootPath, int levelToLoad)
        {
            level.LoadLevel(rootPath, levelToLoad);
            Assert.DoesNotThrow(() => level.GetFilledMap());
            Assert.True(level.IsLoaded);
        }

        [Test]
        [TestCase("TestLevels\\Level_1")]
        public void LevelIsLoadedNotFails_Test(string fileName)
        {
            level.LoadLevel(fileName);
            var map = level.GetFilledMap();

            Assert.NotNull(map);
            Assert.NotNull(map.positions);
            Assert.NotNull(map.tiles);
            Assert.AreEqual(64, map.tiles.Length);
            Assert.AreEqual(64, map.positions.Length);
            Assert.True(level.IsLoaded);
        }

        [Test]
        [TestCase("TestLevels", 1)]
        public void CanReadJSONFileByLevel_Test(string rootPath, int levelToLoad)
        {
            var fileName = $"{rootPath}\\{_gameStaticsLevelValues.LevelRootName}{levelToLoad}";
            var levelTileDataFile = _loaderService.Read<LevelTileData>(fileName);

            Assert.NotNull(levelTileDataFile);
            Assert.AreEqual(1, levelTileDataFile.LevelNumber);
            Assert.AreEqual(8, levelTileDataFile.MapSize);
        }
    }
}