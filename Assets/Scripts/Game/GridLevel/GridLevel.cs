using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GridLevel : ILevel
{
    private readonly ILoaderService _loaderService;
    private LevelTileData TilesData;

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

    public void FillMap<T>(ref T map)
    {
        if (map is Tilemap TileMap)
        {
            var MapSize = TilesData.MapSize;
            int mapSizeForArray = MapSize * MapSize;
            int halfMapSize = MapSize / 2;
            var representations = TilesData.Representations;
            var tiles = TilesData.Tiles;

            var positionArray = new Vector3Int[mapSizeForArray];
            var tileArray = new TileBase[mapSizeForArray];

            int mapIndex = 0;
            for (int yPos = halfMapSize; yPos > -halfMapSize; yPos--)
            {
                for (int xPos = -halfMapSize; xPos < halfMapSize; xPos++)
                {
                    positionArray[mapIndex] = new Vector3Int(xPos, yPos, 0);
                    tileArray[mapIndex] = Resources.Load<TileBase>(representations[mapIndex]);
                    mapIndex++;
                }
            }
            
            TileMap.SetTiles(positionArray, tileArray);
        }
    }

    public bool IsLoaded => TilesCount > 0;

    public int TilesCount => TilesData?.Tiles?.Length ?? 0;
}