using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class LevelManager : MonoBehaviour 
{
    private Tilemap TileMap;

    [SerializeField] private string LevelRootFolder = "Levels";

    [SerializeField] private int CurrentLevel = 1;

    private ILevel _level;

    [Inject] 
    void Construct(ILevel level)
    {
        _level = level;
    }

    void Start()
    {
        TileMap = GetComponentInChildren<Tilemap>();

        _level.LoadLevel(LevelRootFolder, CurrentLevel);
        
        var map = _level.FillMap();

        TileMap.SetTiles(map.positions, map.tiles);
    }
}