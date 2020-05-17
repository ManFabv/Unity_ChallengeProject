using System;
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

            var tile = ScriptableObject.CreateInstance<TileNode>();

            tile.Cost = tileData.Cost;
            tile.Representation = tileData.Representation;
            tile.Position = positionTile;

            var validations = tileValidator.Validate(tile);

            if (validations.Errors.Count > 0) throw new ArgumentException("Find following errors in tiles: " + string.Join(", ", validations.Errors));

            Tiles.Add(tile);
        }

        public void InitializeTileMapTile(List<Vector3Int> positionArray, int xPos, int yPos, List<TileBase> tileArray, string levelRepresentation)
        {
            positionArray.Add(new Vector3Int(xPos, yPos, 0));
            tileArray.Add(_reader.Read<TileBase>(levelRepresentation));
        }
    }
}
