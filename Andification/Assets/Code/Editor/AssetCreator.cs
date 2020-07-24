using System.IO;
using UnityEditor;
using UnityEngine;

namespace Andification.Editor
{
	public static class AssetCreator
	{
		public static void CreateAsset(System.Type assetType, string name, string fullPath)
		{
			ScriptableObject asset = ScriptableObject.CreateInstance(assetType);
			if (!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(fullPath);
			}

			string unityPath = fullPath.Substring(System.Math.Max(0, fullPath.LastIndexOf("Assets")));

			//Create the asset
			AssetDatabase.CreateAsset(asset, $"{unityPath}/{name}.asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			//Select the created asset
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = null;
			Selection.activeObject = asset;
			EditorGUIUtility.PingObject(asset);

			//Inform about creation
			Debug.Log($"Created: '{name}' at: {unityPath}/..");
		}
	}
}