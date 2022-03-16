using System;
using System.Linq;
using System.Collections.Generic;
using InlineHelper;


namespace Traffy.Objects
{

    [Traffy.Annotations.PyBuiltin]
    public sealed class TrByteArray : TrObject
    {
        public FList<byte> contents;
        public override object Native => contents;
        public override string __repr__() => contents.Select(x => $"\\x{x:X}").Prepend("bytearray(b'").Append("')").By(String.Concat);
        public override bool __bool__() => contents.Count != 0;
        public override List<TrObject> __array__ => null;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override bool __le__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqLtE<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqLtE<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <=: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __lt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqLt<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqLt<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for <: '{CLASS.Name}' and '{other.Class.Name}'");

        public override bool __gt__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqGt<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqGt<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ge__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqGtE<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqGtE<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for >=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __ne__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqNe<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqNe<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for !=: '{CLASS.Name}' and '{other.Class.Name}'");


        public override bool __eq__(TrObject other) =>
            (other is TrBytes b)
            ? contents.SeqEq<FList<byte>, FArray<byte>, byte>(b.contents)
            : (other is TrByteArray byteArray)
            ? contents.SeqEq<FList<byte>, FList<byte>, byte>(byteArray.contents)
            : throw new TypeError($"unsupported operand type(s) for ==: '{CLASS.Name}' and '{other.Class.Name}'");

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrByteArray>("bytearray");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("bytearray", TrByteArray.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrByteArray)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrByteArray))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override TrObject __add__(TrObject other)
        {
            if (other is TrBytes b)
            {
                var res = new List<byte>(contents);
                res.AddRange(b.contents);
                return MK.ByteArray(res);
            }
            else if (other is TrByteArray ba)
            {
                var res = new List<byte>(contents);
                res.AddRange(ba.contents);
                return MK.ByteArray(res);
            }
            throw new TypeError($"unsupported operand type(s) for +: '{CLASS.Name}' and '{other.Class.Name}'");
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Str("");
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                if (arg is TrStr)
                    return arg;
                return MK.Str(arg.__str__());
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

}