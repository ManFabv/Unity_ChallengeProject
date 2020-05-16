using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Tests.Infrastructure.Helpers.Math
{
    [TestFixture]
    public class Vector3ExtensionsTests : ZenjectUnitTestFixture
    {
        [Test]
        public void CanConvertVector3ToVector3Int_Test()
        {
            var sourceVector3 = new Vector3(1.5f, 2.0f, 1.3f);
            var sourceVector3AsInt = sourceVector3.AsVector3Int();
            Assert.AreEqual(2, sourceVector3AsInt.x);
            Assert.AreEqual(2, sourceVector3AsInt.y);
            Assert.AreEqual(1, sourceVector3AsInt.z);
        }
    }
}