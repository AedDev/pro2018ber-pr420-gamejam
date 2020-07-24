using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(BarricadeConfiguration), true)]
	public class BarricadeConfigurationEditor : EntityConfigurationEditor
	{
		protected override void DrawBaseSettings()
		{
			base.DrawBaseSettings();
			BarricadeConfiguration settings = target as BarricadeConfiguration;
			IntField(ref settings.BuildCost, ObjectNames.NicifyVariableName(nameof(BarricadeConfiguration.BuildCost)));
		}
	}
}