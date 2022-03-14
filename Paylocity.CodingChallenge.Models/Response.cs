using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Models
{
    public class Response<T> : IResponse<T>
    {
        public T Data { get; set; }
        //public IDictionary<int, string> Errors { get; set; }
    }
}
