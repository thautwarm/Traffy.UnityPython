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
        public AbstractClass()
        {
        }
    }
}