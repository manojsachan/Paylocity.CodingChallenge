using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Models
{
    public class Employee : Person
    {
        public decimal TotalCostOfBenefits { get; set; }
        public decimal AnualSalary { get; set; }
        public List<Dependent> Dependents { get; set; }
    }
}
