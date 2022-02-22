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
- we firstly get to `UnityPython.BackEnd/src/Traffy.Objects/List.cs`
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
