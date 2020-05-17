using System;

namespace PPop.Game.LevelManagers.TilemapStatus
{
    public interface IFSM<in T>
    {
        void Init(T node);
        void Execute(T node);
        void Exit(T node);
        Type StateType();
    }
}
