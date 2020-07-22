using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Data
{
	public abstract class EntityConfiguration : ScriptableObject
	{
		public string Name;
		public bool Invincible;
		public int MaxHealth = 100;
	}
}