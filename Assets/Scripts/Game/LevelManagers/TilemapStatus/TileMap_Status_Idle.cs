using System;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using UnityEngine;

namespace PPop.Game.LevelManagers 
{
    public class TileMap_Status_Idle<T> : Singleton<T>, IFSM<T> where T : TileNode, new()
    {
        public void Init(T node) {}

        public void Execute(T node)
        {
            Debug.Log("Esperando click");
        }

        public void Exit(T node) {}

        public Type StateType() => this.GetType();
    }
}
