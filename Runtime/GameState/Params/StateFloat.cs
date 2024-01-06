using System;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateFloat : StateParam<float>
	{
		public event Action<float, float, float> OnChange;

		public void Set(float val)
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

		public float Add(float count)
		{
			return Change(count);
		}

		public float Del(float count)
		{
			return Change(-count);
		}

		public float Change(float delta)
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
				Value = (float)val;
			else
				Value = 0;
		}

		public override void Write(JObject node, string name)
		{
			node[name] = Value;
		}
	}
}