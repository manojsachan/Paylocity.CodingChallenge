using Paylocity.CodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paylocity.CodingChallenge.DataAccess
{
    public static class EmployeeDataList
    {
        private static readonly List<Employee> employees = new List<Employee>()
        {
            //new Employee() { Id = 1,  FirstName = "Manoj", LastName = "Sachan", AnualSalary = 2000 * 26, Dependents = new List<Person>(){ new Person() { FirstName = "Kia", LastName = "Sachan" } }  }
        };

        public static List<Employee> GetEmployees()
        {
            return employees;
        }

        public static Employee GetEmployee(int empId)
        {
            Employee employee = employees.FirstOrDefault(x => x.Id == empId);
            if (employee == null) { throw new ArgumentException($"Invalid employee Id {empId}"); }
            return employee;
        }

        public static int AddEmployee(Employee employee)
        {
            int empID = employees.Count + 1;
            employee.Id = empID;
            employees.Add(employee);
            return empID;
        }

        public static int AddDependent(Dependent dependent)
        {
            Employee employee = GetEmployee(dependent.EmployeeId);

            if (employee.Dependents == null) { employee.Dependents = new List<Dependent>(); }
            int id = employee.Dependents.Count + 1;
            dependent.Id = id;
            employee.Dependents.Add(dependent);
            return id;
        }
    }
}
