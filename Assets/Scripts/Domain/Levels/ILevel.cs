using PPop.Domain.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Domain.Levels
{
    public interface ILevel
    {
        void LoadLevel(string pathToLevel);
        void LoadLevel(string rootFolder, int level);
        (Vector3Int[] positions, Tile[] tiles) GetFilledMap();
        TileNode GetTileNodeAtPosition(Vector3Int tilePosition);
        bool IsLoaded { get; }
    }
}