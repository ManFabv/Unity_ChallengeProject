using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GridLevel : ILevel 
{
    private readonly ILoaderService _loaderService;
    private LevelTileData TilesData = new LevelTileData();

    [Inject]
    public GridLevel(ILoaderService loaderService)
    {
        _loaderService = loaderService;
    }

    public void LoadLevel(string pathToLevel)
    {
        TilesData = _loaderService.Read<LevelTileData>(pathToLevel);
    }

    public void LoadLevel(string rootFolder, int level)
    {
        var pathToFile = $"{rootFolder}\\Level_{level}";
        LoadLevel(pathToFile);
    }

    public (Vector3Int[] positions, TileBase[] tiles) FillMap()
    {
        var MapSize = TilesData.MapSize;
        var mapSizeForArray = MapSize * MapSize;
        var halfMapSize = MapSize / 2;

        var level = TilesData.Level;
        TilesData.Tiles = new List<Tile>();

        var tileValidator = new TileValidator();

        int tileIndex = 0;
        TilesData.Tiles = new List<Tile>();
        foreach (var levelTile in level)
        {
            var tileData = Resources.Load<TileScriptableObject>($"Tiles\\{levelTile}");

            var tile = new Tile { Cost = tileData.Cost, Representation = tileData.Representation };
            tileValidator.Validate(tile);

            TilesData.Tiles.Add(tile);
            
            tileIndex++;
        }

        var positionArray = new Vector3Int[mapSizeForArray];
        var tileArray = new TileBase[mapSizeForArray];

        int mapIndex = 0;
        for (int yPos = halfMapSize; yPos > -halfMapSize; yPos--)
        {
            for (int xPos = -halfMapSize; xPos < halfMapSize; xPos++)
            {
                positionArray[mapIndex] = new Vector3Int(xPos, yPos, 0);
                tileArray[mapIndex] = Resources.Load<TileBase>(level[mapIndex]);
                
                mapIndex++;
            }
        }

        return (positionArray, tileArray);
    }

    public bool IsLoaded => TilesCount > 0;

    public int TilesCount => TilesData.TilesCount;
}