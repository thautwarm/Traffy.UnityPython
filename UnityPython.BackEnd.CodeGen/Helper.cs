using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CSAST;
using PrettyDoc;
using Traffy;
using Traffy.Annotations;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;

public static class Helper
{
    static HashSet<string> _Keywords = new HashSet<string>{
        "abstract",
        "as",
        "base",
        "bool",
        "break",
        "byte",
        "case",
        "catch",
        "char",
        "checked",
        "class",
        "const",
        "continue",
        "decimal",
        "default",
        "delegate",
        "do",
        "double",
        "else",
        "enum",
        "event",
        "explicit",
        "extern",
        "false",
        "finally",
        "fixed",
        "float",
        "for",
        "foreach",
        "goto",
        "if",
        "implicit",
        "in",
        "int",
        "interface",
        "internal",
        "is",
        "lock",
        "long",
        "namespace",
        "new",
        "null",
        "object",
        "operator",
        "out",
        "override",
        "params",
        "private",
        "protected",
        "public",
        "readonly",
        "ref",
        "return",
        "sbyte",
        "sealed",
        "short",
        "sizeof",
        "stackalloc",
        "static",
        "string",
        "struct",
        "switch",
        "this",
        "throw",
        "true",
        "try",
        "typeof",
        "uint",
        "ulong",
        "unchecked",
        "unsafe",
        "ushort",
        "using",
        "virtual",
        "void",
        "volatile",
        "while",
    };
    public static bool IsKeyword(string s)
    {
        return _Keywords.Contains(s);
    }
    public static string ValidName(this string s)
    {
        // if 's' is a C# keyword
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/
        if (IsKeyword(s))
            return $"@{s}";
        return s;
    }
    public static CSExpr THint(Type t) => new EType((new TId("THint"))[t])["Unique"];
    public static CSExpr Unbox = (new EId("Unbox"))["Apply"];
    public static CSExpr Box = (new EId("Box"))["Apply"];
    public static CSExpr PYARGS = new EId(CSExpr.ARGS);
    public static CSExpr PYKWARGS = new EId(CSExpr.KWARGS);


    public static CSExpr strToPy(string s)
    {
        var mk = new EId("MK");
        return mk["Str"].Call(new EStr(s));
    }

    public static (int, int) countPositionalDefault(MethodInfo meth)
    {
        bool positionalComeToEnd = false;
        int positionalDefaultCount = 0;
        int keywordOnlyArguments = 0;
        foreach(var p in meth.GetParameters())
        {
            var attr = p.GetCustomAttribute<PyBind.Keyword>();
            if (attr != null)
            {
                if (attr.Only || positionalComeToEnd)
                {
                    if (!p.IsOptional)
                        throw new Exception($"{meth}: Keyword {p.Name} only argument must be optional");
                    positionalComeToEnd = true;
                    keywordOnlyArguments++;
                }
                else
                {
                    positionalDefaultCount++;
                }
            }
            else
            {
                if (positionalComeToEnd)
                {
                    throw new Exception($"{meth}: (arg: {p.Name}) Positional arguments must come before keyword arguments");
                }
                if (p.IsOptional)
                {
                    positionalDefaultCount++;
                }
            }
        }
        var positionalNonDefaultCount = meth.GetParameters().Length - keywordOnlyArguments - positionalDefaultCount;
        return (positionalNonDefaultCount, positionalDefaultCount);
    }
    public static IEnumerable<CSStmt> CallFunc(Type retType, (Type t, string Name, object Default)[] ps, int narg, Func<CSExpr[], (string, CSExpr)[], CSExpr> invoke)
    {
        Func<int, string> variable = i => $"_{i}";
        var args = new List<CSExpr>();
        var keywords = new List<(string, CSExpr)>();
        for (int i = 0; i < narg; i++)
        {
            yield return new SDecl(variable(i), null, Unbox.Call(THint(ps[i].t), PYARGS[i]));
            args.Add(new EId(variable(i)));
        }
        for (int i = narg; i < ps.Length; i++)
        {
            yield return new SDecl(variable(i), ps[i].t, null);

            CSStmt elsedo = DBNull.Value == ps[i].Default
                ? new SError(CSExpr.OfConst($"Missing keyword-only argument {ps[i].Name}"))
                : new SAssign(
                    new EId(variable(i)),
                    ps[i].Default is PyBind.SelfProp selfProp
                    ? new EId(variable(0))[ValidName(selfProp.Name)]
                    : CSExpr.OfConst(ps[i].Default));
            yield return new SIf(
                PYKWARGS.IsNotNull().And(
                    PYKWARGS["TryGetValue"].Call(strToPy(ps[i].Name), new EVarOut("__keyword_" + variable(i)))),
                new SAssign(new EId(variable(i)), Unbox.Call(THint(ps[i].t), new EId("__keyword_" + variable(i)))).SingletonArray(),
                elsedo.SingletonArray()
            );
            keywords.Add((ValidName(ps[i].Name), new EId(variable(i))));
        }
        var call = invoke(args.ToArray(), keywords.ToArray());
        if (retType != typeof(void))
        {
            yield return new SReturn(
                Box.Call(call)
            );
        }
        else
        {
            yield return new SExpr(call);
            yield return new SReturn(new EId("Traffy.MK")["None"].Call());
        }
    }
}