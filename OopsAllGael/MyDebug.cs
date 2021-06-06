using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopsAllGael
{
    static class MyDebug
    {
        public class AssertException : Exception
        {
            public AssertException()
            {
            }

            public AssertException(string message)
                : base(message)
            {
            }

            public AssertException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public static void Assert(bool expression)
        {
            if (!expression)
            {
                throw new AssertException();
            }
        }

        public static void Fail()
        {
            throw new AssertException();
        }
    }
}
