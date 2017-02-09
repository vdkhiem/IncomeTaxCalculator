using System.Collections.Generic;
using EmployeeMonthlyPaySlip.Entities;
using EmployeeMonthlyPaySlip.Repositories.Interfaces;

namespace EmployeeMonthlyPaySlip.Repositories
{
	public class TaxableRepository : ITaxableRepository
	{
		public List<IncomeTax> GetIncomeTaxList()
		{
			var incomeTaxes = new List<IncomeTax>();
			incomeTaxes.Add(new IncomeTax() { MinRange = 0, MaxRange = 18200, MinPayableTax = 0, ExtraTax = 0 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 18201, MaxRange = 37000, MinPayableTax = 0, ExtraTax = 0.19 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 37001, MaxRange = 80000, MinPayableTax = 3572, ExtraTax = 0.325 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 80001, MaxRange = 180000, MinPayableTax = 17547, ExtraTax = 0.37 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 180001, MaxRange = int.MaxValue, MinPayableTax = 54547, ExtraTax = 0.45 });
			return incomeTaxes;
		}
	}
}
