using System;

namespace indice.Edi
{
    public class LengthLimitExceededException : Exception
    {
        public LengthLimitExceededException(){ }
        public LengthLimitExceededException(string message) : base(message) { }

        public LengthLimitExceededException(string message, Exception exp) : base(message, exp) { }
    }
}
