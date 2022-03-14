using Microsoft.Extensions.Configuration;
using Paylocity.CodingChallenge.DataAccess;
using Paylocity.CodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paylocity.CodingChallenge.Domain
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IConfiguration _configuration;

        public EmployeeBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Employee> GetEmployees()
        {
            return EmployeeDataList.GetEmployees();
        }

        public Employee GetEmployee(int employeeId)
        {
            Employee employee = EmployeeDataList.GetEmployee(employeeId);
            employee.AnualSalary = _configuration.GetValue<decimal>("EmployeeSalaryPerPayCheck") * _configuration.GetValue<int>("TotalPayChecksPerYear");
            employee.CalcualteCostOfBenefits(_configuration);
            return employee;
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Type = PersonType.Employee;
            int empId = EmployeeDataList.AddEmployee(employee);
            return GetEmployee(empId);
        }

        public Employee AddDependent(Dependent dependent)
        {
            dependent.Type = PersonType.Dependent;
            EmployeeDataList.AddDependent(dependent);
            return GetEmployee(dependent.EmployeeId);
        }
    }

    internal static class EmployeeBusinessExtension {
        internal static void CalcualteCostOfBenefits(this Employee employee, IConfiguration configuration)
        {
            IConfigurationSection deductionRulesSection = configuration.GetSection("DeductionRules");
            decimal employeeCostOfBenefit = deductionRulesSection.GetValue<decimal>("EmployeeCostOfBenefit");
            decimal dependentCostOfBenefit = deductionRulesSection.GetValue<decimal>("DependentCostOfBenefit");
            IConfigurationSection discountSection = deductionRulesSection.GetSection("Discount");
            decimal discountPercenttage = discountSection.GetValue<decimal>("Percentage");
            string discountAppliedTo = discountSection.GetValue<string>("AppliedTo");
            string discountNameStartsWith = discountSection.GetValue<string>("NameStartsWith");

            employee.YearlyCostOfBenefit = employeeCostOfBenefit;
            employee.ApplyDiscount(discountPercenttage, discountAppliedTo, discountNameStartsWith);

            decimal totalCostOfBenefits = employee.YearlyCostOfBenefit;
            employee.Dependents?.ForEach(dependent => {
                dependent.YearlyCostOfBenefit = dependentCostOfBenefit;
                dependent.ApplyDiscount(discountPercenttage, discountAppliedTo, discountNameStartsWith);
                totalCostOfBenefits += dependent.YearlyCostOfBenefit;
            });

            employee.TotalCostOfBenefits = totalCostOfBenefits;
        }

        private static void ApplyDiscount(this Person person, decimal discountPercenttage, string discountAppliedTo, string discountNameStartsWith)
        {
            if (!person.FirstName.StartsWith(discountNameStartsWith, StringComparison.OrdinalIgnoreCase)) { return; }
            if (!discountAppliedTo.Contains(person.Type.ToString())) { return; }
            person.YearlyCostOfBenefit -= ((person.YearlyCostOfBenefit * discountPercenttage) / 100);
        }
    }
}
