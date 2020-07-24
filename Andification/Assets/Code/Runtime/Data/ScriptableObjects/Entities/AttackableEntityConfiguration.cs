namespace Andification.Runtime.Data
{
	public abstract class AttackableEntityConfiguration : EntityConfiguration
	{
		public bool Invincible;
		public int MaxHealth = 100;
	}
}