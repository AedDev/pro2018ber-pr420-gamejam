using System;
using UnityEngine;

namespace Andification.Runtime.GridSystem
{
    public class WorldGrid : MonoBehaviour
    {
        public Vector2Int worldSize = Vector2Int.zero;
        public Vector2 cellSize = Vector2.one;

        private WorldGridCell[] cellMap;

        private void OnValidate()
        {
            if (worldSize.x < 1)
                worldSize.x = 1;

            if (worldSize.y < 1)
                worldSize.y = 1;

            if (cellSize.x < 1)
                cellSize.x = 1;

            if (cellSize.y < 1)
                cellSize.y = 1;
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
