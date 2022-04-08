## Unity Class requires

1. component methods
    - `__add_component__`
    - `__get_component__`
    - `__get_components__`

    ```c#
    internal static TrUnityComponent __add_component__(TrClass _, TrGameObject uo)
    {
        var native_comp = uo.gameObject.AddComponent<$T>();
        if (native_comp == null)
            throw new ValueError("$T.AddComponent: $T has been added!");
        return FromRaw(uo, native_comp);
    }

    internal static bool __get_component__(TrClass _, TrGameObject uo, out TrUnityComponent component)
    {
        var native_comp = uo.gameObject.GetComponent<$T>();
        if (native_comp != null)
        {
            component = FromRaw(uo, native_comp);
            return true;
        }
        component = null;
        return false;
    }

    internal static bool __get_components__(TrClass _, TrGameObject uo, out IEnumerable<TrUnityComponent> components)
    {
        var rects = uo.gameObject.GetComponents<$T>();
        if (rects == null || rects.Length == 0)
        {
            components = null;
            return false;
        }
        components = uo.gameObject.GetComponents<$T>().Select(x => FromRaw(uo, x));
        return true;
    }
    ```
1. setup-cls:

    ```c#
    [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
    internal static void _Create()
    {
        CLASS = TrClass.FromPrototype<Tr$T>("$T");
        CLASS.UnityKind = TrClass.UnityComponentClassKind.BuiltinComponent;
        CLASS.__add_component__ = __add_component__;
        CLASS.__get_component__ = __get_component__;
        CLASS.__get_components__ = __get_components__;
    }
    [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
    internal static void _SetupClasses()
    {
        CLASS.SetupClass();
        CLASS.IsFixed = true;
    }
    ```

3. `FromRaw`

  ```c#
  public static Tr$T FromRaw(TrGameObject uo, $T component)
  {
    var allocations = UnityRTS.Get.allocations;
    if (allocations.TryGetValue(component, out var allocation))
    {
        return allocation as Tr$T;
    }
    var result = new Tr$T(uo, component);
    allocations[component] = result;
    return result;
  }
  ```

4. constructor
    ```c#
    $T native;
    public Tr$T(TrGameObject uo, $T component) : base(uo)
    {
        this.native = component;
    }
    ```

5. py
    ```c#
    public static TrClass CLASS;
    public override TrClass Class => CLASS;
    ```