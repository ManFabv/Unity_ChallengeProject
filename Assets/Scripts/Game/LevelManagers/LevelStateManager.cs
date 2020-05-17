using System;
using PPop.Domain.Tiles;
using Zenject;

namespace PPop.Game.LevelManagers 
{
    public class LevelStateManager : ILevelStateManager
    {
        private ITileMapStatus<TileNode> _currentState;

        [Inject]
        public LevelStateManager(ITileMapStatus<TileNode> initialState, TileNode node)
        {
            ChangeState(initialState, node);
        }

        public void Execute(TileNode node)
        {
            _currentState.Execute(node, this);
        }

        public void ChangeState(ITileMapStatus<TileNode> newState, TileNode node)
        {
            if (newState is null) throw new ArgumentNullException($"New state {typeof(ITileMapStatus<TileNode>).Name} of parameter {nameof(newState)} is null");

            _currentState?.Exit(node);
            _currentState = newState;
            _currentState.Init(node);
        }

        public Type GetCurrentState() => _currentState.GetType();
    }
}
