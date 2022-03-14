using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Models
{
    public class Request<T> : IRequest<T>
    {
        public T Data { get; set; }
    }
}
