﻿using System;
using NUnit.Framework;
using Zenject;

[TestFixture]
public class JsonSchemaBuilderTests : ZenjectUnitTestFixture
{
    private ISchemaBuilder _schemaBuilder;

    [SetUp]
    public void CommonInstall()
    {
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