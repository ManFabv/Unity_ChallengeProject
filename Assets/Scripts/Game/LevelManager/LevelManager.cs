using System.Collections.Generic;
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
        //LevelTileData tiles = new LevelTileData();
        //tiles.MapSize = 8;
        //tiles.LevelNumber = 1;
        //tiles.Level = new List<string>();
        //tiles.Level.Add("desert");
        //Debug.Log(JsonUtility.ToJson(tiles));

        TileMap = GetComponentInChildren<Tilemap>();

        _level.LoadLevel(LevelRootFolder, CurrentLevel);
        _level.FillMap<Tilemap>(ref TileMap);

        //TileMap.SetTiles();

        //int mapSizeForArray = MapSize * MapSize;
        //int halfMapSize = MapSize / 2;

        //var positionArray = new Vector3Int[mapSizeForArray];
        //var tileArray = new TileBase[mapSizeForArray];

        //int mapIndex = 0;
        //for (int yPos = halfMapSize; yPos > -halfMapSize; yPos--)
        //{
        //    for (int xPos = -halfMapSize; xPos < halfMapSize; xPos++)
        //    {
        //        positionArray[mapIndex] = new Vector3Int(xPos, yPos, 0);
        //        tileArray[mapIndex] = Tile;
        //        mapIndex++;
        //    }
        //}

        //TileMap.SetTiles(positionArray, tileArray);
    }
}