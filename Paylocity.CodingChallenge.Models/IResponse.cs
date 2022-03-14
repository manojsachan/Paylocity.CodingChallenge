using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Models
{
    public interface IResponse<T>
    {
        T Data { get; set; }
    }
}
