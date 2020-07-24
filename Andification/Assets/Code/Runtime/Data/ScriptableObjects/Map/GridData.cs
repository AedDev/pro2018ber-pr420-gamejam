using UnityEngine;
using Andification.Runtime.Extensions;
using Andification.Runtime.GridSystem;
using System;

namespace Andification.Runtime.Data.ScriptableObjects.Map
{
    public class GridData : ScriptableObject
    {
        /// <summary>
        /// The name of the grid
        /// </summary>
        public new string name;

        /// <summary>
        /// Size of the grid in cells
        /// </summary>
        [SerializeField] private Vector2Int worldSize = Vector2Int.zero;

        /// <summary>
        /// The size of a single cell in unity units
        /// </summary>
        [SerializeField] private Vector2 cellSize = Vector2.one;

        public Vector2Int WorldSize => worldSize;
        public Vector2 CellSize => cellSize;

        [SerializeField, HideInInspector] private WorldGridCell[] _cellData = null;
        public WorldGridCell[] CellData
        {
            get => _cellData;
        }

        public WorldGridCell[,] CellData2D
        {
            get => _cellData?.ToTwoDimensional(worldSize.x, worldSize.y);
        }

        [SerializeField, HideInInspector] private bool initialized = false;
        public bool Initialized
        {
            get => initialized;
            private set => initialized = value;
        }

        public event EventHandler<WorldGridCell> cellChanged;

        private void OnValidate()
        {
            worldSize.Clamp(Vector2Int.one, Vector2Int.one * int.MaxValue);
            cellSize.Clamp(Vector2.one, Vector2.one * float.MaxValue);
        }

        /// <summary>
        /// Creates a new instance of GridData with basic initialization
        /// </summary>
        /// <param name="worldSize"></param>
        /// <param name="cellSize"></param>
        /// <returns></returns>
        public static GridData CreateInstance(Vector2Int worldSize, Vector2 cellSize)
        {
            var data = CreateInstance<GridData>();
            data.Initialize(worldSize, cellSize);

            return data;
        }

        public void Initialize(Vector2Int worldSize, Vector2 cellSize)
        {
            Initialized = false;

            this.worldSize = worldSize;
            this.cellSize = cellSize;
            this._cellData = new WorldGridCell[worldSize.x * worldSize.y];

            for (int _x = 0; _x < worldSize.x; _x++)
            {
                for (int _y = 0; _y < worldSize.y; _y++)
                {
                    int index = _y * worldSize.x + _x;

                    this._cellData[index] = new WorldGridCell(this, _x, _y, OnCellChanged)
                    {
                        Buildable = (this._cellData[index]?.Buildable).GetValueOrDefault(true),
                        Walkable = (this._cellData[index]?.Walkable).GetValueOrDefault(true),
                        ContentType = (this._cellData[index]?.ContentType).GetValueOrDefault(GridContentType.Nothing),
                        Content = this._cellData[index]?.Content
                    };
                }
            }

            Debug.Log("GridData initialized!");

            Initialized = true;
        }

        private void OnCellChanged(WorldGridCell cell)
        {
            cellChanged?.Invoke(cell, cell);
        }
    }
}