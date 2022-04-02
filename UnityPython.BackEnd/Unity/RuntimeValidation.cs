using System;
using Traffy.Objects;
using UnityEngine;

namespace Traffy.Unity2D
{
    public static class RuntimeValidation
    {
        public static void invalidate_int_range(string msgKind, float i, float high)
        {
            invalidate_int_range(msgKind, i, 0, high);
        }
        public static void invalidate_int_range(string msgKind, float i, float low, float high)
        {
            if (!(i <= high && i >= low))
            {
                throw new ValueError($"{msgKind} should be within {low}-{high}, got {i}");
            }
        }

        public static void invalidate_float_range(string msgKind, float i, float high)
        {
            invalidate_float_range(msgKind, i, 0.0f, high);
        }

        public static void invalidate_float_range(string msgKind, float i, float low, float high)
        {
            if (!(i <= high && i >= low))
            {
                throw new ValueError($"{msgKind} should be within {low}-{high}, got {i}");
            }
        }

        public static Exception invalid_access(GameObject o, string attr)
        {
            return new TypeError($"cannot access {attr} from {o.name}");
        }
    }
}