using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PrettyDoc;
using Traffy;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;

namespace CSAST
{
    public interface CSAST
    {
        public Doc Doc();
    }

    public static class ExtStmt
    {
        public static Doc BlockDoc(this CSStmt[] stmts)
        {
            if (stmts.Length == 1)
            {
                return stmts[0].Doc().Indent(4);
            }
            return VSep(
                "{".Doc(),
                VSep(stmts.Select(x => x.Doc()).ToArray()).Indent(4),
                "}".Doc()
            );
        }

        public static CSExpr IsNotNull(this CSExpr a) => new EOp("!=", a, new EId("null"));

        public static CSExpr And(this CSExpr a, CSExpr b) => new EOp("&&", a, b);
    }

    public abstract class CSExpr : CSAST
    {
        public const string ARGS = "__args";
        public const string KWARGS = "__kwargs";

        public abstract Doc Doc();
        public CSExpr this[string s] => new EAttr(s, this);

        public CSExpr this[CSExpr s] => new EItem(this, s);

        public CSExpr Not() => new ENot(this);
        public CSExpr Call(params CSExpr[] arguments) => new ECall(this, arguments);
        public CSStmt Switch(params Case[] cases) => new SSwitch(this, cases);

        public CSExpr Assign(CSExpr value) => new EAssign(this, value);

        public CSExpr Cast(Type t) => new ECast(this, t);

        public static implicit operator CSExpr(int i) => new EInt(i);
        public static CSExpr operator+(CSExpr a, CSExpr b) => new EOp("+", a, b);
        public static CSExpr operator-(CSExpr a, CSExpr b) => new EOp("+", a, b);

        public static CSExpr OfConst(object o)
        {
            if (o == null)
                return new EId("null");

            switch(o)
            {
                case int i:
                    return new EInt(i);
                case string s:
                    return new EStr(s);
                case bool b:
                    return new EBool(b);
                case float f:
                    return new EFloat(f);
                default:
                    throw new Exception($"Unsupported type {o.GetType().Name}");
            }

        }
    }

    public abstract class CSStmt: CSAST
    {
        public abstract Doc Doc();
    }

    public class SAssign : CSStmt
    {
        public CSExpr left;
        public CSExpr right;
        public SAssign(CSExpr left, CSExpr right)
        {
            this.left = left;
            this.right = right;
        }
        public override Doc Doc()
        {
            return left.Doc() + "=".Doc() + right.Doc() * ";".Doc();
        }
    }

    public class SDecl : CSStmt
    {
        public string n;

        public CSType t;

        public CSExpr expr;

        public SDecl(string n, CSType t, CSExpr expr)
        {
            this.n = n;
            this.t = t;
            this.expr = expr;
        }

        public override Doc Doc()
        {
            if (t == null)
                return "var".Doc() + n.Doc() + "=".Doc() + expr.Doc() * ";".Doc();
            if (expr == null)
                return t.Doc() + n.Doc() * ";".Doc();
            return t.Doc() + n.Doc() + "=".Doc() + expr.Doc() * ";".Doc();
        }
    }

    public class SExpr : CSStmt
    {
        public CSExpr expr;
        public SExpr(CSExpr expr)
        {
            this.expr = expr;
        }
        public override Doc Doc()
        {
            return expr.Doc() * ";".Doc();
        }
    }
    public class SError : CSStmt
    {
        public CSExpr msg;
        public SError(CSExpr msg)
        {
            this.msg = msg;
        }
        public override Doc Doc()
        {
            return "throw new ValueError(".Doc() + msg.Doc() + ");".Doc();
        }
    }


    public class SReturn : CSStmt
    {
        public CSExpr value;
        public SReturn(CSExpr value)
        {
            this.value = value;
        }

        public SReturn()
        {
            this.value = null;
        }
        public override Doc Doc()
        {
            if (value == null)
                return "return;".Doc();
            return "return".Doc() + value.Doc() * ";".Doc();
        }
    }

    public class SIf: CSStmt
    {
        public CSExpr condition;
        public CSStmt[] then;
        public CSStmt[] @else = null;
        public SIf(CSExpr condition, params CSStmt[] then)
        {
            this.condition = condition;
            this.then = then;
        }
        public SIf(CSExpr condition, CSStmt[] then, CSStmt[] @else)
        {
            this.condition = condition;
            this.then = then;
            this.@else = @else;
        }
        public override Doc Doc()
        {
            if (@else == null)
                return VSep(
                    "if".Doc() + condition.Doc().SurroundedBy(Parens),
                    then.BlockDoc()
                );

            return VSep(
                    "if".Doc() + condition.Doc().SurroundedBy(Parens),
                    then.BlockDoc(),
                    "else".Doc(),
                    @else.BlockDoc()
                );
        }
    }

