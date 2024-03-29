using System;
using System.Collections.Generic;
using Traffy.Objects;
namespace Traffy
{
    public static partial class Initialization
    {
        public static void InitRuntime()
        {
            Traffy.Objects.TrClass.BeforeReInitRuntime();
            Traffy.Objects.TrRawObject._Create();
            Traffy.Modules.TrModule_enum._Create();
            Traffy.Modules.TrModule_json._Create();
            Traffy.Modules.TrModule_types._Create();
            Traffy.Modules.TrModule_typing._Create();
            Traffy.Modules.TrModule_future._Create();
            Traffy.Interfaces.Awaitable._Create();
            Traffy.Interfaces.Callable._Create();
            Traffy.Interfaces.Container._Create();
            Traffy.Interfaces.Comparable._Create();
            Traffy.Interfaces.Iterable._Create();
            Traffy.Interfaces.ContextManager._Create();
            Traffy.Interfaces.Hashable._Create();
            Traffy.Interfaces.Sized._Create();
            Traffy.Interfaces.Iterator._Create();
            Traffy.Interfaces.Collection._Create();
            Traffy.Interfaces.Reversible._Create();
            Traffy.Interfaces.Sequence._Create();
            Traffy.Interfaces.Mapping._Create();
            Traffy.Objects.TrABC._Create();
            Traffy.Objects.TrBool._Create();
            Traffy.Objects.TrSharpFunc._Create();
            Traffy.Objects.TrByteArray._Create();
            Traffy.Objects.TrBytes._Create();
            Traffy.Objects.TrClass._Create();
            Traffy.Objects.TrClassMethod._Create();
            Traffy.Objects.TrDict._Create();
            Traffy.Objects.TrBaseException._Create();
            Traffy.Objects.TrException._Create();
            Traffy.Objects.AttributeError._Create();
            Traffy.Objects.NameError._Create();
            Traffy.Objects.TypeError._Create();
            Traffy.Objects.ValueError._Create();
            Traffy.Objects.StopIteration._Create();
            Traffy.Objects.LookupError._Create();
            Traffy.Objects.KeyError._Create();
            Traffy.Objects.IndexError._Create();
            Traffy.Objects.AssertionError._Create();
            Traffy.Objects.ImportError._Create();
            Traffy.Objects.RuntimeError._Create();
            Traffy.Objects.NotImplementedError._Create();
            Traffy.Objects.NativeError._Create();
            Traffy.Objects.TrFloat._Create();
            Traffy.Objects.TrGenerator._Create();
            Traffy.Objects.TrInt._Create();
            Traffy.Objects.TrIter._Create();
            Traffy.Objects.TrList._Create();
            Traffy.Objects.TrSharpMethod._Create();
            Traffy.Objects.TrAnnotatedType._Create();
            Traffy.Objects.TrEnum._Create();
            Traffy.Objects.TrTypedDict._Create();
            Traffy.Objects.TrModule._Create();
            Traffy.Objects.TrNone._Create();
            Traffy.Objects.TrNotImplemented._Create();
            Traffy.Modules.TrModule_abc._Create();
            Traffy.Objects.TrCapsuleObject._Create();
            Traffy.Objects.TrProperty._Create();
            Traffy.Objects.TrRef._Create();
            Traffy.Objects.TrSet._Create();
            Traffy.Objects.TrSlice._Create();
            Traffy.Objects.TrStaticMethod._Create();
            Traffy.Objects.TrStr._Create();
            Traffy.Objects.TrTraceback._Create();
            Traffy.Objects.TrTuple._Create();
            Traffy.Objects.TrUnionType._Create();
            Traffy.Objects.TrFunc._Create();
            Traffy.Initialization._Create();
            Traffy.ModuleSystem._Create();
            Traffy.Objects.TrRawObject.CLASS.__base = new TrClass[] {  };
            Traffy.Modules.TrModule_enum.CLASS.__base = new TrClass[] {  };
            Traffy.Modules.TrModule_json.CLASS.__base = new TrClass[] {  };
            Traffy.Modules.TrModule_types.CLASS.__base = new TrClass[] {  };
            Traffy.Modules.TrModule_typing.CLASS.__base = new TrClass[] {  };
            Traffy.Modules.TrModule_future.CLASS.__base = new TrClass[] {  };
            Traffy.Interfaces.Awaitable.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.Callable.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.Container.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.Comparable.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.Iterable.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.ContextManager.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.Hashable.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.Sized.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS };
            Traffy.Interfaces.Iterator.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Iterable.CLASS };
            Traffy.Interfaces.Collection.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Sized.CLASS,Traffy.Interfaces.Iterable.CLASS,Traffy.Interfaces.Container.CLASS };
            Traffy.Interfaces.Reversible.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Iterable.CLASS };
            Traffy.Interfaces.Sequence.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Reversible.CLASS,Traffy.Interfaces.Collection.CLASS };
            Traffy.Interfaces.Mapping.CLASS.__base = new TrClass[] { Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Collection.CLASS };
            Traffy.Objects.TrABC.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrBool.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS };
            Traffy.Objects.TrSharpFunc.CLASS.__base = new TrClass[] { Traffy.Interfaces.Callable.CLASS };
            Traffy.Objects.TrByteArray.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS,Traffy.Interfaces.Sequence.CLASS };
            Traffy.Objects.TrBytes.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS,Traffy.Interfaces.Sequence.CLASS };
            Traffy.Objects.TrClass.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrClassMethod.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrDict.CLASS.__base = new TrClass[] { Traffy.Interfaces.Mapping.CLASS };
            Traffy.Objects.TrBaseException.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrException.CLASS.__base = new TrClass[] { Traffy.Objects.TrBaseException.CLASS };
            Traffy.Objects.AttributeError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.NameError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.TypeError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.ValueError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.StopIteration.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.LookupError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.KeyError.CLASS.__base = new TrClass[] { Traffy.Objects.LookupError.CLASS };
            Traffy.Objects.IndexError.CLASS.__base = new TrClass[] { Traffy.Objects.LookupError.CLASS };
            Traffy.Objects.AssertionError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.ImportError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.RuntimeError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.NotImplementedError.CLASS.__base = new TrClass[] { Traffy.Objects.RuntimeError.CLASS };
            Traffy.Objects.NativeError.CLASS.__base = new TrClass[] { Traffy.Objects.TrException.CLASS };
            Traffy.Objects.TrFloat.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS };
            Traffy.Objects.TrGenerator.CLASS.__base = new TrClass[] { Traffy.Interfaces.Iterator.CLASS,Traffy.Interfaces.Awaitable.CLASS };
            Traffy.Objects.TrInt.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS };
            Traffy.Objects.TrIter.CLASS.__base = new TrClass[] { Traffy.Interfaces.Iterator.CLASS };
            Traffy.Objects.TrList.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS,Traffy.Interfaces.Sequence.CLASS };
            Traffy.Objects.TrSharpMethod.CLASS.__base = new TrClass[] { Traffy.Interfaces.Callable.CLASS };
            Traffy.Objects.TrAnnotatedType.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrEnum.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrTypedDict.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrModule.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrNone.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrNotImplemented.CLASS.__base = new TrClass[] {  };
            Traffy.Modules.TrModule_abc.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrCapsuleObject.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrProperty.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrRef.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrSet.CLASS.__base = new TrClass[] { Traffy.Interfaces.Collection.CLASS };
            Traffy.Objects.TrSlice.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrStaticMethod.CLASS.__base = new TrClass[] { Traffy.Interfaces.Callable.CLASS };
            Traffy.Objects.TrStr.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS,Traffy.Interfaces.Sequence.CLASS };
            Traffy.Objects.TrTraceback.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrTuple.CLASS.__base = new TrClass[] { Traffy.Interfaces.Comparable.CLASS,Traffy.Interfaces.Sequence.CLASS };
            Traffy.Objects.TrUnionType.CLASS.__base = new TrClass[] {  };
            Traffy.Objects.TrFunc.CLASS.__base = new TrClass[] { Traffy.Interfaces.Callable.CLASS };
            Traffy.Objects.TrRawObject.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrRawObject.CLASS);
            Traffy.Modules.TrModule_enum.CLASS.__mro = TrClass.C3Linearized(Traffy.Modules.TrModule_enum.CLASS);
            Traffy.Modules.TrModule_json.CLASS.__mro = TrClass.C3Linearized(Traffy.Modules.TrModule_json.CLASS);
            Traffy.Modules.TrModule_types.CLASS.__mro = TrClass.C3Linearized(Traffy.Modules.TrModule_types.CLASS);
            Traffy.Modules.TrModule_typing.CLASS.__mro = TrClass.C3Linearized(Traffy.Modules.TrModule_typing.CLASS);
            Traffy.Modules.TrModule_future.CLASS.__mro = TrClass.C3Linearized(Traffy.Modules.TrModule_future.CLASS);
            Traffy.Interfaces.Awaitable.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Awaitable.CLASS);
            Traffy.Interfaces.Callable.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Callable.CLASS);
            Traffy.Interfaces.Container.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Container.CLASS);
            Traffy.Interfaces.Comparable.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Comparable.CLASS);
            Traffy.Interfaces.Iterable.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Iterable.CLASS);
            Traffy.Interfaces.ContextManager.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.ContextManager.CLASS);
            Traffy.Interfaces.Hashable.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Hashable.CLASS);
            Traffy.Interfaces.Sized.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Sized.CLASS);
            Traffy.Interfaces.Iterator.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Iterator.CLASS);
            Traffy.Interfaces.Collection.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Collection.CLASS);
            Traffy.Interfaces.Reversible.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Reversible.CLASS);
            Traffy.Interfaces.Sequence.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Sequence.CLASS);
            Traffy.Interfaces.Mapping.CLASS.__mro = TrClass.C3Linearized(Traffy.Interfaces.Mapping.CLASS);
            Traffy.Objects.TrABC.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrABC.CLASS);
            Traffy.Objects.TrBool.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrBool.CLASS);
            Traffy.Objects.TrSharpFunc.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrSharpFunc.CLASS);
            Traffy.Objects.TrByteArray.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrByteArray.CLASS);
            Traffy.Objects.TrBytes.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrBytes.CLASS);
            Traffy.Objects.TrClass.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrClass.CLASS);
            Traffy.Objects.TrClassMethod.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrClassMethod.CLASS);
            Traffy.Objects.TrDict.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrDict.CLASS);
            Traffy.Objects.TrBaseException.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrBaseException.CLASS);
            Traffy.Objects.TrException.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrException.CLASS);
            Traffy.Objects.AttributeError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.AttributeError.CLASS);
            Traffy.Objects.NameError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.NameError.CLASS);
            Traffy.Objects.TypeError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TypeError.CLASS);
            Traffy.Objects.ValueError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.ValueError.CLASS);
            Traffy.Objects.StopIteration.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.StopIteration.CLASS);
            Traffy.Objects.LookupError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.LookupError.CLASS);
            Traffy.Objects.KeyError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.KeyError.CLASS);
            Traffy.Objects.IndexError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.IndexError.CLASS);
            Traffy.Objects.AssertionError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.AssertionError.CLASS);
            Traffy.Objects.ImportError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.ImportError.CLASS);
            Traffy.Objects.RuntimeError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.RuntimeError.CLASS);
            Traffy.Objects.NotImplementedError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.NotImplementedError.CLASS);
            Traffy.Objects.NativeError.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.NativeError.CLASS);
            Traffy.Objects.TrFloat.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrFloat.CLASS);
            Traffy.Objects.TrGenerator.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrGenerator.CLASS);
            Traffy.Objects.TrInt.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrInt.CLASS);
            Traffy.Objects.TrIter.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrIter.CLASS);
            Traffy.Objects.TrList.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrList.CLASS);
            Traffy.Objects.TrSharpMethod.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrSharpMethod.CLASS);
            Traffy.Objects.TrAnnotatedType.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrAnnotatedType.CLASS);
            Traffy.Objects.TrEnum.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrEnum.CLASS);
            Traffy.Objects.TrTypedDict.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrTypedDict.CLASS);
            Traffy.Objects.TrModule.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrModule.CLASS);
            Traffy.Objects.TrNone.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrNone.CLASS);
            Traffy.Objects.TrNotImplemented.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrNotImplemented.CLASS);
            Traffy.Modules.TrModule_abc.CLASS.__mro = TrClass.C3Linearized(Traffy.Modules.TrModule_abc.CLASS);
            Traffy.Objects.TrCapsuleObject.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrCapsuleObject.CLASS);
            Traffy.Objects.TrProperty.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrProperty.CLASS);
            Traffy.Objects.TrRef.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrRef.CLASS);
            Traffy.Objects.TrSet.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrSet.CLASS);
            Traffy.Objects.TrSlice.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrSlice.CLASS);
            Traffy.Objects.TrStaticMethod.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrStaticMethod.CLASS);
            Traffy.Objects.TrStr.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrStr.CLASS);
            Traffy.Objects.TrTraceback.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrTraceback.CLASS);
            Traffy.Objects.TrTuple.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrTuple.CLASS);
            Traffy.Objects.TrUnionType.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrUnionType.CLASS);
            Traffy.Objects.TrFunc.CLASS.__mro = TrClass.C3Linearized(Traffy.Objects.TrFunc.CLASS);
            Traffy.Interfaces.Awaitable._Init();
            Traffy.Interfaces.Callable._Init();
            Traffy.Interfaces.Collection._Init();
            Traffy.Interfaces.Comparable._Init();
            Traffy.Interfaces.Container._Init();
            Traffy.Interfaces.ContextManager._Init();
            Traffy.Interfaces.Hashable._Init();
            Traffy.Interfaces.Iterable._Init();
            Traffy.Interfaces.Iterator._Init();
            Traffy.Interfaces.Mapping._Init();
            Traffy.Interfaces.Reversible._Init();
            Traffy.Interfaces.Sequence._Init();
            Traffy.Interfaces.Sized._Init();
            Traffy.Interfaces.AbstractClass.generated_BindMethods();
            Traffy.Builtins.InitBuiltins();
            Traffy.Modules.TrModule_abc.generated_BindMethods();
            Traffy.Modules.TrModule_enum.generated_BindMethods();
            Traffy.Modules.TrModule_json.generated_BindMethods();
            Traffy.Modules.TrModule_types.generated_BindMethods();
            Traffy.Modules.TrModule_typing.generated_BindMethods();
            Traffy.Modules.TrModule_future.generated_BindMethods();
            Traffy.Objects.TrABC.generated_BindMethods();
            Traffy.Objects.TrSharpFunc.generated_BindMethods();
            Traffy.Objects.TrByteArray.generated_BindMethods();
            Traffy.Objects.TrBytes.generated_BindMethods();
            Traffy.Objects.TrClassMethod.generated_BindMethods();
            Traffy.Objects.TrDict.generated_BindMethods();
            Traffy.Objects.TrFloat.generated_BindMethods();
            Traffy.Objects.TrGenerator.generated_BindMethods();
            Traffy.Objects.TrInt.generated_BindMethods();
            Traffy.Objects.TrIter.generated_BindMethods();
            Traffy.Objects.TrList.generated_BindMethods();
            Traffy.Objects.TrSharpMethod.generated_BindMethods();
            Traffy.Objects.TrEnum.generated_BindMethods();
            Traffy.Objects.TrTypedDict.generated_BindMethods();
            Traffy.Objects.TrModule.generated_BindMethods();
            Traffy.Objects.TrNotImplemented.generated_BindMethods();
            Traffy.Objects.TrProperty.generated_BindMethods();
            Traffy.Objects.TrSet.generated_BindMethods();
            Traffy.Objects.TrSlice.generated_BindMethods();
            Traffy.Objects.TrStaticMethod.generated_BindMethods();
            Traffy.Objects.TrStr.generated_BindMethods();
            Traffy.Objects.TrTuple.generated_BindMethods();
            Traffy.Objects.TrFunc.generated_BindMethods();
            Traffy.Modules.TrModule_abc._Init();
            Traffy.Modules.TrModule_enum._Init();
            Traffy.Modules.TrModule_json._Init();
            Traffy.Modules.TrModule_types._Init();
            Traffy.Modules.TrModule_typing._Init();
            Traffy.Modules.TrModule_future._Init();
            Traffy.Objects.TrABC._Init();
            Traffy.Objects.TrBool._Init();
            Traffy.Objects.TrSharpFunc._Init();
            Traffy.Objects.TrByteArray._Init();
            Traffy.Objects.TrBytes._Init();
            Traffy.Objects.TrClass._Init();
            Traffy.Objects.TrClassMethod._Init();
            Traffy.Objects.TrDict._Init();
            Traffy.Objects.TrBaseException._Init();
            Traffy.Objects.TrException._Init();
            Traffy.Objects.AttributeError._Init();
            Traffy.Objects.NameError._Init();
            Traffy.Objects.TypeError._Init();
            Traffy.Objects.ValueError._Init();
            Traffy.Objects.StopIteration._Init();
            Traffy.Objects.LookupError._Init();
            Traffy.Objects.KeyError._Init();
            Traffy.Objects.IndexError._Init();
            Traffy.Objects.AssertionError._Init();
            Traffy.Objects.ImportError._Init();
            Traffy.Objects.RuntimeError._Init();
            Traffy.Objects.NotImplementedError._Init();
            Traffy.Objects.NativeError._Init();
            Traffy.Objects.TrFloat._Init();
            Traffy.Objects.TrGenerator._Init();
            Traffy.Objects.TrInt._Init();
            Traffy.Objects.TrIter._Init();
            Traffy.Objects.TrList._Init();
            Traffy.Objects.TrSharpMethod._Init();
            Traffy.Objects.TrAnnotatedType._Init();
            Traffy.Objects.TrEnum._Init();
            Traffy.Objects.TrTypedDict._Init();
            Traffy.Objects.TrNone._Init();
            Traffy.Objects.TrRawObject._Init();
            Traffy.Objects.TrCapsuleObject._Init();
            Traffy.Objects.TrProperty._Init();
            Traffy.Objects.TrRef._Init();
            Traffy.Objects.TrSet._Init();
            Traffy.Objects.TrSlice._Init();
            Traffy.Objects.TrStaticMethod._Init();
            Traffy.Objects.TrStr._Init();
            Traffy.Objects.TrTraceback._Init();
            Traffy.Objects.TrTuple._Init();
            Traffy.Objects.TrUnionType._Init();
            Traffy.Objects.TrFunc._Init();
            Traffy.Objects.TrRawObject._SetupClasses();
            Traffy.Modules.TrModule_enum._SetupClasses();
            Traffy.Modules.TrModule_json._SetupClasses();
            Traffy.Modules.TrModule_types._SetupClasses();
            Traffy.Modules.TrModule_typing._SetupClasses();
            Traffy.Modules.TrModule_future._SetupClasses();
            Traffy.Interfaces.Awaitable._SetupClasses();
            Traffy.Interfaces.Callable._SetupClasses();
            Traffy.Interfaces.Container._SetupClasses();
            Traffy.Interfaces.Comparable._SetupClasses();
            Traffy.Interfaces.Iterable._SetupClasses();
            Traffy.Interfaces.ContextManager._SetupClasses();
            Traffy.Interfaces.Hashable._SetupClasses();
            Traffy.Interfaces.Sized._SetupClasses();
            Traffy.Interfaces.Iterator._SetupClasses();
            Traffy.Interfaces.Collection._SetupClasses();
            Traffy.Interfaces.Reversible._SetupClasses();
            Traffy.Interfaces.Sequence._SetupClasses();
            Traffy.Interfaces.Mapping._SetupClasses();
            Traffy.Objects.TrABC._SetupClasses();
            Traffy.Objects.TrBool._SetupClasses();
            Traffy.Objects.TrSharpFunc._SetupClasses();
            Traffy.Objects.TrByteArray._SetupClasses();
            Traffy.Objects.TrBytes._SetupClasses();
            Traffy.Objects.TrClass._SetupClasses();
            Traffy.Objects.TrClassMethod._SetupClasses();
            Traffy.Objects.TrDict._SetupClasses();
            Traffy.Objects.TrBaseException._SetupClasses();
            Traffy.Objects.TrException._SetupClasses();
            Traffy.Objects.AttributeError._SetupClasses();
            Traffy.Objects.NameError._SetupClasses();
            Traffy.Objects.TypeError._SetupClasses();
            Traffy.Objects.ValueError._SetupClasses();
            Traffy.Objects.StopIteration._SetupClasses();
            Traffy.Objects.LookupError._SetupClasses();
            Traffy.Objects.KeyError._SetupClasses();
            Traffy.Objects.IndexError._SetupClasses();
            Traffy.Objects.AssertionError._SetupClasses();
            Traffy.Objects.ImportError._SetupClasses();
            Traffy.Objects.RuntimeError._SetupClasses();
            Traffy.Objects.NotImplementedError._SetupClasses();
            Traffy.Objects.NativeError._SetupClasses();
            Traffy.Objects.TrFloat._SetupClasses();
            Traffy.Objects.TrGenerator._SetupClasses();
            Traffy.Objects.TrInt._SetupClasses();
            Traffy.Objects.TrIter._SetupClasses();
            Traffy.Objects.TrList._SetupClasses();
            Traffy.Objects.TrSharpMethod._SetupClasses();
            Traffy.Objects.TrAnnotatedType._SetupClasses();
            Traffy.Objects.TrEnum._SetupClasses();
            Traffy.Objects.TrTypedDict._SetupClasses();
            Traffy.Objects.TrModule._SetupClasses();
            Traffy.Objects.TrNone._SetupClasses();
            Traffy.Objects.TrNotImplemented._SetupClasses();
            Traffy.Modules.TrModule_abc._SetupClasses();
            Traffy.Objects.TrCapsuleObject._SetupClasses();
            Traffy.Objects.TrProperty._SetupClasses();
            Traffy.Objects.TrRef._SetupClasses();
            Traffy.Objects.TrSet._SetupClasses();
            Traffy.Objects.TrSlice._SetupClasses();
            Traffy.Objects.TrStaticMethod._SetupClasses();
            Traffy.Objects.TrStr._SetupClasses();
            Traffy.Objects.TrTraceback._SetupClasses();
            Traffy.Objects.TrTuple._SetupClasses();
            Traffy.Objects.TrUnionType._SetupClasses();
            Traffy.Objects.TrFunc._SetupClasses();
        }

    
    }
}

