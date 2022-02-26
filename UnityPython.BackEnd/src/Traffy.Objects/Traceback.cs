using System;
using System.Linq;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Utils;
namespace Traffy.Objects
{

    public class FrameRecord
    {
        public string codename;
        public Metadata metadata;
        public int[] mini_traceback;

        public string GetStackTrace()
        {
            return mini_traceback
                .Select(pointer =>
                    {
                        var span = metadata.FindSpan(pointer);
                        var sourceSpan = metadata.FindSourceSpan(pointer);
                        sourceSpan = " ".Repeat(span.start.col) + sourceSpan;
                        if (span.start.line != span.end.line)
                            sourceSpan = "\n" + sourceSpan + "\n\n";
                        else
                            sourceSpan += "\n";
                        return $"---- file {metadata.filename}, {codename}: {span}\n{sourceSpan}";
                    }
                )
                .By(seq => String.Join("", seq));
        }
    }

    public class TrTraceback: TrObject
    {
        public List<FrameRecord> frameRecords = new List<FrameRecord>();
        public TrExceptionBase cause = null;

        public TrTraceback(){ }
        public void Record(string codename, Metadata metadata, int[] mini_traceback, TrExceptionBase cause)
        {
            var record = new FrameRecord
            {
                codename = codename,
                metadata = metadata,
                mini_traceback = mini_traceback
            };
            frameRecords.Add(record);
        }
        public List<TrObject> __array__ => null;

        static TrClass CLASS;
        public TrClass Class => CLASS;

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrTraceback>("Traceback");
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("Traceback.__new__", TrTraceback.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrTraceback)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrTraceback))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            // Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            throw new TypeError($"cannot create {clsobj.AsClass.Name} from user side.");
        }

        public string GetStackTrace()
        {
            return frameRecords
                .Select(x => x.GetStackTrace())
                .By(x => String.Join("\n", x))
                .By(x => cause == null ? x : $"when handling{cause.GetStackTrace()}\n{x}");
        }

    }
}