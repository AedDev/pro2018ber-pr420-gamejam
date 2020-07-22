using UnityEngine;
using Andification.Runtime.Behaviours.Entities;
using System;

namespace Andification.Runtime.GridSystem
{
    [System.Serializable]
    public class WorldGridCell
    {
        public readonly Vector2Int cellPosition;
        public bool walkable = true;
        public bool buildable = true;
        public GridContentType content = GridContentType.Nothing;
        public BaseEntity containedEntity;

        public WorldGridCell(Vector2Int cellPosition)
        {
            if (cellPosition.x < 0 || cellPosition.y < 0)
                throw new ArgumentOutOfRangeException("x and y cell coordinate need to be a value equal to or greater than 0");

            this.cellPosition = cellPosition;
        }

        public WorldGridCell(int x, int y) : this(new Vector2Int(x, y))
        {
            
        }
    }
}
