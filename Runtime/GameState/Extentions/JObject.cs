using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameState
{
    public static class JObjectExt
    {
        public static JObject GetObjectNode(this JObject node, string name)
        {
            return node.TryGetValue(name, out var val)
                ? (JObject)val
                : (JObject)(node[name] = new JObject());
        }

        public static JArray GetArrayNode(this JObject node, string name)
        {
            return node.TryGetValue(name, out var val)
                ? (JArray)val
                : (JArray)(node[name] = new JArray());
        }
    }
}
