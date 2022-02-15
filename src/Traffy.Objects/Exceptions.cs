using System;
namespace Traffy.Objects
{
    public class NameError: Exception
    {
        public string Kind;
        public string VarName;
        static string formatMsg(string kind, string name)
        {
            // TODO
            return $"{kind} {name}";
        }
        public NameError(string kind, string varname) : base(formatMsg(kind, varname)) {
            Kind = kind;
            VarName = varname;
        }
    }

    public class TypeError: Exception
    {
        public TypeError(string msg) : base(msg) {}
    }


    public class AttributeError: Exception
    {
        public AttributeError(string msg) : base(msg) {}
    }


    public class ValueError: Exception
    {
        public ValueError(string msg) : base(msg) {}
    }


    public class StopIteration: Exception
    {
        public TrObject value;
        public StopIteration(TrObject value, string msg) : base(msg) {}
        public StopIteration(TrObject value) : base() {}
    }




}
