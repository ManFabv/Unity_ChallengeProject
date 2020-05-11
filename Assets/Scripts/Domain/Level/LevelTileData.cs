using System.Collections.Generic;

public class LevelTileData
{
#pragma warning disable 0649
    public List<Tile> Tiles;
    public List<string> Level;
    public int LevelNumber;
    public int MapSize;
#pragma warning restore 0649

    public int TilesCount => Tiles?.Count ?? MapSize*MapSize;
    public Tile this[int index] {
        get => Tiles[index];
        set => Tiles[index] = value;
    }
}