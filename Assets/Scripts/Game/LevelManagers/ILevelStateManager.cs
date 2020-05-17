using System;

namespace PPop.Game.LevelManagers 
{
    public interface ILevelStateManager<T>
    {
        void Execute(T node);
        void ChangeState(ITileMapStatus<T> newState, T node);
        Type GetCurrentState();
    }
}
