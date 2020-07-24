using Andification.Runtime.Data;

namespace Andification.Runtime.Behaviours.Entities
{
	public interface IAttackableEntity
	{
		void Damage(int amount);
	}
	public abstract class AttackableEntity<T> : Entity<T>, IAttackableEntity where T : AttackableEntityConfiguration
	{
		public int MaxHealth { get; private set; }
		public int CurrentHealth { get; private set; }
		public bool Invincible { get; private set; }
		public bool Alive { get; private set; }

		protected override void OnInitialise()
		{
			MaxHealth = CurrentHealth = Configuration.MaxHealth;
			Invincible = Configuration.Invincible;
			Alive = true;
		}

		public virtual void Damage(int amount)
		{
			if (Invincible)
			{
				return;
			}

			CurrentHealth -= amount;
			if (CurrentHealth <= 0)
			{
				Death();
			}
		}

		protected virtual void Death()
		{
			Alive = false;
			TearDown();
		}

		private void TearDown()
		{
			OnTearDown();
			Destroy(gameObject);
		}

		protected virtual void OnTearDown() { }
	}
}