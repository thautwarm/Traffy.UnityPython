using System;
using System.Collections.Generic;
using Traffy.Objects;
namespace Traffy.Objects
{

    public class TrTraceback
    {
        public string funcname;
        public Metadata metadata;
        public int[] mini_traceback;
        public TrExceptionBase cause;

    }
}