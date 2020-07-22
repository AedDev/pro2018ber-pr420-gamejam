using System;
using UnityEngine;
using Andification.Runtime.Extensions;

namespace Andification.Runtime.GridSystem
{
    public class WorldGrid : MonoBehaviour
    {
        public Vector2Int worldSize = Vector2Int.zero;
        public Vector2 cellSize = Vector2.one;
        [Space]
        public bool drawGrid = true;

        private WorldGridCell[] cellMap;

        private void OnDrawGizmosSelected()
        {
            if (drawGrid)
                DrawEditorGrid();
        }

        private void OnValidate()
        {
            ClampProperties();
        }

        private void Awake()
        {
            InitializeGrid();
            ClampProperties();
        }

        private void DrawEditorGrid()
        {
            Gizmos.color = Color.cyan;

            // Horizontal Lines
            for (int i = 0; i < worldSize.y + 1; i++)
            {
                Vector2 startPos = transform.position + new Vector3(transform.position.x, i * cellSize.y);
                Vector2 endPos = transform.position + new Vector3(worldSize.x * worldSize.x, i * cellSize.y);

                Gizmos.DrawLine(startPos, endPos);
            }

            // Vertical Lines
            for (int i = 0; i < worldSize.y + 1; i++)
            {
                Vector2 startPos = transform.position + new Vector3(i * cellSize.x, transform.position.y);
                Vector2 endPos = transform.position + new Vector3(i * cellSize.x, worldSize.x * worldSize.x);

                Gizmos.DrawLine(startPos, endPos);
            }
        }

        private void ClampProperties()
        {
            worldSize.Clamp(Vector2Int.one, Vector2Int.one * int.MaxValue);
            cellSize.Clamp(Vector2.one, Vector2.one * float.MaxValue);

            var localScale = transform.localScale;
            localScale.Clamp(Vector3.one, Vector3.one);
            transform.localScale = localScale;
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
