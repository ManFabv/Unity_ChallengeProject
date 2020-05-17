using System;
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
                        tilemap.SetColor(_endTileNode.Position, Color.red);

                        //TODO: calcular el path
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
        }

        public Type StateType() => this.GetType();
    }
}