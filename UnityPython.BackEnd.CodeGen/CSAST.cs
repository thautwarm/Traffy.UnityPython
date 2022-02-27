using System.Collections.Generic;
using System.Linq;
using PrettyDoc;
using Traffy;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;
using System.Reflection;
using System;

namespace CSAST
{
    public interface CSAST
    {
        public Doc Doc();
    }

    public abstract class CSExpr : CSAST
    {
        public const string ARGS = "__args";
        public const string KWARGS = "__kwargs";

        public abstract Doc Doc();
        public CSExpr this[string s] => new EAttr(s, this);

        public CSExpr this[CSExpr s] => new EItem(this, s);
        public CSExpr Call(params CSExpr[] arguments) => new ECall(this, arguments);
        public CSExpr Switch(params Case[] cases) => new ESwitch(this, cases);

        public CSExpr Cast(Type t) => new ECast(this, t);

        public static implicit operator CSExpr(int i) => new EInt(i);
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
            return value.ToString().Doc();
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
    public class ECall : CSExpr
    {
        public CSExpr func;
        public CSExpr[] args;
        public ECall(CSExpr func, CSExpr[] args)
        {
            this.func = func;
            this.args = args;
        }
        public override Doc Doc()
        {
            return func.Doc() * args.Select(x => x.Doc()).Join(Comma).SurroundedBy(Parens);
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
        public int min;
        public int max = -1;
        public EArgcountError(CSExpr argcount, int min, int max = -1)
        {
            this.argcount = argcount;
            this.min = min;
            this.max = max;
        }
        public override Doc Doc()
        {
            if (max == min)
                return $"throw new ValueError(\"requires {min} argument(s), got \" + {argcount.Doc()})".Doc();
            return $"throw new ValueError(\"requires {min} to {max} argument(s), got \" + {argcount.Doc()})".Doc();
        }
    }
    public record Case(CSExpr caseExpr, CSExpr body)
    {
        public Doc Doc()
        {
            if (caseExpr == null)
            {
                return "_".Doc() * "=>".Doc() * body.Doc();
            }
            return caseExpr.Doc() + "=>".Doc() + body.Doc();
        }
    }

    public class ESwitch : CSExpr
    {
        public CSExpr expr;
        public Case[] cases;
        public ESwitch(CSExpr expr, Case[] cases)
        {
            this.expr = expr;
            this.cases = cases;
        }
        public override Doc Doc()
        {
            return VSep(
                expr.Doc() + "switch".Doc(),
                VSep(
                    "{".Doc(),
                        cases.Select(x => x.Doc()).Join(Comma * NewLine).Indent(4),
                    "}".Doc()
                )
            );
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
        CSExpr body) : CSAST
    {
        public static CSMethod PyMethod(string name, CSType returnType, CSExpr body)
        {
            return new CSMethod(name, returnType, new (string name, CSType type)[2]
            {
                (CSExpr.ARGS,
                        new TId(nameof(BList<object>))[nameof(TrObject)]),
                (CSExpr.KWARGS,
                    new TId(nameof(Dictionary<object, object>))[nameof(TrObject), nameof(TrObject)])
            }, body);
        }
        public Doc Doc()
        {
            var doc_arguments = arguments.Select(x => (x.name.Doc(), x.type.Doc())).ToArray();
            var retIndicator =
                (returnType is TC tc && tc.c == TypeConst.Void) ?
                "".Doc() : "return".Doc();
            return GenerateMethod(
                returnType.Doc(),
                name.Doc(),
                doc_arguments,
                new[] { retIndicator + body.Doc() + ";".Doc() },
                Public: false
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
            var name = t.Name;
            // replace `
            name = name.Replace("`", "");
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