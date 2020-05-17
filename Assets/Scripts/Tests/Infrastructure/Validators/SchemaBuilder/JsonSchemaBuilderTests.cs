using System;
using NUnit.Framework;
using PPop.Domain.Levels;
using PPop.Infrastructure.Helpers.FileAndDirectory;
using PPop.Infrastructure.Validators.SchemaBuilder;
using PPops.Domain.Statics.LevelStatics;
using Zenject;

namespace PPop.Tests.Infrastructure.Validators.SchemaBuilder
{
    [TestFixture]
    public class JsonSchemaBuilderTests : ZenjectUnitTestFixture
    {
        private ISchemaBuilder _schemaBuilder;

        [SetUp]
        public void CommonInstall()
        {
            Container.Bind<IGameStaticsLevelValues>().To<GameStaticsLevelValues>().AsSingle();
            Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
            Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
            _schemaBuilder = Container.Resolve<ISchemaBuilder>();
        }

        [Test]
        [TestCase(typeof(LevelTileData))]
        public void CanBuildSchema_Test(Type objecType)
        {
            var schema = _schemaBuilder.Build(objecType);
            Assert.NotNull(schema);
        }
    }
}
