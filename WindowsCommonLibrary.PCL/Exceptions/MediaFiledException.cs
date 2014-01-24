using System;

namespace WindowsCommonLibrary.PCL.Exceptions
{
    public class MediaFiledException : Exception
    {
        public MediaFiledException()
        {

        }

        public MediaFiledException(string message) : base(message)
        {

        }
    }
}
