using UnityEngine;
using System;
using Andification.Runtime.Data.ScriptableObjects.Map;

namespace Andification.Runtime.GridSystem
{
    [Serializable]
    public class WorldGridCell
    {
        [SerializeField] private Vector2Int cellPosition;
        [NonSerialized] private GridData relatedGrid;
        
        [SerializeField] private bool walkable = true;
        [SerializeField] private bool buildable = true;
        [SerializeField] private GridContentType contentType = GridContentType.Nothing;
        [NonSerialized] private UnityEngine.Object content;

        [NonSerialized] private Action<WorldGridCell> cellChangedHandler;

        public GridData RelatedGrid
        {
            get => relatedGrid;
            private set => relatedGrid = value;
        }

        public Vector2Int CellPosition
        {
            get => cellPosition;
            private set => cellPosition = value;
        }

        public bool Walkable
        {
            get => walkable;
            set 
            {
                if (value != walkable)
                {
                    walkable = value;

                    if (relatedGrid.Initialized)
                        cellChangedHandler?.Invoke(this);
                }
            }
        }

        public bool Buildable
        {
            get => buildable;
            set
            {
                if (value != buildable)
                {
                    buildable = value;

                    if (relatedGrid.Initialized)
                        cellChangedHandler?.Invoke(this);
                }
            }
        }

        public GridContentType ContentType
        {
            get => contentType;
            set
            {
                if (value != contentType)
                {
                    contentType = value;

                    if (relatedGrid.Initialized)
                        cellChangedHandler?.Invoke(this);
                }
            }
        }

        public UnityEngine.Object Content
        {
            get => content;
            set
            {
                if (value != content)
                {
                    content = value;

                    if (relatedGrid.Initialized)
                        cellChangedHandler?.Invoke(this);
                }
            }
        }

        public WorldGridCell(GridData relatedGrid, Vector2Int cellPosition, Action<WorldGridCell> cellChanged = default)
        {
            if (cellPosition.x < 0 || cellPosition.y < 0)
                throw new ArgumentOutOfRangeException("x and y cell coordinate need to be a value equal to or greater than 0");

            RelatedGrid = relatedGrid;
            CellPosition = cellPosition;
            cellChangedHandler = cellChanged;
        }

        public WorldGridCell(GridData relatedGrid, int x, int y, Action<WorldGridCell> cellChanged = default) : this(relatedGrid, new Vector2Int(x, y), cellChanged)
        {
            
        }
    }
}
