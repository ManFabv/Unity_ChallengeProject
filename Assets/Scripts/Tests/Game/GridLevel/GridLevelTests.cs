using NUnit.Framework;
using PPop.Domain.Levels;
using PPop.Game.GridLevels;
using PPop.Infrastructure.Helpers.FileAndDirectory;
using PPop.Infrastructure.Services.Loader;
using PPop.Infrastructure.Validators.SchemaBuilder;
using PPop.Infrastructure.Validators.Validators;
using PPops.Domain.Statics.LevelStatics;
using UnityEngine;
using Zenject;

namespace PPop.Tests.Game.GridLevels
{
    [TestFixture]
    public class GridLevelTests : ZenjectUnitTestFixture
    {
        private ILevel _level;
        private ILoaderService _loaderService;
        private IGridLevelFactory _gridLevelFactory;
        private IGameStaticsLevelValues _gameStaticsLevelValues;

        [SetUp]
        public void CommonInstall()
        {
            Container.Bind<IGridLevelFactory>().To<GridLevelFactory>().AsSingle();
            Container.Bind<IGameStaticsLevelValues>().To<GameStaticsLevelValues>().AsSingle();
            Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
            Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>().AsSingle();
            Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
            Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();

            _gameStaticsLevelValues = Container.Resolve<IGameStaticsLevelValues>();
            _loaderService = Container.Resolve<ILoaderService>();
            _gridLevelFactory = Container.Resolve<IGridLevelFactory>();

            _level = new GridLevel(_loaderService, _gameStaticsLevelValues, _gridLevelFactory);
        }

        [Test]
        public void LevelIsNotLoaded_Test()
        {
            Assert.True(!_level.IsLoaded);
        }

        [Test]
        [TestCase("TestLevels\\Level_1")]
        public void LevelIsLoaded_Test(string fileName)
        {
            _level.LoadLevel(fileName);
            Assert.DoesNotThrow(() => _level.GetFilledMap());
            Assert.True(_level.IsLoaded);
        }

        [Test]
        [TestCase("TestLevels", 1)]
        public void LevelIsNotLoadedByLevelName_Test(string rootPath, int levelToLoad)
        {
            _level.LoadLevel(rootPath, levelToLoad);
            Assert.DoesNotThrow(() => _level.GetFilledMap());
            Assert.True(_level.IsLoaded);
        }

        [Test]
        [TestCase("TestLevels\\Level_1")]
        public void LevelIsLoadedNotFails_Test(string fileName)
        {
            _level.LoadLevel(fileName);
            var map = _level.GetFilledMap();

            Assert.NotNull(map);
            Assert.NotNull(map.positions);
            Assert.NotNull(map.tiles);
            Assert.AreEqual(64, map.tiles.Length);
            Assert.AreEqual(64, map.positions.Length);
            Assert.True(_level.IsLoaded);
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

        [Test]
        [TestCase("TestLevels", 1)]
        public void CanReadJSONFileByLevelAndGetTileNodeAtPosition_Test(string rootPath, int levelToLoad)
        {
            var fileName = $"{rootPath}\\{_gameStaticsLevelValues.LevelRootName}{levelToLoad}";
            _level.LoadLevel(fileName);
            _level.GetFilledMap();

            var expectedPosition = new Vector3Int(-4, 4, 0);

            var tile = _level.GetTileNodeAtPosition(expectedPosition);

            Assert.NotNull(tile);
            Assert.AreEqual(5, tile.Cost);
            Assert.AreEqual("desert", tile.Representation);
            Assert.AreEqual(expectedPosition, tile.Position);
        }
    }
}