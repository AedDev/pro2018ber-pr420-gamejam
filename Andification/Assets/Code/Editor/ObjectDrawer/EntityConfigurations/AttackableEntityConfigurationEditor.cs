using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(AttackableEntityConfiguration), true)]
	public class AttackableEntityConfigurationEditor : EntityConfigurationEditor
	{
		private bool attackableSettingsOpen;
		protected override void CustomInspector()
		{
			base.CustomInspector();
			DrawInFoldout(ref attackableSettingsOpen, "Attackble Settings", DrawAttackableSettings, true);
		}

		protected virtual void DrawAttackableSettings()
		{
			AttackableEntityConfiguration settings = target as AttackableEntityConfiguration;
			
			BoolField(ref settings.Invincible, ObjectNames.NicifyVariableName(nameof(AttackableEntityConfiguration.Invincible)));
			if (!settings.Invincible)
			{
				IntField(ref settings.MaxHealth, ObjectNames.NicifyVariableName(nameof(AttackableEntityConfiguration.MaxHealth)));
			}
		}
	}
}