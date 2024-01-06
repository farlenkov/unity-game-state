using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateStringList : StateParam<List<string>>
	{
		public event Action OnChange;

		public StateStringList()
		{
			Value = new List<string>();
		}

		public void Set(string[] list)
		{
			if (Value == null)
			{
				Value = new List<string>(list);
			}
			else
			{
				Value.Clear();
				Value.AddRange(list);
			}

			if (OnChange != null)
				OnChange();
		}

		public void Set(int i, string val)
		{
			Value[i] = val;

			if (OnChange != null)
				OnChange();
		}

		public void Clear()
		{
			Value.Clear();

			if (OnChange != null)
				OnChange();
		}

		public override bool IsEmpty()
		{
			return Value.Count == 0;
		}

		public override void Read(JObject node, string name)
		{
			Value.Clear();

			if (node.TryGetValue(name, out var val))
			{
				var array = (JArray)val;

				for (var i = 0; i < array.Count; i++)
					Value.Add((string)array[i]);
			}
		}

		public override void Write(JObject node, string name)
		{
			var valueCount = Value.Count;

			if (valueCount == 0)
			{
				node.Remove(name);
				return;
			}

            var array = node.GetArrayNode(name);

            for (var i = 0; i < valueCount; i++)
			{
				if (i < array.Count)
					array[i] = Value[i];
				else
					array.Add(Value[i]);
			}

			while (array.Count > valueCount)
				array.RemoveAt(valueCount);
		}
	}
}