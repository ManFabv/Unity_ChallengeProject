using System;
using Moq;
using Zenject;
using NUnit.Framework;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using PPop.Game.LevelManagers;

namespace PPop.Tests.LevelManagers
{
    [TestFixture]
    public class LevelStateManagerTests : ZenjectUnitTestFixture
    {
        private MockStatusSelected<TileNode> tileNodeFSMSelected;
        private TileNode tileNodeMock;
        private MockStatusIdle<TileNode> tileNodeFSMMockIdle;

        [SetUp]
        public void CommonInstall()
        {
            tileNodeFSMSelected = new MockStatusSelected<TileNode>();
            tileNodeMock = new Mock<TileNode>().Object;
            tileNodeFSMMockIdle = new MockStatusIdle<TileNode>();
        }

        [Test]
        public void CanExecuteStepsOfStatus_Test()
        {
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Init(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Execute(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Exit(tileNodeMock));
        }

        [Test]
        public void CanChangeStatus_Test()
        {
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Init(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Execute(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Exit(tileNodeMock));

            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Init(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Execute(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Exit(tileNodeMock));

            Assert.AreNotEqual(tileNodeFSMMockIdle.StateType(), tileNodeFSMSelected.StateType());
        }

        private class MockStatusSelected<T> : Singleton<T>, IFSM<T> where T : TileNode, new() {
            public void Init(T node) { }
            public void Execute(T node) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }

        private class MockStatusIdle<T> : Singleton<T>, IFSM<T> where T : TileNode, new() {
            public void Init(T node) { }
            public void Execute(T node) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }
    }
}