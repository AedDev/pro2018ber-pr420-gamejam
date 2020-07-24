using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEditor;
using UnityEngine;
using static Andification.Editor.EditorUtils;

namespace Andification.Editor.ObjectDrawer
{
	[CustomEditor(typeof(ProjectileConfiguration), true)]
	public class ProjectileConfigurationEditor : ObjectEditor
	{
		private bool baseSettingsOpen;
		private bool travelSettingsOpen;
		private bool turnSettingsOpen;
		protected override bool ShouldHideBaseInspector => true;
		protected override void CustomInspector()
		{
			DrawInFoldout(ref baseSettingsOpen, "Base Settings", DrawBaseSettings, true);
			DrawInFoldout(ref travelSettingsOpen, "Travel Settings", DrawTravelSettings, true);
			DrawInFoldout(ref turnSettingsOpen, "Turn Settings", DrawTurnSettings, true);
		}

		protected virtual void DrawBaseSettings()
		{
			ProjectileConfiguration settings = target as ProjectileConfiguration;
			
			UnityObjectField<Sprite>(ref settings.ProjectileSprite, ObjectNames.NicifyVariableName(nameof(ProjectileConfiguration.ProjectileSprite)));
			FloatField(ref settings.HitDistance, ObjectNames.NicifyVariableName(nameof(ProjectileConfiguration.HitDistance)));
			FloatField(ref settings.MaxAliveTime, ObjectNames.NicifyVariableName(nameof(ProjectileConfiguration.MaxAliveTime)));
		}

		protected virtual void DrawTravelSettings()
		{
			ProjectileConfiguration settings = target as ProjectileConfiguration;
			
			BoolField(ref settings.InstantTravel, ObjectNames.NicifyVariableName(nameof(ProjectileConfiguration.InstantTravel)));
			if (!settings.InstantTravel)
			{
				FloatField(ref settings.TravelSpeed, ObjectNames.NicifyVariableName(nameof(ProjectileConfiguration.TravelSpeed)));
			}
		}

		protected virtual void DrawTurnSettings()
		{
			ProjectileConfiguration settings = target as ProjectileConfiguration;
			
			BoolField(ref settings.InstantTurn, ObjectNames.NicifyVariableName(nameof(ProjectileConfiguration.InstantTurn)));
			if (!settings.InstantTurn)
			{
				FloatField(ref settings.TurnSpeed, ObjectNames.NicifyVariableName(nameof(ProjectileConfiguration.TurnSpeed)));
			}
		}
	}
}