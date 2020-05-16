using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

[RequireComponent(typeof(Grid))]
public class LevelManager : MonoBehaviour 
{
    private Tilemap TileMap;

    private IGameStaticsLevelValues _gameStaticsLevelValues;

    [SerializeField] private int CurrentLevel = 1;

    private ILevel _level;

    private Camera MainCamera;
    private Grid GridTileMap;

    [Inject] 
    void Construct(ILevel level, IGameStaticsLevelValues gameStaticsLevelValues)
    {
        _level = level;
        _gameStaticsLevelValues = gameStaticsLevelValues;
    }

    void Awake()
    {
        MainCamera = Camera.main;
        TileMap = GetComponentInChildren<Tilemap>();
        GridTileMap = GetComponent<Grid>();
    }

    void Start()
    {
        _level.LoadLevel(_gameStaticsLevelValues.LevelRootFolder, CurrentLevel);
        
        var map = _level.GetFilledMap();

        TileMap.SetTiles(map.positions, map.tiles);
        TileMap.ClearAllTileMapFlags(map.positions);
    }

    //TODO: only for testing purposes. Remove
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = GridTileMap.WorldToCell(mouseWorldPos);
            TileMap.SetColor(coordinate, Color.red);
        }
    }
}