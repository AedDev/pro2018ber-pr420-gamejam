using Andification.Runtime.Behaviours.Entities;

namespace Andification.Runtime.Data
{
	public class TowerConfiguration : AttackingEntityConfiguration
	{
		public int BuildCost = 1;
		public TowerAnimator Prefab;
	}
}