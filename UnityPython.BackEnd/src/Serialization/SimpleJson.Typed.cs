using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;


namespace SimpleJSON
{
    public static partial class JSON
    {
        public const string VariantKey = "$type";

        static Type GetTypeConstructor(this Type self)
        {
            if (self.IsGenericType)
            {
                return self.GetGenericTypeDefinition();
            }
            return self;
        }

        static object MakeInstance(this Type self)
        {
            return Activator.CreateInstance(self, true);
        }

        static T GetStaticField<T>(this Type self, string attr) where T : class
        {
            var o = self.GetField(attr, BindingFlags.Public | BindingFlags.Static);
            if (o == null)
                return null;
            return o.GetValue(null) as T;
        }

        static Action<object[]> GetStaticMethod(this Type self, string attr)
        {
            var o = self.GetMethod(attr, BindingFlags.Public | BindingFlags.Static);
            if (o == null)
                return null;

            void call(object[] args)
            {
                o.Invoke(null, args);
            }
            return call;
        }

        static Action<object, object[]> GetInstanceMethod(this Type self, string attr)
        {
            var o = self.GetMethod(attr, BindingFlags.Public | BindingFlags.Instance);
            if (o == null)
                return null;

            void call(object self, object[] args)
            {
                o.Invoke(self, args);
            }
            return call;
        }


        static Dictionary<Type, Dictionary<string, Type>> interfaces = new Dictionary<Type, Dictionary<string, Type>>();
        // public Dictionary<Type, HashSet<string>>
        public class DeserializationSpecifier<Ctx>
        {
            public (string, Type, Action<object, object>)[] fields;
            public Func<Ctx, object, object> onDeserialized;

            public static Dictionary<Type, DeserializationSpecifier<Ctx>> deserializationActions = new Dictionary<Type, DeserializationSpecifier<Ctx>>();
        }

        public static DeserializationSpecifier<Ctx> ScanSetters_impl<Ctx>(Type typeobject)
        {
            var res = new List<(string, Type, Action<object, object>)>();
            foreach (var prop in typeobject.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanWrite && prop.GetIndexParameters().Length == 0)
                {
                    Action<object, object> setValue = prop.SetValue;
                    res.Add((prop.Name, prop.PropertyType, setValue));
                }
            }

            foreach (var field in typeobject.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                Action<object, object> setValue = field.SetValue;
                res.Add((field.Name, field.FieldType, setValue));
            }


            Func<Ctx, object, object> onDeserialized = null;
            foreach (var meth in typeobject.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (meth.GetCustomAttribute<OnDeserializedAttribute>() != null)
                {
                    if (meth.GetParameters().Length == 0)
                    {
                        object call_no_ctx(Ctx ctx, object self)
                        {
                            return meth.Invoke(self, null);
                        };

                        onDeserialized = call_no_ctx;
                    }
                    else if (meth.GetParameters().Length == 1 && meth.GetParameters()[0].ParameterType == typeof(Ctx))
                    {
                        object call_ctx(Ctx ctx, object self)
                        {
                            object [] args = { ctx };
                            return meth.Invoke(self, args);
                        };

                        onDeserialized = call_ctx;
                    }
                    break;
                }
            }
            return new DeserializationSpecifier<Ctx> { fields = res.ToArray(), onDeserialized = onDeserialized };
        }
        public static DeserializationSpecifier<Ctx> ScanSettersForConcreteClass<Ctx>(Type typeobject)
        {
            if (DeserializationSpecifier<Ctx>.deserializationActions.TryGetValue(typeobject, out var ret))
            {
                return ret;
            }
            if (typeobject.GetCustomAttribute<SerializableAttribute>() == null)
            {
                throw new InvalidCastException($"cannot deserialize {typeobject}: not marked as [Serializable].");
            }
            ret = ScanSetters_impl<Ctx>(typeobject);
            DeserializationSpecifier<Ctx>.deserializationActions[typeobject] = ret;
            return ret;
        }
        static Type TCons_Dictionary = typeof(Dictionary<object, object>).GetGenericTypeDefinition();

