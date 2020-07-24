using UnityEngine;
using Andification.Runtime.Extensions;

namespace Andification.Runtime.GridSystem
{
    public class WorldGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int worldSize = Vector2Int.zero;
        [SerializeField] private Vector2 cellSize = Vector2.one;

        [Space]
        [Header("Debug Stuff")]
        [SerializeField] private bool debugMode = true;
        [SerializeField] private bool drawGrid = true;
        [SerializeField] private Color gridColor = Color.cyan;

        public bool Initialized
        {
            get;
            private set;
        } = false;

        public Vector2Int WorldSize => worldSize;
        public Vector2 CellSize => cellSize;

        private WorldGridCell[] _cellMap = null;
        private WorldGridCell[,] CellMap2D
        {
            get => _cellMap?.ToTwoDimensional(worldSize.x, worldSize.y);
        }

        private void OnDrawGizmos()
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

        private void OnGUI()
        {
            if (debugMode)
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
        }

        private void DrawEditorGrid()
        {
            Gizmos.color = gridColor;

            // Horizontal Lines
            for (int i = 0; i < worldSize.y + 1; i++)
            {
                Vector2 startPos = new Vector3(transform.position.x, (i * cellSize.y) + transform.position.y);
                Vector2 endPos = new Vector3((worldSize.x * cellSize.x) + transform.position.x, (i * cellSize.y) + transform.position.y);

                Gizmos.DrawLine(startPos, endPos);
            }

            // Vertical Lines
            for (int i = 0; i < worldSize.x + 1; i++)
            {
                Vector2 startPos = new Vector3((i * cellSize.x) + transform.position.x, transform.position.y);
                Vector2 endPos = new Vector3((i * cellSize.x) + transform.position.x, (worldSize.y * cellSize.y) + transform.position.y);

                Gizmos.DrawLine(startPos, endPos);
            }

            Gizmos.color = Color.white;
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

        public Vector2Int WorldToCell(Vector2 worldPosition)
        {
            Vector2Int a = new Vector2Int(
                (int)((worldPosition.x + cellSize.x - transform.position.x) / cellSize.x) - 1,
                (int)((worldPosition.y + cellSize.y - transform.position.y) / cellSize.y) - 1
            );
            
            //a.Clamp(Vector2Int.zero, worldSize);
            
            return a;
        }
    }
}
