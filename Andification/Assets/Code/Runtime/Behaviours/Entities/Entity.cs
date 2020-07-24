using Andification.Runtime.Data;

namespace Andification.Runtime.Behaviours.Entities
{
	public abstract class Entity<T> : BaseEntity where T : EntityConfiguration
	{
		public T Configuration { get; private set; }

		public void Initialise(T configuration)
		{
			Configuration = configuration;
			OnCreate();
			OnInitialise();
		}

		protected virtual void OnInitialise() { }
	}
}