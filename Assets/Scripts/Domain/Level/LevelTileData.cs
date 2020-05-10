public class LevelTileData
{
#pragma warning disable 0649
    public Tile[] Tiles;
#pragma warning restore 0649

    public Tile this[int index] {
        get => Tiles[index];
        set => Tiles[index] = value;
    }
}