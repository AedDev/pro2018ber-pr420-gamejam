using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.GridSystem
{
    [System.Serializable]
    public class WorldGridCell
    {
        public readonly Vector2Int cellPosition;
        public bool walkable = true;
        public bool buildable = true;
        public GridContentType content = GridContentType.Nothing;

        public WorldGridCell(Vector2Int cellPosition)
        {
            this.cellPosition = cellPosition;
        }
    }
}
