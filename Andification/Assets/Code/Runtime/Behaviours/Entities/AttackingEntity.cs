using System;
using Andification.Runtime.Data;
using Andification.Runtime.Factories;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public abstract class AttackingEntity<T> : AttackableEntity<T> where T : AttackingEntityConfiguration
	{
		public event Action<IAttackableEntity> StartedAttack;
		public event Action StartedReload;

		private int remainingAttacks;
		private float nextAttackDelay;

		protected override void OnInitialise()
		{
			base.OnInitialise();
			remainingAttacks = Configuration.AttackCount;
		}

		protected virtual void Update()
		{
			if (nextAttackDelay > 0)
			{
				nextAttackDelay -= Time.deltaTime;
			}
		}

		protected bool CanAttack()
		{
			return nextAttackDelay <= 0;
		}

		protected bool Attack(IAttackableEntity target)
		{
			if (target == null)
			{
				return false;
			}

			StartedAttack?.Invoke(target);
			ProjectileFactory.CreateProjectile(Configuration.Projectile, target);
			remainingAttacks--;
			if (remainingAttacks == 0)
			{
				Reload();
			}
			else
			{
				nextAttackDelay += Configuration.AttackSpeed;
			}

			return true;
		}

		private void Reload()
		{
			StartedReload?.Invoke();
			remainingAttacks = Configuration.AttackCount;
			nextAttackDelay += Math.Max(Configuration.AttackSpeed, Configuration.AttackReloadTime);
		}
	}
}