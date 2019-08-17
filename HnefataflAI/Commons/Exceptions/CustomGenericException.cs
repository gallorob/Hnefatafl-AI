using System;

namespace HnefataflAI.Commons.Exceptions
{
    class CustomGenericException : Exception
    {
        public CustomGenericException(string sender, string method, string cause)
            : base(String.Format("[{0}.{1}]\n{2}", sender, method, cause)) { }
    }
}
