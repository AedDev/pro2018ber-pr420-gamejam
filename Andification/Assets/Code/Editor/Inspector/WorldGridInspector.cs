using UnityEditor;
using UnityEngine;
using Andification.Runtime.Extensions;
using Andification.Runtime.GridSystem;
using Andification.Runtime.Data.ScriptableObjects.Map;
using System;

namespace Andification.Editor.Inspector
{
    [CustomEditor(typeof(WorldGrid))]
    public class WorldGridInspector : UnityEditor.Editor
    {
        private Vector2Int worldSize = Vector2Int.one;
        private Vector2 cellSize = Vector2.one;

        private WorldGrid world;

        public Texture2D texDefault;
        public Texture2D texSpawner;
        public Texture2D texWalkable;
        public Texture2D texNotWalkable;
        public Texture2D texTarget;

        private void OnEnable()
        {
            world = target as WorldGrid;

            Debug.Log("Loading map editor textures ...");
            texDefault = Resources.Load<Texture2D>("MapEditor/Textures/Default");
            texSpawner = Resources.Load<Texture2D>("MapEditor/Textures/Spawner");
            texWalkable = Resources.Load<Texture2D>("MapEditor/Textures/Walkable");
            texNotWalkable = Resources.Load<Texture2D>("MapEditor/Textures/NotWalkable");
            texTarget = Resources.Load<Texture2D>("MapEditor/Textures/Target");
            Debug.Log("Map editor textures loaded!");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Create Grid Data", EditorStyles.boldLabel);

            worldSize.Clamp(Vector2Int.one, Vector2Int.one * int.MaxValue);
            cellSize.Clamp(Vector2.one, Vector2.one * float.MaxValue);

            worldSize = EditorGUILayout.Vector2IntField("World Size", worldSize);
            cellSize = EditorGUILayout.Vector2Field("Cell Size", cellSize);

            EditorGUI.BeginDisabledGroup(world.GridDataReference != null);
            if (GUILayout.Button("Create"))
            {
                var gridDataInstance = GridData.CreateInstance(worldSize, cellSize);
                AssetDatabase.CreateAsset(gridDataInstance, $"Assets/GridData-{Guid.NewGuid()}.asset");

                world.LoadGridData(gridDataInstance);
                EditorUtility.SetDirty(world);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(world.GridDataReference == null);
            if (GUILayout.Button("Load dimensions"))
            {
                worldSize = world.GridDataReference.WorldSize;
                cellSize = world.GridDataReference.CellSize;
            }

            if (GUILayout.Button("Re-Initialize"))
            {
                world.GridDataReference.Initialize(world.GridDataReference.WorldSize, world.GridDataReference.CellSize);
                EditorUtility.SetDirty(world.GridDataReference);
                AssetDatabase.SaveAssets();
            }
            EditorGUI.EndDisabledGroup();
        }

        private void OnSceneGUI()
        {
            DrawMapEditorUtils();
        }

        private void DrawMapEditorUtils()
        {
            var data = world.GridDataReference.CellData;
            var zoom = SceneView.currentDrawingSceneView.camera.orthographicSize;

            var typeValues = Enum.GetValues(typeof(GridContentType)) as GridContentType[];

            Handles.BeginGUI();
            for (int i = 0; i < data.Length; i++)
            {
                var worldPos = world.CellToWorld(data[i].CellPosition) - (world.GridDataReference.CellSize / 2) + new Vector2(0, world.GridDataReference.CellSize.y);
                var screenPos = HandleUtility.WorldToGUIPoint(worldPos);
                var cellRect = new Rect(
                    screenPos,
                    new Vector2(world.GridDataReference.CellSize.x, world.GridDataReference.CellSize.y) * 322f / zoom
                );

                var tex = GetTextureForContentType(data[i].ContentType);
                
                if (GUI.Button(cellRect, tex))
                {
                    var curTypeIndex = Array.FindIndex(typeValues, type => world.GridDataReference.CellData[i].ContentType == type);
                    world.GridDataReference.CellData[i].ContentType = typeValues[++curTypeIndex % typeValues.Length];
                }
            }
            Handles.EndGUI();
        }

        private Texture2D GetTextureForContentType(GridContentType type)
        {
            switch (type)
            {
                case GridContentType.Nothing:
                    return texWalkable;

                case GridContentType.Spawner:
                    return texSpawner;

                case GridContentType.Baricade:
                    return texNotWalkable;

                case GridContentType.Target:
                    return texTarget;

                default:
                    return null;
            }
        }
    }
}