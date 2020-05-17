using System;
using Zenject;
using NUnit.Framework;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using PPop.Game.LevelManagers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Tests.LevelManagers
{
    [TestFixture]
    public class LevelManagerPresenterTests : ZenjectUnitTestFixture
    {
        private LevelStateManager _levelStateManager;
        private TileNode tileNodeMock;
        private MockStatusIdle<TileNode> tileNodeFSMMockIdle;
        private MockStatusSelected<TileNode> tileNodeFSMSelected;
        private Tilemap tilemapMock;

        [SetUp]
        public void CommonInstall()
        {
            tileNodeMock = ScriptableObject.CreateInstance<TileNode>(); ;
            tileNodeFSMMockIdle = new MockStatusIdle<TileNode>();
            tileNodeFSMSelected = new MockStatusSelected<TileNode>();
            tilemapMock = new Tilemap();
            _levelStateManager = new LevelStateManager(tileNodeFSMMockIdle, tileNodeMock);
        }

        [Test]
        public void CanInitAndExecuteState_Test()
        {
            Assert.DoesNotThrow(() => _levelStateManager = new LevelStateManager(tileNodeFSMMockIdle, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock, tilemapMock));
        }

        [Test]
        public void CanChangeAndExitState_Test()
        {
            Assert.DoesNotThrow(() => _levelStateManager = new LevelStateManager(tileNodeFSMMockIdle, tileNodeMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock, tilemapMock));
            Assert.DoesNotThrow(() => _levelStateManager.ChangeState(tileNodeFSMSelected, tileNodeMock, tilemapMock));
            Assert.DoesNotThrow(() => _levelStateManager.Execute(tileNodeMock, tilemapMock));
        }

        [Test]
        public void CannotWorkWithEmptyState_Test()
        {
            Assert.Throws<ArgumentNullException>(() => _levelStateManager = new LevelStateManager(null, tileNodeMock));
            Assert.Throws<ArgumentNullException>(() => _levelStateManager.ChangeState(null, tileNodeMock, tilemapMock));
        }

        private class MockStatusSelected<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() 
        {
            public void Init(T node, Tilemap tilemap) { }
            public void Execute(T node, ILevelStateManager<T> levelStateManager, Tilemap tilemap) { }
            public void Exit(T node, Tilemap tilemap) { }
            public Type StateType() => this.GetType();
        }

        private class MockStatusIdle<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() 
        {
            public void Init(T node, Tilemap tilemap) { }
            public void Execute(T node, ILevelStateManager<T> levelStateManager, Tilemap tilemap) { }
            public void Exit(T node, Tilemap tilemap) { }
            public Type StateType() => this.GetType();
        }
    }
}