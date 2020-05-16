using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GridLevel : ILevel 
{
    private readonly ILoaderService _loaderService;
    private readonly IReader _reader;
    private readonly IGameStaticsLevelValues _gameStaticsLevelValues;
    private LevelTileData TilesData = new LevelTileData();

    [Inject]
    public GridLevel(ILoaderService loaderService, IReader reader, IGameStaticsLevelValues gameStaticsLevelValues)
    {
        _loaderService = loaderService;
        _reader = reader;
        _gameStaticsLevelValues = gameStaticsLevelValues;
    }

    public void LoadLevel(string pathToLevel)
    {
        TilesData = _loaderService.Read<LevelTileData>(pathToLevel);
    }

    public void LoadLevel(string rootFolder, int level)
    {
        var pathToFile = $"{rootFolder}\\{_gameStaticsLevelValues.LevelRootName}{level}";
        LoadLevel(pathToFile);
    }

    public (Vector3Int[] positions, TileBase[] tiles) GetFilledMap()
    {
        var MapSize = TilesData.MapSize;
        var mapSizeForArray = MapSize * MapSize;
        var halfMapSize = MapSize / 2;

        var level = TilesData.Level;
        TilesData.Tiles = new List<Tile>();

        var tileValidator = new TileValidator();

        var positionArray = new Vector3Int[mapSizeForArray];
        var tileArray = new TileBase[mapSizeForArray];
        TilesData.Tiles = new List<Tile>();
        
        int mapIndex = 0;
        for (int yPos = halfMapSize; yPos > -halfMapSize; yPos--)
        {
            for (int xPos = -halfMapSize; xPos < halfMapSize; xPos++)
            {
                InitializeTileMapTile(positionArray, mapIndex, xPos, yPos, tileArray, level);
                InitializeTile(level, mapIndex, tileValidator);
                mapIndex++;
            }
        }

        return (positionArray, tileArray);
    }

    private void InitializeTile(List<string> level, int mapIndex, TileValidator tileValidator)
    {
        var tileData = _reader.Read<TileScriptableObject>($"{_gameStaticsLevelValues.LevelRootTilesFolder}\\{level[mapIndex]}");

        var tile = new Tile {Cost = tileData.Cost, Representation = tileData.Representation};
        tileValidator.Validate(tile);

        TilesData.Tiles.Add(tile);
    }

    private void InitializeTileMapTile(Vector3Int[] positionArray, int mapIndex, int xPos, int yPos, TileBase[] tileArray, List<string> level)
    {
        positionArray[mapIndex] = new Vector3Int(xPos, yPos, 0);
        tileArray[mapIndex] = _reader.Read<TileBase>(level[mapIndex]);
    }

    public bool IsLoaded => TilesCount > 0;

    public int TilesCount => TilesData.TilesCount;
}