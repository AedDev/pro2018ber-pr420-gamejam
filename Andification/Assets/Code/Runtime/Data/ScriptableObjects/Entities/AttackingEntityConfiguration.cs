using Andification.Runtime.Behaviours.Entities;

namespace Andification.Runtime.Data
{
	public abstract class AttackingEntityConfiguration : AttackableEntityConfiguration
	{
		public ProjectileConfiguration Projectile;
		public float AttackRange = 3;
		public float AttackSpeed = 1;
		public int AttackCount = 6;
		public float AttackReloadTime = 2.5f;
	}
}