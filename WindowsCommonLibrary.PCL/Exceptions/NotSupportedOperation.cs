using System;

namespace WindowsCommonLibrary.PCL.Exceptions
{
    public class NotSupportedOperation : Exception
    {
        public NotSupportedOperation()
        {

        }

        public NotSupportedOperation(string message) : base(message)
        {

        }
    }
}
