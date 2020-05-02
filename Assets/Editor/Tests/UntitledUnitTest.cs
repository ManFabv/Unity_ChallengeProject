using Zenject;
using NUnit.Framework;
using Moq;

[TestFixture]
public class UntitledUnitTest : ZenjectUnitTestFixture
{
    public class DummyClass
    {
        public int integer = 10;
    }

    [Test]
    public void RunTest1()
    {
        var mock = new Mock<DummyClass>();
        Assert.NotNull(mock);
        Assert.AreEqual(mock.Object.integer, 10);
    }
}