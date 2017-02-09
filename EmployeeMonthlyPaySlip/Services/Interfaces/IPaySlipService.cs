
using EmployeeMonthlyPaySlip.Entities;

namespace EmployeeMonthlyPaySlip.Services.Interfaces
{
	public interface IPaySlipService
	{
		PaySlip GetPaySlip(EmployeeInfo employeeInfo);
	}
}
