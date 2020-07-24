using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Behaviours.Entities;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(EnemyUnitConfiguration), true)]
	public class EnemyUnitConfigurationEditor : AttackableEntityConfigurationEditor
	{
		private bool attackableSettingsOpen;
		protected override void CustomInspector()
		{
			base.CustomInspector();
			DrawInFoldout(ref attackableSettingsOpen, "Enemy Unit Settings", DrawEnemyUnitSettings, true);
		}

		protected override void DrawBaseSettings()
		{
			base.DrawBaseSettings();
			EnemyUnitConfiguration settings = target as EnemyUnitConfiguration;
			UnityObjectField<EnemyUnitAnimator>(ref settings.Prefab, ObjectNames.NicifyVariableName(nameof(EnemyUnitConfiguration.Prefab)));
		}

		protected virtual void DrawEnemyUnitSettings()
		{
			EnemyUnitConfiguration settings = target as EnemyUnitConfiguration;
			
			FloatField(ref settings.MoveSpeed, ObjectNames.NicifyVariableName(nameof(EnemyUnitConfiguration.MoveSpeed)));
			IntField(ref settings.KillReward, ObjectNames.NicifyVariableName(nameof(EnemyUnitConfiguration.KillReward)));
			IntField(ref settings.NexusDamage, ObjectNames.NicifyVariableName(nameof(EnemyUnitConfiguration.NexusDamage)));
		}
	}
}