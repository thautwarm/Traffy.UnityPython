using System;
using System.Runtime.Serialization;
namespace Traffy
{
    public static class InternedStringExt
    {
        public static InternedString ToIntern(this string s)
        {
#if DEBUG
            if (s == null)
                throw new ArgumentNullException(nameof(s));
#endif
            return InternedString.FromString(s);
        }
    }
    [Serializable]
    public struct InternedString: IEquatable<InternedString>
    {
        public string Value { get; private set; }

        [OnDeserialized]
        internal InternedString OnDeserialized(StreamingContext context)
        {
            if (Value == null)
                throw new SerializationException("Value cannot be null");
            return InternedString.FromString(Value);
        }
        private InternedString(string internedStr)
        {
            Value = internedStr;
        }

        public static bool operator ==(InternedString a, InternedString b)
        {
            return object.ReferenceEquals(a.Value, b.Value);
        }
        public static bool operator !=(InternedString a, InternedString b)
        {
            return !object.ReferenceEquals(a.Value, b.Value);
        }

        public static InternedString FromString(string str)
        {
            str = String.Intern(str);
#if DEBUG
            if (str == null)
                throw new ArgumentNullException(nameof(str));
#endif
            var res =  new InternedString(str);
#if DEBUG
            if (res.Value == null)
                throw new ArgumentNullException(nameof(res));
#endif
            return res;
        }

        public bool Equals(InternedString other)
        {
            return object.ReferenceEquals(Value, other.Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return obj is InternedString other && object.ReferenceEquals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        internal static InternedString Unsafe(string value)
        {
            return new InternedString(value);
        }

        internal string UnsafeUnbox()
        {
            return Value;
        }

    }
}