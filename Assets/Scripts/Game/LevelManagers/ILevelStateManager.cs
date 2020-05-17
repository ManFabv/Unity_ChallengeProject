using System;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers 
{
    public interface ILevelStateManager<T> where T : TileNode, new() 
    {
        void Execute(T node);
        void ChangeState(ITileMapStatus<T> newState, T node);
        Type GetCurrentState();
    }
}
