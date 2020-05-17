using PPop.Domain.Tiles;

namespace PPop.Domain.Levels
{
    public class LevelTileData
    {
#pragma warning disable 0649
        public TileNode[] TileNodes = new TileNode[0];
        public string[] Level;
        public int LevelNumber;
        public int MapSize;
#pragma warning restore 0649

        public int TilesCount => TileNodes.Length;
        public TileNode this[int index] {
            get => TileNodes[index];
            set => TileNodes[index] = value;
        }
    }
}