using System;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public abstract class BaseEntity : MonoBehaviour
	{
		public static event Action<BaseEntity> EntityCreated;
		public static event Action<BaseEntity> EntityRemoved;
		public static readonly List<BaseEntity> AllEntities = new List<BaseEntity>();
		public abstract bool IsEnemyEntity { get; }

		protected void OnCreate()
		{
			EntityCreated?.Invoke(this);
			AllEntities.Add(this);
		}

		protected void OnDestroy()
		{
			EntityRemoved?.Invoke(this);
			AllEntities.Remove(this);
		}
	}
}