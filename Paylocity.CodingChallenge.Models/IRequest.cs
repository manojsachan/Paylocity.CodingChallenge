using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Models
{
    public interface IRequest<T>
    {
        T Data { get; set; }
    }
}
