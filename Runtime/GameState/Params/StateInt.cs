using System;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateInt : StateParam<int>
	{
		public event Action<int, int, int> OnChange;

		public void Set(int val)
		{
			if (val != Value)
			{
				var oldValue = Value;
				var delta = val - oldValue;
				Value = val;

				if (OnChange != null)
					OnChange(Value, oldValue, delta);
			}
		}

		public int Add(int count)
		{
			return Change(count);
		}

		public int Del(int count)
		{
			return Change(-count);
		}

		public int Change(int delta)
		{
			if (delta != 0)
			{
				var oldValue = Value;
				Value += delta;

				if (OnChange != null)
					OnChange(Value, oldValue, delta);
			}

			return Value;
		}

		public override bool IsEmpty()
		{
			return Value == 0;
		}

		public override void Read(JObject node, string name)
		{
			if (node.TryGetValue(name, out var val))
				Value = (int)val;
			else
				Value = 0;
		}

		public override void Write(JObject node, string name)
		{
			node[name] = Value;
		}
	}
}