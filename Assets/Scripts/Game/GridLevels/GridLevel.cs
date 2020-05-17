using System;
using System.Collections.Generic;
using PPop.Domain.Levels;
using PPop.Domain.Tiles;
using PPop.Infrastructure.Services.Loader;
using PPop.Infrastructure.Validators.Validators;
using PPops.Domain.Statics.LevelStatics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace PPop.Game.GridLevels
{
    public class GridLevel : ILevel 
    {
        private readonly ILoaderService _loaderService;
        
        private readonly IGameStaticsLevelValues _gameStaticsLevelValues;
        private readonly IGridLevelFactory _gridLevelFactory;
        private LevelTileData TilesData = new LevelTileData();

        [Inject]
        public GridLevel(ILoaderService loaderService, IGameStaticsLevelValues gameStaticsLevelValues, IGridLevelFactory gridLevelFactory)
        {
            _loaderService = loaderService;
            _gameStaticsLevelValues = gameStaticsLevelValues;
            _gridLevelFactory = gridLevelFactory;
        }

        public void LoadLevel(string pathToLevel)
        {
            try
            {
                TilesData = _loaderService.Read<LevelTileData>(pathToLevel);
            }
            catch (NullReferenceException)
            {
                throw new ArgumentException($"Cannot read level file {pathToLevel}");
            }
        }

        public void LoadLevel(string rootFolder, int level)
        {
            var pathToFile = $"{rootFolder}\\{_gameStaticsLevelValues.LevelRootName}{level}";
            LoadLevel(pathToFile);
        }

        public (Vector3Int[] positions, TileBase[] tiles) GetFilledMap()
        {
            var MapSize = TilesData.MapSize;
            var halfMapSize = MapSize / 2;

            var level = TilesData.Level;
            TilesData.Tiles = new List<TileNode>();

            var tileValidator = new TileValidator();

            var positionArray = new List<Vector3Int>();
            var tileArray = new List<TileBase>();
            TilesData.Tiles = new List<TileNode>();
        
            int mapIndex = 0;
            for (int yPos = halfMapSize; yPos > -halfMapSize; yPos--)
            {
                for (int xPos = -halfMapSize; xPos < halfMapSize; xPos++)
                {
                    _gridLevelFactory.InitializeTileMapTile(positionArray, xPos, yPos, tileArray, level[mapIndex]);
                    _gridLevelFactory.InitializeTile(level, mapIndex, tileValidator, new Vector3Int(xPos, yPos, 0), TilesData.Tiles);
                    mapIndex++;
                }
            }
            
            return (positionArray.ToArray(), tileArray.ToArray());
        }

        public bool IsLoaded => TilesCount > 0;

        public int TilesCount => TilesData.TilesCount;
    }
}