using System;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateDouble : StateParam<double>
	{
		public event Action<double, double, double> OnChange;

		public void Set(double val)
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

		public double Add(double count)
		{
			return Change(count);
		}

		public double Del(double count)
		{
			return Change(-count);
		}

		public double Change(double delta)
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
				Value = (double)val;
			else
				Value = 0;
		}

		public override void Write(JObject node, string name = null)
		{
			node[name] = Value;
		}
	}
}