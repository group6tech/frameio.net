using System;
using Frameio.NET.Models;

namespace Frameio.NET
{
    public class FrameioException : Exception
    {
        public int Code { get; }

        public Error[] Errors { get; }

        public FrameioException(int code, Error[] errors, string message) : base(message)
        {
            Code = code;
            Errors = errors;
        }

    }
}
