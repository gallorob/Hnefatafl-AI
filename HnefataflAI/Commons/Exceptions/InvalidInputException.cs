using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HnefataflAI.Commons.Exceptions
{
    class InvalidInputException : Exception
    {
        public InvalidInputException(string cause)
               : base(String.Format("Invalid input: {0}", cause)) { }
    }
}