        static Dictionary<Type, int> TAry_Tuples = new Dictionary<Type, int> {
            {typeof(ValueTuple<>), 1},
            {typeof(ValueTuple<,>), 2},
            {typeof(ValueTuple<,,>), 3},
            {typeof(ValueTuple<,,,>), 4},
            {typeof(ValueTuple<,,,,>), 5},
            {typeof(ValueTuple<,,,,,>), 6},
            {typeof(ValueTuple<,,,,,,>), 7},
            {typeof(ValueTuple<,,,,,,,>), 8}
        };
        static void checkType<T>(Type t)
        {
            if (t != typeof(T))
            {
                throw new InvalidCastException($"expect {t}, got {typeof(T).Name}");
            }
        }
        public static object Deserialize_concreteclass<Ctx>(JSONNode node, Type t, Ctx context)
        {
            switch (node.Tag)
            {
                case JSONNodeType.Array:
                    {
                        var jarray = node.AsArray;
                        if (t.IsArray)
                        {
                            var eltype = t.GetElementType();
                            var array = Array.CreateInstance(eltype, jarray.Count);
                            for (int i = 0; i < jarray.Count; i++)
                            {
                                array.SetValue(Deserialize(jarray[i], eltype, context), i);
                            }
                            return array;
                        }
                        if (t.GetTypeConstructor() == typeof(List<>))
                        {
                            var eltype = t.GenericTypeArguments[0];
                            var lst = t.MakeInstance();
                            var Add = t.GetInstanceMethod("Add");
                            object[] args = new object[1];
                            foreach (var each in jarray)
                            {
                                args[0] = Deserialize(each, eltype, context);
                                Add(lst, args);
                            }
                            return lst;
                        }

                        var tctor = t.GetTypeConstructor();
                        if (TAry_Tuples.TryGetValue(tctor, out int nary) && jarray.Count == nary)
                        {
                            var args = new object[nary];
                            var telts = t.GenericTypeArguments;
                            for(int i = 0; i < telts.Length; i++)
                            {
                                args[i] = Deserialize(jarray[i], telts[i], context);
                            }
                            return Activator.CreateInstance(t, args);
                        }
                        throw new InvalidCastException($"json array(n={jarray.Count}) to {t}");
                    }
                case JSONNodeType.Object:
                    {
                        var jobject = node.AsObject;
                        if (t.GetTypeConstructor() == TCons_Dictionary)
                        {
                            var t_key = t.GenericTypeArguments[0];
                            Func<string, object> parse_key;
                            if (t_key == typeof(string))
                            {
                                parse_key = x => x;
                            }
                            else if (t_key == typeof(int))
                            {
                                parse_key = x => int.Parse(x);
                            }
                            else
                            {
                                throw new InvalidCastException($"dictionary key must be a string; {t} cannot be deserialized");
                            }
                            var t_value = t.GenericTypeArguments[1];
                            var Add = t.GetInstanceMethod("Add");
                            var dict = t.MakeInstance();
                            object[] args = new object[2];
                            foreach (var kv in jobject)
                            {
                                args[0] = parse_key(kv.Key);
#if DEBUG && TESTJSON
                                Console.WriteLine("spec " + node.Tag + " ." + kv.Key + " -> " + t_value.Name);
#endif
                                args[1] = Deserialize(kv.Value, t_value, context);
                                Add(dict, args);
                            }
                            return dict;
                        }
                        var action = ScanSettersForConcreteClass<Ctx>(t);
                        var obj = t.MakeInstance();
                        foreach (var (name_field, t_field, setter) in action.fields)
                        {
                                var jattr = jobject[name_field];
#if DEBUG && TESTJSON
                                Console.WriteLine("real " + node.Tag + " ." + name_field + " -> " + t_field.Name);
#endif

                            var value = Deserialize(jattr, t_field, context);
                            setter(obj, value);
                        }
                        if (action.onDeserialized != null)
                        {
                            obj = action.onDeserialized(context, obj);
                        }
                        return obj;
                    }
                case JSONNodeType.String:
                    {

                        if (typeof(string) == t)
                        {
                            return node.Value;
                        }
                        else if (typeof(byte[]) == t)
                        {
                            return System.Text.Encoding.ASCII.GetBytes(node.Value);
                        }
                        else if (typeof(InlineHelper.FArray<byte>) == t)
                        {
                            return InlineHelper.ExtInlineHelper.Inline(
                                System.Text.Encoding.ASCII.GetBytes(node.Value));
                        }
                        throw new InvalidCastException($"expect {t}, got a string");
                    }
                case JSONNodeType.Boolean:
                    {
                        checkType<bool>(t);
                        return node.AsBool;
                    }
                case JSONNodeType.Int:
                case JSONNodeType.Float:
                    {
                        if (t == typeof(int))
                        {
                            return node.AsInt;
                        }
                        if (t == typeof(uint))
                        {
                            return (uint) node.AsULong;
                        }
                        if (t == typeof(long))
                        {
                            return node.AsLong;
                        }
                        if (t == typeof(ulong))
                        {
                            return node.AsULong;
                        }
                        if (t == typeof(float))
                        {
                            return node.AsFloat;
                        }
                        if (t == typeof(double))
                        {
                            return node.AsDouble;
                        }

                        throw new NotImplementedException($"number type {t} is not supported yet.");
                    }
                case JSONNodeType.NullValue:
                    {
                        if (t.IsClass)
                        {
                            return null;
                        }
                        throw new InvalidCastException($"expect {t}; got null.");
                    }
                case JSONNodeType.Custom:
                case JSONNodeType.None:
                    throw new InvalidCastException($"expect {t}, invalid json!");
            }
            throw new NotImplementedException();
        }


