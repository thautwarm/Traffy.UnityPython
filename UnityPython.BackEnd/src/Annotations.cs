using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Traffy.Annotations
{
    public class MagicMethod: Attribute
    {
        public bool Instance = false;

        public static void Call()
        {
            typeof(Traffy.Objects.TrObject)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .ForEach(Console.WriteLine);
        }
    }

    public class Mark : Attribute
    {
        public object Token;
        public Mark(object token = null)
        {
            Token = token;
        }
        public static IEnumerable<(Type t, Mark attr, Action method)> Query(Type entry, Func<object, bool> predicate)
        {
            return Assembly
                .GetAssembly(entry)
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Select(x => (t, x)))
                .Where(((Type t, MethodInfo mi) pair) =>
                {
                    var attr = pair.mi.GetCustomAttribute<Mark>();
                    return attr != null && pair.mi.GetParameters().Length == 0 && predicate(attr.Token);
                })
                .Select(((Type t, MethodInfo mi) pair) => (
                    pair.t,
                    pair.mi.GetCustomAttribute<Mark>(),
                    (Action)Delegate.CreateDelegate(typeof(Action), null, pair.mi)));
        }

    }
}