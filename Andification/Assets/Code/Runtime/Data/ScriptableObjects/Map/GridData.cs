using UnityEngine;
using Andification.Runtime.Extensions;
using Andification.Runtime.GridSystem;

namespace Andification.Runtime.Data.ScriptableObjects.Map
{
    public class GridData : ScriptableObject
    {
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

        [SerializeField] private WorldGridCell[] _cellData = null;
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
            this.worldSize = worldSize;
            this.cellSize = cellSize;
            this._cellData = new WorldGridCell[worldSize.x * worldSize.y];

            for (int _x = 0, _i = 0; _x < worldSize.x; _x++)
                for (int _y = 0; _y < worldSize.y; _y++, _i++)
                    this._cellData[_i] = new WorldGridCell(this, _x, _y, /* Handler einbauen -> */ null);

            Debug.Log("GridData initialized!");

            this.Initialized = true;
        }
    }
}