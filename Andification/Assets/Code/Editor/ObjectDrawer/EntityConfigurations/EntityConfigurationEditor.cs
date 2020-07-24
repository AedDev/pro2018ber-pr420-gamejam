using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(EntityConfiguration), true)]
	public class EntityConfigurationEditor : ObjectEditor
	{
		private bool baseSettingsOpen;
		protected override bool ShouldHideBaseInspector => true;
		protected override void CustomInspector()
		{
			DrawInFoldout(ref baseSettingsOpen, "Base Settings", DrawBaseSettings, true);
		}

		protected virtual void DrawBaseSettings()
		{
			EntityConfiguration settings = target as EntityConfiguration;
			
			StringField(ref settings.Name, ObjectNames.NicifyVariableName(nameof(EntityConfiguration.Name)));
		}
	}
}