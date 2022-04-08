using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrText
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind___new__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrClass>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrGameObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Unity2D.TrText.__new__(_0,_1));
                    }
                    default:
                        throw new ValueError("__new__() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            static  Traffy.Objects.TrObject __bind_ForceMeshUpdate(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        bool _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("ignoreActiveState"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<bool>.Unique,__keyword__1);
                        else
                            _1 = false;
                        bool _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("forceTextReparsing"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<bool>.Unique,__keyword__2);
                        else
                            _2 = false;
                        _0.ForceMeshUpdate(ignoreActiveState : _1,forceTextReparsing : _2);
                        return Traffy.MK.None();
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<bool>.Unique,__args[1]);
                        bool _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("forceTextReparsing"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<bool>.Unique,__keyword__2);
                        else
                            _2 = false;
                        _0.ForceMeshUpdate(_1,forceTextReparsing : _2);
                        return Traffy.MK.None();
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<bool>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<bool>.Unique,__args[2]);
                        _0.ForceMeshUpdate(_1,_2);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("ForceMeshUpdate() requires 1 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["ForceMeshUpdate"] = TrSharpFunc.FromFunc("ForceMeshUpdate", __bind_ForceMeshUpdate);
            static  Traffy.Objects.TrObject __bind_playText(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("text"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            throw new ValueError( "Missing keyword-only argument text" );
                        float _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("speed"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<float>.Unique,__keyword__2);
                        else
                            _2 = 5f;
                        return Box.Apply(_0.playText(text : _1,speed : _2));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        float _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("speed"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<float>.Unique,__keyword__2);
                        else
                            _2 = 5f;
                        return Box.Apply(_0.playText(_1,speed : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<float>.Unique,__args[2]);
                        return Box.Apply(_0.playText(_1,_2));
                    }
                    default:
                        throw new ValueError("playText() requires 1 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["playText"] = TrSharpFunc.FromFunc("playText", __bind_playText);
            static  Traffy.Objects.TrObject __bind_on(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrEventTriggerType>.Unique,__args[1]);
                        return Box.Apply(_0.on(_1));
                    }
                    default:
                        throw new ValueError("on() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["on"] = TrSharpFunc.FromFunc("on", __bind_on);
            static  Traffy.Objects.TrObject __bind_AddComponent(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        Traffy.Objects.TrObject _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("parameter"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__3);
                        else
                            _3 = null;
                        return Box.Apply(_0._AddComponent(_1,_2,parameter : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[3]);
                        return Box.Apply(_0._AddComponent(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("AddComponent() requires 3 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["AddComponent"] = TrSharpFunc.FromFunc("AddComponent", __bind_AddComponent);
            static  Traffy.Objects.TrObject __bind_TryGetComponent(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrRef>.Unique,__args[2]);
                        return Box.Apply(_0._TryGetComponent(_1,_2));
                    }
                    default:
                        throw new ValueError("TryGetComponent() requires 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["TryGetComponent"] = TrSharpFunc.FromFunc("TryGetComponent", __bind_TryGetComponent);
            static  Traffy.Objects.TrObject __bind_TryGetComponents(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrRef>.Unique,__args[2]);
                        return Box.Apply(_0._TryGetComponents(_1,_2));
                    }
                    default:
                        throw new ValueError("TryGetComponents() requires 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["TryGetComponents"] = TrSharpFunc.FromFunc("TryGetComponents", __bind_TryGetComponents);
            static  Traffy.Objects.TrObject __bind_GetComponent(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(_0._GetComponent(_1));
                    }
                    default:
                        throw new ValueError("GetComponent() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["GetComponent"] = TrSharpFunc.FromFunc("GetComponent", __bind_GetComponent);
            static  Traffy.Objects.TrObject __bind_Destroy(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrText>.Unique,__args[0]);
                        _0.Destroy();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("Destroy() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["Destroy"] = TrSharpFunc.FromFunc("Destroy", __bind_Destroy);
            static  Traffy.Objects.TrObject __read_size(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).size);
            }
            static  void __write_size(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).size = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["size"] = TrProperty.Create(CLASS.Name + ".size", __read_size, __write_size);
            static  Traffy.Objects.TrObject __read_AlignMiddle(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).AlignMiddle);
            }
            Action<TrObject, TrObject> __write_AlignMiddle = null;
            CLASS["AlignMiddle"] = TrProperty.Create(CLASS.Name + ".AlignMiddle", __read_AlignMiddle, __write_AlignMiddle);
            static  Traffy.Objects.TrObject __read_AlignCenter(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).AlignCenter);
            }
            Action<TrObject, TrObject> __write_AlignCenter = null;
            CLASS["AlignCenter"] = TrProperty.Create(CLASS.Name + ".AlignCenter", __read_AlignCenter, __write_AlignCenter);
            static  Traffy.Objects.TrObject __read_AlignLeft(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).AlignLeft);
            }
            Action<TrObject, TrObject> __write_AlignLeft = null;
            CLASS["AlignLeft"] = TrProperty.Create(CLASS.Name + ".AlignLeft", __read_AlignLeft, __write_AlignLeft);
            static  Traffy.Objects.TrObject __read_AlignRight(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).AlignRight);
            }
            Action<TrObject, TrObject> __write_AlignRight = null;
            CLASS["AlignRight"] = TrProperty.Create(CLASS.Name + ".AlignRight", __read_AlignRight, __write_AlignRight);
            static  Traffy.Objects.TrObject __read_AlignTop(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).AlignTop);
            }
            Action<TrObject, TrObject> __write_AlignTop = null;
            CLASS["AlignTop"] = TrProperty.Create(CLASS.Name + ".AlignTop", __read_AlignTop, __write_AlignTop);
            static  Traffy.Objects.TrObject __read_AlignBottom(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).AlignBottom);
            }
            Action<TrObject, TrObject> __write_AlignBottom = null;
            CLASS["AlignBottom"] = TrProperty.Create(CLASS.Name + ".AlignBottom", __read_AlignBottom, __write_AlignBottom);
            static  Traffy.Objects.TrObject __read_alignment(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).alignment);
            }
            static  void __write_alignment(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).alignment = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["alignment"] = TrProperty.Create(CLASS.Name + ".alignment", __read_alignment, __write_alignment);
            static  Traffy.Objects.TrObject __read_autoSize(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).autoSize);
            }
            static  void __write_autoSize(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).autoSize = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["autoSize"] = TrProperty.Create(CLASS.Name + ".autoSize", __read_autoSize, __write_autoSize);
            static  Traffy.Objects.TrObject __read_color(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).color);
            }
            static  void __write_color(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).color = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["color"] = TrProperty.Create(CLASS.Name + ".color", __read_color, __write_color);
            static  Traffy.Objects.TrObject __read_overflow(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).overflow);
            }
            static  void __write_overflow(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).overflow = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["overflow"] = TrProperty.Create(CLASS.Name + ".overflow", __read_overflow, __write_overflow);
            static  Traffy.Objects.TrObject __read_pageCount(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).pageCount);
            }
            Action<TrObject, TrObject> __write_pageCount = null;
            CLASS["pageCount"] = TrProperty.Create(CLASS.Name + ".pageCount", __read_pageCount, __write_pageCount);
            static  Traffy.Objects.TrObject __read_characterCount(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).characterCount);
            }
            Action<TrObject, TrObject> __write_characterCount = null;
            CLASS["characterCount"] = TrProperty.Create(CLASS.Name + ".characterCount", __read_characterCount, __write_characterCount);
            static  Traffy.Objects.TrObject __read_currentPage(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).currentPage);
            }
            static  void __write_currentPage(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).currentPage = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["currentPage"] = TrProperty.Create(CLASS.Name + ".currentPage", __read_currentPage, __write_currentPage);
            static  Traffy.Objects.TrObject __read_maxVisibleCharacters(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).maxVisibleCharacters);
            }
            static  void __write_maxVisibleCharacters(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).maxVisibleCharacters = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["maxVisibleCharacters"] = TrProperty.Create(CLASS.Name + ".maxVisibleCharacters", __read_maxVisibleCharacters, __write_maxVisibleCharacters);
            static  Traffy.Objects.TrObject __read_contents(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).contents);
            }
            static  void __write_contents(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).contents = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["contents"] = TrProperty.Create(CLASS.Name + ".contents", __read_contents, __write_contents);
            static  Traffy.Objects.TrObject __read_width(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).width);
            }
            static  void __write_width(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).width = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["width"] = TrProperty.Create(CLASS.Name + ".width", __read_width, __write_width);
            static  Traffy.Objects.TrObject __read_height(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).height);
            }
            static  void __write_height(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).height = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["height"] = TrProperty.Create(CLASS.Name + ".height", __read_height, __write_height);
            static  Traffy.Objects.TrObject __read_base(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg)._baseObject);
            }
            Action<TrObject, TrObject> __write_base = null;
            CLASS["base"] = TrProperty.Create(CLASS.Name + ".base", __read_base, __write_base);
            static  Traffy.Objects.TrObject __read_x(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).x);
            }
            static  void __write_x(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).x = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["x"] = TrProperty.Create(CLASS.Name + ".x", __read_x, __write_x);
            static  Traffy.Objects.TrObject __read_y(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).y);
            }
            static  void __write_y(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).y = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["y"] = TrProperty.Create(CLASS.Name + ".y", __read_y, __write_y);
            static  Traffy.Objects.TrObject __read_z(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrText)_arg).z);
            }
            static  void __write_z(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrText)_arg).z = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["z"] = TrProperty.Create(CLASS.Name + ".z", __read_z, __write_z);
        }
    }
}
#endif

