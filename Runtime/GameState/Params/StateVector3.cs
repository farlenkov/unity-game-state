using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateVector3 : StateParam<Vector3>
	{
		public event Action<Vector3, Vector3> OnChange;

		public void Set(Vector3 val)
		{
			var oldValue = Value;
			Value = val;

			if (OnChange != null)
				OnChange(Value, oldValue);
		}

		public override bool IsEmpty()
		{
			return Value.sqrMagnitude == 0;
		}

		public override void Read(JObject node, string name)
		{
			if (!node.TryGetValue(name, out var val))
			{
				Value = Vector3.zero;
			}
			else
			{
				var array = (JArray)val;
				var value = Value;

				value.x = array.Count > 0 ? (float)array[0] : 0;
				value.y = array.Count > 1 ? (float)array[1] : 0;
				value.z = array.Count > 2 ? (float)array[2] : 0;

				Value = value;
			}
		}

		public override void Write(JObject node, string name)
		{
            var array = node.GetArrayNode(name);

			if (array.Count > 0) array[0] = Value.x; else array.Add(Value.x);
			if (array.Count > 1) array[1] = Value.y; else array.Add(Value.y);
			if (array.Count > 2) array[2] = Value.z; else array.Add(Value.z);

			while (array.Count > 3)
				array.RemoveAt(3);
		}
	}
}