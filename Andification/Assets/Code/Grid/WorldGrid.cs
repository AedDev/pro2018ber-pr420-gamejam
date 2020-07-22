using System;
using UnityEngine;

namespace Andification.Runtime.GridSystem
{
    public class WorldGrid : MonoBehaviour
    {
        private Vector2Int worldSize = Vector2Int.zero;

        private WorldGridCell[] cellMap;

        private void OnValidate()
        {
            if (worldSize.x < 1)
                worldSize.x = 1;

            if (worldSize.y < 1)
                worldSize.y = 1;
        }

        private void Awake()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            cellMap = new WorldGridCell[worldSize.x * worldSize.y];
        }

        public WorldGridCell GetCellAt(Vector2Int cellPosition)
        {
            throw new NotImplementedException();
        }

        public Vector2 CellToWorld(Vector2Int cellPosition)
        {
            throw new NotImplementedException();
        }

        public Vector2Int WorldToCell(Vector2 worldPosition)
        {
            throw new NotImplementedException();
        }
    }
}
