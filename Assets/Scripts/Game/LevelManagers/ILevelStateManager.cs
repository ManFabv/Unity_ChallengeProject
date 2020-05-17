using System;
using PPop.Game.LevelManagers.TilemapStatus;

namespace PPop.Game.LevelManagers 
{
    public interface ILevelStateManager<T>
    {
        void Execute(T node);
        void ChangeState(IFSM<T> newState, T node);
        Type GetCurrentState();
    }
}
