using UnityEngine;
using UnityEngine.Tilemaps;

public interface ILevel
{
    void LoadLevel(string pathToLevel);
    void LoadLevel(string rootFolder, int level);
    (Vector3Int[] positions, TileBase[] tiles) FillMap();
    bool IsLoaded { get; }
}