using Andification.Runtime.Behaviours.Entities;
using UnityEngine;

namespace Andification.Runtime.Behaviours
{
	public class TowerAnimator : MonoBehaviour
	{
		private Tower target;

		public void Initialise(Tower target)
		{
			this.target = target;
		}
	}
}