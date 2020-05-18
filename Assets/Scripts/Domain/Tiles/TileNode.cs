using System.Collections.Generic;
using PathFinding;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PPop.Domain.Tiles
{
    public class TileNode : TileBase, IAStarNode
    {
        public static Tilemap TileMap;
        public int Cost;
        public string Representation;
        public Vector3Int Position;

        public IEnumerable<IAStarNode> Neighbours => FindNeighbours();

        public float CostTo(IAStarNode neighbour)
        {
            float cost = Cost;
            if (neighbour is TileNode tileNode)
                cost = tileNode.Cost + Cost;

            return cost;
        }

        public float EstimatedCostTo(IAStarNode target)
        {
            return CostTo(target);
        }

        private IEnumerable<IAStarNode> FindNeighbours()
        {
            var positions = new List<Vector3Int>();
            int posXFrom = 0;
            int posXTo = 0;
            if (Position.y%2 == 0)
            {
                posXFrom = Position.x - 1;
                posXTo = Position.x;
            }

            else
            {
                posXFrom = Position.x;
                posXTo = Position.x + 1;
            }

            positions.Add(new Vector3Int(posXFrom, Position.y+1, Position.z));
            positions.Add(new Vector3Int(posXTo, Position.y+1, Position.z));

            positions.Add(new Vector3Int(Position.x - 1, Position.y, Position.z));
            positions.Add(new Vector3Int(Position.x, Position.y, Position.z));
            positions.Add(new Vector3Int(Position.x + 1, Position.y, Position.z));

            positions.Add(new Vector3Int(posXFrom, Position.y - 1, Position.z));
            positions.Add(new Vector3Int(posXTo, Position.y - 1, Position.z));

            List<IAStarNode> neighbours = new List<IAStarNode>();
            
            foreach (var tilePosition in positions)
            {
                var tileNode = TileMap.GetTile<TileNode>(tilePosition);
                
                neighbours.Add(tileNode);
            }

            return neighbours;
        }
    }
}