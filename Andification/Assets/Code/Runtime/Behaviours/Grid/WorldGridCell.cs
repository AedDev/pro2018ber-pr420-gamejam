using UnityEngine;
using System;

namespace Andification.Runtime.GridSystem
{
    [Serializable]
    public class WorldGridCell
    {
        public readonly Vector2Int cellPosition;
        public readonly WorldGrid relatedGrid;
        
        private bool walkable = true;
        private bool buildable = true;
        private GridContentType content = GridContentType.Nothing;
        
        private Action<WorldGridCell> cellChangedHandler;

        public bool Walkable
        {
            get => walkable;
            set 
            {
                if (value != walkable)
                {
                    walkable = value;
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
                    cellChangedHandler?.Invoke(this);
                }
            }
        }

        public GridContentType Content
        {
            get => content;
            set
            {
                if (value != content)
                {
                    content = value;
                    cellChangedHandler?.Invoke(this);
                }
            }
        }

        public WorldGridCell(WorldGrid relatedGrid, Vector2Int cellPosition, Action<WorldGridCell> cellChanged = default)
        {
            if (cellPosition.x < 0 || cellPosition.y < 0)
                throw new ArgumentOutOfRangeException("x and y cell coordinate need to be a value equal to or greater than 0");

            this.relatedGrid = relatedGrid;
            this.cellPosition = cellPosition;
            this.cellChangedHandler = cellChanged;
        }

        public WorldGridCell(WorldGrid relatedGrid, int x, int y, Action<WorldGridCell> cellChanged = default) : this(relatedGrid, new Vector2Int(x, y), cellChanged)
        {
            
        }
    }
}
