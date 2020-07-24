using Andification.Runtime.Behaviours.Entities;
using Andification.Runtime.Data;
using UnityEngine;

namespace Andification.Runtime.Behaviours
{
	public class Projectile : MonoBehaviour, IPoolableObject<Projectile>
	{
		public ObjectPool<Projectile> SelfPool { get; set; }
		public IAttackableEntity TargetEntity { get; private set; }
		public ProjectileConfiguration Configuration { get; private set; }
		public float AliveTime { get; private set; }

		private SpriteRenderer spriteRenderer;

		public void Initialise(ProjectileConfiguration configuration, IAttackableEntity targetEntity)
		{
			//Set base values
			TargetEntity = targetEntity;
			Configuration = configuration;
			AliveTime = 0;

			//Update sprite
			spriteRenderer = spriteRenderer == null ? gameObject.AddComponent<SpriteRenderer>() : spriteRenderer;
			spriteRenderer.sprite = configuration.ProjectileSprite;

			//Start Projectile
			Configuration.ProjectileStart(this);
		}

		private void Update()
		{
			AliveTime += Time.deltaTime;

			//Give control to the configuration
			Configuration.ProjectileUpdate(this);
		}

		public void TearDown()
		{
			SelfPool.ReturnPoolObject(this);
		}
	}
}