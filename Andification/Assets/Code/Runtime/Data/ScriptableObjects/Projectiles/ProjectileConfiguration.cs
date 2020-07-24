using Andification.Runtime.Behaviours;
using Andification.Runtime.Behaviours.Entities;
using UnityEngine;

namespace Andification.Runtime.Data
{
	public abstract class ProjectileConfiguration : AndificationScriptableObject
	{
		//Base Settings
		public Sprite ProjectileSprite;
		public float HitDistance = 0.5f;
		public float MaxAliveTime = 10;

		//Traveling
		public bool InstantTravel;
		public float TravelSpeed = 4;

		//Turning
		public bool InstantTurn;
		public float TurnSpeed = 180;
		
		public void ProjectileStart(Projectile projectile)
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

		public void ProjectileUpdate(Projectile projectile)
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

		protected virtual bool TryReachTarget(Projectile projectile)
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

		private void TargetReached(Projectile projectile)
		{
			projectile.transform.position = (projectile.TargetEntity as BaseEntity).transform.position;
			OnTargetReached(projectile);
			projectile.TearDown();
		}

		private void ProjectileTimeOut(Projectile projectile)
		{
			OnProjectileTimeOut(projectile);
			projectile.TearDown();
		}

		protected abstract void OnProjectileStart(Projectile projectile);
		protected abstract void OnProjectileUpdate(Projectile projectile);
		protected abstract void OnTargetReached(Projectile projectile);
		protected abstract void OnProjectileTimeOut(Projectile projectile);
	}
}