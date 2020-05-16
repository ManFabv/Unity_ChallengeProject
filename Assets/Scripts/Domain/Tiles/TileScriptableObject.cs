using UnityEngine;

namespace PPop.Domain.Tiles
{
    [CreateAssetMenu(fileName = "TileNode", menuName = "ScriptableObjects/TileScriptableObject", order = 1)]
    public class TileScriptableObject : ScriptableObject
    {
        public int Cost;
        public string Representation;
    }
}