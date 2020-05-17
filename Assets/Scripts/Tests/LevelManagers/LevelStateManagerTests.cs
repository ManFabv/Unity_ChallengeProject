using System;
using Moq;
using Zenject;
using NUnit.Framework;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using PPop.Game.LevelManagers;
using UnityEngine;

namespace PPop.Tests.LevelManagers
{
    [TestFixture]
    public class LevelStateManagerTests : ZenjectUnitTestFixture
    {
        private MockStatusSelected<TileNode> tileNodeFSMSelected;
        private TileNode tileNodeMock;
        private MockStatusIdle<TileNode> tileNodeFSMMockIdle;
        private ILevelStateManager<TileNode> levelStateManagerMock;

        [SetUp]
        public void CommonInstall()
        {
            tileNodeFSMSelected = new MockStatusSelected<TileNode>();
            tileNodeMock = ScriptableObject.CreateInstance<TileNode>(); ;
            tileNodeFSMMockIdle = new MockStatusIdle<TileNode>();
            levelStateManagerMock = new Mock<ILevelStateManager<TileNode>>().Object;
        }

        [Test]
        public void CanExecuteStepsOfStatus_Test()
        {
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Init(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Execute(tileNodeMock, levelStateManagerMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Exit(tileNodeMock));
        }

        [Test]
        public void CanChangeStatus_Test()
        {
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Init(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Execute(tileNodeMock, levelStateManagerMock));
            Assert.DoesNotThrow(() => tileNodeFSMSelected.Exit(tileNodeMock));

            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Init(tileNodeMock));
            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Execute(tileNodeMock, levelStateManagerMock));
            Assert.DoesNotThrow(() => tileNodeFSMMockIdle.Exit(tileNodeMock));

            Assert.AreNotEqual(tileNodeFSMMockIdle.StateType(), tileNodeFSMSelected.StateType());
        }

        private class MockStatusSelected<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() {
            public void Init(T node) { }
            public void Execute(T node, ILevelStateManager<T> levelStateManager) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }

        private class MockStatusIdle<T> : Singleton<T>, ITileMapStatus<T> where T : TileNode, new() {
            public void Init(T node) { }
            public void Execute(T node, ILevelStateManager<T> levelStateManager) { }
            public void Exit(T node) { }
            public Type StateType() => this.GetType();
        }
    }
}