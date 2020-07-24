using Andification.Runtime.Data;

namespace Andification.Runtime.Behaviours.Entities
{
	public class EnemyUnit : AttackableEntity<EnemyUnitConfiguration>
	{
		public override bool IsEnemyEntity => true;
	}
}