using marketplace.Models;
using System.Reflection;

namespace marketplace.Helpers
{
	public class StateFactory
	{
		private static Dictionary<string, State> statesCache = FindAllDerivedStates();

		public static State GetState(string stateTypeName)
		{
			return statesCache[stateTypeName];
		}

		private static Dictionary<string, State> FindAllDerivedStates()
		{
			var derivedType = typeof(State);
			var assembly = Assembly.GetAssembly(typeof(State));
			return assembly.GetTypes().Where(t => t != derivedType && derivedType.IsAssignableFrom(t))
						.Select(t => (State)Activator.CreateInstance(t))
						.ToDictionary(k => k.GetType().Name);
		}
	}
}
