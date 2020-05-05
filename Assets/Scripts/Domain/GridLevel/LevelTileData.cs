using System;
using UnityEngine;

[Serializable]
public class LevelTileData
{
#pragma warning disable 0649
    [SerializeField]
    private Tile[] Tiles;
#pragma warning restore 0649

    public int LevelTilesCount => Tiles?.Length ?? 0;

    public Tile this[int index] {
        get => Tiles[index];
        set => Tiles[index] = value;
    }
}