    public class SSwitch : CSStmt
    {
        public CSExpr expr;
        public Case[] cases;
        public SSwitch(CSExpr expr, Case[] cases)
        {
            this.expr = expr;
            this.cases = cases;
        }
        public override Doc Doc()
        {
            return VSep(
                "switch".Doc() * expr.Doc().SurroundedBy(Parens),
                VSep(
                    "{".Doc(),
                        cases.Select(x => x.Doc()).Join(NewLine).Indent(4),
                    "}".Doc()
                )
            );
        }
    }

    public class EId : CSExpr
    {
        public string id;
        public EId(string id)
        {
            this.id = id;
        }
        public override Doc Doc() => id.Doc();
    }

    public class EInt : CSExpr
    {
        public int value;
        public EInt(int value)
        {
            this.value = value;
        }
        public override Doc Doc()
        {
            return value.ToString().Doc();
        }
    }
    public class EVarOut : CSExpr
    {
        public string name;
        public EVarOut(string name)
        {
            this.name = name;
        }
        public override Doc Doc()
        {
            return "out var".Doc() + name.Doc();
        }
    }
    public class EStr : CSExpr
    {
        public string value;
        public EStr(string value)
        {
            this.value = value;
        }
        public override Doc Doc()
        {
            return value.Escape().Doc();
        }
    }
    public class EFloat : CSExpr
    {
        public float value;
        public EFloat(float value)
        {
            this.value = value;
        }
        public override Doc Doc()
        {
            return value.ToString().Doc() * "f".Doc();
        }
    }

    public class ENot : CSExpr
    {
        public CSExpr expr;
        public ENot(CSExpr expr)
        {
            this.expr = expr;
        }
        public override Doc Doc()
        {
            return ("!".Doc() + expr.Doc()).SurroundedBy(Parens);
        }
    }
    public class EBool : CSExpr
    {
        public bool value;
        public EBool(bool value)
        {
            this.value = value;
        }
        public override Doc Doc()
        {
            return value ? "true".Doc() : "false".Doc();
        }
    }

    public class EType : CSExpr
    {
        public CSType type;
        public EType(CSType type)
        {
            this.type = type;
        }
        public override Doc Doc()
        {
            return type.Doc();
        }
    }

    public class EOp : CSExpr
    {
        public string op;
        public CSExpr left;
        public CSExpr right;
        public EOp(string op, CSExpr left, CSExpr right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }

        public override Doc Doc()
        {
            return (left.Doc() + op.Doc() + right.Doc()).SurroundedBy(Parens);
        }
    }
    public class ECall : CSExpr
    {
        public CSExpr func;
        public CSExpr[] args;
        public (string Key, CSExpr Arg)[] keywords;
        public ECall(CSExpr func, CSExpr[] args)
        {
            this.func = func;
            this.args = args;
            this.keywords = Array.Empty<(string, CSExpr)>();
        }

        public ECall(CSExpr func, CSExpr[] args, (string, CSExpr)[] keywords)
        {
            this.func = func;
            this.args = args;
            this.keywords = keywords;
        }

        public override Doc Doc()
        {
            if (keywords.Length == 0)
                return func.Doc() * args.Select(x => x.Doc()).Join(Comma).SurroundedBy(Parens);
            if (args.Length == 0)
                return func.Doc() * keywords.Select(x => x.Key.Doc() + ":".Doc() + x.Arg.Doc()).Join(Comma).SurroundedBy(Parens);
            return func.Doc() *
                ( args.Select(x => x.Doc()).Join(Comma)
                  * Comma
                  * keywords.Select(x => x.Key.Doc() + ":".Doc() + x.Arg.Doc()).Join(Comma)
                ).SurroundedBy(Parens);
        }
    }

    public class EAssign : CSExpr
    {
        public CSExpr left;
        public CSExpr right;
        public EAssign(CSExpr left, CSExpr right)
        {
            this.left = left;
            this.right = right;
        }
        public override Doc Doc()
        {
            return left.Doc() + "=".Doc() + right.Doc();
        }
    }

    public class ECast : CSExpr
    {
        public CSExpr expr;
        public CSType type;
        public ECast(CSExpr expr, CSType type)
        {
            this.expr = expr;
            this.type = type;
        }
        public override Doc Doc()
        {
            return (type.Doc().SurroundedBy(Parens) * expr.Doc()).SurroundedBy(Parens);
        }
    }
    public class EArgcountError : CSExpr
    {
        public CSExpr argcount;
        public string methodname;
        public int min;
        public int max;
        public EArgcountError(CSExpr argcount, string methodname, int min, int max)
        {
            this.argcount = argcount;
            this.methodname = methodname;
            this.min = min;
            this.max = max;
        }
        public override Doc Doc()
        {
            if (max == min)
                return $"throw new ValueError(\"{methodname}() requires {min} positional argument(s), got \" + {argcount.Doc()})".Doc();
            return $"throw new ValueError(\"{methodname}() requires {min} to {max} positional argument(s), got \" + {argcount.Doc()})".Doc();
        }
    }
    public record Case(CSExpr caseExpr, CSStmt[] body)
    {
        public Doc Doc()
        {
            if (caseExpr == null)
            {
                return "default:".Doc() * NewLine * body.BlockDoc();
            }
            return "case".Doc() + caseExpr.Doc() * ":".Doc() * NewLine * body.BlockDoc();
        }
    }

