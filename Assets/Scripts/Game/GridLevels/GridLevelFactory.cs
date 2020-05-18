using System;
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

        public void InitializeTile(string[] level, int mapIndex, Vector3Int tilePosition, TileNode[] tiles)
        {
            var tileData = _reader.Read<TileScriptableObject>($"{_gameStaticsLevelValues.LevelRootTilesFolder}\\{level[mapIndex]}");

            var tile = ScriptableObject.CreateInstance<TileNode>();

            tile.Cost = tileData.Cost;
            tile.Representation = tileData.Representation;
            tile.Position = tilePosition;

            var tileValidator = new TileValidator();
            var validations = tileValidator.Validate(tile);
            if (validations.Errors.Count > 0) throw new ArgumentException("Find following errors in tiles: " + string.Join(", ", validations.Errors));

            tiles[mapIndex] = tile;
        }

        public void InitializeTileMapTile(Vector3Int[] positionArray, int mapIndex, int xPos, int yPos, Tile[] tileBaseArray, string levelRepresentation)
        {
            positionArray[mapIndex] = new Vector3Int(xPos, yPos, 0);
            tileBaseArray[mapIndex] = _reader.Read<Tile>(levelRepresentation);
        }
    }
}
