using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEngine;

namespace Andification.Runtime.Data
{
	public class TowerConfiguration : EntityConfiguration
	{
		public int BaseDamage;
		public float AttackSpeed = 1;
		public int BulletCount = 6;
		public float ReloadTime = 2.5f;
	}
}