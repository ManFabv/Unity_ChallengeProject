using System;
using PPop.Domain.Tiles;
using UnityEngine.Tilemaps;

namespace PPop.Game.LevelManagers 
{
    public interface ILevelStateManager<T> where T : TileNode, new() 
    {
        void Execute(T node, Tilemap tilemap);
        void ChangeState(ITileMapStatus<T> newState, T node, Tilemap tilemap);
        Type GetCurrentState();
    }
}
