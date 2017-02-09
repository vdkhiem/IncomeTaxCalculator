using System.Collections.Generic;
using EmployeeMonthlyPaySlip.Entities;

namespace EmployeeMonthlyPaySlip.Repositories.Interfaces
{
	public interface ITaxableRepository
	{
		List<IncomeTax> GetIncomeTaxList();
	}
}
