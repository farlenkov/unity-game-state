using System;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateString : StateParam<string>
	{
		public event Action<string, string> OnChange;

		public void Set(string val)
		{
			var oldValue = Value;

			if (val != oldValue)
			{
				Value = val;

				if (OnChange != null)
					OnChange(Value, oldValue);
			}
		}

		public void Clear()
		{
			var oldValue = Value;

			if (string.Empty != oldValue)
			{
				Value = null;

				if (OnChange != null)
					OnChange(null, oldValue);
			}
		}

		public override bool IsEmpty()
		{
			return string.IsNullOrEmpty(Value);
		}

		public override void Read(JObject node, string name)
		{
			if (node.TryGetValue(name, out var val))
				Value = (string)val;
			else
				Value = null;
		}

		public override void Write(JObject node, string name)
		{
			if (Value != null)
				node[name] = Value;
			else
				node.Remove(name);
		}
	}
}