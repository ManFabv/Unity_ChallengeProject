using System;
using System.Collections.Generic;
using PathFinding;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Game.LevelManagers.TilemapStatus 
{
    public class TileMap_Status_SelectedStart : Singleton<TileMap_Status_SelectedStart>, ITileMapStatus<TileNode>
    {
        private TileNode _startTileNode;
        private TileNode _endTileNode;
        private IList<IAStarNode> _path;

        public void Init(TileNode node, Tilemap tilemap)
        {
            _startTileNode = node;
        }

        public void Execute(TileNode node, ILevelStateManager<TileNode> levelStateManager, Tilemap tilemap)
        {
            if (node != null)
            {
                if(node == _startTileNode)
                    levelStateManager.ChangeState(TileMap_Status_Idle.Instance, node, tilemap);
                else
                {
                    if (node != _endTileNode)
                    {
                        if(_endTileNode != null) tilemap.SetColor(_endTileNode.Position, Color.white);

                        _endTileNode = node;

                        //TODO: this should be correctly implemented
                        //_path = GetTilesPath();
                        //InitializeColorForPath(_path, tilemap);
                        //TODO: this is only until we implement the path correctly
                        tilemap.SetColor(_startTileNode.Position, Color.green);
                        tilemap.SetColor(_endTileNode.Position, Color.green);
                    }
                    else
                        levelStateManager.ChangeState(TileMap_Status_Idle.Instance, node, tilemap);
                }
            }
        }

        public void Exit(TileNode node, Tilemap tilemap)
        {
            tilemap.SetColor(_startTileNode.Position, Color.white);
            tilemap.SetColor(_endTileNode.Position, Color.white);

            if (_path != null)
            {
                foreach (var tile in _path)
                {
                    if (tile is TileNode tileNode)
                        tilemap.SetColor(tileNode.Position, Color.white);
                }
            }
        }

        public Type StateType() => this.GetType();

        private IList<IAStarNode> GetTilesPath()
        {
            if (_startTileNode is null) throw new ArgumentException($"{nameof(_startTileNode)} is null");
            if (_endTileNode is null) throw new ArgumentException($"{nameof(_endTileNode)} is null");

            return AStar.GetPath(_startTileNode, _endTileNode);
        }

        private void InitializeColorForPath(IList<IAStarNode> path, Tilemap tilemap)
        {
            if(path is null) throw new ArgumentException($"{nameof(path)} is null");

            foreach (var tile in path)
            {
                if(tile is TileNode tileNode)
                    tilemap.SetColor(tileNode.Position, Color.red);
            }

            tilemap.SetColor(_startTileNode.Position, Color.green);
            tilemap.SetColor(_endTileNode.Position, Color.green);
        }
    }
}