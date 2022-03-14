using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Models
{
    public class Person
    {
        public int Id { get; set; }
        public PersonType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal YearlyCostOfBenefit { get; set; }
    }

    public enum PersonType { 
        Employee,
        Dependent
    }
}
