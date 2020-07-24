using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace Andification.Editor
{
	public class ScriptCreationKeywordReplacer : UnityEditor.AssetModificationProcessor
	{
		private const string BASE_NAMESPACE_KEYWORD = "#BASENAMESPACE#";
		private const string RELATIVE_NAMESPACE_KEYWORD = "#RELATIVENAMESPACE#";
		private const string PROJECT_NAMESPACES_KEYWORD = "#PROJECTNAMESPACES#";
		private const string EDITOR_SCRIPT_TARGET_KEYWORD = "#EDITORSCRIPTTARGET#";

		public static void OnWillCreateAsset(string path)
		{
			path = path.Replace(".meta", "");
			int index = path.LastIndexOf(".");
			if (index == -1)
			{
				return;
			}

			string fileType = path.Substring(index);
			if (fileType != ".cs")
			{
				return;
			}

			index = Application.dataPath.LastIndexOf("Assets");
			string fullPath = Application.dataPath.Substring(0, index) + path;
			string fileContent = System.IO.File.ReadAllText(fullPath);
			fileContent = fileContent.Replace(BASE_NAMESPACE_KEYWORD, EditorSettings.projectGenerationRootNamespace);
			fileContent = fileContent.Replace(EDITOR_SCRIPT_TARGET_KEYWORD, GetEditorScriptTarget(path));
			fileContent = fileContent.Replace(RELATIVE_NAMESPACE_KEYWORD, BuildNameSpace(path));
			fileContent = fileContent.Replace(PROJECT_NAMESPACES_KEYWORD, AssemblyFullyQualifiedNamespaces());
			System.IO.File.WriteAllText(path, fileContent);
			AssetDatabase.Refresh();
		}

		private static string BuildNameSpace(string path)
		{
			path = path.Substring(0, path.LastIndexOf("/"));
			path = path.Replace("Assets/Scripts", EditorSettings.projectGenerationRootNamespace);
			path = path.Replace("/", ".");

			return path;
		}

		private static string GetEditorScriptTarget(string path)
		{
			return path.Substring(path.LastIndexOf("/") + 1, path.LastIndexOf(".") - (path.LastIndexOf("/") + 1)).Replace("Editor", "");
		}

		private static string AssemblyFullyQualifiedNamespaces()
		{
			Type[] assemblyTypes = Assembly.GetAssembly(typeof(AndificationScriptableObject)).GetTypes();
			string[] namespaceNames = assemblyTypes
									.Select(type => type.Namespace)
									.Distinct()
									.Where(namespaceName => !string.IsNullOrEmpty(namespaceName))
									.ToArray();

			Array.Sort(namespaceNames);
			StringBuilder fullString = new StringBuilder();
			for (int i = 0; i < namespaceNames.Length; i++)
			{
				fullString.Append($"using {namespaceNames[i]};\n");
			}

			return fullString.ToString();
		}
	}
}