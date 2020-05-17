using System;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers
{
    public interface ITileMapStatus<T> where T : TileNode, new()
    {
        void Init(T node);
        void Execute(T node, ILevelStateManager<T> levelStateManager);
        void Exit(T node);
        Type StateType();
    }
}
