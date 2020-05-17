﻿using System.Collections.Generic;
using PPop.Domain.Tiles;
using PPop.Infrastructure.Validators.Validators;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Game.GridLevels 
{
    public interface IGridLevelFactory
    {
        void InitializeTile(List<string> level, int mapIndex, TileValidator tileValidator, Vector3Int positionTile, List<TileNode> Tiles);

        void InitializeTileMapTile(List<Vector3Int> positionArray, int xPos, int yPos, List<TileBase> tileArray, string levelRepresentation);
    }
}
