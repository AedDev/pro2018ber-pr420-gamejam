﻿using UnityEngine;
using System;
using Andification.Runtime.Extensions;
using Andification.Runtime.Data.ScriptableObjects.Map;

namespace Andification.Runtime.GridSystem
{
    public class WorldGrid : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GridData gridData;

        [Space]
        [Header("Debug Stuff")]
        public bool editMode = true;
        public bool drawGrid = true;
        private Color gridColor = Color.cyan;

        public event EventHandler<WorldGridCell> cellChanged;
        public GridData GridDataReference
        {
            get => gridData;
            set => LoadGridData(value);
        }

        private void OnDrawGizmos()
        {
            if (drawGrid)
                DrawEditorGrid();
        }

        private void OnValidate()
        {
            ClampScaling();
        }

        private void Awake()
        {
            ClampScaling();
        }

        private void OnDisable()
        {
            UnloadGrid();
        }

        private void OnGUI()
        {
            if (editMode)
            {
                var mPos = Input.mousePosition;
                var cam = Camera.main;
                var camWPos = cam.ScreenToWorldPoint(mPos);
                var cPos = WorldToCell(camWPos);
                var cell = GetCellAt(cPos);
                var wPos = cell != null ? cell.CellPosition : Vector2Int.zero;

                GUI.Label(new Rect(20, 20, 300, 20), $"World Positon: {camWPos}");
                GUI.Label(new Rect(20, 40, 300, 20), $"World2Cell: {cPos}");
                GUI.Label(new Rect(20, 60, 300, 20), $"Cell2World: {(cell != null ? wPos.ToString() : "No valid cell!")}");
            }
        }

        private void DrawEditorGrid()
        {
            Gizmos.color = gridColor;

            if (!gridData)
                return;

            // Horizontal Lines
            for (int i = 0; i < gridData.WorldSize.y + 1; i++)
            {
                Vector2 startPos = new Vector3(transform.position.x, (i * gridData.CellSize.y) + transform.position.y);
                Vector2 endPos = new Vector3((gridData.WorldSize.x * gridData.CellSize.x) + transform.position.x, (i * gridData.CellSize.y) + transform.position.y);

                Gizmos.DrawLine(startPos, endPos);
            }

            // Vertical Lines
            for (int i = 0; i < gridData.WorldSize.x + 1; i++)
            {
                Vector2 startPos = new Vector3((i * gridData.CellSize.x) + transform.position.x, transform.position.y);
                Vector2 endPos = new Vector3((i * gridData.CellSize.x) + transform.position.x, (gridData.WorldSize.y * gridData.CellSize.y) + transform.position.y);

                Gizmos.DrawLine(startPos, endPos);
            }

            Gizmos.color = Color.white;
        }

        private void ClampScaling()
        {
            var localScale = transform.localScale;
            localScale.Clamp(Vector3.one, Vector3.one);
            transform.localScale = localScale;
        }

        /// <summary>
        /// Returns the cell at the given cell position or null if the position was out of range
        /// </summary>
        /// <param name="cellPosition">The position of the cell - only positive values allowed!</param>
        /// <returns>The cell at the given position or null</returns>
        public WorldGridCell GetCellAt(Vector2Int cellPosition)
        {
            return GetCellAt(cellPosition.x, cellPosition.y);
        }

        /// <summary>
        /// Returns the cell at the given cell position or null if the position was out of range
        /// </summary>
        /// <param name="x">The x position of the cell - only positive values allowed!</param>
        /// <param name="y">The y position of the cell - only positive values allowed!</param>
        /// <returns>The cell at the given position or null</returns>
        public WorldGridCell GetCellAt(int x, int y)
        {
            if (!gridData.Initialized)
                return null;

            if (x < 0 || x >= gridData.WorldSize.x)
                return null;

            if (y < 0 || y >= gridData.WorldSize.y)
                return null;

            return gridData.CellData[y * gridData.WorldSize.x + x];
        }

        /// <summary>
        /// Calculates the world position for the given cell position
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        public Vector2 CellToWorld(Vector2Int cellPosition)
        {
            return transform.position + new Vector3((cellPosition.x * gridData.CellSize.x) + gridData.CellSize.x / 2, (cellPosition.y * gridData.CellSize.y) + gridData.CellSize.y / 2);
        }

        /// <summary>
        /// Calculates the cell position for the given world position. Also negative cell position values will be returned!
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <returns></returns>
        public Vector2Int WorldToCell(Vector2 worldPosition)
        {
            Vector2Int a = new Vector2Int(
                (int)((worldPosition.x + gridData.CellSize.x - transform.position.x) / gridData.CellSize.x) - 1,
                (int)((worldPosition.y + gridData.CellSize.y - transform.position.y) / gridData.CellSize.y) - 1
            );
            
            //a.Clamp(Vector2Int.zero, worldSize);
            
            return a;
        }

        /// <summary>
        /// Load and link grid data
        /// </summary>
        /// <param name="data"></param>
        public void LoadGridData(GridData data)
        {
            if (gridData != null)
                UnloadGrid();

            Debug.Log($"Loading grid {gridData.name} ...");

            if (data == null)
                Debug.LogException(new ArgumentNullException("No Grid Data provided"), this);

            gridData = data;
            gridData.cellChanged += OnCellChanged;

            Debug.Log($"Loaded grid {gridData.name}");
        }

        /// <summary>
        /// Unload and unlink loaded grid if any
        /// </summary>
        public void UnloadGrid()
        {
            if (gridData != null)
            {
                Debug.Log($"Unloading grid {gridData.name} ...");

                gridData.cellChanged -= OnCellChanged;
                gridData = null;

                Debug.Log($"Grid unloaded");
            }
        }

        private void OnCellChanged(object sender, WorldGridCell cell)
        {
            Debug.Log($"Cell {cell.CellPosition} changed!");
            cellChanged?.Invoke(cell, cell);
        }
    }
}
