using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(AttackingEntityConfiguration), true)]
	public class AttackingEntityConfigurationEditor : AttackableEntityConfigurationEditor
	{
		private bool attackSettingsOpen;
		protected override void CustomInspector()
		{
			base.CustomInspector();
			DrawInFoldout(ref attackSettingsOpen, "Attack Settings", DrawAttackSettings, true);
		}

		protected virtual void DrawAttackSettings()
		{
			AttackingEntityConfiguration settings = target as AttackingEntityConfiguration;
			
			UnityObjectField<ProjectileConfiguration>(ref settings.Projectile, ObjectNames.NicifyVariableName(nameof(AttackingEntityConfiguration.Projectile)));
			FloatField(ref settings.AttackRange, ObjectNames.NicifyVariableName(nameof(AttackingEntityConfiguration.AttackRange)));
			FloatField(ref settings.AttackSpeed, ObjectNames.NicifyVariableName(nameof(AttackingEntityConfiguration.AttackSpeed)));
			IntField(ref settings.AttackCount, ObjectNames.NicifyVariableName(nameof(AttackingEntityConfiguration.AttackCount)));
			FloatField(ref settings.AttackReloadTime, ObjectNames.NicifyVariableName(nameof(AttackingEntityConfiguration.AttackReloadTime)));
		}
	}
}