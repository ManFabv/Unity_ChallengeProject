﻿using System;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;
using UnityEngine;

namespace PPop.Game.LevelManagers.TilemapStatus 
{
    public class TileMap_Status_SelectedStart : Singleton<TileMap_Status_SelectedStart>, ITileMapStatus<TileNode> 
    {
        public void Init(TileNode node)
        {
            Debug.Log("TileMap_Status_SelectedStart");
        }

        public void Execute(TileNode node, ILevelStateManager<TileNode> levelStateManager)
        {
            if (node != null)
                levelStateManager.ChangeState(TileMap_Status_SelectedStart.Instance, node);
        }

        public void Exit(TileNode node)
        {
        }

        public Type StateType() => this.GetType();
    }
}