using System;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers 
{
    public class TileMap_Status_Idle<T> : Singleton<T>, IFSM<T> where T : TileNode, new()
    {
        public void Init(T node)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(T node)
        {
            throw new System.NotImplementedException();
        }

        public void Exit(T node)
        {
            throw new System.NotImplementedException();
        }

        public Type StateType() => this.GetType();
    }
}
