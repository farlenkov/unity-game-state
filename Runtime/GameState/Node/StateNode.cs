using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UnityGameState
{
	public class StateNode
	{
		public string Name;
		public JObject Node { get; private set; }

		protected Dictionary<string, StateParam> ParamsByName = new Dictionary<string, StateParam>();
		protected Dictionary<string, StateNode> Childs = new Dictionary<string, StateNode>();

		public T GetState<T>(string name) where T : StateNode, new()
		{
			if (Childs.TryGetValue(name, out var childState))
			{
				return (T)childState;
			}
			else
			{
				var state = new T() { Name = name };
				state.Read(Node.GetObjectNode(name));
				Childs.Add(name, state);
				return state;
			}
		}

		// PARAMS

		public void AddParam(StateParam param, string name)
		{
			ParamsByName.Add(name, param);

			if (Node != null)
				param.Read(Node, name);
		}

		public StateBool AddBool(string name)
		{
			var param = new StateBool();
			AddParam(param, name);
			return param;
		}

		public StateInt AddInt(string name, int def = 0)
		{
			var param = new StateInt();
			AddParam(param, name);
			return param;
		}

		public StateFloat AddFloat(string name, float def = 0)
		{
			var param = new StateFloat();
			AddParam(param, name);
			return param;
		}

		public StateDouble AddDouble(string name, double def = 0)
		{
			var param = new StateDouble();
			AddParam(param, name);
			return param;
		}

		public StateString AddString(string name)
		{
			var param = new StateString();
			AddParam(param, name);
			return param;
		}

		public StateStringList AddStringList(string name)
		{
			var param = new StateStringList();
			AddParam(param, name);
			return param;
		}

		public StateVector3 AddVector3(string name)
		{
			var param = new StateVector3();
			AddParam(param, name);
			return param;
		}

		// Serialization

		public virtual bool IsEmpty()
		{
			foreach (var param in ParamsByName)
				if (!param.Value.IsEmpty())
					return false;

			foreach (var childState in Childs)
				if (!childState.Value.IsEmpty())
					return false;

			return true;
		}

		public virtual void Read(JObject node)
		{
			Node = node;

			foreach (var param in ParamsByName)
				param.Value.Read(node, param.Key);

			foreach (var childState in Childs)
				childState.Value.Read(node.GetObjectNode(childState.Key));
		}

		public virtual void Write(JObject node)
		{
			foreach (var param in ParamsByName)
			{
				if (param.Value.IsEmpty())
				{
					// Log.InfoEditor("[StateNode: Write] Remove: {0} > {1}", param.Key, param.Value.ToString());
					node.Remove(param.Key);
				}
				else
				{
					// Log.InfoEditor("[StateNode: Write] Write: {0} > {1}", param.Key, param.Value.ToString());
					param.Value.Write(node, param.Key);
				}
			}

			foreach (var childState in Childs)
			{
				if (childState.Value.IsEmpty())
					node.Remove(childState.Key);
				else
					childState.Value.Write(node.GetObjectNode(childState.Key));
			}
		}
    }
}