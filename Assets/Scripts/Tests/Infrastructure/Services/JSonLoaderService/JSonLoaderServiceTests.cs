using System;
using NUnit.Framework;
using Zenject;

namespace Assets.Scripts.Tests.Infrastructure.Services.JSonLoaderService
{
    [TestFixture]
    public class JSonLoaderServiceTests : ZenjectUnitTestFixture
    {
        private ILoaderService _loaderService;
        private IGameStaticsLevelValues _gameStaticsLevelValues;

        [SetUp]
        public void CommonInstall()
        {
            Container.Bind<IGameStaticsLevelValues>().To<GameStaticsLevelValues>().AsSingle();
            Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
            Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>().AsSingle();
            Container.Bind<ILoaderService>().To<global::JSonLoaderService>().AsSingle();
            Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
            _loaderService = Container.Resolve<ILoaderService>();
            _gameStaticsLevelValues = Container.Resolve<IGameStaticsLevelValues>();
        }

        [Test]
        [TestCase("testlevel")]
        public void CanReadJSONFile_Test(string fileName)
        {
            var levelTileDataFile = _loaderService.Read<LevelTileData>(fileName);

            Assert.NotNull(levelTileDataFile);
        }

        [Test]
        [TestCase("TestLevels", 1)]
        public void CanReadJSONFileByLevel_Test(string rootPath, int levelToLoad)
        {
            var fileName =  $"{rootPath}\\{_gameStaticsLevelValues.LevelRootName}{levelToLoad}";
            var levelTileDataFile = _loaderService.Read<LevelTileData>(fileName);

            Assert.NotNull(levelTileDataFile);
            Assert.AreEqual(1, levelTileDataFile.LevelNumber);
            Assert.AreEqual(8, levelTileDataFile.MapSize);
        }
    
        [Test]
        [TestCase("testlevelinvalid")]
        public void CannotReadJSONFileBecauseDoesntComplySchema_Test(string fileName)
        {
            Assert.Throws<ArgumentException>(() => _loaderService.Read<LevelTileData>(fileName));
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
}