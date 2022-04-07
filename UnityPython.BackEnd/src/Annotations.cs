using System;

namespace Traffy.Annotations
{
    public class MagicMethod : Attribute
    {
        public bool Default = false;
        public bool NonInstance = false;
    }

    public class UnitySpecific : Attribute{ }
    public class PyBind : Attribute
    {
        public class Keyword : Attribute
        {
            public bool Only;
            public string Name;
        }

        public class SelfProp : Attribute
        {
            public string Name;
            public SelfProp(string name)
            {
                Name = name;
            }
        }
        public string Name;
    }

    public class PyBuiltin : Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class PyInherit : Attribute
    {
        public Type[] Parents;
        public PyInherit(params Type[] parents)
        {
            Parents = parents;
        }
    }

    public enum SetupMarkKind
    {
        StaticInit = 0,
        CreateRef = 1, // create classes and static objects
        // setup mro
        InitRef = 2, // setup methods
        // setup builtins
        SetupRef = 3, // method setup (do nothing for builtins)

    }

    public class SetupMark : Attribute
    {
        public SetupMarkKind Kind;

        public SetupMark(SetupMarkKind kind)
        {
            Kind = kind;
        }
    }
}