using System;
using System.Collections;
using System.Collections.Generic;
using Andification.Runtime.Data;
using UnityEngine;

namespace Andification.Runtime.Behaviours.Entities
{
	public abstract class Entity<T> : BaseEntity where T: EntityConfiguration
	{
		public T Configuration { get; private set; }
		public int MaxHealth{ get; private set; }
		public int CurrentHealth{ get; private set; }
		public bool Invincible { get; private set; }
		public bool Alive { get; private set; }
		
		public void Initialise(T configuration)
		{
			OnCreate();
			OnInitialise();
			Configuration = configuration;
			MaxHealth = CurrentHealth = configuration.MaxHealth;
			Invincible = configuration.Invincible;
			Alive = true;
		}
		
		protected virtual void OnInitialise() { }

		public virtual void Damage(int amount)
		{
			if (Invincible)
			{
				return;
			}
			
			CurrentHealth -= amount;
			if (CurrentHealth <= 0)
			{
				Death();
			}
		}

		protected virtual void Death()
		{
			Alive = false;
			TearDown();
		}

		private void TearDown()
		{
			OnTearDown();
			Destroy(gameObject);
		}

		protected virtual void OnTearDown() { }
	}
}
