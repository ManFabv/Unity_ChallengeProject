using Zenject;
using NUnit.Framework;

[TestFixture]
public class ReaderTests : ZenjectUnitTestFixture
{
    private IReader _reader;

    [SetUp]
    public void CommonInstall()
    {
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
}