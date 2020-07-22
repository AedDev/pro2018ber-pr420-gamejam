﻿using System;
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

        public bool Initialized
        {
            get;
            private set;
        } = false;

        private WorldGridCell[] _cellMap;
        private WorldGridCell[,] CellMap2D
        {
            get => _cellMap.ToTwoDimensional(worldSize.x, worldSize.y);
        }

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

        private void Update()
        {
            


        }

        private void OnGUI()
        {
            var mPos = Input.mousePosition;
            var cam = Camera.main;
            var camWPos = cam.ScreenToWorldPoint(mPos);
            var cPos = WorldToCell(camWPos);
            var wPos = CellToWorld(cPos);

            GUI.Label(new Rect(20, 20, 300, 20), $"World Positon: {camWPos}");
            GUI.Label(new Rect(20, 40, 300, 20), $"World2Cell: {cPos}");
            GUI.Label(new Rect(20, 60, 300, 20), $"Cell2World: {wPos}");
        }

        // TODO => transform.position is not applied correctly
        private void DrawEditorGrid()
        {
            Gizmos.color = Color.cyan;

            // Horizontal Lines
            for (int i = 0; i < worldSize.y + 1; i++)
            {
                Vector2 startPos = transform.position + new Vector3(transform.position.x, i * cellSize.y);
                Vector2 endPos = transform.position + new Vector3((worldSize.x * cellSize.x) + transform.position.x, i * cellSize.y);

                Gizmos.DrawLine(startPos, endPos);
            }

            // Vertical Lines
            for (int i = 0; i < worldSize.x + 1; i++)
            {
                Vector2 startPos = transform.position + new Vector3(i * cellSize.x, transform.position.y);
                Vector2 endPos = transform.position + new Vector3(i * cellSize.x, (worldSize.y * cellSize.y) + transform.position.y);

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
            var cellMap2D = new WorldGridCell[worldSize.x, worldSize.y];

            // Initialize cells
            for (int x = 0; x < cellMap2D.GetLength(0); x++)
                for (int y = 0; y < cellMap2D.GetLength(1); y++)
                    cellMap2D[x, y] = new WorldGridCell(new Vector2Int(x, y));

            Initialized = true;
        }

        public WorldGridCell GetCellAt(Vector2Int cellPosition)
        {
            if (cellPosition.x < 0 || cellPosition.x > worldSize.x)
                return null;

            if (cellPosition.y < 0 || cellPosition.y > worldSize.y)
                return null;
            
            return CellMap2D[cellPosition.x, cellPosition.y];
        }

        public Vector2 CellToWorld(Vector2Int cellPosition)
        {
            return transform.position + new Vector3((cellPosition.x * cellSize.x) + cellSize.x / 2, (cellPosition.y * cellSize.y) + cellSize.y / 2);
        }

        // TODO => Test if transform.position is applied correctly
        public Vector2Int WorldToCell(Vector2 worldPosition)
        {
            Vector2Int a = new Vector2Int((int)(transform.position.x - worldPosition.x + cellSize.x / cellSize.x), (int)(transform.position.y - worldPosition.y + cellSize.y / cellSize.y));
            a.Clamp(Vector2Int.zero, Vector2Int.one * int.MaxValue);
            return a;
            //throw new NotImplementedException();
        }
    }
}