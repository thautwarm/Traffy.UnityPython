using System;
using Traffy.Objects;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Traffy
{
    public static class ListExt
    {
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void Push(this List<TrObject> self, TrObject o)
        {
            self.Add(o);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]

        public static TrObject Peek(this List<TrObject> self)
        {
            return self[self.Count - 1];
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static TrObject Pop(this List<TrObject> self)
        {
            var i = self.Count - 1;
            var a = self[i];
            self.RemoveAt(i);
            return a;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static (TrObject, TrObject) Pop2(this List<TrObject> self)
        {
            var i = self.Count - 1;
            var a = self[i - 1];
            var b = self[i];
            self.RemoveAt(i);
            self.RemoveAt(i - 1);
            return (a, b);
        }


        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void PopN(this List<TrObject> self, List<TrObject> other, int n)
        {
            int c = self.Count;
            for (int i = c - n; i < c; i++)
            {
                other.Add(self[i]);
            }
            self.RemoveRange(c - n, n);
            return;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void PopN(this List<TrObject> self, TrObject[] other, int n)
        {
            int c = self.Count;
            self.CopyTo(c - n, other, 0, n);
            self.RemoveRange(c - n, n);
            return;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void PopN(this List<TrObject> self, Dictionary<TrObject, TrObject> other, int n)
        {
            int c = self.Count;
            int start = c - n - n;
            for (int i = start; i < c; i += 2)
            {
                other[self[i]] = self[i + 1];
            }
            self.RemoveRange(start, n + n);
            return;
        }
    }

    public class Variable
    {
        public TrObject Value;
        public Variable(TrObject value)
        {
            Value = value;
        }
    }


    public struct Postion
    {
        public int line;
        public int col;

        public override string ToString()
        {
            return $"line {line}, colum {col}";
        }
    }

    public struct Span
    {
        public Postion start;
        public Postion end;

        public override string ToString()
        {
            return $"{start} - {end}";
        }

        public string UnsafeClip(string sourceCode)
        {
            var span = this;
            int line = 1;
            int offset = -1;
            if (line != span.start.line)
                while (++offset < sourceCode.Length)
                {
                    if (sourceCode[offset] == '\n')
                    {
                        if (++line == span.start.line) break;
                    }
                }
            int start_offset = offset + span.start.col + 1;
            if (line != span.end.line)
                while (++offset < sourceCode.Length)
                {
                    if (sourceCode[offset] == '\n')
                    {
                        if (++line == span.end.line) break;
                    }
                }
            int end_offset = offset + span.end.col + 1;
            return sourceCode.Substring(start_offset, end_offset - start_offset);
        }
    }


    [Serializable]
    public class Metadata
    {
        // compactly encoded (int32, int32) pairs as int32[]
        //   'undecoded(span_pointer_compressed: int[])'
        //     =
        //   'span_pointers: (int start, int end)[]'
        // given 'undecoded(position_compressed: int[]) = positions: (int line, int col)[]'
        // a span pointed by the index 'i' at 'span_pointer_compressed' represents
        //    '(positions[span_pointers[i].start], positions[span_pointers[i].end])'
        public uint[] compressedSpanPointers;

        // compactly encoded (int32, int32) pairs as int32[]
        public uint[] compressedPositions;
        public string[] localnames;
        public string[] freenames;
        public string codename;
        public string filename;
        public string sourceCode; // in release mode, can be null

        private Postion[] _positions;
        private (int start, int end)[] _spanPointers;

        [OnDeserialized]
        public Metadata OnDeserialized()
        {
            // Positions.Select(x => x.ToString()).By(x => String.Join(",", x)).By(Console.WriteLine);
            return this;
        }

        private (int start, int end)[] SpanPointers
        {
            get
            {
                if (_spanPointers == null)
                {
                    _spanPointers = Traffy.Utils.IntEncoding.decode(compressedSpanPointers, (start, end) => (start, end));
                }
                return _spanPointers;
            }
        }
        public Postion[] Positions
        {
            get
            {
                if (_positions == null)
                {
                    _positions = Traffy.Utils.IntEncoding.decode(compressedPositions, (line, col) => new Postion { line = line, col = col });
                }
                return _positions;
            }
        }

        public Span FindSpan(int pointer)
        {
            if (_spanPointers == null)
            {
                _spanPointers = Traffy.Utils.IntEncoding.decode(compressedSpanPointers, (start, end) => (start, end));

                // _positions.Select(x => x.ToString()).By(x => String.Join(",", x)).By(Console.WriteLine);
            }
            return new Span { start = Positions[_spanPointers[pointer].start], end = Positions[_spanPointers[pointer].end] };
        }

        public string FindSourceSpan(int pointer)
        {
            Span span = FindSpan(pointer);
            if (sourceCode == null)
                return "";
            // skip the first 'start_line-1' lines and the following 'start_col-1' chars
            return span.UnsafeClip(sourceCode);
        }
    }

    // |arg|vararg|kwonlys|kwarg|
    [Serializable]
    public class TrFuncPointer
    {
        public bool hasvararg;
        public bool haskwarg;
        public int posargcount;
        public int allargcount;
        public Dictionary<int, TrObject> kwindices;
        public Traffy.Asm.TraffyAsm code;
        public Metadata metadata;
        static Variable[] empty_freevars = new Variable[0];
        static (int, TrObject)[] empty_default_args = new (int, TrObject)[0];
        public void Exec(Dictionary<TrObject, TrObject> globals)
        {
            if (code.hasCont)
            {
                throw new InvalidOperationException("cannot eval the code that has continuations");
            }
            var func = new TrFunc(empty_freevars, globals, empty_default_args, this);
            ((TrObject)func).Call();
        }
    }
    public enum STATUS
    {
        NORMAL = 0,
        CONTINUE = 1,
        BREAK = 2,
        RETURN = 3,
    }

    public class Frame
    {
        public STATUS CONT;
        int __pad;
        public TrFunc func;
        public Exception err;
        public Variable[] localvars;
        public Variable[] freevars;
        public TrObject retval;
        public Stack<int> traceback;
        public Stack<int> marked;

        public static Frame UnsafeMake(TrFunc func) => new Frame
        {
            func = func,
            freevars = func.freevars,
            retval = RTS.object_none,
            traceback = new Stack<int>(),
            marked = new Stack<int>()
        };

        public static Frame Make(TrFunc func, Variable[] localvars) => new Frame
        {
            func = func,
            freevars = func.freevars,
            localvars = localvars,
            retval = RTS.object_none,
            traceback = new Stack<int>(),
            marked = new Stack<int>()
        };

        internal Variable load_reference(int operand)
        {
            if (operand < 0)
            {
                return func.freevars[-operand - 1];
            }
            return localvars[operand];
        }
        internal void delete_local(int operand)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal void store_local(int operand, TrObject o)
        {
            localvars[operand].Value = o;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal void store_free(int operand, TrObject o)
        {
            freevars[operand].Value = o;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal TrObject load_local(int operand)
        {
            return localvars[operand].Value;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal TrObject load_free(int operand)
        {
            return freevars[operand].Value;
        }

        internal void delete_global(TrObject v)
        {
            throw new NotImplementedException();
        }

        internal void store_global(TrObject v, TrObject trObject)
        {
            func.globals[v] = trObject;
        }

        internal TrObject load_global(TrObject name)
        {
            if (func.globals.TryGetValue(name, out var v))
            {
                return v;
            }
            throw new NameError("global", name.__str__());
        }

        internal void clear_exception()
        {
            err = null;
        }

        internal void set_exception(Exception e)
        {
            var new_err = RTS.exc_wrap_frame(e, this);
            if (err != null)
            {
                var new_err_ = RTS.exc_frombare(new_err);
                new_err_.UnsafeCause = RTS.exc_frombare(err);
                err = new_err_.AsException();
            }
            else
            {
                err = new_err;
            }
        }

        internal Exception get_exception()
        {
            return err;
        }

        internal bool has_exception()
        {
            return err != null;
        }

        internal Exception exc_notset()
        {
            throw new NotImplementedException();
        }

        internal void mark()
        {
            marked.Push(traceback.Count);
        }

        internal void restore()
        {
            int count = marked.Pop();
            while (traceback.Count > count)
            {
                traceback.Pop();
            }
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal SuppressControlFlow suppress_control_flow()
        {
            return new SuppressControlFlow(this);
        }
    }

    public struct SuppressControlFlow: IDisposable
    {
        public STATUS CONT;
        public Frame Frame;

        public static STATUS MostSevere(STATUS a, STATUS b)
        {
            if (a > b) return a;
            return b;
        }
        public SuppressControlFlow(Frame frame)
        {
            CONT = frame.CONT;
            Frame = frame;
            Frame.CONT = STATUS.NORMAL;
        }

        public void Dispose()
        {
            Frame.CONT =  MostSevere(Frame.CONT, CONT);
        }
    }
}