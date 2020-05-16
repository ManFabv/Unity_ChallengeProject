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
    public class LevelManagerTests : ZenjectUnitTestFixture
    {
        private LevelStateManager<TileNode> _levelStateManager;
        private TileNode tileNodeMock;
        private MockStatusIdle<TileNode> tileNodeFSMMockIdle;
        private MockStatusSelected<TileNode> tileNodeFSMSelected;

        [SetUp]
        public void CommonInstall()
        {
            tileNodeMock = new Mock<TileNode>().Object;
            tileNodeFSMMockIdle = new MockStatusIdle<TileNode>();
            tileNodeFSMSelected = new MockStatusSelected<TileNode>();
            _levelStateManager = new LevelStateManager<TileNode>(tileNodeFSMMockIdle, tileNodeMock);
        }

        [Test]
        public void CanInitAndExecuteState_Test()
        {
            Assert.DoesNotThrow(() => _levelStateManager = new LevelStateManager<TileNode>(tileNodeFSMMockIdle, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock));
        }

        [Test]
        public void CanChangeAndExitState_Test()
        {
            Assert.DoesNotThrow(() => _levelStateManager = new LevelStateManager<TileNode>(tileNodeFSMMockIdle, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.ChangeState(tileNodeFSMSelected, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock));
        }

        [Test]
        public void CannotWorkWithEmptyState_Test()
        {
            Assert.Throws<ArgumentNullException>(() => _levelStateManager = new LevelStateManager<TileNode>(null, tileNodeMock));
            Assert.Throws<ArgumentNullException>(() => _levelStateManager.ChangeState(null, tileNodeMock));
        }

        private class MockStatusSelected<T> : Singleton<T>, IFSM<T> where T : TileNode, new() 
        {
            public void Init(T node) { }
            public void Execute(T node) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }

        private class MockStatusIdle<T> : Singleton<T>, IFSM<T> where T : TileNode, new() 
        {
            public void Init(T node) { }
            public void Execute(T node) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }
    }
}