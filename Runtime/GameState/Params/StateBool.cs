using System;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateBool : StateParam<bool>
	{
		public event Action<bool> OnChange;

		public void Set(bool val)
		{
			if (val != Value)
			{
				Value = val;

				if (OnChange != null)
					OnChange(Value);
			}
		}

		public void Toggle()
		{
			Set(!Value);
		}

		public override bool IsEmpty()
		{
			return !Value;
		}

		public override void Read(JObject node, string name)
		{
			if (node.TryGetValue(name, out var val))
				Value = (int)val == 1;
			else
				Value = false;
		}

		public override void Write(JObject node, string name = null)
		{
			node[name] = Value ? 1 : 0;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}