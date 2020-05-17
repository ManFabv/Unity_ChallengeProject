using System;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Game.LevelManagers.TilemapStatus 
{
    public class TileMap_Status_Idle : Singleton<TileMap_Status_Idle>, ITileMapStatus<TileNode>
    {
        public void Init(TileNode node, Tilemap tilemap) {}

        public void Execute(TileNode node, ILevelStateManager<TileNode> levelStateManager, Tilemap tilemap)
        {
            if (node != null)
                levelStateManager.ChangeState(TileMap_Status_SelectedStart.Instance, node, tilemap);
        }

        public void Exit(TileNode node, Tilemap tilemap)
        {
            tilemap.SetColor(node.Position, Color.red);
        }

        public Type StateType() => this.GetType();
    }
}