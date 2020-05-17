using PPop.Domain.Tiles;
using UnityEngine;

namespace PPop.Game.LevelManagers.TilemapStatus 
{
    public class TileMap_Status_Idle<T> : TileMapStatusBase<T>, ITileMapStatus<T> where T : TileNode, new() 
    {
        public void Init(T node)
        {
            Debug.Log("Esperando click en inicio");
        }

        public void Execute(T node)
        {
        }

        public void Exit(T node) {}
    }
}