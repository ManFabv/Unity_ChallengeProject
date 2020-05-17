using System;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers
{
    public interface ITileMapStatus<in T> where T : TileNode, new()
    {
        void Init(T node);
        void Execute(T node, ILevelStateManager levelStateManager);
        void Exit(T node);
        Type StateType();
    }
}
