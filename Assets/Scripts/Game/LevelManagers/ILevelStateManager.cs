using System;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers 
{
    public interface ILevelStateManager
    {
        void Execute(TileNode node);
        void ChangeState(ITileMapStatus<TileNode> newState, TileNode node);
        Type GetCurrentState();
    }
}
