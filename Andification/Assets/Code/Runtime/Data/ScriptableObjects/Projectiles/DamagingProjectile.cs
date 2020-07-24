﻿using System.Collections.Generic;
using Andification.Runtime.Behaviours;
using Andification.Runtime.Behaviours.Entities;
using UnityEngine;

namespace Andification.Runtime.Data
{
	public class DamagingProjectile : ProjectileConfiguration
	{
		public int Damage;

		//AOE Settings
		public bool AOEDamage;
		public bool AOEOnTimeOut;
		public float AOERange;
		public bool AOEHasFalloff;
		public float ZeroOutAOERange;

		protected override void OnProjectileStart(ProjectileBehaviour projectile) { }

		protected override void OnProjectileUpdate(ProjectileBehaviour projectile) { }

		protected override void OnTargetReached(ProjectileBehaviour projectile)
		{
			if (AOEDamage)
			{
				CauseAOEDamage(projectile);
			}
			else
			{
				CauseSingleDamage(projectile);
			}
		}

		private void CauseSingleDamage(ProjectileBehaviour projectile)
		{
			projectile.TargetEntity.Damage(Damage);
		}

		private void CauseAOEDamage(ProjectileBehaviour projectile)
		{
			foreach (IAttackableEntity entity in GetRelevantEntities(projectile.TargetEntity))
			{
				float sqrDist = ((Vector2) ((entity as BaseEntity).transform.position - projectile.transform.position)).sqrMagnitude;
				if (sqrDist > AOERange)
				{
					continue;
				}

				entity.Damage(AOEHasFalloff
								? Mathf.RoundToInt(Mathf.Lerp(Damage, 0, Mathf.Sqrt(sqrDist) / ZeroOutAOERange))
								: Damage);
			}
		}

		private IEnumerable<IAttackableEntity> GetRelevantEntities(IAttackableEntity mainTarget)
		{
			bool targetsEnemies = mainTarget is EnemyUnit;
			foreach (BaseEntity entity in BaseEntity.AllEntities)
			{
				if (entity is IAttackableEntity attackableEntity)
				{
					if (targetsEnemies == entity.IsEnemyEntity)
					{
						yield return attackableEntity;
					}
				}
			}
		}

		protected override void OnProjectileTimeOut(ProjectileBehaviour projectile)
		{
			if (AOEDamage && AOEOnTimeOut)
			{
				CauseAOEDamage(projectile);
			}
		}
	}
}