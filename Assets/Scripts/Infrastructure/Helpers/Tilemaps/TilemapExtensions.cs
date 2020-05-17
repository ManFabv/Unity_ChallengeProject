using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Infrastructure.Helpers.Tilemaps
{
    public static class TilemapExtensions
    {
        public static void SetAllTilemapFlags(this Tilemap sourceTilemap, Vector3Int[] tilePositions, TileFlags flags)
        {
            foreach (var tilePosition in tilePositions)
            {
                sourceTilemap.SetTileFlags(tilePosition, flags);
            }
        }

        public static void ClearAllTileMapFlags(this Tilemap sourceTilemap, Vector3Int[] tilePositions)
        {
            SetAllTilemapFlags(sourceTilemap, tilePositions, TileFlags.None);
        }
    }
}
