using System;
using Moq;
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
    public class LevelStateManagerTests : ZenjectUnitTestFixture
    {
        private MockStatusSelected<TileNode> tileNodeFSMSelected;
        private TileNode tileNodeMock;
        private MockStatusIdle<TileNode> tileNodeFSMMockIdle;
        private ILevelStateManager<TileNode> levelStateManagerMock;
        private Tilemap tilemapMock;

        [SetUp]
        public void CommonInstall()
        {
            tileNodeFSMSelected = new MockStatusSelected<TileNode>();
            tileNodeMock = ScriptableObject.CreateInstance<TileNode>(); ;
            tileNodeFSMMockIdle = new MockStatusIdle<TileNode>();
            levelStateManagerMock = new Mock<ILevelStateManager<TileNode>>().Object;
            tilemapMock = new Tilemap();
        }

        [Test]
        public void CanExecuteStepsOfStatus_Test()
        {
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Init(tileNodeMock, tilemapMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Execute(tileNodeMock, levelStateManagerMock, tilemapMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Exit(tileNodeMock, tilemapMock));
        }

        [Test]
        public void CanChangeStatus_Test()
        {
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Init(tileNodeMock, tilemapMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Execute(tileNodeMock, levelStateManagerMock, tilemapMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Exit(tileNodeMock, tilemapMock));

            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Init(tileNodeMock, tilemapMock));
            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Execute(tileNodeMock, levelStateManagerMock, tilemapMock));
            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Exit(tileNodeMock, tilemapMock));

            Assert.AreNotEqual(tileNodeFSMMockIdle.StateType(), tileNodeFSMSelected.StateType());
        }

        private class MockStatusSelected<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() {
            public void Init(T node, Tilemap tilemap) { }
            public void Execute(T node, ILevelStateManager<T> levelStateManager, Tilemap tilemap) { }
            public void Exit(T node, Tilemap tilemap) { }
            public Type StateType() => this.GetType();
        }

        private class MockStatusIdle<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() {
            public void Init(T node, Tilemap tilemap) { }
            public void Execute(T node, ILevelStateManager<T> levelStateManager, Tilemap tilemap) { }
            public void Exit(T node, Tilemap tilemap) { }
            public Type StateType() => this.GetType();
        }
    }
}