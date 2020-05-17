using System;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers 
{
    public class LevelStateManager<T> : ILevelStateManager<T> where T : TileNode, new()
    {
        private ITileMapStatus<T> _currentState;

        public LevelStateManager(ITileMapStatus<T> initialState, T node)
        {
            ChangeState(initialState, node);
        }

        public void Execute(T node)
        {
            _currentState.Execute(node);
        }

        public void ChangeState(ITileMapStatus<T> newState, T node)
        {
            if (newState is null) throw new ArgumentNullException($"New state {typeof(ITileMapStatus<T>).Name} of parameter {nameof(newState)} is null");

            _currentState?.Exit(node);
            _currentState = newState;
            _currentState.Init(node);
        }

        public Type GetCurrentState() => _currentState.GetType();
    }
}
