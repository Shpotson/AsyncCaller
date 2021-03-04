using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncCaller
{

    [Serializable]
    public class AsyncCallerException : Exception
    {
        public AsyncCallerException() { }
        public AsyncCallerException(string message) : base(message) { }
        public AsyncCallerException(string message, Exception inner) : base(message, inner) { }
        protected AsyncCallerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
