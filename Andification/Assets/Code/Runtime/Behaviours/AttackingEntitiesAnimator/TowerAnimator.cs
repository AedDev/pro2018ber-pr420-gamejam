using System;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public class TowerAnimator : EntityAnimator<Tower>
	{
		[SerializeField] private Transform turnablePart = default;
		protected override void OnInitialise()
		{
			Target.StartedAttack += UpdateTurnablePart;
		}

		private void OnDestroy()
		{
			Target.StartedAttack -= UpdateTurnablePart;
		}

		private void UpdateTurnablePart(IAttackableEntity targetEntity)
		{
			if(targetEntity is BaseEntity entity)
			{
				turnablePart.right = (entity.transform.position - turnablePart.position).normalized;
			}
		}
	}
}