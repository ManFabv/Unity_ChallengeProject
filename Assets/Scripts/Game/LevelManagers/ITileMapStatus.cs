using System;
using PPop.Domain.Tiles;
using UnityEngine.Tilemaps;

namespace PPop.Game.LevelManagers
{
    public interface ITileMapStatus<T> where T : TileNode, new()
    {
        void Init(T node, Tilemap tilemap);
        void Execute(T node, ILevelStateManager<T> levelStateManager, Tilemap tilemap);
        void Exit(T node, Tilemap tilemap);
        Type StateType();
    }
}
