using System;
using Newtonsoft.Json.Linq;

namespace TIK.Core.Exceptions
{
    public class SelfException : Exception
    {
        public string ErrorCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public SelfException(string errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public SelfException(string errorCode, string message) 
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public SelfException(string errorCode, Exception inner) 
            : base(inner.ToString(), inner)
        {
            ErrorCode = errorCode;
        } 

        public SelfException(string errorCode, JObject errorObject) 
            : this(errorCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
    }
}
