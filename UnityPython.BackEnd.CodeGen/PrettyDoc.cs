namespace PrettyDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ExtPrettyDoc;
    public static class ExtPrettyDoc
    {
        public static Doc Doc(this string self) => new Doc_LineSegment(self);

        public static Doc Doc(this object self) => new Doc_LineSegment(self.ToString() ?? "");
        public static Doc Align(this Doc self) => new Doc_Align(self);
        public static Doc Indent(this int self, Doc doc) => new Doc_Indent(self, doc);
        public static Doc Indent(this Doc doc, int i) => new Doc_Indent(i, doc);
        public static Doc Empty = new Doc_Empty();
        public static Doc Comma = ", ".Doc();
        public static Doc Space = " ".Doc();
        public static (Doc, Doc) Parens = ("(".Doc(), ")".Doc());
        public static (Doc, Doc) Brace = ("{".Doc(), "}".Doc());
        public static (Doc, Doc) Angle = ("<".Doc(), ">".Doc());
        public static (Doc, Doc) Bracket = ("[".Doc(), "]".Doc());
        public static Doc VSep(params Doc[] docs) => new Doc_VSep(docs);
        public static Doc NewLine = VSep(Empty, Empty);
        public static Doc SepOf(this Doc self, params Doc[] args)
        {
            if(args.Length == 0)
            {
                return Empty;
            }
            var res = args[0];
            for(int i = 1; i < args.Length; i++)
            {
                res = res * self * args[i];
            }
            return res;
        }

        public static Doc Join(this IEnumerable<Doc> args, Doc sep)
        {
            return sep.SepOf(args.ToArray());
        }
    }
    public abstract class Doc
    {
        public static Doc operator +(Doc a, Doc b) => a * Space * b;
        public static Doc operator *(Doc a, Doc b) => new Doc_Concat(a, b);

        public static Doc operator >>(Doc a, int b) => a.Indent(b);
        public Doc SurroundedBy((Doc l, Doc r) lr) => lr.l * this * lr.r;
        public Doc Surround(Doc l, Doc r) => l * this * r;
        public static T[] array_concat<T>(T[] a, T[] b)
        {
            var result = new T[a.Length + b.Length];
            a.CopyTo(result, 0);
            b.CopyTo(result, a.Length);
            return result;
        }

        public void Render()
        {
            Render(Console.Write);
        }

        public void Render(Action<string> write)
        {
            Render(write, RenderOptions.Default);
        }
        public void Render(Action<string> write, RenderOptions opts)
        {
            var sentences = Compile();
            PDoc.Render(opts, sentences, write);
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            void write(string s)
            {
                sb.Append(s);
            }
            Render(write);
            return sb.ToString();
        }
        public PDoc[][] Compile()
        {
            switch (this)
            {
                case Doc_LineSegment doc:
                    return new PDoc[][] {
                        new PDoc[] {
                            new PDoc_LineSegment(doc.text)
                        } };
                case Doc_Concat doc:
                {
                    var l = doc.l.Compile();
                    var r = doc.r.Compile();
                    if (l.Length == 0)
                    {
                        return r;
                    }
                    else if (r.Length == 0)
                    {
                        return l;
                    }
                    else
                    {
                        var result = new PDoc[l.Length + r.Length - 1][];
                        var mid = array_concat(l[l.Length - 1], r[0]);
                        return l.SkipLast(1).Append(mid).Concat(r.Skip(1)).ToArray();
                    }
                }
                case Doc_Align doc:
                {
                    var it = doc.it.Compile();
                    if (it.Length == 0)
                    {
                        return it;
                    }
                    it[0] = it[0].Prepend(new PDoc_PushCurrentIndent()).ToArray();
                    it[it.Length - 1] = it[it.Length - 1].Append(new PDoc_PopIndent()).ToArray();
                    return it;
                }

                case Doc_Indent doc:
                {
                    var prefix = new PDoc_PushIndent(doc.indent);
                    var it = doc.it.Compile();
                    if (it.Length == 0)
                    {
                        return it;
                    }
                    it[0] = it[0].Prepend(prefix).ToArray();
                    it[it.Length - 1] = it[it.Length - 1].Append(new PDoc_PopIndent()).ToArray();
                    return it;
                }
                case Doc_Empty _:
                    return new PDoc[][] { new PDoc[0] };
                case Doc_VSep doc:
                    return doc.elements.SelectMany(e => e.Compile()).ToArray();
                default:
                    throw new Exception($"Unhandled case {this.GetType()}");
            }
        }
    }

    public abstract class PDoc
    {

        public static void Render(RenderOptions opts, PDoc[][] sentences, Action<string> write)
        {
            Stack<int> levels = new Stack<int>();
            levels.Push(0);

            if (sentences.Length == 0)
            {
                return;
            }

            for(int i = 0; i < sentences.Length; i++)
            {
                var segments = sentences[i];
                int col = 0;
                bool initialized = false;

                void line_init()
                {
                    if (!initialized)
                    {
                        col = levels.Peek();
                        write(new string(' ', col));
                        initialized = true;
                    }
                }

                foreach(var seg in segments)
                {
                    switch (seg)
                    {
                        case PDoc_LineSegment seg_:
                            line_init();
                            write(seg_.text);
                            col += seg_.text.Length;
                            break;
                        case PDoc_PushCurrentIndent _:
                            levels.Push(col);
                            line_init();
                            break;
                        case PDoc_PopIndent _:
                            levels.Pop();
                            line_init();
                            break;
                        case PDoc_PushIndent seg_:
                            levels.Push(levels.Peek() + seg_.indent);
                            line_init();
                            break;
                        default:
                            throw new InvalidCastException($"unexpected segment {seg}");
                    }
                }
                if (i != sentences.Length - 1)
                    write("\n");
            }
        }
    }

    public class Doc_Empty: Doc {}
    public class Doc_Concat : Doc
    {
        public Doc l;
        public Doc r;
        public Doc_Concat(Doc l, Doc r)
        {
            this.l = l;
            this.r = r;
        }
    }

    public class Doc_VSep : Doc
    {
        public Doc[] elements;

        public Doc_VSep(params Doc[] elements)
        {
            this.elements = elements;
        }

    }

    public class Doc_Align : Doc
    {
        public Doc it;
        public Doc_Align(Doc it)
        {
            this.it = it;
        }
    }

    public class Doc_Indent : Doc
    {
        public int indent;
        public Doc it;
        public Doc_Indent(int ind, Doc it)
        {
            this.indent = ind;
            this.it = it;
        }
    }

    public class Doc_LineSegment : Doc
    {
        public string text;
        public Doc_LineSegment(string text)
        {
            this.text = text;
        }
    }

    public class PDoc_PopIndent : PDoc
    { }

    public class PDoc_PushIndent : PDoc
    {
        public int indent;
        public PDoc_PushIndent(int indent)
        {
            this.indent = indent;
        }
    }

    public class PDoc_PushCurrentIndent : PDoc
    {
    }

    public class PDoc_LineSegment : PDoc
    {
        public string text;
        public PDoc_LineSegment(string text)
        {
            this.text = text;
        }
    }

    public struct RenderOptions
    {
        public int ExpectLineLength;

        public static RenderOptions Default = new RenderOptions { ExpectLineLength = 80 };
    }
}