        public static object Deserialize_concreteclass<Ctx>(JSONNode node, Type t, Dictionary<string, Type> types, Ctx context)
        {
            if (node.Tag == JSONNodeType.NullValue)
            {
                return null;
            }
            if (node.Tag == JSONNodeType.Object)
            {
                var jobject = node.AsObject;
                var typename = jobject.GetValueOrDefault(VariantKey, null);
                if (typename == null)
                {
                    throw new InvalidCastException($"unsolved variant type {t}; '$types' missing in json data.");
                }
                if (types.TryGetValue(typename, out var t_variant))
                {
#if DEBUG && TESTJSON
                    Console.WriteLine(node.Tag + $" ({t.Name}) is {typename}");
#endif
                    return Deserialize_concreteclass<Ctx>(node, t_variant, context);
                }
                var valid_typenames = String.Join(",", types.Keys);
                throw new InvalidCastException($"unknown variant type {t}; not in [{valid_typenames}]");
            }
            throw new InvalidCastException($"invalid cast from json {node.Tag} to {t}");
        }
        public static object Deserialize<Ctx>(JSONNode node, Type typeobject, Ctx context)
        {
            if (typeobject.IsInterface || typeobject.IsAbstract)
            {
                if (!interfaces.TryGetValue(typeobject, out var concreteClasses))
                {
                    concreteClasses = new Dictionary<string, Type>();
                    foreach (var t in typeobject.Assembly.GetTypes())
                    {
                        if (typeobject.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && t.GetCustomAttribute<SerializableAttribute>() != null)
                        {
#if DEBUG && TESTJSON
                            Console.WriteLine($"{t.Name} implements {typeobject.Name}");
#endif
                            concreteClasses[t.Name] = t;
                        }
                    }
                    interfaces[typeobject] = concreteClasses;
                }
                return Deserialize_concreteclass(node, typeobject, concreteClasses, context);
            }
            return Deserialize_concreteclass(node, typeobject, context);
        }

        public static T Deserialize<T, Ctx>(string s) where Ctx: new()
        {
            var context = new Ctx();
            return (T)Deserialize(Parse(s), typeof(T), context);
        }

        public static T Deserialize<T>(string s)
        {
            object context = null;
            return (T)Deserialize<object>(Parse(s), typeof(T), context);
        }

    }
}
