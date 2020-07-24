using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(DamagingProjectile), true)]
	public class DamagingProjectileEditor : ProjectileConfigurationEditor
	{
		private bool damageSettingsOpen;
		protected override void CustomInspector()
		{
			base.CustomInspector();
			DrawInFoldout(ref damageSettingsOpen, "Damage Settings", DrawDamageSettings, true);
		}

		protected virtual void DrawDamageSettings()
		{
			DamagingProjectile settings = target as DamagingProjectile;
			
			IntField(ref settings.Damage, ObjectNames.NicifyVariableName(nameof(DamagingProjectile.Damage)));
			LineBreak();
			BoolField(ref settings.AOEDamage, ObjectNames.NicifyVariableName(nameof(DamagingProjectile.AOEDamage)));
			if (settings.AOEDamage)
			{
				IncreaseIndent();
				FloatField(ref settings.AOERange, ObjectNames.NicifyVariableName(nameof(DamagingProjectile.AOERange)));
				LineBreak();
				BoolField(ref settings.AOEOnTimeOut, ObjectNames.NicifyVariableName(nameof(DamagingProjectile.AOEOnTimeOut)));
				LineBreak();
				BoolField(ref settings.AOEHasFalloff, ObjectNames.NicifyVariableName(nameof(DamagingProjectile.AOEHasFalloff)));
				if (settings.AOEHasFalloff)
				{
					IncreaseIndent();
					FloatField(ref settings.ZeroOutAOERange, ObjectNames.NicifyVariableName(nameof(DamagingProjectile.ZeroOutAOERange)));
					DecreaseIndent();
				}
			}
		}
	}
}