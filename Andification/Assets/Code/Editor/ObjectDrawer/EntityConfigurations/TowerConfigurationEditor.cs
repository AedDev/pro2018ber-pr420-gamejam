using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Behaviours.Entities;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(TowerConfiguration), true)]
	public class TowerConfigurationEditor : AttackingEntityConfigurationEditor
	{
		protected override void DrawBaseSettings()
		{
			base.DrawBaseSettings();
			TowerConfiguration settings = target as TowerConfiguration;
			UnityObjectField<TowerAnimator>(ref settings.Prefab, ObjectNames.NicifyVariableName(nameof(TowerConfiguration.Prefab)));
			IntField(ref settings.BuildCost, ObjectNames.NicifyVariableName(nameof(TowerConfiguration.BuildCost)));
		}
	}
}