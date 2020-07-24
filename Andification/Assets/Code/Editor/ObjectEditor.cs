using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	public abstract class ObjectEditor : UnityEditor.Editor
	{
		protected abstract bool ShouldHideBaseInspector { get; }
		private bool initialised;
		protected virtual void OnInitialize() { }

		public sealed override void OnInspectorGUI()
		{
			//If this is the first frame of being selected call the initialise method
			if (!initialised)
			{
				initialised = true;
				OnInitialize();
			}

			//Reset the indent from last frame to 0
			DecreaseIndent(Indent);

			//Check for unused data if there is we pop a warning message and a button that allows a virtual method call to clean the object up
			if (HasUnusedData())
			{
				EditorGUILayout.HelpBox("This Object has internal unused Data, this can be a result of a formally used List of objects that is now disabled.", MessageType.Warning);
				if (GUILayout.Button("Remove Unused Data"))
				{
					RemoveUnusedData();
				}
			}

			//Draw default inspector if requested
			if (!ShouldHideBaseInspector)
			{
				DrawDefaultInspector();
			}

			//Draw the custom inspector
			CustomInspector();

			//Check for changes
			if (IsDirty)
			{
				ShouldBeDirty(false);
				EditorUtility.SetDirty(target);
			}
		}

		protected abstract void CustomInspector();

		protected virtual bool HasUnusedData()
		{
			return false;
		}

		protected virtual void RemoveUnusedData() { }
	}
}