using Andification.Runtime.Data;

namespace Andification.Runtime.Behaviours.Entities
{
	public class Barricade : Entity<BarricadeConfiguration>
	{
		public override bool IsEnemyEntity => false;
	}
}