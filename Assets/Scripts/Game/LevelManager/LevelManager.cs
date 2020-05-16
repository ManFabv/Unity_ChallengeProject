using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

[RequireComponent(typeof(Grid))]
public class LevelManager : MonoBehaviour 
{
    private Tilemap TileMap;

    [SerializeField] private string LevelRootFolder = "Levels";

    [SerializeField] private int CurrentLevel = 1;

    private ILevel _level;

    private Camera MainCamera;
    private Grid GridTileMap;

    [Inject] 
    void Construct(ILevel level)
    {
        _level = level;
    }

    void Awake()
    {
        MainCamera = Camera.main;
        TileMap = GetComponentInChildren<Tilemap>();
        GridTileMap = GetComponent<Grid>();
    }

    void Start()
    {
        _level.LoadLevel(LevelRootFolder, CurrentLevel);
        
        var map = _level.GetFilledMap();

        TileMap.SetTiles(map.positions, map.tiles);
    }

    //TODO: only for testing purposes. Remove
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = GridTileMap.WorldToCell(mouseWorldPos);
            var selectedTile = TileMap.GetTile(coordinate);
        }
    }
}