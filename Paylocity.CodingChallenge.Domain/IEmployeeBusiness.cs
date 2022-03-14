using Paylocity.CodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Domain
{
    public interface IEmployeeBusiness
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee AddDependent(Dependent dependent);
    }
}
