using PPop.Domain.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Game.GridLevels 
{
    public interface IGridLevelFactory
    {
        void InitializeTile(string[] level, int mapIndex, Vector3Int tilePosition, TileNode[] tiles);

        void InitializeTileMapTile(Vector3Int[] positionArray, int mapIndex, int xPos, int yPos, Tile[] tileBaseArray, string levelRepresentation);
    }
}
