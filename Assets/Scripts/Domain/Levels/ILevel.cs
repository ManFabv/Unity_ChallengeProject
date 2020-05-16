using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Domain.Levels
{
    public interface ILevel
    {
        void LoadLevel(string pathToLevel);
        void LoadLevel(string rootFolder, int level);
        (Vector3Int[] positions, TileBase[] tiles) GetFilledMap();
        bool IsLoaded { get; }
    }
}