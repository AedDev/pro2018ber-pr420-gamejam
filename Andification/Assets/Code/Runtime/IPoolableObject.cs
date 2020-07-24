using UnityEngine;

namespace Andification.Runtime
{
	public interface IPoolableObject<T> where T : Component, IPoolableObject<T>
	{
		ObjectPool<T> SelfPool { get; set; }
		void TearDown();
	}
}