using System;
using Zenject;
using NUnit.Framework;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using PPop.Game.LevelManagers;
using UnityEngine;

namespace PPop.Tests.LevelManagers
{
    [TestFixture]
    public class LevelManagerPresenterTests : ZenjectUnitTestFixture
    {
        private LevelStateManager _levelStateManager;
        private TileNode tileNodeMock;
        private MockStatusIdle<TileNode> tileNodeFSMMockIdle;
        private MockStatusSelected<TileNode> tileNodeFSMSelected;

        [SetUp]
        public void CommonInstall()
        {
            tileNodeMock = ScriptableObject.CreateInstance<TileNode>(); ;
            tileNodeFSMMockIdle = new MockStatusIdle<TileNode>();
            tileNodeFSMSelected = new MockStatusSelected<TileNode>();
            _levelStateManager = new LevelStateManager(tileNodeFSMMockIdle, tileNodeMock);
        }

        [Test]
        public void CanInitAndExecuteState_Test()
        {
            Assert.DoesNotThrow(() => _levelStateManager = new LevelStateManager(tileNodeFSMMockIdle, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock));
        }

        [Test]
        public void CanChangeAndExitState_Test()
        {
            Assert.DoesNotThrow(() => _levelStateManager = new LevelStateManager(tileNodeFSMMockIdle, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.ChangeState(tileNodeFSMSelected, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock));
        }

        [Test]
        public void CannotWorkWithEmptyState_Test()
        {
            Assert.Throws<ArgumentNullException>(() => _levelStateManager = new LevelStateManager(null, tileNodeMock));
            Assert.Throws<ArgumentNullException>(() => _levelStateManager.ChangeState(null, tileNodeMock));
        }

        private class MockStatusSelected<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() 
        {
            public void Init(T node) { }
            public void Execute(T node, ILevelStateManager levelStateManager) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }

        private class MockStatusIdle<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() 
        {
            public void Init(T node) { }
            public void Execute(T node, ILevelStateManager levelStateManager) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }
    }
}