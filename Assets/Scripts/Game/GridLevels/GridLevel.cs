﻿using System;
using System.Linq;
using PPop.Domain.Levels;
using PPop.Domain.Tiles;
using PPop.Infrastructure.Services.Loader;
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
            var mapSize = TilesData.MapSize;
            var halfMapSize = mapSize / 2;
            var fullMapSize = mapSize * mapSize;

            var level = TilesData.Level;
            
            var tileNodeArray = new TileNode[fullMapSize];
            var tilePositionArray = new Vector3Int[fullMapSize];
            var tileBaseArray = new TileBase[fullMapSize];
        
            int mapIndex = 0;
            for (int yPos = halfMapSize; yPos > -halfMapSize; yPos--)
            {
                for (int xPos = -halfMapSize; xPos < halfMapSize; xPos++)
                {
                    _gridLevelFactory.InitializeTileMapTile(tilePositionArray, mapIndex, xPos, yPos, tileBaseArray, level[mapIndex]);
                    _gridLevelFactory.InitializeTile(level, mapIndex, new Vector3Int(xPos, yPos, 0), tileNodeArray);
                    mapIndex++;
                }
            }

            TilesData.TileNodes = tileNodeArray;
            return (tilePositionArray, tileBaseArray);
        }

        public TileNode GetTileNodeAtPosition(Vector3Int tilePosition)
        {
            return TilesData?.TileNodes?.FirstOrDefault(tile => tile.Position == tilePosition);
        }

        public bool IsLoaded => TilesCount > 0;

        public int TilesCount => TilesData.TilesCount;
    }
}