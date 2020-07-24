using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public abstract class EntityAnimator<T> : MonoBehaviour
	{
		[SerializeField] protected SpriteRenderer spriteRenderer = default;
		protected T Target { get; private set; }

		public void Initialise(T target)
		{
			Target = target;
			OnInitialise();
		}

		protected virtual void OnInitialise() { }
	}
}