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

        private void OnEnable()
        {
            world = target as WorldGrid;
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
    }
}