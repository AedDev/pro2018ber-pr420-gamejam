using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public class BaseEntity : MonoBehaviour
	{
		public static event Action<BaseEntity> EntityCreated;
		public static event Action<BaseEntity> EntityRemoved;
		public static readonly List<BaseEntity> AllEntities = new List<BaseEntity>();

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