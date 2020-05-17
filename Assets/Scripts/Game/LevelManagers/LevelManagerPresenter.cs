﻿using PPop.Domain.Levels;
using PPop.Domain.Tiles;
using PPop.Infrastructure.Helpers.Tilemaps;
using PPops.Domain.Statics.LevelStatics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace PPop.Game.LevelManagers
{
    [RequireComponent(typeof(Grid))]
    public class LevelManagerPresenter : MonoBehaviour 
    {
        private Tilemap TileMap;

        private IGameStaticsLevelValues _gameStaticsLevelValues;

        [SerializeField] private int CurrentLevel = 1;

        private ILevel _level;

        private Camera MainCamera;
        private Grid GridTileMap;
        private ILevelStateManager<TileNode> _levelStateManager;

        private TileNode _selectedTileNode;

        [Inject] 
        void Construct(ILevel level, IGameStaticsLevelValues gameStaticsLevelValues, ILevelStateManager<TileNode> levelStateManager)
        {
            _level = level;
            _gameStaticsLevelValues = gameStaticsLevelValues;
            _levelStateManager = levelStateManager;
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

        void Update()
        {
            _selectedTileNode = null;

            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                var coordinate = GridTileMap.WorldToCell(mouseWorldPos);

                _selectedTileNode = _level.GetTileNodeAtPosition(coordinate);

                _levelStateManager.Execute(_selectedTileNode, TileMap);
            }   
        }
    }
}