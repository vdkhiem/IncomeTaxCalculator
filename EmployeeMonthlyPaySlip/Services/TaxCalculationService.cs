using System;
using EmployeeMonthlyPaySlip.Repositories.Interfaces;
using EmployeeMonthlyPaySlip.Services.Interfaces;

namespace EmployeeMonthlyPaySlip.Services
{
	public class TaxCalculationService : ITaxCalculationService
	{
		public ITaxableRepository _taxableRepository;

		public TaxCalculationService(ITaxableRepository taxableRepository)
		{
			_taxableRepository = taxableRepository;
		}

		public double CalculateIncomeTax(double annualSalary)
		{
			double result = 0;

			if (annualSalary < 0)
				return result;

			var incomeTaxes = _taxableRepository.GetIncomeTaxList();
			for (int i = 0; i < incomeTaxes.Count; i++)
			{
				if (annualSalary >= incomeTaxes[i].MinRange && annualSalary <= incomeTaxes[i].MaxRange)
				{
					result = incomeTaxes[i].MinPayableTax +
								incomeTaxes[i].ExtraTax * (annualSalary - (incomeTaxes[i].MinRange - 1));
					result = Math.Round(result / 12);
					break;
				}
			}

			return result;
		}


	}
}
