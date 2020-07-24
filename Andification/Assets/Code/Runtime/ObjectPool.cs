using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime
{
	public class ObjectPool<T> where T : Component, IPoolableObject<T>
	{
		private int poolSize;

		public int PoolSize
		{
			get => poolSize;
			set => IncreasePoolSize(value);
		}

		public List<T> ActiveObjects { get; }

		private readonly T objectToPool;
		private readonly System.Func<T> objectToPoolGetter;
		private readonly Transform poolParent;
		private readonly bool reAdjustPoolSize;
		private readonly Queue<T> inActiveObjects;

		#region Constructor

		public ObjectPool(T objectToPool, Transform poolParent)
		{
			this.objectToPool = objectToPool;
			this.poolParent = poolParent;
			reAdjustPoolSize = true;
			ActiveObjects = new List<T>(poolSize);
			inActiveObjects = new Queue<T>(poolSize);
		}

		public ObjectPool(System.Func<T> objectToPoolGetter)
		{
			this.objectToPoolGetter = objectToPoolGetter;
			reAdjustPoolSize = true;
			ActiveObjects = new List<T>(poolSize);
			inActiveObjects = new Queue<T>(poolSize);
		}

		public ObjectPool(int poolSize, bool reAdjustPoolSize, T objectToPool, Transform poolParent)
		{
			this.objectToPool = objectToPool;
			this.poolParent = poolParent;
			this.reAdjustPoolSize = reAdjustPoolSize;
			ActiveObjects = new List<T>(poolSize);
			inActiveObjects = new Queue<T>(poolSize);
			IncreasePoolSize(poolSize);
		}

		public ObjectPool(int poolSize, bool reAdjustPoolSize, System.Func<T> objectToPoolGetter)
		{
			this.objectToPoolGetter = objectToPoolGetter;
			this.reAdjustPoolSize = reAdjustPoolSize;
			ActiveObjects = new List<T>(poolSize);
			inActiveObjects = new Queue<T>(poolSize);
			IncreasePoolSize(poolSize);
		}

		#endregion

		private void IncreasePoolSize(int newPoolSize)
		{
			int dif = newPoolSize - poolSize;
			if (dif <= 0)
			{
				return;
			}

			poolSize = newPoolSize;
			ActiveObjects.Capacity = newPoolSize;
			for (uint i = 0; i < dif; i++)
			{
				T newObject = objectToPool ? Object.Instantiate(objectToPool, poolParent) : objectToPoolGetter?.Invoke();
				if (newObject == null)
				{
#if EMBER_DEBUG
					throw new System.ArgumentNullException($"Object pool could not create a new poolobject from neither {nameof(objectToPool)} nor {nameof(objectToPoolGetter)}.");
#else
					continue;
#endif
				}

				newObject.gameObject.SetActive(false);
				newObject.SelfPool = this;
				inActiveObjects.Enqueue(newObject);
			}
		}

		public void InsertIntoPool(T newObject, bool asActive = false)
		{
			newObject.gameObject.SetActive(asActive);
			newObject.SelfPool = this;
			if (asActive)
			{
				ActiveObjects.Add(newObject);
			}
			else
			{
				inActiveObjects.Enqueue(newObject);
			}
		}

		public T GetPoolObject()
		{
			if (inActiveObjects.Count == 0)
			{
				if (reAdjustPoolSize)
				{
					IncreasePoolSize(poolSize + 1);
				}
				else
				{
					return null;
				}
			}

			T ret = inActiveObjects.Dequeue();
			ActiveObjects.Add(ret);
			ret.gameObject.SetActive(true);

			return ret;
		}

		public IEnumerable<T> GetMultipleObjects(int amount)
		{
			if (inActiveObjects.Count < amount)
			{
				if (reAdjustPoolSize)
				{
					IncreasePoolSize(poolSize + (amount - inActiveObjects.Count));
				}
				else
				{
					yield break;
				}
			}

			for (int i = 0; i < amount; i++)
			{
				T obj = inActiveObjects.Dequeue();
				obj.gameObject.SetActive(true);

				yield return obj;
			}
		}

		public void DisableAllActive()
		{
			//Iterate through list backwards in case the list count changes because of the ReturnToPool Call
			for (int i = ActiveObjects.Count - 1; i >= 0; i--)
			{
				ActiveObjects[i].TearDown();
			}
		}

		public void ReturnPoolObject(T toQueue)
		{
			ActiveObjects.Remove(toQueue);
			toQueue.gameObject.SetActive(false);
			inActiveObjects.Enqueue(toQueue);
		}
	}
}