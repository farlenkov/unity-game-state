using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public abstract class StateParam
	{
		public abstract bool IsEmpty();

		public abstract void Read(JObject node, string name);

		public abstract void Write(JObject node, string name);
	}

	public abstract class StateParam<T> : StateParam
	{
		public T Value { get; protected set; }

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}