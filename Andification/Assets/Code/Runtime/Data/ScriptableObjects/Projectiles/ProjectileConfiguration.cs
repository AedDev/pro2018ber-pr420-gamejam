using Andification.Runtime.Behaviours;
using Andification.Runtime.Behaviours.Entities;
using UnityEngine;

namespace Andification.Runtime.Data
{
	public abstract class ProjectileConfiguration : ScriptableObject
	{
		public Sprite ProjectileSprite;

		//Traveling
		public bool InstantTravel;
		public float TravelSpeed;

		//Turning
		public bool InstantTurn;
		public float TurnSpeed;

		//Finalising Projectile
		public float HitDistance;
		public float MaxAliveTime;

		public void ProjectileStart(ProjectileBehaviour projectile)
		{
			if (InstantTravel)
			{
				TargetReached(projectile);
			}
			else
			{
				OnProjectileStart(projectile);
			}
		}

		public void ProjectileUpdate(ProjectileBehaviour projectile)
		{
			if (projectile.AliveTime > MaxAliveTime)
			{
				ProjectileTimeOut(projectile);
			}
			else if (!TryReachTarget(projectile))
			{
				OnProjectileUpdate(projectile);
			}
		}

		protected virtual bool TryReachTarget(ProjectileBehaviour projectile)
		{
			Transform projectileTransform = projectile.transform;
			Vector2 posDif = (Vector2) (projectile.TargetEntity as BaseEntity).transform.position - (Vector2) projectileTransform.position;
			float sqrDistance = posDif.sqrMagnitude;
			float travelDistance = TravelSpeed * Time.deltaTime;
			bool targetReached = sqrDistance <= ((travelDistance + HitDistance) * (travelDistance + HitDistance));
			if (targetReached)
			{
				TargetReached(projectile);
			}
			else
			{
				Vector2 direction = posDif / Mathf.Sqrt(sqrDistance);
				if (InstantTurn)
				{
					projectileTransform.forward = direction;
					projectileTransform.position += (Vector3) (direction * travelDistance);
				}
				else
				{
					float targetAngle = Mathf.Atan2(direction.y, direction.x);
					float newAngle = Mathf.MoveTowardsAngle(projectileTransform.eulerAngles.z, targetAngle, TurnSpeed * Time.deltaTime);
					projectileTransform.eulerAngles = new Vector3(0, 0, newAngle);
					projectileTransform.position += projectileTransform.forward * travelDistance;
				}
			}

			return targetReached;
		}

		private void TargetReached(ProjectileBehaviour projectile)
		{
			projectile.transform.position = (projectile.TargetEntity as BaseEntity).transform.position;
			OnTargetReached(projectile);
			projectile.TearDown();
		}

		private void ProjectileTimeOut(ProjectileBehaviour projectile)
		{
			OnProjectileTimeOut(projectile);
			projectile.TearDown();
		}

		protected abstract void OnProjectileStart(ProjectileBehaviour projectile);
		protected abstract void OnProjectileUpdate(ProjectileBehaviour projectile);
		protected abstract void OnTargetReached(ProjectileBehaviour projectile);
		protected abstract void OnProjectileTimeOut(ProjectileBehaviour projectile);
	}
}