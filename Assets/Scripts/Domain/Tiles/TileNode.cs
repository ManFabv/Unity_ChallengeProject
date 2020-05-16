using System.Collections.Generic;
using PathFinding;

namespace PPop.Domain.Tiles
{
    public class TileNode : IAStarNode 
    {
        public int Cost;
        public string Representation;

        public IEnumerable<IAStarNode> Neighbours => throw new System.NotImplementedException();

        public float CostTo(IAStarNode neighbour)
        {
            throw new System.NotImplementedException();
        }

        public float EstimatedCostTo(IAStarNode target)
        {
            throw new System.NotImplementedException();
        }
    }
}