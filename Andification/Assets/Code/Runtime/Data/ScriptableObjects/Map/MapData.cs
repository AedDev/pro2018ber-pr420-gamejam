using UnityEngine;
using Andification.Runtime.GridSystem;

namespace Andification.Runtime.Data.ScriptableObjects.Map
{
    [System.Serializable]
    public class MapData : ScriptableObject
    {
        public new string name;

        public GridData gridData;
        public Vector2[] towerPositions;
        public WaveData[] waves;
    }
}