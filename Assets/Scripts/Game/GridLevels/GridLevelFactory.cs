using System.Collections.Generic;
using PPop.Domain.Tiles;
using PPop.Infrastructure.Helpers.FileAndDirectory;
using PPop.Infrastructure.Validators.Validators;
using PPops.Domain.Statics.LevelStatics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace PPop.Game.GridLevels 
{
    public class GridLevelFactory : IGridLevelFactory 
    {
        private readonly IReader _reader;
        private readonly IGameStaticsLevelValues _gameStaticsLevelValues;

        [Inject]
        public GridLevelFactory(IReader reader, IGameStaticsLevelValues gameStaticsLevelValues)
        {
            _reader = reader;
            _gameStaticsLevelValues = gameStaticsLevelValues;
        }

        public void InitializeTile(List<string> level, int mapIndex, TileValidator tileValidator, Vector3Int positionTile, List<TileNode> Tiles)
        {
            var tileData = _reader.Read<TileScriptableObject>($"{_gameStaticsLevelValues.LevelRootTilesFolder}\\{level[mapIndex]}");

            var tile = new TileNode { Cost = tileData.Cost, Representation = tileData.Representation, Position = positionTile };
            tileValidator.Validate(tile);

            Tiles.Add(tile);
        }

        public void InitializeTileMapTile(Vector3Int[] positionArray, int mapIndex, int xPos, int yPos, TileBase[] tileArray, List<string> level)
        {
            positionArray[mapIndex] = new Vector3Int(xPos, yPos, 0);
            tileArray[mapIndex] = _reader.Read<TileBase>(level[mapIndex]);
        }
    }
}
