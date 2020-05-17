using System;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers.TilemapStatus 
{
    public class TileMapStatusBase<T> : Singleton<T> where T : TileNode, new()
    {
        public Type StateType() => this.GetType();
    }
}
