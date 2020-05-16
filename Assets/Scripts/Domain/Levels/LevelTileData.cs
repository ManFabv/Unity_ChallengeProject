using System.Collections.Generic;
using PPop.Domain.Tiles;

namespace PPop.Domain.Levels
{
    public class LevelTileData
    {
#pragma warning disable 0649
        public List<TileNode> Tiles = new List<TileNode>();
        public List<string> Level;
        public int LevelNumber;
        public int MapSize;
#pragma warning restore 0649

        public int TilesCount => Tiles.Count;
        public TileNode this[int index] {
            get => Tiles[index];
            set => Tiles[index] = value;
        }
    }
}