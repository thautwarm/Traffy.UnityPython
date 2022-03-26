using System;

namespace Traffy.Interfaces
{
    public class AbsMember : Attribute
    {
        public AbsMember()
        { }
    }

    public class MixinMember : Attribute
    {
        public MixinMember()
        { }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public partial class AbstractClass : Attribute
    {
        public Type[] Parents;
        public AbstractClass(params Type[] parents)
        {
            Parents = parents;
        }
    }
}