using System;

namespace CPTech
{
    public class NetException : Exception
    {
        public int Code { get; set; } = -1;

        public NetException(string message) : base(message)
        {
        }

        public NetException(int code, string message) : base(message)
        {
            this.Code = code;
        }
    }
}
