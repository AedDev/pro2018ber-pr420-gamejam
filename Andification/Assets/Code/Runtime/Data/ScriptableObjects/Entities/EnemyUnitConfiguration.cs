using Andification.Runtime.Behaviours.Entities;

namespace Andification.Runtime.Data
{
	public class EnemyUnitConfiguration : AttackableEntityConfiguration
	{
		public EnemyUnitAnimator Prefab;
		public float MoveSpeed = 4;
		public int KillReward = 10;
		public int NexusDamage = 10;
	}
}