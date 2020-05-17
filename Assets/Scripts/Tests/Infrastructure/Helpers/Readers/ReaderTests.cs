using System;
using NUnit.Framework;
using PPop.Domain.Levels;
using PPop.Infrastructure.Helpers.FileAndDirectory;
using PPops.Domain.Statics.LevelStatics;
using Zenject;

namespace PPop.Tests.Infrastructure.Helpers.Readers
{
    [TestFixture]
    public class ReaderTests : ZenjectUnitTestFixture
    {
        private IReader _reader;

        [SetUp]
        public void CommonInstall()
        {
            Container.Bind<IGameStaticsLevelValues>().To<GameStaticsLevelValues>().AsSingle();
            Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
            _reader = Container.Resolve<IReader>();
        }

        [Test]
        [TestCase("testlevel")]
        public void CanLoadFile_Test(string fileName)
        {
            Assert.DoesNotThrow(() => _reader.Read(fileName));
        }

        [Test]
        [TestCase("testlevel")]
        public void CanReadFile_Test(string fileName)
        {
            var fileContent = _reader.Read(fileName);
            Assert.AreNotEqual(string.Empty, fileContent);
        }

        [Test]
        [TestCase("testlevelasd")]
        public void FileNotFound_Test(string fileName)
        {
            Assert.Throws<ArgumentException>(() => _reader.Read(fileName));
        }

        [Test]
        [TestCase(typeof(LevelTileData))]
        public void CanLoadSchemaFile_Test(Type asType)
        {
            Assert.DoesNotThrow(() => _reader.ReadSchema(asType));
        }

        [Test]
        [TestCase(typeof(LevelTileData))]
        public void CanReadSchemaFile_Test(Type asType)
        {
            var fileContent = _reader.ReadSchema(asType);
            Assert.AreNotEqual(string.Empty, fileContent);
        }

        [Test]
        [TestCase(typeof(LevelTileDataMock))]
        public void SchemaFileNotFound_Test(Type asType)
        {
            Assert.Throws<ArgumentException>(() => _reader.ReadSchema(asType));
        }

        private class LevelTileDataMock
        {
            private int value;
        }
    }
}