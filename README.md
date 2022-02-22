## UnityPython

Key points:

1. Run a Python-like programming language on ALL platforms such as iOS, Android, etc.
2. Good IDE support via Pylance, type checked! (see `traffy-types/`)
3. Dynamic and fast code loading in Unity. Edit code and see how UI changes immediately (Unity Editor is painful to me)!
4. An extensible coroutine implementation. `async` and `await` are not as restricted as that in CPython and can be used as fast/fine-grained controlled event loops in game development.

UnityPython is based on CPython 3.10, but not fully compatible to CPython. For instance, `StopIteration` is not used for loop constructs (exceptions are heavy and not used for control flow); `type` is the only metaclass, etc.

```python
import UnityEngine

class MyComponent(UnityEngine.MonoBehaviour):
    def Start(self):
        async def coro():
            while not cond_satisfied:
                await Task.Yield
            do_something()
        def coro2():
            while not cond_satisfied:
                yield
            do_something()
        self.StartCoroutine(coro())
        self.StartCoroutine(coro2())
```

## Contributing

See [0.1 roadmap](https://github.com/thautwarm/TraffyAsm.UnityPython/issues/7)

Basic development workflow:
1. `cd UnityPython.FrontEnd && pip install -e . && cd ..`; requires Python 3.8+ (Python 3.10 is better)
2. `cd UnityPython.BackEnd`
3. `dotnet restore`
4. `unitypython.exe test.src.py --includesrc && dotnet run`
5. edit code and run `unitypython.exe test.src.py --includesrc && dotnet run` again

### How to add a method to datatypes?

For example, if we want to implement `append` for `list`,
- we firstly get to [UnityPython.BackEnd/src/Traffy.Objects/List.cs](https://github.com/thautwarm/Traffy.UnityPython/blob/main/UnityPython.BackEnd/src/Traffy.Objects/List.cs)
- then we find the method annotated with `[Mark(Initialization.TokenClassInit)]`
- see the code

  ```c#
    public static TrObject append(TrObject self, TrObject value)
    {
        ((TrList)self).container.Add(value);
        return RTS.object_none; // RTS = runtime support
    }

   [Mark(Initialization.TokenClassInit)]
    static void _Init()
    {
        CLASS = TrClass.FromPrototype<TrList>();
        CLASS.Name = "list";
        CLASS.InitInlineCacheForMagicMethods();
        CLASS[CLASS.ic__new] = TrStaticMethod.Bind("list.__new__", TrList.datanew);

        // 1. 'TrSharpFunc.FromFunc' converts a CSharp function to UnityPython 'builtin_function'
        // 2. 'CLASS["somemethod".ToIntern()] = python-object' is equal to something like
        //      class list:
        //         somemethod = expr
        CLASS["append".ToIntern()] = TrSharpFunc.FromFunc("list.append", TrList.append);
        ...
    }
  ```

  PS:
  - `TrSharpFunc.FromFunc` creates a method
  - `TrStaticMethod.Bind` creates a `staticmethod`
  - `TrClassMethod.Bind` creates a `classmethod`
  - `TrProperty.Create(getter=null, setter=null)` creates a `property`

### How to add a builtin-function

Basically, you just need to call `Initialization.Prelude(string name, TrObject o)`.

For maintainability, please collect all builtin callables (other than datatypes) at [UnityPython.BackEnd/src/Builtins.cs](https://github.com/thautwarm/Traffy.UnityPython/blob/main/UnityPython.BackEnd/src/Builtins.cs)

```c#

namespace Traffy
{
    public static class Builtins
    {
        static IEnumerator<TrObject> mkrange(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
                yield return MK.Int(i);
        }

        // BLists, or double-ended lists, are designed for fast method call (requires adding a 'self' to the left end)
        static TrObject range(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var narg = args.Count;
            switch(narg)
            {
                case 1:
                    return MK.Iter(mkrange(0, args[0].AsInt(), 1));
                case 2:
                    return MK.Iter(mkrange(args[0].AsInt(),  args[1].AsInt(), 1));
                case 3:
                    return MK.Iter(mkrange(args[0].AsInt(),  args[1].AsInt(), args[1].AsInt()));
                default:
                    throw new TypeError($"range() takes 1 to 3 positional argument(s) but {narg} were given");
            }
        }

        [Mark(Initialization.TokenBuiltinInit)]
        public static void InitRuntime()
        {
            Initialization.Prelude(TrSharpFunc.FromFunc("range", range));
        }

    }
}
```