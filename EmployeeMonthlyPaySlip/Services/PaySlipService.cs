using System;
using EmployeeMonthlyPaySlip.Entities;
using EmployeeMonthlyPaySlip.Services.Interfaces;
using EmployeeMonthlyPaySlip.Exceptions;

namespace EmployeeMonthlyPaySlip.Services
{
	public class PaySlipService : IPaySlipService
	{
		private ITaxCalculationService _taxCalculationService;

		public PaySlipService(ITaxCalculationService taxCalculationService)
		{
			_taxCalculationService = taxCalculationService;
		}

		public PaySlip GetPaySlip(EmployeeInfo employeeInfo)
		{
			ValidateEmployeeInfo(employeeInfo);
			var result = new PaySlip()
			{
				FirstName = employeeInfo.FirstName,
				LastName = employeeInfo.LastName,
				AnnualSalary = employeeInfo.AnnualSalary,
				SuperRate = employeeInfo.SuperRate,
				PayPeriod = employeeInfo.PayPeriod
			};
			result.IncomeTax = (int)_taxCalculationService.CalculateIncomeTax(employeeInfo.AnnualSalary);
			result.NetIncome = result.GrossIncome - result.IncomeTax;
			result.Super = (int)Math.Round(result.GrossIncome * double.Parse(result.SuperRate.Substring(0, result.SuperRate.IndexOf('%'))) / 100);

			return result;
		}

		private void ValidateEmployeeInfo(EmployeeInfo employeeInfo)
		{
			if (employeeInfo.AnnualSalary < 0)
			{
				throw new NegativeAnnualSalaryException("Annual Salary cannot be negative");
			}
			var rate = double.Parse(employeeInfo.SuperRate.Substring(0, employeeInfo.SuperRate.IndexOf('%')));
			if (rate < 0 || rate > 50)
			{
				throw new InvalidSuperRateException("Supper rate must between 0 and 50");
			}
		}
	}
}
