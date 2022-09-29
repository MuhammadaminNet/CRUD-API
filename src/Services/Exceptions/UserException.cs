using System;

namespace Services.Exceptions
{
    public class UserException : Exception
    {
        public int Code { get; set; }
        public UserException(int code, string massage) : base(massage)
        {
            Code = code;
        }
    }
}
