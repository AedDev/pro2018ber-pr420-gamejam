using Andification.Runtime.Behaviours;
using Andification.Runtime.Behaviours.Entities;
using Andification.Runtime.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Andification.Runtime.Factories
{
	public static class ProjectileFactory
	{
		private static ObjectPool<Projectile> ProjectilePool;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialise()
		{
			SetupPool();
			SceneManager.activeSceneChanged += ActiveSceneChanged;
			Application.quitting += TearDown;
		}

		private static void ActiveSceneChanged(Scene prevScene, Scene newScene)
		{
			SetupPool();
		}

		private static void SetupPool()
		{
			ProjectilePool = new ObjectPool<Projectile>(CreateNewEmptyProjectile);
		}

		private static void TearDown()
		{
			SceneManager.activeSceneChanged -= ActiveSceneChanged;
			Application.quitting -= TearDown;
		}

		public static void CreateProjectile(ProjectileConfiguration projectileConfiguration, IAttackableEntity projectileTarget)
		{
			Projectile projectile = ProjectilePool.GetPoolObject();
			projectile.Initialise(projectileConfiguration, projectileTarget);
		}

		private static Projectile CreateNewEmptyProjectile()
		{
			GameObject projectileObject = new GameObject("Projectile");

			//TODO: Give the projectile a parent
			projectileObject.transform.SetParent(null);
			Projectile projectile = projectileObject.AddComponent<Projectile>();

			return projectile;
		}
	}
}