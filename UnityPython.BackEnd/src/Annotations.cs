using System;
using System.Collections.Generic;
using System.Linq;

namespace Traffy.Annotations
{
    public class MagicMethod: Attribute
    {
        public bool Default = false;
        public bool NonInstance = false;
    }

    public class PyBind: Attribute
    {
        public class Keyword: Attribute
        {
            public bool Only;
        }
        public string Name;
    }

    public class PyBuiltin: Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class PyInherit: Attribute
    {
        public Type[] Parents;
        public PyInherit(params Type[] parents)
        {
            Parents = parents;
        }
    }

    public enum SetupMarkKind
    {
        CreateRef = 0, // create classes and static objects
        // setup mro
        InitRef = 1, // setup methods
        // setup builtins
        SetupRef = 2, // method setup (do nothing for builtins)
        
    }

    public class SetupMark: Attribute
    {
        public SetupMarkKind Kind;   

        public SetupMark(SetupMarkKind kind)
        {
            Kind = kind;
        }
    }

    // public class Mark : Attribute
    // {
    //     public object Token;
    //     public Mark(object token = null)
    //     {
    //         Token = token;
    //     }
    //     public static IEnumerable<(Type t, Mark attr, Action method)> Query(Type entry, Func<object, bool> predicate)
    //     {
    //         return Assembly
    //             .GetAssembly(entry)
    //             .GetTypes()
    //             .SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Select(x => (t, x)))
    //             .Where(((Type t, MethodInfo mi) pair) =>
    //             {
    //                 var attr = pair.mi.GetCustomAttribute<Mark>();
    //                 return attr != null && pair.mi.GetParameters().Length == 0 && predicate(attr.Token);
    //             })
    //             .Select(((Type t, MethodInfo mi) pair) => (
    //                 pair.t,
    //                 pair.mi.GetCustomAttribute<Mark>(),
    //                 (Action)Delegate.CreateDelegate(typeof(Action), null, pair.mi)));
    //     }
    // }
}