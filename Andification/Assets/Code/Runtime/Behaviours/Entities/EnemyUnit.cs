﻿using System;
using Andification.Runtime.Data;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public class EnemyUnit : AttackableEntity<EnemyUnitConfiguration>
	{
		private const float REACHED_NODE_THRESHOLD = 0.05f;
		public override bool IsEnemyEntity => true;
		private int gateIndex;
		private int nodeIndex;
		private Vector2 nextNode;

		public void UnitInitialisation(int gateIndex)
		{
			this.gateIndex = gateIndex;
			UpdateNodeIndex(0);
		}

		private void Update()
		{
			float stepDistance = Configuration.MoveSpeed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, nextNode, stepDistance);

			if (((Vector2) transform.position - nextNode).sqrMagnitude < REACHED_NODE_THRESHOLD)
			{
				UpdateNodeIndex(nodeIndex + 1);
			}
		}

		private void UpdateNodeIndex(int newNodeIndex)
		{
			Vector2Int[] selfPath = UnitPathManager.GetUnitPath(gateIndex);
			if (newNodeIndex == selfPath.Length - 1)
			{
				ReachedExitNode();
				return;
			}
			
			nodeIndex = newNodeIndex;
			nextNode = selfPath[newNodeIndex + 1];
			
			//Update direction and position
			Transform t = transform;
			t.position = (Vector2)selfPath[newNodeIndex];
			t.right = (nextNode - (Vector2) t.position).normalized;
		}

		protected override void Death()
		{
			GameData.s_instance.CurrentMoney.value += Configuration.KillReward;
			base.Death();
		}
		
		private void ReachedExitNode()
		{
			GameData.s_instance.CurrentLive.value -= Configuration.NexusDamage;
			TearDown();
		}
	}
}