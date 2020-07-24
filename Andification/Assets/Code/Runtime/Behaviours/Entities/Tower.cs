using Andification.Runtime.Data;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public class Tower : AttackingEntity<TowerConfiguration>
	{
		public override bool IsEnemyEntity => false;

		protected override void Update()
		{
			base.Update();
			while (CanAttack())
			{
				if (!Attack(GetNearestEnemyUnit(transform.position, Configuration.AttackRange)))
				{
					break;
				}
			}
		}

		private static EnemyUnit GetNearestEnemyUnit(Vector2 point, float maxRange)
		{
			int closestIndex = -1;
			float closestDist = (maxRange * maxRange) + 0.1f;
			for (int i = 0; i < AllEntities.Count; i++)
			{
				if (AllEntities[i] is EnemyUnit unit && unit.Alive)
				{
					float dist = ((Vector2) unit.transform.position - point).sqrMagnitude;
					if (dist < closestDist)
					{
						closestDist = dist;
						closestIndex = i;
					}
				}
			}

			return closestIndex != -1 ? (EnemyUnit) AllEntities[closestIndex] : null;
		}
	}
}