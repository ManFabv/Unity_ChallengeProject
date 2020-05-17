using System;
using PPop.Domain.Tiles;
using UnityEngine.Tilemaps;
using Zenject;

namespace PPop.Game.LevelManagers 
{
    public class LevelStateManager : ILevelStateManager<TileNode>
    {
        private ITileMapStatus<TileNode> _currentState;

        [Inject]
        public LevelStateManager(ITileMapStatus<TileNode> initialState, TileNode node)
        {
            ChangeState(initialState, node, new Tilemap());
        }

        public void Execute(TileNode node, Tilemap tilemap)
        {
            _currentState.Execute(node, this, tilemap);
        }

        public void ChangeState(ITileMapStatus<TileNode> newState, TileNode node, Tilemap tilemap)
        {
            if (newState is null) throw new ArgumentNullException($"New state {typeof(ITileMapStatus<TileNode>).Name} of parameter {nameof(newState)} is null");

            _currentState?.Exit(node, tilemap);
            _currentState = newState;
            _currentState.Init(node, tilemap);
        }

        public Type GetCurrentState() => _currentState.GetType();
    }
}