    public class EAttr : CSExpr
    {
        public CSExpr value;
        public string attr;
        public EAttr(string attr, CSExpr value)
        {
            this.attr = attr;
            this.value = value;
        }
        public override Doc Doc()
        {
            return value.Doc() * ".".Doc() * attr.Doc();
        }
    }

    public class EItem : CSExpr
    {
        public CSExpr value;
        public CSExpr index;
        public EItem(CSExpr value, CSExpr index)
        {
            this.value = value;
            this.index = index;
        }
        public override Doc Doc()
        {
            return value.Doc() * "[".Doc() * index.Doc() * "]".Doc();
        }
    }

    public record CSMethod(
        string name,
        CSType returnType,
        (string name, CSType type)[] arguments,
        CSStmt[] body,
        bool Public = false) : CSAST
    {
        public static CSMethod PyMethod(string name, CSType returnType, CSStmt[] body, bool Public = false)
        {
            return new CSMethod(name, returnType, new (string name, CSType type)[2]
            {
                (CSExpr.ARGS,
                        new TId(nameof(BList<object>))[nameof(TrObject)]),
                (CSExpr.KWARGS,
                    new TId(nameof(Dictionary<object, object>))[nameof(TrObject), nameof(TrObject)])
            }, body, Public: Public);
        }
        public Doc Doc()
        {
            var doc_arguments = arguments.Select(x => (x.name.Doc(), x.type.Doc())).ToArray();
            return GenerateMethod(
                returnType.Doc(),
                name.Doc(),
                doc_arguments,
                body.Select(x => x.Doc()).ToArray(),
                Public: Public,
                Static: true
            );
        }
    }

    public abstract class CSType : CSAST
    {
        public abstract Doc Doc();

        public CSType this[params CSType[] args] => new TGen(this, args);

        public static implicit operator CSType(string s) => new TId(s);

        public static implicit operator CSType(Type t)
        {
            if (t.IsArray)
            {
                return new TArr(t.GetElementType());
            }
            if (t.IsGenericType)
            {
                if (t.GetGenericTypeDefinition() == t)
                    goto TypeId;
                return new TGen(t.GetGenericTypeDefinition(), t.GetGenericArguments().Select(x => (CSType)x).ToArray());
            }
            if (t == typeof(long))
                return new TC(TypeConst.Long);
            if (t == typeof(string))
                return new TC(TypeConst.String);
            if (t == typeof(float))
                return new TC(TypeConst.Float);
            if (t == typeof(bool))
                return new TC(TypeConst.Bool);
            if (t == typeof(object))
                return new TC(TypeConst.Object);
            if (t == typeof(void))
                return new TC(TypeConst.Void);
            TypeId:
            var name = t.Name;
            // replace `
            var stripIndex = name.IndexOf('`');
            if (stripIndex != -1)
                name = name.Substring(0, stripIndex);
            if (t.Namespace != null)
                name = t.Namespace + "." + name;
            return new TId(name);
        }
    }

    public enum TypeConst
    {
        Long,
        Float,
        Bool,
        String,
        Void,
        Object,
    }
    public class TC : CSType
    {
        public TypeConst c;
        public TC(TypeConst c)
        {
            this.c = c;
        }
        public override Doc Doc()
        {
            return c.ToString().ToLowerInvariant().Doc();
        }
    }
    public class TId : CSType
    {
        public string Name;
        public TId(string name)
        {
            Name = name;
        }
        public override Doc Doc()
        {
            return Name.Doc();
        }
    }

    public class TArr : CSType
    {
        public CSType elt;
        public TArr(CSType elt)
        {
            this.elt = elt;
        }
        public override Doc Doc()
        {
            return elt.Doc() * "[]".Doc();
        }
    }

    public class TGen : CSType
    {
        CSType __base;
        CSType[] TypeArguments;
        public TGen(CSType __base, params CSType[] TypeArguments)
        {
            this.__base = __base;
            this.TypeArguments = TypeArguments;
        }
        public override Doc Doc()
        {
            return __base.Doc() * TypeArguments.Select(x => x.Doc()).Join(Comma).SurroundedBy(Angle);
        }
    }
}