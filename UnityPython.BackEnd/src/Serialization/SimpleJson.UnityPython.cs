using System.Collections.Generic;
using Traffy;
using Traffy.Objects;

namespace SimpleJSON
{

    public static partial class JSON
    {
        public static JSONNode PyToJson(TrObject py)
        {
            HashSet<TrObject> visited = new HashSet<TrObject>(new IdComparer());
            return _PyToJson(visited, py);
        }
        public static JSONNode _PyToJson(HashSet<TrObject> visited, TrObject py)
        {
            if(!visited.Add(py))
                throw new ValueError("Cannot serialize cyclic reference in JSON data");

            switch(py)
            {
                case TrList list:
                {
                    var ret = new JSONArray();
                    foreach(var each in list.container)
                    {
                        ret.Add(_PyToJson(visited, each));
                    }
                    return ret;
                }
                
                case TrTuple tuple:
                {
                    var ret = new JSONArray();
                    foreach(var each in tuple.elts.UnList)
                    {
                        ret.Add(_PyToJson(visited, each));
                    }
                    return ret;
                }
                case TrDict dict:
                {
                    var ret = new JSONObject();
                    foreach(var kv in dict.container)
                    {
                        ret[kv.Key.__str__()] = _PyToJson(visited, kv.Value);
                    }
                    return ret;
                }
                case TrInt int_:
                {
                    return int_.value;
                }
                case TrFloat float_:
                {
                    return float_.value;
                }
                case TrStr s:
                {
                    return new JSONString(s.value);
                }
                case TrBool b:
                {
                    return new JSONBool(b.value);
                }
                case TrNone _:
                {
                    return JSONNull.CreateOrGet();
                }
                default:
                {
                    throw new TypeError($"PyToJson: unsupported type {py.Class.Name}");
                }
            }

        }
        public static TrObject JSONToPy(JSONNode node)
        {
            switch (node.Tag)
            {
                case JSONNodeType.Array:
                {
                    var jarray = node.AsArray;
                    var ret = RTS.barelist_create(jarray.Count);
                    foreach (var each in jarray)
                    {
                        RTS.barelist_add(ret, JSONToPy(each));
                    }
                    return MK.List(ret);
                }
                case JSONNodeType.Object:
                {
                    var jobject = node.AsObject;
                    var ret = RTS.baredict_create();
                    foreach(var kv in jobject)
                    {
                        var key = kv.Key;
                        var value = JSONToPy(kv.Value);
                        RTS.baredict_set(ret, MK.Str(key), value);
                    }
                    return MK.Dict(ret);
                }
                case JSONNodeType.String:
                {
                    return MK.Str(node.Value);
                }
                case JSONNodeType.Boolean:
                {
                    return MK.Bool(node.AsBool);
                }
                case JSONNodeType.Int:
                {
                    return MK.Int(node.AsLong);
                }
                case JSONNodeType.Float:
                {
                    return MK.Float(node.AsDouble);
                }
                case JSONNodeType.NullValue:
                {
                    return MK.None();
                }
                default:
                    throw new ValueError("invalid json");
            }
        }
    }
}